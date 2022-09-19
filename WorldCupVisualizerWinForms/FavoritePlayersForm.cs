using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DataLayer;
using DataLayer.Models;
using DataLayer.Services;
using WorldCupVisualizerWinForms.Properties;

namespace WorldCupVisualizerWinForms
{
    public partial class FavoritePlayersForm : Form
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ImageManager _imageManager = ImageManager.Instance;
        private const string FavoritePlayersFileName = "favorite_players.txt";
        private const string FavoriteTeamFileName = "favorite_team.txt";
        private const string FileDirectory = @"../../../DataLayer/Files/";

        private List<PlayerModel> allPlayers = new List<PlayerModel>();
        private List<PlayerModel> favoritePlayers = new List<PlayerModel>();

        private List<PlayerUserControl> selectedPlayerUserControls = new List<PlayerUserControl>();

        public FavoritePlayersForm()
        {
            //Following line is a bit hacky because ideally injection would be over constructor
            //but i couldn't figure out how to make this happen in winforms on this .net version
            _matchRepository = (IMatchRepository)Program.ServiceProvider.GetService(typeof(IMatchRepository));
            InitializeComponent();
            FillData();
        }

        private async void FillData()
        {
            FillAllPlayersFromRepository();
        }

        private async void FillAllPlayersFromRepository()
        {
            var players = await _matchRepository.GetPlayersFor(ReadFifaCode());
            if (players.Count == 0)
            {
                MessageBox.Show("Wanted league doesnt contain your favorite team...", "Error", MessageBoxButtons.OK);
                return;
            }

            allPlayers.AddRange(players);
            FillFavoritePlayersFromFile();
            RefreshPlayerUserControls(allPlayers, allPlayersPanel, false);
        }

        private void FillFavoritePlayersFromFile()
        {
            string filePath = FileDirectory + FavoritePlayersFileName;
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No favorite players file found...");
                return;
            }

            string[] playerNames = File.ReadAllLines(filePath);
            foreach (string playerName in playerNames)
            {
                favoritePlayers.Add(allPlayers.Find(player => player.Name == playerName));
            }

            if (favoritePlayers
                .Contains(null)) // If favorite player text file contained faulty player, it will result in a null value in this list.
            {
                System.Diagnostics.Debug.WriteLine(
                    "Favorite players contained faulty player.. removing favorite players file");
                File.Delete(FileDirectory + FavoritePlayersFileName);
                return;
            }

            RefreshPlayerUserControls(favoritePlayers, favoritePlayersPanel, true);
        }

        private void RefreshPlayerUserControls(List<PlayerModel> players, FlowLayoutPanel panel, bool isFavoritePanel)
        {
            panel.Controls.Clear();
            players.ForEach(player =>
            {
                PlayerUserControl userControl = new PlayerUserControl()
                {
                    Name = player.Name,
                    Captain = player.Captain ? "Captain" : "Player",
                    Number = player.ShirtNumber.ToString(),
                    Position = player.Position
                };
                userControl.pictureBox1.Image = Image.FromFile(_imageManager.GetUriForPlayerImage(player.Name));
                userControl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                //Setup context menu
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add("Change image",
                    (sender, args) =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                        _imageManager.SaveImageForPlayer(player.Name, openFileDialog.FileName);

                        //recursively refresh both panels
                        RefreshPlayerUserControls(allPlayers, allPlayersPanel, false);
                        RefreshPlayerUserControls(favoritePlayers, favoritePlayersPanel, true);
                    });

                if (!isFavoritePanel) // If we are setting a NOT favorite panel, we want the Add button, else we want a Remove button;
                {
                    userControl.MouseDown += InitializeMouseDown;
                    contextMenu.MenuItems.Add("Add",
                        (sender, args) =>
                        {
                            favoritePlayersPanel_DragDrop(sender, null);
                        }); // we just mimic like we did a drag and drop with the selected user contrrol
                    userControl.ContextMenu = contextMenu;
                }
                else
                {
                    contextMenu.MenuItems.Add("Remove",
                        (sender, args) =>
                        {
                            favoritePlayers.Remove(allPlayers.Find(p => p.Name == player.Name));
                            RefreshPlayerUserControls(favoritePlayers, favoritePlayersPanel,
                                true); // recursively rebuild new favorite player controls.
                        });
                    userControl.ContextMenu = contextMenu;
                }

                panel.Controls.Add(userControl);
            });
        }

        private void InitializeMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PlayerUserControl chosenUserControl = sender as PlayerUserControl;
                if (selectedPlayerUserControls.Contains(chosenUserControl))
                {
                    chosenUserControl.BackColor = Color.Transparent;
                    selectedPlayerUserControls.Remove(chosenUserControl);
                }
                else
                {
                    chosenUserControl.BackColor = Color.LightBlue;
                    selectedPlayerUserControls.Add(chosenUserControl);
                }

                chosenUserControl.DoDragDrop(chosenUserControl, DragDropEffects.Copy);
            }
        }

        private string ReadFifaCode()
        {
            return File.ReadAllText(FileDirectory + FavoriteTeamFileName);
        }

        private void favoritePlayersPanel_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Copy;

        private void favoritePlayersPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (favoritePlayers.Count >= 3)
            {
                UnselectPlayerUserControls();
                return;
            }

            var allSelectedNames = selectedPlayerUserControls.Select(control => control.Name);
            var favoritePlayerNames = favoritePlayers.Select(player => player.Name);
            var intersection = allSelectedNames.Intersect(favoritePlayerNames);
            var toRemove = selectedPlayerUserControls.Where(control => intersection.Contains(control.Name));
            selectedPlayerUserControls = selectedPlayerUserControls.Except(toRemove).ToList();

            favoritePlayers.AddRange(allPlayers.FindAll(player =>
                {
                    int maxToTake = 3;
                    // returns either 3 or maximum available slots in favorite players panel
                    int playersToTake = Math.Min(maxToTake, (maxToTake - favoritePlayers.Count));
                    return selectedPlayerUserControls
                        .Take(playersToTake)
                        .Select(control => control.Name)
                        .Contains(player.Name);
                }
            ));
            WriteFavoritePlayersToFile();
            RefreshPlayerUserControls(favoritePlayers, favoritePlayersPanel, true);
            UnselectPlayerUserControls();
        }

        private void WriteFavoritePlayersToFile()
        {
            string filePath = FileDirectory + FavoritePlayersFileName;
            List<string> playersToWrite = favoritePlayers.Select(player => player.Name).ToList();
            File.WriteAllLines(filePath, playersToWrite);
        }

        private void UnselectPlayerUserControls()
        {
            selectedPlayerUserControls.ForEach(control => { control.BackColor = Color.Transparent; });
            selectedPlayerUserControls.Clear();
        }

        private void UnselectPlayerUserControl(PlayerUserControl playerUserControl)
        {
            playerUserControl.BackColor = Color.Transparent;
            selectedPlayerUserControls.Remove(playerUserControl);
        }

        private async void btnHighestScore_Click(object sender, EventArgs e)
        {
            var playersToGoals = await _matchRepository.GetPlayersOrderedByGoals(ReadFifaCode());
            if (playersToGoals.Count == 0)
            {
                MessageBox.Show("Something went wrong during API call.", "Error", MessageBoxButtons.OK);
                return;
            }

            OrderedByForm orderedByForm = new OrderedByForm(playersToGoals, "goal");
            orderedByForm.Show();
        }

        private async void btnYellowCards_Click(object sender, EventArgs e)
        {
            var playersToYellowCards = await _matchRepository.GetPlayersOrderedByYellowCards(ReadFifaCode());
            if (playersToYellowCards.Count == 0)
            {
                MessageBox.Show("Something went wrong during API call.", "Error", MessageBoxButtons.OK);
                return;
            }

            OrderedByForm orderedByForm = new OrderedByForm(playersToYellowCards, "yellow-card");
            orderedByForm.Show();
        }

        private async void btnAttendance_Click(object sender, EventArgs e)
        {
            var matchesByAttendance = await _matchRepository.GetMatchesOrderedByVisitors(ReadFifaCode());
            if (matchesByAttendance.Count == 0)
            {
                MessageBox.Show("Something went wrong during API call.", "Error", MessageBoxButtons.OK);
                return;
            }

            OrderedByForm orderedByForm = new OrderedByForm(matchesByAttendance);
            orderedByForm.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void FavoritePlayersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to close the application?",
                "Exit",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                this.Dispose();
                var allOpenForms = Application.OpenForms;
                foreach (Form form in allOpenForms)
                {
                    form.Dispose();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
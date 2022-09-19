using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;
using DataLayer.Models;
using DataLayer.Services;

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ConfigManager _configManager = ConfigManager.Instance;
        private readonly string FileDirectory = @"../../../DataLayer/Files/";
        private readonly string FavoriteTeamFileName = "favorite_team.txt";

        // These are mappings of different player positions to respective columns in the grid view.
        private readonly Dictionary<string, int> firstTeamColumns = new Dictionary<string, int>()
        {
            { "Goalie", 0 },
            { "Defender", 1 },
            { "Midfield", 2 },
            { "Forward", 3 },
        };

        private readonly Dictionary<string, int> secondTeamColumns = new Dictionary<string, int>()
        {
            { "Goalie", 9 },
            { "Defender", 8 },
            { "Midfield", 7 },
            { "Forward", 6 },
        };

        private CountryModel firstTeam;
        private CountryModel secondTeam;
        private MatchModel selectedMatch;
        private List<MatchModel> allMatches;


        // Dependency injected Interfaces for DataLayer access
        public MainWindow(IMatchRepository matchRepository, ICountryRepository countryRepository)
        {
            this._matchRepository = matchRepository;
            this._countryRepository = countryRepository;

            SetLocalization();
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            SetLocalization();
            SetScreenSize();
            InitializeFirstTeam();
        }

        private void SetScreenSize()
        {
            switch (_configManager.GetResolution())
            {
                case "FullScreen":
                    WindowState = WindowState.Maximized;
                    break;
                case "Size480p":
                    Height = 480;
                    Width = 720;
                    break;
                case "Size576p":
                    Height = 576;
                    Width = 720;
                    break;
                case "Size720p":
                    Height = 720;
                    Width = 1280;
                    break;
                case "Size1080p":
                    Height = 1080;
                    Width = 1920;
                    break;
                default:
                    Height = 720;
                    Width = 1280;
                    break;
            }
        }

        private async void InitializeFirstTeam()
        {
            var countries = await _countryRepository.GetCountries();
            var country = countries.FirstOrDefault(c => c.FifaCode == ReadFavoriteTeamFifaCode());
            if (country == null)
            {
                country = countries.First();
            }

            cbFirstTeam.ItemsSource = countries;
            cbFirstTeam.SelectedItem = country;
            firstTeam = cbFirstTeam.SelectedItem as CountryModel;
            InitializeSecondTeam();
        }

        private async void InitializeSecondTeam()
        {
            cbSecondTeam.ItemsSource = await _matchRepository.GetOpponentsForFifaCode(firstTeam?.FifaCode);
            cbSecondTeam.SelectedIndex = 0;
            secondTeam = await _countryRepository.GetCountryForFifaCode((cbSecondTeam.SelectedItem as TeamModel)?.Code);
            allMatches = await GetAllMatchesFor(firstTeam.FifaCode);
            SelectCurrentMatch(allMatches);
            FillScoreBoard();
            FieldSetup();
        }

        private void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayerUserControl panel = sender as PlayerUserControl;
            new PlayerDetailsWindow(panel.Player, panel.Match).ShowDialog();
        }

        private async void cbFirstTeam_DropDownClosed(object sender, EventArgs e)
        {
            var newlySelectedTeam = cbFirstTeam.SelectedItem as CountryModel;
            if (firstTeam == newlySelectedTeam)
            {
                return;
            }

            firstTeam = newlySelectedTeam;
            InitializeSecondTeam();
        }

        private async void cbSecondTeam_DropDownClosed(object sender, EventArgs e)
        {
            if (secondTeam == null)
            {
                return;
            }

            //handle case where team wasnt actually changed
            if (secondTeam.FifaCode == (cbSecondTeam.SelectedItem as TeamModel)?.Code)
            {
                return;
            }

            secondTeam = await _countryRepository.GetCountryForFifaCode((cbSecondTeam.SelectedItem as TeamModel)?.Code);
            allMatches = await GetAllMatchesFor(firstTeam?.FifaCode);
            SelectCurrentMatch(allMatches);
            FillScoreBoard();
            FieldSetup();
        }

        private async Task<List<MatchModel>> GetAllMatchesFor(string fifaCode)
        {
            var matches = await _matchRepository.GetMatchesForFifaCode(fifaCode);
            if (matches.Count == 0)
            {
                MessageBox.Show(Properties.Resources.emptyResultFromBackend, "Error", MessageBoxButton.OK);
                return null;
            }

            return matches;
        }

        private void SelectCurrentMatch(List<MatchModel> matches)
        {
            if (matches == null)
            {
                return;
            }

            selectedMatch = matches.FirstOrDefault(m =>
                (m.HomeTeam.Code == firstTeam.FifaCode && m.AwayTeam.Code == secondTeam.FifaCode) ||
                (m.HomeTeam.Code == secondTeam.FifaCode && m.AwayTeam.Code == firstTeam.FifaCode));
        }

        private void FillScoreBoard()
        {
            if (selectedMatch == null)
            {
                return;
            }

            lbScore.Content = selectedMatch.HomeTeam.Code == firstTeam.FifaCode
                ? $"{selectedMatch?.HomeTeam.Goals} vs {selectedMatch?.AwayTeam.Goals}"
                : $"{selectedMatch?.AwayTeam.Goals} vs {selectedMatch?.HomeTeam.Goals}";
        }

        private void FieldSetup()
        {
            if (selectedMatch == null)
            {
                return;
            }

            ClearPreviousField();
            List<PlayerModel> firstTeamPlayers = selectedMatch.HomeTeamStatistics.StartingEleven;
            List<PlayerModel> secondTeamPlayers = selectedMatch.AwayTeamStatistics.StartingEleven;
            SetupTeamForMapping(firstTeamPlayers, firstTeamColumns);
            SetupTeamForMapping(secondTeamPlayers, secondTeamColumns);
        }

        private void ClearPreviousField()
        {
            IEnumerable<StackPanel> stackPanels = fieldGrid.Children.Cast<StackPanel>();
            stackPanels.ToList().ForEach(p => p.Children.Clear());
        }

        private void SetupTeamForMapping(List<PlayerModel> players, Dictionary<string, int> mappings)
        {
            var goalie = players.Find(p => p.Position == "Goalie");
            var defenders = players.FindAll(p => p.Position == "Defender");
            var midfields = players.FindAll(p => p.Position == "Midfield");
            var forwards = players.FindAll(p => p.Position == "Forward");

            PlacePlayer(goalie, mappings["Goalie"]);
            PlacePlayers(defenders, mappings["Defender"]);
            PlacePlayers(midfields, mappings["Midfield"]);
            PlacePlayers(forwards, mappings["Forward"]);
        }

        private void PlacePlayer(PlayerModel player, int column)
        {
            PlayerUserControl control = new PlayerUserControl(player, selectedMatch);
            control.MouseDoubleClick += Control_MouseDoubleClick;
            StackPanel stackPanel =
                fieldGrid.Children.Cast<StackPanel>().FirstOrDefault(i => Grid.GetColumn(i) == column);
            stackPanel.Children.Add(control);
        }

        private void PlacePlayers(List<PlayerModel> players, int column)
        {
            players.ForEach(player => { PlacePlayer(player, column); });
        }

        private async void Firstbtn_Animation_Completed(object sender, EventArgs e)
        {
            var country = await _countryRepository.GetCountryForFifaCode(firstTeam.FifaCode);
            new TeamDetailsWindow(country).ShowDialog();
        }

        private async void Secondbtn_Animation_Completed(object sender, EventArgs e)
        {
            var country = await _countryRepository.GetCountryForFifaCode(secondTeam.FifaCode);
            new TeamDetailsWindow(country).ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.appClosing, "Close Application", MessageBoxButton.OKCancel) !=
                MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void ctxItemSettings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
            SetLocalization();
            SetScreenSize();
        }

        private void SetLocalization()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(_configManager.GetLanguage());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_configManager.GetLanguage());
        }

        private string ReadFavoriteTeamFifaCode()
        {
            if (!File.Exists(FileDirectory + FavoriteTeamFileName))
            {
                return null;
            }

            return File.ReadAllText(FileDirectory + FavoriteTeamFileName);
        }
    }
}
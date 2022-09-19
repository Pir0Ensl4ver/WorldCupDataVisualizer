using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using DataLayer.Models;
using DataLayer.Services;
using WorldCupVisualizerWinForms.Properties;

namespace WorldCupVisualizerWinForms
{
    public partial class OrderedByForm : Form
    {
        private readonly ImageManager _imageManager = ImageManager.Instance;
        private Dictionary<PlayerModel, int> orderedPlayers;
        private List<MatchModel> venuesByAttendance;
        private String eventType;

        private int printMargin = 5;

        public OrderedByForm(Dictionary<PlayerModel, int> orderedPlayers, String eventType)
        {
            this.orderedPlayers = orderedPlayers;
            this.eventType = eventType;
            InitializeComponent();
            FillPlayersInPanel();
        }

        public OrderedByForm(List<MatchModel> venuesByAttendance)
        {
            InitializeComponent();
            this.venuesByAttendance = venuesByAttendance;
            FillVenuesInPanel();
        }

        private void FillVenuesInPanel()
        {
            foreach (var match in venuesByAttendance)
            {
                VenueUserControl userControl = new VenueUserControl();
                userControl.Location = match.Location;
                userControl.Attendance = match.Attendance.ToString();
                userControl.HomeTeam = match.HomeTeam.Code;
                userControl.AwayTeam = match.AwayTeam.Code;
                sortedPanel.Controls.Add(userControl);
            }
        }

        private void FillPlayersInPanel()
        {
            foreach (var playerToCount in orderedPlayers)
            {
                PlayerUserControl
                    userControl =
                        new PlayerUserControl(); // a bit hacky to reuse this user control, since we dont need to show all data.
                userControl.Name = playerToCount.Key.Name;
                userControl.Number = GetLabelTextForEvent() + playerToCount.Value;
                userControl.Captain = ""; // we dont care about this. we just want number of goals/yellow-cards
                userControl.Position = ""; // we dont care about this. we just want number of goals/yellow-cards
                userControl.pictureBox1.Image =
                    Image.FromFile(_imageManager.GetUriForPlayerImage(playerToCount.Key.Name));
                userControl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                sortedPanel.Controls.Add(userControl);
            }
        }

        private String GetLabelTextForEvent()
        {
            switch (eventType)
            {
                case "goal": return "Goals: ";
                case "yellow-card": return "Yellow cards: ";
                default: return "";
            }
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (orderedPlayers != null)
            {
                PrintPlayers(sender, e);
            }
            else if (venuesByAttendance != null)
            {
                PrintVenues(sender, e);
            }
        }

        private void PrintPlayers(object sender, PrintPageEventArgs e)
        {
            int x = 20;
            int y = 25;
            int imageDimensions = 50;
            Font font = new Font("Arial", 5);

            e.Graphics.DrawString(GetPageTitle(), font, Brushes.Black, x, 5);

            foreach (var playerToEvent in orderedPlayers)
            {
                int localY = y;
                PlayerModel player = playerToEvent.Key;
                int eventCount = playerToEvent.Value;
                Rectangle rectangle = new Rectangle(x, y, imageDimensions, imageDimensions);
                e.Graphics.DrawImage(Image.FromFile(_imageManager.GetUriForPlayerImage(player.Name)), rectangle);
                e.Graphics.DrawString(player.Name, font, Brushes.Black, x + imageDimensions + printMargin, localY);
                localY += font.Height + printMargin;
                e.Graphics.DrawString($"{GetLabelTextForEvent()} {eventCount.ToString()}", font, Brushes.Black,
                    x + imageDimensions + printMargin, localY);
                y += imageDimensions + printMargin;
                if (y + imageDimensions > e.PageBounds.Bottom)
                {
                    x += 300;
                    y = 25;
                }
            }

            e.HasMorePages = false;
        }

        private void PrintVenues(object sender, PrintPageEventArgs e)
        {
        }

        private string GetPageTitle()
        {
            return eventType switch
            {
                "goal" => "Players ordered by goals:",
                "yellow-card" => "Players ordered by yellow cards:",
                _ => "Venues ordered by attendance:"
            };
        }

        private void printSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }
    }
}
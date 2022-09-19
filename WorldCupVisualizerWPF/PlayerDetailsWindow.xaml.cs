using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer.Models;
using DataLayer.Services;

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for PlayerDetailsWindow.xaml
    /// </summary>
    public partial class PlayerDetailsWindow : Window
    {
        private readonly ImageManager _imageManager = ImageManager.Instance;

        public PlayerDetailsWindow(PlayerModel player, MatchModel match)
        {
            InitializeComponent();
            imgPlayer.Source = new BitmapImage(new Uri(_imageManager.GetUriForPlayerImage(player.Name)));
            lbName.Content = $"{Properties.Resources.nameText}: {player.Name}";
            lbNumber.Content = $"{Properties.Resources.shirtNumberText}: {player.ShirtNumber}";
            lbPosition.Content = $"{Properties.Resources.positionText}: {player.Position}";
            lbYellowCards.Content =
                $"{Properties.Resources.yellowCardText}: {GetEventCountForPlayer(player, match, "yellow-card")}";
            int goals = Int32.Parse(GetEventCountForPlayer(player, match, "goal"));
            int penaltyGoals = Int32.Parse(GetEventCountForPlayer(player, match, "goal-penalty"));
            lbGoals.Content = $"{Properties.Resources.goalsText}: {goals + penaltyGoals}";
            var status = player.Captain ? "Captain" : "Player";
            lbPlayer.Content = $"{Properties.Resources.statusText}: {status}";
        }

        private string GetEventCountForPlayer(PlayerModel player, MatchModel match, string eventType)
        {
            // A bit hacky because we check for both teams and add them together
            // reasoning is that player wont exist in opposing team so no events for that player in opposing team.
            // meaning it will alway be 0 events in opposing team events so it wont affect the result
            
            var awayTeamEventCount =
                match.AwayTeamEvents.Count(e => e.Player == player.Name && e.TypeOfEvent == eventType);
            var homeTeamEventCount =
                match.HomeTeamEvents.Count(e => e.Player == player.Name && e.TypeOfEvent == eventType);
            return (awayTeamEventCount + homeTeamEventCount).ToString();
        }
    }
}
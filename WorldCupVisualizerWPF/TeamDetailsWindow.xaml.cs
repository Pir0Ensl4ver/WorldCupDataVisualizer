using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for TeamDetailsWindow.xaml
    /// </summary>
    public partial class TeamDetailsWindow : Window
    {
        public TeamDetailsWindow(CountryModel country)
        {
            InitializeComponent();
            this.lbFifaCode.Content = $"{Properties.Resources.fifaCode}: {country.FifaCode}";
            this.lbName.Content = $"{Properties.Resources.nameText}: {country.Country}";
            this.lbWon.Content = $"{Properties.Resources.wonText}: {country.Wins}";
            this.lbLost.Content = $"{Properties.Resources.lostText}: {country.Losses}";
            this.lbPlayed.Content = $"{Properties.Resources.playedText}: {country.GamesPlayed}";
            this.lbUndecided.Content = $"{Properties.Resources.undecidedText}: {country.Draws}";
        }
    }
}
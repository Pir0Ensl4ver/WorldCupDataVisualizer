using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
using DataLayer.Models;
using DataLayer.Services;

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl : UserControl
    {
        private readonly ImageManager _imageManager = ImageManager.Instance;
        
        public PlayerModel Player { get; set; }
        public MatchModel Match { get; set; }
        
        public PlayerUserControl(PlayerModel player, MatchModel match)
        {
            InitializeComponent();
            Player = player;
            Match = match;
            Setup();
        }

        private void Setup()
        {
            lbName.Content = Player.Name;
            imgPlayer.Source = new BitmapImage(new Uri(_imageManager.GetUriForPlayerImage(Player.Name), UriKind.RelativeOrAbsolute));
            imgPlayer.Stretch = Stretch.UniformToFill;
        }

    }
}

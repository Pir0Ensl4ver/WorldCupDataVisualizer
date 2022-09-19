using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
using DataLayer.Services;

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly ConfigManager _configManager = ConfigManager.Instance;

        private enum ScreenSizeType
        {
            FullScreen,
            Size480p,
            Size576p,
            Size720p,
            Size1080p
        }

        public SettingsWindow()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            cbLanguage.DisplayMemberPath = "Key";
            AttachDataSourceToComboBox(cbLanguage, Languages.LanguageDictionary);
            AttachDataSourceToComboBox(cbChampionshipType, Enum.GetValues(typeof(League)));
            AttachDataSourceToComboBox(cbScreenSize, Enum.GetValues(typeof(ScreenSizeType)));
            cbLanguage.SelectedIndex = 0;
            cbChampionshipType.SelectedIndex = 0;
            cbScreenSize.SelectedIndex = 0;
        }

        private void AttachDataSourceToComboBox(ComboBox comboBox, IEnumerable dataSource)
        {
            comboBox.ItemsSource = dataSource;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.confirmationMessage, "Application Settings",
                    MessageBoxButton.OKCancel) ==
                MessageBoxResult.OK)
            {
                //Language
                var chosenLanguage = ((KeyValuePair<string, string>)cbLanguage.SelectedValue).Value;
                _configManager.SetLanguage(chosenLanguage);
                //League
                _configManager.SetLeague(cbChampionshipType.SelectedItem.ToString());
                //Resolution
                _configManager.SetResolution(cbScreenSize.SelectedItem.ToString());
                Close();
            }
        }
    }
}
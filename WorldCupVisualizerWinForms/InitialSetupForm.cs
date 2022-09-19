using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DataLayer.Models;
using DataLayer.Services;

namespace WorldCupVisualizerWinForms
{
    public partial class InitialSetupForm : Form
    {
        private readonly ConfigManager _configManager = ConfigManager.Instance;

        public InitialSetupForm()
        {
            InitializeComponent();
            Setup();
        }


        private void Setup()
        {
            AttachDataSourceToComboBox(chooseLanguageCB, Languages.LanguageDictionary);
            AttachDataSourceToComboBox(chooseLeagueCB, Enum.GetValues(typeof(League)));
            chooseLanguageCB.SelectedIndex = 0;
            chooseLeagueCB.SelectedIndex = 0;
        }

        private void AttachDataSourceToComboBox(ComboBox comboBox, IEnumerable dataSource)
        {
            comboBox.DataSource = new BindingSource(dataSource, null);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Hide();
            _configManager.SetLanguage(chooseLanguageCB.SelectedValue.ToString());
            _configManager.SetLeague(chooseLeagueCB.SelectedValue.ToString());
            FavoriteCountryForm countryForm = new FavoriteCountryForm();
            countryForm.Show();
        }

        private void chooseLanguageCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = chooseLanguageCB.SelectedValue.ToString();
            _configManager.SetLanguage(selectedLanguage);
            PropagateCultureChange();
        }

        private void chooseLeagueCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLeague = chooseLeagueCB.SelectedValue.ToString();
            _configManager.SetLeague(selectedLeague);
            File.Delete(@"../../../DataLayer/Files/favorite_players.txt");
        }
        
        private void PropagateCultureChange()
        {
            Thread.CurrentThread.CurrentCulture =
                CultureInfo.GetCultureInfo(_configManager.GetLanguage());
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(_configManager.GetLanguage());
        }

    }
}
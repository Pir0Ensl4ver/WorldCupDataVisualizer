using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DataLayer.Models;
using DataLayer.Services;

namespace WorldCupVisualizerWinForms
{
    public partial class SettingsForm : Form
    {
        private readonly ConfigManager _configManager = ConfigManager.Instance;

        public SettingsForm()
        {
            InitializeComponent();
            AttachDataSourceToComboBox(chooseLanguageCB, Languages.LanguageDictionary);
            AttachDataSourceToComboBox(chooseLeagueCB, Enum.GetValues(typeof(League)));
        }

        private void AttachDataSourceToComboBox(ComboBox comboBox, IEnumerable dataSource)
        {
            comboBox.DataSource = new BindingSource(dataSource, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to save this confiuration?",
                "Save",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Setup language
                string selectedLanguage = chooseLanguageCB.SelectedValue.ToString();
                _configManager.SetLanguage(selectedLanguage);

                // Setup league
                string selectedLeague = chooseLeagueCB.SelectedValue.ToString();
                _configManager.SetLeague(selectedLeague);

                PropagateCultureChange();
                File.Delete(@"../../../DataLayer/Files/favorite_team.txt");
            }
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
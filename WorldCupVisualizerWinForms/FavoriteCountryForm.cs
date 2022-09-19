using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repository.Api;

namespace WorldCupVisualizerWinForms
{
    public partial class FavoriteCountryForm : Form
    {
        private readonly ICountryRepository _countryRepository;
        private const string FileDirectory = @"../../../DataLayer/Files/";

        public FavoriteCountryForm()
        {
            _countryRepository = (ICountryRepository)Program.ServiceProvider.GetService(typeof(ICountryRepository));
            InitializeComponent();
            FillTeams();
        }

        private async void FillTeams()
        {
            var countries = await _countryRepository.GetCountries();
            progressBar.Hide();
            chooseFavoriteTeamCB.DataSource = countries;
            chooseFavoriteTeamCB.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            File.Delete(FileDirectory + "favorite_players.txt");
            SaveChosenCountryToFile();

            this.Hide();
            FavoritePlayersForm form = new FavoritePlayersForm();
            form.Show();
        }

        private void SaveChosenCountryToFile()
        {
            string chosenTeam = ((CountryModel)chooseFavoriteTeamCB.SelectedItem).FifaCode;
            File.WriteAllText(FileDirectory + "favorite_team.txt", chosenTeam);
        }
    }
}
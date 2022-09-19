using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using DataLayer.File;
using DataLayer.Repository.Api;
using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WorldCupVisualizerWinForms
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        private static readonly ConfigManager ConfigManager = ConfigManager.Instance;


        /// <summary>
        /// Dependency injection setup
        /// This enables for the application to automatically inject either File or Api respositories
        /// based on the reading_mode.txt file content
        ///
        /// However I couldn't figure out how to have a fully automatic DI in this .net version on WinForms
        /// So usage is visible in the constructor method of FavoritePlayerForm
        ///
        /// We default to API if reading mode is not present
        /// </summary>
        static void ConfigureServices()
        {
            var readingMode = ConfigManager.GetReadingMode();
            var services = new ServiceCollection();
            if (readingMode != null && readingMode == "file")
            {
                services.AddTransient<IMatchRepository, FileMatchRepository>();
                services.AddTransient<ICountryRepository, FileCountryRepository>();
            }
            else
            {
                services.AddTransient<IMatchRepository, ApiMatchRepository>();
                services.AddTransient<ICountryRepository, ApiCountryRepository>();
            }

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();

            var language = ConfigManager.GetLanguage();
            var league = ConfigManager.GetLeague();
            var favoriteTeamAlreadyPresent = File.Exists(@"../../../DataLayer/Files/" + "favorite_team.txt");

            if (language == null || league == null)
            {
                Application.Run(new InitialSetupForm());
                return;
            }

            SetupLocalization(language);

            if (!favoriteTeamAlreadyPresent)
            {
                Application.Run(new FavoriteCountryForm());
                return;
            }

            Application.Run(new FavoritePlayersForm());
        }


        private static void SetupLocalization(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
    }
}
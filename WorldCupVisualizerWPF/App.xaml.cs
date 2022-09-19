using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows;
using DataLayer;
using DataLayer.File;
using DataLayer.Repository.Api;
using DataLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;

namespace WorldCupVisualizerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;
        private readonly ConfigManager _configManager = ConfigManager.Instance;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Dependency injection setup
        /// This enables for the application to automatically inject either File or Api respositories
        /// based on the reading_mode.txt file content
        /// 
        /// We default to API if reading mode is not present
        /// </summary>
        private void ConfigureServices(ServiceCollection services)
        {
            var readingMode = _configManager.GetReadingMode();
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

            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var language = _configManager.GetLanguage();
            var league = _configManager.GetLeague();
            var resolution = _configManager.GetResolution();

            if (language == null || league == null || resolution == null)
            {
                SettingsWindow settings = new SettingsWindow();
                settings.Show();
                return;
            }

            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
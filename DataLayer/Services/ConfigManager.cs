using System;

namespace DataLayer.Services
{
    public class ConfigManager
    {
        private const string ConfigDirectory = @"../../../DataLayer/Files/";
        private const string LanguageSettingFile = "language.txt";
        private const string LeagueSettingFile = "league.txt";
        private const string ReadingModeSettingsFile = "reading_mode.txt";
        private const string ResolutionSettingFile = "resolution.txt";

        private static readonly Lazy<ConfigManager> _lazy =
            new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance =>
            _lazy.Value; // using lazy initialization to have a sort of singleton instance of this object. 

        private ConfigManager()
        {
        }

        public void SetLanguage(string language)
        {
            string path = ConfigDirectory + LanguageSettingFile;
            System.IO.File.WriteAllText(path, language);
        }

        public void SetLeague(string league)
        {
            string path = ConfigDirectory + LeagueSettingFile;
            System.IO.File.WriteAllText(path, league);
        }

        public void SetResolution(string resolution)
        {
            string path = ConfigDirectory + ResolutionSettingFile;
            System.IO.File.WriteAllText(path, resolution);
        }

        public string GetLanguage()
        {
            string path = ConfigDirectory + LanguageSettingFile;
            return !System.IO.File.Exists(path) ? null : System.IO.File.ReadAllLines(path)[0];
        }

        public string GetLeague()
        {
            string path = ConfigDirectory + LeagueSettingFile;
            return !System.IO.File.Exists(path) ? null : System.IO.File.ReadAllLines(path)[0];
        }

        public string GetResolution()
        {
            string path = ConfigDirectory + ResolutionSettingFile;
            return !System.IO.File.Exists(path) ? "Size720p" : System.IO.File.ReadAllLines(path)[0];
        }
        
        public string GetReadingMode()
        {
            string path = ConfigDirectory + ReadingModeSettingsFile;
            return !System.IO.File.Exists(path) ? "api" : System.IO.File.ReadAllLines(path)[0];
        }
    }
}
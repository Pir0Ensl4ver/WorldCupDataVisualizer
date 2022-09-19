using System;
using System.Configuration;
using DataLayer.Models;

namespace DataLayer.Services
{
    public sealed class EndpointPrefixDeterminer
    {
        private const string DataPrefix = @"../../../DataLayer/Files/Data/";
        
        private static readonly ConfigManager _configManager = ConfigManager.Instance;
        private static readonly Lazy<EndpointPrefixDeterminer> _lazy =
            new Lazy<EndpointPrefixDeterminer>(() => new EndpointPrefixDeterminer());

        public static EndpointPrefixDeterminer Instance =>
            _lazy.Value; // using lazy initialization to have a sort of singleton instance of this object. 

        private EndpointPrefixDeterminer()
        {
        }

        public string GetEndpointPrefix()
        {
            var league = _configManager.GetLeague();
            Enum.TryParse(league, out League currentLeague);
            var result = currentLeague switch
            {
                League.Female => "http://worldcup.sfg.io",
                League.Male => "https://world-cup-json-2018.herokuapp.com",
                _ => throw new Exception("League was not properly set")
            };

            return result;
        }

        public string GetFilePrefix()
        {
            var league = _configManager.GetLeague();
            Enum.TryParse(league, out League currentLeague);
            var result = currentLeague switch
            {
                League.Female => DataPrefix + "/women/",
                League.Male => DataPrefix + "/men/",
                _ => throw new Exception("League was not properly set")
            };

            return result;
        }
    }
}
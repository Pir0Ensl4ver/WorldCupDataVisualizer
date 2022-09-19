using System.Collections.Generic;

namespace DataLayer.Models
{
    public static class Languages
    {
        public static Dictionary<string, string> LanguageDictionary { get; } = new Dictionary<string, string>()
        {
            { "English", "en" }, { "Croatian", "hr" }
        };
    }
}
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DataLayer.Models
{
    public class CountryModel
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("fifa_code")]
        public string FifaCode { get; set; }
        
        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }
        
        [JsonPropertyName("draws")]
        public int Draws { get; set; }
        
        [JsonPropertyName("games_played")]
        public int GamesPlayed { get; set; }
        
        public override string ToString()
        {
            return $"{Country} | ({FifaCode})";
        }
    }
}
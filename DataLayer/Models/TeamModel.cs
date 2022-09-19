using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class TeamModel
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("goals")]
        public int Goals { get; set; }
        
        [JsonPropertyName("penalties")]
        public int Penalties { get; set; }

        public override string ToString()
        {
            return $"{Country} | ({Code})";
        }
    }
}
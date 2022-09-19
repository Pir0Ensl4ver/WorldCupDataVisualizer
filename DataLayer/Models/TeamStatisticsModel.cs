using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class TeamStatisticsModel
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("starting_eleven")]
        public List<PlayerModel> StartingEleven { get; set; }
        
        [JsonPropertyName("substitutes")]
        public List<PlayerModel> Substitutes { get; set; }
    }
}
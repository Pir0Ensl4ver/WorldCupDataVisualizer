using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class MatchModel
    {
        [JsonPropertyName("venue")]
        public string Venue { get; set; }
        
        [JsonPropertyName("location")]
        public string Location { get; set; }
        
        [JsonPropertyName("attendance")]
        public string Attendance { get; set; }
        
        [JsonPropertyName("home_team")]
        public TeamModel HomeTeam { get; set; }
        
        [JsonPropertyName("away_team")]
        public TeamModel AwayTeam { get; set; }
        
        [JsonPropertyName("home_team_statistics")]
        public TeamStatisticsModel HomeTeamStatistics { get; set; }
        
        [JsonPropertyName("away_team_statistics")]
        public TeamStatisticsModel AwayTeamStatistics { get; set; }
        
        [JsonPropertyName("home_team_events")]
        public List<TeamEventsModel> HomeTeamEvents { get; set; }
        
        [JsonPropertyName("away_team_events")]
        public List<TeamEventsModel> AwayTeamEvents { get; set; }

        public override string ToString()
        {
            return $"{HomeTeam.Code} vs {AwayTeam.Code}";
        }
    }
}
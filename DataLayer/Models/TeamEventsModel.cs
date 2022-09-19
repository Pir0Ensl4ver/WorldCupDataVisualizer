using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class TeamEventsModel
    {
        [JsonPropertyName("id")] 
        public int Id { get; set; }
        
        [JsonPropertyName("type_of_event")]
        public string TypeOfEvent { get; set; }
        
        [JsonPropertyName("player")]
        public string Player { get; set; }
        
        [JsonPropertyName("time")]
        public string Time;
    }
}
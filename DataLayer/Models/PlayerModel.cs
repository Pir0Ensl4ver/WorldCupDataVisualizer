using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class PlayerModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("captain")]
        public bool Captain { get; set; }
        
        [JsonPropertyName("shirt_number")]
        public int ShirtNumber { get; set; }
        
        [JsonPropertyName("position")]
        public string Position { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerModel p && p.Name == Name
                                        && p.Captain == Captain
                                        && p.ShirtNumber == ShirtNumber
                                        && p.Position == Position;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 ^ Name.GetHashCode();
            hash = hash * 23 ^ Captain.GetHashCode();
            hash = hash * 23 ^ ShirtNumber.GetHashCode();
            hash = hash * 23 ^ Position.GetHashCode();
            return hash;
        }
    }
}
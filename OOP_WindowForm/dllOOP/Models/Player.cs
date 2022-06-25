using dllOOP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.Models
{
    public class Player 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long ShirtNumber { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
        public NationalTeam Nation{ get; set; }
        public Sex Sex{ get; set; }
        public string PicturePath { get; set; }
        public int Goals { get; set; } = 0;
        public int YCards { get; set; } = 0;
        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   Name == player.Name;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }

    public enum Position { Defender, Forward, Goalie, Midfield };
}

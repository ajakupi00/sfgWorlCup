using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.Models
{
    public class Player
    {
        public string Name { get; set; }
        public bool Captain { get; set; }
        public int Shirt_Number { get; set; }
        public string Position { get; set; }
        public NationalTeam Country { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.Models
{
    public class Match
    {
        public string Venue { get; set; } //City
        public string Location { get; set; }
        public long Attendance { get; set; }
        public NationalTeam HomeTeam { get; set; }
        public NationalTeam AwayTeam { get; set; }
    }
}

using dllOOOP.Models;
using dllOOP.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public class SfgWomenRepo : ISfg

    {
        public List<Match> GetMatches(NationalTeam team)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse<NationalTeam>> GetNationalTeams()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayers(NationalTeam team)
        {
            throw new NotImplementedException();
        }

    }
}

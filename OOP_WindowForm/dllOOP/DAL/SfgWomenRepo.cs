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
        public Task<RestResponse<Match>> GetMatches(NationalTeam team)
        {
            throw new NotImplementedException();
        }

        public Task<RestResponse<NationalTeam>> GetNationalTeams()
        {
            throw new NotImplementedException();
        }

        public async Task<HashSet<Player>> GetPlayers(NationalTeam team)
        {
            throw new NotImplementedException();
        }
    }
}

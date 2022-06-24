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
    public interface ISfg
    {
        Task<RestResponse<NationalTeam>> GetNationalTeams();
        Task<HashSet<Player>> GetPlayers(NationalTeam team);
        Task<RestResponse<Match>> GetMatches(NationalTeam team);

    }
}

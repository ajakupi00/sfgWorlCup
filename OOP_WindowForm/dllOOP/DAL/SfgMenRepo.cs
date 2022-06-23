using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public class SfgMenRepo : ISfg
    {
        public List<NationalTeam> GetNationalTeams()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayers(NationalTeam team)
        {
            throw new NotImplementedException();
        }
    }
}

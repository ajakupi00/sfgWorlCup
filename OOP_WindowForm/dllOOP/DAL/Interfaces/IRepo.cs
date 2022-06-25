using dllOOOP.Models;
using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL.Interfaces
{
    public interface IRepo
    {
        void SetLanguage(string lang);
        void SetSexSetting(Sex sex);
        void SetFavoriteTeam(NationalTeam team);
        void SaveFavoritePlayers(List<Player> players);

        string GetLanguage();
        Sex GetSexSetting();
        NationalTeam GetFavoriteTeam();
        List<Player> GetFavoritePlayers();
    }
}

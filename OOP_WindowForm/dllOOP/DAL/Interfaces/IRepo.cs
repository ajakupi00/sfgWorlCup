using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllOOP.DAL.Interfaces
{
    public interface IRepo
    {
        //SET
        void SetLanguage(string lang);
        void SetSexSetting(Sex sex);
        void SetFavoriteTeam(NationalTeam team);
        void SaveFavoritePlayers(List<Player> players);
        void SavePlayersImages(List<Player> playersWithImages);

        //GET
        string GetLanguage();
        Sex GetSexSetting();
        NationalTeam GetFavoriteTeam();
        List<Player> GetFavoritePlayers();
        Control GetPicture(string filepath);
        List<Player> GetPlayersImages(Sex sex, NationalTeam nation);
    }
}

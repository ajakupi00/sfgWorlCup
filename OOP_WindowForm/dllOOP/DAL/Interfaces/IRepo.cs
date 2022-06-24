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
        //POSTAVI OMILJENU REPREZENTACIJU
        //POSTAVI OMILJENA 3 IGRACA

        string GetLanguage();
        Sex GetSexSetting();
        NationalTeam GetFavoriteTeam();
        //DOHVATI OMILJENU REPREZENTACIJU
        //DOHVATI OMILJENA 3 IGRACA
    }
}

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
        //POSTAVI JEZIK
        void SetLanguage(string lang);
        //POSTAVI SPOL STATITSTIKE
        void SetSexSetting(Sex sex);
        //POSTAVI OMILJENU REPREZENTACIJU
        //POSTAVI OMILJENA 3 IGRACA

        //DOHVATI JEZIK
        string GetLanguage();
        //DOHVATI SPOL STATITSTIKE
        Sex GetSexSetting();
        //DOHVATI OMILJENU REPREZENTACIJU
        //DOHVATI OMILJENA 3 IGRACA
    }
}

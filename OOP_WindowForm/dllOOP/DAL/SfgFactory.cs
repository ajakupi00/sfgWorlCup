using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public static class SfgFactory
    {
        public static ISfg GetSfg(Sex sex) {
            if (sex == Sex.MEN)
                return new SfgMenRepo();
            else
                return new SfgWomenRepo();
        }
    }
}

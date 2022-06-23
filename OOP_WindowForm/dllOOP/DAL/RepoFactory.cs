using dllOOP.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public static class RepoFactory
    {
        public static IRepo GetRepo() => new FileRepository();
    }
}

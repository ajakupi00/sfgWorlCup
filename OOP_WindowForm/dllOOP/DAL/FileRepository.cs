using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllOOP.DAL
{
    public class FileRepository : IRepo
    {
        private readonly string DIR = AppDomain.CurrentDomain.BaseDirectory;
        private string FILE_NAME = @"\settings.txt";
        private string PATH;
        private string[] SETTINGS = new string[2];

        public FileRepository()
        {
            if (!Directory.Exists(DIR))
            {
                Directory.CreateDirectory(DIR);
            }
            if (!File.Exists(DIR+FILE_NAME))
            {
                File.Create(DIR + FILE_NAME);
                
            }
            PATH = DIR + FILE_NAME;
        }

        public void SetLanguage(string lang)
        {
            SETTINGS[0] = lang;
            string[] lines = File.ReadAllLines(PATH);
            if (lines.Count() > 1) SETTINGS[1] = lines[1];
            File.WriteAllLines(PATH, SETTINGS);
        }

        public void SetSexSetting(Sex sex)
        {
            SETTINGS[1] = sex.ToString();
            string[] lines = File.ReadAllLines(PATH);
            if (lines.Count() > 1) SETTINGS[0] = lines[0];
            File.WriteAllLines(PATH, SETTINGS);
        }
    }
}

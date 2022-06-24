using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class Form1 : Form
    {
        private SfgMenRepo sfgMen = new SfgMenRepo();
        public Form1()
        {
            InitializeComponent();
            NapuniPodatke();
        }


        private async void NapuniPodatke()
        {
            label1.Text = "Dohvaćam podatke...";
            RestResponse<NationalTeam> odgovorPodaci = await sfgMen.GetNationalTeams();
            List<NationalTeam> podaci = SfgMenRepo.DeserializeObject(odgovorPodaci);
            HashSet<Player> players = await sfgMen.GetPlayers(podaci[9]);
            foreach (Player player in players)
            {
                comboBox1.Items.Add(player.Name);
            }

            comboBox1.SelectedIndex = 0;
        }

    }
}

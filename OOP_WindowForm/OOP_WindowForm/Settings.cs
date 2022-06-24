using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            cbGender.Items.Add(Sex.MEN.ToString());
            cbGender.Items.Add(Sex.WOMEN.ToString());
            cbGender.SelectedIndex = 0;
        }


        //private async void NapuniPodatke()
        //{
        //    label1.Text = "Dohvaćam podatke...";
        //    RestResponse<NationalTeam> odgovorPodaci = await sfgMen.GetNationalTeams();
        //    List<NationalTeam> podaci = SfgMenRepo.DeserializeObject(odgovorPodaci);
        //    HashSet<Player> players = await sfgMen.GetPlayers(podaci[1]);
        //    foreach (Player player in players)
        //    {
        //        cbLanguage.Items.Add(player.Name);
        //    }

        //    cbLanguage.SelectedIndex = 0;
        //}

    }
}

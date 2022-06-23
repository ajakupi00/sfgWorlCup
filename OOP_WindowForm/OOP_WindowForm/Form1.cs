using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class Form1 : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
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
            List<NationalTeam> podaci = SfgMenRepo.NewMethod(odgovorPodaci);
            comboBox1.Text = string.Empty;

            foreach (var korisnik in podaci)
            {
                comboBox1.Items.Add(korisnik.Country);
            }
            comboBox1.SelectedIndex = 0;
        }



    }
}

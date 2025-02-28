﻿using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class FavoriteNation : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
        private ISfg sfg;
        public bool Called = false;
        public FavoriteNation()
        {
            sfg = SfgFactory.GetSfg(repo.GetSexSetting());
            InitializeComponent();
        }

        private void FavoriteNation_Load(object sender, EventArgs e)
        {
            Localize();
            NapuniPodatke();
        }

        private async void NapuniPodatke()
        {

            RestResponse<NationalTeam> odgovorPodaci = await sfg.GetNationalTeams();
            List<NationalTeam> teams = SfgMenRepo.DeserializeObject(odgovorPodaci);

            foreach (NationalTeam team in teams)
                cbNations.Items.Add(team);
            cbNations.SelectedIndex = 0;
            btnSave.Enabled = true;
            lblLoading.Text = OOP_WindowForm.Resources.Resource.NationsLoaded;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NationalTeam team = (NationalTeam)cbNations.SelectedItem;
            repo.SetFavoriteTeam(team);
            btnContinue.Enabled = true;

        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            new FavoritePlayers().Show();
            this.Hide();
        }

        private void Localize()
        {
            lblLoading.Text = OOP_WindowForm.Resources.Resource.Loading;
            lblChoose.Text = OOP_WindowForm.Resources.Resource.ChooseFavNation;
            btnContinue.Text = OOP_WindowForm.Resources.Resource.Continue;
            btnSave.Text = OOP_WindowForm.Resources.Resource.Save;
        }
    }
}

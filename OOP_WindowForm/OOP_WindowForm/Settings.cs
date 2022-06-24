using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class Settings : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
        public Settings()
        {
            InitializeComponent();
            try
            {
                string lang = SetKultura(repo.GetLanguage());
                Init(lang);
            }
            catch (Exception)
            {
                Init("hr");
            }
        }

        private void Init(string lang)
        {
            lblGender.Text = Resources.Resource.Gender;

            cbGender.Items.Add(Sex.MEN);
            cbGender.Items.Add(Sex.WOMEN);
            try
            {
                Sex sex = repo.GetSexSetting();
                if (cbGender.Items[0].ToString() == sex.ToString())
                    cbGender.SelectedIndex = 0;
                else
                    cbGender.SelectedIndex = 1;
            }
            catch (Exception)
            {
                cbGender.SelectedIndex = 0;
            }
           
            lblLanguage.Text = Resources.Resource.Language;
            btnSave.Text = Resources.Resource.Save;
           if(lang == "hr" || lang == "" || lang == null)
                 btnLanguage.Text = Resources.Resource.English;
           else
                 btnLanguage.Text = Resources.Resource.Croatian;

        }


        private string SetKultura(string jezik)
        {
            var kultura = new CultureInfo(jezik);

            Thread.CurrentThread.CurrentUICulture = kultura;
            Thread.CurrentThread.CurrentCulture = kultura;

            return jezik;

        }


        private void btnLanguage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string lang;
            if (btn.Text == Resources.Resource.English)
                lang = SetKultura("en");
            else
                lang = SetKultura("hr");
            this.Controls.Clear();
            this.InitializeComponent();
            this.Init(lang);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            string lang = currentCulture.Name.Substring(0, 2);
            repo.SetLanguage(lang);
            repo.SetSexSetting((Sex)cbGender.SelectedItem);
            new FavoriteNation().Show();
            this.Hide();
        }
    }
}

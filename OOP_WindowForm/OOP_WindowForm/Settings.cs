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
        public Settings()
        {
            InitializeComponent();
            Init("hr");
        }

        private void Init(string lang)
        {
            lblGender.Text = Resources.Resource.Gender;

            cbGender.Items.Add(Sex.MEN.ToString());
            cbGender.Items.Add(Sex.WOMEN.ToString());
            cbGender.SelectedIndex = 0;

            lblLanguage.Text = Resources.Resource.Language;
            btnSave.Text = Resources.Resource.Save;
           if(lang == "hr")
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
    }
}

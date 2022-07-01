using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OOP_WPF
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private IRepo repo = RepoFactory.GetRepo();
        private Sex sex;
        private string lang;
        private string screensize;
        private bool fullscreen;
        private bool saved;

        public bool Called { get; set; }
        public Settings()
        {
            InitializeComponent();
            GetSettings();
            InitSettings();
        }

        private void InitSettings()
        {
            lblSettingsStatus.Text = OOP_WPF.Resources.Resource.SettingsNotSaved;
            btnContinue.Visibility = Visibility.Hidden;
            cbSex.Items.Clear();
            cbSex.Items.Add(sex);
            if (sex == Sex.MEN)
                cbSex.Items.Add(Sex.WOMEN);
            else
                cbSex.Items.Add(Sex.MEN);
            cbSex.SelectedIndex = 0;


            cbResolution.Items.Add("Fullscreen");
            cbResolution.Items.Add("1920 x 1080");
            cbResolution.Items.Add("1280 x 720");
            if (fullscreen)
                cbResolution.SelectedIndex = 0;
            else if(screensize == "1920 x 1080")
                cbResolution.SelectedIndex = 1;
            else if(screensize == "1280 x 720")
                cbResolution.SelectedIndex = 2;
            else
                cbResolution.SelectedIndex = 0;

            if (lang == "hr")
                SetKultura(lang);
            else
                SetKultura(lang);
        }

        private void GetSettings()
        {
            sex = repo.GetSexSetting();
            lang = repo.GetLanguage();
            string screen = repo.GetResolution();
            if (screen == "fulscreen")
                fullscreen = true;
            else
            {
                fullscreen = false;
                screensize = screen;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            if (!Called)
            {
                btnContinue.Visibility = Visibility.Visible;
            }else if(Called && saved)
            {
                System.Windows.MessageBox.Show("Settings saved!");
                this.Close();
                
            }
        }

        private void Save()
        {

            repo.SetLanguage(lang);
            repo.SetSexSetting(sex);
            if (fullscreen)
                repo.SetResolution("fullscreen");
            else
                repo.SetResolution(screensize);
            saved = true;
            lblSettingsStatus.Text = OOP_WPF.Resources.Resource.SettingsSaved;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new TeamOverview().Show();
        }

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (btnLanguage.Content.ToString() == "Engleski")
            {
                lang = "en";
                SetKultura(lang);
                btnLanguage.Content = OOP_WPF.Resources.Resource.CurrentLang;
            }
            else
            {
                lang = "hr";
                SetKultura(lang);
                btnLanguage.Content = OOP_WPF.Resources.Resource.CurrentLang;
            }

        }
        private void SetKultura(string jezik)
        {
            var kultura = new CultureInfo(jezik);

            Thread.CurrentThread.CurrentUICulture = kultura;
            Thread.CurrentThread.CurrentCulture = kultura;

            lblLang.Text = OOP_WPF.Resources.Resource.Language;
            lblSex.Text = OOP_WPF.Resources.Resource.Gender;
            lblRes.Text = OOP_WPF.Resources.Resource.ScreenResolution;
            if(!saved)
                 lblSettingsStatus.Text = OOP_WPF.Resources.Resource.SettingsNotSaved;
            else
                 lblSettingsStatus.Text = OOP_WPF.Resources.Resource.SettingsSaved;

        }
        private void cbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((System.Windows.Controls.ComboBox)sender).IsEditable) return;
                 sex = (Sex)cbSex.SelectedItem;
        }

        private void cbResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbResolution.SelectedItem)
            {
                case "Fullscreen":
                    fullscreen = true;
                    break;
                case "1920 x 1080":
                    screensize = "1920 x 1080";
                    fullscreen = false;
                    break;
                case "1280 x 720":
                    screensize = "1280 x 720";
                    fullscreen = false;
                    break;
                default:
                    break;
            }
        }

        private void settingForm_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Called)
            {
                switch (e.Key)
                {
                    case System.Windows.Input.Key.Enter:
                        Save();
                        break;
                    case System.Windows.Input.Key.Escape:
                        CloseSettings();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CloseSettings()
        {
            if(System.Windows.MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                this.Hide();
            }
        }
    }
}

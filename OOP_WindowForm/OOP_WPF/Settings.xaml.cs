using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
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
        public Settings()
        {
            InitializeComponent();
            GetSettings();
            InitSettings();
        }

        private void InitSettings()
        {
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
                btnLanguage.Content = "Engleski";
            else
                btnLanguage.Content = "Croatian";
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
            repo.SetLanguage(lang);
            repo.SetSexSetting(sex);
            if (fullscreen)
                repo.SetResolution("fullscreen");
            else
                repo.SetResolution(screensize);
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (btnLanguage.Content.ToString() == "Engleski")
            {
                lang = "eng";
                btnLanguage.Content = "Croatian";
            }
            else
            {
                lang = "hr";
                btnLanguage.Content = "Engleski";
            }
        }

        private void cbSex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
    }
}

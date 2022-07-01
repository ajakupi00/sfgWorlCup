using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_WPF
{
    /// <summary>
    /// Interaction logic for TeamOverview.xaml
    /// </summary>
    public partial class TeamOverview : Window
    {
        private IRepo repo = RepoFactory.GetRepo();
        private List<Match> matchesOfTeam = new List<Match>();
        private Match match;
        private ISfg sfg;
        private NationalTeam favTeam;
        private NationalTeam opponentTeam;
        private Sex sex;
        public TeamOverview()
        {
            InitializeComponent();
            InitSettings();
            InitLabels();
            InitCombos();
        }

        private void InitLabels()
        {
            lblFavNation.Content = OOP_WPF.Resources.Resource.FNation;
            lblNation.Content = OOP_WPF.Resources.Resource.ONation;
            btnFavStat.Content = OOP_WPF.Resources.Resource.ShowStat;
            btnStat.Content = OOP_WPF.Resources.Resource.ShowStat;
            btnStarters.Content = OOP_WPF.Resources.Resource.Starters;
            menuFileExit.Header = OOP_WPF.Resources.Resource.Exit;
            menuSetting.Header = OOP_WPF.Resources.Resource.Setting;
            menuItemSetting.Header = OOP_WPF.Resources.Resource.Edit;
        }

        private async void InitCombos()
        {
            lblFavTeam.Content = "";
            lblGoalFav.Content = "";
            lblTeam.Content = "";
            lblGoalTeam.Content = "";
            lblDots.Content = "";
            lblScore.Content = OOP_WPF.Resources.Resource.LoadingTeams;

            RestResponse<NationalTeam> odgovorPodaci = await sfg.GetNationalTeams();
            List<NationalTeam> teams = SfgMenRepo.DeserializeObject(odgovorPodaci);
            if (favTeam == null || !teams.Contains(favTeam))
            {
                foreach (NationalTeam team in teams)
                {
                    cbFavNation.Items.Add(team);
                }
                lblScore.Content = OOP_WPF.Resources.Resource.ChoseFav;
                btnFavStat.IsEnabled = false;
                btnStat.IsEnabled = false;
                cbNation.IsEnabled = false;
                return;
            }
            RestResponse<Match> response = await sfg.GetMatches(favTeam);
            List<Match> matches = SfgMenRepo.DeserializeObject(response);

            matchesOfTeam = matches;
            cbFavNation.Items.Clear();
            foreach (NationalTeam team in teams)
            {
                cbFavNation.Items.Add(team);

                if (team.FifaCode == favTeam.FifaCode)
                {
                    cbFavNation.SelectedIndex = cbFavNation.Items.Count - 1;
                }

            }
            cbNation.Items.Clear();

            foreach (NationalTeam team in teams)
            {
                if (team.FifaCode != favTeam.FifaCode)
                {
                    foreach (Match match in matches)
                    {
                        if (match.HomeTeamCountry == team.Country || match.AwayTeamCountry == team.Country)
                            cbNation.Items.Add(team);
                    }
                }

            }
            lblScore.Content = OOP_WPF.Resources.Resource.ChooseOpponent;



        }

        private void InitSettings()
        {
            sex = repo.GetSexSetting();
            sfg = SfgFactory.GetSfg(sex);
            favTeam = repo.GetFavoriteTeam();
            string resx = repo.GetResolution();
            if (resx == "fullscreen")
            {
                teamForm.WindowState = WindowState.Maximized;
            }
            else
            {
                teamForm.WindowState = WindowState.Normal;
                string[] res = resx.Split('x');
                int width = int.Parse(res[0].Trim());
                int height = int.Parse(res[1].Trim());
                this.Height = height;
                this.Width = width;
            }

        }

        private void btnStat_Click(object sender, RoutedEventArgs e)
        {
            if (opponentTeam == null)
                return;
            TeamStat stat = new TeamStat();
            stat.Team = opponentTeam;
            stat.Show();
        }

        private void cbFavNation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.IsEditable)
            {
                favTeam = (NationalTeam)cb.SelectedItem;
                btnFavStat.IsEnabled = true;
                btnStat.IsEnabled = true;
                cbNation.IsEnabled = true;
                InitCombos();

            }

        }

        private void cbNation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.IsEditable)
            {
                opponentTeam = (NationalTeam)cb.SelectedItem;
                UpdateScore();
                btnStarters.Visibility = Visibility.Visible;
            }

        }

        private async void UpdateScore()
        {
            RestResponse<Match> response = await sfg.GetMatches(favTeam);
            List<Match> matches = SfgMenRepo.DeserializeObject(response);
            match = matches.FirstOrDefault(m => (m.HomeTeamCountry == favTeam.Country && m.AwayTeamCountry == opponentTeam.Country) || (m.AwayTeamCountry == favTeam.Country && m.HomeTeamCountry == opponentTeam.Country));
            lblScore.Content = OOP_WPF.Resources.Resource.Score;
            lblFavTeam.Content = favTeam.FifaCode;
            lblGoalFav.Content = (match.HomeTeam.Country == favTeam.Country) ? match.HomeTeam.Goals : match.AwayTeam.Goals;
            lblTeam.Content = opponentTeam.FifaCode;
            lblGoalTeam.Content = (match.HomeTeam.Country == opponentTeam.Country) ? match.HomeTeam.Goals : match.AwayTeam.Goals;
            lblDots.Content = ":";
        }

        private void btnFavStat_Click(object sender, RoutedEventArgs e)
        {
            TeamStat stat = new TeamStat();
            stat.Team = favTeam;
            stat.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayersFormation playersFormation = new PlayersFormation(match, sex, favTeam, opponentTeam);
            playersFormation.Show();
        }

        private void menuItemSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            Grid content = (Grid)settings.Content;
            settings.Called = true;
            settings.Show();
            settings.Closing += Settings_Closing;
        }

        private void Settings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
            new TeamOverview().Show();
        }

        private void menuFileExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        
    }
}

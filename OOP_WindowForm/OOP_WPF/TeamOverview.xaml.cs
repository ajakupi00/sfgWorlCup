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

        private ISfg sfg;
        private NationalTeam favTeam;
        private NationalTeam opponentTeam;
        public TeamOverview()
        {
            InitializeComponent();
            InitSettings();
            InitCombos();
        }

        private async void InitCombos()
        {
            lblFavTeam.Content = "";
            lblGoalFav.Content = "";
            lblTeam.Content = "";
            lblGoalTeam.Content = "";
            lblDots.Content = "";
            lblScore.Content = "Loading countires..";
            RestResponse<NationalTeam> odgovorPodaci = await sfg.GetNationalTeams();
            List<NationalTeam> teams = SfgMenRepo.DeserializeObject(odgovorPodaci);

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
            lblScore.Content = "Choose opponent team";



        }

        private void InitSettings()
        {
            sfg = SfgFactory.GetSfg(repo.GetSexSetting());
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
            this.Close();
        }

        private void cbFavNation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.IsEditable)
            {
                favTeam = (NationalTeam)cb.SelectedItem;
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
            }
            
        }

        private async void UpdateScore()
        {
            RestResponse<Match> response = await sfg.GetMatches(favTeam);
            List<Match> matches = SfgMenRepo.DeserializeObject(response);
            Match match = matches.FirstOrDefault(m => (m.HomeTeamCountry == favTeam.Country && m.AwayTeamCountry == opponentTeam.Country) || (m.AwayTeamCountry == favTeam.Country && m.HomeTeamCountry == opponentTeam.Country));
            lblScore.Content = "Score";
            lblFavTeam.Content = favTeam.FifaCode;
            lblGoalFav.Content = (match.HomeTeam.Country == favTeam.Country) ? match.HomeTeam.Goals : match.AwayTeam.Goals;
            lblTeam.Content = opponentTeam.FifaCode;
            lblGoalTeam.Content = (match.HomeTeam.Country == opponentTeam.Country) ? match.HomeTeam.Goals : match.AwayTeam.Goals;
            lblDots.Content = ":";
        }
    }
}

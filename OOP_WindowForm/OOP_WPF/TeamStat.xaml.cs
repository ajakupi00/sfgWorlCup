using dllOOP.Models;
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
    /// Interaction logic for TeamStat.xaml
    /// </summary>
    public partial class TeamStat : Window
    {
        private NationalTeam team;
        public NationalTeam Team { get => team;
            set {
                team = value;
                InitStats();
            } }
        public TeamStat()
        {
            InitializeComponent();
        }

        private void InitStats()
        {
            lblCountryName.Content = Team.Country;
            lblCode.Content = team.FifaCode;
            lblMatches.Content = team.GamesPlayed;
            lblWins.Content = team.Wins;
            lblLosses.Content = team.Losses;
            lblDraws.Content = team.Draws;
            lblGoalsFor.Content = team.GoalsFor;
            lblGoalsAgainst.Content = team.GoalsAgainst;
            lblGoalsDifferential.Content = team.GoalDifferential;
        }
    }
}

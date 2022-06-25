using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using OOP_WindowForm.UserControls;
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
    public partial class RankListForm : Form
    {
        private IRepo repo = RepoFactory.GetRepo();

        private ISfg sfg;
        private Sex sex;
        private NationalTeam nation;
        private List<Match> matches;
        public RankListForm()
        {
            sex = repo.GetSexSetting();
            sfg = SfgFactory.GetSfg(sex);
            nation = repo.GetFavoriteTeam();
            InitializeComponent();
        }

        private async void RankListForm_Load(object sender, EventArgs e)
        {
            await LoadMatches(nation);
            LoadPlayers(nation);
        }

        private async void LoadPlayers(NationalTeam nation)
        {
            HashSet<Player> players = await sfg.GetPlayers(nation);
            List<Player> playersList = players.ToList();
            AppendGoalsAndCards(playersList);
            playersList.Sort((x, y) => -x.Goals.CompareTo(y.Goals));
            playersList.ForEach(p =>
            {
                pnlPlayers.Controls.Add(new PlayerStatControl
                {
                    PlayerName = p.Name,
                    Goals = p.Goals,
                    Cards = p.YCards
                });
            });
        }

        private void AppendGoalsAndCards(List<Player> playersList)
        {
            foreach (Match match in matches)
            {
                if (match.HomeTeamCountry == nation.Country)
                {
                    foreach (Player player in playersList)
                    {
                        //match.HomeTeamEvents.ForEach(e =>
                        //{
                        //    if (e.TypeOfEvent == TypeOfEvent.Goal || e.TypeOfEvent == TypeOfEvent.GoalPenalty)
                        //    {
                        //        if (e.Player == player.Name)
                        //            player.Goals++;
                        //    }
                        //    else if (e.TypeOfEvent == TypeOfEvent.YellowCard)
                        //        player.YCards++;
                        //});
                        foreach (TeamEvent e in match.HomeTeamEvents)
                        {
                            if (e.TypeOfEvent == TypeOfEvent.Goal || e.TypeOfEvent == TypeOfEvent.GoalPenalty)
                            {
                                if (e.Player == player.Name)
                                    player.Goals++;
                            }
                            else if (e.TypeOfEvent == TypeOfEvent.YellowCard)
                                if (e.Player == player.Name)
                                    player.YCards++;
                        }
                    }
                }
                else
                {
                    foreach (Player player in playersList)
                    {
                        match.AwayTeamEvents.ForEach(e =>
                        {
                            if (e.TypeOfEvent == TypeOfEvent.Goal || e.TypeOfEvent == TypeOfEvent.GoalPenalty)
                            {
                                if (e.Player == player.Name)
                                    player.Goals++;
                            }
                            else if (e.TypeOfEvent == TypeOfEvent.YellowCard)
                                player.YCards++;
                        });
                    }
                }
            }
        }

        private async Task LoadMatches(NationalTeam nation)
        {
            RestResponse<Match> odgovorPodaci = await sfg.GetMatches(nation);
            List<Match> teamMatches = SfgMenRepo.DeserializeObject(odgovorPodaci);
            teamMatches.Sort((x, y) => -x.Attendance.CompareTo(y.Attendance));
            matches = teamMatches;
            teamMatches.ForEach(m =>
            {
                pnlMatches.Controls.Add(new MatchStatControl
                {
                    MatchLocation = m.Location,
                    Attendance = m.Attendance,
                    HomeTeam = m.HomeTeamCountry,
                    AwayTeam = m.AwayTeamCountry
                });
            });
        }
    }
}

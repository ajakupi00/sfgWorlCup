using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace OOP_WPF
{
    /// <summary>
    /// Interaction logic for PlayersFormation.xaml
    /// </summary>
    public partial class PlayersFormation : Window
    {
        private IRepo repo = RepoFactory.GetRepo();
        public Match Match { get; set; }
        public Sex Sex { get; set; }
        public NationalTeam HomeCountry { get; set; }
        public NationalTeam AwayCountry { get; set; }

        public PlayersFormation(Match match, Sex sex, NationalTeam home, NationalTeam away)
        {
            this.Match = match;
            this.Sex = sex;
            this.HomeCountry = home;
            this.AwayCountry = away;
            InitializeComponent();
            HomeTeam();
            AwayTeam();
        }

        private void AwayTeam()
        {
            List<Player> starters = Match.AwayTeamStatistics.StartingEleven;
            string tactics = Match.AwayTeamStatistics.Tactics;
            awayFormation.Content = tactics;
            DivideField(rightHalf, tactics, starters, false);
        }

        private void HomeTeam()
        {
            List<Player> starters = Match.HomeTeamStatistics.StartingEleven;
            string tactics = Match.HomeTeamStatistics.Tactics;
            if(HomeCountry.Country != Match.HomeTeamCountry)
            {
                var temp = HomeCountry;
                HomeCountry = AwayCountry;
                AwayCountry = temp;
            }
            homeFormation.Content = tactics + $" {HomeCountry.FifaCode}";
            DivideField(leftHalf, tactics, starters, true);
        }

        private void DivideField(Grid half, string tactics, List<Player> starters, bool home)
        {
            string[] tactic = tactics.Split('-');
            int formation = tactic.Length;

            for (int i = 0; i < formation + 1; i++) // + GOALIE
            {
                half.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int maxPlayerPerColumn = int.Parse(tactic[0]);
            for (int i = 1; i < formation; i++)
            {
                int temp = int.Parse(tactic[i]);
                if (maxPlayerPerColumn < temp)
                    maxPlayerPerColumn = temp;

            }

            for (int i = 0; i < maxPlayerPerColumn; i++)
            {
                half.RowDefinitions.Add(new RowDefinition());
            }

            AddPlayerControls(half, starters, maxPlayerPerColumn, tactic, home);
        }

        private void AddPlayerControls(Grid half, List<Player> starters, int maxplayerspercolumn, string[] tactic, bool home)
        {
            List<Player> defenders = starters.FindAll(p => p.Position == Position.Defender);
            List<Player> midfielders = starters.FindAll(p => p.Position == Position.Midfield);
            List<Player> attackers = starters.FindAll(p => p.Position == Position.Forward);


            UserControls.Player goalie = new UserControls.Player();
            Player player = starters.Find(p => p.Position == Position.Goalie);
            AddGoalie(half, maxplayerspercolumn, goalie, player, home);

            int ndef = int.Parse(tactic[0]);
            int nToRemove = 0;
            int membersOffDef = 0;
            //  DEFENDERS
            for (int i = 0; i < defenders.Count; i++)
            {
               
                if (i < ndef)
                {
                    AddPlayer(half, maxplayerspercolumn, defenders, ndef, ref nToRemove, ref membersOffDef, i, 0, 1, home);
                }
            }
            defenders.RemoveRange(0, nToRemove);
            nToRemove = 0;
            if(membersOffDef < ndef)
            {
                int exisitingDef = membersOffDef;
                for (int i = 0; i < midfielders.Count; i++)
                {
                    if(membersOffDef < ndef)
                    {
                        AddPlayer(half, maxplayerspercolumn, midfielders, ndef, ref nToRemove, ref membersOffDef, i, exisitingDef,1, home);
                    }
                }
                midfielders.RemoveRange(0, nToRemove);
                nToRemove = 0;
            }


            // FIRST MIDFIELD
            int nfMid = int.Parse(tactic[1]);
            // CHECK LEFT DEFENDER
            int membersOffMid = 0;
            if (defenders.Count > 0)
            {
                for (int i = 0; i < defenders.Count; i++)
                {
                    if (i < nfMid)
                    {
                        AddPlayer(half, maxplayerspercolumn, defenders, nfMid, ref nToRemove, ref membersOffMid, i, 0, 2, home);
                    }
                }
            }
            defenders.RemoveRange(0, nToRemove);
            nToRemove = 0;
            
            // ADD MIDFIELDERS
            if(membersOffMid < nfMid)
            {
                int defInMid = membersOffMid;
                for (int i = 0; i < midfielders.Count; i++)
                {
                    if (membersOffMid < nfMid)
                    {
                        AddPlayer(half, maxplayerspercolumn, midfielders, nfMid, ref nToRemove, ref membersOffMid, i, defInMid, 2, home);
                    }
                }
            }
            midfielders.RemoveRange(0, nToRemove);
            nToRemove = 0;
            // CHECK IF SPACE LEFT FOR ATTACKERS
            // ADD ATTACKERS
            if(membersOffMid < nfMid)
            {
                for (int i = 0; i < attackers.Count; i++)
                {
                    if (membersOffMid < nfMid)
                    {
                        AddPlayer(half, maxplayerspercolumn, attackers, nfMid, ref nToRemove, ref membersOffMid, i, membersOffMid, 2, home);
                    }
                }
            }
            if(nToRemove != 0)
            {
                attackers.RemoveRange(0, nToRemove);
            }
            nToRemove = 0;


            // SECOND MIDFIELD
                // CHECK IF EXISTS SECOND MIDFIELD
                // ADD REMAINING MIDFIELDERS
                // CHECK IF SPACE FOR ATTACKERS
                // ADD ATTACKERS

            if(tactic.Length > 3)
            {
                int nsMid = int.Parse(tactic[2]);
                int membersOffsMid = 0;
                for (int i = 0; i < midfielders.Count; i++)
                {
                    if(i < nsMid)
                    {
                        AddPlayer(half, maxplayerspercolumn, midfielders, nsMid, ref nToRemove, ref membersOffsMid, i, 0, 3, home);


                        //pl.SetValue(Grid.ColumnProperty, 3);
                    }
                }
                midfielders.RemoveRange(0, nToRemove);
                nToRemove = 0;
                if(membersOffsMid < nsMid)
                {
                    for (int i = 0; i < attackers.Count; i++)
                    {
                        int existingMemebers = membersOffsMid;
                        if(membersOffsMid < nsMid)
                        {
                            AddPlayer(half, maxplayerspercolumn, attackers, nsMid, ref nToRemove, ref membersOffsMid, i, existingMemebers, 3, home);
                        }
                    }
                    attackers.RemoveRange(0, nToRemove);
                    nToRemove = 0;
                }
            }


            //  ATTACKERS
            // CHECK  REMAINING MIDFIELDERS
            // CHECK IF SPACE FOR ATTACKERS
            // ADD ATTACKERS

            int length = tactic.Length;
            int nAtt = int.Parse(tactic[length - 1]);
            int membersOfAttack = 0;
            if(midfielders.Count > 0)
            {
                for (int i = 0; i < midfielders.Count; i++)
                {
                    if (i < nAtt)
                    {
                        AddPlayer(half, maxplayerspercolumn, midfielders, nAtt, ref nToRemove, ref membersOfAttack, i, 0, length, home);

                        //pl.SetValue(Grid.ColumnProperty, length);
                    }
                }
                midfielders.RemoveRange(0, nToRemove);
                nToRemove = 0;
            }
            if(membersOfAttack < nAtt)
            {
                for (int i = 0; i < attackers.Count; i++)
                {
                    int existing = membersOfAttack;
                    if(membersOfAttack < nAtt)
                    {
                        AddPlayer(half, maxplayerspercolumn, attackers, nAtt, ref nToRemove, ref membersOfAttack, i, existing, length, home);
                    }
                }
            }

        }

        private void AddPlayer(Grid half, int maxplayerspercolumn, List<Player> defenders, int ndef, ref int nToRemove, ref int membersOffDef, int i, int existing, int length, bool home)
        {
            UserControls.Player pl = new UserControls.Player();
            pl.PlayerName = defenders[i].Name;
            pl.ShirtNUmber = defenders[i].ShirtNumber.ToString();
            pl.Image = CheckImage(defenders[i], home);
            pl.InitFields();
            pl.VerticalAlignment = VerticalAlignment.Center;
            pl.HorizontalAlignment = HorizontalAlignment.Center;
            pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ndef);
            pl.SetValue(Grid.ColumnProperty, length);
            pl.SetValue(Grid.RowProperty, i + existing);
            pl.MouseDoubleClick += CheckPlayer;
            half.Children.Add(pl);
            nToRemove++;
            membersOffDef++;
        }


        private void AddGoalie(Grid half, int maxplayerspercolumn, UserControls.Player goalie, Player player, bool home)
        {
            goalie.PlayerName = player.Name;
            goalie.ShirtNUmber = player.ShirtNumber.ToString();
            goalie.Image = CheckImage(player, home);
            goalie.InitFields();
            goalie.VerticalAlignment = VerticalAlignment.Center;
            goalie.HorizontalAlignment = HorizontalAlignment.Center;
            goalie.SetValue(Grid.RowSpanProperty, maxplayerspercolumn );
            goalie.SetValue(Grid.ColumnProperty, 0);
            goalie.MouseDoubleClick += CheckPlayer;
            half.Children.Add(goalie);
        }

        private void CheckPlayer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControls.Player control = (UserControls.Player)sender;
            bool found = false;
            Player player = Match.HomeTeamStatistics.StartingEleven.Find(p => p.Name == control.PlayerName ? found = true : found = false);
            if (!found)
            {
                player = Match.AwayTeamStatistics.StartingEleven.Find(p => p.Name == control.PlayerName);
                player.PicturePath = control.Image;
                foreach (TeamEvent ev in Match.AwayTeamEvents)
                {
                    if (ev.TypeOfEvent == TypeOfEvent.Goal && ev.Player == player.Name)
                        player.Goals++;
                    else if (ev.TypeOfEvent == TypeOfEvent.GoalPenalty && ev.Player == player.Name)
                        player.Goals++;
                    else if (ev.TypeOfEvent == TypeOfEvent.YellowCard && ev.Player == player.Name)
                        player.YCards++;
                }
                new PlayerStat(player).Show();
                return;
            }
            player.PicturePath = control.Image;
            foreach (TeamEvent ev in Match.HomeTeamEvents)
            {
                if (ev.TypeOfEvent == TypeOfEvent.Goal && ev.Player == player.Name)
                    player.Goals++;
                else if (ev.TypeOfEvent == TypeOfEvent.GoalPenalty && ev.Player == player.Name)
                    player.Goals++;
                else if (ev.TypeOfEvent == TypeOfEvent.YellowCard && ev.Player == player.Name)
                    player.YCards++;
            }
            new PlayerStat(player).Show();
        }

        private string CheckImage(Player player, bool home)
        {
            if (home)
                return repo.GetPlayerImage(Sex, HomeCountry, player);
            else
                return repo.GetPlayerImage(Sex, AwayCountry, player);
        }


    }
}

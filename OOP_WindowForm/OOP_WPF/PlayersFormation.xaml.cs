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
        public Match Match { get; set; }

        public PlayersFormation(Match match)
        {
            this.Match = match;
            InitializeComponent();
            HomeTeam();
            AwayTeam();
        }

        private void AwayTeam()
        {
            List<Player> starters = Match.AwayTeamStatistics.StartingEleven;
            string tactics = Match.AwayTeamStatistics.Tactics;
            DivideField(rightHalf, tactics, starters);
        }

        private void HomeTeam()
        {
            List<Player> starters = Match.HomeTeamStatistics.StartingEleven;
            string tactics = Match.HomeTeamStatistics.Tactics;
            DivideField(leftHalf, tactics, starters);
        }

        private void DivideField(Grid half, string tactics, List<Player> starters)
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

            AddPlayerControls(half, starters, maxPlayerPerColumn, tactic);
        }

        private void AddPlayerControls(Grid half, List<Player> starters, int maxplayerspercolumn, string[] tactic)
        {
            List<Player> defender = starters.FindAll(p => p.Position == Position.Defender);
            List<Player> midfielders = starters.FindAll(p => p.Position == Position.Midfield);
            List<Player> attackers = starters.FindAll(p => p.Position == Position.Forward);


            UserControls.Player goalie = new UserControls.Player();
            Player player = starters.Find(p => p.Position == Position.Goalie);
            AddGoalie(half, maxplayerspercolumn, goalie, player);

            for (int i = 0; i < tactic.Length; i++)
            {
                int ntact = int.Parse(tactic[i]);
                int nd = 0;
                for (int j = 0; j < ntact; j++)
                {
                    UserControls.Player pl = new UserControls.Player();
                    if (i == 0) //IF DEFENDER
                    {
                        AddPlayer(half, maxplayerspercolumn, i, ntact, j, pl, defender);
                        if (j == ntact - 1)
                        {
                            defender.RemoveRange(0, ntact);
                            continue;
                        }
                    }

                    else if (i == 1) //FIRST MIDFIELD
                    {
                        if (defender.Count > 0)
                        {

                            pl.PlayerName = defender[nd].Name;
                            pl.ShirtNUmber = defender[nd].ShirtNumber.ToString();
                            pl.InitFields();
                            pl.VerticalAlignment = VerticalAlignment.Center;
                            pl.HorizontalAlignment = HorizontalAlignment.Center;
                            pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ntact);
                            pl.SetValue(Grid.ColumnProperty, i + 1);
                            pl.SetValue(Grid.RowProperty, j);
                            half.Children.Add(pl);
                            defender.Remove(defender[nd]);
                            continue;
                        }
                        if (j < midfielders.Count)
                        {
                            AddPlayer(half, maxplayerspercolumn, i, ntact, j, pl, midfielders);
                            nd++;
                            continue;
                        }
                        if (tactic.Length == 3 && nd <= midfielders.Count)
                        {
                            pl.PlayerName = midfielders[nd].Name;
                            pl.ShirtNUmber = midfielders[nd].ShirtNumber.ToString();
                            pl.InitFields();
                            pl.VerticalAlignment = VerticalAlignment.Center;
                            pl.HorizontalAlignment = HorizontalAlignment.Center;
                            pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ntact);
                            pl.SetValue(Grid.ColumnProperty, i + 1);
                            pl.SetValue(Grid.RowProperty, j);
                            half.Children.Add(pl);
                            nd++;
                        }
                        if (j == ntact - 1)
                        {
                            midfielders.RemoveRange(0, nd);
                            continue;

                        }
                    }
                    else if (i == 2 && tactic.Length > 3) // SECOND MIDFIELD
                    {
                        if (j < midfielders.Count)
                        {
                            AddPlayer(half, maxplayerspercolumn, i, ntact, j, pl, midfielders);
                            nd++;
                            continue;
                        }
                        if (j == midfielders.Count)
                        {
                            midfielders.RemoveRange(0, nd);
                            nd = 0;
                        }
                        if (j < attackers.Count && midfielders.Count == 0)
                        {
                            pl.PlayerName = attackers[nd].Name;
                            pl.ShirtNUmber = attackers[nd].ShirtNumber.ToString();
                            pl.InitFields();
                            pl.VerticalAlignment = VerticalAlignment.Center;
                            pl.HorizontalAlignment = HorizontalAlignment.Center;
                            pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ntact);
                            pl.SetValue(Grid.ColumnProperty, i + 1);
                            pl.SetValue(Grid.RowProperty, j);
                            half.Children.Add(pl);
                            nd++;
                        }
                        if (j == ntact - 1)
                        {
                            attackers.RemoveRange(0, nd);
                            continue;
                        }
                        
                    }
                    else // ATTACKERS
                    {
                        AddPlayer(half, maxplayerspercolumn, i, ntact, j, pl, attackers);
                    }
                }
            }
        }


        private static void AddGoalie(Grid half, int maxplayerspercolumn, UserControls.Player goalie, Player player)
        {
            goalie.PlayerName = player.Name;
            goalie.ShirtNUmber = player.ShirtNumber.ToString();
            goalie.InitFields();
            goalie.VerticalAlignment = VerticalAlignment.Center;
            goalie.HorizontalAlignment = HorizontalAlignment.Center;
            goalie.SetValue(Grid.RowSpanProperty, maxplayerspercolumn);
            goalie.SetValue(Grid.ColumnProperty, 0);
            half.Children.Add(goalie);
        }



        private static void AddPlayer(Grid half, int maxplayerspercolumn, int i, int ntact, int j, UserControls.Player pl, List<Player> mid)
        {
            pl.PlayerName = mid[j].Name;
            pl.ShirtNUmber = mid[j].ShirtNumber.ToString();
            pl.InitFields();
            pl.VerticalAlignment = VerticalAlignment.Center;
            pl.HorizontalAlignment = HorizontalAlignment.Center;
            pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ntact);
            pl.SetValue(Grid.ColumnProperty, i + 1);
            pl.SetValue(Grid.RowProperty, j);
            half.Children.Add(pl);
        }

    }
}

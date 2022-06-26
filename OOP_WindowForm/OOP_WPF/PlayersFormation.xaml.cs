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

            UserControls.Player goalie = new UserControls.Player();
            Player player = starters.Find(p => p.Position == Position.Goalie);
            goalie.PlayerName = player.Name;
            goalie.ShirtNUmber = player.ShirtNumber.ToString();
            goalie.InitFields();
            goalie.VerticalAlignment = VerticalAlignment.Center;
            goalie.HorizontalAlignment = HorizontalAlignment.Center;
            goalie.SetValue(Grid.RowSpanProperty, maxplayerspercolumn);
            goalie.SetValue(Grid.ColumnProperty, 0);
            half.Children.Add(goalie);

            for (int i = 0; i < tactic.Length; i++)
            {
                int ntact = int.Parse(tactic[i]);
                for (int j = 0; j < ntact; j++)
                {
                    if (i == 0)
                    {
                        UserControls.Player pl = new UserControls.Player();
                        List<Player> def = starters.FindAll(p => p.Position == Position.Defender);
                        if (def.Count <= j)
                            break;
                        pl.PlayerName = def[j].Name;
                        pl.ShirtNUmber = def[j].ShirtNumber.ToString();
                        pl.InitFields();
                        pl.VerticalAlignment = VerticalAlignment.Center;
                        pl.HorizontalAlignment = HorizontalAlignment.Center;
                        pl.SetValue(Grid.RowSpanProperty, maxplayerspercolumn / ntact);
                        pl.SetValue(Grid.ColumnProperty, i + 1);
                        pl.SetValue(Grid.RowProperty, j);
                        half.Children.Add(pl);
                    }
                    else if (i == 1)
                    {
                        UserControls.Player pl = new UserControls.Player();
                        List<Player> mid = starters.FindAll(p => p.Position == Position.Midfield);
                        if (mid.Count <= j)
                            break;
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
                    else if (i == 2 && tactic.Length > 3)
                    {
                        UserControls.Player pl = new UserControls.Player();
                        List<Player> mid = starters.FindAll(p => p.Position == Position.Midfield);
                        if (mid.Count <= j)
                            break;
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
                    else
                    {
                        UserControls.Player pl = new UserControls.Player();
                        List<Player> mid = starters.FindAll(p => p.Position == Position.Forward);
                        if (mid.Count <= j)
                            break;
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

        }
    }
}

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
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm
{
    public partial class RankListForm : Form
    {
        private IRepo repo = RepoFactory.GetRepo();

        private ISfg sfg;
        private Sex sex;
        private string lang;
        private NationalTeam nation;
        private List<Match> matches;
        private List<Player> players;

        private Settings settingsForm;
        private FavoriteNation nationForm;

        private int itemsOnPage = 0;
        private int left;
        public RankListForm()
        {
            InitializeComponent();
            InitSettings();
            Localize();

        }

        private void Localize()
        {
            printAllToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.PrintAll;
            settingsToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.Settings;
            changeToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.SettingsChange;
            changeTeamToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.SettingsChangeTeam;
            applicationToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.Application;
            closeToolStripMenuItem.Text = OOP_WindowForm.Resources.Resource.Close;
            lblPlyRankings.Text = OOP_WindowForm.Resources.Resource.PlayerRanking;
            lblMatchRankings.Text = OOP_WindowForm.Resources.Resource.MatchRanking;
            lblSort.Text = OOP_WindowForm.Resources.Resource.SortBy;

        }

        private void InitSettings()
        {
            sex = repo.GetSexSetting();
            sfg = SfgFactory.GetSfg(sex);
            lang = repo.GetLanguage();
            SetKultura(lang);
           
            nation = repo.GetFavoriteTeam();
        }
        private void SetKultura(string jezik)
        {
            var kultura = new CultureInfo(jezik);

            Thread.CurrentThread.CurrentUICulture = kultura;
            Thread.CurrentThread.CurrentCulture = kultura;



        }

        private async void RankListForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadMatches(nation);
                LoadPlayers(nation);
            }
            catch (Exception)
            {

                MessageBox.Show(OOP_WindowForm.Resources.Resource.TeamNotQualified);
            }
        }


        private async void LoadPlayers(NationalTeam nation)
        {
            HashSet<Player> set = await sfg.GetPlayers(nation);
            List<Player> playersList = set.ToList();
            AppendGoalsAndCards(playersList);
            playersList.Sort((x, y) => -x.Goals.CompareTo(y.Goals));
            players = playersList;
            AppendComboBox();
            AddPlayersControls(playersList);
        }

        private void AddPlayersControls(List<Player> playersList)
        {
            pnlPlayers.Controls.Clear();
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

        private void AppendComboBox()
        {
            cbPlayerSort.Items.Add(OOP_WindowForm.Resources.Resource.Goals);
            cbPlayerSort.Items.Add(OOP_WindowForm.Resources.Resource.YCards);
            cbPlayerSort.SelectedIndex = 0;
        }

        private void AppendGoalsAndCards(List<Player> playersList)
        {
            foreach (Match match in matches)
            {
                if (match.HomeTeamCountry == nation.Country)
                {
                    foreach (Player player in playersList)
                    {
                        match.HomeTeamEvents.ForEach(e =>
                        {
                            if (e.TypeOfEvent == TypeOfEvent.Goal || e.TypeOfEvent == TypeOfEvent.GoalPenalty)
                            {
                                if (e.Player == player.Name)
                                    player.Goals++;
                            }
                            else if (e.TypeOfEvent == TypeOfEvent.YellowCard)
                                if (e.Player == player.Name)
                                    player.YCards++;
                        });
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
                                if (e.Player == player.Name)
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

        private void cbPlayerSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            switch (cb.SelectedItem.ToString())
            {
                case "Yellow cards":
                    players.Sort((x, y) => -x.YCards.CompareTo(y.YCards));
                    AddPlayersControls(players);
                    break;
                default:
                    players.Sort((x, y) => -x.Goals.CompareTo(y.Goals));
                    AddPlayersControls(players);
                    break;
            }
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Print()
        {
            printPreviewDialog.ShowDialog();

        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (itemsOnPage++ == 0)
            {
                PrintPrvaStranica(e);
                e.HasMorePages = true;
            }
            else if (itemsOnPage++ == 2)
            {
                PrintDrugaStranica(e);
                e.HasMorePages = true;
            }
            else
            {
                PrintTrecaStranica(e);
            }
        }

        private void PrintTrecaStranica(PrintPageEventArgs e)
        {
            int x = 0;
            int y = 0; //450
            int ymax = printDocument.DefaultPageSettings.Bounds.Height; //1200
            int xmax = printDocument.DefaultPageSettings.Bounds.Width; //1200
            printDocument.DefaultPageSettings.Landscape = true;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            for (int i = 0; i < pnlMatches.Controls.Count; i++)
            {
                if (y >= ymax - 50)
                {
                    y = 0;
                    x += pnlMatches.Controls[i].Width + 20;
                }
                Rectangle rect = new Rectangle(0, 0, pnlMatches.Controls[i].Width, pnlMatches.Controls[i].Height);
                Bitmap bitmap = new Bitmap(pnlMatches.Controls[i].Width, pnlMatches.Controls[i].Height);
                pnlMatches.Controls[i].DrawToBitmap(bitmap, rect);

                e.Graphics.DrawImage(bitmap, new Point(x, y));
                y += pnlMatches.Controls[i].Height + 20;
            }
        }

        private void PrintDrugaStranica(PrintPageEventArgs e)
        {
            int x = 0;
            int y = 0; //450
            int ymax = printDocument.DefaultPageSettings.Bounds.Height; //1200
            int xmax = printDocument.DefaultPageSettings.Bounds.Width; //1200
            printDocument.DefaultPageSettings.Landscape = true;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            for (int i = 0; i < pnlPlayers.Controls.Count; i++)
            {
                if (x >= pnlPlayers.Controls[i].Width + 19 && y >= ymax - 20)
                {
                    left = 0;
                    return;
                }
                if (y >= ymax - 20)
                {
                    y = 0;
                    x += pnlPlayers.Controls[i].Width + 20;
                }
                Rectangle rect = new Rectangle(0, 0, pnlPlayers.Controls[i].Width, pnlPlayers.Controls[i].Height);
                Bitmap bitmap = new Bitmap(pnlPlayers.Controls[i].Width, pnlPlayers.Controls[i].Height);
                pnlPlayers.Controls[i].DrawToBitmap(bitmap, rect);

                e.Graphics.DrawImage(bitmap, new Point(x, y));
                y += pnlPlayers.Controls[i].Height + 20;


            }
        }


        private void PrintPrvaStranica(PrintPageEventArgs e)
        {
            int x = 0;
            int y = 0; //450
            int ymax = printDocument.DefaultPageSettings.Bounds.Height; //1200
            int xmax = printDocument.DefaultPageSettings.Bounds.Width; //1200
            printDocument.DefaultPageSettings.Landscape = true;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            for (int i = 0; i < pnlPlayers.Controls.Count; i++)
            {
                if (x >= pnlPlayers.Controls[i].Width + 19 && y >= ymax - 20)
                {
                    left = pnlPlayers.Controls.Count - i;
                    return;
                }
                if (y >= ymax - 20)
                {
                    y = 0;
                    x += pnlPlayers.Controls[i].Width + 20;
                }
                Rectangle rect = new Rectangle(0, 0, pnlPlayers.Controls[i].Width, pnlPlayers.Controls[i].Height);
                Bitmap bitmap = new Bitmap(pnlPlayers.Controls[i].Width, pnlPlayers.Controls[i].Height);
                pnlPlayers.Controls[i].DrawToBitmap(bitmap, rect);

                e.Graphics.DrawImage(bitmap, new Point(x, y));
                y += pnlPlayers.Controls[i].Height + 20;


            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                Print();
                printDocument.Print();
            }
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new Settings();
            settingsForm.Called = true;
            settingsForm.Controls["btnContinue"].Visible = false;
            settingsForm.Show();
            settingsForm.FormClosing += SettingsForm_FormClosing;

        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InitSettings();
            this.Controls.Clear();
            InitializeComponent();
            Localize();
            RankListForm_Load(this, new EventArgs());
        }

        private void changeTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nationForm = new FavoriteNation();
            nationForm.Controls["btnContinue"].Visible = false;
            nationForm.Show();
            nationForm.FormClosing += SettingsForm_FormClosing;
        }
    }
}

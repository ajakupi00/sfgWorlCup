using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using OOP_WindowForm.UserControls;
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
    public partial class FavoritePlayers : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
        private ISfg sfg;
        public FavoritePlayers()
        {
            sfg = SfgFactory.GetSfg(repo.GetSexSetting());
            InitializeComponent();
        }

        private void FavoritePlayers_Load(object sender, EventArgs e)
        {
            LoadPlayers();
        }

        private async void LoadPlayers()
        {
            NationalTeam team = repo.GetFavoriteTeam();
            HashSet<Player> players = await sfg.GetPlayers(team);
            foreach (Player player in players)
            {
                
                PlayerControl playerControl = new PlayerControl()
                {
                    PlayerName = player.Name,
                    Position = (dllOOOP.Models.Position)player.Position,
                    Captain = player.Captain,
                    Favorite = false,
                    ShirtNumber = int.Parse(player.ShirtNumber.ToString()),
                };
                playerControl.ContextMenuStrip.Items[0].Click += favoriteStripItem_Click;
                playerControl.ContextMenuStrip.Items[1].Click += removeFromFavoriteStripItem_Click;
                pnlPlayers.Controls.Add(playerControl);
            }

        }

        private void pnlFavPlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void pnlFavPlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (pnlFavPlayers.Controls.Count >= 3)
            {
                MessageBox.Show("Not allowed to add more than 3 players!");
                return;
            }
            PlayerControl player = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
            player.Favorite = true;
            pnlFavPlayers.Controls.Add(player);
        }

        

        private void favoriteStripItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            ToolStrip ts = item.Owner;
            ContextMenuStrip strip = (ContextMenuStrip)item.Owner;
            PlayerControl player = (PlayerControl)strip.SourceControl;
            if (pnlFavPlayers.Controls.Count >= 3)
            {
                MessageBox.Show("Not allowed to add more than 3 players!");
                 player.Favorite = false;
                return;
            }
            player.Favorite = true;
            pnlPlayers.Controls.Remove(player);
            pnlFavPlayers.Controls.Add(player);
        }

        private void removeFromFavoriteStripItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            ToolStrip ts = item.Owner;
            ContextMenuStrip strip = (ContextMenuStrip)item.Owner;
            PlayerControl player = (PlayerControl)strip.SourceControl;
            player.Favorite = false;
            pnlFavPlayers.Controls.Remove(player);
            pnlPlayers.Controls.Add(player);
        }

        private void pnlPlayers_DragDrop(object sender, DragEventArgs e)
        {
            PlayerControl player = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
            player.Favorite = false;
            pnlPlayers.Controls.Add(player);
        }

        private void pnlPlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}

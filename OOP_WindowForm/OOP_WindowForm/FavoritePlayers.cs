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
                pnlPlayers.Controls.Add(new PlayerControl()
                {
                    PlayerName = player.Name,
                    Position = (dllOOOP.Models.Position)player.Position,
                    Captain = player.Captain,
                    Favorite = false,
                    ShirtNumber = int.Parse(player.ShirtNumber.ToString())
                });
            }

        }
    }
}

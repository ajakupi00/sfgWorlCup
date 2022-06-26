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
        private Sex worldCupGender;
        private NationalTeam nationalTeam;

        private OpenFileDialog ofd = new OpenFileDialog();
        private List<PlayerControl> selected = new List<PlayerControl>();
        private List<Player> playersWithImages = new List<Player>();
        private List<Player> existingImages;

        public FavoritePlayers()
        {
            Sex sex = repo.GetSexSetting();
            sfg = SfgFactory.GetSfg(sex);
            worldCupGender = sex;
            nationalTeam = repo.GetFavoriteTeam();
            existingImages = repo.GetPlayersImages(sex, nationalTeam);
            InitializeComponent();
            InitOpenFileDialog();
        }
        private void InitOpenFileDialog()
        {
            ofd.Filter = "Pictures|*.jpeg;*.jpg;*.png;|All files|*.*";
            ofd.Multiselect = false;
            ofd.Title = "Load picture...";
            ofd.InitialDirectory = Application.StartupPath;
        }

        private void FavoritePlayers_Load(object sender, EventArgs e)
        {
            try
            {
                List<Player> players = repo.GetFavoritePlayers();
                if (players.Count > 0 && players[0].Sex == worldCupGender && players[0].Nation.FifaCode == nationalTeam.FifaCode)
                {
                    LoadPlayers(players);
                }
                else
                {

                    LoadPlayers();
                }
            }
            catch (Exception)
            {
                LoadPlayers();
            }
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
                    Position = player.Position,
                    Captain = player.Captain,
                    Favorite = false,
                    ShirtNumber = int.Parse(player.ShirtNumber.ToString()),
                };
                if (existingImages != null)
                    playerControl.PicturePath = (existingImages.Exists(p => p.Name == player.Name)) ? existingImages.FirstOrDefault(p => p.Name == player.Name).PicturePath : "";

                playerControl.ContextMenuStrip.Items[0].Click += favoriteStripItem_Click;
                playerControl.ContextMenuStrip.Items[1].Click += removeFromFavoriteStripItem_Click;
                playerControl.MouseDown += Player_MouseDownClick;
                pnlPlayers.Controls.Add(playerControl);
            }

        }

        private async void LoadPlayers(List<Player> favPlayers)
        {
            HashSet<Player> players = await sfg.GetPlayers(nationalTeam);
            favPlayers.ForEach(f => players.Remove(f));
            favPlayers.ForEach(f =>
            {
                PlayerControl playerControl = new PlayerControl()
                {
                    PlayerName = f.Name,
                    Position = (dllOOP.Models.Position)f.Position,
                    Captain = f.Captain,
                    Favorite = true,
                    ShirtNumber = int.Parse(f.ShirtNumber.ToString()),
                };
                if (existingImages != null)
                    playerControl.PicturePath = (existingImages.Exists(p => p.Name == f.Name)) ? existingImages.FirstOrDefault(p => p.Name == f.Name).PicturePath : "";
                playerControl.ContextMenuStrip.Items[0].Click += favoriteStripItem_Click;
                playerControl.ContextMenuStrip.Items[1].Click += removeFromFavoriteStripItem_Click;
                playerControl.MouseDown += Player_MouseDownClick;
                playerControl.Controls["image"].DoubleClick += FavoritePlayers_DoubleClick;
                pnlFavPlayers.Controls.Add(playerControl);
            });
            foreach (Player player in players)
            {

                PlayerControl playerControl = new PlayerControl()
                {
                    PlayerName = player.Name,
                    Position = (dllOOP.Models.Position)player.Position,
                    Captain = player.Captain,
                    Favorite = false,
                    ShirtNumber = int.Parse(player.ShirtNumber.ToString()),
                };
                if (existingImages != null)
                    playerControl.PicturePath = (existingImages.Exists(p => p.Name == player.Name)) ? existingImages.FirstOrDefault(p => p.Name == player.Name).PicturePath : "";

                playerControl.ContextMenuStrip.Items[0].Click += favoriteStripItem_Click;
                playerControl.ContextMenuStrip.Items[1].Click += removeFromFavoriteStripItem_Click;
                playerControl.MouseDown += Player_MouseDownClick;
                playerControl.Controls["image"].DoubleClick += FavoritePlayers_DoubleClick;
                pnlPlayers.Controls.Add(playerControl);
            }

        }

        private void FavoritePlayers_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            LoadPicture(pb);
        }

        private void LoadPicture(PictureBox pb)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var maxPictures = 1;
                if (ofd.FileNames.Length > maxPictures)
                {
                    MessageBox.Show($"A maximum of 1 image is allowed.\nOnly 1 pictures will be added.");
                }
                foreach (var file in ofd.FileNames)
                {
                    ShowPicture(file, pb);
                }
            }
        }

        private void ShowPicture(string file, PictureBox pb)
        {
            pb.Image = Image.FromFile(file);
            PlayerControl parent = (PlayerControl)pb.Parent;
            parent.PicturePath = file;
            playersWithImages.Add(PlayerControl.ParseFromControl(parent, worldCupGender, nationalTeam));
        }

        private void Player_MouseDownClick(object sender, MouseEventArgs e)
        {
            PlayerControl player = (PlayerControl)sender;
            if (selected.Contains(player))
            {
                player.BackColor = Color.White;
                selected.Remove(player);
                return;
            }
            if (selected.Count < 3)
            {
                player.BackColor = Color.LightBlue;
                selected.Add(player);
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
            if (selected.Count > 1)
            {
                selected.ForEach(p =>
                {
                    if (pnlFavPlayers.Controls.Count < 3)
                    {
                        p.Favorite = true;
                        p.BackColor = Color.White;
                        pnlFavPlayers.Controls.Add(p);
                    }

                });
                selected.Clear();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pnlFavPlayers.Controls.Count < 1)
            {
                MessageBox.Show("Add players to save them!");
                return;
            }
            List<Player> players = new List<Player>();
            foreach (PlayerControl control in pnlFavPlayers.Controls)
            {
                players.Add(PlayerControl.ParseFromControl(control, worldCupGender, nationalTeam));

            }
            if(existingImages != null)
                 existingImages.ForEach(i => playersWithImages.Add(i));
            repo.SavePlayersImages(playersWithImages);
            repo.SaveFavoritePlayers(players);
            this.Close();
            new RankListForm().Show();
        }
    }
}

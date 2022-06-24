using dllOOOP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm.UserControls
{
    public partial class PlayerControl : UserControl
    {
        private bool favorite;
        private string playerName;
        private Position position;
        private bool captain;
        private int shirtNumber;


        public bool Favorite
        {
            get
            {
                return favorite;
            }
            set
            {
                favorite = value;
                if (favorite == true)
                {
                    pngStar.Visible = true;
                    playerMenu.Items[0].Visible = false;
                    playerMenu.Items[1].Visible = true;
                }
                else
                {
                    pngStar.Visible = false;
                    playerMenu.Items[0].Visible = true;
                    playerMenu.Items[1].Visible = false;
                }
            }
        }

        public string PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                lblPlayerName.Text = value;
            }
        }
        public Position Position
        {
            get => position;
            set
            {
                position = value;
                lblplayerPosition.Text = value.ToString();
            }
        }
        public bool Captain
        {
            get => captain;
            set
            {
                captain = value;
                if (captain)
                    lblPlayerCaptain.Visible = true;
            }
        }

        public int ShirtNumber
        {
            get => shirtNumber;
            set { 
                shirtNumber = value;
                lblShirtNumber.Text = value.ToString();
            }
        }

        public PlayerControl()
        {
            InitializeComponent();;
            this.ContextMenuStrip = playerMenu;
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            ((PlayerControl)sender).DoDragDrop(((PlayerControl)sender), DragDropEffects.Move);
        }

        public virtual void favoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite = true;
        }

        public virtual void removeFromFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite = false;
        }
    }
}

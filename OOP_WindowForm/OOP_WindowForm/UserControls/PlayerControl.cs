﻿using dllOOP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_WindowForm.UserControls
{
    public partial class PlayerControl : UserControl
    {
        public readonly string DIR = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        private bool favorite;
        private string playerName;
        private dllOOP.Models.Position position;
        private bool captain;
        private int shirtNumber;
        private string picturePath = "";

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
        public dllOOP.Models.Position Position
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

        public string PicturePath { 
            get => picturePath;
            set { 
                if(value != "")
                {
                    string[] details = value.Split('\\');
                    string path = details[details.Length - 1];
                    if (!File.Exists(DIR + @"\" + path))
                    {
                        File.Copy(value, DIR + @"\" +path);
                    }
                    picturePath = DIR + @"\" + path;
                    pbImage.Image = Image.FromFile(PicturePath);
                }
            } }

        public PlayerControl()
        {
            InitializeComponent();;
            this.ContextMenuStrip = playerMenu;
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            PlayerControl player = ((PlayerControl)sender);
            player.DoDragDrop(player, DragDropEffects.Move);

        }

        public virtual void favoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite = true;
        }

        public virtual void removeFromFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite = false;
        }

        public static Player ParseFromControl(PlayerControl control, Sex sex, NationalTeam nation)
        {
            string[] details = control.PicturePath.Split('\\');
            string path = details[details.Length - 1];
            return new Player
            {
                Name = control.PlayerName,
                Captain = control.Captain,
                Position = (dllOOP.Models.Position)control.position,
                ShirtNumber = control.ShirtNumber,
                Sex = sex,
                Nation = nation,
                PicturePath = path
            };
        }


        
    }
}

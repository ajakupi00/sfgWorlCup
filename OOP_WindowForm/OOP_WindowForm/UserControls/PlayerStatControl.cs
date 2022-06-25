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
    public partial class PlayerStatControl : UserControl
    {
        private string playerName;
        private string picturePath;
        private int goals;
        private int cards;
        public PlayerStatControl()
        {
            InitializeComponent();
        }

        public string PlayerName { 
            get => playerName;
            set {
                playerName = value;
                lblName.Text = value;
            }
        }
        public int Goals { 
            get => goals;
            set {
                goals = value;
                lblGoals.Text = value.ToString();
            } 
        }
        public int Cards { 
            get => cards; 
            set {
                cards = value;
                lblCards.Text = value.ToString();
            } 
        }

        public string PicturePath { 
            get => picturePath;
            set {
                picturePath = value;
                pbImage.Image = Image.FromFile(value);
            }
        }
    }
}

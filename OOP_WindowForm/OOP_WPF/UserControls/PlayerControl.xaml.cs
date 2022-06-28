using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        private readonly string DIR = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        private string path = @"football-player.png";
        public string Image
        {
            get => path;
            set
            {
                if (value != "")
                    path = value;
            }
        }
        public string PlayerName { get; set; }
        public string ShirtNUmber { get; set; }
        public Player()
        {
            InitializeComponent();
        }

        public void InitFields()
        {
            image.Source = new BitmapImage(new Uri(DIR + @"\"+ path));
            lblName.Text = PlayerName;
            lblNumber.Text = ShirtNUmber;
        }
    }
}

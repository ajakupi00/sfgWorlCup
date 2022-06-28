using dllOOP.Models;
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
using System.Windows.Shapes;

namespace OOP_WPF
{
    /// <summary>
    /// Interaction logic for PlayerStat.xaml
    /// </summary>
    public partial class PlayerStat : Window
    {
        private readonly string DIR = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        private Player player;
        public PlayerStat(Player player)
        {
            this.player = player;
            InitializeComponent();
            InitStats();
        }

        private void InitStats()
        {
            playerImage.Source = new BitmapImage(new Uri(DIR + @"\" +player.PicturePath));
            lblNumber.Content = player.ShirtNumber.ToString();
            lblName.Text = player.Name;
            if (player.Captain)
                lblCaptain.Visibility = Visibility.Visible;
            lblPosition.Content = player.Position.ToString();
            lblGoals.Content = player.Goals.ToString();
            lblYCards.Content = player.YCards.ToString();
        }
    }
}

using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TeamOverview.xaml
    /// </summary>
    public partial class TeamOverview : Window
    {
        private IRepo repo = RepoFactory.GetRepo();
        public TeamOverview()
        {
            InitializeComponent();
            InitSettings();
        }

        private void InitSettings()
        {
            string resx = repo.GetResolution();
            if (resx == "fullscreen")
            {
                teamForm.WindowState = WindowState.Maximized;
            }
            else
            {
                teamForm.WindowState = WindowState.Normal;
                string[] res = resx.Split('x');
                int width = int.Parse(res[0].Trim());
                int height = int.Parse(res[1].Trim());
                this.Height = height;
                this.Width = width;
            }
            
            
        }
    }
}

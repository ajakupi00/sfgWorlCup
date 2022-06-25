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
    public partial class MatchStatControl : UserControl
    {
        private string matchLocation;
        private long attendance;
        private string homeTeam;
        private string awayTeam;
        public MatchStatControl()
        {
            InitializeComponent();
        }

        public string MatchLocation
        {
            get => matchLocation;
            set
            {
                matchLocation = value;
                lblLocation.Text = value;
            }
        }
        public long Attendance
        {
            get => attendance;
            set
            {
                attendance = value;
                lblAttendance.Text = value.ToString();
            }
        }
        public string HomeTeam
        {
            get => homeTeam;
            set
            {
                homeTeam = value;
                lblHome.Text = value;
            }
        }
        public string AwayTeam { 
            get => awayTeam; 
            set
            {
                awayTeam = value;
                lblAway.Text = value;
            }
        }
    }
}

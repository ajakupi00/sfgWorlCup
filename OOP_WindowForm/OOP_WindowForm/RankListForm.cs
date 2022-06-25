using dllOOOP.Models;
using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
using dllOOP.Models;
using OOP_WindowForm.UserControls;
using RestSharp;
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
    public partial class RankListForm : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
        
        private ISfg sfg;
        private Sex sex;
        private NationalTeam nation;
        public RankListForm()
        {
            sex = repo.GetSexSetting();
            sfg = SfgFactory.GetSfg(sex);
            nation = repo.GetFavoriteTeam();
            InitializeComponent();
        }

        private void RankListForm_Load(object sender, EventArgs e)
        {
            LoadMatches(nation);
        }

        private async void LoadMatches(NationalTeam nation)
        {
            RestResponse<Match> odgovorPodaci = await sfg.GetMatches(nation);
            List<Match> matches = SfgMenRepo.DeserializeObject(odgovorPodaci);
            matches.Sort((x, y) => -x.Attendance.CompareTo(y.Attendance));
            matches.ForEach(m => {
                pnlMatches.Controls.Add(new MatchStatControl
                {
                    MatchLocation = m.Location,
                    Attendance = m.Attendance,
                    HomeTeam = m.HomeTeamCountry,
                    AwayTeam = m.AwayTeamCountry
                });
            });
        }
    }
}

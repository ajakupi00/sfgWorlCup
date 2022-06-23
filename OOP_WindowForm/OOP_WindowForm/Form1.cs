using dllOOP.DAL;
using dllOOP.DAL.Interfaces;
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
    public partial class Form1 : Form
    {
        private IRepo repo = RepoFactory.GetRepo();
        public Form1()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            label1.Text = "Name: " + Application.ProductName;
            label2.Text = "Current Version: " + Application.ProductVersion;
            label3.Text = "Company Name: " + Application.CompanyName;
            label4.Text = "Last Update: 2020-06-07";
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = "About Us";


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

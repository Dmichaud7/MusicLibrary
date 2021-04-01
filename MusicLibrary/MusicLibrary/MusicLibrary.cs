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
    public partial class MusicLibrary : Form
    {
        private int childFormNumber = 0;

        public MusicLibrary()
        {
            InitializeComponent();
        }

        public StatusStrip StatusBar
        {
            get{ return statusStrip; }
            set { statusStrip = value; }
        }

        public ToolStripStatusLabel StatusStripLabel
        {
            get { return toolStripStatusLabel; }
            set { toolStripStatusLabel = value; }
        }

        public ToolStripProgressBar Progress
        {
            get { return toolStripProgressBar1; }
            set { toolStripProgressBar1 = value; }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = null;
            ToolStripMenuItem m = (ToolStripMenuItem)sender;

            switch (m.Tag)
            {
                case "AddBand":
                    childForm = new frmAddBands();
                    break;
                case "AddMember":
                    childForm = new frmAddMembers();
                    break;
                case "ViewBand":
                    childForm = new frmBands();
                    break;
                case "ViewMember":
                    childForm = new frmMembers();
                    break;
                case "ViewBandMember":
                    childForm = new frmBandMember();
                    break;
                case "About":
                    childForm = new frmAbout();
                    break;

            }

            if (childForm != null)
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f.GetType() == childForm.GetType())
                    {
                        /*
                         This technique requires consideration taken on child forms that load data on form load. 
                         This will result in stale data if data is only retrieved on form load. Also consider loading data on form Activated event also.
                         See frmDataGridViewEvents, frmDataGridViewControls, frmDataGridViewCRUD
                         
                         */
                        f.Activate();
                        return;
                    }
                }

                childForm.MdiParent = this;
                childForm.Show();
            }

            //Form childForm = new Form();
            //childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            //childForm.Show();
        }

        private void ShowForm(object sender, EventArgs e)
        {
            Form childForm = null;
            ToolStripButton m = (ToolStripButton)sender;

            switch (m.Tag)
            {
                case "AddBand":
                    childForm = new frmAddBands();
                    break;
                case "AddMember":
                    childForm = new frmAddMembers();
                    break;
                case "ViewBand":
                    childForm = new frmBands();
                    break;
                case "ViewMember":
                    childForm = new frmMembers();
                    break;
                case "AddBandMember":
                    childForm = new frmBandMember();
                    break;
                case "About":
                    childForm = new frmAbout();
                    break;


            }

            if (childForm != null)
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f.GetType() == childForm.GetType())
                    {
                     
                        f.Activate();
                        return;
                    }
                }

                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MusicLibrary_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            Splash mySplash = new Splash();
            Login myLogin = new Login();

            toolStripStatusLabel.Text = "Welcome";
            toolStripProgressBar1.Visible = false;


            mySplash.ShowDialog();

            if (mySplash.DialogResult != DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                myLogin.ShowDialog();
            }

            if (myLogin.DialogResult == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

     
    }
}

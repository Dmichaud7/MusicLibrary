using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortOrder = System.Windows.Forms.SortOrder;

namespace MusicLibrary
{
    public partial class frmMembers : Form
    {
        public frmMembers()
        {
            InitializeComponent();
        }

        int currentRecord = 0;
        int numberOfMembers = 0;
    


        private void LoadMembers()
        {
            try
            {
                dgvMembers.RowHeadersVisible = false;
                dgvMembers.Visible = false;
                DataTable dt = DataAccess.GetData("SELECT MemberID, FirstName + ' ' + LastName AS FullName FROM Member ORDER BY FullName");
                UIUtilities.FillListControl(cmbMembers, "FullName", "MemberID", dt, true, "---Select a Member---");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void frmMembers_Load(object sender, EventArgs e)
        {

            try
            {
                HideControls(false);
                LoadMembers();
                ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = "Select a Member...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            
        }

        private void cmbMembers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if(cmbMembers.SelectedIndex != 0)
                {
                    HideControls(true);
                    dgvMembers.Visible = true;
                    dgvMembers.ReadOnly = true;
                    string sql = $@"SELECT Band.BandID, Band.BandName, Band.Genre, Band.FoundedDate, Band.[Description], Band.Active 
                    FROM Member INNER JOIN BandMember ON Member.MemberID = BandMember.MemberID
                    INNER JOIN Band ON BandMember.BandID = Band.BandID
                    WHERE Member.MemberID = {cmbMembers.SelectedValue};";

                    DataTable dt = DataAccess.GetData(sql);
                    dgvMembers.DataSource = dt;

                    dgvMembers.Columns["BandID"].Visible = false;
                    dgvMembers.Columns["BandName"].HeaderText = "Band Name";
                    dgvMembers.Columns["FoundedDate"].HeaderText = "Founded Date";

                    numberOfMembers = cmbMembers.Items.Count - 1;
                    currentRecord = Convert.ToInt32(cmbMembers.SelectedIndex);


                    string sqlMember = $@"SELECT MemberID, FirstName, MiddleName, LastName, JoinDate, Instrument FROM Member WHERE MemberID = {cmbMembers.SelectedValue}";
                    DataSet ds = new DataSet();
                    ds = DataAccess.GetDatas(sqlMember);

                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow selectedMember = ds.Tables[0].Rows[0];

                      
                        lblFirstName.Text = selectedMember["FirstName"].ToString();
                        lblMiddleName.Text = selectedMember["MiddleName"].ToString();
                        lblLastName.Text = selectedMember["LastName"].ToString();
                        lblJoinDate.Text = selectedMember["JoinDate"].ToString();
                        lblInstrument.Text = selectedMember["Instrument"].ToString();
                    }


                        
                   

                    DisplayToolStrip();

                    dgvMembers.ReadOnly = true;
                    dgvMembers.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("Please select a member.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

      

        private void DisplayToolStrip()
        {
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Current member {currentRecord} of {numberOfMembers}";
        }

        private void HideControls(bool value)
        {
            
            lbl1.Visible = value;
            lbl2.Visible = value;
            lbl3.Visible = value;
            lbl4.Visible = value;
            lbl5.Visible = value;

            lblFirstName.Visible = value;
            lblMiddleName.Visible = value;
            lblLastName.Visible = value;
            lblJoinDate.Visible = value;
            lblInstrument.Visible = value;
                
     
        }

        private void dgvMembers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewColumn newColumn = dgvMembers.Columns[e.ColumnIndex];
                DataGridViewColumn oldColumn = dgvMembers.SortedColumn;
                ListSortDirection direction;

                // If oldColumn is null, then the DataGridView is not sorted.
                if (oldColumn != null)
                {
                    // Sort the same column again, reversing the SortOrder.
                    if (oldColumn == newColumn &&
                        dgvMembers.SortOrder == SortOrder.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        // Sort a new column and remove the old SortGlyph.
                        direction = ListSortDirection.Ascending;
                        oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }

                // Sort the selected column.
                dgvMembers.Sort(newColumn, direction);
                newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    SortOrder.Ascending : SortOrder.Descending;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void dgvMembers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn column in dgvMembers.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
    }
}

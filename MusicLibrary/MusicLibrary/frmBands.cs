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
    public partial class frmBands : Form
    {
        public frmBands()
        {
            InitializeComponent();
        }

        int currentRecord = 0;
        int numberOfBand = 0;
   


        private void frmBands_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBands();
                HideControls(false);
                ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = "Select a Band...";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            
               
            
          
        }

        private void LoadBands()
        {
            dgvBands.RowHeadersVisible = false;
            dgvBands.Visible = false;
            
            DataTable dt = DataAccess.GetData("SELECT BandID, BandName FROM Band ORDER BY BandName;");
            UIUtilities.FillListControl(cmbBands, "BandName", "BandID", dt, true, "---Select a Band---");

        }

        private void cmbBands_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                

                if (cmbBands.SelectedIndex != 0)
                {
                    HideControls(true);
                    dgvBands.ReadOnly = true;
                    dgvBands.Visible = true;


                    

                    string sql = $@"SELECT Member.MemberID, Member.FirstName, Member.MiddleName, Member.LastName, Member.JoinDate, Member.Instrument
                                FROM Band INNER JOIN BandMember ON Band.BandID = BandMember.BandID
                                INNER JOIN Member ON BandMember.MemberID = Member.MemberID
                                WHERE Band.BandID = {cmbBands.SelectedValue}";


                


                    numberOfBand = cmbBands.Items.Count - 1;
                    currentRecord = Convert.ToInt32(cmbBands.SelectedIndex);

                



                    sql = DataAccess.SQLCleaner(sql);
                  

                    DataTable dt = DataAccess.GetData(sql);
                    dgvBands.DataSource = dt;


                    DisplayToolStrip();

                    dgvBands.Columns["MemberID"].Visible = false;


                    dgvBands.AutoResizeColumns();

                    string sqlBand = $@"SELECT BandID, Genre, FoundedDate, Description, Active FROM Band WHERE BandID = {cmbBands.SelectedValue}";
                    DataSet ds = new DataSet();
                    ds = DataAccess.GetDatas(sqlBand);

                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow selectedBand = ds.Tables[0].Rows[0];


                        lblGenre.Text = selectedBand["Genre"].ToString();
                        lblFoundedDate.Text = selectedBand["FoundedDate"].ToString();
                        txtDescription.Text = selectedBand["Description"].ToString();
                        chkActive.Checked = Convert.ToBoolean(selectedBand["Active"]);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a band.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void DisplayToolStrip()
        {
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Current band {currentRecord} of {numberOfBand}";
        }

        private void HideControls(bool value)
        {
            txtDescription.Visible = value;
            lbl1.Visible = value;
            lbl2.Visible = value;
            lbl3.Visible = value;
            lbl4.Visible = value;
            lblFoundedDate.Visible = value;
            lblGenre.Visible = value;
            chkActive.Visible = value;
        }

        private void dgvBands_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewColumn newColumn = dgvBands.Columns[e.ColumnIndex];
                DataGridViewColumn oldColumn = dgvBands.SortedColumn;
                ListSortDirection direction;

                // If oldColumn is null, then the DataGridView is not sorted.
                if (oldColumn != null)
                {
                    // Sort the same column again, reversing the SortOrder.
                    if (oldColumn == newColumn &&
                        dgvBands.SortOrder == SortOrder.Ascending)
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
                dgvBands.Sort(newColumn, direction);
                newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    SortOrder.Ascending : SortOrder.Descending;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void dgvBands_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                // Put each of the columns into programmatic sort mode.
                foreach (DataGridViewColumn column in dgvBands.Columns)
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

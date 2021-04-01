using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UT = MusicLibrary.UIUtilities;

namespace MusicLibrary
{
    public partial class frmAddBands : Form
    {
        public frmAddBands()
        {
            InitializeComponent();
        }

        int currentRecord = 0;
        int numberOfBand = 0;

        int currentBandID = 0;
        int firsBandID = 0;
        int lastBandID = 0;
        int? previousBandID;
        int? nextBandID;

        #region Events
        private void frmAddBands_Load(object sender, EventArgs e)
        {
            try
            {
                txtBandID.Enabled = false;
                LoadFirstBand();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure you wish to delete this band?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteBand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressBarWait();
                ProgressBar(true);
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (string.IsNullOrEmpty(txtBandID.Text))
                    {
                        CreateBand();

                    }
                    else
                    {
                        SaveBandChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ((MusicLibrary)this.MdiParent).Progress.Value = 0;
                UT.ClearControls(this.grpBands.Controls);
              
                btnSave.Text = "Create";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;

                NavigationState(false);
                ProgressBar(true);
                ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Adding...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadBandDetails();
                
                btnSave.Text = "Save";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;

                NavigationState(true);
                NextPreviousButtonManagement();
                ProgressBar(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void frmAddBands_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
        #endregion

        private void LoadFirstBand()
        {
            object bandID = DataAccess.GetValue("SELECT TOP (1) BandID from Band ORDER BY BandName");

            firsBandID = Convert.ToInt32(bandID);
            currentBandID = firsBandID;
            LoadBandDetails();
            NextPreviousButtonManagement();
            ProgressBar(false);
        }

        #region Helpers
        private void NextPreviousButtonManagement()
        {
            btnPrevious.Enabled = previousBandID != null;
            btnNext.Enabled = nextBandID != null;
        }

        private void NavigationState(bool enableState)
        {
            btnLast.Enabled = enableState;
            btnFirst.Enabled = enableState;
            btnNext.Enabled = enableState;
            btnPrevious.Enabled = enableState;
        }

        private void Navigation_Handler(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            switch (b.Name)
            {
                case "btnFirst":
                    currentBandID = firsBandID;
                    break;
                case "btnLast":
                    currentBandID = lastBandID;
                    break;
                case "btnPrevious":
                    currentBandID = previousBandID.Value;
                    break;
                case "btnNext":
                    currentBandID = nextBandID.Value;
                    break;
            }

            LoadBandDetails();
            NextPreviousButtonManagement();


        }
        private void DisplayToolStrip()
        {
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Current band {currentRecord} of {numberOfBand}";
        }
        private void ProgressBar(bool value)
        {
            ((MusicLibrary)this.MdiParent).Progress.Visible = value;
        }
        private void ProgressBarWait()
        {
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = "Processing...";
            ((MusicLibrary)this.MdiParent).Progress.Value = 0;
            ((MusicLibrary)this.MdiParent).StatusBar.Refresh();

            while (((MusicLibrary)this.MdiParent).Progress.Value < ((MusicLibrary)this.MdiParent).Progress.Maximum)
            {
                Thread.Sleep(10);
                ((MusicLibrary)this.MdiParent).Progress.Value += 1;
            }

            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = "Processed";

        }

        #endregion

        #region Navigation

        private void LoadBandDetails()
        {
            

            string sqlBand = $"SELECT * FROM Band WHERE BandID = {currentBandID}";


            string sqlNav =

            $@"
                SELECT 
                (SELECT COUNT(*) FROM Band) AS NumberOfBand,
                (SELECT TOP(1) BandID as FirstBandID FROM Band ORDER BY BandName
                ) as FirstBandID,
                q.PreviousBandID,
                q.NextBandID,
                (
                    SELECT TOP(1) BandID as LastBandID FROM Band ORDER BY BandName Desc
                ) as LastBandID,
                q.RowNumber
                FROM
                (
                    SELECT BandID, BandName,
                    LEAD(BandID) OVER(ORDER BY BandName) AS NextBandID,
                    LAG(BandID) OVER(ORDER BY BandName) AS PreviousBandID,
                    ROW_NUMBER() OVER(ORDER BY BandName) AS 'RowNumber'
                    FROM Band
                ) AS q
                WHERE q.BandID = {currentBandID}
                ORDER BY q.BandName
                ";


            sqlNav = DataAccess.SQLCleaner(sqlNav);
            string[] sqlStatements = new string[] { sqlBand, sqlNav };

            DataSet ds = new DataSet();
            ds = DataAccess.GetData(sqlStatements);

            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow selectedBand = ds.Tables[0].Rows[0];

                txtBandID.Text = selectedBand["BandID"].ToString();
                txtBandName.Text = selectedBand["BandName"].ToString();
                txtGenre.Text = selectedBand["Genre"].ToString();
                dtpFoundedDate.Text = selectedBand["FoundedDate"].ToString();
                txtDescription.Text = selectedBand["Description"].ToString();
                chkActive.Checked = Convert.ToBoolean(selectedBand["Active"]);

                numberOfBand = Convert.ToInt32(ds.Tables["Table1"].Rows[0]["NumberOfBand"]);
                firsBandID = Convert.ToInt32(ds.Tables[1].Rows[0]["FirstBandID"]);
                previousBandID = ds.Tables[1].Rows[0]["PreviousBandID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["PreviousBandID"]) : (int?)null;
                nextBandID = ds.Tables[1].Rows[0]["NextBandID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["NextBandID"]) : (int?)null;
                lastBandID = Convert.ToInt32(ds.Tables[1].Rows[0]["LastBandID"]);
                currentRecord = Convert.ToInt32(ds.Tables[1].Rows[0]["RowNumber"]);

                DisplayToolStrip();
            }
            else
            {
                MessageBox.Show("The band no longer exists");
                LoadFirstBand();
            }
        }

        #endregion

        #region NoQuery

        private void CreateBand()
        {
            if (txtBandName.Text == "" || txtGenre.Text == "")
            {
                MessageBox.Show("Please fill all the required fields! (Band Name and Genre)");
            }
            else
            {
                //Bus rule

                string sqlbandName = $"SELECT COUNT(*) FROM Band WHERE bandName = '{txtBandName.Text}'";

                int bandNameCheck = Convert.ToInt32(DataAccess.GetValue(sqlbandName));

                if (bandNameCheck == 0)
                {
                    string sqlInsertBand = $@"
                    INSERT INTO Band 
                    (BandName,Genre,FoundedDate,Description,Active)
                    VALUES
                    (
                        '{DataAccess.SQLFix(txtBandName.Text.Trim())}',
                        '{DataAccess.SQLFix(txtGenre.Text.Trim())}',
                        '{dtpFoundedDate.Text}',
                         '{DataAccess.SQLFix(txtDescription.Text.Trim())}',
                        {(chkActive.Checked ? 1 : 0)}
                    
                    )";

                    sqlInsertBand = DataAccess.SQLCleaner(sqlInsertBand);
                    int rowsAffected = DataAccess.SendData(sqlInsertBand);

                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Band was created");
                        btnAdd_Click(null, null);

                        UT.ClearControls(this.grpBands.Controls);
                    }
                    else
                    {
                        MessageBox.Show("The database reported no rows affected");
                    }
                }
                else
                {
                    MessageBox.Show("Band already exist");
                }
            }
            

            NavigationState(false);
        }

        private void SaveBandChanges()
        {

            if(txtBandName.Text == "" || txtGenre.Text == "")
            {
                MessageBox.Show("Please fill all the required fields! (Band Name and Genre)");
            }
            else
            {
                string updateBands = $@"
            UPDATE Band
            SET BandName = '{DataAccess.SQLFix(txtBandName.Text.Trim())}',
            Genre = '{DataAccess.SQLFix(txtGenre.Text.Trim())}',
            FoundedDate = '{dtpFoundedDate.Text}',
            Description = '{DataAccess.SQLFix(txtDescription.Text.Trim())}',
            Active = {(chkActive.Checked ? 1 : 0)}
            WHERE BandID = {txtBandID.Text}";

                updateBands = DataAccess.SQLCleaner(updateBands);

                int rowAffected = DataAccess.SendData(updateBands);

                if (rowAffected == 1)
                {
                    MessageBox.Show("Band updated");
                    LoadFirstBand();
                }
                else
                {
                    MessageBox.Show("The database reported no rows affected");
                }
            }

     
        }
        

        private void DeleteBand()
        {



            string sqlBandCheck = $"SELECT COUNT(*) FROM BandMember WHERE BandID = {txtBandID.Text}";
            int bandCheck = Convert.ToInt32(DataAccess.GetValue(sqlBandCheck));

            if(bandCheck == 0)
            {
                string sqlDeleteBands = $"DELETE FROM Band WHERE BandID = {txtBandID.Text}";

                int rowsAffected = DataAccess.SendData(sqlDeleteBands);

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Band was been delete");
                }
                else
                {
                    MessageBox.Show("The databse reported no rows affected");
                }
            }
            else
            {
                MessageBox.Show("This band cannot be delete becasue it still was members in it");
            }

           
        }
        #endregion


    }
}

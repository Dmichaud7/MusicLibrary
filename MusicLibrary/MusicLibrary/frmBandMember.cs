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
    public partial class frmBandMember : Form
    {
        public frmBandMember()
        {
            InitializeComponent();
        }



        int currentRecord = 0;
        int numberOfBandMember = 0;

        int currentBandID = 0;
        int currentMemberID = 0;
       

        int firstBandID = 0;
        int firstMemberID = 0;

        int lastBandID = 0;
        int lastMemberID = 0;

        int? nextBandID = 0;
        int? nextMemberID = 0;

        int? previousBandID = 0;
        int? previousMemberID = 0;

        #region Events
        private void frmBandMember_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBands();
                LoadMembers();
                LoadFirstBandMembers();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                UT.ClearControls(this.grpBandMember.Controls);
                ((MusicLibrary)this.MdiParent).Progress.Value = 0;

                LoadBands();
                LoadMembers();

      

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you wish to remove this band menber from the band?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteBandMember();
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
                    if(btnSave.Text == "Create")
                    {
                        CreateBandMember();
                    }
                    else
                    {
                        SaveMemberChanges();
                    }

                    
                }
             
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
                LoadBandMembers();
               

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
        #endregion

        #region Navigation
        private void NavigationState(bool enableState)
        {
            btnLast.Enabled = enableState;
            btnFirst.Enabled = enableState;
            btnNext.Enabled = enableState;
            btnPrevious.Enabled = enableState;
        }

        private void LoadBands()
        {
            //string sqlBands = "SELECT BandID, BandName FROM Band ORDER BY BandName";
            DataTable dt = DataAccess.GetData("SELECT BandID, BandName FROM Band ORDER BY BandName");
            UT.FillListControl(cmbBand, "BandName", "BandID", dt, true, "---Please select a band---");
        }

        private void LoadMembers()
        {
            //string sqlmembers = "SELECT MemberID, FirstName + ' ' + LastName AS FullName FROM Member ORDER BY FullName";
            DataTable dt = DataAccess.GetData("SELECT MemberID, FirstName + ' ' + LastName AS FullName FROM Member ORDER BY FirstName, LastName");
            UT.FillListControl(cmbMember, "FullName", "MemberID", dt, true, "---Please select a member---");
        }

        private void LoadFirstBandMembers()
        {
            DataTable firstBandMember = DataAccess.GetData("SELECT TOP (1) BandID, MemberID FROM BandMember");

            if (firstBandMember.Rows.Count > 0)
            {
                currentBandID = Convert.ToInt32(firstBandMember.Rows[0]["BandID"]);
                currentMemberID = Convert.ToInt32(firstBandMember.Rows[0]["MemberID"]);
                

                firstBandID = currentBandID;
                firstMemberID = currentMemberID;


                LoadBandMembers();
                NextPreviousButtonManagement();

            }
        }

        private void LoadBandMembers()
        {
            

            string sqlMemberID = $"SELECT * FROM BandMember WHERE BandID = {currentBandID} AND MemberID = {currentMemberID}";
            string sqlNav = 
                $@"
                    SELECT 
                    (SELECT COUNT(*) FROM BandMember) AS NumberOfBandMember,
                    (SELECT TOP(1) BandID as FirstBandID FROM BandMember
                    ) as FirstBandID,
                    (
                        SELECT TOP(1) MemberID as FirstMemberID FROM BandMember
                    ) as FirstMemberID,
                    q.PreviousBandID,
                    q.PreviousMemberID,
                    q.NextBandID,
                    q.NextMemberID,
                    (
                        SELECT TOP(1) BandID as LastBandID FROM BandMember ORDER BY BandID Desc
                    ) as LastBandID,
                    (
                        SELECT TOP(1) MemberID as LastMemberID FROM BandMember ORDER BY BandID Desc
                    ) as LastMemberID
                    FROM
                    (
                        SELECT BandID, MemberID,
	                    LEAD(BandID) OVER(ORDER BY BandID) AS NextBandID,
	                    LEAD(MemberID) OVER(ORDER BY BandID) AS NextMemberID,  
	                    LAG(BandID) OVER(ORDER BY BandID) AS PreviousBandID,
	                    LAG(MemberID) OVER(ORDER BY BandID) AS PreviousMemberID,
                        ROW_NUMBER() OVER(ORDER BY BandID) AS 'RowNumber'
                        FROM BandMember
                    ) AS q
                    WHERE q.BandID = {currentBandID} AND q.MemberID = {currentMemberID}
                    ORDER BY q.BandID, q.MemberID
                    ";
            

            sqlNav = DataAccess.SQLCleaner(sqlNav);

            string[] sqlStatements = new string[] { sqlMemberID, sqlNav };

            DataSet ds = new DataSet();
            ds = DataAccess.GetData(sqlStatements);

            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow selectedBandMember = ds.Tables[0].Rows[0];
                int bandID = Convert.ToInt32(selectedBandMember["BandID"]);
                int memberID = Convert.ToInt32(selectedBandMember["MemberID"]);

                cmbBand.SelectedValue = selectedBandMember["BandID"];
                cmbMember.SelectedValue = selectedBandMember["MemberID"];

                numberOfBandMember = Convert.ToInt32(ds.Tables[1].Rows[0]["NumberOfBandMember"]);
                firstBandID = Convert.ToInt32(ds.Tables[1].Rows[0]["FirstBandID"]);
                firstMemberID = Convert.ToInt32(ds.Tables[1].Rows[0]["FirstMemberID"]);
                previousBandID = ds.Tables[1].Rows[0]["PreviousBandID"] != DBNull.Value ? Convert.ToInt32(ds.Tables["Table1"].Rows[0]["PreviousBandID"]) : (int?)null;
                previousMemberID = ds.Tables[1].Rows[0]["PreviousMemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables["Table1"].Rows[0]["PreviousMemberID"]) : (int?)null; ;
                nextBandID = ds.Tables[1].Rows[0]["NextBandID"] != DBNull.Value ? Convert.ToInt32(ds.Tables["Table1"].Rows[0]["NextBandID"]) : (int?)null;
                nextMemberID = ds.Tables[1].Rows[0]["NextMemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables["Table1"].Rows[0]["NextMemberID"]) : (int?)null; ;
                lastBandID = Convert.ToInt32(ds.Tables[1].Rows[0]["LastBandID"]);
                lastMemberID = Convert.ToInt32(ds.Tables[1].Rows[0]["LastMemberID"]);
                //currentRecord = Convert.ToInt32(ds.Tables[1].Rows[0]["RowNumber"]);


                DisplayToolStrip();
            }
            else
            {
              
                MessageBox.Show("The band member no longer exists");
                LoadFirstBandMembers();
            }

          
        }
        private void NextPreviousButtonManagement()
        {
            btnPrevious.Enabled = previousMemberID != null;
            btnNext.Enabled = nextMemberID != null;
        }

        private void Navigation_Handler(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            switch (b.Name)
            {
                case "btnFirst":
                    currentBandID = firstBandID;
                    currentMemberID = firstMemberID;
                    break;
                case "btnLast":
                    currentBandID = lastBandID;
                    currentMemberID = lastMemberID;
                    break;
                case "btnPrevious":
                    currentBandID = previousBandID.Value;
                    currentMemberID = previousMemberID.Value;
                    break;
                case "btnNext":
                    currentBandID = nextBandID.Value;
                    currentMemberID = nextMemberID.Value;
                    break;
            }

            ProgressBar(false);
            LoadBandMembers();
            NextPreviousButtonManagement();


        }

        #endregion

        #region NoQuery

        private void CreateBandMember()
        {
            

            if(cmbBand.SelectedIndex > 0 && cmbMember.SelectedIndex > 0)
            {
                //bus rule
                string memberCount = $"SELECT COUNT(*) FROM BandMember WHERE MemberID = {cmbMember.SelectedValue}";
                int countMember = Convert.ToInt32(DataAccess.GetValue(memberCount));


                
                if(countMember <= 3)
                {
                    int bandID = Convert.ToInt32(cmbBand.SelectedValue);
                    int memberID = Convert.ToInt32(cmbMember.SelectedValue);
                    string sqlInsertBandMember = $"INSERT INTO BandMember (BandID, MemberID) VALUES({bandID}, {memberID})";

                    int rowsAffected = DataAccess.SendData(sqlInsertBandMember);

                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Band member was created.");
                        LoadFirstBandMembers();
                        btnAdd_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("The database did not report any rows affected");
                    }
                }
                else
                {
                    MessageBox.Show("Member cannot be part of more then 4 band at a time!");
                }

             
            }
            else
            {
                MessageBox.Show("Make sure to select a band and member");
            }

         
        }

        private void DeleteBandMember()
        {
            int bandID = Convert.ToInt32(cmbBand.SelectedValue);
            int memberID = Convert.ToInt32(cmbMember.SelectedValue);

            Console.WriteLine(bandID);
            Console.WriteLine(memberID);

            string sqlMemberBandCheck = $"SELECT COUNT(*) FROM BandMember WHERE BandID = {bandID} AND MemberID = {memberID}";
            int memberBandCheck = Convert.ToInt32(DataAccess.GetValue(sqlMemberBandCheck));

            if(memberBandCheck == 1)
            {
                string sqlDeleteBandMember = $"DELETE FROM BandMember WHERE MemberID = {memberID} AND BandID = {bandID}";

                int rowsAffected = DataAccess.SendData(sqlDeleteBandMember);

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Band member was been remove");
                    LoadBandMembers();

                }
                else
                {
                    MessageBox.Show("Band member does not exist");
                }
            }
            else
            {
                MessageBox.Show("This band member cannot be delete becasue it does not exist ");
            }




        }

        private void SaveMemberChanges()
        {
            if (cmbBand.SelectedIndex > 0 && cmbMember.SelectedIndex > 0)
            {

                string updateBandMember = $@"
                UPDATE BandMember
                SET  
                BandID = {cmbBand.SelectedValue},
                MemberID = {cmbMember.SelectedValue}
                WHERE MemberID = {cmbMember.SelectedValue} AND BandID = {cmbBand.SelectedValue}";

                updateBandMember = DataAccess.SQLCleaner(updateBandMember);

                int rowAffected = DataAccess.SendData(updateBandMember);

                if (rowAffected == 1)
                {
                    MessageBox.Show("Band member updated");
                    LoadBandMembers();
                }
                else
                {
                    MessageBox.Show("The database reported no rows affected");
                }
            }

            else
            {
                MessageBox.Show("Make sure to select a band and member");
            }


        }
        #endregion
        #region Helper

        private void DisplayToolStrip()
        {

            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Current band {currentRecord} of {numberOfBandMember}";

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

  

        private void frmBandMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
          
        }
        #endregion
    }
}

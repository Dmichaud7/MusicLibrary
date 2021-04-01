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
using UI = MusicLibrary.UIUtilities;

namespace MusicLibrary
{
    public partial class frmAddMembers : Form
    {
        public frmAddMembers()
        {
            InitializeComponent();
        }

        int currentRecord = 0;
        int numberOfMember = 0;

        int currentMemberID = 0;
        int firstMemberID = 0;
        int lastMemberID = 0;
        int? previousMemberID;
        int? nextMemberID;


   

        #region Events
        private void frmAddMembers_Load(object sender, EventArgs e)
        {
            try
            {
                txtMemberID.Enabled = false;
                LoadFirstMember();
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
                UI.ClearControls(this.grpMembers.Controls);
                
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
                
                LoadMemberDetails();
                
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressBarWait();
                ProgressBar(true);
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (string.IsNullOrEmpty(txtMemberID.Text))
                    {
                        CreateMember();
                        
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you wish to delete this member?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteMember();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void frmAddMembers_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
        #endregion




        #region Navigation
        private void LoadFirstMember()
        {
            object memberID = DataAccess.GetValue("SELECT TOP (1) MemberID from Member ORDER BY FirstName, LastName");

            firstMemberID = Convert.ToInt32(memberID);
            currentMemberID = firstMemberID;
            LoadMemberDetails();
            NextPreviousButtonManagement();
            ProgressBar(false);
        }

        private void LoadMemberDetails()
        {
          

            string sqlMember = $"SELECT * FROM Member WHERE MemberID = {currentMemberID}";

            string sqlNav =
                $@"
                SELECT 
                (SELECT COUNT(*) FROM Member) AS NumberOfMember,
                (SELECT TOP(1) MemberID as FirstMemberID FROM Member ORDER BY FirstName,LastName
                ) as FirstMemberID,
                q.PreviousMemberID,
                q.NextMemberID,
                (
                    SELECT TOP(1) MemberID as LastMemberID FROM Member ORDER BY FirstName Desc,LastName Desc
                ) as LastMemberID,
                q.RowNumber
                FROM
                (
                    SELECT MemberID, FirstName, LastName,
                    LEAD(MemberID) OVER(ORDER BY FirstName,LastName) AS NextMemberID,
                    LAG(MemberID) OVER(ORDER BY FirstName,LastName) AS PreviousMemberID,
                    ROW_NUMBER() OVER(ORDER BY FirstName,LastName) AS 'RowNumber'
                    FROM Member
                ) AS q
                WHERE q.MemberID = {currentMemberID}
                ORDER BY q.FirstName,q.LastName
                ";

            sqlNav = DataAccess.SQLCleaner(sqlNav);
            
            string[] sqlStatements = new string[] { sqlMember, sqlNav };
            DataSet ds = new DataSet();
            ds = DataAccess.GetData(sqlStatements);

            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow selectedMember = ds.Tables[0].Rows[0];

                txtMemberID.Text = selectedMember["MemberID"].ToString();
                txtFirstName.Text = selectedMember["FirstName"].ToString();
                txtMiddleName.Text = selectedMember["MiddleName"].ToString();
                txtLastName.Text = selectedMember["LastName"].ToString();
                dtpJoinDate.Text = selectedMember["JoinDate"].ToString();
                txtInstrument.Text = selectedMember["Instrument"].ToString();

                numberOfMember = Convert.ToInt32(ds.Tables["Table1"].Rows[0]["NumberOfMember"]);
                firstMemberID = Convert.ToInt32(ds.Tables[1].Rows[0]["FirstMemberID"]);
                previousMemberID = ds.Tables[1].Rows[0]["PreviousMemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["PreviousMemberID"]) : (int?)null;
                nextMemberID = ds.Tables[1].Rows[0]["NextMemberID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["NextMemberID"]) : (int?)null;
                lastMemberID = Convert.ToInt32(ds.Tables[1].Rows[0]["LastMemberID"]);
                currentRecord = Convert.ToInt32(ds.Tables[1].Rows[0]["RowNumber"]);

                DisplayToolStrip();
            }
            else
            {
                MessageBox.Show("The member no longer exists");
                LoadFirstMember();
            }
        }
        #endregion

        #region Navigation Helpers

        private void NextPreviousButtonManagement()
        {
            btnPrevious.Enabled = previousMemberID != null;
            btnNext.Enabled = nextMemberID != null;
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
                    currentMemberID = firstMemberID;
                    break;
                case "btnLast":
                    currentMemberID = lastMemberID;
                    break;
                case "btnPrevious":
                    currentMemberID = previousMemberID.Value;
                    break;
                case "btnNext":
                    currentMemberID = nextMemberID.Value;
                    break;
            }

            LoadMemberDetails();
            NextPreviousButtonManagement();


        }
        #endregion







        #region NoQuery

        private void CreateMember()
        {

            //Bus rule

            string sqlfullName = $"SELECT COUNT(*) FROM Member WHERE firstName = '{txtFirstName.Text}' AND lastName ='{txtLastName.Text}'";

            int firstNameMember = Convert.ToInt32(DataAccess.GetValue(sqlfullName));
           

            if (firstNameMember == 0 )
            {
                if(txtFirstName.Text == "" || txtLastName.Text == "" || txtInstrument.Text == "")
                {
                    MessageBox.Show("Please fill all the required fields! (First Name, Last Name and Instrument)");
                }
                else
                {
                    string sqlInsertMember = $@"
                INSERT INTO Member 
                (FirstName,MiddleName,LastName,JoinDate,Instrument)
                VALUES
                (
                    '{DataAccess.SQLFix(txtFirstName.Text.Trim())}',
                    '{DataAccess.SQLFix(txtMiddleName.Text.Trim())}',
                    '{DataAccess.SQLFix(txtLastName.Text.Trim())}',
                     '{dtpJoinDate.Text}',
                    '{DataAccess.SQLFix(txtInstrument.Text.Trim())}'

                    )
                ";

                    sqlInsertMember = DataAccess.SQLCleaner(sqlInsertMember);

                    int rowsAffected = DataAccess.SendData(sqlInsertMember);

                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Member was created");
                        btnAdd_Click(null, null);


                        UI.ClearControls(this.grpMembers.Controls);

                    }
                    else
                    {
                        MessageBox.Show("The database reported no rows affected");
                    }
                }

               

            }
            else
            {
                MessageBox.Show("Member already exist");
            }

            NavigationState(false);

        }

        private void SaveMemberChanges()
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtInstrument.Text == "")
            {
                MessageBox.Show("Please fill all the required fields! (First Name, Last Name and Instrument)");
            }
            else
            {
                string updateMember = $@"
                UPDATE Member
                SET  
                FirstName = '{DataAccess.SQLFix(txtFirstName.Text.Trim())}',
                MiddleName = '{DataAccess.SQLFix(txtMiddleName.Text.Trim())}',
                LastName = '{DataAccess.SQLFix(txtLastName.Text.Trim())}',
                JoinDate = '{dtpJoinDate.Text}',
                Instrument = '{DataAccess.SQLFix(txtInstrument.Text.Trim())}'
                WHERE MemberID = {txtMemberID.Text}";

                updateMember = DataAccess.SQLCleaner(updateMember);

                int rowAffected = DataAccess.SendData(updateMember);

                if (rowAffected == 1)
                {
                    MessageBox.Show("Member updated");
                    LoadFirstMember();
                }
                else
                {
                    MessageBox.Show("The database reported no rows affected");
                }
            }

           
        

         
        }
        private void DeleteMember()
        {
            string sqlMemberCheck = $"SELECT COUNT(*) FROM BandMember WHERE MemberID = {txtMemberID.Text}";
            int memberCheck = Convert.ToInt32(DataAccess.GetValue(sqlMemberCheck));

          

            if (memberCheck == 0)
            {
                string sqlDeleteMember = $"DELETE FROM Member WHERE MemberID = {txtMemberID.Text}";

                int rowsAffected = DataAccess.SendData(sqlDeleteMember);

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Member was been delete");
                    LoadFirstMember();
                    
                }
                else
                {
                    MessageBox.Show("The databse reported no rows affected");
                }
            }
            else
            {
                MessageBox.Show("This Member cannot be delete becasue it still part of a band");
            }


        }
        #endregion

        #region Helper

        private void DisplayToolStrip()
        {
            ((MusicLibrary)this.MdiParent).StatusStripLabel.Text = $"Current member {currentRecord} of {numberOfMember}";
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
    }



}


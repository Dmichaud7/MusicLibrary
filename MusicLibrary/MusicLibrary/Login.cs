using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicLibrary
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable login = DataAccess.GetData($@"SELECT * FROM Login WHERE Username = '{txtUsername.Text.Trim().ToLower()}' AND Password = '{txtPassword.Text.Trim().ToLower()}'");

                

                if (login.Rows.Count == 1)
                {
                    //login was successful
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Login failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Login_Load(object sender, EventArgs e)
        {

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            btnLogin.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.Transparent;
            this.Text = Application.ProductName + " - Login";
        
            txtUsername.Text = Environment.UserName;
            // this will mask the password box with a bullet
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

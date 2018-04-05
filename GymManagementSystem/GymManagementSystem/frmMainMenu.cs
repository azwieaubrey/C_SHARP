using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace GymManagementSystem
{
    public partial class frmMainMenu : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.ShowDialog();
        }

        private void subCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubCategory frm = new frmSubCategory();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.ShowDialog();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistration frm = new frmRegistration();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

        private void membershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMembershipType frm = new frmMembershipType();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.ShowDialog();
        }

  

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Calc.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Notepad.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Wordpad.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void mSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("WinWord.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("TaskMgr.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString();
        }

 

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogs frm = new frmLogs();
            frm.Reset();
            frm.lblUser.Text = lblUser.Text;
            frm.ShowDialog();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer2.Enabled = false;
        }

    

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

 

        private void membershipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            button1.PerformClick();
        }


   
        private void backupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void restoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var _with1 = openFileDialog1;
                _with1.Filter = ("DB Backup File|*.bak;");
                _with1.FilterIndex = 4;
                //Clear the file name
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    timer2.Enabled = true;
                    SqlConnection.ClearAllPools();
                    cc.con = new SqlConnection(cs.DBConn);
                    cc.con.Open();
                    string cb = "USE Master ALTER DATABASE [" + System.Windows.Forms.Application.StartupPath + "\\gms_db.mdf] SET Single_User WITH Rollback Immediate Restore database [" + System.Windows.Forms.Application.StartupPath + "\\gms_db.mdf] FROM disk='" + openFileDialog1.FileName + "' WITH REPLACE ALTER DATABASE [" + System.Windows.Forms.Application.StartupPath + "\\gms_db.mdf] SET Multi_User ";
                    cc.cmd = new SqlCommand(cb);
                    cc.cmd.Connection = cc.con;
                    cc.cmd.ExecuteReader();
                    cc.con.Close();
                    st1 = lblUser.Text;
                    st2 = "Successfully restore the database";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Successfully performed", "Database Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

        private void supplierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSupplier frm = new frmSupplier();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStock frm = new frmStock();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

        private void membershipToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmCustomerMembership frm = new frmCustomerMembership();
            frm.lblUser.Text = lblUser.Text;
            frm.Reset();
            frm.Show();
        }

        private void fitnessMeasureToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void salesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            st1 = lblUser.Text;
            st2 = "Successfully logged out";
            cf.LogFunc(st1, System.DateTime.Now, st2);
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
            frm.UserID.Text = "";
            frm.Password.Text = "";
            frm.ProgressBar1.Visible = false;
            frm.UserID.Focus();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace GymManagementSystem
{
    public partial class frmMembershipType : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public frmMembershipType()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            Autocomplete();
            GetData();
        }
        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string sql = "SELECT MAX(M_ID+1) FROM Membership";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                }
                cc.cmd.Dispose();
                cc.con.Close();
                cc.con.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cq = "delete from Membership where M_ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cq);
                cc.cmd.Connection = cc.con;
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lblUser.Text;
                    st2 = "deleted the membership type '" + txtMembershipType.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    Reset();
                    Autocomplete();
                    GetData();
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Autocomplete();
                    GetData();
                }
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Autocomplete()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT distinct Type FROM Membership", cc.con);
                cc.ds = new DataSet();
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.da.Fill(cc.ds, "Membership");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= cc.ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(cc.ds.Tables[0].Rows[i]["Type"].ToString());

                }
                txtMembershipType.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtMembershipType.AutoCompleteCustomSource = col;
                txtMembershipType.AutoCompleteMode = AutoCompleteMode.Suggest;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        public void Reset()
        {
            txtMembershipType.Text = "";
            txtID.Text = "";
            txtChargesPerMonth.Text="";
            txtServiceTax.Text = "";
            txtVAT.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtMembershipType.Focus();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMembershipType.Text == "")
            {
                MessageBox.Show("Please enter sub category", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMembershipType.Focus();
                return;
            }
            if (txtChargesPerMonth.Text == "")
            {
                MessageBox.Show("Please enter charges/month", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChargesPerMonth.Focus();
                return;
            }
            if (txtServiceTax.Text == "")
            {
                MessageBox.Show("Please enter service tax", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServiceTax.Focus();
                return;
            }
            if (txtVAT.Text == "")
            {
                MessageBox.Show("Please enter VAT", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVAT.Focus();
                return;
            }
            try
            {
                auto();
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string ct = "select type from Membership where type=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtMembershipType.Text);
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    MessageBox.Show("Membership Type Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMembershipType.Text = "";
                    txtMembershipType.Focus();
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    return;
                }

                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "insert into Membership(M_ID,Type,ChargesPerMonth,ServiceTax,VAT) VALUES (" + txtID.Text + ",@d1," + txtChargesPerMonth.Text + "," + txtServiceTax.Text +"," + txtVAT.Text + ")";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtMembershipType.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "added the new membership type '" + txtMembershipType.Text + "'";
                cf.LogFunc(st1,System.DateTime.Now,st2);
                Autocomplete();
                GetData();
                btnSave.Enabled = false;
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMembershipType.Text == "")
                {
                    MessageBox.Show("Please enter sub category", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMembershipType.Focus();
                    return;
                }
                if (txtChargesPerMonth.Text == "")
                {
                    MessageBox.Show("Please enter charges/month", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChargesPerMonth.Focus();
                    return;
                }
                if (txtServiceTax.Text == "")
                {
                    MessageBox.Show("Please enter service tax", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtServiceTax.Focus();
                    return;
                }
                if (txtVAT.Text == "")
                {
                    MessageBox.Show("Please enter VAT", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVAT.Focus();
                    return;
                }
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "update Membership set Type=@d1,ChargesPerMonth=" + txtChargesPerMonth.Text + ",ServiceTax=" + txtServiceTax.Text +",VAT=" + txtVAT.Text + " where M_ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtMembershipType.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "updated the membership type '" + txtMembershipType.Text + "' details";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                Autocomplete();
                GetData();
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dgw.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dgw.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
    
        }

        private void dgw_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow dr = dgw.SelectedRows[0];
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            txtID.Text = dr.Cells[0].Value.ToString();
            txtMembershipType.Text = dr.Cells[1].Value.ToString();
            txtChargesPerMonth.Text = dr.Cells[2].Value.ToString();
            txtServiceTax.Text = dr.Cells[3].Value.ToString();
            txtVAT.Text = dr.Cells[4].Value.ToString();
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            txtMembershipType.Focus();
            btnSave.Enabled = false;
        }
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                String sql = "SELECT RTRIM(M_ID),RTRIM(Type),RTRIM(ChargesPerMonth),RTRIM(ServiceTax),RTRIM(VAT) from Membership order by Type";
                cc.cmd = new SqlCommand(sql, cc.con);
                cc.rdr = cc.cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgw.Rows.Clear();
                while (cc.rdr.Read() == true)
                {
                    dgw.Rows.Add(cc.rdr[0], cc.rdr[1],cc.rdr[2],cc.rdr[3],cc.rdr[4]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChargesPerMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtServiceTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
    
    }
}

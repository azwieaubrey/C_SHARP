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
    public partial class frmCustomerMembership : Form
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public frmCustomerMembership()
        {
            InitializeComponent();
        }
        public void Calculate()
        {
            try
            {
                double val1 = 0;
                double val2 = 0;
                double val3 = 0;
                double val4 = 0;
                double val5 = 0;
                double val6 = 0;
                double val7 = 0;
                double val8 = 0;
                double val9 = 0;
                double val10 = 0;
                double val11 = 0;
                int val12 = 0;
                double val13 = 0;
                int.TryParse(txtMonths.Text, out val12);
                double.TryParse(txtChargesPerMonth.Text, out val13);
                double.TryParse(txtDiscountPer.Text, out val2);
                double.TryParse(txtServiceTaxPer.Text, out val5);
                double.TryParse(txtVATPer.Text, out val7);
                double.TryParse(txtTotalPaid.Text, out val10);
                val1 = Convert.ToDouble(val12 * val13);
                val1 = Math.Round(val1, 2);
                txtTotalCharges.Text = val1.ToString();
                val3 = Convert.ToDouble((val1 * val2) / 100);
                val3 = Math.Round(val3, 2);
                txtDiscountAmount.Text = val3.ToString();
                val4 = Convert.ToDouble(val1 - val3);
                val4 = Math.Round(val4, 2);
                txtSubTotal.Text = val4.ToString();
                val6 = Convert.ToDouble((val4 * val5) / 100);
                val6 = Math.Round(val6, 2);
                txtServiceTaxAmount.Text = val6.ToString();
                val8 = Convert.ToDouble((val4 * val7) / 100);
                val8 = Math.Round(val8, 2);
                txtVATAmount.Text = val8.ToString();
                val9 = Convert.ToDouble(val4 + val6 + val8);
                val9 = Math.Round(val9, 2);
                txtGrandTotal.Text = val9.ToString();
                val11 = Convert.ToDouble(val9 - val10);
                val11 = Math.Round(val11, 2);
                txtBalance.Text = val11.ToString();
                dtpDateTo.Text= dtpDateFrom.Value.Date.AddMonths(val12).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string sql = "SELECT MAX(CM_ID+1) FROM CustomerMembership";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                    txtMembershipID.Text = Convert.ToString("M" + Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                    txtMembershipID.Text = Convert.ToString("M" + Num);
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Reset()
        {
            txtAddress.Text = "";
            txtBalance.Text = "";
            txtChargesPerMonth.Text = "";
            txtTotalPaid.Text = "";
            txtTotalCharges.Text = "";
            txtSubTotal.Text = "";
            txtServiceTaxPer.Text = "";
            txtServiceTaxAmount.Text = "";
            txtMonths.Text = "";
            txtMembershipID.Text = "";
            txtMemberName.Text = "";
            txtVATAmount.Text = "";
            txtVATPer.Text = "";
            txtM_ID.Text = "";
            txtID.Text = "";
            txtCity.Text = "";
            txtContactNo.Text = "";
            txtDiscountAmount.Text = "";
            txtDiscountPer.Text = "";
            txtEmailID.Text = "";
            cmbMembershipType.SelectedIndex = -1;
            txtGrandTotal.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            Picture.Image = Properties.Resources.photo;
            dtpBillDate.Text = System.DateTime.Now.ToString();
            dtpDateFrom.Text = System.DateTime.Today.ToString();
            dtpDateTo.Text = System.DateTime.Today.ToString();
            dtpBillDate.Enabled = true;
            auto();
        }
        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtTotalPaid_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtMonths_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }
        public void FillCombo()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string ct = "select RTRIM(type) from Membership order by type";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                while (cc.rdr.Read())
                {
                    cmbMembershipType.Items.Add(cc.rdr[0]);
                }
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbMembershipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = cc.con.CreateCommand();
                cc.cmd.CommandText = "SELECT M_ID, ServiceTax,VAT,ChargesPerMonth from Membership where Type=@d1";
                cc.cmd.Parameters.AddWithValue("@d1", cmbMembershipType.Text);
                cc.rdr = cc.cmd.ExecuteReader();

                if (cc.rdr.Read())
                {
                    txtMembershipTypeID.Text = cc.rdr.GetValue(0).ToString().Trim();
                    txtServiceTaxPer.Text = cc.rdr.GetValue(1).ToString().Trim();
                    txtVATPer.Text = cc.rdr.GetValue(2).ToString().Trim();
                    txtChargesPerMonth.Text = cc.rdr.GetValue(3).ToString().Trim();
                }
                if ((cc.rdr != null))
                {
                    cc.rdr.Close();
                }
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }
                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomerMembership_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void txtMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDiscountPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtTotalPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerRecord frm = new frmCustomerRecord();
            frm.Reset();
            frm.lblOperation.Text = "Membership";
            frm.lblUser.Text = lblUser.Text;
            frm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberName.Text == "")
                {
                    MessageBox.Show("Please retrieve member info", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberName.Focus();
                    return;
                }
                if (txtMonths.Text == "")
                {
                    MessageBox.Show("Please enter months", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonths.Focus();
                    return;
                }
                if (cmbMembershipType.Text == "")
                {
                    MessageBox.Show("Please select membership type", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMembershipType.Focus();
                    return;
                }
                if (txtDiscountPer.Text == "")
                {
                    MessageBox.Show("Please enter discount %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscountPer.Focus();
                    return;
                }
                if (txtTotalPaid.Text == "")
                {
                    MessageBox.Show("Please enter total paid", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTotalPaid.Focus();
                    return;
                }
                double val1 = 0;
                double val2 = 0;
                double.TryParse(txtGrandTotal.Text, out val1);
                double.TryParse(txtTotalPaid.Text, out val2);
                if (val2 > val1)
                {
                    MessageBox.Show("Total paid can not more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPaid.Text = "";
                    txtTotalPaid.Focus();
                    return;

                }
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string ct = "SELECT * FROM CustomerMembership WHERE DateFrom <= '" + dtpDateTo.Value.Date + "' AND DateTo >= '" + dtpDateFrom.Value.Date + "' and CustomerID=" + txtM_ID.Text + "";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.rdr = cc.cmd.ExecuteReader();
                if (cc.rdr.Read())
                {
                    MessageBox.Show("Membership has not expired yet.." + "\n" + "Renewal is not allowed now", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    return;
                }
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "insert into CustomerMembership(CM_ID,CustMembershipID,BillDate,CustomerID,MembershipID,DateFrom,Months,DateTo,ChargesPerMonth,TotalCharges,DiscountPer,DiscountAmount,SubTotal,VATPer,VATAmount,ServiceTaxPer,ServiceTaxAmount,GrandTotal,TotalPaid,Balance) VALUES (" + txtID.Text +",'" + txtMembershipID.Text + "',@d1," + txtM_ID.Text + "," + txtMembershipTypeID.Text + ",@d2," + txtMonths.Text +",@d3,"+ txtChargesPerMonth.Text +","+ txtTotalCharges.Text + ","+ txtDiscountPer.Text  +", "+ txtDiscountAmount.Text +","+ txtSubTotal.Text +"," + txtVATPer.Text +"," + txtVATAmount.Text + ","+ txtServiceTaxPer.Text +"," + txtServiceTaxAmount.Text + "," + txtGrandTotal.Text + "," + txtTotalPaid.Text + "," + txtBalance.Text + ")";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", dtpBillDate.Value);
                cc.cmd.Parameters.AddWithValue("@d2", dtpDateFrom.Text);
                cc.cmd.Parameters.AddWithValue("@d3", dtpDateTo.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "added the new membership having membership id '" + txtMembershipID.Text + "' of member '" + txtMemberName.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btnSave.Enabled = false;
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTotalPaid_Validating(object sender, CancelEventArgs e)
        {
            double val1=0;
            double val2 = 0;
            double.TryParse(txtGrandTotal.Text, out val1);
            double.TryParse(txtTotalPaid.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Total paid can not more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Text = "";
                txtTotalPaid.Focus();
                return;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string ct = "delete from CustomerMembership where CM_ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lblUser.Text;
                    st2 = "deleted the membership record having membership id '" + txtMembershipID.Text + "' of member '" + txtMemberName.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberName.Text == "")
                {
                    MessageBox.Show("Please retrieve member info", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberName.Focus();
                    return;
                }
                if (txtMonths.Text == "")
                {
                    MessageBox.Show("Please enter months", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonths.Focus();
                    return;
                }
                if (cmbMembershipType.Text == "")
                {
                    MessageBox.Show("Please select membership type", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMembershipType.Focus();
                    return;
                }
                if (txtDiscountPer.Text == "")
                {
                    MessageBox.Show("Please enter discount %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscountPer.Focus();
                    return;
                }
                if (txtTotalPaid.Text == "")
                {
                    MessageBox.Show("Please enter total paid", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTotalPaid.Focus();
                    return;
                }
                double val1 = 0;
                double val2 = 0;
                double.TryParse(txtGrandTotal.Text, out val1);
                double.TryParse(txtTotalPaid.Text, out val2);
                if (val2 > val1)
                {
                    MessageBox.Show("Total paid can not more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPaid.Text = "";
                    txtTotalPaid.Focus();
                    return;

                }
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "Update CustomerMembership set CustMembershipID='" + txtMembershipID.Text + "',CustomerID=" + txtM_ID.Text + ",MembershipID=" + txtMembershipTypeID.Text + ",DateFrom=@d2,Months=" + txtMonths.Text + ",DateTo=@d3,ChargesPerMonth=" + txtChargesPerMonth.Text + ",TotalCharges=" + txtTotalCharges.Text + ",DiscountPer=" + txtDiscountPer.Text + ", DiscountAmount=" + txtDiscountAmount.Text + ",SubTotal=" + txtSubTotal.Text + ",VATPer=" + txtVATPer.Text + ",VATAmount=" + txtVATAmount.Text + ",ServiceTaxPer=" + txtServiceTaxPer.Text + ",ServiceTaxAmount=" + txtServiceTaxAmount.Text + ",GrandTotal=" + txtGrandTotal.Text + ",TotalPaid=" + txtTotalPaid.Text + ",balance=" + txtBalance.Text + " where CM_ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d2", dtpDateFrom.Text);
                cc.cmd.Parameters.AddWithValue("@d3", dtpDateTo.Text);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "updated the membership record having membership id '" + txtMembershipID.Text + "' of member '" + txtMemberName.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerMembershipRecord frm = new frmCustomerMembershipRecord();
            frm.Reset();
            frm.lblOperation.Text = "Customer Membership";
            frm.lblUser.Text = lblUser.Text;
            frm.Show();
        }


    }
}

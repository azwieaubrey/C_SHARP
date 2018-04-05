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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
namespace GymManagementSystem
{
    public partial class frmCustomerMembershipRecord : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public frmCustomerMembershipRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(CM_ID) as [ID],RTRIM(CustMembershipID) as [Membership ID],Convert(DateTime,BillDate,131) as [Bill Date],RTRIM(Customer.CustomerID) as [Member ID],RTRIM(Name) as [Member Name],RTRIM(MembershipID) as [Membership Type ID],RTRIM(Type) as [MemberShip Type],Convert(Datetime,DateFrom,103) as [Date From],RTRIM(Months) as [Months],Convert(Datetime,DateTo,103) as [Date To],RTRIM(CustomerMembership.ChargesPerMonth) as [Charges Per Month],RTRIM(TotalCharges) as [Total Charges],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount],RTRIM(SubTotal) as [Sub Total],RTRIM(VATPer) as [VAT %],RTRIM(VATAmount) as [VAT],RTRIM(ServiceTaxPer) as [Service Tax %],RTRIM(ServiceTaxAmount) as [Service Tax],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPaid) as [Total Paid],RTRIM(Balance) as [Balance] from Membership,CustomerMembership,Customer where Customer.C_ID=CustomerMembership.CustomerID and Membership.M_ID=CustomerMembership.MembershipID order by BillDate", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "CustomerMembership");
                dgw.DataSource = cc.ds.Tables["CustomerMembership"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtGuestName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(CM_ID) as [ID],RTRIM(CustMembershipID) as [Membership ID],Convert(DateTime,BillDate,131) as [Bill Date],RTRIM(Customer.CustomerID) as [Member ID],RTRIM(Name) as [Member Name],RTRIM(MembershipID) as [Membership Type ID],RTRIM(Type) as [MemberShip Type],Convert(Datetime,DateFrom,103) as [Date From],RTRIM(Months) as [Months],Convert(Datetime,DateTo,103) as [Date To],RTRIM(CustomerMembership.ChargesPerMonth) as [Charges Per Month],RTRIM(TotalCharges) as [Total Charges],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount],RTRIM(SubTotal) as [Sub Total],RTRIM(VATPer) as [VAT %],RTRIM(VATAmount) as [VAT],RTRIM(ServiceTaxPer) as [Service Tax %],RTRIM(ServiceTaxAmount) as [Service Tax],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPaid) as [Total Paid],RTRIM(Balance) as [Balance] from Membership,CustomerMembership,Customer where Customer.C_ID=CustomerMembership.CustomerID and Membership.M_ID=CustomerMembership.MembershipID and Name like '" + txtMemberName.Text + "%' order by Name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "CustomerMembership");
                dgw.DataSource = cc.ds.Tables["CustomerMembership"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Reset()
        {
            txtMemberName.Text = "";
            GetData();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            try
            {
                if (lblOperation.Text == "Customer Membership")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmCustomerMembership frm = new frmCustomerMembership();
                    frm.Show();
                    frm.txtID.Text = dr.Cells[0].Value.ToString();
                    frm.txtMembershipID.Text = dr.Cells[1].Value.ToString();
                    frm.dtpBillDate.Text = dr.Cells[2].Value.ToString();
                    frm.txtMemberID.Text = dr.Cells[3].Value.ToString();
                    frm.txtMembershipTypeID.Text = dr.Cells[5].Value.ToString();
                    frm.cmbMembershipType.Text = dr.Cells[6].Value.ToString();
                    frm.dtpDateFrom.Text = dr.Cells[7].Value.ToString();
                    frm.txtMonths.Text = dr.Cells[8].Value.ToString();
                    frm.dtpDateTo.Text = dr.Cells[9].Value.ToString();
                    frm.txtChargesPerMonth.Text = dr.Cells[10].Value.ToString();
                    frm.txtTotalCharges.Text = dr.Cells[11].Value.ToString();
                    frm.txtDiscountPer.Text = dr.Cells[12].Value.ToString();
                    frm.txtDiscountAmount.Text = dr.Cells[13].Value.ToString();
                    frm.txtSubTotal.Text = dr.Cells[14].Value.ToString();
                    frm.txtVATPer.Text = dr.Cells[15].Value.ToString();
                    frm.txtVATAmount.Text = dr.Cells[16].Value.ToString();
                    frm.txtServiceTaxPer.Text = dr.Cells[17].Value.ToString();
                    frm.txtServiceTaxAmount.Text = dr.Cells[18].Value.ToString();
                    frm.txtGrandTotal.Text = dr.Cells[19].Value.ToString();
                    frm.txtTotalPaid.Text = dr.Cells[20].Value.ToString();
                    frm.txtBalance.Text = dr.Cells[21].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.btnDelete.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.dtpBillDate.Enabled = false;
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
                    cc.con = new SqlConnection(cs.DBConn);
                    cc.con.Open();
                    cc.cmd = cc.con.CreateCommand();
                    cc.cmd.CommandText = "SELECT C_ID,Name,Address,City,ContactNo,Email,Photo from Customer where CustomerID='" + dr.Cells[3].Value + "'";
                    cc.rdr = cc.cmd.ExecuteReader();

                    if (cc.rdr.Read())
                    {
                        frm.txtM_ID.Text = cc.rdr.GetValue(0).ToString().Trim();
                        frm.txtMemberName.Text = cc.rdr.GetValue(1).ToString().Trim();
                        frm.txtAddress.Text = cc.rdr.GetValue(2).ToString().Trim();
                        frm.txtCity.Text = cc.rdr.GetValue(3).ToString().Trim();
                        frm.txtContactNo.Text = cc.rdr.GetValue(4).ToString().Trim();
                        frm.txtEmailID.Text = cc.rdr.GetValue(5).ToString().Trim();
                        byte[] data = (byte[])cc.rdr.GetValue(6);
                        MemoryStream ms = new MemoryStream(data);
                        frm.Picture.Image = Image.FromStream(ms);
                    }
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    if (cc.con.State == ConnectionState.Open)
                    {
                        cc.con.Close();
                    }
                }
                if (lblOperation.Text == "Fitness Measure")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                  
                    cc.con = new SqlConnection(cs.DBConn);
                    cc.con.Open();
                    cc.cmd = cc.con.CreateCommand();
                    cc.cmd.CommandText = "SELECT C_ID,Name,Address,City,ContactNo,Email,Photo from Customer where CustomerID='" + dr.Cells[3].Value + "'";
                    cc.rdr = cc.cmd.ExecuteReader();

                    if (cc.rdr.Read())
                    {
                        
                    }
                    if ((cc.rdr != null))
                    {
                        cc.rdr.Close();
                    }
                    if (cc.con.State == ConnectionState.Open)
                    {
                        cc.con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomerRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }


    }
}

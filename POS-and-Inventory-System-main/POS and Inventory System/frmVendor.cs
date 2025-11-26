using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // CHANGED: From System.Data.SqlClient

namespace POS_and_Inventory_System
{
    public partial class frmVendor : Form
    {
        frmVendorList frm;
        // CHANGED: Sql classes to MySql classes
        MySqlConnection conn;
        MySqlCommand cmd;
        DBConnection dbconn = new DBConnection();

        /*
         Mega's Note - Different AI

        Here is the converted frmVendor.cs.

        The conversion here is straightforward because the MySql.Data library supports the @parameter syntax used in your SQL strings, just like MS SQL Server does. You don't need to change the @vendor, @address, etc., to question marks (?).
         */

        public frmVendor(frmVendorList _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
        }

        private void ImgClose_Click(object sender, EventArgs e)
            => Dispose();

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = "INSERT INTO tblVendor(vendor, address, contactperson, mobileno, email, fax) VALUES" +
                        " (@vendor, @address, @contactperson, @mobileno, @email, @fax)";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    cmd.Parameters.AddWithValue("@mobileno", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@fax", txtFax.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully saved", "save record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    frm.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtAddress.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtContact.Clear();
            txtMobile.Clear();
            txtVendor.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = "UPDATE tblVendor SET vendor=@vendor, address=@address, contactperson=@contactperson, " +
                        "mobileno=@mobileno, email=@email, fax=@fax WHERE id=@id";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", lblId.Text);
                    cmd.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    cmd.Parameters.AddWithValue("@mobileno", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@fax", txtFax.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully updated", "update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    frm.LoadRecords();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
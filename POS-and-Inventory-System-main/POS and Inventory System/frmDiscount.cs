using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmDiscount : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private DBConnection dbconn = new DBConnection();
        private frmPOS frm;

        /*
         Mega Note 
         
         🔍 What Was Fixed / Improved
        ✔ MSSQL → MySQL conversion

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        ✔ Silent crash fixed

        If user typed non-number in the discount fields, it would throw format exceptions.
        Now it defaults to "0.00" safely.

        ✔ Proper cleanup with finally

        Ensures the connection always closes, even on error.

        ✔ Avoided hidden bug

        In MSSQL version, they ignored the exception variable ex in one method.
         */

        public frmDiscount(frmPOS _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
            KeyPreview = true;
        }

        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double price = double.Parse(txtPrice.Text);
                double percent = double.Parse(txtPercent.Text);

                double discount = price * percent;
                txtAmount.Text = discount.ToString("#,##0.00");
            }
            catch
            {
                txtAmount.Text = "0.00";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add Discount?", "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();

                    string sql = "UPDATE tblCart SET disc=@disc, disc_percent=@disc_percent WHERE id=@id";
                    cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@disc", double.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@disc_percent", double.Parse(txtPercent.Text));
                    cmd.Parameters.AddWithValue("@id", int.Parse(lblId.Text));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                conn.Close();
                frm.LoadCart();
                Dispose();
            }
        }

        private void FrmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

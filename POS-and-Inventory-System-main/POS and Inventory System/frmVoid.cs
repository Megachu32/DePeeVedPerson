using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // CHANGED: From System.Data.SqlClient

namespace POS_and_Inventory_System
{
    public partial class frmVoid : Form
    {
        // CHANGED: Sql classes to MySql classes
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        frmCancelDetails frm;

        /*
         
         differnt AI
        mega's Note - 


         */

        public frmVoid(frmCancelDetails _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
        }

        private void BtnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != string.Empty)
                {
                    string user = "";
                    bool found = false;

                    conn.Open();
                    string sql = "SELECT * FROM tblUser WHERE username=@username AND password=@password";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", txtUser.Text);
                    cmd.Parameters.AddWithValue("@password", txtPass.Text);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        found = true;
                        user = dr["username"].ToString();
                    }
                    dr.Close();
                    conn.Close(); // CRITICAL: Close here before calling methods that open it again

                    if (found)
                    {
                        SaveCancelOrder(user);

                        if (frm.cboAction.Text == "Yes")
                        {
                            UpdateData("UPDATE tblProduct SET qty = qty + " + int.Parse(frm.txtCancelQty.Text) + " WHERE pcode='" + frm.txtPCode.Text + "'");
                        }

                        // MySQL syntax check: 'LIKE' works, but '=' is slightly faster for IDs. Keeping 'LIKE' to match your logic.
                        UpdateData("UPDATE tblCart SET qty = qty - " + int.Parse(frm.txtCancelQty.Text) + " WHERE id LIKE '" + frm.txtId.Text + "'");

                        MessageBox.Show("Order transaction sucessfully cancelled!", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                        frm.RefreshList();
                        frm.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveCancelOrder(string user)
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO tblCancel (transno, pcode, price, qty, sdate, voidBy, cancelledBy, reason, action) " +
                    "VALUES (@transno, @pcode, @price, @qty, @sdate, @voidBy, @cancelledBy, @reason, @action)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@transno", frm.txtTransNo.Text);
                cmd.Parameters.AddWithValue("@pcode", frm.txtPCode.Text);
                cmd.Parameters.AddWithValue("@price", double.Parse(frm.txtPrice.Text));
                cmd.Parameters.AddWithValue("@qty", int.Parse(frm.txtCancelQty.Text));
                cmd.Parameters.AddWithValue("@sdate", DateTime.Now); // MySQL Connector handles DateTime object to format automatically
                cmd.Parameters.AddWithValue("@voidBy", user);
                cmd.Parameters.AddWithValue("@cancelledBy", frm.txtCancel.Text);
                cmd.Parameters.AddWithValue("@reason", frm.txtReason.Text);
                cmd.Parameters.AddWithValue("@action", frm.cboAction.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateData(string sql)
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}
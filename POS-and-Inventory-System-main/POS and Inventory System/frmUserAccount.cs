using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // CHANGED: From System.Data.SqlClient

namespace POS_and_Inventory_System
{
    public partial class frmUserAccount : Form
    {
        // CHANGED: Sql classes to MySql classes
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmDashboard frm;

        /*
         mega's note - 003: different AI

        ⚠️ Crucial Change: Boolean vs. TinyInt
        Microsoft SQL Server uses a BIT data type which behaves very much like a C# bool. MySQL often uses TINYINT(1) (where 0 is false and 1 is true).

        Reading Data: I changed bool.Parse(dr["isactive"].ToString()) to Convert.ToBoolean(dr["isactive"]). bool.Parse fails on MySQL because it tries to parse the string "1" or "0" as a boolean, which throws an error.

        Saving Data: I removed .ToString() from cbActive.Checked. MySQL handles the raw C# bool better than the string "True".
         */

        public frmUserAccount(frmDashboard _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
        }

        private void FrmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (Height - metroTabControl1.Height) / 2;
        }

        private void Clear()
        {
            txtName.Clear();
            txtPass.Clear();
            txtConfirmPass.Clear();
            txtUser.Clear();
            cboRole.Text = "";
            txtUser.Focus();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Password did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conn.Open();
                string sql = "INSERT INTO tblUser (username, password, role, name) VALUES (@username, @password, @role, @name)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@role", cboRole.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("New Account has saved");
                Clear();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPass2.Text != frm._pass)
                {
                    MessageBox.Show("Old Password did not matched!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtNewPass2.Text != txtConfirmPass2.Text)
                {
                    MessageBox.Show("Confirm new password did not matched", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                conn.Open();
                cmd = new MySqlCommand("UPDATE tblUser SET password=@password WHERE username=@username", conn);
                cmd.Parameters.AddWithValue("@password", txtNewPass2.Text);
                cmd.Parameters.AddWithValue("@username", txtUser2.Text);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Password has been successfully changed!", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmPass2.Clear();
                txtNewPass2.Clear();
                txtOldPass2.Clear();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtUser3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT * FROM tblUser WHERE username=@username", conn);
                cmd.Parameters.AddWithValue("@username", txtUser3.Text);
                dr = cmd.ExecuteReader();

                // CHANGED: Logic to handle reading boolean from MySQL safely
                if (dr.Read())
                {
                    // Convert.ToBoolean handles 0/1 (TinyInt) correctly. bool.Parse does not.
                    cbActive.Checked = Convert.ToBoolean(dr["isactive"]);
                }
                else
                {
                    cbActive.Checked = false;
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSave3_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = true;
                conn.Open();
                cmd = new MySqlCommand("SELECT * FROM tblUser WHERE username=@username", conn);
                cmd.Parameters.AddWithValue("@username", txtUser3.Text);
                dr = cmd.ExecuteReader();
                // Check if row exists
                if (dr.Read())
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
                dr.Close();
                conn.Close();

                if (found)
                {
                    conn.Open();
                    cmd = new MySqlCommand("UPDATE tblUser SET isactive=@isactive WHERE username=@username", conn);

                    // CHANGED: Removed .ToString(). Pass the boolean directly. 
                    // MySQL Connector converts true/false to 1/0 automatically.
                    cmd.Parameters.AddWithValue("@isactive", cbActive.Checked);

                    cmd.Parameters.AddWithValue("@username", txtUser3.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Account status has been successfully updated.", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUser3.Clear();
                    cbActive.Checked = true;
                }
                else
                {
                    MessageBox.Show("Account does not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}
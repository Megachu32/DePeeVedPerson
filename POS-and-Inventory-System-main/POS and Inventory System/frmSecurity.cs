using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmSecurity : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();

        public string _pass, _username = "";
        public bool _isActive = false;

        /*
         mega's note

                ✅ What Was Changed for MySQL
        ✔ Replaced all MSSQL classes:
        MSSQL	                        ➜	MySQL
        SqlConnection	            	    MySqlConnection
        SqlCommand		                    MySqlCommand
        SqlDataReader		                MySqlDataReader
        AddWithValue stays the same   	✔	Works
        Connection string	            ✔	Must be MySQL format
        ⚠ Important MySQL Differences You Need to Know
        1. bool columns are usually stored as TINYINT(1)

        So .ToString() == "1" is correct.

        2. Date/Time functions differ

        Not used here, so you're fine.

        3. MySQL is case-sensitive on Linux

        Make sure your table names match exactly:

        tblUser

        column isactive, username, etc.
         */

        public frmSecurity()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            KeyPreview = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string _role = "", _name = "";
            try
            {
                bool found = false;

                conn.Open();
                string sql = "SELECT * FROM tblUser WHERE username=@username AND password=@password";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);

                dr = cmd.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                    _pass = dr["password"].ToString();
                    _isActive = dr["isactive"].ToString() == "1";
                }
                else found = false;

                dr.Close();
                conn.Close();

                if (found)
                {
                    if (!_isActive)
                    {
                        MessageBox.Show("Account is deactivated. Unable to login.",
                            "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show("Access Granted! Welcome " + _name,
                        "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPass.Clear();
                    txtUser.Clear();

                    Hide();

                    if (_role == "Cashier")
                    {
                        frmPOS frm = new frmPOS();
                        frm.lblUser.Text = _username;
                        frm.lblName.Text = _name + " | " + _role;
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmDashboard frm = new frmDashboard();
                        frm.lblName.Text = _name;
                        frm.lblRole.Text = _role;
                        frm._pass = _pass;
                        frm._user = _username;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password",
                        "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmSecurity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                BtnLogin_Click(sender, e);
            else if (e.KeyCode == Keys.Escape)
                BtnCancel_Click(sender, e);
        }
    }
}

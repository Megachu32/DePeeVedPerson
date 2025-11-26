using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // CHANGED: From System.Data.SqlClient

namespace POS_and_Inventory_System
{
    public partial class frmStore : Form
    {
        // CHANGED: Sql classes to MySql classes
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        DBConnection db = new DBConnection();

        /*
         Mega's Note

        still different AI.
         */

        public frmStore()
        {
            InitializeComponent();
            conn = new MySqlConnection(db.MyConnection());
        }

        public void LoadRecords()
        {
            conn.Open();
            string sql = "SELECT * FROM tblStore";
            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();

            // CHANGED: Simplified logic. dr.Read() returns true if a row is found.
            if (dr.Read())
            {
                txtAddress.Text = dr["address"].ToString();
                txtStore.Text = dr["store"].ToString();
            }
            else
            {
                txtStore.Clear();
                txtAddress.Clear();
            }
            dr.Close();
            conn.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE STORE DETAILS?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int count;
                    conn.Open();
                    string sql = "SELECT count(*) FROM tblStore";
                    cmd = new MySqlCommand(sql, conn);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    conn.Close();

                    if (count > 0)
                    {
                        conn.Open();
                        string sql1 = "UPDATE tblStore SET store=@store, address=@address";
                        cmd = new MySqlCommand(sql1, conn);
                        cmd.Parameters.AddWithValue("@store", txtStore.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close(); // ADDED: Close connection after update
                    }
                    else
                    {
                        conn.Open();
                        string sql1 = "INSERT into tblStore (store, address) VALUES (@store, @address)";
                        cmd = new MySqlCommand(sql1, conn);
                        cmd.Parameters.AddWithValue("@store", txtStore.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close(); // ADDED: Close connection after insert
                    }

                    MessageBox.Show("STORE DETAILS HAS BEEN SUCCESSFULLY SAVED!", "SAVE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Safety check to close connection if error occurs while open
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}
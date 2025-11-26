using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // CHANGED: From System.Data.SqlClient

namespace POS_and_Inventory_System
{
    public partial class frmStockIn : Form
    {
        // CHANGED: Sql classes to MySql classes
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;

        /*
         Mega's Note

        changed AI lol

        some logic might broke
         */

        public frmStockIn()
        {
            InitializeComponent();
            // Ensure dbconn.MyConnection() returns a MySQL connection string now
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadVendor();
        }

        public void LoadStockIn()
        {
            int i = 0;
            dgvStocks.Rows.Clear();
            conn.Open();
            // MySQL Syntax check: LIKE works the same, usually case-insensitive by default in MySQL
            string sql = "SELECT * FROM vwStockIn WHERE refno LIKE '" + txtRefNo.Text + "' AND status LIKE 'Pending'";
            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvStocks.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(),
                    dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr["vendor"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        public void Clear()
        {
            txtStockInBy.Clear();
            txtRefNo.Clear();
            dtStockInDate.Value = DateTime.Now;
        }

        private void LoadStockInHistory()
        {
            try
            {
                int i = 0;
                dgvStocks2.Rows.Clear();
                conn.Open();

                // CHANGED: 
                // 1. MSSQL 'cast(sdate as date)' becomes MySQL 'DATE(sdate)'
                // 2. MySQL requires 'yyyy-MM-dd' format for date strings. ToShortDateString() is risky if your PC is set to specific regions.
                string sDate1 = dtFrom.Value.ToString("yyyy-MM-dd");
                string sDate2 = dtTo.Value.ToString("yyyy-MM-dd");

                string sql = "SELECT * FROM vwStockIn WHERE DATE(sdate) BETWEEN '" + sDate1 + "' AND '" + sDate2 + "' AND status LIKE 'Done'";

                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvStocks2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(),
                        DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["vendor"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Added null check safety
                if (dr != null) dr.Close();
                conn.Close();
                btnSave.Show();
            }
        }

        private void BtnLoadRecord_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void TxtRefNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains('.'.ToString()) == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;
            e.Handled = true;
        }

        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            conn.Open();
            string sql = "SELECT * FROM tblVendor";
            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void CboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CboVendor_TextChanged(object sender, EventArgs e)
        {
            // Note: Opening connection repeatedly without checking logic can cause issues if connection is already open, 
            // but keeping original structure for you.
            try
            {
                conn.Open();
                string sql = "SELECT * FROM tblVendor WHERE vendor LIKE '" + cboVendor.Text + "'";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read()) // Changed from Read(); ... HasRows to if(dr.Read())
                {
                    lblVendorId.Text = dr["id"].ToString();
                    txtContactPerson.Text = dr["contactperson"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                // Optionally log error
            }
        }

        private void LinkGenerate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            txtRefNo.Clear();
            txtRefNo.Text += rnd.Next();
        }

        private void LinkLblBrowseProduct_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStockIn frm = new frmSearchProductStockIn(this);
            frm.LoadProduct();
            frm.ShowDialog();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStocks.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save this records?", "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dgvStocks.Rows.Count; i++)
                        {
                            // UPDATE PRODUCT TABLE QUANTITY
                            conn.Open();
                            string sql = "UPDATE tblProduct SET qty=qty + " + int.Parse(dgvStocks.Rows[i].Cells[5].Value.ToString()) +
                                " WHERE pcode LIKE '" + dgvStocks.Rows[i].Cells[3].Value.ToString() + "'";
                            cmd = new MySqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            // UPDATE STOCK TABLE QUANTITY
                            conn.Open();
                            string sql1 = "UPDATE tblStockIn SET qty=qty + " + int.Parse(dgvStocks.Rows[i].Cells[5].Value.ToString()) +
                                ", status='Done' WHERE id LIKE '" + dgvStocks.Rows[i].Cells[1].Value.ToString() + "'";
                            cmd = new MySqlCommand(sql1, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        Clear();
                        LoadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvStocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvStocks.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new MySqlCommand("DELETE FROM tblStockIn WHERE id = '" + dgvStocks.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Item has been successfully removed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIn();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Assuming Util is a helper class you have
            Util.CloseForm(this);
        }
    }
}
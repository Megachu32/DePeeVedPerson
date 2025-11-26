using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmSearchProductStockIn : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmStockIn fList;

        /*
         mega's note

        ✅ What was changed
        ✔ Switched namespaces

        System.Data.SqlClient → MySql.Data.MySqlClient

        ✔ Replaced classes

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        SqlDataReader → MySqlDataReader

        ✔ Parameterized all queries

        So you don't get SQL injection issues.

        ✔ MySQL-friendly date handling

        MySQL can accept a .NET DateTime directly because parameterized queries convert it safely.
         
         */

        public frmSearchProductStockIn(frmStockIn _fList)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = _fList;
        }

        public void LoadProduct()
        {
            try
            {
                int i = 0;
                dgvProductList.Rows.Clear();
                conn.Open();

                string sql = "SELECT pcode, pdesc, qty FROM tblProduct WHERE pdesc LIKE @search ORDER BY pdesc";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dgvProductList.Rows.Add(i,
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        dr["qty"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                conn.Close();
            }
        }

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProductList.Columns[e.ColumnIndex].Name;

            if (colName == "Select")
            {
                if (string.IsNullOrEmpty(fList.txtRefNo.Text))
                {
                    MessageBox.Show("Please enter Reference No.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fList.txtRefNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(fList.txtStockInBy.Text))
                {
                    MessageBox.Show("Please enter Stock-In By.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fList.txtStockInBy.Focus();
                    return;
                }

                if (MessageBox.Show("Add this item?", "Add Item",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();

                    string sql = @"INSERT INTO tblStockIn
                                   (refno, pcode, sdate, qty, stockInBy, status, vendorId)
                                   VALUES (@refno, @pcode, @sdate, @qty, @stockInBy, @status, @vendorId)";

                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@refno", fList.txtRefNo.Text);
                    cmd.Parameters.AddWithValue("@pcode", dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@sdate", fList.dtStockInDate.Value);
                    cmd.Parameters.AddWithValue("@qty", 0);
                    cmd.Parameters.AddWithValue("@stockInBy", fList.txtStockInBy.Text);
                    cmd.Parameters.AddWithValue("@status", "Pending");
                    cmd.Parameters.AddWithValue("@vendorId", fList.lblVendorId.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Successfully added!", "Add Item",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    fList.LoadStockIn();
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
            => LoadProduct();

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

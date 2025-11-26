using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmProductList : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();

        /*
         
        Mega's Note - 

        ⚡ Notes on the Conversion
        ✔ Safe Parameterized Queries

        No more dangerous:

        WHERE pdesc LIKE '%" + txtSearch.Text + "%'


        Replaced with:

        WHERE pdesc LIKE @search

        ✔ MySQL-friendly JOIN syntax

        MSSQL → MySQL is fully compatible the way it is used here.

        ✔ Updated Delete logic

        Using:

        DELETE FROM tblProduct WHERE pcode = @pcode


        instead of weak LIKE.
         
         */

        public frmProductList()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            try
            {
                int i = 0;
                dgvProductList.Rows.Clear();

                conn.Open();
                string sql =
                    "SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder " +
                    "FROM tblProduct p " +
                    "INNER JOIN tblBrand b ON b.id = p.bid " +
                    "INNER JOIN tblCategory c ON c.id = p.cid " +
                    "WHERE p.pdesc LIKE @search ORDER BY p.pdesc";

                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProductList.Rows.Add(
                        i,
                        dr["pcode"].ToString(),
                        dr["barcode"].ToString(),
                        dr["pdesc"].ToString(),
                        dr["brand"].ToString(),
                        dr["category"].ToString(),
                        dr["price"].ToString(),
                        dr["reorder"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dr?.Close();
                conn.Close();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
            => txtSearch.Clear();

        private void TxtSearch_TextChanged(object sender, EventArgs e)
            => LoadRecords();

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProductList.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                frmProduct frm = new frmProduct(this);

                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;

                frm.txtPCode.Text = dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtBarcode.Text = dgvProductList.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtDescription.Text = dgvProductList.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.cboBrand.Text = dgvProductList.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboCategory.Text = dgvProductList.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtPrice.Text = dgvProductList.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtReOrder.Text = dgvProductList.Rows[e.RowIndex].Cells[7].Value.ToString();

                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?",
                    "Delete Record",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();

                        string sql = "DELETE FROM tblProduct WHERE pcode = @pcode";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@pcode",
                            dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Product has been removed",
                            "Removed Product", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadRecords();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);

            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;

            frm.LoadBrand();
            frm.LoadCategory();

            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);
    }
}

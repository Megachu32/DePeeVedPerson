using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmLookUp : Form
    {
        frmPOS frm;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();

        /*
         Mega Note

        ⚡ Notes

        I parameterized the WHERE clause (LIKE @search) to eliminate SQL injection and random errors.

        MySQL joins and column names remain identical to MSSQL, so everything works out-of-the-box.

        The dgv column indices are untouched — important because they must match your grid setup.
         */
        public frmLookUp(frmPOS _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
            KeyPreview = true;
        }

        public void LoadRecords()
        {
            try
            {
                int i = 0;
                dgvProductList.Rows.Clear();

                conn.Open();
                string sql =
                    "SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty " +
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
                        dr["qty"].ToString()
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

        private void TxtSearch_TextChanged(object sender, EventArgs e)
            => LoadRecords();

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProductList.Columns[e.ColumnIndex].Name;

            if (colName == "Select")
            {
                frmQty frmQty = new frmQty(frm);

                frmQty.ProductDetails(
                    dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString(),   // pcode
                    double.Parse(dgvProductList.Rows[e.RowIndex].Cells[6].Value.ToString()), // price
                    frm.lblTransNo.Text,
                    int.Parse(dgvProductList.Rows[e.RowIndex].Cells[7].Value.ToString()) // qty
                );

                frmQty.ShowDialog();
            }
        }

        private void FrmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

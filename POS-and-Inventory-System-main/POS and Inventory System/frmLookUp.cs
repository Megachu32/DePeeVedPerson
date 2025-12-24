using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmLookUp : Form
    {
        frmPOS frm;
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public frmLookUp(frmPOS _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
            KeyPreview = true;
        }

        public void LoadRecords()
        {
            if(dgvProductList.Rows.Count > 0 ) dgvProductList.Rows.Clear();

            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Tables["dtProductsView"];

            conn.Open();
            string sql = @"
                SELECT
                    ROW_NUMBER() OVER (ORDER BY p.product_id) AS id,
                    p.sku AS sku,
                    p.name AS NAME,
                    p.type AS TYPE,
                    p.model AS model,
                    p.status AS STATUS,
                    COALESCE(i.stock, 0) AS stock,
                    p.price AS price
                FROM products p
                LEFT JOIN inventory i
                    ON i.product_id = p.product_id;
            ";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvProductList.DataSource = dt;
            conn.Close();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e) 
            => LoadRecords();

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProductList.Columns[e.ColumnIndex].Name;
            if (colName == "select")
            {
                //when button is clicked send data to qty form so you can input qty
                frmQty frmQty = new frmQty(frm);
                frmQty.ProductDetails(dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    double.Parse(dgvProductList.Rows[e.RowIndex].Cells[7].Value.ToString()), frm.lblTransNo.Text, 
                    int.Parse(dgvProductList.Rows[e.RowIndex].Cells[6].Value.ToString()), dgvProductList.Rows[e.RowIndex].Cells[5].ToString());
                frmQty.ShowDialog();
            }
        }

        private void FrmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e) 
            => Dispose();
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmStaff : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public frmStaff()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Tables["dtStaff"];

            int i = 0;
            conn.Open();
            string sql = @"
                SELECT *
                FROM staff

                ORDER BY staff_id ASC
            ";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);

            da.Fill(dt);
            dgvStaffList.DataSource = dt;
            conn.Close();
        }

        //private void BtnClear_Click(object sender, EventArgs e) 
        //    => txtSearch.Clear();

        private void TxtSearch_TextChanged(object sender, EventArgs e) 
            => LoadRecords();

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvStaffList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //frmStaff frm = new frmStaff(this);
                //frm.btnSave.Enabled = false;
                //frm.btnUpdate.Enabled = true;
                //frm.txtPCode.Text = dgvProductList.Rows[e.RowIndex].Cells[0].Value.ToString();

                //frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record", "Delete Record",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = sql = @"
                        START TRANSACTION;

                        DELETE si FROM sale_items si 
                        JOIN products p ON si.product_id = p.product_id
                        WHERE p.sku = @sku;

                        DELETE pr FROM preorders pr 
                        JOIN products p ON pr.product_id = p.product_id
                        WHERE p.sku = @sku;

                        DELETE inv FROM inventory inv
                        JOIN products p ON inv.product_id = p.product_id
                        WHERE p.sku = @sku;

                        DELETE FROM products WHERE sku = @sku;

                        COMMIT;
                    ";

                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sku", dgvStaffList.Rows[e.RowIndex].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadRecords();
                    MessageBox.Show("Product has been removed", "Removed Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //private void BtnAdd_Click(object sender, EventArgs e)
        //{
        //    frmProduct frm = new frmProduct(this);
        //    frm.btnSave.Enabled = true;
        //    frm.btnUpdate.Enabled = false;
        //    frm.LoadCategory();
        //    frm.ShowDialog();
        //}

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
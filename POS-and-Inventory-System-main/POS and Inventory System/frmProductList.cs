using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmProductList : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public frmProductList()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Tables["dtProducts"];

            int i = 0;
            conn.Open();
            string sql = @"
                SELECT *
                FROM products as p
                LEFT JOIN inventory as i ON p.product_id = i.product_id
                WHERE (p.name LIKE @search OR p.sku LIKE @search) 
                  AND p.status != 'inactive'
                ORDER BY type ASC
            ";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

            da.Fill(dt);
            dgvProductList.DataSource = dt;
            conn.Close();
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
                frm.guna2ButtonSave.Enabled = false;
                frm.guna2Button1.Enabled = true;
                frm.txtPCode.Text = dgvProductList.Rows[e.RowIndex].Cells[0].Value.ToString();
                frm.fillData();
                frm.ShowDialog();
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
                    cmd.Parameters.AddWithValue("@sku", dgvProductList.Rows[e.RowIndex].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadRecords();
                    MessageBox.Show("Product has been removed", "Removed Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            //frm.btnSave.Enabled = true;
            //frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);

        private void printRaport_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Open Connection
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. The Query (Selects ALL staff first)
                string sql = @"
                    select
                    p.product_id as id,
                    p.sku as sku, 
                    p.name as name,
                    p.type as type,
                    p.model as model,
                    p.status as status,
                    p.price as price,
                    i.stock as stock
                    from products as p
                    join inventory as i on i.product_id = p.product_id
                    order by type";

                // 3. Prepare Adapter & Dataset
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // 4. Fill the table "dtStaff"
                da.Fill(ds, "dtProducts");

                if (ds.Tables["dtProducts"].Rows.Count == 0)
                {
                    MessageBox.Show("No staff records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 5. Load the Report
                CrystalReport2 report = new CrystalReport2();
                report.SetDataSource(ds);

                // --- NEW STEP: Pass the Parameter ---
                // We force it to show "active" staff. 
                // If you want to see inactive ones, change this string to "inactive".
                //report.SetParameterValue("Type", "active");

                // 6. Show the Report
                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Staff Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
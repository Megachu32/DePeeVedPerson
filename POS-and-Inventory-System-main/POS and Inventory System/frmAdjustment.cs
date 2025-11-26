using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmAdjustment : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        private DBConnection dbconn = new DBConnection();
        private frmDashboard frm;

        private int currentStock = 0;   // Inventory qty

        /*
         Mega's Note -- 001 - logging feature

        CREATE TABLE inventory_adjustments (
        id INT AUTO_INCREMENT PRIMARY KEY,
        reference_no VARCHAR(50),
        product_id INT,
        qty INT,
        action VARCHAR(50),
        remarks TEXT,
        date DATE,
        user VARCHAR(50)
        );


        the table above were missing from the current MySQL database.
        for the current code to work, you'll need to either run the code above,
        or remove the code related to logging features from this file.

         */
        public frmAdjustment(frmDashboard _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;

            LoadRecords();

            // Generate reference number
            txtRefNo.Text = new Random().Next().ToString();
        }

        // ================================
        // LOAD PRODUCT LIST
        // ================================
        public void LoadRecords()
        {
            int i = 0;
            dgvProduct.Rows.Clear();

            string sql =
                @"SELECT p.id, p.barcode, p.product_name, p.brand, p.category, p.sale_price,
                         COALESCE(inv.stock, 0) AS stock
                  FROM products p
                  LEFT JOIN inventory inv ON inv.product_id = p.id
                  WHERE p.product_name LIKE @search
                  ORDER BY p.product_name";

            conn.Open();
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(
                    i,
                    dr["id"].ToString(),
                    dr["barcode"].ToString(),
                    dr["product_name"].ToString(),
                    dr["brand"].ToString(),
                    dr["category"].ToString(),
                    dr["sale_price"].ToString(),
                    dr["stock"].ToString()
                );
            }

            dr.Close();
            conn.Close();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                LoadRecords();
        }

        // ================================
        // SELECT ROW
        // ================================
        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;

            if (colName == "Select")
            {
                txtPCode.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(); // product_id
                txtDesc.Text =
                    dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString() + " " +
                    dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString() + " " +
                    dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();

                currentStock = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
        }

        // ================================
        // SAVE ADJUSTMENT
        // ================================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int qty = int.Parse(txtQty.Text);

                if (cboCommand.Text == "")
                {
                    MessageBox.Show("Please select an action.", "WARNING",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (qty <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0.", "WARNING",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // REMOVE
                if (cboCommand.Text == "REMOVE FROM INVENTORY")
                {
                    if (qty > currentStock)
                    {
                        MessageBox.Show(
                            "Cannot remove more than stock on hand.",
                            "WARNING",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    UpdateInventory(-qty);
                }

                // ADD
                else if (cboCommand.Text == "ADD TO INVENTORY")
                {
                    UpdateInventory(qty);
                }

                // Log adjustment
                InsertAdjustmentRecord(qty);

                MessageBox.Show("Stock has been successfully adjusted.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadRecords();
                Clear();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================
        // UPDATE INVENTORY TABLE
        // ================================
        private void UpdateInventory(int delta)
        {
            string sql =
                @"UPDATE inventory
                  SET stock = stock + @delta
                  WHERE product_id = @id";

            conn.Open();
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@delta", delta);
            cmd.Parameters.AddWithValue("@id", txtPCode.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // ================================
        // INSERT ADJUSTMENT LOG
        // ================================
        private void InsertAdjustmentRecord(int qty)
        {
            string sql =
                @"INSERT INTO inventory_adjustments
                    (reference_no, product_id, qty, action, remarks, date, user)
                  VALUES
                    (@ref, @product, @qty, @action, @remarks, @date, @user)";

            conn.Open();
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ref", txtRefNo.Text);
            cmd.Parameters.AddWithValue("@product", txtPCode.Text);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@action", cboCommand.Text);
            cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@user", txtUser.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();

        public void Clear()
        {
            txtDesc.Clear();
            txtPCode.Clear();
            txtQty.Clear();
            txtRemarks.Clear();
            cboCommand.Text = "";

            txtRefNo.Text = new Random().Next().ToString();
        }
    }
}

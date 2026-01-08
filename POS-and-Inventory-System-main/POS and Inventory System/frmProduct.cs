using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmProduct : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmProductList fList;

        public frmProduct(frmProductList frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frm;
            LoadCategory();
        }

        public void LoadCategory() // calls in other place outside this form use to fill combobox
        {
            cmbStatus.Items.Clear();
            cmbType.Items.Clear();

            cmbType.Items.Add("iPhone");
            cmbType.Items.Add("iPad");
            cmbType.Items.Add("MacBook");
            cmbType.Items.Add("Accessories");

            cmbStatus.Items.Add("active");
            cmbStatus.Items.Add("incoming");
            cmbStatus.Items.Add("inactive");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Declare transaction outside try block so we can access it in catch
            MySqlTransaction transaction = null;

            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Save Product",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    // Start a local transaction
                    transaction = conn.BeginTransaction();

                    // 1. INSERT PRODUCT AND GET THE NEW ID
                    // We add "; SELECT LAST_INSERT_ID();" to the end of the query
                    string sqlProduct = @"
                INSERT INTO products (sku, name, type, model, generation, release_date, price, color, storage, specifications, status, description) 
                VALUES (@sku, @name, @type, @model, @generation, @release_date, @price, @color, @storage, @specifications, @status, @description);
                SELECT LAST_INSERT_ID();";

                    cmd = new MySqlCommand(sqlProduct, conn);
                    cmd.Transaction = transaction; // Important: Link command to transaction

                    cmd.Parameters.AddWithValue("@sku", txtPCode.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@type", cmbType.Text);
                    cmd.Parameters.AddWithValue("@model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@generation", Convert.ToInt16(txtGeneration.Text));
                    cmd.Parameters.AddWithValue("@release_date", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@color", txtColor.Text);
                    cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    cmd.Parameters.AddWithValue("@specifications", txtSpecific.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);

                    // ExecuteScalar returns the first column of the first row (our ID)
                    int newProductId = Convert.ToInt32(cmd.ExecuteScalar());

                    // 2. INSERT INTO INVENTORY
                    // I am assuming you have a textbox for stock called 'txtStock'. 
                    // If not, replace Convert.ToInt32(txtStock.Text) with your default value (e.g., 0).
                    string sqlInventory = "INSERT INTO inventory (product_id, stock) VALUES (@product_id, @stock)";

                    using (MySqlCommand cmdInv = new MySqlCommand(sqlInventory, conn))
                    {
                        cmdInv.Transaction = transaction; // Link to the same transaction
                        cmdInv.Parameters.AddWithValue("@product_id", newProductId);

                        // CHANGE THIS: Ensure txtStock exists, or set a default value like 0
                        int stockValue = 0;
                        if (!string.IsNullOrEmpty(numericUpDown1.Value.ToString()))
                        {
                            stockValue = Convert.ToInt32(numericUpDown1.Value.ToString());
                        }
                        cmdInv.Parameters.AddWithValue("@stock", stockValue);

                        cmdInv.ExecuteNonQuery();
                    }

                    // If we reached here, both inserts worked. Commit the changes.
                    transaction.Commit();
                    conn.Close();

                    MessageBox.Show("Product and Inventory saved successfully.", "Product Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    fList.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                // If an error occurred, cancel the transaction so no partial data is saved
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtPCode.Clear();
            txtName.Clear();
            cmbType.Text = "";
            txtModel.Clear();
            txtGeneration.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtPrice.Clear();
            txtColor.Clear();
            txtStorage.Clear();
            txtSpecific.Clear();
            txtDescription.Clear();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            MySqlTransaction transaction = null;

            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Update Product",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // 1. UPDATE PRODUCT
                    // We use 'UPDATE' instead of 'INSERT ... ON DUPLICATE KEY' because this is strictly an Update button.
                    // We use the SKU (txtPCode) to find the record to update.
                    string sqlProduct = @"
                UPDATE products 
                SET name = @name, 
                    type = @type, 
                    model = @model, 
                    generation = @generation, 
                    release_date = @release_date, 
                    price = @price, 
                    color = @color, 
                    storage = @storage, 
                    specifications = @specifications, 
                    status = @status, 
                    description = @description
                WHERE sku = @sku";

                    cmd = new MySqlCommand(sqlProduct, conn);
                    cmd.Transaction = transaction; // Link to transaction

                    cmd.Parameters.AddWithValue("@sku", txtPCode.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@type", cmbType.Text);
                    cmd.Parameters.AddWithValue("@model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@generation", Convert.ToInt16(txtGeneration.Text));
                    cmd.Parameters.AddWithValue("@release_date", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@color", txtColor.Text);
                    cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    cmd.Parameters.AddWithValue("@specifications", txtSpecific.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);

                    cmd.ExecuteNonQuery();

                    // 2. UPDATE INVENTORY
                    // We use a subquery (SELECT id FROM products WHERE sku = @sku) to get the ID automatically.
                    // This 'INSERT ... ON DUPLICATE KEY UPDATE' ensures that if the inventory row 
                    // is missing for some reason, it creates it. If it exists, it updates it.
                    string sqlInventory = @"
                INSERT INTO inventory (product_id, stock) 
                VALUES ((SELECT product_id FROM products WHERE sku = @sku), @stock)
                ON DUPLICATE KEY UPDATE stock = @stock";

                    using (MySqlCommand cmdInv = new MySqlCommand(sqlInventory, conn))
                    {
                        cmdInv.Transaction = transaction;
                        cmdInv.Parameters.AddWithValue("@sku", txtPCode.Text);

                        // Handle Stock input safely
                        int stockValue = 0;
                        if (!string.IsNullOrEmpty(numericUpDown1.Value.ToString()))
                        {
                            stockValue = Convert.ToInt32(numericUpDown1.Value.ToString());
                        }
                        cmdInv.Parameters.AddWithValue("@stock", stockValue);

                        cmdInv.ExecuteNonQuery();
                    }

                    // Commit changes
                    transaction.Commit();
                    conn.Close();

                    MessageBox.Show("Product and Inventory successfully updated.", "Product Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    fList.LoadRecords();
                    this.Dispose(); // Close the form after update
                }
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains('.'.ToString()) == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;
            e.Handled = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            //if (txtPCode.Text.ToString() != "") fillData(); // if the sku is filled, fill the data for updating
        }

        public void fillData() //call when updating to fill the fields
        {
            try
            {
                conn.Open();
                string sql = @"
                    SELECT p.sku, p.name, p.type, p.model, p.generation, p.release_date, p.price, p.color, p.storage, p.specifications, p.status, p.description, IFNULL(i.stock, 0) as stock
                    FROM products as p
                    LEFT JOIN inventory as i ON i.product_id = p.product_id
                    WHERE sku = @sku
                    LIMIT 1;
                ";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sku", txtPCode.Text);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtPCode.Text = dr["sku"].ToString();
                    txtName.Text = dr["name"].ToString();
                    cmbType.Text = dr["type"].ToString();
                    txtModel.Text = dr["model"].ToString();
                    txtGeneration.Text = dr["generation"].ToString();
                    txtPrice.Text = dr["price"].ToString();
                    txtColor.Text = dr["color"].ToString();
                    txtStorage.Text = dr["storage"].ToString();
                    txtSpecific.Text = dr["specifications"].ToString();
                    txtDescription.Text = dr["description"].ToString();
                    numericUpDown1.Value = Convert.ToInt32(dr["stock"].ToString());
                    cmbStatus.Text = dr["status"].ToString();

                    // safe date handling
                    if (DateTime.TryParse(dr["release_date"].ToString(), out DateTime date))
                        dateTimePicker1.Value = date;
                    else
                        dateTimePicker1.Value = DateTime.Today;
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
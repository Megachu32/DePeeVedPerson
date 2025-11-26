using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmPOS : Form
    {
        private DBConnection dbconn = new DBConnection();

        // in-memory cart model
        private class CartItem
        {
            public int ProductId { get; set; }
            public string SKU { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public int Qty { get; set; }
            public double Discount { get; set; } = 0;
            public double Total => (Price * Qty) - Discount;
        }

        private List<CartItem> cart = new List<CartItem>();

        // defaults
        private const double VAT_RATE = 0.10; // 10% fallback VAT if no tblVat exists
        // TODO: Native variable for Value After Tax. Currently, it's missing form the table. the default will be 10% the original price.
        /*
         * 
            Your app needs a VAT percentage to calculate receipts.

            Since your database has no VAT table, I gave it a default: 10% VAT.

            Why 10%?
            Because many POS systems use 10% as a placeholder when VAT is unknown.

            But you can safely change it.
         */


        public frmPOS()
        {
            InitializeComponent();
            lblDateNo.Text = DateTime.Now.ToLongDateString();
            KeyPreview = true;

            // Generate a fresh transno for this session
            lblTransNo.Text = GenerateTransNo();
            txtQty.Text = "1";
        }

        private string GenerateTransNo()
        {
            // Simple timestamp-based transno; unique for each new transaction
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text)) return;

                // Find product by SKU (used as barcode replacement)
                string sku = txtSearch.Text.Trim();

                using (var conn = new MySqlConnection(dbconn.MyConnection()))
                {
                    conn.Open();

                    string sql = "SELECT p.product_id, p.sku, p.name, p.price, IFNULL(i.stock,0) AS stock " +
                                 "FROM products p LEFT JOIN inventory i ON p.product_id = i.product_id " +
                                 "WHERE p.sku LIKE @sku LIMIT 1";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sku", sku);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                int stock = Convert.ToInt32(dr["stock"]);
                                int productId = Convert.ToInt32(dr["product_id"]);
                                string foundSku = dr["sku"].ToString();
                                double price = Convert.ToDouble(dr["price"]);
                                int qtyToAdd = 1;
                                if (!int.TryParse(txtQty.Text, out qtyToAdd)) qtyToAdd = 1;

                                // Add to in-memory cart (will validate stock)
                                AddToCart(productId, foundSku, dr["name"].ToString(), price, qtyToAdd, stock);
                                // clear searchbox selection for convenience
                                txtSearch.SelectionStart = 0;
                                txtSearch.SelectionLength = txtSearch.Text.Length;
                                txtSearch.Focus();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddToCart(int productId, string sku, string name, double price, int qtyToAdd, int availableStock)
        {
            // find existing
            var existing = cart.Find(c => c.ProductId == productId);
            if (existing != null)
            {
                if (existing.Qty + qtyToAdd > availableStock)
                {
                    MessageBox.Show($"Unable to proceed. Remaining qty on hand is {availableStock}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existing.Qty += qtyToAdd;
            }
            else
            {
                if (qtyToAdd > availableStock)
                {
                    MessageBox.Show($"Unable to proceed. Remaining qty on hand is {availableStock}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    SKU = sku,
                    Description = name,
                    Price = price,
                    Qty = qtyToAdd
                });
            }

            LoadCart();
        }

        public void LoadCart()
        {
            try
            {
                dgvBrandList.Rows.Clear();
                int i = 0;
                double total = 0, discount = 0;
                foreach (var item in cart)
                {
                    i++;
                    total += item.Total;
                    discount += item.Discount;
                    // Add row: index, productId (used as "id"), sku, description, price, qty, disc, total
                    dgvBrandList.Rows.Add(i, item.ProductId.ToString(), item.SKU, item.Description,
                        item.Price.ToString("#,##0.00"), item.Qty.ToString(), item.Discount.ToString("#,##0.00"), item.Total.ToString("#,##0.00"));
                }

                lblSalesTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();

                bool hasRecord = cart.Count > 0;
                btnSetPayment.Enabled = hasRecord;
                btnAddDiscount.Enabled = hasRecord;
                btnClearCart.Enabled = hasRecord;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Cart Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvBrandList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure valid row click
            if (e.RowIndex < 0) return;

            string colName = dgvBrandList.Columns[e.ColumnIndex].Name;

            // product id sits at cell index 1 in our Add row order
            int productId = Convert.ToInt32(dgvBrandList.Rows[e.RowIndex].Cells[1].Value);
            var item = cart.Find(c => c.ProductId == productId);
            if (item == null) return;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item", "Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cart.Remove(item);
                    LoadCart();
                }
            }
            else if (colName == "colAdd")
            {
                // check current stock from DB
                int available = GetStockForProduct(productId);
                if (item.Qty + int.Parse(txtQty.Text) <= available)
                {
                    item.Qty += int.Parse(txtQty.Text);
                    LoadCart();
                }
                else
                {
                    MessageBox.Show($"Remaining qty on hand is {available} !", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (colName == "colRemove")
            {
                int removeQty = int.Parse(txtQty.Text);
                if (item.Qty > removeQty)
                {
                    item.Qty -= removeQty;
                    LoadCart();
                }
                else
                {
                    // remove the item entirely
                    cart.Remove(item);
                    LoadCart();
                }
            }
        }

        private int GetStockForProduct(int productId)
        {
            try
            {
                using (var conn = new MySqlConnection(dbconn.MyConnection()))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT IFNULL(stock,0) FROM inventory WHERE product_id = @pid", conn))
                    {
                        cmd.Parameters.AddWithValue("@pid", productId);
                        var r = cmd.ExecuteScalar();
                        if (r == null || r == DBNull.Value) return 0;
                        return Convert.ToInt32(r);
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public void GetCartTotal()
        {
            double discount = 0;
            double sales = 0;
            double.TryParse(lblDiscount.Text, out discount);
            double.TryParse(lblSalesTotal.Text, out sales);

            // Try to read VAT from DB.tblVat if exists. If not, use default VAT_RATE
            double vatRate = VAT_RATE;
            try
            {
                using (var conn = new MySqlConnection(dbconn.MyConnection()))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT vat FROM tblVat LIMIT 1", conn))
                    {
                        var r = cmd.ExecuteScalar();
                        if (r != null && r != DBNull.Value)
                        {
                            vatRate = Convert.ToDouble(r);
                        }
                    }
                }
            }
            catch
            {
                // ignore - keep default vatRate
            }

            double vat = Math.Round(sales * vatRate, 2);
            double vatable = Math.Round(sales - vat, 2);

            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
        }

        private void DgvBrandList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBrandList.CurrentRow == null) return;
            int i = dgvBrandList.CurrentRow.Index;
            id = dgvBrandList[1, i].Value.ToString(); // productId as string
            price = dgvBrandList[4, i].Value.ToString();
        }

        private string id;
        private string price;

        private void FrmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                BtnNew_Click(sender, e);
            else if (e.KeyCode == Keys.F2)
                BtnSearchProd_Click(sender, e);
            else if (e.KeyCode == Keys.F3)
                BtnAddDiscount_Click(sender, e);
            else if (e.KeyCode == Keys.F4)
                BtnSetPayment_Click(sender, e);
            else if (e.KeyCode == Keys.F5)
                BtnClearCart_Click(sender, e);
            else if (e.KeyCode == Keys.F6)
                BtnDailySales_Click(sender, e);
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }
            else if (e.KeyCode == Keys.F10)
                BtnClose_Click(sender, e);
        }

        private void BtnSearchProd_Click(object sender, EventArgs e)
        {
            if (lblTransNo.Text == null) return;
            frmLookUp lookUpFrm = new frmLookUp(this);
            lookUpFrm.LoadRecords();
            lookUpFrm.ShowDialog();
        }

        private void BtnAddDiscount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id)) return;
            frmDiscount discountFrm = new frmDiscount(this);
            discountFrm.lblId.Text = id;
            discountFrm.txtPrice.Text = price;
            discountFrm.ShowDialog();
        }

        private void BtnSetPayment_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Cart is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bring up payment UI - existing frmSettle expects txtSale to hold the amount
            frmSettle setFrm = new frmSettle(this);
            setFrm.txtSale.Text = lblDisplayTotal.Text;
            var result = setFrm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // assume payment accepted, commit sale
                CommitSale(setFrm); // pass form in case it has payment details
            }
        }

        // Commit sale to sales + sale_items and update inventory
        private void CommitSale(Form settleForm)
        {
            using (var conn = new MySqlConnection(dbconn.MyConnection()))
            {
                conn.Open();
                using (var trx = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert sale
                        string insertSale = "INSERT INTO sales (sale_date, subtotal, tax, total) VALUES (@dt, @sub, @tax, @total)";
                        double subtotal = 0;
                        foreach (var c in cart) subtotal += c.Price * c.Qty;
                        // tax: read from previously calculated label (or recompute)
                        double tax = 0;
                        double.TryParse(lblVat.Text, out tax);
                        double total = 0;
                        double.TryParse(lblDisplayTotal.Text, out total);

                        using (var cmdSale = new MySqlCommand(insertSale, conn, trx))
                        {
                            cmdSale.Parameters.AddWithValue("@dt", DateTime.Now);
                            cmdSale.Parameters.AddWithValue("@sub", subtotal);
                            cmdSale.Parameters.AddWithValue("@tax", tax);
                            cmdSale.Parameters.AddWithValue("@total", total);
                            cmdSale.ExecuteNonQuery();
                        }

                        long saleId = -1;
                        using (var cmdL = new MySqlCommand("SELECT LAST_INSERT_ID()", conn, trx))
                        {
                            saleId = Convert.ToInt64(cmdL.ExecuteScalar());
                        }

                        // Insert sale items and update inventory
                        foreach (var c in cart)
                        {
                            using (var cmdItem = new MySqlCommand(
                                "INSERT INTO sale_items (sale_id, product_id, quantity, unit_price) VALUES (@sid, @pid, @qty, @price)",
                                conn, trx))
                            {
                                cmdItem.Parameters.AddWithValue("@sid", saleId);
                                cmdItem.Parameters.AddWithValue("@pid", c.ProductId);
                                cmdItem.Parameters.AddWithValue("@qty", c.Qty);
                                cmdItem.Parameters.AddWithValue("@price", c.Price);
                                cmdItem.ExecuteNonQuery();
                            }

                            // decrement inventory
                            using (var cmdUpd = new MySqlCommand(
                                "UPDATE inventory SET stock = GREATEST(IFNULL(stock,0) - @qty, 0) WHERE product_id = @pid",
                                conn, trx))
                            {
                                cmdUpd.Parameters.AddWithValue("@qty", c.Qty);
                                cmdUpd.Parameters.AddWithValue("@pid", c.ProductId);
                                cmdUpd.ExecuteNonQuery();
                            }
                        }

                        trx.Commit();
                        MessageBox.Show("Sale successfully committed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear cart and refresh UI
                        cart.Clear();
                        lblTransNo.Text = GenerateTransNo();
                        LoadCart();
                    }
                    catch (Exception ex)
                    {
                        trx.Rollback();
                        MessageBox.Show("Failed to commit sale: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cart.Clear();
                LoadCart();
            }
        }

        private void BtnDailySales_Click(object sender, EventArgs e)
        {
            frmSoldItems soldFrm = new frmSoldItems();
            soldFrm.dtFrom.Enabled = false;
            soldFrm.dtTo.Enabled = false;
            soldFrm.sUser = lblUser.Text;
            soldFrm.cboCashier.Enabled = false;
            soldFrm.cboCashier.Text = lblUser.Text;
            soldFrm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (dgvBrandList.Rows.Count > 0)
            {
                MessageBox.Show("Unable to Logout. Please cancel the transaction", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Logout Application", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (dgvBrandList.Rows.Count > 0) return;
            lblTransNo.Text = GenerateTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }
    }
}

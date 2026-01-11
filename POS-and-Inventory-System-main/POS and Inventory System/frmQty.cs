using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmQty : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmPOS fPos;

        private string pcode;
        private double price;
        private int qty;
        private int amount;
        private string name;
        private int discount;

        public frmQty(frmPOS _fPos)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fPos = _fPos;
        }

        // just for the passing of data from look up form to qty form
        public void ProductDetails(string _pcode, double _price, int _qty)
        {
            pcode = _pcode;
            price = _price;
            qty = _qty;

        }

        //private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //when enter is pressed on the qty textbox
        //    if (e.KeyChar == 13 && txtQty.Text != string.Empty)
        //    {
        //        string id = "";
        //        string status = "";
        //        bool found = false;

        //        //basically search the product returning status and it's stock.
        //        conn.Open();
        //        string sql = @"
        //            SELECT 
        //                p.product_id    AS id,
        //                p.name          AS name,
        //             IFNULL(i.stock,0)         AS stock,
        //                p.status        AS status,
        //                p.price         AS price,
        //                CASE
        //                WHEN d.is_active = 1 
        //                THEN d.discount_percentage
        //                ELSE 0
        //                END             AS discount
        //            FROM products AS p
        //            LEFT JOIN inventory AS i ON i.product_id = p.product_id
        //            LEFT JOIN discounts AS d ON p.product_id = d.product_id
        //            WHERE p.sku = @sku;
        //        ";
        //        cmd = new MySqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@sku", pcode);
        //        dr = cmd.ExecuteReader();
        //        dr.Read();
        //        if (dr.HasRows)
        //        {
        //            id = dr["id"].ToString();
        //            name = dr["name"].ToString();
        //            status = dr["status"].ToString();
        //            amount = int.Parse(dr["stock"].ToString());
        //            price = (status == "active") ? double.Parse(dr["price"].ToString()) : double.Parse(dr["price"].ToString()) / 2;
        //            discount = int.Parse(dr["discount"].ToString());
        //        }

        //        dr.Close();
        //        conn.Close();

        //        // checkes for if the product is inactive, doesn't have the stock or is incoming
        //        if ((amount < (int.Parse(txtQty.Text)) || status == "inactive") && status != "incoming")
        //        {
        //            MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            Dispose();
        //            return;
        //        }

        //        int qtyToAdd = int.Parse(txtQty.Text);

        //        // calculate using UNIT price
        //        double linePrice = price;
        //        double discountRate = discount / 100.0;

        //        // get table
        //        DataSet1 ds = fPos.ds;
        //        DataTable dt = ds.Tables["dtCheckOut"];

        //        if (dt == null)
        //        {
        //            fPos.btnClearCart.Enabled = false; 
        //            fPos.btnSetPayment.Enabled = false;
        //        }
        //        else
        //        {
        //            fPos.btnClearCart.Enabled = true;
        //            fPos.btnSetPayment.Enabled = true;
        //        }

        //            // try find existing row
        //            DataRow existingRow = dt.AsEnumerable()
        //                .FirstOrDefault(r => r["product_id"].ToString() == id); // basically loops variable r.product_id match id

        //        //if not null update
        //        if (existingRow != null)
        //        {
        //            int oldQty = Convert.ToInt32(existingRow["qty"]);
        //            int newQty = oldQty + qtyToAdd;

        //            if(amount < newQty && status != "incoming")
        //            {
        //                MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                Dispose();
        //                return;
        //            }

        //            double subtotal = linePrice * newQty;
        //            double discountAmount = subtotal * discountRate;

        //            existingRow["qty"] = newQty;
        //            existingRow["price"] = linePrice;   // update if price changed
        //            existingRow["discount"] = discount;
        //            existingRow["total"] = subtotal - discountAmount;
        //        }
        //        else
        //        {
        //            // ADD new row
        //            int qty = qtyToAdd;
        //            double subtotal = linePrice * qty;
        //            double discountAmount = subtotal * discountRate;

        //            DataRow row = dt.NewRow();
        //            row["product_id"] = id;
        //            row["name"] = name;
        //            row["qty"] = qty;
        //            row["price"] = linePrice;
        //            row["discount"] = discount;
        //            row["total"] = subtotal - discountAmount;

        //            if(status == "incoming")
        //            {
        //                row["order_mode"] = "pre_order";
        //            }
        //            else
        //            {
        //                row["order_mode"] = "normal";
        //            }

        //                dt.Rows.Add(row);
        //        }

        //        dt = ds.Tables["dtCheckOut"];
        //        // Use double or decimal for money/tax calculations
        //        double finalTotal = Convert.ToDouble(dt.Compute("SUM(total)", ""));
        //        fPos.lblSalesTotal.Text = (finalTotal != null || finalTotal != 0) ? finalTotal.ToString("N2") : "00.00";

        //        fPos.txtSearch.Clear();
        //        fPos.txtSearch.Focus();
        //        fPos.dgvBrandList.DataSource = dt;
        //        Dispose();

        //    }
        //}

        //new function, update how active and incoming products are handled
        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if Enter is pressed and text is not empty
            if (e.KeyChar == 13 && txtQty.Text != string.Empty)
            {
                string id = "";
                string name = "";
                string status = "";
                int stockOnDb = 0;
                double price = 0;
                double discount = 0;

                // 1. DATABASE LOOKUP
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Note: Make sure 'pcode' is defined in your class scope
                string sql = @"
            SELECT 
                p.product_id    AS id,
                p.name          AS name,
                IFNULL(i.stock,0) AS stock,
                p.status        AS status,
                p.price         AS price,
                CASE
                    WHEN d.is_active = 1 
                    THEN d.discount_percentage
                    ELSE 0
                END             AS discount
            FROM products AS p
            LEFT JOIN inventory AS i ON i.product_id = p.product_id
            LEFT JOIN discounts AS d ON p.product_id = d.product_id
            WHERE p.sku = @sku";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sku", pcode); // Ensure 'pcode' is available
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = dr["id"].ToString();
                            name = dr["name"].ToString();
                            status = dr["status"].ToString();
                            stockOnDb = int.Parse(dr["stock"].ToString());
                            discount = int.Parse(dr["discount"].ToString());

                            // --- LOGIC FOR 50% DEPOSIT ---
                            double originalPrice = double.Parse(dr["price"].ToString());

                            if (status.ToLower() == "incoming")
                            {
                                // Calculate 50%
                                price = originalPrice / 2;
                                // IMPORTANT: Change name so Receipt clearly says "Deposit"
                                name = name + " (50% Deposit)";
                            }
                            else
                            {
                                price = originalPrice;
                            }
                        }
                        else
                        {
                            dr.Close();
                            conn.Close();
                            return; // Product not found
                        }
                    }
                }
                conn.Close();

                // 2. STOCK VALIDATION
                int qtyRequested = int.Parse(txtQty.Text);

                // Logic: If it is NOT incoming, we must check stock.
                // Incoming items (pre-orders) usually have 0 stock, so we skip the check for them.
                if (status.ToLower() != "incoming" && (stockOnDb < qtyRequested || status == "inactive"))
                {
                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + stockOnDb, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. ADD TO CART LOGIC
                double linePrice = price;
                double discountRate = discount / 100.0;

                DataSet1 ds = fPos.ds;
                DataTable dt = ds.Tables["dtCheckOut"];

                // Enable buttons if cart was empty
                if (dt.Rows.Count == 0)
                {
                    fPos.btnClearCart.Enabled = true;
                    fPos.btnSetPayment.Enabled = true;
                }

                // Check if item already exists in the cart
                DataRow existingRow = dt.AsEnumerable()
                    .FirstOrDefault(r => r["product_id"].ToString() == id);

                if (existingRow != null)
                {
                    // UPDATE EXISTING ROW
                    int oldQty = Convert.ToInt32(existingRow["qty"]);
                    int newQty = oldQty + qtyRequested;

                    // Re-check stock for the new total quantity
                    if (status.ToLower() != "incoming" && stockOnDb < newQty)
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + stockOnDb, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double subtotal = linePrice * newQty;
                    double discountAmount = subtotal * discountRate;

                    existingRow["qty"] = newQty;
                    existingRow["price"] = linePrice;
                    existingRow["discount"] = discount;
                    existingRow["total"] = subtotal - discountAmount;

                    // Ensure status/order_mode is preserved or updated
                    if (status.ToLower() == "incoming") existingRow["order_mode"] = "pre_order";
                    else existingRow["order_mode"] = "normal";
                }
                else
                {
                    // ADD NEW ROW
                    double subtotal = linePrice * qtyRequested;
                    double discountAmount = subtotal * discountRate;

                    DataRow row = dt.NewRow();
                    row["product_id"] = id;
                    row["name"] = name;
                    row["qty"] = qtyRequested;
                    row["price"] = linePrice;
                    row["discount"] = discount;
                    row["total"] = subtotal - discountAmount;

                    if (status.ToLower() == "incoming")
                    {
                        row["order_mode"] = "pre_order";
                    }
                    else
                    {
                        row["order_mode"] = "normal";
                    }

                    // Ensure your DataTable actually has a column named "order_mode" 
                    // If it doesn't, this line will crash. 
                    // If you get an error here, go to your DataSet designer and add the column.
                    dt.Rows.Add(row);
                }

                // 4. UPDATE TOTAL ON MAIN FORM
                // Using object arithmetic to safely sum the column
                object sumObj = dt.Compute("SUM(total)", "");
                double finalTotal = (sumObj != DBNull.Value) ? Convert.ToDouble(sumObj) : 0.00;

                fPos.lblSalesTotal.Text = finalTotal.ToString("N2");

                // 5. CLEANUP UI
                fPos.txtSearch.Clear();
                fPos.txtSearch.Focus();

                // Refresh grid
                fPos.dgvBrandList.DataSource = dt;

                this.Dispose();
            }
        }
    }
}
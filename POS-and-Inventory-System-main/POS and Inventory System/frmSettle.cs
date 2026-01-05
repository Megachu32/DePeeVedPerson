using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS_and_Inventory_System
{
    public partial class frmSettle : Form
    {
        frmPOS fpos;
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();

        double sale;
        double cash;
        double change;

        public frmSettle(frmPOS _fpos)
        {
            InitializeComponent();
            fpos = _fpos;
            conn = new MySqlConnection(dbconn.MyConnection());
            KeyPreview = true;
            lblTotal.Text = fpos.lblSalesTotal.Text;
        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            sale = Convert.ToDouble(fpos.lblSalesTotal.Text);
            cash = Convert.ToDouble(txtCash.Text);
            change = cash - sale;
            try
            {
                lblCash.Text = cash.ToString("#,##.00");
                if(change >= 0)
                {
                    lblChange.Text = change.ToString("#,##.00");
                }
                else
                {
                    lblChange.Text = "00.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(change.ToString());
            }
        }

        private void Btn7_Click(object sender, EventArgs e) 
            => txtCash.Text += btn7.Text;

        private void Btn8_Click(object sender, EventArgs e)
            => txtCash.Text += btn8.Text;

        private void Btn9_Click(object sender, EventArgs e)
            => txtCash.Text += btn9.Text;

        private void BtnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void Btn4_Click(object sender, EventArgs e)
            => txtCash.Text += btn4.Text;

        private void Btn5_Click(object sender, EventArgs e)
            => txtCash.Text += btn5.Text;

        private void Btn6_Click(object sender, EventArgs e)
            => txtCash.Text += btn6.Text;

        private void Btn0_Click(object sender, EventArgs e)
            => txtCash.Text += btn0.Text;

        private void Btn1_Click(object sender, EventArgs e)
            => txtCash.Text += btn1.Text;

        private void Btn2_Click(object sender, EventArgs e)
            => txtCash.Text += btn2.Text;

        private void Btn3_Click(object sender, EventArgs e)
            => txtCash.Text += btn3.Text;

        private void Btn00_Click(object sender, EventArgs e)
            => txtCash.Text += btn00.Text;

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Initial Validation
                if (!double.TryParse(lblChange.Text, out double changeVal) || changeVal < 0)
                {
                    MessageBox.Show("Insufficient Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Open Connection ONCE
                if (conn.State == ConnectionState.Closed) conn.Open();

                int realCustomerId = 0;
                int realStoreId = 0;
                long realSaleId = 0;

                // --- STEP A: GET STORE ID ---
                string sqlStore = "SELECT store_id FROM staff WHERE staff_id = @staff_id";
                using (MySqlCommand cmdStore = new MySqlCommand(sqlStore, conn))
                {
                    cmdStore.Parameters.AddWithValue("@staff_id", fpos.staffId);
                    object result = cmdStore.ExecuteScalar();
                    realStoreId = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }

                // --- STEP B: REGISTER CUSTOMER ---
                string sqlCust = @"INSERT INTO customers (NAME, phone, email, government_id, created_at) 
                           VALUES (@name, @phone, @email, @gov, @date)";
                using (MySqlCommand cmdCust = new MySqlCommand(sqlCust, conn))
                {
                    cmdCust.Parameters.AddWithValue("@name", (fpos.comboBox1.Text == "offline") ? "Walk-in" : "Online");
                    cmdCust.Parameters.AddWithValue("@phone", "111111111");
                    cmdCust.Parameters.AddWithValue("@email", "lolme@gmail.com");
                    cmdCust.Parameters.AddWithValue("@gov", "000-000-0000");
                    cmdCust.Parameters.AddWithValue("@date", fpos.timeDate);
                    cmdCust.ExecuteNonQuery();
                    realCustomerId = (int)cmdCust.LastInsertedId; // Accurate way to get ID
                }

                // --- STEP C: CREATE SALE ---
                string sqlSale = @"INSERT INTO sales (customer_id, customer_ref, sale_date, total, store_id, payment_method, purchase_type, pickup_method)
                           VALUES (@cust_id, @ref, @date, @total, @store, @pay, @pur, @pick)";
                using (MySqlCommand cmdSale = new MySqlCommand(sqlSale, conn))
                {
                    string custRef = (fpos.comboBox1.Text == "offline") ? "WLK" + realCustomerId : "ONL" + realCustomerId;
                    cmdSale.Parameters.AddWithValue("@cust_id", realCustomerId);
                    cmdSale.Parameters.AddWithValue("@ref", custRef);
                    cmdSale.Parameters.AddWithValue("@date", fpos.timeDate);
                    cmdSale.Parameters.AddWithValue("@total", sale);
                    cmdSale.Parameters.AddWithValue("@store", realStoreId);
                    cmdSale.Parameters.AddWithValue("@pay", fpos.comboBox3.Text);
                    cmdSale.Parameters.AddWithValue("@pur", fpos.comboBox1.Text);
                    cmdSale.Parameters.AddWithValue("@pick", fpos.comboBox2.Text);
                    cmdSale.ExecuteNonQuery();
                    realSaleId = cmdSale.LastInsertedId;
                }

                // --- STEP D: START TRANSACTION FOR ITEMS ---
                DataTable dt = fpos.ds.Tables["dtCheckOut"];
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string productid = row["product_id"].ToString();
                            int qty = Convert.ToInt32(row["qty"]);
                            string status = "";
                            decimal price = Convert.ToDecimal(row["price"]);
                            long preorderId = 0;

                            string temp = @"SELECT status FROM products where product_id = @productid";
                            using (MySqlCommand pCmd = new MySqlCommand(temp, conn, trans))
                            {
                                pCmd.Parameters.AddWithValue("@productid", productid);
                                pCmd.ExecuteNonQuery();
                                status = pCmd.ExecuteScalar().ToString();
/*                                MessageBox.Show(status);
*/                            }

                            // 1. Preorder Logic
                            if (status.ToLower() == "incoming")
                            {
                                string preSql = @"INSERT INTO preorders (customer_id, product_id, preorder_date, status, money_hold_amount, payment_method)
                                          SELECT @cid, product_id, @date, 'order_placed', @hold, @method FROM products WHERE product_id = @productid";
                                using (MySqlCommand pCmd = new MySqlCommand(preSql, conn, trans))
                                {
                                    pCmd.Parameters.AddWithValue("@cid", realCustomerId);
                                    pCmd.Parameters.AddWithValue("@date", fpos.timeDate);
                                    pCmd.Parameters.AddWithValue("@hold", price * 0.10m);
                                    pCmd.Parameters.AddWithValue("@method", fpos.comboBox3.Text);
                                    pCmd.Parameters.AddWithValue("@productid", productid);
                                    pCmd.ExecuteNonQuery();
                                    preorderId = (int)pCmd.LastInsertedId;
                                }
                            }

                            // 2. Inventory Upsert
                            if (status.ToLower() != "incoming")
                            {
                                string invSql = @"INSERT INTO inventory (product_id, stock)
                                    SELECT product_id, 0 FROM products WHERE product_id = @productid
                                    ON DUPLICATE KEY UPDATE stock = stock - @qty";
                                using (MySqlCommand iCmd = new MySqlCommand(invSql, conn, trans))
                                {
                                    iCmd.Parameters.AddWithValue("@qty", qty);
                                    iCmd.Parameters.AddWithValue("@productid", productid);
                                    iCmd.ExecuteNonQuery();
                                }
                            }

                            // 3. Discount Lookup
                            decimal discAmt = 0;
                            string discSql = "SELECT discount_percentage FROM discounts WHERE product_id = @productid AND is_active=1 LIMIT 1";
                            using (MySqlCommand dCmd = new MySqlCommand(discSql, conn, trans))
                            {
                                dCmd.Parameters.AddWithValue("@productid", productid);
                                object dRes = dCmd.ExecuteScalar();
                                if (dRes != null && dRes != DBNull.Value)
                                    discAmt = (price * qty) * (Convert.ToDecimal(dRes) / 100);
                            }

                            // 4. Insert Sale Item
                            string itemSql = @"
                                        INSERT INTO sale_items (sale_id, product_id, quantity, unit_price, discount_amount, preorder_id)
                                        SELECT @sid, product_id, @qty, @prc, @disc, @preorderid
                                        FROM products WHERE product_id=@productid";
                            using (MySqlCommand sCmd = new MySqlCommand(itemSql, conn, trans))
                            {
                                sCmd.Parameters.AddWithValue("@sid", realSaleId);
                                sCmd.Parameters.AddWithValue("@qty", qty);
                                sCmd.Parameters.AddWithValue("@prc", price);
                                sCmd.Parameters.AddWithValue("@disc", discAmt);
                                sCmd.Parameters.AddWithValue("@productid", productid);
                                if(preorderId > 0)
                                {
                                    sCmd.Parameters.AddWithValue("@preorderid", preorderId);
                                }
                                else
                                {
                                    sCmd.Parameters.AddWithValue("@preorderid", null);
                                }
                                sCmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw; // Let the outer catch handle the alert
                    }
                }

                fpos.ds.Tables["dtCheckOut"].Clear();

                // 3. Finalize
                frmReceipt frm = new frmReceipt(fpos);
                frm.LoadReport(txtCash.Text, lblChange.Text);
                frm.ShowDialog();

                MessageBox.Show("Payment Successfully Saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fpos.LoadCart();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void FrmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
            else if (e.KeyCode == Keys.Enter)
                BtnEnter_Click(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

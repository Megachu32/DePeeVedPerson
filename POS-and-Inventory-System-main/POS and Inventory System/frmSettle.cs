using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
            //safely extract text to avoid error/crashing

            //sale = Convert.ToDouble(fpos.lblSalesTotal.Text);
            double.TryParse(fpos.lblSalesTotal.Text, out sale);
            //cash = Convert.ToDouble(txtCash.Text);
            if (!double.TryParse(txtCash.Text, out cash))
            {
                cash = 0;
            }
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

        private int doubleToInt(double input)
        {
            int result;

                result = Convert.ToInt32(input);

            return result;
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
                //if (!double.TryParse(lblChange.Text, out double changeVal) || changeVal < 0)
                //{
                //    MessageBox.Show("Insufficient Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                /*
                 step:  read if text is value
                        calculate if the resulting number is negative
                        if negative, show warning and exit
                 */

                if (!double.TryParse(lblChange.Text, out double changeVal) || doubleToInt(change)<0)
                {
                    MessageBox.Show("Invalid Cash Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse the Cash Amount entered by user (assuming txtCash is your input box)
                if (!decimal.TryParse(txtCash.Text, out decimal cashPaid))
                {
                    MessageBox.Show("Invalid Cash Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    realCustomerId = (int)cmdCust.LastInsertedId;
                }

                // --- STEP C: CREATE SALE (UPDATED with New Columns) ---
                // Added: paid_amount, change_amount, staff_id, status
                string sqlSale = @"INSERT INTO sales 
        (customer_id, customer_ref, sale_date, total, paid_amount, change_amount, store_id, staff_id, payment_method, purchase_type, pickup_method, status)
        VALUES 
        (@cust_id, @ref, @date, @total, @paid, @change, @store, @staff, @pay, @pur, @pick, 'completed')";

                using (MySqlCommand cmdSale = new MySqlCommand(sqlSale, conn))
                {
                    string custRef = (fpos.comboBox1.Text == "offline") ? "WLK" + realCustomerId : "ONL" + realCustomerId;

                    cmdSale.Parameters.AddWithValue("@cust_id", realCustomerId);
                    cmdSale.Parameters.AddWithValue("@ref", custRef);
                    cmdSale.Parameters.AddWithValue("@date", fpos.timeDate);
                    cmdSale.Parameters.AddWithValue("@total", sale); // Assuming 'sale' is the Grand Total variable

                    // NEW PARAMETERS
                    cmdSale.Parameters.AddWithValue("@paid", cashPaid);
                    cmdSale.Parameters.AddWithValue("@change", Convert.ToDecimal(changeVal));
                    cmdSale.Parameters.AddWithValue("@staff", fpos.staffId);

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

                            // This 'price' is ALREADY the 50% amount because of your frmQTY logic
                            decimal price = Convert.ToDecimal(row["price"]);

                            string status = "";
                            long preorderId = 0;

                            // 1. Get Status from Database 
                            // (We still need this to know if it goes into 'preorders' table)
                            string temp = @"SELECT status FROM products where product_id = @productid";
                            using (MySqlCommand pCmd = new MySqlCommand(temp, conn, trans))
                            {
                                pCmd.Parameters.AddWithValue("@productid", productid);
                                object statusObj = pCmd.ExecuteScalar();
                                status = statusObj != null ? statusObj.ToString() : "";
                                if(status == "incoming")
                                {
                                    //frmUserregis frm = frmUserregis(fpos);
                                    //frm.Show();
                                }
                            }

                            // 2. Preorder Logic
                            if (status.ToLower() == "incoming")
                            {
                                string preSql = @"INSERT INTO preorders (customer_id, product_id, preorder_date, status, money_hold_amount, payment_method, pickup_code)
                                  SELECT @cid, product_id, @date, 'order_placed', @hold, @method, @pickupCode  FROM products WHERE product_id = @productid";
                                using (MySqlCommand pCmd = new MySqlCommand(preSql, conn, trans))
                                {
                                    pCmd.Parameters.AddWithValue("@cid", realCustomerId);
                                    pCmd.Parameters.AddWithValue("@date", fpos.timeDate);
                                    pCmd.Parameters.AddWithValue("@pickupCode", fpos.lblTransNo.Text);
                                    // --- CRITICAL CHANGE IS HERE ---
                                    // OLD CODE: pCmd.Parameters.AddWithValue("@hold", price * 0.50m); 
                                    // NEW CODE: We use 'price' directly because it is already halved in frmQTY
                                    pCmd.Parameters.AddWithValue("@hold", price);
                                    // -------------------------------

                                    pCmd.Parameters.AddWithValue("@method", fpos.comboBox3.Text);
                                    pCmd.Parameters.AddWithValue("@productid", productid);
                                    pCmd.ExecuteNonQuery();
                                    preorderId = (int)pCmd.LastInsertedId;
                                }
                            }

                            // 3. Inventory Upsert (Only for normal items)
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

                            // 4. Discount Lookup (Standard Logic)
                            decimal discAmt = 0;
                            string discSql = "SELECT discount_percentage FROM discounts WHERE product_id = @productid AND is_active=1 LIMIT 1";

                            using (MySqlCommand dCmd = new MySqlCommand(discSql, conn, trans))

                            {

                                dCmd.Parameters.AddWithValue("@productid", productid);

                                object dRes = dCmd.ExecuteScalar();

                                if (dRes != null && dRes != DBNull.Value)

                                    discAmt = (price * qty) * (Convert.ToDecimal(dRes) / 100);

                            }

                            // 5. Insert Sale Item
                            // We insert the price exactly as it is in the cart (The 50% amount)
                            string itemSql = @"INSERT INTO sale_items (sale_id, product_id, quantity, unit_price, discount_amount, preorder_id)
                               VALUES (@sid, @productid, @qty, @prc, @disc, @preorderid)";

                            using (MySqlCommand sCmd = new MySqlCommand(itemSql, conn, trans))
                            {
                                sCmd.Parameters.AddWithValue("@sid", realSaleId);
                                sCmd.Parameters.AddWithValue("@qty", qty);
                                sCmd.Parameters.AddWithValue("@prc", price); // Saves the deposit amount
                                sCmd.Parameters.AddWithValue("@disc", discAmt);
                                sCmd.Parameters.AddWithValue("@productid", productid);
                                sCmd.Parameters.AddWithValue("@preorderid", preorderId > 0 ? (object)preorderId : DBNull.Value);
                                sCmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }




                    // testing



                    DataSet1 ds = fpos.ds;



                    // 1. Open Connection (if not already open)

                    if (conn.State == ConnectionState.Closed) conn.Open();



                    // 2. The Header Query (Updated to use specific ID)

                    string sql = @"

            SELECT 

                st.name AS staff_name,

                c.name AS customer_name,

                s.customer_ref AS customer_ref,

                /* Generates Invoice No: YYYYMMDD + 4-digit padded Customer ID */

                CONCAT(DATE_FORMAT(s.sale_date, '%Y%m%d'), LPAD(s.customer_id, 4, '0')) AS invoice_no,

                DATE(s.sale_date) AS DATE,

                TIME(s.sale_date) AS TIME,

                str.store_name,

                str.store_address AS store_location,

                str.company_name,

                str.company_location,

                str.customer_service_phone AS cs_number,

                s.payment_method,

                s.purchase_type,

                s.order_mode,

                s.store_id,

                s.sale_id, /* Ensure this is selected so we can map it */

                s.total AS total_amount,

                (SELECT SUM(quantity) FROM sale_items WHERE sale_id = s.sale_id) AS total_items

            FROM sales s

            JOIN customers c ON s.customer_id = c.customer_id

            LEFT JOIN stores str ON s.store_id = str.store_id

            /* FIX: Link the staff correctly. If sales table has no staff_id, pass it as a parameter instead */

            LEFT JOIN staff st ON st.staff_id = @staffIdParam

            WHERE s.sale_id = @targetSaleId";  // <--- KEY CHANGE: Target specific ID



                    // 3. Create the Header Adapter & Temp Table

                    DataTable dtTempHeader = new DataTable();

                    using (MySqlCommand cmdHeader = new MySqlCommand(sql, conn))

                    {

                        // Pass the ID we generated during the INSERT phase

                        cmdHeader.Parameters.AddWithValue("@targetSaleId", realSaleId);

                        // Pass the staff ID (assuming fpos.staffId exists)

                        cmdHeader.Parameters.AddWithValue("@staffIdParam", fpos.staffId);



                        using (MySqlDataAdapter daInvoice = new MySqlDataAdapter(cmdHeader))

                        {

                            daInvoice.Fill(dtTempHeader);

                        }

                    }



                    if (dtTempHeader.Rows.Count > 0)

                    {

                        // 4. Import the Header Row

                        // Clear old data first to be safe

                        ds.Tables["dtInvoice"].Clear();

                        // ImportRow is great: it ignores columns that exist in SQL but not in your Dataset

                        ds.Tables["dtInvoice"].ImportRow(dtTempHeader.Rows[0]);



                        // 5. Fetch the Items

                        string checkOutSql = @"

                SELECT 

                    si.product_id,

                    p.name,

                    si.quantity AS qty,

                    si.unit_price AS price,

                    IFNULL(d.discount_percentage,0) AS discount_percentage,

                    si.discount_amount AS discount,

                    (si.quantity * si.unit_price) AS total,

                    (si.quantity * si.unit_price) - si.discount_amount AS total_after,

                    si.sale_id,

                    s.store_id,

                    s.order_mode

                FROM sale_items si

                JOIN products p ON si.product_id = p.product_id

                JOIN sales s ON si.sale_id = s.sale_id

                LEFT JOIN discounts d ON d.product_id = si.product_id

                WHERE si.sale_id = @lastSaleId";



                        using (MySqlCommand cmdItems = new MySqlCommand(checkOutSql, conn))

                        {

                            cmdItems.Parameters.AddWithValue("@lastSaleId", realSaleId);



                            using (MySqlDataAdapter daItems = new MySqlDataAdapter(cmdItems))

                            {

                                // Clear old items from the "cart" view so we only print what we just bought

                                ds.Tables["dtCheckOut"].Clear();

                                daItems.Fill(ds.Tables["dtCheckOut"]);

                            }

                        }



                        // 6. Now you are ready to show the report!



                        // this is for non direct printing



                        //// 2. Instantiate Report

                        //CrystalReport1 myReport = new CrystalReport1();



                        //// 3. Pass the WHOLE Dataset (Contains dtInvoice AND dtCheckOut)

                        //myReport.SetDataSource(fpos.ds);



                        //// 4. Show Report

                        //using (frmReportViewer viewerForm = new frmReportViewer())

                        //{

                        //    viewerForm.crystalReportViewer1.ReportSource = myReport;

                        //    viewerForm.ShowDialog();

                        //}





                        //this is for direcrt printing

                        CrystalReport1 myReport = new CrystalReport1();



                        // 1. Pass the Dataset

                        myReport.SetDataSource(fpos.ds);



                        // 2. (Optional) Specify a specific printer name

                        // If you don't set this, it prints to the Windows Default Printer

                        // myReport.PrintOptions.PrinterName = "EPSON TM-T82 Receipt"; 



                        // 3. Print Directly

                        // Parameters: (Copies, Collated, StartPage, EndPage)

                        // 0, 0 means "Print All Pages"

                        myReport.PrintToPrinter(1, false, 0, 0);



                        // 5. Cleanup

                        fpos.ds.Tables["dtInvoice"].Clear();

                        fpos.ds.Tables["dtCheckOut"].Clear();



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            MessageBox.Show("Payment Successfully Saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fpos.btnClearCart.Enabled = false;
            fpos.btnSetPayment.Enabled = false;

            if (fpos.dgvBrandList.Rows.Count > 0) return;
            fpos.GetTransNo();
            fpos.txtSearch.Enabled = true;
            fpos.txtSearch.Focus();
            this.Dispose();
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

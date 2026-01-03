using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using static System.Net.Mime.MediaTypeNames;

namespace POS_and_Inventory_System
{
    public partial class frmPOS : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;
        private DBConnection dbconn = new DBConnection();

        int qty;
        string id;
        string price;

        public DataSet1 ds = new DataSet1();

        public frmPOS()
        {
            InitializeComponent();
            lblDateNo.Text = DateTime.Now.ToLongDateString();
            conn = new MySqlConnection(dbconn.MyConnection());
            KeyPreview = true;
            //NotifyCriticalItems();
        }

        //public void NotifyCriticalItems()
        //{
        //    string critical = "";
        //    conn.Open();
        //    cmd = new SqlCommand("SELECT count(*) FROM vwCriticalItems", conn);
        //    string count = cmd.ExecuteScalar().ToString();
        //    conn.Close();

        //    int i = 0;
        //    conn.Open();
        //    cmd = new SqlCommand("SELECT * FROM vwCriticalItems", conn);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        i++;
        //        critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine;
        //    }
        //    dr.Close();
        //    conn.Close();

        //    PopupNotifier popup = new PopupNotifier();
        //    popup.Image = Properties.Resources.error;
        //    popup.TitleText = count + "Critical Item(s)";
        //    popup.ContentText = critical;
        //    popup.Popup();
        //}

        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transNo;
                conn.Open();
                string sql = @"
                    SELECT LPAD(COUNT(sale_id) + 1, 4, '0')
                    FROM sales
                ";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblTransNo.Text = sdate + dr[0].ToString();
                }
                else
                {
                    transNo = sdate + "0001";
                    lblTransNo.Text = transNo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        //text box search text changed event
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty) return;
                else
                {
                    string _pcode;
                    double _price;
                    int _qty;
                    conn.Open();
                    string sql = @"
                        SELECT 
                        p.product_id AS pcode,
                        p.price AS price,
                        i.stock AS stock
                        FROM products AS p
                        JOIN inventory AS i ON i.product_id = p.product_id
                        WHERE sku LIKE @text
                    ";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@text", txtSearch.Text);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {

                        qty = int.Parse(dr["stock"].ToString()); // stock of the product in inventory
                        _pcode = dr["pcode"].ToString(); // product id
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text); // quantity to add

                        dr.Close();
                        conn.Close();

                        AddToCart(_pcode, _price, _qty);
                    }
                    else
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
        }

        // Adds a product to the cart for the current transaction.
        // If the product already exists, it increases the quantity.
        // Before adding/updating, it checks if enough stock is available.
        private void AddToCart(string _pcode, double _price, int _qty)
        {
            //string id = "";
            //bool found = false;
            //int cartQty = 0;
            //int _transno = int.Parse(lblTransNo.Text.Substring(lblTransNo.Text.Length - 4)); //takes the last 4 digits of transno to get sale id
            //conn.Open();
            //string sql = @"
            //    SELECT *
            //    FROM sales AS s
            //    LEFT JOIN sale_items AS si ON si.sale_id = s.sale_id
            //    WHERE s.sale_id=@transno AND s.total = NULL OR s.total = 0 AND si.product_id = @pcode
            //";
            //cmd = new MySqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("@transno", _transno);
            //cmd.Parameters.AddWithValue("@pcode", _pcode);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //if (dr.HasRows)
            //{
            //    found = true;
            //    id = dr["id"].ToString();
            //    cartQty = int.Parse(dr["qty"].ToString());
            //}
            //else found = false;
            //dr.Close();
            //conn.Close();

            //search in the dgv or in the tablele

            //if there's products go here
            //if (found)
            //{
            //    if (qty < (int.Parse(txtQty.Text) + cartQty))
            //    {
            //        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    conn.Open();
            //    string sql1 = "UPDATE tblCart SET qty=(qty +" + _qty + ") WHERE id= '" + id + "'";
            //    cmd = new MySqlCommand(sql1, conn);
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    txtSearch.SelectionStart = 0;
            //    txtSearch.SelectionLength = txtSearch.Text.Length;
            //    LoadCart();
            //    //Dispose();
            //}
            //// if there's no products go here
            //else
            //{
            //    if (qty < int.Parse(txtQty.Text))
            //    {
            //        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    conn.Open();
            //    string sql1 = "INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier) " +
            //        "VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)";
            //    cmd = new MySqlCommand(sql1, conn);
            //    cmd.Parameters.AddWithValue("@transno", lblTransNo.Text);
            //    cmd.Parameters.AddWithValue("@pcode", _pcode);
            //    cmd.Parameters.AddWithValue("@price", _price);
            //    cmd.Parameters.AddWithValue("@qty", _qty);
            //    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            //    cmd.Parameters.AddWithValue("@cashier", lblUser.Text);
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    txtSearch.SelectionStart = 0;
            //    txtSearch.SelectionLength = txtSearch.Text.Length;
            //    LoadCart();
            //    //Dispose();
            //}
        }

        public void LoadCart()
        {
            try
            {
                bool hasRecord = false;
                dgvBrandList.Rows.Clear();
                int i = 0;
                double total = 0, discount = 0;
                conn.Open();
                string sql = "SELECT c.id, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart AS c INNER JOIN " +
                    "tblProduct AS p on c.pcode=p.pcode WHERE transno LIKE '" + lblTransNo.Text + "' AND status LIKE 'Pending'";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += double.Parse(dr["total"].ToString());
                    discount += double.Parse(dr["disc"].ToString());
                    dgvBrandList.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(),
                        dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());
                    hasRecord = true;
                }
                dr.Close();
                conn.Close();
                lblSalesTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                btnSetPayment.Enabled = hasRecord;
                //btnAddDiscount.Enabled = hasRecord;
                btnClearCart.Enabled = hasRecord;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }


        private void DgvBrandList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string colName = dgvBrandList.Columns[e.ColumnIndex].Name;
            //if (colName == "Delete")
            //{
            //    if (MessageBox.Show("Remove this item", "Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        conn.Open();
            //        string sql = "DELETE FROM tblCart WHERE id LIKE '" + dgvBrandList.Rows[e.RowIndex].Cells[1].Value.ToString() + "'";
            //        cmd = new MySqlCommand(sql, conn);
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //        MessageBox.Show("item has successfully removed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LoadCart();
            //    }
            //}
            //else if (colName == "colAdd")
            //{
            //    int i = 0;
            //    conn.Open();
            //    string sql = "SELECT sum(qty) AS qty FROM tblProduct WHERE pcode LIKE '" + 
            //        dgvBrandList.Rows[e.RowIndex].Cells[2].Value.ToString() + "' GROUP BY pcode";
            //    cmd = new MySqlCommand(sql, conn);
            //    i = int.Parse(cmd.ExecuteScalar().ToString());
            //    conn.Close();

            //    if (int.Parse(dgvBrandList.Rows[e.RowIndex].Cells[5].Value.ToString()) < i)
            //    {
            //        conn.Open();
            //        string sql2 = "UPDATE tblCart SET qty = qty +" + int.Parse(txtQty.Text) + " WHERE transno LIKE '" + 
            //            lblTransNo.Text + "' AND pcode LIKE '" + dgvBrandList.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
            //        cmd = new MySqlCommand(sql2, conn);
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //        LoadCart();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Remaining qty on hand is " + i + " !", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //else if (colName == "colRemove")
            //{
            //    int i = 0;
            //    conn.Open();
            //    string sql = "SELECT sum(qty) AS qty FROM tblCart WHERE pcode LIKE '" + dgvBrandList.Rows[e.RowIndex].Cells[2].Value.ToString() + 
            //        "' AND transno LIKE '" + lblTransNo.Text + "' GROUP BY transno, pcode";
            //    cmd = new MySqlCommand(sql, conn);
            //    i = int.Parse(cmd.ExecuteScalar().ToString());
            //    conn.Close();

            //    if (i > 1)
            //    {
            //        conn.Open();
            //        string sql2 = "UPDATE tblCart SET qty = qty - " + int.Parse(txtQty.Text) + " WHERE transno LIKE '" +
            //            lblTransNo.Text + "' AND pcode LIKE '" + dgvBrandList.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
            //        cmd = new MySqlCommand(sql2, conn);
            //        cmd.ExecuteNonQuery();
            //        conn.Close();

            //        LoadCart();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Remaining qty on cart is " + i + " !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            // Check if the clicked cell is the Delete button column (e.g., index 5)
            string colName = dgvBrandList.Columns[e.ColumnIndex].Name;
            if (colName == "delete" || colName == "Delete")
            {
                string itemName = dgvBrandList.Rows[e.RowIndex].Cells["name"].Value.ToString();

                var confirmResult = MessageBox.Show($"Remove {itemName} from cart?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    DataRowView drv = (DataRowView)dgvBrandList.Rows[e.RowIndex].DataBoundItem;
                    drv.Delete();

                    // Re-calculate your VAT, Vatable, and Sales Total
                    //UpdateTotals();
                }
            }

        }

        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblSalesTotal.Text);
            //double vat = sales * dbconn.GetVal();
            //double vatable = sales - vat;

            //lblVat.Text = vat.ToString("#,##0.00");
            //lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
        }

        private void DgvBrandList_SelectionChanged(object sender, EventArgs e)
        {
            int i = dgvBrandList.CurrentRow.Index;
            id = dgvBrandList[1, i].Value.ToString();
            price = dgvBrandList[4, i].Value.ToString();
        }

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
            frmLookUp lookUpFrm = new frmLookUp(this);
            lookUpFrm.LoadRecords();
            lookUpFrm.ShowDialog();
        }

        private void BtnAddDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount discountFrm = new frmDiscount(this);
            discountFrm.lblId.Text = id;
            discountFrm.txtPrice.Text = price;
            discountFrm.ShowDialog();
        }

        private void BtnSetPayment_Click(object sender, EventArgs e)
        {
            frmSettle setFrm = new frmSettle(this);
            //setFrm.txtSale.Text = lblDisplayTotal.Text;
            setFrm.ShowDialog();
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                cmd = new MySqlCommand("DELETE FROM tblCart WHERE transno LIKE '" + lblTransNo.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("All items has been successful removed", "Remove Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            GetTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        //public void insert
    }
}

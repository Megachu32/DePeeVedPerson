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

        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //when enter is pressed on the qty textbox
            if (e.KeyChar == 13 && txtQty.Text != string.Empty)
            {
                string id = "";
                string status = "lolme";
                bool found = false;

                //basically search the product returning status and it's stock.
                conn.Open();
                string sql = @"
                    SELECT 
                        p.product_id    AS id,
                        p.name          AS name,
	                    IFNULL(i.stock, 0)         AS stock,
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
                    WHERE p.sku = @sku
                ";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sku", pcode);
                dr = cmd.ExecuteReader();
                if (dr.Read()) { 
                    if (dr.HasRows)
                    {
                        id = dr["id"].ToString();
                        name = dr["name"].ToString();
                        status = dr["status"].ToString();
                        amount = Convert.ToInt32(dr["stock"]);
                        price = Convert.ToDouble(dr["price"]);
                        discount = Convert.ToInt32(dr["discount"]);
                    }                
                }
                
                dr.Close();
                conn.Close();
  
                //checkes for if the product is inactive, doesn't have the stock or is incoming
                if (status == "incoming")
                {
                    MessageBox.Show("this product is still incoming", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (qty < (int.Parse(txtQty.Text)) || status == "inactive")
                {
                    MessageBox.Show("this product is empty", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Dispose();
                    return;
                }

                int qtyToAdd = int.Parse(txtQty.Text);

                // calculate using UNIT price
                double linePrice = price;
                double discountRate = discount / 100.0;

                // get table
                DataSet1 ds = fPos.ds;
                DataTable dt = ds.Tables["dtCheckOut"];

                // try find existing row
                DataRow existingRow = dt.AsEnumerable()
                    .FirstOrDefault(r => r["product_id"].ToString() == id); // basically loops variable r.product_id match id

                //enable or disable buttons based on if data table or the dgv is null
                if (dt == null)
                {
                    fPos.btnClearCart.Enabled = false;
                    fPos.btnSetPayment.Enabled = false;
                }
                else
                {
                    fPos.btnClearCart.Enabled = true;
                    fPos.btnSetPayment.Enabled = true;
                }

                //if not null update
                if (existingRow != null)
                {
                    int oldQty = Convert.ToInt32(existingRow["qty"]);
                    int newQty = oldQty + qtyToAdd;

                    if(newQty > amount)
                    {
                        MessageBox.Show("Not enough stock available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Dispose();
                        return;
                    }

                    double subtotal = linePrice * newQty;
                    double discountAmount = subtotal * discountRate;

                    existingRow["qty"] = newQty;
                    existingRow["price"] = linePrice;   // update if price changed
                    existingRow["discount"] = discount;
                    existingRow["total"] = subtotal - discountAmount;

                }
                else
                {
                    // ADD new row
                    int qty = qtyToAdd;
                    double subtotal = linePrice * qty;
                    double discountAmount = subtotal * discountRate;

                    DataRow row = dt.NewRow();
                    row["product_id"] = id;
                    row["name"] = name;
                    row["qty"] = qty;
                    row["price"] = linePrice;
                    row["discount"] = discount;
                    row["total"] = subtotal - discountAmount;
                    if (status == "incoming")
                    {
                        row["order_mode"] = "pre-order";
                    }
                    else
                    {
                        row["order_mode"] = "normal";
                    }
                    dt.Rows.Add(row);
                }

                fPos.txtSearch.Clear();
                fPos.txtSearch.Focus();
                fPos.dgvBrandList.DataSource = dt;
                Dispose();
                
            }
        }
    }
}
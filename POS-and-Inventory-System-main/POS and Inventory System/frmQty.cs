using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
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
        private string transNo;
        private string status;
        public frmQty(frmPOS _fPos)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fPos = _fPos;
        }

        // just for the passing of data from look up form to qty form
        public void ProductDetails(string _pcode, double _price, string _transNo, int _qty, string _status)
        {
            pcode = _pcode;
            price = _price;
            transNo = _transNo;
            qty = _qty;
            status = _status;
        }

        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtQty.Text != string.Empty)
            {
                string id = "";
                int stock = 0;
                string status = "";
                bool found = false;

                //basically search the product returning status and it's stock.
                conn.Open();
                string sql = @"
                    SELECT 
	                    i.stock     AS stock,
                        p.status    AS status
                    FROM products AS p
                    JOIN inventory AS i ON i.product_id = p.product_id
                    WHERE p.sku = @sku
                ";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sku", pcode);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    status = dr["status"].ToString();
                    stock = int.Parse(dr["stock"].ToString());
                }
                else found = false;
                dr.Close();
                conn.Close();

                if (found)
                {
                    // checkes for if the product is inactive, doesn't have the stock or is incoming
                    if ((qty < (int.Parse(txtQty.Text)) || status == "inactive") && status != "incoming")
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Open();
                    cmd = new MySqlCommand("UPDATE tblCart SET qty=(qty +" + int.Parse(txtQty.Text) + ") WHERE id= '" + id +"'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    fPos.txtSearch.Clear();
                    fPos.txtSearch.Focus();
                    fPos.LoadCart();
                    Dispose();
                }
                //else
                //{
                //    if (qty < int.Parse(txtQty.Text))
                //    {
                //        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //    conn.Open();
                //    sql = "INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier) VALUES " +
                //        "(@transno, @pcode, @price, @qty, @sdate, @cashier)";
                //    cmd = new MySqlCommand(sql, conn);
                //    cmd.Parameters.AddWithValue("@transno", transNo);
                //    cmd.Parameters.AddWithValue("@pcode", pcode);
                //    cmd.Parameters.AddWithValue("@price", price);
                //    cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                //    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                //    cmd.Parameters.AddWithValue("@cashier", fPos.lblUser.Text);
                //    cmd.ExecuteNonQuery();
                //    conn.Close();

                //    fPos.txtSearch.Clear();
                //    fPos.txtSearch.Focus();
                //    fPos.LoadCart();
                //    Dispose();
                //}
            }
        }
    }
}
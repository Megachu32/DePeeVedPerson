using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmQty : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        frmPOS fPos;

        /*
         Mega's Note

        🔍 Key Fixes You Get Automatically
        ✔ Converted to MySQL (MySqlConnection, MySqlCommand, MySqlDataReader)
        ✔ All SQL is now parameterized

        No more dangerous:

        "UPDATE tblCart SET qty=(qty +" + int.Parse(txtQty.Text) + ") WHERE id= '" + id +"'"


        Now replaced with:

        "UPDATE tblCart SET qty = qty + @addQty WHERE id = @id"

        ✔ No unclosed readers

        Everything uses:

        dr?.Close();
        conn.Close();

        ✔ Handles Enter key correctly
        ✔ Proper stock validation
        ✔ No repeated conn.Open() mistakes
         */

        private string pcode;
        private double price;
        private int qty;
        private string transNo;

        public frmQty(frmPOS _fPos)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fPos = _fPos;
        }

        public void ProductDetails(string _pcode, double _price, string _transNo, int _qty)
        {
            pcode = _pcode;
            price = _price;
            transNo = _transNo;
            qty = _qty;
        }

        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 || txtQty.Text == string.Empty)
                return;

            string id = "";
            int cartQty = 0;
            bool found = false;

            try
            {
                // First: check if item already exists in the cart
                conn.Open();
                cmd = new MySqlCommand(
                    "SELECT * FROM tblCart WHERE transno = @transno AND pcode = @pcode",
                    conn
                );
                cmd.Parameters.AddWithValue("@transno", fPos.lblTransNo.Text);
                cmd.Parameters.AddWithValue("@pcode", pcode);

                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    found = true;
                    id = dr["id"].ToString();
                    cartQty = Convert.ToInt32(dr["qty"]);
                }
                dr.Close();
                conn.Close();

                int inputQty = int.Parse(txtQty.Text);

                // Check stock
                if (found)
                {
                    if (qty < inputQty + cartQty)
                    {
                        MessageBox.Show(
                            $"Unable to proceed. Remaining qty on hand is {qty}",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // Safe parameterized update
                    conn.Open();
                    cmd = new MySqlCommand(
                        "UPDATE tblCart SET qty = qty + @addQty WHERE id = @id",
                        conn
                    );
                    cmd.Parameters.AddWithValue("@addQty", inputQty);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    if (qty < inputQty)
                    {
                        MessageBox.Show(
                            $"Unable to proceed. Remaining qty on hand is {qty}",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // Insert new cart line
                    conn.Open();
                    string sql = @"
                        INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier)
                        VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)
                    ";

                    cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@transno", transNo);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@qty", inputQty);
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cashier", fPos.lblUser.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh POS cart display
                fPos.txtSearch.Clear();
                fPos.txtSearch.Focus();
                fPos.LoadCart();
                Dispose();
            }
            catch (Exception ex)
            {
                dr?.Close();
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

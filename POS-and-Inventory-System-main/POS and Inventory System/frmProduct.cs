using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmProduct : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        frmProductList fList;

        /*
         ⚡ Summary of Important Fixes
        🚫 SQL Injection fixed

        Original:

        WHERE brand LIKE '" + cboBrand.Text + "'


        New (safe):

        WHERE brand = @brand

        ✔ MySQL LIMIT 1 used

        Because MySQL doesn’t support MSSQL’s TOP.

        ✔ MySQL syntax & classes
        ✔ Safe dr.Close()
        ✔ Correct update/insert logic preserved
         */

        public frmProduct(frmProductList frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frm;
        }

        public void LoadCategory()
        {
            try
            {
                cboCategory.Items.Clear();
                conn.Open();

                string sql = "SELECT category FROM tblCategory";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                    cboCategory.Items.Add(dr["category"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dr?.Close();
                conn.Close();
            }
        }

        public void LoadBrand()
        {
            try
            {
                cboBrand.Items.Clear();
                conn.Open();

                string sql = "SELECT brand FROM tblBrand";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                    cboBrand.Items.Add(dr["brand"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dr?.Close();
                conn.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?",
                    "Save Product",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                string bid = "";
                string cid = "";

                // GET BRAND ID
                conn.Open();
                string sqlBrand = "SELECT id FROM tblBrand WHERE brand = @brand LIMIT 1";
                cmd = new MySqlCommand(sqlBrand, conn);
                cmd.Parameters.AddWithValue("@brand", cboBrand.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read()) bid = dr["id"].ToString();
                dr.Close();
                conn.Close();

                // GET CATEGORY ID
                conn.Open();
                string sqlCat = "SELECT id FROM tblCategory WHERE category = @cat LIMIT 1";
                cmd = new MySqlCommand(sqlCat, conn);
                cmd.Parameters.AddWithValue("@cat", cboCategory.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read()) cid = dr["id"].ToString();
                dr.Close();
                conn.Close();

                // INSERT PRODUCT
                conn.Open();
                string sqlInsert = @"
                    INSERT INTO tblProduct (pcode, barcode, pdesc, bid, cid, price, reorder)
                    VALUES (@pcode, @barcode, @pdesc, @bid, @cid, @price, @reorder)";
                cmd = new MySqlCommand(sqlInsert, conn);

                cmd.Parameters.AddWithValue("@pcode", txtPCode.Text);
                cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                cmd.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                cmd.Parameters.AddWithValue("@bid", bid);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReOrder.Text));

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Product has been successfully saved.",
                    "Product Saving",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Clear();
                fList.LoadRecords();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtPrice.Clear();
            txtDescription.Clear();
            txtPCode.Clear();
            txtBarcode.Clear();
            cboBrand.Text = "";
            cboCategory.Text = "";
            txtReOrder.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?",
                    "Update Product",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                string bid = "";
                string cid = "";

                // BRAND ID
                conn.Open();
                string sqlBrand = "SELECT id FROM tblBrand WHERE brand = @brand LIMIT 1";
                cmd = new MySqlCommand(sqlBrand, conn);
                cmd.Parameters.AddWithValue("@brand", cboBrand.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read()) bid = dr["id"].ToString();
                dr.Close();
                conn.Close();

                // CATEGORY ID
                conn.Open();
                string sqlCat = "SELECT id FROM tblCategory WHERE category = @cat LIMIT 1";
                cmd = new MySqlCommand(sqlCat, conn);
                cmd.Parameters.AddWithValue("@cat", cboCategory.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read()) cid = dr["id"].ToString();
                dr.Close();
                conn.Close();

                // UPDATE PRODUCT
                conn.Open();
                string sqlUpdate = @"
                    UPDATE tblProduct 
                    SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid,
                        price=@price, reorder=@reorder
                    WHERE pcode = @pcode";

                cmd = new MySqlCommand(sqlUpdate, conn);

                cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                cmd.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                cmd.Parameters.AddWithValue("@bid", bid);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReOrder.Text));
                cmd.Parameters.AddWithValue("@pcode", txtPCode.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Product has been successfully updated.",
                    "Product Update",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Clear();
                fList.LoadRecords();
                Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
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
            if ((e.KeyChar == '.') && !(sender as TextBox).Text.Contains(".")) return;
            if ((e.KeyChar == '.') && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) return;

            e.Handled = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

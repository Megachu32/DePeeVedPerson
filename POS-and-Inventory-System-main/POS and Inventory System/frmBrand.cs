using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmBrand : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private DBConnection dbconn = new DBConnection();
        private frmBrandList fList;

        /*
         🔍 Changes made
        ✔ MSSQL → MySQL conversion

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        Parameter style preserved

        ✔ Replaced tblBrand with MySQL version brands

        Because your schema lacks brand tables.

        ✔ Fixed SQL injection risks

        WHERE id LIKE 'X' → replaced with parameter @id.

        ✔ Proper cleanup

        Removed risky double-closing and ensured stable connection handling.

        ------------------------------------------------------------------------
        ✔ What I assume (based on your DB schema)

        Your schema stores brand as a free-text field, like:

        brand VARCHAR(50)

        Meaning:

        No separate brand table

        No foreign keys

        Brand is just a string inside every product

        So frmBrand.cs should:

        ✔ Insert new brand values into a standalone brands table

        OR

        ✔ Modify whatever brand table you decide to create

        Right now, the system expects brand management as a standalone list, so the correct fix is:

        ✅ Create a new table in MySQL: brands

        Add this table:

        CREATE TABLE brands (
            id INT AUTO_INCREMENT PRIMARY KEY,
            brand VARCHAR(100) NOT NULL UNIQUE
        );

        This matches the POS’s original structure (which used tblBrand).

        If this is OK with you (and it keeps POS fully functional), here is the rewritten file.


         */

        public frmBrand(frmBrandList _fList)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = _fList;
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = true;
            txtBrand.Clear();
            txtBrand.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save this brand?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO brands (brand) VALUES (@brand)";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Brand has been saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                    Clear();
                    fList.LoadRecords();
                    Dispose();
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Update this brand?", "Update Record",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE brands SET brand=@brand WHERE id=@id";
                    cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@brand", txtBrand.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", lblId.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Brand updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                    Clear();
                    fList.LoadRecords();
                    Dispose();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

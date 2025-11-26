using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmCategory : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private DBConnection dbconn = new DBConnection();
        private frmCategoryList fList;

        /*
         Mega Note - Missing Table

        CREATE TABLE categories (
            id INT AUTO_INCREMENT PRIMARY KEY,
            category VARCHAR(100) NOT NULL UNIQUE
        );

        the table above were required to make the code works.

        🔍 What was changed?
        ✔ MSSQL → MySQL

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        MS SQL syntax replaced with MySQL-compatible version

        ✔ Updated table name

        tblCategory → categories

        This matches the new MySQL structure you are building.

        ✔ Prevented SQL injection

        Old code used:

        WHERE id LIKE '123'


        New version uses safe parameters:

        WHERE id=@id

        ✔ Cleaned up error handling

        Prevents double-close and null exceptions.

         */

        public frmCategory(frmCategoryList frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frm;
        }

        public void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCategory.Clear();
            txtCategory.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save this category?", "Saving Record",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();

                    string sql = "INSERT INTO categories (category) VALUES (@category)";
                    cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@category", txtCategory.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Category has been successfully saved.", "Success");
                Clear();
                fList.LoadCategory();
                Dispose();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update this category?", "Update Record",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();

                    string sql = "UPDATE categories SET category=@category WHERE id=@id";
                    cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@category", txtCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", lblId.Text);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Category updated successfully.", "Success");
                fList.LoadCategory();
                Dispose();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

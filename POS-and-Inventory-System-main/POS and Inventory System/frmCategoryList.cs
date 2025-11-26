using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmCategoryList : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;
        private DBConnection dbconn = new DBConnection();

        public frmCategoryList()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadCategory();
        }

        /*
         Mega Note -

        Here is the clean, correct, fully MySQL-converted version of frmCategoryList.cs — matching the categories table you chose earlier and fully compatible with your MySQL-based POS rewrite.

        All MSSQL components are removed and replaced with:

        MySqlConnection

        MySqlCommand

        MySqlDataReader

        Safe parameter usage replaces insecure LIKE 'id' deletion.

        Null-safe handling prevents crashes when closing the reader.

        🔍 What was fixed and improved
        ✔ Entire file converted from MSSQL → MySQL

        No more SqlConnection or SQL Server logic.

        ✔ Table updated

        tblCategory → categories

        ✔ Safe deletion

        Old code (dangerous):

        DELETE FROM tblCategory WHERE id LIKE '5'


        New code (safe):

        DELETE FROM categories WHERE id=@id

        ✔ Null-safety

        Prevented the common crash:

        dr.Close() → NullReferenceException

        ✔ Cleaned up UI logic

        Now behaves consistently with the modernized forms we’ve converted.

         */
        public void LoadCategory()
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                conn.Open();

                string sql = "SELECT * FROM categories ORDER BY category";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(
                        i,
                        dr["id"].ToString(),
                        dr["category"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();

                conn.Close();
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            // ======================
            // EDIT CATEGORY
            // ======================
            if (colName == "Edit")
            {
                frmCategory frm = new frmCategory(this);
                frm.lblId.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;

                frm.ShowDialog();
            }

            // ======================
            // DELETE CATEGORY
            // ======================
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Delete this category?", "Delete Category",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();

                        string sql = "DELETE FROM categories WHERE id=@id";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id",
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()
                        );

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    finally
                    {
                        conn.Close();
                        MessageBox.Show("Category deleted successfully.",
                            "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadCategory();
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);
    }
}

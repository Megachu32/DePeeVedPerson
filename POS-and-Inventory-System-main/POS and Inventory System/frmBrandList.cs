using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmBrandList : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;
        private DBConnection dbconn = new DBConnection();

        /*
         this form required the changes from frmBrand.cs to work properly.
        make do with this information as you wish.

        🔧 Changes Made
        ✔ MSSQL removed

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        SqlDataReader → MySqlDataReader

        ✔ Updated table from tblBrand → brands

        Matches the table you approved earlier.

        ✔ Removed dangerous SQL LIKE deletion

        Old code:

        DELETE FROM tblBrand WHERE id LIKE '12'


        New safe version:

        DELETE FROM brands WHERE id=@id

        ✔ Prevented crashes

        Ensured:

        if (dr != null && !dr.IsClosed)
            dr.Close();


        So your app won’t crash when dr is null.

        ✔ Compatible with the MySQL version of frmBrand.cs
         */
        public frmBrandList()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            try
            {
                int i = 0;
                dgvBrandList.Rows.Clear();
                conn.Open();

                string sql = "SELECT * FROM brands ORDER BY brand";
                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dgvBrandList.Rows.Add(
                        i,
                        dr["id"].ToString(),
                        dr["brand"].ToString()
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

        private void DgvBrandList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvBrandList.Columns[e.ColumnIndex].Name;

            // ======================
            // EDIT BRAND
            // ======================
            if (colName == "Edit")
            {
                frmBrand frm = new frmBrand(this);
                frm.lblId.Text = dgvBrandList[1, e.RowIndex].Value.ToString();
                frm.txtBrand.Text = dgvBrandList[2, e.RowIndex].Value.ToString();
                frm.ShowDialog();
            }

            // ======================
            // DELETE BRAND
            // ======================
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Delete this brand?", "Delete Record",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();

                        string sql = "DELETE FROM brands WHERE id=@id";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", dgvBrandList[1, e.RowIndex].Value.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    finally
                    {
                        conn.Close();
                        MessageBox.Show("Brand has been successfully deleted.",
                            "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecords();
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand(this);
            frm.ShowDialog();
        }
    }
}

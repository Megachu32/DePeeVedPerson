using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmPreorderList : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public frmPreorderList()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Tables["dtPreoder"];

            int i = 0;
            conn.Open();
            string sql = @"
                SELECT 
                    pro.name AS NAME,
                    p.status AS STATUS,
                    p.money_hold_amount AS money_hold,
                    p.final_charge_amount AS final_charge,
                    p.reserved_for_pickup_until AS reserved_time,
                    c.name AS customer_name,
                    p.pickup_code AS pickup_code,
                    p.quantity AS qty
                FROM 
                    Preorders AS p
                JOIN 
                    Customers AS c ON p.customer_id = c.customer_id
                JOIN
                    products AS pro ON pro.product_id = p.product_id
                WHERE 
                    p.status = 'order_placed';
            ";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);

            da.Fill(dt);
            dgvStaffList.DataSource = dt;
            conn.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
            => txtSearch.Clear();

        private void TxtSearch_TextChanged(object sender, EventArgs e) 
            => LoadRecords();

        private void DgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvStaffList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //frmStaffEdit frm = new frmStaffEdit(this);
                //frm.btnSave.Enabled = false;
                //frm.btnUpdate.Enabled = true;
                //frm.txtName.Text = dgvStaffList.Rows[e.RowIndex].Cells[0].Value.ToString();

                //frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record", "Delete Record",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = sql = @"
                        START TRANSACTION;

                        SELECT * FROM staff WHERE staff_id = @id FOR UPDATE;

                        DELETE FROM staff WHERE staff_id = @id;

                        COMMIT;
                    ";

                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", dgvStaffList.Rows[e.RowIndex].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadRecords();
                    MessageBox.Show("Staff has been removed", "Removed Staff", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //frmStaffEdit frm = new frmStaffEdit(this);
            ////frm.btnSave.Enabled = true;
            ////frm.btnUpdate.Enabled = false;
            //frm.LoadCategory();
            //frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printRaport_Click(object sender, EventArgs e)
        {

        }
    }
}
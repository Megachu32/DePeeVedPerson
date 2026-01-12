using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmStaff : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public frmStaff()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Tables["dtStaff"];

            int i = 0;
            conn.Open();
            string sql = @"
                SELECT *
                FROM staff

                ORDER BY staff_id ASC
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
                frmStaffEdit frm = new frmStaffEdit(this);
                //frm.btnSave.Enabled = false;
                //frm.btnUpdate.Enabled = true;
                frm.txtName.Text = dgvStaffList.Rows[e.RowIndex].Cells[0].Value.ToString();

                frm.ShowDialog();
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
            frmStaffEdit frm = new frmStaffEdit(this);
            //frm.btnSave.Enabled = true;
            //frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printRaport_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Open Connection
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. The Query (Selects ALL staff first)
                string sql = @"
            SELECT 
                staff_id, 
                name, 
                email, 
                phone, 
                username, 
                role, 
                hire_date, 
                status 
            FROM staff 
            ORDER BY name ASC";

                // 3. Prepare Adapter & Dataset
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // 4. Fill the table "dtStaff"
                da.Fill(ds, "dtStaff");

                if (ds.Tables["dtStaff"].Rows.Count == 0)
                {
                    MessageBox.Show("No staff records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 5. Load the Report
                CRStaff report = new CRStaff();
                report.SetDataSource(ds);

                // --- NEW STEP: Pass the Parameter ---
                // We force it to show "active" staff. 
                // If you want to see inactive ones, change this string to "inactive".
                report.SetParameterValue("ActiveCheck", "active");

                // 6. Show the Report
                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Staff Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmStaffEdit : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmStaff fList;
        int _staffID;

        public frmStaffEdit(frmStaff frms)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frms;
            LoadCategory();
        }

        public void LoadCategory() // calls in other place outside this form use to fill combobox
        {
            cmbRole.Items.Add("cashier");
            cmbRole.Items.Add("admin");
            cmbRole.Items.Add("manager");

            cmbStatus.Items.Add("active");
            cmbStatus.Items.Add("inactive");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Save Product",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();

                    string sql2 = @"
                        INSERT INTO staff (name, email, phone, username, role, hire_date, status) 
                        VALUES (@name, @email, @phone, @username, @role, @hire_date, @status)
                    ";
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@hire_date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Staff has been success saved.", "Staff Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    fList.LoadRecords();


                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtName.Clear();
            cmbRole.Text = "";
            cmbStatus.Items.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            dateTimePicker1 = new DateTimePicker();
            txtUsername.Clear();
            cmbStatus.Text = "";
            cmbStatus.Items.Clear();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Staff?", "Save Staff", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    string sql2 = @"
                        INSERT INTO staff (staff_id, name, email, phone, username, role, hire_date, status) 
                        VALUES (@id, @name, @email, @phone, @username, @role, @hire_date, @status)
                        ON DUPLICATE KEY UPDATE
                            name = @name,
                            email = @email,
                            phone = @phone,
                            username = @username,
                            role = @role,
                            hire_date = @hire_date,
                            status = @status;
                    ";

                    cmd = new MySqlCommand(sql2, conn);
                    cmd.Parameters.AddWithValue("@id", _staffID);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@hire_date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully updated.", "Product Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    fList.LoadRecords();
                    Dispose();
                }
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
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains('.'.ToString()) == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;
            e.Handled = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            if (int.TryParse(txtName.Text, out _)) fillData(); // if the sku is filled, fill the data for updating
        }

        public void fillData() //call when updating to fill the fields
        {
            _staffID = Convert.ToInt32(txtName.Text);

            try
            {
                conn.Open();
                string sql = @"
                    SELECT name, email, phone, username, role, hire_date, status
                    FROM staff
                    WHERE staff_id = @id
                    LIMIT 1;
                ";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", _staffID);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtName.Text = dr["name"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtPhone.Text = dr["phone"].ToString();
                    txtUsername.Text = dr["username"].ToString();
                    cmbStatus.Text = dr["status"].ToString();
                    cmbRole.Text = dr["role"].ToString();

                    // safe date handling
                    if (DateTime.TryParse(dr["hire_date"].ToString(), out DateTime date))
                        dateTimePicker1.Value = date;
                    else
                        dateTimePicker1.Value = DateTime.Today;
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmCustomerEdit : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        MySqlDataReader dr;
        frmSettle fList;
        int _staffID;

        public frmCustomerEdit(frmSettle frms)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frms;
            LoadCategory();
        }

        public void LoadCategory() // calls in other place outside this form use to fill combobox
        {
            //cmbRole.Items.Add("cashier");
            //cmbRole.Items.Add("admin");
            //cmbRole.Items.Add("manager");

            //cmbStatus.Items.Add("active");
            //cmbStatus.Items.Add("inactive");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure the contact is correct?", "Save Contact",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    conn.Open();

                    // 1. The SQL Query with placeholders (@)
                    string sql = @"UPDATE customers 
                       SET 
                           name = @name, 
                           email = @email, 
                           phone = @phone, 
                           government_id = @govId 
                       WHERE customer_id = @id";

                    // 2. Create the Command (The 'cmd')
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // 3. Fill the placeholders with real data from your TextBoxes
                        // (Replace 'txtName', 'txtEmail' with the actual names of your textboxes)
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@govId", txtGovId.Text);

                        // For the ID, you likely have it saved in a variable or a label
                        cmd.Parameters.AddWithValue("@id", fList.realCustomerId);

                        // 4. EXECUTE the query
                        // ExecuteNonQuery is used for UPDATE, INSERT, and DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Customer ID not found.");
                        }
                    }

                    conn.Close();

                    Dispose();

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
            //cmbRole.Text = "";
            //cmbStatus.Items.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtGovId.Clear();
            //dateTimePicker1 = new DateTimePicker();
            //txtUsername.Clear();
            //cmbStatus.Text = "";
            //cmbStatus.Items.Clear();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
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
            //if (int.TryParse(txtName.Text, out _)) fillData(); // if the sku is filled, fill the data for updating
        }

        public void fillData() //call when updating to fill the fields
        {
            //_staffID = Convert.ToInt32(txtName.Text);

            //try
            //{
            //    conn.Open();
            //    string sql = @"
            //        SELECT name, email, phone, username, role, hire_date, status
            //        FROM staff
            //        WHERE staff_id = @id
            //        LIMIT 1;
            //    ";
            //    cmd = new MySqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("@id", _staffID);
            //    dr = cmd.ExecuteReader();

            //    if (dr.Read())
            //    {
            //        txtName.Text = dr["name"].ToString();
            //        txtEmail.Text = dr["email"].ToString();
            //        txtPhone.Text = dr["phone"].ToString();
            //        //txtUsername.Text = dr["username"].ToString();
            //        //cmbStatus.Text = dr["status"].ToString();
            //        //cmbRole.Text = dr["role"].ToString();

            //        // safe date handling
            //        //if (DateTime.TryParse(dr["hire_date"].ToString(), out DateTime date))
            //            //dateTimePicker1.Value = date;
            //        //else
            //            //dateTimePicker1.Value = DateTime.Today;
            //    }

            //    dr.Close();
            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    conn.Close();
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
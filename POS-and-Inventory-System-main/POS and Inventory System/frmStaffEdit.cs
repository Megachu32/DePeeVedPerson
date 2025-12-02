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
        frmProductList fList;

        public frmStaffEdit(frmProductList frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            fList = frm;

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
                        INSERT INTO staff (name, email, phone, username, password, hire_date, status) 
                        VALUES (@name, @email, @phone, @username, @password, @hire_date, @status)
                    ";
                    cmd = new MySqlCommand(sql2, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    //cmd.Parameters.AddWithValue("@type", cmbType.Text);
                    //cmd.Parameters.AddWithValue("@model", txtModel.Text);
                    //cmd.Parameters.AddWithValue("@generation", Convert.ToInt16(txtGeneration.Text));
                    //cmd.Parameters.AddWithValue("@release_date", dateTimePicker1.Value.Date);
                    //cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    //cmd.Parameters.AddWithValue("@color", txtColor.Text);
                    //cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    //cmd.Parameters.AddWithValue("@specifications", txtSpecific.Text);
                    //cmd.Parameters.AddWithValue("@status", "active");
                    //cmd.Parameters.AddWithValue("@description", txtDescription.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been success saved.", "Product Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //txtPCode.Clear();
            //txtName.Clear();
            //cmbType.Text = "";
            //txtModel.Clear();
            //txtGeneration.Clear();
            //dateTimePicker1.Value = DateTime.Now;
            //txtPrice.Clear();
            //txtColor.Clear();
            //txtStorage.Clear();
            //txtSpecific.Clear();
            //txtDescription.Clear();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Save Product", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";
                    conn.Open();
                    //string sql = "SELECT id FROM tblBrand WHERE brand LIKE '" + cboBrand.Text + "'";
                    //cmd = new SqlCommand(sql, conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) bid = dr[0].ToString();
                    dr.Close();
                    conn.Close();

                    conn.Open();
                    //string sql1 = "SELECT id FROM tblCategory WHERE category LIKE '" + cboCategory.Text + "'";
                    //cmd = new SqlCommand(sql1, conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) cid = dr[0].ToString();
                    dr.Close();
                    conn.Close();

                    conn.Open();
                    string sql2 = @"
                        INSERT INTO products (sku, name, type, model, generation, release_date, price, color, storage, specifications, status, description) 
                        VALUES (@sku, @name, @type, @model, @generation, @release_date, @price, @color, @storage, @specifications, @status, @description)
                        
                        ON DUPLICATE KEY UPDATE
                        name = @name,
                        type = @type,
                        model = @model,
                        generation = @generation,
                        release_date = @release_date,
                        price = @price,
                        color = @color,
                        storage = @storage,
                        specifications = @specifications,
                        status = @status,
                        description = @description;
                    ";
                    cmd = new MySqlCommand(sql2, conn);
                    //cmd.Parameters.AddWithValue("@sku", txtPCode.Text);
                    //cmd.Parameters.AddWithValue("@name", txtName.Text);
                    //cmd.Parameters.AddWithValue("@type", cmbType.Text);
                    //cmd.Parameters.AddWithValue("@model", txtModel.Text);
                    //cmd.Parameters.AddWithValue("@generation", Convert.ToInt16(txtGeneration.Text));
                    //cmd.Parameters.AddWithValue("@release_date", dateTimePicker1.Value.Date);
                    //cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                    //cmd.Parameters.AddWithValue("@color", txtColor.Text);
                    //cmd.Parameters.AddWithValue("@storage", txtStorage.Text);
                    //cmd.Parameters.AddWithValue("@specifications", txtSpecific.Text);
                    //cmd.Parameters.AddWithValue("@status", "active");
                    //cmd.Parameters.AddWithValue("@description", txtDescription.Text);
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
            //if (txtPCode.Text.ToString() != "") fillData(); // if the sku is filled, fill the data for updating
        }

        public void fillData() //call when updating to fill the fields
        {
            try
            {
                conn.Open();
                string sql = @"
                    SELECT sku, name, type, model, generation, release_date, price, color, storage, specifications, status, description
                    FROM products
                    WHERE sku = @sku
                    LIMIT 1;
                ";
                cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@sku", txtPCode.Text);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    //txtPCode.Text = dr["sku"].ToString();
                    //txtName.Text = dr["name"].ToString();
                    //cmbType.Text = dr["type"].ToString();
                    //txtModel.Text = dr["model"].ToString();
                    //txtGeneration.Text = dr["generation"].ToString();
                    //txtPrice.Text = dr["price"].ToString();
                    //txtColor.Text = dr["color"].ToString();
                    //txtStorage.Text = dr["storage"].ToString();
                    //txtSpecific.Text = dr["specifications"].ToString();
                    //txtDescription.Text = dr["description"].ToString();

                    // safe date handling
                    if (DateTime.TryParse(dr["release_date"].ToString(), out DateTime date))
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
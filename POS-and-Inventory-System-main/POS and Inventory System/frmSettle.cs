using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmSettle : Form
    {
        frmPOS fpos;
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();

        /*
         mega's note

        ✅ What You Need in MySQL for This File

        This form only touches two tables:

        tblProduct

        Needs at least:

        pcode

        qty (int)

        tblCart

        Needs at least:

        id

        pcode

        status

        No views involved.
        No datetime conversion issues.
        No complex joins.

        This one is clean.
         */
        public frmSettle(frmPOS _fpos)
        {
            InitializeComponent();
            fpos = _fpos;
            conn = new MySqlConnection(dbconn.MyConnection());
            KeyPreview = true;
        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            if (txtCash.Text == "") return;

            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text);
                double change = cash - sale;

                txtChange.Text = change.ToString("#,##0.00");
            }
            catch
            {
                txtChange.Text = "0.00";
            }
        }

        private void Btn7_Click(object sender, EventArgs e) => txtCash.Text += btn7.Text;
        private void Btn8_Click(object sender, EventArgs e) => txtCash.Text += btn8.Text;
        private void Btn9_Click(object sender, EventArgs e) => txtCash.Text += btn9.Text;
        private void BtnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void Btn4_Click(object sender, EventArgs e) => txtCash.Text += btn4.Text;
        private void Btn5_Click(object sender, EventArgs e) => txtCash.Text += btn5.Text;
        private void Btn6_Click(object sender, EventArgs e) => txtCash.Text += btn6.Text;
        private void Btn0_Click(object sender, EventArgs e) => txtCash.Text += btn0.Text;
        private void Btn1_Click(object sender, EventArgs e) => txtCash.Text += btn1.Text;
        private void Btn2_Click(object sender, EventArgs e) => txtCash.Text += btn2.Text;
        private void Btn3_Click(object sender, EventArgs e) => txtCash.Text += btn3.Text;
        private void Btn00_Click(object sender, EventArgs e) => txtCash.Text += btn00.Text;

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChange.Text == "" || double.Parse(txtChange.Text) < 0)
                {
                    MessageBox.Show("Insufficient Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // update stock + mark as sold
                for (int i = 0; i < fpos.dgvBrandList.Rows.Count; i++)
                {
                    string pcode = fpos.dgvBrandList.Rows[i].Cells[2].Value.ToString();
                    int qty = int.Parse(fpos.dgvBrandList.Rows[i].Cells[5].Value.ToString());
                    string id = fpos.dgvBrandList.Rows[i].Cells[1].Value.ToString();

                    // update product qty
                    conn.Open();
                    string updateProduct = "UPDATE tblProduct SET qty = qty - @qty WHERE pcode = @pcode";
                    cmd = new MySqlCommand(updateProduct, conn);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // update cart status
                    conn.Open();
                    string updateCart = "UPDATE tblCart SET status='Sold' WHERE id=@id";
                    cmd = new MySqlCommand(updateCart, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // show receipt
                frmReceipt frm = new frmReceipt(fpos);
                frm.LoadReport(txtCash.Text, txtChange.Text);
                frm.ShowDialog();

                MessageBox.Show("Payment Successfully Saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fpos.LoadCart();
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
            else if (e.KeyCode == Keys.Enter)
                BtnEnter_Click(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

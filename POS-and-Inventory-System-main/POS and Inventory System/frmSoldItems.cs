using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmSoldItems : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        public string sUser;

        /*
         Mega's Note

        there might be some missing code because it was originally commented. but the code that required sql should still be here.

        ✔️ This version is fully MySQL-compatible

        Uses MySqlConnection, MySqlCommand, MySqlDataReader

        Fixes date formatting for MySQL

        Keeps your commented code intact but correctly converted

        Leaves UI logic untouched

        Safe for dropping into your MySQL-based POS project
         */

        public frmSoldItems()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
        }

        // ================================
        // COMMENTED ORIGINAL MSSQL CODE  
        // Converted to MySQL but still commented because original was commented
        // ================================

        /*
        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            string sql;
            dgvSoldItems.Rows.Clear();
            conn.Open();

            string dateFrom = dtFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string dateTo = dtTo.Value.ToString("yyyy-MM-dd HH:mm:ss");

            if (cboCashier.Text == "All Cashier")
                sql = "SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total " +
                      "FROM tblCart c INNER JOIN tblProduct p ON c.pcode=p.pcode " +
                      "WHERE status='Sold' AND sdate BETWEEN '" + dateFrom + "' AND '" + dateTo + "'";
            else
                sql = "SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total " +
                      "FROM tblCart c INNER JOIN tblProduct p ON c.pcode=p.pcode " +
                      "WHERE status='Sold' AND cashier='" + cboCashier.Text +
                      "' AND sdate BETWEEN '" + dateFrom + "' AND '" + dateTo + "'";

            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                _total += double.Parse(dr["total"].ToString());
                dgvSoldItems.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(),
                    dr["pcode"].ToString(), dr["pdesc"].ToString(),
                    dr["price"].ToString(), dr["qty"].ToString(),
                    dr["disc"].ToString(), dr["total"].ToString());
            }

            dr.Close();
            conn.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }
        */

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void CboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /*
        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");

            conn.Open();
            string sql = "SELECT username FROM tblUser WHERE role='Cashier'";
            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString());
            }

            dr.Close();
            conn.Close();
        }
        */

        private void DgvSoldItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSoldItems.Columns[e.ColumnIndex].Name;
            if (colName == "Cancel")
            {
                frmCancelDetails frm = new frmCancelDetails(this);
                frm.txtId.Text = dgvSoldItems.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtTransNo.Text = dgvSoldItems.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPCode.Text = dgvSoldItems.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtDescription.Text = dgvSoldItems.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtPrice.Text = dgvSoldItems.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtQty.Text = dgvSoldItems.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtDiscount.Text = dgvSoldItems.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.txtTotal.Text = dgvSoldItems.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.txtCancel.Text = sUser;

                frm.ShowDialog();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

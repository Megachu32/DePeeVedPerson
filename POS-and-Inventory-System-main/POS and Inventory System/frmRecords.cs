using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmRecords : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();

        /*
        Mega's Note - MANY MANY MISSING TABLE
        
        since the data will be stored in a table that will be used in other form, use the following sql codes as a reference.

        CREATE VIEW vwSoldItems AS
        SELECT
            c.pcode,
            p.pdesc,
            c.qty,
            c.total,
            c.sdate,
            c.status
        FROM tblCart c
        JOIN tblProduct p ON p.pcode = c.pcode;

        CREATE VIEW vwCancelledOrder AS
        SELECT *
        FROM tblCancelledOrder;  -- if you have a cancelled order table

        CREATE VIEW vwCriticalItems AS
        SELECT 
            p.pcode,
            p.barcode,
            p.pdesc,
            b.brand,
            c.category,
            p.price,
            p.reorder,
            p.qty
        FROM tblProduct p
        JOIN tblBrand b ON p.bid=b.id
        JOIN tblCategory c ON p.cid=c.id
        WHERE p.qty <= p.reorder;

        CREATE VIEW vwStockIn AS
        SELECT *
        FROM tblStockIn;   -- assuming you later add this table



         */

        public frmRecords()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            LoadCriticalItems();
            LoadInventory();
            CancelledOrders();
            LoadStockInHistory();
        }

        // ==============================
        // TOP SELLING RECORDS
        // ==============================
        public void LoadRecords()
        {
            int i = 0;
            string sql;

            dgvTopSell.Rows.Clear();
            conn.Open();

            if (cboTopSelect.Text == "SORT BY QTY")
            {
                sql = "SELECT pcode, pdesc, COALESCE(SUM(qty),0) AS qty, COALESCE(SUM(total),0) AS total " +
                      "FROM vwSoldItems " +
                      "WHERE DATE(sdate) BETWEEN @d1 AND @d2 AND status='Sold' " +
                      "GROUP BY pcode, pdesc ORDER BY qty DESC LIMIT 10";
            }
            else
            {
                sql = "SELECT pcode, pdesc, COALESCE(SUM(qty),0) AS qty, COALESCE(SUM(total),0) AS total " +
                      "FROM vwSoldItems " +
                      "WHERE DATE(sdate) BETWEEN @d1 AND @d2 AND status='Sold' " +
                      "GROUP BY pcode, pdesc ORDER BY total DESC LIMIT 10";
            }

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@d1", dtFrom.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@d2", dtTo.Value.ToString("yyyy-MM-dd"));

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvTopSell.Rows.Add(
                    i,
                    dr["pcode"].ToString(),
                    dr["pdesc"].ToString(),
                    dr["qty"].ToString(),
                    Convert.ToDouble(dr["total"]).ToString("#,##0.0")
                );
            }

            dr.Close();
            conn.Close();
        }

        // ==============================
        // CANCELLED ORDERS
        // ==============================
        public void CancelledOrders()
        {
            int i = 0;
            dgvCancelledRecords.Rows.Clear();
            conn.Open();

            string sql =
                "SELECT * FROM vwCancelledOrder " +
                "WHERE sdate BETWEEN @d1 AND @d2";

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@d1", dtFrom5.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@d2", dtTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCancelledRecords.Rows.Add(
                    i,
                    dr["transno"].ToString(),
                    dr["pcode"].ToString(),
                    dr["pdesc"].ToString(),
                    dr["price"].ToString(),
                    dr["qty"].ToString(),
                    dr["total"].ToString(),
                    dr["sdate"].ToString(),
                    dr["voidBy"].ToString(),
                    dr["cancelledby"].ToString(),
                    dr["reason"].ToString(),
                    dr["action"].ToString()
                );
            }
            dr.Close();
            conn.Close();
        }

        // ==============================
        // INVENTORY LIST
        // ==============================
        public void LoadInventory()
        {
            int i = 0;
            dgvInventory.Rows.Clear();
            conn.Open();

            string sql =
                "SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty, p.reorder " +
                "FROM tblProduct p " +
                "JOIN tblBrand b ON p.bid = b.id " +
                "JOIN tblCategory c ON p.cid = c.id";

            cmd = new MySqlCommand(sql, conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                i++;
                dgvInventory.Rows.Add(
                    i,
                    dr["pcode"].ToString(),
                    dr["barcode"].ToString(),
                    dr["pdesc"].ToString(),
                    dr["brand"].ToString(),
                    dr["category"].ToString(),
                    dr["price"].ToString(),
                    dr["reorder"].ToString(),
                    dr["qty"].ToString()
                );
            }

            dr.Close();
            conn.Close();
        }

        // ==============================
        // CRITICAL STOCKS
        // ==============================
        public void LoadCriticalItems()
        {
            try
            {
                dgvCriticalStocks.Rows.Clear();
                int i = 0;

                conn.Open();
                cmd = new MySqlCommand("SELECT * FROM vwCriticalItems", conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dgvCriticalStocks.Rows.Add(
                        i,
                        dr[0].ToString(),
                        dr[1].ToString(),
                        dr[2].ToString(),
                        dr[3].ToString(),
                        dr[4].ToString(),
                        dr[5].ToString(),
                        dr[6].ToString(),
                        dr[7].ToString()
                    );
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

        // ==============================
        // STOCK-IN HISTORY
        // ==============================
        private void LoadStockInHistory()
        {
            int i = 0;
            dgvStocks2.Rows.Clear();
            conn.Open();

            string sql =
                "SELECT * FROM vwStockIn " +
                "WHERE DATE(sdate) BETWEEN @d1 AND @d2 AND status='Done'";

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@d1", dtFrom6.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@d2", dtTo6.Value.ToString("yyyy-MM-dd"));

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvStocks2.Rows.Add(
                    i,
                    dr[0].ToString(),
                    dr[1].ToString(),
                    dr[2].ToString(),
                    dr[3].ToString(),
                    dr[4].ToString(),
                    Convert.ToDateTime(dr[5]).ToShortDateString(),
                    dr[6].ToString()
                );
            }

            dr.Close();
            conn.Close();
        }

        // ==============================
        // CHARTS
        // ==============================
        public void LoadChartTopSelling()
        {
            conn.Open();

            string sql;
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                sql = "SELECT pcode, COALESCE(SUM(qty),0) AS qty " +
                      "FROM vwSoldItems WHERE DATE(sdate) BETWEEN @d1 AND @d2 " +
                      "AND status='Sold' GROUP BY pcode ORDER BY qty DESC LIMIT 10";
            }
            else
            {
                sql = "SELECT pcode, COALESCE(SUM(total),0) AS total " +
                      "FROM vwSoldItems WHERE DATE(sdate) BETWEEN @d1 AND @d2 " +
                      "AND status='Sold' GROUP BY pcode ORDER BY total DESC LIMIT 10";
            }

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@d1", dtFrom.Value.ToString("yyyy-MM-dd"));
            da.SelectCommand.Parameters.AddWithValue("@d2", dtTo.Value.ToString("yyyy-MM-dd"));

            DataSet ds = new DataSet();
            da.Fill(ds, "TOPSELLING");

            chart1.DataSource = ds.Tables["TOPSELLING"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;
            series.IsValueShownAsLabel = true;

            if (cboTopSelect.Text == "SORT BY QTY")
            {
                series.XValueMember = "pcode";
                series.YValueMembers = "qty";
                series.LabelFormat = "(#,##0)";
            }
            else
            {
                series.XValueMember = "pcode";
                series.YValueMembers = "total";
                series.LabelFormat = "(#,##0.00)";
            }

            conn.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Util.CloseForm(this);
    }
}

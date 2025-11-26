using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_and_Inventory_System
{
    public partial class frmChart : Form
    {
        private MySqlConnection conn;
        private DBConnection dbconn = new DBConnection();

        /*
         Mega Note

        nothing.

        🔍 What Was Changed
        ✔ MSSQL → MySQL Conversion

        SqlConnection → MySqlConnection

        SqlDataAdapter → MySqlDataAdapter

        Removed all SQL Server namespaces.

        ✔ Safe connection handling

        Wrapped in try/catch/finally.

        Guaranteed conn.Close() even on error.

        ✔ Chart logic untouched

        Your chart uses:

        pdesc as X axis

        total as Y axis

        These must exist in your SQL query, e.g.:

        SELECT pdesc, SUM(qty) AS total FROM sales GROUP BY pdesc;
         */

        public frmChart()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();

        public void LoadChartSold(string sql)
        {
            try
            {
                conn.Open();

                // MySQL adapter instead of SqlDataAdapter
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "SOLD");
                chart1.DataSource = ds.Tables["SOLD"];

                Series series = chart1.Series[0];
                series.ChartType = SeriesChartType.Doughnut;
                series.Name = "SOLD ITEMS";

                chart1.Series[0].XValueMember = "pdesc";
                chart1.Series[0].YValueMembers = "total";

                chart1.Series[0].LabelFormat = "(#,##0.00)";
                chart1.Series[0].IsValueShownAsLabel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

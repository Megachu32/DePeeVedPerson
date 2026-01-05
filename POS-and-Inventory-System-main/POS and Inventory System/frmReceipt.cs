using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmReceipt : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DBConnection dbconn = new DBConnection();
        string store = "Walter Ville";
        string address = "nowhere lmao fuck off";
        frmPOS frm;
        public frmReceipt(frmPOS _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
            KeyPreview = true;
        }

        private void FrmReceipt_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
        }

        public void LoadReport(string pcash, string pchange)
        {
            ReportDataSource rptDataSource;
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                conn.Open();

                string sql = @"
                    SELECT 
                        st.name AS staff_name,
                        c.name AS customer_name,
                        s.customer_ref AS customer_ref,
                        /* Generates Invoice No: YYYYMMDD + 4-digit padded Customer ID */
                        CONCAT(DATE_FORMAT(s.sale_date, '%Y%m%d'), LPAD(s.customer_id, 4, '0')) AS invoice_no,
                        DATE(s.sale_date) AS DATE,
                        TIME(s.sale_date) AS TIME,
                        str.store_name,
                        str.store_address AS store_location,
                        str.company_name,
                        str.company_location,
                        str.customer_service_phone AS cs_number,
                        s.payment_method,
                        s.purchase_type,
                        s.order_mode,
                        s.store_id,
                        s.total AS total_amount,
                        /* Total count of distinct items in this specific sale */
                        (SELECT SUM(quantity) FROM sale_items WHERE sale_id = s.sale_id) AS total_items
                    FROM sales s
                    JOIN customers c ON s.customer_id = c.customer_id
                    LEFT JOIN stores str ON s.store_id = str.store_id
                    /* Note: You may need to add a staff_id column to your sales table to link the cashier */
                    LEFT JOIN staff st ON st.role = 'cashier' AND st.status = 'active' 
                    ORDER BY s.sale_id DESC 
                    LIMIT 1;
                ";

                //string invoiceSql = "/* The first query above */";
                //MySqlDataAdapter daInvoice = new MySqlDataAdapter(sql, conn);
                //DataTable dtInvoice = ds.Tables["dtInvoice"];
                //daInvoice.Fill(dtInvoice);

                // 1. Create a temporary table to hold the results of the first query
                DataTable dtTempHeader = new DataTable();
                MySqlDataAdapter daInvoice = new MySqlDataAdapter(sql, conn);
                daInvoice.Fill(dtTempHeader);

                if (dtTempHeader.Rows.Count > 0)
                {
                    // 2. Get the sale_id from the temp table (it doesn't matter if it's not in dtInvoice)
                    int lastId = Convert.ToInt32(dtTempHeader.Rows[0]["sale_id"]);

                    // 3. Manually import the row into your official dtInvoice
                    // This will only copy columns that EXIST in your dtInvoice schema
                    DataTable dtInvoice = ds.Tables["dtInvoice"];
                    dtInvoice.ImportRow(dtTempHeader.Rows[0]);

                    // 4. Now use lastId to get the items for dtCheckOut
                    string checkOutSql = @"
                        SELECT 
                            si.product_id,
                            p.name,
                            si.quantity AS qty,
                            si.unit_price AS price,
                            si.discount_amount AS discount,
                            (si.quantity * si.unit_price) - si.discount_amount AS total,
                            si.sale_id,
                            s.store_id,
                            s.order_mode
                        FROM sale_items si
                        JOIN products p ON si.product_id = p.product_id
                        JOIN sales s ON si.sale_id = s.sale_id
                        WHERE si.sale_id = @lastSaleId";

                    MySqlCommand cmdItems = new MySqlCommand(checkOutSql, conn);
                    cmdItems.Parameters.AddWithValue("@lastSaleId", lastId);

                    MySqlDataAdapter daItems = new MySqlDataAdapter(cmdItems);
                    DataTable dtCheckOut = ds.Tables["dtCheckOut"];
                    daItems.Fill(dtCheckOut);
                }

                //da.SelectCommand = new MySqlCommand(sql, conn);
                //da.Fill(ds.Tables["dtSold"]);
                conn.Close();

                //sql = "SELECT c.id, c.transno, c.pcode, c.price, c.qty, c.disc, c.total, c.sdate, c.status, p.pdesc FROM tblCart AS c " +
                //    "INNER JOIN tblProduct as p on p.pcode = c.pcode WHERE transno like '" + frm.lblTransNo.Text + "'";
                //da.SelectCommand = new MySqlCommand(sql, conn);
                //da.Fill(ds.Tables["dtSold"]);
                //conn.Close();

                ReportParameter pVatable = new ReportParameter("pVatable", "pVatable");
                ReportParameter pVat = new ReportParameter("pVat", "pVat");
                ReportParameter pDiscount = new ReportParameter("pDiscount", "pDiscount");
                ReportParameter pTotal = new ReportParameter("pTotal", "pTotal");
                ReportParameter pCash = new ReportParameter("pCash", "pCash");
                ReportParameter pChange = new ReportParameter("pChange", "pChange");
                ReportParameter pStore = new ReportParameter("pStore", "pStore");
                ReportParameter pAddress = new ReportParameter("pAddress", "pAddress");
                ReportParameter pTransaction = new ReportParameter("pTransaction", "Invoice #: " + frm.lblTransNo.Text);
                ReportParameter pCashier = new ReportParameter("pCashier", "pCashier");

                reportViewer1.LocalReport.SetParameters(pVatable);
                reportViewer1.LocalReport.SetParameters(pVat);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);


                rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

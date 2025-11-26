using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;

namespace POS_and_Inventory_System
{
    public partial class frmReportSold : Form
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DBConnection dbconn = new DBConnection();
        frmSoldItems frm;

        /*
         mega's note

        ✅ NOTES ABOUT THE CONVERSION
        1. Date format

        Using:

        frm.dtFrom.Value.ToString("yyyy-MM-dd")


        Because MySQL handles dates more consistently in ISO format.

        2. SQL Server functions removed

        You're not using anything like ISNULL() or TOP here, so nothing to rewrite.

        3. Views NOT required

        This code uses only:

        tblCart

        tblProduct

        No vwSoldItems, vwCriticalItems, etc.

        So no extra view creation needed.
         */

        public frmReportSold(frmSoldItems _frm)
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            frm = _frm;
        }

        public void LoadReport()
        {
            try
            {
                ReportDataSource rptDS;
                string sql = "";
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                conn.Open();

                if (frm.cboCashier.Text == "All Cashier")
                {
                    sql = "SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc AS discount, c.total " +
                          "FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode " +
                          "WHERE c.status = 'Sold' AND c.sdate BETWEEN '" +
                          frm.dtFrom.Value.ToString("yyyy-MM-dd") + "' AND '" +
                          frm.dtTo.Value.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    sql = "SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc AS discount, c.total " +
                          "FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode " +
                          "WHERE c.status = 'Sold' AND c.sdate BETWEEN '" +
                          frm.dtFrom.Value.ToString("yyyy-MM-dd") + "' AND '" +
                          frm.dtTo.Value.ToString("yyyy-MM-dd") + "' AND c.cashier = '" +
                          frm.cboCashier.Text + "'";
                }

                da.SelectCommand = new MySqlCommand(sql, conn);
                da.Fill(ds.Tables["dtSoldReport"]);
                conn.Close();

                ReportParameter pDate = new ReportParameter(
                    "pDate",
                    "Date From: " + frm.dtFrom.Value.ToShortDateString() +
                    " To: " + frm.dtTo.Value.ToShortDateString()
                );

                ReportParameter pCashier = new ReportParameter(
                    "pCashier",
                    "Cashier: " + frm.cboCashier.Text
                );

                ReportParameter pHeader = new ReportParameter(
                    "pHeader",
                    "SALES REPORT"
                );

                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);

                rptDS = new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

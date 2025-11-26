using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmInventoryReport : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private DBConnection dbconn = new DBConnection();

        /*
         Mega Note

        ✔ Notes About the Report System

        RDLC report files don’t change; only your data source changes.

        MySQL works fine with ReportViewer as long as you use MySqlDataAdapter.

        The dataset (DataSet1.xsd) must have the same table names as used in the code:

        dtSoldItems

        dtTopSelling

        dtInventory

        dtStockIn

        dtCancelled

        If the dataset names don’t match exactly, the report will show blank pages — nothing else.
         */

        public frmInventoryReport()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
        }

        private void FrmInventoryReport_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
        }

        // ===============================
        //      SOLD ITEMS REPORT
        // ===============================
        public void LoadSoldItems(string sql, string param)
        {
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptSold.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                conn.Open();
                da.SelectCommand = new MySqlCommand(sql, conn);
                da.Fill(ds.Tables["dtSoldItems"]);
                conn.Close();

                reportViewer1.LocalReport.SetParameters(new ReportParameter("pDate", param));

                var rptDS = new ReportDataSource("DataSet1", ds.Tables["dtSoldItems"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ===============================
        //      TOP SELLING REPORT
        // ===============================
        public void LoadTopSelling(string sql, string param, string header)
        {
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptTop.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                conn.Open();
                da.SelectCommand = new MySqlCommand(sql, conn);
                da.Fill(ds.Tables["dtTopSelling"]);
                conn.Close();

                reportViewer1.LocalReport.SetParameters(new ReportParameter("pDate", param));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("pHeader", header));

                var rptDS = new ReportDataSource("DataSet1", ds.Tables["dtTopSelling"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ===============================
        //      INVENTORY REPORT
        // ===============================
        public void LoadReport()
        {
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report3.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                string sql = @"SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, 
                                      p.price, p.qty, p.reorder
                               FROM tblProduct p
                               INNER JOIN tblBrand b ON p.bid = b.id
                               INNER JOIN tblCategory c ON p.cid = c.id";

                conn.Open();
                da.SelectCommand = new MySqlCommand(sql, conn);
                da.Fill(ds.Tables["dtInventory"]);
                conn.Close();

                var rptDS = new ReportDataSource("DataSet1", ds.Tables["dtInventory"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ===============================
        //      STOCK-IN REPORT
        // ===============================
        public void LoadStockInReport(string sql, string param)
        {
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptStockIn.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();

                reportViewer1.LocalReport.SetParameters(new ReportParameter("pDate", param));

                conn.Open();
                da.SelectCommand = ne

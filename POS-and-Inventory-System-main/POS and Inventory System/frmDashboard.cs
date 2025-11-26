using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Tulpep.NotificationWindow;
using POS_and_Inventory_System.DAL;

namespace POS_and_Inventory_System
{
    public partial class frmDashboard : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        DBConnection dbconn = new DBConnection();
        DashboardDAL dDal = new DashboardDAL();

        public string _pass, _user;

        /*
         Mega Note


        🔍 Key Fixes & Improvements
        ✔ MSSQL → MySQL conversion

        SqlConnection → MySqlConnection

        SqlCommand → MySqlCommand

        SqlDataReader → MySqlDataReader

        Removed SQL Server namespaces

        ✔ More stable notification loading

        Prevented crashes if data reader is null

        Ensured safe cleanup in finally

        ✔ Kept your UI logic intact

        Nothing visual was changed—only data access.
         */

        public frmDashboard()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());

            NotifyCriticalItems();

            // dashboard summary numbers
            lblDailySales.Text = dbconn.DailySales().ToString("#,##0.00");
            lblProduct.Text = dbconn.ProductLine().ToString("#,##0");
            lblStockOnHand.Text = dbconn.StockOnHand().ToString("#,##0");
            lblCritical.Text = dbconn.CriticalItems().ToString("#,##0");

            dDal.LoadDashboard(chart1);
        }

        public void NotifyCriticalItems()
        {
            string critical = "";
            string count = "0";

            try
            {
                conn.Open();

                // get count
                cmd = new MySqlCommand("SELECT COUNT(*) FROM vwCriticalItems", conn);
                count = cmd.ExecuteScalar()?.ToString() ?? "0";

                // get details
                cmd = new MySqlCommand("SELECT * FROM vwCriticalItems", conn);
                dr = cmd.ExecuteReader();

                int i = 0;
                while (dr.Read()

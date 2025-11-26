using MySql.Data.MySqlClient;
using System;

namespace POS_and_Inventory_System
{
    class DBConnection
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        private double dailySales;
        private int productLine;
        private int stockOnHand;
        private int critical;

        // CHANGE THIS ONE VALUE TO CHANGE THE DATABASE
        private string connString = "Server=localhost;Database=db_project;Uid=root;Pwd=;";

        public string MyConnection()
        {
            return connString;
        }

        // ----------------------------
        // DAILY SALES
        // ----------------------------
        public double DailySales()
        {
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");

            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                string sql = "SELECT IFNULL(SUM(total), 0) AS total " +
                             "FROM tblCart WHERE sdate = @date AND status = 'Sold'";

                using (cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@date", sdate);
                    dailySales = Convert.ToDouble(cmd.ExecuteScalar());
                }
            }

            return dailySales;
        }

        // ----------------------------
        // TOTAL PRODUCT LINES
        // ----------------------------
        public int ProductLine()
        {
            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                using (cmd = new MySqlCommand("SELECT COUNT(*) FROM tblProduct", conn))
                {
                    productLine = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return productLine;
        }

        // ----------------------------
        // STOCK ON HAND
        // ----------------------------
        public int StockOnHand()
        {
            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                using (cmd = new MySqlCommand("SELECT IFNULL(SUM(qty), 0) FROM tblProduct", conn))
                {
                    stockOnHand = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return stockOnHand;
        }

        // ----------------------------
        // CRITICAL ITEMS
        // ----------------------------
        public int CriticalItems()
        {
            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                using (cmd = new MySqlCommand("SELECT COUNT(*) FROM vwCriticalItems", conn))
                {
                    critical = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return critical;
        }

        // ----------------------------
        // VAT VALUE
        // ----------------------------
        public double GetVal()
        {
            double vat = 0;

            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                using (cmd = new MySqlCommand("SELECT vat FROM tblVat LIMIT 1", conn))
                {
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        vat = Convert.ToDouble(dr["vat"]);
                    }
                    dr.Close();
                }
            }

            return vat;
        }

        // ----------------------------
        // GET PASSWORD BY USERNAME
        // ----------------------------
        public string GetPassword(string user)
        {
            string password = "";

            using (conn = new MySqlConnection(MyConnection()))
            {
                conn.Open();

                string sql = "SELECT password FROM tblUser WHERE username = @username LIMIT 1";

                using (cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user);

                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        password = dr["password"].ToString();
                    }
                    dr.Close();
                }
            }

            return password;
        }
    }
}

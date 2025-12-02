using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    class DBConnection
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;

        private double dailySales;
        private int productLine;
        private int stockOnHand;
        private int critical;
        string connString;
        public string MyConnection()
        {
            //string conn = @"datasource = localhost; username = root; password = ; database = pos_inventory_db";
            connString = @"Server=localhost; Database=db_project; User Id= root; Password= ;";
            return connString;
        }

        public double DailySales()
        {
            //string sdate = DateTime.Now.ToShortDateString();
            DateTime dateAtMorning = DateTime.Today;
            DateTime dateAtNight = DateTime.Today.AddDays(1).AddSeconds(-1);

            conn = new MySqlConnection(MyConnection());
            conn.Open();
            //MessageBox.Show(dateAtNight);
            string sql = "SELECT sum(total) AS total FROM sales WHERE sale_date BETWEEN @morning AND @night";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@morning", MySqlDbType.DateTime).Value = dateAtMorning;
            cmd.Parameters.Add("@night", MySqlDbType.DateTime).Value = dateAtNight;

            dailySales = Convert.ToDouble(cmd.ExecuteScalar());
            conn.Close();
            return dailySales;
        }

        //idk what productline is for
        public int ProductLine()
        {
            conn = new MySqlConnection(MyConnection());
            conn.Open();
            cmd = new MySqlCommand("SELECT count(*) FROM tblProduct", conn);
            productLine = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return productLine;
        }

        //total of all stock in inventory
        public int StockOnHand()
        {
            conn = new MySqlConnection(MyConnection());
            conn.Open();
            cmd = new MySqlCommand("SELECT sum(stock) AS qty FROM inventory", conn);
            stockOnHand = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            conn.Close();
            return stockOnHand;
        }

        //devices like iphone, ipad, mac not accessory items
        public int CriticalItems()
        {
            conn = new MySqlConnection(MyConnection());
            conn.Open();
            cmd = new MySqlCommand("SELECT count(*) FROM products WHERE type IN ('iphone', 'ipad', 'mac')", conn);
            critical = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return critical;
        }

        //public double GetVal()
        //{
        //    double vat = 0;
        //    conn = new SqlConnection(MyConnection());
        //    //conn.ConnectionString = MyConnection();
        //    conn.Open();
        //    string sql = "SELECT * FROM tblVat";
        //    cmd = new MySqlCommand(sql, conn);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        vat = double.Parse(dr["vat"].ToString());
        //    }
        //    dr.Close();
        //    conn.Close();
        //    return vat;
        //}

        //public string GetPassword(string user)
        //{
        //    string password = "";
        //    conn = new SqlConnection(MyConnection());
        //    //conn.ConnectionString = MyConnection();
        //    conn.Open();
        //    string sql = "SELECT * FROM tblUser WHERE username=@username";
        //    cmd = new SqlCommand(sql, conn);
        //    cmd.Parameters.AddWithValue("@username", user);
        //    dr = cmd.ExecuteReader();
        //    dr.Read();
        //    if (dr.HasRows)
        //    {
        //        password = dr["password"].ToString();
        //    }
        //    dr.Close();
        //    conn.Close();
        //    return password;
        //}
    }
}

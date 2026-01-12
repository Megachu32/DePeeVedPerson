using MySql.Data.MySqlClient;
using POS_and_Inventory_System.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace POS_and_Inventory_System
{
    public partial class frmDashboard : Form
    {
        //SqlConnection conn;
        //SqlCommand cmd;
        //SqlDataReader dr;

        DataSet1 ds = new DataSet1();
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        DBConnection dbconn = new DBConnection();
        public string _pass, _user;
        DashboardDAL dDal = new DashboardDAL();

        public frmDashboard()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.MyConnection());
            //NotifyCriticalItems();

            //lblDailySales.Text = dbconn.DailySales().ToString("#,##0.00");
            //lblProduct.Text = dbconn.ProductLine().ToString("#,##0");
            lblStockOnHand.Text = dbconn.StockOnHand().ToString("#,##0");
            //lblCritical.Text = dbconn.CriticalItems().ToString("#,##0");
        }

        //notification we don't use delete later if needed
        //public void NotifyCriticalItems()
        //{
        //    string critical = "";
        //    conn.Open();
        //    //cmd = new SqlCommand("SELECT count(*) FROM vwCriticalItems", conn);
        //    string count = cmd.ExecuteScalar().ToString();
        //    conn.Close();

        //    int i = 0;
        //    conn.Open();
        //    //cmd = new SqlCommand("SELECT * FROM vwCriticalItems", conn);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        i++;
        //        critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine;
        //    }
        //    dr.Close();
        //    conn.Close();

        //    PopupNotifier popup = new PopupNotifier();
        //    popup.Image = Properties.Resources.error;
        //    popup.TitleText = count + "Critical Item(s)";
        //    popup.ContentText = critical;
        //    popup.Popup();
        //}

        private void BtnBrand_Click(object sender, EventArgs e) 
            => Util.ShowFormInPanel(new frmBrandList(), pnlMain);

        private void BtnCategory_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmCategoryList(), pnlMain);


        private void BtnStockIn_Click(object sender, EventArgs e) 
            => Util.ShowFormInPanel(new frmPreorderList(), pnlMain);
        private void BtnRecords_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            pnlMain.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void BtnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog();
        }

        private void BtnStore_Click(object sender, EventArgs e)
        {
            frmStore frm = new frmStore();
            frm.LoadRecords();
            frm.ShowDialog();
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount(this);
            frm.ShowDialog();
            frm.txtUser2.Text = _user;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
        }


        private void btnProduct_Click(object sender, EventArgs e)
           => Util.ShowFormInPanel(new frmProductList(), pnlMain);

        //private void BtnVendor_Click(object sender, EventArgs e)
        //    => Util.ShowFormInPanel(new frmVendorList(), pnlMain);

        private void btnDashboard_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmDashboard(), pnlMain);

        private void BtnStaff_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmStaff(), pnlMain);

        private void lblDailySales_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdjust_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmDiscounted(), pnlMain);       

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printRaport_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Updated Query: Uses SUM(quantity) for true popularity
                string sql = @"
            SELECT 
                si.product_id, 
                p.name, 
                COALESCE(SUM(si.quantity), 0) AS frequency
            FROM sale_items si
            JOIN products p ON p.product_id = si.product_id
            GROUP BY si.product_id, p.name
            ORDER BY frequency DESC
            LIMIT 10";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // Fills the table "dtPopItem" exactly as named in your DataSet
                da.Fill(ds, "dtPopItem");

                if (ds.Tables["dtPopItem"].Rows.Count == 0)
                {
                    MessageBox.Show("No data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CRPopularItem report = new CRPopularItem();
                report.SetDataSource(ds);

                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        //private void btnProduct_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // 1. Open Connection
        //        if (conn.State == ConnectionState.Closed) conn.Open();

        //        // 2. The Query (Selects ALL staff first)
        //        string sql = @"
        //            select
        //            p.product_id as id,
        //            p.sku as sku, 
        //            p.name as name,
        //            p.type as type,
        //            p.model as model,
        //            p.status as status,
        //            p.price as price,
        //            i.stock as stock
        //            from products as p
        //            join inventory as i on i.product_id = p.product_id
        //            order by type";

        //        // 3. Prepare Adapter & Dataset
        //        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
        //        DataSet1 ds = new DataSet1();

        //        // 4. Fill the table "dtStaff"
        //        da.Fill(ds, "dtProducts");

        //        if (ds.Tables["dtProducts"].Rows.Count == 0)
        //        {
        //            MessageBox.Show("No staff records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }

        //        // 5. Load the Report
        //        CrystalReport2 report = new CrystalReport2();
        //        report.SetDataSource(ds);

        //        // --- NEW STEP: Pass the Parameter ---
        //        // We force it to show "active" staff. 
        //        // If you want to see inactive ones, change this string to "inactive".
        //        //report.SetParameterValue("Type", "active");

        //        // 6. Show the Report
        //        frmReportViewer viewer = new frmReportViewer();
        //        viewer.crystalReportViewer1.ReportSource = report;
        //        viewer.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Staff Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open) conn.Close();
        //    }
        //}

        private void btnStaff_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1. Open Connection
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. The Query (Selects ALL staff first)
                string sql = @"
            SELECT 
                staff_id, 
                name, 
                email, 
                phone, 
                username, 
                role, 
                hire_date, 
                status 
            FROM staff 
            ORDER BY name ASC";

                // 3. Prepare Adapter & Dataset
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // 4. Fill the table "dtStaff"
                da.Fill(ds, "dtStaff");

                if (ds.Tables["dtStaff"].Rows.Count == 0)
                {
                    MessageBox.Show("No staff records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 5. Load the Report
                CRStaff report = new CRStaff();
                report.SetDataSource(ds);

                // --- NEW STEP: Pass the Parameter ---
                // We force it to show "active" staff. 
                // If you want to see inactive ones, change this string to "inactive".
                report.SetParameterValue("ActiveCheck", "active");

                // 6. Show the Report
                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Staff Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void btnStockIn_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1. Open Connection
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. The Query (Exactly as you provided)
                string sql = @"
            SELECT
                po.customer_id AS 'customer_id',
                p.name AS 'name',
                po.reserved_for_pickup_until AS 'date'
            FROM preorders po
            JOIN products p ON p.product_id = po.product_id
            WHERE po.status = 'order_placed'";

                // 3. Prepare Adapter & Dataset
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // 4. Fill the specific table
                // IMPORTANT: Must match "dtPreorderItems" from your screenshot exactly!
                da.Fill(ds, "dtPreorderItems");

                // 5. Check if we have records
                if (ds.Tables["dtPreorderItems"].Rows.Count == 0)
                {
                    MessageBox.Show("No active preorders found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 6. Load the Report
                // Based on your file list, the report file is CRPreorderItem.rpt
                CRPreorderItem report = new CRPreorderItem();
                report.SetDataSource(ds);

                // 7. Show the Report
                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Preorder Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void btnAdjust_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1. Open Connection
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. The Query (Exactly as you designed it)
                // I added 'WHERE d.status = 1' or similar if you only want active discounts, 
                // but for now we stick to your exact logic.
                string sql = @"
            SELECT 
                d.discount_id,
                p.name,
                p.price,
                d.discount_percentage
            FROM discounts d
            JOIN products p ON p.product_id = d.product_id";

                // 3. Prepare the Adapter & Dataset
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet1 ds = new DataSet1();

                // 4. Fill the specific table "dtDiscount"
                // (Must match the name in your Dataset schema screenshot)
                da.Fill(ds, "dtDiscount");

                // 5. Check if data exists
                if (ds.Tables["dtDiscount"].Rows.Count == 0)
                {
                    MessageBox.Show("No discounted items found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 6. Load the Report
                // Since we renamed it back to CRDiscount.rpt, the class is CRDiscount
                CRDiscount report = new CRDiscount();
                report.SetDataSource(ds);

                // 7. Show the Report
                frmReportViewer viewer = new frmReportViewer();
                viewer.crystalReportViewer1.ReportSource = report;
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Always close connection
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void loadDataGridView()
        {
            try
            {
                conn.Open();
                string sql = @"
                    SELECT
                        p.name,
                        SUM(s.quantity) AS total_sold,
                        SUM(sa.total) AS money_generated
                    FROM
                        products AS p
                    JOIN
                        sale_items AS s ON s.product_id = p.product_id
                    JOIN sales AS sa ON sa.sale_id = s.sale_id
                    GROUP BY
                        p.product_id
                    ORDER BY
                        total_sold DESC
                    LIMIT 5;
                ";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds.dtDashboard1);
                dataGridView1.DataSource = ds.dtDashboard1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

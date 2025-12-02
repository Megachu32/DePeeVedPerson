using MySql.Data.MySqlClient;
using POS_and_Inventory_System.DAL;
using System;
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
            => Util.ShowFormInPanel(new frmStockIn(), pnlMain);
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


        private void BtnProduct_Click(object sender, EventArgs e)
           => Util.ShowFormInPanel(new frmProductList(), pnlMain);

        //private void BtnVendor_Click(object sender, EventArgs e)
        //    => Util.ShowFormInPanel(new frmVendorList(), pnlMain);

        private void btnDashboard_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmDashboard(), pnlMain);

        private void BtnStaff_Click(object sender, EventArgs e)
            => Util.ShowFormInPanel(new frmStaff(), pnlMain);

        private void BtnAdjust_Click(object sender, EventArgs e)
        {
            frmAdjustment frm = new frmAdjustment(this);
            frm.txtUser.Text = lblName.Text;
            frm.ShowDialog();
        }
    }
}

using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace POS_and_Inventory_System
{
    public partial class frmReceipt : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        DBConnection dbconn = new DBConnection();

        string store = "Walter Ville";
        string address = "nowhere lmao fuck off";

        frmPOS frm;

        /*
         Mega's Note

        yes, it actually say Fuck off.

        the AI did not tell us to fuck off, but it's already a part of the code.

        🔥 Changes Worth Noting
        1. Sanitized the query

        Old:

        WHERE transno like '" + frm.lblTransNo.Text + "'"


        New:

        WHERE c.transno = @transno

        2. Switched everything to MySQL equivalents

        MySqlConnection

        MySqlCommand

        MySqlDataAdapter

        3. Proper parameter list

        Less ugly repeating SetParameters() 10 times → now one clean array.

        4. Connection safety

        Less chances of leaked connections.
         */

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
            try
            {
                reportViewer1.LocalReport.ReportPath =
                    Application.StartupPath + @"\Reports\Report1.rdlc";

                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();

                string sql = @"
                    SELECT 
                        c.id, c.transno, c.pcode, c.price, c.qty, 
                        c.disc, c.total, c.sdate, c.status,
                        p.pdesc
                    FROM tblCart AS c
                    INNER JOIN tblProduct AS p ON p.pcode = c.pcode
                    WHERE c.transno = @transno
                ";

                using (MySqlDataAdapter da = new MySqlDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@transno", frm.lblTransNo.Text);

                    conn.Open();
                    da.Fill(ds.Tables["dtSold"]);
                    conn.Close();
                }

                // Report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("pVatable", frm.lblVatable.Text),
                    new ReportParameter("pVat", frm.lblVat.Text),
                    new ReportParameter("pDiscount", frm.lblDiscount.Text),
                    new ReportParameter("pTotal", frm.lblSalesTotal.Text),
                    new ReportParameter("pCash", pcash),
                    new ReportParameter("pChange", pchange),
                    new ReportParameter("pStore", store),
                    new ReportParameter("pAddress", address),
                    new ReportParameter("pTransaction", "Invoice #: " + frm.lblTransNo.Text),
                    new ReportParameter("pCashier", frm.lblUser.Text)
                };

                reportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource rptDataSource =
                    new ReportDataSource("DataSet1", ds.Tables["dtSold"]);

                reportViewer1.LocalReport.DataSources.Add(rptDataSource);

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 30;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => Dispose();
    }
}

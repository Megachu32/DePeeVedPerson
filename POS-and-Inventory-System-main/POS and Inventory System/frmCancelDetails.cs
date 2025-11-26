using System;
using System.Windows.Forms;

namespace POS_and_Inventory_System
{
    public partial class frmCancelDetails : Form
    {
        frmSoldItems frm;
        public frmCancelDetails(frmSoldItems _frm)
        {
            InitializeComponent();
            frm = _frm;
        }

        /*
         Mega Note

        this code doesn't have any database operations hence doesn't need a change of database (MSSQL to MySQL).
         */

        private void CboAction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboAction.Text != string.Empty && txtQty.Text != string.Empty && txtReason.Text != string.Empty)
                {
                    if (int.Parse(txtQty.Text) >= int.Parse(txtCancelQty.Text))
                    {
                        frmVoid frm = new frmVoid(this);
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RefreshList()
        {
            //frm.LoadRecord();
        }

        private void BtnClose_Click(object sender, EventArgs e) 
            => Dispose();

    }
}

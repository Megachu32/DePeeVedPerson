namespace POS_and_Inventory_System
{
    partial class frmCancelDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCancelDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtVoid = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCancel = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCancelQty = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.guna2ButtonCancle = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(228)))), ((int)(((byte)(221)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1312, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "- C A N C E L  O R D E R  D E T A I L S -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(29, 311);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID";
            // 
            // txtId
            // 
            this.txtId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtId.Location = new System.Drawing.Point(256, 108);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(275, 33);
            this.txtId.TabIndex = 0;
            this.txtId.Text = "  ";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(655, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "TRANSACTION NO.";
            // 
            // txtTransNo
            // 
            this.txtTransNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTransNo.Location = new System.Drawing.Point(898, 106);
            this.txtTransNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtTransNo.Name = "txtTransNo";
            this.txtTransNo.ReadOnly = true;
            this.txtTransNo.Size = new System.Drawing.Size(275, 33);
            this.txtTransNo.TabIndex = 3;
            this.txtTransNo.Text = "  ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(29, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 29);
            this.label4.TabIndex = 5;
            this.label4.Text = "SOLD ITEM";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(29, 157);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "PRODUCT CODE";
            // 
            // txtPCode
            // 
            this.txtPCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPCode.Location = new System.Drawing.Point(256, 160);
            this.txtPCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtPCode.Name = "txtPCode";
            this.txtPCode.ReadOnly = true;
            this.txtPCode.Size = new System.Drawing.Size(275, 33);
            this.txtPCode.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(29, 211);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "DESCRIPTION";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDescription.Location = new System.Drawing.Point(256, 202);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(275, 50);
            this.txtDescription.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(793, 157);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 29);
            this.label7.TabIndex = 5;
            this.label7.Text = "PRICE";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(777, 242);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 29);
            this.label8.TabIndex = 5;
            this.label8.Text = "TOTAL";
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPrice.Location = new System.Drawing.Point(898, 146);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(275, 33);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTotal.Location = new System.Drawing.Point(518, 535);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(275, 33);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(597, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(281, 29);
            this.label9.TabIndex = 5;
            this.label9.Text = "QUANTITY / DISCOUNT";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(29, 360);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 29);
            this.label10.TabIndex = 5;
            this.label10.Text = "VOID BY";
            // 
            // txtQty
            // 
            this.txtQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQty.Location = new System.Drawing.Point(931, 198);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(125, 33);
            this.txtQty.TabIndex = 5;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVoid
            // 
            this.txtVoid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtVoid.Location = new System.Drawing.Point(256, 270);
            this.txtVoid.Margin = new System.Windows.Forms.Padding(4);
            this.txtVoid.Name = "txtVoid";
            this.txtVoid.ReadOnly = true;
            this.txtVoid.Size = new System.Drawing.Size(275, 33);
            this.txtVoid.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(29, 271);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(205, 29);
            this.label11.TabIndex = 5;
            this.label11.Text = "CANCEL ITEM(S)";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(29, 411);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(193, 29);
            this.label12.TabIndex = 5;
            this.label12.Text = "CANCELLED BY";
            // 
            // txtCancel
            // 
            this.txtCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCancel.Location = new System.Drawing.Point(256, 311);
            this.txtCancel.Margin = new System.Windows.Forms.Padding(4);
            this.txtCancel.Name = "txtCancel";
            this.txtCancel.ReadOnly = true;
            this.txtCancel.Size = new System.Drawing.Size(275, 33);
            this.txtCancel.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(29, 467);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(262, 29);
            this.label13.TabIndex = 5;
            this.label13.Text = "ADD TO INVENTORY?";
            // 
            // cboAction
            // 
            this.cboAction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboAction.FormattingEnabled = true;
            this.cboAction.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cboAction.Location = new System.Drawing.Point(256, 357);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(275, 34);
            this.cboAction.TabIndex = 10;
            this.cboAction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboAction_KeyPress);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(723, 315);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 29);
            this.label14.TabIndex = 5;
            this.label14.Text = "REASON(S)";
            // 
            // txtReason
            // 
            this.txtReason.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtReason.Location = new System.Drawing.Point(898, 320);
            this.txtReason.Margin = new System.Windows.Forms.Padding(4);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(275, 55);
            this.txtReason.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(631, 285);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(238, 29);
            this.label15.TabIndex = 5;
            this.label15.Text = "CANCEL QUANTITY";
            // 
            // txtCancelQty
            // 
            this.txtCancelQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCancelQty.Location = new System.Drawing.Point(898, 279);
            this.txtCancelQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtCancelQty.Name = "txtCancelQty";
            this.txtCancelQty.Size = new System.Drawing.Size(275, 33);
            this.txtCancelQty.TabIndex = 11;
            this.txtCancelQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDiscount.Location = new System.Drawing.Point(931, 238);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.ReadOnly = true;
            this.txtDiscount.Size = new System.Drawing.Size(125, 33);
            this.txtDiscount.TabIndex = 6;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1268, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // guna2ButtonCancle
            // 
            this.guna2ButtonCancle.BackColor = System.Drawing.Color.Transparent;
            this.guna2ButtonCancle.BorderRadius = 10;
            this.guna2ButtonCancle.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonCancle.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonCancle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2ButtonCancle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2ButtonCancle.FillColor = System.Drawing.Color.Black;
            this.guna2ButtonCancle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonCancle.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonCancle.Location = new System.Drawing.Point(951, 411);
            this.guna2ButtonCancle.Name = "guna2ButtonCancle";
            this.guna2ButtonCancle.Size = new System.Drawing.Size(105, 45);
            this.guna2ButtonCancle.TabIndex = 18;
            this.guna2ButtonCancle.Text = "Cancel";
            // 
            // frmCancelDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1312, 732);
            this.ControlBox = false;
            this.Controls.Add(this.guna2ButtonCancle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboAction);
            this.Controls.Add(this.txtCancel);
            this.Controls.Add(this.txtVoid);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtTransNo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtCancelQty);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtPCode);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCancelDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox txtId;
        public System.Windows.Forms.TextBox txtTransNo;
        public System.Windows.Forms.TextBox txtPCode;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtPrice;
        public System.Windows.Forms.TextBox txtTotal;
        public System.Windows.Forms.TextBox txtQty;
        public System.Windows.Forms.TextBox txtDiscount;
        public System.Windows.Forms.TextBox txtVoid;
        public System.Windows.Forms.TextBox txtCancel;
        public System.Windows.Forms.ComboBox cboAction;
        public System.Windows.Forms.TextBox txtReason;
        public System.Windows.Forms.TextBox txtCancelQty;
        private System.Windows.Forms.Button btnClose;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonCancle;
    }
}
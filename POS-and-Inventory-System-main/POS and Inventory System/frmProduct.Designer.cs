namespace POS_and_Inventory_System
{
    partial class frmProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProduct));
            this.txtPCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtGeneration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPublishDate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtStorage = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSpecific = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.guna2ButtonSave = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ButtonUpd = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // txtPCode
            // 
            this.txtPCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPCode.Location = new System.Drawing.Point(225, 36);
            this.txtPCode.Name = "txtPCode";
            this.txtPCode.Size = new System.Drawing.Size(375, 34);
            this.txtPCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "PRODUCT CODE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "MODEL";
            // 
            // txtModel
            // 
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModel.Location = new System.Drawing.Point(225, 159);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(375, 34);
            this.txtModel.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtName.Location = new System.Drawing.Point(225, 76);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(375, 34);
            this.txtName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 29);
            this.label7.TabIndex = 12;
            this.label7.Text = "NAME";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(228)))), ((int)(((byte)(221)))));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 34);
            this.label1.TabIndex = 14;
            this.label1.Text = "- PRODUCT MODULE -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnClose.Location = new System.Drawing.Point(576, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 25);
            this.btnClose.TabIndex = 15;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // txtGeneration
            // 
            this.txtGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtGeneration.Location = new System.Drawing.Point(225, 199);
            this.txtGeneration.Name = "txtGeneration";
            this.txtGeneration.Size = new System.Drawing.Size(375, 34);
            this.txtGeneration.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(2, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 29);
            this.label9.TabIndex = 17;
            this.label9.Text = "GENERASI";
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(225, 116);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(375, 37);
            this.cmbType.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(2, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 29);
            this.label10.TabIndex = 19;
            this.label10.Text = "TYPE";
            // 
            // txtPublishDate
            // 
            this.txtPublishDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPublishDate.Location = new System.Drawing.Point(225, 239);
            this.txtPublishDate.Name = "txtPublishDate";
            this.txtPublishDate.Size = new System.Drawing.Size(375, 34);
            this.txtPublishDate.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(184, 29);
            this.label11.TabIndex = 21;
            this.label11.Text = "PUBLISH DATE";
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPrice.Location = new System.Drawing.Point(225, 280);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(375, 34);
            this.txtPrice.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(2, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 29);
            this.label12.TabIndex = 23;
            this.label12.Text = "PRICE";
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtColor.Location = new System.Drawing.Point(225, 321);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(375, 34);
            this.txtColor.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(2, 321);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 29);
            this.label13.TabIndex = 25;
            this.label13.Text = "COLOR";
            // 
            // txtStorage
            // 
            this.txtStorage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtStorage.Location = new System.Drawing.Point(225, 364);
            this.txtStorage.Name = "txtStorage";
            this.txtStorage.Size = new System.Drawing.Size(375, 34);
            this.txtStorage.TabIndex = 26;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(2, 364);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(130, 29);
            this.label14.TabIndex = 27;
            this.label14.Text = "STORAGE";
            // 
            // txtSpecific
            // 
            this.txtSpecific.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSpecific.Location = new System.Drawing.Point(225, 410);
            this.txtSpecific.Name = "txtSpecific";
            this.txtSpecific.Size = new System.Drawing.Size(375, 34);
            this.txtSpecific.TabIndex = 28;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(2, 410);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(196, 29);
            this.label15.TabIndex = 29;
            this.label15.Text = "SPECIFICATION";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDescription.Location = new System.Drawing.Point(225, 451);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(375, 34);
            this.txtDescription.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(2, 451);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(177, 29);
            this.label16.TabIndex = 31;
            this.label16.Text = "DESCRIPTION";
            // 
            // guna2ButtonSave
            // 
            this.guna2ButtonSave.BackColor = System.Drawing.Color.Transparent;
            this.guna2ButtonSave.BorderRadius = 10;
            this.guna2ButtonSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2ButtonSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2ButtonSave.FillColor = System.Drawing.Color.Black;
            this.guna2ButtonSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonSave.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonSave.Location = new System.Drawing.Point(384, 491);
            this.guna2ButtonSave.Name = "guna2ButtonSave";
            this.guna2ButtonSave.Size = new System.Drawing.Size(105, 45);
            this.guna2ButtonSave.TabIndex = 32;
            this.guna2ButtonSave.Text = "save";
            // 
            // guna2ButtonUpd
            // 
            this.guna2ButtonUpd.BackColor = System.Drawing.Color.Transparent;
            this.guna2ButtonUpd.BorderRadius = 10;
            this.guna2ButtonUpd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonUpd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonUpd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2ButtonUpd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2ButtonUpd.FillColor = System.Drawing.Color.Black;
            this.guna2ButtonUpd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonUpd.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonUpd.Location = new System.Drawing.Point(495, 491);
            this.guna2ButtonUpd.Name = "guna2ButtonUpd";
            this.guna2ButtonUpd.Size = new System.Drawing.Size(105, 45);
            this.guna2ButtonUpd.TabIndex = 33;
            this.guna2ButtonUpd.Text = "update";
            // 
            // frmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(626, 548);
            this.ControlBox = false;
            this.Controls.Add(this.guna2ButtonUpd);
            this.Controls.Add(this.guna2ButtonSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtSpecific);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtStorage);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPublishDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtGeneration);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtPCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmProduct_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox txtPCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtModel;
        public System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.TextBox txtGeneration;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtPublishDate;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtStorage;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txtSpecific;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label16;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonSave;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonUpd;
    }
}
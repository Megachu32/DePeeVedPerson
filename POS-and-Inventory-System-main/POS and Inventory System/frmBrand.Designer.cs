namespace POS_and_Inventory_System
{
    partial class frmBrand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrand));
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtBrand = new MetroFramework.Controls.MetroTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.guna2ButtonSave = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ButtonUpd = new Guna.UI2.WinForms.Guna2Button();
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
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "- BRAND MODULE -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(40, 44);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 29);
            this.lblId.TabIndex = 4;
            // 
            // txtBrand
            // 
            this.txtBrand.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.txtBrand.CustomButton.Image = null;
            this.txtBrand.CustomButton.Location = new System.Drawing.Point(137, 2);
            this.txtBrand.CustomButton.Name = "";
            this.txtBrand.CustomButton.Size = new System.Drawing.Size(241, 241);
            this.txtBrand.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBrand.CustomButton.TabIndex = 1;
            this.txtBrand.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtBrand.CustomButton.UseSelectable = true;
            this.txtBrand.CustomButton.Visible = false;
            this.txtBrand.Lines = new string[0];
            this.txtBrand.Location = new System.Drawing.Point(12, 99);
            this.txtBrand.MaxLength = 32767;
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.PasswordChar = '\0';
            this.txtBrand.PromptText = "Enter Brand Name...";
            this.txtBrand.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBrand.SelectedText = "";
            this.txtBrand.SelectionLength = 0;
            this.txtBrand.SelectionStart = 0;
            this.txtBrand.ShortcutsEnabled = true;
            this.txtBrand.Size = new System.Drawing.Size(381, 246);
            this.txtBrand.TabIndex = 0;
            this.txtBrand.UseSelectable = true;
            this.txtBrand.WaterMark = "Enter Brand Name...";
            this.txtBrand.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtBrand.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBrand.Click += new System.EventHandler(this.txtBrand_Click);
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
            this.btnClose.Location = new System.Drawing.Point(355, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
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
            this.guna2ButtonSave.Location = new System.Drawing.Point(197, 361);
            this.guna2ButtonSave.Name = "guna2ButtonSave";
            this.guna2ButtonSave.Size = new System.Drawing.Size(105, 45);
            this.guna2ButtonSave.TabIndex = 13;
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
            this.guna2ButtonUpd.Location = new System.Drawing.Point(308, 361);
            this.guna2ButtonUpd.Name = "guna2ButtonUpd";
            this.guna2ButtonUpd.Size = new System.Drawing.Size(97, 45);
            this.guna2ButtonUpd.TabIndex = 14;
            this.guna2ButtonUpd.Text = "upd";
            // 
            // frmBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(405, 414);
            this.ControlBox = false;
            this.Controls.Add(this.guna2ButtonUpd);
            this.Controls.Add(this.guna2ButtonSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBrand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblId;
        public MetroFramework.Controls.MetroTextBox txtBrand;
        private System.Windows.Forms.Button btnClose;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonSave;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonUpd;
    }
}
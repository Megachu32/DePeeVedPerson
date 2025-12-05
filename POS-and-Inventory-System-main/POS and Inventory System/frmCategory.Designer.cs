namespace POS_and_Inventory_System
{
    partial class frmCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCategory));
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtCategory = new MetroFramework.Controls.MetroTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.guna2ButtonUpdate = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ButtonSave = new Guna.UI2.WinForms.Guna2Button();
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
            this.label1.Size = new System.Drawing.Size(399, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "- C A T E G O R Y  D E T A I L S -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(13, 105);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 20);
            this.lblId.TabIndex = 11;
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCategory.CustomButton.Image = null;
            this.txtCategory.CustomButton.Location = new System.Drawing.Point(353, 2);
            this.txtCategory.CustomButton.Name = "";
            this.txtCategory.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.txtCategory.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCategory.CustomButton.TabIndex = 1;
            this.txtCategory.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCategory.CustomButton.UseSelectable = true;
            this.txtCategory.CustomButton.Visible = false;
            this.txtCategory.Lines = new string[0];
            this.txtCategory.Location = new System.Drawing.Point(14, 46);
            this.txtCategory.MaxLength = 32767;
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.PasswordChar = '\0';
            this.txtCategory.PromptText = "Enter Category...";
            this.txtCategory.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCategory.SelectedText = "";
            this.txtCategory.SelectionLength = 0;
            this.txtCategory.SelectionStart = 0;
            this.txtCategory.ShortcutsEnabled = true;
            this.txtCategory.Size = new System.Drawing.Size(375, 24);
            this.txtCategory.TabIndex = 0;
            this.txtCategory.UseSelectable = true;
            this.txtCategory.WaterMark = "Enter Category...";
            this.txtCategory.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCategory.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
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
            this.btnClose.Location = new System.Drawing.Point(353, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // guna2ButtonUpdate
            // 
            this.guna2ButtonUpdate.BorderRadius = 10;
            this.guna2ButtonUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2ButtonUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2ButtonUpdate.FillColor = System.Drawing.Color.Black;
            this.guna2ButtonUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonUpdate.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonUpdate.Location = new System.Drawing.Point(198, 76);
            this.guna2ButtonUpdate.Name = "guna2ButtonUpdate";
            this.guna2ButtonUpdate.Size = new System.Drawing.Size(95, 32);
            this.guna2ButtonUpdate.TabIndex = 12;
            this.guna2ButtonUpdate.Text = "Update";
            this.guna2ButtonUpdate.Click += new System.EventHandler(this.guna2ButtonUpdate_Click);
            // 
            // guna2ButtonSave
            // 
            this.guna2ButtonSave.BorderRadius = 10;
            this.guna2ButtonSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2ButtonSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2ButtonSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2ButtonSave.FillColor = System.Drawing.Color.Black;
            this.guna2ButtonSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonSave.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonSave.Location = new System.Drawing.Point(299, 76);
            this.guna2ButtonSave.Name = "guna2ButtonSave";
            this.guna2ButtonSave.Size = new System.Drawing.Size(95, 32);
            this.guna2ButtonSave.TabIndex = 13;
            this.guna2ButtonSave.Text = "Save";
            this.guna2ButtonSave.Click += new System.EventHandler(this.guna2ButtonSave_Click);
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(402, 115);
            this.ControlBox = false;
            this.Controls.Add(this.guna2ButtonSave);
            this.Controls.Add(this.guna2ButtonUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnClose;
        public MetroFramework.Controls.MetroTextBox txtCategory;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonUpdate;
        private Guna.UI2.WinForms.Guna2Button guna2ButtonSave;
    }
}
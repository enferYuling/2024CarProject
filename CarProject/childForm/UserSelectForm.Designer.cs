namespace CarProject.childForm
{
    partial class UserSelectForm
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
            this.User_GridView = new System.Windows.Forms.DataGridView();
            this.srue_btn = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.User_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // User_GridView
            // 
            this.User_GridView.AllowUserToAddRows = false;
            this.User_GridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.User_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.User_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.UserId,
            this.UserCode,
            this.Account,
            this.RealName,
            this.Address});
            this.User_GridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.User_GridView.Location = new System.Drawing.Point(0, 0);
            this.User_GridView.MultiSelect = false;
            this.User_GridView.Name = "User_GridView";
            this.User_GridView.RowHeadersVisible = false;
            this.User_GridView.RowTemplate.Height = 23;
            this.User_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.User_GridView.Size = new System.Drawing.Size(584, 330);
            this.User_GridView.TabIndex = 0;
            this.User_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.User_GridView_CellClick);
            // 
            // srue_btn
            // 
            this.srue_btn.Location = new System.Drawing.Point(234, 334);
            this.srue_btn.Name = "srue_btn";
            this.srue_btn.Size = new System.Drawing.Size(75, 23);
            this.srue_btn.TabIndex = 1;
            this.srue_btn.Text = "确 认";
            this.srue_btn.UseVisualStyleBackColor = true;
            this.srue_btn.Click += new System.EventHandler(this.srue_btn_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "多选";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Visible = false;
            // 
            // UserId
            // 
            this.UserId.DataPropertyName = "UserId";
            this.UserId.HeaderText = "用户id";
            this.UserId.Name = "UserId";
            this.UserId.Visible = false;
            // 
            // UserCode
            // 
            this.UserCode.DataPropertyName = "UserCode";
            this.UserCode.HeaderText = "用户编号";
            this.UserCode.Name = "UserCode";
            // 
            // Account
            // 
            this.Account.DataPropertyName = "Account";
            this.Account.HeaderText = "登录账号";
            this.Account.Name = "Account";
            // 
            // RealName
            // 
            this.RealName.DataPropertyName = "RealName";
            this.RealName.HeaderText = "真实姓名";
            this.RealName.Name = "RealName";
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "详细地址";
            this.Address.Name = "Address";
            // 
            // UserSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.srue_btn);
            this.Controls.Add(this.User_GridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSelectForm";
            this.ShowIcon = false;
            this.Text = "选择用户";
            this.Load += new System.EventHandler(this.UserSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.User_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView User_GridView;
        private System.Windows.Forms.Button srue_btn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
    }
}
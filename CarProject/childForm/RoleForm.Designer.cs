namespace CarProject.childForm
{
    partial class RoleForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.search_rolename = new HZH_Controls.Controls.TextBoxEx();
            this.search_btn = new System.Windows.Forms.Button();
            this.add_btn = new System.Windows.Forms.Button();
            this.batch_btn = new System.Windows.Forms.Button();
            this.canle_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.YC_GridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RoleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YC_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.18957F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.65403F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.886256F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.79621F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.606635F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.58294F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.516651F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.516651F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.search_rolename, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.search_btn, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.add_btn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.batch_btn, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.canle_btn, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.25373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.74627F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(851, 96);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // search_rolename
            // 
            this.search_rolename.DecLength = 2;
            this.search_rolename.InputType = HZH_Controls.TextInputType.NotControl;
            this.search_rolename.Location = new System.Drawing.Point(89, 3);
            this.search_rolename.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.search_rolename.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.search_rolename.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.search_rolename.Name = "search_rolename";
            this.search_rolename.OldText = null;
            this.search_rolename.PromptColor = System.Drawing.Color.Gray;
            this.search_rolename.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.search_rolename.PromptText = "请输入";
            this.search_rolename.RegexPattern = "";
            this.search_rolename.Size = new System.Drawing.Size(143, 21);
            this.search_rolename.TabIndex = 1;
            // 
            // search_btn
            // 
            this.search_btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.search_btn.FlatAppearance.BorderSize = 0;
            this.search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_btn.ForeColor = System.Drawing.Color.White;
            this.search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.search_btn.ImageIndex = 0;
            this.search_btn.Location = new System.Drawing.Point(722, 3);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(57, 26);
            this.search_btn.TabIndex = 4;
            this.search_btn.Text = "查 询";
            this.search_btn.UseVisualStyleBackColor = false;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // add_btn
            // 
            this.add_btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.add_btn.FlatAppearance.BorderSize = 0;
            this.add_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_btn.ForeColor = System.Drawing.Color.White;
            this.add_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.add_btn.ImageIndex = 4;
            this.add_btn.Location = new System.Drawing.Point(3, 35);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(63, 26);
            this.add_btn.TabIndex = 6;
            this.add_btn.Text = "新建";
            this.add_btn.UseVisualStyleBackColor = false;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // batch_btn
            // 
            this.batch_btn.BackColor = System.Drawing.SystemColors.Control;
            this.batch_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.batch_btn.ForeColor = System.Drawing.Color.Black;
            this.batch_btn.Location = new System.Drawing.Point(89, 35);
            this.batch_btn.Name = "batch_btn";
            this.batch_btn.Size = new System.Drawing.Size(75, 27);
            this.batch_btn.TabIndex = 7;
            this.batch_btn.Text = "批量删除";
            this.batch_btn.UseVisualStyleBackColor = false;
            this.batch_btn.Click += new System.EventHandler(this.batch_btn_Click);
            // 
            // canle_btn
            // 
            this.canle_btn.BackColor = System.Drawing.SystemColors.Control;
            this.canle_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.canle_btn.ForeColor = System.Drawing.Color.Black;
            this.canle_btn.Location = new System.Drawing.Point(786, 3);
            this.canle_btn.Name = "canle_btn";
            this.canle_btn.Size = new System.Drawing.Size(60, 26);
            this.canle_btn.TabIndex = 8;
            this.canle_btn.Text = "取 消";
            this.canle_btn.UseVisualStyleBackColor = false;
            this.canle_btn.Click += new System.EventHandler(this.canle_btn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 25);
            this.panel1.TabIndex = 9;
            // 
            // YC_GridView
            // 
            this.YC_GridView.AllowUserToAddRows = false;
            this.YC_GridView.BackgroundColor = System.Drawing.Color.White;
            this.YC_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.YC_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.RoleName,
            this.RoleCode,
            this.Column4,
            this.Column5,
            this.Column6,
            this.RoleId});
            this.YC_GridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YC_GridView.Location = new System.Drawing.Point(0, 96);
            this.YC_GridView.Name = "YC_GridView";
            this.YC_GridView.RowHeadersVisible = false;
            this.YC_GridView.RowTemplate.Height = 23;
            this.YC_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.YC_GridView.Size = new System.Drawing.Size(851, 454);
            this.YC_GridView.TabIndex = 5;
            this.YC_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.YC_GridView_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            // 
            // RoleName
            // 
            this.RoleName.DataPropertyName = "RoleName";
            this.RoleName.HeaderText = "角色名称";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            // 
            // RoleCode
            // 
            this.RoleCode.DataPropertyName = "RoleCode";
            this.RoleCode.HeaderText = "角色编号";
            this.RoleCode.Name = "RoleCode";
            this.RoleCode.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "";
            this.Column4.Name = "Column4";
            this.Column4.Text = "编辑";
            this.Column4.UseColumnTextForButtonValue = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            this.Column5.Text = "用户关联";
            this.Column5.UseColumnTextForButtonValue = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Text = "删除";
            this.Column6.UseColumnTextForButtonValue = true;
            // 
            // RoleId
            // 
            this.RoleId.DataPropertyName = "RoleId";
            this.RoleId.HeaderText = "RoleId";
            this.RoleId.Name = "RoleId";
            this.RoleId.Visible = false;
            // 
            // RoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 550);
            this.Controls.Add(this.YC_GridView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RoleForm";
            this.ShowIcon = false;
            this.Text = "角色管理";
            this.Load += new System.EventHandler(this.RoleForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YC_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private HZH_Controls.Controls.TextBoxEx search_rolename;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button batch_btn;
        private System.Windows.Forms.Button canle_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView YC_GridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleCode;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleId;
    }
}
namespace CarProject.childForm
{
    partial class Cofig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListGridView = new Sunny.UI.UIDataGridView();
            this.savebtn = new Sunny.UI.UIButton();
            this.addbtn = new Sunny.UI.UIButton();
            this.deletebtn = new Sunny.UI.UIButton();
            this.xz = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rowindex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configNmae = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cofigType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.configAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListGridView
            // 
            this.ListGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ListGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ListGridView.BackgroundColor = System.Drawing.Color.White;
            this.ListGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ListGridView.ColumnHeadersHeight = 32;
            this.ListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xz,
            this.rowindex,
            this.configid,
            this.configNmae,
            this.cofigType,
            this.configAddress});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ListGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ListGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListGridView.EnableHeadersVisualStyles = false;
            this.ListGridView.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.ListGridView.Location = new System.Drawing.Point(0, 0);
            this.ListGridView.Name = "ListGridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ListGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ListGridView.RowTemplate.Height = 23;
            this.ListGridView.SelectedIndex = -1;
            this.ListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListGridView.Size = new System.Drawing.Size(704, 334);
            this.ListGridView.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ListGridView.TabIndex = 0;
            this.ListGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListGridView_CellClick);
            // 
            // savebtn
            // 
            this.savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.savebtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.savebtn.Location = new System.Drawing.Point(604, 357);
            this.savebtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(61, 35);
            this.savebtn.TabIndex = 1;
            this.savebtn.Text = "保存";
            this.savebtn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addbtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addbtn.Location = new System.Drawing.Point(13, 340);
            this.addbtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(60, 22);
            this.addbtn.TabIndex = 2;
            this.addbtn.Text = "添加";
            this.addbtn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deletebtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deletebtn.Location = new System.Drawing.Point(79, 339);
            this.deletebtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(60, 23);
            this.deletebtn.TabIndex = 3;
            this.deletebtn.Text = "删除";
            this.deletebtn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // xz
            // 
            this.xz.HeaderText = "选中";
            this.xz.Name = "xz";
            this.xz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.xz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.xz.Width = 50;
            // 
            // rowindex
            // 
            this.rowindex.DataPropertyName = "rowindex";
            this.rowindex.HeaderText = "rowindex";
            this.rowindex.Name = "rowindex";
            this.rowindex.Visible = false;
            // 
            // configid
            // 
            this.configid.DataPropertyName = "cofigid";
            this.configid.HeaderText = "Column1";
            this.configid.Name = "configid";
            this.configid.Visible = false;
            // 
            // configNmae
            // 
            this.configNmae.DataPropertyName = "cofigName";
            this.configNmae.HeaderText = "配置名称";
            this.configNmae.Name = "configNmae";
            // 
            // cofigType
            // 
            this.cofigType.DataPropertyName = "cofigTypename";
            this.cofigType.HeaderText = "配置类型";
            this.cofigType.Items.AddRange(new object[] {
            "进程",
            "文件",
            "图片"});
            this.cofigType.Name = "cofigType";
            this.cofigType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cofigType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // configAddress
            // 
            this.configAddress.DataPropertyName = "configAddress";
            this.configAddress.HeaderText = "配置地址";
            this.configAddress.Name = "configAddress";
            // 
            // Cofig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 404);
            this.Controls.Add(this.deletebtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.ListGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cofig";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Cofig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIDataGridView ListGridView;
        private Sunny.UI.UIButton savebtn;
        private Sunny.UI.UIButton addbtn;
        private Sunny.UI.UIButton deletebtn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn xz;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowindex;
        private System.Windows.Forms.DataGridViewTextBoxColumn configid;
        private System.Windows.Forms.DataGridViewTextBoxColumn configNmae;
        private System.Windows.Forms.DataGridViewComboBoxColumn cofigType;
        private System.Windows.Forms.DataGridViewTextBoxColumn configAddress;
    }
}
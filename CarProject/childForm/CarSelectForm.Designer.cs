namespace CarProject.childForm
{
    partial class CarSelectForm
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
            this.Car_GridView = new System.Windows.Forms.DataGridView();
            this.sure_btn = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.carcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ssgsmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Car_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Car_GridView
            // 
            this.Car_GridView.AllowUserToAddRows = false;
            this.Car_GridView.BackgroundColor = System.Drawing.Color.White;
            this.Car_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Car_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.carcode,
            this.ssgsmc,
            this.carid});
            this.Car_GridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.Car_GridView.Location = new System.Drawing.Point(0, 0);
            this.Car_GridView.Name = "Car_GridView";
            this.Car_GridView.ReadOnly = true;
            this.Car_GridView.RowHeadersVisible = false;
            this.Car_GridView.RowTemplate.Height = 23;
            this.Car_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Car_GridView.Size = new System.Drawing.Size(618, 303);
            this.Car_GridView.TabIndex = 0;
            this.Car_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Car_GridView_CellClick);
            // 
            // sure_btn
            // 
            this.sure_btn.Location = new System.Drawing.Point(522, 309);
            this.sure_btn.Name = "sure_btn";
            this.sure_btn.Size = new System.Drawing.Size(75, 23);
            this.sure_btn.TabIndex = 1;
            this.sure_btn.Text = "确定";
            this.sure_btn.UseVisualStyleBackColor = true;
            this.sure_btn.Click += new System.EventHandler(this.sure_btn_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "多选";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // carcode
            // 
            this.carcode.DataPropertyName = "carcode";
            this.carcode.HeaderText = "小车编号";
            this.carcode.Name = "carcode";
            // 
            // ssgsmc
            // 
            this.ssgsmc.DataPropertyName = "companyname";
            this.ssgsmc.HeaderText = "所属公司名称";
            this.ssgsmc.Name = "ssgsmc";
            // 
            // carid
            // 
            this.carid.DataPropertyName = "carid";
            this.carid.HeaderText = "小车id";
            this.carid.Name = "carid";
            this.carid.Visible = false;
            // 
            // CarSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(618, 344);
            this.Controls.Add(this.sure_btn);
            this.Controls.Add(this.Car_GridView);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CarSelectForm";
            this.Text = "选择小车";
            this.Load += new System.EventHandler(this.CarSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Car_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Car_GridView;
        private System.Windows.Forms.Button sure_btn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn carcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ssgsmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn carid;
    }
}
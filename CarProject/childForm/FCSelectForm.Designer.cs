namespace CarProject.childForm
{
    partial class FCSelectForm
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
            this.FC_GridView = new System.Windows.Forms.DataGridView();
            this.sure_btn = new System.Windows.Forms.Button();
            this.sheltersid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shelterscode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.FC_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FC_GridView
            // 
            this.FC_GridView.AllowUserToAddRows = false;
            this.FC_GridView.BackgroundColor = System.Drawing.Color.White;
            this.FC_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FC_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sheltersid,
            this.shelterscode});
            this.FC_GridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.FC_GridView.Location = new System.Drawing.Point(0, 0);
            this.FC_GridView.MultiSelect = false;
            this.FC_GridView.Name = "FC_GridView";
            this.FC_GridView.RowHeadersVisible = false;
            this.FC_GridView.RowTemplate.Height = 23;
            this.FC_GridView.Size = new System.Drawing.Size(562, 303);
            this.FC_GridView.TabIndex = 1;
            // 
            // sure_btn
            // 
            this.sure_btn.Location = new System.Drawing.Point(463, 314);
            this.sure_btn.Name = "sure_btn";
            this.sure_btn.Size = new System.Drawing.Size(75, 23);
            this.sure_btn.TabIndex = 2;
            this.sure_btn.Text = "确定";
            this.sure_btn.UseVisualStyleBackColor = true;
            this.sure_btn.Click += new System.EventHandler(this.sure_btn_Click);
            // 
            // sheltersid
            // 
            this.sheltersid.DataPropertyName = "sheltersid";
            this.sheltersid.HeaderText = "方舱id";
            this.sheltersid.Name = "sheltersid";
            // 
            // shelterscode
            // 
            this.shelterscode.DataPropertyName = "shelterscode";
            this.shelterscode.HeaderText = "方舱编号";
            this.shelterscode.Name = "shelterscode";
            // 
            // FCSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 349);
            this.Controls.Add(this.sure_btn);
            this.Controls.Add(this.FC_GridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FCSelectForm";
            this.ShowIcon = false;
            this.Text = "选择方舱";
            ((System.ComponentModel.ISupportInitialize)(this.FC_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView FC_GridView;
        private System.Windows.Forms.Button sure_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheltersid;
        private System.Windows.Forms.DataGridViewTextBoxColumn shelterscode;
    }
}
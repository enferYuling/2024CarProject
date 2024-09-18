namespace DYDTCK
{
    partial class DYDT
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hpdt_btn = new System.Windows.Forms.Button();
            this.lddt_btn = new System.Windows.Forms.Button();
            this.rhdt_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DT_Panel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hpdt_btn
            // 
            this.hpdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.hpdt_btn.FlatAppearance.BorderSize = 0;
            this.hpdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hpdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hpdt_btn.ForeColor = System.Drawing.Color.White;
            this.hpdt_btn.Location = new System.Drawing.Point(723, 3);
            this.hpdt_btn.Name = "hpdt_btn";
            this.hpdt_btn.Size = new System.Drawing.Size(67, 31);
            this.hpdt_btn.TabIndex = 5;
            this.hpdt_btn.Text = "航拍地图";
            this.hpdt_btn.UseVisualStyleBackColor = false;
            this.hpdt_btn.Click += new System.EventHandler(this.hpdt_btn_Click);
            // 
            // lddt_btn
            // 
            this.lddt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.lddt_btn.FlatAppearance.BorderSize = 0;
            this.lddt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lddt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lddt_btn.ForeColor = System.Drawing.Color.White;
            this.lddt_btn.Location = new System.Drawing.Point(647, 3);
            this.lddt_btn.Name = "lddt_btn";
            this.lddt_btn.Size = new System.Drawing.Size(67, 31);
            this.lddt_btn.TabIndex = 6;
            this.lddt_btn.Text = "雷达地图";
            this.lddt_btn.UseVisualStyleBackColor = false;
            this.lddt_btn.Click += new System.EventHandler(this.lddt_btn_Click);
            // 
            // rhdt_btn
            // 
            this.rhdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.rhdt_btn.FlatAppearance.BorderSize = 0;
            this.rhdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rhdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rhdt_btn.ForeColor = System.Drawing.Color.White;
            this.rhdt_btn.Location = new System.Drawing.Point(570, 3);
            this.rhdt_btn.Name = "rhdt_btn";
            this.rhdt_btn.Size = new System.Drawing.Size(67, 31);
            this.rhdt_btn.TabIndex = 4;
            this.rhdt_btn.Text = "融合地图";
            this.rhdt_btn.UseVisualStyleBackColor = false;
            this.rhdt_btn.Click += new System.EventHandler(this.rhdt_btn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.rhdt_btn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lddt_btn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.hpdt_btn, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DT_Panel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.111111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.333333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.55556F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 36);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(794, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "激光雷达地图及GPS地图查看";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DT_Panel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.DT_Panel, 4);
            this.DT_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DT_Panel.Location = new System.Drawing.Point(3, 86);
            this.DT_Panel.Name = "DT_Panel";
            this.DT_Panel.Size = new System.Drawing.Size(794, 361);
            this.DT_Panel.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(561, 41);
            this.label2.TabIndex = 9;
            this.label2.Text = "正在初始化";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DYDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DYDT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DYDT_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button hpdt_btn;
        private System.Windows.Forms.Button lddt_btn;
        private System.Windows.Forms.Button rhdt_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel DT_Panel;
        private System.Windows.Forms.Label label2;
    }
}


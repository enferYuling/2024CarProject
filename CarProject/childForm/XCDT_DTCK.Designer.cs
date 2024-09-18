namespace CarProject.childForm
{
    partial class XCDT_DTCK
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
            this.ucPanelTitle1 = new HZH_Controls.Controls.UCPanelTitle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rhdt_btn = new System.Windows.Forms.Button();
            this.lddt_btn = new System.Windows.Forms.Button();
            this.hpdt_btn = new System.Windows.Forms.Button();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.ucPanelTitle1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // ucPanelTitle1
            // 
            this.ucPanelTitle1.BackColor = System.Drawing.Color.Transparent;
            this.ucPanelTitle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle1.ConerRadius = 10;
            this.ucPanelTitle1.Controls.Add(this.webView21);
            this.ucPanelTitle1.Controls.Add(this.panel2);
            this.ucPanelTitle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPanelTitle1.FillColor = System.Drawing.Color.White;
            this.ucPanelTitle1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucPanelTitle1.IsCanExpand = false;
            this.ucPanelTitle1.IsExpand = false;
            this.ucPanelTitle1.IsRadius = true;
            this.ucPanelTitle1.IsShowRect = true;
            this.ucPanelTitle1.Location = new System.Drawing.Point(0, 0);
            this.ucPanelTitle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucPanelTitle1.Name = "ucPanelTitle1";
            this.ucPanelTitle1.Padding = new System.Windows.Forms.Padding(1);
            this.ucPanelTitle1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle1.RectWidth = 1;
            this.ucPanelTitle1.Size = new System.Drawing.Size(821, 482);
            this.ucPanelTitle1.TabIndex = 0;
            this.ucPanelTitle1.Title = "激光雷达地图及GPS地图查看";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.rhdt_btn);
            this.panel2.Controls.Add(this.lddt_btn);
            this.panel2.Controls.Add(this.hpdt_btn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 37);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(197, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 4;
            // 
            // rhdt_btn
            // 
            this.rhdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.rhdt_btn.FlatAppearance.BorderSize = 0;
            this.rhdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rhdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rhdt_btn.Location = new System.Drawing.Point(585, 3);
            this.rhdt_btn.Name = "rhdt_btn";
            this.rhdt_btn.Size = new System.Drawing.Size(67, 31);
            this.rhdt_btn.TabIndex = 1;
            this.rhdt_btn.Text = "融合地图";
            this.rhdt_btn.UseVisualStyleBackColor = false;
            this.rhdt_btn.Click += new System.EventHandler(this.rhdt_btn_Click);
            // 
            // lddt_btn
            // 
            this.lddt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.lddt_btn.FlatAppearance.BorderSize = 0;
            this.lddt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lddt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lddt_btn.Location = new System.Drawing.Point(663, 3);
            this.lddt_btn.Name = "lddt_btn";
            this.lddt_btn.Size = new System.Drawing.Size(67, 31);
            this.lddt_btn.TabIndex = 3;
            this.lddt_btn.Text = "雷达地图";
            this.lddt_btn.UseVisualStyleBackColor = false;
            this.lddt_btn.Click += new System.EventHandler(this.lddt_btn_Click);
            // 
            // hpdt_btn
            // 
            this.hpdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.hpdt_btn.FlatAppearance.BorderSize = 0;
            this.hpdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hpdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hpdt_btn.Location = new System.Drawing.Point(741, 3);
            this.hpdt_btn.Name = "hpdt_btn";
            this.hpdt_btn.Size = new System.Drawing.Size(67, 31);
            this.hpdt_btn.TabIndex = 2;
            this.hpdt_btn.Text = "航拍地图";
            this.hpdt_btn.UseVisualStyleBackColor = false;
            this.hpdt_btn.Click += new System.EventHandler(this.hpdt_btn_Click);
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(1, 72);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(819, 409);
            this.webView21.TabIndex = 6;
            this.webView21.ZoomFactor = 1D;
            // 
            // XCDT_DTCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 482);
            this.Controls.Add(this.ucPanelTitle1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XCDT_DTCK";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.XCDT_DTCK_Load);
            this.ucPanelTitle1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HZH_Controls.Controls.UCPanelTitle ucPanelTitle1;
        private System.Windows.Forms.Button rhdt_btn;
        private System.Windows.Forms.Button lddt_btn;
        private System.Windows.Forms.Button hpdt_btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
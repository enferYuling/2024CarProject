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
            this.lddt_btn = new System.Windows.Forms.Button();
            this.hpdt_btn = new System.Windows.Forms.Button();
            this.rhdt_btn = new System.Windows.Forms.Button();
            this.dt_panle = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucPanelTitle1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucPanelTitle1
            // 
            this.ucPanelTitle1.BackColor = System.Drawing.Color.Transparent;
            this.ucPanelTitle1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucPanelTitle1.ConerRadius = 10;
            this.ucPanelTitle1.Controls.Add(this.panel2);
            this.ucPanelTitle1.Controls.Add(this.dt_panle);
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
            this.ucPanelTitle1.Size = new System.Drawing.Size(800, 450);
            this.ucPanelTitle1.TabIndex = 0;
            this.ucPanelTitle1.Title = "激光雷达地图及GPS地图查看";
            // 
            // lddt_btn
            // 
            this.lddt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.lddt_btn.FlatAppearance.BorderSize = 0;
            this.lddt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lddt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lddt_btn.Location = new System.Drawing.Point(650, 3);
            this.lddt_btn.Name = "lddt_btn";
            this.lddt_btn.Size = new System.Drawing.Size(67, 31);
            this.lddt_btn.TabIndex = 3;
            this.lddt_btn.Text = "雷达地图";
            this.lddt_btn.UseVisualStyleBackColor = false;
            // 
            // hpdt_btn
            // 
            this.hpdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.hpdt_btn.FlatAppearance.BorderSize = 0;
            this.hpdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hpdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hpdt_btn.Location = new System.Drawing.Point(728, 3);
            this.hpdt_btn.Name = "hpdt_btn";
            this.hpdt_btn.Size = new System.Drawing.Size(67, 31);
            this.hpdt_btn.TabIndex = 2;
            this.hpdt_btn.Text = "航拍地图";
            this.hpdt_btn.UseVisualStyleBackColor = false;
            // 
            // rhdt_btn
            // 
            this.rhdt_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.rhdt_btn.FlatAppearance.BorderSize = 0;
            this.rhdt_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rhdt_btn.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rhdt_btn.Location = new System.Drawing.Point(572, 3);
            this.rhdt_btn.Name = "rhdt_btn";
            this.rhdt_btn.Size = new System.Drawing.Size(67, 31);
            this.rhdt_btn.TabIndex = 1;
            this.rhdt_btn.Text = "融合地图";
            this.rhdt_btn.UseVisualStyleBackColor = false;
            // 
            // dt_panle
            // 
            this.dt_panle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dt_panle.Location = new System.Drawing.Point(1, 1);
            this.dt_panle.Name = "dt_panle";
            this.dt_panle.Size = new System.Drawing.Size(798, 448);
            this.dt_panle.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rhdt_btn);
            this.panel2.Controls.Add(this.lddt_btn);
            this.panel2.Controls.Add(this.hpdt_btn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 37);
            this.panel2.TabIndex = 5;
            // 
            // XCDT_DTCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucPanelTitle1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XCDT_DTCK";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ucPanelTitle1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HZH_Controls.Controls.UCPanelTitle ucPanelTitle1;
        private System.Windows.Forms.Button rhdt_btn;
        private System.Windows.Forms.Button lddt_btn;
        private System.Windows.Forms.Button hpdt_btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel dt_panle;
    }
}
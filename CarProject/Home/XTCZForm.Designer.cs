namespace CarProject.Home
{
    partial class XTCZForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XTCZForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.jsgl_btn = new Sunny.UI.UIButton();
            this.sjkcz_btn = new Sunny.UI.UIButton();
            this.FormPanel = new Sunny.UI.UIPanel();
            this.titlePanel = new Sunny.UI.UIPanel();
            this.configbtn = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.configbtn);
            this.splitContainer1.Panel1.Controls.Add(this.jsgl_btn);
            this.splitContainer1.Panel1.Controls.Add(this.sjkcz_btn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FormPanel);
            this.splitContainer1.Panel2.Controls.Add(this.titlePanel);
            this.splitContainer1.Size = new System.Drawing.Size(1051, 630);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 0;
            // 
            // jsgl_btn
            // 
            this.jsgl_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.jsgl_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.jsgl_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.jsgl_btn.Location = new System.Drawing.Point(0, 35);
            this.jsgl_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.jsgl_btn.Name = "jsgl_btn";
            this.jsgl_btn.Size = new System.Drawing.Size(180, 35);
            this.jsgl_btn.TabIndex = 1;
            this.jsgl_btn.Text = "角色管理";
            this.jsgl_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.jsgl_btn.Click += new System.EventHandler(this.jsgl_btn_Click);
            // 
            // sjkcz_btn
            // 
            this.sjkcz_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sjkcz_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.sjkcz_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sjkcz_btn.Location = new System.Drawing.Point(0, 0);
            this.sjkcz_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.sjkcz_btn.Name = "sjkcz_btn";
            this.sjkcz_btn.Size = new System.Drawing.Size(180, 35);
            this.sjkcz_btn.TabIndex = 0;
            this.sjkcz_btn.Text = "数据库操作";
            this.sjkcz_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sjkcz_btn.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // FormPanel
            // 
            this.FormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormPanel.Location = new System.Drawing.Point(0, 41);
            this.FormPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FormPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(867, 589);
            this.FormPanel.TabIndex = 1;
            this.FormPanel.Text = "uiPanel2";
            this.FormPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titlePanel
            // 
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.titlePanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(867, 41);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.Text = "uiPanel1";
            this.titlePanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // configbtn
            // 
            this.configbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configbtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.configbtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.configbtn.Location = new System.Drawing.Point(0, 70);
            this.configbtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.configbtn.Name = "configbtn";
            this.configbtn.Size = new System.Drawing.Size(180, 35);
            this.configbtn.TabIndex = 2;
            this.configbtn.Text = "配置管理";
            this.configbtn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.configbtn.Click += new System.EventHandler(this.configbtn_Click);
            // 
            // XTCZForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 630);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XTCZForm";
            this.Text = "系统操作";
            this.Resize += new System.EventHandler(this.XTCZForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Sunny.UI.UIPanel FormPanel;
        private Sunny.UI.UIPanel titlePanel;
        private Sunny.UI.UIButton sjkcz_btn;
        private Sunny.UI.UIButton jsgl_btn;
        private Sunny.UI.UIButton configbtn;
    }
}
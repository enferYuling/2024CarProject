namespace CarProject.childForm
{
    partial class RoleAddForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.jsbh_text = new System.Windows.Forms.TextBox();
            this.jsmc_text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sure_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色编号";
            // 
            // jsbh_text
            // 
            this.jsbh_text.Location = new System.Drawing.Point(143, 63);
            this.jsbh_text.Name = "jsbh_text";
            this.jsbh_text.Size = new System.Drawing.Size(147, 21);
            this.jsbh_text.TabIndex = 1;
            // 
            // jsmc_text
            // 
            this.jsmc_text.Location = new System.Drawing.Point(143, 108);
            this.jsmc_text.Name = "jsmc_text";
            this.jsmc_text.Size = new System.Drawing.Size(147, 21);
            this.jsmc_text.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "角色名称";
            // 
            // sure_btn
            // 
            this.sure_btn.Location = new System.Drawing.Point(143, 197);
            this.sure_btn.Name = "sure_btn";
            this.sure_btn.Size = new System.Drawing.Size(75, 23);
            this.sure_btn.TabIndex = 4;
            this.sure_btn.Text = "确 定";
            this.sure_btn.UseVisualStyleBackColor = true;
            this.sure_btn.Click += new System.EventHandler(this.sure_btn_Click);
            // 
            // RoleAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 390);
            this.Controls.Add(this.sure_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jsmc_text);
            this.Controls.Add(this.jsbh_text);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoleAddForm";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.RoleAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox jsbh_text;
        private System.Windows.Forms.TextBox jsmc_text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sure_btn;
    }
}
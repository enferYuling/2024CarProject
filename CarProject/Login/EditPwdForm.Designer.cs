namespace CarProject.Login
{
    partial class EditPwdForm
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
            this.account_text = new System.Windows.Forms.TextBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pwd_text1 = new System.Windows.Forms.TextBox();
            this.pwd_text2 = new System.Windows.Forms.TextBox();
            this.srue_btn = new System.Windows.Forms.Button();
            this.ts_lab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号：";
            // 
            // account_text
            // 
            this.account_text.Location = new System.Drawing.Point(114, 70);
            this.account_text.Name = "account_text";
            this.account_text.Size = new System.Drawing.Size(153, 21);
            this.account_text.TabIndex = 1;
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(303, 70);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(69, 23);
            this.search_btn.TabIndex = 2;
            this.search_btn.Text = "查找";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "确认密码";
            this.label3.Visible = false;
            // 
            // pwd_text1
            // 
            this.pwd_text1.Location = new System.Drawing.Point(114, 101);
            this.pwd_text1.Name = "pwd_text1";
            this.pwd_text1.PasswordChar = '*';
            this.pwd_text1.Size = new System.Drawing.Size(153, 21);
            this.pwd_text1.TabIndex = 5;
            this.pwd_text1.Visible = false;
            this.pwd_text1.TextChanged += new System.EventHandler(this.pwd_text1_TextChanged);
            // 
            // pwd_text2
            // 
            this.pwd_text2.Location = new System.Drawing.Point(114, 132);
            this.pwd_text2.Name = "pwd_text2";
            this.pwd_text2.PasswordChar = '*';
            this.pwd_text2.Size = new System.Drawing.Size(153, 21);
            this.pwd_text2.TabIndex = 6;
            this.pwd_text2.TextChanged += new System.EventHandler(this.pwd_text2_TextChanged);
            // 
            // srue_btn
            // 
            this.srue_btn.Location = new System.Drawing.Point(169, 225);
            this.srue_btn.Name = "srue_btn";
            this.srue_btn.Size = new System.Drawing.Size(75, 23);
            this.srue_btn.TabIndex = 7;
            this.srue_btn.Text = "确认";
            this.srue_btn.UseVisualStyleBackColor = true;
            this.srue_btn.Click += new System.EventHandler(this.srue_btn_Click);
            // 
            // ts_lab
            // 
            this.ts_lab.AutoSize = true;
            this.ts_lab.ForeColor = System.Drawing.Color.Red;
            this.ts_lab.Location = new System.Drawing.Point(149, 179);
            this.ts_lab.Name = "ts_lab";
            this.ts_lab.Size = new System.Drawing.Size(0, 12);
            this.ts_lab.TabIndex = 8;
            // 
            // EditPwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 319);
            this.Controls.Add(this.ts_lab);
            this.Controls.Add(this.srue_btn);
            this.Controls.Add(this.pwd_text2);
            this.Controls.Add(this.pwd_text1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.account_text);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditPwdForm";
            this.ShowIcon = false;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.EditPwdForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox account_text;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pwd_text1;
        private System.Windows.Forms.TextBox pwd_text2;
        private System.Windows.Forms.Button srue_btn;
        private System.Windows.Forms.Label ts_lab;
    }
}
namespace CarProject.childForm
{
    partial class RoleUserForm
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
            this.RoleName_lab = new System.Windows.Forms.Label();
            this.add_user = new System.Windows.Forms.Button();
            this.delete_btn = new System.Windows.Forms.Button();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前角色";
            // 
            // RoleName_lab
            // 
            this.RoleName_lab.AutoSize = true;
            this.RoleName_lab.Location = new System.Drawing.Point(94, 119);
            this.RoleName_lab.Name = "RoleName_lab";
            this.RoleName_lab.Size = new System.Drawing.Size(0, 12);
            this.RoleName_lab.TabIndex = 1;
            // 
            // add_user
            // 
            this.add_user.BackColor = System.Drawing.Color.DodgerBlue;
            this.add_user.FlatAppearance.BorderSize = 0;
            this.add_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_user.ForeColor = System.Drawing.Color.White;
            this.add_user.Location = new System.Drawing.Point(417, 121);
            this.add_user.Name = "add_user";
            this.add_user.Size = new System.Drawing.Size(75, 23);
            this.add_user.TabIndex = 3;
            this.add_user.Text = "添加用户";
            this.add_user.UseVisualStyleBackColor = false;
            this.add_user.Click += new System.EventHandler(this.add_user_Click);
            // 
            // delete_btn
            // 
            this.delete_btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.delete_btn.FlatAppearance.BorderSize = 0;
            this.delete_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_btn.ForeColor = System.Drawing.Color.White;
            this.delete_btn.Location = new System.Drawing.Point(417, 183);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(75, 23);
            this.delete_btn.TabIndex = 4;
            this.delete_btn.Text = "删除用户";
            this.delete_btn.UseVisualStyleBackColor = false;
            this.delete_btn.Click += new System.EventHandler(this.delete_btn_Click);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(564, 38);
            this.uiPanel1.TabIndex = 6;
            this.uiPanel1.Text = "用户关联";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(211, 42);
            this.listBox.Name = "listBox";
            this.listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox.Size = new System.Drawing.Size(188, 292);
            this.listBox.TabIndex = 7;
            // 
            // RoleUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(564, 343);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.delete_btn);
            this.Controls.Add(this.add_user);
            this.Controls.Add(this.RoleName_lab);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoleUserForm";
            this.ShowIcon = false;
            this.Text = "用户关联";
            this.Load += new System.EventHandler(this.RoleUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RoleName_lab;
        private System.Windows.Forms.Button add_user;
        private System.Windows.Forms.Button delete_btn;
        private Sunny.UI.UIPanel uiPanel1;
        private System.Windows.Forms.ListBox listBox;
    }
}
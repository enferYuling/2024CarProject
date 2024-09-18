using CarProject.Models;
using HZH_Controls;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Windows.Forms;

namespace CarProject.childForm
{
    public partial class XCJTYMForm : Form
    {
        public readonly SqlSugarClient db;
        public XCJTYMForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }
        public void LoadData()
        {
            var list = this.db.Queryable<Pro_sheltersInfo>().ToList();

            // 使用LINQ查询对DataGridView进行分组
            var groupedData = from Pro_sheltersInfo row in list
                              group row by row.CreateDate into grp
                              select new
                              {
                                  GroupName = grp.Key,
                                  GroupData = grp.ToList()
                              };

            // 清空DataGridView中的数据
            //  YC_GridView.DataSource=null;

            // 将分组后的数据重新添加到DataGridView中
            // 假设我们要创建5个GroupBox
            const int numberOfGroupBoxes = 5;
            const int groupBoxSpacing =5; // 组间距
            int groupBoxPosition =2; // 组的起始位置

            foreach (var group in groupedData)
            {
                System.Windows.Forms.GroupBox groupBox = new System.Windows.Forms.GroupBox();
                // ' 设置GroupBox属性
                groupBox.Height = 150;
                groupBox.Width = panel1.Width;
                groupBox.Location = new Point(groupBoxSpacing, groupBoxPosition);

                groupBox.Text = group.GroupName.ToDate().ToString("yyyy年MM月dd日");
                /// ' 设置DataGridView属性
                UIDataGridView dataGridView = new UIDataGridView();
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.Parent = groupBox;
                dataGridView.DataSource = group.GroupData;
                dataGridView.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn item =new DataGridViewTextBoxColumn();
                item.HeaderText = "A区";
                item.Name = "A";
                item.DataPropertyName = "shelterscode";
                item.ValueType= typeof(string);
                
                dataGridView.Columns.Add(item);
                panel1.Controls.Add(groupBox);
                // 更新组的位置
                groupBoxPosition += groupBox.Height + groupBoxSpacing;
            }
        }

        private void XCJTYMForm_Load(object sender, EventArgs e)
        {
           
         
            LoadData();
            
           
        }

        private void RCX_img_Click(object sender, EventArgs e)
        {

        }

        private void RCX_img_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            childForm.XCJTJMCKForm form=new XCJTJMCKForm(db);
            
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}

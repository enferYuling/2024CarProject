using CarProject.Models;
using HZH_Controls;
using HZH_Controls.Controls;
using Newtonsoft.Json.Linq;
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
    public partial class CarSelectForm : Form
    {
        public DataRow SelectDataRow = null;
        public List<DataGridViewRow> SelectRows = null;
        public readonly SqlSugarClient db;
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool isMiuSelect = false;
        public CarSelectForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void sure_btn_Click(object sender, EventArgs e)
        {
            if (!isMiuSelect)
            {
                // 获取选中的行
                DataGridViewRow selectedRow = Car_GridView.SelectedRows[0];
                // 获取行数据

                var query = selectedRow.DataBoundItem as DataRowView;
                SelectDataRow = query.Row;
                DialogResult = DialogResult.OK;
                
            }
            else
            {
                // 使用LINQ查询获取满足条件的行
                 SelectRows = Car_GridView.Rows.Cast<DataGridViewRow>()
                                     .Where(row => row.Cells["Column1"].Value.ToBool() == true)
                                     .ToList();
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void CarSelectForm_Load(object sender, EventArgs e)
        {
            Car_GridView.AutoGenerateColumns = false;
            if (isMiuSelect)
            {
                Car_GridView.MultiSelect = true;
                Car_GridView.Columns["Column1"].Visible = true;
            }
            else
            {
                Car_GridView.MultiSelect= false;
                Car_GridView.Columns["Column1"].Visible = false;
            }
            var dt = this.db.Queryable<Pro_CarInfo>().Where(a => a.Enabled == 1 && a.sheltersid == 0 && string.IsNullOrEmpty(a.workorderclerkid) && string.IsNullOrEmpty(a.operatorid)).ToDataTable();
            Car_GridView.DataSource = dt;

        }

        private void Car_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isMiuSelect&&e.RowIndex>=0)
            {
                // 获取对应的单元格
                DataGridViewCell Cell = Car_GridView.Rows[e.RowIndex].Cells["Column1"];
                // 获取选中的行
                //    DataGridViewRow selectedRow = YC_GridView.SelectedRows[0];
                // 获取行数据

                //  var query = selectedRow.DataBoundItem as DataRowView;
                if (Cell.Value.ToBool())
                {
                    Cell.Value = false;

                }
                else
                {
                    Cell.Value = true;

                }
            }
        }
    }
}

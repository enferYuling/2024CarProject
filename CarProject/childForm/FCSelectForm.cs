using SqlSugar;
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
    public partial class FCSelectForm : Form
    {
        public DataRow SelectDataRow = null;
        public readonly SqlSugarClient db;
        public FCSelectForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void sure_btn_Click(object sender, EventArgs e)
        {
            // 获取选中的行
            DataGridViewRow selectedRow = FC_GridView.SelectedRows[0];
            // 获取行数据

            var query = selectedRow.DataBoundItem as DataRowView;
            SelectDataRow = query.Row;
            DialogResult = DialogResult.OK;
        }
    }
}

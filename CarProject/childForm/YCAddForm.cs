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
    public partial class YCAddForm : Form
    {
        public readonly SqlSugarClient db;
        public bool isReadonly=false;
        public string taskid;
        public YCAddForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void YCAddForm_Load(object sender, EventArgs e)
        {
            if (isReadonly)
            {
                ycdbh_text.ReadOnly = true;
                czcl_text.ReadOnly = true;
                czry_text.ReadOnly = true;
                ycadd_btn.Visible = false;
                ycdelete_btn.Visible = false;
                YCGridView.ReadOnly = true;
                detailGridView.ReadOnly = true;
            }
        }
    }
}

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
    public partial class XCJTJMCKForm : Form
    {
        public readonly SqlSugarClient db;
        public XCJTJMCKForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void XCJTJMCKForm_Load(object sender, EventArgs e)
        {

        }
    }
}

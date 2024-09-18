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
    public partial class DataForm : Form
    {
        public readonly SqlSugarClient db;
        public DataForm(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(table_text.Text)) MessageBox.Show("请输入表名");
            db.DbFirst
            //类
            .SettingClassTemplate(old => { return old;/*修改old值替换*/ })
            //类构造函数
            .SettingConstructorTemplate(old => { return old;/*修改old值替换*/ })
             .SettingNamespaceTemplate(old => {
                 return old + "\r\nusing SqlSugar;"; //追加引用SqlSugar
             })
            //属性备注
            .SettingPropertyDescriptionTemplate(old => { return old;/*修改old值替换*/})

            //属性:新重载 完全自定义用配置
            .SettingPropertyTemplate((columns, temp, type) => {

                var columnattribute = "\r\n           [SugarColumn({0})]";
                List<string> attributes = new List<string>();
                if (columns.IsPrimarykey)
                    attributes.Add("IsPrimaryKey=true");
                if (columns.IsIdentity)
                    attributes.Add("IsIdentity=true");
                if (!columns.IsPrimarykey)
                    attributes.Add("IsIgnore=false");
                if (attributes.Count == 0)
                {
                    columnattribute = "";
                }
                return temp.Replace("{PropertyType}", type)
                            .Replace("{PropertyName}", columns.DbColumnName)
                            .Replace("{SugarColumn}", string.Format(columnattribute, string.Join(",", attributes)));
            })
            .Where(table_text.Text)
           .CreateClassFile("E:\\temp", "CarProject.Models");

            //  this.db.DbFirst.IsCreateAttribute().Where(table_text.Text).CreateClassFile("E:\\temp", "Models");
        }
    }
}

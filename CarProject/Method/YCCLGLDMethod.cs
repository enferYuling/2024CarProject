using CarProject.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Method
{
    public class YCCLGLDMethod
    {
        public readonly SqlSugarClient db;
        public YCCLGLDMethod(SqlSugarClient datadb)
        {
            this.db = datadb;
        }
        /// <summary>
        /// 加载方舱数据
        /// </summary>
        /// <param name="code">方舱编号</param>
        /// <param name="status">方舱状态</param>
        /// <returns></returns>
        public DataTable LoadFCData(string code,int? status)
        {
            DataTable dt = new DataTable();
            var exp = Expressionable.Create<Pro_sheltersInfo>();
            exp.AndIF(!string.IsNullOrEmpty(code), it => it.shelterscode == code);//.OrIf 是条件成立才会拼接OR
            exp.AndIF(status != null && status != -1, it => it.status == status);//.OrIf 是条件成立才会拼接OR
            try
            {
                dt = this.db.Queryable<Pro_sheltersInfo>().Where(exp.ToExpression()).ToDataTable();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

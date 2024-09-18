using HZH_Controls;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Common
{
    public class CodeHelp
    {
        public readonly SqlSugarClient db;
        public CodeHelp(SqlSugarClient datadb)
        {
            this.db = datadb;
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="dateTime"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string encoded(string account,DateTime? dateTime,string tableName)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var count = "0";
            if (!string.IsNullOrEmpty(tableName))
            {
                string sql = "select count(*) as count from " + tableName;
                var dt = db.Ado.GetDataTable(sql);
                count = (dt.Rows[0]["count"].ToInt() + 1).ToString();
            }
            var datetext = dateTime.ToDate().ToString("yyyyMMdd");
           
            string code=account + datetext+count;
            return code;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SqlSugar;
 
namespace CarProject.Common
{
    class DataHelp
    {
        
        SqlConnection sc;
        public SqlConnection connect()
        {
            string str = @"Server=.; Database=CarProject;User ID=sa;Password=123456;Timeout=30;MultipleActiveResultSets=true";
             sc=new SqlConnection(str);
            sc.Open();
            return sc;
        }
        public SqlCommand command(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql,connect());
            return cmd;
        }
        public int Ececute (string sql)
        {
            return command(sql).ExecuteNonQuery();
        }
        public SqlDataReader read(string sql)
        {
            return command(sql).ExecuteReader();
        }
        public void DaoClose()
        {
            sc.Close();
        }
        
    }

}

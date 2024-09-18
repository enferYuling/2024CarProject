using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHCNetSDK;
using static CHCNetSDK.CHCNet;

namespace CarProject.Method
{
   
    public class LJTSMethod
    {
        public readonly SqlSugarClient db;
        LOGINRESULTCALLBACK LoginCallBack = null;
        public LJTSMethod(SqlSugarClient datadb)
        {
            this.db = datadb;
        }
        public NET_DVR_USER_LOGIN_INFO GetSPZSX(string ip,string dk,string account ,string pwd)
        {
            var struLogInfo = new NET_DVR_USER_LOGIN_INFO();
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(dk) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
            {
                return struLogInfo;
            }
           
            //设备IP地址或者域名
            byte[] byIP = System.Text.Encoding.Default.GetBytes(ip);
            struLogInfo.sDeviceAddress = new byte[129];
            byIP.CopyTo(struLogInfo.sDeviceAddress, 0);
          
            //设备用户名112
            byte[] byUserName = System.Text.Encoding.Default.GetBytes(account);
            struLogInfo.sUserName = new byte[64];
            byUserName.CopyTo(struLogInfo.sUserName, 0);

            //设备密码
            byte[] byPassword = System.Text.Encoding.Default.GetBytes(pwd);
            struLogInfo.sPassword = new byte[64];
            byPassword.CopyTo(struLogInfo.sPassword, 0);

            struLogInfo.wPort = ushort.Parse(dk);//设备服务端口号
            
            return struLogInfo;
        }
         
    }
}

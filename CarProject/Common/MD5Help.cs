using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CarProject.Common
{
    public class MD5Help
    {
        public static string GetMD5Hash(string input)
        {
            // 创建一个MD5对象
            using (MD5 md5 = MD5.Create())
            {
                // 将输入字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                // 计算输入字节数组的哈希值
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                // 将字节数组转换为字符串
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // 将字节转换为十六进制字符串
                }
                return sb.ToString();
            }
        }
    }
}

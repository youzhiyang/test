using CommonUtility.IOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visitor.Controllers.util
{
    public class Cryptography
    {
        /// <summary>
        /// 读取指定文件数据并加密
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="name">文件名</param>
        /// <param name="content">数据</param>
        /// </summary>
        public static string EncryptString(string path, string name, string key)
        {
            var fileStr = FileReader.Instance.Read(path + name);
            string encryptString = CommonUtility.StringHelper.Garble.Instance.EncryptString(fileStr, key);
            return encryptString;
        }

        /// <summary>
        /// 解密指定文件数据
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="name">文件名</param
        /// <param name="key">key</param>
        /// </summary>
        public static string DecryptString(string path, string name, string key)
        {
            var configStr = FileReader.Instance.Read(path + name);
            string decryptstring = CommonUtility.StringHelper.Garble.Instance.DecryptString(configStr, key);
            return decryptstring;
        }
    }
}
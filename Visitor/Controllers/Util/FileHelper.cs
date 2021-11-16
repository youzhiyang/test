using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Visitor.Controllers.Util
{
    public class FileHelper
    {
        /// <summary>
        /// 把数据写入指定文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="name">文件名</param>
        /// <param name="content">写入的值</param>
        /// </summary>
        public static void WriteToFile(string path, string name, string content)
        {
            StreamWriter stringWriter = null;
            try
            {
                FileStream file = new FileStream(path + name, FileMode.Create, FileAccess.Write);
                stringWriter = new StreamWriter(file);
                stringWriter.Write(content);
            }
            finally 
            {
                if (stringWriter != null)
                {
                    stringWriter.Close();
                }
            }
        }
    }
}
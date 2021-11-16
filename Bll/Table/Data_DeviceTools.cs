using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Table
{
    /// <summary>
    /// 设备工具相关业务
    /// </summary>
    public class Data_DeviceTools
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly Data_DeviceTools Instance = new Data_DeviceTools();

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public ResponseData GetDeviceTools()
        {
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Business_DeviceToolsDal().GetDeviceTools<DeviceTools>(conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_设备工具
    /// </summary>
    public partial class Business_DeviceTools
    {
        /// <summary>
        /// 设备id
        /// </summary>
        protected string _deviceID;

        /// <summary>
        /// 设备名称
        /// </summary>
        protected string _deviceName;

        /// <summary>
        /// 数据字典id
        /// </summary>
        protected string _dictID;

        /// <summary>
        /// 业务数据_设备工具
        /// </summary>
        public Business_DeviceTools()
        { 

        }

        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        /// <summary>
        /// 数据字典id
        /// </summary>
        public string DictID
        {
            get { return _dictID; }
            set { _dictID = value; }
        }
    }
}

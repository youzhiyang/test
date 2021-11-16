using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Model.Entity.Custom
{
    /// <summary>
    /// 设备工具信息
    /// </summary>
    public class DeviceTools
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
        /// 字典名
        /// </summary>
        protected string _name;

        /// <summary>
        /// 排序
        /// </summary>
        protected int _order;

        /// <summary>
        /// 设备工具信息
        /// </summary>
        public DeviceTools()
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

        /// <summary>
        /// 数据字典名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 数据字典排序
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}

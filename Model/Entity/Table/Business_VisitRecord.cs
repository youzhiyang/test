using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_来访记录
    /// </summary>
    public partial class Business_VisitRecord
    {
        /// <summary>
        /// 来访记录ID
        /// </summary>
        protected int _visitRecordID;

        /// <summary>
        /// 来访授权码
        /// </summary>
        protected string _authCode;

        /// <summary>
        /// 来访事由代码
        /// </summary>
        protected string _reasonCode;

        /// <summary>
        /// 来访事由
        /// </summary>
        protected string _reasonName;

        /// <summary>
        /// 自定义来访事由
        /// </summary>
        protected string _reason;

        /// <summary>
        /// 访问区域
        /// </summary>
        protected string _areas;

        /// <summary>
        /// 携带设备与工具
        /// </summary>
        protected string _deviceNTools;

        /// <summary>
        /// 登记时间
        /// </summary>
        protected DateTime _registerTime;

        /// <summary>
        /// 入场引导人
        /// </summary>
        protected int _enterGuide;

        /// <summary>
        /// 业务数据_来访记录
        /// </summary>
        public Business_VisitRecord()
        {
        }

        /// <summary>
        /// 来访记录ID
        /// </summary>
        public int VisitRecordID
        {
            get { return _visitRecordID; }
            set { _visitRecordID = value; }
        }

        /// <summary>
        /// 来访授权码
        /// </summary>
        public string AuthCode
        {
            get { return _authCode; }
            set { _authCode = value; }
        }

        /// <summary>
        /// 来访事由代码
        /// </summary>
        public string ReasonCode
        {
            get { return _reasonCode; }
            set { _reasonCode = value; }
        }

        /// <summary>
        /// 来访事由
        /// </summary>
        public string ReasonName
        {
            get { return _reasonName; }
            set { _reasonName = value; }
        }

        /// <summary>
        /// 自定义来访事由
        /// </summary>
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        /// <summary>
        /// 访问区域
        /// </summary>
        public string Areas
        {
            get { return _areas; }
            set { _areas = value; }
        }

        /// <summary>
        /// 携带设备与工具
        /// </summary>
        public string DeviceNTools
        {
            get { return _deviceNTools; }
            set { _deviceNTools = value; }
        }

        /// <summary>
        /// 携带设备与工具
        /// </summary>
        public DateTime RegisterTime
        {
            get { return _registerTime; }
            set { _registerTime = value; }
        }


        /// <summary>
        /// 入场引导人
        /// </summary>
        public int EnterGuide
        {
            get { return _enterGuide; }
            set { _enterGuide = value; }
        }

        /// <summary>
        /// 重置业务数据_来访记录
        /// </summary>
        public void Reset()
        {
            _visitRecordID = 0;
            _authCode = null;
            _reasonCode = null;
            _reasonName = null;
            _reason = null;
            _areas = null;
            _deviceNTools = null;
            _registerTime = DateTime.Now;
        }

        /// <summary>
        /// 复制业务数据_来访记录
        /// </summary>
        public void AssignFrom(Business_VisitRecord AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _visitRecordID = AObj._visitRecordID;
            _authCode = AObj._authCode;
            _reasonCode = AObj._reasonCode;
            _reasonName = AObj._reasonName;
            _reason = AObj._reason;
            _areas = AObj._areas;
            _deviceNTools = AObj._deviceNTools;
            _registerTime = AObj._registerTime;
        }

        /// <summary>
        /// 偏移时间
        /// </summary>
        public void OffsetTime(int offsetMinutes)
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.GetType().Equals(typeof(DateTime)))
                {
                    property.SetValue(this, ((DateTime)property.GetValue(this, null)).AddMinutes(offsetMinutes), null);
                }
                else if (property.GetType().Equals(typeof(DateTime?)))
                {
                    var value = (DateTime?)property.GetValue(this, null);
                    if (value.HasValue)
                    {
                        property.SetValue(this, value.Value.AddMinutes(offsetMinutes), null);
                    }
                }
            }
        }
    }
}

using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_访客记录
    /// </summary>
    public partial class Business_VisitorRecord
    {
        /// <summary>
        /// 访客记录ID
        /// </summary>
        protected int _visitorRecordID;

        /// <summary>
        /// 来访记录ID
        /// </summary>
        protected int _visitRecordID;

        /// <summary>
        /// 身份证件号
        /// </summary>
        protected string _certID;

        /// <summary>
        /// 来访组织
        /// </summary>
        protected string _visitOrgization;

        /// <summary>
        /// 进入时间
        /// </summary>
        protected DateTime? _enterTime;

        /// <summary>
        /// 离开时间
        /// </summary>
        protected DateTime? _leaveTime;

        /// <summary>
        /// 电话号码
        /// </summary>
        protected string _phone;

        /// <summary>
        /// 离场引导人
        /// </summary>
        protected int _leaveGuide;

        /// <summary>
        /// 业务数据_访客记录
        /// </summary>
        public Business_VisitorRecord()
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
        /// 访客记录ID
        /// </summary>
        public int VisitorRecordID
        {
            get { return _visitorRecordID; }
            set { _visitorRecordID = value; }
        }

        /// <summary>
        /// 身份证件号
        /// </summary>
        public string CertID
        {
            get { return _certID; }
            set { _certID = value; }
        }

        /// <summary>
        /// 来访组织
        /// </summary>
        public string VisitOrgization
        {
            get { return _visitOrgization; }
            set { _visitOrgization = value; }
        }

        /// <summary>
        /// 进入时间
        /// </summary>
        public DateTime? EnterTime
        {
            get { return _enterTime; }
            set { _enterTime = value; }
        }

        /// <summary>
        /// 离开时间
        /// </summary>
        public DateTime? LeaveTime
        {
            get { return _leaveTime; }
            set { _leaveTime = value; }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        /// <summary>
        /// 离场引导人
        /// </summary>
        public int LeaveGuide
        {
            get { return _leaveGuide; }
            set { _leaveGuide = value; }
        }

        /// <summary>
        /// 重置业务数据_访客记录
        /// </summary>
        public void Reset()
        {
            _visitorRecordID = 0;
            _visitRecordID = 0;
            _certID = null;
            _visitOrgization = null;
            _enterTime = null;
            _leaveTime = null;
        }

        /// <summary>
        /// 复制业务数据_访客记录
        /// </summary>
        public void AssignFrom(Business_VisitorRecord AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _visitorRecordID = AObj._visitorRecordID;
            _visitRecordID = AObj._visitRecordID;
            _certID = AObj._certID;
            _visitOrgization = AObj._visitOrgization;
            _enterTime = AObj._enterTime;
            _leaveTime = AObj._leaveTime;
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

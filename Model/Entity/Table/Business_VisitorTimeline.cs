using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_访客时间线
    /// </summary>
    public partial class Business_VisitorTimeline
    {
        /// <summary>
        /// 
        /// </summary>
        protected int _visitorTimelineID;

        /// <summary>
        /// 记录时间
        /// </summary>
        protected DateTime _recordTime;

        /// <summary>
        /// 是否离场时间
        /// </summary>
        protected bool _isLeaveTime;

        /// <summary>
        /// 业务数据_访客时间线
        /// </summary>
        public Business_VisitorTimeline()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int VisitorTimelineID
        {
            get { return _visitorTimelineID; }
            set { _visitorTimelineID = value; }
        }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime
        {
            get { return _recordTime; }
            set { _recordTime = value; }
        }

        /// <summary>
        /// 是否离场时间
        /// </summary>
        public bool IsLeaveTime
        {
            get { return _isLeaveTime; }
            set { _isLeaveTime = value; }
        }

        /// <summary>
        /// 重置业务数据_访客时间线
        /// </summary>
        public void Reset()
        {
            _visitorTimelineID = 0;
            _recordTime = DateTime.Now;
            _isLeaveTime = false;
        }

        /// <summary>
        /// 复制业务数据_访客时间线
        /// </summary>
        public void AssignFrom(Business_VisitorTimeline AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _visitorTimelineID = AObj._visitorTimelineID;
            _recordTime = AObj._recordTime;
            _isLeaveTime = AObj._isLeaveTime;
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

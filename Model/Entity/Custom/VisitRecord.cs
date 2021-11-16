using System.Collections.Generic;
using Visitor.Model.Entity.Table;

namespace Visitor.Model.Entity.Custom
{
    /// <summary>
    /// 来访信息
    /// </summary>
    public class VisitRecord : Business_VisitRecord
    {
        /// <summary>
        /// 来访人信息列表
        /// </summary>
        protected List<Visitor> _visitorList;

        /// <summary>
        /// 区域列表
        /// </summary>
        protected List<Data_Area> _dataAreaList;

        /// <summary>
        /// 入场引导人
        /// </summary>
        protected Business_Staff _enterGuideEntity;

        /// <summary>
        /// 来访信息
        /// </summary>
        public VisitRecord() : base()
        {
            _visitorList = new List<Visitor>();
        }

        /// <summary>
        /// 来访人信息列表
        /// </summary>
        public List<Visitor> VisitorList
        {
            get { return _visitorList; }
            set { _visitorList = value; }
        }

        /// <summary>
        /// 区域列表
        /// </summary>
        public List<Data_Area> DataAreaList
        {
            get { return _dataAreaList; }
            set { _dataAreaList = value; }
        }

        /// <summary>
        /// 入场引导人
        /// </summary>
        public Business_Staff EnterGuideEntity
        {
            get { return _enterGuideEntity; }
            set { _enterGuideEntity = value; }
        }

        /// <summary>
        /// 重置来访信息
        /// </summary>
        public new void Reset()
        {
            base.Reset();
            _visitorList.Clear();
            //_visitorRecordList.Clear();
        }

        /// <summary>
        /// 复制来访信息
        /// </summary>
        public void AssignFrom(VisitRecord AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            base.AssignFrom(AObj);
            _visitorList.Clear();
            foreach (var certList in AObj._visitorList)
            {
                var temp = new Visitor();
                temp.AssignFrom(certList);
                _visitorList.Add(temp);
            }
        }
    }
}

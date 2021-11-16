using System.Collections.Generic;
using Visitor.Model.Entity.Table;
using Visitor.Model.Entity.View;

namespace Visitor.Model.Entity.Custom
{

    /// <summary>
    /// 离场人信息
    /// </summary>
    public class LeaveVisitor
    {
        /// <summary>
        /// 离场引导人
        /// </summary>
        protected int _leaveGuide;

        /// <summary>
        /// 离场人信息列表
        /// </summary>
        protected List<Visitor> _visitorList;

        /// <summary>
        /// 离场人信息列表
        /// </summary>
        public List<Visitor> VisitorList
        {
            get { return _visitorList; }
            set { _visitorList = value; }
        }

        /// <summary>
        /// 离场人信息
        /// </summary>
        public LeaveVisitor() : base()
        {
        }

        /// <summary>
        /// 离场引导人
        /// </summary>
        public int LeaveGuide
        {
            get { return _leaveGuide; }
            set { _leaveGuide = value; }
        }

    }
}

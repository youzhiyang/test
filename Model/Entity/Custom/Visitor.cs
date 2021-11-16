using System;
using Visitor.Model.Entity.Table;
using Visitor.Model.Entity.View;

namespace Visitor.Model.Entity.Custom
{

    /// <summary>
    /// 来访人信息
    /// </summary>
    public class Visitor : View_Cert
    {
        /// <summary>
        /// 来访组织
        /// </summary>
        protected string _visitOrgization;

        /// <summary>
        /// 电话号码
        /// </summary>
        protected string _phone;

        /// <summary>
        /// 进入时间
        /// </summary>
        protected DateTime? _enterTime;

        /// <summary>
        /// 离开时间
        /// </summary>
        protected DateTime? _leaveTime;

        /// <summary>
        /// 来访记录ID
        /// </summary>
        protected int _visitRecordID;

        /// <summary>
        /// 离场引导人
        /// </summary>
        protected int _leaveGuide;

        /// <summary>
        /// 离场引导人
        /// </summary>
        protected Business_Staff _leaveGuideEntity;

        /// <summary>
        /// 来访人信息
        /// </summary>
        public Visitor() : base()
        {
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
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
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
        /// 离场引导人
        /// </summary>
        public int LeaveGuide
        {
            get { return _leaveGuide; }
            set { _leaveGuide = value; }
        }

        /// <summary>
        /// 离场引导人
        /// </summary>
        public Business_Staff LeaveGuideEntity
        {
            get { return _leaveGuideEntity; }
            set { _leaveGuideEntity = value; }
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
        /// 重置来访人信息
        /// </summary>
        public new void Reset()
        {
            base.Reset();
            _visitOrgization = null;
        }

        /// <summary>
        /// 复制来访人信息
        /// </summary>
        public void AssignFrom(Visitor AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            base.AssignFrom(AObj);
            _visitOrgization = AObj._visitOrgization;
        }

        /// <summary>
        /// 从写Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Visitor visitor &&
                   _certID == visitor._certID &&
                   _certTypeCode == visitor._certTypeCode &&
                   _certTypeName == visitor._certTypeName &&
                   _name == visitor._name &&
                   _gender == visitor._gender &&
                   _nation == visitor._nation &&
                   _bornDay == visitor._bornDay &&
                   _certAddress == visitor._certAddress &&
                   _certOrg == visitor._certOrg &&
                   _effDate == visitor._effDate &&
                   _expDate == visitor._expDate &&
                   _identityPic == visitor._identityPic &&
                   _picFront == visitor._picFront &&
                   _picBack == visitor._picBack &&
                   CertID == visitor.CertID &&
                   CertTypeCode == visitor.CertTypeCode &&
                   CertTypeName == visitor.CertTypeName &&
                   Name == visitor.Name &&
                   Gender == visitor.Gender &&
                   Nation == visitor.Nation &&
                   BornDay == visitor.BornDay &&
                   CertAddress == visitor.CertAddress &&
                   CertOrg == visitor.CertOrg &&
                   EffDate == visitor.EffDate &&
                   ExpDate == visitor.ExpDate &&
                   IdentityPic == visitor.IdentityPic &&
                   PicFront == visitor.PicFront &&
                   PicBack == visitor.PicBack &&
                   _visitOrgization == visitor._visitOrgization &&
                   _phone == visitor._phone &&
                   _enterTime == visitor._enterTime &&
                   _leaveTime == visitor._leaveTime &&
                   _visitRecordID == visitor._visitRecordID &&
                   VisitOrgization == visitor.VisitOrgization &&
                   Phone == visitor.Phone &&
                   EnterTime == visitor.EnterTime &&
                   LeaveTime == visitor.LeaveTime &&
                   VisitRecordID == visitor.VisitRecordID;
        }
    }
}

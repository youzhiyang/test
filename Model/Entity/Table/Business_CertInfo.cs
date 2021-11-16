using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_身份证件信息
    /// </summary>
    public partial class Business_CertInfo
    {
        /// <summary>
        /// 身份证件号
        /// </summary>
        protected string _certID;

        /// <summary>
        /// 证件类型代码
        /// </summary>
        protected string _certTypeCode;

        /// <summary>
        /// 证件类型
        /// </summary>
        protected string _certTypeName;

        /// <summary>
        /// 姓名
        /// </summary>
        protected string _name;

        /// <summary>
        /// 性别
        /// </summary>
        protected string _gender;

        /// <summary>
        /// 民族
        /// </summary>
        protected string _nation;

        /// <summary>
        /// 出生日期
        /// </summary>
        protected DateTime _bornDay;

        /// <summary>
        /// 证件地址
        /// </summary>
        protected string _certAddress;

        /// <summary>
        /// 发证机关
        /// </summary>
        protected string _certOrg;

        /// <summary>
        /// 起始有效期
        /// </summary>
        protected DateTime _effDate;

        /// <summary>
        /// 截止有效期
        /// </summary>
        protected DateTime _expDate;

        /// <summary>
        /// 业务数据_身份证件信息
        /// </summary>
        public Business_CertInfo()
        {
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
        /// 证件类型代码
        /// </summary>
        public string CertTypeCode
        {
            get { return _certTypeCode; }
            set { _certTypeCode = value; }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertTypeName
        {
            get { return _certTypeName; }
            set { _certTypeName = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get { return _nation; }
            set { _nation = value; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BornDay
        {
            get { return _bornDay; }
            set { _bornDay = value; }
        }

        /// <summary>
        /// 证件地址
        /// </summary>
        public string CertAddress
        {
            get { return _certAddress; }
            set { _certAddress = value; }
        }

        /// <summary>
        /// 发证机关
        /// </summary>
        public string CertOrg
        {
            get { return _certOrg; }
            set { _certOrg = value; }
        }

        /// <summary>
        /// 起始有效期
        /// </summary>
        public DateTime EffDate
        {
            get { return _effDate; }
            set { _effDate = value; }
        }

        /// <summary>
        /// 截止有效期
        /// </summary>
        public DateTime ExpDate
        {
            get { return _expDate; }
            set { _expDate = value; }
        }

        /// <summary>
        /// 重置业务数据_身份证件信息
        /// </summary>
        public void Reset()
        {
            _certID = null;
            _certTypeCode = null;
            _certTypeName = null;
            _name = null;
            _gender = null;
            _nation = null;
            _bornDay = DateTime.Now;
            _certAddress = null;
            _certOrg = null;
            _effDate = DateTime.Now;
            _expDate = DateTime.Now;
        }

        /// <summary>
        /// 复制业务数据_身份证件信息
        /// </summary>
        public void AssignFrom(Business_CertInfo AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _certID = AObj._certID;
            _certTypeCode = AObj._certTypeCode;
            _certTypeName = AObj._certTypeName;
            _name = AObj._name;
            _gender = AObj._gender;
            _nation = AObj._nation;
            _bornDay = AObj._bornDay;
            _certAddress = AObj._certAddress;
            _certOrg = AObj._certOrg;
            _effDate = AObj._effDate;
            _expDate = AObj._expDate;
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

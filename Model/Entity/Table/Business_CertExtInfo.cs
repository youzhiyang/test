using System;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_身份证件扩展信息
    /// </summary>
    public partial class Business_CertExtInfo
    {
        /// <summary>
        /// 身份证件信息ID
        /// </summary>
        protected string _certID;

        /// <summary>
        /// 证件照片
        /// </summary>
        protected string _identityPic;

        /// <summary>
        /// 证件正面照片
        /// </summary>
        protected string _picFront;

        /// <summary>
        /// 证件背面照片
        /// </summary>
        protected string _picBack;

        /// <summary>
        /// 业务数据_身份证件扩展信息
        /// </summary>
        public Business_CertExtInfo()
        {
        }

        /// <summary>
        /// 身份证件信息ID
        /// </summary>
        public string CertID
        {
            get { return _certID; }
            set { _certID = value; }
        }

        /// <summary>
        /// 证件照片
        /// </summary>
        public string IdentityPic
        {
            get { return _identityPic; }
            set { _identityPic = value; }
        }

        /// <summary>
        /// 证件正面照片
        /// </summary>
        public string PicFront
        {
            get { return _picFront; }
            set { _picFront = value; }
        }

        /// <summary>
        /// 证件背面照片
        /// </summary>
        public string PicBack
        {
            get { return _picBack; }
            set { _picBack = value; }
        }

        /// <summary>
        /// 重置业务数据_身份证件扩展信息
        /// </summary>
        public void Reset()
        {
            _certID = null;
            _identityPic = null;
            _picFront = null;
            _picBack = null;
        }

        /// <summary>
        /// 复制业务数据_身份证件扩展信息
        /// </summary>
        public void AssignFrom(Business_CertExtInfo AObj)
        {
            if (AObj == null)
            {
                Reset();
                return;
            }
            _certID = AObj._certID;
            _identityPic = AObj._identityPic;
            _picFront = AObj._picFront;
            _picBack = AObj._picBack;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Model.Entity.Table
{
    /// <summary>
    /// 业务数据_员工信息
    /// </summary>
    public partial class Business_Staff
    {
        /// <summary>
        /// 员工id
        /// </summary>
        protected string _staffID;

        /// <summary>
        /// 员工名
        /// </summary>
        protected string _staffName;

        /// <summary>
        /// 所属公司
        /// </summary>
        protected string _staffCompany;

        /// <summary>
        /// 员工工号
        /// </summary>
        protected string _staffNumber;

        /// <summary>
        /// 岗位
        /// </summary>
        protected string _staffJob;

        /// <summary>
        /// 性别
        /// </summary>
        protected string _staffSex;

        /// <summary>
        /// 入职日期
        /// </summary>
        protected string _staffHireDate;

        /// <summary>
        /// 家庭住址
        /// </summary>
        protected string _staffAddr;

        /// <summary>
        /// 业务数据_员工信息
        /// </summary>
        public Business_Staff()
        { 

        }

        /// <summary>
        /// 员工id
        /// </summary>
        public string StaffID
        {
            get { return _staffID; }
            set { _staffID = value; }
        }

        /// <summary>
        /// 员工名
        /// </summary>
        public string StaffName
        {
            get { return _staffName; }
            set { _staffName = value; }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string StaffCompany
        {
            get { return _staffCompany; }
            set { _staffCompany = value; }
        }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string StaffNumber
        {
            get { return _staffNumber; }
            set { _staffNumber = value; }
        }

        /// <summary>
        /// 岗位
        /// </summary>
        public string StaffJob
        {
            get { return _staffJob; }
            set { _staffJob = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string StaffSex
        {
            get { return _staffSex; }
            set { _staffSex = value; }
        }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string StaffHireDate
        {
            get { return _staffHireDate; }
            set { _staffHireDate = value; }
        }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public string StaffAddr
        {
            get { return _staffAddr; }
            set { _staffAddr = value; }
        }

    }
}

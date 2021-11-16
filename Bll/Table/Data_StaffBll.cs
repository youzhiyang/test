using Newtonsoft.Json;
using System;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Table
{
    /// <summary>
    /// 员工相关业务
    /// </summary>
    public class Data_StaffBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly Data_StaffBll Instance = new Data_StaffBll();

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns></returns>
        public ResponseData GetStaff()
        {
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Business_StaffDal().GetBusiness_Staff<Business_Staff>(conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }

        /// <summary>
        /// 根据id员工信息
        /// </summary>
        /// <returns></returns>
        public ResponseData GetStaffById()
        {
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Business_StaffDal().GetBusiness_Staff<Business_Staff>(conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }
    }
}

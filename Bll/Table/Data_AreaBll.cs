using Newtonsoft.Json;
using System;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Table
{
    /// <summary>
    /// 区域相关业务
    /// </summary>
    public class Data_AreaBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly Data_AreaBll Instance = new Data_AreaBll();

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public ResponseData GetAreas()
        {
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Data_AreaDal().GetAreas<Data_Area>(conn)) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }
    }
}

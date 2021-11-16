using DingTalkService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Visitor.Bll;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll.Business
{
    /// <summary>
    /// 来访相关业务
    /// </summary>
    public class VisitBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly VisitBll Instance = new VisitBll();

        /// <summary>
        /// 新增/更新来访信息列表
        /// </summary>
        /// <param name="visitRecords">来访信息列表</param>
        /// <returns></returns>
        public ResponseData UpsertVisits(IEnumerable<VisitRecord> visitRecords)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    var certDal = new Business_CertInfoDal();
                    var certExtDal = new Business_CertExtInfoDal();
                    var visitorDal = new Business_VisitorRecordDal();
                    var visitRecordUpsertCount = 0;
                    var certUpsertCount = 0;
                    var visitorUpsertCount = 0;
                    foreach (var visit in visitRecords)
                    {
                        //记录登记信息
                        visit.VisitRecordID = new Business_VisitRecordDal().UpsertBusiness_VisitRecord(visit, conn, tran);
                        if (visit.VisitRecordID <= 0)
                        {
                            tran.Rollback();
                            data.errcode = -2;
                            data.errmsg = "保存失败。";
                            return data;
                        }
                        visitRecordUpsertCount++;
                        //添加/更新身份证件信息
                        certUpsertCount += certDal.UpsertBusiness_CertInfos(visit.VisitorList, conn, tran);
                        certUpsertCount += certExtDal.UpsertBusiness_CertExtInfos(visit.VisitorList, conn, tran);
                        certUpsertCount -= visit.VisitorList.Count * 2;
                        //记录来访人员
                        var visitorRecords = new List<Business_VisitorRecord>();
                        foreach (var visitor in visit.VisitorList)
                        {
                            var visitorRecord = new Business_VisitorRecord()
                            {
                                VisitRecordID = visit.VisitRecordID,
                                CertID = visitor.CertID,
                                VisitOrgization = visitor.VisitOrgization,
                                Phone = visitor.Phone
                            };
                            visitorRecords.Add(visitorRecord);
                            visitorRecord.VisitorRecordID = visitorDal.UpsertBusiness_VisitorRecord(visitorRecord, conn, tran);
                            if (visitorRecord.VisitorRecordID <= 0)
                            {
                                tran.Rollback();
                                data.errcode = -2;
                                data.errmsg = "保存失败。";
                                return data;
                            }
                            visitorUpsertCount++;
                            //更新进入时间
                            visitorUpsertCount -= visitorDal.UpdateEnterTime(visitorRecord, conn, tran);
                        }
                    }
                    if ((visitRecordUpsertCount != visitRecords.Count()) && (certUpsertCount != 0) && (visitorUpsertCount != 0))
                    {
                        tran.Rollback();
                        data.errcode = -2;
                        data.errmsg = "保存失败。";
                    }
                    else
                    {
                        tran.Commit();
                        data.errcode = 0;
                        data.errmsg = "保存成功。";
                    }
                }
                //钉钉客户端访客通知
                ResponseData responseData = DingTalkBll.Instance.SendEnterVisitorNotify(visitRecords);
                if (responseData.errcode != 0)
                {
                    data.errcode = responseData.errcode;
                    data.errmsg = "通知钉钉客户端失败" + responseData.errmsg;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                data.errcode = -1;
                data.errmsg = "保存失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 新增/更新离场信息列表
        /// </summary>
        /// <param name="certs">身份认证信息列表</param>
        /// <returns></returns>
        public ResponseData UpsertLeaves(LeaveVisitor leaveVisitor)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    foreach (var visitor in leaveVisitor.VisitorList)
                    {
                        //获取最新访客记录
                        var visitorInfo = new Business_VisitorRecordDal().GetBusiness_VisitorRecords<Business_VisitorRecord>(visitor.CertID, conn, tran);
                        if (visitorInfo == null)
                        {
                            tran.Rollback();
                            data.errcode = -2;
                            data.errmsg = "您没有登记记录";
                            return data;
                        }
                        visitorInfo.LeaveGuide = leaveVisitor.LeaveGuide;
                        //更新访客离开时间
                        var visitDelCount = new Business_VisitorRecordDal().UpdateLeaveTime(visitorInfo, conn, tran);
                        if (visitDelCount == 0)
                        {
                            tran.Rollback();
                            data.errcode = -2;
                            data.errmsg = "更新访客离开时间失败。";
                            return data;
                        }
                    }
                    tran.Commit();
                    data.errcode = 0;
                    data.errmsg = "离场登记成功。";

                    //钉钉客户端访客通知
                    ResponseData responseData = DingTalkBll.Instance.SendLeaveVisitorNotify(leaveVisitor);
                    if (responseData.errcode != 0)
                    {
                        data.errcode = responseData.errcode;
                        data.errmsg = "通知钉钉客户端失败" + responseData.errmsg;
                    }
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "更新离场信息失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 新增/更新访客时间线列表
        /// </summary>
        /// <param name="visitorTimelines">访客时间线列表</param>
        /// <returns></returns>
        public ResponseData UpsertVisitorTimelines(IEnumerable<Business_VisitorTimeline> visitorTimelines)
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    var tran = conn.BeginTransaction();
                    var visitTimelineUpsertCount = new Business_VisitorTimelineDal().UpsertBusiness_VisitorTimelines(visitorTimelines, conn, tran);
                    if (visitTimelineUpsertCount != visitorTimelines.Count())
                    {
                        tran.Rollback();
                        data.errcode = -2;
                        data.errmsg = "保存失败。";
                    }
                    else
                    {
                        tran.Commit();
                        data.errcode = 0;
                        data.errmsg = "保存成功。";
                    }
                }
            }
            catch (Exception ex)
            {
                data.errcode = -1;
                data.errmsg = "保存失败。" + ex.Message;
            }
            return data;
        }

        /// <summary>
        /// 获取最新访客记录
        /// </summary>
        /// <returns></returns>
        public ResponseData GetBusiness_VisitRecord()
        {
            var data = new ResponseData();
            try
            {
                using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
                {
                    return new ResponseData() { DataJSON = JsonConvert.SerializeObject(new Business_VisitRecordDal().GetBusiness_VisitRecord(conn, orderBy:"DESC")) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData() { errcode = -1, errmsg = "读取数据失败。" + ex.Message };
            }
        }
    }
}

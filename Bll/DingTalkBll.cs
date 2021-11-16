using DingTalkService;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Visitor.Dal;
using Visitor.Dal.Table;
using Visitor.Model.Entity.Custom;
using Visitor.Model.Entity.Table;

namespace Visitor.Bll
{
    public class DingTalkBll
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static readonly DingTalkBll Instance = new DingTalkBll();

        /// <summary>
        /// 钉钉客户端访客通知
        /// </summary>
        public ResponseData SendEnterVisitorNotify(IEnumerable<VisitRecord> visitRecords)
        {
            foreach (var visitRecord in visitRecords)
            {
                string message = getEnterMessage(visitRecord);
                var robotConversation = new DingTalkService.Models.RobotConversation();
                robotConversation.msgtype = robotConversation._msgtype.Markdown;
                robotConversation.markdown.title = "访客通知";
                robotConversation.markdown.text = message;
                DingTalkService.Models.ResponseData responseData = DingTalkAPIService.Instance.SendVisitorNotify(robotConversation);
                return new ResponseData() { errcode = responseData.errcode, errmsg = responseData.errmsg };
            }
            return null;
        }

        /// <summary>
        /// 获取来访消息
        /// </summary>
        public string getEnterMessage(VisitRecord visitRecord)
        {
            var visitMsg = "";
            var areasStr = "";
            var reason = "";
            var deviceTools = "";
            var count = 0;
            List<Model.Entity.Custom.Visitor> visitorList = visitRecord.VisitorList;
            foreach (var visit in visitorList)
            {
                var visitOrgization = visit.VisitOrgization;
                if (count == visitorList.Count - 1)
                {
                    visitMsg += visit.Name + "   " + visitOrgization;
                }
                else
                {
                    visitMsg += visit.Name + "   " + visitOrgization + "  \n  ## ";
                    count++;
                }
            }
            List<Data_Area> areas;
            Business_Staff staff;
            using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
            {
                int[] ids = JsonConvert.DeserializeObject<int[]>(visitRecord.Areas);
                areas = (List<Data_Area>)new Data_AreaDal().GetAreasByIds<Data_Area>(ids, conn);
                staff = new Business_StaffDal().GetBusiness_StaffById<Business_Staff>(visitRecord.EnterGuide, conn);
            }
            count = 0;
            foreach (var area in areas)
            {
                if (count == areas.Count - 1)
                {
                    areasStr += area.Name.Trim();
                }
                else
                {
                    areasStr += area.Name.Trim() + "、";
                    count++;
                }
            }
            if (visitRecord.ReasonCode == "Other")
            {
                reason = visitRecord.Reason;
            }
            else
            {
                reason = visitRecord.ReasonName;
            }
            if (!string.IsNullOrEmpty(visitRecord.DeviceNTools))
            {
                deviceTools += "  \n  ### 携带工具:  \n  ## " + visitRecord.DeviceNTools;
            }
            string time = DateTime.Now.ToString();
            string message = "### <center>                    访客通知</center>  \n  ### 来访时间：  \n  ## " + time + "  \n  ### 来访事由：  \n  ## "+ reason + "  \n  ### 访问区域：  \n  ## "+ areasStr + deviceTools + "  \n  ### 来访人员：  \n  ## " + visitMsg + "  \n  ### 引导人：  \n  ## " + staff.StaffName + "";
            return message;
        }

        /// <summary>
        /// 钉钉客户端离开通知
        /// </summary>
        public ResponseData SendLeaveVisitorNotify(LeaveVisitor leaveVisitor)
        {
            string message = getLeaveMessage(leaveVisitor);
            var robotConversation = new DingTalkService.Models.RobotConversation();
            robotConversation.msgtype = robotConversation._msgtype.Markdown;
            robotConversation.markdown.title = "访客通知";
            robotConversation.markdown.text = message;
            DingTalkService.Models.ResponseData responseData = DingTalkAPIService.Instance.SendVisitorNotify(robotConversation);
            return new ResponseData() { errcode = responseData.errcode, errmsg = responseData.errmsg };
        }

        /// <summary>
        /// 获取离场消息
        /// </summary>
        public string getLeaveMessage(LeaveVisitor leaveVisitor)
        {
            Business_Staff staff;
            var visitMsg = "";
            var count = 0;
            using (var conn = DbFactory.CreateConnection(BllParams.ConnString))
            {
                staff = new Business_StaffDal().GetBusiness_StaffById<Business_Staff>(leaveVisitor.LeaveGuide, conn);
            }
            foreach (var visitor in leaveVisitor.VisitorList)
            {
                var visitOrgization = visitor.VisitOrgization;
                if (count == leaveVisitor.VisitorList.Count - 1)
                {
                    visitMsg += visitor.Name + "   " + visitOrgization;
                }
                else
                {
                    visitMsg += visitor.Name + "   " + visitOrgization + "  \n  ## ";
                    count++;
                }
            }
            string time = DateTime.Now.ToString();
            string message = "### <center>                    访客通知</center>  \n  ### 离开时间:  \n  ## " + time + "  \n  ### 离开人员:  \n  ## " + visitMsg + "  \n  ### 引导人:  \n  ## " + staff.StaffName;
            return message;
        }
    }
}

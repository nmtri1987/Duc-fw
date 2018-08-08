using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RBVH.HR.Models;

namespace RBVH.HR.Controllers.Report
{
    public class ReportController : ApiController
    {
        public MissingScanTimeCollection Post(MissCanTimePara objPara)
        {
            return MissingScanTimeManager.GetMissingScanTime(objPara);
        }

        public System.Data.DataTable Post(EMPTMSPara objPara,string Event)
        {
            return MissingScanTimeManager.EmployeeTMSReport(objPara);
        }
        public REmployeeCollections Post(REmployeePara objPara,string Event,string Para)
        {
            return REmployeeManager.GetReminderList(objPara);
        }
    }
}

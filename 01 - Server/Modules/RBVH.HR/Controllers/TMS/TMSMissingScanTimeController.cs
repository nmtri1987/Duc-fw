using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RBVH.HR.Models;
namespace Biz.TMS.Controllers
{
    public class TMSMissingScanTimeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public TMSMissingScanTimeCollection Get(int EmployeeCode, DateTime StartDate, DateTime EndDate, int EntityID)
        {
            return TMSMissingScanTimeManager.GetMissingData(EmployeeCode,StartDate,EndDate,EntityID);
        }

      
    }
}
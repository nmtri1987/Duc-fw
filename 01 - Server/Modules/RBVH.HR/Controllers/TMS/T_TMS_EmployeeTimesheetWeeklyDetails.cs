
using Biz.TMS.Models;
using System.Web.Http;
using System.Collections.Generic;
namespace Biz.TMS.Controllers
{
    public class T_TMS_EmployeeTimesheetWeeklyDetailsController : BaseApi<T_TMS_EmployeeTimesheetWeeklyDetails>
    {
        //public IEnumerable<T_TMS_EmployeeTimesheetWeeklyDetails> Post(string method, [FromBody] EMPWeelFilter value)
        //{
        //    return T_TMS_EmployeeTimesheetWeeklyDetailsManager.Search(value);
        //}
        public System.Data.DataTable Post(string method, [FromBody] EMPWeelFilter value, string action)
        {
            return T_TMS_EmployeeTimesheetWeeklyDetailsManager.EmployeeWeeklyReport(value);
        }
    }
}
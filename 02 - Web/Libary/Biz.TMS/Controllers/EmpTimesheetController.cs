using System.Web.Mvc;
using Biz.TMS.Services;
using DataTables.Mvc;
using Newtonsoft.Json;
namespace Biz.TMS.Controllers
{
    using Core.Models;

    public class EmpTimesheetController : BaseController<T_TMS_EmployeeDailyTimesheetTransaction>
    {
        public override ContentResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(1, requestModel, "EmployeeCode", "EmployeeCode");
            SearchKey.Condition = " tbl.EmployeeCode = " + CurrentUser.EmployeeCode + " ";
            SearchKey.OrderBy = "DateID";
            SearchKey.OrderDirection = "Desc";
            T_TMS_EmployeeDailyTimesheetTransactionCollection collection = T_TMS_EmployeeDailyTimesheetTransactionManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, collection, TotalRecord, TotalRecord)), "application/json");
        }
    }
}
namespace Biz.PDM.Controllers
{
    using System.Web.Mvc;
    using Core.Models;
    using Core.Security;
    using DataTables.Mvc;
    using LV.TMS.Models;
    using LV.TMS.Services;
    using Newtonsoft.Json;
    using System;

    [CustomAuthorize]
    [AccessRights("ScanTimeApproval")]
    public class ApproveTimeSheetController : BaseController
    {
        string ViewFolder = "~/Views/LV/TMS/ScanTimeApproval/";
        public ActionResult Index()
        {
            return View(ViewFolder + "Index.cshtml");
        }

        public ActionResult List()
        {
            SearchParams prams = new SearchParams();
            prams.EndDate = SystemConfig.CurrentDate;
           prams.StartDate= SystemConfig.CurrentDate.AddDays(-30);
            return View(ViewFolder + "List.cshtml",prams);
        }

        public JsonResult GetData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            SearchFilter searchFilter = ScanTimeApprovalManager.SearchData(requestModel);
            ScanTimeApprovalSqlParameters parameters = new ScanTimeApprovalSqlParameters();

            parameters.FromDate = SystemConfig.CurrentDate.AddDays(-30);
            parameters.ToDate = SystemConfig.CurrentDate;
            parameters.UserLoggedIn = CustomerAuthorize.CurrentUser.EmployeeCode;
            parameters.ShowWaiting = true;
            parameters.StartRow = (requestModel.Start / requestModel.Length) + 1;
            parameters.EndRow = requestModel.Length;
            parameters.OrderBy = string.IsNullOrEmpty(searchFilter.OrderBy) ? "EmployeeNo" : searchFilter.OrderBy;
            parameters.OrderDirection = searchFilter.OrderDirection == "ASC" ? 1 : 0;
            parameters.FilterBy = requestModel.Search.Value;
            parameters.ShowUnNoReg = false ;
            parameters.Condition = requestModel.Search.Value;

            ScanTimeApprovalCollection collection = ScanTimeApprovalManager.GetAll(parameters);
            int totalRecord = 0;
            if (collection.Count > 0)
            {
                totalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, totalRecord, totalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {

            SearchFilter searchFilter = ScanTimeApprovalManager.SearchData(requestModel);
            ScanTimeApprovalSqlParameters parameters = new ScanTimeApprovalSqlParameters();
            var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
            SearchParams param = new SearchParams();

            if (!string.IsNullOrEmpty(searchprm))
            {
                param = JsonConvert.DeserializeObject<SearchParams>(searchprm, dateTimeConverter);
            }

            parameters.FromDate = param.StartDate ?? SystemConfig.CurrentDate.AddDays(-30);
            parameters.ToDate = param.EndDate ?? SystemConfig.CurrentDate;
            parameters.UserLoggedIn = CustomerAuthorize.CurrentUser.EmployeeCode;
            parameters.ShowWaiting = param.ShowMissing == "on";
            parameters.StartRow = (requestModel.Start / requestModel.Length) + 1;
            parameters.EndRow = requestModel.Length;
            parameters.OrderBy = string.IsNullOrEmpty(searchFilter.OrderBy) ? "EmployeeNo" : searchFilter.OrderBy;
            parameters.OrderDirection = searchFilter.OrderDirection == "ASC" ? 1 : 0;
            parameters.FilterBy = parameters.FilterBy = requestModel.Search.Value;
            parameters.ShowUnNoReg = false;

            ScanTimeApprovalCollection collection = ScanTimeApprovalManager.GetAll(parameters);
            int totalRecord = 0;
            if (collection.Count > 0)
            {
                totalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, totalRecord, totalRecord), JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult HeaderLink()
        {
            string columnName = Biz.Core.CommonHelper.JsonColumnType<ScanTimeApproval>();

            return this.Content(columnName, "application/json");
        }

        [HttpPost]
        public JsonResult ButtonSubmitEvent(ScanTimeApprovalCollection objItem, string Event)
        {
            var result = 0;
            var timesheetCollection = new TimesheetEntryCollection();
            TimesheetApproveReject timesheetApproveReject = new TimesheetApproveReject();
           
            foreach (ScanTimeApproval item in objItem)
            {
                if (item.isSelect == "on")
                {
                    var timesheetEntry = new TimesheetEntry()
                    {
                        EmployeeCode = item.EmployeeCode,
                        WorkDate = item.WorkDate,
                        ManualIn = item.ManualIn,
                        ManualOut = item.ManualOut,
                        RequestorNote = item.RequestorNote,
                        ApproverNote = item.ApproverNote,
                    };

                    timesheetCollection.Add(timesheetEntry);
                }

            }
            switch (Event)
            {
                case "approve":                   
                    timesheetApproveReject.IsApproved = true;
                    break;
                case "reject":                  
                    timesheetApproveReject.IsApproved = false;
                    break;

            }
            timesheetApproveReject.TimesheetEntryCollection = timesheetCollection;
            timesheetApproveReject.UserLoggedIn = CurrentUser.EmployeeCode;
            if (timesheetCollection.Count > 0)
            {
                result = ScanTimeApprovalManager.Update(timesheetApproveReject);
            }
            

            return Json(new
            {
                objItem,
                Event,
                result,
                JsonRequestBehavior.AllowGet
            });
        }
    }
}

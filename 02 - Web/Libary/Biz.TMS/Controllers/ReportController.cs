using Biz.TMS.Models;
using Biz.TMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz.Core.Security;
using Biz.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DataTables.Mvc;
using System.Data;
using Helpers;
using Biz.Core.Helpers;
using Biz.Core.Converts;
using ClosedXML.Excel;

namespace Biz.TMS.Controllers
{
    public class ReportController :BaseController
    {
        string ViewFolder = "~/Views/Report/";
        public ActionResult Index()
        {
            return View(ViewFolder + "MissScanTime/Index.cshtml");
        }
        public ActionResult list()
        {
            //T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.GetAll();
            return View(ViewFolder + "MissScanTime/list.cshtml");
        }

        public ActionResult EmpTMS()
        {
            return View(ViewFolder + "EmpTMS.cshtml");
        }

        public ActionResult Info(int EmployeeCode,bool? ajax)
        {
            return RedirectToAction("Info", "RBVHEmployee",new  { EmployeeCode=EmployeeCode, ajax= true });
            //RBVHEmployee objItem = Biz.Core.Services.RBVHEmployeeManager.GetById(EmployeeCode);
            //return View("~/Views/PDM/RBVHEmployee/Info.cshtml", objItem);
        }

        public ActionResult headerLink()
        {
            string ColumnName = Biz.Core.CommonHelper.JsonColumnType<MissingScanTime>();
            return Content(ColumnName, "application/json");
        }

        //public ContentResult Get(string EmployeeNo, string WD)
        //{
           

            
        //    return Content(JsonConvert.SerializeObject(collection), "application/json");
        //    //return View(ViewFolder + "EmpTms.cshtml");
        //}
        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
         
            MissCanTimePara b = new MissCanTimePara()
            {
                StartDate = DateTime.Now.AddDays(-31),
                
                EndDate = DateTime.Now,
                EmployeeCode = 0,
                EntityID = 10001
            };
            MissingScanTimeCollection collection = ReportManager.GetExMonthlyReport(b);
            //    SearchFilter SearchKey = SearchFilter.SearchData(1, requestModel, "Id", "Id");
            //    T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.Search(SearchKey);
            //    int TotalRecord = 0;
            //    if (collection.Count > 0)
            //    {
            //        TotalRecord = collection[0].TotalRecord;
            //    }
            return Json(new DataTablesResponse(requestModel.Draw, collection, collection.Count, collection.Count), JsonRequestBehavior.AllowGet);
        }
        public ContentResult GetEmpTMS([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)//, int EntityID = 10002,  DateTime? fromdate = null, DateTime? todate = null)
        {
            
            EMPTMSPara ObjPara = JsonConvert.DeserializeObject<EMPTMSPara>(searchprm, 
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "dd/MM/yyyy"
                });
            //EMPTMSPara SearchKey = new EMPTMSPara()
            //{
            //    EntityID = 10002,
            //    DeptID = 0,
            //    @FromDate = fromdate ,
            //    @ToDate = todate,
            //    OrderBy = "EmployeeNo",
            //    OrderDirection = "DESC",
            //    Page = (requestModel.Start / requestModel.Length) + 1,
            //    PageSize = requestModel.Length,
            //};
            //fromdate = fromdate == null ? SystemConfig.CurrentDate : fromdate;
            //todate = todate == null ? SystemConfig.CurrentDate : todate;
            ObjPara.OrderBy = "DeptName";
            ObjPara.OrderDirection = "ASC";
            ObjPara.Page = (requestModel.Start / requestModel.Length) + 1;
                ObjPara.PageSize = requestModel.Length;
            DataTable collection = new DataTable();
            collection = ReportManager.EmployeeTMSSummaryReport(ObjPara);
            int TotalRecord = 0;
            if (collection.Rows.Count > 0)
            {
                TotalRecord = Convert.ToInt32(collection.Rows[0]["TotalRecord"]);

            }

            IEnumerable<DataRow> rows = collection.AsEnumerable().ToList();
            object temp = new object();
            foreach (var item in rows)
            {
                temp = item.Table;
            }
            //DataTablesResponseExtend results = new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord);
            return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord)), "application/json");
        }
        [CustomAuthorize]
        public void TMSEmployeeExportExcel(string searchprm, int PageSize)
        {
            EMPTMSPara ObjPara = JsonConvert.DeserializeObject<EMPTMSPara>(searchprm,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore,
                   DateFormatString = "dd/MM/yyyy"
               });

            ObjPara.OrderBy = "EmployeeNo";
            ObjPara.OrderDirection = "Desc";
            ObjPara.Page = 1;
            ObjPara.PageSize = PageSize;
            DataTable dt = new DataTable();
            dt = ReportManager.EmployeeTMSSummaryReport(ObjPara);
            string fileName = "TMS_Employee_Summary_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            List<ExcelCellModel> listd = new List<ExcelCellModel>();

            ExcelCellModel ex = new ExcelCellModel();
            DateTime mytime = ObjPara.FromDate.Value;
            List<int> columnIndex = new List<int>();
            int b = 9;
            for (DateTime day = ObjPara.FromDate.Value; day < ObjPara.ToDate.Value; day = day.AddDays(1.0))
            {
                if (BDatetime.isWeekDay(day))
                {
                    columnIndex.Add(b);
                }
                b = b+1;
            }
         
            ex.BackgroundColorInfo.ColunmIndex = columnIndex;
            ex.BackgroundColorInfo.BackgorundColor= XLColor.FromHtml("#FFFFCC");
            ex.BackgroundColorInfo.FontColor = XLColor.Black;
            //ex. = XLColor.Black;// CellValue(0,PageSize)

            listd.Add(ex);
            FileInputHelper.ExportExcel(dt, fileName, "TMS Employee Summary", true, listd);
          //  return fileName;
        }
        public string ExportExcel()
        {
            MissCanTimePara b = new MissCanTimePara()
            {
                StartDate = DateTime.Now.AddDays(-31),

                EndDate = DateTime.Now,
                EmployeeCode = 0
            };
            MissingScanTimeCollection collection = ReportManager.GetExMonthlyReport(b);
            DataTable dt = collection.ToDataTable<MissingScanTime>();
            string fileName = "RBVHEmployee_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            //set Column Date Format 
            int[] DateColumn = { 8,9,10,11 };
            string[] sheetName = { "Daily_TMS" };
            ExcelPara mypara = new ExcelPara()
            {
                ds = ds,
                sheetName = sheetName,
                fileName = fileName,
                DateColumns = DateColumn,
                DateFormat = "h:mm:ss"
            };
            FileInputHelper.ExportMultiSheetExcelExtend(mypara);
            FileInputHelper.ExportExcel(dt, fileName, "RBVHEmployee List", false);
            return fileName;
        }
    }
}

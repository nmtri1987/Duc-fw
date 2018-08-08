using Biz.Core.Converts;
using Biz.Core.Helpers;
using Biz.Core.Models;
using Biz.Core.Security;
using DataTables.Mvc;
using Helpers;
using LV.TMS.Models;
using LV.TMS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
namespace LV.TMS.Controllers
{
    [CustomAuthorize]
    [AccessRights("hrm_atd_ScanTime")]
    public class TimesheetController : BaseController
    {
        private string ViewFolder = "~/Views/LV/TMS/hrm_atd_ScanTime/";

        public ActionResult Index()
        {
            return View(ViewFolder + "Index.cshtml");
        }

        public ActionResult EmpTms(int EmployeeCode)
        {
            return View(ViewFolder + "EmpTms.cshtml");
        }

        public ActionResult list()
        {
            //hrm_atd_ScanTimeCollection collection = hrm_atd_ScanTimeManager.GetAll();
            return View(ViewFolder + "list.cshtml");
        }

        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
            SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "ID" : SearchKey.OrderBy;
            hrm_atd_ScanTimeCollection collection = hrm_atd_ScanTimeManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public JsonResult GetEmployeeGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int EmployeeCode)
        {
            ScanTimeFilter SearchKey = SearchData(1, requestModel, "EmployeeNo", "EmployeeNo");
            //SearchKey.Condition = "employeeID = " + EmployeeCode + " ";
            hrm_atd_ScanTimeCollection collection = hrm_atd_ScanTimeManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public static ScanTimeFilter SearchData(int CompanyID, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,
           string Col, string OrderByDefault, string strCondition = "", string OrderDirection = "ASC")
        {
            ColumnCollection col = requestModel.Columns;
            string OrderBy = OrderByDefault;

            foreach (Column item in col)
            {
                //if (item.IsOrdered && item.Data!="EmpPoint")
                if (item.IsOrdered)
                {
                    OrderBy = item.Data;
                    OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                    break;
                }
            }

            return new ScanTimeFilter()
            {
                CompanyID = CompanyID,
                Keyword = requestModel.Search.Value,
                Page = (requestModel.Start / requestModel.Length) + 1,
                PageSize = requestModel.Length,
                ColumnsName = Col,
                OrderBy = OrderBy,
                OrderDirection = OrderDirection,
                Condition = strCondition
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            ScanTimeFilter SearchKey = SearchData(1, requestModel, "EmployeeNo", "EmployeeNo");
            //SearchKey.FromDate = SystemConfig.CurrentDate.AddDays(-30);
            //SearchKey.ToDate = SystemConfig.CurrentDate;
            SearchKey.FromDate = SearchKey.FromDate;
            SearchKey.FromDate = SearchKey.ToDate;
            SearchKey.EmployeeCode = CurrentUser.EmployeeCode;
            // SearchKey.Condition = " tbl.employeeID = " + CurrentUser.EmployeeCode + " ";
            // SearchKey.OrderBy = "Scan_Time";
            // SearchKey.OrderDirection = "Desc";
            hrm_atd_ScanTimeCollection collection = hrm_atd_ScanTimeManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            ScanTimeFilter ObjPara = new ScanTimeFilter();
            ObjPara.FromDate = SystemConfig.CurrentDate.AddDays(-30);
            ObjPara.ToDate = SystemConfig.CurrentDate;
            //SearchKey.FromDate = SystemConfig.CurrentDate.AddDays(-30);
            //SearchKey.ToDate = SystemConfig.CurrentDate;

            if (!string.IsNullOrEmpty(searchprm))
            {
                 ObjPara = JsonConvert.DeserializeObject<ScanTimeFilter>(searchprm,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "dd/MM/yyyy"
                    });
            }
            //var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
            //TimeSheetearchpara ObjPara = JsonConvert.DeserializeObject<TimeSheetearchpara>(searchprm,dateTimeConverter);
            //ScanTimeFilter SearchKey = ScanTimeFilter.SearchData(1, requestModel, "EmployeeID", "EmployeeID");
            
            //SearchKey.OrderBy = "WorkDate";
            //SearchKey.OrderDirection = "Desc";

            ObjPara.OrderBy = "WorkDate";
            ObjPara.OrderDirection = "Desc";
            ObjPara.Page = (requestModel.Start / requestModel.Length) + 1;
            ObjPara.PageSize = requestModel.Length;
            ObjPara.Condition = "";
            ObjPara.EmployeeCode = CurrentUser.EmployeeCode;
            ObjPara.CompanyID = CurrentUser.CompanyID;

            hrm_atd_ScanTimeCollection collection  = hrm_atd_ScanTimeManager.Search(ObjPara);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //return Json(new DataTablesResponse(requestModel.Draw,null , TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for scrolling page
        /// </summary>
        /// <returns></returns>
        public ContentResult GetPg(int page, int pagesize)
        {
            string condition = "";
            SearchFilter SearchKey = SearchFilter.SearchPG(1, page, pagesize, "EmployeeNo", "EmployeeNo", "Desc", condition);
            hrm_atd_ScanTimeCollection objItem = hrm_atd_ScanTimeManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult headerLink()
        {
            string ColumnName = Biz.Core.CommonHelper.JsonColumnType<hrm_atd_ScanTime>();

            return this.Content(ColumnName, "application/json");
        }

        public SearchFilter BindSearch(string searchprm)
        {
            SearchFilter SearchKey = new SearchFilter();
            SearchKey.ColumnsName = "EmployeeID,Bar_Code";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "Work_Date";
            SearchKey.OrderDirection = "Desc";
            RBVHEmployeeSearchpara ObjPara = JsonConvert.DeserializeObject<RBVHEmployeeSearchpara>(searchprm,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            SearchKey.Condition = "";
            return SearchKey;
        }

        /// <summary>
        /// use for scrolling page
        /// </summary>
        /// <returns></returns>

        public ActionResult Get(Guid ID)
        {
            hrm_atd_ScanTime objItem = hrm_atd_ScanTimeManager.GetById(ID);
            return View(objItem);
        }

        [HttpGet]
        public ActionResult Get(Guid ID, string action)
        {
            hrm_atd_ScanTime objItem = hrm_atd_ScanTimeManager.GetById(ID);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        /// <summary>
        /// use for setting up default value
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "hrm_atd_ScanTimelist")
        {
            return View(ViewFolder + "Create.cshtml", new hrm_atd_ScanTime()
            {
                ID = Guid.Empty,
                TargetDisplayID = TargetID
            });
        }

        /// <summary>
        /// use for setting up default value
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(Guid ID, string TargetID = "hrm_atd_ScanTimelist")
        {
            hrm_atd_ScanTime objItem = hrm_atd_ScanTimeManager.GetById(ID);
            objItem.TargetDisplayID = TargetID;
            return View(ViewFolder + "Create.cshtml", objItem);
        }

        [HttpPost]
        public ContentResult Save(string objdata, string value)
        {
            JsonObject js = new JsonObject();
            js.StatusCode = 200;
            js.Message = "Upload Success";
            try
            {
                hrm_atd_ScanTime obj = JsonConvert.DeserializeObject<hrm_atd_ScanTime>(objdata);
                obj = hrm_atd_ScanTimeManager.Update(obj);
                if (obj.EmployeeNo==String.Empty)
                {
                    js.StatusCode = 400;
                    js.Message = "Has Errors. Please contact Admin for more information";
                }
                else
                {
                    js.Data = obj;
                }
            }
            catch (Exception objEx)
            {
                js.StatusCode = 400;
                js.Message = objEx.Message;
            }

            return Content(JsonConvert.SerializeObject(js), "application/json");
        }

        [HttpPost]
        public ActionResult Create(hrm_atd_ScanTime model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //  model.CreatedUser = CurrentUser.UserName;
                    if (model.ID == Guid.Empty)
                    {
                        //get default value
                        //	hrm_atd_ScanTime b = hrm_atd_ScanTimeManager.GetById(model.ID);
                        hrm_atd_ScanTimeManager.Update(model);
                    }
                    else
                    {
                        // TODO: Add insert logic here
                        //	 model.CreatedDate = SystemConfig.CurrentDate;
                        hrm_atd_ScanTimeManager.Add(model);
                    }
                    return View(ViewFolder + "list.cshtml", hrm_atd_ScanTimeManager.GetAll());
                }
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(hrm_atd_ScanTime model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    hrm_atd_ScanTimeManager.Update(model);
                    //return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult hrm_atd_ScanTimeEvt(hrm_atd_ScanTimeCollection scanList, string Event)
        {
            // You have your books IDs on the deleteInputs array
            switch (Event.ToLower())
            {
                case "submit":

                    int EmpCode = CurrentUser.EmployeeCode;
                    foreach(hrm_atd_ScanTime item in scanList)
                    {
                        if (item.isSelect == "on")
                        {
                            item.EmployeeCode = EmpCode;
                           // objEmp = Biz.Core.Services.RBVHEmployeeManager.GetByDomainId()
                            hrm_atd_ScanTimeManager.Submit(item);
                        }
                    }
                    //if (ID != null && ID.Length > 0)
                    //{
                    //    int length = ID.Length;
                    //    hrm_atd_ScanTime objItem;
                    //    for (int i = 0; i <= length - 1; i++)
                    //    {
                    //        objItem = hrm_atd_ScanTimeManager.GetById(ID[i]);
                    //        if (objItem != null)
                    //        {
                    //            hrm_atd_ScanTimeManager.Delete(objItem);
                    //        }
                    //    }
                    //    return View(ViewFolder + "list.cshtml", hrm_atd_ScanTimeManager.GetAll());
                    //}
                    break;
                case "cancel":

                    //if (ID != null && ID.Length > 0)
                    //{
                    //    int length = ID.Length;
                    //    hrm_atd_ScanTime objItem;
                    //    for (int i = 0; i <= length - 1; i++)
                    //    {
                    //        objItem = hrm_atd_ScanTimeManager.GetById(ID[i]);
                    //        if (objItem != null)
                    //        {
                    //            hrm_atd_ScanTimeManager.Delete(objItem);
                    //        }
                    //    }
                    //    return View(ViewFolder + "list.cshtml", hrm_atd_ScanTimeManager.GetAll());
                    //}
                    break;
            }

            return View("PostFrm");
        }

        #region Import Employee by excel

        public ActionResult ListUpload()
        {
            return View(ViewFolder + "ListUpload.cshtml");
        }

        /// <summary>
        /// Upload The Excel File
        /// </summary>
        /// <returns></returns>
        public ContentResult ImportExcelFile()
        {
            JsonObject obj = new JsonObject();
            HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
            //    string fileName = file.FileName;
            //   string fileContentType = file.ContentType;
            // byte[] fileBytes = new byte[file.ContentLength];
            // var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<hrm_atd_ScanTime>(file.InputStream, 2,1);
            obj.StatusCode = 200;
            obj.Message = "Upload Success";
            try
            {
                DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
                obj.Data = dt;
            }
            catch (Exception objEx)
            {
                obj.StatusCode = 400;
                obj.Message = objEx.Message;
            }

            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }

        [HttpPost]
        public ContentResult SaveExcel(string item)
        {
            //string b = Request["item"];
            IEnumerable<hrm_atd_ScanTime> objItemList = JsonConvert.DeserializeObject<IEnumerable<hrm_atd_ScanTime>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (hrm_atd_ScanTime objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                //objitem.CreatedDate = SystemConfig.CurrentDate;

                hrm_atd_ScanTimeManager.Add(objitem);
            }

            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }

        /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            hrm_atd_ScanTimeCollection collection = hrm_atd_ScanTimeManager.GetAll();
            DataTable dt = collection.ToDataTable<hrm_atd_ScanTime>();
            string fileName = "hrm_atd_ScanTime_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "hrm_atd_ScanTime List", false);
            return fileName;
        }

        #endregion Import Employee by excel
    }
}
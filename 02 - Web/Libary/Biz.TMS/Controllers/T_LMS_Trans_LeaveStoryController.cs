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
using DataTables.Mvc;
using System.Data;
using Helpers;
using Biz.Core.Helpers;
using Biz.Core.Converts;
namespace Biz.TMS.Controllers
{
 //   [CustomAuthorize]
	//[AccessRights("T_LMS_Trans_LeaveStory")]
    public class LeaveStoryController :  BaseController
    {
		string ViewFolder = "~/Views/TMS/T_LMS_Trans_LeaveStory/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            //T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.GetAll();
            return View(ViewFolder + "list.cshtml");
        }

        public ContentResult Lms(string EmployeeNo, string WD)
        {
            LeaveWFCollection collection = T_LMS_Trans_LeaveStoryManager.GetEmployeeLeaveReason(10001, EmployeeNo, WD);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
            //return View(ViewFolder + "EmpTms.cshtml");
        }

        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "Id" : SearchKey.OrderBy;
            T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public ActionResult headerLink()
        {
            string ColumnName = Biz.Core.CommonHelper.JsonColumnType<T_LMS_Trans_LeaveStory>();

            //return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
            return Content(ColumnName, "application/json");
        }
        public JsonResult GetEmpGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int EmployeeCode, string WD)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(1, requestModel, "Id", "Id");
            SearchKey.Condition = "employeeCode = " + EmployeeCode + " ";
            T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "Id", "Id");
            T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

		
		/// <summary>
        /// use for scrolling page 
        /// </summary>
        /// <returns></returns>
		 public ContentResult GetPg(int page, int pagesize)
        {
            string condition = "";
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"Id", "Id","Desc", condition);
            T_LMS_Trans_LeaveStoryCollection objItem =T_LMS_Trans_LeaveStoryManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int Id)
        {
            T_LMS_Trans_LeaveStory objItem = T_LMS_Trans_LeaveStoryManager.GetById(Id);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int Id, string action)
        {
            T_LMS_Trans_LeaveStory objItem = T_LMS_Trans_LeaveStoryManager.GetById(Id);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_LMS_Trans_LeaveStorylist")
        {
            return View(ViewFolder + "Create.cshtml",new T_LMS_Trans_LeaveStory()
            {
				Id = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int Id,string TargetID = "T_LMS_Trans_LeaveStorylist")
        {
            T_LMS_Trans_LeaveStory objItem = T_LMS_Trans_LeaveStoryManager.GetById(Id);
			objItem.TargetDisplayID = TargetID;
            return View(ViewFolder + "Create.cshtml",objItem);
        }

		[HttpPost]
        public ContentResult Save(string objdata, string value)
        {
            JsonObject js = new JsonObject();
            js.StatusCode = 200;
            js.Message = "Upload Success";
            try
            {
                T_LMS_Trans_LeaveStory obj = JsonConvert.DeserializeObject<T_LMS_Trans_LeaveStory>(objdata);
                obj = T_LMS_Trans_LeaveStoryManager.Update(obj);
                if (obj.Id==0)
                {
                    js.StatusCode = 400;
                    js.Message = "Has Errors. Please contact Admin for more information";
                }else
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
        public ActionResult Create(T_LMS_Trans_LeaveStory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.Id != 0)
                 {
					//get default value
					//	T_LMS_Trans_LeaveStory b = T_LMS_Trans_LeaveStoryManager.GetById(model.Id);
                    T_LMS_Trans_LeaveStoryManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_LMS_Trans_LeaveStoryManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_LMS_Trans_LeaveStoryManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_LMS_Trans_LeaveStory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_LMS_Trans_LeaveStoryManager.Update(model);
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
        public ActionResult T_LMS_Trans_LeaveStoryEvt(int[] Id, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (Id != null && Id.Length > 0)
                    {
                        int length = Id.Length ;
                        T_LMS_Trans_LeaveStory objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_LMS_Trans_LeaveStoryManager.GetById(Id[i]);
                            if (objItem != null)
                            {
                                T_LMS_Trans_LeaveStoryManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_LMS_Trans_LeaveStoryManager.GetAll());
                    }
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_LMS_Trans_LeaveStory>(file.InputStream, 2,1);
            obj.StatusCode = 200;
            obj.Message = "Upload Success";
            try
            {
                DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
                obj.Data = dt;
            }
            catch(Exception objEx)
            {
                obj.StatusCode = 400;
                obj.Message = objEx.Message;
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
    
        public ContentResult abc(string item)
        {
            //string b = Request["item"];
            //IEnumerable<T_LMS_Trans_LeaveStory> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_LMS_Trans_LeaveStory>>(item);

            //JsonObject obj = new JsonObject();
            //obj.StatusCode = 200;
            //obj.Message = "The process is sucessed";
            //foreach (T_LMS_Trans_LeaveStory objitem in objItemList)
            //{
            //    //default value
            //    //objitem.CreatedUser = CurrentUser.UserName;
            //    objitem.CreatedDate = SystemConfig.CurrentDate;
                
            //    T_LMS_Trans_LeaveStoryManager.Add(objitem);
            //}
            
            return Content("test", "application/json");
        }
    
        public ContentResult abc(T_LMS_Trans_LeaveStory item)
        {
            ////string b = Request["item"];
            ////IEnumerable<T_LMS_Trans_LeaveStory> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_LMS_Trans_LeaveStory>>(item);

            //JsonObject obj = new JsonObject();
            //obj.StatusCode = 200;
            //obj.Message = "The process is sucessed";
            ////foreach (T_LMS_Trans_LeaveStory objitem in objItemList)
            ////{
            ////    //default value
            ////    //objitem.CreatedUser = CurrentUser.UserName;
            ////    objitem.CreatedDate = SystemConfig.CurrentDate;

            ////    T_LMS_Trans_LeaveStoryManager.Add(objitem);
            ////}

            //return Content(JsonConvert.SerializeObject(obj), "application/json");
            return Content("test 1");
        }
        /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_LMS_Trans_LeaveStoryCollection collection = T_LMS_Trans_LeaveStoryManager.GetAll();
            DataTable dt = collection.ToDataTable<T_LMS_Trans_LeaveStory>();
            string fileName = "T_LMS_Trans_LeaveStory_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_LMS_Trans_LeaveStory List", false);
            return fileName;
        }
        
        public string ExportExMonthTMS(int UserID,string MonthID, string FilterBy, string FilterValue)
        {
            External_TimesCollection collection = T_LMS_Trans_LeaveStoryManager.GetExMonthlyReport(UserID, MonthID, FilterBy, FilterValue);
            DataSet ds = new DataSet();
            DataTable dt = collection.ToDataTable<External_Times>();
            string fileName = UserID+ "_Monthly_TimeSheet_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] sheetName = { "Daily_TMS"};
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate", "ErrorMesssage" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            ds.Tables.Add(dt);
            //set Column Date Format 
            //int[] DateColumn = { 5, 6 };
            //ExcelPara mypara = new ExcelPara()
            //{
            //    ds = ds,
            //    sheetName = sheetName,
            //    fileName = fileName,
            //    DateColumns = DateColumn,
            //    DateFormat = "h:mm:ss tt"
            //};
            //FileInputHelper.ExportMultiSheetExcelExtend(mypara);
            FileInputHelper.ExportExcel(dt, fileName, "Monthly_TimeSheet", false);
            return fileName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID">Manager/DirectManagerID</param>
        /// <param name="FD">FromDate</param>
        /// <param name="TD">ToDate</param>
        /// <param name="AID">Associate ID</param>
        /// <returns></returns>
        public string ExportExDailyTMS(int UserID, string FD,string TD, int AID)
        {
            DataSet ds = new DataSet();
            ExternalDailyCollection collection = T_LMS_Trans_LeaveStoryManager.ExportExDailyTMS(UserID, FD, TD, AID);
            DataTable dt = collection.ToDataTable<ExternalDaily>();
            string fileName = AID+"_Monthly_TimeSheet_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate", "ErrorMesssage" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            
            if (collection.Count > 0)
            {
                ds.Tables.Add(dt);
                int[] DateColumn = { 6, 7 };
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
            }
            else
            {
                FileInputHelper.ExportExcel(dt, fileName, "Monthly_TimeSheet", false);
            }
            //set Column Date Format 
            
            //
            return fileName;
        }
        #endregion
    }
}
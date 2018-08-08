using BRVH.HR.OG.Models;
using BRVH.HR.OG.Services;
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
namespace BRVH.HR.OG.Controllers
{
 //   [CustomAuthorize]
	//[AccessRights("TimeLog")]
    public class TimeLogController :  BaseController
    {
		string ViewFolder = "~/Views/OG/TimeLog/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            TimeLogCollection collection = TimeLogManager.GetAll();
            return View(ViewFolder + "list.cshtml", collection);
        }

		[HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "TimeLogId" : SearchKey.OrderBy;
            TimeLogCollection collection = TimeLogManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "TimeLogId", "TimeLogId");
            TimeLogCollection collection = TimeLogManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"TimeLogId", "TimeLogId","Desc", condition);
            TimeLogCollection objItem =TimeLogManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int TimeLogId)
        {
            TimeLog objItem = TimeLogManager.GetById(TimeLogId);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int TimeLogId, string action)
        {
            TimeLog objItem = TimeLogManager.GetById(TimeLogId);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "TimeLoglist")
        {
            return View(ViewFolder + "Create.cshtml",new TimeLog()
            {
				TimeLogId = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int TimeLogId,string TargetID = "TimeLoglist")
        {
            TimeLog objItem = TimeLogManager.GetById(TimeLogId);
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
                TimeLog obj = JsonConvert.DeserializeObject<TimeLog>(objdata);
                obj = TimeLogManager.Update(obj);
                if (obj.TimeLogId==0)
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
        public ActionResult Create(TimeLog model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
                 //model.CreatedUser = CurrentUser.UserName;
				 if (model.TimeLogId != 0)
                 {
					//get default value
					//	TimeLog b = TimeLogManager.GetById(model.TimeLogId);
                    TimeLogManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
					// model.CreatedDate = SystemConfig.CurrentDate;
                    TimeLogManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", TimeLogManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(TimeLog model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    TimeLogManager.Update(model);
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
        public ActionResult TimeLogEvt(int[] TimeLogId, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (TimeLogId != null && TimeLogId.Length > 0)
                    {
                        int length = TimeLogId.Length ;
                        TimeLog objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = TimeLogManager.GetById(TimeLogId[i]);
                            if (objItem != null)
                            {
                                TimeLogManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", TimeLogManager.GetAll());
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<TimeLog>(file.InputStream, 2,1);
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
        [HttpPost]
        public ContentResult SaveExcel(string item)
        {
            //string b = Request["item"];
            IEnumerable<TimeLog> objItemList = JsonConvert.DeserializeObject<IEnumerable<TimeLog>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (TimeLog objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
            //    objitem.CreatedDate = SystemConfig.CurrentDate;
                
                TimeLogManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            TimeLogCollection collection = TimeLogManager.GetAll();
            DataTable dt = collection.ToDataTable<TimeLog>();
            string fileName = "TimeLog_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "TimeLog List", false);
            return fileName;
        }
        #endregion
    }
}
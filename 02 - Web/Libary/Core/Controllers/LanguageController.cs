using Biz.Core.Models;
using Biz.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz.Core.Security;
using Newtonsoft.Json;
using DataTables.Mvc;
using System.Data;
using Helpers;
using Biz.Core.Helpers;
using Biz.Core.Converts;

namespace Biz.Core.Controllers
{
    [CustomAuthorize]
	[AccessRights("Language")]
    public class LanguageController :  BaseController
    {
		string ViewFolder = "~/Views/CORE/Language/";

        public ActionResult Index()
        {
            //LanguageCollection collection = LanguageManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            LanguageCollection collection = LanguageManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "list.cshtml", collection);
        }

		[HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "LanguageId" : SearchKey.OrderBy;
            LanguageCollection collection = LanguageManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public ActionResult Get(int LanguageId)
        {
            Language objItem = LanguageManager.GetById(LanguageId, CurrentUser.CompanyID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int LanguageId, string action)
        {
            Language objItem = LanguageManager.GetById(LanguageId,CurrentUser.CompanyID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }
		
        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "Languagelist")
        {
		
            return View(ViewFolder + "Create.cshtml",new Language()
            {
				LanguageId = 0,
				CreatedDate = SystemConfig.CurrentDate,
				CompanyID = CurrentUser.CompanyID,
				TargetDisplayID = TargetID
            });
        }

		  /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int LanguageId,string TargetID = "Languagelist")
        {
            Language objItem = LanguageManager.GetById(LanguageId,CurrentUser.CompanyID);
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
                Language obj = JsonConvert.DeserializeObject<Language>(objdata);
                obj = LanguageManager.Update(obj);
                if (obj.LanguageId==0)
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
        public ActionResult Create(Language model)
        {
            try
            {
                if (ModelState.IsValid)
                {

				 model.CompanyID = CurrentUser.CompanyID;

				 if (model.LanguageId != 0)
                 {
					//get default value
					Language objOldLanguage = LanguageManager.GetById(model.LanguageId, CurrentUser.CompanyID);
					if (objOldLanguage != null)
                    {
							model.CreatedDate = objOldLanguage.CreatedDate;
							model.CreatedUser = objOldLanguage.CreatedUser;
					}

                    LanguageManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
					 //model.CreatedUser = CurrentUser.UserName;
					 model.CreatedDate = SystemConfig.CurrentDate;
                    LanguageManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", LanguageManager.GetAll(CurrentUser.CompanyID));
                }
				
            }
            catch(Exception ObjEx)
            {
				//LogHelper.AddLog(new IfindLog() {LinkUrl=Request.Url.AbsoluteUri,Exception= ObjEx.Message,Message = ObjEx.StackTrace});
                return View(model);
            }
			return View(model);
        }

      

        [HttpPost]
        public ActionResult Update(Language model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    LanguageManager.Update(model);
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
        public ActionResult LanguageEvt(int[] LanguageId, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (LanguageId != null && LanguageId.Length > 0)
                    {
                        int length = LanguageId.Length ;
                        Language objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = LanguageManager.GetById(LanguageId[i],CurrentUser.CompanyID);
                            if (objItem != null)
                            {
                                LanguageManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", LanguageManager.GetAll(CurrentUser.CompanyID));
                    }
                    break;
            }
           
           
            return View("PostFrm");
            
        }

        #region SearchData

        public ActionResult headerLink()
        {
            string ColumnName = CommonHelper.JsonColumnType<Language>();

            return this.Content(ColumnName, "application/json");
        }
        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, "LanguageId", "LanguageId");
            LanguageCollection collection = LanguageManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //{{Class}Collection data = LanguageManager.GetAll(CurrentUser.CompanyID);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for scrolling page 
        /// </summary>
        /// <returns></returns>
        public ContentResult GetPg(int page, int pagesize)
        {
            string condition = "";
            SearchFilter SearchKey = SearchFilter.SearchPG(CurrentUser.CompanyID, page, pagesize, "LanguageId", "LanguageId", "Desc", condition);
            LanguageCollection objItem = LanguageManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }
        #endregion
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<Language>(file.InputStream, 2,1);
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
            IEnumerable<Language> objItemList = JsonConvert.DeserializeObject<IEnumerable<Language>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (Language objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                objitem.CompanyID = CurrentUser.CompanyID;
                LanguageManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            LanguageCollection collection = LanguageManager.GetAll(CurrentUser.CompanyID);
            DataTable dt = collection.ToDataTable<Language>();
            string fileName = "Language_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "Language List", false);
            return fileName;
        }
        #endregion
    }
}
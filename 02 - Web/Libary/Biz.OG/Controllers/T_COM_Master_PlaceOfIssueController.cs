using Biz.OG.Models;
using Biz.OG.Services;
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
namespace Biz.OG.Controllers
{
    [CustomAuthorize]
	[AccessRights("T_COM_Master_PlaceOfIssue")]
    public class T_COM_Master_PlaceOfIssueController :  BaseController
    {
		string ViewFolder = "~/Views/OG/T_COM_Master_PlaceOfIssue/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            T_COM_Master_PlaceOfIssueCollection collection = T_COM_Master_PlaceOfIssueManager.GetAll();
            return View(ViewFolder + "list.cshtml", collection);
        }

		[HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "POI_ID" : SearchKey.OrderBy;
            T_COM_Master_PlaceOfIssueCollection collection = T_COM_Master_PlaceOfIssueManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "POI_ID", "POI_ID");
            T_COM_Master_PlaceOfIssueCollection collection = T_COM_Master_PlaceOfIssueManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"POI_ID", "POI_ID","Desc", condition);
            T_COM_Master_PlaceOfIssueCollection objItem =T_COM_Master_PlaceOfIssueManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int POI_ID)
        {
            T_COM_Master_PlaceOfIssue objItem = T_COM_Master_PlaceOfIssueManager.GetById(POI_ID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int POI_ID, string action)
        {
            T_COM_Master_PlaceOfIssue objItem = T_COM_Master_PlaceOfIssueManager.GetById(POI_ID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_COM_Master_PlaceOfIssuelist")
        {
            return View(ViewFolder + "Create.cshtml",new T_COM_Master_PlaceOfIssue()
            {
				POI_ID = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int POI_ID,string TargetID = "T_COM_Master_PlaceOfIssuelist")
        {
            T_COM_Master_PlaceOfIssue objItem = T_COM_Master_PlaceOfIssueManager.GetById(POI_ID);
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
                T_COM_Master_PlaceOfIssue obj = JsonConvert.DeserializeObject<T_COM_Master_PlaceOfIssue>(objdata);
                obj = T_COM_Master_PlaceOfIssueManager.Update(obj);
                if (obj.POI_ID==0)
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
        public ActionResult Create(T_COM_Master_PlaceOfIssue model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.POI_ID != 0)
                 {
					//get default value
					//	T_COM_Master_PlaceOfIssue b = T_COM_Master_PlaceOfIssueManager.GetById(model.POI_ID);
                    T_COM_Master_PlaceOfIssueManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_COM_Master_PlaceOfIssueManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_COM_Master_PlaceOfIssueManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_COM_Master_PlaceOfIssue model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_COM_Master_PlaceOfIssueManager.Update(model);
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
        public ActionResult T_COM_Master_PlaceOfIssueEvt(int[] POI_ID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (POI_ID != null && POI_ID.Length > 0)
                    {
                        int length = POI_ID.Length ;
                        T_COM_Master_PlaceOfIssue objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_COM_Master_PlaceOfIssueManager.GetById(POI_ID[i]);
                            if (objItem != null)
                            {
                                T_COM_Master_PlaceOfIssueManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_COM_Master_PlaceOfIssueManager.GetAll());
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_COM_Master_PlaceOfIssue>(file.InputStream, 2,1);
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
            IEnumerable<T_COM_Master_PlaceOfIssue> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_COM_Master_PlaceOfIssue>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (T_COM_Master_PlaceOfIssue objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                T_COM_Master_PlaceOfIssueManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_COM_Master_PlaceOfIssueCollection collection = T_COM_Master_PlaceOfIssueManager.GetAll();
            DataTable dt = collection.ToDataTable<T_COM_Master_PlaceOfIssue>();
            string fileName = "T_COM_Master_PlaceOfIssue_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_COM_Master_PlaceOfIssue List", false);
            return fileName;
        }
        #endregion
    }
}
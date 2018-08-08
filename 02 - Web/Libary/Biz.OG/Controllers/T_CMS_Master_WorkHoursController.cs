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
	[AccessRights("T_CMS_Master_WorkHours")]
    public class T_CMS_Master_WorkHoursController :  BaseController
    {
		string ViewFolder = "~/Views/OG/T_CMS_Master_WorkHours/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            T_CMS_Master_WorkHoursCollection collection = T_CMS_Master_WorkHoursManager.GetAll();
            return View(ViewFolder + "list.cshtml", collection);
        }

		[HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "WorkHoursID" : SearchKey.OrderBy;
            T_CMS_Master_WorkHoursCollection collection = T_CMS_Master_WorkHoursManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "WorkHoursID", "WorkHoursID");
            T_CMS_Master_WorkHoursCollection collection = T_CMS_Master_WorkHoursManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"WorkHoursID", "WorkHoursID","Desc", condition);
            T_CMS_Master_WorkHoursCollection objItem =T_CMS_Master_WorkHoursManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int WorkHoursID)
        {
            T_CMS_Master_WorkHours objItem = T_CMS_Master_WorkHoursManager.GetById(WorkHoursID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int WorkHoursID, string action)
        {
            T_CMS_Master_WorkHours objItem = T_CMS_Master_WorkHoursManager.GetById(WorkHoursID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_CMS_Master_WorkHourslist")
        {
            return View(ViewFolder + "Create.cshtml",new T_CMS_Master_WorkHours()
            {
				WorkHoursID = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int WorkHoursID,string TargetID = "T_CMS_Master_WorkHourslist")
        {
            T_CMS_Master_WorkHours objItem = T_CMS_Master_WorkHoursManager.GetById(WorkHoursID);
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
                T_CMS_Master_WorkHours obj = JsonConvert.DeserializeObject<T_CMS_Master_WorkHours>(objdata);
                obj = T_CMS_Master_WorkHoursManager.Update(obj);
                if (obj.WorkHoursID==0)
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
        public ActionResult Create(T_CMS_Master_WorkHours model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.WorkHoursID != 0)
                 {
					//get default value
					//	T_CMS_Master_WorkHours b = T_CMS_Master_WorkHoursManager.GetById(model.WorkHoursID);
                    T_CMS_Master_WorkHoursManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_CMS_Master_WorkHoursManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_CMS_Master_WorkHoursManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_CMS_Master_WorkHours model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_CMS_Master_WorkHoursManager.Update(model);
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
        public ActionResult T_CMS_Master_WorkHoursEvt(int[] WorkHoursID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (WorkHoursID != null && WorkHoursID.Length > 0)
                    {
                        int length = WorkHoursID.Length ;
                        T_CMS_Master_WorkHours objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_CMS_Master_WorkHoursManager.GetById(WorkHoursID[i]);
                            if (objItem != null)
                            {
                                T_CMS_Master_WorkHoursManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_CMS_Master_WorkHoursManager.GetAll());
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_CMS_Master_WorkHours>(file.InputStream, 2,1);
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
            IEnumerable<T_CMS_Master_WorkHours> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_CMS_Master_WorkHours>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (T_CMS_Master_WorkHours objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                T_CMS_Master_WorkHoursManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_CMS_Master_WorkHoursCollection collection = T_CMS_Master_WorkHoursManager.GetAll();
            DataTable dt = collection.ToDataTable<T_CMS_Master_WorkHours>();
            string fileName = "T_CMS_Master_WorkHours_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_CMS_Master_WorkHours List", false);
            return fileName;
        }
        #endregion
    }
}
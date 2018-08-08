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
 //   [CustomAuthorize]
	//[AccessRights("T_LMS_Master_AnnualLeave")]
    public class T_LMS_Master_AnnualLeaveController :  BaseController
    {
		string ViewFolder = "~/Views/OG/T_LMS_Master_AnnualLeave/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            //T_LMS_Master_AnnualLeaveCollection collection = T_LMS_Master_AnnualLeaveManager.GetAll();
            return View(ViewFolder + "list.cshtml");
        }

		[HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "ID" : SearchKey.OrderBy;
            T_LMS_Master_AnnualLeaveCollection collection = T_LMS_Master_AnnualLeaveManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "Grade_Id", "Grade_Id");
            T_LMS_Master_AnnualLeaveCollection collection = T_LMS_Master_AnnualLeaveManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"ID", "ID","Desc", condition);
            T_LMS_Master_AnnualLeaveCollection objItem =T_LMS_Master_AnnualLeaveManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int Grade_Id)
        {
            T_LMS_Master_AnnualLeave objItem = T_LMS_Master_AnnualLeaveManager.GetById(Grade_Id);
            return View(objItem);
        }

		//[HttpGet]
        [HttpPost]
		 public ActionResult Get(int Grade_Id, string action)
        {
            T_LMS_Master_AnnualLeave objItem = T_LMS_Master_AnnualLeaveManager.GetById(Grade_Id);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_LMS_Master_AnnualLeavelist")
        {
            return View(ViewFolder + "Create.cshtml",new T_LMS_Master_AnnualLeave()
            {
				Grade_Id = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int Grade_Id, string TargetID = "T_LMS_Master_AnnualLeavelist")
        {
            T_LMS_Master_AnnualLeave objItem = T_LMS_Master_AnnualLeaveManager.GetById(Grade_Id);
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
                T_LMS_Master_AnnualLeave obj = JsonConvert.DeserializeObject<T_LMS_Master_AnnualLeave>(objdata);
                obj = T_LMS_Master_AnnualLeaveManager.Update(obj);
                if (obj.Grade_Id == 0)
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
        public ActionResult Create(T_LMS_Master_AnnualLeave model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.Grade_Id != 0)
                 {
					//get default value
					//	T_LMS_Master_AnnualLeave b = T_LMS_Master_AnnualLeaveManager.GetById(model.ID);
                    T_LMS_Master_AnnualLeaveManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_LMS_Master_AnnualLeaveManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_LMS_Master_AnnualLeaveManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_LMS_Master_AnnualLeave model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_LMS_Master_AnnualLeaveManager.Update(model);
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
        public ActionResult T_LMS_Master_AnnualLeaveEvt(int[] ID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (ID != null && ID.Length > 0)
                    {
                        int length = ID.Length ;
                        T_LMS_Master_AnnualLeave objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_LMS_Master_AnnualLeaveManager.GetById(ID[i]);
                            if (objItem != null)
                            {
                                T_LMS_Master_AnnualLeaveManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_LMS_Master_AnnualLeaveManager.GetAll());
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_LMS_Master_AnnualLeave>(file.InputStream, 2,1);
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
            IEnumerable<T_LMS_Master_AnnualLeave> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_LMS_Master_AnnualLeave>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (T_LMS_Master_AnnualLeave objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                T_LMS_Master_AnnualLeaveManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_LMS_Master_AnnualLeaveCollection collection = T_LMS_Master_AnnualLeaveManager.GetAll();
            DataTable dt = collection.ToDataTable<T_LMS_Master_AnnualLeave>();
            string fileName = "T_LMS_Master_AnnualLeave_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_LMS_Master_AnnualLeave List", false);
            return fileName;
        }
        #endregion
    }
}
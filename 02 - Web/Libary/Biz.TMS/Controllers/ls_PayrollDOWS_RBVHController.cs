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
    [CustomAuthorize]
	[AccessRights("ls_PayrollDOWS_RBVH")]
    public class ls_PayrollDOWS_RBVHController :  BaseController
    {
		string ViewFolder = "~/Views/TMS/ls_PayrollDOWS_RBVH/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            ls_PayrollDOWS_RBVHCollection collection = ls_PayrollDOWS_RBVHManager.GetAll(CurrentUser.EntityID);
            return View(ViewFolder + "list.cshtml", collection);
        }

        public ActionResult headerLink()
        {
            string ColumnName = Biz.Core.CommonHelper.JsonColumnType<ls_PayrollDOWS_RBVH>();

            return this.Content(ColumnName, "application/json");
        }

        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "ENtityID" : SearchKey.OrderBy;
            ls_PayrollDOWS_RBVHCollection collection = ls_PayrollDOWS_RBVHManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "ENtityID", "ENtityID");
            ls_PayrollDOWS_RBVHCollection collection = ls_PayrollDOWS_RBVHManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"ENtityID", "ENtityID","Desc", condition);
            ls_PayrollDOWS_RBVHCollection objItem =ls_PayrollDOWS_RBVHManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int ENtityID)
        {
            ls_PayrollDOWS_RBVH objItem = ls_PayrollDOWS_RBVHManager.GetById(ENtityID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int ENtityID, string action)
        {
            ls_PayrollDOWS_RBVH objItem = ls_PayrollDOWS_RBVHManager.GetById(ENtityID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "ls_PayrollDOWS_RBVHlist")
        {
            return View(ViewFolder + "Create.cshtml",new ls_PayrollDOWS_RBVH()
            {
				ENtityID = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int ENtityID,string TargetID = "ls_PayrollDOWS_RBVHlist")
        {
            ls_PayrollDOWS_RBVH objItem = ls_PayrollDOWS_RBVHManager.GetById(ENtityID);
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
                ls_PayrollDOWS_RBVH obj = JsonConvert.DeserializeObject<ls_PayrollDOWS_RBVH>(objdata);
                obj = ls_PayrollDOWS_RBVHManager.Update(obj);
                if (obj.ENtityID==0)
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
        public ActionResult Create(ls_PayrollDOWS_RBVH model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.ENtityID != 0)
                 {
					//get default value
					//	ls_PayrollDOWS_RBVH b = ls_PayrollDOWS_RBVHManager.GetById(model.ENtityID);
                    ls_PayrollDOWS_RBVHManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    ls_PayrollDOWS_RBVHManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", ls_PayrollDOWS_RBVHManager.GetAll(CurrentUser.EntityID));
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(ls_PayrollDOWS_RBVH model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    ls_PayrollDOWS_RBVHManager.Update(model);
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
        public ActionResult ls_PayrollDOWS_RBVHEvt(int[] ENtityID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (ENtityID != null && ENtityID.Length > 0)
                    {
                        int length = ENtityID.Length ;
                        ls_PayrollDOWS_RBVH objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = ls_PayrollDOWS_RBVHManager.GetById(ENtityID[i]);
                            if (objItem != null)
                            {
                                ls_PayrollDOWS_RBVHManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", ls_PayrollDOWS_RBVHManager.GetAll(CurrentUser.EntityID));
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<ls_PayrollDOWS_RBVH>(file.InputStream, 2,1);
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
            IEnumerable<ls_PayrollDOWS_RBVH> objItemList = JsonConvert.DeserializeObject<IEnumerable<ls_PayrollDOWS_RBVH>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (ls_PayrollDOWS_RBVH objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                //objitem.CreatedDate = SystemConfig.CurrentDate;
                
                ls_PayrollDOWS_RBVHManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            ls_PayrollDOWS_RBVHCollection collection = ls_PayrollDOWS_RBVHManager.GetAll(CurrentUser.EntityID);
            DataTable dt = collection.ToDataTable<ls_PayrollDOWS_RBVH>();
            string fileName = "ls_PayrollDOWS_RBVH_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "ls_PayrollDOWS_RBVH List", false);
            return fileName;
        }
        #endregion
    }
}
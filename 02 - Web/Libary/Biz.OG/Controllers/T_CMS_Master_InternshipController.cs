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
using Biz.Core;
namespace Biz.OG.Controllers
{
    [CustomAuthorize]
	[AccessRights("Internship")]
    public class InternshipController :  BaseController//<T_CMS_Master_Internship>
    {
		string ViewFolder = "~/Views/OG/T_CMS_Master_Internship/";
        string screenID = "Internship";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            T_CMS_Master_InternshipCollection collection = T_CMS_Master_InternshipManager.GetAll();
            return View(ViewFolder + "list.cshtml", collection);
        }

        public ActionResult headerLink()
        {
            //string ColumnName = CommonHelper.JsonColumnType<T_CMS_Master_Internship>();
            //return this.Content(ColumnName, "application/json");
            HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<T_CMS_Master_Internship>(CurrentUser, screenID);
            return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
        }

        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "ID" : SearchKey.OrderBy;
            T_CMS_Master_InternshipCollection collection = T_CMS_Master_InternshipManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID,requestModel, "ID", "ID");
            T_COM_Master_EntityCollection EntityAllows = T_COM_Master_EntityManager.GetAllByEmployeeCode(CurrentUser.EmployeeCode);

            T_COM_Master_Entity entity;
            if (EntityAllows.Count > 0)
            {
                SearchKey.Condition += " (Entity_Id in ( ";
                for (int i = 0; i < EntityAllows.Count; i++)
                {
                    entity = EntityAllows[i];
                    SearchKey.Condition += " '" + entity.EntityId + "'  ";
                    if (i != EntityAllows.Count - 1)
                    {
                        SearchKey.Condition += " , ";
                    }
                }
                SearchKey.Condition += ") ) ";
            }

            T_CMS_Master_InternshipCollection collection = T_CMS_Master_InternshipManager.Search(SearchKey);
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
            T_CMS_Master_InternshipCollection objItem =T_CMS_Master_InternshipManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int ID)
        {
            T_CMS_Master_Internship objItem = T_CMS_Master_InternshipManager.GetById(ID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int ID, string action)
        {
            T_CMS_Master_Internship objItem = T_CMS_Master_InternshipManager.GetById(ID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_CMS_Master_Internshiplist")
        {
            return View(ViewFolder + "Create.cshtml",new T_CMS_Master_Internship()
            {
				ID = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int ID,string TargetID = "T_CMS_Master_Internshiplist")
        {
            T_CMS_Master_Internship objItem = T_CMS_Master_InternshipManager.GetById(ID);
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
                T_CMS_Master_Internship obj = JsonConvert.DeserializeObject<T_CMS_Master_Internship>(objdata);
                obj = T_CMS_Master_InternshipManager.Update(obj);
                if (obj.ID==0)
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
        public ActionResult Create(T_CMS_Master_Internship model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.ID != 0)
                 {
					//get default value
					//	T_CMS_Master_Internship b = T_CMS_Master_InternshipManager.GetById(model.ID);
                    T_CMS_Master_InternshipManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_CMS_Master_InternshipManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_CMS_Master_InternshipManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_CMS_Master_Internship model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_CMS_Master_InternshipManager.Update(model);
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
        public ActionResult T_CMS_Master_InternshipEvt(int[] ID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (ID != null && ID.Length > 0)
                    {
                        int length = ID.Length ;
                        T_CMS_Master_Internship objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_CMS_Master_InternshipManager.GetById(ID[i]);
                            if (objItem != null)
                            {
                                T_CMS_Master_InternshipManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_CMS_Master_InternshipManager.GetAll());
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
        public  ContentResult ImportExcelFile()
        {
            JsonObject obj = new JsonObject();
            HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
            //    string fileName = file.FileName;
            //   string fileContentType = file.ContentType;
            // byte[] fileBytes = new byte[file.ContentLength];
            // var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_CMS_Master_Internship>(file.InputStream, 2,1);
            obj.StatusCode = 200;
            obj.Message = "Upload Success";
            try
            {
                DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
                IEnumerable<T_CMS_Master_Internship> objItemList = ExcelHelper.ToDataTable(file.InputStream).ToList<T_CMS_Master_Internship>();
                T_CMS_Master_InternshipCollection ErrorList = T_CMS_Master_InternshipManager.ImportData(objItemList, CurrentUser.EmployeeCode);
                obj.Data = ErrorList;
                if (ErrorList.Count > 0)
                {
                    obj.StatusCode = 400;
                    obj.Message = "Can't import into system :" + ErrorList.Count + "/" + objItemList.Count<T_CMS_Master_Internship>();
                }
                //obj.Data = dt;
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
            IEnumerable<T_CMS_Master_Internship> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_CMS_Master_Internship>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (T_CMS_Master_Internship objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                T_CMS_Master_InternshipManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_CMS_Master_InternshipCollection collection = T_CMS_Master_InternshipManager.GetAll();
            DataTable dt = collection.ToDataTable<T_CMS_Master_Internship>();
            string fileName = "T_CMS_Master_Internship_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" ,"ID","InternNo", "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate","EmployeeCode","SalutationID","ProbationsPeriodCD","FullName","StatusID",
            "ApproverLevel","InternLevel","IDCardNo","PassportNo","DeptID","LocationID","EmpTypeID","EmpSubTypeID","IsActive","CreatedBy","ModifiedBy","ModifiedDate","ErrorMesssage","Dept","WorkHours","UniversityID"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_CMS_Master_Internship List", false);
            return fileName;
        }
        #endregion
    }
}
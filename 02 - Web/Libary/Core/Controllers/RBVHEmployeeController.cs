
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
	[AccessRights("RBVHEmployee")]
    public class EmployeeController :  BaseController
    {
		string ViewFolder = "~/Views/PDM/RBVHEmployee/";
        string screenID = "RBVHEmployee";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }


        public ActionResult list()
        {
            //  RBVHEmployeeCollection collection = RBVHEmployeeManager.GetAll();
            return View(ViewFolder + "list.cshtml");
        }
        public ActionResult HRBPlist()
        {
            //  RBVHEmployeeCollection collection = RBVHEmployeeManager.GetAll();
            return View(ViewFolder + "HRBPList.cshtml");
        }
        public ActionResult Detail(int EmployeeCode)
        {
            RBVHEmployee objItem = RBVHEmployeeManager.GetById(EmployeeCode);
            return View(ViewFolder + "Detail.cshtml", objItem);
        }
        public ActionResult ActiveList()
        {
            //  RBVHEmployeeCollection collection = RBVHEmployeeManager.GetAll();
            return View(ViewFolder + "ActiveList.cshtml");
        }

        public ActionResult Info(int EmployeeCode)
        {
            RBVHEmployee objItem = RBVHEmployeeManager.GetById(EmployeeCode);
            return View(ViewFolder + "Info.cshtml", objItem);
        }

        public ActionResult Lms(string EmployeeNo, string WD)
        {
            return View(ViewFolder + "list.cshtml");
        }

        public ActionResult Get(int EmployeeCode)
        {
            RBVHEmployee objItem = RBVHEmployeeManager.GetById(EmployeeCode);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int EmployeeCode, string action)
        {
            RBVHEmployee objItem = RBVHEmployeeManager.GetById(EmployeeCode);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "RBVHEmployeelist")
        {
            return View(ViewFolder + "Create.cshtml",new RBVHEmployee()
            {
				EmployeeCode = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int EmployeeCode,string TargetID = "RBVHEmployeelist")
        {
            RBVHEmployee objItem = RBVHEmployeeManager.GetById(EmployeeCode);
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
                RBVHEmployee obj = JsonConvert.DeserializeObject<RBVHEmployee>(objdata);
                obj = RBVHEmployeeManager.Update(obj);
                if (obj.EmployeeCode==0)
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
        public ActionResult Create(RBVHEmployee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.EmployeeCode != 0)
                 {
					//get default value
					//	RBVHEmployee b = RBVHEmployeeManager.GetById(model.EmployeeCode);
                    RBVHEmployeeManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    RBVHEmployeeManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", RBVHEmployeeManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(RBVHEmployee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    RBVHEmployeeManager.Update(model);
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
        public ActionResult RBVHEmployeeEvt(int[] EmployeeCode, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (EmployeeCode != null && EmployeeCode.Length > 0)
                    {
                        int length = EmployeeCode.Length ;
                        RBVHEmployee objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = RBVHEmployeeManager.GetById(EmployeeCode[i]);
                            if (objItem != null)
                            {
                                RBVHEmployeeManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", RBVHEmployeeManager.GetAll());
                    }
                    break;
            }
           
           
            return View("PostFrm");
            
        }
        #region Search
      
        [HttpPost]
        public ContentResult Search(RBVHSearchFilter SearchKey)
        {
            SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "EmployeeCode" : SearchKey.OrderBy;
            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            RBVHSearchFilter SearchKey = RBVHSearchFilter.SearchData(1, requestModel, "EmployeeCode,FirstName_EN,MiddleName_EN,LastName_EN,DomainId,EmployeeNo", "EmployeeCode");
            SearchKey.Condition = " tbl.IsActive=1 ";
            SearchKey.CDate = SystemConfig.CurrentDate;
            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActiveData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            RBVHSearchFilter SearchKey = RBVHSearchFilter.SearchData(1, requestModel, "EmployeeCode,FirstName_EN,MiddleName_EN,LastName_EN,DomainId", "EmployeeCode");
            SearchKey.Condition = " IsActive=0 ";
            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            RBVHSearchFilter SearchKey = BindSearch(searchprm);
            SearchKey = RBVHSearchFilter.SearchData(SearchKey, requestModel);
           
            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHRBPSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            RBVHSearchFilter SearchKey = BindSearchHRDB(searchprm);
            SearchKey = RBVHSearchFilter.SearchData(SearchKey, requestModel);

            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
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
            RBVHSearchFilter SearchKey = RBVHSearchFilter.SearchPG(1, page, pagesize, "EmployeeCode,FirstName_EN,MiddleName_EN,LastName_EN,DomainId", "EmployeeCode", "Desc", condition);
            RBVHEmployeeCollection objItem = RBVHEmployeeManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }
        public ActionResult headerLink()
        {
            //string ColumnName = CommonHelper.JsonColumnType<RBVHEmployee>();

            ////return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
            //return Content(ColumnName, "application/json");
            HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<RBVHEmployee>(CurrentUser, screenID);
            return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
        }
        public RBVHSearchFilter BindSearchHRDB(string searchprm)
        {
            RBVHSearchFilter SearchKey = new RBVHSearchFilter();
            SearchKey.OrderBy = "EmployeeCode";
            SearchKey.ColumnsName = "EmployeeCode,FirstName_EN,MiddleName_EN,LastName_EN,DomainId,EmployeeNo";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "EmployeeCode";
            SearchKey.OrderDirection = "Desc";
            SearchKey.Condition = " ext.HRBP_ID=" + CurrentUser.EmployeeCode + " ";
            SearchKey.CDate = SystemConfig.CurrentDate;
            RBVHEmployeeSearchpara ObjPara = JsonConvert.DeserializeObject<RBVHEmployeeSearchpara>(searchprm,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            if (ObjPara != null)
            {
                if (!string.IsNullOrEmpty(ObjPara.EntityId))
                {
                    SearchKey.EntityID = int.Parse(ObjPara.EntityId);
                }
                    
                if (!string.IsNullOrEmpty(ObjPara.OrgId))
                {
                    SearchKey.Condition += " and tbl.GroupCode = " + ObjPara.OrgId + " ";
                }

            }
            
            SearchKey.CDate = SystemConfig.CurrentDate;
            return SearchKey;
        }
        public RBVHSearchFilter BindSearch(string searchprm)
        {
            RBVHSearchFilter SearchKey = new RBVHSearchFilter();
            SearchKey.OrderBy = "EmployeeCode";
            SearchKey.ColumnsName = "EmployeeCode,FirstName_EN,MiddleName_EN,LastName_EN,DomainId,EmployeeNo";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "EmployeeCode";
            SearchKey.OrderDirection = "Desc";
            SearchKey.CDate = SystemConfig.CurrentDate;
            SearchKey.Condition = "";
            RBVHEmployeeSearchpara ObjPara = JsonConvert.DeserializeObject<RBVHEmployeeSearchpara>(searchprm,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            if (ObjPara != null)
            {
                if (!string.IsNullOrEmpty(ObjPara.EntityId))
                {
                    SearchKey.EntityID = int.Parse(ObjPara.EntityId);
                }
                if (!string.IsNullOrEmpty(ObjPara.OrgId))
                {
                    SearchKey.Condition += " tbl.GroupCode = " + ObjPara.OrgId + " ";
                }
            }
            
            return SearchKey;
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<RBVHEmployee>(file.InputStream, 2,1);
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
            IEnumerable<RBVHEmployee> objItemList = JsonConvert.DeserializeObject<IEnumerable<RBVHEmployee>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (RBVHEmployee objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                RBVHEmployeeManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel(string searchprm="")
        {

            RBVHSearchFilter SearchKey = BindSearch(searchprm);
            
           
            RBVHEmployeeCollection collection = RBVHEmployeeManager.Search(SearchKey);
            
            DataTable dt = collection.ToDataTable<RBVHEmployee>();
            string fileName = "RBVHEmployee_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            //string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            //for (int i = 0; i < RemoveColumn.Length; i++)
            //{
            //    if (dt.Columns.Contains(RemoveColumn[i]))
            //    {
            //        dt.Columns.Remove(RemoveColumn[i]);
            //    }
            //}
            HeaderItemCollection RemoveColumn = CommonHelper.HideColumn<RBVHEmployee>(CurrentUser, "RBVHEmployee");
            for (int i = 0; i < RemoveColumn.Count; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i].name))
                {
                    dt.Columns.Remove(RemoveColumn[i].name);
                }
            }

            FileInputHelper.ExportExcel(dt, fileName, "RBVHEmployee List", false);
            return fileName;
        }
        public void HeadCount(int EntityID, DateTime ExeDate)
        {
            DataTable dt = RBVHEmployeeManager.GetRealTimeEmployeeList(EntityID, ExeDate);

            FileInputHelper.ExportExcel(dt, "Employee List ", "RBVHEmployee List", false);

        }
        #endregion
    }
}
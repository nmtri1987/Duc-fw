//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Security.DAL.Security;
//using Newtonsoft.Json;
//using DataTables.Mvc;
//using System.Data;
//using Helpers;
//using Biz.Core.Converts;
//using Biz.Core.Helpers;
//using Biz.Core.Models;
//using ifinds.Core.Services;
//using System.Threading.Tasks;
//namespace Biz.Core.Controllers
//{
//    [CustomAuthorize]
//	//[AccessRights("Company")]
//    public class CompanyController :  BaseController
//    {
//		string ViewFolder = "~/Views/Core/Company/";

//        public ActionResult Index()
//        {
//            //CompanyCollection collection = CompanyManager.GetAll(CurrentUser.CompanyID);
//            return View(ViewFolder+"Index.cshtml");
//        }
//        public async Task<ActionResult> DownloadLink(string remoteUri, string filename)
//        {

//            string myStringWebResource = null;
//            try
//            {
//                string fullPathWhereToSave = CommonHelper.MapPath("~/img/") + filename;
//                //var success = FileDownloader.DownloadFile(remoteUri, fullPathWhereToSave, 3000);
//                return Content("");
//            }
//            catch (Exception objEx)
//            {
//                return Content(objEx.Message);
//            }

//        }
//        public ActionResult list()
//        {
//            CompanyCollection collection = CompanyManager.GetAll();
//            return View(ViewFolder + "list.cshtml", collection);
//        }

//		[HttpPost]
//        public ContentResult Search(SearchFilter SearchKey)
//        {
//		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "CompanyID" : SearchKey.OrderBy;
//            CompanyCollection collection = CompanyManager.Search(SearchKey);
//            return Content(JsonConvert.SerializeObject(collection), "application/json");
//        }

//        public ActionResult Get(int CompanyID)
//        {
//            Company objItem = CompanyManager.GetById(CompanyID);
//            return View(objItem);
//        }

//		[HttpGet]
//		 public ActionResult Get(int CompanyID, string action)
//        {
//            Company objItem = CompanyManager.GetById(CompanyID);
//             return Content(JsonConvert.SerializeObject(objItem), "application/json");
//        }

//		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
//        {
//            ColumnCollection col = requestModel.Columns;
//            string OrderBy = "CompanyID", OrderDirection = "ASC";

//            foreach (Column item in col)
//            {
//				//if (item.IsOrdered && item.Data!="EmpPoint")
//                if (item.IsOrdered )
//                {
//                    OrderBy = item.Data;
//                    OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
//                    break;
//                }
//            }

//            SearchFilter SearchKey = new SearchFilter()
//            {
//                CompanyID = CurrentUser.CompanyID,
//                Keyword = requestModel.Search.Value,
//                Page =  (requestModel.Start/ requestModel.Length ) + 1,
//                PageSize = requestModel.Length,
//                ColumnsName = "CompanyID",
//                OrderBy = OrderBy,
//                OrderDirection=OrderDirection,
//            };

//            CompanyCollection collection = CompanyManager.Search(SearchKey);
//            int TotalRecord = 0;
//            if (collection.Count > 0)
//            {
//                TotalRecord = collection[0].TotalRecord;
//            }
//            //CompanyCollection data = CompanyManager.GetAll(CurrentUser.CompanyID);
//            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
//        }
//        /// <summary>
//        /// use for setting up default value 
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Create(string TargetID = "Companylist")
//        {
		
//            return View(ViewFolder + "Create.cshtml",new Company()
//            {
//				CompanyID = 0,
//				CreatedDate = SystemConfig.CurrentDate,
//				TargetDisplayID = TargetID
//            });
//        }

//		  /// <summary>
//        /// use for setting up default value 
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Update(int CompanyID,string TargetID = "Companylist")
//        {
//            Company objItem = CompanyManager.GetById(CompanyID);
//			objItem.TargetDisplayID = TargetID;
//            return View(ViewFolder + "Create.cshtml",objItem);
//        }

//        [HttpPost]
//        public ActionResult Create(Company model)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {

//				 //model.CompanyID = CurrentUser.CompanyID;

//				 if (model.CompanyID != 0)
//                 {
//					//get default value
//					Company objOldCompany = CompanyManager.GetById(model.CompanyID);
//					if (objOldCompany != null)
//                    {
//							model.CreatedDate = objOldCompany.CreatedDate;
//							model.CreatedUser = objOldCompany.CreatedUser;
//					}

//                    CompanyManager.Update(model);
                  
//				 }else
//				 {
//					// TODO: Add insert logic here
//					 model.CreatedUser = CurrentUser.UserName;
//					 model.CreatedDate = SystemConfig.CurrentDate;
//                    CompanyManager.Add(model);
                   
//				 }
//					return View(ViewFolder+"list.cshtml", CompanyManager.GetAll());
//                }
				
//            }
//            catch(Exception ObjEx)
//            {
//				////LogHelper.AddLog(new IfindLog() {LinkUrl=Request.Url.AbsoluteUri,Exception= ObjEx.Message,Message = ObjEx.StackTrace});
//                return View(model);
//            }
//			return View(model);
//        }

      

//        [HttpPost]
//        public ActionResult Update(Company model)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    // TODO: Add insert logic here
//                    CompanyManager.Update(model);
//                    //return RedirectToAction("Index");
//                }
//                return View(model);
//            }
//            catch
//            {
//                return View(model);
//            }
//        }

//		[HttpPost]
//        public ActionResult CompanyEvt(int[] CompanyID, string Action)
//        {
//            // You have your books IDs on the deleteInputs array
//            switch (Action.ToLower())
//            {
//                case "delete":

//                    if (CompanyID != null && CompanyID.Length > 0)
//                    {
//                        int length = CompanyID.Length ;
//                        Company objItem;
//                        for (int i = 0; i <= length-1; i++)
//                        {
//                            objItem = CompanyManager.GetById(CompanyID[i]);
//                            if (objItem != null)
//                            {
//                                CompanyManager.Delete(objItem);
//                            }
//                        }
//						return View(ViewFolder+"list.cshtml", CompanyManager.GetAll());
//                    }
//                    break;
//            }
           
           
//            return View("PostFrm");
            
//        }

//		#region Import Employee by excel 

//		public ActionResult ListUpload()
//        {
//            return View(ViewFolder + "ListUpload.cshtml");
//        }
        

//         /// <summary>
//        /// Upload The Excel File
//        /// </summary>
//        /// <returns></returns>
//        public ContentResult ImportExcelFile()
//        {
//            JsonObject obj = new JsonObject();
//            HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
//            //    string fileName = file.FileName;
//            //   string fileContentType = file.ContentType;
//            // byte[] fileBytes = new byte[file.ContentLength];
//            // var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
//            //DataTable dt = ExcelHelper.getClassFromExcelPackage<Company>(file.InputStream, 2,1);
//            obj.StatusCode = 200;
//            obj.Message = "Upload Success";
//            try
//            {
//                DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
//                obj.Data = dt;
//            }
//            catch(Exception objEx)
//            {
//                obj.StatusCode = 400;
//                obj.Message = objEx.Message;
//            }
            
//            return Content(JsonConvert.SerializeObject(obj), "application/json");
//        }
//        [HttpPost]
//        public ContentResult SaveExcel(string item)
//        {
//            //string b = Request["item"];
//            IEnumerable<Company> objItemList = JsonConvert.DeserializeObject<IEnumerable<Company>>(item);

//            JsonObject obj = new JsonObject();
//            obj.StatusCode = 200;
//            obj.Message = "The process is sucessed";
//            foreach (Company objitem in objItemList)
//            {
//                //default value
//              //  objitem.CreatedUser = CurrentUser.UserName;
//                objitem.CreatedDate = SystemConfig.CurrentDate;
//                objitem.CompanyID = CurrentUser.CompanyID;
//                CompanyManager.Add(objitem);
//            }
            
//            return Content(JsonConvert.SerializeObject(obj), "application/json");
//        }
//		 /// <summary>
//        /// ExportExcel File
//        /// </summary>
//        /// <returns></returns>
//        public string ExportExcel()
//        {
//            CompanyCollection collection = CompanyManager.GetAll();
//            DataTable dt = collection.ToDataTable<Company>();
//            string fileName = "Company_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
//            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
//            for (int i = 0; i < RemoveColumn.Length; i++)
//            {
//                if (dt.Columns.Contains(RemoveColumn[i]))
//                {
//                    dt.Columns.Remove(RemoveColumn[i]);
//                }
//            }
//            FileInputHelper.ExportExcel(dt, fileName, "Company List", false);
//            return fileName;
//        }
//        #endregion
//    }
//}
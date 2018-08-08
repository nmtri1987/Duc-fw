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
	[AccessRights("T_COm_Master_Org")]
    public class T_COm_Master_OrgController :  BaseController
    {
		string ViewFolder = "~/Views/OG/T_COm_Master_Org/";
        public ActionResult Index()
        {
         
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            SearchFilter SearchKey = BindSearch("");
            SearchKey.Keyword = "10001";
            T_COm_Master_OrgCollection collection = T_COm_Master_OrgManager.Search(SearchKey);

            //T_COm_Master_OrgCollection collection = T_COm_Master_OrgManager.GetAll();
            return View(ViewFolder + "list.cshtml", collection);
        }
        public SearchFilter BindSearch(string searchprm)
        {
            SearchFilter SearchKey = new SearchFilter();
            SearchKey.OrderBy = "ID";
            SearchKey.ColumnsName = "Entity_Id";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "ID";
            SearchKey.OrderDirection = "Desc";
            //T_COm_Master_OrgCollection ObjPara = JsonConvert.DeserializeObject<RBVHEmployeeSearchpara>(searchprm,
            //    new JsonSerializerSettings
            //    {
            //        NullValueHandling = NullValueHandling.Ignore
            //    });

            SearchKey.Condition = "";
            return SearchKey;
        }
        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "OrgId" : SearchKey.OrderBy;
            T_COm_Master_OrgCollection collection = T_COm_Master_OrgManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
             SearchFilter SearchKey = SearchFilter.SearchData(1,requestModel, "OrgId", "OrgId");
            T_COm_Master_OrgCollection collection = T_COm_Master_OrgManager.Search(SearchKey);
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
            SearchFilter SearchKey = SearchFilter.SearchPG(1,page,pagesize,"OrgId", "OrgId","Desc", condition);
            T_COm_Master_OrgCollection objItem =T_COm_Master_OrgManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        public ActionResult Get(int OrgId)
        {
            T_COm_Master_Org objItem = T_COm_Master_OrgManager.GetById(OrgId);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int OrgId, string action)
        {
            T_COm_Master_Org objItem = T_COm_Master_OrgManager.GetById(OrgId);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "T_COm_Master_Orglist")
        {
            return View(ViewFolder + "Create.cshtml",new T_COm_Master_Org()
            {
				OrgId = 0,
				TargetDisplayID = TargetID
            });
        }
		 /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int OrgId,string TargetID = "T_COm_Master_Orglist")
        {
            T_COm_Master_Org objItem = T_COm_Master_OrgManager.GetById(OrgId);
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
                T_COm_Master_Org obj = JsonConvert.DeserializeObject<T_COm_Master_Org>(objdata);
                obj = T_COm_Master_OrgManager.Update(obj);
                if (obj.OrgId==0)
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
        public ActionResult Create(T_COm_Master_Org model)
        {
            try
            {
                if (ModelState.IsValid)
                {
				 
               //  model.CreatedUser = CurrentUser.UserName;
				 if (model.OrgId != 0)
                 {
					//get default value
					//	T_COm_Master_Org b = T_COm_Master_OrgManager.GetById(model.OrgId);
                    T_COm_Master_OrgManager.Update(model);
                  
				 }else
				 {
					// TODO: Add insert logic here
				//	 model.CreatedDate = SystemConfig.CurrentDate;
                    T_COm_Master_OrgManager.Add(model);
                   
				 }
					return View(ViewFolder+"list.cshtml", T_COm_Master_OrgManager.GetAll());
                }
				
            }
            catch
            {
                return View(model);
            }
			return View(model);
        }

       

        [HttpPost]
        public ActionResult Update(T_COm_Master_Org model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_COm_Master_OrgManager.Update(model);
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
        public ActionResult T_COm_Master_OrgEvt(int[] OrgId, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (OrgId != null && OrgId.Length > 0)
                    {
                        int length = OrgId.Length ;
                        T_COm_Master_Org objItem;
                        for (int i = 0; i <= length-1; i++)
                        {
                            objItem = T_COm_Master_OrgManager.GetById(OrgId[i]);
                            if (objItem != null)
                            {
                                T_COm_Master_OrgManager.Delete(objItem);
                            }
                        }
						return View(ViewFolder+"list.cshtml", T_COm_Master_OrgManager.GetAll());
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<T_COm_Master_Org>(file.InputStream, 2,1);
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
            IEnumerable<T_COm_Master_Org> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_COm_Master_Org>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (T_COm_Master_Org objitem in objItemList)
            {
                //default value
                //objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                
                T_COm_Master_OrgManager.Add(objitem);
            }
            
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
		 /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            T_COm_Master_OrgCollection collection = T_COm_Master_OrgManager.GetAll();
            DataTable dt = collection.ToDataTable<T_COm_Master_Org>();
            string fileName = "T_COm_Master_Org_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
            string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "T_COm_Master_Org List", false);
            return fileName;
        }
        #endregion
    }
}
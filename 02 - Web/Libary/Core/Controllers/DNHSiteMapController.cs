using Biz.Core.Models;
using Biz.Core.Services;
using System;
using System.Collections.Generic;
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
	[AccessRights("SiteMap")]

    public class SiteMapController : BaseController
    {
        string ViewFolder = "~/Views/CS/DNHSiteMap/";
        string screenID = "DNHSiteMap";
        public ActionResult Index()
        {
            //DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "Index.cshtml");
        }
        public ContentResult SiteGetAllPage()
        {
            DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }
        public ActionResult list()
        {
            //DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "list.cshtml");
        }

        public ActionResult SecondMenu(Guid? NodeID)
        {
            //ViewBag.NodeID = NodeID;
            Biz.Core.Models.DNHSiteMap objItem = Biz.Core.Services.DNHSiteMapManager.GetById(NodeID, CurrentUser.CompanyID);
            //DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "SiteMap.cshtml", objItem);
        }
        public ActionResult Info(Guid? NodeID)
        {
            ViewBag.NodeID = NodeID;
            //DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "Info.cshtml");
        }
        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
            SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "NodeID" : SearchKey.OrderBy;
            DNHSiteMapCollection collection = DNHSiteMapManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public ActionResult Get(Guid NodeID)
        {
            DNHSiteMap objItem = DNHSiteMapManager.GetById(NodeID, CurrentUser.CompanyID);
            return View(objItem);
        }

        [HttpGet]
        public ActionResult Get(Guid NodeID, string action)
        {
            DNHSiteMap objItem = DNHSiteMapManager.GetById(NodeID, CurrentUser.CompanyID);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }

        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(Guid? ParentID)
        {

            return View(ViewFolder + "Create.cshtml", new DNHSiteMap()
            {
                NodeID = Guid.Empty,
                CreatedDate = SystemConfig.CurrentDate,
                CompanyID = CurrentUser.CompanyID,
                CreatedUser = CurrentUser.UserName,
                ParentID = ParentID.Value
            });
        }

        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(Guid NodeID, string TargetID = "DNHSiteMaplist")
        {
            DNHSiteMap objItem = DNHSiteMapManager.GetById(NodeID, CurrentUser.CompanyID);
            objItem.TargetDisplayID = TargetID;
            return View(ViewFolder + "Create.cshtml", objItem);
        }

        [HttpPost]
        public ContentResult Save(string objdata, string value)
        {
            JsonObject js = new JsonObject();
            js.StatusCode = 200;
            js.Message = "Upload Success";
            try
            {
                DNHSiteMap obj = JsonConvert.DeserializeObject<DNHSiteMap>(objdata);
                obj = DNHSiteMapManager.Update(obj);
                if (obj.NodeID == Guid.Empty)
                {
                    js.StatusCode = 400;
                    js.Message = "Has Errors. Please contact Admin for more information";
                }
                else
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
        public ActionResult Create(DNHSiteMap model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.CompanyID = CurrentUser.CompanyID;

                    if (model.NodeID != Guid.Empty)
                    {
                        //get default value
                        DNHSiteMap objOldDNHSiteMap = DNHSiteMapManager.GetById(model.NodeID, CurrentUser.CompanyID);
                        if (objOldDNHSiteMap != null)
                        {
                            model.CreatedDate = objOldDNHSiteMap.CreatedDate;
                            model.CreatedUser = objOldDNHSiteMap.CreatedUser;
                        }

                        DNHSiteMapManager.Update(model);

                    }
                    else
                    {
                        // TODO: Add insert logic here
                        model.CreatedUser = CurrentUser.UserName;
                        model.CreatedDate = SystemConfig.CurrentDate;
                        DNHSiteMapManager.Add(model);

                    }
                    return View(ViewFolder + "list.cshtml", DNHSiteMapManager.GetAll(CurrentUser.CompanyID));
                }

            }
            catch (Exception ObjEx)
            {
                //	LogHelper.AddLog(new IfindLog() {LinkUrl=Request.Url.AbsoluteUri,Exception= ObjEx.Message,Message = ObjEx.StackTrace});
                return View(model);
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Update(DNHSiteMap model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    DNHSiteMapManager.Update(model);
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
        public ActionResult SiteMapEvt(Guid[] NodeID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (NodeID != null && NodeID.Length > 0)
                    {
                        int length = NodeID.Length;
                        DNHSiteMap objItem;
                        for (int i = 0; i <= length - 1; i++)
                        {
                            objItem = DNHSiteMapManager.GetById(NodeID[i], CurrentUser.CompanyID);
                            if (objItem != null)
                            {
                                DNHSiteMapManager.Delete(objItem);
                            }
                        }
                        return View(ViewFolder + "list.cshtml", DNHSiteMapManager.GetAll(CurrentUser.CompanyID));
                    }
                    break;
            }


            return View("PostFrm");

        }

        #region SearchData

        public ActionResult headerLink()
        {
            HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<DNHSiteMap>(CurrentUser, screenID);
            return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
            //string ColumnName = CommonHelper.JsonColumnType<DNHSiteMap>();

            //return this.Content(ColumnName, "application/json");
        }
        public SearchFilter BindSearch(string searchprm)
        {
            SearchFilter SearchKey = new SearchFilter();
            SearchKey.OrderBy = "NodeID";
            SearchKey.ColumnsName = "NodeID";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "ID";
            SearchKey.OrderDirection = "Desc";
            DNHSiteMap ObjPara = JsonConvert.DeserializeObject<DNHSiteMap>(searchprm,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            SearchKey.Condition = "";
            return SearchKey;
        }

        public JsonResult GetSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            SearchFilter SearchKey = BindSearch(searchprm);
            SearchKey = SearchFilter.SearchData(SearchKey, requestModel);
            DNHSiteMapCollection collection = DNHSiteMapManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSitemapNodeID([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, Guid? NodeID)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, "NodeID", "NodeID");
            if (NodeID.HasValue && NodeID != Guid.Empty)
            {
                SearchKey.Condition = " (ParentID='" + NodeID + "')  ";
            }
            else
            {
                SearchKey.Condition = " (ParentID is null)  ";
            }

            DNHSiteMapCollection collection = DNHSiteMapManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, "NodeID", "NodeID");
            DNHSiteMapCollection collection = DNHSiteMapManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //DNHSiteMapCollection data = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for scrolling page 
        /// </summary>
        /// <returns></returns>
        public ContentResult GetPg(int page, int pagesize)
        {
            string condition = "";
            SearchFilter SearchKey = SearchFilter.SearchPG(CurrentUser.CompanyID, page, pagesize, "NodeID", "NodeID", "Desc", condition);
            DNHSiteMapCollection objItem = DNHSiteMapManager.Search(SearchKey);
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<DNHSiteMap>(file.InputStream, 2,1);
            obj.StatusCode = 200;
            obj.Message = "Upload Success";
            try
            {
                DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
                obj.Data = dt;
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
            IEnumerable<DNHSiteMap> objItemList = JsonConvert.DeserializeObject<IEnumerable<DNHSiteMap>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (DNHSiteMap objitem in objItemList)
            {
                //default value
                objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                objitem.CompanyID = CurrentUser.CompanyID;
                DNHSiteMapManager.Add(objitem);
            }

            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
        /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            DNHSiteMapCollection collection = DNHSiteMapManager.GetAll(CurrentUser.CompanyID);
            DataTable dt = collection.ToDataTable<DNHSiteMap>();
            string fileName = "DNHSiteMap_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "DNHSiteMap List", false);
            return fileName;
        }
        #endregion
    }
}
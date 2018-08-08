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
using Helpers;
using Biz.Core.Converts;
using System.Data;
using Biz.Core.Helpers;
namespace ifinds.Object.OG.Controllers
{
    //[CustomAuthorize]
    //[AccessRights("EPPosition")]
    public class EPPositionController : BaseController
    {
        string ViewFolder = "~/Views/OG/EP/EPPosition/";

        public ActionResult Index()
        {
            //EPPositionCollection collection = EPPositionManager.GetAll(CurrentUser.CompanyID);
            return View(ViewFolder + "Index.cshtml");
        }

        public ActionResult list()
        {
            EPPositionCollection collection = EPPositionManager.GetAll(1);
            return View(ViewFolder + "list.cshtml", collection);
        }

        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
            SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "PositionID" : SearchKey.OrderBy;
            EPPositionCollection collection = EPPositionManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            ColumnCollection col = requestModel.Columns;
            string OrderBy = "PositionID", OrderDirection = "ASC";

            foreach (Column item in col)
            {
                //if (item.IsOrdered && item.Data!="EmpPoint")
                if (item.IsOrdered)
                {
                    OrderBy = item.Data;
                    OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                    break;
                }
            }
            SearchFilter SearchKey = new SearchFilter()
            {
                CompanyID = CurrentUser.CompanyID,
                Keyword = requestModel.Search.Value,
                Page = (requestModel.Start / requestModel.Length) + 1,
                PageSize = requestModel.Length,
                ColumnsName = "PositionID,Description",
                OrderBy = OrderBy,
                OrderDirection = OrderDirection,
            };
            EPPositionCollection collection = EPPositionManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //EPPositionCollection data = EPPositionManager.GetAll(CurrentUser.CompanyID);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(string PositionID)
        {
            EPPosition objItem = EPPositionManager.GetById(PositionID, CurrentUser.CompanyID);
            return View(objItem);
        }

        [HttpGet]
        public ActionResult Get(string PositionID, string action)
        {
            EPPosition objItem = EPPositionManager.GetById(PositionID, CurrentUser.CompanyID);
            return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string TargetID = "EPPositionlist")
        {
            return View(ViewFolder + "Create.cshtml", new EPPosition()
            {
                EPPositionKey = SystemConst.AddNew,
                CreatedDate = SystemConfig.CurrentDate,
                CompanyID = CurrentUser.CompanyID,
                TargetDisplayID = TargetID,
               
            });
        }

        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(string PositionID, string TargetID = "EPPositionlist")
        {
            EPPosition objItem = EPPositionManager.GetById(PositionID, CurrentUser.CompanyID);
            objItem.EPPositionKey = objItem.PositionID;
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
                EPPosition obj = JsonConvert.DeserializeObject<EPPosition>(objdata);
                obj = EPPositionManager.Update(obj);
                if (string.IsNullOrEmpty(obj.PositionCD))
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
        public ActionResult Create(EPPosition model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.CompanyID = CurrentUser.CompanyID;

                    if (model.EPPositionKey.Trim() != SystemConst.AddNew)
                    {
                        //get default value
                        EPPosition objOldEPPosition = EPPositionManager.GetById(model.PositionID, CurrentUser.CompanyID);
                        if (objOldEPPosition != null)
                        {
                            model.CreatedDate = objOldEPPosition.CreatedDate;
                            model.CreatedUser = objOldEPPosition.CreatedUser;
                        }

                        EPPositionManager.Update(model);

                    }
                    else
                    {
                        // TODO: Add insert logic here
                        model.CreatedUser = CurrentUser.UserName;
                        model.CreatedDate = SystemConfig.CurrentDate;
                        EPPositionManager.Add(model);

                    }
                    return View(ViewFolder + "list.cshtml", EPPositionManager.GetAll(CurrentUser.CompanyID));
                }

            }
            catch (Exception ObjEx)
            {
               // LogHelper.AddLog(new IfindLog() { LinkUrl = Request.Url.AbsoluteUri, Exception = ObjEx.Message, Message = ObjEx.StackTrace });
                return View(model);
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Update(EPPosition model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    EPPositionManager.Update(model);
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
        public ActionResult EPPositionEvt(string[] PositionID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (PositionID != null && PositionID.Length > 0)
                    {
                        int length = PositionID.Length;
                        EPPosition objItem;
                        for (int i = 0; i <= length - 1; i++)
                        {
                            objItem = EPPositionManager.GetById(PositionID[i], CurrentUser.CompanyID);
                            if (objItem != null)
                            {
                                EPPositionManager.Delete(objItem);

                            }
                        }
                        return View(ViewFolder + "list.cshtml", EPPositionManager.GetAll(CurrentUser.CompanyID));
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
            //DataTable dt = ExcelHelper.getClassFromExcelPackage<EPPosition>(file.InputStream, 2,1);
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
            IEnumerable<EPPosition> objItemList = JsonConvert.DeserializeObject<IEnumerable<EPPosition>>(item);

            JsonObject obj = new JsonObject();
            obj.StatusCode = 200;
            obj.Message = "The process is sucessed";
            foreach (EPPosition objitem in objItemList)
            {
                //default value
                objitem.CreatedUser = CurrentUser.UserName;
                objitem.CreatedDate = SystemConfig.CurrentDate;
                objitem.CompanyID = CurrentUser.CompanyID;
                EPPositionManager.Add(objitem);
            }

            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
        /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        public string ExportExcel()
        {
            EPPositionCollection collection = EPPositionManager.GetAll(CurrentUser.CompanyID);
            DataTable dt = collection.ToDataTable<EPPosition>();
            string fileName = "EPPosition_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord", "CreatedUser", "CreatedDate" };
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (dt.Columns.Contains(RemoveColumn[i]))
                {
                    dt.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(dt, fileName, "EPPosition List", false);
            return fileName;
        }
        #endregion
    }
}
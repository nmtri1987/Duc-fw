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
    [AccessRights("LocaleStringResource")]
    public class LocaleStringResourceController : BaseController<DNHLocaleStringResource> { }
    //  public class LocaleStringResourceController :  BaseController
    //  {
    //string ViewFolder = "~/Views/Core/LocaleStringResource/";

    //      public ActionResult Index()
    //      {
    //          //LocaleStringResourceCollection collection = LocaleStringResourceManager.GetAll(CurrentUser.CompanyID);
    //          return View(ViewFolder+"Index.cshtml");
    //      }

    //public ActionResult list()
    //      {
    //          LocaleStringResourceCollection collection = LocaleStringResourceManager.GetAll(CurrentUser.CompanyID);
    //          return View(ViewFolder + "list.cshtml", collection);
    //      }

    //[HttpPost]
    //      public ContentResult Search(SearchFilter SearchKey)
    //      {
    //    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "LocaleStringResourceID" : SearchKey.OrderBy;
    //          LocaleStringResourceCollection collection = LocaleStringResourceManager.Search(SearchKey);
    //          return Content(JsonConvert.SerializeObject(collection), "application/json");
    //      }

    //      public ActionResult Get(int LocaleStringResourceID)
    //      {
    //          LocaleStringResource objItem = LocaleStringResourceManager.GetById(LocaleStringResourceID, CurrentUser.CompanyID);
    //          return View(objItem);
    //      }

    //[HttpGet]
    // public ActionResult Get(int LocaleStringResourceID, string action)
    //      {
    //          LocaleStringResource objItem = LocaleStringResourceManager.GetById(LocaleStringResourceID,CurrentUser.CompanyID);
    //           return Content(JsonConvert.SerializeObject(objItem), "application/json");
    //      }

    //      /// <summary>
    //      /// use for setting up default value 
    //      /// </summary>
    //      /// <returns></returns>
    //      public ActionResult Create(string TargetID = "LocaleStringResourcelist")
    //      {

    //          return View(ViewFolder + "Create.cshtml",new LocaleStringResource()
    //          {
    //		LocaleStringResourceID = 0,
    //		CreatedDate = SystemConfig.CurrentDate,
    //		CompanyID = CurrentUser.CompanyID,
    //		TargetDisplayID = TargetID
    //          });
    //      }

    //  /// <summary>
    //      /// use for setting up default value 
    //      /// </summary>
    //      /// <returns></returns>
    //      public ActionResult Update(int LocaleStringResourceID,string TargetID = "LocaleStringResourcelist")
    //      {
    //          LocaleStringResource objItem = LocaleStringResourceManager.GetById(LocaleStringResourceID,CurrentUser.CompanyID);
    //	objItem.TargetDisplayID = TargetID;
    //          return View(ViewFolder + "Create.cshtml",objItem);
    //      }

    //  [HttpPost]
    //      public ContentResult Save(string objdata, string value)
    //      {
    //          JsonObject js = new JsonObject();
    //          js.StatusCode = 200;
    //          js.Message = "Upload Success";
    //          try
    //          {
    //              LocaleStringResource obj = JsonConvert.DeserializeObject<LocaleStringResource>(objdata);
    //              obj = LocaleStringResourceManager.Update(obj);
    //              if (obj.LocaleStringResourceID==0)
    //              {
    //                  js.StatusCode = 400;
    //                  js.Message = "Has Errors. Please contact Admin for more information";
    //              }else
    //              {
    //                  js.Data = obj;
    //              }
    //          }
    //          catch (Exception objEx)
    //          {
    //              js.StatusCode = 400;
    //              js.Message = objEx.Message;
    //          }

    //          return Content(JsonConvert.SerializeObject(js), "application/json");
    //      }
    //      [HttpPost]
    //      public ActionResult Create(LocaleStringResource model)
    //      {
    //          try
    //          {
    //              if (ModelState.IsValid)
    //              {

    //		 model.CompanyID = CurrentUser.CompanyID;

    //		 if (model.LocaleStringResourceID != 0)
    //               {
    //			//get default value
    //			LocaleStringResource objOldLocaleStringResource = LocaleStringResourceManager.GetById(model.LocaleStringResourceID, CurrentUser.CompanyID);
    //			if (objOldLocaleStringResource != null)
    //                  {
    //					model.CreatedDate = objOldLocaleStringResource.CreatedDate;
    //					model.CreatedUser = objOldLocaleStringResource.CreatedUser;
    //			}

    //                  LocaleStringResourceManager.Update(model);

    //		 }else
    //		 {
    //			// TODO: Add insert logic here
    //			// model.CreatedUser = CurrentUser.UserName;
    //			 model.CreatedDate = SystemConfig.CurrentDate;
    //                  LocaleStringResourceManager.Add(model);

    //		 }
    //			return View(ViewFolder+"list.cshtml", LocaleStringResourceManager.GetAll(CurrentUser.CompanyID));
    //              }

    //          }
    //          catch(Exception ObjEx)
    //          {
    //		//LogHelper.AddLog(new IfindLog() {LinkUrl=Request.Url.AbsoluteUri,Exception= ObjEx.Message,Message = ObjEx.StackTrace});
    //              return View(model);
    //          }
    //	return View(model);
    //      }



    //      [HttpPost]
    //      public ActionResult Update(LocaleStringResource model)
    //      {
    //          try
    //          {
    //              if (ModelState.IsValid)
    //              {
    //                  // TODO: Add insert logic here
    //                  LocaleStringResourceManager.Update(model);
    //                  //return RedirectToAction("Index");
    //              }
    //              return View(model);
    //          }
    //          catch
    //          {
    //              return View(model);
    //          }
    //      }

    //[HttpPost]
    //      public ActionResult LocaleStringResourceEvt(int[] LocaleStringResourceID, string Action)
    //      {
    //          // You have your books IDs on the deleteInputs array
    //          switch (Action.ToLower())
    //          {
    //              case "delete":

    //                  if (LocaleStringResourceID != null && LocaleStringResourceID.Length > 0)
    //                  {
    //                      int length = LocaleStringResourceID.Length ;
    //                      LocaleStringResource objItem;
    //                      for (int i = 0; i <= length-1; i++)
    //                      {
    //                          objItem = LocaleStringResourceManager.GetById(LocaleStringResourceID[i],CurrentUser.CompanyID);
    //                          if (objItem != null)
    //                          {
    //                              LocaleStringResourceManager.Delete(objItem);
    //                          }
    //                      }
    //				return View(ViewFolder+"list.cshtml", LocaleStringResourceManager.GetAll(CurrentUser.CompanyID));
    //                  }
    //                  break;
    //          }


    //          return View("PostFrm");

    //      }
    //      #region SearchData

    //      public ActionResult headerLink()
    //      {
    //          string ColumnName = CommonHelper.JsonColumnType<LocaleStringResource>();

    //          return this.Content(ColumnName, "application/json");
    //      }
    //      public SearchFilter BindSearch(string searchprm)
    //      {
    //          SearchFilter SearchKey = new SearchFilter();
    //          SearchKey.OrderBy = "ID";
    //          SearchKey.ColumnsName = "ID,EmployeeCode,MiddleName_EN,LastName_EN,FirstName_EN,DeptID,ContractNo,HighestDegree";
    //          SearchKey.Page = 1;
    //          SearchKey.PageSize = 5000;
    //          SearchKey.OrderBy = "ID";
    //          SearchKey.OrderDirection = "Desc";
    //          LocaleStringResource ObjPara = JsonConvert.DeserializeObject<LocaleStringResource>(searchprm,
    //              new JsonSerializerSettings
    //              {
    //                  NullValueHandling = NullValueHandling.Ignore
    //              });

    //          SearchKey.Condition = "";
    //          return SearchKey;
    //      }

    //      public JsonResult GetSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
    //      {
    //          SearchFilter SearchKey = BindSearch(searchprm);
    //          SearchKey = SearchFilter.SearchData(SearchKey, requestModel);
    //          LocaleStringResourceCollection collection = LocaleStringResourceManager.Search(SearchKey);
    //          int TotalRecord = 0;
    //          if (collection.Count > 0)
    //          {
    //              TotalRecord = collection[0].TotalRecord;
    //          }
    //          return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
    //      }

    //      public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    //      {
    //          SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, "LocaleStringResourceID", "LocaleStringResourceID");
    //          LocaleStringResourceCollection collection = LocaleStringResourceManager.Search(SearchKey);
    //          int TotalRecord = 0;
    //          if (collection.Count > 0)
    //          {
    //              TotalRecord = collection[0].TotalRecord;
    //          }
    //          //LocaleStringResourceCollection data = LocaleStringResourceManager.GetAll(CurrentUser.CompanyID);
    //          return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
    //      }

    //      /// <summary>
    //      /// use for scrolling page 
    //      /// </summary>
    //      /// <returns></returns>
    //      public ContentResult GetPg(int page, int pagesize)
    //      {
    //          string condition = "";
    //          SearchFilter SearchKey = SearchFilter.SearchPG(CurrentUser.CompanyID, page, pagesize, "LocaleStringResourceID", "LocaleStringResourceID", "Desc", condition);
    //          LocaleStringResourceCollection objItem = LocaleStringResourceManager.Search(SearchKey);
    //          return Content(JsonConvert.SerializeObject(objItem), "application/json");
    //      }
    //      #endregion
    //      #region Import Employee by excel 

    //      public ActionResult ListUpload()
    //      {
    //          return View(ViewFolder + "ListUpload.cshtml");
    //      }


    //       /// <summary>
    //      /// Upload The Excel File
    //      /// </summary>
    //      /// <returns></returns>
    //      public ContentResult ImportExcelFile()
    //      {
    //          JsonObject obj = new JsonObject();
    //          HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
    //          //    string fileName = file.FileName;
    //          //   string fileContentType = file.ContentType;
    //          // byte[] fileBytes = new byte[file.ContentLength];
    //          // var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
    //          //DataTable dt = ExcelHelper.getClassFromExcelPackage<LocaleStringResource>(file.InputStream, 2,1);
    //          obj.StatusCode = 200;
    //          obj.Message = "Upload Success";
    //          try
    //          {
    //              DataTable dt = ExcelHelper.ToDataTable(file.InputStream);
    //              obj.Data = dt;
    //          }
    //          catch(Exception objEx)
    //          {
    //              obj.StatusCode = 400;
    //              obj.Message = objEx.Message;
    //          }

    //          return Content(JsonConvert.SerializeObject(obj), "application/json");
    //      }
    //      [HttpPost]
    //      public ContentResult SaveExcel(string item)
    //      {
    //          //string b = Request["item"];
    //          IEnumerable<LocaleStringResource> objItemList = JsonConvert.DeserializeObject<IEnumerable<LocaleStringResource>>(item);

    //          JsonObject obj = new JsonObject();
    //          obj.StatusCode = 200;
    //          obj.Message = "The process is sucessed";
    //          foreach (LocaleStringResource objitem in objItemList)
    //          {
    //              //default value
    //              //objitem.CreatedUser = CurrentUser.UserName;
    //              objitem.CreatedDate = SystemConfig.CurrentDate;
    //              objitem.CompanyID = CurrentUser.CompanyID;
    //              LocaleStringResourceManager.Add(objitem);
    //          }

    //          return Content(JsonConvert.SerializeObject(obj), "application/json");
    //      }
    // /// <summary>
    //      /// ExportExcel File
    //      /// </summary>
    //      /// <returns></returns>
    //      public string ExportExcel()
    //      {
    //          LocaleStringResourceCollection collection = LocaleStringResourceManager.GetAll(CurrentUser.CompanyID);
    //          DataTable dt = collection.ToDataTable<LocaleStringResource>();
    //          string fileName = "LocaleStringResource_" +SystemConfig.CurrentDate.ToString("MM-dd-yyyy") ;
    //          string[] RemoveColumn = { "CompanyID" , "TargetDisplayID", "ReturnDisplay","TotalRecord", "CreatedUser","CreatedDate"};
    //          for (int i = 0; i < RemoveColumn.Length; i++)
    //          {
    //              if (dt.Columns.Contains(RemoveColumn[i]))
    //              {
    //                  dt.Columns.Remove(RemoveColumn[i]);
    //              }
    //          }
    //          FileInputHelper.ExportExcel(dt, fileName, "LocaleStringResource List", false);
    //          return fileName;
    //      }
    //      #endregion
    //  }
}
using Biz.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Biz.Core;
using DataTables.Mvc;
using System.Data;
using Helpers;
using System.Reflection;
using Biz.Core.Converts;
using Biz.Core.Infrastructure;
using Biz.Core.Models;
using Biz.Core.Helpers;
using Biz.Core.Services;
using Autofac;
using Biz.Core.Models;
public class BaseController : Controller
{
    protected override void Initialize(RequestContext requestContext)
    {
        ///get controller name
        var routeValues = requestContext.HttpContext.Request.RequestContext.RouteData.Values;
        string controllerName = "";
        if (routeValues.ContainsKey("controller"))
            controllerName = (string)routeValues["controller"];
        else
            controllerName = string.Empty;
        ViewBag.Controller = controllerName;


        base.Initialize(requestContext);
    }

    public static DNHUsers CurrentUser
    {
        get
        {
            return CustomerAuthorize.CurrentUser;
        }

    }
    //protected virtual new CustomPrincipal User
    //{
    //    get { return HttpContext.User as CustomPrincipal; }
    //}

    
}

//[CustomAuthorizeAttribute]
public class BaseController<T> : Controller where T : class, new()
{

    protected override void Initialize(RequestContext requestContext)
    {
        ///get controller name
        var routeValues = requestContext.HttpContext.Request.RequestContext.RouteData.Values;
        string controllerName = "";
        string action = "";
        if (routeValues.ContainsKey("controller"))
            controllerName = (string)routeValues["controller"];
        else
            controllerName = string.Empty;
        ViewBag.Controller = controllerName;
        if (routeValues.ContainsKey("action"))
        {
            action = (string)routeValues["action"];
        }
        if (!CurrentUser.IsAdmin)
        {
            if (!CustomerAuthorize.CheckSiteMapPermission(CurrentUser, controllerName, action))
            {
                requestContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            }
        }
        base.Initialize(requestContext);
    }

    public static DNHUsers CurrentUser
    {
        get
        {
            return CustomerAuthorize.CurrentUser;
        }

    }
    protected virtual new CustomPrincipal User
    {
        get { return HttpContext.User as CustomPrincipal; }
    }
    #region Standard Event
    #region View List
    public virtual ActionResult Index()
    {
        //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
        return View(DNHClass.ViewFolder + "Index.cshtml");
    }
    public virtual ActionResult list()
    {
        //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
        return View(DNHClass.ViewFolder + "list.cshtml");
    }

    public virtual ActionResult ListUpload()
    {
        return View(DNHClass.ViewFolder + "ListUpload.cshtml");
    }
    #endregion

    #region Standard Event 
    [HttpGet]
    public virtual ActionResult Get(string ID)
    {
        JsonObject js = new JsonObject();
        try
        {
            var task = this.Create();
            if (task != null)
            {
                js.Data = task.Get(ID);
                js.ShowStatus = false;
            }
        }
        catch (Exception ex)
        {
            js.StatusCode = 400;
            js.Message = ex.Message;
        }
        return Content(JsonConvert.SerializeObject(js), "application/json");
    }

    /// <summary>
    /// use for setting up default value 
    /// </summary>
    /// <returns></returns>
    public virtual ActionResult Create(string TargetID = "Companylist")
    {
        var task = this.Create();
        if (task != null)
        {
            return View(DNHClass.ViewFolder + "Create.cshtml", task.Default());
        }
        else
        {
            return View(DNHClass.ViewFolder + "Create.cshtml", new T());
        }
    }
    [HttpPost]
    public virtual ActionResult Save(T model)
    {
        JsonObject js = new JsonObject();
        try
        {
            if (ModelState.IsValid)
            {
                js.StatusCode = 200;
                js.Message = "Upload Success";
                try
                {
                    var task = this.Create();
                    if (task != null)
                    {
                        //  string jsmodel = JsonConvert.SerializeObject(model);
                        task.Save(model);
                    }
                }
                catch (Exception objEx)
                {
                    js.StatusCode = 400;
                    js.Message = objEx.Message;
                }

            }
            else
            {
                js.StatusCode = 200;
                js.Message = "Fields Invalids";
               // return View(model);
            }

        }
        catch (Exception ObjEx)
        {
            js.StatusCode = 200;
            js.Message = ObjEx.Message;
            //LogHelper.AddLog(new IfindLog() {LinkUrl=Request.Url.AbsoluteUri,Exception= ObjEx.Message,Message = ObjEx.StackTrace});
            //return View(model);
        }
        return Content(JsonConvert.SerializeObject(js), "application/json");
    }

    /// <summary>
    /// use for setting up default value 
    /// </summary>
    /// <returns></returns>
    public virtual ActionResult Update(string ID)
    {
        string error = "";
        try
        {
            var task = this.Create();
            if (task != null)
            {
                string js = task.Get(ID);
                T objItem = JsonConvert.DeserializeObject<T>(js);
                return View(DNHClass.ViewFolder + "Create.cshtml", objItem);

            }
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        return View(DNHClass.ViewFolder + "Error.cshtml", error);
    }

    [HttpPost]
    public virtual ActionResult Evt(string[] KeyID, string Action)
    {
        var task = this.Create();
        if (task != null)
        {
            //  string jsmodel = JsonConvert.SerializeObject(model);


            // You have your books IDs on the deleteInputs array
            switch (Action.ToLower())
            {
                case "delete":

                    if (KeyID != null && KeyID.Length > 0)
                    {
                        int length = KeyID.Length;
                        T objItem;
                        for (int i = 0; i <= length - 1; i++)
                        {
                            objItem = JsonConvert.DeserializeObject<T>(task.Get(KeyID[i]));
                            if (objItem != null)
                            {
                                task.Del(objItem);
                                //CompanyManager.Delete(objItem);
                            }
                        }
                        return View(DNHClass.ViewFolder + "list.cshtml");
                    }
                    break;
            }
        }
        return View(DNHClass.ViewFolder + "list.cshtml");
    }

    #endregion

    [HttpPost]
    public virtual ContentResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    {
        SearchFilter mySearch = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, DNHClass.MyColumn, DNHClass.Key);

        var task = this.Create();
        DataTable table = new DataTable();
        string js = "";
        if (task != null)
        {
            //js = task.SearchData(SearchKey);
            table = task.SearchData(mySearch).ToDataTable<T>();
            //table = task.SearchData(SearchKey);
        }
        int TotalRecord = 0;
        if (table.Rows.Count > 0)
        {
            TotalRecord = (int)table.Rows[0]["TotalRecord"];
        }
        //{{Class}Collection data = {{Class}}Manager.GetAll(CurrentUser.CompanyID);
        return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, table, TotalRecord, TotalRecord)), "application/json");
        //return Json(new DataTablesResponseExtend(requestModel.Draw, table, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
    }

    public virtual ContentResult Search(string value)
    {

        var task = this.Create();

        if (task != null)
        {
            SearchFilter mySearch = new SearchFilter()
            {
                CompanyID = CurrentUser.CompanyID,
                Keyword = value,
                Page = 1,
                PageSize = 15,
                ColumnsName = DNHClass.MyColumn,
                OrderBy = DNHClass.Key,
                OrderDirection = "Desc",
                Condition = ""
            };
            //js = task.SearchData(SearchKey);
            IEnumerable<T> collection = task.SearchData(mySearch);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
            //table = task.SearchData(SearchKey);
        }
        return Content("0");

    }
    #endregion
    public virtual ActionResult headerLink()
    {
        //string ColumnName = CommonHelper.JsonColumnType<RBVHEmployee>();

        ////return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
        //return Content(ColumnName, "application/json");
        Type t = typeof(T);
        HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<T>(CurrentUser, t.ToString());
        return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
    }

    #region Method
    public virtual ContentResult ImportExcelFile()
    {
        JsonObject obj = new JsonObject();
        HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;

        obj.StatusCode = 200;
        obj.Message = "Upload Success";
        try
        {
            DataTable table = ExcelHelper.ToDataTable(file.InputStream);
            
            //Save File into system
            CommonHelper.SaveImportFile<T>(CurrentUser, DNHClass.TableName, file.FileName, table.ToList<T>());
            //get Type of system to importdata
            var task = this.Create();
            if (task != null)
            {
                table = task.ImportData(table);
            }
            IEnumerable<T> ErrorList = table.ToList<T>();
            if (table.Rows.Count > 0)
            {
                //Save ErrorList Intosystem for next time loadding
                CommonHelper.SaveImportErrorFile<T>(CurrentUser, DNHClass.TableName, ErrorList);
                obj.StatusCode = 400;
                obj.Data = ErrorList;
            }
        }
        catch (Exception objEx)
        {
            obj.StatusCode = 400;
            obj.Message = objEx.Message;
        }

        return Content(JsonConvert.SerializeObject(obj), "application/json");
    }
    public virtual void ExportExcel(string searchprm, int PageSize)
    {
        SearchFilter mySearch = BaseSearch(searchprm, PageSize);
        var task = this.Create();
        DataTable table = new DataTable();
        if (task != null)
        {
            table = task.SearchData(mySearch).ToDataTable<T>();
            Type t = typeof(T);
            HeaderItemCollection RemoveColumn = CommonHelper.HideColumn<T>(CurrentUser, t.ToString());
            for (int i = 0; i < RemoveColumn.Count; i++)
            {
                if (table.Columns.Contains(RemoveColumn[i].name))
                {
                    table.Columns.Remove(RemoveColumn[i].name);
                }
            }
        }
        FileInputHelper.ExportExcel(table, "Report_" + DNHClass.TableName + "_" + CommonHelper.GenerateRandomDigitCode(5), "Report", true);
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="Type">//Biz.Core.Services.Messages.QueuedMessagesSendTask, Biz.Core</param>
    /// <returns></returns>
    private IServiceManager<T> Create()
    {
        IServiceManager<T> task = null;

        var scope = EngineContext.Current.ContainerManager.Scope();
        var type2 = System.Type.GetType(DNHClass.ServiceMngClass);
        if (type2 != null)
        {
            object instance;
            //mapping string because ojbect and we use autofac
            if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
            {
                //not resolved
                instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
            }
            task = instance as IServiceManager<T>;
        }
        return task;
    }

    private SearchFilter BaseSearch(string searchprm, int PageSize)
    {
        SearchFilter SearchKey = new SearchFilter();
        SearchKey.ColumnsName = DNHClass.MyColumn;
        SearchKey.Page = 1;
        SearchKey.PageSize = PageSize;
        SearchKey.OrderBy = DNHClass.Key;
        SearchKey.OrderDirection = "Desc";
        //List<object> ObjPara = JsonConvert.DeserializeObject<object>(searchprm,
        //    new JsonSerializerSettings
        //    {
        //        NullValueHandling = NullValueHandling.Ignore
        //    });

        SearchKey.Condition = "";
        return SearchKey;
    }

    private ObjectClass DNHClass
    {
        get
        {
            try
            {
                Type t = typeof(T);
                string[] NamespaceArr = t.ToString().Split('.');
                string classFullName = t.FullName; //ifinds.Object.CS.Company
                string className = t.Name;
                string dll = t.Assembly.GetName().Name;// ifinds.Object.CS
                string typeName = dll + ".Services." + className + "Manager, " + dll;
              //  HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<T>(CurrentUser, t.ToString());
                HeaderItemCollection ColumnName = CommonHelper.UserSearchColumn<T>(CurrentUser, t.ToString());
                string column = "";
                bool isFirst = true;
                string key = "";
               
                for (int i = 0; i < ColumnName.Count; i++)
                {
                    if (isFirst)
                    {
                        key = ColumnName[i].name;
                        isFirst = false;
                    }
                    column += ColumnName[i].name;
                    if (i != ColumnName.Count - 1)
                    {
                        column += ",";
                    }
                }
                return new ObjectClass()
                {
                    Key = key,
                    dll = dll,
                    ServiceMngClass = typeName,
                    MyType = t.ToString(),
                    MyColumn = column,
                    ViewFolder = "~/Views/" + NamespaceArr[1] + "/" + className + "/",
                    TableName = className
                };
            }
            catch
            {
            }
            return null;
        }
    }
}
public class ObjectClass
{
    public string Key { get; set; }
    public string TableName { get; set; }
    public string MyColumn { get; set; }
    public string MyType { get; set; }
    public string dll { get; set; }
    public string ServiceMngClass { get; set; }
    public string GetAllClass { get; set; }
    public string ViewFolder { get; set; }

}


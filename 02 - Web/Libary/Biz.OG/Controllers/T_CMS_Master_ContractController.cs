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
using Biz.Core;
using System.Reflection;

namespace Biz.OG.Controllers
{
    //[CustomAuthorize]
    //[AccessRights("Contract")]
    public class ContractController :  BaseController
    {
		string ViewFolder = "~/Views/OG/T_CMS_Master_Contract/";
        string screenID = "Contract";
        public ActionResult Index()
        {
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            return View(ViewFolder+"Index.cshtml");
        }

		public ActionResult list()
        {
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            return View(ViewFolder + "list.cshtml");
        }
        public ActionResult ContractHistory()
        {
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            return View(ViewFolder + "ContractHistory.cshtml");
        }
       
        public ActionResult ApendixHistory()
        {
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            return View(ViewFolder + "ApendixHistory.cshtml");
        }

        public ActionResult ContractPending()
        {
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            return View(ViewFolder + "ContractPending.cshtml");
        }
        [HttpPost]
        public ContentResult Search(SearchFilter SearchKey)
        {
		    SearchKey.OrderBy = string.IsNullOrEmpty(SearchKey.OrderBy) ? "ID" : SearchKey.OrderBy;
            T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);
            return Content(JsonConvert.SerializeObject(collection), "application/json");
        }

		//public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
  //      {
  //          SearchFilter SearchKey = new SearchFilter()
  //          {
                
  //              Keyword = requestModel.Search.Value,
  //              Page = requestModel.Start + 1,
  //              PageSize = requestModel.Length,
  //              ColumnsName = "ID",
  //              OrderBy = "ID",
  //              OrderDirection="",
  //          };
  //          T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);
  //          int TotalRecord = 0;
  //          if (collection.Count > 0)
  //          {
  //              TotalRecord = collection[0].TotalRecord;
  //          }
  //          //T_CMS_Master_ContractCollection data = T_CMS_Master_ContractManager.GetAll(CurrentUser.CompanyID);
  //          return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
  //      }

        public JsonResult GetGata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            SearchFilter SearchKey = SearchFilter.SearchData(CurrentUser.CompanyID, requestModel, "ID,EmployeeCode,MiddleName_EN,LastName_EN,FirstName_EN,DeptID,ContractNo,HighestDegree", "ID","","Desc");
            SearchKey.Condition = " tbl.IsActive=0 and T_COm_Master_Org.Entity_Id="+CurrentUser.EntityID+" and tbl.employeeCode not in (  select EmployeeCode from T_COM_Master_Employee_Position  where T_COM_Master_Employee_Position.IsAcitve=1)  ";
            T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //{{Class}Collection data = {{Class}}Manager.GetAll(CurrentUser.CompanyID);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,string searchprm)
        {
            SearchFilter SearchKey = BindSearch(searchprm);
            SearchKey  = SearchFilter.SearchData(SearchKey, requestModel);
            T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            //{{Class}Collection data = {{Class}}Manager.GetAll(CurrentUser.CompanyID);
            return Json(new DataTablesResponse(requestModel.Draw, collection, TotalRecord, TotalRecord), JsonRequestBehavior.AllowGet);
        }
        public ContentResult GetContractHistoryData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);

            DataTable dt = T_CMS_Master_ContractManager.CMS_ContractHistoryReport(int.Parse(ObjPara.EntityId), SystemConfig.CurrentDate, true);
            int TotalRecord = 0;
            if (dt.Rows.Count > 0)
            {
                TotalRecord = dt.Rows.Count;
            }
            //{{Class}Collection data = {{Class}}Manager.GetAll(CurrentUser.CompanyID);
            IEnumerable<DataRow> rows = dt.AsEnumerable().ToList();
            object temp = new object();
            foreach (var item in rows)
            {
                temp = item.Table;
            }
            return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord)), "application/json");
        }

        public ContentResult GetContractPendingData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)
        {
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               });
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(SearchKey);

            DataTable dt = T_CMS_Master_ContractManager.CMS_ContractPendingReport(int.Parse(ObjPara.EntityId), SystemConfig.CurrentDate, true);
            int TotalRecord = 0;
            if (dt.Rows.Count > 0)
            {
                TotalRecord = dt.Rows.Count;
            }
            //{{Class}Collection data = {{Class}}Manager.GetAll(CurrentUser.CompanyID);
            IEnumerable<DataRow> rows = dt.AsEnumerable().ToList();
            object temp = new object();
            foreach (var item in rows)
            {
                temp = item.Table;
            }
            return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord)), "application/json");
        }

        public void CMSHistory(string searchprm)
        {
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
              new JsonSerializerSettings
              {
                  NullValueHandling = NullValueHandling.Ignore
              });
            DataTable dt = new DataTable();
            try
            {
                bool isActive = false;
                if (ObjPara.IsActive!=null && ObjPara.IsActive.ToLower() == "on") isActive = true;
                 dt = T_CMS_Master_ContractManager.CMS_ContractHistoryReport(int.Parse(ObjPara.EntityId), null, isActive);
            }
            catch
            {

            }
            FileInputHelper.ExportExcel(dt, "CMS_History_" + SystemConfig.CurrentDate.ToShortDateString(), "Contract History List", false);

        }
        public void ApendixHisReport(string searchprm)
        {
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
              new JsonSerializerSettings
              {
                  NullValueHandling = NullValueHandling.Ignore
              });
            DataTable dt = new DataTable();
            try
            {
                bool isActive = false;
                if (ObjPara.IsActive != null && ObjPara.IsActive.ToLower() == "on") isActive = true;
                dt = T_CMS_Master_ContractManager.CMS_AppendixHistoryReport(int.Parse(ObjPara.EntityId), null, isActive);
            }
            catch
            {

            }
            FileInputHelper.ExportExcel(dt, "Apendix_History_"+SystemConfig.CurrentDate.ToShortDateString(), "Apendix History List", false);

        }

        public void ContractPendingReport(string searchprm)
        {
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
              new JsonSerializerSettings
              {
                  NullValueHandling = NullValueHandling.Ignore
              });
            DataTable dt = new DataTable();
            try
            {
                bool isActive = false;
                if (ObjPara.IsActive != null && ObjPara.IsActive.ToLower() == "on") isActive = true;
                dt = T_CMS_Master_ContractManager.CMS_ContractPendingReport(int.Parse(ObjPara.EntityId), null, isActive);
            }
            catch
            {

            }
            FileInputHelper.ExportExcel(dt, "Contract_Pending_" + SystemConfig.CurrentDate.ToShortDateString(), "Contract Pending List", false);

        }
        public ActionResult Get(int ID)
        {
            T_CMS_Master_Contract objItem = T_CMS_Master_ContractManager.GetById(ID);
            return View(objItem);
        }

		[HttpGet]
		 public ActionResult Get(int ID, string action)
        {
            T_CMS_Master_Contract objItem = T_CMS_Master_ContractManager.GetById(ID);
             return Content(JsonConvert.SerializeObject(objItem), "application/json");
        }


        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(ViewFolder + "Create.cshtml",new T_CMS_Master_Contract()
            {
				ID = 0
            });
        }

        [HttpPost]
        public ActionResult Create(T_CMS_Master_Contract model)
        {
            try
            {
              //  ModelState.Remove("dateEffetPosition");
                if (ModelState.IsValid)
                {
                    //model.CompanyID= CurrentUser.CompanyID;
                    // model.CompanyID = CurrentUser.CompanyID;
                    // model.CreatedUser = CurrentUser.UserName;
                    if (model.ID != 0)
                    {
                        //get default value
                        T_CMS_Master_Contract b = T_CMS_Master_ContractManager.GetById(model.ID);
                        T_CMS_Master_EmploymentType objEmpType;
                        T_CMS_Master_EmploymentSubType objEmpSubType;
                        T_COm_Master_Org objOrg;
                        b.ModifiedDate = SystemConfig.CurrentDate;

                        b.FirstName_EN = model.FirstName_EN.ToUpper();
                        b.LastName_EN = model.LastName_EN.ToUpper();
                        b.MiddleName_EN = model.MiddleName_EN.ToUpper();
                        b.FirstName_VN = model.FirstName_VN.ToUpper();
                        b.LastName_VN = model.LastName_VN.ToUpper();
                        b.MiddleName_VN = model.MiddleName_VN.ToUpper();

                        b.IDCardNo = model.IDCardNo;
                        b.IDPOI = model.IDPOI;
                        b.DOB = model.DOB;
                        b.PassportNo = model.PassportNo;
                        b.PassportPOI = model.PassportPOI;
                        b.LabourDOI = model.LabourDOI;
                        b.PerAddress = model.PerAddress;
                        b.IDDOI = model.IDDOI;
                        b.HighestDegree = model.HighestDegree;
                        b.POB = model.POB;
                        b.PassportDOI = model.PassportDOI;
                        b.LabourBookNo = model.LabourBookNo;
                        b.LabourPOI = model.LabourPOI;
                        b.Remarks = model.Remarks;


                        b.DeptID = model.DeptID;
                        b.LocationID = model.LocationID;
                        b.ContractTerm = model.ContractTerm;
                        b.PositionID = model.PositionID;
                        b.AnnualLeave = model.AnnualLeave;
                        b.WorkHoursID = model.WorkHoursID;

                        //duoble check to get AnuualLeave
                        b.GradeID = model.GradeID;
                        b.AnnualLeave = model.AnnualLeave;
                        b.ContractTerm = model.ContractTerm;
                        b.EmpSubTypeID = model.EmpSubTypeID;
                        b.SalutationID = model.SalutationID;

                        objOrg = T_COm_Master_OrgManager.GetById(model.DeptID);
                        if (objOrg != null)
                        {
                            objEmpType = T_CMS_Master_EmploymentTypeManager.GetAllByEntityID(objOrg.Entity_Id, "C");
                            objEmpSubType = T_CMS_Master_EmploymentSubTypeManager.GetById(objEmpType.EmpTypeID);
                        }
                        int ProbationID = 0;
                        
                        if (model.ContractTerm == 0)
                        {
                            //b.EmpSubTypeID = 101;//seasonable Sub-Type
                            b.ProbationsPeriod = 0;
                        }else
                        {
                            b.ProbationsPeriod = model.ContractTerm / 12;
                        }
                        //else if (model.ContractTerm < 12)
                        //{
                        //  //  b.EmpSubTypeID = 103;//seasonable Sub-Type
                        //    b.ProbationsPeriod = null;
                        //}
                        //else
                        //{
                        //    //b.EmpSubTypeID = 102;//seasonable Sub-Type
                        //    b.ProbationsPeriod = 2;
                        //}
                        
                      
                        b.Grossoffer = model.Grossoffer;
                        //model.CreatedDate = b.CreatedDate;
                        //  model.CreatedUser = b.CreatedUser;
                        b.Joiningdate = model.Joiningdate;
                        b.Enddate = model.Enddate;
                        b.ModifiedBy = CurrentUser.EmployeeCode;
                        b.OriginalDate = SystemConfig.CurrentDate;

                        ///set default value only
                        b.HomeGrossOfferEffectiveFrom = SystemConfig.CurrentDate;
                        b.HostGrossOfferEffectiveFrom = SystemConfig.CurrentDate;
                        b.HomeGrossOfferEffectiveTo = SystemConfig.CurrentDate;
                        b.HostGrossOfferEffectiveTo = SystemConfig.CurrentDate;
                        b.WorkPermitTo = model.Enddate;
                        b.OriginalDate = SystemConfig.CurrentDate;
                        b.WorkPermitFrom = SystemConfig.CurrentDate;

                        //if (model.LabourDOI.ToString().IndexOf("01/01/0001") == -1)
                        //{
                        //    b.LabourDOI =null;
                        //}
                        //if (model.PassportDOI.ToString().IndexOf("01/01/0001") == -1)
                        //{
                        //    model.PassportDOI = null;
                        //}
                        T_CMS_Master_ContractManager.Update(b);

                    }
                    else
                    {
                        // TODO: Add insert logic here
                        model.CreatedDate = SystemConfig.CurrentDate;
                        T_CMS_Master_ContractManager.Add(model);

                    }
                    return View(ViewFolder + "list.cshtml");
                }

            }
            catch
            {
                return View(ViewFolder + "Create.cshtml", model);
            }
            return View(ViewFolder + "Create.cshtml", model);
        }

        /// <summary>
        /// use for setting up default value 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int ID)
        {
            T_CMS_Master_Contract objItem = T_CMS_Master_ContractManager.GetById(ID);
         
            return View(ViewFolder + "Create.cshtml",objItem);
        }

        [HttpPost]
        public ActionResult Update(T_CMS_Master_Contract model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    T_CMS_Master_ContractManager.Update(model);
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
        public ContentResult T_CMS_Master_ContractEvt(int[] ID, string Action)
        {
            // You have your books IDs on the deleteInputs array
            JsonObject obj = new JsonObject();
            switch (Action.ToLower())
            {
                case "delete":

                    if (ID != null && ID.Length > 0)
                    {
                        int length = ID.Length;
                        T_CMS_Master_Contract objItem;
                        for (int i = 0; i <= length - 1; i++)
                        {
                            objItem = T_CMS_Master_ContractManager.GetById(ID[i]);
                            if (objItem != null)
                            {
                                T_CMS_Master_ContractManager.Delete(objItem);
                            }
                        }

                    }
                    break;
                case "print":

                    if (ID != null && ID.Length > 0)
                    {
                        int length = ID.Length;
                        ContractReportCollection objList = new ContractReportCollection();
                        T_CMS_Master_Contract objItem;
                        ContractReport objReportItem;
                        
                        for (int i = 0; i <= length - 1; i++)
                        {
                            objItem = T_CMS_Master_ContractManager.GetById(ID[i]);
                            if (objItem.ID != 0)
                            {
                                objItem.StatusID = 3;
                                //objItem.IsActive = true;
                                objItem.ModifiedDate = SystemConfig.CurrentDate;
                                T_CMS_Master_ContractManager.Update(objItem);
                                objReportItem = new ContractReport()
                                {
                                    ID = CommonHelper.EncyptURLString(objItem.ID.ToString()),
                                    EntityId = CommonHelper.EncyptURLString(objItem.CMSOrg.Entity_Id.ToString()),
                                    EmployeeCode= CommonHelper.EncyptURLString(objItem.EmployeeCode.ToString()),
                                    filepath = CommonHelper.EncyptURLString("Templates\\Rpt_Contract_RBVH.rdlc"),
                                    Mode = CommonHelper.EncyptURLString("CONTRACT"),
                                    filetype= "PDF"
                                };
                                //objItem.ID = CommonHelper.EncyptURLString(objItem.ID);

                                objList.Add(objReportItem);
                            }
                        }
                        obj.Data = objList;

                    }
                    break;
            }

            return Content(JsonConvert.SerializeObject(obj), "application/json");
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
                IEnumerable<T_CMS_Master_Contract> objItemList = dt.ToList<T_CMS_Master_Contract>();
             //   CommonHelper.SaveImportFile<T_CMS_Master_Contract>(CurrentUser, "T_CMS_Master_Contract", file.FileName, objItemList);
                T_CMS_Master_ContractCollection ErrorList = T_CMS_Master_ContractManager.ImportData(objItemList, CurrentUser.EmployeeCode);
                if (ErrorList.Count > 0) {
                    obj.Data = ErrorList;
                }
                if (ErrorList.Count > 0)
                {
                    CommonHelper.SaveImportErrorFile<T_CMS_Master_Contract>(CurrentUser, "T_CMS_Master_Contract", ErrorList);
                    obj.StatusCode = 400;
                    obj.Message = "Can't import into system :" + ErrorList.Count + "/" + objItemList.Count<T_CMS_Master_Contract>();
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
            JsonObject obj = new JsonObject();
            try
            {
                IEnumerable<T_CMS_Master_Contract> objItemList = JsonConvert.DeserializeObject<IEnumerable<T_CMS_Master_Contract>>(item,
                   new JsonSerializerSettings
                   {
                       NullValueHandling = NullValueHandling.Ignore
                   });

                obj.StatusCode = 200;
                obj.Message = "The process is sucessed";

                T_CMS_Master_ContractCollection ErrorList = T_CMS_Master_ContractManager.ImportData(objItemList, CurrentUser.EmployeeCode);
                obj.Data = ErrorList;
                if (ErrorList.Count > 0)
                {
                    obj.StatusCode = 400;
                    obj.Message = "Can't import into system :" + ErrorList.Count +"/"+ objItemList.Count<T_CMS_Master_Contract>();
                }
               
            }
            catch (Exception objEx)
            {
                obj.StatusCode = 400;
                obj.Message = objEx.Message;
            }


            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }
        public SearchFilter BindSearch(string searchprm)
        {
            SearchFilter SearchKey = new SearchFilter();
            SearchKey.OrderBy = "ID";
            SearchKey.ColumnsName = "ID,EmployeeCode,MiddleName_EN,LastName_EN,FirstName_EN,DeptID,ContractNo,HighestDegree";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderBy = "ID";
            SearchKey.OrderDirection = "Desc";
            ContractSearchpara ObjPara = JsonConvert.DeserializeObject<ContractSearchpara>(searchprm,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            string isactive = "0";
            string strDefaultEntity = "10001";
            string strDefaultSearch = "  tbl.employeeCode not in (  select EmployeeCode from T_COM_Master_Employee_Position  where T_COM_Master_Employee_Position.IsAcitve=1) and ";

            if (ObjPara != null)
            {
                if (ObjPara.IsActive != null)
                {
                    isactive = "1";

                }
                else
                {
                    SearchKey.Condition = strDefaultSearch;
                }
                if (!string.IsNullOrEmpty(ObjPara.EntityId))
                {
                    strDefaultEntity = ObjPara.EntityId;
                }
                if (!string.IsNullOrEmpty(ObjPara.EmpCode))
                {
                    SearchKey.Condition += " (tbl.EmployeeCode like '%" + ObjPara.EmpCode + "%' or  tbl.ContractNo like '%" + ObjPara.EmpCode + "%') and ";
                }
                if (!string.IsNullOrEmpty(ObjPara.EmpName))
                {

                    SearchKey.Keyword = ObjPara.EmpName;
                    //SearchKey.Condition += " (tbl.LastName_EN + '' '' + tbl.MiddleName_EN + '' '' + tbl.FirstName_EN like ''%"+ ObjPara.EmpName + "%'') and ";

                }
                if (!string.IsNullOrEmpty(ObjPara.start))
                {
                    string[] startDate = ObjPara.start.Split('/');
                    string[] EnDate = ObjPara.end.Split('/');
                    if (startDate.Length > 1)
                    {
                        string startD = startDate[2] + startDate[1] + startDate[0];
                        string EndD = startD;
                        if (EnDate.Length > 1)
                        {
                            EndD = EnDate[2] + EnDate[1] + EnDate[0];
                        }
                        SearchKey.Condition += " (tbl.Joiningdate >='" + startD + "' and tbl.Joiningdate<='" + EndD + "') and ";
                    }
                }
            }
            SearchKey.Condition += " tbl.IsActive=" + isactive + " and T_COm_Master_Org.Entity_Id=" + strDefaultEntity + "   ";
            return SearchKey;
        }
        /// <summary>
        /// ExportExcel File
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult headerLink()
        {
            //string ColumnName = CommonHelper.JsonColumnType<T_CMS_Master_Contract>();
            HeaderItemCollection ColumnName = CommonHelper.UserConfigPageFolder<T_CMS_Master_Contract>(CurrentUser, screenID);
            return Content(JsonConvert.SerializeObject(ColumnName), "application/json");
        }
     

       
        public void ExportErrorList()
        {
            DataTable mydata = CommonHelper.GetImportErrorFile<T_CMS_Master_Contract>(CurrentUser, "T_CMS_Master_Contract");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord",
                "CreatedUser", "CreatedDate","StatusID", "ApproverLevel","ContractLevel","SalutationID","PositionID","LocationID",
                "PassportNo","PassportDOI","PassportPOI","LabourBookNo","LabourDOI",
                "LabourPOI","AnnualLeave","EmpTypeID",
                "EmpSubTypeID","WorkPermitNo","WorkPermitFrom","WorkPermitTo",
                "HomeGrossOffer","HomeGrossOfferEffectiveFrom",
                "HomeGrossOfferEffectiveTo","RelocationallowanceCurrency","Relocationallowance",
                "GoabroadallowanceCurrency","Goabroadallowance","WaivingallowanceCurrency","Waivingallowance","HostCountryCurrency",
                "HostGrossOfferEffectiveFrom","HostGrossOfferEffectiveTo","Remarks","CreatedBy","ModifiedBy",
                "ModifiedDate","IsActive","OriginalDate","WorkFlowStatus","HandPhone","WorkHoursID","ContractTerm","ProbationsPeriod","IDPOI","Fullname",
                "EmployeeCode","ContractNo","EmployeeNO","EmployeeInfo","DirectCode","INDirectCode",

                //"FirstName_EN","MiddleName_EN","LastName_EN","ContractNo","EmployeeCode",
            "GradeID","CMSOrg","Mode","DeptID","ErrorMesssage"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (mydata.Columns.Contains(RemoveColumn[i]))
                {
                    mydata.Columns.Remove(RemoveColumn[i]);
                }
            }
            FileInputHelper.ExportExcel(mydata, "Contract_Error_List", "Contract_Error_List", false);
        }
        public void ExportExcel(string searchprm)
        {
            DataSet ds = new DataSet();
            DataTable mydata;
            int RBVH = 10001;
            //T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.GetAll(false);
            SearchFilter mySearch = BindSearch(searchprm);
       
            T_CMS_Master_ContractCollection collection = T_CMS_Master_ContractManager.Search(mySearch);
            
             mydata = collection.ToDataTable<T_CMS_Master_Contract>();
            string fileName = "Contract_" + SystemConfig.CurrentDate.ToString("MM-dd-yyyy");
            string[] RemoveColumn = { "CompanyID", "TargetDisplayID", "ReturnDisplay", "TotalRecord",
                "CreatedUser", "CreatedDate","StatusID", "ApproverLevel","ContractLevel","SalutationID","PositionID","LocationID",
                "PassportNo","PassportDOI","PassportPOI","LabourBookNo","LabourDOI",
                "LabourPOI","AnnualLeave","EmpTypeID",
                "EmpSubTypeID","WorkPermitNo","WorkPermitFrom","WorkPermitTo",
                "HomeGrossOffer","HomeGrossOfferEffectiveFrom",
                "HomeGrossOfferEffectiveTo","RelocationallowanceCurrency","Relocationallowance",
                "GoabroadallowanceCurrency","Goabroadallowance","WaivingallowanceCurrency","Waivingallowance","HostCountryCurrency",
                "HostGrossOfferEffectiveFrom","HostGrossOfferEffectiveTo","Remarks","CreatedBy","ModifiedBy",
                "ModifiedDate","IsActive","OriginalDate","WorkFlowStatus","HandPhone","TempAddress","WorkHoursID","ContractTerm","ProbationsPeriod","IDPOI","Fullname",
                //"FirstName_EN","MiddleName_EN","LastName_EN","ContractNo","EmployeeCode",
            "GradeID","CMSOrg","Mode","DeptID","ErrorMesssage"};
            for (int i = 0; i < RemoveColumn.Length; i++)
            {
                if (mydata.Columns.Contains(RemoveColumn[i]))
                {
                    mydata.Columns.Remove(RemoveColumn[i]);
                }
            }
            ds.Tables.Add(mydata);

            //DNH Position
            T_COM_Master_PositionCollection deColection = T_COM_Master_PositionManager.GetAll();
             mydata = deColection.ToDataTable<T_COM_Master_Position>();
            ds.Tables.Add(mydata);

            //DNH Grade
            T_COM_Master_GradeCollection objGrades = T_COM_Master_GradeManager.GetAll();
            mydata = objGrades.ToDataTable<T_COM_Master_Grade>();
            ds.Tables.Add(mydata);

            //DN Saluation

            T_CMS_Master_SalutationCollection objSalutation = T_CMS_Master_SalutationManager.GetAll();
            mydata = objSalutation.ToDataTable<T_CMS_Master_Salutation>();
            ds.Tables.Add(mydata);

            //DN Working Hours
            T_CMS_Master_WorkHoursCollection objWK = T_CMS_Master_WorkHoursManager.GetAll();
            //T_CMS_InterfaceLacviet_WorkingHoursCollection objWK = T_CMS_InterfaceLacviet_WorkingHoursManager.GetAll();
            mydata = objWK.ToDataTable<T_CMS_Master_WorkHours>();
            ds.Tables.Add(mydata);

            //DN Working Contract Term
            T_CMS_Static_ContractTermCollection objCT = T_CMS_Static_ContractTermManager.GetAll();
            mydata = objCT.ToDataTable<T_CMS_Static_ContractTerm>();
            ds.Tables.Add(mydata);

            //DN Working Probation
            T_CMS_Static_PeriodOfProbationCollection objPr = T_CMS_Static_PeriodOfProbationManager.GetAll();
            objPr.Add(new T_CMS_Static_PeriodOfProbation()
            {
                ID = 100,
                Period = 0,
            });
            mydata = objPr.ToDataTable<T_CMS_Static_PeriodOfProbation>();
            ds.Tables.Add(mydata);

            //DN  Degree
            T_COM_Master_DegreeCollection objDg = T_COM_Master_DegreeManager.GetAll();
            mydata = objDg.ToDataTable<T_COM_Master_Degree>();
            ds.Tables.Add(mydata);

            //DN Working Location
            T_COM_Master_LocationCollection objLocation = T_COM_Master_LocationManager.GetAllByEntityID(RBVH);
            mydata = objLocation.ToDataTable<T_COM_Master_Location>();
            ds.Tables.Add(mydata);

            //DN Place of Issues
            T_COM_Master_PlaceOfIssueCollection objPOI = T_COM_Master_PlaceOfIssueManager.GetAll();
            mydata = objPOI.ToDataTable<T_COM_Master_PlaceOfIssue>();
            ds.Tables.Add(mydata);

            //DN Place of Issues
            T_COm_Master_OrgCollection objDepartment = T_COm_Master_OrgManager.GetAll();
         
            mydata = objDepartment.ToDataTable<T_COm_Master_Org>();
            ds.Tables.Add(mydata);

            // ds.Tables.Add(dt2);
            //ds.Tables.Add(dt3);
            List<string> groupCodeList = new List<string>();
            string[] sheetName = { "Contract", "Position", "Grade", "Saluation", "Working Hours", "Contract Term", "Probation", "Degree", "Location","Place Of Issue","Department" };
            List<DropdownModelExcel> listDrop = new List<DropdownModelExcel>()
            {
                new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=11,
                    ColumnShowDropData=2,
                    RangeSheetData="B2:B"+(objDepartment.Count+1),
                    IsHideSheetDataDrop=true
                },
                new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=4,
                    ColumnShowDropData=3,
                    RangeSheetData="B2:B"+(objSalutation.Count+1),
                    IsHideSheetDataDrop=true
                },
                 new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=3,
                    ColumnShowDropData=10,
                    RangeSheetData="B2:B"+(objGrades.Count+1),
                    IsHideSheetDataDrop=true
                },
                new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=2,
                    ColumnShowDropData=11, //column 12
                    RangeSheetData="B2:B"+(deColection.Count+1),
                    IsHideSheetDataDrop=true
                },//location
                  new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=9,
                    ColumnShowDropData=16, //column 16 -Location
                    RangeSheetData="B2:B"+(objLocation.Count+1),
                    IsHideSheetDataDrop=true
                },
                  new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=5,
                    ColumnShowDropData=17, //column 16
                    RangeSheetData="B2:B"+(objWK.Count+1),
                    IsHideSheetDataDrop=true
                },
                   new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=6,
                    ColumnShowDropData=18, //column 16
                    RangeSheetData="B2:B"+(objCT.Count+1),
                    IsHideSheetDataDrop=true
                },
                   //propration Period
                    new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=7,
                    ColumnShowDropData=19, //column 16
                    RangeSheetData="B2:B"+(objPr.Count+1),
                    IsHideSheetDataDrop=true
                },
                   new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=8,
                    ColumnShowDropData=25, //column 16
                    RangeSheetData="B2:B"+(objDg.Count+1),
                    IsHideSheetDataDrop=true
                },
                   //ID Place of Birth
                    new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=10,
                    ColumnShowDropData=21, //column 16
                    RangeSheetData="D2:D"+(objPOI.Count+1),
                    IsHideSheetDataDrop=true
                },
                   //ID Place of issues
                    new DropdownModelExcel()
                {
                    SheetShowDrop=1,
                    SheetDataDrop=10,
                    ColumnShowDropData=24, //column 16
                    RangeSheetData="B2:B"+(objPOI.Count+1),
                    IsHideSheetDataDrop=true
                },


            };
            //set Column Date Format 
            int[] DateColumn = { 14, 15,20,23 };
            ExcelPara mypara = new ExcelPara()
            {
                ds = ds,
                sheetName=sheetName,
                fileName=fileName,
                list=listDrop,
                DateColumns = DateColumn,
                DateFormat ="dd-mmm-yyyy"
            };
            //FileInputHelper.ExportExcel(dt, fileName, "Contract List", false);
             FileInputHelper.ExportMultiSheetExcelExtend(mypara);
            //return fileName;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Biz.OG.Models;
using Biz.Core.Models;
using Biz.Core.Domain.Messages;
using Biz.OG.Const;

namespace Biz.OG.Services
{
    public class T_CMS_Master_ContractManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_CMS_Master_Contract.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_CMS_Master_Contract.{0}";
        private const string SETTINGS_CompanyID_Probation = "ifinds.Models.T_CMS_Master_Contract.Probation{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.T_CMS_Master_Contract.USer.{0}";
        private const string SETTINGS_EmployeeCode_KEY = "ifinds.Models.T_CMS_Master_Contract.EmployeeCode.{0}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.T_CMS_Master_Contract.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "Contract";
        #endregion
        public static T_CMS_Master_ContractCollection GetAll(bool isActive)
        {
            T_CMS_Master_ContractCollection items = new T_CMS_Master_ContractCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_ContractCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                //HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?isActive={0}", isActive)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_ContractCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        /// <summary>
        /// return the Probation Email List
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static QueuedEmailCollection GetProbationContractList(int CompanyID)
        {

            QueuedEmailCollection items = new QueuedEmailCollection();
            
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                //HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}", CompanyID)).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<QueuedEmailCollection>().GetAwaiter().GetResult();
                }
            }
            return items;
        }
        public static T_CMS_Master_Contract GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Master_Contract)obj2; }

            T_CMS_Master_Contract b = new T_CMS_Master_Contract();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_Contract>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_CMS_Master_Contract GetbyEmpCode(int EmployeeCode)
        {
            string key = String.Format(SETTINGS_EmployeeCode_KEY, EmployeeCode);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Master_Contract)obj2; }

            T_CMS_Master_Contract b = new T_CMS_Master_Contract();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EmployeeCode={0}&Type=empcode", EmployeeCode)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_Contract>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_CMS_Master_Contract Add(T_CMS_Master_Contract objItem)
        {
            T_CMS_Master_Contract b = new T_CMS_Master_Contract();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_Contract>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_CMS_Master_Contract objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.ID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        /// <summary>
        /// import data from excel file 
        /// </summary>
        /// <param name="objItem"></param>
        /// <returns>return the error Import List</returns>
        public static T_CMS_Master_ContractCollection ImportData(IEnumerable<T_CMS_Master_Contract> objItemList,int EmployeeCode)
        {
            T_COm_Master_Org objOrg;
            T_COM_Master_Grade objGrade;
            T_COM_Master_Position objPosition;
            T_CMS_Master_WorkHours objWH;
            T_COM_Master_PlaceOfIssue objPOI;
            T_CMS_Master_EmploymentSubType objEmpSubType;
            int SaluationID = 1;
            T_CMS_Master_ContractCollection ErrorList = new T_CMS_Master_ContractCollection();
            bool isError = false;
            string strError = "";
            T_CMS_Master_EmploymentType objEmpType;
            T_COM_Master_EntityCollection EntityAllows = T_COM_Master_EntityManager.GetAllByEmployeeCode(EmployeeCode);
            string EntityList = "";
            foreach(T_COM_Master_Entity entity in EntityAllows)
            {
                EntityList += entity.EntityId + ";";
            }
            foreach (T_CMS_Master_Contract objitem in objItemList)
            {
                isError = false;
                strError = "";
                try
                {
                    objPosition = T_COM_Master_PositionManager.GetByName(objitem.PostionName);
                    string[] strPosition = objitem.PostionName.Split('-');
                    if (objPosition.PositionID == 0)
                    {
                        if (strPosition.Length > 0)
                        {
                            objitem.PostionName = strPosition[0];
                        }
                    }
                    string[] strPOI = objitem.IDPOICD.Split('-');
                    if (strPOI.Length > 0)
                    {
                        objitem.IDPOICD = strPOI[0];
                    }
                    objOrg = T_COm_Master_OrgManager.GetByOrgName(objitem.DepartCD);

                    //check permission that you can add to this Entity or not 
                    if (EntityList.IndexOf(objOrg.Entity_Id.ToString()) == -1)
                    {
                        strError += "<div>You don't have Permission to Import to this Group: <span class='impval'>" + objitem.DepartCD + " </span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Grade Name:" + objitem.GradeName));
                        isError = true;
                    }
                    
                    objGrade = T_COM_Master_GradeManager.GetByName(objitem.GradeName);
                    objPosition = T_COM_Master_PositionManager.GetByName(objitem.PostionName);
                    objWH = T_CMS_Master_WorkHoursManager.GetByWorkHours(objitem.WorkHoursCD);
                    
                    objPOI = T_COM_Master_PlaceOfIssueManager.GetByPOI_Name_VN(objitem.IDPOICD.Trim());

                    if (objGrade.GradeID == 0)
                    {
                        strError += "<div>Invalid Grade Name: <span class='impval'>" + objitem.GradeName + " </span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Grade Name:" + objitem.GradeName));
                        isError = true;
                    }
                    if (objPosition.PositionID == 0)
                    {
                        strError += "<div>Invalid Postion Name: <span class='impval'>" + objitem.PostionName + " </span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Postion Name:" + objitem.PostionName));
                        isError = true;
                    }
                    if (objWH.WorkHoursID == 0)
                    {
                        strError += "<div>Invalid  Work Hours Name: <span class='impval'>" + objitem.WorkHoursCD + "</span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Work Hours Name:" + objitem.WorkHoursCD));
                        isError = true;
                    }
                    if (objPOI.POI_ID == 0)
                    {
                        strError += "<div>Invalid  Place of Issues Name: <span class='impval'>" + objitem.IDPOICD + "</span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Place of Issues Name:" + objitem.IDPOICD));
                        isError = true;
                    }

                    if (isError)
                    {
                        ErrorList.Add(AddErrorMessage(objitem, strError));
                        continue;
                    }
                    //if we dont have the org get the default value for Org/Department ID
                    if (objOrg.OrgId == 0)
                    {
                        //objOrg.OrgId = 1003; //Default RBVH  1003	RBVH/ETI
                        ErrorList.Add(AddErrorMessage(objitem, "Invalid Department Name:" + objitem.DepartCD));
                        continue;
                    }

                    //get emp subtype by entity and Contract
                   
                        objEmpType = T_CMS_Master_EmploymentTypeManager.GetAllByEntityID(objOrg.Entity_Id, "C");
                   

                    if (objEmpType != null)
                    {
                        objitem.EmpTypeID = objEmpType.EmpTypeID;

                        if (objOrg.Entity_Id != 10001)
                        {
                            objEmpSubType = T_CMS_Master_EmploymentSubTypeManager.GetEmpTypeID(objEmpType.EmpTypeID, objitem.EmpTypeCD);
                            if (objEmpSubType != null)
                            {
                                objitem.EmpSubTypeID = objEmpSubType.EmpSubTypeID;
                            }
                            else
                            {
                                ErrorList.Add(AddErrorMessage(objitem, "Invalid EmployeeSubType Name:" + objitem.EmpTypeCD));
                                continue;
                            }
                        }
                        
                       
                    }

                    if (string.IsNullOrEmpty(objitem.POB))
                    {
                        objitem.POB = "";
                    }
                    if (string.IsNullOrEmpty(objitem.HighestDegree))
                    {
                        objitem.HighestDegree = "";
                    }
                    if (string.IsNullOrEmpty(objitem.PerAddress))
                    {
                        objitem.PerAddress = "";
                    }
                    //Location = 6;// objitem.LocationCD.Split('-')[0].Trim();
                    objitem.ID = 0; //Add new
                    objitem.Mode = "SAVE";
                    //default value
                    objitem.CreatedBy = EmployeeCode;

                    //Grade = int.Parse(objitem.GradeName.Split('-')[1].Trim());

                    objitem.GradeID = objGrade.GradeID;
                    //add annual leave
                    T_LMS_Master_AnnualLeave objAL = T_LMS_Master_AnnualLeaveManager.GetById(objitem.GradeID);
                    if (objAL.Grade_Id != 0)
                    {
                        objitem.AnnualLeave = objAL.NoOfDays;
                    }
                    

                    objitem.PositionID = objPosition.PositionID;
                    if (objitem.SalutationCD.IndexOf("Ms") != -1)
                    {
                        SaluationID = 2;
                    }
                    else if (objitem.SalutationCD.IndexOf("Mrs") != -1)
                    {
                        SaluationID = 3;
                    }else
                    {
                        SaluationID = 1;
                    }

                


                    objitem.WorkHoursID = objWH.WorkHoursID;
                    objitem.SalutationID = SaluationID; //DNH fix later
                    objitem.LocationID = 6;// int.Parse(Location);
                    objitem.IDPOI = objPOI.POI_ID;
                    //objitem.HighestDegree 
                    objitem.CreatedDate = SystemConfig.CurrentDate;
                    objitem.PassportDOI = null;
                    objitem.ModifiedDate = SystemConfig.CurrentDate;
                    objitem.HostCountryCurrency = "VND";
                    string period= objitem.ProbationsPeriodCD.Replace("Days", "");
                    int monthPeriod = 0;
                    if (period == "60")
                    {
                        objitem.ProbationsPeriod = 2;
                        monthPeriod = 2;
                    }
                    else if (period == "30")
                    {
                        objitem.ProbationsPeriod = 1;
                        monthPeriod = 1;
                    }
                    else
                    {
                        objitem.ProbationsPeriod = 0;

                    }
                    objitem.DeptID = objOrg.OrgId;
                    objitem.LabourDOI = null;
                    objitem.ModifiedDate = SystemConfig.CurrentDate;
                    objitem.OriginalDate = SystemConfig.CurrentDate;
                    objitem.WorkPermitFrom = SystemConfig.CurrentDate;
                    objitem.EmpTypeID = objEmpType.EmpTypeID; //DNH
                    if (objitem.ContractTermCD.IndexOf("Indefinite") != -1)
                    {
                        if (objOrg.Entity_Id == 10001)
                        {
                            objitem.EmpSubTypeID = 101;//Indefinite Sub-Type
                        }
                            objitem.WorkPermitTo = SystemConfig.EndDate;
                        objitem.Enddate = SystemConfig.EndDate;
                        objitem.ContractTerm = 0;
                        //check if the the contract have the probation term it will calcuation about enddate --> use for report information
                    }
                    else
                    {

                        if (objOrg.Entity_Id == 10001)
                        {
                              objitem.EmpSubTypeID = 100;//definite Sub-Type
                        }


                        string[] strContractTerm = objitem.ContractTermCD.Split(' ');


                        if (strContractTerm.Length > 0)
                        {
                            int intTerm = int.Parse(strContractTerm[0]);
                            if (intTerm < 12)
                            {
                                if (objOrg.Entity_Id == 10001)
                                {
                                     objitem.EmpSubTypeID = 103;//seasonable Sub-Type
                                }
                            }
                            
                            objitem.ContractTerm = intTerm;
                            DateTime enddate = objitem.Joiningdate.AddMonths(intTerm);
                            enddate = enddate.AddDays(-1);
                            objitem.WorkPermitTo = enddate;
                            objitem.Enddate = enddate;
                        }
                        
                    }

                    if (objitem.ProbationsPeriod > 0)
                    {
                        if (monthPeriod == (int)eMonthProbation.ONE_MONTH_PROBATION_PERIOD)
                        {
                            objitem.Enddate = objitem.Joiningdate.AddDays(Constant.ONE_MONTH_PROBATION_PERIOD);
                        }
                        if (monthPeriod == (int)eMonthProbation.TWO_MONTH_PROBATION_PERIOD)
                        {
                            objitem.Enddate = objitem.Joiningdate.AddDays(Constant.TWO_MONTH_PROBATION_PERIOD);
                        }
                    }

                    objitem.HomeGrossOfferEffectiveFrom = SystemConfig.CurrentDate;
                    objitem.HostGrossOfferEffectiveFrom = SystemConfig.CurrentDate;
                    objitem.HomeGrossOfferEffectiveTo = SystemConfig.CurrentDate;
                    objitem.HostGrossOfferEffectiveTo = SystemConfig.CurrentDate;


                    objitem.FirstName_EN = objitem.FirstName_EN.ToUpper();
                    objitem.MiddleName_EN = objitem.MiddleName_EN.ToUpper();
                    objitem.LastName_EN = objitem.LastName_EN.ToUpper();

                    objitem.FirstName_VN = objitem.FirstName_VN.ToUpper();
                    objitem.MiddleName_VN = objitem.MiddleName_VN.ToUpper();
                    objitem.LastName_VN = objitem.LastName_VN.ToUpper();

                    Add(objitem);
                }
                catch(Exception ObjEx)
                {
                    ErrorList.Add(AddErrorMessage(objitem, ObjEx.Message));
                    
                }
                
            }
            return ErrorList;
        }
        public static T_CMS_Master_Contract AddErrorMessage(T_CMS_Master_Contract objItem,string message)
        {
            objItem.ErrorMesssage += "<div>"+message+"</div>";
            return objItem;
        }

      public static T_CMS_Master_Contract Update(T_CMS_Master_Contract objItem)
        {

            T_CMS_Master_Contract item = new T_CMS_Master_Contract();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_CMS_Master_Contract>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_CMS_Master_Contract objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_EmployeeCode_KEY, objItem.EmployeeCode));
            //HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser));
            HttpCache.RemoveSearchCache(SystemConfig.AllowSearchCache, SETTINGS_Search_KEY);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strEndPoint"></param>
        /// <param name="ReturnObject">"application/json" / XML</param>
        /// <returns></returns>
        public static string HouseEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }

		 #region new method
        public static T_CMS_Master_ContractCollection GetAllByUser(string CreatedUser)
        {
            T_CMS_Master_ContractCollection items = new T_CMS_Master_ContractCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_ContractCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_ContractCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static System.Data.DataTable CMS_ContractHistoryReport(int EntityID, DateTime? ExeDate, bool IsActiveEmp)
        {
            System.Data.DataTable items = new System.Data.DataTable();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?EntityID={0}&ExeDate={1}&IsActiveEmp={2}&Type=ctracthistory&DepID=", EntityID, ExeDate, IsActiveEmp)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<System.Data.DataTable>().GetAwaiter().GetResult();
                }
            }

            return items;
        }
        public static System.Data.DataTable CMS_ContractPendingReport(int EntityID, DateTime? ExeDate, bool IsActiveEmp)
        {
            System.Data.DataTable items = new System.Data.DataTable();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?EntityID={0}&ExeDate={1}&IsActiveEmp={2}&Type=ctractpending&DepID=", EntityID, ExeDate, IsActiveEmp)).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<System.Data.DataTable>().GetAwaiter().GetResult();
                }
            }
            return items;
        }

        public static System.Data.DataTable CMS_AppendixHistoryReport(int EntityID, DateTime? ExeDate, bool IsActiveEmp)
        {
            System.Data.DataTable items = new System.Data.DataTable();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?EntityID={0}&ExeDate={1}&IsActiveEmp={2}&Type=Apendix&DepID=", EntityID, ExeDate, IsActiveEmp)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<System.Data.DataTable>().GetAwaiter().GetResult();
                }
            }

            return items;
        }

        public static T_CMS_Master_ContractCollection Search(SearchFilter value)
        {
            T_CMS_Master_ContractCollection items = new T_CMS_Master_ContractCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection,value.Condition);

            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_CMS_Master_ContractCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_ContractCollection>().GetAwaiter().GetResult();
                }
            }
            
			if (SystemConfig.AllowSearchCache)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
		#endregion
    }
    
}
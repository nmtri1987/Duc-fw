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
using System.Data;
namespace Biz.OG.Services
{
    public class T_CMS_Master_InternshipManager 
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_CMS_Master_Internship.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_CMS_Master_Internship.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_CMS_Master_Internship.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_CMS_Master_Internship.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "T_CMS_Master_Internship";
        #endregion
        public static T_CMS_Master_InternshipCollection GetAll()
        {
            T_CMS_Master_InternshipCollection items = new T_CMS_Master_InternshipCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_InternshipCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_InternshipCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_CMS_Master_Internship GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Master_Internship)obj2; }

            T_CMS_Master_Internship b = new T_CMS_Master_Internship();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_Internship>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }
        //public static IEnumerable<T_CMS_Master_Internship> ImportData(IEnumerable<T_CMS_Master_Internship> objItemList, int EmployeeCode)
        //{
        //    //IEnumerable < T_CMS_Master_Internship > b= new IEnumerable<T_CMS_Master_Internship>();
        //    return objItemList;
        //}
        public virtual DataTable ImportData(DataTable objList, int EmployeeCode)
        {
            return new DataTable();
        }
        public static T_CMS_Master_InternshipCollection ImportData(IEnumerable<T_CMS_Master_Internship> objItemList, int EmployeeCode)
        {
            T_COm_Master_Org objOrg;
            T_CMS_Master_WorkHours objWH;
            T_COM_Master_University objUni;
            T_CMS_Master_EmploymentType objEmpType;
            //T_CMS_Master_EmploymentTypeCollection objEmpTypes;
            T_CMS_Master_EmploymentSubType objEmpSubType;
            T_CMS_Master_InternshipCollection ErrorList = new T_CMS_Master_InternshipCollection();
            int SaluationID = 1;
            bool isError = false;
            string strError = "";
            string EntityList = "";
            T_COM_Master_EntityCollection EntityAllows = T_COM_Master_EntityManager.GetAllByEmployeeCode(EmployeeCode);
            foreach (T_COM_Master_Entity entity in EntityAllows)
            {
                EntityList += entity.EntityId + ";";
            }
            foreach (T_CMS_Master_Internship objitem in objItemList)
            {
                isError = false;
                strError = "";
                try
                {
                    objOrg = T_COm_Master_OrgManager.GetByOrgName(objitem.DepartCD);
                    objWH = T_CMS_Master_WorkHoursManager.GetByWorkHours(objitem.WorkHoursCD);

                    if (objWH.WorkHoursID == 0)
                    {
                        strError += "<div>Invalid  Work Hours Name: <span class='impval'>" + objitem.WorkHoursCD + "</span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Work Hours Name:" + objitem.WorkHoursCD));
                        isError = true;
                    }
                    if (objOrg.OrgId == 0)
                    {
                        strError += "<div>Invalid  Depart Name: <span class='impval'>" + objitem.DepartCD + "</span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Work Hours Name:" + objitem.WorkHoursCD));
                        isError = true;
                    }
                    if (!string.IsNullOrEmpty(objitem.UniversityCD))
                    {
                        objUni = T_COM_Master_UniversityManager.GetByName(objitem.UniversityCD);
                        if (objUni.UniversityID == 0)
                        {
                            strError += "<div>Invalid  University Name: <span class='impval'>" + objitem.UniversityCD + "</span></div>";
                            //ErrorList.Add(AddErrorMessage(objitem, "Invalid Work Hours Name:" + objitem.WorkHoursCD));
                            isError = true;
                        }else
                        {
                            objitem.UniversityID = objUni.UniversityID;
                        }
                    }

                    if (EntityList.IndexOf(objOrg.Entity_Id.ToString()) == -1)
                    {
                        strError += "<div>You don't have Permission to Import to this Group: <span class='impval'>" + objitem.DepartCD + " </span></div>";
                        //ErrorList.Add(AddErrorMessage(objitem, "Invalid Grade Name:" + objitem.GradeName));
                        isError = true;
                    }
                    if (isError)
                    {
                        objitem.ErrorMesssage += "<div>" + strError + "</div>";
                        ErrorList.Add(objitem);
                        continue;
                    }


                    if (objitem.SalutationCD.IndexOf("Ms") != -1)
                    {
                        objitem.StatusID= 2;
                    }
                    else if (objitem.SalutationCD.IndexOf("Mrs") != -1)
                    {
                        objitem.StatusID = 3;
                    }
                    else
                    {
                        objitem.StatusID = 1;
                    }
                    //get emp subtype by entity and internship
                    objEmpType = T_CMS_Master_EmploymentTypeManager.GetAllByEntityID(objOrg.Entity_Id,"I");

                    
                    if (objEmpType != null)
                    {
                        objitem.EmpTypeID = objEmpType.EmpTypeID;
                        objEmpSubType = T_CMS_Master_EmploymentSubTypeManager.GetEmpTypeID(objEmpType.EmpTypeID, "Intern");
                        if (objEmpSubType != null)
                        {
                            objitem.EmpSubTypeID = objEmpSubType.EmpSubTypeID;
                        }
                    }
                    DateTime enddate = objitem.Joiningdate.AddMonths(objitem.PeriodofInternship);
                    enddate = enddate.AddDays(-1);
                    objitem.Enddate = enddate;
                    objitem.DeptID = objOrg.OrgId;
                    objitem.WorkHours = objWH.WorkHoursID;
                    objitem.CreatedBy = EmployeeCode;
                    objitem.CreatedDate = SystemConfig.CurrentDate;
                    Add(objitem);
                }
                catch (Exception ObjEx)
                {
                    objitem.ErrorMesssage += "<div>" + ObjEx.Message + "</div>";
                    ErrorList.Add(objitem);
                }
            }
            return ErrorList;
        }

        public static T_CMS_Master_Internship Add(T_CMS_Master_Internship objItem)
        {
            T_CMS_Master_Internship b = new T_CMS_Master_Internship();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_Internship>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_CMS_Master_Internship objItem)
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
        public static T_CMS_Master_Internship Update(T_CMS_Master_Internship objItem)
        {

            T_CMS_Master_Internship item = new T_CMS_Master_Internship();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_CMS_Master_Internship>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_CMS_Master_Internship objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ID));
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
        public static T_CMS_Master_InternshipCollection GetAllByUser(string CreatedUser)
        {
            T_CMS_Master_InternshipCollection items = new T_CMS_Master_InternshipCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_InternshipCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_InternshipCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_CMS_Master_InternshipCollection Search(SearchFilter value)
        {
            T_CMS_Master_InternshipCollection items = new T_CMS_Master_InternshipCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_CMS_Master_InternshipCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_InternshipCollection>().GetAwaiter().GetResult();
                }
            }
            
			if (SystemConfig.AllowSearchCache && items.Count>0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
		#endregion
    }
    
}
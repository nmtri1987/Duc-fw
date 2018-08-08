using System;
using System.Net.Http;
using Helpers;
using Biz.TMS.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biz.TMS.Services
{
    public class T_TMS_EmployeeDailyTimesheetTransactionManager : IServiceManager<T_TMS_EmployeeDailyTimesheetTransaction>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_TMS_EmployeeDailyTimesheetTransaction.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_TMS_EmployeeDailyTimesheetTransaction.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.T_TMS_EmployeeDailyTimesheetTransaction.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.T_TMS_EmployeeDailyTimesheetTransaction.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "EmployeeDailyTimesheetTransaction";
        #endregion

        #region Servier Method

        public static T_TMS_EmployeeDailyTimesheetTransaction Add(T_TMS_EmployeeDailyTimesheetTransaction objItem)
        {
            T_TMS_EmployeeDailyTimesheetTransaction b = new T_TMS_EmployeeDailyTimesheetTransaction();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_TMS_EmployeeDailyTimesheetTransaction>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static T_TMS_EmployeeDailyTimesheetTransaction Update(T_TMS_EmployeeDailyTimesheetTransaction objItem)
        {

            T_TMS_EmployeeDailyTimesheetTransaction item = new T_TMS_EmployeeDailyTimesheetTransaction();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_TMS_EmployeeDailyTimesheetTransaction>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.ID, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            return "";
        }
        public virtual T_TMS_EmployeeDailyTimesheetTransaction Save(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            model.CreatedDate = SystemConfig.CurrentDate;
            string ErrorMessage = ValidateData(model);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (model.CompanyID != 0)
                {
                    model = Update(model);
                }
                else
                {
                    model = Add(model);
                }
            }
            else
            {
                model.ErrorMesssage = ErrorMessage;
            }
            return model;
        }

		public virtual T_TMS_EmployeeDailyTimesheetTransaction Default()
        {
            return new T_TMS_EmployeeDailyTimesheetTransaction()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate
            };
        }

        public virtual string Get(string id)
        {

            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }

		public static T_TMS_EmployeeDailyTimesheetTransaction GetById(Guid id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_TMS_EmployeeDailyTimesheetTransaction)obj2; }

            T_TMS_EmployeeDailyTimesheetTransaction b = new T_TMS_EmployeeDailyTimesheetTransaction();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_TMS_EmployeeDailyTimesheetTransaction>().GetAwaiter().GetResult();
                }
            }
			if(b!=null && b.CompanyID!=0){HttpCache.Max(key, b);}
            return b;
        }

        public static T_TMS_EmployeeDailyTimesheetTransactionCollection Search(SearchFilter value)
        {
            T_TMS_EmployeeDailyTimesheetTransactionCollection items = new T_TMS_EmployeeDailyTimesheetTransactionCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_TMS_EmployeeDailyTimesheetTransactionCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_TMS_EmployeeDailyTimesheetTransactionCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }


        public virtual IEnumerable<T_TMS_EmployeeDailyTimesheetTransaction> SearchData(SearchFilter value)
        {
            T_TMS_EmployeeDailyTimesheetTransactionCollection items = new T_TMS_EmployeeDailyTimesheetTransactionCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_TMS_EmployeeDailyTimesheetTransactionCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_TMS_EmployeeDailyTimesheetTransactionCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count>0)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }

        public virtual DataTable ImportData(DataTable objList)
        {
            IEnumerable<T_TMS_EmployeeDailyTimesheetTransaction> myList = objList.ToList<T_TMS_EmployeeDailyTimesheetTransaction>();
            T_TMS_EmployeeDailyTimesheetTransactionCollection ErrorList = new T_TMS_EmployeeDailyTimesheetTransactionCollection();
            foreach (T_TMS_EmployeeDailyTimesheetTransaction objitem in myList)
            {
                try
                {
                    Save(objitem);
                }
                catch (Exception objEx)
                {
                    objitem.ErrorMesssage = "<div class='error'>" + objEx.Message + "</div>";
                    ErrorList.Add(objitem);

                }
            }
            return ErrorList.ToDataTable<T_TMS_EmployeeDailyTimesheetTransaction>();
        }
        #endregion
        public static void RemoveCache(T_TMS_EmployeeDailyTimesheetTransaction objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
            HttpCache.RemoveSearchCache(SystemConfig.AllowSearchCache, SETTINGS_Search_KEY);
        }
        public static string HouseEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }
    }
}
using System;
using System.Net.Http;
using Helpers;
using Biz.CS.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biz.CS.Services
{
    public class ScheduleTaskManager : IServiceManager<ScheduleTask>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.ScheduleTask.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.ScheduleTask.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.ScheduleTask.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.ScheduleTask.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "ScheduleTask";
        #endregion

        #region Servier Method

        public static ScheduleTask Add(ScheduleTask objItem)
        {
            ScheduleTask b = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static ScheduleTask Update(ScheduleTask objItem)
        {

            ScheduleTask item = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(ScheduleTask model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.Id, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(ScheduleTask model)
        {
            return "";
        }
        public virtual ScheduleTask Save(ScheduleTask model)
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

		public virtual ScheduleTask Default()
        {
            return new ScheduleTask()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                //CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate
            };
        }

        public virtual string Get(string id)
        {
            string b = ""; 
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}", id,Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }

		public static ScheduleTask GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (ScheduleTask)obj2; }

            ScheduleTask b = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Id={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                }
            }
			if(b!=null && b.CompanyID!=0){HttpCache.Max(key, b);}
            return b;
        }
        public virtual IEnumerable<ScheduleTask> SearchData(SearchFilter value)
        {
            ScheduleTaskCollection items = new ScheduleTaskCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (ScheduleTaskCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScheduleTaskCollection>().GetAwaiter().GetResult();
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
            IEnumerable<ScheduleTask> myList = objList.ToList<ScheduleTask>();
            ScheduleTaskCollection ErrorList = new ScheduleTaskCollection();
            foreach (ScheduleTask objitem in myList)
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
            return ErrorList.ToDataTable<ScheduleTask>();
        }
        #endregion
        public static void RemoveCache(ScheduleTask objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Id));
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

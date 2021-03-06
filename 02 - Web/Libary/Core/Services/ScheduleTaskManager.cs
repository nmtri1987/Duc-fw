using System;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;

namespace Biz.Core.Services
{
    public class ScheduleTaskManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.ScheduleTask.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.ScheduleTask.{0}{1}";
        private const string SETTINGS_Type_KEY = "ifinds.Models.ScheduleTask.{0}{1}";
        private const string SETTINGS_User_KEY = "ifinds.Models.ScheduleTask.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.ScheduleTask.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "ScheduleTask";
        #endregion
        public static ScheduleTaskCollection GetAll(int CompanyID)
        {
            ScheduleTaskCollection items = new ScheduleTaskCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (ScheduleTaskCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScheduleTaskCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static ScheduleTask GetById(int id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (ScheduleTask)obj2; }

            ScheduleTask b = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Id={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static ScheduleTask GetByType(string Type, int CompanyID)
        {
            string key = String.Format(SETTINGS_Type_KEY, Type, CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (ScheduleTask)obj2; }

            ScheduleTask b = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Type={0}&CompanyID={1}", Type, CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


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
        public static void Delete(ScheduleTask objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.Id,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static ScheduleTask Update(ScheduleTask objItem)
        {

            ScheduleTask item = new ScheduleTask();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.Id), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<ScheduleTask>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(ScheduleTask objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Id,objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_Type_KEY, objItem.Type, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser,objItem.CompanyID));
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
        public static ScheduleTaskCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            ScheduleTaskCollection items = new ScheduleTaskCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (ScheduleTaskCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScheduleTaskCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static ScheduleTaskCollection Search(SearchFilter value)
        {
            ScheduleTaskCollection items = new ScheduleTaskCollection();
			//string key = string.Format(SETTINGS_Search_KEY, value.CompanyID+value.Keyword+value.Page+value.PageSize+value.OrderBy+value.OrderDirection)
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
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
            
			if (SystemConfig.AllowSearchCache)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
		#endregion
    }
    
}
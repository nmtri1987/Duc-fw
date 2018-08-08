using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Biz.Core.Models;
namespace Biz.Core.Services
{
    public class UserActivityManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.UserActivity.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.UserActivity.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.UserActivity.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.UserActivity.Search.{0}";
        private const string Resource = "UserActivity";
        #endregion
        public static UserActivityCollection GetAll(int CompanyID)
        {
            UserActivityCollection items = new UserActivityCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (UserActivityCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<UserActivityCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static UserActivity GetById(string id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (UserActivity)obj2; }

            UserActivity b = new UserActivity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?UserActivityCD={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<UserActivity>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static UserActivity Add(UserActivity objItem)
        {
            UserActivity b = new UserActivity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<UserActivity>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(UserActivity objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.UserActivityCD,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static UserActivity Update(UserActivity objItem)
        {

            UserActivity item = new UserActivity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.UserActivityCD), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<UserActivity>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(UserActivity objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.UserActivityCD,objItem.CompanyID));
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
        public static UserActivityCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            UserActivityCollection items = new UserActivityCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (UserActivityCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<UserActivityCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static UserActivityCollection Search(SearchFilter value)
        {
            UserActivityCollection items = new UserActivityCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID + value.Keyword + value.Page + value.PageSize + value.OrderBy + value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (UserActivityCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<UserActivityCollection>().GetAwaiter().GetResult();
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
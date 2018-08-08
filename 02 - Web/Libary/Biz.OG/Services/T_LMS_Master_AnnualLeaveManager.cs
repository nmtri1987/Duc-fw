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
namespace Biz.OG.Services
{
    public class T_LMS_Master_AnnualLeaveManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_LMS_Master_AnnualLeave.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_LMS_Master_AnnualLeave.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_LMS_Master_AnnualLeave.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_LMS_Master_AnnualLeave.Search.{0}";
        private const string Resource = "T_LMS_Master_AnnualLeave";
        #endregion
        public static T_LMS_Master_AnnualLeaveCollection GetAll()
        {
            T_LMS_Master_AnnualLeaveCollection items = new T_LMS_Master_AnnualLeaveCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_LMS_Master_AnnualLeaveCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeaveCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Grade_Id"></param>
        /// <returns></returns>
        public static T_LMS_Master_AnnualLeave GetById(int Grade_Id)
        {
            string key = String.Format(SETTINGS_ID_KEY, Grade_Id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_LMS_Master_AnnualLeave)obj2; }

            T_LMS_Master_AnnualLeave b = new T_LMS_Master_AnnualLeave();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", Grade_Id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeave>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_LMS_Master_AnnualLeave Add(T_LMS_Master_AnnualLeave objItem)
        {
            T_LMS_Master_AnnualLeave b = new T_LMS_Master_AnnualLeave();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeave>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_LMS_Master_AnnualLeave objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.Grade_Id)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_LMS_Master_AnnualLeave Update(T_LMS_Master_AnnualLeave objItem)
        {

            T_LMS_Master_AnnualLeave item = new T_LMS_Master_AnnualLeave();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.Grade_Id), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeave>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_LMS_Master_AnnualLeave objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Grade_Id));
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
        public static T_LMS_Master_AnnualLeaveCollection GetAllByUser(string CreatedUser)
        {
            T_LMS_Master_AnnualLeaveCollection items = new T_LMS_Master_AnnualLeaveCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_LMS_Master_AnnualLeaveCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeaveCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_LMS_Master_AnnualLeaveCollection Search(SearchFilter value)
        {
            T_LMS_Master_AnnualLeaveCollection items = new T_LMS_Master_AnnualLeaveCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_LMS_Master_AnnualLeaveCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Master_AnnualLeaveCollection>().GetAwaiter().GetResult();
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
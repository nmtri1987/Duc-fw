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
    public class T_COm_Master_OrgManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_COm_Master_Org.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_COm_Master_Org.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_COm_Master_Org.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_COm_Master_Org.Search.{0}";
        private const string Resource = "T_COm_Master_Org";
        #endregion
        public static T_COm_Master_OrgCollection GetAll()
        {
            T_COm_Master_OrgCollection items = new T_COm_Master_OrgCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COm_Master_OrgCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COm_Master_OrgCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_COm_Master_Org GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COm_Master_Org)obj2; }

            T_COm_Master_Org b = new T_COm_Master_Org();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?OrgId={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COm_Master_Org>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_COm_Master_Org GetByOrgName(string OrgName)
        {
            string key = String.Format(SETTINGS_User_KEY, OrgName);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COm_Master_Org)obj2; }

            T_COm_Master_Org b = new T_COm_Master_Org();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", OrgName)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COm_Master_Org>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_COm_Master_Org Add(T_COm_Master_Org objItem)
        {
            T_COm_Master_Org b = new T_COm_Master_Org();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COm_Master_Org>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_COm_Master_Org objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.OrgId)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_COm_Master_Org Update(T_COm_Master_Org objItem)
        {

            T_COm_Master_Org item = new T_COm_Master_Org();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.OrgId), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_COm_Master_Org>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_COm_Master_Org objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.OrgId));
			HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.OrgName));
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
        public static T_COm_Master_OrgCollection GetAllName(string OrgName)
        {
            T_COm_Master_OrgCollection items = new T_COm_Master_OrgCollection();

            string key = String.Format(SETTINGS_User_KEY, OrgName);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COm_Master_OrgCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", OrgName)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COm_Master_OrgCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_COm_Master_OrgCollection Search(SearchFilter value)
        {
            T_COm_Master_OrgCollection items = new T_COm_Master_OrgCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_COm_Master_OrgCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COm_Master_OrgCollection>().GetAwaiter().GetResult();
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
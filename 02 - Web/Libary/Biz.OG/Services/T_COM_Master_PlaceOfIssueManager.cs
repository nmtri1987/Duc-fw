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
    public class T_COM_Master_PlaceOfIssueManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_COM_Master_PlaceOfIssue.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_COM_Master_PlaceOfIssue.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_COM_Master_PlaceOfIssue.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_COM_Master_PlaceOfIssue.Search.{0}";
        private const string Resource = "T_COM_Master_PlaceOfIssue";
        #endregion
        public static T_COM_Master_PlaceOfIssueCollection GetAll()
        {
            T_COM_Master_PlaceOfIssueCollection items = new T_COM_Master_PlaceOfIssueCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_PlaceOfIssueCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssueCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_COM_Master_PlaceOfIssue GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_PlaceOfIssue)obj2; }

            T_COM_Master_PlaceOfIssue b = new T_COM_Master_PlaceOfIssue();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?POI_ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssue>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_COM_Master_PlaceOfIssue GetByPOI_Name_VN(string POI_Name_VN)
        {
            string key = String.Format(SETTINGS_ID_KEY, POI_Name_VN);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_PlaceOfIssue)obj2; }

            T_COM_Master_PlaceOfIssue b = new T_COM_Master_PlaceOfIssue();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?POI_Name_VN={0}", POI_Name_VN)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssue>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_COM_Master_PlaceOfIssue Add(T_COM_Master_PlaceOfIssue objItem)
        {
            T_COM_Master_PlaceOfIssue b = new T_COM_Master_PlaceOfIssue();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssue>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_COM_Master_PlaceOfIssue objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.POI_ID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_COM_Master_PlaceOfIssue Update(T_COM_Master_PlaceOfIssue objItem)
        {

            T_COM_Master_PlaceOfIssue item = new T_COM_Master_PlaceOfIssue();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.POI_ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssue>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_COM_Master_PlaceOfIssue objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.POI_ID));
			HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.POI_Name_VN));
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
        public static T_COM_Master_PlaceOfIssueCollection GetAllByUser(string CreatedUser)
        {
            T_COM_Master_PlaceOfIssueCollection items = new T_COM_Master_PlaceOfIssueCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_PlaceOfIssueCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssueCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_COM_Master_PlaceOfIssueCollection Search(SearchFilter value)
        {
            T_COM_Master_PlaceOfIssueCollection items = new T_COM_Master_PlaceOfIssueCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_COM_Master_PlaceOfIssueCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_PlaceOfIssueCollection>().GetAwaiter().GetResult();
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
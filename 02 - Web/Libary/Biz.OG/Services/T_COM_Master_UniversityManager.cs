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
    public class T_COM_Master_UniversityManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_COM_Master_University.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_COM_Master_University.{0}";
        private const string SETTINGS_ID_Name = "ifinds.Models.T_COM_Master_University.Name.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.T_COM_Master_University.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_COM_Master_University.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "T_COM_Master_University";
        #endregion
        public static T_COM_Master_UniversityCollection GetAll()
        {
            T_COM_Master_UniversityCollection items = new T_COM_Master_UniversityCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_UniversityCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_UniversityCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_COM_Master_University GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_University)obj2; }

            T_COM_Master_University b = new T_COM_Master_University();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?UniversityID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_University>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UniversityName"></param>
        /// <returns></returns>
        public static T_COM_Master_University GetByName(string UniversityName)
        {
            string key = String.Format(SETTINGS_ID_Name, UniversityName);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_University)obj2; }

            T_COM_Master_University b = new T_COM_Master_University();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?UniversityName={0}&ev=e", UniversityName)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_University>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static T_COM_Master_University Add(T_COM_Master_University objItem)
        {
            T_COM_Master_University b = new T_COM_Master_University();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_University>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_COM_Master_University objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.UniversityID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_COM_Master_University Update(T_COM_Master_University objItem)
        {

            T_COM_Master_University item = new T_COM_Master_University();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.UniversityID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_COM_Master_University>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_COM_Master_University objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.UniversityID));
			HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_Name, objItem.UniversityName_EN));
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
        public static T_COM_Master_UniversityCollection GetAllByUser(string CreatedUser)
        {
            T_COM_Master_UniversityCollection items = new T_COM_Master_UniversityCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_UniversityCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_UniversityCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_COM_Master_UniversityCollection Search(SearchFilter value)
        {
            T_COM_Master_UniversityCollection items = new T_COM_Master_UniversityCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_COM_Master_UniversityCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_UniversityCollection>().GetAwaiter().GetResult();
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
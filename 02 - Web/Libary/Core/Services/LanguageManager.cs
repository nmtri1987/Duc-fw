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
    public class LanguageManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.Language.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.Language.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.Language.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.Language.Search.{0}";
        private const string Resource = "Language";
        #endregion
        public static LanguageCollection GetAll(int CompanyID)
        {
            LanguageCollection items = new LanguageCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (LanguageCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LanguageCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static Language GetById(int id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (Language)obj2; }

            Language b = new Language();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?LanguageId={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<Language>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }
        /// <summary>
        /// DNH Need finish to day 
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetResourceName(string ResourceName, int CompanyID)
        {
            return ResourceName;
            //string key = String.Format(SETTINGS_ID_KEY, ResourceName, CompanyID);
            //object obj2 = HttpCache.Get(key);
            //if (obj2 != null) { return (Language)obj2; }

            //Language b = new Language();
            //using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            //{

            //    HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?LanguageId={0}&CompanyID={1}", id, CompanyID)).GetAwaiter().GetResult();

            //    if (response.IsSuccessStatusCode)
            //    {
            //        b = response.Content.ReadAsAsync<Language>().GetAwaiter().GetResult();
            //    }
            //}
            //HttpCache.Max(key, b);
            //return b;
        }

        public static Language Add(Language objItem)
        {
            Language b = new Language();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<Language>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(Language objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.LanguageId,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static Language Update(Language objItem)
        {

            Language item = new Language();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.LanguageId), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<Language>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(Language objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.LanguageId,objItem.CompanyID));
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
        public static LanguageCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            LanguageCollection items = new LanguageCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (LanguageCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LanguageCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static LanguageCollection Search(SearchFilter value)
        {
            LanguageCollection items = new LanguageCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID + value.Keyword + value.Page + value.PageSize + value.OrderBy + value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (LanguageCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LanguageCollection>().GetAwaiter().GetResult();
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
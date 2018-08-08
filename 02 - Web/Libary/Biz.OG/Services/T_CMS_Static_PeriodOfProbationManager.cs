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
    public class T_CMS_Static_PeriodOfProbationManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_CMS_Static_PeriodOfProbation.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_CMS_Static_PeriodOfProbation.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_CMS_Static_PeriodOfProbation.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_CMS_Static_PeriodOfProbation.Search.{0}";
        private const string Resource = "T_CMS_Static_PeriodOfProbation";
        #endregion
        public static T_CMS_Static_PeriodOfProbationCollection GetAll()
        {
            T_CMS_Static_PeriodOfProbationCollection items = new T_CMS_Static_PeriodOfProbationCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Static_PeriodOfProbationCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbationCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_CMS_Static_PeriodOfProbation GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Static_PeriodOfProbation)obj2; }

            T_CMS_Static_PeriodOfProbation b = new T_CMS_Static_PeriodOfProbation();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbation>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_CMS_Static_PeriodOfProbation Add(T_CMS_Static_PeriodOfProbation objItem)
        {
            T_CMS_Static_PeriodOfProbation b = new T_CMS_Static_PeriodOfProbation();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbation>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_CMS_Static_PeriodOfProbation objItem)
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
        public static T_CMS_Static_PeriodOfProbation Update(T_CMS_Static_PeriodOfProbation objItem)
        {

            T_CMS_Static_PeriodOfProbation item = new T_CMS_Static_PeriodOfProbation();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbation>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_CMS_Static_PeriodOfProbation objItem)
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
        public static T_CMS_Static_PeriodOfProbationCollection GetAllByUser(string CreatedUser)
        {
            T_CMS_Static_PeriodOfProbationCollection items = new T_CMS_Static_PeriodOfProbationCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Static_PeriodOfProbationCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbationCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_CMS_Static_PeriodOfProbationCollection Search(SearchFilter value)
        {
            T_CMS_Static_PeriodOfProbationCollection items = new T_CMS_Static_PeriodOfProbationCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_CMS_Static_PeriodOfProbationCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Static_PeriodOfProbationCollection>().GetAwaiter().GetResult();
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
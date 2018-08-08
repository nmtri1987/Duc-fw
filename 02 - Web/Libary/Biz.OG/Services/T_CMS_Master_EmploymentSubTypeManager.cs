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
    public class T_CMS_Master_EmploymentSubTypeManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_CMS_Master_EmploymentSubType.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_CMS_Master_EmploymentSubType.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_CMS_Master_EmploymentSubType.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_CMS_Master_EmploymentSubType.Search.{0}";
        private const string Resource = "T_CMS_Master_EmploymentSubType";
        #endregion
        public static T_CMS_Master_EmploymentSubTypeCollection GetAll()
        {
            T_CMS_Master_EmploymentSubTypeCollection items = new T_CMS_Master_EmploymentSubTypeCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_EmploymentSubTypeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubTypeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_CMS_Master_EmploymentSubType GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Master_EmploymentSubType)obj2; }

            T_CMS_Master_EmploymentSubType b = new T_CMS_Master_EmploymentSubType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EmpSubTypeID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubType>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_CMS_Master_EmploymentSubType Add(T_CMS_Master_EmploymentSubType objItem)
        {
            T_CMS_Master_EmploymentSubType b = new T_CMS_Master_EmploymentSubType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubType>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_CMS_Master_EmploymentSubType objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.EmpSubTypeID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_CMS_Master_EmploymentSubType Update(T_CMS_Master_EmploymentSubType objItem)
        {

            T_CMS_Master_EmploymentSubType item = new T_CMS_Master_EmploymentSubType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.EmpSubTypeID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubType>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_CMS_Master_EmploymentSubType objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.EmpSubTypeID));
			HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.EmpTypeID));
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
        public static T_CMS_Master_EmploymentSubTypeCollection GetAllByEmpTypeID(int EmpTypeID)
        {
            T_CMS_Master_EmploymentSubTypeCollection items = new T_CMS_Master_EmploymentSubTypeCollection();

            string key = String.Format(SETTINGS_User_KEY, EmpTypeID);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_EmploymentSubTypeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetAllByEmpType?EmpTypeID={0}&ev=", EmpTypeID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubTypeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static T_CMS_Master_EmploymentSubType GetEmpTypeID(int EmpTypeID,string EmpSubTypeDescription)
        {
            T_CMS_Master_EmploymentSubType objItem = null;
            T_CMS_Master_EmploymentSubTypeCollection items = GetAllByEmpTypeID(EmpTypeID);
            if (items.Count > 0)
            {
                foreach (T_CMS_Master_EmploymentSubType item in items)
                {
                    if (item.EmpSubTypeDescription.ToLower().IndexOf(EmpSubTypeDescription.ToLower())!=-1 && item.IsActive)
                    {
                        objItem = item;
                        break;
                    }
                }
            }
            return objItem;
        }


        public static T_CMS_Master_EmploymentSubTypeCollection Search(SearchFilter value)
        {
            T_CMS_Master_EmploymentSubTypeCollection items = new T_CMS_Master_EmploymentSubTypeCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_CMS_Master_EmploymentSubTypeCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentSubTypeCollection>().GetAwaiter().GetResult();
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
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
    public class T_CMS_Master_EmploymentTypeManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_CMS_Master_EmploymentType.all";
        private const string SETTINGS_ALL_KEY_EntityID = "ifinds.Models.T_CMS_Master_EmploymentType.all.{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_CMS_Master_EmploymentType.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_CMS_Master_EmploymentType.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_CMS_Master_EmploymentType.Search.{0}";
        private const string Resource = "T_CMS_Master_EmploymentType";
        #endregion
        public static T_CMS_Master_EmploymentTypeCollection GetAll()
        {
            T_CMS_Master_EmploymentTypeCollection items = new T_CMS_Master_EmploymentTypeCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_EmploymentTypeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentTypeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_CMS_Master_EmploymentTypeCollection GetAllByEntityID(int EntityID)
        {
            T_CMS_Master_EmploymentTypeCollection items = new T_CMS_Master_EmploymentTypeCollection();
            string key = String.Format(SETTINGS_ALL_KEY_EntityID, EntityID);  ;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_EmploymentTypeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                //HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EntityID={0}&method=1", EntityID)).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentTypeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_CMS_Master_EmploymentType GetAllByEntityID(int EntityID,string EmpTypeCode)
        {
            T_CMS_Master_EmploymentType objItem=null;
            T_CMS_Master_EmploymentTypeCollection items = GetAllByEntityID(EntityID);
            if (items.Count > 0)
            {
                foreach(T_CMS_Master_EmploymentType item in items)
                {
                    if(item.EmpTypeCode.ToLower()== EmpTypeCode.ToLower())
                    {
                        objItem = item;
                        break;
                    }
                }
            }
            return objItem;
        }
        public static T_CMS_Master_EmploymentType GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_CMS_Master_EmploymentType)obj2; }

            T_CMS_Master_EmploymentType b = new T_CMS_Master_EmploymentType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EmpTypeID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_EmploymentType>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_CMS_Master_EmploymentType Add(T_CMS_Master_EmploymentType objItem)
        {
            T_CMS_Master_EmploymentType b = new T_CMS_Master_EmploymentType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_CMS_Master_EmploymentType>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_CMS_Master_EmploymentType objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.EmpTypeID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_CMS_Master_EmploymentType Update(T_CMS_Master_EmploymentType objItem)
        {

            T_CMS_Master_EmploymentType item = new T_CMS_Master_EmploymentType();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.EmpTypeID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_CMS_Master_EmploymentType>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_CMS_Master_EmploymentType objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);

            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY_EntityID, objItem.EntityID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.EmpTypeID));
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
        public static T_CMS_Master_EmploymentTypeCollection GetAllByUser(string CreatedUser)
        {
            T_CMS_Master_EmploymentTypeCollection items = new T_CMS_Master_EmploymentTypeCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_CMS_Master_EmploymentTypeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentTypeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_CMS_Master_EmploymentTypeCollection Search(SearchFilter value)
        {
            T_CMS_Master_EmploymentTypeCollection items = new T_CMS_Master_EmploymentTypeCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_CMS_Master_EmploymentTypeCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_CMS_Master_EmploymentTypeCollection>().GetAwaiter().GetResult();
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
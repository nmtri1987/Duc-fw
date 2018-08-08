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
using Biz.Core.Security;
namespace Biz.OG.Services
{
    public class T_COM_Master_EntityManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_COM_Master_Entity.all.{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_COM_Master_Entity.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_COM_Master_Entity.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_COM_Master_Entity.Search.{0}";
        private const string Resource = "T_COM_Master_Entity";
        #endregion
        public static T_COM_Master_EntityCollection GetAll()
        {
            T_COM_Master_EntityCollection items = new T_COM_Master_EntityCollection();
           
            string key = String.Format(SETTINGS_ALL_KEY,  CustomerAuthorize.CurrentUser.EmployeeCode); 
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_EntityCollection)obj2;
            }
            T_COM_Master_EntityCollection filter = new T_COM_Master_EntityCollection();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_EntityCollection>().GetAwaiter().GetResult();
                }
            }
            if (items.Count > 0 && CustomerAuthorize.CurrentUser!=null)
            {
               
                foreach (T_COM_Master_Entity b in items)
                {
                    if(CustomerAuthorize.CurrentUser.EntityID == b.EntityId)
                    {
                        filter.Add(b);
                    }
                }
            }
            HttpCache.Max(key, filter);
            return items;
        }
        public static T_COM_Master_Entity GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_Entity)obj2; }

            T_COM_Master_Entity b = new T_COM_Master_Entity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EntityId={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_Entity>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_COM_Master_Entity Add(T_COM_Master_Entity objItem)
        {
            T_COM_Master_Entity b = new T_COM_Master_Entity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_Entity>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_COM_Master_Entity objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.EntityId)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_COM_Master_Entity Update(T_COM_Master_Entity objItem)
        {

            T_COM_Master_Entity item = new T_COM_Master_Entity();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.EntityId), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_COM_Master_Entity>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_COM_Master_Entity objItem)
        {
            //HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, CustomerAuthorize.CurrentUser.EmployeeCode));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.EntityId));
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
        public static T_COM_Master_EntityCollection GetAllByEmployeeCode(int EmployeeCode)
        {
            T_COM_Master_EntityCollection items = new T_COM_Master_EntityCollection();

            string key = String.Format(SETTINGS_User_KEY, EmployeeCode.ToString());
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_EntityCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyEmployeeCode?EmployeeCode={0}", EmployeeCode)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_EntityCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_COM_Master_EntityCollection Search(SearchFilter value)
        {
            T_COM_Master_EntityCollection items = new T_COM_Master_EntityCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_COM_Master_EntityCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_EntityCollection>().GetAwaiter().GetResult();
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
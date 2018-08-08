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
    public class EmployeeInRolesManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.EmployeeInRoles.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.EmployeeInRoles.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.EmployeeInRoles.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.EmployeeInRoles.Search.{0}";
        private const string Resource = "EmployeeInRoles";
        #endregion
        public static EmployeeInRolesCollection GetAll(int CompanyID)
        {
            EmployeeInRolesCollection items = new EmployeeInRolesCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (EmployeeInRolesCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<EmployeeInRolesCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static EmployeeInRoles GetById(string id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (EmployeeInRoles)obj2; }

            EmployeeInRoles b = new EmployeeInRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Rolename={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<EmployeeInRoles>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static EmployeeInRoles Add(EmployeeInRoles objItem)
        {
            EmployeeInRoles b = new EmployeeInRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<EmployeeInRoles>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(EmployeeInRoles objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.Rolename,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static EmployeeInRoles Update(EmployeeInRoles objItem)
        {

            EmployeeInRoles item = new EmployeeInRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.Rolename), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<EmployeeInRoles>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(EmployeeInRoles objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Rolename,objItem.CompanyID));
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
        public static EmployeeInRolesCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            EmployeeInRolesCollection items = new EmployeeInRolesCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (EmployeeInRolesCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<EmployeeInRolesCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static EmployeeInRolesCollection Search(SearchFilter value)
        {
            EmployeeInRolesCollection items = new EmployeeInRolesCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID + value.Keyword + value.Page + value.PageSize + value.OrderBy + value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (EmployeeInRolesCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<EmployeeInRolesCollection>().GetAwaiter().GetResult();
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
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
    public class RBVHEmployeeManager 
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.RBVHEmployee.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.RBVHEmployee.{0}";
        private const string SETTINGS_Domain_KEY = "ifinds.Models.RBVHEmployee.Domain.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.RBVHEmployee.USer.{0}";
        
        private const string SETTINGS_Search_KEY = "ifinds.Models.RBVHEmployee.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "T_COM_Master_Employee";
        #endregion
        public static RBVHEmployeeCollection GetAll()
        {
            RBVHEmployeeCollection items = new RBVHEmployeeCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (RBVHEmployeeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<RBVHEmployeeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static System.Data.DataTable GetRealTimeEmployeeList(int EntityID,DateTime ReportDate)
        {
            System.Data.DataTable items = new System.Data.DataTable();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetReport?entityid={0}&Name=emplist&RefID={1}", EntityID, ReportDate.ToShortDateString())).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<System.Data.DataTable>().GetAwaiter().GetResult();
                }
            }
            return items;
        }
      
        public static RBVHEmployee GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (RBVHEmployee)obj2; }

            RBVHEmployee b = new RBVHEmployee();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?EmployeeCode={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<RBVHEmployee>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static RBVHEmployee GetByDomainId(string DomainId)
        {
            string key = String.Format(SETTINGS_Domain_KEY, DomainId);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (RBVHEmployee)obj2; }

            RBVHEmployee b = new RBVHEmployee();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?DomainId={0}", DomainId)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<RBVHEmployee>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static RBVHEmployee Add(RBVHEmployee objItem)
        {
            RBVHEmployee b = new RBVHEmployee();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<RBVHEmployee>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(RBVHEmployee objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.EmployeeCode)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static RBVHEmployee Update(RBVHEmployee objItem)
        {

            RBVHEmployee item = new RBVHEmployee();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.EmployeeCode), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<RBVHEmployee>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(RBVHEmployee objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.EmployeeCode));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_Domain_KEY, objItem.DomainId));
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
        public static RBVHEmployeeCollection GetAllByUser(string CreatedUser)
        {
            RBVHEmployeeCollection items = new RBVHEmployeeCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (RBVHEmployeeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<RBVHEmployeeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static RBVHEmployeeCollection Search(RBVHSearchFilter value)
        {
            RBVHEmployeeCollection items = new RBVHEmployeeCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition + value.EntityID);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (RBVHEmployeeCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<RBVHEmployeeCollection>().GetAwaiter().GetResult();
                }
            }
            
			if (SystemConfig.AllowSearchCache && items.Count>0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
		#endregion
    }
    
}
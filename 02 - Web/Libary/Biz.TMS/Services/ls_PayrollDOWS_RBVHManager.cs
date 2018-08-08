using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Biz.TMS.Models;
using Biz.Core.Models;
namespace Biz.TMS.Services
{
    public class ls_PayrollDOWS_RBVHManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.ls_PayrollDOWS_RBVH.all.{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.ls_PayrollDOWS_RBVH.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.ls_PayrollDOWS_RBVH.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.ls_PayrollDOWS_RBVH.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "ls_PayrollDOWS_RBVH";
        #endregion
        public static ls_PayrollDOWS_RBVHCollection GetAll(int EntityID)
        {
            ls_PayrollDOWS_RBVHCollection items = new ls_PayrollDOWS_RBVHCollection();
            string key = String.Format(SETTINGS_ALL_KEY, EntityID);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (ls_PayrollDOWS_RBVHCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ENtityID={0}", EntityID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVHCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static ls_PayrollDOWS_RBVH GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (ls_PayrollDOWS_RBVH)obj2; }

            ls_PayrollDOWS_RBVH b = new ls_PayrollDOWS_RBVH();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?id={0}&action=getid", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVH>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static ls_PayrollDOWS_RBVH Add(ls_PayrollDOWS_RBVH objItem)
        {
            ls_PayrollDOWS_RBVH b = new ls_PayrollDOWS_RBVH();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVH>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(ls_PayrollDOWS_RBVH objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.ENtityID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static ls_PayrollDOWS_RBVH Update(ls_PayrollDOWS_RBVH objItem)
        {

            ls_PayrollDOWS_RBVH item = new ls_PayrollDOWS_RBVH();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ENtityID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVH>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(ls_PayrollDOWS_RBVH objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ENtityID));
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
        public static ls_PayrollDOWS_RBVHCollection GetAllByUser(string CreatedUser)
        {
            ls_PayrollDOWS_RBVHCollection items = new ls_PayrollDOWS_RBVHCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (ls_PayrollDOWS_RBVHCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVHCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static ls_PayrollDOWS_RBVHCollection Search(SearchFilter value)
        {
            ls_PayrollDOWS_RBVHCollection items = new ls_PayrollDOWS_RBVHCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (ls_PayrollDOWS_RBVHCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ls_PayrollDOWS_RBVHCollection>().GetAwaiter().GetResult();
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
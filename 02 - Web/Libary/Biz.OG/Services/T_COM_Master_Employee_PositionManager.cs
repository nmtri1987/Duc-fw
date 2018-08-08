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
    public class T_COM_Master_Employee_PositionManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_COM_Master_Employee_Position.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_COM_Master_Employee_Position.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_COM_Master_Employee_Position.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_COM_Master_Employee_Position.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "T_COM_Master_Employee_Position";
        #endregion
        public static T_COM_Master_Employee_PositionCollection GetAll()
        {
            T_COM_Master_Employee_PositionCollection items = new T_COM_Master_Employee_PositionCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_Employee_PositionCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_Employee_PositionCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static T_COM_Master_Employee_Position GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_COM_Master_Employee_Position)obj2; }

            T_COM_Master_Employee_Position b = new T_COM_Master_Employee_Position();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_Employee_Position>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static T_COM_Master_Employee_Position Add(T_COM_Master_Employee_Position objItem)
        {
            T_COM_Master_Employee_Position b = new T_COM_Master_Employee_Position();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_COM_Master_Employee_Position>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_COM_Master_Employee_Position objItem)
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
        public static T_COM_Master_Employee_Position Update(T_COM_Master_Employee_Position objItem)
        {

            T_COM_Master_Employee_Position item = new T_COM_Master_Employee_Position();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_COM_Master_Employee_Position>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_COM_Master_Employee_Position objItem)
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
        public static T_COM_Master_Employee_PositionCollection GetAllByUser(string CreatedUser)
        {
            T_COM_Master_Employee_PositionCollection items = new T_COM_Master_Employee_PositionCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_COM_Master_Employee_PositionCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_Employee_PositionCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_COM_Master_Employee_PositionCollection Search(SearchFilter value)
        {
            T_COM_Master_Employee_PositionCollection items = new T_COM_Master_Employee_PositionCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_COM_Master_Employee_PositionCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_COM_Master_Employee_PositionCollection>().GetAwaiter().GetResult();
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
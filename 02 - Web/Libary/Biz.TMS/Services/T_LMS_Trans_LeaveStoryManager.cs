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
    public class T_LMS_Trans_LeaveStoryManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.T_LMS_Trans_LeaveStory.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.T_LMS_Trans_LeaveStory.{0}";
		private const string SETTINGS_User_KEY = "ifinds.Models.T_LMS_Trans_LeaveStory.USer.{0}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.T_LMS_Trans_LeaveStory.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "T_LMS_Trans_LeaveStory";
        #endregion
        public static T_LMS_Trans_LeaveStoryCollection GetAll()
        {
            T_LMS_Trans_LeaveStoryCollection items = new T_LMS_Trans_LeaveStoryCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_LMS_Trans_LeaveStoryCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStoryCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static LeaveWFCollection GetEmployeeLeaveReason(int EntityID,string EmployeeCode,string WorkDate)
        {
            LeaveWFCollection items = new LeaveWFCollection();
           
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/Get?EntityID={0}&EmployeeCode={1}&WorkDate={2}", EntityID, EmployeeCode, WorkDate)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LeaveWFCollection>().GetAwaiter().GetResult();
                }
            }
            
            return items;
        }
        public static T_LMS_Trans_LeaveStory GetById(int id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (T_LMS_Trans_LeaveStory)obj2; }

            T_LMS_Trans_LeaveStory b = new T_LMS_Trans_LeaveStory();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Id={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStory>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

       

        public static External_TimesCollection GetExMonthlyReport(int userid, string monthid, string FilterBy, string FilterValue)
        {
            External_TimesCollection items = new External_TimesCollection();
        
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("ExScanTime?userid={0}&monthid={1}&FilterBy={2}&FilterValue={3}", userid, monthid, FilterBy, FilterValue)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<External_TimesCollection>().GetAwaiter().GetResult();
                }
            }
            
            return items;
        }

        public static ExternalDailyCollection ExportExDailyTMS(int UserID, string FD, string TD, int AID)
        {
            ExternalDailyCollection items = new ExternalDailyCollection();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("ExScanTime?UserID={0}&FD={1}&TD={2}&AID={3}", UserID, FD, TD, AID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ExternalDailyCollection>().GetAwaiter().GetResult();
                }
            }

            return items;
        }

        public static T_LMS_Trans_LeaveStory Add(T_LMS_Trans_LeaveStory objItem)
        {
            T_LMS_Trans_LeaveStory b = new T_LMS_Trans_LeaveStory();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStory>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(T_LMS_Trans_LeaveStory objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "/{0}", objItem.Id)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static T_LMS_Trans_LeaveStory Update(T_LMS_Trans_LeaveStory objItem)
        {

            T_LMS_Trans_LeaveStory item = new T_LMS_Trans_LeaveStory();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.Id), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStory>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(T_LMS_Trans_LeaveStory objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Id));
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
        public static T_LMS_Trans_LeaveStoryCollection GetAllByUser(string CreatedUser)
        {
            T_LMS_Trans_LeaveStoryCollection items = new T_LMS_Trans_LeaveStoryCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (T_LMS_Trans_LeaveStoryCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStoryCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static T_LMS_Trans_LeaveStoryCollection Search(SearchFilter value)
        {
            T_LMS_Trans_LeaveStoryCollection items = new T_LMS_Trans_LeaveStoryCollection();
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (T_LMS_Trans_LeaveStoryCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<T_LMS_Trans_LeaveStoryCollection>().GetAwaiter().GetResult();
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
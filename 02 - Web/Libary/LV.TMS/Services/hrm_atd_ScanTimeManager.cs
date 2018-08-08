using Helpers;
using LV.TMS.Models;
using System;
using System.Net.Http;

namespace LV.TMS.Services
{
    public class hrm_atd_ScanTimeManager
    {
        #region Constants

        private const string SETTINGS_ALL_KEY = "ifinds.Models.hrm_atd_ScanTime.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.hrm_atd_ScanTime.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.hrm_atd_ScanTime.USer.{0}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.hrm_atd_ScanTime.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "hrm_atd_ScanTime";

        #endregion Constants

        public static hrm_atd_ScanTimeCollection GetAll()
        {
            hrm_atd_ScanTimeCollection items = new hrm_atd_ScanTimeCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (hrm_atd_ScanTimeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<hrm_atd_ScanTimeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static hrm_atd_ScanTime GetById(Guid id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (hrm_atd_ScanTime)obj2; }

            hrm_atd_ScanTime b = new hrm_atd_ScanTime();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<hrm_atd_ScanTime>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static hrm_atd_ScanTime GetById(string[] id,string[] select, string[] manual_in, string[] manual_out,string[] requestor_note ,string Event)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (hrm_atd_ScanTime)obj2; }

            hrm_atd_ScanTime b = new hrm_atd_ScanTime();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<hrm_atd_ScanTime>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static hrm_atd_ScanTime Add(hrm_atd_ScanTime objItem)
        {
            hrm_atd_ScanTime b = new hrm_atd_ScanTime();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?action=add", objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<hrm_atd_ScanTime>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static hrm_atd_ScanTime Submit(hrm_atd_ScanTime objItem)
        {
            hrm_atd_ScanTime b = new hrm_atd_ScanTime();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?action=submit", objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<hrm_atd_ScanTime>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static void Delete(hrm_atd_ScanTime objItem)
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

        public static hrm_atd_ScanTime Update(hrm_atd_ScanTime objItem)
        {
            hrm_atd_ScanTime item = new hrm_atd_ScanTime();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "/{0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<hrm_atd_ScanTime>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(hrm_atd_ScanTime objItem)
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

        public static hrm_atd_ScanTimeCollection GetAllByUser(string CreatedUser)
        {
            hrm_atd_ScanTimeCollection items = new hrm_atd_ScanTimeCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (hrm_atd_ScanTimeCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}", CreatedUser)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<hrm_atd_ScanTimeCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static hrm_atd_ScanTimeCollection Search(ScanTimeFilter value)
        {
            hrm_atd_ScanTimeCollection items = new hrm_atd_ScanTimeCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.FromDate, value.ToDate, value.EmployeeCode, value.CompanyID, value.Condition);
            //if (SystemConfig.AllowSearchCache)
            //{
            //    object obj2 = HttpCache.Get(key);

            //    if ((obj2 != null))
            //    {
            //        return (hrm_atd_ScanTimeCollection)obj2;
            //    }
            //}

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<hrm_atd_ScanTimeCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }

        public static hrm_atd_ScanTimeCollection Search(SearchFilter value)
        {
            hrm_atd_ScanTimeCollection items = new hrm_atd_ScanTimeCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (hrm_atd_ScanTimeCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<hrm_atd_ScanTimeCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }

        #endregion new method
    }
}
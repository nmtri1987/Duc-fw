namespace LV.TMS.Services
{
    using System;
    using System.Net.Http;
    using DataTables.Mvc;
    using Helpers;
    using Models;
    public class ScanTimeApprovalManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.ScanTimeApproval.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.ScanTimeApproval.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.ScanTimeApproval.USer.{0}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.ScanTimeApproval.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "ScanTimeApproval";
        #endregion
        public static ScanTimeApprovalCollection GetAll(ScanTimeApprovalSqlParameters value)
        {
            ScanTimeApprovalCollection items = new ScanTimeApprovalCollection();
            string key = SETTINGS_ALL_KEY;
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null) && value.IsOrdering)
            {
                return (ScanTimeApprovalCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format(Resource), value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScanTimeApprovalCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static SearchFilter SearchData(IDataTablesRequest requestModel)
        {
            SearchFilter searchFilter = new SearchFilter();
            ColumnCollection col = requestModel.Columns;
            foreach (Column item in col)
            {
                if (item.IsOrdered)
                {
                    searchFilter.OrderBy = item.Data;
                    searchFilter.OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                    searchFilter.IsOrdering = true;
                    break;
                }
            }

            return searchFilter;
        }

        public static ScanTimeApproval GetById(Guid id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (ScanTimeApproval)obj2; }

            ScanTimeApproval b = new ScanTimeApproval();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(Resource + $"?ID={id}").GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScanTimeApproval>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static ScanTimeApproval Add(ScanTimeApproval objItem)
        {
            ScanTimeApproval b = new ScanTimeApproval();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<ScanTimeApproval>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static void Delete(ScanTimeApproval objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(Resource + $"/{objItem.EmployeeCode}").GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }
        }
        public static int Update(TimesheetApproveReject objItem)
        {
            var result = 0;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(Resource + $"/", objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<int>().GetAwaiter().GetResult();
                }
            }
            return result;
        }

        public static void RemoveCache(ScanTimeApproval objItem)
        {
            HttpCache.RemoveByPattern(SETTINGS_ALL_KEY);
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.EmployeeCode));
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
        public static ScanTimeApprovalCollection GetAllByUser(string createdUser)
        {
            ScanTimeApprovalCollection items = new ScanTimeApprovalCollection();

            string key = String.Format(SETTINGS_User_KEY, createdUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (ScanTimeApprovalCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(Resource + $"/GetbyUser?usr={createdUser}").GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScanTimeApprovalCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static ScanTimeApprovalCollection Search(ScanTimeApprovalSqlParameters value)
        {
            ScanTimeApprovalCollection items = new ScanTimeApprovalCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (ScanTimeApprovalCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "/Get", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<ScanTimeApprovalCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
        #endregion
    }

}
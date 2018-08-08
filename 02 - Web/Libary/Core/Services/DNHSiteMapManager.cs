using System;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;
using Newtonsoft.Json;
namespace Biz.Core.Services
{
    public class DNHSiteMapManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHSiteMap.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHSiteMap.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.DNHSiteMap.USer.{0}{1}{2}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.DNHSiteMap.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHSiteMap";
        #endregion
        public static DNHSiteMapCollection GetAll(int CompanyID)
        {
            DNHSiteMapCollection items = new DNHSiteMapCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (DNHSiteMapCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHSiteMapCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static DNHSiteMap GetById(Guid? id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHSiteMap)obj2; }

            DNHSiteMap b = new DNHSiteMap();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?NodeID={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHSiteMap>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static DNHSiteMap Add(DNHSiteMap objItem)
        {
            DNHSiteMap b = new DNHSiteMap();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHSiteMap>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(DNHSiteMap objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.NodeID,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static DNHSiteMap Update(DNHSiteMap objItem)
        {

            DNHSiteMap item = new DNHSiteMap();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.NodeID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHSiteMap>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(DNHSiteMap objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.NodeID,objItem.CompanyID));
            //HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
            //HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser,objItem.CompanyID, objItem.ParentID));
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
        public static DNHSiteMapCollection GetAllByUser(string CreatedUser,int CompanyID, Guid? NodeID=null)
        {
            DNHSiteMapCollection items = new DNHSiteMapCollection();

            //string key = String.Format(SETTINGS_User_KEY, CreatedUser, CompanyID, NodeID);
            //object obj2 = HttpCache.Get(key);

            //if ((obj2 != null))
            //{
            //    return (DNHSiteMapCollection)obj2;
            //}
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}&NodeID={2}", CreatedUser,CompanyID, NodeID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHSiteMapCollection>().GetAwaiter().GetResult();
                }
            }
            //HttpCache.Max(key, items);
            return items;
        }

		public static DNHSiteMapCollection Search(SearchFilter value)
        {
            DNHSiteMapCollection items = new DNHSiteMapCollection();
            //string key = string.Format(SETTINGS_Search_KEY, value.CompanyID+value.Keyword+value.Page+value.PageSize+value.OrderBy+value.OrderDirection)
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            //string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHSiteMapCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHSiteMapCollection>().GetAwaiter().GetResult();
                }
            }
            
			if (SystemConfig.AllowSearchCache & items.Count>0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
        #endregion
        #region SiteMap Method 
        public static SearchFilter BindSearch(int CompanyID)
        {
            SearchFilter SearchKey = new SearchFilter();
            SearchKey.CompanyID = CompanyID;
            SearchKey.OrderBy = "NodeID";
            SearchKey.ColumnsName = "NodeID,Position,Title,Description,Url,Expanded,IsFolder,ScreenID,ParentID,CreatedUser,CreatedDate,IconImage";
            SearchKey.Page = 1;
            SearchKey.PageSize = 5000;
            SearchKey.OrderDirection = "Desc";
            return SearchKey;
        }
        public static DNHSiteMapCollection GetParentSiteMap(int CompanyID)
        {
            DNHSiteMapCollection items = new DNHSiteMapCollection();
            SearchFilter myFilter = BindSearch(CompanyID);
            myFilter.Condition = "  ParentID is null ";
            items= Search(myFilter);
            return items;
        }

        public static DNHSiteMapCollection GetChildSiteMap(int CompanyID, Guid NodeID)
        {
            DNHSiteMapCollection items = new DNHSiteMapCollection();
            SearchFilter myFilter = BindSearch(CompanyID);
            myFilter.Condition = "  ParentID = '"+ NodeID + "'" ;
            items = Search(myFilter);
            return items;
        }

        #endregion
    }

}
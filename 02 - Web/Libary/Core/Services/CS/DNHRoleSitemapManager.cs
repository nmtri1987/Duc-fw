using System;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biz.Core.Services
{
    public class DNHRoleSitemapManager : IServiceManager<DNHRoleSitemap>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHRoleSitemap.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHRoleSitemap.{0}{1}{2}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHRoleSitemap.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHRoleSitemap.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHRoleSitemap";
        #endregion
        #region AddOn Method
        public static DNHRoleSitemapCollection GetByRoles(string RoleName)
        {
            string cond = " RoleName='" + RoleName + "'";
            SearchFilter myfilter = new SearchFilter()
            {
                CompanyID = 1,
                Keyword = "",
                Page = 1,
                PageSize = 1000,
                ColumnsName = "CompanyID",
                OrderBy = "CompanyID",
                OrderDirection = "DESC",
                Condition = cond
            };
            
            return Search(myfilter);
        }
        #endregion
        #region Servier Method

        public static DNHRoleSitemap Add(DNHRoleSitemap objItem)
        {
            DNHRoleSitemap b = new DNHRoleSitemap();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHRoleSitemap>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHRoleSitemap Update(DNHRoleSitemap objItem)
        {

            DNHRoleSitemap item = new DNHRoleSitemap();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHRoleSitemap>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHRoleSitemap model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.NodeID, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHRoleSitemap model)
        {
            return "";
        }
        public virtual DNHRoleSitemap Save(DNHRoleSitemap model)
        {
            model.CreateDate = SystemConfig.CurrentDate;
            string ErrorMessage = ValidateData(model);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                DNHRoleSitemap old= GetbyID(model.NodeID.ToString(), model.CompanyID, model.RoleName);
                
                if (old!=null)
                {
                    model = Update(model);
                }
                else
                {
                    model = Add(model);
                }
                //insert or update success Remove Folder
                if (model != null && model.CompanyID!=0)
                {
                    Biz.Core.CommonHelper.DelteSiteMap(model.CompanyID);
                }
            }
            else
            {
                model.ErrorMesssage = ErrorMessage;
            }
            return model;
        }
        public virtual DNHRoleSitemap Default()
        {
            return new DNHRoleSitemap()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreateDate = SystemConfig.CurrentDate
            };
        }
        public virtual string Get(string id)
        {

            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public static DNHRoleSitemap GetbyID(string NodeID, int CompanyID, string RoleName)
        {
            string key = String.Format(SETTINGS_ID_KEY, NodeID, CompanyID, RoleName);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHRoleSitemap)obj2; }

            DNHRoleSitemap b = null; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&RefID={2}", NodeID, CompanyID, RoleName)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsAsync<DNHRoleSitemap>().GetAwaiter().GetResult();
                }
            }
            if (b != null && b.CompanyID!=0)
            {
                HttpCache.Max(key, b);
            }
            return b;
        }
        public static DNHRoleSitemapCollection Search(SearchFilter value)
        {
            DNHRoleSitemapCollection items = new DNHRoleSitemapCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHRoleSitemapCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHRoleSitemapCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }
        public virtual IEnumerable<DNHRoleSitemap> SearchData(SearchFilter value)
        {
            DNHRoleSitemapCollection items = new DNHRoleSitemapCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHRoleSitemapCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHRoleSitemapCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }

        public virtual DataTable ImportData(DataTable objList)
        {
            IEnumerable<DNHRoleSitemap> myList = objList.ToList<DNHRoleSitemap>();
            DNHRoleSitemapCollection ErrorList = new DNHRoleSitemapCollection();
            foreach (DNHRoleSitemap objitem in myList)
            {
                try
                {
                    Save(objitem);
                }
                catch (Exception objEx)
                {
                    objitem.ErrorMesssage = "<div class='error'>" + objEx.Message + "</div>";
                    ErrorList.Add(objitem);

                }
            }
            return ErrorList.ToDataTable<DNHRoleSitemap>();
        }
        #endregion
        public static void RemoveCache(DNHRoleSitemap objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.NodeID, objItem.CompanyID, objItem.RoleName));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
            HttpCache.RemoveSearchCache(SystemConfig.AllowSearchCache, SETTINGS_Search_KEY);
            if (objItem != null && objItem.CompanyID != 0)
            {
                Biz.Core.CommonHelper.DelteSiteMap(objItem.CompanyID);
            }
        }
        public static string HouseEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }
    }
}

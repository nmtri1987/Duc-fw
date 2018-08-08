using System;
using System.Net.Http;
using Helpers;
using Biz.CS.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biz.CS.Services
{
    public class DNHLanguageManager : IServiceManager<DNHLanguage>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHLanguage.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHLanguage.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHLanguage.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHLanguage.Search.{0}";
        private const string Resource = "DNHLanguage";
        #endregion

        #region Servier Method

        public static DNHLanguage Add(DNHLanguage objItem)
        {
            DNHLanguage b = new DNHLanguage();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHLanguage>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHLanguage Update(DNHLanguage objItem)
        {

            DNHLanguage item = new DNHLanguage();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHLanguage>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHLanguage model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.LanguageID, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHLanguage model)
        {
            return "";
        }
        public virtual DNHLanguage Save(DNHLanguage model)
        {
            model.CreatedDate = SystemConfig.CurrentDate;
            string ErrorMessage = ValidateData(model);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (model.CompanyID != 0)
                {
                    model = Update(model);
                }
                else
                {
                    model = Add(model);
                }
            }
            else
            {
                model.ErrorMesssage = ErrorMessage;
            }
            return model;
        }
        public virtual DNHLanguage Default()
        {
            return new DNHLanguage()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate
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
        public static DNHLanguageCollection GetAll()
        {
            Biz.Core.DNHUsers CurrentUser = Biz.Core.Security.CustomerAuthorize.CurrentUser;
            SearchFilter value = new SearchFilter()
            {
                CompanyID = CurrentUser.CompanyID,
                Keyword = "",
                Page = 1,
                PageSize = 5000,
                ColumnsName = "LanguageID,Name,LanguageCulture,Published",
                OrderBy = "Name",
                OrderDirection = "Desc",
                Condition = " Published=1 "
            };
            DNHLanguageCollection items = new DNHLanguageCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID + value.Keyword + value.Page + value.PageSize + value.OrderBy + value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHLanguageCollection)obj2;
                }
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHLanguageCollection>().GetAwaiter().GetResult();
                }
            }
            if (SystemConfig.AllowSearchCache && items.Count > 0)
            {
                HttpCache.Max(key, items);
            }
            return items;
        }
        public virtual IEnumerable<DNHLanguage> SearchData(SearchFilter value)
        {
            DNHLanguageCollection items = new DNHLanguageCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHLanguageCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHLanguageCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }

        public virtual DataTable ImportData(DataTable objList)
        {
            IEnumerable<DNHLanguage> myList = objList.ToList<DNHLanguage>();
            DNHLanguageCollection ErrorList = new DNHLanguageCollection();
            foreach (DNHLanguage objitem in myList)
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
            return ErrorList.ToDataTable<DNHLanguage>();
        }
        #endregion
        public static void RemoveCache(DNHLanguage objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.LanguageID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
            HttpCache.RemoveSearchCache(SystemConfig.AllowSearchCache, SETTINGS_Search_KEY);
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

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
    public class DNHLocaleStringResourceManager : IServiceManager<DNHLocaleStringResource>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHLocaleStringResource.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHLocaleStringResource.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHLocaleStringResource.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHLocaleStringResource.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHLocaleStringResource";
        #endregion

        #region Servier Method

        public static DNHLocaleStringResource Add(DNHLocaleStringResource objItem)
        {
            DNHLocaleStringResource b = new DNHLocaleStringResource();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHLocaleStringResource>().GetAwaiter().GetResult();
                }
            }
            
            RemoveCache(b);
            return b;
        }

        public static DNHLocaleStringResource Update(DNHLocaleStringResource objItem)
        {

            DNHLocaleStringResource item = new DNHLocaleStringResource();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHLocaleStringResource>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHLocaleStringResource model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.ResourceName, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHLocaleStringResource model)
        {
            return "";
        }
        public virtual DNHLocaleStringResource Save(DNHLocaleStringResource model)
        {
            model.CreatedDate = SystemConfig.CurrentDate;
            string ErrorMessage = ValidateData(model);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (model.LocaleStringResourceID != 0)
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
        public virtual DNHLocaleStringResource Default()
        {
            return new DNHLocaleStringResource()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate
            };
        }
        public virtual string Get(string ResourceName)
        {

            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?LocaleStringResourceID={0}&CompanyID={1}", ResourceName,CommonHelper.CurrentUser.CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public static DNHLocaleStringResource GetbyID(string ResourceName, int CompanyID, int LanguageID)
        {
            string key = String.Format(SETTINGS_ID_KEY, ResourceName, CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHLocaleStringResource)obj2; }

            DNHLocaleStringResource b = null; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&RefID={2}", ResourceName, CompanyID, LanguageID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsAsync<DNHLocaleStringResource>().GetAwaiter().GetResult(); 
                }
            }
            if(b!=null && !string.IsNullOrEmpty(b.ResourceName))
            {
                HttpCache.Max(key, b);
            }
            return b;
        }
        public virtual IEnumerable<DNHLocaleStringResource> SearchData(SearchFilter value)
        {
            DNHLocaleStringResourceCollection items = new DNHLocaleStringResourceCollection();
            if (value.OrderBy.ToLower() == "id")
            {
                value.OrderBy = "LocaleStringResourceID";
            }
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHLocaleStringResourceCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHLocaleStringResourceCollection>().GetAwaiter().GetResult();
                }
            }

            if (SystemConfig.AllowSearchCache && items.Count>0)
            {
                HttpCache.Max(key, items);
            }
            return items;

            //   return Search(value);
        }

        public virtual DataTable ImportData(DataTable objList)
        {
            IEnumerable<DNHLocaleStringResource> myList = objList.ToList<DNHLocaleStringResource>();
            DNHLocaleStringResourceCollection ErrorList = new DNHLocaleStringResourceCollection();
            foreach (DNHLocaleStringResource objitem in myList)
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
            return ErrorList.ToDataTable<DNHLocaleStringResource>();
        }
        #endregion
        public static void RemoveCache(DNHLocaleStringResource objItem)
        {
            if (objItem != null)
            {
                HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
                HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ResourceName));
                HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
                HttpCache.RemoveSearchCache(SystemConfig.AllowSearchCache, SETTINGS_Search_KEY);
            }
        }
        public static string HouseEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }
        #region Business Method
        public static DNHLocaleStringResource LResource(string ResourceName, int CompanyID, int LanguageID)
        {
            DNHLocaleStringResource objItem = GetbyID(ResourceName, CompanyID, LanguageID);
            if (objItem == null)
            {
                objItem = new DNHLocaleStringResource() {
                    CompanyID=CompanyID,
                    LanguageID=LanguageID,
                    ResourceName=ResourceName,
                    ResourceValue=ResourceName,
                    CreatedDate = SystemConfig.CurrentDate,
                    CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName
                };
                objItem = Add(objItem);
            }
            return objItem;
        }
        #endregion
    }
}

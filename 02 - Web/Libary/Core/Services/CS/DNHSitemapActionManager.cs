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
    public class DNHSitemapActionManager : IServiceManager<DNHSitemapAction>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHSitemapAction.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHSitemapAction.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHSitemapAction.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHSitemapAction.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHSitemapAction";
        #endregion

        #region Servier Method

        public static DNHSitemapAction Add(DNHSitemapAction objItem)
        {
            DNHSitemapAction b = new DNHSitemapAction();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHSitemapAction>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHSitemapAction Update(DNHSitemapAction objItem)
        {

            DNHSitemapAction item = new DNHSitemapAction();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHSitemapAction>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHSitemapAction model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.ID, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHSitemapAction model)
        {
            return "";
        }
        public virtual DNHSitemapAction Save(DNHSitemapAction model)
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

		public virtual DNHSitemapAction Default()
        {
            return new DNHSitemapAction()
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

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?id={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public static string Get(DNHSitemapAction model)
        {

            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format(Resource + "?action={0}","action" ), model).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public static DNHSitemapActionCollection GetById(int id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHSitemapActionCollection)obj2; }

            DNHSitemapActionCollection items = new DNHSitemapActionCollection();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&action=get", id, CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHSitemapActionCollection>().GetAwaiter().GetResult();
                }
            }
			if(items != null && items.Count>0){HttpCache.Max(key, items);}
            return items;
        }
        public virtual IEnumerable<DNHSitemapAction> SearchData(SearchFilter value)
        {
            DNHSitemapActionCollection items = new DNHSitemapActionCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHSitemapActionCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHSitemapActionCollection>().GetAwaiter().GetResult();
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
            IEnumerable<DNHSitemapAction> myList = objList.ToList<DNHSitemapAction>();
            DNHSitemapActionCollection ErrorList = new DNHSitemapActionCollection();
            foreach (DNHSitemapAction objitem in myList)
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
            return ErrorList.ToDataTable<DNHSitemapAction>();
        }
        #endregion
        public static void RemoveCache(DNHSitemapAction objItem)
        {
            if (objItem != null)
            {
                HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
                HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ID));
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
    }
}

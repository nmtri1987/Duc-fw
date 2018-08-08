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
    public class DNHRolesManager : IServiceManager<DNHRoles>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHRoles.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHRoles.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHRoles.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHRoles.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHRoles";
        #endregion

        #region Servier Method

        public static DNHRoles Add(DNHRoles objItem)
        {
            DNHRoles b = new DNHRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHRoles>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHRoles Update(DNHRoles objItem)
        {

            DNHRoles item = new DNHRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHRoles>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHRoles model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.Rolename, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHRoles model)
        {
            return "";
        }
        public virtual DNHRoles Save(DNHRoles model)
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

		public virtual DNHRoles Default()
        {
            return new DNHRoles()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                //CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
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

		public static DNHRoles GetById(string id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHRoles)obj2; }

            DNHRoles b = new DNHRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Rolename={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHRoles>().GetAwaiter().GetResult();
                }
            }
			if(b!=null && b.CompanyID!=0){HttpCache.Max(key, b);}
            return b;
        }
        public virtual IEnumerable<DNHRoles> SearchData(SearchFilter value)
        {
            DNHRolesCollection items = new DNHRolesCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHRolesCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHRolesCollection>().GetAwaiter().GetResult();
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
            IEnumerable<DNHRoles> myList = objList.ToList<DNHRoles>();
            DNHRolesCollection ErrorList = new DNHRolesCollection();
            foreach (DNHRoles objitem in myList)
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
            return ErrorList.ToDataTable<DNHRoles>();
        }
        #endregion
        public static void RemoveCache(DNHRoles objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Rolename));
           // HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
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

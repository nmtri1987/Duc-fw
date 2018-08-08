using System;
using System.Net.Http;
using Helpers;
using Biz.OG.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Biz.OG.Services
{
    public class DNHUsersManager : IServiceManager<DNHUsers>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHUsers.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHUsers.{0}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHUsers.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHUsers.Search.{0}{1}{2}{3}{4}{5}{6}";
        private const string Resource = "DNHUsers";
        #endregion

        #region Servier Method

        public static DNHUsers Add(DNHUsers objItem)
        {
            DNHUsers b = new DNHUsers();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHUsers>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHUsers Update(DNHUsers objItem)
        {

            DNHUsers item = new DNHUsers();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHUsers>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHUsers model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.Id, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHUsers model)
        {
            return "";
        }
        public virtual DNHUsers Save(DNHUsers model)
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

		public virtual DNHUsers Default()
        {
            return new DNHUsers()
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

		public static DNHUsers GetById(string id)
        {
            string key = String.Format(SETTINGS_ID_KEY, id);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHUsers)obj2; }

            DNHUsers b = new DNHUsers();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Id={0}", id)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHUsers>().GetAwaiter().GetResult();
                }
            }
			if(b!=null && b.CompanyID!=0)){HttpCache.Max(key, b);}
            return b;
        }
        public virtual IEnumerable<DNHUsers> SearchData(SearchFilter value)
        {
            DNHUsersCollection items = new DNHUsersCollection();
            string key = string.Format(SETTINGS_Search_KEY,value.CompanyID ,value.Keyword ,value.Page ,value.PageSize,value.OrderBy,value.OrderDirection,value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHUsersCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHUsersCollection>().GetAwaiter().GetResult();
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
            IEnumerable<DNHUsers> myList = objList.ToList<DNHUsers>();
            DNHUsersCollection ErrorList = new DNHUsersCollection();
            foreach (DNHUsers objitem in myList)
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
            return ErrorList.ToDataTable<DNHUsers>();
        }
        #endregion
        public static void RemoveCache(DNHUsers objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Id));
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
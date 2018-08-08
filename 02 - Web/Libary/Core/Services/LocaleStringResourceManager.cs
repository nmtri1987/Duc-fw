using System;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;
using System.Collections.Generic;
using System.Data;

using Biz.Core.Converts;
namespace Biz.Core.Services
{
    public class LocaleStringResourceManager: IServiceManager<LocaleStringResource>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.LocaleStringResource.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.LocaleStringResource.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.LocaleStringResource.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.LocaleStringResource.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "LocaleStringResource";
        #endregion
        #region Base Class IServiceManager
        public virtual void Del(LocaleStringResource model)
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
        public string ValidateData(LocaleStringResource model)
        {
            return "";
        }
        public virtual LocaleStringResource Save(LocaleStringResource model)
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
        public virtual LocaleStringResource Default()
        {
            return new LocaleStringResource()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
             //   CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate
            };
        }
        public virtual string Get(string ResourceName)
        {

            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}", ResourceName)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public static LocaleStringResource GetbyID(string ResourceName, int CompanyID, int LanguageID)
        {
            string key = String.Format(SETTINGS_ID_KEY, ResourceName, CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (LocaleStringResource)obj2; }

            LocaleStringResource b = null; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&RefID={2}", ResourceName, CompanyID, LanguageID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    b = response.Content.ReadAsAsync<LocaleStringResource>().GetAwaiter().GetResult();
                }
            }
            if (b != null && !string.IsNullOrEmpty(b.ResourceName))
            {
                HttpCache.Max(key, b);
            }
            return b;
        }
        public virtual IEnumerable<LocaleStringResource> SearchData(SearchFilter value)
        {
            LocaleStringResourceCollection items = new LocaleStringResourceCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (LocaleStringResourceCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LocaleStringResourceCollection>().GetAwaiter().GetResult();
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
            IEnumerable<LocaleStringResource> myList = objList.ToList<LocaleStringResource>();
            LocaleStringResourceCollection ErrorList = new LocaleStringResourceCollection();
            foreach (LocaleStringResource objitem in myList)
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
            return ErrorList.ToDataTable<LocaleStringResource>();
        }
        #endregion
        public static LocaleStringResourceCollection GetAll(int CompanyID)
        {
            LocaleStringResourceCollection items = new LocaleStringResourceCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (LocaleStringResourceCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LocaleStringResourceCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static LocaleStringResource GetById(int id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (LocaleStringResource)obj2; }

            LocaleStringResource b = new LocaleStringResource();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?LocaleStringResourceID={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<LocaleStringResource>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static LocaleStringResource Add(LocaleStringResource objItem)
        {
            LocaleStringResource b = new LocaleStringResource();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<LocaleStringResource>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(LocaleStringResource objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.LocaleStringResourceID,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static LocaleStringResource Update(LocaleStringResource objItem)
        {

            LocaleStringResource item = new LocaleStringResource();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.LocaleStringResourceID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<LocaleStringResource>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(LocaleStringResource objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.LocaleStringResourceID,objItem.CompanyID));
			HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser,objItem.CompanyID));
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
        public static LocaleStringResourceCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            LocaleStringResourceCollection items = new LocaleStringResourceCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (LocaleStringResourceCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LocaleStringResourceCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static LocaleStringResourceCollection Search(SearchFilter value)
        {
            LocaleStringResourceCollection items = new LocaleStringResourceCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (LocaleStringResourceCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<LocaleStringResourceCollection>().GetAwaiter().GetResult();
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
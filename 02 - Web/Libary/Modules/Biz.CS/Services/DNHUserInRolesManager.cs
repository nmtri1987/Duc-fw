using System;
using System.Net.Http;
using Helpers;
using Biz.CS.Models;
using System.Data;
using Biz.Core.Converts;
using System.Collections.Generic;
using Newtonsoft.Json;
using Biz.CS.Models;

namespace Biz.CS.Services
{
    public class DNHUserInRolesManager : IServiceManager<DNHUserInRoles>
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHUserInRoles.all";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHUserInRoles.{0}{1}{2}";
        private const string SETTINGS_User_KEY = "ifinds.Models.DNHUserInRoles.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.DNHUserInRoles.Search.{0}";
        private const string Resource = "DNHUserInRoles";
        #endregion

        #region Servier Method

        public static DNHUserInRoles Add(DNHUserInRoles objItem)
        {
            DNHUserInRoles b = new DNHUserInRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHUserInRoles>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }

        public static DNHUserInRoles Update(DNHUserInRoles objItem)
        {

            DNHUserInRoles item = new DNHUserInRoles();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.CompanyID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHUserInRoles>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        #endregion
        #region Base Class IServiceManager
        public virtual void Del(DNHUserInRoles model)
        {
            if (model != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {
                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", model.UserID, model.CompanyID)).GetAwaiter().GetResult();
                }
                RemoveCache(model);
            }
        }
        public string ValidateData(DNHUserInRoles model)
        {
            return "";
        }
        public virtual DNHUserInRoles Save(DNHUserInRoles model)
        {
            model.CreatedDate = SystemConfig.CurrentDate;
            model.ModifiedDate = SystemConfig.CurrentDate;
            string ErrorMessage = ValidateData(model);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                DNHUserInRoles OldRole = JsonConvert.DeserializeObject<DNHUserInRoles>(Get(model.UserID, model.CompanyID, model.RoleName));
                if (OldRole== null)
                {
                    DNHUsers objUser = DNHUsersManager.GetById(model.UserID,model.CompanyID);
                    if (objUser!=null)
                    {
                        model.UserName = objUser.UserName;
                    }
                    model = Add(model);
                }
                else
                {
                    model = Update(model);
                }
            }
            else
            {
                model.ErrorMesssage = ErrorMessage;
            }
            return model;
        }
        public virtual DNHUserInRoles Default()
        {
            return new DNHUserInRoles()
            {
                CompanyID = Biz.Core.Security.CustomerAuthorize.CurrentUser.CompanyID,
                CreatedUser = Biz.Core.Security.CustomerAuthorize.CurrentUser.UserName,
                CreatedDate = SystemConfig.CurrentDate.Date
            };
        }
        public virtual string Get(string id, int CompanyID, string RoleName)
        {
            string b = ""; ;
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&RefID={2}", id, CompanyID, RoleName)).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public virtual string Get(string id)
        {
            string b = ""; 
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}&RefID={2}", id)).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsStringAsync().Result;
                }
            }
            return b;
        }
        public virtual IEnumerable<DNHUserInRoles> SearchData(SearchFilter value)
        {
            DNHUserInRolesCollection items = new DNHUserInRolesCollection();
            string key = string.Format(SETTINGS_Search_KEY, value.CompanyID + value.Keyword + value.Page + value.PageSize + value.OrderBy + value.OrderDirection);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHUserInRolesCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHUserInRolesCollection>().GetAwaiter().GetResult();
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
            IEnumerable<DNHUserInRoles> myList = objList.ToList<DNHUserInRoles>();
            DNHUserInRolesCollection ErrorList = new DNHUserInRolesCollection();
            foreach (DNHUserInRoles objitem in myList)
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
            return ErrorList.ToDataTable<DNHUserInRoles>();
        }
        #endregion
        public static void RemoveCache(DNHUserInRoles objItem)
        {
            Biz.Core.Security.CustomerAuthorize.ResetUser();
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.UserID,objItem.CompanyID,objItem.RoleName));
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

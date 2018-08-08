using System;
using System.Collections.Generic;
using System.Linq;
using Biz.Core.Domain.Messages;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;
namespace Biz.Core.Services.Messages
{
    public partial class QueuedEmailService
    {

    }

    public class QueuedEmailManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.QueuedEmail.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.QueuedEmail.{0}{1}";
        private const string SETTINGS_User_KEY = "ifinds.Models.QueuedEmail.USer.{0}{1}";
        private const string SETTINGS_Search_KEY = "ifinds.Models.QueuedEmail.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "QueuedEmail";
        #endregion
        public static QueuedEmailCollection GetAll(int CompanyID)
        {
            QueuedEmailCollection items = new QueuedEmailCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (QueuedEmailCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}", CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<QueuedEmailCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static QueuedEmail GetById(int id, int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id, CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (QueuedEmail)obj2; }

            QueuedEmail b = new QueuedEmail();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Id={0}&CompanyID={1}", id, CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<QueuedEmail>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }


        public static QueuedEmail Add(QueuedEmail objItem)
        {
            QueuedEmail b = new QueuedEmail();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<QueuedEmail>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(QueuedEmail objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.Id, objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static QueuedEmail Update(QueuedEmail objItem)
        {

            QueuedEmail item = new QueuedEmail();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.Id), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<QueuedEmail>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(QueuedEmail objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.Id, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_User_KEY, objItem.CreatedUser, objItem.CompanyID));
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
        public static QueuedEmailCollection GetAllByUser(string CreatedUser, int CompanyID)
        {
            QueuedEmailCollection items = new QueuedEmailCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (QueuedEmailCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser, CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<QueuedEmailCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

        public static QueuedEmailCollection GetUnSendingEmail()
        {
            QueuedEmailCollection items = new QueuedEmailCollection();
            SearchFilter value = new SearchFilter();
            value.CompanyID = 1;
            value.Keyword = "";
            value.OrderBy = "CreatedOnUtc";
            value.ColumnsName = "CreatedOnUtc";
            value.OrderDirection = "desc";
            value.Condition = " CompanyID=1 and sentONUtc is null ";
            value.Page = 1;
            value.PageSize = 50;

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<QueuedEmailCollection>().GetAwaiter().GetResult();
                }
            }

            return items;
        }
        public static QueuedEmailCollection Search(SearchFilter value)
        {
            QueuedEmailCollection items = new QueuedEmailCollection();
            //string key = string.Format(SETTINGS_Search_KEY, value.CompanyID+value.Keyword+value.Page+value.PageSize+value.OrderBy+value.OrderDirection)
            string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (QueuedEmailCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<QueuedEmailCollection>().GetAwaiter().GetResult();
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

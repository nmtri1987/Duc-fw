using System;
using System.Net.Http;
using Helpers;
using Biz.Core.Models;

namespace Biz.Core.Services
{
    public class DNHMessageTemplateManager
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "ifinds.Models.DNHMessageTemplate.all{0}";
        private const string SETTINGS_ID_KEY = "ifinds.Models.DNHMessageTemplate.{0}{1}";
		private const string SETTINGS_User_KEY = "ifinds.Models.DNHMessageTemplate.USer.{0}{1}";
		private const string SETTINGS_Search_KEY = "ifinds.Models.DNHMessageTemplate.Search.{0}{1}{2}{3}{4}{5}";
        private const string Resource = "DNHMessageTemplate";
        #endregion
        public static DNHMessageTemplateCollection GetAll(int CompanyID)
        {
            DNHMessageTemplateCollection items = new DNHMessageTemplateCollection();
            string key = String.Format(SETTINGS_ALL_KEY, CompanyID);  
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (DNHMessageTemplateCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?CompanyID={0}",  CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHMessageTemplateCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }
        public static DNHMessageTemplate GetById(int id,int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, id,CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHMessageTemplate)obj2; }

            DNHMessageTemplate b = new DNHMessageTemplate();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?ID={0}&CompanyID={1}", id,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHMessageTemplate>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        /// <summary>
        /// Get DNH template by Company
        /// </summary>
        /// <param name="messageTemplateName"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static DNHMessageTemplate GetMessageTemplateByName(string messageTemplateName, int CompanyID)
        {
            string key = String.Format(SETTINGS_ID_KEY, messageTemplateName, CompanyID);
            object obj2 = HttpCache.Get(key);
            if (obj2 != null) { return (DNHMessageTemplate)obj2; }

            DNHMessageTemplate b = new DNHMessageTemplate();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "?Temp={0}&CompanyID={1}&temp=", messageTemplateName, CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHMessageTemplate>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, b);
            return b;
        }

        public static DNHMessageTemplate Add(DNHMessageTemplate objItem)
        {
            DNHMessageTemplate b = new DNHMessageTemplate();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {

                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    b = response.Content.ReadAsAsync<DNHMessageTemplate>().GetAwaiter().GetResult();
                }
            }
            RemoveCache(b);
            return b;
        }
        public static void Delete(DNHMessageTemplate objItem)
        {
            if (objItem != null)
            {
                using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
                {

                    HttpResponseMessage response = client.DeleteAsync(string.Format(Resource + "?id={0}&CompanyID={1}", objItem.ID,objItem.CompanyID)).GetAwaiter().GetResult();

                }
                RemoveCache(objItem);
            }

        }
        public static DNHMessageTemplate Update(DNHMessageTemplate objItem)
        {

            DNHMessageTemplate item = new DNHMessageTemplate();
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PutAsJsonAsync(string.Format(Resource + "?id={0}", objItem.ID), objItem).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<DNHMessageTemplate>().GetAwaiter().GetResult();
                    RemoveCache(item);
                }
            }
            return item;
        }

        public static void RemoveCache(DNHMessageTemplate objItem)
        {
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ALL_KEY, objItem.CompanyID));
            HttpCache.RemoveByPattern(string.Format(SETTINGS_ID_KEY, objItem.ID,objItem.CompanyID));
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
        public static DNHMessageTemplateCollection GetAllByUser(string CreatedUser,int CompanyID)
        {
            DNHMessageTemplateCollection items = new DNHMessageTemplateCollection();

            string key = String.Format(SETTINGS_User_KEY, CreatedUser);
            object obj2 = HttpCache.Get(key);

            if ((obj2 != null))
            {
                return (DNHMessageTemplateCollection)obj2;
            }
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.GetAsync(string.Format(Resource + "/GetbyUser?usr={0}&CompanyID={1}", CreatedUser,CompanyID)).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHMessageTemplateCollection>().GetAwaiter().GetResult();
                }
            }
            HttpCache.Max(key, items);
            return items;
        }

		public static DNHMessageTemplateCollection Search(SearchFilter value)
        {
            DNHMessageTemplateCollection items = new DNHMessageTemplateCollection();
			//string key = string.Format(SETTINGS_Search_KEY, value.CompanyID+value.Keyword+value.Page+value.PageSize+value.OrderBy+value.OrderDirection)
			string key = string.Format(SETTINGS_Search_KEY, value.Keyword, value.Page, value.PageSize, value.OrderBy, value.OrderDirection, value.Condition);
            if (SystemConfig.AllowSearchCache)
            {
                object obj2 = HttpCache.Get(key);

                if ((obj2 != null))
                {
                    return (DNHMessageTemplateCollection)obj2;
                }
            }

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource + "?method=search", value).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<DNHMessageTemplateCollection>().GetAwaiter().GetResult();
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
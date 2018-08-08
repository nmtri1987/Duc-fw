using daitiphu.common.Localization;
using DTP.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ifinds.Api.Controllers
{
    public class LanguageController : ApiController
    {
        //// GET: api/Language
        public tblLanguageCollection Get()
        {
            return tblLanguageManager.GetAllItem();
        }

        // GET: api/Language/5
        public string Get(string id)
        {
            tblLanguage language = daitiphu.common.ContentHelper.Current.WorkingLanguage;
            string item = tblLanguageManager.GetLocaleResourceString(id, 7);//(language.LanguageId) change later
            if (item == null)
            {
                string message = "Resource doesn't exist";

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            return item;
        }

        //// POST: api/Language
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Language/5
        public void Put(int id, [FromBody]string value)
        {
            tblLanguage objLang = tblLanguageManager.GetItemByID(id);
            if (objLang != null)
            {
                daitiphu.common.Util.CommonHelper.setCurrentLanguage(objLang);
            }
        }

        //// DELETE: api/Language/5
        //public void Delete(int id)
        //{
        //}
    }
}

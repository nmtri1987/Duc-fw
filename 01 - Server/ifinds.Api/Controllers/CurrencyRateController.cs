using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CA.Models;
using ifinds.Object.CA;
namespace ifinds.Object.CA.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CurrencyRateController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CurrencyRateCollection Get(int CompanyID)
        {
            return CurrencyRateManager.GetAllItem(CompanyID);
        }

        public CurrencyRateCollection GetbyUser(string usr, int CompanyID)
        {
            return CurrencyRateManager.GetbyUser(usr, CompanyID);
        }





        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="CurrencyRateId">The COM group identifier.</param>
        /// <returns></returns>
        public CurrencyRate Get(int CuryRateID, int CompanyID)
        {
            return CurrencyRateManager.GetItemByID(CuryRateID, CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public CurrencyRate Post([FromBody]CurrencyRate value)
        {
            CurrencyRate objItem = new CurrencyRate();
            try
            {
                objItem = CurrencyRateManager.AddItem(value);
            }
            catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog() { LinkUrl = Request.RequestUri.AbsoluteUri, Exception = ObjEx.Message, Message = ObjEx.StackTrace });
            }
            return objItem;
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public CurrencyRate Put(string id, [FromBody]CurrencyRate value)
        {
            CurrencyRate objItem = new CurrencyRate();
            try
            {
                objItem = CurrencyRateManager.UpdateItem(value);
            }
            catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog() { LinkUrl = Request.RequestUri.AbsoluteUri, Exception = ObjEx.Message, Message = ObjEx.StackTrace });
            }
            return objItem;
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CurrencyRateCollection Post(string method, [FromBody] SearchFilter value)
        {
            return CurrencyRateManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id, int CompanyID)
        {
            CurrencyRateManager.DeleteItem(id, CompanyID);
        }
    }
}
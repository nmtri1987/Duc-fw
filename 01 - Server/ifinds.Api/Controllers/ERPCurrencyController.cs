using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CA.Models;
using ifinds.Object.CA;
namespace ifinds.Object.FI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ERPCurrencyController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CurrencyCollection Get(int CompanyID)
        {
            return CurrencyManager.GetAllItem(CompanyID);
        }

		public CurrencyCollection GetbyUser(string usr,int CompanyID)
        {
            return CurrencyManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="CurrencyId">The COM group identifier.</param>
        /// <returns></returns>
        public Currency Get(string CuryID,int CompanyID)
        {
            return CurrencyManager.GetItemByID(CuryID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Currency Post([FromBody]Currency value)
        {
            Currency objItem = new Currency();
            try
            {
				objItem = CurrencyManager.AddItem(value);
			}
			catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog(){LinkUrl = Request.RequestUri.AbsoluteUri,Exception = ObjEx.Message,Message = ObjEx.StackTrace});
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
        public Currency Put(string id, [FromBody]Currency value)
        {
            Currency objItem = new Currency();
            try
            {
				objItem= CurrencyManager.UpdateItem(value);
			}
			catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog(){LinkUrl = Request.RequestUri.AbsoluteUri,Exception = ObjEx.Message,Message = ObjEx.StackTrace});
            }
            return objItem;
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CurrencyCollection Post(string method,[FromBody] SearchFilter value)
        {
            return CurrencyManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id,int CompanyID)
        {
            CurrencyManager.DeleteItem(id,CompanyID);
        }
    }
}
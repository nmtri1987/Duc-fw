using DTP.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTP.Object.GSM;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ContactsController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ContactsCollection Get()
        {
            return ContactsManager.GetAllItem();
        }

        public ContactsCollection GetbyUser(string usr)
        {
            return ContactsManager.GetbyUser(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="ContactsId">The COM group identifier.</param>
        /// <returns></returns>
        public Contacts Get(Int32 Id)
        {
            return ContactsManager.GetItemByID(Id);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Contacts Post([FromBody]Contacts value)
        {
            return ContactsManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public Contacts Put(string id, [FromBody]Contacts value)
        {
            return ContactsManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Int32 id)
        {
            ContactsManager.DeleteItem(id);
        }
    }
}
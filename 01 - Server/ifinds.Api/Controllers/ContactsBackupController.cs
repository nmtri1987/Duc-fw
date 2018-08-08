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
    public class ContactsBackupController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ContactsBackupCollection Get()
        {
            return ContactsBackupManager.GetSimPort();
        }

		public ContactsBackupCollection GetbyUser(string usr)
        {
            return ContactsBackupManager.GetbyUser(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="ContactsBackupId">The COM group identifier.</param>
        /// <returns></returns>
        public ContactsBackup Get(Int32 Id)
        {
            return ContactsBackupManager.GetItemByID(Id);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ContactsBackup Post([FromBody]ContactsBackup value)
        {
            return ContactsBackupManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public ContactsBackup Put(string id, [FromBody]ContactsBackup value)
        {
            return ContactsBackupManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Int32 id)
        {
            ContactsBackupManager.DeleteItem(id);
        }
    }
}
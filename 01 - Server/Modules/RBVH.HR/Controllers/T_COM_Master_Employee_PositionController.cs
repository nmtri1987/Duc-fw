using System.Web.Http;

using RBVH.HR.Models;
namespace RBVH.HR.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_COM_Master_Employee_PositionController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_Employee_PositionCollection Get()
        {
            return T_COM_Master_Employee_PositionManager.GetAllItem();
        }

		public T_COM_Master_Employee_PositionCollection GetbyUser(string usr)
        {
            return T_COM_Master_Employee_PositionManager.GetbyUser(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_Employee_PositionId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Employee_Position Get(int ID)
        {
            return T_COM_Master_Employee_PositionManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Employee_Position Post([FromBody]T_COM_Master_Employee_Position value)
        {
            return T_COM_Master_Employee_PositionManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Employee_Position Put(string id, [FromBody]T_COM_Master_Employee_Position value)
        {
            return T_COM_Master_Employee_PositionManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_Employee_PositionCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_Employee_PositionManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_Employee_PositionManager.DeleteItem(id);
        }
    }
}
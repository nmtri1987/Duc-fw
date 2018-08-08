
using System.Web.Http;

using Biz.OG.Models;
namespace Biz.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_COM_Master_GradeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_GradeCollection Get()
        {
            return T_COM_Master_GradeManager.GetAllItem();
        }

		public T_COM_Master_GradeCollection GetbyUser(string usr)
        {
            return T_COM_Master_GradeManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_GradeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Grade Get(int GradeID)
        {
            return T_COM_Master_GradeManager.GetItemByID(GradeID);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_GradeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Grade Get(string GradeName)
        {
            return T_COM_Master_GradeManager.GetItemByGradeName(GradeName);
        }
        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Grade Post([FromBody]T_COM_Master_Grade value)
        {
            return T_COM_Master_GradeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Grade Put(string id, [FromBody]T_COM_Master_Grade value)
        {
            return T_COM_Master_GradeManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_GradeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_GradeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_GradeManager.DeleteItem(id);
        }
    }
}
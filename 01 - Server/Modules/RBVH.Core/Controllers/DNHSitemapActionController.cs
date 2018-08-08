
using RBVH.Core.Models;
using System.Collections.Generic;
namespace RBVH.Core.Controllers
{
    public class DNHSitemapActionController : BaseApi<DNHSitemapAction>
    {
        public DNHSitemapAction Post(DNHSitemapAction model, string action)
        {
            switch (action)
            {
                case "":
                default:
                    return DNHSitemapActionManager.Get(model) ;
                    break;
            }
            
        }
        public IEnumerable<DNHSitemapAction> Get(int ID, int CompanyID,string action)
        {
            return DNHSitemapActionManager.GetDataByID(ID, CompanyID);
        }
    }
}
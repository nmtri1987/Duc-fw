using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using RBVH.Core.Models;
using DTP.Core.Directory;
using ActiveDirectory;
namespace RBVH.TMS.Ext.Controllers
{
    [AutoInvalidateCacheOutput]
    public class UserController : ApiController
    {

        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public List<ADUser> Get(int CompanyID)
        {
            return ADUser.GetUsers(DomainManager.RootPath);
            //return SettingManager.GetAllItem(CompanyID);
        }
    }
}

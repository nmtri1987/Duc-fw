using System;
using System.ComponentModel.DataAnnotations;
namespace Biz.OG
{
    public class DNHUsers : BaseEntity
    {
	   [LocalizedDisplayNameAttribute(CompanyID)] 



































    }
    public class DNHUsersCollection : BaseEntityCollection<DNHUsers> { }
}

namespace Biz.OG.Controllers
{
    public class DNHUsersController : BaseController<DNHUsers> {}
}
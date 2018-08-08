//using System;
//using System.ComponentModel.DataAnnotations;
//namespace Biz.CS
//{
//    public class DNHRoleSitemap : BaseEntity
//    {

//        [Required]
//        public int CompanyID { get; set; }

//        [Required]
//        public string RoleName { get; set; }

//        [Required]
//        public Guid NodeID { get; set; }

//        //public string SiteMapName
//        //{
//        //    get
//        //    {
//        //        return Biz.CS.Services.DNHSiteMapManager.GetById(NodeID, CompanyID).Title;
//        //    }
//        //}

//        public int Access { get; set; }


//        public string CreatedUser { get; set; }

//        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
//        public DateTime CreateDate { get; set; }

//    }
//    public class DNHRoleSitemapCollection : BaseEntityCollection<DNHRoleSitemap> { }

//}
using Biz.Core.Models;
namespace Biz.Core.Controllers
{
    public class RoleSitemapController : BaseController<DNHRoleSitemap> { }
}
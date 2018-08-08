using System;
using System.ComponentModel.DataAnnotations;
namespace Biz.Core.Models
{
    public class DNHRoleSitemap : BaseEntity
    {


        public int CompanyID { get; set; }

        [Required]
        [LocalizedDisplayName("RoleName")]
        public string RoleName { get; set; }

        [Required]
        [LocalizedDisplayName("NodeID")]
        public Guid NodeID { get; set; }

        [Required]
        [LocalizedDisplayName("Access")]
        public int Access { get; set; }


        public string CreatedUser { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }

    }
    public class DNHRoleSitemapCollection : BaseEntityCollection<DNHRoleSitemap> { }

}

//namespace Biz.Core.Controllers
//{
//    public class RoleSitemapController : BaseController<Biz.Core.Models.DNHRoleSitemap> {}
//}
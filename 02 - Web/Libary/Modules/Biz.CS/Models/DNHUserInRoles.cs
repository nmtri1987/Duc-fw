using System;
using System.ComponentModel.DataAnnotations;
using Biz.CS.Services;
using Biz.Core.Attribute;
namespace Biz.CS.Models
{
    //[DataContract]
    public class DNHUserInRoles : BaseEntity
    {
        [ColumnAttribute(Hide =true)]
        public int CompanyID { get; set; }
        [ColumnAttribute(Hide = true)]
        public string UserID { get; set; }

        
        public string RoleName { get; set; }

       

        public string ApplicationName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }

        [ColumnAttribute(Hide = true,NotAllowSearch =true)]
        public Biz.Core.Models.DNHRoleSitemapCollection SiteMapInRoles
        {
            get
            {
                if (!string.IsNullOrEmpty(RoleName))
                {
                    return Biz.Core.Services.DNHRoleSitemapManager.GetByRoles(RoleName);
                }
                return null;
            }
        }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        
    }
    public class DNHUserInRolesCollection : BaseEntityCollection<DNHUserInRoles> { }
}

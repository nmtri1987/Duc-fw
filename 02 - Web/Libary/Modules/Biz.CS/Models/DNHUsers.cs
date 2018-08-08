using Biz.Core.Attribute;
using System;
using System.ComponentModel.DataAnnotations;
namespace Biz.CS
{
    public class DNHUsers : BaseEntity
    {
        [ColumnAttribute(DataType = "alboolfnc", NotAllowSearch = true,ActionLink  = "selectRow")]
        public string Select
        {
            get;
            set;
        }
        [LocalizedDisplayNameAttribute("CompanyID")]

        public int CompanyID { get; set; }

        [LocalizedDisplayNameAttribute("Id")]

        public string Id { get; set; }

        [LocalizedDisplayNameAttribute("Email")]

        public string Email { get; set; }

        [LocalizedDisplayNameAttribute("EmailConfirmed")]

        public bool EmailConfirmed { get; set; }

        [LocalizedDisplayNameAttribute("PasswordHash")]

        public string PasswordHash { get; set; }

        [LocalizedDisplayNameAttribute("SecurityStamp")]

        public string SecurityStamp { get; set; }

        [LocalizedDisplayNameAttribute("PhoneNumber")]

        public string PhoneNumber { get; set; }

        [LocalizedDisplayNameAttribute("PhoneNumberConfirmed")]

        public bool PhoneNumberConfirmed { get; set; }

        [LocalizedDisplayNameAttribute("TwoFactorEnabled")]

        public bool TwoFactorEnabled { get; set; }

        [LocalizedDisplayNameAttribute("LockoutEndDateUtc")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime LockoutEndDateUtc { get; set; }

        [LocalizedDisplayNameAttribute("LockoutEnabled")]

        public bool LockoutEnabled { get; set; }

        [LocalizedDisplayNameAttribute("AccessFailedCount")]

        public int AccessFailedCount { get; set; }

        [LocalizedDisplayNameAttribute("UserName")]

        public string UserName { get; set; }

        [LocalizedDisplayNameAttribute("CreatedUser")]

        public string CreatedUser { get; set; }

        [LocalizedDisplayNameAttribute("CreatedDate")]

        public DateTime CreatedDate { get; set; }

        [LocalizedDisplayNameAttribute("IsAdmin")]

        public bool IsAdmin { get; set; }

        [LocalizedDisplayNameAttribute("Application")]

        public string Application { get; set; }


    }
    public class DNHUsersCollection : BaseEntityCollection<DNHUsers> { }
}


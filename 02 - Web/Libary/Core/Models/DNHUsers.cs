using Biz.Core.Attribute;
using System;
using System.ComponentModel.DataAnnotations;
namespace Biz.Core
{
    public class DNHUsers : BaseEntity
    {
       
        public int CompanyID { get; set; }


        public string Id { get; set; }


        public string Email { get; set; }


        public bool EmailConfirmed { get; set; }


        public string PasswordHash { get; set; }


        public string SecurityStamp { get; set; }


        public string PhoneNumber { get; set; }


        public bool PhoneNumberConfirmed { get; set; }


        public bool TwoFactorEnabled { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime LockoutEndDateUtc { get; set; }


        public bool LockoutEnabled { get; set; }


        public int AccessFailedCount { get; set; }


        public string UserName { get; set; }


        public string CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }


        public bool IsAdmin { get; set; }


        public string Application { get; set; }

        public Biz.Core.Models.DNHSiteMapCollection UserSiteMaps { get; set; }
        int _LanguageID = 1;
        public int UserLanguageID
        {
            get
            {
                return _LanguageID;
            }
            set
            {
                _LanguageID = value;
            }
        }
        #region RBVH Fields
        public int EmployeeCode { get; set; }
        public int EntityID { get; set; }
        public string EmployeeName { get; set; }
        public string DomainID { get; set; }
        public string EmployeeNumber { get; set; }
        public string DeptGrp { get; set; }
        public DateTime JoinedDate { get; set; }
        #endregion

    }
    public class DNHUsersCollection : BaseEntityCollection<DNHUsers> { }
}


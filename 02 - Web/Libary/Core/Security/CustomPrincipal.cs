using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace Biz.Core.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }

    public class RBVHUser
    {
        public int EmployeeCode { get; set; }
        public int EntityID { get; set; }
        public string EmployeeName { get; set; }
        public string DomainID { get; set; }
        public string EmployeeNumber { get; set; }
        public string DeptGrp { get; set; }
        public string AdminLocationId { get; set; }
        public string EmployeeLocationName { get; set; }
        public string AdminLocationName { get; set; }
        public string NTId { get; set; }
        public string EmployeeRolev { get; set; }
        public string Grpv { get; set; }
        public string Dept { get; set; }
        public string PositionName { get; set; }

        /// <summary>
        /// it is following by OrgChart
        /// </summary>
        public int DeprtmanetId { get; set; }
        public string EntityName { get; set; }
        public DateTime JoinedDate { get; set; }
        public string EntityAdminHome { get; set; }

        public int _CompanyID = 1;
        public int CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                _CompanyID = value;
            }
        }
        public string UserName { get; set; }
        public Biz.Core.Models.DNHSiteMapCollection UserSiteMaps { get; set; }
    }
    public class AuthenticatedUser
    {
        private int _company = 1;
        public int CompanyID
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
            }
        }
        public string BrandCD { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
        public string RoleKey { get; set; }
        public string TimeZonzeID { get; set; }
        public UserInfo userInfo
        {
            get
            {
                return new UserInfo();
            }
        }
        public string EmpCD { get; set; }

        public bool isNonAutoLogout { get; set; }
        public bool isViewAllCompanyReport { get; set; }
    }
    public class UserInfo : BaseEntity
    {

        public string UserId { get; set; }


        public string UserName { get; set; }


        public string Mobile { get; set; }

        [Required]
        public int CountryId { get; set; }


        public int LocationId { get; set; }


        public string Address { get; set; }


        public int Rating { get; set; }


        public bool Sex { get; set; }


        public string ImgUrl { get; set; }


        public string facebookId { get; set; }


        public string twitterid { get; set; }


        public bool linkFB { get; set; }


        public string SkypeId { get; set; }


        public bool linkLinken { get; set; }


        public bool linkTwitter { get; set; }


        public string Linkenid { get; set; }


        public string Position { get; set; }


        public string PhoneNumber { get; set; }


        public string WebsiteUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public string cmnd { get; set; }


        public bool isActive { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string FullName { get; set; }
        public string CuryId { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string BranchCD { get; set; }
        public string ImageLink
        {
            get
            {

                if (string.IsNullOrEmpty(ImgUrl))
                {
                    ImgUrl = "usrimg.jpg";
                }
                return string.Format(SystemConst.UserFolder, ImgUrl);
            }
        }
    }
    public class UserInfoCollection : BaseEntityCollection<UserInfo> { }

    public class SiteMapInRoles : BaseEntity
    {
        public int CompanyID { get; set; }

        public float Position { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public bool Expanded { get; set; }

        public bool IsFolder { get; set; }

        public string ScreenID { get; set; }

        public Guid NodeID { get; set; }

        public Guid ParentID { get; set; }


    }
    public class SiteMapInRolesCollection : BaseEntityCollection<SiteMapInRoles> { }
}
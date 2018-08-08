using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.Core.Models
{
    //[DataContract]
    public class UsersInRoles : BaseEntity
    {
        public int CompanyID { get; set; }

        public string Username { get; set; }

        public string Rolename { get; set; }

        public string ApplicationName { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdateUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Descr { get; set; }
        public bool IsSelected { get; set; }
    }
    public class UsersInRolesCollection : BaseEntityCollection<UsersInRoles> { }
}
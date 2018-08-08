using System;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
using Newtonsoft.Json;
namespace Biz.CS
{
    [Serializable]
    [JsonObject]
    public class DNHRoles : BaseEntity
    {
        [LocalizedDisplayName("CompanyID")]

        public int CompanyID { get; set; }

        [LocalizedDisplayName("Rolename")]

        public string Rolename { get; set; }


        [LocalizedDisplayName("ApplicationName")]

        public string ApplicationName { get; set; }

        [LocalizedDisplayName("Descr")]

        public string Descr { get; set; }

        [LocalizedDisplayName("isGuest")]

        public bool isGuest { get; set; }

        [LocalizedDisplayName("Createduser")]

        public string Createduser { get; set; }

        [LocalizedDisplayName("CreatedDate")]

        public DateTime CreatedDate { get; set; }


    }
    [Serializable]
    public class DNHRolesCollection : BaseEntityCollection<DNHRoles> { }
}


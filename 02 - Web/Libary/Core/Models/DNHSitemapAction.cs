using System;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
using Newtonsoft.Json;
namespace Biz.Core.Models
{
    [Serializable]
    [JsonObject]
    public class DNHSitemapAction : BaseEntity
    {
        
        [LocalizedDisplayName("CompanyID")]

        public int CompanyID { get; set; }

        [LocalizedDisplayName("ID")]

        public int ID { get; set; }

        [LocalizedDisplayName("RoleName")]

        public string RoleName { get; set; }

        [LocalizedDisplayName("NodeID")]

        public Guid NodeID { get; set; }

        [LocalizedDisplayName("ActionName")]

        public string ActionName { get; set; }

        [LocalizedDisplayName("Allow")]

        public bool Allow { get; set; }

        [LocalizedDisplayName("Edit")]

        public bool Edit { get; set; }

        [LocalizedDisplayName("CreatedUser")]

        public string CreatedUser { get; set; }

        [LocalizedDisplayName("CreatedDate")]

        public DateTime CreatedDate { get; set; }

        [LocalizedDisplayName("ScreenID")]

        public string ScreenID { get; set; }
       
       

    }
    [Serializable]
    public class DNHSitemapActionCollection : BaseEntityCollection<DNHSitemapAction> { }
}


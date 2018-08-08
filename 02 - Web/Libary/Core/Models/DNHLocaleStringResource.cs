using System;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;

namespace Biz.Core.Models
{
    public class DNHLocaleStringResource : BaseEntity
    {
        [ColumnAttribute(Hide = true)]
        public int LocaleStringResourceID { get; set; }

        [ColumnAttribute(DataType = "key", ActionLink = "Update", NotAllowSearch = true)]
        public int ID
        {
            get
            {
                return LocaleStringResourceID;
            }
        }

       

        [ColumnAttribute(Hide = true)]
        public int CompanyID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int LanguageID { get; set; }

      
        public string ResourceName { get; set; }


        public string ResourceValue { get; set; }


        public string CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }


    }
    public class DNHLocaleStringResourceCollection : BaseEntityCollection<DNHLocaleStringResource> { }
}

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
    public class LocaleStringResource : BaseEntity
    {


        public int LocaleStringResourceID { get; set; }


        public int LanguageID { get; set; }


        public string ResourceName { get; set; }


        public string ResourceValue { get; set; }


        public int CompanyID { get; set; }


        public int CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }


    }
    public class LocaleStringResourceCollection : BaseEntityCollection<LocaleStringResource> { }
}
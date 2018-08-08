using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
using Biz.Core.Attribute;
namespace Biz.CS.Models
{
    //[DataContract]
    public class DNHSiteMap : BaseEntity
    {

        [ColumnAttribute(Hide = true)]
        public int CompanyID { get; set; }

        [ColumnAttribute(DataType ="key",ActionLink ="Update")]
        public Guid NodeID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int Position { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public string Url { get; set; }


        public bool Expanded { get; set; }


        public bool IsFolder { get; set; }


        public string ScreenID { get; set; }

        [ColumnAttribute(Hide = true)]
        public Guid? ParentID { get; set; }


        public string CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }


        public string IconImage { get; set; }


    }
    public class DNHSiteMapCollection : BaseEntityCollection<DNHSiteMap> { }
}
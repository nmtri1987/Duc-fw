using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.OG.Models
{
    //[DataContract]
    public class EPPosition : BaseEntity
    {
        public int CompanyID { get; set; }

        public string PositionID { get; set; }

        public string Description { get; set; }

        public string CreatedUser { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public string PositionCD {
            get
            {
                return PositionID + " - " + Description;
            }
        }
        public string EPPositionKey { get; set; }


    }
    public class EPPositionCollection : BaseEntityCollection<EPPosition> { }
}
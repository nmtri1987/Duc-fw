using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.TMS.Models
{
    //[DataContract]
    public class ls_PayrollDOWS_RBVH : BaseEntity
    {


        public int Dow_ID { get; set; }


        public string Dow_Code { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Beg_Day { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime End_Day { get; set; }


        public int Dow_Num { get; set; }


        public int ENtityID { get; set; }


    }
    public class ls_PayrollDOWS_RBVHCollection : BaseEntityCollection<ls_PayrollDOWS_RBVH> { }
}
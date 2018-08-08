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
    public class T_CMS_InterfaceLacviet_WorkingHours : BaseEntity
    {


        public int WorkingId { get; set; }

        public string WorkingHourCD
        {
            get
            {
                //return WorkingId + "-" + WorkingHours +"Hours";
                return WorkingHours + " Hours";
            }
        }

        public int WorkingHours { get; set; }


        public int EntityID { get; set; }


        public string Note { get; set; }


        public int LacvietID { get; set; }


        public int DefaultShiftId { get; set; }


        public int CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime CreateDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


    }
    public class T_CMS_InterfaceLacviet_WorkingHoursCollection : BaseEntityCollection<T_CMS_InterfaceLacviet_WorkingHours> { }
}
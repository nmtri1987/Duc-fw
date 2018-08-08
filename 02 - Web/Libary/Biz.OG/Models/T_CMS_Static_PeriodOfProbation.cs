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
    public class T_CMS_Static_PeriodOfProbation : BaseEntity
    {


        public int ID { get; set; }

        public string CD
        {
            get
            {
                return  Period + " " + PeriodDescription;
            }
        }

        public int EmpSubTypeID { get; set; }


        public int Period { get; set; }


        public string PeriodDescription { get; set; }


        public bool IsActive { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


    }
    public class T_CMS_Static_PeriodOfProbationCollection : BaseEntityCollection<T_CMS_Static_PeriodOfProbation> { }
}
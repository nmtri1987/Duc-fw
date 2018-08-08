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
    public class T_CMS_Static_ContractTerm : BaseEntity
    {


        public int ID { get; set; }

        public string ContractTerm
        {
            get
            {
                if (ContractTermMonths != 0)
                {
                    return ContractTermMonths + " " + PeriodDescription;
                }else
                {
                    return "Indefinite contract";
                }
                
            }
        }

        public int EmpSubTypeID { get; set; }


        public int ContractTermMonths { get; set; }


        public bool IsActive { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public string PeriodDescription { get; set; }


    }
    public class T_CMS_Static_ContractTermCollection : BaseEntityCollection<T_CMS_Static_ContractTerm> { }
}
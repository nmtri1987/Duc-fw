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
    public class T_COM_Master_Grade : BaseEntity
    {


        public int GradeID { get; set; }
        public string GradeName { get; set; }

        public string GradeCD
        {
            get
            {
                return  GradeName +"-"+ GradeID;
            }
        }

        


        public int EntityID { get; set; }


        public bool IsMLevel { get; set; }


        public string EmployeeType { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


        public int Hierarchical_Level { get; set; }


    }
    public class T_COM_Master_GradeCollection : BaseEntityCollection<T_COM_Master_Grade> { }
}
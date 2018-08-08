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
    public class T_COM_Master_Degree : BaseEntity
    {


        public int DegreeID { get; set; }

        public string Degree { get; set; }
        public string DegreeCD
        {
            get
            {
                return DegreeID + "-" + Degree;
            }


        }
        


        public string DegreeVN { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


    }
    public class T_COM_Master_DegreeCollection : BaseEntityCollection<T_COM_Master_Degree> { }
}
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
    public class T_COM_Master_Position : BaseEntity
    {


        public int PositionID { get; set; }
        public string PositionName_EN { get; set; }
        public string PositionCD
        {
            get
            {
                return PositionName_EN + "-" + PositionID;
            }
        }
        


        public string PositionName_VN { get; set; }


        public int EntityID { get; set; }


        public string EmployeeType { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


    }
    public class T_COM_Master_PositionCollection : BaseEntityCollection<T_COM_Master_Position> { }

    public class PositionR:BaseEntity
    {
        public int PositionID { get; set; }
        public string PostionName { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
 
    }
    public class PositionRCollection : BaseEntityCollection<PositionR> { }
}
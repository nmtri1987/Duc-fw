using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
using Biz.OG.Services;
namespace Biz.OG.Models
{
    //[DataContract]
    public class T_COM_Master_Employee_Position : BaseEntity
    {


        public int ID { get; set; }

        public string PositionName
        {
            get
            {
                return T_COM_Master_PositionManager.GetById(PositionID).PositionName_EN;
            }
        }
        public int EmployeeCode { get; set; }


        public int GradeID { get; set; }


        public int PositionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }


        public decimal HomeCountrySalary { get; set; }


        public string HostCountryCurrency { get; set; }


        public string HomeCountryCurrency { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveTo { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsAcitve { get; set; }


    }
    public class T_COM_Master_Employee_PositionCollection : BaseEntityCollection<T_COM_Master_Employee_Position> { }
}
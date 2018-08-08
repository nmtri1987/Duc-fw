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
    
    public class T_COM_Master_University : BaseEntity
    {
        public int UniversityID { get; set; }

        public string UniversityName_EN { get; set; }


        public string UniversityName_VN { get; set; }


        public int EntityID { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


    }
 
    public class T_COM_Master_UniversityCollection : BaseEntityCollection<T_COM_Master_University> { }
}
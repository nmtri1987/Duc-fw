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
public class T_CMS_Master_Salutation :BaseEntity
{

public int SalutationID{ get; set; }

        public string SalutationCD
        {
            get
            {
                return Salutation_EN;
            } 
        }
public string Salutation_EN{ get; set; }

public string Salutation_VN{ get; set; }

public string Gender{ get; set; }

public bool IsActive{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

}
public class T_CMS_Master_SalutationCollection : BaseEntityCollection<T_CMS_Master_Salutation> { }
}
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
public class T_COM_Master_Location :BaseEntity
{

public int LocationID{ get; set; }
        public string LocationName { get; set; }
        public string LocationCD { get
            {
                return LocationID + "-" + LocationName;
            }
        }

        

public string LocationShortName{ get; set; }

public string Address_EN{ get; set; }

public string Address_VN{ get; set; }

public int EntityID{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

public bool IsActive{ get; set; }

}
public class T_COM_Master_LocationCollection : BaseEntityCollection<T_COM_Master_Location> { }
}
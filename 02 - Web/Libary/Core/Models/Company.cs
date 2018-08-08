using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.Core.Models
{
//[DataContract]
public class Company :BaseEntity
{


public int CompanyID{ get; set; }


public string CompanyCD{ get; set; }


public string BaseCuryID{ get; set; }


public int BAccountID{ get; set; }


public string CountryID{ get; set; }


public string PhoneMask{ get; set; }


public int ParentCompanyID{ get; set; }


public string CompanyType{ get; set; }


public string Theme{ get; set; }


public bool IsReadOnly{ get; set; }


public bool IsTemplate{ get; set; }


public string CompanyKey{ get; set; }


public int Sequence{ get; set; }


public int CreatedUser{ get; set; }


public DateTime CreatedDate{ get; set; }



}
public class CompanyCollection : BaseEntityCollection<Company> { }
}
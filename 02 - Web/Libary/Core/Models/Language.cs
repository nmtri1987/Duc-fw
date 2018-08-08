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
public class Language :BaseEntity
{

public int CompanyID{ get; set; }

public int LanguageId{ get; set; }

public string Name{ get; set; }

public string LanguageCulture{ get; set; }

public bool Published{ get; set; }

public int DisplayOrder{ get; set; }

public int CreatedUser{ get; set; }

public DateTime CreatedDate{ get; set; }

}
public class LanguageCollection : BaseEntityCollection<Language> { }
}
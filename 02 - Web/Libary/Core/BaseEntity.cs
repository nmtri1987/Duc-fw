using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Runtime.Serialization;
using Biz.Core.Services;

[Serializable()]
public abstract class BaseEntity
{
    public string TargetDisplayID { get; set; }
    public string ReturnDisplay { get; set; }
    public int TotalRecord { get; set; }

    public string isSelect { get; set; }

    [AllowHtml]
    public string ErrorMesssage { get; set; }
    
}
[Serializable()]
public abstract class BaseEntityCollection<T> : List<T> where T : BaseEntity, new()
{
    public int TotalRecord { get; set; }

}
public class DNHBase
{
    public static string L(string Resource)
    {
        Biz.Core.DNHUsers objUser = Biz.Core.Security.CustomerAuthorize.CurrentUser;
        if (objUser != null)
        {
            return Biz.Core.Services.DNHLocaleStringResourceManager.LResource(Resource, objUser.CompanyID, objUser.UserLanguageID).ResourceValue;
        }
        return Resource;
    }
    public string L(string ResourceKey,int LanguageID)
    {
        return LanguageManager.GetResourceName(ResourceKey, LanguageID);
    }
}

public class LocalizedDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
{
    public LocalizedDisplayNameAttribute(string key) : base(FormatMessage(key))
    {
    }

    private static string FormatMessage(string key)
    {
       return  DNHBase.L(key);
        // TODO: fetch the corresponding string from your resource file
        //throw new NotImplementedException();
    }
}
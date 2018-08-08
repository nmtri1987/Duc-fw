using Biz.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SystemConfig
{
    public static bool isAllowCache = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowCache"].ToString());
    public static bool AllowSearchCache = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowRemoveCacheSearch"].ToString());
    public static string loginKey = ConfigurationManager.AppSettings["SessionLoginKey"].ToString();
    public static string sitemapAccessKey = ConfigurationManager.AppSettings["SitemapAccessKey"].ToString();
    public static string UserAcess = ConfigurationManager.AppSettings["UserAccess"].ToString();
    public static int DefaultEntity = int.Parse(ConfigurationManager.AppSettings["EntityID"].ToString());
    public static int StartTime = 9;
    public static int EndTime = 18;
    public static int WorkingHour = 8;
    public static decimal DefaultPoint = 1000;
  
    public static DateTime EndDate
    {
        get
        {
            return DateTime.Parse("9999/12/31");
        }
    }
    public static DateTime CurrentDate
    {
        get
        {
            ///implement code later
            return DateTime.Now;
        }
    }

    //public static string BaseCurency(int CompanyID)
    //{
    //    Company model = CompanyManager.GetById(CompanyID);
    //        ///implement code later
    //        return model.BaseCuryID;

    //}
    public static decimal ConvertBaseCurency(decimal Amount,decimal InverseValue,decimal ConversionValue)
    {
        decimal baseAmount = 0;
        if (InverseValue > 0)
        {
            baseAmount = Amount * InverseValue;
        }
        else
        {
            baseAmount = Amount * (1 / ConversionValue);
        }
        ///implement code later
        return baseAmount;

    }

    public static decimal ConvertCurencyCurentcy(decimal Amount, decimal Value)
    {
        return Amount * Value;

    }

    public static string FinFormat(string str)
    {

            return string.Format("{0}-{1}", str.Substring(4, 2), str.Substring(0, 4));
    }

   
}

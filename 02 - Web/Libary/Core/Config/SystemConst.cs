using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SystemConst
{
    public const string RootEndpoint = "RootEndpoint";
    public const string HouseBanking = "HouseBankingAPI";
    public const string DateFM = "{0:dd/MM/yyyy}";
    //public const string DateFM = "{0:MM/dd/yyyy}";
    public const string APIJosonReturnValue = "application/json";
    public const string AddNew = "New";
    public const int CharityType = 1;
    public const string DefaultCuryId = "VND";
    public const string UserSalt = "0987230483";
    
    //user setup
    public const string UserFolder = "~/Img/Users/{0}";
    public const string UserFolderProfile = "~/img/Users/Profile/{0}";

    public const string EmpFolderProfile = "~/img/Employee/Profile/{0}";

    //project setup
    public const string PMTimeActiveFolder= "~/Upload/Project/{0}/{1}/{2}/";
    public const string PMProjectFolder = "~/Upload/Project/{0}/";

    //use to calculate in Models.ComGroup
    public const int dayNew = 7;
    public const string ComGroupFolder ="~/Img/Group/{0}/";
    public const string EventFolder = "~/Img/Event/{0}/";

    //TimeZone
    public const int WorkingHour = 8;
}

public class ContentType
{
    public const string FormData = "application/x-www-form-urlencoded";
}
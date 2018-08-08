using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;
using Biz.TMS.Models;
using System.Net.Http;
namespace Biz.TMS.Services
{
    public class EmpTaskManager
    {
        #region Constants

     
        private const string Resource = "EmpTask";

        #endregion Constants
        public static void  EmployeeGroup_Daily_Add(EmpTaskFilter objItem)
        {
            objItem.TaskType = "day";
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();
            }
            
        }

        public static void Employee_Weekly_Add(EmpTaskFilter objItem)
        {
            objItem.TaskType = "week";
            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Resource, objItem).GetAwaiter().GetResult();
            }

        }
        public static string HouseEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }
    }
}

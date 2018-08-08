using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Biz.TMS.Models;
using Biz.Core.Models;

namespace Biz.TMS.Services
{
    public class ReportManager
    {
        public static MissingScanTimeCollection GetExMonthlyReport(MissCanTimePara WebPara)
        {
            MissingScanTimeCollection items = new MissingScanTimeCollection();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Report"), WebPara).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<MissingScanTimeCollection>().GetAwaiter().GetResult();
                }
            }

            return items;
        }

        public static System.Data.DataTable EmployeeTMSSummaryReport(EMPTMSPara WebPara)
        {
            System.Data.DataTable items = new System.Data.DataTable();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Report?Event=tms"), WebPara).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<System.Data.DataTable>().GetAwaiter().GetResult();
                }
            }

            return items;
        }
        public static REmployeeCollections FiveDayReminder(REmployeePara objPra)
        {
            REmployeeCollections items = new REmployeeCollections();

            using (var client = WebApiHelper.myclient(HouseEndpoint, SystemConst.APIJosonReturnValue))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Report?Event=FiveD&Para="), objPra).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    items = response.Content.ReadAsAsync<REmployeeCollections>().GetAwaiter().GetResult();
                }
            }

            return items;
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

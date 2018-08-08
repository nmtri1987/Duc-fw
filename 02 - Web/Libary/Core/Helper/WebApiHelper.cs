using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Helpers
{
    public class WebApiHelper
    {
        public static HttpClient myclient(string EndPoint,string ReturnObject)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri(EndPoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ReturnObject));
            }catch(Exception objEx)
            {

            }
            return client;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RBVH.Core
{
    public class ModuleConfig
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["iWebConnect"].ToString();
        public static string MyConnection
        {
            get
            {
                if (ConnectionString != null)
                {
                    return DTP.Core.CryptoUtil.Decrypt(ConnectionString, true);
                }
                return null;
            }
        }
        //public const string HRConnection = ConfigurationManager.ConnectionStrings["iWebConnect"].ToString();
    }
}


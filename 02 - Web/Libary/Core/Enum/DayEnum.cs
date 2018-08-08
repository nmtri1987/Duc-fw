using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ifinds.Core.Enums
{
    public class DayEnum
    {
        enum Days { Sat, Sun, Mon, Tue, Wed, Thu, Fri };
        public static List<string> ReturnDay()
        {
            Array myarr = Enum.GetValues(typeof(Days));
            List<string> lstDays = Enum.GetValues(typeof(Days)).Cast<Days>().Select(x => x.ToString()).ToList();
            return lstDays;
        }
    }
   
}



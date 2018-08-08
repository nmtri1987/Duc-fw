using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OG.Const
{
    public class Constant
    {
        public const int ONE_MONTH_PROBATION_PERIOD = 29;
        public const int TWO_MONTH_PROBATION_PERIOD = 59;        
    }

    public enum eMonthProbation
    {
        ONE_MONTH_PROBATION_PERIOD = 1,
        TWO_MONTH_PROBATION_PERIOD = 2
    }
}

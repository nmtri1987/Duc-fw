using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifinds.Core.Enums
{
    public enum AccessRightsEnum
    {
        [Description("Not Set")]
        NotSet = 0,
        [Description("Revoked")]
        Revoked = 1,
        [Description("View Only")]
        ViewOnly = 2,
        [Description("Granted")]
        Granted = 3,
        [Description("Edit")]
        Edit = 4,
        [Description("Insert")]
        Insert = 5,
        [Description("Delete")]
        Delete = 6 ,
    }
}

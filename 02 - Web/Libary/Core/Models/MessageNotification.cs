using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Core.Models
{
    public class MessageNotification
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string FromUserImage { get; set; }
        public string LinkRedirect { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CompanyID { get; set; }
        public int NoNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

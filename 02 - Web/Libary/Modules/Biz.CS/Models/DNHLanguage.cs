using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.CS.Models
{
    public class DNHLanguage : BaseEntity
    {
        public int CompanyID { get; set; }
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class DNHLanguageCollection : BaseEntityCollection<DNHLanguage> { }
}

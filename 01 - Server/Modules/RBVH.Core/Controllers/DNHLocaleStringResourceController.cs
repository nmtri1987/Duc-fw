using RBVH.Core.Models;

namespace RBVH.Core.Controllers
{
    public class DNHLocaleStringResourceController : BaseApi<DNHLocaleStringResource>
    {
        public DNHLocaleStringResource Get(int LocaleStringResourceID, int CompanyID)
        {
            return DNHLocaleStringResourceManager.GetByID(LocaleStringResourceID, CompanyID);
        }
    }
}

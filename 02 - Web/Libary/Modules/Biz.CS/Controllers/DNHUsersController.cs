using Biz.Core.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Biz.CS.Controllers
{
    public class DNHUsersController : BaseController<DNHUsers> {


        [HttpPost]
        public JsonResult DNHUsersEvt(DNHUsersCollection usersColection, string Event)
        {
            var result = 0;
            
            switch (Event)
            {
                case "changeuser":
                    if (usersColection.Count > 0)
                    {
                        DNHUsers userSelect = usersColection.Where(m => !string.IsNullOrEmpty(m.isSelect)).FirstOrDefault();
                        if(userSelect != null)
                        {
                            CustomerAuthorize.ChangeUser(userSelect.UserName);
                            result = 1;
                        }
                        
                    }
                    //timesheetApproveReject.IsApproved = true;
                    break;
            }


            return Json(new
            {
                Event,
                result,
                JsonRequestBehavior.AllowGet
            });
        }
    }
}
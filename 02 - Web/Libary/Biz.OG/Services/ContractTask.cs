using System;
//using Biz.Core.Services.Logging;
using Biz.Core.Services.Tasks;
using Biz.Core.Domain.Messages;
using Biz.OG.Models;
using Biz.Core.Services.Messages;
namespace Biz.OG.Services
{

    public partial class ContractTask : ITask
    {
        
        public virtual void Execute()
        {
            //T_CMS_Master_ContractCollection
            try
            {
                //get the probation List
                //QueuedEmailCollection objEmailList = T_CMS_Master_ContractManager.GetProbationContractList(1);

                //foreach (QueuedEmail queuedEmail in objEmailList)
                //{
                //    //Insert into QueEmail and the email will auto send by the system
                //    QueuedEmailManager.Add(queuedEmail);


                //}
                //QueuedEmailManager.Add(new QueuedEmail()
                //{
                //    CompanyID = 1,
                //    Id = 0,
                //    To = "Lap.NhanMinh@vn.bosch.com",
                //    CC= "Linh.BuiPhuocHoang2@jp.bosch.com",
                //    From = "Tuan.HoHoang@vn.bosch.com",
                //    Subject="Reminder - Go to Binh Hung",
                //    Body="It is a day we spend together, please join Binh Hung Tour",
                // CreatedDate = DateTime.Now,
                // CreatedOnUtc = DateTime.UtcNow

                //});

            }
            catch (Exception exc)
            {
                // _logger.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
            }
            finally
            {
                //queuedEmail.SentTries = queuedEmail.SentTries + 1;
                //    _queuedEmailService.UpdateQueuedEmail(queuedEmail);
            }
        }
    }
}

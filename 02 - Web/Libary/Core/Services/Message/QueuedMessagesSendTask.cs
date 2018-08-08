using System;
//using Biz.Core.Services.Logging;
using Biz.Core.Services.Tasks;
using Biz.Core.Domain.Messages;
namespace Biz.Core.Services.Messages
{
    /// <summary>
    /// Represents a task for sending queued message 
    /// </summary>
    public partial class QueuedMessagesSendTask : ITask
    {
        

        

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute()
        {
            var maxTries = 3;
            
            var queuedEmails = QueuedEmailManager.GetUnSendingEmail(); //new QueuedEmailCollection();
            //_queuedEmailService.SearchEmails(fromEmail, toEmail, createdFromUtc, createdToUtc,
            //loadNotSentItemsOnly=true, loadOnlyItemsToBeSent=true, maxTries, loadNewest=false, 0, 500);
       //     IPagedList<QueuedEmail> SearchEmails(string fromEmail,
       //string toEmail, DateTime? createdFromUtc, DateTime? createdToUtc,
       //bool loadNotSentItemsOnly, bool loadOnlyItemsToBeSent, int maxSendTries,
       //bool loadNewest, int pageIndex = 0, int pageSize = int.MaxValue);
            foreach (var queuedEmail in queuedEmails)
            {
                var bcc = String.IsNullOrWhiteSpace(queuedEmail.Bcc) 
                            ? null 
                            : queuedEmail.Bcc.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var cc = String.IsNullOrWhiteSpace(queuedEmail.CC) 
                            ? null 
                            : queuedEmail.CC.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    EmailSender.SendEmail(null,
                        queuedEmail.Subject, 
                        queuedEmail.Body,
                       queuedEmail.From, 
                       queuedEmail.FromName, 
                       queuedEmail.To, 
                       queuedEmail.ToName,
                       queuedEmail.ReplyTo,
                       queuedEmail.ReplyToName,
                       bcc, 
                       cc, 
                       queuedEmail.AttachmentFilePath,
                       queuedEmail.AttachmentFileName,
                       queuedEmail.AttachedDownloadId);

                    queuedEmail.SentOnUtc = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                   // _logger.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
                }
                finally
                {
                    queuedEmail.SentTries = queuedEmail.SentTries + 1;
                    QueuedEmailManager.Update(queuedEmail);
                }
            }
        }
    }
}

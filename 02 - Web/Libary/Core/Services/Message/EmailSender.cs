using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Biz.Core.Domain.Messages;
using Biz.Core.Domain.Media;
using Biz.Core.Models;
using Biz.Core.Messages;
namespace Biz.Core.Services.Messages
{
    /// <summary>
    /// Email sender
    /// </summary>
    public partial class EmailSender //: IEmailSender
    {
        //private readonly IDownloadService _downloadService;

        //public EmailSender(IDownloadService downloadService)
        //{
        //    this._downloadService = downloadService;
        //}

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="replyTo">ReplyTo address</param>
        /// <param name="replyToName">ReplyTo display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachedment)</param>
        /// <param name="headers">Headers</param>
        public static void SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
             string replyTo = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            int attachedDownloadId = 0, IDictionary<string, string> headers = null)
        {
            var message = new MailMessage();
            //from, to, reply to
            message.From = new MailAddress(fromAddress, fromName);
            message.To.Add(new MailAddress(toAddress, toName));
            if (!String.IsNullOrEmpty(replyTo))
            {
                message.ReplyToList.Add(new MailAddress(replyTo, replyToName));
            }

            //BCC
            if (bcc != null)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }

            //CC
            if (cc != null)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }

            //content
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //headers
            if (headers != null)
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

            //create the file attachment for this e-mail message
            if (!String.IsNullOrEmpty(attachmentFilePath) &&
                File.Exists(attachmentFilePath))
            {
                var attachment = new Attachment(attachmentFilePath);
                attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentFilePath);
                attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentFilePath);
                attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentFilePath);
                if (!String.IsNullOrEmpty(attachmentFileName))
                {
                    attachment.Name = attachmentFileName;
                }
                message.Attachments.Add(attachment);
            }
            //another attachment?
            if (attachedDownloadId > 0)
            {
                var download = new Download(); // _downloadService.GetDownloadById(attachedDownloadId);
                if (download != null)
                {
                    //we do not support URLs as attachments
                    if (!download.UseDownloadUrl)
                    {
                        string fileName = !String.IsNullOrWhiteSpace(download.Filename) ? download.Filename : download.DownloadGuid.ToString();
                        fileName += download.Extension;
                        var ms = new MemoryStream(download.DownloadBinary);                        
                        var attachment = new Attachment(ms, fileName);
                        //string contentType = !String.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : "application/octet-stream";
                        //var attachment = new Attachment(ms, fileName, contentType);
                        attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
                        attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
                        attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
                        message.Attachments.Add(attachment);                        
                    }
                }
            }
            if (emailAccount == null)
            {
                emailAccount = new EmailAccount{
                    Host = "rb-smtp-int.bosch.com"
                  //  UseDefaultCredentials=
                };
            }
            //send email
            using (var smtpClient = new SmtpClient())
            {
               // smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                //  smtpClient.Port = emailAccount.Port;
                //   smtpClient.EnableSsl = emailAccount.EnableSsl;
                //    smtpClient.Credentials = emailAccount.UseDefaultCredentials ? 
                //  CredentialCache.DefaultNetworkCredentials :
                //  new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="emailAccount"></param>
        /// <param name="languageId"></param>
        /// <param name="tokens"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="toName"></param>
        /// <param name="attachmentFilePath"></param>
        /// <param name="attachmentFileName"></param>
        /// <param name="replyToEmailAddress"></param>
        /// <param name="replyToName"></param>
        /// <param name="fromEmail"></param>
        /// <param name="fromName"></param>
        /// <param name="subject"></param>
        /// <returns>Retuern QueuedEmail ID</returns>
        public static int SendNotification(int CompanyID,DNHMessageTemplate messageTemplate,
          EmailAccount emailAccount,IEnumerable<Token> tokens,
          string toEmailAddress, string toName,
          string attachmentFilePath = null, string attachmentFileName = null,
          string replyToEmailAddress = null, string replyToName = null,
          string fromEmail = null, string fromName = null, string subject = null)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");
            Tokenizer _tokenizer = new Tokenizer(new MessageTemplatesSettings());
            //retrieve localized message template data
            var bcc = messageTemplate.BccEmailAddresses;
            if (String.IsNullOrEmpty(subject))
                subject = messageTemplate.Subject;
            var body = messageTemplate.Body;

            //Replace subject and body tokens 
            var subjectReplaced = _tokenizer.Replace(subject, tokens, false);
            var bodyReplaced = _tokenizer.Replace(body, tokens, false);

            //limit name length
            toName = CommonHelper.EnsureMaximumLength(toName, 300);

            var email = new QueuedEmail
            {
                CompanyID = CompanyID,
                Priority = QueuedEmailPriority.High,
                From = !string.IsNullOrEmpty(fromEmail) ? fromEmail : emailAccount.Email,
                FromName = !string.IsNullOrEmpty(fromName) ? fromName : emailAccount.DisplayName,
                To = toEmailAddress,
                ToName = toName,
                ReplyTo = replyToEmailAddress,
                ReplyToName = replyToName,
                CC = string.Empty,
                Bcc = bcc,
                Subject = subjectReplaced,
                Body = bodyReplaced,
                AttachmentFilePath = attachmentFilePath,
                AttachmentFileName = attachmentFileName,
                AttachedDownloadId = messageTemplate.AttachedDownloadId,
                CreatedOnUtc = DateTime.UtcNow,
                CreatedDate = SystemConfig.CurrentDate,
                ///EmailAccountId = emailAccount.Id,
                DontSendBeforeDateUtc = !messageTemplate.DelayBeforeSend.HasValue ? null
                    : (DateTime?)(DateTime.UtcNow + TimeSpan.FromHours(messageTemplate.DelayPeriod.ToHours(messageTemplate.DelayBeforeSend.Value)))
            };

            QueuedEmailManager.Add(email);
            return email.Id;
        }

    }
}

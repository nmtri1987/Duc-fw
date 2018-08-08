using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.Core.Models;
using Biz.Core.Services;
using Biz.Core.Domain.Messages;
using Biz.Core.Services.Messages;
namespace Biz.Core.Messages
{
    public partial class WorkflowMessageService
    {
        protected virtual DNHMessageTemplate GetActiveMessageTemplate(string messageTemplateName, int CompanyID)
        {
            var messageTemplate = DNHMessageTemplateManager.GetMessageTemplateByName(messageTemplateName, CompanyID);

            //no template found
            if (messageTemplate == null)
                return null;

            //ensure it's active
            var isActive = messageTemplate.IsActive;
            if (!isActive)
                return null;

            return messageTemplate;
        }
        #region Example
        public virtual int SendProductEmailAFriendMessage(int languageId,string customerEmail, string friendsEmail, string personalMessage)
        {
            MessageTokenProvider _messageTokenProvider = new MessageTokenProvider();
         
            languageId = 7;

            var messageTemplate = GetActiveMessageTemplate(MessageTemplateSystemNames.EmailAFriendMessage,1);
            if (messageTemplate == null)
                return 0;

            //email account
          //  var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            //_messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            //_messageTokenProvider.AddCustomerTokens(tokens, customer);
            _messageTokenProvider.Add5DaysNotWorking(tokens);
            tokens.Add(new Token("EmailAFriend.PersonalMessage", personalMessage, true));
            tokens.Add(new Token("EmailAFriend.Email", customerEmail));

            //event notification
            //_eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            return SendNotification(messageTemplate, new EmailAccount(), languageId, tokens, friendsEmail, string.Empty);
        }
        /// <summary>
        /// Send notification
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <param name="emailAccount">Email account</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="tokens">Tokens</param>
        /// <param name="toEmailAddress">Recipient email address</param>
        /// <param name="toName">Recipient name</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name</param>
        /// <param name="replyToEmailAddress">"Reply to" email</param>
        /// <param name="replyToName">"Reply to" name</param>
        /// <param name="fromEmail">Sender email. If specified, then it overrides passed "emailAccount" details</param>
        /// <param name="fromName">Sender name. If specified, then it overrides passed "emailAccount" details</param>
        /// <param name="subject">Subject. If specified, then it overrides subject of a message template</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendNotification(DNHMessageTemplate messageTemplate,
            EmailAccount emailAccount, int languageId, IEnumerable<Token> tokens,
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
            var bodyReplaced = _tokenizer.Replace(body, tokens, true);

            //limit name length
            toName = CommonHelper.EnsureMaximumLength(toName, 300);

            var email = new QueuedEmail
            {
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
                ///EmailAccountId = emailAccount.Id,
                DontSendBeforeDateUtc = !messageTemplate.DelayBeforeSend.HasValue ? null
                    : (DateTime?)(DateTime.UtcNow + TimeSpan.FromHours(messageTemplate.DelayPeriod.ToHours(messageTemplate.DelayBeforeSend.Value)))
            };

            QueuedEmailManager.Add(email);
            return email.Id;
        }
        #endregion
    }
}

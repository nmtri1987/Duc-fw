using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.Core.Models;
using Biz.Core.Services;
using Biz.Core.Domain.Messages;
using Biz.Core.Services.Messages;
using Biz.Core.Messages;
using Biz.TMS.Models;
namespace Biz.TMS.Messages
{
    public partial class WorkflowMessageService
    {
        protected static DNHMessageTemplate GetActiveMessageTemplate(string messageTemplateName, int CompanyID)
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
        public static int SendFiveDayReminderMessage(string ToEmail, EmpReminderModel objEmp)
        {
            MessageTokenProvider _messageTokenProvider = new MessageTokenProvider();

            

            var messageTemplate = GetActiveMessageTemplate(MessageTemplateSystemNames.EmailAFriendMessage, 1);
            if (messageTemplate == null)
                messageTemplate = new DNHMessageTemplate() { Body= "%PM.Employee(s)%",Subject="TMS-5 Days Reminders"} ;

            //email account
            //  var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            
            _messageTokenProvider.Add5DaysNotWorking(tokens, objEmp);
            
            //tokens.Add(new Token("EmailAFriend.Email", customerEmail));

            //event notification
            //_eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            return EmailSender.SendNotification(1, messageTemplate, new EmailAccount(),  tokens, ToEmail, string.Empty,null,null,null,null, "Duc.NguyenHoai@vn.bosch.com", "Contract Manager System");
        }
        #endregion
    }
}

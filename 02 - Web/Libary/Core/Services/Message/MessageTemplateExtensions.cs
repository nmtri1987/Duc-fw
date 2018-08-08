using System.Collections.Generic;
using Biz.Core.Models;

namespace Biz.Core.Messages
{
    /// <summary>
    /// Represents message template  extensions
    /// </summary>
    public static class MessageTemplateExtensions
    {
        /// <summary>
        /// Get token groups of message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Collection of token group names</returns>
        public static IEnumerable<string> GetTokenGroups(this DNHMessageTemplate messageTemplate)
        {
            //groups depend on which tokens are added at the appropriate methods in IWorkflowMessageService
            switch (messageTemplate.Name)
            {
                case MessageTemplateSystemNames.FiveDaysReminderNotification:
                    return new[] { TokenGroupNames.FiveDaysReminder, TokenGroupNames.CustomerTokens };
                default:
                    return new string[] { };
            }
        }
    }
}
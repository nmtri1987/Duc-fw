using Biz.Core.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Core.Signalr
{
    [HubName("userNoti")]
    public class UserNoti:Hub
    {
        public static void SendMessage(MessageNotification model)
        {
            UserOnline us = new UserOnline();
            List<UserOnlineModel> list= us.GetUserOnline();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<UserNoti>();
            foreach(UserOnlineModel item in list)
            {
                if (item.UserName.Trim().ToLower() == model.ToUser.Trim().ToLower() && item.Company==model.CompanyID)
                {
                    context.Clients.Client(item.ClientID).showUsersNotification(model);
                    break;
                }
                
            }
            
        }
        public static void UpdateNotification(string ToUser,int CompanyID,int number)
        {
            UserOnline us = new UserOnline();
            List<UserOnlineModel> list = us.GetUserOnline();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<UserNoti>();
            foreach (UserOnlineModel item in list)
            {
                if (item.UserName.Trim().ToLower() == ToUser.Trim().ToLower() && item.Company == CompanyID)
                {
                    context.Clients.Client(item.ClientID).updateUsersNotification(number);
                    break;
                }

            }

        }
    }
}

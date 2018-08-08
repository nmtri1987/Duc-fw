using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Biz.Core.Signalr
{
    public class UserOnline : Hub
    {
        static List<UserOnlineModel> listUserName = new List<UserOnlineModel>();
        public override Task OnConnected()
        {
            string userName = Context.QueryString["username"];
            string timeLogin = Context.QueryString["datetime"];
            int company =System.Convert.ToInt32(Context.QueryString["company"]);
            var serverVars = Context.Request.GetHttpContext().Request.ServerVariables;
            var Ip = GetRemoteIpAddress(Context.Request);// serverVars["REMOTE_ADDR"];
            addUserOnline(userName, Ip, timeLogin, company);
            UpdateConnect(userName, company);
            ShowUsersOnLine();
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            string userName = Context.QueryString["username"];
            string timeLogin = Context.QueryString["datetime"];
            int company = System.Convert.ToInt32(Context.QueryString["company"]);
            string clientId = GetClientID();
            var serverVars = Context.Request.GetHttpContext().Request.ServerVariables;
            var Ip = GetRemoteIpAddress(Context.Request);
            addUserOnline(userName, Ip, timeLogin, company);
            UpdateConnect(userName, company);
            ShowUsersOnLine();
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.QueryString["username"];
            int company = Convert.ToInt32(Context.QueryString["company"]);
            string clientId = GetClientID();
            removeUserOnline(clientId);
            UpdateConnect(userName, company);
            ShowUsersOnLine();

            return base.OnDisconnected(stopCalled);
        }


        public string GetClientID()
        {
            string clientId = string.Empty;
            if (!(Context.QueryString["clientId"] == null))
            {
                clientId = Convert.ToString(Context.QueryString["clientId"]);
            }
            else
            {
                clientId = Context.ConnectionId;
            }
            return clientId;
        }
        public void ShowUsersOnLine()
        {

            Clients.All.ShowUsersOnLine(listUserName);
        }

        public void addUserOnline(string userName, string Ip, string timeLogin, int company)
        {
            bool isExist = false;
            UserOnlineModel model = new UserOnlineModel();
            DateTime date = Convert.ToDateTime(timeLogin);
            string clientId = GetClientID();
            int count = listUserName.Where(m => m.UserName.Trim() == userName.Trim() && m.Date == date && m.Company == company).Count<UserOnlineModel>();
            foreach (var item in listUserName)
            {
                if (item.ClientID.Trim() == clientId.Trim() || count > 0)
                {
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                model.ClientID = clientId;
                model.Date = Convert.ToDateTime(timeLogin);
                model.UserName = userName;
                model.IP = Ip;
                model.IsOnline = true;
                model.Company = company;
                listUserName.Add(model);
            }

            //else
            //{
            //    listUserName[index].ClientID = clientId;
            //    listUserName[index].Date= DateTime.Now;
            //}
        }
        public void removeUserOnline(string clientId)
        {
            bool isExist = false;
            UserOnlineModel model = new UserOnlineModel();
            foreach (var item in listUserName)
            {
                if (item.ClientID.Trim() == clientId.Trim())
                {
                    model = item;
                    isExist = true;
                    break;
                }
            }
            if (isExist)
            {
                listUserName.Remove(model);
            }
        }

        public string GetRemoteIpAddress(IRequest request)
        {
            object ipAddress;
            if (request.Environment.TryGetValue("server.RemoteIpAddress", out ipAddress))
            {
                return ipAddress as string;
            }
            return null;
        }
        public void UpdateConnect(string userName, int company)
        {
            DateTime? maxDateTime = new DateTime();
            var listUser = listUserName.Where(m => m.UserName.Trim() == userName.Trim() && m.Company == company);

            if (listUser.Count<UserOnlineModel>() > 0)
            {
                string userclient = string.Empty;

                foreach (UserOnlineModel item in listUser)
                {
                    item.IsOnline = false;
                    if (item.Date > maxDateTime)
                    {
                        userclient = item.ClientID;
                        maxDateTime = item.Date;
                        //item.IsOnline = true;
                    }

                }
                foreach (var item in listUser)
                {
                    if (item.ClientID.Trim() == userclient.Trim())
                    {
                        item.IsOnline = true;
                        break;
                    }
                }
                foreach (var item in listUserName)
                {
                    foreach (var items in listUser)
                    {
                        if (items.ClientID == item.ClientID)
                        {
                            item.IsOnline = items.IsOnline;
                        }
                    }
                }
            }

        }

        public List<UserOnlineModel> GetUserOnline()
        {
            return listUserName;
        }
    }

    public class UserOnlineModel
    {
        public string ClientID { get; set; }
        public DateTime? Date { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public bool IsOnline { get; set; }
        public int Company { get; set; }
    }
}
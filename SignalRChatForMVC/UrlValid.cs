using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRChatForMVC.Model;
using SignalRChatForMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRChatForMVC
{

    [HubName("UrlHubZzl")]
    public class UrlValid : Hub
    {
        static List<CurrentUser> ConnectedUsers = new List<CurrentUser>();

        public void Connect(string token)
        {
            var id = Context.ConnectionId;
            var entity = TokenContext.GetUserByToken(token);
            if (ConnectedUsers.Count(x => x.Token == token) == 0)
            {
                ConnectedUsers.Add(new CurrentUser
                {
                    ConnectionId = id,
                    Token = token,
                    UserID = entity.UserID,
                    UserName = entity.UserName
                });
                Clients.All.onNewUserConnected(token);
                Clients.All.onUserList(ConnectedUsers);
                Clients.All.onConnected(id, token, null);
            }
            else
            {

                Clients.Client(id).onExistUserConnected(id, token);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void Exit(string token)
        {
            var id = Context.ConnectionId;

            OnDisconnected();
            Clients.All.onUserList(ConnectedUsers);
            Clients.Client(id).onExit(token);
        }

        public void Message(string fromUser, string toUser, string message)
        {
            var id = Context.ConnectionId;
            var toConnection = ConnectedUsers.FirstOrDefault(x => x.Token == toUser);
            if (toConnection == null)
            {
                Clients.Client(id).onMessageError("你的接收方不在线...");
            }
            else
            {
                toConnection.MessageCount += 1;
                toConnection.MessageList.Add(new Message
                {
                    ID = Guid.NewGuid().ToString(),
                    Info = message,
                    ToUser = toUser,
                    FromUser = fromUser,
                    IsNew = 1,
                });
                Clients.Client(toConnection.ConnectionId).onMessage(toConnection.MessageCount);
                Clients.Client(toConnection.ConnectionId).onMessageList(toConnection.MessageList);
            }
        }
        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Token);

            }
            return base.OnDisconnected();
        }

    }
}

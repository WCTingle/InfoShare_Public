using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;

namespace Tingle_WebForms
{
    public class ChatHub : Hub
    {
        internal readonly static ConcurrentDictionary<string, string> _CurrentChatters = new ConcurrentDictionary<string, string>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public override System.Threading.Tasks.Task OnConnected()
        {

            // Get the current user with the new Identity system
            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            _CurrentChatters.AddOrUpdate(Context.ConnectionId, currentUser.DisplayName, (s, t) => s);
            Clients.All.NotifyConnection(CurrentUserName);
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string outString;
            var thisUser = CurrentUserName;
            _CurrentChatters.TryRemove(Context.ConnectionId, out outString);
            Clients.All.NotifyDisconnection(thisUser);

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// The name of the current user accessing the Hub
        /// </summary>
        public string CurrentUserName { get { return _CurrentChatters[Context.ConnectionId]; } } //Context.User.Identity.Name; } }

        /// <summary>
        /// Send a message out to all chatters
        /// </summary>
        /// <param name="msg"></param>
        public void Broadcast(string msg)
        {

            if (msg.StartsWith("<br />\n")) msg = msg.Substring(7);
            Clients.All.BroadcastMessage(CurrentUserName, msg);

        }

        /// <summary>
        /// Send a private message to a specific user
        /// </summary>
        /// <param name="targetUser"></param>
        /// <param name="msg"></param>
        public void Whisper(string targetUser, string msg)
        {

            Clients.User(targetUser).whisper(CurrentUserName, msg);

        }
    }
}
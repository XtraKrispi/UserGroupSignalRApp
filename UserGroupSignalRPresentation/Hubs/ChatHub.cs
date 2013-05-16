using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace UserGroupSignalRPresentation.Hubs
{
    /// <summary>
    /// SignalR Hub used in the chat portion of the application
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// List of messages that is stored statically to maintain an in memory history of messages.
        /// </summary>
        private static readonly List<UserMessage> Messages = new List<UserMessage>();
        private static readonly List<string> Connections = new List<string>(); 

        /// <summary>
        /// Server side method that sends a message back to the client consisting of all historical messages.
        /// This method is called on the client directly.
        /// </summary>
        public void GetMessages()
        {
            try
            {
                Clients.Caller.Messages(Messages);
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// A server side method that is used to broadcast when a message is sent.  This method is called
        /// directly on the client.
        /// </summary>
        /// <param name="username">The username of the user sending the message</param>
        /// <param name="messageText">The message text</param>
        public void SendMessage(string username, string messageText)
        {
            var message = new UserMessage
                              {
                                  Message = messageText,
                                  SentOn = DateTime.Now,
                                  Username = username
                              };

            
            Messages.Add(message);
            Clients.All.MessageSent(message);
        }
        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            return base.OnDisconnected();
        }
    }
}
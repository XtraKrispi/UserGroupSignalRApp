using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using UserGroupSignalRPresentation.Hubs;

namespace UserGroupSignalRPresentation.PersistentConnections
{
    /// <summary>
    /// SignalR Persistent Connection version of the ChatHub
    /// </summary>
    public class ChatPersistentConnection : PersistentConnection
    {
        /// <summary>
        /// List of messages that is stored statically to maintain an in memory history of messages.
        /// </summary>
        private static readonly List<UserMessage> Messages = new List<UserMessage>();

        /// <summary>
        /// Method that is called when a message is received
        /// </summary>
        /// <param name="request">The request context</param>
        /// <param name="connectionId">The connection Id sending the message</param>
        /// <param name="data">The message data being sent</param>
        /// <returns>A task, in this case a broadcasted message</returns>
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            // We need to convert the data coming in to a dynamic object
            // so we can determine what the client wants
            var obj = (dynamic)JsonConvert.DeserializeObject(data);

            // Based on the action, we either want to send back all messages, 
            // or broadcast to the clients that a message was sent.
            if (obj.action.Value == "getMessages")
            {
                return Connection.Broadcast(new {action = "messages", data = Messages});
            }
            if (obj.action.Value == "sendMessage")
            {
                var messageText = obj.message.messageText.Value;
                var username = obj.message.username.Value;
                var message = new UserMessage
                                  {
                                      Message = messageText,
                                      SentOn = DateTime.Now,
                                      Username = username
                                  };


                Messages.Add(message);
                return Connection.Broadcast(new {action = "messageSent", data = message});
            }

            return Connection.Broadcast("No Message");
        }
    }
}
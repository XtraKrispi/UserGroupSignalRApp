using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserGroupSignalRPresentation.Hubs
{
    /// <summary>
    /// Class to store details of a user message
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// The username of the user that sent the message
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The message text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The date indicating when the message was sent
        /// </summary>
        public DateTime SentOn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Models
{
    /// <summary>
    /// Inheritance of the <see cref="System.EventArgs"/> class
    /// </summary>
    public class ClientEventArgs : EventArgs
    {
        private static readonly List<Log> _logs = new List<Log>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Server.Models.ClientEventArgs"/> class with the specified 
        /// </summary>
        /// <param name="client"> Client's endpoint</param>
        /// <param name="action"> Client's action <see cref="Server.Models.Log.ClienAction"/></param>
        /// <param name="info">Additional information</param>
        public ClientEventArgs(EndPoint client, string action, string info = "")
        {
            _logs.Add(new Log(client, (Log.ClienAction)Enum.Parse(typeof(Log.ClienAction), action), info));
        }

        /// <summary>
        /// List of all the logs of the current session
        /// </summary>
        public List<Log> Logs => _logs;
    }
}

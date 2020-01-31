using System.Net;

namespace Server.Models
{
    /// <summary>
    /// <see cref="Server.Models.Log"/> allows to log client's action
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server.Models.Log"/> class with the specified 
        /// </summary>
        /// <param name="client"> Client's endpoint</param>
        /// <param name="action"> Client's action <see cref="Server.Models.Log.ClienAction"/></param>
        /// <param name="info">Additional information</param>
        public Log(EndPoint client, ClienAction action, string info = "")
        {
            Client = client;
            Action = action;
            Info = info;
        }

        /// <summary>
        /// Client's possible action
        /// </summary>
        public enum ClienAction
        {
            /// <summary>
            /// New client connected
            /// </summary>
            Connesso,

            /// <summary>
            /// Used for logging file upload
            /// </summary>
            Upload,

            /// <summary>
            /// Used for logging file download 
            /// </summary>
            Download

        }

        /// <summary>
        /// Client's EndPoint
        /// </summary>
        public EndPoint Client { get; private set; }

        /// <summary>
        /// Client's action <see cref="Server.Models.Log.ClienAction"/>
        /// </summary>
        public ClienAction Action { get; private set; }

        /// <summary>
        /// Additional information of client's action
        /// </summary>
        public string Info { get; private set; }

        /// <summary>
        /// Returns client's ip, action and additional information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Client} \n {Action} \n {(Info != "" ? Info : "")} \n\n";
        }
    }
}

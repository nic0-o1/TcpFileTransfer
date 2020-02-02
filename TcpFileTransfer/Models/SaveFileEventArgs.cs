using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpFileTransfer.Models
{
    /// <summary>
    /// Inheritance of the <see cref="System.EventArgs"/> class
    /// </summary>
    public class SaveFileEventArgs : EventArgs
    {
        /// <summary>
        /// Byte array containing the file content
        /// </summary>
        public byte[] received;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpFileTransfer.Models.SaveFileEventArgs"/> class  
        /// </summary>
        /// <param name="received">Byte array containing the file content</param>
        public SaveFileEventArgs(byte[] received)
        {
            this.received = received;
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TcpFileTransfer.Models
{
    /// <summary>
    /// <see cref="TcpFileTransfer.Models.ServerManagement"/> class allows to manage server connection
    /// </summary>
    public class ServerManagement
    {
        /// <summary>
        /// Represents the maximum size of files that can be uploaded at a time
        /// </summary>
        public static long SizeToUpload;

        /// <summary>
        /// Event for handling client's file save action
        /// </summary>
        public event EventHandler<SaveFileEventArgs> SaveFileEvent;

        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;

        private Byte[] received = new Byte[1000000];
        private Byte[] toSend = new Byte[1000000];

        private readonly Encoding encoding = Encoding.GetEncoding("Windows-1252");


        /// <summary>
        /// Initializes a new instance of the <see cref="TcpFileTransfer.Models.ServerManagement"/> class.
        /// </summary>
        /// <param name="ip">Server's ip</param>
        /// <param name="port">Server's port</param>
        public ServerManagement(string ip, int port = 55000)
        {
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();

            SizeToUpload = toSend.Length;
        }

        /// <summary>
        /// Client's request for updating file list 
        /// </summary>
        public void RequestDirectory()
        {
            Array.Clear(toSend, 0, toSend.Length);
            toSend = encoding.GetBytes("Directory;£&");
            stream.Write(toSend, 0, toSend.Length);
        }

        /// <summary>
        /// Receive from the server the updated shared folder
        /// </summary>
        public List<string> ReceiveDirectory()
        {
            Array.Clear(received, 0, received.Length);
            stream.Read(received, 0, received.Length);

            TrimEnd(received);

            Console.WriteLine(encoding.GetString(received));

            return JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
        }

        /// <summary>
        /// Disconnect the client from the server
        /// </summary>
        public void Disconnect()
        {
            if (tcpClient.Connected)
            {
                Array.Clear(toSend, 0, toSend.Length);
                toSend = encoding.GetBytes("Disconnect");
                stream.Write(toSend, 0, toSend.Length);
                tcpClient.Client.Close();
            }

        }

        /// <summary>
        /// Check if the server's connection has been disconnected
        /// </summary>
        /// <returns>True in case of disconnection</returns>
        public bool CheckForServerDisconnection()
        {
            if (stream.DataAvailable)
            {
                received = new Byte[1000000];
                toSend = new Byte[1000000];
                stream.Read(received, 0, received.Length);
                byte[] toSave = TrimEnd(received);

                string content = encoding.GetString(toSave);

                if (content.Contains("disconnection"))
                {
                    // server.Client.Close();
                    tcpClient.Client.Disconnect(true);
                    Console.WriteLine("dis");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Converts a file into byte array
        /// </summary>
        /// <param name="fileName">path of the file</param>
        /// <returns>byte array of the file</returns>
        private byte[] FileToByteArray(string fileName)
        {
            byte[] fileData = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryReader binaryReader = new BinaryReader(fs);
                fileData = binaryReader.ReadBytes((int)fs.Length);
            }
            return fileData;
        }

        /// <summary>
        /// Check if the server returned an error
        /// </summary>
        public bool CheckForErrors()
        {
            received = new Byte[1000000];
            stream.Read(received, 0, received.Length);
            received = TrimEnd(received);
            string msg = encoding.GetString(received);
            if (msg.Contains("Errore"))
            {
                throw new ArgumentException(msg);
            }
            return false;
        }

        /// <summary>
        /// Upload file to server's directory
        /// </summary>
        /// <param name="dropped">List of file to upload</param>
        public void UploadFiles(List<string> dropped)
        {
            Array.Clear(toSend, 0, toSend.Length);
            byte[] temp = new byte[1000000];
            string send = "upload;£&";
            foreach (string x in dropped)
            {
                send += x + ";£&";
                temp = FileToByteArray(x);

                toSend = encoding.GetBytes(send);

                byte[] content = toSend.Concat(temp).ToArray();

                toSend = TrimEnd(content);
                stream.Write(content, 0, content.Length);

                Array.Clear(toSend, 0, toSend.Length);

                if (CheckForErrors())
                {
                    break;
                }
                send = "upload;£&";
            }
            SizeToUpload = toSend.Length;
            RequestDirectory();

        }

        /// <summary>
        /// Request a file for being downloaded
        /// </summary>
        public void RequestFile(string toDownload)
        {
            if (tcpClient.Connected)
            {
                string send = "download;£&" + toDownload + ";£&";

                toSend = encoding.GetBytes(send);

                toSend = TrimEnd(toSend);

                stream.Write(toSend, 0, toSend.Length);

                toSend = new Byte[1000000];

                ReciveFile();
            }
        }

        /// <summary>
        /// Receive the selected file from the server
        /// </summary>
        private void ReciveFile()
        {
            try
            {
                Array.Clear(received, 0, received.Length);
                Array.Clear(toSend, 0, toSend.Length);

                stream.Read(received, 0, received.Length);

                SaveFileEvent?.Invoke(this, new SaveFileEventArgs(TrimEnd(received)));
            }
            catch (System.IO.IOException)
            {
                Console.Write("IO excp");
            }
            catch { }
        }

        /// <summary>
        /// Remove the empty parts of a byte array
        /// </summary>
        /// <param name="array">array to clear</param>
        /// <returns>The cleared array</returns>
        private byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }
    }
}

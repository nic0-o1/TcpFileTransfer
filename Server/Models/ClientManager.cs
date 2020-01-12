using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
    /// <summary>
    ///<see cref="Server.ClientManager"/> class allows to manage client's connection
    /// </summary>
    internal class ClientManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static string sharedDir;
        public static string selectedPath;
        public static bool canUpload = false;

        private readonly TcpClient _tcpClient;
        private Byte[] toSend = new Byte[400000];
        private Byte[] received = new Byte[400000];
        private readonly Encoding encoding = Encoding.GetEncoding("Windows-1252");
        private readonly NetworkStream stream;

         
        /// <summary>
        /// Initializes a new instance of the <see cref="Server.ClientManager"/> class.
        /// </summary>
        /// <param name="client">client to manage</param>
        public ClientManager(TcpClient client)
        {
            _tcpClient = client;
            stream = client.GetStream();

            log.Info($"Nuovo client connesso. IP {client.Client.RemoteEndPoint}");
        }

        /// <summary>
        /// Method <c>ManageConnection</c> manage the client connection
        /// </summary>
        public void ManageConnection()
        {
           // try
            {
                sendDirectory();
                while (true)
                {
                    readPath();
                }
            }
            //catch { }

        }

        /// <summary>
        /// Gives the content of a directory with files and subdirectories.
        /// Removes the path before the actual directory
        /// </summary>
        /// <returns>JSON with the content of the directories</returns>
        private void InitializeDirectory()
        {
            List<string> files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories).ToList();

            files = files.Select(x => x.Replace(selectedPath, string.Empty)).ToList();
            files = files.Select(p => (!string.IsNullOrEmpty(p) && p.Length > 1) ? p.Substring(1) : p).ToList();

            sharedDir = JsonConvert.SerializeObject(files);

            sendDirectory();
        }

        /// <summary>
        /// Send the shared directory content
        /// </summary>
        private void sendDirectory()
        {
            toSend = encoding.GetBytes(sharedDir);
            stream.Write(toSend, 0, toSend.Length);
        }

        /// <summary>
        /// Receive from the client the path of the file to download
        /// </summary>
        private void readPath()
        {
            Array.Clear(received, 0, received.Length);
            stream.Read(received, 0, received.Length);
            checkMessage();
        }

        /// <summary>
        /// Check the kind of message
        /// </summary>
        private void checkMessage()
        {
            received = TrimEnd(received);
            string rec = encoding.GetString(received);
            Array.Clear(toSend, 0, toSend.Length);

            string[] path = Regex.Split(rec, ";£&");

            if (path[0].Contains("download"))
            {
                toSend = File.ReadAllBytes(Path.Combine(selectedPath, path[1]));
                stream.Write(toSend, 0, toSend.Length);

                log.Info($"Download file: {path[1]} IP {_tcpClient.Client.RemoteEndPoint}");
            }
            else if (path[0].Contains("upload"))
            {
                if (canUpload)
                {
                    UploadFile(path);
                }
                else
                {
                    string error = "Errore: impossibile effettuare il caricamento del file. Upload disabilitato";

                    Array.Clear(toSend, 0, toSend.Length);
                    toSend = encoding.GetBytes(error);
                    toSend = TrimEnd(toSend);

                    stream.Write(toSend, 0, toSend.Length);
                }
            }
        }

        /// <summary>
        /// Save the client's file and send the updated directory information
        /// </summary>
        /// <param name="path">name of the file</param>
        private void UploadFile(string[] path)
        {
            string[] val = path[1].Split('\\');

            File.WriteAllText(Path.Combine(selectedPath, val[val.Length - 1]), path[2]);

            log.Info($"Caricato file: {val[val.Length - 1]} IP {_tcpClient.Client.RemoteEndPoint}");

            InitializeDirectory();
        }

        /// <summary>
        /// Remove the empty parts of a byte array
        /// </summary>
        /// <param name="array">array to clear</param>
        /// <returns>The cleared array</returns>
        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

    }
}

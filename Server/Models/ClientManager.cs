using Newtonsoft.Json;
using Server.Models;
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
    public class ClientManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly List<TcpClient> connectedClient = new List<TcpClient>();

        /// <summary>
        /// Event for handling client's action
        /// </summary>
        public event EventHandler<ClientEventArgs> ClientEvent;

        /// <summary>
        /// Event for handling client's upload action
        /// </summary>
        public event EventHandler<EventArgs> ContentUpdateEvent;

        /// <summary>
        /// Contains all files in the selected directory
        /// </summary>
        public static string sharedDir;

        /// <summary>
        /// The selected path of the shared directory
        /// </summary>
        public static string selectedPath;

        /// <summary>
        /// It tells whereas the upload is enabled or not 
        /// </summary>
        public static bool canUpload;

        private bool canRead = true;
        private Byte[] toSend = new Byte[1000000];
        private readonly TcpClient _tcpClient;
        private readonly Byte[] received = new Byte[1000000];
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
            connectedClient.Add(client);

            log.Info($"Nuovo client connesso. IP {client.Client.RemoteEndPoint}");

        }

        /// <summary>
        /// Method <c>ManageConnection</c> manage the client connection
        /// </summary>
        public void ManageConnection()
        {
            ClientEvent?.Invoke(this, new ClientEventArgs(_tcpClient.Client.RemoteEndPoint, "Connesso")); //l'invoke non può essere messo nel costruttore(l'evento è ancora null)
            try
            {
                SendDirectory();
                FormServer.ServerClosingEvent += FormServer_ServerClosingEvent; 

                while (canRead)
                {
                    ReadMessage();
                }
            }
            catch { }

        }

        /// <summary>
        /// Send the shared directory content
        /// </summary>
        private void SendDirectory()
        {
            toSend = encoding.GetBytes(sharedDir);
            stream.Write(toSend, 0, toSend.Length);
        }

        /// <summary>
        /// Receive from the client the path of the file to download
        /// </summary>
        private void ReadMessage()
        {
            try
            {
                if (stream != null)
                {
                    Array.Clear(received, 0, received.Length);
                    stream.Read(received, 0, received.Length);
                    CheckMessage();
                }
            }
            catch (System.IO.IOException) { canRead = false; }
            catch (ObjectDisposedException) { canRead = false; }
            catch { }
        }

        private void FormServer_ServerClosingEvent(object sender, EventArgs e)
        {
            canRead = false;
            foreach (TcpClient x in connectedClient)
            {
                NetworkStream st = x.GetStream();
                byte[] temp = new byte[500];
                temp = encoding.GetBytes("disconnection");
                st.Write(temp, 0, temp.Length);
                x.GetStream().Close();
                x.Close();
            }
            connectedClient.Clear();

            log.Warn("Server arrestato");
        }

        /// <summary>
        /// Gives the content of a directory with files and subdirectories.
        /// Removes the path before the actual directory and send it to tthe client
        ///<see cref="SendDirectory"/> class for sending the directory 
        /// </summary>
        private void InitializeDirectory()
        {
            List<string> files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories).ToList();

            files = files.Select(x => x.Replace(selectedPath, string.Empty)).ToList();
            files = files.Select(p => (!string.IsNullOrEmpty(p) && p.Length > 1) ? p.Substring(1) : p).ToList();

            sharedDir = JsonConvert.SerializeObject(files);

            SendDirectory();
        }

        /// <summary>
        /// Check the kind of message
        /// </summary>
        private void CheckMessage()
        {
            string rec = encoding.GetString(received);
            Array.Clear(toSend, 0, toSend.Length);

            string[] path = Regex.Split(rec, ";£&");

            if (path[0].Contains("download"))
            {
                toSend = File.ReadAllBytes(Path.Combine(selectedPath, path[1]));

                stream.Write(toSend, 0, toSend.Length);

                log.Warn($"Download file: {path[1]} IP {_tcpClient.Client.RemoteEndPoint}");

                ClientEvent?.Invoke(this, new ClientEventArgs(_tcpClient.Client.RemoteEndPoint, "Download", path[1]));

            }
            else if (path[0].Contains("upload"))
            {
                if (canUpload)
                {
                    SaveReceivedFile(path);
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
            else if (path[0].Contains("Directory"))
            {
                InitializeDirectory();
            }
            else if (path[0].Contains("Disconnect"))
            {
                DisconnectCurrentClient();
            }
        }
        /// <summary>
        /// Disconnect the current connected client
        /// </summary>
        private void DisconnectCurrentClient()
        {
            log.Info("Client disconnesso " + _tcpClient.Client.RemoteEndPoint);

            stream.Close();
            _tcpClient.Close();
            connectedClient.Remove(_tcpClient);
        }

        /// <summary>
        /// Save the client's file and send the updated directory information
        /// </summary>
        /// <param name="path">name of the file</param>
        private void SaveReceivedFile(string[] path)
        {
            string[] val = path[1].Split('\\');

            File.WriteAllBytes(Path.Combine(selectedPath, val[val.Length - 1]), TrimEnd(encoding.GetBytes(path[2])));

            log.Info($"Caricato file: {val[val.Length - 1]} IP {_tcpClient.Client.RemoteEndPoint}");

            ClientEvent?.Invoke(this, new ClientEventArgs(_tcpClient.Client.RemoteEndPoint, "Upload", val[val.Length - 1]));

            ContentUpdateEvent?.Invoke(this, new EventArgs());

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

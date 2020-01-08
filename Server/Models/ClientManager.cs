using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientManager
    {
        private TcpClient _tcpClient;
        private Byte[] toSend = new Byte[400000];
        private Byte[] received = new Byte[40000];
        private Encoding encoding = Encoding.GetEncoding("Windows-1252");
        private NetworkStream stream;
        public static string sharedDir;
        public static string selectedPath;
        public static bool canUpload = false;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClientManager(TcpClient client)
        {
            //log4net.Config.XmlConfigurator.Configure();
            _tcpClient = client;
            stream = client.GetStream();
            log.Info($"Nuovo client connesso. IP {client.Client.RemoteEndPoint}");
        }

        public void ManageConnection()
        {
            //try
            {
                sendDirectory();
                while (true)
                {
                    readPath();
                }
            }
           // catch { }

        }
        private void InitializeDirectory()
        {
            List<string> files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories).ToList();

            files = files.Select(x => x.Replace(selectedPath, string.Empty)).ToList();
            files = files.Select(p => (!string.IsNullOrEmpty(p) && p.Length > 1) ? p.Substring(1) : p).ToList();

            sharedDir =  JsonConvert.SerializeObject(files);

            sendDirectory();
        }
        private void sendDirectory()
        {
            toSend = encoding.GetBytes(sharedDir);
            stream.Write(toSend, 0, toSend.Length);
        }
        private void readPath()
        {
            Array.Clear(received, 0, received.Length);
            stream.Read(received, 0, received.Length);
            checkMessage();
        }
        private void checkMessage()
        {
            byte[] receivedCorrect = new byte[40000];
            Array.Clear(receivedCorrect, 0, receivedCorrect.Length);
            receivedCorrect = TrimEnd(received);
            string rec = encoding.GetString(receivedCorrect);
            Array.Clear(toSend, 0, toSend.Length);
            string[] path = rec.Split(';');
            if (path[0].Contains("download"))
            {
                toSend = File.ReadAllBytes(selectedPath+"\\" + path[1]);
                stream.Write(toSend, 0, toSend.Length);
                log.Info($"Download file: {path[1]} IP {_tcpClient.Client.RemoteEndPoint}");
            }
            else if (path[0].Contains("upload"))
            {
                if (canUpload)
                {
                    uploadFile(path);
                }
                else
                {
                    string error = "Errore: impossibile effettuare il caricamento del file. Upload disabilitato";
                    toSend = new Byte[400000];
                    toSend = encoding.GetBytes(error);
                    toSend = TrimEnd(toSend);
                    stream.Write(toSend, 0, toSend.Length);
                }
            }
        }
        private void uploadFile(string [] path)
        {
            string[] val = path[1].Split('\\');
            byte[] toSave = TrimEnd(received);
            File.WriteAllText(selectedPath + "\\" + val[val.Length - 1], path[2]);
            log.Info($"Caricato file: {val[val.Length - 1]} IP {_tcpClient.Client.RemoteEndPoint}");
            InitializeDirectory();
        }
        
        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

    }
}

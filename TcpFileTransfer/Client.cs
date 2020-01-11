using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TcpFileTransfer
{
    public partial class Client : MetroFramework.Forms.MetroForm
    {
        public Client()
        {
            InitializeComponent();
            groupBox1.AllowDrop = true;
        }

        private TcpClient server;
        private Byte[] toSend = new Byte[400000];
        private NetworkStream stream;
        private string toDownload;
        private Byte[] received = new Byte[400000];
        private readonly Encoding encoding = Encoding.GetEncoding("Windows-1252");

        private const int PORT = 55000;

        /// <summary>
        /// Connect to the TCP server
        /// </summary>
        /// <param name="ip">Server IP</param>
        private void connectToServer(string ip)
        {
            server = new TcpClient(ip, PORT);
        }

        /// <summary>
        /// Check if an ip address is correct
        /// </summary>
        /// <param name="toCheck">ip to check</param>
        /// <exception cref="ArgumentException">Thrown when the ip address is not correct</exception>
        private void checkIP(string toCheck)
        {
            if (!IPAddress.TryParse(toCheck, out IPAddress ip))
                throw new ArgumentException("Indirizzo ip non valido");
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                checkIP(txtIpServer.Text);

                connectToServer(txtIpServer.Text);

                stream = server.GetStream();

                ReceiveDirectory();

                lblIP.Text = $"Connesso a : {txtIpServer.Text}";
            }

            catch (ArgumentException) { txtIpServer.WithError = true; lblErroreIP.Text = "Indirizzo IP non valido"; }
            
            catch (SocketException) { MetroFramework.MetroMessageBox.Show(this, "\n\nImpossible contattare il server all'indirizzo: " + txtIpServer.Text, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            catch (Exception ex) { MetroFramework.MetroMessageBox.Show(this, "\n\n" + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        /// <summary>
        /// Receive from the server the updated shared folder
        /// </summary>
        private void ReceiveDirectory()
        {
            stream.Read(received, 0, received.Length);

            listBox1.DataSource = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            toDownload = listBox1.GetItemText(listBox1.SelectedItem);

            string send = "download;£&" + toDownload;

            toSend = encoding.GetBytes(send);

            toSend = TrimEnd(toSend);

            stream.Write(toSend, 0, toSend.Length);

            toSend = new Byte[400000];

            ReciveFile();
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

        /// <summary>
        /// Receive the selected file from the server
        /// </summary>
        private void ReciveFile()
        {
            received = new Byte[400000];
            toSend = new Byte[400000];
            stream.Read(received, 0, received.Length);
            byte[] toSave = TrimEnd(received);
            saveFile();
        }

        /// <summary>
        /// Saves the received file from the server
        /// </summary>
        private void saveFile()
        {
            string[] fileName = toDownload.Split('\\');
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleziona cartella di destinazione";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    byte[] toSave = TrimEnd(received);
                    File.WriteAllBytes(fbd.SelectedPath + "\\" + fileName[fileName.Length - 1], toSave);
                }
            }
        }

        private void groupBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Move;
                groupBox1.BackgroundImage = TcpFileTransfer.Properties.Resources.drag;
            }
        }

        private string[] dropped;
        private void groupBox1_DragDrop(object sender, DragEventArgs e)
        {
            dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            groupBox1.BackgroundImage = null;

        }

        private void groupBox1_DragLeave(object sender, EventArgs e)
        {
            groupBox1.BackgroundImage = null;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            toSend = new byte[400000];
            string send = "upload;£&";
            foreach (string x in dropped)
            {
                send += x + ";£&"+ File.ReadAllText(x);

                toSend = encoding.GetBytes(send);
                toSend = TrimEnd(toSend);
                stream.Write(toSend, 0, toSend.Length);

                toSend = new byte[400000];
                if (CheckForErrors())
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Check if the server returned an error
        /// </summary>
        private bool CheckForErrors()
        {
            received = new Byte[400000];
            stream.Read(received, 0, received.Length);
            string msg = encoding.GetString(received);
            if (msg.Contains("Errore"))
            {
                MetroFramework.MetroMessageBox.Show(this, "\n\n" + msg, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                received = TrimEnd(received);
                string rec = encoding.GetString(received);
                listBox1.DataSource = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
                return false;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (server.Connected && MetroFramework.MetroMessageBox.Show(this, "\n\nInterrompere la connessione al server ?", "Server connesso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                server.Client.Close();
                lblIP.Text = String.Empty;
            }
        }
    }
}

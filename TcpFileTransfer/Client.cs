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

        private void connectToServer(string ip, Int32 port)
        {
            server = new TcpClient(ip, 55000);
        }
        private bool checkIP(string toCheck)
        {
            IPAddress ip;
            if (!IPAddress.TryParse("1234.12.12.12", out ip))
                return false;
            return true;
            
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (checkIP(txtIpServer.Text))
            {
                connectToServer(txtIpServer.Text, 0);

                stream = server.GetStream();

                stream.Read(received, 0, received.Length);

                List<string> elem = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
                listBox1.DataSource = elem;

                lblIP.Text = $"Connesso a : {txtIpServer.Text}";
                lblIP.Text = $"Nope : {txtIpServer.Text}";


                //test
            }
            else
            {
                txtIpServer.WithError = true;
                lblErroreIP.Text = "Indirizzo IP non valido";
            }

        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            toDownload = listBox1.GetItemText(listBox1.SelectedItem);
            string send = "download;" + toDownload;
            toSend = encoding.GetBytes(send);
            toSend = TrimEnd(toSend);
            stream.Write(toSend, 0, toSend.Length);
            toSend = new Byte[400000];
            ReciveFile();
        }
        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        private void ReciveFile()
        {
            received = new Byte[400000];
            toSend = new Byte[400000];
            stream.Read(received, 0, received.Length);
            byte[] toSave = TrimEnd(received);
            saveFile();
        }
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
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
            string send = "upload;";
            foreach (string x in dropped)
            {
                send += x + ";" + File.ReadAllText(x);
                toSend = encoding.GetBytes(send);
                toSend = TrimEnd(toSend);
                stream.Write(toSend, 0, toSend.Length);
                toSend = new byte[400000];
                if (checkError())
                {
                    break;
                }
            }
        }
        private bool checkError()
        {
            received = new Byte[400000];
            stream.Read(received, 0, received.Length);
            string msg = encoding.GetString(received);
            if (msg.Contains("Errore"))
            {
                MessageBox.Show(msg);
                return true;
            }
            else
            {
                received = TrimEnd(received);
                string rec = encoding.GetString(received);
                List<string> elem = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
                listBox1.DataSource = elem;
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

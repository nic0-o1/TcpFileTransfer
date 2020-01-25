//Rosati-Nicolò Client-TCP file transfer
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TcpFileTransfer
{
    public partial class Client : MetroFramework.Forms.MetroForm
    {
        private enum Status { Online, Offline }

        /// <summary>
        /// Initialize the client fields
        /// </summary>
        public Client()
        {
            InitializeComponent();
            ToggleFields(Status.Offline);
            CheckForIllegalCrossThreadCalls = false;
            SizeToUpload = toSend.Length;
        }

        private long SizeToUpload;  //Represents the maximum size of files that can be uploaded at a time

        private TcpClient server;
        private NetworkStream stream;

        private Byte[] received = new Byte[1000000];
        private Byte[] toSend = new Byte[1000000];

        private readonly Encoding encoding = Encoding.GetEncoding("Windows-1252");

        private string toDownload;

        /// <summary>
        /// Set field's properties based on the given status
        /// </summary>
        /// <param name="s">Given status</param>
        /// <exception cref="InvalidOperationException">Thrown when can't find a given status</exception>
        private void ToggleFields(Status s)
        {
            switch (s)
            {
                case Status.Online:
                    {
                        txtIpServer.Clear();
                        btnDisconnect.Enabled = true;
                        lstBoxFile.Enabled = true;
                        btnUpload.Enabled = true;
                        picReload.Enabled = true;
                        btnConnect.Enabled = false;
                        btnUpload.ForeColor = Color.White;
                        btnConnect.BackColor = Color.FromArgb(230, 230, 230);
                        btnDisconnect.BackColor = Color.FromArgb(255, 128, 128);
                        btnUpload.BackColor = SystemColors.MenuHighlight;
                        break;
                    }
                case Status.Offline:
                    {
                        btnDisconnect.Enabled = false;
                        btnUpload.Enabled = false;
                        lstBoxFile.Enabled = false;
                        picReload.Enabled = false;
                        btnConnect.Enabled = true;
                        btnUpload.BackColor = Color.FromArgb(230, 230, 230);
                        btnConnect.BackColor = Color.FromArgb(128, 255, 128);
                        btnDisconnect.BackColor = Color.FromArgb(230, 230, 230);
                        btnUpload.ForeColor = SystemColors.ControlText;
                        break;
                    }
                default:
                    throw new InvalidOperationException("Status not found");
            }
        }

        /// <summary>
        /// Check if an ip address is correct
        /// </summary>
        /// <param name="toCheck">ip to check</param>
        /// <exception cref="ArgumentException">Thrown when the ip address is not correct</exception>
        private void CheckIP(string toCheck)
        {
            if (!IPAddress.TryParse(toCheck, out _))
            {
                throw new ArgumentException("Indirizzo IP non valido");
            }
        }

        /// <summary>
        /// Connect to the TCP server
        /// </summary>
        /// <param name="ip">Server IP</param>
        /// <param name="port">Server's port default 55000</param>
        private void ConnectToServer(string ip, int port = 55000)
        {
            server = new TcpClient(ip, port);
        }

        /// <summary>
        /// Allows to manage server's connection
        /// </summary>
        private void ManageServer()
        {
            try
            {
                CheckIP(txtIpServer.Text);

                ConnectToServer(txtIpServer.Text);

                stream = server.GetStream();

                ReceiveDirectory();

                lblIP.Text = $"Connesso a : {txtIpServer.Text}";

                ToggleFields(Status.Online);
            }

            catch (ArgumentException) { lblErroreIP.Visible = true; txtIpServer.WithError = true; lblErroreIP.Text = "Indirizzo IP non valido"; }

            catch (SocketException) { txtIpServer.Clear(); MetroFramework.MetroMessageBox.Show(this, "\n\nImpossible contattare il server all'indirizzo: " + txtIpServer.Text, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            catch (Exception ex) { MetroFramework.MetroMessageBox.Show(this, "\n\n" + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// Receive from the server the updated shared folder
        /// </summary>
        private void ReceiveDirectory()
        {
            Array.Clear(received, 0, received.Length);
            stream.Read(received, 0, received.Length);

            TrimEnd(received);

            lstBoxFile.DataSource = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ManageServer))
            {
                IsBackground = true
            };
            lblErroreIP.Visible = false;
            t.Start();
        }

        private void lstBoxFile_MouseClick(object sender, MouseEventArgs e)
        {
            toSend = new Byte[1000000];
            toDownload = lstBoxFile.GetItemText(lstBoxFile.SelectedItem);

            string send = "download;£&" + toDownload + ";£&";

            toSend = encoding.GetBytes(send);

            toSend = TrimEnd(toSend);

            stream.Write(toSend, 0, toSend.Length);

            toSend = new Byte[1000000];

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
            received = new Byte[1000000];
            toSend = new Byte[1000000];
            stream.Read(received, 0, received.Length);
            byte[] toSave = TrimEnd(received);
            SaveFile();
        }

        /// <summary>
        /// Saves the received file from the server
        /// </summary>
        private void SaveFile()
        {
            string[] fileName = toDownload.Split('\\');
            string[] fileInfo = fileName[fileName.Length - 1].Split('.');

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Seleziona cartella di destinazione";
                sfd.Filter = "Tutti i file (*.*)|*.*";
                sfd.FileName = fileInfo[0];
                sfd.DefaultExt = fileInfo[1];

                DialogResult result = sfd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(sfd.FileName))
                {
                    byte[] toSave = TrimEnd(received);
                    File.WriteAllBytes(sfd.FileName, toSave);
                }
            }
        }

        private void lstFileToUpload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Move;
                lstFileToUpload.BackColor = Color.FromArgb(230, 230, 230);
            }
        }

        private readonly List<string> dropped = new List<string>();
        private void lstFileToUpload_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (new FileInfo(items[0]).Length > SizeToUpload)
            {
                MetroFramework.MetroMessageBox.Show(this, "\n\nSuperato il limite di upload", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SizeToUpload -= new FileInfo(items[0]).Length;
                dropped.AddRange(items.ToList());
                showFileToUpload();
            }
            lstFileToUpload.BackColor = Color.White;
        }

        private void lstFileToUpload_DragLeave(object sender, EventArgs e)
        {
            lstFileToUpload.BackColor = Color.White;
        }

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



        private void btnUpload_Click(object sender, EventArgs e)
        {
            toSend = new byte[1000000];
            byte[] temp = new byte[1000000];
            string send = "upload;£&";
            foreach (string x in dropped)
            {
                send += x + ";£&";
                temp = FileToByteArray(x);


                toSend = encoding.GetBytes(send);

                //System.Buffer.BlockCopy(temp, 0, toSend, 0, temp.Length);

                byte[] rv = toSend.Concat(temp).ToArray();

                toSend = TrimEnd(rv);
                stream.Write(rv, 0, rv.Length);

                toSend = new byte[1000000];
                if (CheckForErrors())
                {
                    break;
                }
                send = "upload;£&";
            }
            dropped.Clear();
            lstFileToUpload.Items.Clear();
            SizeToUpload = toSend.Length;
        }
        /// <summary>
        /// Update the ListView control with the dropped files
        /// </summary>
        private void showFileToUpload()
        {
            lstFileToUpload.Items.Clear();
            foreach (string data in dropped)
            {
                lstFileToUpload.Items.Add(new ListViewItem { Text = Path.GetFileName(data), ImageIndex = 0 });
            }
        }

        /// <summary>
        /// Check if the server returned an error
        /// </summary>
        private bool CheckForErrors()
        {
            received = new Byte[1000000];
            stream.Read(received, 0, received.Length);
            received = TrimEnd(received);
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
                lstBoxFile.DataSource = JsonConvert.DeserializeObject<List<string>>(encoding.GetString(received));
                return false;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (server.Connected && MetroFramework.MetroMessageBox.Show(this, "\n\nInterrompere la connessione al server ?", "Server connesso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                server.Client.Close();
                lblIP.Text = String.Empty;
                ToggleFields(Status.Offline);
            }
        }

        private void lstFileToUpload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFileToUpload.SelectedItems.Count > 0)
            {
                lstFileToUpload.SelectedItems[0].BackColor = Color.Transparent; //use the color of your background of the listView
            }
        }

        private void lstFileToUpload_MouseClick(object sender, MouseEventArgs e)
        {
            dropped.RemoveAt(lstFileToUpload.SelectedIndices[0]);
            showFileToUpload();
        }

        private void picReload_Click(object sender, EventArgs e)
        {
            Array.Clear(toSend, 0, toSend.Length);
            toSend = encoding.GetBytes("Directory;£&");
            stream.Write(toSend, 0, toSend.Length);

            ReceiveDirectory();
        }
    }
}

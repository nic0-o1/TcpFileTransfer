
//Rosati-Nicolò Server-TCP file transfer
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// File transfer over TCP.
    /// </summary>
    public partial class FormServer : MetroFramework.Forms.MetroForm
    {
        private enum Status { Offline, Online, DragIn, DragOut };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int PORT = 55000;

        /// <summary>
        /// Initialize the server components
        /// </summary>
        public FormServer()
        {
            InitializeComponent();
            ToggleFields(Status.Offline);
            CheckForIllegalCrossThreadCalls = false;
            lblIP.Text = "Indirizzo IP: " + GetIP();
            log4net.Config.XmlConfigurator.Configure();
            btnStart.BackColor = Color.FromArgb(230, 230, 230);
            btnStart.Enabled = false;

        }

        private TcpListenerEx server;
        private string selectedPath;
        private string currentPath;

        /// <summary>
        /// Set field's properties based on the given status
        /// </summary>
        /// <param name="s">Status</param>
        private void ToggleFields(Status s)
        {
            switch (s)
            {
                case Status.Offline:
                    {
                        lblState.ForeColor = Color.Red;
                        lblState.Text = "Offline";
                        picReload.Enabled = false;
                        btnClose.Enabled = false;
                        btnChooseDir.Enabled = true;
                        btnStart.Enabled = true;
                        btnClose.BackColor = Color.FromArgb(230, 230, 230);
                        btnStart.BackColor = Color.FromArgb(128, 255, 128);
                        break;
                    }
                case Status.Online:
                    {
                        log.Info("Server Avviato");
                        lblState.ForeColor = Color.Green;
                        lblState.Text = "Online";
                        btnStart.Enabled = false;
                        btnChooseDir.Enabled = false;
                        btnClose.Enabled = true;
                        picReload.Enabled = true;
                        btnStart.BackColor = Color.FromArgb(230, 230, 230);
                        btnClose.BackColor = Color.FromArgb(255, 128, 128);
                        break;
                    }
                case Status.DragIn:
                    {
                        lstViewFiles.BackColor = Color.FromArgb(230, 230, 230);
                        lblDrag.BackColor = Color.FromArgb(230, 230, 230);
                        lblDrag.Visible = true;
                        break;
                    }
                case Status.DragOut:
                    {
                        lstViewFiles.BackColor = SystemColors.Window;
                        lblDrag.Visible = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// Get the computer's local IP
        /// </summary>
        /// <returns>IP string</returns>
        private string GetIP()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                GatewayIPAddressInformation addr = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
                if (addr != null && !addr.Address.ToString().Equals("0.0.0.0"))
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Gives the content of a directory with files and subdirectories.
        /// Removes the path before the actual directory
        /// </summary>
        /// <param name="path">Path of the selected directory</param>
        /// <returns>JSON with the content of the directories</returns>
        private string InitializeDirectory(string path)
        {
            List<string> files = Directory.GetFiles(path, "*.*").ToList();

            files = files.Select(x => x.Replace(path, string.Empty)).ToList(); //rimuove la parte del percorso prima della cartella
            files = files.Select(p => (!string.IsNullOrEmpty(p) && p.Length > 1) ? p.Substring(1) : p).ToList();  //rimouove il primo \

            return JsonConvert.SerializeObject(files);
        }

        /// <summary>
        /// Starts the TCP server
        /// </summary>
        /// <param name="portObj"> port object</param>
        /// <exception cref="SocketException"></exception>
        private void StartServer(object portObj)
        {
            try
            {
                int port = Convert.ToInt32(portObj);
                server = new TcpListenerEx(IPAddress.Parse(GetIP()), port);
                server.Start();
                ToggleFields(Status.Online);

                while (server.Active)
                {
                    TcpClient client = server.AcceptTcpClient();
                    ClientManager clientManager = new ClientManager(client);
                    clientManager.ClientEvent += ClientManager_ClientEvent;

                    Thread clientThread = new Thread(new ThreadStart(clientManager.ManageConnection))
                    {
                        IsBackground = true
                    };

                    clientThread.Start();
                }
            }
            catch { }
        }

        private void ClientManager_ClientEvent(object sender, Models.ClientEventArgs e)
        {

            lstBoxLog.DataSource = null;
            lstBoxLog.DataSource = e.Logs;


        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ParameterizedThreadStart(StartServer))
            {
                IsBackground = true
            };
            t.Start(PORT);
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ClientManager.sharedDir = InitializeDirectory(fbd.SelectedPath);
                    ClientManager.selectedPath = fbd.SelectedPath;
                    selectedPath = fbd.SelectedPath;
                    lblPath.Text = selectedPath;
                    lblPath.Visible = true;
                    btnStart.Enabled = true;
                    PopulateTreeView();
                    btnStart.BackColor = Color.FromArgb(128, 255, 128);
                }
            }
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (server.Active && MetroFramework.MetroMessageBox.Show(this, "\n\nServer attivo. Uscire ?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    server.Stop();
                }
            }
            catch { }
        }

        private void canUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (MetroUploadToggle.Checked)
            {
                if (MetroFramework.MetroMessageBox.Show(this, "\n\nSi sta per attivare l'upload. Continuare ?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ClientManager.canUpload = true;
                }
                else
                {
                    MetroUploadToggle.Checked = false;
                }
            }
            else
            {
                ClientManager.canUpload = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (server.Active && MetroFramework.MetroMessageBox.Show(this, "\n\nProcedere con lo spegnimento del server ?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                server.Stop();
                ToggleFields(Status.Offline);
            }
        }

        private void picReload_Click(object sender, EventArgs e)
        {
            UpdateFileExplorer(lastSelected);
        }

        #region TreeWiew and ListView management

        /// <summary>
        /// Populates the treeview based on the selected directory
        /// </summary>
        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(selectedPath);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name)
                {
                    Tag = info
                };
                treeView1.Nodes.Add(rootNode);
            }
        }

        /// <summary>
        /// Update the listview with the content of the selected directory
        /// </summary>
        /// <param name="newSelected">The selected node</param>
        private void UpdateFileExplorer(TreeNode newSelected)
        {
            lstViewFiles.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            currentPath = nodeDirInfo.FullName;
            lblPath.Text = currentPath;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item;
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
             new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                lstViewFiles.Items.Add(item);
            }

            lstViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private TreeNode lastSelected;
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            lastSelected = newSelected;
            UpdateFileExplorer(newSelected);

        }

        #region ListView DragAndDrop
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            //As soon as you release the file will be copied
            if (server.Active)
            {
                string[] dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
                string[] val = dropped[0].Split('\\');
                File.Copy(dropped[0], Path.Combine(currentPath, val[val.Length - 1]));
                UpdateFileExplorer(lastSelected);
            }
            ToggleFields(Status.DragOut);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Move;
                ToggleFields(Status.DragIn);
            }
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            ToggleFields(Status.DragOut);
        }
        #endregion

        #endregion

    }
}

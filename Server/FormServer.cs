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
    public partial class FormServer : MetroFramework.Forms.MetroForm
    {
        public FormServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            log4net.Config.XmlConfigurator.Configure();

        }
        private string sharedDir;
        private TcpListenerEx server;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int PORT = 55000;
        private string selectedPath;
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
        private string InitializeDirectory(string path)
        {
            List<string> files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();

            files = files.Select(x => x.Replace(path, string.Empty)).ToList();
            files = files.Select(p => (!string.IsNullOrEmpty(p) && p.Length > 1) ? p.Substring(1) : p).ToList();

            return JsonConvert.SerializeObject(files);
        }
        private void StartServer(object portObj)
        {
            int port = Convert.ToInt32(portObj);
            server = new TcpListenerEx(IPAddress.Parse(GetIP()), port);
            server.Start();
            log.Info("server avviato");
            Debug.WriteLine(GetIP());
            lblStatus.Text = "server avviato";

            while (server.Active)
            {
                TcpClient client = server.AcceptTcpClient();

                ClientManager clientManager = new ClientManager(client);
                Thread clientThread = new Thread(new ThreadStart(clientManager.ManageConnection))
                {
                    IsBackground = true
                };
                clientThread.Start();

            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ParameterizedThreadStart(StartServer));
            t.IsBackground = true;
            t.Start(PORT);
            btnStart.Enabled = false;
            btnClose.Enabled = true;
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    sharedDir = InitializeDirectory(fbd.SelectedPath);
                    ClientManager.sharedDir = sharedDir;
                    ClientManager.selectedPath = fbd.SelectedPath;
                    selectedPath = fbd.SelectedPath;
                    lblPath.Text = selectedPath;
                    PopulateTreeView();
                    btnStart.Enabled = true;
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
            }
            else
            {
                ClientManager.canUpload = false;
            }
        }
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
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs,
            TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0)
                {
                    Tag = subDir,
                    ImageKey = "folder"
                };
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            lstViewFiles.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            lblPath.Text = nodeDirInfo.FullName;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"),
             new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                lstViewFiles.Items.Add(item);
            }
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

        private string[] dropped;
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            string[] val = dropped[0].Split('\\');
            string newPath = selectedPath + "\\" + val[val.Length - 1];
            File.Copy(dropped[0], newPath);
            //groupBox1.BackgroundImage = null;
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Move;
                //listView1.BackgroundImage = Server.Properties.Resources.drag;
            }
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            //listView1.BackgroundImage = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (server.Active && MetroFramework.MetroMessageBox.Show(this, "\n\nprocedere con lo spegnimento del server ?", "Attenzione", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                server.Stop();
                btnClose.Enabled = false;
                btnStart.Enabled = true;
            }
        }

        private void picReload_Click(object sender, EventArgs e)
        {
            PopulateTreeView();
        }
    }
}

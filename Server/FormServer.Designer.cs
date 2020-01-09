namespace Server
{
    partial class FormServer
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServer));
            this.btnStart = new MetroFramework.Controls.MetroTile();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.MetroUploadToggle = new MetroFramework.Controls.MetroToggle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lstViewFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblPath = new MetroFramework.Controls.MetroLabel();
            this.btnClose = new MetroFramework.Controls.MetroTile();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnChooseDir = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.picReload = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.ActiveControl = null;
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(23, 272);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(109, 46);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Avvia Server";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStart.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnStart.UseCustomBackColor = true;
            this.btnStart.UseCustomForeColor = true;
            this.btnStart.UseSelectable = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 178);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(52, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Upload";
            // 
            // MetroUploadToggle
            // 
            this.MetroUploadToggle.AutoSize = true;
            this.MetroUploadToggle.Location = new System.Drawing.Point(23, 213);
            this.MetroUploadToggle.Name = "MetroUploadToggle";
            this.MetroUploadToggle.Size = new System.Drawing.Size(80, 17);
            this.MetroUploadToggle.Style = MetroFramework.MetroColorStyle.Green;
            this.MetroUploadToggle.TabIndex = 10;
            this.MetroUploadToggle.Text = "Off";
            this.MetroUploadToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MetroUploadToggle.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.MetroUploadToggle.UseCustomBackColor = true;
            this.MetroUploadToggle.UseCustomForeColor = true;
            this.MetroUploadToggle.UseSelectable = true;
            this.MetroUploadToggle.CheckedChanged += new System.EventHandler(this.canUpload_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(500, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstViewFiles);
            this.splitContainer1.Size = new System.Drawing.Size(577, 531);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 11;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(192, 531);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // lstViewFiles
            // 
            this.lstViewFiles.AllowDrop = true;
            this.lstViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewFiles.HideSelection = false;
            this.lstViewFiles.Location = new System.Drawing.Point(0, 0);
            this.lstViewFiles.Name = "lstViewFiles";
            this.lstViewFiles.Size = new System.Drawing.Size(381, 531);
            this.lstViewFiles.SmallImageList = this.imageList1;
            this.lstViewFiles.TabIndex = 0;
            this.lstViewFiles.UseCompatibleStateImageBehavior = false;
            this.lstViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome ";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tipo";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ultima modifica";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(531, 30);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(83, 19);
            this.lblPath.TabIndex = 12;
            this.lblPath.Text = "metroLabel2";
            // 
            // btnClose
            // 
            this.btnClose.ActiveControl = null;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(23, 338);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 46);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Chiudi Server";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnClose.UseCustomBackColor = true;
            this.btnClose.UseCustomForeColor = true;
            this.btnClose.UseSelectable = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnChooseDir
            // 
            this.btnChooseDir.Location = new System.Drawing.Point(23, 124);
            this.btnChooseDir.Name = "btnChooseDir";
            this.btnChooseDir.Size = new System.Drawing.Size(89, 26);
            this.btnChooseDir.TabIndex = 14;
            this.btnChooseDir.Text = "Scegli";
            this.btnChooseDir.UseSelectable = true;
            this.btnChooseDir.Click += new System.EventHandler(this.btnChooseDir_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 81);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(151, 19);
            this.metroLabel2.TabIndex = 15;
            this.metroLabel2.Text = "Cartella condivisione file";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(165, 299);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 19);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "metroLabel3";
            // 
            // picReload
            // 
            this.picReload.BackgroundImage = global::Server.Properties.Resources.reload_;
            this.picReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picReload.Location = new System.Drawing.Point(500, 30);
            this.picReload.Name = "picReload";
            this.picReload.Size = new System.Drawing.Size(25, 19);
            this.picReload.TabIndex = 17;
            this.picReload.TabStop = false;
            this.picReload.Click += new System.EventHandler(this.picReload_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 628);
            this.Controls.Add(this.picReload);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.btnChooseDir);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MetroUploadToggle);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.Name = "FormServer";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "TCP HFS Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormServer_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTile btnStart;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroToggle MetroUploadToggle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lstViewFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MetroFramework.Controls.MetroLabel lblPath;
        private MetroFramework.Controls.MetroTile btnClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MetroFramework.Controls.MetroButton btnChooseDir;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private System.Windows.Forms.PictureBox picReload;
    }
}


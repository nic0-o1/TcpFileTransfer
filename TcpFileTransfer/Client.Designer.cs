namespace TcpFileTransfer
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtIpServer = new MetroFramework.Controls.MetroTextBox();
            this.btnDisconnect = new MetroFramework.Controls.MetroTile();
            this.btnConnect = new MetroFramework.Controls.MetroTile();
            this.btnUpload = new MetroFramework.Controls.MetroTile();
            this.lblErroreIP = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.lstBoxFile = new System.Windows.Forms.ListBox();
            this.lstFileToUpload = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.picReload = new System.Windows.Forms.PictureBox();
            this.lblIP = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(23, 73);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(129, 25);
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Indirizzo Server";
            // 
            // txtIpServer
            // 
            // 
            // 
            // 
            this.txtIpServer.CustomButton.Image = null;
            this.txtIpServer.CustomButton.Location = new System.Drawing.Point(292, 1);
            this.txtIpServer.CustomButton.Name = "";
            this.txtIpServer.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtIpServer.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtIpServer.CustomButton.TabIndex = 1;
            this.txtIpServer.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtIpServer.CustomButton.UseSelectable = true;
            this.txtIpServer.CustomButton.Visible = false;
            this.txtIpServer.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtIpServer.Lines = new string[0];
            this.txtIpServer.Location = new System.Drawing.Point(24, 115);
            this.txtIpServer.MaxLength = 32767;
            this.txtIpServer.Name = "txtIpServer";
            this.txtIpServer.PasswordChar = '\0';
            this.txtIpServer.PromptText = "ip server";
            this.txtIpServer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIpServer.SelectedText = "";
            this.txtIpServer.SelectionLength = 0;
            this.txtIpServer.SelectionStart = 0;
            this.txtIpServer.ShortcutsEnabled = true;
            this.txtIpServer.Size = new System.Drawing.Size(314, 23);
            this.txtIpServer.TabIndex = 16;
            this.txtIpServer.UseSelectable = true;
            this.txtIpServer.WaterMark = "ip server";
            this.txtIpServer.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtIpServer.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.ActiveControl = null;
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(23, 235);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(109, 46);
            this.btnDisconnect.TabIndex = 18;
            this.btnDisconnect.Text = "Disconnetti";
            this.btnDisconnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDisconnect.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnDisconnect.UseCustomBackColor = true;
            this.btnDisconnect.UseCustomForeColor = true;
            this.btnDisconnect.UseSelectable = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.ActiveControl = null;
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConnect.Location = new System.Drawing.Point(23, 169);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(109, 46);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "Connetti";
            this.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConnect.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnConnect.UseCustomBackColor = true;
            this.btnConnect.UseCustomForeColor = true;
            this.btnConnect.UseSelectable = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.ActiveControl = null;
            this.btnUpload.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(438, 438);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(111, 42);
            this.btnUpload.TabIndex = 19;
            this.btnUpload.Text = "Carica";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpload.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnUpload.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnUpload.UseCustomBackColor = true;
            this.btnUpload.UseCustomForeColor = true;
            this.btnUpload.UseSelectable = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblErroreIP
            // 
            this.lblErroreIP.AutoSize = true;
            this.lblErroreIP.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblErroreIP.ForeColor = System.Drawing.Color.Red;
            this.lblErroreIP.Location = new System.Drawing.Point(158, 83);
            this.lblErroreIP.Name = "lblErroreIP";
            this.lblErroreIP.Size = new System.Drawing.Size(0, 0);
            this.lblErroreIP.TabIndex = 21;
            this.lblErroreIP.UseCustomBackColor = true;
            this.lblErroreIP.UseCustomForeColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(658, 73);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(91, 25);
            this.metroLabel2.TabIndex = 22;
            this.metroLabel2.Text = "Elenco file";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(422, 73);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(127, 25);
            this.metroLabel3.TabIndex = 24;
            this.metroLabel3.Text = "File da caricare";
            // 
            // lstBoxFile
            // 
            this.lstBoxFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxFile.FormattingEnabled = true;
            this.lstBoxFile.ItemHeight = 20;
            this.lstBoxFile.Location = new System.Drawing.Point(627, 115);
            this.lstBoxFile.Name = "lstBoxFile";
            this.lstBoxFile.Size = new System.Drawing.Size(412, 304);
            this.lstBoxFile.TabIndex = 0;
            this.lstBoxFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBoxFile_MouseClick);
            // 
            // lstFileToUpload
            // 
            this.lstFileToUpload.AllowDrop = true;
            this.lstFileToUpload.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstFileToUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFileToUpload.HideSelection = false;
            this.lstFileToUpload.Location = new System.Drawing.Point(422, 115);
            this.lstFileToUpload.Name = "lstFileToUpload";
            this.lstFileToUpload.Size = new System.Drawing.Size(205, 304);
            this.lstFileToUpload.SmallImageList = this.imageList1;
            this.lstFileToUpload.TabIndex = 26;
            this.lstFileToUpload.UseCompatibleStateImageBehavior = false;
            this.lstFileToUpload.View = System.Windows.Forms.View.Details;
            this.lstFileToUpload.SelectedIndexChanged += new System.EventHandler(this.lstFileToUpload_SelectedIndexChanged);
            this.lstFileToUpload.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFileToUpload_DragDrop);
            this.lstFileToUpload.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFileToUpload_DragEnter);
            this.lstFileToUpload.DragLeave += new System.EventHandler(this.lstFileToUpload_DragLeave);
            this.lstFileToUpload.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstFileToUpload_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome file";
            this.columnHeader1.Width = 200;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "delete.png");
            // 
            // picReload
            // 
            this.picReload.BackgroundImage = global::TcpFileTransfer.Properties.Resources.reload_;
            this.picReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picReload.Cursor = System.Windows.Forms.Cursors.Default;
            this.picReload.Location = new System.Drawing.Point(627, 79);
            this.picReload.Name = "picReload";
            this.picReload.Size = new System.Drawing.Size(25, 19);
            this.picReload.TabIndex = 27;
            this.picReload.TabStop = false;
            this.picReload.Click += new System.EventHandler(this.picReload_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(3, 601);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(83, 19);
            this.lblIP.TabIndex = 28;
            this.lblIP.Text = "metroLabel4";
            this.lblIP.Visible = false;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 628);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.picReload);
            this.Controls.Add(this.lstFileToUpload);
            this.Controls.Add(this.lstBoxFile);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.lblErroreIP);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIpServer);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.Name = "Client";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "TCP Client";
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtIpServer;
        private MetroFramework.Controls.MetroTile btnDisconnect;
        private MetroFramework.Controls.MetroTile btnConnect;
        private MetroFramework.Controls.MetroTile btnUpload;
        private MetroFramework.Controls.MetroLabel lblErroreIP;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.ListBox lstBoxFile;
        private System.Windows.Forms.ListView lstFileToUpload;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox picReload;
        private MetroFramework.Controls.MetroLabel lblIP;
    }
}


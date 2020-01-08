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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtIpServer = new MetroFramework.Controls.MetroTextBox();
            this.btnDisconnect = new MetroFramework.Controls.MetroTile();
            this.btnConnect = new MetroFramework.Controls.MetroTile();
            this.btnUpload = new MetroFramework.Controls.MetroTile();
            this.lblIP = new MetroFramework.Controls.MetroLabel();
            this.lblErroreIP = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(23, 353);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(431, 238);
            this.listBox1.TabIndex = 12;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox1.Location = new System.Drawing.Point(650, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 433);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.groupBox1_DragDrop);
            this.groupBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.groupBox1_DragEnter);
            this.groupBox1.DragLeave += new System.EventHandler(this.groupBox1_DragLeave);
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
            this.btnUpload.Location = new System.Drawing.Point(613, 525);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(111, 42);
            this.btnUpload.TabIndex = 19;
            this.btnUpload.Text = "Carica";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpload.UseSelectable = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(24, 309);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(83, 19);
            this.lblIP.TabIndex = 20;
            this.lblIP.Text = "metroLabel2";
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
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 679);
            this.Controls.Add(this.lblErroreIP);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIpServer);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox1);
            this.MaximizeBox = false;
            this.Name = "Client";
            this.Text = "HFS TCP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtIpServer;
        private MetroFramework.Controls.MetroTile btnDisconnect;
        private MetroFramework.Controls.MetroTile btnConnect;
        private MetroFramework.Controls.MetroTile btnUpload;
        private MetroFramework.Controls.MetroLabel lblIP;
        private MetroFramework.Controls.MetroLabel lblErroreIP;
    }
}


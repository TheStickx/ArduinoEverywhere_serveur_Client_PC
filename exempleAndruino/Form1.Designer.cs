

namespace exempleAndruino
{
    partial class Form1
    {


        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TopNouvelOrdre = new System.Windows.Forms.Timer(this.components);
            this.RtspStart = new System.Windows.Forms.Button();
            this.RtspStop = new System.Windows.Forms.Button();
            this.Options = new System.Windows.Forms.TabControl();
            this.Serveur = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.ButtonDeconnect = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.AdressBox = new System.Windows.Forms.TextBox();
            this.Video = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NomDuFlux = new System.Windows.Forms.TextBox();
            this.textBoxRtspurl = new System.Windows.Forms.TextBox();
            this.MsgTest = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TexteRecu = new System.Windows.Forms.TextBox();
            this.Envoyer = new System.Windows.Forms.Button();
            this.TextAEnvoyer = new System.Windows.Forms.TextBox();
            this.Erreures = new System.Windows.Forms.TabPage();
            this.labelError = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.VLC_View = new AxAXVLC.AxVLCPlugin2();
            this.PasswTextBox = new System.Windows.Forms.TextBox();
            this.labelPassw = new System.Windows.Forms.Label();
            this.Options.SuspendLayout();
            this.Serveur.SuspendLayout();
            this.Video.SuspendLayout();
            this.MsgTest.SuspendLayout();
            this.panel3.SuspendLayout();
            this.Erreures.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VLC_View)).BeginInit();
            this.SuspendLayout();
            // 
            // TopNouvelOrdre
            // 
            this.TopNouvelOrdre.Interval = 300;
            this.TopNouvelOrdre.Tick += new System.EventHandler(this.TickRelanceOrdre);
            // 
            // RtspStart
            // 
            this.RtspStart.Location = new System.Drawing.Point(514, 15);
            this.RtspStart.Name = "RtspStart";
            this.RtspStart.Size = new System.Drawing.Size(96, 22);
            this.RtspStart.TabIndex = 8;
            this.RtspStart.Text = "video start";
            this.RtspStart.UseVisualStyleBackColor = true;
            this.RtspStart.Click += new System.EventHandler(this.RtspStart_Click);
            // 
            // RtspStop
            // 
            this.RtspStop.Location = new System.Drawing.Point(514, 41);
            this.RtspStop.Name = "RtspStop";
            this.RtspStop.Size = new System.Drawing.Size(96, 23);
            this.RtspStop.TabIndex = 10;
            this.RtspStop.Text = "video stop";
            this.RtspStop.UseVisualStyleBackColor = true;
            this.RtspStop.Click += new System.EventHandler(this.RtspStop_Click);
            // 
            // Options
            // 
            this.Options.Controls.Add(this.Serveur);
            this.Options.Controls.Add(this.Video);
            this.Options.Controls.Add(this.MsgTest);
            this.Options.Controls.Add(this.Erreures);
            this.Options.Dock = System.Windows.Forms.DockStyle.Top;
            this.Options.Location = new System.Drawing.Point(0, 0);
            this.Options.Name = "Options";
            this.Options.SelectedIndex = 0;
            this.Options.Size = new System.Drawing.Size(833, 108);
            this.Options.TabIndex = 14;
            // 
            // Serveur
            // 
            this.Serveur.Controls.Add(this.labelPassw);
            this.Serveur.Controls.Add(this.PasswTextBox);
            this.Serveur.Controls.Add(this.label2);
            this.Serveur.Controls.Add(this.label1);
            this.Serveur.Controls.Add(this.textBoxPort);
            this.Serveur.Controls.Add(this.ButtonDeconnect);
            this.Serveur.Controls.Add(this.ButtonStart);
            this.Serveur.Controls.Add(this.AdressBox);
            this.Serveur.Location = new System.Drawing.Point(4, 22);
            this.Serveur.Name = "Serveur";
            this.Serveur.Padding = new System.Windows.Forms.Padding(3);
            this.Serveur.Size = new System.Drawing.Size(825, 82);
            this.Serveur.TabIndex = 0;
            this.Serveur.Text = "serveur";
            this.Serveur.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "adresse";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(65, 42);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(46, 20);
            this.textBoxPort.TabIndex = 17;
            this.textBoxPort.Text = "13000";
            this.textBoxPort.TextChanged += new System.EventHandler(this.PortValide);
            // 
            // ButtonDeconnect
            // 
            this.ButtonDeconnect.Location = new System.Drawing.Point(309, 42);
            this.ButtonDeconnect.Name = "ButtonDeconnect";
            this.ButtonDeconnect.Size = new System.Drawing.Size(80, 23);
            this.ButtonDeconnect.TabIndex = 16;
            this.ButtonDeconnect.Text = "Déconnecter";
            this.ButtonDeconnect.UseVisualStyleBackColor = true;
            this.ButtonDeconnect.Click += new System.EventHandler(this.ButtonDeconnect_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(309, 13);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(80, 23);
            this.ButtonStart.TabIndex = 15;
            this.ButtonStart.Text = "Connecter";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // AdressBox
            // 
            this.AdressBox.Location = new System.Drawing.Point(65, 16);
            this.AdressBox.Name = "AdressBox";
            this.AdressBox.Size = new System.Drawing.Size(120, 20);
            this.AdressBox.TabIndex = 14;
            this.AdressBox.DoubleClick += new System.EventHandler(this.AdresseLocale);
            // 
            // Video
            // 
            this.Video.Controls.Add(this.label4);
            this.Video.Controls.Add(this.label3);
            this.Video.Controls.Add(this.NomDuFlux);
            this.Video.Controls.Add(this.RtspStop);
            this.Video.Controls.Add(this.textBoxRtspurl);
            this.Video.Controls.Add(this.RtspStart);
            this.Video.Location = new System.Drawing.Point(4, 22);
            this.Video.Name = "Video";
            this.Video.Padding = new System.Windows.Forms.Padding(3);
            this.Video.Size = new System.Drawing.Size(825, 82);
            this.Video.TabIndex = 1;
            this.Video.Text = "Video";
            this.Video.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Adresse à utiliser";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "adressedonnée par serveur";
            // 
            // NomDuFlux
            // 
            this.NomDuFlux.Location = new System.Drawing.Point(156, 15);
            this.NomDuFlux.Name = "NomDuFlux";
            this.NomDuFlux.ReadOnly = true;
            this.NomDuFlux.Size = new System.Drawing.Size(343, 20);
            this.NomDuFlux.TabIndex = 14;
            // 
            // textBoxRtspurl
            // 
            this.textBoxRtspurl.Location = new System.Drawing.Point(156, 41);
            this.textBoxRtspurl.Name = "textBoxRtspurl";
            this.textBoxRtspurl.Size = new System.Drawing.Size(343, 20);
            this.textBoxRtspurl.TabIndex = 13;
            this.textBoxRtspurl.Text = "rtsp://192.168.1.133:5540/ch0";
            // 
            // MsgTest
            // 
            this.MsgTest.Controls.Add(this.panel3);
            this.MsgTest.Controls.Add(this.Envoyer);
            this.MsgTest.Controls.Add(this.TextAEnvoyer);
            this.MsgTest.Location = new System.Drawing.Point(4, 22);
            this.MsgTest.Name = "MsgTest";
            this.MsgTest.Padding = new System.Windows.Forms.Padding(3);
            this.MsgTest.Size = new System.Drawing.Size(825, 82);
            this.MsgTest.TabIndex = 2;
            this.MsgTest.Text = "Messages Test";
            this.MsgTest.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TexteRecu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(819, 42);
            this.panel3.TabIndex = 8;
            // 
            // TexteRecu
            // 
            this.TexteRecu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexteRecu.Location = new System.Drawing.Point(0, 0);
            this.TexteRecu.Multiline = true;
            this.TexteRecu.Name = "TexteRecu";
            this.TexteRecu.Size = new System.Drawing.Size(819, 42);
            this.TexteRecu.TabIndex = 8;
            // 
            // Envoyer
            // 
            this.Envoyer.Location = new System.Drawing.Point(328, 11);
            this.Envoyer.Name = "Envoyer";
            this.Envoyer.Size = new System.Drawing.Size(75, 23);
            this.Envoyer.TabIndex = 5;
            this.Envoyer.Text = "Envoyer";
            this.Envoyer.UseVisualStyleBackColor = true;
            this.Envoyer.Click += new System.EventHandler(this.Envoyer_Click);
            // 
            // TextAEnvoyer
            // 
            this.TextAEnvoyer.Location = new System.Drawing.Point(8, 11);
            this.TextAEnvoyer.Name = "TextAEnvoyer";
            this.TextAEnvoyer.Size = new System.Drawing.Size(314, 20);
            this.TextAEnvoyer.TabIndex = 4;
            this.TextAEnvoyer.Text = "<Message cool>=<micheal jordan est mort>";
            // 
            // Erreures
            // 
            this.Erreures.Controls.Add(this.labelError);
            this.Erreures.Location = new System.Drawing.Point(4, 22);
            this.Erreures.Name = "Erreures";
            this.Erreures.Padding = new System.Windows.Forms.Padding(3);
            this.Erreures.Size = new System.Drawing.Size(825, 82);
            this.Erreures.TabIndex = 3;
            this.Erreures.Text = "Erreures";
            this.Erreures.UseVisualStyleBackColor = true;
            // 
            // labelError
            // 
            this.labelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelError.Location = new System.Drawing.Point(3, 3);
            this.labelError.Multiline = true;
            this.labelError.Name = "labelError";
            this.labelError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.labelError.Size = new System.Drawing.Size(819, 76);
            this.labelError.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Location = new System.Drawing.Point(15, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 16;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawJoy);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveJoyBegin);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveJoy);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveJoyEnd);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(133, 602);
            this.panel2.TabIndex = 17;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.VLC_View);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(133, 108);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(700, 602);
            this.panel4.TabIndex = 18;
            // 
            // VLC_View
            // 
            this.VLC_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VLC_View.Enabled = true;
            this.VLC_View.Location = new System.Drawing.Point(0, 0);
            this.VLC_View.Name = "VLC_View";
            this.VLC_View.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VLC_View.OcxState")));
            this.VLC_View.Size = new System.Drawing.Size(700, 602);
            this.VLC_View.TabIndex = 8;
            // 
            // PasswTextBox
            // 
            this.PasswTextBox.Location = new System.Drawing.Point(203, 42);
            this.PasswTextBox.Name = "PasswTextBox";
            this.PasswTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswTextBox.TabIndex = 20;
            // 
            // labelPassw
            // 
            this.labelPassw.AutoSize = true;
            this.labelPassw.Location = new System.Drawing.Point(126, 45);
            this.labelPassw.Name = "labelPassw";
            this.labelPassw.Size = new System.Drawing.Size(71, 13);
            this.labelPassw.TabIndex = 21;
            this.labelPassw.Text = "Mot de passe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 710);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Options);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveToReg);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Options.ResumeLayout(false);
            this.Serveur.ResumeLayout(false);
            this.Serveur.PerformLayout();
            this.Video.ResumeLayout(false);
            this.Video.PerformLayout();
            this.MsgTest.ResumeLayout(false);
            this.MsgTest.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.Erreures.ResumeLayout(false);
            this.Erreures.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VLC_View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TopNouvelOrdre;
        private System.Windows.Forms.Button RtspStart;
        private System.Windows.Forms.Button RtspStop;
        private System.Windows.Forms.TabControl Options;
        private System.Windows.Forms.TabPage Serveur;
        private System.Windows.Forms.Button ButtonDeconnect;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.TextBox AdressBox;
        private System.Windows.Forms.TabPage Video;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NomDuFlux;
        private System.Windows.Forms.TextBox textBoxRtspurl;
        private System.Windows.Forms.TabPage MsgTest;
        private System.Windows.Forms.Button Envoyer;
        private System.Windows.Forms.TextBox TextAEnvoyer;
        private System.Windows.Forms.TabPage Erreures;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TexteRecu;
        private System.Windows.Forms.TextBox labelError;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel4;
        private AxAXVLC.AxVLCPlugin2 VLC_View;
        private System.Windows.Forms.Label labelPassw;
        private System.Windows.Forms.TextBox PasswTextBox;
    }
}


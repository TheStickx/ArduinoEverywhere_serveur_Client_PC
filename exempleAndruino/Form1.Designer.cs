

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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.Envoyer = new System.Windows.Forms.Button();
            this.TexteRecu = new System.Windows.Forms.Label();
            this.TextAEnvoyer = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TopNouvelOrdre = new System.Windows.Forms.Timer(this.components);
            this.labelError = new System.Windows.Forms.Label();
            this.ButtonDeconnect = new System.Windows.Forms.Button();
            this.VLC_View = new AxAXVLC.AxVLCPlugin2();
            this.RtspStart = new System.Windows.Forms.Button();
            this.textBoxRtspurl = new System.Windows.Forms.TextBox();
            this.RtspStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VLC_View)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(44, 55);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 0;
            this.ButtonStart.Text = "Connecter";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // Envoyer
            // 
            this.Envoyer.Location = new System.Drawing.Point(44, 111);
            this.Envoyer.Name = "Envoyer";
            this.Envoyer.Size = new System.Drawing.Size(75, 23);
            this.Envoyer.TabIndex = 1;
            this.Envoyer.Text = "Envoyer";
            this.Envoyer.UseVisualStyleBackColor = true;
            this.Envoyer.Click += new System.EventHandler(this.Envoyer_Click);
            // 
            // TexteRecu
            // 
            this.TexteRecu.AutoSize = true;
            this.TexteRecu.Location = new System.Drawing.Point(41, 153);
            this.TexteRecu.Name = "TexteRecu";
            this.TexteRecu.Size = new System.Drawing.Size(108, 13);
            this.TexteRecu.TabIndex = 2;
            this.TexteRecu.Text = "pas encore connecté";
            // 
            // TextAEnvoyer
            // 
            this.TextAEnvoyer.Location = new System.Drawing.Point(146, 111);
            this.TextAEnvoyer.Name = "TextAEnvoyer";
            this.TextAEnvoyer.Size = new System.Drawing.Size(314, 20);
            this.TextAEnvoyer.TabIndex = 3;
            this.TextAEnvoyer.Text = "<Message cool>=<micheal jordan est mort>";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Location = new System.Drawing.Point(146, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawJoy);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveJoyBegin);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveJoy);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveJoyEnd);
            // 
            // TopNouvelOrdre
            // 
            this.TopNouvelOrdre.Interval = 300;
            this.TopNouvelOrdre.Tick += new System.EventHandler(this.TickRelanceOrdre);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(12, 365);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(35, 13);
            this.labelError.TabIndex = 5;
            this.labelError.Text = "label1";
            // 
            // ButtonDeconnect
            // 
            this.ButtonDeconnect.Location = new System.Drawing.Point(148, 55);
            this.ButtonDeconnect.Name = "ButtonDeconnect";
            this.ButtonDeconnect.Size = new System.Drawing.Size(80, 23);
            this.ButtonDeconnect.TabIndex = 6;
            this.ButtonDeconnect.Text = "Déconnecter";
            this.ButtonDeconnect.UseVisualStyleBackColor = true;
            this.ButtonDeconnect.Click += new System.EventHandler(this.ButtonDeconnect_Click);
            // 
            // VLC_View
            // 
            this.VLC_View.Enabled = true;
            this.VLC_View.Location = new System.Drawing.Point(294, 153);
            this.VLC_View.Name = "VLC_View";
            this.VLC_View.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VLC_View.OcxState")));
            this.VLC_View.Size = new System.Drawing.Size(503, 281);
            this.VLC_View.TabIndex = 7;
            // 
            // RtspStart
            // 
            this.RtspStart.Location = new System.Drawing.Point(476, 111);
            this.RtspStart.Name = "RtspStart";
            this.RtspStart.Size = new System.Drawing.Size(122, 22);
            this.RtspStart.TabIndex = 8;
            this.RtspStart.Text = "video start";
            this.RtspStart.UseVisualStyleBackColor = true;
            this.RtspStart.Click += new System.EventHandler(this.RtspStart_Click);
            // 
            // textBoxRtspurl
            // 
            this.textBoxRtspurl.Location = new System.Drawing.Point(319, 60);
            this.textBoxRtspurl.Name = "textBoxRtspurl";
            this.textBoxRtspurl.Size = new System.Drawing.Size(415, 20);
            this.textBoxRtspurl.TabIndex = 9;
            this.textBoxRtspurl.Text = "rtsp://192.168.1.133:5540/ch0";
            // 
            // RtspStop
            // 
            this.RtspStop.Location = new System.Drawing.Point(615, 111);
            this.RtspStop.Name = "RtspStop";
            this.RtspStop.Size = new System.Drawing.Size(96, 23);
            this.RtspStop.TabIndex = 10;
            this.RtspStop.Text = "video stop";
            this.RtspStop.UseVisualStyleBackColor = true;
            this.RtspStop.Click += new System.EventHandler(this.RtspStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 469);
            this.Controls.Add(this.RtspStop);
            this.Controls.Add(this.textBoxRtspurl);
            this.Controls.Add(this.RtspStart);
            this.Controls.Add(this.VLC_View);
            this.Controls.Add(this.ButtonDeconnect);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextAEnvoyer);
            this.Controls.Add(this.TexteRecu);
            this.Controls.Add(this.Envoyer);
            this.Controls.Add(this.ButtonStart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.VLC_View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button Envoyer;
        private System.Windows.Forms.Label TexteRecu;
        private System.Windows.Forms.TextBox TextAEnvoyer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer TopNouvelOrdre;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button ButtonDeconnect;
        private AxAXVLC.AxVLCPlugin2 VLC_View;
        private System.Windows.Forms.Button RtspStart;
        private System.Windows.Forms.TextBox textBoxRtspurl;
        private System.Windows.Forms.Button RtspStop;
    }
}


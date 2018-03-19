

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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.Envoyer = new System.Windows.Forms.Button();
            this.TexteRecu = new System.Windows.Forms.Label();
            this.TextAEnvoyer = new System.Windows.Forms.TextBox();
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
            this.TexteRecu.Location = new System.Drawing.Point(41, 192);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 469);
            this.Controls.Add(this.TextAEnvoyer);
            this.Controls.Add(this.TexteRecu);
            this.Controls.Add(this.Envoyer);
            this.Controls.Add(this.ButtonStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button Envoyer;
        private System.Windows.Forms.Label TexteRecu;
        private System.Windows.Forms.TextBox TextAEnvoyer;
    }
}


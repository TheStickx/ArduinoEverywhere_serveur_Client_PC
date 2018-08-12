using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

// it's required for reading/writing into the registry:
using Microsoft.Win32;

//----------------------------------------------------------------------------
// note importante : pour utiliser VLC il faut compiler en X86 
// voir one note 
//----------------------------------------------------------------------------
/*

        public delegate void ReceptionFluxRtspHandler(String sMessage);
        public ReceptionFluxRtspHandler ReceptionFluxRtsp;
 */


namespace exempleAndruino
{
    public partial class Form1 : Form
    {
        public AsynchronousClient PourReseau;

        public Form1()
        {
            InitializeComponent();

            PourReseau = new AsynchronousClient(this)
            {
                ReceptionMsgCool = new AsynchronousClient.ReceptionMsgCoolHandler(MAJTexteRecu),
                ReceptionMsgHexa = new AsynchronousClient.ReceptionMsgHexaHandler(MAJMessageHexa),
                ReceptionCapteur = new AsynchronousClient.ReceptionCapteurHandler(MAJMessageCapteurs),
                ReceptionDErreur = new AsynchronousClient.ReceptionDErreurHandler(AfficheLErreur),
                ReceptionFluxRtsp = new AsynchronousClient.ReceptionFluxRtspHandler(ReceptionFluxRtsp),
                port = 13000

            };

            LoadConf();
        }

        public void MAJTexteRecu(String sMessage)
        {
            TexteRecu.Text += sMessage + "\n";
        }

        private string CompleteMessage;
        private const int GaugeHistWidth = 100;
        private const int GaugeHistHeight = 100;
        private int[] HistoriqueGauge = new int[GaugeHistWidth];
        private int GaugeCurrentValue = 0;
        private Pen _oscilloPen = new Pen(Color.Red);

        public void MAJMessageHexa(String sMessage)
        {
            int taille = TexteRecu.Text.Length;
            taille = taille > 200 ? taille - 200 : 0;

            // TexteRecu.Text = TexteRecu.Text.Substring(taille) + "\n" + sMessage + " => " + PourReseau.HexToString(sMessage);

            int iDebutEtiquette, iDebutContenu, iFinContenu, iGaugeBrowse;
            String Etiquette, Contenu;

            // ajoutons ce qu'on viens de recevoir à ce qu'on a déjà.
            // voyons ensuite si on sait le traiter.
            // DonneesRecues += Reception;
            CompleteMessage += PourReseau.HexToString(sMessage);


            iDebutEtiquette = CompleteMessage.IndexOf("V");
            iDebutContenu = CompleteMessage.IndexOf(":");
            if (iDebutContenu >= 0) iFinContenu = CompleteMessage.IndexOf(">", iDebutContenu + 1);
            else iFinContenu = -1;
            // si on a pas d'étiquette complette ou de contenu complet.
            // on sort ce sera la prochaine fois
            while ((iDebutEtiquette >= 0) && (iDebutContenu > 0) && (iFinContenu > 0))
            {
                // on a une étiquette
                Etiquette = CompleteMessage.Substring(iDebutEtiquette + 1, iDebutContenu - iDebutEtiquette - 1);
                Contenu = CompleteMessage.Substring(iDebutContenu + 1, iFinContenu - iDebutContenu - 1);
                //On rétrécis le Contenu de ce qui est traité
                CompleteMessage = CompleteMessage.Substring(iFinContenu + 1);

                // Selon l'etiquette on fait un truc
                if (Etiquette == "1")
                {
                    /* private string CompleteMessage;
        private const int GaugeHistWidth= 100;
        private const int GaugeHistHeight = 100;
        private int[] historiqueGauge = new int[GaugeHistWidth];
        private int GaugeElementActuel = 0; */
                    TexteRecu.Text = TexteRecu.Text.Substring(taille) + " " + Contenu;

                    Bitmap bmp = new Bitmap(GaugeHistWidth, GaugeHistHeight); // déclare une image bitmap
                    Graphics g = Graphics.FromImage(bmp); // déclare un objet graphics attaché au bitmap

                    // dessine le fond
                    g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, GaugeHistWidth - 1, GaugeHistHeight - 1));
                    // met à jours le tableau
                    HistoriqueGauge[(GaugeHistWidth - 1) - GaugeCurrentValue] = Convert.ToInt32(Contenu) * GaugeHistHeight / 1024;
                    // élément suivant
                    GaugeCurrentValue = GaugeCurrentValue < GaugeHistWidth - 1 ? GaugeCurrentValue + 1 : 0;
                    // GaugeCurrentValue = GaugeCurrentValue > 0 ? GaugeCurrentValue - 1 : GaugeHistWidth - 1;


                    for (iGaugeBrowse = 0; iGaugeBrowse < GaugeHistWidth - 2; iGaugeBrowse++)
                    {
                        if (iGaugeBrowse > ( GaugeHistWidth - GaugeCurrentValue - 1 ))
                        {
                            g.DrawLine(_oscilloPen
                                      , iGaugeBrowse, GaugeHistHeight - HistoriqueGauge[(GaugeHistWidth - 1) * 2 - iGaugeBrowse - GaugeCurrentValue ] - 2 
                                      , iGaugeBrowse + 1, GaugeHistHeight - HistoriqueGauge[(GaugeHistWidth - 1) * 2 - iGaugeBrowse - GaugeCurrentValue - 1 ] - 2);
                        }
                        else
                        {
                            if (iGaugeBrowse == ( GaugeHistWidth - GaugeCurrentValue - 1 ))
                            {
                               g.DrawLine(_oscilloPen
                                          , iGaugeBrowse, GaugeHistHeight - HistoriqueGauge[GaugeHistWidth - 1 - iGaugeBrowse - GaugeCurrentValue] - 2
                                          , iGaugeBrowse + 1, GaugeHistHeight - HistoriqueGauge[(GaugeHistWidth - 1) * 2 - iGaugeBrowse - GaugeCurrentValue] - 2 );
                            }
                            else
                            {
                                g.DrawLine(_oscilloPen
                                          , iGaugeBrowse, GaugeHistHeight - HistoriqueGauge  [ GaugeHistWidth - 1 - iGaugeBrowse - GaugeCurrentValue] - 2
                                          , iGaugeBrowse + 1, GaugeHistHeight - HistoriqueGauge  [GaugeHistWidth - 2 - iGaugeBrowse - GaugeCurrentValue] - 2 );
                            }
                        }
                    }


                    g.Dispose(); // libère l'objet 
                    GraphGauge.Image = bmp; // affiche l'image
                }

                iDebutEtiquette = CompleteMessage.IndexOf("<");
                iDebutContenu = CompleteMessage.IndexOf(">=<");
                if (iDebutContenu >= 0) iFinContenu = CompleteMessage.IndexOf(">", iDebutContenu + 3);
            }

        }

        public void MAJMessageCapteurs(String sMessage)
        {
            TexteRecu.Text += sMessage + "\n";
        }

        public void AfficheLErreur(String sMessageErreur)
        {
            labelError.Text = sMessageErreur;
        }

        public void ReceptionFluxRtsp(String sFluxRtsp)
        {
            // lancement de flux rtsp
            int CacheTime;
            if (!Int32.TryParse(textBoxVideoCache.Text, out CacheTime))   // essaye de parser une string en int 
            {                                                             // (retourne la valeur par out dans CacheTime (ou autre variable) 
                textBoxVideoCache.Text = "600";                           // selon error code on peut faire un truc ou pas.
                CacheTime = 600;                                          // ici on remet une valeur correcte dans le textbox
            }
            NomDuFlux.Text = sFluxRtsp;
            VLC_View.playlist.stop();
            VLC_View.playlist.items.clear();

            System.Threading.Thread.Sleep(1000);

            VLC_View.playlist.add(textBoxRtspurl.Text, "mycam", ":network-caching=" + CacheTime);
            VLC_View.playlist.play();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            PourReseau.ipAddress = IPAddress.Parse(AdressBox.Text);
            // le port est déjà spécifié dans PortValide
            PourReseau.StartClient();
            TopNouvelOrdre.Start();

            // pour forcer l'envoy des valeur du joyStick
            PreviousMessageHexa = "";
        }

        private void Envoyer_Click(object sender, EventArgs e)
        {
            PourReseau.Send(TextAEnvoyer.Text);
        }

        private float JoyX = 40.0F, JoyY = 50.2F;
        private bool JoyMooving = false;

        private void MoveJoyBegin(object sender, MouseEventArgs e)
        {
            JoyMooving = true;
            FixJoy(e.X, e.Y);
        }

        private void MoveJoyEnd(object sender, MouseEventArgs e)
        {
            JoyMooving = false;
            FixJoy(40, 50.2F);
        }

        private void MoveJoy(object sender, MouseEventArgs e)
        {
            if (JoyMooving)
            {
                FixJoy(e.X, e.Y);
            }
        }

        private void FixJoy(float X, float Y)
        {
            JoyX = X > 100.0F ? 100.0F : X < 0.0F ? 0.0F : (float)X;

            JoyY = Y > 100.0F ? 100.0F : Y < 0.0F ? 0.0F : (float)Y;


            panel1.Invalidate();
        }

        private String PreviousMessageHexa;

        private void TickRelanceOrdre(object sender, EventArgs e)
        {
            String MessageHexa;

            MessageHexa = "M1:" + PourReseau.ByteToHex((byte)(JoyY * 2.55F)) + "> M2:" + PourReseau.ByteToHex((byte)(JoyX * 2.55F)) + "> ";
            MessageHexa = PourReseau.StringToHex(MessageHexa);

            // on n'envoy le message que si les valeur du joystick ont changé.
            if (PreviousMessageHexa != MessageHexa)
            {
                PourReseau.SendMsgHexa(MessageHexa);
                PreviousMessageHexa = MessageHexa;
            }
        }

        private void ButtonDeconnect_Click(object sender, EventArgs e)
        {
            TopNouvelOrdre.Stop();
            PourReseau.Shut();
        }

        private void RtspStart_Click(object sender, EventArgs e)
        {
            PourReseau.VideoRequest();
            VLC_View.playlist.add(textBoxRtspurl.Text);
            VLC_View.playlist.play();
        }

        private void RtspStop_Click(object sender, EventArgs e)
        {
            PourReseau.VideoStop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PortValide(object sender, EventArgs e)
        {
            if (!Int32.TryParse(textBoxPort.Text, out PourReseau.port))
            {
                textBoxPort.Text = "13000";
            }
        }

        private void AdresseLocale(object sender, EventArgs e)
        {

            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());  //"host.contoso.com"
            AdressBox.Text = ipHostInfo.AddressList[0].ToString();
        }

        private RegistryKey baseRegistryKey = Registry.CurrentUser;  //LocalMachine
        private string subKey = "Software\\" + Application.ProductName;

        private void LoadConf()
        {
            AdressBox.Text = LoadReg("Adress");
            if (AdressBox.Text == "")
            {
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());  //"host.contoso.com"
                AdressBox.Text = ipHostInfo.AddressList[0].ToString();
            }

            textBoxPort.Text = LoadReg("Port");
            PasswTextBox.Text = LoadReg("Passw");
            textBoxRtspurl.Text = LoadReg("Video");
        }

        private String LoadReg(String Key)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)

            if (sk1 != null)
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.    ToUpper
                    return (string)sk1.GetValue(Key);
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    AfficheLErreur(e.ToString() + "\nReading registry " + Key);
                }
            }
            return "";
        }

        private void SaveToReg(object sender, FormClosingEventArgs e)
        {
            SaveVal("Adress", AdressBox.Text);
            SaveVal("Port", textBoxPort.Text);
            SaveVal("Passw", PasswTextBox.Text);
            SaveVal("Video", textBoxRtspurl.Text);
        }

        private void SaveVal(String Key, String Value)
        {
            try
            {
                // Setting   Software
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(Key, Value);
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                AfficheLErreur(e.ToString() + "\nWriting registry " + Key);
            }
        }


        private void DrawJoy(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen Pen = new Pen(Color.Gray, 3);
            // Create location and size of ellipse.
            float width = 20.0F;
            float height = 20.0F;
            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(Pen, JoyX - 10.0F, JoyY - 10.0F, width, height);
        }
    }
}

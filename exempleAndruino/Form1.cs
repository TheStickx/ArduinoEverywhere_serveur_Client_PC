using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

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
                ReceptionCapteur = new AsynchronousClient.ReceptionCapteurHandler(MAJTexteRecu),
                ReceptionDErreur = new AsynchronousClient.ReceptionDErreurHandler(AfficheLErreur)

            };
            TopNouvelOrdre.Start();
        } 

        public void AfficheLErreur(String sMessageErreur)
        {
            labelError.Text = sMessageErreur;
        }

        public void MAJMessageCapteurs(String sMessage)
        {
            TexteRecu.Text += sMessage;
        }

        public void MAJMessageHexa(String sMessage)
        {
            TexteRecu.Text += sMessage;
        }

        public void MAJTexteRecu(String sMessage)
        {
            TexteRecu.Text += sMessage;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            PourReseau.StartClient();
            TopNouvelOrdre.Start();
        }

        private void Envoyer_Click(object sender, EventArgs e)
        {
            PourReseau.Send( TextAEnvoyer.Text );
        }

        private float JoyX = 50.0F, JoyY = 50.0F;
        private bool JoyMooving = false;
        
        private void MoveJoyBegin(object sender, MouseEventArgs e)
        {
            JoyMooving = true;
            FixJoy(e.X, e.Y);
        }

        private void MoveJoyEnd(object sender, MouseEventArgs e)
        {
            JoyMooving = false;
            FixJoy(50, 50);
        }

        private void MoveJoy(object sender, MouseEventArgs e)
        {
            if ( JoyMooving )
            {
                FixJoy(e.X , e.Y);
            }
        }

        private void FixJoy ( int X, int Y )
        {
            JoyX = X > 100.0F ? 100.0F : X < 0.0F ? 0.0F : (float)X;
            
            JoyY = Y > 100.0F ? 100.0F : Y < 0.0F ? 0.0F : (float)Y;
            

            panel1.Invalidate();
        }

        private void TickRelanceOrdre(object sender, EventArgs e)
        {
            String MessageHexa;

            MessageHexa = "M1:" + PourReseau.ByteToHex((byte)(JoyX * 2.55F)) + "> M2:" + PourReseau.ByteToHex((byte)(JoyY * 2.55F)) + "> ";
            MessageHexa = PourReseau.StringToHex(MessageHexa);
            PourReseau.SendMsgHexa(MessageHexa);
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

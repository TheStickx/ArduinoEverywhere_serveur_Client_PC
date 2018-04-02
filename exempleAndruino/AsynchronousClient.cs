using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace exempleAndruino
{
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        //public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousClient
    {
        // The port number for the remote device.
        private const int port = 13000;
        Socket clientAndruino = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

        public delegate void ReceptionMsgCoolHandler(String sMessage);
        public ReceptionMsgCoolHandler ReceptionMsgCool;

        public delegate void ReceptionMsgHexaHandler(String sMessage);
        public ReceptionMsgHexaHandler ReceptionMsgHexa; 

        public delegate void ReceptionCapteurHandler(String sMessage);
        public ReceptionCapteurHandler ReceptionCapteur;

        public delegate void ReceptionDErreurHandler(String sMessage);
        public ReceptionDErreurHandler ReceptionDErreur;

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;
        public Form PourRetourVersGUI;

        public AsynchronousClient(Form NecessairePourLaSuite)
        {
            PourRetourVersGUI = NecessairePourLaSuite;
        }

        public void StartClient()
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());  //"host.contoso.com"
                IPAddress ipAddress = ipHostInfo.AddressList[0]; //Dns.GetHostName()
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Connect to the remote endpoint.
                clientAndruino.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), clientAndruino);

            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);
                Receive();
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                PourRetourVersGUI.Invoke(ReceptionMsgCool, new object[]
                { "Socket connected to {0}" + client.RemoteEndPoint.ToString() });

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

        //-------------------------------------------------------------
        // Operations de chaines vers hexa
        public String StringToHex(String sMessageAReformuler)
        {
            int i, iMessageAreformulerTaille, Tempi;
            String messageReformate=""; // = "<message hexa>=<";// = gcnew  String("<message hexa>=");
            String ConnerieTemporaire;
            //= "<message hexa>=";

            iMessageAreformulerTaille = sMessageAReformuler.Length;

            for (i = 0; i < iMessageAreformulerTaille; i++)
            {

                Tempi = (int)sMessageAReformuler.Substring(i, 1).ToCharArray()[0];
                ConnerieTemporaire = String.Format("{0,10:X2}", Tempi).Trim();
                messageReformate += ConnerieTemporaire;
                //Integer.toHexString(sMessageAReformuler.contenu[i]);
            }
            //messageReformate += ">";
            return messageReformate;
        }

        public String HexToString(String MessageARecoder)
        {
            int iPosition, iLongeurMessageARecoder, i;
            String sMessageHexa = "";
            char cOctet;

            iPosition = 0;
            iLongeurMessageARecoder = MessageARecoder.Length;

            while (iPosition < iLongeurMessageARecoder)
            {
                cOctet = (char)Convert.ToInt32
                        (MessageARecoder.Substring(iPosition, 2), 16);

                //tMessageHexa.contenu[iDansContenu] = (byte)Integer.parseInt(sOctet, 16);
                sMessageHexa += cOctet;

                iPosition += 2;
            }

            return sMessageHexa;
        }
        //-------------------------------------------------------------
        //  Convertir des nombres en TexteHexa
        public String ByteToHex(byte Octet)
        {
            String Hex = Convert.ToString(Octet, 16);
            int ILong = 2 - Hex.Length;

            for (int i = 0; i < ILong; i++)
            {
                Hex = "0" + Hex;
            }
            return Hex;
        }

        public String ShortToHex(short DeuxOctet)
        {
            String Hex = Convert.ToString(DeuxOctet, 16);
            int ILong = 4 - Hex.Length;
            
            for (int i = 0; i < ILong; i++)
            {
                Hex = "0" + Hex;
            }
            return Hex;
        }

        public String IntToHex(int Integer)
        {
            String Hex = Convert.ToString(Integer, 16);
            int ILong = 8 - Hex.Length;
            
            for (int i = 0; i < ILong; i++)
            {
                Hex = "0" + Hex;
            }
            return Hex;
        }
        //-------------------------------------------------------------
        //  Convertir un TexteHexa en nombre
        public byte HexToByte(String MessageByte)
        {
            int longueur = MessageByte.Length > 2 ?  2 : MessageByte.Length;
            return (byte)Convert.ToInt32
                        (MessageByte.Substring(0, longueur), 16);
        }

        public short HexToShort(String MessageByte)
        {
            int longueur = MessageByte.Length > 4 ? 4 : MessageByte.Length;
            return (short)Convert.ToInt32
                        (MessageByte.Substring(0, longueur), 16);
        }

        public int HexToInt(String MessageByte)
        {
            int longueur = MessageByte.Length > 8 ? 8 : MessageByte.Length;
            return Convert.ToInt32
                        (MessageByte.Substring(0, longueur), 16);
        }
        //-------------------------------------------------------------

        private String DonneesRecues;

        public void ProcessReception(String Reception)
        {
            int iDebutEtiquette, iDebutContenu, iFinContenu;
            String Etiquette, Contenu;

            // ajoutons ce qu'on viens de recevoir à ce qu'on a déjà.
            // voyons ensuite si on sait le traiter.
            DonneesRecues += Reception;

            iDebutEtiquette = DonneesRecues.IndexOf("<");
            iDebutContenu = DonneesRecues.IndexOf(">=<");
            if (iDebutContenu >= 0) iFinContenu = DonneesRecues.IndexOf(">", iDebutContenu + 3);
            else iFinContenu = -1;
            // si on a pas d'étiquette complette ou de contenu complet.
            // on sort ce sera la prochaine fois
            while ((iDebutEtiquette >= 0) && (iDebutContenu > 0) && (iFinContenu > 0))
            {
                // on a une étiquette
                Etiquette = DonneesRecues.Substring(iDebutEtiquette + 1, iDebutContenu - iDebutEtiquette - 1);
                Contenu = DonneesRecues.Substring(iDebutContenu + 3, iFinContenu - iDebutContenu - 3);
                //On rétrécis le Contenu de ce qui est traité
                DonneesRecues = DonneesRecues.Substring(iFinContenu + 1);

                // Selon l'etiquette on fait un truc
                if (Etiquette == "Message cool")
                {
                    // affiche pour le fun
                    //Console.WriteLine(Etiquette + "=>" + Contenu);
                    PourRetourVersGUI.Invoke(ReceptionMsgCool, new object[]
                        { Contenu });

                }
                if (Etiquette == "message hexa")
                {
                    // affiche pour le fun
                    //Console::WriteLine(Etiquette + "=>" + Contenu);
                    //Console::WriteLine(Etiquette + ">>" + MessageHexToTabDeByte(Contenu));
                    //Console.Write(MessageHexToTabDeByte(Contenu));

                    PourRetourVersGUI.Invoke(ReceptionMsgHexa, new object[]
                        { Contenu });

                }

                if (Etiquette == "capteurs")
                {
                    // affiche pour le fun
                    PourRetourVersGUI.Invoke(ReceptionCapteur, new object[]
                        { Contenu });
                }

                iDebutEtiquette = DonneesRecues.IndexOf("<");
                iDebutContenu = DonneesRecues.IndexOf(">=<");
                if (iDebutContenu >= 0) iFinContenu = DonneesRecues.IndexOf(">", iDebutContenu + 3);
            }
        }

        private void Receive()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = clientAndruino;

                // Begin receiving the data from the remote device.
                clientAndruino.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    ProcessReception(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    //PourRetourVersGUI.Invoke(MessageAssynchrone, new object[]
                    //    { Encoding.ASCII.GetString(state.buffer, 0, bytesRead) });


                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.
                    /*if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.
                    receiveDone.Set();*/

                }
            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

        public void SendMsgCool(String data)
        {
            Send( "<Message cool>=<" + data + ">" );
        }

        public void SendMsgHexa(String data)
        {
            Send( "<message hexa>=<" + data + ">" );
        }

        public void SendCapteur(String data)
        {
            Send( "<capteurs>=<" + data + ">" );
        }

        public void Send(String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            try
            {
                // Begin sending the data to the remote device.
                clientAndruino.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), clientAndruino);
            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);



                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                ReceptionDErreur(e.ToString());
            }
        }

    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// State object for reading client data asynchronously
public class StateObject
{
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    public bool bEnvoye = true;

    //----------------------------------------------------------------------
    // utilisation de cet enum
    // NA - utilisé lors du retour de la fonction ProcessReception
    enum EnumConnectionStyle  { NA, Undefine, Application, Arduino };
    EnumConnectionStyle ConnectionStyle;


        // Received data string.
        //public StringBuilder sb = new StringBuilder();

        //public class EnCodeDecode


        private
		String  DonneesRecues;
	
	    public String  StringToHex(String sMessageAReformuler)
        {
            int i, iMessageAreformulerTaille, Tempi;
            String  messageReformate="<message hexa>=<";// = gcnew  String("<message hexa>=");
            String  ConnerieTemporaire;
            //= "<message hexa>=";

            iMessageAreformulerTaille = sMessageAReformuler.Length;

            for (i = 0; i < iMessageAreformulerTaille; i++)
            {

                Tempi = (int)sMessageAReformuler.Substring(i, 1).ToCharArray()[0];
                ConnerieTemporaire = String.Format("{0,10:X2}", Tempi).Trim();  //.Trim()
                messageReformate += " " + ConnerieTemporaire;  //Integer.toHexString(sMessageAReformuler.contenu[i]);
            }
            messageReformate += ">";
            return messageReformate;
        }

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
                if (Etiquette == " Message cool")
                {
                    // affiche pour le fun
                    Console.WriteLine(Etiquette + "=>" + Contenu);

                }
                if (Etiquette == "message hexa")
                {
                    // affiche pour le fun
                    //Console::WriteLine(Etiquette + "=>" + Contenu);
                    //Console::WriteLine(Etiquette + ">>" + MessageHexToTabDeByte(Contenu));
                    Console.Write(MessageHexToTabDeByte(Contenu));

                }

                iDebutEtiquette = DonneesRecues.IndexOf("<");
                iDebutContenu = DonneesRecues.IndexOf(">=<");
                if (iDebutContenu >= 0) iFinContenu = DonneesRecues.IndexOf(">", iDebutContenu + 3);
            }
        }

        public String MessageHexToTabDeByte(String MessageARecoder)
        {
            int iPosition, iLongeurMessageARecoder, i;
            String sMessageHexa="";
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
    
}

public class AsynchronousSocketListener
{
    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);
    public static ManualResetEvent SendDone = new ManualResetEvent(false);

    private static StateObject[]  InfoConnection = new StateObject[10] ;

    public AsynchronousSocketListener()
    {
    }

    public static void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);
        String venantDeLaConsole;

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);


                // Wait until a connection is made before continuing.
                allDone.WaitOne();
                //while  ( true )
                while (InfoConnection[0].workSocket.Connected)
                {
                    if (InfoConnection[0].workSocket.Connected && Console.KeyAvailable)
                    {
                        venantDeLaConsole = Console.ReadLine();
                        Send(InfoConnection[0].workSocket, venantDeLaConsole);
                    }
                    //Send(listener, venantDeLaConsole);
                    //allDone.Reset();
                }



            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Get the socket that handles the client request.
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);


        StateObject state = new StateObject();
        state.workSocket = handler;

        // Create the state object.
        /*
        InfoConnection[0] = new StateObject
        {
            workSocket = handler , 
            buffer = new byte[StateObject.BufferSize]
        };*/
        InfoConnection[0] = new StateObject();
        InfoConnection[0].workSocket = handler;
        
        // Signal the main thread to continue.
        allDone.Set();

        handler.BeginReceive(InfoConnection[0].buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), InfoConnection[0]);
        /*
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);*/


    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket. 
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.
            content = Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead);
            // state.sb.Append(Encoding.ASCII.GetString(
            //    state.buffer, 0, bytesRead));
            // ---------------------------
            // Ma routine
            state.ProcessReception(content);
            // ---------------------------
            // il faut toujours reprogrammer le call back
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state); 
            {
                /*
                // ---------------------------

                // nonn je fait plus comme ça maintenant c'est une trame de 0 octets qui
                // provoque la fermeture
                //
                // Check for end-of-file tag. If it is not there, read 
                // more data.
                //content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.
                    //Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                } */
            }
        }
        else
        {
            Shut(state);
        }

    }

    private static void Send(Socket handler, String data)
    {

        //SendDone.Set();
        // Convert the string data to byte data using ASCII encoding.
        data += "\r\n";
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        //handler.Send(byteData);

        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
        //SendDone.Reset();
        //SendDone.WaitOne();
        /*
        handler.BeginSend(InfoConnection[0].buffer, 0, InfoConnection[0].buffer.Length, 0,
                    new AsyncCallback(SendCallback), InfoConnection[0].workSocket);
                    */
        SendDone.WaitOne();
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            SendDone.Set();
            //SendDone.Reset();
            // Retrieve the socket from the state object.
            //StateObject state = (StateObject)ar.AsyncState;
            //Socket handler = state.workSocket;
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            {
                //int bytesSent = handler.EndSend(InfoConnection[0].workSocket);
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                //SendDone.Reset();
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
                /*
                if ( bytesSent > 0 )
                {
                    byte[] Saloperie = new Byte[1];
                    handler.BeginSend(Saloperie, 0, 1, 0,
                            new AsyncCallback(SendCallback), handler);
                }*/

                //handler.SendPacketsAsync
                /*
                if (!InfoConnection[0].bEnvoye)
                {
                    handler.BeginSend(InfoConnection[0].buffer, 0, InfoConnection[0].buffer.Length, 0,
                            new AsyncCallback(SendCallback), handler);  //StateObject.BufferSize
                    InfoConnection[0].bEnvoye = true;
                }
            handler.BeginSend(InfoConnection[0].buffer, 0, InfoConnection[0].buffer.Length, 0,
                    new AsyncCallback(SendCallback), handler);  //StateObject.BufferSize
                */
            }

            /*
    handler.BeginSend(InfoConnection[0].buffer, 0, InfoConnection[0].buffer.Length, 0,
                new AsyncCallback(SendCallback), InfoConnection[0].workSocket);*/



        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Shut(StateObject State)
    {
        State.workSocket.Shutdown(SocketShutdown.Both);
        State.workSocket.Close();
    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}

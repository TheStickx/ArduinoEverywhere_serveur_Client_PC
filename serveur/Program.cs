﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// it's required for reading/writing into the registry:
using Microsoft.Win32;

// State object for reading client data asynchronously
public class StateObject
{
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    //public bool bEnvoye = true;

    //----------------------------------------------------------------------
    // utilisation de cet enum
    // NA           - utilisé lors du retour de la fonction ProcessReception: signifie Rien à signaler 
    //          ou comme quoi le message ne permet pas de conclure qu'on change de connection type
    //          Si ConnectionStyle a cette valeur cela veu dire qu'il est libre
    // Undefine     - état par défaut lors de la création du state obbject
    // Application  - ProcessReception a recu une demande claire du client pour stypuler qu'il est 'Application'
    // Arduino      - ProcessReception a recu une demande claire du client pour stypuler qu'il est 'Arduino'
    public enum EnumConnectionStyle { NA, Undefine, Application, Arduino };
    public EnumConnectionStyle ConnectionStyle;
    public int WhichStObjectAmI;
    public bool MultiConnexion;
    public bool BTstatus;
}

public class AsynchronousSocketListener
{
    private const int NmaxStObjects = 10;
    private static StateObject[] InfoConnection = new StateObject[NmaxStObjects];
    Socket listener;

    String PasswordOfClients;
    int PortOfServer;
    Boolean DebugMode;

    public AsynchronousSocketListener()
    {
    }

    public void StartListening()
    {
        // on initialise tout les StObjects 
        for (int i = 0; i < NmaxStObjects; i++)
        {
            InfoConnection[i] = new StateObject
            {
                // ConnectionStyle = .....NA; => par défaut n'a pas de connection. voir définition de StateObject.EnumConnectionStyle
                ConnectionStyle = StateObject.EnumConnectionStyle.NA,
                // WhichStObjectAmI = N° d'indice Ainsi quand on le récupère dans les callback on a leur indice
                WhichStObjectAmI = i
            };
        }
        LoadConf();

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PortOfServer);
        String venantDeLaConsole;

        // Create a TCP/IP socket.
        listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for first connection...on port " + PortOfServer + " (press hey for menu)");

                ShouldIAcceptNewConnexion(); // nouvelle méthode pour begin accept plus soupple

                // Wait until a connection is made before continuing.
                while (true)
                {
                    /* if (InfoConnection[0].workSocket.Connected && Console.KeyAvailable)
                    {
                        if (Console.KeyAvailable)
                        {
                            venantDeLaConsole = Console.ReadLine();
                            RepeatToOthers(StateObject.EnumConnectionStyle.Undefine, venantDeLaConsole);
                        }
                    }*/
                    venantDeLaConsole = Console.ReadLine();
                    
                    switch (venantDeLaConsole)
                    {
                        case "port":
                            Console.WriteLine("Entrez le port:");
                            venantDeLaConsole = Console.ReadLine();
                            if (!Int32.TryParse (venantDeLaConsole, out PortOfServer ) )
                            {
                                Console.WriteLine("valeure incorrecte port = 13000");
                                PortOfServer = 13000;
                            }
                            if (PortOfServer>65535 || PortOfServer <0)
                            {
                                Console.WriteLine("valeure incorrecte port = 13000");
                                PortOfServer = 13000;
                            }
                            Console.WriteLine("Le changement de port sera effectif au prochain lancement");
                            break;

                        case "password":
                            Console.WriteLine("Entrez le Mot de passe:");
                            break;

                        case "debug":
                            Console.WriteLine("debug (y/n):");
                            venantDeLaConsole = Console.ReadLine();

                            if (venantDeLaConsole == "y")
                            {
                                DebugMode = true;
                                Console.WriteLine("\ndebug mode activé\n");
                            }
                            else
                            {
                                DebugMode = false;
                                Console.WriteLine("\ndebug mode désactivé\n");
                            }
                            break;

                        case "exit":
                            Console.WriteLine("\nWriting conf to register (press a key)");
                            Console.Read();
                            SaveToReg();
                            return;

                        default:
                            Console.WriteLine("\ntappez le nom du paramètre parmis cette liste pour pouvoir le changer:\nport\npassword\ndebug\nexit\n\n");
                            break;

                    }

                    
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine( DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    private static int FreeStObjectNumber()
    {
        for (int i = 0; i < NmaxStObjects; i++)
        {
            if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.NA)
            {
                return i;
            }
        }
        return -1;
    }

    private void ShouldIAcceptNewConnexion()
    {
        for (int i = 0; i < NmaxStObjects; i++)
        {
            if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.NA)
            {
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);
            }
        }
    }

    private static void RepeatToOthers(StateObject.EnumConnectionStyle QuelTypeParles, String data)
    {
        switch (QuelTypeParles)
        {
            // les NA ne parlent pas.

            case StateObject.EnumConnectionStyle.Undefine:
                // les Undefine parles aux Undefine
                for (int i = 0; i < NmaxStObjects; i++)
                {
                    if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.Undefine)
                    {
                        Send(InfoConnection[i].workSocket, data);
                    }
                }
                break;

            case StateObject.EnumConnectionStyle.Application:
                // les Application parlent aux Arduino
                for (int i = 0; i < NmaxStObjects; i++)
                {
                    if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.Arduino)
                    {
                        Send(InfoConnection[i].workSocket, data);
                    }
                }
                break;

            case StateObject.EnumConnectionStyle.Arduino:
                // les Arduino parles aux Application
                for (int i = 0; i < NmaxStObjects; i++)
                {
                    if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.Application)
                    {
                        Send(InfoConnection[i].workSocket, data);
                    }
                }
                break;
        }

    }

    private static int CombienDeBT()
    {
        int NbBT=0;

        for (int i = 0; i < NmaxStObjects; i++)
                {
            if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.Arduino)
            {
                if (InfoConnection[i].BTstatus == true)
                {
                    NbBT++;
                }
            }
        }

        return NbBT;
    }

    private static int CombienDApplis()
    {
        int NbApp = 0;

        for (int i = 0; i < NmaxStObjects; i++)
        {
            if (InfoConnection[i].ConnectionStyle == StateObject.EnumConnectionStyle.Application)
            {
                NbApp++;
            }
        }

        return NbApp;
    }

    public void AcceptCallback(IAsyncResult ar)
    {
        // Get the socket that handles the client request.
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
        int StObjectNumber;

        StateObject state = new StateObject
        {
            workSocket = handler
        };

        // Create the state object.
        StObjectNumber = FreeStObjectNumber();
        InfoConnection[StObjectNumber].workSocket = handler;
        InfoConnection[StObjectNumber].ConnectionStyle = StateObject.EnumConnectionStyle.Undefine; // au début on sait pas mais du coups plus NA
        Console.WriteLine("\nnouvelle connection avec {0}\n", handler.RemoteEndPoint.ToString());

        handler.BeginReceive(InfoConnection[StObjectNumber].buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), InfoConnection[StObjectNumber]);

        ShouldIAcceptNewConnexion();  // d'ici on autorise à accepter de nouvelles connexions
    }

    public void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        try
        {
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
                ProcessReception( state.WhichStObjectAmI , content);
            }
            else
            {
                Shut(state);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString());

            Shut(state);

            ShouldIAcceptNewConnexion(); // par contre on peut peut être accepter de nouvelles connexions
        }

    }

    private static void Send(Socket handler, String data)
    {
        // Convert the string data to byte data using ASCII encoding.
        data += "\r\n";
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.

        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
        }
        catch (Exception e)
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString());
        }
    }

    private static void Shut(StateObject State)
    {
        if (InfoConnection[State.WhichStObjectAmI].workSocket.Connected )
        {
            InfoConnection[State.WhichStObjectAmI].workSocket.Shutdown(SocketShutdown.Both);
            InfoConnection[State.WhichStObjectAmI].workSocket.Close();
        }
        InfoConnection[State.WhichStObjectAmI].ConnectionStyle = StateObject.EnumConnectionStyle.NA;
    }

    private String DonneesRecues;

    public void ProcessReception( int THatStObjetct, String Reception)
    {
        int iDebutEtiquette, iDebutContenu, iFinContenu;
        String Etiquette, Contenu;
        bool DontShutThis = true;

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
            // <client description>=<side=application_side multicon=Nok>
            // <client description>=<side=arduino_side multicon=Nok>

            if (Etiquette == "client description")
            {
                String Temp;
                int PositionEspace;
                bool Multi, MultiOk, SideOk;
                StateObject.EnumConnectionStyle Side;

                try
                {
                    PositionEspace = Contenu.IndexOf(" ");
                    if (PositionEspace >= 0)
                    {
                        Temp = Contenu.Substring(0, PositionEspace);
                        Contenu = Contenu.Substring(PositionEspace + 1, Contenu.Length - PositionEspace -1);

                        if ((Temp.Substring(0, 5) == "side=") && (Contenu.Substring(0, 9) == "multicon="))
                        {
                            MultiOk = false;
                            SideOk = false;
                            Side = StateObject.EnumConnectionStyle.NA;
                            Multi = true;

                            // side
                            if (Temp == "side=arduino_side")
                            {
                                Side = StateObject.EnumConnectionStyle.Arduino;
                                SideOk = true;
                            }
                            if (Temp == "side=application_side")
                            {
                                Side = StateObject.EnumConnectionStyle.Application;
                                SideOk = true;
                            }

                            // MultiCon
                            if (Contenu == "multicon=Ok")
                            {
                                Multi = true;
                                MultiOk = true;
                            }
                            if (Contenu == "multicon=Nok")
                            {
                                Multi = false;
                                MultiOk = true;

                                // doit détecter si une autre connexion est déjà en cours 
                                // => si oui on ferme et on ne reconfigure pas le call back
                                // idem MultiOk repasse à false 
                                for (int i = 0; i < NmaxStObjects; i++)
                                {
                                    if (InfoConnection[i].ConnectionStyle == Side)
                                    {
                                        // Shut(InfoConnection[i]);  ben non c'est la connection en cours qu'il faut fermer
                                        Shut(InfoConnection[THatStObjetct]); 
                                        DontShutThis = false;
                                        Console.WriteLine("La connexion N°{0} est refusé car Multicon=NOK\n", THatStObjetct);
                                        MultiOk = false;
                                    }
                                }
                            }

                            // Tout est défini 
                            if ( SideOk && MultiOk )
                            {
                                InfoConnection[THatStObjetct].MultiConnexion = Multi;
                                InfoConnection[THatStObjetct].ConnectionStyle = Side;

                                Console.WriteLine("La connexion N°{0} est acceptée\n", THatStObjetct);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString());
                }
            }

            if (Etiquette == "Message cool")
            {
                // affiche pour le fun
                if (DebugMode) Console.Write("\n" + Etiquette + "=>" + Contenu);
                RepeatToOthers( InfoConnection[THatStObjetct].ConnectionStyle , "<" + Etiquette + ">=<" + Contenu + ">");
            }

            if (Etiquette == "BTstat")
            {
                // récupère le statut du BlueTooth
                if (DebugMode) Console.Write("\n" + Etiquette + "=>" + Contenu);
                if (InfoConnection[THatStObjetct].ConnectionStyle == StateObject.EnumConnectionStyle.Arduino )
                {
                    if (Contenu == "OK")
                    {
                        InfoConnection[THatStObjetct].BTstatus = true;
                    }
                    if (Contenu == "NOK")
                    {
                        InfoConnection[THatStObjetct].BTstatus = false;
                    }
                    // réponds en donnant le nombre d'applis
                    Send(InfoConnection[THatStObjetct].workSocket, "<NBAppli>=<" + CombienDApplis() + ">");
                }
                else
                {
                    Console.Write("\nErreur " + Etiquette + "=>" + Contenu + "Sollicitation incohérente: Seul ARDUINO EVERYWERE a un BlueToot");
                }
            }

            if (Etiquette == "PingApp")
            {
                // récupère le statut du BlueTooth
                if (DebugMode) Console.Write("\n" + Etiquette + "=>" + Contenu);
                if (InfoConnection[THatStObjetct].ConnectionStyle == StateObject.EnumConnectionStyle.Application)
                {
                    // réponds en donnant le nombre de BlueTooth
                    Send(InfoConnection[THatStObjetct].workSocket, "<NbrBT>=<" + CombienDeBT() + ">");
                }
                else
                {
                    Console.Write("\nErreur " + Etiquette + "=>" + Contenu + "Sollicitation incohérente: Ping app depuis ARDUINO EVERYWERE");
                }
            }

            if (Etiquette == "message hexa")
            {
                // affiche pour le fun
                if (DebugMode) Console.Write("\n" + DateTime.Now.ToString("HH:mm:ss ") + Contenu + " => " + MessageHexToTabDeByte(Contenu));
                RepeatToOthers(InfoConnection[THatStObjetct].ConnectionStyle, "<" + Etiquette + ">=<" + Contenu + ">");
            }

            if (Etiquette == "capteurs")
            {
                // affiche pour le fun
                RepeatToOthers(InfoConnection[THatStObjetct].ConnectionStyle, "<" + Etiquette + ">=<" + Contenu + ">");
            }

            if (Etiquette == "video")
            {
                // affiche pour le fun
                if (DebugMode) Console.Write("\n" + Etiquette + "=>" + Contenu);
                RepeatToOthers(InfoConnection[THatStObjetct].ConnectionStyle, "<" + Etiquette + ">=<" + Contenu + ">");
            }

            iDebutEtiquette = DonneesRecues.IndexOf("<");
            iDebutContenu = DonneesRecues.IndexOf(">=<");
            if (iDebutContenu >= 0) iFinContenu = DonneesRecues.IndexOf(">", iDebutContenu + 3);
        }
        // ---------------------------
        // il faut toujours reprogrammer le call back
        if (DontShutThis)
        {
            InfoConnection[THatStObjetct].workSocket.BeginReceive
                (InfoConnection[THatStObjetct].buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), InfoConnection[THatStObjetct]);
        }
    }

    public String MessageHexToTabDeByte(String MessageARecoder)
    {
        int iPosition, iLongeurMessageARecoder;
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

    private RegistryKey baseRegistryKey = Registry.CurrentUser;  //LocalMachine
    private string subKey = "Software\\Arduino Everywhere Server";

    private void LoadConf()
    {
        if (!Int32.TryParse(LoadReg("Port"), out PortOfServer)) PortOfServer = 13000;

        PasswordOfClients = LoadReg("password");

        if (LoadReg("debug") == "yes") DebugMode = true;
        else DebugMode = false;
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
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString() + "\nReading registry " + Key);
            }
        }
        return "";
    }

    private void SaveToReg()
    {
        SaveVal("Port", String.Format("{0}", PortOfServer));
        SaveVal("password", PasswordOfClients);
        if (DebugMode) SaveVal("debug", "yes");
        else SaveVal("debug", "no");
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
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss ")   + e.ToString() + "\nWriting registry " + Key);
        }
    }
}

public class Launcher
{

    public static int Main(String[] args)
    {
        AsynchronousSocketListener Lanch = new AsynchronousSocketListener();

        Lanch.StartListening();
        return 0;
    }
}
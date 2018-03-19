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

        private delegate void MAJTexteRecuDelegateHandler(String sMessage);
        private MAJTexteRecuDelegateHandler MAJTexteRecuDelegate;

        public Form1()
        {
            InitializeComponent();
            PourReseau = new AsynchronousClient(this);

            MAJTexteRecuDelegate = new MAJTexteRecuDelegateHandler(MAJTexteRecu);
        }

        public class StateObject
        {
            // Client socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 256;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }

        public class AsynchronousClient
        {
            // The port number for the remote device.
            private const int port = 13000;
            Socket clientAndruino = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

            // ManualResetEvent instances signal completion.
            private static ManualResetEvent connectDone =
                new ManualResetEvent(false);
            private static ManualResetEvent sendDone =
                new ManualResetEvent(false);
            private static ManualResetEvent receiveDone =
                new ManualResetEvent(false);

            // The response from the remote device.
            private static String response = String.Empty;
            public Form1 PourAgirSurForm1;

            public AsynchronousClient(Form1 NecessairePourLaSuite)
            {
                PourAgirSurForm1 = NecessairePourLaSuite;
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
                    /*
                    connectDone.WaitOne();

                    // Send test data to the remote device.
                    Send(data: "<Message cool>=<This is a test>");
                    sendDone.WaitOne();

                    // Receive the response from the remote device.
                    Receive();
                    receiveDone.WaitOne();

                    // Write the response to the console.
                    Console.WriteLine("Response received : {0}", response);

                    // Release the socket.
                    clientAndruino.Shutdown(SocketShutdown.Both);
                    clientAndruino.Close(); */

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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

                    PourAgirSurForm1.Invoke(PourAgirSurForm1.MAJTexteRecuDelegate, new object[] 
                    { "Socket connected to {0}" + client.RemoteEndPoint.ToString() });
                    

                    // Signal that the connection has been made.
                    connectDone.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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
                    Console.WriteLine(e.ToString());
                }
            }

            private  void ReceiveCallback(IAsyncResult ar)
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
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                        
                        PourAgirSurForm1.Invoke(PourAgirSurForm1.MAJTexteRecuDelegate, new object[]
                            { Encoding.ASCII.GetString(state.buffer, 0, bytesRead) });


                        // Get the rest of the data.
                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                        // All the data has arrived; put it in response.
                        if (state.sb.Length > 1)
                        {
                            response = state.sb.ToString();
                        }
                        // Signal that all bytes have been received.
                        receiveDone.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public void Send(String data)
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.
                clientAndruino.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), clientAndruino);
            }

            private static void SendCallback(IAsyncResult ar)
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
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void MAJTexteRecu(String sMessage)
        {

            TexteRecu.Text += sMessage;

        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            PourReseau.StartClient();
        }

        private void Envoyer_Click(object sender, EventArgs e)
        {
            PourReseau.Send( TextAEnvoyer.Text );
        }
    }
}

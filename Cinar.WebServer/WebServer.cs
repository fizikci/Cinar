using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Web;

namespace Cinar.WebServer
{
    public class WebServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        public WebServer(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            listenThread = new Thread(listenForClients);
            listenThread.Start();
        }

        public void Stop()
        {
            listenThread.Abort();
            tcpListener.Stop();
        }

        private void listenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication
                //with connected client
                Thread clientThread = new Thread(handleClientComm);
                clientThread.Start(client);
            }
        }

        private void handleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                if(ProcessRequest!=null)
                    ProcessRequest(Encoding.UTF8.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }

        public Action<string> ProcessRequest;
    }


}
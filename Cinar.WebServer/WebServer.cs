using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Web;
using System.Web.Hosting;

namespace Cinar.WebServer
{
    public class CinarWebServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        public bool ProcessRequestsInSeperateThreads = true;

        public CinarWebServer(int port)
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
                if (ProcessRequestsInSeperateThreads)
                {
                    Thread clientThread = new Thread(handleClientComm);
                    clientThread.Start(client);
                }
                else
                {
                    handleClientComm(client);
                }
            }
        }

        private void handleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient) client;
            NetworkStream clientStream = tcpClient.GetStream();

            //message has successfully been received
            if (ProcessRequest != null)
            {
                Request request = parseRequest(clientStream);
                StreamWriter outputStream = new StreamWriter(new BufferedStream(tcpClient.GetStream()));
                Response response = new Response(outputStream);

                response.WriteLine("HTTP/1.0 200 OK");
                response.WriteLine("Content-Type: text/html");
                response.WriteLine("Connection: close");
                response.WriteLine("");


                ProcessRequest(request, response);

                response.OutputStream.Flush();
                response.OutputStream.Close();

            }

            tcpClient.Close();
        }

        private Request parseRequest(Stream inputStream)
        {
            Request r = new Request();

            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            r.Method = tokens[0].ToUpper();
            r.Url = tokens[1];
            r.ProtocolVersion = tokens[2];

            String line;
            while ((line = streamReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    break;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                r.Headers[name] = value;
            }

            if (r.Method.Equals("POST"))
            {
                const int BUF_SIZE = 4096;
                const int MAX_POST_SIZE = 10 * 1024 * 1024;

                MemoryStream ms = new MemoryStream();
                if (r.Headers.ContainsKey("Content-Length"))
                {
                    int content_len = Convert.ToInt32(r.Headers["Content-Length"]);
                    if (content_len > MAX_POST_SIZE)
                        throw new Exception(String.Format("POST Content-Length({0}) too big for this simple server", content_len));

                    byte[] buf = new byte[BUF_SIZE];
                    int to_read = content_len;
                    while (to_read > 0)
                    {
                        int numread = inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                        if (numread == 0)
                        {
                            if (to_read == 0)
                                break;
                            throw new Exception("client disconnected during post");
                        }
                        to_read -= numread;
                        ms.Write(buf, 0, numread);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                }

                r.PostData = new StreamReader(ms).ReadToEnd();
            }

            return r;
        }
        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }

        public Action<Request, Response> ProcessRequest;
    }

    public class Request
    {
        public string Method;
        public string Url;
        public string ProtocolVersion;

        public Hashtable Headers = new Hashtable();
        public string PostData;
    }

    public class Response
    {
        private StreamWriter outputStream;
        public Response(StreamWriter outputStream)
        {
            this.outputStream = outputStream;
        }

        public StreamWriter OutputStream {
            get { return outputStream; }
        }

        public void Write(string format, params object[] args)
        {
            this.OutputStream.Write(format, args);
        }

        public void WriteLine(string format, params object[] args)
        {
            this.OutputStream.WriteLine(format, args);
        }
    }
}
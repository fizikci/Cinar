using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cinar.POP3
{
	public class Pop3Client
	{
		private Pop3Credential m_credential;

		private const int m_pop3port = 110;
		private const int MAX_BUFFER_READ_SIZE = 256;
		
		private long m_inboxPosition = 0;
		private long m_directPosition = -1;

		private Socket m_socket = null;
 
		private Pop3Message m_pop3Message = null;

		public Pop3Credential UserDetails
		{
			set { m_credential = value; }
			get { return m_credential; }
		}

		public string From
		{
			get { return m_pop3Message.From; }
		}

		public string To
		{
			get { return m_pop3Message.To; }
		}

		public string Subject
		{
			get { return m_pop3Message.Subject; }
		}

		public string Body
		{
			get { return m_pop3Message.Body; }
		}

		public IEnumerator MultipartEnumerator
		{
			get { return m_pop3Message.MultipartEnumerator; }
		}

		public bool IsMultipart
		{
			get { return m_pop3Message.IsMultipart; }
		}


		public Pop3Client(string user, string pass, string server)
		{
			m_credential = new Pop3Credential(user,pass,server);
		}

		private Socket GetClientSocket()
		{
			Socket s = null;
			
			try
			{
				IPHostEntry hostEntry = null;
        
				// Get host related information.
				hostEntry = Dns.GetHostEntry(m_credential.Server);

				// Loop through the AddressList to obtain the supported 
				// AddressFamily. This is to avoid an exception that 
				// occurs when the host IP Address is not compatible 
				// with the address family 
				// (typical in the IPv6 case).
				
				foreach(IPAddress address in hostEntry.AddressList)
				{
					IPEndPoint ipe = new IPEndPoint(address, m_pop3port);
				
					Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

					tempSocket.Connect(ipe);

					if(tempSocket.Connected)
					{
						// we have a connection.
						// return this socket ...
						s = tempSocket;
						break;
					}
					else
					{
						continue;
					}
				}
			}
			catch(Exception e)
			{
				throw new Pop3ConnectException(e.ToString());
			}

			// throw exception if can't connect ...
			if(s == null)
			{
				throw new Pop3ConnectException("Error : connecting to " + m_credential.Server);
			}
			
			return s;
		}

		//send the data to server
		private void Send(String data) 
		{
			if(m_socket == null)
			{
				throw new Pop3MessageException("Pop3 connection is closed");
			}

			try
			{
				// Convert the string data to byte data 
				// using ASCII encoding.
				
				byte[] byteData = Encoding.ASCII.GetBytes(data+"\r\n");
				
				// Begin sending the data to the remote device.
				m_socket.Send(byteData);
			}
			catch(Exception e)
			{
				throw new Pop3SendException(e.ToString());
			}
		}

		private string GetPop3String()
		{
			if(m_socket == null)
			{
				throw new 
					Pop3MessageException("Connection to POP3 server is closed");
			}

			byte[] buffer = new byte[MAX_BUFFER_READ_SIZE];
			string line = null;

			try
			{
                int byteCount = m_socket.Receive(buffer, buffer.Length, 0);

				line = Encoding.ASCII.GetString(buffer, 0, byteCount);
			}
			catch(Exception e)
			{
				throw new Pop3ReceiveException(e.ToString());
			}

			return line;
		}

		private void LoginToInbox()
		{
			string returned;

			// send username ...
			Send("user "+m_credential.User);
		
			// get response ...
			returned = GetPop3String();

			if( !returned.Substring(0,3).Equals("+OK") )
			{
				throw new Pop3LoginException("login not excepted");
			}

			// send password ...
			Send("pass "+m_credential.Pass);

			// get response ...
			returned = GetPop3String();

			if( !returned.Substring(0,3).Equals("+OK") )
			{
				throw new 
					Pop3LoginException("login/password not accepted");
			}
		}

		public long MessageCount
		{
			get 
			{
				long count = 0;
			
				if(m_socket==null)
				{
					throw new Pop3MessageException("Pop3 server not connected");
				}

				Send("stat");

				string returned = GetPop3String();

				// if values returned ...
				if( Regex.Match(returned,
					@"^.*\+OK[ |	]+([0-9]+)[ |	]+.*$").Success )
				{
						// get number of emails ...
						count = long.Parse( Regex
						.Replace(returned.Replace("\r\n","")
						, @"^.*\+OK[ |	]+([0-9]+)[ |	]+.*$" ,"$1") );
				}

				return(count);
			}
		}

		public void CloseConnection()
		{			
			Send("quit");

			m_socket = null;
			m_pop3Message = null;
		}

		public bool DeleteEmail()
		{
			bool ret = false;

			Send("dele "+m_inboxPosition);

			string returned = GetPop3String();

			if( Regex.Match(returned,
				@"^.*\+OK.*$").Success )
			{
				ret = true;
			}

			return ret;
		}

		public bool NextEmail(long directPosition)
		{
			bool ret;

			if( directPosition >= 0 )
			{
				m_directPosition = directPosition;
				ret = NextEmail();
			}
			else
			{
				throw new Pop3MessageException("Position less than zero");
			}

			return ret;
		}

		public bool NextEmail()
		{
			string returned;

			long pos;

			if(m_directPosition == -1)
			{
				if(m_inboxPosition == 0)
				{
					pos = 1;
				}
				else
				{
					pos = m_inboxPosition + 1;
				}
			}
			else
			{
				pos = m_directPosition+1;
				m_directPosition = -1;
			}

			// send username ...
			Send("list "+pos.ToString());
		
			// get response ...
			returned = GetPop3String();

			// if email does not exist at this position
			// then return false ...

			if( returned.Substring(0,4).Equals("-ERR") )
			{
				return false;
			}

			m_inboxPosition = pos;

			// strip out CRLF ...
			string[] noCr = returned.Split(new char[]{ '\r' });

			// get size ...
			string[] elements = noCr[0].Split(new char[]{ ' ' });

			long size = long.Parse(elements[2]);

			// ... else read email data
			m_pop3Message = new Pop3Message(m_inboxPosition,size,m_socket);

            return true;
		}

		public void OpenInbox()
		{
			// get a socket ...
			m_socket = GetClientSocket();

			// get initial header from POP3 server ...
			string header = GetPop3String();

			if( !header.Substring(0,3).Equals("+OK") )
			{
				throw new Exception("Invalid initial POP3 response");
			}
		
			// send login details ...
			LoginToInbox();
		}

        public MailMessage ReadAsMailMessage()
        {
            MailMessage msg = new MailMessage();
            msg.Body = Body;
            msg.Subject = Subject;
            msg.From = From;
            msg.To = To;

            if (IsMultipart)
            {
                msg.Attachments = new List<Pop3Component>();
                IEnumerator enumerator = MultipartEnumerator;
                while (enumerator.MoveNext())
                {
                    Pop3Component multipart = (Pop3Component)enumerator.Current;
                    if (multipart.IsBody)
                        msg.Body = multipart.Data;
                    else
                        msg.Attachments.Add(multipart);
                }
            }

            return msg;
        }
	}

    public class MailMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<Pop3Component> Attachments { get; set; }
    }
}

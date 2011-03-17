using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cinar.POP3
{
	/// <summary>
	/// DLM: Stores the From:, To:, Subject:, body and attachments
	/// within an email. Binary attachments are Base64-decoded
	/// </summary>

	public class Pop3Message
	{
		private Socket m_client;

		private Pop3MessageComponents m_messageComponents;

		private string m_from;
		private string m_to;
		private string m_subject;
		private string m_contentType;
		private string m_body;

		private bool m_isMultipart = false;

		private string m_multipartBoundary;
		
		private const int m_fromState=0;
		private const int m_toState=1;
		private const int m_subjectState = 2;
		private const int m_contentTypeState = 3;
		private const int m_notKnownState = -99;
		private const int m_endOfHeader = -98;

		// this array corresponds with above
		// enumerator ...

		private string[] m_lineTypeString =
		{
			"From",
			"To",
			"Subject",
			"Content-Type"
		};
		
		private long m_messageSize = 0;
		private long m_inboxPosition = 0;

		Pop3StateObject m_pop3State = null;

		ManualResetEvent m_manualEvent = new ManualResetEvent(false);

		public IEnumerator MultipartEnumerator
		{
			get { return m_messageComponents.ComponentEnumerator; }
		}

		public bool IsMultipart
		{
			get { return m_isMultipart; }
		}

		public string From
		{
			get { return m_from; }
		}

		public string To
		{
			get { return m_to; }
		}

		public string Subject
		{
			get { return m_subject; }
		}

		public string Body
		{
			get { return m_body; }
		}

		public long InboxPosition
		{
			get { return m_inboxPosition; }
		}

		//send the data to server
		private void Send(String data) 
		{
			try
			{
				// Convert the string data to byte data 
				// using ASCII encoding.
				
				byte[] byteData = Encoding.ASCII.GetBytes(data+"\r\n");
				
				// Begin sending the data to the remote device.
				m_client.Send(byteData);
			}
			catch(Exception e)
			{
				throw new Pop3SendException(e.ToString());
			}
		}

		private void StartReceiveAgain(string data)
		{
			// receive more data if we expect more.
			// note: a literal "." (or more) followed by
			// "\r\n" in an email is prefixed with "." ...

			if( !data.EndsWith("\r\n.\r\n") )
			{
				m_client.BeginReceive(m_pop3State.buffer,0,
					Pop3StateObject.BufferSize,0,
					new AsyncCallback(ReceiveCallback),
					m_pop3State);
			}
			else
			{
				// stop receiving data ...
				m_manualEvent.Set();
			}
		}

		private void ReceiveCallback( IAsyncResult ar ) 
		{
			try 
			{
				// Retrieve the state object and the client socket 
				// from the asynchronous state object.
				
				Pop3StateObject stateObj = 
					(Pop3StateObject) ar.AsyncState;
				
				Socket client = stateObj.workSocket;
				
				// Read data from the remote device.
				int bytesRead = client.EndReceive(ar);

				if (bytesRead > 0) 
				{
					// There might be more data, 
					// so store the data received so far.
					
					stateObj.sb.Append(
						Encoding.ASCII.GetString(stateObj.buffer
						,0,bytesRead));

					// read more data from pop3 server ...
					StartReceiveAgain(stateObj.sb.ToString());
				}
			} 
			catch (Exception e) 
			{
				m_manualEvent.Set();

				throw new 
					Pop3ReceiveException("RecieveCallback error" + 
					e.ToString());
			}
		}

		private void StartReceive()
		{
			// start receiving data ...
			m_client.BeginReceive(m_pop3State.buffer,0,
				Pop3StateObject.BufferSize,0,
				new AsyncCallback(ReceiveCallback),
				m_pop3State);

			// wait until no more data to be read ...
			m_manualEvent.WaitOne();
		}

		private int GetHeaderLineType(string line)
		{
			int lineType = m_notKnownState;

			for(int i=0; i<m_lineTypeString.Length; i++)
			{
				string match = m_lineTypeString[i];

				if( Regex.Match(line,"^"+match+":"+".*$").Success )
				{
					lineType = i;
					break;
				}
				else
				if( line.Length == 0 )
				{
					lineType = m_endOfHeader;
					break;
				}
			}

			return lineType;
		}

		private long ParseHeader(string[] lines)
		{
			int numberOfLines = lines.Length;
			long bodyStart = 0;

			for(int i=0; i<numberOfLines; i++)
			{
				string currentLine = lines[i].Replace("\n","");

				int lineType = GetHeaderLineType(currentLine);

				switch(lineType)
				{
					// From:
					case m_fromState:
							m_from = Pop3Parse.From(currentLine);	
					break;

					// Subject:
					case m_subjectState:
							m_subject =	Pop3Parse.Subject(currentLine);
					break;

					// To:
					case m_toState:
							m_to = Pop3Parse.To(currentLine);
					break;

					// Content-Type
					case m_contentTypeState:
							
						m_contentType = 
							Pop3Parse.ContentType(currentLine);
						
						m_isMultipart = 
							Pop3Parse.IsMultipart(m_contentType);

						if(m_isMultipart)
						{
							// if boundary definition is on next
							// line ...

							if(m_contentType
								.Substring(m_contentType.Length-1,1).
								Equals(";"))
							{
								++i;

								m_multipartBoundary
									= Pop3Parse.
									MultipartBoundary(lines[i].
									Replace("\n",""));
							}
							else
							{
								// boundary definition is on same
								// line as "Content-Type" ...

								m_multipartBoundary =
									Pop3Parse
									.MultipartBoundary(m_contentType);
							}
						}

					break;

					case m_endOfHeader:
							bodyStart = i+1;
					break;
				}

				if(bodyStart>0)
				{
					break;
				}
			}

			return(bodyStart);
		}
		
		private void ParseEmail(string[] lines)
		{
			long startOfBody = ParseHeader(lines);
			long numberOfLines = lines.Length;

			m_messageComponents = 
				new Pop3MessageComponents(lines,startOfBody
				,m_multipartBoundary,m_contentType);
		}

		private void LoadEmail()
		{
			// tell pop3 server we want to start reading
			// email (m_inboxPosition) from inbox ...

			Send("retr "+m_inboxPosition);

			// start receiving email ...
			StartReceive();

			// parse email ...
			ParseEmail(
				m_pop3State.sb.ToString().Split(new char[] { '\r'}));

			// remove reading pop3State ...
			m_pop3State = null;
		}

		public Pop3Message(long position, long size, Socket client)
		{
			m_inboxPosition = position;
			m_messageSize = size;
			m_client = client;

			m_pop3State = new Pop3StateObject();
			m_pop3State.workSocket = m_client;
			m_pop3State.sb = new StringBuilder();

			// load email ...
			LoadEmail();

			// get body (if it exists) ...
			IEnumerator multipartEnumerator =
				MultipartEnumerator;

			while( multipartEnumerator.MoveNext() )
			{
				Pop3Component multipart = (Pop3Component)
					multipartEnumerator.Current;

				if( multipart.IsBody )
				{
					m_body = multipart.Data;
					break;
				}
			}
		}

		public override string ToString()
		{
			IEnumerator enumerator = MultipartEnumerator;

			string ret = 
				"From    : "+m_from+ "\r\n"+
				"To      : "+m_to+ "\r\n"+
				"Subject : "+m_subject+"\r\n";

			while( enumerator.MoveNext() )
			{
				ret += ((Pop3Component)enumerator.Current).ToString()+"\r\n";
			}
	
			return ret;
		}
	}
}

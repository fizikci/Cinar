using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cinar.POP3
{
	/// <summary>
	/// Holds the current state of the client
	/// socket.
	/// </summary>

	public class Pop3StateObject
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
}

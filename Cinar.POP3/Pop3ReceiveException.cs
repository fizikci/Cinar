using System;

namespace Cinar.POP3
{
	public class Pop3ReceiveException : Exception
	{
		private string m_exceptionString;

		public Pop3ReceiveException(): base()
		{
			m_exceptionString = null;
		}

		public Pop3ReceiveException(string exceptionString): base()
		{
			m_exceptionString = exceptionString;
		}

		public Pop3ReceiveException(string exceptionString,
			Exception ex) : base(exceptionString,ex)
		{
		}

		public override string ToString()
		{
			return m_exceptionString;
		}
	}
}

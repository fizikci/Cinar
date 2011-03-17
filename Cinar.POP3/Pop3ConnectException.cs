using System;

namespace Cinar.POP3
{
	public class Pop3ConnectException : Exception
	{
		private string m_exceptionString;

		public Pop3ConnectException(): base()
		{
			m_exceptionString = null;
		}

		public Pop3ConnectException(string exceptionString): base()
		{
			m_exceptionString = exceptionString;
		}

		public Pop3ConnectException(string exceptionString,
			Exception ex) : base(exceptionString,ex)
		{
		}

		public override string ToString()
		{
			return m_exceptionString;
		}
	}
}

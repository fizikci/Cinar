using System;

namespace Cinar.POP3
{
	public class Pop3SendException : Exception
	{
		private string m_exceptionString;

		public Pop3SendException(): base()
		{
			m_exceptionString = null;
		}

		public Pop3SendException(string exceptionString): base()
		{
			m_exceptionString = exceptionString;
		}

		public Pop3SendException(string exceptionString,
			Exception ex) : base(exceptionString,ex)
		{
		}

		public override string ToString()
		{
			return m_exceptionString;
		}
	}
}

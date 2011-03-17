using System;

namespace Cinar.POP3
{
	public class Pop3MessageException : Exception
	{
		private string m_exceptionString;

		public Pop3MessageException(): base()
		{
			m_exceptionString = null;
		}

		public Pop3MessageException(string exceptionString): base()
		{
			m_exceptionString = exceptionString;
		}

		public Pop3MessageException(string exceptionString,
			Exception ex) : base(exceptionString,ex)
		{
		}

		public override string ToString()
		{
			return m_exceptionString;
		}
	}
}

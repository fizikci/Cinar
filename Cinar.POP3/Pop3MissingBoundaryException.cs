using System;

namespace Cinar.POP3
{
	public class Pop3MissingBoundaryException : Exception
	{
		private string m_exceptionString;

		public Pop3MissingBoundaryException(): base()
		{
			m_exceptionString = null;
		}

		public Pop3MissingBoundaryException(string exceptionString): base()
		{
			m_exceptionString = exceptionString;
		}

		public Pop3MissingBoundaryException(string exceptionString,
			Exception ex) : base(exceptionString,ex)
		{
		}

		public override string ToString()
		{
			return m_exceptionString;
		}
	}
}

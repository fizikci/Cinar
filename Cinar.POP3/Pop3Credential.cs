using System;

namespace Cinar.POP3
{
	/// <summary>
	/// Summary description for Credentials.
	/// </summary>
	public class Pop3Credential
	{
		private string m_user;
		private string m_pass;
        private string m_server;

		public string User
		{
			set { m_user = value; }
			get { return m_user; }
		}

		public string Pass
		{
			set { m_pass = value; }
			get { return m_pass; }
		}

		public string Server
		{
			set { m_server = value; }
			get { return m_server; }
		}

        public Pop3Credential(string user, string pass, string server)
        {
            m_user = user;
            m_pass = pass;
            m_server = server;
        }

		public Pop3Credential()
		{
			m_user = null;
			m_pass = null;
			m_server = null;
		}
	}
}

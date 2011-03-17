using System;
using System.Text.RegularExpressions;

namespace Cinar.POP3
{
	/// <summary>
	/// Summary description for Pop3ParseMessage.
	/// </summary>
	public class Pop3Parse
	{
		private static string[] m_lineUpperTypeString =
		{ 
			"From",
			"To",
			"Subject",
			"Content-Type"
		};

		private static string[] m_lineSubTypeString =
		{
			"Content-Type",
			"Content-Transfer-Encoding",
			"Content-Description",
			"Content-Disposition"
		};

		private static string[] m_nextLineTypeString =
		{
			"name",
			"filename"
		};

		// Mapping to lineSubTypeString ...
		public const int ContentTypeType = 0;
		public const int ContentTransferEncodingType = 1;
		public const int ContentDescriptionType = 2;
		public const int ContentDispositionType = 3;

		// Mapping to nextLineTypeString ...
		public const int NameType = 0;
		public const int FilenameType = 1;

		// Non-string mappers ...
		public const int UnknownType = -99;
		public const int EndOfHeader = -98;
		public const int MultipartBoundaryFound = -97;
		public const int ComponetsDone = -96;

		public static string[] LineUpperTypeString
		{
			get { return m_lineUpperTypeString; }
		}

		public static string[] LineSubTypeString
		{
			get { return m_lineSubTypeString; }
		}

		public static string[] NextLineTypeString
		{
			get { return m_nextLineTypeString; }
		}

		public static string From(string line)
		{
			return
				Regex.Replace(line
				,@"^From:.*[ |<]([a-z|A-Z|0-9|\.|\-|_]+@[a-z|A-Z|0-9|\.|\-|_]+).*$"
				,"$1");
		}

		public static string Subject(string line)
		{
			return
				Regex.Replace(line
				,@"^Subject: (.*)$"
				,"$1");
		}

		public static string To(string line)
		{
			return
				Regex.Replace(line
				,@"^To:.*[ |<]([a-z|A-Z|0-9|\.|\-|_]+@[a-z|A-Z|0-9|\.|\-|_]+).*$"
				,"$1");
		}

		public static string ContentType(string line)
		{
			return
				Regex.Replace(line
				,@"^Content-Type: (.*)$"
				,"$1");
		}

		public static string ContentTransferEncoding(string line)
		{
			return
				Regex.Replace(line
				,@"^Content-Transfer-Encoding: (.*)$"
				,"$1");
		}

		public static string ContentDescription(string line)
		{
			return
				Regex.Replace(line
				,@"^Content-Description: (.*)$"
				,"$1");
		}

		public static string ContentDisposition(string line)
		{
			return
				Regex.Replace(line
				,@"^Content-Disposition: (.*)$"
				,"$1");
		}

		public static bool IsMultipart(string line)
		{
			return
				Regex.Match(line,"^multipart/.*").Success;
		}

		public static string MultipartBoundary(string line)
		{
			return
				Regex.Replace(line
				,"^.*boundary=[\"]*([^\"]*).*$"
				,"$1");
		}

		public static string Name(string line)
		{
			return Regex.Replace(line,
				"^[ |	]+name=[\"]*([^\"]*).*$","$1");
		}

		public static string Filename(string line)
		{
			return Regex.Replace(line,
				"^[ |	]+filename=[\"]*([^\"]*).*$","$1");
		}

		public static int GetSubHeaderNextLineType(string line)
		{
			int lineType = Pop3Parse.UnknownType;

			for(int i=0; i<Pop3Parse.NextLineTypeString.Length; i++)
			{
				string match = Pop3Parse.NextLineTypeString[i];

				if( Regex.Match(line,"^[ |	]+"+match+"="+".*$").Success )
				{
					lineType = i;
					break;
				}
				if( line.Length == 0 )
				{
					lineType = Pop3Parse.EndOfHeader;
					break;
				}
			}

			return lineType;
		}

		public static int GetSubHeaderLineType(string line,
			string boundary)
		{
			int lineType = Pop3Parse.UnknownType;

			for(int i=0; i<Pop3Parse.LineSubTypeString.Length; i++)
			{
				string match = Pop3Parse.LineSubTypeString[i];

				if( Regex.Match(line,"^"+match+":"+".*$").Success )
				{
					lineType = i;
					break;
				}
				else
				if( line.Equals("--"+boundary) )
				{
					lineType = Pop3Parse.MultipartBoundaryFound;
					break;
				}
				else
				if( line.Equals("--"+boundary+"--") )
				{
					lineType = Pop3Parse.ComponetsDone;
					break;
				}
				if( line.Length == 0 )
				{
					lineType = Pop3Parse.EndOfHeader;
					break;
				}
			}

			return lineType;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Cinar.Extensions
{
    public class FtpClient
    {
        internal string url;
        internal string password;
        internal string userName;

        public FtpClient(string url, string userName, string password)
        {
            this.url = url;
            this.userName = userName;
            this.password = password;

            Root = new FileItem();
            Root.Permissions = "d";
            Root.ftp = this;
        }

        public FileItem Root;
    }

    public class FileItem
    {
        public string Permissions;
        public int Count;
        public string Op1;
        public string Op2;
        public long Size;
        private string Month;
        private string Day;
        private string Year;

        public string FileName;

        internal FtpClient ftp;
        internal string ParentFolder = "";

        public bool IsDirectory
        {
            get
            {
                return Permissions.StartsWith("d");
            }
        }

        internal static FileItem Parse(FtpClient ftp, string parentFolder, string data)
        {
            string pattern = @"
^                       # Beginning Anchor
(?<Permissions>[^\s]+)  # Get permissions into named capture
(?:\s+)                 # Match but don't capture space
(?<Count>\d+)
(?:\s+)
(?<Op1>[^\s]+)          # Continue with capturing valued text into named
(?:\s+)                 # captures and matching, but not capturing space which is ignored.
(?<Op2>[^\s]+)
(?:\s+)
(?<Size>[^\s]+)
(?:\s+)
(?<Month>[^\s]+)
(?:\s+)
(?<Day>[^\s]+)
(?:\s+)
(?<Year>[^\s]+)
(?:\s+)
(?<FileName>[^\r\n]+)";

            // Ignore option only applies to the pattern so we can comment it.
            var mtGroup = Regex.Match(data, pattern, RegexOptions.IgnorePatternWhitespace).Groups;

            FileItem fi = new FileItem();
            fi.Permissions = mtGroup["Permissions"].Value;
            fi.Count = int.Parse(mtGroup["Count"].Value);
            fi.Permissions = mtGroup["Permissions"].Value;
            fi.Op1 = mtGroup["Op1"].Value;
            fi.Op2 = mtGroup["Op2"].Value;
            fi.Size = long.Parse(mtGroup["Size"].Value);
            fi.Month = mtGroup["Month"].Value;
            fi.Day = mtGroup["Day"].Value;
            fi.Year = mtGroup["Year"].Value;
            fi.FileName = mtGroup["FileName"].Value;

            fi.ParentFolder = parentFolder;

            fi.ftp = ftp;

            return fi;
        }


        public List<FileItem> ListFiles()
        {
            if (!this.IsDirectory)
                return new List<FileItem>();


            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.url + "/" + this.ParentFolder + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(ftp.userName, ftp.password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string res = reader.ReadToEnd();

            //Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();

            return res.Replace("\r", "").Split('\n').Where(l => !String.IsNullOrWhiteSpace(l)).Select(l => FileItem.Parse(ftp, this.ParentFolder + "/" + this.FileName, l)).ToList();
        }
        public bool DeleteFile()
        {
            if (this.IsDirectory)
                return false;

            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.url + "/" + this.ParentFolder + "/" + FileName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(ftp.userName, ftp.password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UploadFile(string localFilePath, string ftpFileName)
        {
            if (!this.IsDirectory)
                return false;
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.url + "/" + this.ParentFolder + "/" + FileName + "/" + ftpFileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(ftp.userName, ftp.password);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(localFilePath);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DownloadFile(string localFilePath)
        {
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.url + "/" + this.ParentFolder + "/" + FileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(ftp.userName, ftp.password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                using (Stream file = File.OpenWrite(localFilePath))
                {
                    responseStream.CopyTo(file);
                }

                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}

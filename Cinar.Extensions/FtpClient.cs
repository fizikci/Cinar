using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Cinar.Extensions
{
    public class FtpClient
    {
        internal string url;
        internal string password;
        internal string userName;

        public FtpClient(string url, string userName, string password)
        {
            if (!url.StartsWith("ftp://", StringComparison.InvariantCultureIgnoreCase)) url = "ftp://" + url;
            this.url = url;
            this.userName = userName;
            this.password = password;

            Root = new FileItem();
            Root.Permissions = "d";
            Root.ftp = this;
        }

        public FileItem Root;

        public bool DeleteFile(string path)
        {
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" +path);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(this.userName, this.password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool FileExists(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + path);
            request.Credentials = new NetworkCredential(this.userName, this.password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
                throw ex;
            }
            return true;
        }

        public bool UploadFile(string folder, string localFilePath, string uploadFileNameAs=null)
        {
            try
            {
                string fileNameAs = uploadFileNameAs ?? Path.GetFileName(localFilePath);
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(this.url + folder + fileNameAs);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(userName, password);

                // Copy the contents of the file to the request stream.
                request.ContentLength = new FileInfo(localFilePath).Length;

                using (Stream sourceStream = File.OpenRead(localFilePath))
                {
                    using (Stream output = request.GetRequestStream())
                    {
                        sourceStream.CopyTo(output, 1*1024*1024);
                    }
                }


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
        public string ParentFolder = "";

        public string FullPath
        {
            get {
                return ftp.url + ParentFolder + "/" + FileName;
            }
        }

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
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.url + this.ParentFolder + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(ftp.userName, ftp.password);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string res = reader.ReadToEnd();

                        //Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);
                        return res.Replace("\r", "").Split('\n').Where(l => !String.IsNullOrWhiteSpace(l)).Select(l => FileItem.Parse(ftp, this.ParentFolder + "/" + this.FileName, l)).ToList();
                    }
                }
            }

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

                Directory.CreateDirectory(Path.GetDirectoryName(localFilePath));

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

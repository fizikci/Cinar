using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Cinar.Zip
{
    public class ZipUtility
    {
        /// <summary>
        /// Compresses the files in the nominated folder, and creates a zip file on disk named as outPathname.
        /// </summary>
        /// <param name="outPathname"></param>
        /// <param name="password"></param>
        /// <param name="folderName"></param>
        public static void CreateSample(string outPathname, string password, string folderName)
        {
            using (FileStream fsOut = File.Create(outPathname))
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
                {

                    zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

                    zipStream.Password = password; // optional. Null is the same as not setting.

                    // This setting will strip the leading part of the folder path in the entries, to
                    // make the entries relative to the starting folder.
                    // To include the full path for each entry up to the drive root, assign folderOffset = 0.
                    int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

                    CompressFolder(folderName, zipStream, folderOffset);
                }
            }
        }

        /// <summary>
        /// Recurses down the folder structure
        /// </summary>
        /// <param name="path"></param>
        /// <param name="zipStream"></param>
        /// <param name="folderOffset"></param>
        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }

        /// <summary>
        /// Compresses the supplied memory stream, naming it as zipEntryName, into a zip,
        /// which is returned as a memory stream or a byte array.
        /// </summary>
        /// <param name="memStreamIn"></param>
        /// <param name="zipEntryName"></param>
        /// <returns></returns>
        public static MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName)
        {

            MemoryStream outputMemStream = new MemoryStream();
            using (ZipOutputStream zipStream = new ZipOutputStream(outputMemStream))
            {

                zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

                ZipEntry newEntry = new ZipEntry(zipEntryName);
                newEntry.DateTime = DateTime.Now;

                zipStream.PutNextEntry(newEntry);

                StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
                zipStream.CloseEntry();

                zipStream.IsStreamOwner = false; // False stops the Close also Closing the underlying stream.
            }

            outputMemStream.Position = 0;
            return outputMemStream;
        }

        /// <summary>
        /// This will accumulate each of the files named in the fileList into a zip file,
        /// and stream it to the browser.
        /// This approach writes directly to the Response OutputStream.
        /// The browser starts to receive data immediately which should avoid timeout problems.
        /// This also avoids an intermediate memorystream, saving memory on large files.        /// </summary>
        /// <param name="zipFileList"></param>
        public static void DownloadZipToBrowser(IEnumerable<string> zipFileList, HttpContext context)
        {

            context.Response.ContentType = "application/zip";
            // If the browser is receiving a mangled zipfile, IIS Compression may cause this problem. Some members have found that
            //    Response.ContentType = "application/octet-stream"     has solved this. May be specific to Internet Explorer.

            context.Response.AppendHeader("content-disposition", "attachment; filename=\"Download.zip\"");
            context.Response.CacheControl = "Private";
            context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(3)); // or put a timestamp in the filename in the content-disposition

            byte[] buffer = new byte[4096];

            ZipOutputStream zipOutputStream = new ZipOutputStream(context.Response.OutputStream);
            zipOutputStream.SetLevel(3); //0-9, 9 being the highest level of compression

            foreach (string fileName in zipFileList)
            {

                Stream fs = File.OpenRead(fileName);	// or any suitable inputstream

                ZipEntry entry = new ZipEntry(ZipEntry.CleanName(fileName));
                entry.Size = fs.Length;
                // Setting the Size provides WinXP built-in extractor compatibility,
                //  but if not available, you can set zipOutputStream.UseZip64 = UseZip64.Off instead.

                zipOutputStream.PutNextEntry(entry);

                int count = fs.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    zipOutputStream.Write(buffer, 0, count);
                    count = fs.Read(buffer, 0, buffer.Length);
                    if (!context.Response.IsClientConnected)
                    {
                        break;
                    }
                    context.Response.Flush();
                }
                fs.Close();
            }
            zipOutputStream.Close();

            context.Response.Flush();
            context.Response.End();
        }

        public static void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;		// AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;			// Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];		// 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }


        /// <summary>
        /// // Calling example:
        /// WebClient webClient = new WebClient();
        /// Stream data = webClient.OpenRead("http://www.example.com/test.zip");
        /// //This stream cannot be opened with the ZipFile class because CanSeek is false.
        /// GetViaZipInput(data, @"c:\temp");
        /// </summary>
        /// <param name="zipStream"></param>
        /// <param name="outFolder"></param>
        public static void GetViaZipInput(Stream zipStream, string outFolder)
        {

            ZipInputStream zipInputStream = new ZipInputStream(zipStream);
            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            while (zipEntry != null)
            {
                String entryFileName = zipEntry.Name;
                // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                // Optionally match entrynames against a selection list here to skip as desired.
                // The unpacked length is available in the zipEntry.Size property.

                byte[] buffer = new byte[4096];		// 4K is optimum

                // Manipulate the output filename here as desired.
                String fullZipToPath = Path.Combine(outFolder, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                // of the file, but does not waste memory.
                // The "using" will close the stream even if an exception occurs.
                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                }
                zipEntry = zipInputStream.GetNextEntry();
            }
        }
    }
}

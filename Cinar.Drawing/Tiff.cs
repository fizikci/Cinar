using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace Cinar.Drawing
{
    public class TiffUtil
    {
        public static Image Save(Image[] bmp, string location)
        {
            ImageConverter converter = new ImageConverter();

            return (Image)converter.ConvertFrom(SaveData(bmp, location));
        }

        public static byte[] SaveData(Image[] bmp, string location)
        {
            MemoryStream ms = new MemoryStream();
            if (bmp != null)
            {
                try
                {
                    ImageCodecInfo codecInfo = getCodec();

                    for (int i = 0; i < bmp.Length; i++)
                    {
                        if (bmp[i] == null)
                            break;
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);
                    }

                    if (bmp.Length == 1)
                    {
                        EncoderParameters iparams = new EncoderParameters(1);
                        Encoder iparam = Encoder.Compression;
                        EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionCCITT4));
                        iparams.Param[0] = iparamPara;
                        if (string.IsNullOrEmpty(location))
                            bmp[0].Save(ms, codecInfo, iparams);
                        else
                            bmp[0].Save(location, codecInfo, iparams);
                    }
                    else if (bmp.Length > 1)
                    {
                        Encoder saveEncoder;
                        Encoder compressionEncoder;
                        EncoderParameter SaveEncodeParam;
                        EncoderParameter CompressionEncodeParam;
                        EncoderParameters EncoderParams = new EncoderParameters(2);

                        saveEncoder = Encoder.SaveFlag;
                        compressionEncoder = Encoder.Compression;

                        // Save the first page (frame).
                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                        CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                        EncoderParams.Param[0] = CompressionEncodeParam;
                        EncoderParams.Param[1] = SaveEncodeParam;

                        if (string.IsNullOrEmpty(location))
                        {
                            bmp[0].Save(ms, codecInfo, EncoderParams);
                        }
                        else
                        {
                            File.Delete(location);
                            bmp[0].Save(location, codecInfo, EncoderParams);
                        }

                        for (int i = 1; i < bmp.Length; i++)
                        {
                            if (bmp[i] == null)
                                break;

                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                            CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                            EncoderParams.Param[0] = CompressionEncodeParam;
                            EncoderParams.Param[1] = SaveEncodeParam;
                            bmp[0].SaveAdd(bmp[i], EncoderParams);
                        }

                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                        EncoderParams.Param[0] = SaveEncodeParam;
                        bmp[0].SaveAdd(EncoderParams);

                        bmp[0].Save(ms, codecInfo, EncoderParams);
                    }
                    if (string.IsNullOrEmpty(location))
                        return ms.ToArray();
                    else
                        return File.ReadAllBytes(location);
                }
                catch (System.Exception ee)
                {
                    throw new Exception(ee.Message + "  Error in saving as multipage ");
                }
            }
            else
                return null;

        }

        public static Image SaveToExistingFile(string fileName, Image[] bmp)
        {
            try
            {
                //bmp[0] is containing Image from Existing file on which we will append newly scanned Images
                //SO first we will dicide wheter existing file is single page or multipage

                Image origionalFile = null;

                FileStream fr = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite);
                origionalFile = Image.FromStream(fr);
                int PageNumber = GetPageNumber(origionalFile);

                if (bmp != null)
                {
                    for (int i = 0; i < bmp.Length; i++)
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);

                    if (PageNumber > 1)//Existing File is multi page tiff file
                        saveImageExistingMultiplePage(bmp, origionalFile, PageNumber, "shreeTemp.tif");
                    else if (PageNumber == 1)//Existing file is single page file
                        saveImageExistingSinglePage(bmp, origionalFile, "shreeTemp.tif");
                }
                else
                    throw new Exception("Please give existing File and newly scanned image");

                fr.Flush();
                fr.Close();

                System.IO.File.Replace("shreeTemp.tif", fileName, "Backup.tif", true);


                return origionalFile;
            }
            catch (System.Exception ee)
            {
                throw new Exception(ee.Message + " Error in saving as multipage ");
            }
        }

        public static int GetPageNumber(Image img)
        {
            Guid objGuid = img.FrameDimensionsList[0];
            FrameDimension objDimension = new FrameDimension(objGuid);

            //Gets the total number of frames in the .tiff file
            int PageNumber = img.GetFrameCount(objDimension);

            return PageNumber;
        }

        public static Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            // Lock source bitmap in memory
            BitmapData sourceData = source.LockBits(new System.Drawing.Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Copy image data to binary array
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            // Unlock source bitmap
            source.UnlockBits(sourceData);

            // Create destination bitmap
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(new System.Drawing.Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = source.Height;
            int width = source.Width;
            int threshold = 500;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] + sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Return
            return destination;
        }

        private static ImageCodecInfo getCodec()
        {
            string type = "TIFF";
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string enumName = type.ToString();
                if (info[i].FormatDescription.Equals(enumName))
                    return info[i];
            }

            return null;
        }
        private static void saveImageExistingSinglePage(Image[] bmp, Image origionalFile, string location)
        {
            try
            {
                //Now load the Codecs 
                ImageCodecInfo codecInfo = getCodec();

                Encoder saveEncoder;
                Encoder compressionEncoder;
                EncoderParameter saveEncodeParam;
                EncoderParameter compressionEncodeParam;
                EncoderParameters encoderParams = new EncoderParameters(2);

                saveEncoder = Encoder.SaveFlag;
                compressionEncoder = Encoder.Compression;

                // Save the first page (frame).
                saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                encoderParams.Param[0] = compressionEncodeParam;
                encoderParams.Param[1] = saveEncodeParam;

                origionalFile = ConvertToBitonal((Bitmap)origionalFile);
                origionalFile.Save(location, codecInfo, encoderParams);

                for (int i = 0; i < bmp.Length; i++)
                {
                    saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    encoderParams.Param[0] = compressionEncodeParam;
                    encoderParams.Param[1] = saveEncodeParam;
                    origionalFile.SaveAdd(bmp[i], encoderParams);
                }

                saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                encoderParams.Param[0] = saveEncodeParam;
                origionalFile.SaveAdd(encoderParams);
            }
            catch (System.Exception ee)
            {
                throw ee;
            }
        }
        private static void saveImageExistingMultiplePage(Image[] bmp, Image origionalFile, int PageNumber, string location)
        {
            try
            {
                //Now load the Codecs 
                ImageCodecInfo codecInfo = getCodec();

                Encoder saveEncoder;
                Encoder compressionEncoder;
                EncoderParameter saveEncodeParam;
                EncoderParameter compressionEncodeParam;
                EncoderParameters encoderParams = new EncoderParameters(2);
                Bitmap pages;
                Bitmap nextPage;

                saveEncoder = Encoder.SaveFlag;
                compressionEncoder = Encoder.Compression;

                origionalFile.SelectActiveFrame(FrameDimension.Page, 0);
                pages = new Bitmap(origionalFile);
                pages = ConvertToBitonal(pages);

                // Save the first page (frame).
                saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                encoderParams.Param[0] = compressionEncodeParam;
                encoderParams.Param[1] = saveEncodeParam;

                pages.Save(location, codecInfo, encoderParams);

                for (int i = 1; i < PageNumber; i++)
                {
                    saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    encoderParams.Param[0] = compressionEncodeParam;
                    encoderParams.Param[1] = saveEncodeParam;

                    origionalFile.SelectActiveFrame(FrameDimension.Page, i);
                    nextPage = new Bitmap(origionalFile);
                    nextPage = ConvertToBitonal(nextPage);
                    pages.SaveAdd(nextPage, encoderParams);
                }

                for (int i = 0; i < bmp.Length; i++)
                {
                    saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                    compressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                    encoderParams.Param[0] = compressionEncodeParam;
                    encoderParams.Param[1] = saveEncodeParam;
                    bmp[i] = (Bitmap)ConvertToBitonal((Bitmap)bmp[i]);
                    pages.SaveAdd(bmp[i], encoderParams);
                }

                saveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                encoderParams.Param[0] = saveEncodeParam;
                pages.SaveAdd(encoderParams);
            }
            catch (System.Exception ee)
            {
                throw ee;
            }
        }

        public static Image GetPage(Image image, int i)
        {
            Guid objGuid = image.FrameDimensionsList[0];
            FrameDimension objDimension = new FrameDimension(objGuid);

            image.SelectActiveFrame(objDimension, i);

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);

            Image pageImage = Image.FromStream(ms);
            return pageImage;
        }
    }


    public class TiffUtil2
    {
        #region Variable & Class Definitions

        private static System.Drawing.Imaging.ImageCodecInfo tifImageCodecInfo;

        private static EncoderParameter tifEncoderParameterMultiFrame;
        private static EncoderParameter tifEncoderParameterFrameDimensionPage;
        private static EncoderParameter tifEncoderParameterFlush;
        private static EncoderParameter tifEncoderParameterCompression;
        private static EncoderParameter tifEncoderParameterLastFrame;
        private static EncoderParameter tifEncoderParameter24BPP;
        private static EncoderParameter tifEncoderParameter1BPP;

        private static EncoderParameters tifEncoderParametersPage1;
        private static EncoderParameters tifEncoderParametersPageX;
        private static EncoderParameters tifEncoderParametersPageLast;

        private static System.Drawing.Imaging.Encoder tifEncoderSaveFlag;
        private static System.Drawing.Imaging.Encoder tifEncoderCompression;
        private static System.Drawing.Imaging.Encoder tifEncoderColorDepth;

        private static bool encoderAssigned;

        public static string tempDir;
        public static bool initComplete;

        public TiffUtil2(string tempPath)
        {
            try
            {
                if (!initComplete)
                {
                    if (!tempPath.EndsWith("\\"))
                        tempDir = tempPath + "\\";
                    else
                        tempDir = tempPath;

                    Directory.CreateDirectory(tempDir);
                    initComplete = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Retrieve Page Count of a multi-page TIFF file

        public int getPageCount(string fileName)
        {
            int pageCount = -1;

            try
            {
                Image img = Bitmap.FromFile(fileName);
                pageCount = img.GetFrameCount(FrameDimension.Page);
                img.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pageCount;
        }

        public int getPageCount(Image img)
        {
            int pageCount = -1;
            try
            {
                pageCount = img.GetFrameCount(FrameDimension.Page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pageCount;
        }

        #endregion

        #region Retrieve multiple single page images from a single multi-page TIFF file

        public Image[] getTiffImages(Image sourceImage, int[] pageNumbers)
        {
            MemoryStream ms = null;
            Image[] returnImage = new Image[pageNumbers.Length];

            try
            {
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    ms = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    returnImage[i] = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ms.Close();
            }

            return returnImage;
        }

        public Image[] getTiffImages(Image sourceImage)
        {
            MemoryStream ms = null;
            int pageCount = getPageCount(sourceImage);

            Image[] returnImage = new Image[pageCount];

            try
            {
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                for (int i = 0; i < pageCount; i++)
                {
                    ms = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, i);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    returnImage[i] = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ms.Close();
            }

            return returnImage;
        }

        public Image[] getTiffImages(string sourceFile, int[] pageNumbers)
        {
            Image[] returnImage = new Image[pageNumbers.Length];

            try
            {
                Image sourceImage = Bitmap.FromFile(sourceFile);
                returnImage = getTiffImages(sourceImage, pageNumbers);
                sourceImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImage = null;
            }

            return returnImage;
        }

        #endregion

        #region Retrieve a specific page from a multi-page TIFF image

        public Image getTiffImage(string sourceFile, int pageNumber)
        {
            Image returnImage = null;

            try
            {
                Image sourceImage = Image.FromFile(sourceFile);
                returnImage = getTiffImage(sourceImage, pageNumber);
                sourceImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImage = null;
            }

            return returnImage;
        }

        public Image getTiffImage(Image sourceImage, int pageNumber)
        {
            MemoryStream ms = null;
            Image returnImage = null;

            try
            {
                ms = new MemoryStream();
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);
                sourceImage.SelectActiveFrame(objDimension, pageNumber);
                sourceImage.Save(ms, ImageFormat.Tiff);
                returnImage = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ms.Close();
            }

            return returnImage;
        }

        public bool getTiffImage(string sourceFile, string targetFile, int pageNumber)
        {
            bool response = false;

            try
            {
                Image returnImage = getTiffImage(sourceFile, pageNumber);
                returnImage.Save(targetFile);
                returnImage.Dispose();
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        #endregion

        #region Split a multi-page TIFF file into multiple single page TIFF files

        public string[] splitTiffPages(string sourceFile, string targetDirectory)
        {
            string[] returnImages;

            try
            {
                Image sourceImage = Bitmap.FromFile(sourceFile);
                Image[] sourceImages = splitTiffPages(sourceImage);

                int pageCount = sourceImages.Length;

                returnImages = new string[pageCount];
                for (int i = 0; i < pageCount; i++)
                {
                    FileInfo fi = new FileInfo(sourceFile);
                    string babyImg = targetDirectory + "\\" + fi.Name.Substring(0, (fi.Name.Length - fi.Extension.Length)) + "_PAGE" + (i + 1).ToString().PadLeft(3, '0') + fi.Extension;
                    sourceImages[i].Save(babyImg);
                    returnImages[i] = babyImg;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImages = null;
            }

            return returnImages;
        }

        public Image[] splitTiffPages(Image sourceImage)
        {
            Image[] returnImages;

            try
            {
                int pageCount = getPageCount(sourceImage);
                returnImages = new Image[pageCount];

                for (int i = 0; i < pageCount; i++)
                {
                    Image img = getTiffImage(sourceImage, i);
                    returnImages[i] = (Image)img.Clone();
                    img.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImages = null;
            }

            return returnImages;
        }

        #endregion

        #region Merge multiple single page TIFF to a single multi page TIFF

        public bool mergeTiffPages(string[] sourceFiles, string targetFile)
        {
            bool response = false;

            try
            {
                assignEncoder();

                // If only 1 page was passed, copy directly to output
                if (sourceFiles.Length == 1)
                {
                    File.Copy(sourceFiles[0], targetFile, true);
                    return true;
                }

                int pageCount = sourceFiles.Length;

                // First page
                Image finalImage = Image.FromFile(sourceFiles[0]);
                finalImage.Save(targetFile, tifImageCodecInfo, tifEncoderParametersPage1);

                // All other pages
                for (int i = 1; i < pageCount; i++)
                {
                    Image img = Image.FromFile(sourceFiles[i]);
                    finalImage.SaveAdd(img, tifEncoderParametersPageX);
                    img.Dispose();
                }

                // Last page
                finalImage.SaveAdd(tifEncoderParametersPageLast);
                finalImage.Dispose();
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = false;
            }

            return response;
        }

        public byte[] myMergeTiffStreams(MemoryStream[] sourceStream)
        {
            MemoryStream targetStream = new MemoryStream();
            try
            {
                assignEncoder();

                // If only 1 page was passed, copy directly to output
                if (sourceStream.Length == 1)
                {
                    
                    return sourceStream[0].ToArray();
                }

                int pageCount = sourceStream.Length;

                // First page
                Image finalImage = Image.FromStream(sourceStream[0]);
                finalImage.Save(targetStream, tifImageCodecInfo, tifEncoderParametersPage1);

                // All other pages
                for (int i = 1; i < pageCount; i++)
                {
                    Image img = Image.FromStream(sourceStream[i]);
                    finalImage.SaveAdd(img, tifEncoderParametersPageX);
                    img.Dispose();
                }

                // Last page
                finalImage.SaveAdd(tifEncoderParametersPageLast);
                finalImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return targetStream.ToArray();
        }

        public bool mergeTiffPages(string sourceFile, string targetFile, int[] pageNumbers)
        {
            bool response = false;

            try
            {
                assignEncoder();

                // Get individual Images from the original image
                Image sourceImage = Bitmap.FromFile(sourceFile);
                MemoryStream ms = new MemoryStream();
                Image[] sourceImages = new Image[pageNumbers.Length];
                Guid guid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(guid);
                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    sourceImages[i] = Image.FromStream(ms);
                }

                // Merge individual Images into one Image
                // First page
                Image finalImage = sourceImages[0];
                finalImage.Save(targetFile, tifImageCodecInfo, tifEncoderParametersPage1);
                // All other pages
                for (int i = 1; i < pageNumbers.Length; i++)
                {
                    finalImage.SaveAdd(sourceImages[i], tifEncoderParametersPageX);
                }
                // Last page
                finalImage.SaveAdd(tifEncoderParametersPageLast);
                finalImage.Dispose();

                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public bool mergeTiffPagesAlternate(string sourceFile, string targetFile, int[] pageNumbers)
        {
            bool response = false;

            try
            {
                // Initialize the encoders, occurs once for the lifetime of the class
                assignEncoder();

                // Get individual Images from the original image
                Image sourceImage = Bitmap.FromFile(sourceFile);
                MemoryStream[] msArray = new MemoryStream[pageNumbers.Length];
                Guid guid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(guid);
                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    msArray[i] = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(msArray[i], ImageFormat.Tiff);
                }

                // Merge individual page streams into single stream
                MemoryStream ms = mergeTiffStreams(msArray);
                Image targetImage = Bitmap.FromStream(ms);
                targetImage.Save(targetFile);

                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public System.IO.MemoryStream mergeTiffStreams(System.IO.MemoryStream[] tifsStream)
        {
            EncoderParameters ep = null;
            System.IO.MemoryStream singleStream = new System.IO.MemoryStream();

                string type = "TIFF";
                ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo result = null;
                for (int i = 0; i < info.Length; i++)
                {
                    string enumName = type.ToString();
                    if (info[i].FormatDescription.Equals(enumName))
                         result = info[i];
                }
            try
            {

                assignEncoder();

                Image imgTif = Image.FromStream(tifsStream[0]);

                if (tifsStream.Length > 1)
                {
                    // Multi-Frame
                    ep = new EncoderParameters(2);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.MultiFrame);
                    ep.Param[1] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionCCITT4);
                }
                else
                {
                    // Single-Frame
                    ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionCCITT4);
                }

                //Save the first page
                imgTif.Save(singleStream, tifImageCodecInfo, ep);

                if (tifsStream.Length > 1)
                {
                    ep = new EncoderParameters(2);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.FrameDimensionPage);

                    //Add the rest of pages
                    for (int i = 1; i < tifsStream.Length; i++)
                    {
                        Image pgTif = Image.FromStream(tifsStream[i]);

                        ep.Param[1] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionCCITT4);

                        imgTif.SaveAdd(pgTif, ep);
                    }

                    ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.Flush);
                    imgTif.SaveAdd(ep);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ep != null)
                {
                    ep.Dispose();
                }
            }

            return singleStream;
        }

        #endregion

        #region Internal support functions

        private void assignEncoder()
        {
            try
            {
                if (encoderAssigned == true)
                    return;

                foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageEncoders())
                {
                    if (ici.MimeType == "image/tiff")
                    {
                        tifImageCodecInfo = ici;
                    }
                }

                tifEncoderSaveFlag = System.Drawing.Imaging.Encoder.SaveFlag;
                tifEncoderCompression = System.Drawing.Imaging.Encoder.Compression;
                tifEncoderColorDepth = System.Drawing.Imaging.Encoder.ColorDepth;

                tifEncoderParameterMultiFrame = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.MultiFrame);
                tifEncoderParameterFrameDimensionPage = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.FrameDimensionPage);
                tifEncoderParameterFlush = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.Flush);
                tifEncoderParameterCompression = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionRle);
                tifEncoderParameterLastFrame = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.LastFrame);
                tifEncoderParameter24BPP = new EncoderParameter(tifEncoderColorDepth, (long)24);
                tifEncoderParameter1BPP = new EncoderParameter(tifEncoderColorDepth, (long)8);

                // ******************************************************************* //
                // *** Have only 1 of the following 3 groups assigned for encoders *** //
                // ******************************************************************* //

                // Regular
                tifEncoderParametersPage1 = new EncoderParameters(1);
                tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                tifEncoderParametersPageX = new EncoderParameters(1);
                tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                tifEncoderParametersPageLast = new EncoderParameters(1);
                tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;

                //// Regular
                //tifEncoderParametersPage1 = new EncoderParameters(2);
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageX = new EncoderParameters(2);
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageLast = new EncoderParameters(2);
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                //// 24 BPP Color
                //tifEncoderParametersPage1 = new EncoderParameters(2);
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameter24BPP;
                //tifEncoderParametersPageX = new EncoderParameters(2);
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameter24BPP;
                //tifEncoderParametersPageLast = new EncoderParameters(2);
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                //// 1 BPP BW
                //tifEncoderParametersPage1 = new EncoderParameters(2);
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageX = new EncoderParameters(2);
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageLast = new EncoderParameters(2);
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                encoderAssigned = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap source)
        {
            try
            {
                Bitmap bm = new Bitmap(source.Width, source.Height);
                Graphics g = Graphics.FromImage(bm);

                ColorMatrix cm = new ColorMatrix(new float[][] { new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0, 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 0, 1 } });
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(source, new System.Drawing.Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();

                return bm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        public static byte[] SaveData(Image[] bmp)//, string location)
        {
            MemoryStream[] streams = new MemoryStream[bmp.Length];
            for (int i = 0; i < bmp.Length; i++)
            {
                streams[i] = new MemoryStream((byte[])new ImageConverter().ConvertTo(bmp[i], typeof(byte[])));                
            }
            TiffUtil2 util = new TiffUtil2("");

            return util.myMergeTiffStreams(streams);
        }

        public static Image[] SplitTiffPages(Image sourceImage)
        {
            TiffUtil2 t = new TiffUtil2("");
            //return t.splitTiffPages(sourceImage);
            return t.getTiffImages(sourceImage);
        }

        public static Image ScaleImage(Image orjImg, int width, int height)
        {
            if (height == 0) height = Convert.ToInt32(width * (double)orjImg.Height / (double)orjImg.Width);
            if (width == 0) width = Convert.ToInt32(height * (double)orjImg.Width / (double)orjImg.Height);

            Bitmap imgDest = new Bitmap(width, height);
            imgDest.SetResolution(orjImg.HorizontalResolution, orjImg.VerticalResolution);
            Graphics grDest = Graphics.FromImage(imgDest);
            grDest.SmoothingMode = SmoothingMode.AntiAlias;
            grDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grDest.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grDest.DrawImage(orjImg, 0f, 0f, (float)width, (float)height);
            grDest.Dispose();
            return imgDest;
        }

    }

}

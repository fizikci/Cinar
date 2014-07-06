using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System;
using System.Drawing.Drawing2D;


namespace Cinar.Extensions
{
    public class WebPageBitmap
    {
        private WebBrowser webBrowser;
        private string url;
        private int width;
        private int wait;

        public WebPageBitmap(string url, int width, bool scrollBarsEnabled, int wait)
        {
            this.width = width;
            this.url = url;
            this.wait = wait;

            webBrowser = new WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(documentCompletedEventHandler);
            webBrowser.Size = new Size(width, 100);
            webBrowser.ScrollBarsEnabled = false;
        }

        /// <summary>
        /// Fetches the image 
        /// </summaryEnds>
        /// <returns>true is the operation ended with a success</returns>
        public void Fetch()
        {
            DateTime start = DateTime.Now;
            webBrowser.Navigate(url);
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                if ((start - DateTime.Now).Milliseconds > wait)
                    throw new Exception("Timeout reached. Try longer wait.");
                Application.DoEvents();
            }
        }

        private void documentCompletedEventHandler(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error +=
                new HtmlElementErrorEventHandler(SuppressScriptErrorsHandler);
        }

        public void SuppressScriptErrorsHandler(object sender, HtmlElementErrorEventArgs e)
        {
            e.Handled = true;
        }

        internal Bitmap GetBitmap(int thumbwidth)
        {
            int height = webBrowser.Document.Body.ScrollRectangle.Height;
            webBrowser.Height = height;

            Bitmap bitmap = new Bitmap(width, height);
            Rectangle bitmapRect = new Rectangle(0, 0, width, height);

            webBrowser.DrawToBitmap(bitmap, bitmapRect);
            return bitmap;
        }

        public static Bitmap GetBitmap(string url, int width, int waitMiliSeconds)
        {
            WebPageBitmap webBitmap = new WebPageBitmap(url, width, false, waitMiliSeconds);
            webBitmap.Fetch();

            Bitmap thumbnail = webBitmap.GetBitmap(width);
            return thumbnail;
        }
    }
}

using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System;
using System.Drawing.Drawing2D;


namespace Cinar.HTMLParser
{
    public class WebPageBitmap
    {
        private WebBrowser webBrowser;
        private string url;
        private int width;

        public WebPageBitmap(string url, int width, bool scrollBarsEnabled, int wait)
        {
            this.width = width;
            this.url = url;

            webBrowser = new WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(documentCompletedEventHandler);
            webBrowser.Size = new Size(width, 100);
            webBrowser.ScrollBarsEnabled = false;
        }

        /// <summary>
        /// Fetches the image 
        /// </summary>
        /// <returns>true is the operation ended with a success</returns>
        public void Fetch()
        {
            webBrowser.Navigate(url);
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
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

        public static Bitmap GetBitmap(string url, int width)
        {
            WebPageBitmap webBitmap = new WebPageBitmap(url, width, false, 10000);
            webBitmap.Fetch();

            Bitmap thumbnail = webBitmap.GetBitmap(width);
            return thumbnail;
        }
    }
}

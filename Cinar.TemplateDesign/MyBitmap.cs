using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Cinar.TemplateDesign
{
    class MyBitmap
    {
        #region WIN API
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        };

        public enum StockObjects
        {

            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = NULL_BRUSH,
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 16,
            DEFAULT_GUI_FONT = 17,
            DC_BRUSH = 18,
            DC_PEN = 19,
        }

        [DllImport("gdi32.dll")]
        static extern IntPtr GetStockObject(int fnObject);

        public enum PenStyle
        {

            PS_SOLID = 0, //The pen is solid.
            PS_DASH = 1, //The pen is dashed.
            PS_DOT = 2, //The pen is dotted.
            PS_DASHDOT = 3, //The pen has alternating dashes and dots.
            PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
            PS_NULL = 5, //The pen is invisible.
            PS_INSIDEFRAME = 6,
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,

            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,

            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,

            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000

        };

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);

        [Flags]
        public enum ETOOptions : uint
        {
            ETO_CLIPPED = 0x4,
            ETO_GLYPH_INDEX = 0x10,
            ETO_IGNORELANGUAGE = 0x1000,
            ETO_NUMERICSLATIN = 0x800,
            ETO_NUMERICSLOCAL = 0x400,
            ETO_OPAQUE = 0x2,
            ETO_PDY = 0x2000,
            ETO_RTLREADING = 0x800,
        }

        [DllImport("gdi32.dll", EntryPoint = "ExtTextOutW")]
        static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions,
           [In] ref RECT lprc, [MarshalAs(UnmanagedType.LPWStr)] string lpString,
           uint cbCount, [In] int[] lpDx);

        [DllImport("gdi32.dll")]
        static extern bool GetTextExtentPoint(IntPtr hdc, string lpString,
           int cbString, ref Size lpSize);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32.dll")]
        static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport("gdi32", EntryPoint = "ExtTextOut")]
        public static extern int ExtTextOutA(int hDC, int x, int y, int wOptions, ref RECT lpRect, string lpString, int nCount, ref int lpDX);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("user32.dll", SetLastError = false, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);

        [DllImport("gdi32")]
        static extern int Rectangle(IntPtr hDC, int X1, int Y1, int X2, int Y2);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern int DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern int BitBlt(IntPtr hdcDst, int xDst, int yDst, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, int rop);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        static extern bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect);


        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO bmi, uint Usage, out IntPtr bits, IntPtr hSection, uint dwOffset);
        static uint BI_RGB = 0;
        static uint DIB_RGB_COLORS = 0;
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public uint biSize;
            public int biWidth, biHeight;
            public short biPlanes, biBitCount;
            public uint biCompression, biSizeImage;
            public int biXPelsPerMeter, biYPelsPerMeter;
            public uint biClrUsed, biClrImportant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public uint[] cols;
        }

        static uint MAKERGB(int r, int g, int b)
        {
            return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
        }
        #endregion

        bool _isMonochrome;

        IntPtr _hdc0;
        IntPtr _hbm0;
        IntPtr _sdc;
        //Gdi values api values
        Bitmap _bitmap;
        Graphics _graphics;

        public MyBitmap(bool monochrome, int width, int height)
        {
            _isMonochrome = monochrome;

            if (_isMonochrome)
                newGdiBitmap(width, height);
            else
            {
                _bitmap = new Bitmap(width, height);
                _graphics = Graphics.FromImage(_bitmap);
            }
        }

        void newGdiBitmap(int width, int height)
        {
            int bpp = 1;
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.biSize = 40;  // the size of the BITMAPHEADERINFO struct
            bmi.biWidth = width;
            bmi.biHeight = height;
            bmi.biPlanes = 1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
            bmi.biBitCount = (short)bpp; // ie. 1bpp or 8bpp
            bmi.biCompression = BI_RGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
            bmi.biSizeImage = (uint)(((width + 7) & 0xFFFFFFF8) * height / 8);
            bmi.biXPelsPerMeter = 1000000; // not really important
            bmi.biYPelsPerMeter = 1000000; // not really important
            // Now for the colour table.
            uint ncols = (uint)1 << bpp; // 2 colours for 1bpp; 256 colours for 8bpp
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
            if (bpp == 1) { bmi.cols[0] = MAKERGB(0, 0, 0); bmi.cols[1] = MAKERGB(255, 255, 255); }
            else { for (int i = 0; i < ncols; i++) bmi.cols[i] = MAKERGB(i, i, i); }
            // For 8bpp we've created an palette with just greyscale colours.
            // You can set up any palette you want here. Here are some possibilities:
            // greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
            // rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
            //          for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
            // optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
            // 
            // Now create the indexed bitmap "hbm0"
            IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
            _hbm0 = CreateDIBSection(IntPtr.Zero, ref bmi, DIB_RGB_COLORS, out bits0, IntPtr.Zero, 0);
            //
            // Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
            // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
            _sdc = GetDC(IntPtr.Zero);       // First we obtain the DC for the screen
            // Next, create a DC for the original hbitmap
            //IntPtr hdc = CreateCompatibleDC(sdc); SelectObject(hdc, hbm);
            // and create a DC for the monochrome hbitmap
            _hdc0 = CreateCompatibleDC(_sdc);
            SelectObject(_hdc0, _hbm0);

            // Set background color
            /*RECT r = new RECT();
            r.left = 0;
            r.top = 0;
            r.bottom = h;
            r.right = w;
            IntPtr brush1 = CreateSolidBrush(MAKERGB(255, 255, 255));
            FillRect(hdc0, ref r, brush1);*/
        }

        public Bitmap GetBitmap()
        {
            System.Drawing.Bitmap result = null;
            if (_isMonochrome)
            {
                result = System.Drawing.Bitmap.FromHbitmap(_hbm0);
                clean();
            }
            else
                result = _bitmap;

            return result;
        }

        void clean()
        {
            // Finally some cleanup.
            //DeleteDC(_hdc);
            DeleteDC(_hdc0);
            ReleaseDC(IntPtr.Zero, _sdc);
            //DeleteObject(_hbm);
            DeleteObject(_hbm0);
        }

        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            if (_isMonochrome)
            {
                IntPtr brushHandle = CreateSolidBrush((uint)ColorTranslator.ToWin32(((SolidBrush)brush).Color));

                RECT rect = new RECT();
                rect.left = rect.right = x;
                rect.top = rect.bottom = y;
                rect.right += width;
                rect.bottom += height;

                FillRect(_hdc0, ref rect, brushHandle);
            }
            else
            {
                _graphics.FillRectangle(brush, x, y, width, height);
            }
        }

        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            if (_isMonochrome)
            {
                IntPtr brushHandle = CreateSolidBrush(0x0); // black, of format : //0x00bbggrr
                FillRgn(_hdc0, CreateEllipticRgn(x, y, x + width, y + height), brushHandle);
                DeleteObject(brushHandle);
            }
            else
            {
                _graphics.FillEllipse(brush, x, y, width, height);
            }
        }

        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            if (_isMonochrome)
            {
                IntPtr penHandle = CreatePenHandle(pen);
                IntPtr oldpen = SelectObject(_sdc, penHandle);

                MoveToEx(_hdc0, x1, y1, IntPtr.Zero);
                LineTo(_hdc0, x2, y2);
            }
            else
            {
                _graphics.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            if (_isMonochrome)
            {

                SelectObject(_hdc0, GetStockObject((int)StockObjects.NULL_BRUSH));
                IntPtr penHandle = CreatePenHandle(pen);
                IntPtr oldpen = SelectObject(_sdc, penHandle);

                Rectangle(_hdc0, x, y, x + width, y + height);
            }
            else
            {
                _graphics.DrawRectangle(pen, x, y, width, height);
            }
        }

        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            if (_isMonochrome)
            {
                SelectObject(_hdc0, GetStockObject((int)StockObjects.NULL_BRUSH));
                IntPtr penHandle = CreatePenHandle(pen);
                IntPtr oldpen = SelectObject(_sdc, penHandle);

                Ellipse(_hdc0, x, y, x + width, y + height);
            }
            else
            {
                _graphics.DrawEllipse(pen, x, y, width, height);
            }
        }

        public void DrawString(string text, Font font, Brush brush, RectangleF rect)
        {
            if (_isMonochrome)
            {
                //We have to use SelectObject to set the font, color and other properties.
                IntPtr last_font = SelectObject(_hdc0, font.ToHfont());

                Size MeasureSize = new Size(0, 0);
                int y_pos = 0;
                int x_pos = 2;

                //Tasks such as these should not be done in the drawing loop as they
                //slow down the drawing.
                String[] lines = text.Split(new char[] { '\n' });

                string testStr = "qwertyuıopğüasdfghjklşizxcvbnmöçÜĞPPOIUYTREWİŞLKJHGFDSAÇÖMNBVCXZ";
                GetTextExtentPoint(_hdc0, testStr, testStr.Length, ref MeasureSize);

                try
                {
                    //We draw out each line of text.
                    for (int c = 0; c < lines.Length; c++)
                    {
                        String line = lines[c];
                        TextOut(_hdc0, x_pos, y_pos, line, line.Length);

                        y_pos += MeasureSize.Height;
                        //!! You can use the MeasureSize.Width property to implement word wrapping.
                    }
                }
                finally
                {
                    DeleteObject(SelectObject(_hdc0, last_font));
                }
            }
            else
            {
                _graphics.DrawString(text, font, brush, rect);
            }
        }

        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            if (_isMonochrome)
            {
            }
            else
            {
                _graphics.DrawImage(image, x, y, width, height);
            }
        }

        public void DrawImageUnscaled(Image image, int x, int y)
        {
            if (_isMonochrome)
            {
            }
            else
            {
                _graphics.DrawImageUnscaled(image, x, y);
            }
        }

        public static IntPtr CreatePenHandle(Pen pen)
        {
            Color penColor = pen.Color;
            int penWidth = (int)pen.Width;

            IntPtr penHandle = CreatePen(PenStyle.PS_SOLID | PenStyle.PS_GEOMETRIC | PenStyle.PS_ENDCAP_ROUND, penWidth, (uint)ColorTranslator.ToWin32(penColor));
            return penHandle;
        }
    }
}

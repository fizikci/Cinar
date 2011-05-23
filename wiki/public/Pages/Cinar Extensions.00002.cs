Çınar Extensions
admin|2011/05/20 22:37:03
##PAGE##
==Tanım==
Çınar Teknoloji'nin utility kütüphanesidir. Resim işleme, analitik geometri, string işleme, reflection, JSON gibi konularda onlarca fonksiyon içerir.

==Resim İşleme==
@@
bitmap.ScaleImage(int width, int height) : Bitmap
bitmap.CropImage(int x, int y, int width, int height) : Bitmap
image.SaveJpeg(string path, long quality)
@@

==Analitik Geometri==
@@
point.FindDistanceTo(Point point2) : float
point.FindMidPointTo(Point point2) : Point
point.Subtract(Point point2) : Size
point.CreateRect(Point point2) : Rectangle
point.GetLinePart(Point toPoint, float length, LinePart linePart) : Pair<Point>

size.Multiply(int number) : Size
size.Multiply(Size numbers) : Size

rectangle.GetCorners() : Point[]
rectangle.GetSideCenters() : Point[]
rectangle.GetMidPoint() : Point
@@




Çınar Extensions
admin|2011/05/20 22:32:14
##PAGE##
==Tanım==
Çınar Teknoloji'nin utility kütüphanesidir. Resim işleme, analitik geometri, string işleme, reflection, JSON gibi konularda onlarca fonksiyon içerir.

==Resim İşleme==
@@
ScaleImage(this Bitmap orjImg, int width, int height) : Bitmap
SaveJpeg(this Image img, string path, long quality)
CropImage(this Bitmap orjImg, int x, int y, int width, int height) : Bitmap
@@

==Analitik Geometri==
@@
FindDistanceTo(this Point p, Point point) : float
FindMidPointTo(this Point p, Point point) : Point
Subtract(this Point p, Point p2) : Size
CreateRect(this Point p1, Point p2) : Rectangle
GetLinePart(this Point p1, Point p2, float length, LinePart linePart) : Pair<Point>
Multiply(this Size p, int number) : Size
Multiply(this Size p, Size numbers) : Size
GetCorners(this Rectangle rect) : Point[]
GetSideCenters(this Rectangle rect) : Point[]
GetMidPoint(this Rectangle rect) : Point
FindClosestSideCenters(this Rectangle rect, Rectangle rectangle) : Pair<Point>
@@




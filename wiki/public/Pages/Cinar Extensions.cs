Çınar Extensions
admin|2011/05/20 23:01:17
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

==String İşleme==
@@
queryString.ParseQueryString() : NameValueCollection
htmlString.StripHtmlTags() : string
str.StrCrop(int length) : string
str.SplitWithTrim(char seperator) : string[]
str.SplitAndGetLast(char seperator) : string
str.MakeFileName() : string
relativeUrl.ConvertToAbsoluteURL(string baseUrl) : string
dataTable.ToStringTable() : string
dataTable.ToHtmlTable() : string
str.MakePhoneNumber() : string
str.EvaluateAsTemplate(object obj) : string
str.ToFloat() : float
str.ToInt(int baseNumber) : float
str.ToColor() : Color
str.ToEnum<T>() : T
str.ContainedBy(List<string> list) : bool
url.DownloadPage(ref Encoding resolvedEncoding) : string
@@

==Reflection==
@@
obj.GetAttribute<T>() : T // obj: Type, MethodInfo, PropertyInfo, vs..
obj.GetMemberValue(string memberName) : object
obj.GetMemberValue<T>(string memberName) : T
obj.SetMemberValue(string memberName, object val)
obj.SetIndexedValue(string indexer, bool stringIndexer, object val)
obj.SetIndexedValue(object indexer, object val)
obj.GetIndexedValue(string indexer, bool stringIndexer) : object
obj.GetIndexedValue(object indexer) : object
intVal.ToEnum<T>() : T
obj.ToXML() : string
obj.CompareFields(object obj2, Func<PropertyInfo, bool> predicate) : string
obj.CompareFields(object obj2) : string
@@

==JSON==
@@
obj.ToHTMLString() : string 
obj.ToJS() : string
obj.ToJSON() : string
dataTable.ToJSON() : string
dataRow.ToJSON() : string
hashTable.ToJSON() : string
@@

==Diğer==
@@
obj.Clone() : object
obj.Clone(Func<PropertyInfo, bool> predicate) : object
CopyDirectory(string src, string dst)
obj.IsNumeric() : bool
@@
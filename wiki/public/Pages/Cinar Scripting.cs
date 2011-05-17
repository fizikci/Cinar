Çınar Scripting
admin|2011/05/17 01:31:12
##PAGE##
==Tanım==
Çınar Scripting; Javascript ile C# arası bir script dili, editörü, debugging / code completion desteğini içeren scripting kütüphanesidir.

==Amacı==
* Uygulamanıza runtime'da yeni özellikler kazandırmanıza yardım eder.
* .Net Framework ile kullanılabilir.
* Editörünün syntax highlighting, code completion ve debugging yetenekleri vardır.
* Cinar Database Tools yazılımının standart script dilidir. Pek çok özellik onun üzerine bina edilmiştir.
* Template yaklaşımında bir dildir. Bu yönüye PHP veya ASP'ye benzer.

==Örnek Kodlar==
Yorumlayıcı ile kullanımı:
@@
    Interpreter engine = new Interpreter("script kodu... script kodu... script kodu... ", null);
    engine.SetAttribute("db", Provider.Database);
    engine.SetAttribute("table", table);
    engine.SetAttribute("util", new Util());
    engine.Parse();
    engine.Execute();
@@
Yukarıdaki örnekte;
* Önce bir kod ile Interpreter oluşturuluyor.
* Kod çalıştırılmadan önce SetAttribute(...) metodu ile ortam değişkenleri tanımlanıyor.
* Parse() metodu ile kodun parse edilmesi,
* Execute() metodu ile parse edilen kodun çalıştırılması sağlanıyor.

Bu aşamada engine.Output çağrısı kodun çıktısını döndürür.
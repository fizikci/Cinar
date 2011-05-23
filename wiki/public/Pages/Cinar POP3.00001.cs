Çınar POP3
admin|2011/05/21 13:50:10
##PAGE##
==Tanım==
Bir POP3 servera oturum açıp, gelen maillerin indirilmesini sağlayan kütüphane.

==Örnek Kod==
@@
$
	using Cinar.POP3;

	var client = new Pop3Client("remove@yoursite.com", "şifre", "mail.yoursite.com");
	client.OpenInbox();
	while(client.NextEmail())
	{
		var from = client.From;
		var body = client.Body;
		db.ExecuteNonQuery("update member set send_newsletter=0 where email= '"+email+"'");
		client.DeleteEmail();
	}
	client.CloseConnection();
$
@@

Yukarıdaki örnekte, Çınar Script ile, bülten üyeliğinden çıkmak için remove@yoursite.com adresine email atan üyenin bu talebi gerçekleştiriliyor.
* new POP3Client satırında remove@yoursite.com hesabıyla mail sunucusunda oturum açılıyor.
* client.OpenInbox satırı ile gelen mailleri okumaya hazırız.
* while(client.NextEmail()) satırında gelen her bir maili okuyoruz.
* client.From maili gönderen kişinin email adresi.
* Bunu kullanarak veritabanın kullanıcının email üyeliğini iptal ediyoruz.
* client.DeleteEmail() ile işi biten maili sunucudan siliyoruz.
* client.CloseConnection() ile oturumu sonlandırıyoruz.
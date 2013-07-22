using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Cinar.CMS.Library.Entities;
using Cinar.Database;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class MembershipForm : Module
    {
        private string newFormHtml;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string NewFormHtml
        {
            get { return newFormHtml; }
            set { newFormHtml = value; }
        }

        private string editFormHtml;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string EditFormHtml
        {
            get { return editFormHtml; }
            set { editFormHtml = value; }
        }

        private string messageToNewUser = "Tebrikler.<br/>{SiteName} �yeli�iniz kaydedildi. Size g�nderdi�imiz onay mesaj�na cevap veriniz.";
        [ColumnDetail(ColumnType = DbType.Text)]
        public string MessageToNewUser
        {
            get { return messageToNewUser; }
            set { messageToNewUser = value; }
        }

        private string messageForSaved = "De�i�iklikler kaydedildi.";
        [ColumnDetail(ColumnType = DbType.Text)]
        public string MessageForSaved
        {
            get { return messageForSaved; }
            set { messageForSaved = value; }
        }

        public MembershipForm() {
            string uyelikSozlesmesi = @"
                        <div class=""subTitle"">�yelik S�zle�mesi</div>
                        <div style=""background:#efefef;font-size:smaller;height:100px;overflow:scroll"">
                            <b>{SiteName} �yelik S�zle�mesi</b><br/>
                            <br/>
                            A�a��daki ""siteden yararlanma ko�ullar�"" siteden yararlanmaya ba�lan�lmadan �nce dikkatlice okunmal�d�r.<br/>
                            <br/>
                            {SiteAddress} sitesine giri� yap�lmas� bu ko�ullar�n dikkatlice okundu�u ve aynen kabul edildi�i ve bunlara ayk�r� davran��lar�n sadece kullan�c� i�in hukuki ve cezai sorumluluklar gerektirece�ini anlad�klar�n� kabul anlam�na gelir. E�er herhangi bir kullan�c� bu ko�ullar� kabul etmiyorsa, siteye giri� ve yararlanma hakk� bulunmamaktad�r.<br/>
                            <br/>
                            <b>1. S�zle�menin onaylanmas�</b><br/>
                            <br/>
                            {SiteName} �yesi olmak istiyorsan�z, �nce kay�t formunu doldurman�z ve {SiteName} �yelik s�zle�mesi h�k�mlerine uymay� kabul ve taahh�t etmeniz gereklidir. Karar�n�za g�re �yelik formunun alt�nda bulunan (Kabul ediyorum) veya (Kabul etmiyorum) butonunu t�klay�n�z.<br/>
                            <br/>
                            <b>2. Hizmetlerin tan�m�</b><br/>
                            <br/>
                            {SiteName} s�zle�meyi onaylayan �yelerine internet ortam�nda de�i�ik servisler (haber, ajandam, vb.) sunar.<br/>
                            <br/>
                            <b>3. {SiteName} �yelik sistemi ve gizlilik ilkesi</b><br/>
                            <br/>
                            {SiteName} �yesi kendisinin belirleyece�i bir ""kullan�c� ad�"" ve ""�ifre""ye sahip olur. ""Kullan�c� ad�"" �yeye �zeldir ve ayn�  ""kullan�c� ad�"" iki farkl� �yeye verilmez. ""�ifre"" sadece kullan�c� taraf�ndan bilinir. Kullan�c� diledi�i zaman �ifresini de�i�tirebilir. (�lk �yelik a�amas�nda �ifre otomatik olarak �yenin e-mail adresine yollan�r.) �ifrenin se�imi ve korunmas� tamam�yla kullan�c�n�n sorumlulu�undad�r. {SiteName}, �ifre kullan�m�ndan do�acak problemlerden kesinlikle sorumlu de�ildir. �yelik bilgileri ve ki�isel bilgiler �yenin onay� al�nmadan ba�ka �yelere a��lmaz. Kullan�c�n�n {SiteName} �yeli�i gerektiren servislere ba�lanabilmesi i�in kullan�c� ad�n� ve �ifresini girmesi gereklidir. Bu i�lem �ye giri�i yapmak �eklinde tan�mlan�r.<br/>
                            <br/>
                            <b>4. �ye'nin y�k�ml�l�kleri</b><br/>
                            <br/>
                            �ye {SiteName} servislerinden yararland��� s�rada, kay�t formunda yer alan bilgilerin do�ru oldu�unu ve bu bilgilerin gerekli oldu�u (�ifre unutma gibi) durumlarda bilginin hatal� veya noksan olmas�ndan do�acak zararlardan dolay� sorumlulu�un kendisine ait oldu�unu, bu hallerde �yeli�inin sona erebilece�ini,<br/>
                            <br/>
                            {SiteName} taraf�ndan verilen servislerin ve yaz�l�mlar�n telif hakk�n�n {SiteName} taraf�na ait oldu�unu, bu yaz�l�mlar� ya da servisleri hi�bir �ekilde izinsiz �o�alt�p, da��tmayaca��n�, yay�nlamayaca��n�, pazarlamayaca��n�,<br/>
                            <br/>
                            {SiteName} servislerini kulland���nda ileri s�rd��� �ahsi fikir, d���nce, ifade, {SiteName} ortam�na ekledi�i dosyalar�n sorumlulu�unun kendisine ait oldu�unu ve {SiteName} taraf�n�n bu dosyalardan ve cep telefonuna g�nderilen k�sa mesajlardan (SMS) dolay� hi�bir �ekilde sorumlu tutulamayaca��n�,<br/>
                            <br/>
                            {SiteName} ortam�nda site geneline zarar verecek veya {SiteName}'i ba�ka sitelerle mahkemelik duruma getirecek herhangi bir yaz�l�m veya materyal bulunduramayaca��n�, payla�amayaca��n� ve cezai bir durum do�arsa t�m cezai sorumluluklar� �st�ne ald���n�,<br/>
                            <br/>
                            {SiteName} taraf�ndan sa�lanan servislerde baz� hallerde 18 ya��ndan b�y�klere �zel bilgilerin yer alabilece�ini ve bu sebeple {SiteName} taraf�n�n sorumlu olmayaca��n�,<br/>
                            <br/>
                            Servislerinin kullan�m� s�ras�nda kaybolacak, ve/veya eksik al�nacak, yanl�� adrese iletilecek bilgi, mesaj ve dosyalardan {SiteName} taraf�n�n sorumlu olmayaca��n�,<br/>
                            <br/>
                            {SiteName}'de sunulan hizmetlere {SiteName} taraf�ndan belirlenen �ekil d���nda ve yetkisiz �ekilde ula�mamay� ve yaz�l�m ile servislerin �zelliklerini hi�bir �ekilde de�i�tirmemeyi, de�i�tirilmi� oldu�u belli olanlar� kullanmamay� ve s�z� ge�en maddelere uymad��� durumlarda {SiteName} taraf�n�n u�rayabilece�i t�m maddi ve manevi zararlar� �demeyi,<br/>
                            <br/>
                            �ye verilerinin {SiteName}'in ihmali g�r�lmeden yetkisiz ki�ilerce okunmas�ndan (�yenin bilgilerini ba�ka ki�iler ile payla�mas�, siteden ayr�l�rken ��k�� yapmamas�, vb.durumlardan) dolay� gelebilecek zararlardan �t�r� {SiteName} taraf�n�n sorumlu olmayaca��n�,<br/>
                            <br/>
                            Tehdid edici, ahlak d���, �rk��, ayr�mc�, T�rkiye Cumhuriyeti Yasalar�na, vatanda�� oldu�u di�er �lkelerin yasalar�na ve uluslararas� anla�malara ayk�r� mesajlar g�ndermemeyi,<br/>
                            <br/>
                            Ortama eklenecek yaz��ma, konu ba�l�klar�, rumuzlar� genel ahlak, g�rg� ve hukuk kurallar�na uygun olmas�n�,<br/>
                            <br/>
                            Di�er kullan�c�lar� taciz ve tehdit etmemeyi,<br/>
                            <br/>
                            Di�er kullan�c�lar�n servisi kullanmas�n� etkileyecek �ekilde davranmamay�,<br/>
                            <br/>
                            Di�er kullan�c�lar�n bilgisayar�ndaki bilgilere ya da yaz�l�ma zarar verecek bilgi veya programlar g�ndermemeyi,<br/>
                            <br/>
                            {SiteName} servislerini kullanarak elde edilen herhangi bir kay�t veya elde edilmi� malzemelerin tamam�yla kullan�c�n�n r�zas� dahilinde oldu�unu, kullan�c� bilgisayar�nda yarataca�� ar�zalar, bilgi kayb� ve di�er kay�plar�n sorumlulu�unun tamam�yla kendisine ait oldu�unu, servisin kullan�m�ndan dolay� u�rayabilece�i zararlardan dolay� {SiteName} taraf�ndan tazminat talep etmemeyi,<br/>
                            <br/>
                            {SiteName} taraf�ndan izin almadan {SiteName} servislerini ticari ya da reklam amac�yla kullanmamay�,<br/>
                            <br/>
                            {SiteName} taraf�n�n, diledi�i zaman veya s�rekli olarak t�m sistemi izleyebilece�ini,<br/>
                            <br/>
                            Kurallara ayk�r� davrand��� takdirde {SiteName} taraf�n�n gerekli m�dahalelerde bulunma, �yeyi servis d���na ��karma ve �yeli�e son verme hakk�na sahip oldu�unu,<br/>
                            <br/>
                            {SiteName} taraf�n�n, kendi sistemini ticari ama�la kullanabilece�ini,<br/>
                            <br/>
                            Kanunlara g�re postalanmas� yasak olan bilgileri postalamamay� ve zincir posta(chain mail), yaz�l�m vir�s� (vb.) gibi g�nderilme yetkisi olmayan postalar� da��tmamay�,<br/>
                            <br/>
                            Ba�kalar�na ait olan ki�isel bilgileri kay�t etmemeyi, k�t�ye kullanmamay�,<br/>
                            <br/>
                            �ye ""kullan�c�"" ad�yla yapaca�� her t�rl� i�lemden bizzat kendisinin sorumlu oldu�unu,<br/>
                            <br/>
                            �yeli�ini tek tarafl� olarak iptal ettirse bile, bu iptal i�leminden �nce, �yeli�i s�ras�nda ger�ekle�tirdi�i icraatlardan sorumlu olaca��n�,<br/>
                            <br/>
                            T�m bu maddeleri daha sonra hi�bir itiraza mahal vermeyecek �ekilde okudu�unu,<br/>
                            <br/>
                            Kabul ve taahh�t etmi�tir.<br/>
                            <br/>
                            <b>5. {SiteName} taraf�na verilen yetkiler</b><br/>
                            <br/>
                            {SiteName} herhangi bir zamanda sistemin �al��mas�n� ge�ici bir s�re ask�ya alabilir veya tamamen durdurabilir. Sistemin ge�ici bir s�re ask�ya al�nmas� veya tamamen durdurulmas�ndan dolay� {SiteName} taraf�n�n �yelerine veya ���nc� �ah�slara kar�� hi�bir sorumlulu�u olmayacakt�r. {SiteName}, servislerinin her zaman ve her �art alt�nda zaman�nda, g�venli ve hatas�z olarak sunulaca��n�, servis kullan�m�ndan elde edilen sonu�lar�n her zaman do�ru ve g�venilir olaca��n�, servis kalitesinin herkesin beklentilerine cevap verece�ini taahh�t etmez.<br/>
                            <br/>
                            {SiteName}, kendi sitesi �st�nden yap�lan ve zarar do�urabilecek her t�rl� haberle�meyi, yay�n�, bilgi aktar�m�n� istedi�i zaman kesme hakk�n� ve gereken ko�ullar olu�tu�u takdirde �ye mesajlar�n� silme, �yeyi servislerden menetme ve �yeli�ine son verme hakk�n� sakl� tutar.<br/>
                            <br/>
                            {SiteName}, �yelerinin bilgilerini a�a��da belirtilen ko�ullardan birinin ya da hepsinin ger�ekle�mesi halinde ilgili resmi merciler ile payla�ma hakk�na sahiptir:<br/>
                            <br/>
                            Resmi makamlardan �yeye y�nelik su� duyurusu ya da resmi soru�turma talebi gelmesi durumunda<br/>
                            <br/>
                            �yenin sistemin �al��mas�n� engelleyecek bir sabotaj ya da sald�r� yapt���n�n tespit edilmesi durumunda<br/>
                            <br/>
                            �yeli�i s�zle�mede belirtilen sebeplerden dolay� iptal edilmi� bir �yenin yeniden �ye olarak s�zle�me ihlalini tekrarlamas� durumunda<br/>
                            <br/>
                            {SiteName} �yelerin dosyalar�n�n saklanmas� i�in uygun g�rece�i b�y�kl�kte kota tahsisi yapabilir. �htiyaca g�re kotalar� artt�rma ve azaltma yetkisini sakl� tutar.<br/>
                            <br/>
                            {SiteName} �yelerin servislerden yararlanmalar� s�ras�nda ortamda bulunduracaklar� dosyalar�n, mesajlar�n, bilgilerin baz�lar�n� veya tamam�n� uygun g�rece�i periyotlarla yedekleme ve silme yetkisine sahiptir. Yedekleme ve silme i�lemlerinden dolay� {SiteName} sorumlu tutulmayacakt�r.<br/>
                            <br/>
                            {SiteName} kendi �retti�i ve/veya d��ardan sat�n ald��� bilgi, belge, yaz�l�m, tasar�m, grafik vb. eserlerin m�lkiyet ve m�lkiyetten do�an telif haklar�na sahiptir.<br/>
                            <br/>
                            {SiteName} �yelerinin �retti�i ve yay�nlanmak �zere kendi iradesiyle sisteme y�kledi�i (�rne�in panoya ekledi�i mesaj, �iir, haber, dosya, web sayfas� gibi) bilgi, belge, yaz�l�m, tasar�m, grafik, resim vb. Eserleri tan�t�m, duyurum amac� ile yay�nlama ve/veya site i�erisinde {SiteName} taraf�ndan uygun g�r�lecek ba�ka bir adrese ta��ma haklar�na sahiptir. Yay�nlanan bu bilgilerin ba�ka kullan�c�lar taraf�ndan kopyalanmas� ve/veya yay�nlanmas� ihtimal dahilindedir. Bu hallerde �ye, {SiteName} taraf�ndan telif ve benzeri hi� bir �cret talep etmeyecektir.<br/>
                            <br/>
                            {SiteName} �yenin iletti�i ki�isel bilgilerin i�erik sa�lay�c�lar ve web servisleri kullan�c�lar�na �yenin �zl�k haklar�n� taciz etmeyecek �ekilde gerekli ileti�im, tan�t�m, mal teslimi, reklam vb. ama�lar i�in kulland�r�lmas� konusunda tam yetkilidir.<br/>
                            <br/>
                            {SiteName}, �yenin ba�ka web-sitelerine ge�i�ini sa�layabilir. Bu taktirde �ye ge�i� yapaca�� sitelerin i�eri�inden {SiteName} taraf�n�n sorumlu olmad���n� kabul eder.<br/>
                            <br/>
                            {SiteName}, ileride do�acak teknik zaruretler ve mevzuata uyum amac�yla �yelere haber vermek zorunda olmadan i�bu s�zle�menin uygulamas�nda de�i�iklikler yapabilece�i gibi mevcut maddelerini de�i�tirebilir, iptal edebilir veya yeni maddeler ilave edebilir. �yeler s�zle�meyi her zaman bu sayfan�n adresinde bulabilir ve okuyabilirler. Servisler ile ilgili de�i�iklikler site i�erisinde duyurulacakt�r ve gerekti�inde �yenin hizmetlerden yararlanabilmesi i�in s�zle�me de�i�ikliklerini ilgili butonu t�klamak suretiyle onaylamas� istenecektir.<br/>
                            <br/>
                            {SiteName} �yelik gerektirmeyen servisleri zaman i�erisinde �yelik gerektiren bir duruma d�n��t�r�lebilir. {SiteName} ilave servisler a�abilir, baz� servislerini k�smen veya tamamen de�i�tirebilir, veya �cretli hale d�n��t�rebilir.<br/>
                            <br/>
                            <b>6. {SiteName} kay�tlar�n�n ge�erlili�i</b><br/>
                            <br/>
                            �ye bu s�zle�meden do�abilecek ihtilaflarda {SiteName} taraf�n�n defter kay�tlar�yla, mikrofilm, mikrofi�, ve bilgisayar kay�tlar�n�n HUMK 287. Madde anlam�nda muteber, ba�lay�c�, kesin ve m�nhas�r delil te�kil edece�ini ve bu maddenin delil s�zle�mesi niteli�inde oldu�unu, belirten {SiteName} kay�tlar�na her t�rl� itiraz ve bunlar�n usul�ne uygun tutuldu�u hususunda yemin teklif hakk�ndan pe�inen feragat etti�ini kabul, beyan ve taahh�t eder.<br/>
                            <br/>
                            <b>7. Uygulanacak H�k�mler</b><br/>
                            <br/>
                            Bu s�zle�meyle ilgili olarak ��kabilecek itilaflarda �ncelikle bu s�zle�medeki h�k�mler, h�k�m bulunmayan hallerde ise T�rk Kanunlar� (BK, TTK, HUMK, MK ve sair) uygulanacakt�r.<br/>
                            <br/>
                            <b>8. Yetkili Mahkeme ve �cra Daireleri</b><br/>
                            <br/>
                            ��bu s�zle�menin uygulanmas�ndan do�abilecek her t�rl� uyu�mazl�klar�n ��z�m�nde �stanbul Merkez(Sultanahmet) Mahkemeleri ile �cra M�d�rl�kleri yetkili olacakt�r.<br/>
                            <br/>
                            <b>9. Y�r�rl�k</b><br/>
                            <br/>
                            Kullan�c� kay�t formunu doldurup formun alt�nda bulunan ""kabul ediyorum"" butonunu t�klad���nda bu s�zle�me taraflar aras�nda s�resiz olarak y�r�rl��e girer.<br/>
                            <br/>
                            <b>10. Fesih<br/>
                            <br/>
                            {SiteName} diledi�i zaman bu s�zle�meyi sona erdirebilir.<br/>
                        </div>
                        <input type=""checkbox"" name=""cbAcceptRules"" value=1/> S�zle�meyi kabul ediyorum.
            ";
            string uyelikSozlesmesiCheck = @" onclick=""if(!this.form.cbAcceptRules.checked) {alert('S�zle�meyi kabul etmelisiniz.'); return false;}""";
            string formHtml = @"
                <table border=""0"" cellpadding=""4"">
                <tr>
                    <td>
                        <div class=""subTitle"">Login Info</div>
                        Email<br/><input type=""text"" name=""Email""/><br/>
                        Password<br/><input type=""password"" name=""Password""/><br/>
                        Password Again<br/><input type=""password"" name=""Password2""/><br/>

                        <div class=""subTitle"">Personal Info</div>
                        Nick<br/><input type=""text"" name=""Nick""/><br/>
                        Avatar<br/><input type=""file"" name=""Avatar""/><br/>
                        Name<br/><input type=""text"" name=""Name""/><br/>
                        Surname<br/><input type=""text"" name=""Surname""/><br/>
                        Gender<br/><select name=""Gender""><option>Erkek</option><option>Bayan</option></select><br/>
                    </td>
                    <td>
                        <div class=""subTitle"">Contact Info</div>
                        PhoneCell<br/><input type=""text"" name=""PhoneCell""/><br/>
                        PhoneWork<br/><input type=""text"" name=""PhoneWork""/><br/>
                        PhoneHome<br/><input type=""text"" name=""PhoneHome""/><br/>
                        AddressLine1<br/><input type=""text"" name=""AddressLine1""/><br/>
                        AddressLine2<br/><input type=""text"" name=""AddressLine2""/><br/>
                        City<br/><input type=""text"" name=""City""/><br/>
                        Country<br/><input type=""text"" name=""Country""/><br/>
                        ZipCode<br/><input type=""text"" name=""ZipCode""/><br/>
                        Web<br/><input type=""text"" name=""Web""/><br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class=""subTitle"">Professional Info</div>
                        Occupation<br/><input type=""text"" name=""Occupation""/><br/>
                        Company<br/><input type=""text"" name=""Company""/><br/>
                        Department<br/><input type=""text"" name=""Department""/><br/>
                    </td>
                    <td>
                        <div class=""subTitle"">Other</div>
                        Education<br/><input type=""text"" name=""Education""/><br/>
                        Certificates<br/><textarea name=""Certificates""></textarea><br/>
                        About<br/><textarea name=""About""></textarea><br/>
                    </td>
                </tr>
                <tr>
                    <td colspan=""2"">{0}</td>
                </tr>
                <tr>
                    <td colspan=""2"" align=""right"">
                        <input type=""reset"" value=""Reset""/>
                        <input type=""submit"" value=""Save""{1}/>
                    </td>
                </tr>
                </table>
                ";
            this.newFormHtml = String.Format(formHtml, uyelikSozlesmesi, uyelikSozlesmesiCheck);
            this.editFormHtml = String.Format(formHtml, String.Empty, String.Empty);
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            User user = Provider.User.IsAnonim() ? new User() : Provider.User;
            string st = Provider.Request["st"];
            MembershipFormStatus status = MembershipFormStatus.EditUser;
            if (String.IsNullOrEmpty(st))
                status = user.Id == 0 ? MembershipFormStatus.NewUser : MembershipFormStatus.EditUser;
            else
                status = (MembershipFormStatus)Enum.Parse(typeof(MembershipFormStatus), st);

            if (status == MembershipFormStatus.Error) {
                List<string> errorList = (List<string>)Provider.Session["membershipErrors"];
                sb.Append("<div class=\"errorPanel\"><ul>");
                errorList.ForEach(delegate(string msg)
                {
                    sb.AppendFormat("<li>{0}</li>", msg);
                });
                sb.Append("</ul></div>");

                status = user.Id == 0 ? MembershipFormStatus.NewUser : MembershipFormStatus.EditUser;
            }

            switch (status)
            {
                case MembershipFormStatus.NewUser:
                    sb.Append(getForm(user, this.newFormHtml));
                    break;

                case MembershipFormStatus.EditUser:
                    sb.Append(getForm(user, this.editFormHtml));
                    break;

                case MembershipFormStatus.NewUserSaved:
                    sb.Append(this.messageToNewUser);
                    break;

                case MembershipFormStatus.EditUserSaved:
                    sb.Append(this.messageForSaved);
                    break;

                default:
                    break;
            }
            sb.Replace("{SiteName}", Provider.Configuration.SiteName);
            sb.Replace("{SiteAddress}", Provider.Configuration.SiteAddress);

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        private string getForm(User user, string formHtml) {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"
                        <script>
                        document.observe('dom:loaded', function(){
                            var user_data = {};
                            _USERDATA_
                            var elems = Form.getElements('fMembership');
                            for(var i=0; i<elems.length; i++)
                                elems[i].setValue(user_data[elems[i].name]);
                        });
                        </script>");

            sb.AppendFormat("<form id=\"fMembership\" method=\"post\" action=\"SaveMember.ashx\" enctype=\"multipart/form-data\">\n");

            sb.Append(formHtml);
            bool isPostBack = Provider.Request["Email"] != null;

            StringBuilder sbUserData = new StringBuilder();
            foreach (PropertyInfo pi in user.GetType().GetProperties())
            {
                if (pi.Name.Contains("Password") || pi.DeclaringType != typeof(User) || pi.GetSetMethod() == null) 
                    continue; //***
                object val = pi.GetValue(user, null);
                sbUserData.AppendLine("                            user_data[\"" + pi.Name + "\"] = \"" + CMSUtility.HtmlEncode(isPostBack ? Provider.Request[pi.Name] : (val == null ? "" : val.ToString())) + "\";");
            }

            sb.AppendFormat("</form>");
            sb = sb.Replace("_USERDATA_", sbUserData.ToString());
            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.subTitle {{background:#dfdfdf;font-weight:bold;padding:2px;margin-top:10px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.errorPanel {{background:#efefef;border:1px dotted #afafaf;padding:2px;margin-top:10px}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }

    public enum MembershipFormStatus 
    { 
        NewUser,
        NewUserSaved,
        EditUser,
        EditUserSaved,
        Error
    }
}

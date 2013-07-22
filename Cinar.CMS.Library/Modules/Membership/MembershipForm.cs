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

        private string messageToNewUser = "Tebrikler.<br/>{SiteName} üyeliðiniz kaydedildi. Size gönderdiðimiz onay mesajýna cevap veriniz.";
        [ColumnDetail(ColumnType = DbType.Text)]
        public string MessageToNewUser
        {
            get { return messageToNewUser; }
            set { messageToNewUser = value; }
        }

        private string messageForSaved = "Deðiþiklikler kaydedildi.";
        [ColumnDetail(ColumnType = DbType.Text)]
        public string MessageForSaved
        {
            get { return messageForSaved; }
            set { messageForSaved = value; }
        }

        public MembershipForm() {
            string uyelikSozlesmesi = @"
                        <div class=""subTitle"">Üyelik Sözleþmesi</div>
                        <div style=""background:#efefef;font-size:smaller;height:100px;overflow:scroll"">
                            <b>{SiteName} Üyelik Sözleþmesi</b><br/>
                            <br/>
                            Aþaðýdaki ""siteden yararlanma koþullarý"" siteden yararlanmaya baþlanýlmadan önce dikkatlice okunmalýdýr.<br/>
                            <br/>
                            {SiteAddress} sitesine giriþ yapýlmasý bu koþullarýn dikkatlice okunduðu ve aynen kabul edildiði ve bunlara aykýrý davranýþlarýn sadece kullanýcý için hukuki ve cezai sorumluluklar gerektireceðini anladýklarýný kabul anlamýna gelir. Eðer herhangi bir kullanýcý bu koþullarý kabul etmiyorsa, siteye giriþ ve yararlanma hakký bulunmamaktadýr.<br/>
                            <br/>
                            <b>1. Sözleþmenin onaylanmasý</b><br/>
                            <br/>
                            {SiteName} üyesi olmak istiyorsanýz, önce kayýt formunu doldurmanýz ve {SiteName} üyelik sözleþmesi hükümlerine uymayý kabul ve taahhüt etmeniz gereklidir. Kararýnýza göre üyelik formunun altýnda bulunan (Kabul ediyorum) veya (Kabul etmiyorum) butonunu týklayýnýz.<br/>
                            <br/>
                            <b>2. Hizmetlerin tanýmý</b><br/>
                            <br/>
                            {SiteName} sözleþmeyi onaylayan üyelerine internet ortamýnda deðiþik servisler (haber, ajandam, vb.) sunar.<br/>
                            <br/>
                            <b>3. {SiteName} üyelik sistemi ve gizlilik ilkesi</b><br/>
                            <br/>
                            {SiteName} üyesi kendisinin belirleyeceði bir ""kullanýcý adý"" ve ""þifre""ye sahip olur. ""Kullanýcý adý"" üyeye özeldir ve ayný  ""kullanýcý adý"" iki farklý üyeye verilmez. ""Þifre"" sadece kullanýcý tarafýndan bilinir. Kullanýcý dilediði zaman þifresini deðiþtirebilir. (Ýlk üyelik aþamasýnda þifre otomatik olarak üyenin e-mail adresine yollanýr.) Þifrenin seçimi ve korunmasý tamamýyla kullanýcýnýn sorumluluðundadýr. {SiteName}, þifre kullanýmýndan doðacak problemlerden kesinlikle sorumlu deðildir. Üyelik bilgileri ve kiþisel bilgiler üyenin onayý alýnmadan baþka üyelere açýlmaz. Kullanýcýnýn {SiteName} üyeliði gerektiren servislere baðlanabilmesi için kullanýcý adýný ve þifresini girmesi gereklidir. Bu iþlem üye giriþi yapmak þeklinde tanýmlanýr.<br/>
                            <br/>
                            <b>4. Üye'nin yükümlülükleri</b><br/>
                            <br/>
                            Üye {SiteName} servislerinden yararlandýðý sýrada, kayýt formunda yer alan bilgilerin doðru olduðunu ve bu bilgilerin gerekli olduðu (þifre unutma gibi) durumlarda bilginin hatalý veya noksan olmasýndan doðacak zararlardan dolayý sorumluluðun kendisine ait olduðunu, bu hallerde üyeliðinin sona erebileceðini,<br/>
                            <br/>
                            {SiteName} tarafýndan verilen servislerin ve yazýlýmlarýn telif hakkýnýn {SiteName} tarafýna ait olduðunu, bu yazýlýmlarý ya da servisleri hiçbir þekilde izinsiz çoðaltýp, daðýtmayacaðýný, yayýnlamayacaðýný, pazarlamayacaðýný,<br/>
                            <br/>
                            {SiteName} servislerini kullandýðýnda ileri sürdüðü þahsi fikir, düþünce, ifade, {SiteName} ortamýna eklediði dosyalarýn sorumluluðunun kendisine ait olduðunu ve {SiteName} tarafýnýn bu dosyalardan ve cep telefonuna gönderilen kýsa mesajlardan (SMS) dolayý hiçbir þekilde sorumlu tutulamayacaðýný,<br/>
                            <br/>
                            {SiteName} ortamýnda site geneline zarar verecek veya {SiteName}'i baþka sitelerle mahkemelik duruma getirecek herhangi bir yazýlým veya materyal bulunduramayacaðýný, paylaþamayacaðýný ve cezai bir durum doðarsa tüm cezai sorumluluklarý üstüne aldýðýný,<br/>
                            <br/>
                            {SiteName} tarafýndan saðlanan servislerde bazý hallerde 18 yaþýndan büyüklere özel bilgilerin yer alabileceðini ve bu sebeple {SiteName} tarafýnýn sorumlu olmayacaðýný,<br/>
                            <br/>
                            Servislerinin kullanýmý sýrasýnda kaybolacak, ve/veya eksik alýnacak, yanlýþ adrese iletilecek bilgi, mesaj ve dosyalardan {SiteName} tarafýnýn sorumlu olmayacaðýný,<br/>
                            <br/>
                            {SiteName}'de sunulan hizmetlere {SiteName} tarafýndan belirlenen þekil dýþýnda ve yetkisiz þekilde ulaþmamayý ve yazýlým ile servislerin özelliklerini hiçbir þekilde deðiþtirmemeyi, deðiþtirilmiþ olduðu belli olanlarý kullanmamayý ve sözü geçen maddelere uymadýðý durumlarda {SiteName} tarafýnýn uðrayabileceði tüm maddi ve manevi zararlarý ödemeyi,<br/>
                            <br/>
                            Üye verilerinin {SiteName}'in ihmali görülmeden yetkisiz kiþilerce okunmasýndan (üyenin bilgilerini baþka kiþiler ile paylaþmasý, siteden ayrýlýrken çýkýþ yapmamasý, vb.durumlardan) dolayý gelebilecek zararlardan ötürü {SiteName} tarafýnýn sorumlu olmayacaðýný,<br/>
                            <br/>
                            Tehdid edici, ahlak dýþý, ýrkçý, ayrýmcý, Türkiye Cumhuriyeti Yasalarýna, vatandaþý olduðu diðer ülkelerin yasalarýna ve uluslararasý anlaþmalara aykýrý mesajlar göndermemeyi,<br/>
                            <br/>
                            Ortama eklenecek yazýþma, konu baþlýklarý, rumuzlarý genel ahlak, görgü ve hukuk kurallarýna uygun olmasýný,<br/>
                            <br/>
                            Diðer kullanýcýlarý taciz ve tehdit etmemeyi,<br/>
                            <br/>
                            Diðer kullanýcýlarýn servisi kullanmasýný etkileyecek þekilde davranmamayý,<br/>
                            <br/>
                            Diðer kullanýcýlarýn bilgisayarýndaki bilgilere ya da yazýlýma zarar verecek bilgi veya programlar göndermemeyi,<br/>
                            <br/>
                            {SiteName} servislerini kullanarak elde edilen herhangi bir kayýt veya elde edilmiþ malzemelerin tamamýyla kullanýcýnýn rýzasý dahilinde olduðunu, kullanýcý bilgisayarýnda yaratacaðý arýzalar, bilgi kaybý ve diðer kayýplarýn sorumluluðunun tamamýyla kendisine ait olduðunu, servisin kullanýmýndan dolayý uðrayabileceði zararlardan dolayý {SiteName} tarafýndan tazminat talep etmemeyi,<br/>
                            <br/>
                            {SiteName} tarafýndan izin almadan {SiteName} servislerini ticari ya da reklam amacýyla kullanmamayý,<br/>
                            <br/>
                            {SiteName} tarafýnýn, dilediði zaman veya sürekli olarak tüm sistemi izleyebileceðini,<br/>
                            <br/>
                            Kurallara aykýrý davrandýðý takdirde {SiteName} tarafýnýn gerekli müdahalelerde bulunma, üyeyi servis dýþýna çýkarma ve üyeliðe son verme hakkýna sahip olduðunu,<br/>
                            <br/>
                            {SiteName} tarafýnýn, kendi sistemini ticari amaçla kullanabileceðini,<br/>
                            <br/>
                            Kanunlara göre postalanmasý yasak olan bilgileri postalamamayý ve zincir posta(chain mail), yazýlým virüsü (vb.) gibi gönderilme yetkisi olmayan postalarý daðýtmamayý,<br/>
                            <br/>
                            Baþkalarýna ait olan kiþisel bilgileri kayýt etmemeyi, kötüye kullanmamayý,<br/>
                            <br/>
                            Üye ""kullanýcý"" adýyla yapacaðý her türlü iþlemden bizzat kendisinin sorumlu olduðunu,<br/>
                            <br/>
                            Üyeliðini tek taraflý olarak iptal ettirse bile, bu iptal iþleminden önce, üyeliði sýrasýnda gerçekleþtirdiði icraatlardan sorumlu olacaðýný,<br/>
                            <br/>
                            Tüm bu maddeleri daha sonra hiçbir itiraza mahal vermeyecek þekilde okuduðunu,<br/>
                            <br/>
                            Kabul ve taahhüt etmiþtir.<br/>
                            <br/>
                            <b>5. {SiteName} tarafýna verilen yetkiler</b><br/>
                            <br/>
                            {SiteName} herhangi bir zamanda sistemin çalýþmasýný geçici bir süre askýya alabilir veya tamamen durdurabilir. Sistemin geçici bir süre askýya alýnmasý veya tamamen durdurulmasýndan dolayý {SiteName} tarafýnýn üyelerine veya üçüncü þahýslara karþý hiçbir sorumluluðu olmayacaktýr. {SiteName}, servislerinin her zaman ve her þart altýnda zamanýnda, güvenli ve hatasýz olarak sunulacaðýný, servis kullanýmýndan elde edilen sonuçlarýn her zaman doðru ve güvenilir olacaðýný, servis kalitesinin herkesin beklentilerine cevap vereceðini taahhüt etmez.<br/>
                            <br/>
                            {SiteName}, kendi sitesi üstünden yapýlan ve zarar doðurabilecek her türlü haberleþmeyi, yayýný, bilgi aktarýmýný istediði zaman kesme hakkýný ve gereken koþullar oluþtuðu takdirde üye mesajlarýný silme, üyeyi servislerden menetme ve üyeliðine son verme hakkýný saklý tutar.<br/>
                            <br/>
                            {SiteName}, üyelerinin bilgilerini aþaðýda belirtilen koþullardan birinin ya da hepsinin gerçekleþmesi halinde ilgili resmi merciler ile paylaþma hakkýna sahiptir:<br/>
                            <br/>
                            Resmi makamlardan üyeye yönelik suç duyurusu ya da resmi soruþturma talebi gelmesi durumunda<br/>
                            <br/>
                            Üyenin sistemin çalýþmasýný engelleyecek bir sabotaj ya da saldýrý yaptýðýnýn tespit edilmesi durumunda<br/>
                            <br/>
                            Üyeliði sözleþmede belirtilen sebeplerden dolayý iptal edilmiþ bir üyenin yeniden üye olarak sözleþme ihlalini tekrarlamasý durumunda<br/>
                            <br/>
                            {SiteName} üyelerin dosyalarýnýn saklanmasý için uygun göreceði büyüklükte kota tahsisi yapabilir. Ýhtiyaca göre kotalarý arttýrma ve azaltma yetkisini saklý tutar.<br/>
                            <br/>
                            {SiteName} üyelerin servislerden yararlanmalarý sýrasýnda ortamda bulunduracaklarý dosyalarýn, mesajlarýn, bilgilerin bazýlarýný veya tamamýný uygun göreceði periyotlarla yedekleme ve silme yetkisine sahiptir. Yedekleme ve silme iþlemlerinden dolayý {SiteName} sorumlu tutulmayacaktýr.<br/>
                            <br/>
                            {SiteName} kendi ürettiði ve/veya dýþardan satýn aldýðý bilgi, belge, yazýlým, tasarým, grafik vb. eserlerin mülkiyet ve mülkiyetten doðan telif haklarýna sahiptir.<br/>
                            <br/>
                            {SiteName} üyelerinin ürettiði ve yayýnlanmak üzere kendi iradesiyle sisteme yüklediði (örneðin panoya eklediði mesaj, þiir, haber, dosya, web sayfasý gibi) bilgi, belge, yazýlým, tasarým, grafik, resim vb. Eserleri tanýtým, duyurum amacý ile yayýnlama ve/veya site içerisinde {SiteName} tarafýndan uygun görülecek baþka bir adrese taþýma haklarýna sahiptir. Yayýnlanan bu bilgilerin baþka kullanýcýlar tarafýndan kopyalanmasý ve/veya yayýnlanmasý ihtimal dahilindedir. Bu hallerde üye, {SiteName} tarafýndan telif ve benzeri hiç bir ücret talep etmeyecektir.<br/>
                            <br/>
                            {SiteName} üyenin ilettiði kiþisel bilgilerin içerik saðlayýcýlar ve web servisleri kullanýcýlarýna üyenin özlük haklarýný taciz etmeyecek þekilde gerekli iletiþim, tanýtým, mal teslimi, reklam vb. amaçlar için kullandýrýlmasý konusunda tam yetkilidir.<br/>
                            <br/>
                            {SiteName}, üyenin baþka web-sitelerine geçiþini saðlayabilir. Bu taktirde üye geçiþ yapacaðý sitelerin içeriðinden {SiteName} tarafýnýn sorumlu olmadýðýný kabul eder.<br/>
                            <br/>
                            {SiteName}, ileride doðacak teknik zaruretler ve mevzuata uyum amacýyla üyelere haber vermek zorunda olmadan iþbu sözleþmenin uygulamasýnda deðiþiklikler yapabileceði gibi mevcut maddelerini deðiþtirebilir, iptal edebilir veya yeni maddeler ilave edebilir. Üyeler sözleþmeyi her zaman bu sayfanýn adresinde bulabilir ve okuyabilirler. Servisler ile ilgili deðiþiklikler site içerisinde duyurulacaktýr ve gerektiðinde üyenin hizmetlerden yararlanabilmesi için sözleþme deðiþikliklerini ilgili butonu týklamak suretiyle onaylamasý istenecektir.<br/>
                            <br/>
                            {SiteName} üyelik gerektirmeyen servisleri zaman içerisinde üyelik gerektiren bir duruma dönüþtürülebilir. {SiteName} ilave servisler açabilir, bazý servislerini kýsmen veya tamamen deðiþtirebilir, veya ücretli hale dönüþtürebilir.<br/>
                            <br/>
                            <b>6. {SiteName} kayýtlarýnýn geçerliliði</b><br/>
                            <br/>
                            Üye bu sözleþmeden doðabilecek ihtilaflarda {SiteName} tarafýnýn defter kayýtlarýyla, mikrofilm, mikrofiþ, ve bilgisayar kayýtlarýnýn HUMK 287. Madde anlamýnda muteber, baðlayýcý, kesin ve münhasýr delil teþkil edeceðini ve bu maddenin delil sözleþmesi niteliðinde olduðunu, belirten {SiteName} kayýtlarýna her türlü itiraz ve bunlarýn usulüne uygun tutulduðu hususunda yemin teklif hakkýndan peþinen feragat ettiðini kabul, beyan ve taahhüt eder.<br/>
                            <br/>
                            <b>7. Uygulanacak Hükümler</b><br/>
                            <br/>
                            Bu sözleþmeyle ilgili olarak çýkabilecek itilaflarda öncelikle bu sözleþmedeki hükümler, hüküm bulunmayan hallerde ise Türk Kanunlarý (BK, TTK, HUMK, MK ve sair) uygulanacaktýr.<br/>
                            <br/>
                            <b>8. Yetkili Mahkeme ve Ýcra Daireleri</b><br/>
                            <br/>
                            Ýþbu sözleþmenin uygulanmasýndan doðabilecek her türlü uyuþmazlýklarýn çözümünde Ýstanbul Merkez(Sultanahmet) Mahkemeleri ile Ýcra Müdürlükleri yetkili olacaktýr.<br/>
                            <br/>
                            <b>9. Yürürlük</b><br/>
                            <br/>
                            Kullanýcý kayýt formunu doldurup formun altýnda bulunan ""kabul ediyorum"" butonunu týkladýðýnda bu sözleþme taraflar arasýnda süresiz olarak yürürlüðe girer.<br/>
                            <br/>
                            <b>10. Fesih<br/>
                            <br/>
                            {SiteName} dilediði zaman bu sözleþmeyi sona erdirebilir.<br/>
                        </div>
                        <input type=""checkbox"" name=""cbAcceptRules"" value=1/> Sözleþmeyi kabul ediyorum.
            ";
            string uyelikSozlesmesiCheck = @" onclick=""if(!this.form.cbAcceptRules.checked) {alert('Sözleþmeyi kabul etmelisiniz.'); return false;}""";
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

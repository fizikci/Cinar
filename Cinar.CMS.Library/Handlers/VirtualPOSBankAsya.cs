using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using System.Linq;

namespace OnlineSinav.Web.Library.Handlers
{
    public class VirtualPOSBankAsya : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string merchantId = Provider.AppSettings["merchantId"];
                string merchantPassword = Provider.AppSettings["merchantPassword"];
                double amount = 0d;
                string brandId = Provider.Request["CardType"];
                string pan = "";
                // Remove non-digits
                for (int i = 0; i < Provider.Request["CardNumber"].Length; i++)
                {
                    if (char.IsDigit(Provider.Request["CardNumber"], i))
                        pan += Provider.Request["CardNumber"][i].ToString();
                }

                string expiryDate = Provider.Request["ExpiryYear"] + Provider.Request["ExpiryMonth"];
                string cvv2 = Provider.Request["CVV2"];

                CardType ct = (CardType) Enum.Parse(typeof (CardType), brandId);
                string cardValid = Utility.ValidateCreditCardNumber(ct, pan);

                if (cardValid != "")
                {
                    context.Response.Write("HATA: " + cardValid);
                    return;
                }

                string request = createSaleRequest(merchantId, merchantPassword, amount, pan, expiryDate, cvv2, brandId);
                string response = send(request);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNode node = doc.SelectSingleNode("//HostResponse/@ResultCode");
                int sonuc = int.Parse(node.Value);

                if (sonuc == 0)
                {
                    try
                    {
                        PaymentTransaction s = new PaymentTransaction();
                        s.Amount = (int)(amount * 100);
                        s.CheckoutType = CheckoutTypes.CreditCard;
                        s.Result = response;
                        s.Save();
                    }
                    catch (Exception ex)
                    {
                        string msg = amount+" TL tahsil edildi. Bu bilgi bir hata nedeniyle veritabanına kaydedilemedi. Lütfen bu bilgiyi manuel olarak kaydediniz.";
                        Provider.SendMail("Tahsilat yapıldı ama sisteme kaydedilemedi!!!", msg);
                        Provider.Log("Checkout", "Error", msg);
                    }

                    context.Response.Write("OK");
                }
                else
                {
                    context.Response.Write("HATA: Ödemenizin onaylanmasında bir hata oluştu.");
                    Provider.Log("Checkout", "Error", amount+" TL tahsil edilmek istendi ama servisten " + sonuc + " nolu hata döndü. (" + (resultCodes.ContainsKey(sonuc) ? resultCodes[sonuc] : "??") + ")");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("HATA: " + ex.Message);
                Provider.Log("Checkout", "Error", ex.Message + (ex.InnerException != null ? "(" + ex.InnerException.Message + ")" : ""));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        private string createSaleRequest(string merchantId, string merchantPassword, double amount, string pan, string expiryDate, string cvv2, string brandId)
        {
            string request = "";
            try
            {
                XmlNode node = null;
                XmlDocument _msgTemplate = new XmlDocument();
                _msgTemplate.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><ePaymentMsg VersionInfo=\"2.0\" TT=\"Request\" RM=\"Direct\" CT=\"Money\">" +
                    "<Operation ActionType=\"Sale\"><OpData><MerchantInfo MerchantId=\"\" MerchantPassword=\"\" />" +
                    "<ActionInfo><TrnxCommon TrnxID=\"\" Protocol=\"156\"><AmountInfo Amount=\"0\" Currency=\"792\" /></TrnxCommon><PaymentTypeInfo>" +
                    "<InstallmentInfo NumberOfInstallments=\"0\" /></PaymentTypeInfo></ActionInfo><PANInfo PAN=\"\" ExpiryDate=\"\" CVV2=\"\" BrandID=\"\" />" +
                    "<OrderInfo><OrderLine>0</OrderLine></OrderInfo><OrgTrnxInfo /><CustomData></CustomData></OpData></Operation></ePaymentMsg>");
                node = _msgTemplate.SelectSingleNode("//ePaymentMsg/Operation/OpData/MerchantInfo");
                node.Attributes["MerchantId"].Value = merchantId;
                node.Attributes["MerchantPassword"].Value = merchantPassword;
                node = _msgTemplate.SelectSingleNode("//ePaymentMsg/Operation/OpData/ActionInfo/TrnxCommon");
                node.Attributes["TrnxID"].Value = Guid.NewGuid().ToString();
                node = _msgTemplate.SelectSingleNode("//ePaymentMsg/Operation/OpData/ActionInfo/TrnxCommon/AmountInfo");
                string strAmount = amount.ToString("####.00");
                strAmount = strAmount.Replace(",", ".");
                node.Attributes["Amount"].Value = strAmount;
                node.Attributes["Currency"].Value = "949"; // 949:TL, 840:USD
                node = _msgTemplate.SelectSingleNode("//ePaymentMsg/Operation/OpData/ActionInfo/PaymentTypeInfo/InstallmentInfo");
                node.Attributes["NumberOfInstallments"].Value = "0";
                node = _msgTemplate.SelectSingleNode("//ePaymentMsg/Operation/OpData/PANInfo");
                node.Attributes["PAN"].Value = pan;
                node.Attributes["ExpiryDate"].Value = expiryDate;
                node.Attributes["CVV2"].Value = cvv2;
                node.Attributes["BrandID"].Value = brandId;
                request = _msgTemplate.OuterXml;
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string send(string request)
        {
            try
            {
                string postData = "";
                string responseData = "";
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("ISO-8859-9");

                postData = "https://vps.bankasya.com.tr/iposnet/sposnet.aspx?prmstr=[DATA]";
                postData = postData.Replace("[DATA]", request);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(postData);
                webReq.Timeout = Convert.ToInt32(40000);
                webReq.KeepAlive = false;
                WebResponse webResp = webReq.GetResponse();
                Stream respStream = webResp.GetResponseStream();

                byte[] buffer = new byte[10000];
                int len = 0, r = 1;
                while (r > 0)
                {
                    r = respStream.Read(buffer, len, 10000 - len);
                    len += r;
                }
                respStream.Close();
                responseData = encoding.GetString(buffer, 0, len).Replace("\r", "").Replace("\n", "");
                return responseData;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private Dictionary<int, string> resultCodes = new Dictionary<int, string>()
        {
            {0,	"İşlem onaylandı ve başarıyla tamamlandı."},
            {1,	"Bankanızı arayın. Kullanılan kredi kartının bankası işleme onay vermemektedir."},
            {2,	"Bankanızı arayın. Özel bir durum oluştu."},
            {3,	"Geçersiz üye işyeri ya da servis sağlayıcı."},
            {4,	"Karta el koy. Kart bloke edilmiş durumda."},
            {5,	"Red. İşlem onaylanmadı. Kart blokeli ya da bilgileri hatalı girilmiş olabilir."},
            {6,	"Hata oluştu. (Sadece dosya güncelleme işlemlerinde oluşur.)"},
            {7,	"Karta el koy. Özel bir durum oluştu."},
            {8,	"Kartın kimlik kontrolü başarısız."},
            {9,	"Tekrar deneyin."},
            {11, "Onaylandı (VIP)."},
            {12, "Red. Geçersiz İşlem. Tanımsız işlem yetkisi."},
            {13, "Red. İşlem tutarı hatalı. "},
            {14, "Hatalı kart numarası."},
            {15, "Red. Geçersiz kart banka bilgisi."},
            {25, "Kayıt dosyada bulunamadı."},
            {28, "Kart red edildi."},
            {29, "Kart bilgileri bulunamadı."},
            {30, "Bankanızı arayın. Format hatası."},
            {33, "Kart zaman aşımına uğramış. Karta el koy."},
            {36, "Red. Kısıtlanmış kart. Karta el koy."},
            {38, "Red. Hatalı şifre giriş limiti aşıldı. Karta el koy."},
            {41, "Red. Kayıp kart. Karta el koy."},
            {43, "Red. Çalınıtı kart. Karta el koy."},
            {51, "Red. Yetersiz bakiye."},
            {52, "Red. Cari hesap bulunamadı."},
            {53, "Red. Mevduat hesabı bulunamadı."},
            {54, "Red. Vadesi dolmuş kart"},
            {55, "Red. Hatalı PIN. Tekrar şifre giriniz."},
            {57, "Red. İşlem tipi karta kapalı."},
            {58, "Red. Terminalin işlem tipine yetkisi yok."},
            {61, "Bankanızı arayın. Kart günlük nakit çekim tutarı aşıldı."},
            {62, "Red. Kısıtlı kart."},
            {63, "Red. Güvenlik ihlali."},
            {65, "Bankanızı arayın. Kartın aylık nakit çekim adet limiti aşıldı. "},
            {75, "Bankanızı arayın. Şifre deneme sayısı aşıldı."},
            {76, "Senkronizasyon hatası."},
            {77, "İşlem isteği red edildi. "},
            {78, "Red. Yeni PIN'in güvenli PIN koşullarını sağlamıyor. "},
            {79, "Red. ARQC hatası. İşlem talebi onaylanmadı."},
            {80, "Yığın hatası."},
            {81, "Bankanızı arayın."},
            {82, "Geçersiz CVC2 veya CVV2."},
            {85, "İşlem talebi onaylandı. (PIN yönetim mesajları için geçerlidir.)"},
            {89, "Bankanızı arayın. Eprom hatası veya seri numarası uyuşmazlığı."},
            {91, "Red. Bağlantı yok. Karşı bankadan yanıt alınamıyor."},
            {92, "Red. Routing için finansal kurum bilinmiyor."},
            {93, "Geçersiz kart numarası."},
            {96, "Red. Bağlantı yok. Sistem arızası. "},
            {98, "İşlem daha önce iptal edilmiş."},
            {99, "Bankanızı arayın. Terminal YTL kuruyla işleme kapalı."},
            {-1, "Uygun (boş) terminal bulunamadı."},

        };
    }
}
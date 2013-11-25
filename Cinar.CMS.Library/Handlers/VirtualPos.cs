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

namespace Cinar.CMS.Library.Handlers
{
    public abstract class VirtualPOS : IHttpHandler, IRequiresSessionState
    {
        public static int AmountInCents {
            get {
                if (Provider.Session["virtualpos_amount"] == null)
                    Provider.Session["virtualpos_amount"] = 0;
                return (int) Provider.Session["virtualpos_amount"]; 
            }
            set { Provider.Session["virtualpos_amount"] = value;}
        }

        protected CardType CardType;
        protected string CardNumber;
        protected string ExpiryDate;
        protected string CVV2;

        protected string Response;
        protected int ResponseErrorNo;
        protected string ResponseErrorMessage;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                CardNumber = "";
                // Remove non-digits
                for (int i = 0; i < Provider.Request["CardNumber"].Length; i++)
                {
                    if (char.IsDigit(Provider.Request["CardNumber"], i))
                        CardNumber += Provider.Request["CardNumber"][i].ToString();
                }

                int year = 0;
                int.TryParse(Provider.Request["ExpiryYear"], out year);
                if (year < DateTime.Now.Year)
                    throw new Exception("Son kullanma tarihi yanlış");

                int month = 0;
                int.TryParse(Provider.Request["ExpiryMonth"], out month);
                if (!(month>=1 && month<=12))
                    throw new Exception("Son kullanma tarihi yanlış");

                ExpiryDate = Provider.Request["ExpiryYear"] + Provider.Request["ExpiryMonth"];
                CVV2 = Provider.Request["CVV2"];

                if (CVV2.Length != 3 || !CVV2.CanConvertToInteger())
                    throw new Exception("CCV2 kodu hatalı");

                CardType = (CardType) Enum.Parse(typeof (CardType), Provider.Request["CardType"]);
                string cardValid = Utility.ValidateCreditCardNumber(CardType, CardNumber);

                if (cardValid != "")
                {
                    context.Response.Write("HATA: " + cardValid);
                    return;
                }


                if (Transact())
                {
                    try
                    {
                        PaymentTransaction s = new PaymentTransaction();
                        s.Amount = AmountInCents;
                        s.CheckoutType = CheckoutTypes.CreditCard;
                        s.Result = Response;
                        s.Save();
                    }
                    catch (Exception ex)
                    {
                        string msg = (AmountInCents/100d)+" TL tahsil edildi. Bu bilgi bir hata nedeniyle veritabanına kaydedilemedi. Lütfen bu bilgiyi manuel olarak kaydediniz.";
                        Provider.SendMail("Tahsilat yapıldı ama sisteme kaydedilemedi!!!", msg);
                        Provider.Log("Error", "Checkout", msg);
                    }

                    context.Response.Write("OK");
                }
                else
                {
                    context.Response.Write("HATA: Ödemenizin onaylanmasında bir hata oluştu.");
                    Provider.Log("Error", "Checkout", (AmountInCents/100d)+" TL tahsil edilmek istendi ama servisten " + ResponseErrorNo + " nolu hata döndü. (" + ResponseErrorMessage + ")");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("HATA: " + ex.Message);
                Provider.Log( "Error","Checkout", ex.Message + (ex.InnerException != null ? "(" + ex.InnerException.Message + ")" : ""));
            }
        }

        protected abstract bool Transact();

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}
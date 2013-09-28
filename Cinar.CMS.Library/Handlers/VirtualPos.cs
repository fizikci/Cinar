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
        public static double Amount {
            get {
                if (Provider.Session["virtualpos_amount"] == null)
                    Provider.Session["virtualpos_amount"] = 0d;
                return (double) Provider.Session["virtualpos_amount"]; 
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

                ExpiryDate = Provider.Request["ExpiryYear"] + Provider.Request["ExpiryMonth"];
                CVV2 = Provider.Request["CVV2"];

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
                        s.Amount = (int)(Amount * 100);
                        s.CheckoutType = CheckoutTypes.CreditCard;
                        s.Result = Response;
                        s.Save();
                    }
                    catch (Exception ex)
                    {
                        string msg = Amount+" TL tahsil edildi. Bu bilgi bir hata nedeniyle veritabanına kaydedilemedi. Lütfen bu bilgiyi manuel olarak kaydediniz.";
                        Provider.SendMail("Tahsilat yapıldı ama sisteme kaydedilemedi!!!", msg);
                        Provider.Log("Checkout", "Error", msg);
                    }

                    context.Response.Write("OK");
                }
                else
                {
                    context.Response.Write("HATA: Ödemenizin onaylanmasında bir hata oluştu.");
                    Provider.Log("Checkout", "Error", Amount+" TL tahsil edilmek istendi ama servisten " + ResponseErrorNo + " nolu hata döndü. (" + ResponseErrorMessage + ")");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("HATA: " + ex.Message);
                Provider.Log("Checkout", "Error", ex.Message + (ex.InnerException != null ? "(" + ex.InnerException.Message + ")" : ""));
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
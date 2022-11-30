using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class nap_tien_chuyen_khoan_tu_dong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] != null)
                {

                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }

        [WebMethod]
        public static string GetBankInfo(string BankID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentdate = DateTime.Now;
            var obj_user = AccountController.GetByUsername(username);
            {
                string url = "http://tt.mona.media/paymentservice.asmx/GetBankInfo?UID=4&Key=monasms-autobanking&BankID=" + BankID + "";
                var s = PJUtils.ConnectApi(url);
                if (s != null)
                {
                    Resp obj = JsonConvert.DeserializeObject<Resp>(s);
                    var k = obj;
                    if (k.Code == 102)
                    {
                        PaymentInfo p = new PaymentInfo();
                        p.BankID = k.bank.ID;
                        p.BankName = k.bank.BankName;
                        p.BankNumber = k.bank.BankNumber;
                        p.AccountHolder = k.bank.AccountHolder;
                        p.BankBrance = k.bank.BankBrance;
                        string uKey = "";
                        if (obj_user.Username.Length > 3)
                        {
                            uKey = obj_user.Username.Substring(0, 3);
                        }
                        else
                        {
                            uKey = obj_user.Username;
                        }

                        string min = currentdate.Minute.ToString();
                        string mil = currentdate.Millisecond.ToString();

                        p.Note = uKey.ToUpper() + PJUtils.RandomStringWithText(4).ToUpper() + min + mil;
                        HistoryAutoBankingController.Insert(obj_user.ID, p.Note, DateTime.Now, obj_user.Username);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        return serializer.Serialize(p);
                    }
                    else
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        return serializer.Serialize(null);
                    }
                }
                else
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(null);
                }
            }

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Credentials = CredentialCache.DefaultCredentials;
            //request.Method = "GET";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    var encoding = ASCIIEncoding.ASCII;

            //    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            //    {
            //        string responseText = reader.ReadToEnd();
            //        var s = responseText;
            //        Resp obj = JsonConvert.DeserializeObject<Resp>(s);
            //        var k = obj;
            //        JavaScriptSerializer serializer = new JavaScriptSerializer();
            //        return serializer.Serialize(k);
            //    }
            //}
        }

        [WebMethod]
        public static string CheckPayment(string BankID, string Note)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            {
                string url = "http://tt.mona.media/paymentservice.asmx/CheckPayment?UID=4&Key=monasms-autobanking&BankID=" + BankID + "&Note=" + Note + "";
                var s = PJUtils.ConnectApi(url);
                if (s != null)
                {
                    Payment obj = JsonConvert.DeserializeObject<Payment>(s);
                    var k = obj;
                    if (k.Code == 102)
                    {
                        string[] ct = k.Message.Split('|');
                        double Amount = Convert.ToDouble(ct[0]);
                        int HpayID = ct[1].ToInt();
                        AccountController.UserWallet_Auto(obj_user.ID, Convert.ToDouble(Amount), Convert.ToInt32(HpayID), Note);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        return serializer.Serialize(k);
                    }
                    else
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        return serializer.Serialize(null);
                    }
                }
                else
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(null);
                }
            }

        }

        public class Payment
        {
            public int Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
        }

        public class Resp
        {
            public int Code { get; set; }
            public string Status { get; set; }
            public Bank bank { get; set; }
        }

        public class Bank
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string AccountHolder { get; set; }
            public string BankName { get; set; }
            public string BankNumber { get; set; }
            public string BankBrance { get; set; }
            public bool IsHidden { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public bool IsUse { get; set; }
        }

        public class PaymentInfo
        {
            public int BankID { get; set; }
            public string Note { get; set; }
            public string BankName { get; set; }
            public string BankNumber { get; set; }
            public string AccountHolder { get; set; }
            public string BankBrance { get; set; }
        }
    }
}
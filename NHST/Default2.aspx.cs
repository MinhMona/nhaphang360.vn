using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;


namespace NHST
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "hopco";
            //PJUtils.SendMailGmail_new( "phuong.nguyenthanh90@gmail.com", 
            //    "Reset Mật khẩu trên Vận chuyển đa quốc gia", "Mật khẩu mới của bạn trên hệ thống Vận chuyển đa quốc gia: abc", "");
            LoadConfiguration();
            //Response.Write(PJUtils.Decrypt("userpass", "tTBOzjnB40wZxAeu1GKBew==") +"<br/>");
            //Response.Write(PJUtils.Decrypt("userpass", "zhwzmrQIK2wBxnqxvtjkpA=="));
        }
        public void sendMessage()
        {
            //string kq = ESMS.Send("+84934064443", "test tin nhắn");
            //Response.Write(kq);
            try
            {
                PJUtils.SendMailGmail_new( 
                    "phuong.nguyenthanh90@gmail.com", "Reset Mật khẩu trên NHẬP HÀNG 360",
                    "Mật khẩu mới của bạn trên hệ thống NHẬP HÀNG 360: <strong>hehe</strong>", "");
            }
            catch { }
        }
        [WebMethod]
        public static string UpdateNotification(int ID, string UserName)
        {
            string ret = NotificationController.UpdateStatus(ID, 1, DateTime.Now, UserName);
            return ret;
        }
        [WebMethod]
        public static string closewebinfo()
        {
            HttpContext.Current.Session["infoclose"] = "ok";
            return "ok";
        }
        [WebMethod]
        public static string setIsread()
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                NotificationController.UpdateStatus_SQL(username, 1);
            }
            return "ok";
        }
        public void LoadConfiguration()
        {
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                ltr_Email.Text += "<a class=\"img\" href=\"mailto:"+ c.EmailSupport + "\"><i class=\"fa fa-envelope\"></i></a>";
                ltr_Email.Text += "<a class=\"info\" href=\"mailto:" + c.EmailSupport + "\"><div class=\"title\"><strong>Email Contact</strong></div><p>" + c.EmailSupport + "</p></a>";
                ltr_time.Text = c.TimeWork;
                ltr_Hotline.Text += "<a class=\"img\" href=\"tel:" + c.Hotline + "\"><i class=\"fa fa-phone\"></i></a>";
                ltr_Hotline.Text += "<a class=\"info\" href=\"tel:" + c.Hotline + "\"><div class=\"title\"><strong>HOtline</strong></div><p>" + c.Hotline + "</p></a>";
                //ltrEmailSupportHotlineCurrency.Text += "<a href=\"javascript:;\" class=\"contact-link\"><i class=\"fa fa-yen\"></i>1 = " + string.Format("{0:N0}", Convert.ToDouble(c.Currency)) + "</a>";
                //ltrEmailSupportHotlineCurrency.Text += "<a href=\"mailto:" + c.EmailSupport + "\" class=\"contact-link\"><i class=\"fa fa-envelope\"></i>" + c.EmailSupport + "</a>";
                
                ////ltrEmailSupportHotlineCurrency.Text += "<a href=\"tel:" + c.Hotline + "\" class=\"contact-link\"><i class=\"fa fa-phone\"></i>" + c.Hotline + "</a>";


                //ltr_Social.Text += "<li><a href=\"" + c.Facebook + "\"><i class=\"fa fa-facebook\"></i></a></li>";
                //ltr_Social.Text += "<li><a href=\"" + c.Twitter + "\"><i class=\"fa fa-twitter\"></i></a></li>";
                //ltr_Social.Text += "<li><a href=\"" + c.Instagram + "\"><i class=\"fa fa-instagram\"></i></a></li>";
                //ltr_Social.Text += "<li><a href=\"" + c.Skype + "\"><i class=\"fa fa-skype\"></i></a></li>";

                //ltr_Social1.Text += "<li class=\"social\"><a href=\"" + c.Facebook + "\"><i class=\"fa fa-facebook\"></i></a></li>";
                //ltr_Social1.Text += "<li class=\"social\"><a href=\"" + c.Twitter + "\"><i class=\"fa fa-twitter\"></i></a></li>";
                //ltr_Social1.Text += "<li class=\"social\"><a href=\"" + c.Instagram + "\"><i class=\"fa fa-instagram\"></i></a></li>";
                //ltr_Social1.Text += "<li class=\"social\"><a href=\"" + c.Skype + "\"><i class=\"fa fa-skype\"></i></a></li>";

                //ltr_Hotline.Text += "<p><a href=\"tel:" + c.Hotline + "\">" + c.Hotline + "</a></p>";
                //ltr_Timework.Text += "<p><a href=\"javascript:;\">" + c.TimeWork + "</a></p>";

                //ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Hotline: <br/>" + c.Hotline + "</a></li>";
                //ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Email: <br/>" + c.EmailContact + "</a></li>";
                //ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Địa chỉ: <br/>" + c.Address + "</a></li>";
            }
        }
    }
}
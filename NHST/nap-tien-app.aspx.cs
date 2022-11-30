using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class nap_tien_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadBank();
                LoadDLL();
            }
        }

        public void LoadDLL()
        {
            List<Bank> lb = new List<Bank>();
            var bank = BankController.GetAll();
            if (bank.Count > 0)
            {
                foreach (var item in bank)
                {
                    Bank nb = new Bank();
                    nb.ID = item.ID;
                    nb.BankInfo = item.BankName + " - " + item.BankNumber + " - " + item.Branch + " - " + item.AccountHolder;
                    lb.Add(nb);
                }
            }

            if (lb.Count > 0)
            {
                ddlBank.DataSource = lb;
                ddlBank.DataBind();
            }
        }

        public class Bank
        {
            public int ID { get; set; }
            public string BankInfo { get; set; }
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);

            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    pnMobile.Visible = true;
                    var user = AccountController.GetByID(UID);
                    if (user != null)
                    {
                        //ltrBalance.Text = string.Format("{0:N0}", user.Wallet) + " vnđ";
                        ltrIfn.Text = user.Username;
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }
        public void LoadBank()
        {
            List<Bank> lb = new List<Bank>();
            var bank = BankController.GetAll();
            if (bank.Count > 0)
            {
                foreach (var item in bank)
                {
                    Bank nb = new Bank();
                    nb.ID = item.ID;
                    nb.BankInfo = item.BankName + " - " + item.AccountHolder + " - " + item.BankNumber + " - " + item.Branch;
                    lb.Add(nb);

                    ltrBank.Text += "<div class=\"col s12 m6 l4\">";
                    ltrBank.Text += "<div class=\"bank-wrap card-panel hoverable\">";
                    ltrBank.Text += "<hr />";
                    ltrBank.Text += "<p class=\"teal-text text-darken-4 font-weight-800 mt-0 display-flex space-between\">Tên ngân hàng: <span>" + item.BankName + "</span>";
                    ltrBank.Text += "</p>";
                    ltrBank.Text += "<div class=\"flex-row\">";
                    ltrBank.Text += "<div class=\"text-grow\">Chủ tài khoản: " + item.AccountHolder + "</div>";
                    ltrBank.Text += "</div>";
                    ltrBank.Text += "<div class=\"flex-row\">";
                    ltrBank.Text += "<div class=\"text-grow\">Số tài khoản: " + item.BankNumber + "</div>";
                    ltrBank.Text += "</div>";
                    ltrBank.Text += "<div class=\"flex-row\">";
                    ltrBank.Text += "<div class=\"text-grow\">Chi nhánh: " + item.Branch + "";
                    ltrBank.Text += "</div>";
                    ltrBank.Text += "</div>";
                    ltrBank.Text += "</div>";
                    ltrBank.Text += "</div>";
                }
            }

            if (lb.Count > 0)
            {
                ddlBank.DataSource = lb;
                ddlBank.DataBind();
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            DateTime currentDate = DateTime.Now;
            int UID = ViewState["UID"].ToString().ToInt(0);
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                string kq = AdminSendUserWalletController.Insert(u.ID, u.Username, Convert.ToDouble(pAmount.Value), 1, Convert.ToInt32(ddlBank.SelectedValue), txtNote.Text, currentDate, u.Username);
                if (kq.ToInt(0) > 0)
                {
                    var setNoti = SendNotiEmailController.GetByID(3);
                    if (setNoti != null)
                    {
                        if (setNoti.IsSentNotiAdmin == true)
                        {

                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationsController.Inser(admin.ID, admin.Username, kq.ToInt(), "Có yêu cầu nạp tiền mới.", 2, currentDate, u.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "manager/HistorySendWallet/";
                                    PJUtils.PushNotiDesktop(admin.ID, " có yên cầu nạp tiền mới.", datalink);
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {


                                    NotificationsController.Inser(manager.ID, manager.Username, kq.ToInt(), "Có yêu cầu nạp tiền mới.",
                                    1, currentDate, u.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "manager/HistorySendWallet/";
                                    PJUtils.PushNotiDesktop(manager.ID, " có yên cầu nạp tiền mới.", datalink);
                                }
                            }
                        }

                        if (setNoti.IsSentEmailAdmin == true)
                        {
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new(admin.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Có yêu cầu nạp tiền mới từ tài khoản " + u.Username + " với số tiền " + Convert.ToDouble(pAmount.Value) + "", "");
                                    }
                                    catch { }
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new(manager.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Có yêu cầu nạp tiền mới từ tài khoản " + u.Username + " với số tiền " + Convert.ToDouble(pAmount.Value) + "", "");
                                    }
                                    catch { }
                                }
                            }

                        }
                        var c = ConfigurationController.GetByTop1();
                        //var new_wallet = u.Wallet + (Convert.ToDouble(pAmount.Value));
                        if (setNoti.IsSendEmailUser == true)
                        {
                            try
                            {
                                StringBuilder html = new StringBuilder();
                                html.Append("<!DOCTYPE html>");
                                html.Append("<html lang=\"en\">");
                                html.Append("<head>");
                                html.Append("   <meta charset=\"UTF-8\">");
                                html.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                                html.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">");
                                html.Append("<title>Document</title>");
                                html.Append("</head>");
                                html.Append("<body style=\"margin: 0; padding:0\">");
                                html.Append("<table style=\"font-family: sans-serif; font-size: 14px; border-collapse: collapse; width: 500px; max-width: 100%; margin: auto\">");
                                html.Append("<tr>");
                                html.Append("<td style=\"padding: 10px; background-color: #fca777; color: #fff; text-align: center\"><strong><p>KÍNH CHÀO QUÝ KHÁCH!</p><p><a style=\"text-decoration: none\" href=\"https://nhaphang360.vn/\" target=\"_blank\"><strong>NHẬP HÀNG 360<strong></a> THÔNG BÁO NẠP VÍ THÀNH CÔNG</p></strong></td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>THÔNG TIN GIAO DỊCH</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Tên KH: " + AccountInfoController.GetByUserID(u.ID).FirstName + " " + AccountInfoController.GetByUserID(u.ID).LastName + " </td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>SĐT:  " + AccountInfoController.GetByUserID(u.ID).Phone + "</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Username: " + u.Username + "</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Số tiền nạp ví: " + string.Format("{0:N0}", (Convert.ToDouble(pAmount.Value))) + " VNĐ</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Số dư hiện tại: " + string.Format("{0:N0}", u.Wallet) + " VNĐ</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>ID nạp ví: " + u.ID + "</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Thời gian: " + AdminSendUserWalletController.GetByUID_New(u.ID).CreatedDate + "</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>Quý khách vui lòng truy cập tài khoản để kiểm tra chi tiết.</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td><a style=\"text-decoration: none\" href=\"https://nhaphang360.vn/\" target=\"_blank\"><strong>NHẬP HÀNG 360<strong></a> chân thành cảm ơn!</td>");
                                html.Append("</tr>");
                                html.Append("<tr>");
                                html.Append("<td>==============================</td>");
                                html.Append("<tr>");
                                html.Append("<td>Mọi thắc mắc xin vui lòng liên hệ: <a href=\"tel:"+ c.Hotline + "\">"+ c.Hotline + "</a></td>");
                                html.Append("</tr>");
                                html.Append("</tr>");
                                html.Append("</table>");
                                html.Append("</body>");
                                html.Append("</html>");

                                //"NHẬP HÀNG 360 THÔNG BÁO NẠP VÍ THÀNH CÔNG",
                                //                        "Kính Chào Quý Khách! <br>" +
                                //                        "THÔNG TIN GIAO DỊCH <br>" +
                                //                        "Tên KH: " + AccountInfoController.GetByUserID(u.ID).LastName + "<br>" +
                                //                        "SĐT: " + AccountInfoController.GetByUserID(u.ID).Phone + "<br>" +
                                //                        "Username: " + u.Username + "<br>" +
                                //                        "Số tiền nạp ví: " + string.Format("{0:N0}", (Convert.ToDouble(pAmount.Value))).Replace(",", ".") + " VNĐ" + "<br>" +
                                //                        //"Số dư hiện tại: " + string.Format("{0:N0}", u.Wallet).Replace(",", ".") + " VNĐ"  +"<br>" +
                                //                        "ID nạp ví: " + u.ID + "<br>" +
                                //                        "Thời gian: " + AdminSendUserWalletController.GetByUID_New(u.ID).CreatedDate + "<br>" +
                                //                        "Quý khách vui lòng truy cập tài khoản để kiểm tra chi tiết.<br>" +
                                //                        "BEE - SHIP.com chân thành cảm ơn! <br>" +
                                //                        "Mọi thắc mắc xin vui lòng liên hệ: 09879 04 078", "");


                                PJUtils.SendMailGmail_new(u.Email,
                                                        "NHẬP HÀNG 360 THÔNG BÁO GỬI YÊU CẦU NẠP VÍ THÀNH CÔNG",
                                                        "Quý khách " + u.Username + " gửi thông tin thành công, vui lòng chờ admin kiểm duyệt.", "");

                            }
                            catch { }
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlert("Gửi thông tin thành công, vui lòng chờ admin kiểm duyệt", "s", true, Page);
                }
            }
        }
    }
}
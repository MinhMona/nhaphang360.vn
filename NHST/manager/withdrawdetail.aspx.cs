using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using System.Text;

namespace NHST.manager
{
    public partial class withdrawdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 7)
                        Response.Redirect("/trang-chu");
                    loaddata();
                }
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var w = WithdrawController.GetByID(id);
                if (w != null)
                {
                    ViewState["ID"] = id;
                    int status = w.Status.ToString().ToInt(1);
                    lblUsername.Text = w.Username;
                    pAmount.Value = w.Amount;
                    ddlStatus.SelectedValue = status.ToString();
                    txtNote.Text = w.Note;
                    if (status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;
                }
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int id = ViewState["ID"].ToString().ToInt();
            var setNoti = SendNotiEmailController.GetByID(4);
            var w = WithdrawController.GetByID(id);
            string BackLink = "/manager/Withdraw-List.aspx";
            if (w != null)
            {
                if (w.Status < 3)
                {
                    int status = ddlStatus.SelectedValue.ToString().ToInt(1);
                    if (status == 2)
                    {
                        var acc = AccountController.GetByUsername(username);
                        if (acc.RoleID == 7)
                        {
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Bạn không có quyền duyệt yêu cầu này", "e", true, BackLink, Page);
                        }
                        else if (acc.RoleID == 0)
                        {
                            int uid_rut = w.UID.ToString().ToInt();
                            var user_rut = AccountController.GetByID(uid_rut);
                            if (user_rut != null)
                            {
                                WithdrawController.UpdateStatus(id, status, currentDate, username);

                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiUser == true)
                                    {
                                        NotificationsController.Inser(user_rut.ID, user_rut.Username, 0, "Admin đã duyệt lệnh rút tiền của bạn.",
3, currentDate, username, true);
                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                        string datalink = "" + strUrl + "rut-tien/";
                                        PJUtils.PushNotiDesktop(user_rut.ID, "Admin đã duyệt lệnh rút tiền của bạn.", datalink);
                                    }
                                    var c = ConfigurationController.GetByTop1();
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
                                            html.Append("<td style=\"padding: 10px; background-color: #fca777; color: #fff; text-align: center\"><strong><p>KÍNH CHÀO QUÝ KHÁCH!</p><p><a style=\"text-decoration: none\" href=\"https://nhaphang360.vn/\" target=\"_blank\"><strong>NHẬP HÀNG 360<strong></a> THÔNG BÁO RÚT VÍ THÀNH CÔNG</p></strong></td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>THÔNG TIN GIAO DỊCH</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Tên KH: " + AccountInfoController.GetByUserID(user_rut.ID).FirstName + " " + AccountInfoController.GetByUserID(user_rut.ID).LastName + " </td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>SĐT:  " + AccountInfoController.GetByUserID(user_rut.ID).Phone + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Username: " + user_rut.Username + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Số tiền rút ví: " + string.Format("{0:N0}", w.Amount) + " VNĐ</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Số dư hiện tại: " + string.Format("{0:N0}", user_rut.Wallet) + " VNĐ</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>ID rút ví: " + user_rut.ID + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Thời gian: " + AdminSendUserWalletController.GetByUID_New(user_rut.ID).CreatedDate + "</td>");
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
                                            html.Append("<td>Mọi thắc mắc xin vui lòng liên hệ: <a href=\"tel:" + c.Hotline + "\">" + c.Hotline + "</a></td>");
                                            html.Append("</tr>");
                                            html.Append("</tr>");
                                            html.Append("</table>");
                                            html.Append("</body>");
                                            html.Append("</html>");




                                            PJUtils.SendMailGmail_new( user_rut.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", html.ToString(), "");
                                        }
                                        catch { }
                                    }
                                }
                                
                                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                            }

                        }
                    }
                    else if (status == 3)
                    {
                        int uid_rut = w.UID.ToString().ToInt();
                        var user_rut = AccountController.GetByID(uid_rut);
                        if (user_rut != null)
                        {
                            double wallet = Convert.ToDouble(user_rut.Wallet.ToString());
                            double amount = Convert.ToDouble(w.Amount.ToString());

                            double newwallet = wallet + amount;

                            //Cập nhật lại ví
                            AccountController.updateWallet(uid_rut, newwallet, currentDate, username);

                            //Thêm vào History Pay wallet
                            HistoryPayWalletController.Insert(uid_rut, username, 0, amount, "Admin Hủy lệnh Rút tiền", newwallet, 2, 6, currentDate, username);

                            //Thêm vào lệnh rút tiền
                            WithdrawController.UpdateStatus(id, 3, currentDate, username);

                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(user_rut.ID, user_rut.Username, 0, "Admin đã hủy lệnh rút tiền của bạn.",
3, currentDate, username, true);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "rut-tien/";
                                    PJUtils.PushNotiDesktop(user_rut.ID, "Admin đã hủy lệnh rút tiền của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new( user_rut.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Admin đã hủy lệnh rút tiền của bạn.", "");
                                    }
                                    catch { }
                                }
                            }


                        }
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                    else
                    {
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                }
            }
        }
    }
}
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
    public partial class add_withdraw : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            string urlName = Request.UrlReferrer.ToString();
            ltr.Text = "<a href=\"" + urlName + "\" class=\"btn\">Trở về</a>";


            if (Request.QueryString["u"] != null)
            {
                string username = Request.QueryString["u"];
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    rp_username.Text = username;
                    ViewState["Username"] = rp_username.Text;
                    rp_textarea.Text = username + " đã rút tiền từ tài khoản.";
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(pAmount.Value) > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                string username_bitru = ViewState["Username"].ToString();
                var acc = AccountController.GetByUsername(username_bitru);
                string BackLink = "/manager/Withdraw-List.aspx";
                if (acc != null)
                {
                    int UID = acc.ID;
                    double wallet = Convert.ToDouble(acc.Wallet);
                    double amount = Convert.ToDouble(pAmount.Value);
                    //int status = ddlStatus.SelectedValue.ToInt();
                    DateTime currentDate = DateTime.Now;
                    if (wallet >= amount)
                    {
                        //Cho rút
                        double leftwallet = wallet - amount;

                        //Cập nhật lại ví
                        AccountController.updateWallet(UID, leftwallet, currentDate, username_current);

                        //Thêm vào History Pay wallet
                        HistoryPayWalletController.Insert(UID, username_bitru, 0, amount, "Rút tiền", leftwallet, 1, 5, currentDate, username_current);

                        //Thêm vào lệnh rút tiền
                        string kq = WithdrawController.Insert(UID, username_bitru, amount, 2, rp_textarea.Text, currentDate, username_current);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo lệnh rút tiền thành công", "s", true, BackLink, Page);
                        var c = ConfigurationController.GetByTop1();
                        var setNoti = SendNotiEmailController.GetByID(3);
                        if (setNoti != null)
                        {
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
                                    html.Append("<td>Tên KH: " + AccountInfoController.GetByUserID(acc.ID).FirstName + " " + AccountInfoController.GetByUserID(acc.ID).LastName + " </td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>SĐT:  " + AccountInfoController.GetByUserID(acc.ID).Phone + "</td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>Username: " + acc.Username + "</td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>Số tiền rút ví: " + string.Format("{0:N0}", amount) + " VNĐ</td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>Số dư hiện tại: " + string.Format("{0:N0}", leftwallet) + " VNĐ</td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>ID rút ví: " + kq.ToInt() + "</td>");
                                    html.Append("</tr>");
                                    html.Append("<tr>");
                                    html.Append("<td>Thời gian: " + currentDate + "</td>");
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


                                    PJUtils.SendMailGmail_new( acc.Email,
                                        "Thông báo tại NHẬP HÀNG 360.", html.ToString(), "");
                                }
                                catch { }
                            }
                        }

                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Số tiền không đủ để tạo lệnh rút", "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Không tồn tại tài khoản trên", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Chưa nhập số tiền cần rút", "e", true, Page);
            }

        }
    }
}
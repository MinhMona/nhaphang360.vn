using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Controllers;

namespace NHST
{
    public partial class ClientMasterLogined : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Redirect("/website-under-contructing.aspx");
                checkuser();                
                LoadConfiguration();
            }
        }
        public void LoadConfiguration()
        {
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                ltrEmailSupportHotlineCurrency.Text += "<a href=\"javascript:;\" class=\"contact-link\">Tỷ giá: 1<i class=\"fa fa-yen\" style=\"margin-right:0\"></i> = " + string.Format("{0:N0}", Convert.ToDouble(c.Currency)) + "</a>";
                ltrEmailSupportHotlineCurrency.Text += "<a href=\"mailto:" + c.EmailSupport + "\" class=\"contact-link\"><i class=\"fa fa-envelope\"></i>" + c.EmailSupport + "</a>";
                //ltrEmailSupportHotlineCurrency.Text += "<a href=\"tel:" + c.Hotline + "\" class=\"contact-link\"><i class=\"fa fa-phone\"></i>" + c.Hotline + "</a>";


                ltr_Social.Text += "<li><a href=\"" + c.Facebook + "\"><i class=\"fa fa-facebook\"></i></a></li>";
                ltr_Social.Text += "<li><a href=\"" + c.Twitter + "\"><i class=\"fa fa-twitter\"></i></a></li>";
                ltr_Social.Text += "<li><a href=\"" + c.Instagram + "\"><i class=\"fa fa-instagram\"></i></a></li>";
                ltr_Social.Text += "<li><a href=\"" + c.Skype + "\"><i class=\"fa fa-skype\"></i></a></li>";

                ltr_Social1.Text += "<li class=\"social\" target=\"_blank\"><a href=\"" + c.Facebook + "\"><i class=\"fa fa-facebook\"></i></a></li>";
                ltr_Social1.Text += "<li class=\"social\" target=\"_blank\"><a href=\"" + c.Twitter + "\"><i class=\"fa fa-twitter\"></i></a></li>";
                ltr_Social1.Text += "<li class=\"social\" target=\"_blank\"><a href=\"" + c.Instagram + "\"><i class=\"fa fa-instagram\"></i></a></li>";
                ltr_Social1.Text += "<li class=\"social\" target=\"_blank\"><a href=\"" + c.Skype + "\"><i class=\"fa fa-skype\"></i></a></li>";

                ltr_Hotline.Text += "<p><a href=\"tel:" + c.Hotline + "\">" + c.Hotline + "</a></p>";
                ltr_Timework.Text += "<p><a href=\"javascript:;\">" + c.TimeWork + "</a></p>";

                ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Hotline: <br/>" + c.Hotline + "</a></li>";
                ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Email: <br/>" + c.EmailSupport + "</a></li>";
                ltrContact.Text += "<li class=\"width100\"><a href=\"javascript:;\">Địa chỉ: <br/>" + c.Address + "</a></li>";

                string infocontent = c.InfoContent;
                if (Session["infoclose"] == null)
                {
                    if (!string.IsNullOrEmpty(infocontent))
                    {
                        ltr_infor.Text += "<div class=\"sec webinfo\">";
                        ltr_infor.Text += "<div class=\"all width-100-percent\">";
                        ltr_infor.Text += "<div class=\"main\">";
                        ltr_infor.Text += "<div class=\"textcontent\">";
                        ltr_infor.Text += "<span>" + infocontent + "</span>";
                        ltr_infor.Text += "<a href=\"javascript:;\" onclick=\"closewebinfo()\" class=\"icon-close-info\"><i class=\"fa fa-times\"></i></a>";
                        ltr_infor.Text += "</div></div></div></div>";
                    }
                }
            }
        }        
        public void checkuser()
        {
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                string fullname = "Guest";
                if (obj_user != null)
                {
                    fullname = AccountInfoController.GetByUserID(obj_user.ID).FirstName + " " + AccountInfoController.GetByUserID(obj_user.ID).LastName;
                }
                ltr_login.Text = "";                
                ltr_user.Text += "";

                var notis = NotificationController.GetByReceivedID(obj_user.ID);
                var cart = OrderTempController.GetAllByUID(obj_user.ID);
                int cartcount = 0;
                if (cart != null)
                {
                    //ltr_noti.Text += "<a href=\"/gio-hang\" class=\"flex-btn  cart-user\"><span class=\"badge\">" + cart.Count + "</span> <i class=\"fa fa-shopping-cart\"></i></a>";
                    cartcount = cart.Count;
                }
                else
                {
                    //ltr_noti.Text += "<a href=\"/gio-hang\" class=\"flex-btn  cart-user\"><span class=\"badge\">0</span> <i class=\"fa fa-shopping-cart\"></i></a>";
                }
                ltr_login.Text += "   <li class=\"dropdown-custom noti-user\">";
                //ltr_login.Text += "       <a href=\"/thong-bao-cua-ban\"><i class=\"fa fa-bell\"></i><span class=\"lbl\">Thông báo</span> &nbsp;"
                //                                + "<span class=\"notifications m-color\">(" + notis.Count + ")</span></a>";
                ltr_login.Text += "       <a href=\"javascript:;\" onclick=\"setfullisread()\"><i class=\"fa fa-bell\"></i><span class=\"lbl\">Thông báo</span> &nbsp;"
                                                + "<span class=\"notifications m-color\">(" + notis.Count + ")</span></a>";
                if (notis.Count > 0)
                {
                    ltr_login.Text += "  <div class=\"sub-menu-wrap\">";
                    ltr_login.Text += "      <ul class=\"sub-menu noti-sub-withscroll\">";
                    foreach (var item in notis)
                    {
                        ltr_login.Text += "          <li><a href=\"javascript:;\" onclick=\"updatestatusnoti('" + item.ID + "','" + obj_user.Username + "','" + item.OrderID + "')\">" + item.Message + "</a></li>";
                    }
                    ltr_login.Text += "      </ul>";
                    ltr_login.Text += "  </div>";
                }
                else
                {

                }

                ltr_login.Text += "   </li>";
                ltr_login.Text += "   <li><a href=\"/gio-hang\"><i class=\"fa fa-shopping-cart\"></i>&nbsp;<span class=\"lbl\">Giỏ hàng </span>"
                                                + "<span class=\"products-in-cart m-color\">(" + cartcount + ")</span></a>";
                ltr_login.Text += "   </li>";

                ltr_login.Text += "   <li><a href=\"/danh-sach-don-hang\" class=\"m-color\">Quản lý đơn hàng</a></li>";
                ltr_login.Text += "   <li class=\"activity-thumb dropdown\" style=\"float:none;\"><a href=\"/thong-tin-nguoi-dung\">" + username + "<i class=\"arrow fa fa-caret-down m-color\"></i></a>";
                ltr_login.Text += "     <div class=\"sub-menu-wrap\">";
                ltr_login.Text += "      <ul class=\"sub-menu\">";
                ltr_login.Text += "          <li><a href=\"/thong-tin-nguoi-dung\">Tài khoản</a></li>";
                ltr_login.Text += "          <li><a href=\"/gio-hang\">Giỏ hàng</a></li>";
                ltr_login.Text += "          <li><a href=\"/danh-sach-don-hang\">Danh sách đơn hàng</a></li>";
                ltr_login.Text += "          <li><a href=\"/bang-tich-luy-diem\">Điểm tích lũy</a></li>";
                ltr_login.Text += "          <li><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch</a></li>";
                ltr_login.Text += "          <li><a href=\"/nap-tien\">Nạp tiền</a></li>";
                ltr_login.Text += "          <li><a href=\"/rut-tien\">Rút tiền</a></li>";
                ltr_login.Text += "          <li><a href=\"/Logout\">Đăng xuất</a></li>";
                ltr_login.Text += "      </ul>";
                ltr_login.Text += "     </div>";
                ltr_login.Text += "  </li>";
                ltr_login.Text += "   <li><a href=\"javascript:;\" class=\"m-color\">Số dư: " + string.Format("{0:N0}", obj_user.Wallet).Replace(",", ".") + " vnđ</a></li>";                
            }
            else
            {
                ltr_login.Text = "<li id=\"loginlink\"><a href=\"/dang-nhap\">Đăng nhập</a> | <a href=\"/dang-ky\">Đăng ký</a></li>";
            }
        }
    }
}
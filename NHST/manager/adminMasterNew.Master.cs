using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class adminMasterNew : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username_current);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 1)
                        {
                            if (!string.IsNullOrEmpty(obj_user.LoginStatus))
                            {
                                if (Session["StateLogin"].ToString() == obj_user.LoginStatus)
                                {

                                    hdfMainLoginID.Value = obj_user.ID.ToString();
                                    hdfMainLoginStatus.Value = obj_user.LoginStatus;
                                }
                                else
                                {
                                    Session.Abandon();
                                    Response.Redirect("/");
                                }
                            }
                            else
                            {

                                hdfMainLoginID.Value = obj_user.ID.ToString();
                                hdfMainLoginStatus.Value = "1";
                            }
                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }


                    }
                    LoadNotification();
                    LoadMenu();
                }
            }
        }

        public void LoadMenu()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int Role = Convert.ToInt32(obj_user.RoleID);
                if (Role != 1)
                {
                    StringBuilder html = new StringBuilder();
                    if (Role == 0)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");
                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Tổng quan</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan active\" href=\"/manager/home\"><i class=\"material-icons\">dashboard</i><span class=\"menu-title\">Trang điều khiển</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">settings</i><span class=\"menu-title\">Cài đặt</span></a><div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/configuration.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Hệ thống</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Tariff-TQVN.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí TQ - VN</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Tariff-Buypro\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí dịch vụ mua hàng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/pricechangeList.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí thanh toán hộ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/User-Level.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí người dùng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/banklist.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách ngân hàng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/thiet-lap-thong-bao.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thông báo</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Noti-app-list.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thông báo app</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nhân viên - khách hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">assignment_ind</i><span class=\"menu-title\">Nhân viên</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/stafflist\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách nhân viên</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/admin-staff-income\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý hoa hồng</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/userlist\"><i class=\"material-icons\">people</i><span class=\"menu-title\">Danh sách khách hàng</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Đơn hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("</li>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Đơn hàng</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/orderlist\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH mua hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/orderlist?ot=3\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH mua hộ khác</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/transportation-list\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH vận chuyển hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-thanh-toan-ho\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thanh toán hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-yeu-cau-giao-hang\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách yêu cầu giao hàng</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/orderlist\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">ĐH mua hộ</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ComplainList\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Xử lý khiếu nại</span></a>");
                        html.Append("</li>");


                        //html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Ký gửi</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Ký gửi</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách ký gửi</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/request-outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Yêu cầu xuất kho</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thống kê cước ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho ký gửi đã yêu cầu</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho kiện chưa yêu cầu</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/add-transportation-new\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thêm mới đơn ký gửi</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        //html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/TQWareHouse-DHH\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm hàng kho TQ</span></a>");
                        html.Append("</li>");
                       
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/VNWarehouse-DHH\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm hàng kho VN</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/OutStock.aspx\"><i class=\"material-icons\">rv_hookup</i><span class=\"menu-title\">Xuất kho</span></a></li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ConvertLink\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Chuyển đổi link</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Quản lý kiện hàng</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");                       
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Warehouse-Management\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý bao hàng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Order-Transaction-Code\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý mã vận đơn</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/kien-that-lac\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiện thất lạc</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/kien-troi-noi\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiện trôi nổi</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nghiệp vụ kế toán</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">attach_money</i><span class=\"menu-title\">Quản lý nạp - rút tiền</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/add-wallet-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Nạp tiền cá nhân</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/HistorySendWallet\" data-i18n=\"\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu nạp tiền</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Withdraw-List\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu rút tiền</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/RequestRechargeCYN\" data-i18n=\"\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu nạp tiền tệ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/refund-cyn\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu rút tiền tệ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/accountant-outstock-payment\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thanh toán xuất kho</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">assessment</i><span class=\"menu-title\">Thống kê</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-Outstock.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>In phiếu mua hộ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-OrderV.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Doanh thu</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-recharge.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền nạp</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-user-wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Số dư</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-User-Use-Wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Giao dịch</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-money.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền mua thật</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng mua kho TQ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-buypro.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận mua hàng hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-loinhuan-thanhtoanho.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận thanh toán hộ</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nội dung trang ngoài</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">create</i><span class=\"menu-title\">Bài viết</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Page-Type-List\"><i class=\"material-icons\">radio_button_unchecked</i><span>Chuyên mục bài viết</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/PageList\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách bài viết</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/Home-Config\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Nội dung trang chủ</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 2)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");
                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Tổng quan và cấu hình</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan active\" href=\"/manager/home\"><i class=\"material-icons\">dashboard</i><span class=\"menu-title\">Trang điều khiển</span></a>");
                        html.Append("</li>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">settings</i><span class=\"menu-title\">Cài đặt</span></a><div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/configuration.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Hệ thống</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Tariff-TQVN.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí TQ - VN</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Tariff-Buypro\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí dịch vụ mua hàng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/pricechangeList.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí thanh toán hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/User-Level.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Phí người dùng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/thiet-lap-thong-bao.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thông báo</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        //html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nhân viên - khách hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">assignment_ind</i><span class=\"menu-title\">Nhân viên</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/stafflist\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách nhân viên</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/admin-staff-income\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý hoa hồng</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/userlist\"><i class=\"material-icons\">people</i><span class=\"menu-title\">Danh sách khách hàng</span></a>");
                        html.Append("</li>");


                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Đơn hàng</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/orderlist\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH mua hộ</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/orderlist?ot=3\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH mua hộ khác</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/transportation-list\"><i class=\"material-icons\">radio_button_unchecked</i><span>ĐH vận chuyển hộ</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-thanh-toan-ho\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thanh toán hộ</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-yeu-cau-giao-hang\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách yêu cầu giao hàng</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Đơn hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/orderlist\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">ĐH mua hộ</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ComplainList\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Xử lý khiếu nại</span></a>");
                        html.Append("</li>");

                        //html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Ký gửi</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Ký gửi</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách ký gửi</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/request-outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Yêu cầu xuất kho</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thống kê cước ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho ký gửi đã yêu cầu</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho kiện chưa yêu cầu</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-phien-xuat-kho-ky-gui\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách phiên xuất kho ký gửi</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        //html.Append("</li>");


                        //html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nghiệp vụ kho</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/TQWareHouse-DHH\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm hàng kho TQ</span></a>");
                        // html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm kho TQ</span></a>");
                        // html.Append("<div class=\"collapsible-body\">");
                        // html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        // html.Append("<li><a class=\"collapsible-body\" href=\"/manager/tao-don-ky-gui\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tạo đơn ký gửi hộ</span></a></li>");
                        // html.Append("<li><a class=\"collapsible-body\" href=\"/manager/TQWareHouse-DHH\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng kho TQ </span></a></li>");
                        //// html.Append("<li><a class=\"collapsible-body\" href=\"/manager/TQWareHouse\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng TQ - Ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href =\"/manager/import_smallpackage\"><i class=\"material-icons\">store</i><span>Import KhoTQ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href =\"/manager/add-package-customer\"><i class=\"material-icons\">store</i><span>Gán kiện cho khách</span></a></li>");
                        // html.Append("</ul>");
                        // html.Append("</div>");
                        html.Append("</li>");


                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/VNWarehouse-DHH\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm hàng kho VN</span></a>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kiểm kho VN</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/VNWarehouse-DHH\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng kho VN</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/VNWarehouse\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng VN - Đơn ký gửi</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ConvertLink\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Chuyển đổi link</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/OutStock.aspx\"><i class=\"material-icons\">rv_hookup</i><span class=\"menu-title\">Xuất kho</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Quản lý kiện hàng</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Warehouse-Management\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý bao hàng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Order-Transaction-Code\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý mã vận đơn</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/kien-that-lac\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiện thất lạc</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/kien-troi-noi\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiện trôi nổi</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");


                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nghiệp vụ kế toán</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">attach_money</i><span class=\"menu-title\">Quản lý nạp - rút tiền</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/add-wallet-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Nạp tiền cá nhân</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/HistorySendWallet\" data-i18n=\"\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu nạp tiền</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Withdraw-List\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu rút tiền</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/RequestRechargeCYN\" data-i18n=\"\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu nạp tiền tệ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/refund-cyn\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lịch sử yêu cầu rút tiền tệ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/accountant-outstock-payment\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thanh toán xuất kho</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">assessment</i><span class=\"menu-title\">Thống kê</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-OrderV.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Doanh thu</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-recharge.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền nạp</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-user-wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Số dư</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-User-Use-Wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Giao dịch</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng mua kho TQ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-buypro.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận mua hàng hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-money.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền mua thật</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-loinhuan-thanhtoanho.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận thanh toán hộ</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Nội dung trang ngoài</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">create</i><span class=\"menu-title\">Bài viết</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Page-Type-List\"><i class=\"material-icons\">radio_button_unchecked</i><span>Chuyên mục bài viết</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/PageList\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách bài viết</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/Home-Config\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Nội dung trang chủ</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 3)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");
                        html.Append("</li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Đơn hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/orderlist\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">ĐH mua hộ</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ComplainList\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Xử lý khiếu nại</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ConvertLink\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Chuyển đổi link</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan\" href=\"/manager/staff-income\"><i class=\"material-icons\">attach_money</i><span class=\"menu-title\">Kiểm tra hoa hồng NV</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 4)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kho hàng</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/tao-don-ky-gui\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tạo đơn ký gửi hộ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/TQWareHouse-DHH\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng kho TQ </span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/TQWareHouse\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng TQ - Ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href =\"/manager/import_smallpackage\"><i class=\"material-icons\">store</i><span>Import KhoTQ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Warehouse-Management\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý bao hàng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Order-Transaction-Code\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý mã vận đơn</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/add-transportation-new\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thêm mới đơn ký gửi</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/kien-that-lac.aspx\"><i class=\"material-icons\">rv_hookup</i><span class=\"menu-title\">Kiện thất lạc</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/kien-troi-noi.aspx\"><i class=\"material-icons\">dns</i><span class=\"menu-title\">Kiện trôi nổi</span></a></li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 5)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");

                        //html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Ký gửi</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Ký gửi</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách ký gửi</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/request-outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Yêu cầu xuất kho</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thống kê cước ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho ký gửi đã yêu cầu</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho kiện chưa yêu cầu</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/add-transportation-new\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thêm mới đơn ký gửi</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        //html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">store</i><span class=\"menu-title\">Kho hàng</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/VNWarehouse-DHH\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng kho VN</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href =\"/manager/add-package-customer\"><i class=\"material-icons\">radio_button_unchecked</i><span>Gán kiện cho khách</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/VNWarehouse\"><i class=\"material-icons\">radio_button_unchecked</i><span>Kiểm hàng VN - Đơn ký gửi</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/OutStock\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho cho khách</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Warehouse-Management\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý bao hàng</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Order-Transaction-Code\"><i class=\"material-icons\">radio_button_unchecked</i><span>Quản lý mã vận đơn</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/kien-that-lac.aspx\"><i class=\"material-icons\">rv_hookup</i><span class=\"menu-title\">Kiện thất lạc</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/kien-troi-noi.aspx\"><i class=\"material-icons\">dns</i><span class=\"menu-title\">Kiện trôi nổi</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 6)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/sale-userlist\"><i class=\"material-icons\">people</i><span class=\"menu-title\">Danh sách khách hàng</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/Saler-AddUser.aspx\"><i class=\"material-icons\">person_add</i><span class=\"menu-title\">Thêm khách hàng</span></a></li>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"/manager/tao-don-hang-khac\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Đơn hàng mua hộ TMDT khác</span></a>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Đơn hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/orderlist\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">ĐH mua hộ</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ConvertLink\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Chuyển đổi link</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan\" href=\"/manager/staff-income\"><i class=\"material-icons\">attach_money</i><span class=\"menu-title\">Kiểm tra hoa hồng NV</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 7)
                    {
                        html.Append("<ul class=\"sidenav sidenav-collapsible leftside-navigation collapsible sidenav-fixed menu-shadow\" id=\"slide-out\" data-menu=\"menu-navigation\" data-collapsible=\"menu-accordion\">");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/Withdraw-List\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Lịch sử rút tiền</span></a></li>");
                        //html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/refund-cyn\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Lịch sử rút tiền tệ</span></a></li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/historysendwalletaccountant\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Lịch sử nạp tiền</span></a></li>");
                        //html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/RequestRechargeCYN\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Lịch sử nạp tiền tệ</span></a></li>");
                        //html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/RechargeCYN\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Lịch sử nạp tiền tệ</span></a></li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/Accountant-User-List\"><i class=\"material-icons\">people</i><span class=\"menu-title\">Danh sách người dùng</span></a></li>");

                        html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Đơn hàng</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/orderlist\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">ĐH mua hộ</span></a>");
                        html.Append("</li>");

                        //html.Append("<li class=\"navigation-header\"><a class=\"navigation-header-text\">Ký gửi</a><i class=\"navigation-header-icon material-icons\">more_horiz</i>");
                        //html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"javascript:;\"><i class=\"material-icons\">library_books</i><span class=\"menu-title\">Ký gửi</span></a>");
                        //html.Append("<div class=\"collapsible-body\">");
                        //html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách ký gửi</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/request-outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Yêu cầu xuất kho</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thống kê cước ký gửi</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho ký gửi đã yêu cầu</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/outstock-vch-user\"><i class=\"material-icons\">radio_button_unchecked</i><span>Xuất kho kiện chưa yêu cầu</span></a></li>");
                        ////html.Append("<li><a class=\"collapsible-body\" href=\"/manager/danh-sach-phien-xuat-kho-ky-gui\"><i class=\"material-icons\">radio_button_unchecked</i><span>Danh sách phiên xuất kho ký gửi</span></a></li>");
                        //html.Append("</ul>");
                        //html.Append("</div>");
                        //html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"collapsible-header waves-effect waves-cyan \" href=\"#\"><i class=\"material-icons\">assessment</i><span class=\"menu-title\">Thống kê</span></a>");
                        html.Append("<div class=\"collapsible-body\">");
                        html.Append("<ul class=\"collapsible collapsible-sub\" data-collapsible=\"accordion\">");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-OrderV.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Doanh thu</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-recharge.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền nạp</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-user-wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Số dư</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-User-Use-Wallet.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Giao dịch</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-money.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Tiền mua thật</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-order.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Đơn hàng mua kho TQ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/Report-buypro.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận mua hàng hộ</span></a></li>");
                        //html.Append("<li><a class=\"collapsible-body\" href=\"/manager/report-loinhuan-thanhtoanho.aspx\"><i class=\"material-icons\">radio_button_unchecked</i><span>Lợi nhuận thanh toán hộ</span></a></li>");
                        html.Append("<li><a class=\"collapsible-body\" href=\"/manager/accountant-outstock-payment\"><i class=\"material-icons\">radio_button_unchecked</i><span>Thanh toán xuất kho</span></a></li>");
                        html.Append("</ul>");
                        html.Append("</div>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/ConvertLink\"><i class=\"material-icons\">report</i><span class=\"menu-title\">Chuyển đổi link</span></a>");
                        html.Append("</li>");

                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/manager/staff-income\"><i class=\"material-icons\">attach_money</i><span class=\"menu-title\">Kiểm tra hoa hồng NV</span></a>");
                        html.Append("</li>");
                        html.Append("<li class=\"bold\"><a class=\"waves-effect waves-cyan \" href=\"/dang-xuat\"><i class=\"material-icons\">reply_all</i><span class=\"menu-title\">Đăng xuất</span></a></li>");
                        html.Append("</ul>");
                    }
                    ltrMenu.Text = html.ToString();
                }
            }
        }

        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {

                    string uImage = "";
                    var ui = AccountInfoController.GetByUserID(obj_user.ID);
                    if (ui != null)
                    {
                        if (!string.IsNullOrEmpty(ui.IMGUser))
                        {
                            if (File.Exists(Server.MapPath("" + ui.IMGUser + "")))
                            {
                                uImage = ui.IMGUser;
                            }
                            else
                            {
                                uImage = "/NO-IMAGE.jpg";
                            }
                        }
                        else
                        {
                            uImage = "/NO-IMAGE.jpg";
                        }
                    }

                    var noti = NotificationsController.GetAll(obj_user.ID);
                    //ltrinfo.Text += "<li class=\"hide-on-med-and-down\"><span class=\"flag-icon flag-icon-cn\"></span>1 ¥ = <span class=\"flag-icon flag-icon-vn\"></span>" + string.Format("{0:N0}", config.Currency) + " Đồng</li>";
                    ltrinfo.Text += "<li class=\"hide-on-med-and-down\"><a class=\"waves-effect waves-block waves-light toggle-fullscreen\" href=\"javascript:void(0);\"><i class=\"material-icons\">settings_overscan</i></a></li>";
                    //ltrinfo.Text += "<li class=\"hide-on-large-only\"><a class=\"waves-effect waves-block waves-light search-button\" href=\"javascript:void(0);\"><i class=\"material-icons\">search</i></a></li>";
                    ltrinfo.Text += "<li><a class=\"waves-effect waves-block waves-light notification-button\" href=\"javascript:void(0);\" data-target=\"notifications-dropdown\"><i class=\"material-icons\">notifications_none<small class=\"notification-badge orange accent-3\">" + noti.Where(x => x.Status == 1).ToList().Count + "</small></i></a></li>";
                    ltrinfo.Text += "<li><a class=\"waves-effect waves-block waves-light profile-button\" href=\"javascript:void(0);\" data-target=\"profile-dropdown\"><span class=\"avatar-status avatar-online\"><img src=\"" + uImage + "\" alt=\"avatar\"><i></i></span></a></li>";

                    List<tbl_Notification> all = new List<tbl_Notification>();

                    StringBuilder html = new StringBuilder();
                    var dh = NotificationsController.GetAllByNotifType(1, obj_user.ID);
                    var tc = NotificationsController.GetAllByNotifType(2, obj_user.ID);
                    var kn = NotificationsController.GetAllByNotifType(5, obj_user.ID);
                   
                    all = NotificationsController.GetAll(obj_user.ID).Where(x => x.Status == 1).OrderByDescending(x => x.ID).Take(10).ToList();

                    html.Append("<li>");
                    html.Append("<h6>Thông báo<span class=\"new badge\">" + noti.Where(x => x.Status == 1).ToList().Count + "</span></h6>");
                    html.Append("</li>");
                    html.Append("<li class=\"divider\"></li>");
                    html.Append("<li style=\"height:500px;overflow-y:auto\">");
                    html.Append("<div class=\"tab-wrap green lighten-2\"> ");
                    html.Append("<ul class=\"tabs\">");
                    html.Append("<li class=\"tab col m3\"><a  class=\"active\" href=\"#noti-all\">Tất cả</a></li>");
                    html.Append("<li class=\"tab col m3\"><a class=\"\" href=\"#noti-tc\">Tài chính</a></li>  ");
                    html.Append("<li class=\"tab col m3\"><a class=\"\" href=\"#noti-order\">Đơn hàng</a></li>");
                    html.Append("<li class=\"tab col m3\"><a class=\"\" href=\"#noti-report\">Khiếu nại</a></li>    ");
                    html.Append("</ul>");
                    html.Append("<ul id=\"noti-all\">");
                    if (all.Count > 0)
                    {
                        foreach (var item in all)
                        {
                            html.Append("<li>");
                            if (item.NotifType == 1)
                            {
                                html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">");
                            }
                            else if (item.NotifType == 2)
                            {
                                html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\">");
                            }
                            else if (item.NotifType == 5)
                            {
                                html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">");
                            }
                            else
                            {
                                html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/admin-noti')\">");
                            }

                            html.Append("<div class=\"icon-noti\">");
                            html.Append("<span class=\"material-icons icon-bg-circle cyan small\">add_shopping_cart</span>");
                            html.Append("</div>");
                            html.Append("<div class=\"noti-content\">");
                            html.Append("<p class=\"content\">" + item.Message + "</p>");
                            html.Append("<time class=\"media-meta\" datetime=\"2015-06-12T20:50:48+08:00\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>");
                            html.Append("</div>");
                            html.Append("</a>");
                            html.Append("</li>");
                        }
                    }
                    html.Append("</ul>");

                    html.Append("<ul id=\"noti-tc\"> ");
                    if (tc.Count > 0)
                    {
                        foreach (var item in tc)
                        {
                            html.Append("<li>");
                            html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/HistorySendWallet')\"> ");
                            html.Append("<div class=\"icon-noti\">");
                            html.Append("<span class=\"material-icons icon-bg-circle cyan small\">add_shopping_cart</span>  ");
                            html.Append("</div>");
                            html.Append("<div class=\"noti-content\">");
                            html.Append("<p class=\"content\">" + item.Message + "</p>");
                            html.Append("<time class=\"media-meta\" datetime=\"2015-06-12T20:50:48+08:00\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>");
                            html.Append("</div>");
                            html.Append("</a>");
                            html.Append("</li>");
                        }
                    }

                    html.Append("</ul>");

                    html.Append("<ul id=\"noti-order\">");
                    if (dh.Count > 0)
                    {
                        foreach (var item in dh)
                        {
                            html.Append("<li>");
                            html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/OrderDetail.aspx?id=" + item.OrderID + "')\">");
                            html.Append("<div class=\"icon-noti\">");
                            html.Append("<span class=\"material-icons icon-bg-circle cyan small\">add_shopping_cart</span> ");
                            html.Append("</div>");
                            html.Append("<div class=\"noti-content\">");
                            html.Append("<p class=\"content\">" + item.Message + "</p>");
                            html.Append("<time class=\"media-meta\" datetime=\"2015-06-12T20:50:48+08:00\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>");
                            html.Append("</div>");
                            html.Append("</a>");
                            html.Append("</li>");
                        }
                    }

                    html.Append("</ul>");

                    html.Append("<ul id=\"noti-report\">");
                    if (kn.Count > 0)
                    {
                        foreach (var item in kn)
                        {
                            html.Append("<li>");
                            html.Append("<a class=\"grey-text text-darken-2\" onclick=\"checkisRead('" + item.ID + "','/manager/ComplainList')\">");
                            html.Append("<div class=\"icon-noti\">");
                            html.Append("<span class=\"material-icons icon-bg-circle cyan small\">add_shopping_cart</span>   ");
                            html.Append("</div>");
                            html.Append("<div class=\"noti-content\">");
                            html.Append("<p class=\"content\">" + item.Message + "</p>");
                            html.Append("<time class=\"media-meta\" datetime=\"2015-06-12T20:50:48+08:00\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</time>");
                            html.Append("</div>");
                            html.Append("</a>");
                            html.Append("</li>");
                        }
                    }

                    html.Append("</ul>");
                    html.Append("</div>");
                    html.Append("</li>");

                    html.Append("<li><a href=\"/manager/admin-noti\" class=\"teal-text text-darken-4 center-align padding-2 viewall\">Xem tất cả</a></li>");
                    ltrNoti.Text = html.ToString();

                }
            }
        }

    }
}
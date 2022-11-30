using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class adminMaster : System.Web.UI.MasterPage
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
                            //lUName.Text = obj_user.Username;

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
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li class=\"active\"><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i home\"></span>Cài đặt <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/configuration.aspx\">Cấu hình hệ thống</a></li>");
                        html.Append("           <li><a href=\"/manager/Tariff-TQVN.aspx\">TL phí TQ-VN</a></li>");
                        html.Append("           <li><a href=\"/manager/Tariff-Buypro\">TL phí dịch vụ mua hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/User-Level.aspx\">TL người dùng</a></li>");
                        html.Append("           <li><a href=\"/manager/thiet-lap-thong-bao.aspx\">Thiết lập thông báo</a></li>");
                        html.Append("           <li><a href=\"/manager/Home-Config.aspx\">Nội dung trang chủ</a></li>");
                        //html.Append("           <li><a href=\"/manager/linkmarquee-list.aspx\">Quản lý chữ chạy ngang</a></li>");
                        //html.Append("           <li><a href=\"#\">TL vận chuyển</a></li>");
                        //html.Append("           <li><a href=\"/manager/pricechangeList.aspx\">TL Phí thanh toán hộ</a></li>");
                        //html.Append("           <li><a href=\"#\">TL phí vận chuyển hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i user\"></span>Nhân viên <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/stafflist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/admin-staff-income\">Kiểm tra hoa hồng NV</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/userlist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/HistorySendWallet\">Lịch sử nạp</a></li>");
                        //html.Append("           <li><a href=\"/manager/RequestRechargeCYN\">Lịch sử nạp tệ</a></li>");
                        html.Append("           <li><a href=\"/manager/Withdraw-List\">Lịch sử rút</a></li>");
                        //html.Append("           <li><a href=\"/manager/refund-cyn\">Lịch sử rút tệ</a></li>");
                        //html.Append("           <li><a href=\"/manager/accountant-payment\">Thanh toán hóa đơn</a></li>");
                        html.Append("           <li><a href=\"/manager/accountant-outstock-payment\">Thanh toán xuất kho</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        //html.Append("   <li><a href=\"/manager/orderlist\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        //html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        //html.Append("   <li><a href=\"/manager/danh-sach-thanh-toan-ho\"><span class=\"nav-i panelList\"></span>Thanh toán hộ</a></li>");
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i paper\"></span>Bài viết <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Page-Type-List\">Danh mục</a></li>");
                        html.Append("           <li><a href=\"/manager/PageList\">Danh sách trang</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i grid\"></span>Thống kê <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Report-Income.aspx\">TK doanh thu</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-Orders.aspx\">TK đơn hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-recharge.aspx\">TK tiền nạp</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-user-wallet.aspx\">TK số dư</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-order.aspx\">TK đơn hàng mua, kho TQ, kho đích</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-User-Use-Wallet.aspx\">TK giao dịch</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-buypro.aspx\">TK lợi nhuận mua hàng hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/report-loinhuan-thanhtoanho.aspx\">TK lợi nhuận thanh toán hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 2)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i user\"></span>Nhân viên <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/stafflist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/admin-staff-income\">Kiểm tra hoa hồng NV</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/userlist\">Danh sách</a></li>");
                        html.Append("           <li><a href=\"/manager/HistorySendWallet\">Lịch sử nạp</a></li>");
                        //html.Append("           <li><a href=\"/manager/RequestRechargeCYN\">Lịch sử nạp tệ</a></li>");
                        html.Append("           <li><a href=\"/manager/Withdraw-List\">Lịch sử rút</a></li>");
                        //html.Append("           <li><a href=\"/manager/refund-cyn\">Lịch sử rút tệ</a></li>");
                        //html.Append("           <li><a href=\"/manager/accountant-payment\">Thanh toán hóa đơn</a></li>");
                        html.Append("           <li><a href=\"/manager/accountant-outstock-payment\">Thanh toán xuất kho</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        //html.Append("           <li><a href=\"/manager/transportation-list\">Đơn hàng VC hộ</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        //html.Append("   <li><a href=\"/manager/danh-sach-thanh-toan-ho\"><span class=\"nav-i panelList\"></span>Thanh toán hộ</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i paper\"></span>Bài viết <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Page-Type-List\">Danh mục</a></li>");
                        html.Append("           <li><a href=\"/manager/PageList\">Danh sách trang</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/ComplainList.aspx\"><span class=\"nav-i cube\"></span>Khiếu nại</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i grid\"></span>Thống kê <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Report-Income.aspx\">TK doanh thu</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-Orders.aspx\">TK đơn hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-recharge.aspx\">TK tiền nạp</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-user-wallet.aspx\">TK số dư</a></li>");
                        //html.Append("           <li><a href=\"/manager/Report-order.aspx\">TK đơn hàng mua, kho TQ, kho đích</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-User-Use-Wallet.aspx\">TK giao dịch</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 3)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        //html.Append("   <li><a href=\"/manager/orderlist\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/staff-income\"><span class=\"nav-i panelList\"></span>Kiểm tra hoa hồng NV</a></li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 4)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/TQWareHouse\">Kiểm hàng TQ</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 5)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i cube\"></span>Nghiệp vụ kho <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/VNWarehouse\">Kiểm hàng VN</a></li>");
                        html.Append("           <li><a href=\"/manager/OutStock\">Xuất kho cho khách</a></li>");
                        html.Append("           <li><a href=\"/manager/Warehouse-Management\">Quản lý bao hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Order-Transaction-Code\">Quản lý mã vận đơn</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-that-lac.aspx\"><span class=\"nav-i cube\"></span>Kiện thất lạc</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/kien-troi-noi.aspx\"><span class=\"nav-i cube\"></span>Kiện trôi nổi</a>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 6)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        //html.Append("   <li><a href=\"/manager/orderlist\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"/manager/sale-userlist\"><span class=\"nav-i panelList\"></span>Danh sách khách hàng</a></li>");
                        html.Append("   <li><a href=\"/manager/Saler-AddUser.aspx\"><span class=\"nav-i panelList\"></span>Thêm tài khoản</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        //html.Append("           <li><a href=\"/manager/tao-don-hang-khac\">Tạo đơn hàng TMĐT khác</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/manager/staff-income\"><span class=\"nav-i panelList\"></span>Kiểm tra hoa hồng NV</a></li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
                        html.Append("</ul>");
                    }
                    else if (Role == 7)
                    {
                        html.Append("<ul class=\"nav-ul\">");
                        html.Append("   <li><a href=\"/manager/Withdraw-List\"><span class=\"nav-i panelList\"></span>Lịch sử rút tiền</a></li>");
                        html.Append("   <li><a href=\"/manager/refund-cyn\"><span class=\"nav-i panelList\"></span>Lịch sử rút tiền tệ</a></li>");
                        html.Append("   <li><a href=\"/manager/historysendwalletaccountant\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền</a></li>");
                        html.Append("   <li><a href=\"/manager/RequestRechargeCYN\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền tệ</a></li>");
                        html.Append("   <li><a href=\"/manager/RechargeCYN\"><span class=\"nav-i panelList\"></span>Lịch sử nạp tiền tệ</a></li>");
                        html.Append("   <li><a href=\"/manager/Accountant-User-List\"><span class=\"nav-i panelList\"></span>Danh sách người dùng</a></li>");
                        //html.Append("   <li><a href=\"/manager/OrderList\"><span class=\"nav-i panelList\"></span>Đơn hàng</a></li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i panelList\"></span>Đơn hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/orderlist?ot=1\">Đơn hàng mua hộ</a></li>");
                        //html.Append("           <li><a href=\"/manager/orderlist?ot=3\">Đơn hàng mua hộ khác</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"#\" class=\"sub-toggle\"><span class=\"nav-i users\"></span>Khách Hàng <i class=\"caret\"></i></a>");
                        html.Append("       <ul class=\"side-sub\">");
                        html.Append("           <li><a href=\"/manager/Report-Income.aspx\">Báo cáo doanh thu</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-Orders.aspx\">Báo cáo đơn hàng</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-recharge.aspx\">Báo cáo tiền khách nạp</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-user-wallet.aspx\">Báo cáo khách hàng có số dư</a></li>");
                        //html.Append("           <li><a href=\"/manager/Report-order.aspx\">Báo cáo đơn hàng đã mua, kho TQ, kho đích</a></li>");
                        html.Append("           <li><a href=\"/manager/Report-User-Use-Wallet.aspx\">Báo cáo lịch sử giao dịch</a></li>");
                        html.Append("           <li><a href=\"/manager/accountant-outstock-payment\">Thanh toán xuất kho</a></li>");
                        html.Append("       </ul>");
                        html.Append("   </li>");
                        html.Append("   <li><a href=\"/dang-xuat\"><span class=\"fa fa-sign-out\"></span>Sign out</a></li>");
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
                    ltrinfo.Text += "";
                    ltrinfo.Text += "<a href=\"/manager/home\" class=\"right-it rate\"><i class=\"fa fa-home\"></i></a>";
                    ltrinfo.Text += "<a href=\"/manager/configuration\" class=\"right-it rate\"> ¥1 = " + string.Format("{0:N0}", config.Currency) + "</a>";
                    var noti = NotificationsController.GetAll(obj_user.ID);
                    ltrinfo.Text += "<a href=\"/manager/admin-noti\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Where(x => x.Status == 1).ToList().Count + "</span></a>";
                    ltrinfo.Text += "<span class=\"right-it username\"><a href=\"/thong-tin-nguoi-dung\" target=\"_blank\">" + obj_user.Username + "</a></span>";
                    ltrinfo.Text += "<a href=\"/trang-chu\" class=\"right-it rate\"><span class=\"right-it username\">Trang ngoài</span></a>";
                    ltrinfo.Text += "<a href=\"/dang-xuat\" class=\"right-it logout\"><i class=\"fa fa-sign-out\"></i>Sign out</a>";
                }
                //if (obj_user.RoleID == 0)
                //{

                //}
                int UID = obj_user.ID;
                var notiadmin = NotificationController.GetByReceivedID(UID);
                //ltrAmountNoti.Text = notiadmin.Count.ToString();
                //if (notiadmin.Count > 0)
                //{
                //    StringBuilder html = new StringBuilder();
                //    foreach (var item in notiadmin)
                //    {
                //        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','2')\">" + item.Message + "</a></li>");
                //    }
                //    ltrNoti.Text = html.ToString();
                //}
                //else
                //{
                //    ltrNoti.Text = "<li role=\"presentation\"><a href=\"javascript:;\">Không có thông báo mới</a></li>";
                //}
            }
        }
    }
}
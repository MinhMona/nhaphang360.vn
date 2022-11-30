using MB.Extensions;
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
    public partial class home_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);

            StringBuilder html = new StringBuilder();
            pnMobile.Visible = true;           

            html.Append("<div class=\"white-nooffset-cont homepage\">");
            html.Append("  <div class=\"home-heading\">");
            html.Append("   <a href=\"/tracking-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"hh-boxit\">");
            html.Append(" <span class=\"circle-icon\">");
            html.Append("    <img src =\"/App_Themes/App/images/icon-search.png\" alt=\"\"></span> Tracking");
            html.Append("   </a>");
            html.Append("   <a href=\"/don-hang-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"hh-boxit\">");
            html.Append("  <span class=\"circle-icon\">");
            html.Append("      <img src=\"/App_Themes/App/images/icon-store.png\" alt=\"\"></span> Mua hàng hộ");
            html.Append("  </a>");
            //html.Append("  <a href=\"/thanh-toan-ho-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"hh-boxit\">");
            //html.Append(" <span class=\"circle-icon\">");
            //html.Append("  <img src=\"/App_Themes/App/images/icon-usd.png\" alt=\"\"></span> Thanh toán hộ");
            //html.Append("  </a>");
            //html.Append("    <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"hh-boxit\">");
            //html.Append(" <span class=\"circle-icon\">");
            //html.Append("   <img src=\"/App_Themes/App/images/icon-truck.png\" alt=\"\"></span> Vận chuyển hộ");
            //html.Append(" </a>");
            html.Append("  </div>");
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                html.Append(" <div class=\"page-feature taichinh-feature\">");
                html.Append("  <p class=\"feature-caption\"><span class=\"circle-icon\">");
                html.Append("  <img src =\"/App_Themes/App/images/icon-budget.png\" alt=\"\"></span> Số dư</p>");
                html.Append("  <div class=\"taichinh-grayblock\">");
                html.Append("   <div class=\"tcitem\">");
                html.Append("  <p class=\"price\">" + string.Format("{0:N0}", Convert.ToDouble(u.Wallet)) + " VNĐ</p>");
                html.Append(" <a href=\"/lich-su-giao-dich-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"btn primary-btn pill-btn\">Lịch sử GD vnđ</a>");
                html.Append("   </div>");
                html.Append("  <div class=\"tcitem\">");
                html.Append("    <p class=\"price gray-txt\">¥" + string.Format("{0:N0}", Convert.ToDouble(u.WalletCYN)) + "</p>");
                html.Append("  <a href=\"/lich-su-giao-dich-tien-te-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"btn xanhreu-btn pill-btn\">Lịch sử GD tệ</a>");
                html.Append("  </div>");
                html.Append(" </div>");
                html.Append("  </div>");
            }

            var conf = ConfigurationController.GetByTop1();
            if (conf != null)
            {
                if (!string.IsNullOrEmpty(conf.NotiPopup))
                {
                    html.Append("  <div class=\"page-feature\" style=\"background:#ed1c24; color:#fff;\">");
                    html.Append("  <div class=\"article-cont center-txt\">");
                    html.Append("       <p>" + conf.NotiPopup + "</p>");
                    html.Append("  </div>");
                    html.Append(" </div>");
                }
               
                ltrZalo1.Text += "<div class=\"img-zalo\">";
                ltrZalo1.Text += "<a href=\"https://" + conf.Hotline + "\".com><img src=\"/App_Themes/NH360/images/phone.png\" alt=\"\"></a>";
                ltrZalo1.Text += "</div>";

                ltrZalo1.Text += "<div class=\"img-zalo\">";
                ltrZalo1.Text += "<a href=\"" + conf.ZaloLink + "\"><img src=\"/App_Themes/NH360/images/zalo.png\" alt=\"\"></a>";
                ltrZalo1.Text += "</div>";

                ltrZalo1.Text += "<div class=\"img-zalo\">";
                ltrZalo1.Text += "<a href=\"" + conf.Twitter + "\"><img src=\"/App_Themes/NH360/images/mess.png\" alt=\"\"></a>";
                ltrZalo1.Text += "</div>";
            }

            #region Mua hàng hộ

            html.Append(" <div class=\"page-feature home-collapse-feat\">");
            html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            html.Append("    <div class=\"heading\">");
            html.Append("    <p class=\"left-lb\">");
            html.Append("         <span class=\"circle-icon\">");
            html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            html.Append("     Mua hàng hộ");
            html.Append("  </p>");
            html.Append("   <p class=\"right-meta\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-chevron-up'></i>\" data-hide=\"Mở rộng <i class='fa fa-chevron-down'></i>\" href=\"#chitiettb\">Thu gọn<i class='fa fa-chevron-up'></i></a></p>");
            html.Append("   </div>");
            html.Append("  <div class=\"collapse-content opts-wrap\">");
            //html.Append("      <a class=\"opts-row\" href=\"/dat-hang-ngoai-he-thong-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("  <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("     <img src=\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Đặt hàng ngoài hệ thống</span>");
            //html.Append("  <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            html.Append("  <a class=\"opts-row\" href=\"/don-hang-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span> Đơn hàng</span>");
            html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/danh-sach-don-hang-khac-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("  <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("   <img src =\"/App_Themes/App/images/icon-homefeat4.png\" alt=\"\"></span> Đơn hàng thương mại điện tử khác</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("   </a>");
            html.Append("  </div>");
            html.Append("  </div>");
            html.Append("  </div>");

            #endregion

            //#region Vận chuyển ký gửi
            //html.Append(" <div class=\"page-feature home-collapse-feat\">");
            //html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            //html.Append("    <div class=\"heading\">");
            //html.Append("    <p class=\"left-lb\">");
            //html.Append("         <span class=\"circle-icon\">");
            //html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            //html.Append("     Vận chuyển ký gửi");
            //html.Append("  </p>");
            //html.Append("   <p class=\"right-meta\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-chevron-up'></i>\" data-hide=\"Mở rộng <i class='fa fa-chevron-down'></i>\" href=\"#chitiettb\">Thu gọn<i class='fa fa-chevron-up'></i></a></p>");
            //html.Append("   </div>");
            //html.Append("  <div class=\"collapse-content opts-wrap\">");
            //html.Append("      <a class=\"opts-row\" href=\"/tao-ma-van-don-ky-gui-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("  <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("     <img src=\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Tạo đơn hàng ký gửi</span>");
            //html.Append("  <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/danh-sach-kien-ky-gui-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span> Danh sách kiện ký gửi</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/thong-ke-cuoc-ky-gui-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span> Thống kê cước VC</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            //html.Append("  </div>");
            //html.Append("  </div>");
            //html.Append("  </div>");
            //#endregion

            //#region Thanh toán hộ
            //html.Append(" <div class=\"page-feature home-collapse-feat\">");
            //html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            //html.Append("    <div class=\"heading\">");
            //html.Append("    <p class=\"left-lb\">");
            //html.Append("         <span class=\"circle-icon\">");
            //html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            //html.Append("     Thanh toán hộ");
            //html.Append("  </p>");
            //html.Append("   <p class=\"right-meta\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-chevron-up'></i>\" data-hide=\"Mở rộng <i class='fa fa-chevron-down'></i>\" href=\"#chitiettb\">Thu gọn<i class='fa fa-chevron-up'></i></a></p>");
            //html.Append("   </div>");
            //html.Append("  <div class=\"collapse-content opts-wrap\">");
            //html.Append("      <a class=\"opts-row\" href=\"/tao-don-thanh-toan-ho-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("  <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("     <img src=\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Tạo đơn</span>");
            //html.Append("  <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/thanh-toan-ho-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span> Danh sách đơn</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            //html.Append("  </div>");
            //html.Append("  </div>");
            //html.Append("  </div>");
            //#endregion

            #region Tài chính
            html.Append(" <div class=\"page-feature home-collapse-feat\">");
            html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            html.Append("    <div class=\"heading\">");
            html.Append("    <p class=\"left-lb\">");
            html.Append("         <span class=\"circle-icon\">");
            html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            html.Append("     Tài chính");
            html.Append("  </p>");
            html.Append("   <p class=\"right-meta\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-chevron-up'></i>\" data-hide=\"Mở rộng <i class='fa fa-chevron-down'></i>\" href=\"#chitiettb\">Thu gọn<i class='fa fa-chevron-up'></i></a></p>");
            html.Append("   </div>");
            html.Append("  <div class=\"collapse-content opts-wrap\">");
            html.Append("      <a class=\"opts-row\" href=\"/rut-tien-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            html.Append("  <span class=\"lb gray-txt\"><span class=\"icon\">");
            html.Append("     <img src=\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Rút tiền</span>");
            html.Append("  <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/rut-tien-te-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Nạp tiền tệ</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            html.Append("  <a class=\"opts-row\" href=\"/nap-tien-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat1.png\" alt=\"\"></span>Nạp tiền</span>");
            html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            html.Append("  </a>");
            html.Append("  <a class=\"opts-row\" href=\"/lich-su-giao-dich-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span>Lịch sử giao dịch</span>");
            html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            html.Append("  </a>");
            //html.Append("  <a class=\"opts-row\" href=\"/lich-su-giao-dich-tien-te-app.aspx?UID=" + UID + "&Key=" + Key + "\">");
            //html.Append("    <span class=\"lb gray-txt\"><span class=\"icon\">");
            //html.Append("      <img src =\"/App_Themes/App/images/icon-homefeat2.png\" alt=\"\"></span>Lịch sử giao dịch tệ</span>");
            //html.Append("   <span class=\"txt\"><i class=\"fa fa-angle-right\"></i></span>");
            //html.Append("  </a>");
            html.Append("  </div>");
            html.Append("  </div>");
            html.Append("  </div>");
            #endregion

            #region Khiếu nại
            html.Append(" <div class=\"page-feature home-collapse-feat\">");
            html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            html.Append("    <div class=\"heading\">");
            html.Append("    <p class=\"left-lb\">");
            html.Append("         <span class=\"circle-icon\">");
            html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            html.Append("     Khiếu nại");
            html.Append("  </p>");
            html.Append("   <a class=\"right-meta\" href=\"/khieu-nai-app.aspx?UID=" + UID + "&Key=" + Key + "\"><i class=\"fa fa-angle-right\"></i></a>");
            html.Append("   </div>");
            html.Append("  </div>");
            html.Append("  </div>");
            #endregion

            #region Quản lý tài khoản
            html.Append(" <div class=\"page-feature home-collapse-feat\">");
            html.Append("   <div class=\"collapse-wrap order-group\" style=\"margin: -10px -15px\">");
            html.Append("    <div class=\"heading\">");
            html.Append("    <p class=\"left-lb\">");
            html.Append("         <span class=\"circle-icon\">");
            html.Append("        <img src =\"/App_Themes/App/images/icon-box.png\" alt=\"\"></span>");
            html.Append("     Điểm tích lũy");
            html.Append("  </p>");
            html.Append("   <a class=\"right-meta\" href=\"/diem-tich-luy-app.aspx?UID=" + UID + "&Key=" + Key + "\"><i class=\"fa fa-angle-right\"></i></a>");
            html.Append("   </div>");
            html.Append("  </div>");
            html.Append("  </div>");
            #endregion

            html.Append(" </div>");            
            ltrhome.Text = html.ToString();            
        }
    }
}
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class MinhAnMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();
                LoadData();
            }
        }

        public void LoadMenu()
        {
            string html = "";
            var categories = MenuController.GetByLevel(0);
            if (categories != null)
            {
                html += "<ul class=\"main-menu-nav\">";
                foreach (var c in categories)
                {
                    //string cls = LeoUtils.ConvertToUnSign(c.MenuName);
                    //string spl = cls.Replace("-", string.Empty);
                    var categories2 = MenuController.GetByLevel(c.ID);
                    if (categories2 != null)
                    {
                        html += " <li class=\"has-submenu\">";

                        if (!string.IsNullOrEmpty(c.MenuLink))
                        {
                            if (Convert.ToBoolean(c.Target))
                                html += "<a target=\"_blank\" href=\"" + c.MenuLink + "\">" + c.MenuName + "</a>";
                            else
                                html += "<a href=\"" + c.MenuLink + "\">" + c.MenuName + "</a>";
                        }
                        else
                        {
                            html += "<a href=\"javascript:;\">" + c.MenuName + "</a>";
                        }

                        html += "<ul class=\"submenu\">";
                        foreach (var item in categories2)
                        {
                            html += " <li>";
                            if (Convert.ToBoolean(c.Target))
                                html += "   <a target=\"_blank\" href =\"" + item.MenuLink + "\">" + item.MenuName + "</a>";
                            else
                                html += "   <a href =\"" + item.MenuLink + "\">" + item.MenuName + "</a>";
                            html += "</li>";
                        }
                        html += " </ul>";
                    }
                    else
                    {
                        html += " <li>";
                        if (Convert.ToBoolean(c.Target))
                            html += "<a target=\"_blank\" href=\"" + c.MenuLink + "\">" + c.MenuName + "</a>";
                        else
                            html += "<a href=\"" + c.MenuLink + "\">" + c.MenuName + "</a>";
                        html += "</li>";
                    }
                }
                html += "</ul>";
                ltrMenu.Text = html;
            }
        }

        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                #region lấy meta
                HtmlHead objHeader = (HtmlHead)Page.Header;
                HtmlMeta meta = new HtmlMeta();
                meta = new HtmlMeta();
                meta.Attributes.Add("name", "description");
                meta.Content = confi.MetaDescription;
                objHeader.Controls.Add(meta);

                meta = new HtmlMeta();
                meta.Attributes.Add("name", "keyword");
                meta.Content = confi.MetaKeyword;
                objHeader.Controls.Add(meta);
                ltrSEO.Text += "<script>" + confi.GoogleAnalytics + "</script>";
                ltrSEO.Text += "<script>" + confi.WebmasterTools + "</script>";

                ltrHeader.Text += "<script>" + confi.HeaderScriptCode + "</script>";
                ltrFooterScript.Text += "<script>" + confi.FooterScriptCode + "</script>";
                #endregion

                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
                string timework = confi.TimeWork;

                ltrFooter.Text = confi.FooterLeft;

                ltrTopLeft.Text += "<li>Tỉ giá: <span>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></li>";
                ltrTopLeft.Text += "<li>Hotline: <span><a href=\"tel:" + hotline + "\">" + hotline + "</a></span></li>";
                ltrTopLeft.Text += "<li>Email: <span><a href=\"mailto:" + email + "\">" + email + "</a></span></li>";

                ltrLogo.Text = "  <a href=\"/\"><img src=\"" + confi.LogoIMG + "\" alt=\"\"></a>";

                ltrPhone.Text = " <p><a href=\"tel:" + hotline + "\">" + hotline + "</a></p>";
                ltrTimeWork.Text = " <p>" + timework + "</p>";

                ltrSocial.Text += "<li><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fa fa-facebook\" aria-hidden=\"true\"></i></a></li>";
                ltrSocial.Text += "<li><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fa fa-twitter\" aria-hidden=\"true\"></i></a></li>";
                ltrSocial.Text += "<li><a href=\"" + confi.YoutubeLink + "\" target=\"_blank\"><i class=\"fa fa-youtube-play\" aria-hidden=\"true\"></i></a></li>";
                ltrSocial.Text += "<li><a href=\"" + confi.Instagram + "\" target=\"_blank\"><i class=\"fa fa-instagram\" aria-hidden=\"true\"></i></a></li>";
                ltrSocial.Text += "<li><a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fa fa-google-plus\" aria-hidden=\"true\"></i></a></li>";

                string infocontent = confi.InfoContent;
                if (Session["infoclose"] == null)
                {
                    if (!string.IsNullOrEmpty(infocontent))
                    {
                        ltr_infor.Text += "<div class=\"sec webinfo\">";
                        ltr_infor.Text += "<div class=\"all-info\">";
                        ltr_infor.Text += "<div class=\"main-info\">";
                        ltr_infor.Text += "<div class=\"textcontent\">";
                        ltr_infor.Text += "<span>" + infocontent + "</span>";
                        ltr_infor.Text += "<a href=\"javascript:;\" onclick=\"closewebinfo()\" class=\"icon-close-info\"><i class=\"fa fa-times\"></i></a>";
                        ltr_infor.Text += "</div></div></div></div>";
                    }
                }
            }
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    var ordershoptemp = OrderShopTempController.GetByUID(acc.ID);
                    int count = 0;
                    if (ordershoptemp.Count > 0)
                        count = ordershoptemp.Count;
                    #region phần thông báo
                    decimal levelID = Convert.ToDecimal(acc.LevelID);
                    int levelID1 = Convert.ToInt32(acc.LevelID);
                    string level = "1 Vương Miện";
                    var userLevel = UserLevelController.GetByID(levelID1);
                    if (userLevel != null)
                    {
                        level = userLevel.LevelName;
                    }
                    string userIMG = "/App_Themes/CIQOrder/images/user-icon.png";
                    var ai = AccountInfoController.GetByUserID(acc.ID);
                    if (ai != null)
                    {
                        if (!string.IsNullOrEmpty(ai.IMGUser))
                            userIMG = ai.IMGUser;
                    }

                    decimal countLevel = UserLevelController.GetAll("").Count();
                    decimal te = levelID / countLevel;
                    te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                    decimal tile = te * 100;
                    string levelIconList = "";
                    string levelIconSingle = "";
                    var userLevels = UserLevelController.GetAll("");
                    if (userLevels.Count > 0)
                    {
                        foreach (var item in userLevels)
                        {
                            if (item.ID <= levelID)
                            {
                                levelIconList += "<img style=\"margin-right:5px;width:15%\" src=\"/App_Themes/StarExpress/images/vm-active.png\">";
                                //levelIconSingle += "<img src=\"/App_Themes/CIQOrder/images/vm-active.png\">";
                            }
                            else
                            {
                                levelIconList += "<img style=\"margin-right:5px;width:15%\" src=\"/App_Themes/StarExpress/images/vm-inactive.png\">";
                            }
                        }
                    }
                    #endregion

                    #region New
                    ltrLogin.Text += "<div class=\"acc-info\">";
                    ltrLogin.Text += "<a class=\"acc-info-btn\" href=\"/thong-tin-nguoi-dung\"><i class=\"icon fa fa-user\"></i><span>" + username + "</span></a>";
                    ltrLogin.Text += "<div class=\"status-desktop\">";
                    ltrLogin.Text += "<div class=\"status-wrap\">";
                    ltrLogin.Text += "<div class=\"status__header\">";
                    ltrLogin.Text += "<h4>" + level + "</h4>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"status__body\">";
                    ltrLogin.Text += "<div class=\"level\">";
                    ltrLogin.Text += "<div class=\"level__info\">";
                    ltrLogin.Text += "<p>Level</p>";
                    ltrLogin.Text += "<p class=\"rank\">" + level + "</p>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"level__process\"><span style=\"width: " + tile + "%\"></span></div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"balance\">";
                    ltrLogin.Text += "<p>Số dư:</p>";
                    ltrLogin.Text += "<div class=\"balance__number\"><p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p></div>";
                    ltrLogin.Text += "</div>";
                    if (acc.RoleID != 1)
                        ltrLogin.Text += "<div class=\"links\"><a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "<div class=\"links\"><a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "<div class=\"links\"><a href=\"/danh-sach-don-hang?t=1\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "<div class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"status__footer\"><a href=\"/dang-xuat\" class=\"ft-btn\">ĐĂNG XUẤT</a></div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"status-mobile\">";
                    ltrLogin.Text += "<a href=\"/thong-tin-nguoi-dung\" class=\"user-menu-logo\"><img src=\"" + userIMG + "\" alt=\"\"></a>";
                    ltrLogin.Text += "<h3 class=\"username\">" + username + "</h3>";
                    ltrLogin.Text += "<div class=\"user-info\">Số tiền: <span class=\"money\">" + string.Format("{0:N0}", acc.Wallet) + "</span> vnđ | Level <span class=\"vip\">" + level + "</span></div>";
                    ltrLogin.Text += "<div class=\"nav-percent\">";
                    ltrLogin.Text += "<div class=\"nav-percent-ok\" style=\"width: " + tile + "%\"></div>";
                    ltrLogin.Text += "</div>";
                    ltrLogin.Text += "<div class=\"profile-bottom\">";
                    ltrLogin.Text += "<ul class=\"menu-in-profile\">";
                    ltrLogin.Text += "<li><a href=\"/\"><i class=\"fa fa-home\"></i>TRANG CHỦ</a></li>";
                    ltrLogin.Text += "<li><a href=\"/theo-doi-mvd\"><i class=\"fa fa-search\"></i>TRACKING</a></li>";
                    ltrLogin.Text += "<li><a href=\"/danh-sach-don-hang?t=1\"><i class=\"fa fa-home\"></i>MUA HÀNG HỘ</a></li>";
                    ltrLogin.Text += "<li><a href=\"/lich-su-giao-dich\"><i class=\"fa fa-money\"></i>TÀI CHÍNH</a></li>";
                    ltrLogin.Text += "<li><a href=\"/khieu-nai\"><i class=\"fa fa-exclamation\"></i>KHIẾU NẠI</a></li>";
                    ltrLogin.Text += "<li><a href=\"/thong-tin-nguoi-dung\"><i class=\"fa fa-user\"></i>QUẢN LÝ TÀI KHOẢN</a></li>";
                    ltrLogin.Text += "</ul>";
                    ltrLogin.Text += "</div><a href=\"/dang-xuat\" class=\"main-btn\">Đăng xuất</a></div>";
                    ltrLogin.Text += "<div class=\"overlay-status-mobile\"></div>";
                    ltrLogin.Text += "</div>";
                    #endregion

                }
            }
            else
            {
                ltrLogin.Text = "<div class=\"login-register\"><a href=\"/dang-ky\">Đăng ký</a>/<a href=\"/dang-nhap\">Đăng nhập</a></div>";
            }
        }
    }
}
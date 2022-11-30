using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class UserMasterNew : System.Web.UI.MasterPage
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
                var ordershoptemp = OrderShopTempController.GetByUID(obj_user.ID);
                //ltrCountCart.Text = "(" + ordershoptemp.Count + ")";
            }

            if (HttpContext.Current.Session["notshowEx"] != null)
            {
                if (HttpContext.Current.Session["notshowEx"].ToString() != "0")
                {
                    hdfShowExtension.Value = "1";
                }
                else
                {
                    hdfShowExtension.Value = "0";
                }
            }
        }

        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username_current);
            if (acc != null)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    string uImage = "";
                    var ui = AccountInfoController.GetByUserID(acc.ID);
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

                    decimal levelID = Convert.ToDecimal(acc.LevelID);
                    int levelID1 = Convert.ToInt32(acc.LevelID);
                    string level = "Vip 0";
                    var userLevel = UserLevelController.GetByID(levelID1);
                    if (userLevel != null)
                    {
                        level = userLevel.LevelName;
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
                                levelIconList += "<img style=\"margin-right:5px;width:15%\" src=\"/App_Themes/CIQOrder/images/vm-active.png\">";
                                //levelIconSingle += "<img src=\"/App_Themes/CIQOrder/images/vm-active.png\">";
                            }
                            else
                            {
                                levelIconList += "<img style=\"margin-right:5px;width:15%\" src=\"/App_Themes/CIQOrder/images/vm-inactive.png\">";
                            }
                        }
                    }


                    if (acc.RoleID != 1)
                        ltrHeaderLeft.Text += "<li><a href=\"/manager/login\">Quản lý</a></li>";
                    ltrHeaderLeft.Text += "<li class=\"hide-on-med-and-down\">¥1 = </span>" + string.Format("{0:N0}", config.Currency) + " Đồng</li>";
                    ltrHeaderLeft.Text += "<li class=\"hide-on-med-and-down\"><a class=\"waves-effect waves-block waves-light toggle-fullscreen\" href=\"javascript:void(0);\"><i class=\"material-icons\">settings_overscan</i></a></li>";
                    //ltrHeaderLeft.Text += "<li class=\"hide-on-large-only\"><a class=\"waves-effect waves-block waves-light search-button\" href=\"javascript:void(0);\"><i class=\"material-icons\">search</i></a></li>";
                    var noti = NotificationsController.GetAll(acc.ID);
                    if (acc.RoleID != 1)
                    {
                        ltrHeaderLeft.Text += "<li><a class=\"waves-effect waves-block waves-light\" href=\"/manager/admin-noti\"><i class=\"material-icons\">notifications_none<small class=\"notification-badge orange accent-3\">" + noti.Where(x => x.Status == 1).ToList().Count + "</small></i></a></li>";
                    }
                    else
                    {
                        ltrHeaderLeft.Text += "<li><a class=\"waves-effect waves-block waves-light\" href=\"/thong-bao-cua-ban\"><i class=\"material-icons\">notifications_none<small class=\"notification-badge orange accent-3\">" + noti.Where(x => x.Status == 1).ToList().Count + "</small></i></a></li>";
                    }
                    ltrHeaderLeft.Text += "<li class=\"user-box\"><a class=\"waves-block\" href=\"javascript:void(0);\"><span class=\"avatar-status avatar-online\">";
                    ltrHeaderLeft.Text += "<img src=\"" + uImage + "\" alt=\"avatar\"><i></i></span></a>";

                    //New
                    ltrHeaderLeft.Text += "<div class=\"status-desktop\">";
                    ltrHeaderLeft.Text += "<div class=\"status-wrap\">";
                    ltrHeaderLeft.Text += "<div class=\"status__header\"><h4>" + level + "</h4></div>";
                    ltrHeaderLeft.Text += "<div class=\"status__body\">";
                    ltrHeaderLeft.Text += "<div class=\"level\">";
                    ltrHeaderLeft.Text += "<div class=\"level\">";
                    ltrHeaderLeft.Text += "<div class=\"level__info\"><p>Level</p><p class=\"rank\">" + level + "</p></div>";
                    ltrHeaderLeft.Text += "<div class=\"level__process\"><span style=\"width: " + tile + "%\"></span></div>";
                    ltrHeaderLeft.Text += "</div>";
                    ltrHeaderLeft.Text += "<div class=\"balance\"><p>Số dư:</p>";
                    ltrHeaderLeft.Text += "<div class=\"balance__number\"><p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p></div>";
                    ltrHeaderLeft.Text += "</div>";
                    if (acc.RoleID != 1)
                        ltrHeaderLeft.Text += "<div class=\"links\"><a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrHeaderLeft.Text += "<div class=\"links\"><a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrHeaderLeft.Text += "<div class=\"links\"><a href=\"/danh-sach-don-hang?t=1\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrHeaderLeft.Text += "<div class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></div>";
                    ltrHeaderLeft.Text += "</div>";
                    ltrHeaderLeft.Text += "<div class=\"status__footer\"><a href=\"/dang-xuat\" class=\"ft-btn\">ĐĂNG XUẤT</a></div></div></div>";

                    ltrHeaderLeft.Text += "</li>";



                }

            }
        }
    }
}
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class ClientMasterNew : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                ltrCountCart.Text = "(" + ordershoptemp.Count + ")";
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
                    string hotline = config.Hotline;
                    ltrHotlineCall.Text += "<a href=\"tel:" + hotline + "\" class=\"fancybox\">";
                    ltrHotlineCall.Text += "<div class=\"coccoc-alo-phone coccoc-alo-green coccoc-alo-show\" id=\"coccoc-alo-phoneIcon\">";
                    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-circle\"></div>";
                    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-circle-fill\"></div>";
                    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-img-circle\"></div>";
                    ltrHotlineCall.Text += "</div>";
                    ltrHotlineCall.Text += "</a>";

                    ltrinfo.Text += "<a href=\"/\" class=\"right-it rate\"><i class=\"fa fa-home\"></i></a>";
                    ltrinfo.Text += "<a href=\"javascript:;\" class=\"right-it rate\"> ¥1 = " + string.Format("{0:N0}", config.Currency) + "</a>";
                    var noti = NotificationsController.GetAll(acc.ID);
                    if (acc.RoleID != 1)
                    {
                        ltrinfo.Text += "<a href=\"/manager/admin-noti\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Where(x=>x.Status == 1).ToList().Count + "</span></a>";
                    }
                    else
                        ltrinfo.Text += "<a href=\"/thong-bao-cua-ban\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">" + noti.Where(x => x.Status == 1).ToList().Count + "</span></a>";
                    string html = "";
                    decimal levelID = Convert.ToDecimal(acc.LevelID);
                    int levelID1 = Convert.ToInt32(acc.LevelID);
                    string level = "1 Vương Miện";
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



                    ltrinfo.Text += "  <div class=\"right-it username dropdown\">";
                    ltrinfo.Text += "      <a href=\"#\" class=\"link__item\">" + acc.Username + "</a>";
                    ltrinfo.Text += "<div class=\"status-wrap\">";
                    ltrinfo.Text += "  <div class=\"status\">";
                    //ltrinfo.Text += "      <header><h4>" + level + "</h4></header>";
                    ltrinfo.Text += "      <header><h4 style=\"font-size:16px;\">" + acc.Username + "</h4></header>";
                    ltrinfo.Text += "      <main>";
                    //ltrinfo.Text += "          <section class=\"level\">";
                    //ltrinfo.Text += "              <div class=\"level__info\">";
                    //ltrinfo.Text += "                  <p style=\"width:85%;text-align:left;\">Level</p>";
                    //ltrinfo.Text += "                  <p class=\"rank\">" + level + "</p>";
                    //ltrinfo.Text += "              </div>";
                    //ltrinfo.Text += "              <div class=\"process\">";
                    //ltrinfo.Text += "                  <span style=\"width: " + tile + "%\"></span>";
                    //ltrinfo.Text += "              </div>";
                    //ltrinfo.Text += "          </section>";

                    ltrinfo.Text += "          <section class=\"level\">";
                    ltrinfo.Text += "              <div class=\"process\" style=\"background:none;height:auto;\">";
                    ltrinfo.Text += levelIconList;
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "              <div class=\"process\">";
                    ltrinfo.Text += "                  <span style=\"width: " + tile + "%\"></span>";
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"balance\">";
                    ltrinfo.Text += "              <p>Số dư:</p>";
                    ltrinfo.Text += "              <div class=\"balance__number\">";
                    ltrinfo.Text += "                  <p class=\"vnd\">" + string.Format("{0:N0}", acc.Wallet) + " vnđ</p>";
                    //ltrLogin.Text += "                  <p class=\"cny\">2450Y</p>";
                    ltrinfo.Text += "              </div>";
                    ltrinfo.Text += "          </section>";
                    if (acc.RoleID != 1)
                    {
                        ltrinfo.Text += "          <section class=\"links\">";
                        ltrinfo.Text += "              <a href=\"/manager/login\">Quản trị<i class=\"fa fa-caret-right\"></i></a>";
                        ltrinfo.Text += "          </section>";
                    }
                    ltrinfo.Text += "          <section class=\"links\">";
                    ltrinfo.Text += "              <a href=\"/thong-tin-nguoi-dung\">Thông tin tài khoản<i class=\"fa fa-caret-right\"></i></a>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"links\">";
                    ltrinfo.Text += "              <a href=\"/danh-sach-don-hang?t=1\">Đơn hàng của bạn<i class=\"fa fa-caret-right\"></i></a>";
                    ltrinfo.Text += "          </section>";
                    ltrinfo.Text += "          <section class=\"links\"><a href=\"/lich-su-giao-dich\">Lịch sử giao dịch<i class=\"fa fa-caret-right\"></i></a></section>";
                    ltrinfo.Text += "      </main>";
                    ltrinfo.Text += "      <footer><a href=\"/dang-xuat\" class=\"btn btn-3\">ĐĂNG XUẤT</a></footer>";
                    ltrinfo.Text += "  </div>";
                    ltrinfo.Text += "</div>";
                    ltrinfo.Text += "  </div>";
                    //ltrinfo.Text += "<span class=\"right-it username\">" + obj_user.Username + "</span>";
                    ltrinfo.Text += "<a href=\"/lich-su-giao-dich\" class=\"right-it rate\"><span class=\"right-it username\">Số dư TK: " + string.Format("{0:N0}", acc.Wallet) + " vnđ</span></a>";
                    ltrinfo.Text += "<a href=\"/\" class=\"right-it rate\"><span class=\"right-it username\">Trang ngoài</span></a>";
                    ltrinfo.Text += "<a href=\"/dang-xuat\" class=\"right-it logout\"><i class=\"fa fa-sign-out\"></i>Sign out</a>";
                    #region Lấy ra thông tin bên trái
                    string uImage = "";
                    var ui = AccountInfoController.GetByUserID(acc.ID);
                    if (ui != null)
                    {
                        if (!string.IsNullOrEmpty(ui.IMGUser))
                        {
                            if (File.Exists(Server.MapPath("" + ui.IMGUser + "")))
                            {
                                uImage = "<img src=\"" + ui.IMGUser + "\" alt=\"\" style=\"border:solid 1px #ccc;width:80px;height:80px\" />";
                            }
                            else
                            {
                                uImage = "<img src=\"NO-IMAGE.jpg\" alt=\"\" style=\"border:solid 1px #ccc;width:80px;height:80px\"  />";
                            }
                        }
                        else
                        {
                            uImage = "<img src=\"NO-IMAGE.jpg\" alt=\"\" style=\"border:solid 1px #ccc;width:80px;height:80px\"  />";
                        }
                    }
                    ltrUserInfo.Text += "<div class=\"inside-user-info\">";
                    ltrUserInfo.Text += uImage;
                    ltrUserInfo.Text += "<a href=\"/thong-tin-nguoi-dung.aspx\" class=\"uname\">" + username_current + "</a>";
                    ltrUserInfo.Text += "<div class=\"process-user\" style=\"\">";
                    ltrUserInfo.Text += levelIconList;
                    ltrUserInfo.Text += "</div>";
                    ltrUserInfo.Text += "<div class=\"status-wrap\" style=\"display: block; position: relative; float: left; width: 100%;\">";
                    ltrUserInfo.Text += "<div class=\"status\" style=\"top: 0; width: 100%;\">";
                    ltrUserInfo.Text += "<main style=\"width: 100%; float: left; background: none;\">";
                    ltrUserInfo.Text += "<section class=\"level\" style=\"background: none;\">";
                    ltrUserInfo.Text += "<div class=\"process\"><span style=\"width: " + tile + "%\"></span></div>";
                    ltrUserInfo.Text += "</section>";
                    ltrUserInfo.Text += "</main>";
                    ltrUserInfo.Text += "</div>";
                    ltrUserInfo.Text += "</div>";
                    ltrUserInfo.Text += "<a href=\"/lich-su-giao-dich\" class=\"u-wallet-money\">Số dư: " + string.Format("{0:N0}", acc.Wallet) + " Đ</a>";
                    ltrUserInfo.Text += "</div>";
                    #endregion
                }
                //if (obj_user.RoleID == 0)
                //{

                //}
                int UID = acc.ID;
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
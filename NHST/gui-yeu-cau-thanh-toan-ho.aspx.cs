using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class gui_yeu_cau_thanh_toan_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "nhutsg8844";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }

        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int level = Convert.ToInt32(u.LevelID);
                int UID = u.ID;
                double pc_config = 0;
                double currencygiagoc = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                    currencygiagoc = Convert.ToDouble(config.PricePayHelpDefault);
                }
                StringBuilder html = new StringBuilder();

                txtUsername.Text = username;
                var rq = RouteData.Values["order"].ToString();
                if (rq.Contains("all_"))
                {
                    double TotalPrice = 0;
                    string[] splitList = rq.Split('_');
                    string[] list = splitList[1].Split('|');

                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            int phID = Convert.ToInt32(list[i]);
                            var ph = PayHelpTempController.GetByID(u.ID, phID);
                            if (ph != null)
                            {
                                html.Append("<div class=\"row order-wrap itemyeuau\" data-id=\"" + ph.ID + "\">");
                                html.Append("<div class=\"input-field col s12 m6\">");
                                html.Append("<input type=\"text\"  value=\"" + ph.Desc2 + "\" class=\"txtDesc2\" value=\"0\" disabled>");
                                html.Append("<label class=\"active\">Giá tiền (tệ)</label>");
                                html.Append("</div>");
                                html.Append("<div class=\"input-field col s12 m6\">");
                                html.Append("<input type=\"text\" value=\"" + ph.Desc1 + "\" disabled class=\"txtDesc1\">");
                                html.Append("<label class=\"active\">Nội dung</label>");
                                html.Append("</div>");
                                html.Append("<a href='javascript:;' class=\"remove-order tooltipped\" data-position=\"top\" data-tooltip=\"Xóa\"><i class=\"material-icons\">remove_circle</i></a>");
                                html.Append("</div>");
                                TotalPrice += Convert.ToDouble(ph.Desc2);
                            }
                        }

                        double totalpricevnd = 0;
                        double currency = 0;

                        double amount = TotalPrice;
                        if (amount > 0)
                        {
                            double totalpriceVNDGiagoc = currencygiagoc * amount;

                            var pricechange = PriceChangeController.GetByPriceFT(amount);
                            double pc = 0;
                            if (pricechange != null)
                            {
                                if (level == 1)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                                }
                                else if (level == 2)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                                }
                                else if (level == 3)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                                }
                                else if (level == 4)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                                }
                                else if (level == 5)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                                }
                                else if (level == 6)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                                }
                                else if (level == 7)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                                }
                                else if (level == 8)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                                }
                                else if (level == 9)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                                else
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                                //pc = Convert.ToDouble(pricechange.PriveVND);
                            }

                            totalpricevnd = pc * amount;
                            currency = pc;
                        }

                        pAmount.Value = amount;
                        rVND.Value = totalpricevnd;
                        rTigia.Value = currency;
                    }
                }
                else
                {
                    double TotalPrice = 0;
                    int phID = Convert.ToInt32(rq);
                    var ph = PayHelpTempController.GetByID(u.ID, phID);
                    if (ph != null)
                    {
                        html.Append("<div class=\"row order-wrap itemyeuau\" data-id=\"" + ph.ID + "\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input type=\"text\"  value=\"" + ph.Desc2 + "\" class=\"txtDesc2\" value=\"0\" disabled>");
                        html.Append("<label class=\"active\">Giá tiền (tệ)</label>");
                        html.Append("</div>");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input type=\"text\" value=\"" + ph.Desc1 + "\" disabled class=\"txtDesc1\">");
                        html.Append("<label class=\"active\">Nội dung</label>");
                        html.Append("</div>");
                        html.Append("<a href='javascript:;' class=\"remove-order tooltipped\" data-position=\"top\" data-tooltip=\"Xóa\"><i class=\"material-icons\">remove_circle</i></a>");
                        html.Append("</div>");
                        TotalPrice += Convert.ToDouble(ph.Desc2);
                    }

                    double totalpricevnd = 0;
                    double currency = 0;

                    double amount = TotalPrice;
                    if (amount > 0)
                    {
                        double totalpriceVNDGiagoc = currencygiagoc * amount;

                        var pricechange = PriceChangeController.GetByPriceFT(amount);
                        double pc = 0;
                        if (pricechange != null)
                        {
                            if (level == 1)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                            }
                            else if (level == 2)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                            }
                            else if (level == 3)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                            }
                            else if (level == 4)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                            }
                            else if (level == 5)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                            }
                            else if (level == 6)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                            }
                            else if (level == 7)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                            }
                            else if (level == 8)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                            }
                            else if (level == 9)
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                            }
                            else
                            {
                                pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                            }
                            //pc = Convert.ToDouble(pricechange.PriveVND);
                        }

                        totalpricevnd = pc * amount;
                        currency = pc;
                    }

                    pAmount.Value = amount;
                    rVND.Value = totalpricevnd;
                    rTigia.Value = currency;
                }
                ltrTTH.Text = html.ToString();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int level = Convert.ToInt32(u.LevelID);
                int UID = u.ID;
                double pc_config = 0;
                double currencygiagoc = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                    currencygiagoc = Convert.ToDouble(config.PricePayHelpDefault);
                }
                double amount = 0;
                if (!string.IsNullOrEmpty(hdfAmount.Value))
                    amount = Convert.ToDouble(hdfAmount.Value);

                if (amount > 0)
                {
                    double totalpriceVNDGiagoc = currencygiagoc * amount;

                    string note = txtNote.Text;
                    string list = hdflist.Value;
                    var pricechange = PriceChangeController.GetByPriceFT(amount);
                    double pc = 0;
                    if (pricechange != null)
                    {
                        if (level == 1)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                        }
                        else if (level == 2)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                        }
                        else if (level == 3)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                        }
                        else if (level == 4)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                        }
                        else if (level == 5)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                        }
                        else if (level == 6)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                        }
                        else if (level == 7)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                        }
                        else if (level == 8)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                        }
                        else if (level == 9)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                        }
                        else
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                        }
                        //pc = Convert.ToDouble(pricechange.PriveVND);
                    }

                    double totalpricevnd = pc * amount;
                    string kq = PayhelpController.Insert(UID, username, note, amount.ToString(), totalpricevnd.ToString(), pc.ToString(),
                        currencygiagoc.ToString(), totalpriceVNDGiagoc.ToString(), 0, "", currentDate, username);
                    int pID = kq.ToInt(0);
                    if (pID > 0)
                    {
                        string[] items = list.Split('|');
                        for (int i = 0; i < items.Length - 1; i++)
                        {
                            string[] item = items[i].Split(',');
                            int phID = Convert.ToInt32(item[0]);
                            var ph = PayHelpTempController.GetByID(u.ID, phID);
                            if (ph != null)
                            {
                                string des1 = item[1];
                                string des2 = item[2];
                                if (!string.IsNullOrEmpty(des1) || !string.IsNullOrEmpty(des2))
                                {
                                    string vID = PayhelpDetailController.Insert(pID, des1, des2, currentDate, username);
                                    PayhelpDetailController.UpdateFriendsAccount(vID.ToInt(0), ph.FriendsAccount, ph.OrderID, ph.Customer);
                                    PayHelpTempController.Delete(phID);
                                }
                            }
                            else
                            {
                                string des1 = item[1];
                                string des2 = item[2];
                                if (!string.IsNullOrEmpty(des1) || !string.IsNullOrEmpty(des2))
                                {
                                    PayhelpDetailController.Insert(pID, des1, des2, currentDate, username);
                                }
                            }
                        }

                        var setNoti = SendNotiEmailController.GetByID(18);
                        if (setNoti != null)
                        {
                            if (setNoti.IsSentNotiAdmin == true)
                            {
                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        NotificationsController.Inser(admin.ID, admin.Username, pID, "Có đơn thanh toán hộ mới ID là: " + pID + "",
                          8, currentDate, username, false);
                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                        string datalink = "" + strUrl + "manager/OrderDetail/" + pID;
                                        PJUtils.PushNotiDesktop(admin.ID, "Có đơn hàng thanh toán hộ mới là : " + pID, datalink);
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
                                            PJUtils.SendMailGmail_new( admin.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", "Có đơn thanh toán hộ mới ID là: " + pID, "");
                                        }
                                        catch { }

                                    }
                                }
                            }

                        }
                    }
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Gửi yêu cầu thành công", "s", true, "/danh-sach-thanh-toan-ho", Page);
                }
                else
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập số tiền", "e", false, Page);
            }
            else
                PJUtils.ShowMessageBoxSwAlert("Không tìm thấy user", "e", false, Page);
        }

        [WebMethod]
        public static string getCurrency(string totalprice)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int level = Convert.ToInt32(u.LevelID);
                double pc_config = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                }

                double amount = Convert.ToDouble(totalprice);
                if (amount > 0)
                {
                    var pricechange = PriceChangeController.GetByPriceFT(amount);
                    double pc = 0;
                    if (pricechange != null)
                    {
                        if (level == 1)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                        }
                        else if (level == 2)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                        }
                        else if (level == 3)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                        }
                        else if (level == 4)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                        }
                        else if (level == 5)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                        }
                        else if (level == 6)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                        }
                        else if (level == 7)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                        }
                        else if (level == 8)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                        }
                        else if (level == 9)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                        }
                        else
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                        }
                        return pc.ToString();
                    }
                }
            }
            return "0";
        }

    }
}
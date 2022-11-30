using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;

namespace NHST
{
    public partial class thanh_toan_ho : System.Web.UI.Page
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
                ltrIfn.Text = username;
                var listpa = PayhelpController.GetAllUID(u.ID);
                if (listpa.Count > 0)
                {
                    pagingall(listpa);
                }
            }
        }
        #region Paging
        public void pagingall(List<tbl_PayHelp> acs)
        {
            int PageSize = 15;
            if (acs.Count > 0)
            {
                int TotalItems = acs.Count;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;

                int UID = Convert.ToInt32(ViewState["UID"]);
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    string tdstatus = "Đã duyệt";
                    int status = Convert.ToInt32(item.Status);
                    if (status == 0)
                        tdstatus = "Chưa thanh toán";
                    else if (status == 1)
                        tdstatus = "Đã thanh toán";
                    else
                        tdstatus = "Đã hủy";

                    ltr.Text += "<tr>";
                    ltr.Text += "   <td>" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>";
                    ltr.Text += "   <td>" + string.Format("{0:N0}", item.TotalPrice).Replace(",", ".") + "</td>";
                    ltr.Text += "   <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)).Replace(",", ".") + "</td>";
                    ltr.Text += "   <td>" + string.Format("{0:N0}", Convert.ToDouble(item.Currency)).Replace(",", ".") + "</td>";
                    bool isNotComplete = false;
                    if (item.IsNotComplete != null)
                        isNotComplete = Convert.ToBoolean(item.IsNotComplete);
                    if (isNotComplete == true)
                    {
                        ltr.Text += "   <td><span class='bg-red'>Đang hoàn thiện</span></td>";
                    }
                    else
                    {
                        ltr.Text += "   <td>" + PJUtils.ReturnStatusPayHelp(status) + "</td>";
                    }
                    ltr.Text += "   <td>";
                    if (status == 0)
                        ltr.Text += "<a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover \" style=\"text-decoration:underline;\" onclick=\"deleteTrade('" + item.ID + "')\">Hủy yêu cầu</a>";
                    ltr.Text += "   </td>";
                    ltr.Text += "   <td><a class=\"pill-btn btn order-btn main-btn hover submit-btn\" href=\"/chi-tiet-thanh-toan-ho/" + item.ID + "\" >Chi tiết</a></td>";
                    ltr.Text += "   <td>";
                    if (status == 0)
                        ltr.Text += "<a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover submit-btn\" onclick=\"paymoney($(this),'" + item.ID + "')\">Thanh toán</a>";
                    ltr.Text += "   </td>";
                    ltr.Text += "</tr>";
                }
            }
        }
        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));

        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.Append("<a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a>");
                output.Append("<a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><</a>");
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                //output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {

                    output.Append("<span class=\"current\">" + i.ToString() + "</span>");
                    //output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                    //output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a>");
                output.Append("<a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">></a>");
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                output.Append("<a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
        protected void btnSend_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
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
                            string des1 = item[0];
                            string des2 = item[1];
                            if (!string.IsNullOrEmpty(des1) || !string.IsNullOrEmpty(des2))
                            {
                                PayhelpDetailController.Insert(pID, des1, des2, currentDate, username);
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
                                        PJUtils.PushNotiDesktop(admin.ID, "Có đơn thanh toán hộ mới ID là: " + pID, datalink);
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
                    PJUtils.ShowMessageBoxSwAlert("Gửi yêu cầu thành công", "s", true, Page);
                }
                else
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập số tiền", "e", false, Page);
            }
            else
                PJUtils.ShowMessageBoxSwAlert("Không tìm thấy user", "e", false, Page);
        }

        protected void btna_Click(object sender, EventArgs e)
        {
            PJUtils.ShowMessageBoxSwAlert("Không tìm thấy user", "e", false, Page);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                int ID = hdfTradeID.Value.ToInt(0);
                var p = PayhelpController.GetByIDAndUID(ID, UID);
                if (p != null)
                {
                    PayhelpController.UpdateStatus(ID, 2, DateTime.Now, username);
                    PJUtils.ShowMessageBoxSwAlert("Hủy yêu cầu thành công", "s", true, Page);
                }
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            var id = hdfTradeID.Value.ToInt(0);
            if (id > 0)
            {
                string username = Session["userLoginSystem"].ToString();
                DateTime currentDate = DateTime.Now;
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    var p = PayhelpController.GetByIDAndUID(id, UID);
                    if (p != null)
                    {
                        double wallet = Convert.ToDouble(u.Wallet);
                        double walletCYN = Convert.ToDouble(u.WalletCYN);

                        double Totalprice_left = 0;

                        double Currency = Convert.ToDouble(p.Currency);
                        double TotalPrice = Convert.ToDouble(p.TotalPrice);
                        double TotalPriceVND = Convert.ToDouble(p.TotalPriceVND);
                        var setNoti = SendNotiEmailController.GetByID(18);
                        if (walletCYN > 0)
                        {
                            if (walletCYN >= TotalPrice)
                            {
                                double walletCYN_left = walletCYN - TotalPrice;

                                AccountController.updateWalletCYN(UID, walletCYN_left);
                                HistoryPayWalletCYNController.Insert(UID, username, TotalPrice, walletCYN_left, 1, 1, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
                                    currentDate, username);

                                PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var adminlist = AccountController.GetAllByRoleID(0);
                                        if (adminlist.Count > 0)
                                        {
                                            foreach (var a in adminlist)
                                            {
                                                NotificationsController.Inser(a.ID, a.Username, id, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
            8, currentDate, username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + id;
                                                PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", datalink);
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
                                                        "Thông báo tại NHẬP HÀNG 360.", username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
                                                }
                                                catch { }

                                            }
                                        }
                                    }
                                }
                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                            }
                            else
                            {
                                double walletCYN_left = TotalPrice - walletCYN;
                                double totalpricevndpay = walletCYN_left * Currency;
                                if (wallet >= totalpricevndpay)
                                {

                                    AccountController.updateWalletCYN(UID, 0);
                                    HistoryPayWalletCYNController.Insert(UID, username, walletCYN, 0, 1, 1, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
                                        currentDate, username);

                                    //double totalpricevndpay = walletCYN_left * Currency;
                                    double walletleft = wallet - totalpricevndpay;
                                    AccountController.updateWallet(UID, walletleft, currentDate, username);

                                    HistoryPayWalletController.Insert(UID, username, 0, totalpricevndpay,
                                        username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", walletleft, 1, 9, currentDate, username);

                                    PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiAdmin == true)
                                        {
                                            var adminlist = AccountController.GetAllByRoleID(0);
                                            if (adminlist.Count > 0)
                                            {
                                                foreach (var a in adminlist)
                                                {
                                                    NotificationsController.Inser(a.ID, a.Username, id, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
                8, currentDate, username, false);
                                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                    string datalink = "" + strUrl + "manager/OrderDetail/" + id;
                                                    PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", datalink);
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
                                                            "Thông báo tại NHẬP HÀNG 360.", username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
                                                    }
                                                    catch { }

                                                }
                                            }
                                        }
                                    }
                                    PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
                                }
                            }
                        }
                        else
                        {
                            if (wallet >= TotalPriceVND)
                            {
                                double walletleft = wallet - TotalPriceVND;
                                AccountController.updateWallet(UID, walletleft, currentDate, username);

                                HistoryPayWalletController.Insert(UID, username, 0, TotalPriceVND,
                                    username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", walletleft, 1, 9, currentDate, username);

                                PayhelpController.UpdateStatus(id, 1, currentDate, username);

                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var adminlist = AccountController.GetAllByRoleID(0);
                                        if (adminlist.Count > 0)
                                        {
                                            foreach (var a in adminlist)
                                            {
                                                NotificationsController.Inser(a.ID, a.Username, id, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
            8, currentDate, username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + id;
                                                PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", datalink);
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
                                                        "Thông báo tại NHẬP HÀNG 360.", username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
                                                }
                                                catch { }

                                            }
                                        }
                                    }
                                }

                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
                            }
                        }
                    }
                }
            }
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
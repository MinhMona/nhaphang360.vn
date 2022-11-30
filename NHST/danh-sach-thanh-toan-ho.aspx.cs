using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class danh_sach_thanh_toan_ho : System.Web.UI.Page
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
                StringBuilder html = new StringBuilder();

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

                    html.Append("<tr>");

                    html.Append(" <tr>");
                    html.Append(" <td>" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                    html.Append(" <td>" + string.Format("{0:N0}", item.TotalPrice).Replace(",", ".") + "</td>");
                    html.Append(" <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)).Replace(",", ".") + "</td>");
                    html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Currency)).Replace(",", ".") + "</td>");

                    bool isNotComplete = false;
                    if (item.IsNotComplete != null)
                        isNotComplete = Convert.ToBoolean(item.IsNotComplete);
                    if (isNotComplete == true)
                    {
                        html.Append("<td class=\"no-wrap\"><span class=\"badge red darken-2 white-text border-radius-2\">Đang hoàn thiện</span></td>");
                    }
                    else
                    {
                        html.Append("   <td class=\"no-wrap\">" + PJUtils.ReturnStatusPayHelpNew(status) + "</td>");
                    }

                    html.Append("<td><div class=\"action-table\">");
                    if (status == 0)
                        html.Append(" <a href=\"javascript:;\" onclick=\"deleteTrade('" + item.ID + "')\" data-position=\"top\"><i class=\"material-icons\">delete</i><span>Xóa</span></a>");
                    html.Append("<a href=\"/chi-tiet-thanh-toan-ho/" + item.ID + "\" data-position=\"top\"><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a>");
                    if (status == 0)
                        html.Append("<a href=\"javascript:;\" onclick=\"paymoney($(this),'" + item.ID + "')\" data-position=\"top\"><i class=\"material-icons\">payment</i><span>Thanh toán</span></a>");
                    html.Append(" </div></td>");
                    html.Append("</tr>");
                }
                ltr.Text = html.ToString();
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
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<a class=\"prev-page pagi-button\" title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Prev</a>");
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
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<a class=\"pagi-button current-active\">" + i.ToString() + "</a>");
                }
                else
                {
                    output.Append("<a class=\"pagi-button\" href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
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
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<a class=\"next-page pagi-button\" title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

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

                                #region Old
                                //AccountController.updateWalletCYN(UID, walletCYN_left);
                                //HistoryPayWalletCYNController.Insert(UID, username, TotalPrice, walletCYN_left, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
                                //    currentDate, username);
                                //PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                #endregion
                                int st = TransactionController.PayMent(UID, id, walletCYN_left, username, TotalPrice, 1, 1, username + " đã trả tiền thanh toán tiền hộ đơn: "+id+".", currentDate, 1);
                                if (st == 1)
                                {
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
                                                    PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán hộ đơn: " + id + ".", datalink);
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
                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau.", "e", true, Page);
                                }
                            }
                            else
                            {
                                double walletCYN_left = TotalPrice - walletCYN;
                                double totalpricevndpay = walletCYN_left * Currency;
                                if (wallet >= totalpricevndpay)
                                {
                                    double walletleft = wallet - totalpricevndpay;
                                    #region Old
                                    //AccountController.updateWalletCYN(UID, 0);
                                    //HistoryPayWalletCYNController.Insert(UID, username, walletCYN, 0, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
                                    //    currentDate, username);

                                    //AccountController.updateWallet(UID, walletleft, currentDate, username);

                                    //HistoryPayWalletController.Insert(UID, username, 0, totalpricevndpay,
                                    //    username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);

                                    //PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                    #endregion

                                    string content = username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".";
                                    int st = TransactionController.PaymentPayhelp(id, UID, username, walletleft, 0, totalpricevndpay, content, 1, 9, 1, 1, walletCYN, 1, currentDate);
                                    if (st == 1)
                                    {
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
                                                        PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán hộ đơn: " + id + ".", datalink);
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
                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại.", "e", true, Page);

                                    }
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
                                #region Old
                                //AccountController.updateWallet(UID, walletleft, currentDate, username);
                                //HistoryPayWalletController.Insert(UID, username, 0, TotalPriceVND, username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);
                                //PayhelpController.UpdateStatus(id, 1, currentDate, username);
                                #endregion

                                int st = TransactionController.PayhelpWallet(id, 1, UID, username, walletleft, TotalPriceVND, username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", 1, 9, currentDate);
                                if (st == 1)
                                {
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
                                                    PJUtils.PushNotiDesktop(a.ID, username + " đã trả tiền thanh toán hộ đơn: " + id + ".", datalink);
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
                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại.", "e", true, Page);
                                }
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
    }
}
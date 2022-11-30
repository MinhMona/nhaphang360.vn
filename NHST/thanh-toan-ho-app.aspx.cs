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
    public partial class thanh_toan_ho_app : System.Web.UI.Page
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
            int UID = Convert.ToInt32(Request.QueryString["UID"]);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    ViewState["Key"] = Key;
                    pnMobile.Visible = true;
                    string fr = Request.QueryString["fr"];
                    if (Request.QueryString["fr"] != null)
                        txtCYNfrom.Text = fr;
                    string to = Request.QueryString["to"];


                    if (Request.QueryString["to"] != null)
                        txtCYNto.Text = to;


                    int rmd = 1;
                    int status = Request.QueryString["stt"].ToInt(-1);
                    if (Request.QueryString["stt"] != null)
                        ddlStatus.SelectedValue = status.ToString();

                    if (status == 5)
                    {
                        rmd = 0;
                    }

                    var pay = PayhelpController.GetAllUID(UID);
                    if (pay.Count > 0)
                    {
                        if (status == -1)
                        {
                            if (!string.IsNullOrEmpty(fr) && !string.IsNullOrEmpty(to))
                            {
                                pay = pay.Where(x => Convert.ToDouble(x.TotalPrice) >= Convert.ToDouble(fr) && Convert.ToDouble(x.TotalPrice) <= Convert.ToDouble(to)).ToList();
                            }
                        }
                        else if (status == 5)
                        {
                            if (!string.IsNullOrEmpty(fr) && !string.IsNullOrEmpty(to))
                            {
                                pay = pay.Where(x => Convert.ToDouble(x.TotalPrice) >= Convert.ToDouble(fr) && Convert.ToDouble(x.TotalPrice) <= Convert.ToDouble(to) && x.IsNotComplete == true).ToList();
                            }
                            else
                            {
                                pay = pay.Where(x => x.IsNotComplete == true).ToList();
                            }
                        }
                        else if (status == 0)
                        {
                            if (!string.IsNullOrEmpty(fr) && !string.IsNullOrEmpty(to))
                            {
                                pay = pay.Where(x => Convert.ToDouble(x.TotalPrice) >= Convert.ToDouble(fr) && Convert.ToDouble(x.TotalPrice) <= Convert.ToDouble(to) && x.Status == status && x.IsNotComplete == false).ToList();
                            }
                            else
                            {
                                pay = pay.Where(x => x.Status == status && x.IsNotComplete == false).ToList();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(fr) && !string.IsNullOrEmpty(to))
                            {
                                pay = pay.Where(x => Convert.ToDouble(x.TotalPrice) >= Convert.ToDouble(fr) && Convert.ToDouble(x.TotalPrice) <= Convert.ToDouble(to) && x.Status == status).ToList();
                            }
                            else
                            {
                                pay = pay.Where(x => x.Status == status).ToList();
                            }
                        }

                        pagingall(pay);
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }

        #region Paging
        public void pagingall(List<tbl_PayHelp> acs)
        {
            int PageSize = 10;
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

                    html.Append(" <div class=\"thanhtoanho-list\">");
                    html.Append(" <div class=\"all\">");
                    html.Append(" <div class=\"order-group offset15\">");
                    html.Append("  <div class=\"heading\">");
                    html.Append(" <p class=\"left-lb\">");
                    html.Append("<span class=\"circle-icon\"><img src=\"/App_Themes/App/images/icon-store.png\" style=\"height:12px\" alt=\"\"></span>");
                    html.Append("     ID: " + item.ID + "");
                    html.Append("  </p>");
                    html.Append("  <p class=\"right-meta\">Ngày gửi: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</span></p>");
                    html.Append(" </div>");
                    html.Append("  <div class=\"smr\">");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Tổng tiền(¥):</p>");
                    html.Append("   <p>" + string.Format("{0:N0}", item.TotalPrice).Replace(",", ".") + "</p>");
                    html.Append("  </div>");
                    html.Append("  <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tổng tiền(vnđ):</p>");
                    html.Append("  <p>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)).Replace(",", ".") + "</p>");
                    html.Append("  </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tỉ giá(vnđ):</p>");
                    html.Append("  <p>" + string.Format("{0:N0}", Convert.ToDouble(item.Currency)).Replace(",", ".") + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Trạng thái:</p>");

                    bool isNotComplete = false;
                    if (item.IsNotComplete != null)
                        isNotComplete = Convert.ToBoolean(item.IsNotComplete);
                    if (isNotComplete == true)
                    {
                        html.Append("     <p><span class='bg-red'>Đang hoàn thiện</span></p>");
                    }
                    else
                    {
                        html.Append("   <p>" + PJUtils.ReturnStatusPayHelp(Convert.ToInt32(item.Status)) + "</p>");
                    }

                    //html.Append("     <p class=\"xanhla-txt\">Đã hoàn thành<i class=\"fa fa-check-circle\"></i></p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Ghi chú:</p>");
                    html.Append("    <p class=\"\">" + item.Note + "</p>");
                    html.Append("  </div>");
                    html.Append("  <div class=\"collapse-wrap\">");
                    html.Append("    <div class=\"flex-justify-space\">");
                    html.Append("  <p class=\"gray-txt\">Chi tiết:</p>");
                    html.Append(" <p class=\"xanhreu-txt\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-angle-down'></i>\" data-hide=\"Xem thêm <i class='fa fa-angle-up'></i>\" href=\"#chitiettb\">Xem thêm <i class='fa fa-angle-up'></i></a></p>");

                    html.Append("  </div>");
                    html.Append("   <div style =\"display:none;\" class=\"collapse-content\">");


                    var dt = PayhelpDetailController.GetByPayhelpID(item.ID);
                    if (dt.Count() > 0)
                    {
                        foreach (var temp in dt)
                        {
                            html.Append("    <table class=\"tb-wlb\">");
                            html.Append(" <tr>");
                            html.Append("   <td class=\"lb\">Giá tiền</td>");
                            html.Append("   <td>" + temp.Desc2 + "</td>");
                            html.Append("  </tr>");
                            html.Append("  <tr>");
                            html.Append("   <td class=\"lb\">Nội dung</td>");
                            html.Append("    <td>" + temp.Desc1 + "</td>");
                            html.Append("   </tr>");
                            html.Append("  </table>");
                        }
                    }

                    html.Append("   </div>");
                    html.Append(" </div>");


                    html.Append("  </div>");

                    if (item.Status == 0)
                    {
                        html.Append(" <div class=\"couple-btn\">");
                        html.Append("<a onclick=\"paymoney($(this),'" + item.ID + "')\" class=\"btn\">thanh toán</a>");
                        html.Append(" <a onclick=\"deleteTrade('" + item.ID + "')\" class=\"btn\">Huỷ đơn hàng</a>");
                        html.Append(" </div>");
                    }


                    html.Append(" </div>");

                    html.Append(" </div>");
                    html.Append("</div>");
                }
                ltrTotal.Text = html.ToString();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            DateTime currentDate = DateTime.Now;

            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                int ID = hdfTradeID.Value.ToInt(0);
                var p = PayhelpController.GetByIDAndUID(ID, UID);
                if (p != null)
                {
                    PayhelpController.UpdateStatus(ID, 2, DateTime.Now, u.Username);
                    PJUtils.ShowMessageBoxSwAlert("Hủy yêu cầu thành công", "s", true, Page);
                }
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            var id = hdfTradeID.Value.ToInt(0);
            if (id > 0)
            {
                int UID = Convert.ToInt32(ViewState["UID"]);

                DateTime currentDate = DateTime.Now;
                var u = AccountController.GetByID(UID);
                if (u != null)
                {
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
                                HistoryPayWalletCYNController.Insert(UID, u.Username, TotalPrice, walletCYN_left, 1, 1, u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
                                    currentDate, u.Username);

                                PayhelpController.UpdateStatus(id, 1, currentDate, u.Username);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var adminlist = AccountController.GetAllByRoleID(0);
                                        if (adminlist.Count > 0)
                                        {
                                            foreach (var a in adminlist)
                                            {
                                                NotificationsController.Inser(a.ID, a.Username, id, u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
        8, currentDate, u.Username, false);
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
                                                        "Thông báo tại NHẬP HÀNG 360.", u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
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
                                    HistoryPayWalletCYNController.Insert(UID, u.Username, walletCYN, 0, 1, 1, u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
                                        currentDate, u.Username);

                                    //double totalpricevndpay = walletCYN_left * Currency;
                                    double walletleft = wallet - totalpricevndpay;
                                    AccountController.updateWallet(UID, walletleft, currentDate, u.Username);

                                    HistoryPayWalletController.Insert(UID, u.Username, 0, totalpricevndpay,
                                        u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", walletleft, 1, 9, currentDate, u.Username);

                                    PayhelpController.UpdateStatus(id, 1, currentDate, u.Username);
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiAdmin == true)
                                        {
                                            var adminlist = AccountController.GetAllByRoleID(0);
                                            if (adminlist.Count > 0)
                                            {
                                                foreach (var a in adminlist)
                                                {
                                                    NotificationsController.Inser(a.ID, a.Username, id, u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
   8, currentDate, u.Username, false);
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
                                                            "Thông báo tại NHẬP HÀNG 360.", u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
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
                                AccountController.updateWallet(UID, walletleft, currentDate, u.Username);

                                HistoryPayWalletController.Insert(UID, u.Username, 0, TotalPriceVND,
                                    u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", walletleft, 1, 9, currentDate, u.Username);

                                PayhelpController.UpdateStatus(id, 1, currentDate, u.Username);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var adminlist = AccountController.GetAllByRoleID(0);
                                        if (adminlist.Count > 0)
                                        {
                                            foreach (var a in adminlist)
                                            {
                                                NotificationsController.Inser(a.ID, a.Username, id, u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".",
       8, currentDate, u.Username, false);
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
                                                        "Thông báo tại NHẬP HÀNG 360.", u.Username + " đã trả tiền thanh toán tiền hộ đơn: " + id + ".", "");
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

        protected void btnSear_Click(object sender, EventArgs e)
        {
            int UID = ViewState["UID"].ToString().ToInt(0);
            string CYNfrom = txtCYNfrom.Text;
            string CYNto = txtCYNto.Text;
            string status = ddlStatus.SelectedValue;
            string Key = ViewState["Key"].ToString();
            Response.Redirect("/thanh-toan-ho-app.aspx?UID=" + UID + "&fr=" + CYNfrom + "&to=" + CYNto + "&stt=" + status + "&Key=" + Key + "");
        }
    }
}
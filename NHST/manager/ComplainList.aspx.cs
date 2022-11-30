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
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class ComplainList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 3)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";
            if (!string.IsNullOrEmpty(rdatefrom.Text))
            {
                fd = rdatefrom.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rdateto.Text))
            {
                td = rdateto.Text.ToString();
            }

            if (!string.IsNullOrEmpty(searchname))
            {
                Response.Redirect("ComplainList?s=" + searchname + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
            }
            else
            {
                Response.Redirect("ComplainList?stt=" + status + "&fd=" + fd + "&td=" + td + "");
            }
        }
        private void LoadData()
        {
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }

            int status = Request.QueryString["stt"].ToInt(-1);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            ddlStatus.SelectedValue = status.ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
            {
                rdatefrom.Text = fd;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                rdateto.Text = td;

            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            var total = ComplainController.GetTotal(search, fd, td, status);
            var la = ComplainController.GetAllBySQL(search, page, 20, fd, td, status);
            pagingall(la, total);
        }
        [WebMethod]
        public static string loadinfoComplain(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var com = ComplainController.GetByID(ID.ToInt(0));
            if (com != null)
            {
                ComplainListShow l = new ComplainListShow();
                if (com != null)
                {

                    var ordershop = MainOrderController.GetAllByID(Convert.ToInt32(com.OrderID));
                    if (ordershop != null)
                    {
                        l.TiGia = string.Format("{0:N0}", Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                        l.AmountCNY = string.Format("{0:N0}", Convert.ToDouble(com.Amount) / Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                        l.UserName = com.CreatedBy;
                        l.ShopID = com.OrderID.ToString();
                        l.AmountVND = Convert.ToDouble(com.Amount).ToString();
                        l.ComplainText = com.ComplainText;
                        l.Status = com.Status.ToString();
                        //ddlStatus.SelectedValue = com.Status.ToString();

                        if (!string.IsNullOrEmpty(com.IMG))
                        {
                            var b = com.IMG.Split('|').ToList();
                            l.ListIMG = b;
                        }
                    }
                }
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }
        public partial class ComplainListShow
        {
            public string TiGia { get; set; }
            public string UserName { get; set; }
            public string AmountCNY { get; set; }
            public string AmountVND { get; set; }
            public string ComplainText { get; set; }
            public string Status { get; set; }
            public string UrlIMG { get; set; }
            public List<string> ListIMG { get; set; }
            public string ShopID { get; set; }

        }

        #region Pagging
        public void pagingall(List<tbl_Complain> acs, int total)
        {
            int PageSize = 20;
            if (total > 0)
            {
                int TotalItems = total;
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
                StringBuilder hcm = new StringBuilder();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    string statusString = "";
                    if (item.Status == 0)
                        statusString = "<span class=\"badge red darken-2 white-text border-radius-2\">Đã hủy</span></td>";
                    if (item.Status == 1)
                        statusString = "<span class=\"badge orange  darken-2 white-text border-radius-2\">Chờ duyệt</span></td>";
                    if (item.Status == 2)
                        statusString = "<span class=\"badge yellow darken-2 white-text border-radius-2\">Đang xử lý</span></td>";
                    if (item.Status == 3)
                        statusString = "<span class=\"badge green darken-2 white-text border-radius-2\">Đã xử lý</span></td>";
                 
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.CreatedBy + "</td>");
                    hcm.Append("<td>" + item.OrderID + "</td>");
                    hcm.Append("<td>" + item.Amount + "</td>");
                    hcm.Append("<td>" + item.ComplainText + "</td>");
                    hcm.Append("<td>" + statusString + "</td>");
                    hcm.Append("<td>" + item.CreatedDate.ToString().Remove(item.CreatedDate.ToString().Length - 6, 6) + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href=\"javascript:;\" onclick=\"Complain(" + item.ID + ")\" class=\"edit-mode\" data-position=\"top\"> ");
                    hcm.Append(" <i class=\"material-icons\">remove_red_eye</i><span>Xem</span></a>");
                    hcm.Append("<a href =\"OrderDetail.aspx?id=" + item.OrderID + "\" target=\"_blank\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Chi tiết đơn</span></a>");
                    hcm.Append("</div>");
                    hcm.Append("</td>");
                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
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
                if (pageUrl.IndexOf("Page=") > 0)
                {
                    int a = pageUrl.IndexOf("Page=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&Page={0}";
                }
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = hdfID.Value.ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/ComplainList.aspx";
            if (ID > 0)
            {
                var com = ComplainController.GetByID(ID);
                if (com != null)
                {
                    var setNoti = SendNotiEmailController.GetByID(10);
                    int status = lbStatus.SelectedValue.ToInt();

                    ComplainController.Update(com.ID, txtAmountVND.Text.ToString(), status, DateTime.Now, username_current);
                    if (status == 3)
                    {
                        string uReceive = hdfUserName.Value.Trim().ToLower();
                        var u = AccountController.GetByUsername(uReceive);
                        if (u != null)
                        {
                            int UID = u.ID;
                            double wallet = Convert.ToDouble(u.Wallet);
                            wallet = wallet + Convert.ToDouble(txtAmountVND.Text);

                            AccountController.updateWallet(u.ID, wallet, currentDate, username_current);
                            HistoryPayWalletController.Insert(u.ID, u.Username, Convert.ToInt32(com.OrderID), Convert.ToDouble(txtAmountVND.Text),
                                u.Username + " đã được hoàn tiền khiếu nại của đơn hàng: " + com.OrderID + " vào tài khoản.",
                                wallet, 2, 7, currentDate, username_current);
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(Convert.ToInt32(u.ID),
                               AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                               "Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.",
                               5, currentDate, ac.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "khieu-nai/";
                                    PJUtils.PushNotiDesktop(u.ID, "Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new( u.Email,
                                            "Thông báo tại NHẬP HÀNG 360.",
                                            "Admin đã duyệt khiếu nại đơn hàng: "
                                            + com.OrderID + "  của bạn.", "");
                                    }
                                    catch { }
                                }
                            }

                            //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                            //    AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                            //    "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                            //    5, currentDate, ac.Username);
                        }
                    }
                    else if (status == 4)
                    {
                        string uReceive = hdfUserName.Value.Trim().ToLower();
                        var u = AccountController.GetByUsername(uReceive);
                        if (u != null)
                        {
                            int UID = u.ID;
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(Convert.ToInt32(u.ID),
                            AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID), "Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", 5, currentDate, ac.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "khieu-nai/";
                                    PJUtils.PushNotiDesktop(u.ID, "Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new(
                                            u.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Admin đã hủy khiếu nại đơn hàng: "
                                            + com.OrderID + "  của bạn.", "");
                                    }
                                    catch { }
                                }
                            }
                            //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                            //   AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                            //   "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                            //   5, currentDate, ac.Username);
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/ComplainList.aspx";
            if (ID > 0)
            {
                var com = ComplainController.GetByID(ID);
                if (com != null)
                {
                    var setNoti = SendNotiEmailController.GetByID(10);
                    int status = lbStatus.SelectedValue.ToInt();

                    ComplainController.Update(com.ID, txtAmountVND.Text.ToString(), status, DateTime.Now, username_current);
                    if (status == 3)
                    {
                        string uReceive = hdfUserName.Value.Trim().ToLower();
                        var u = AccountController.GetByUsername(uReceive);
                        if (u != null)
                        {
                            int UID = u.ID;
                            double wallet = Convert.ToDouble(u.Wallet);
                            wallet = wallet + Convert.ToDouble(txtAmountVND.Text);

                            AccountController.updateWallet(u.ID, wallet, currentDate, username_current);
                            HistoryPayWalletController.Insert(u.ID, u.Username, Convert.ToInt32(com.OrderID), Convert.ToDouble(txtAmountVND.Text),
                                u.Username + " đã được hoàn tiền khiếu nại của đơn hàng: " + com.OrderID + " vào tài khoản.",
                                wallet, 2, 7, currentDate, username_current);
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(Convert.ToInt32(u.ID),
                               AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                               "Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.",
                               5, currentDate, ac.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "khieu-nai/";
                                    PJUtils.PushNotiDesktop(u.ID, "Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new( u.Email,
                                            "Thông báo tại NHẬP HÀNG 360.",
                                            "Admin đã duyệt khiếu nại đơn hàng: "
                                            + com.OrderID + "  của bạn.", "");
                                    }
                                    catch { }
                                }
                            }

                            //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                            //    AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                            //    "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                            //    5, currentDate, ac.Username);
                        }
                    }
                    else if (status == 4)
                    {
                        string uReceive = hdfUserName.Value.Trim().ToLower();
                        var u = AccountController.GetByUsername(uReceive);
                        if (u != null)
                        {
                            int UID = u.ID;
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(Convert.ToInt32(u.ID),
                            AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID), "Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.",
                            5, currentDate, ac.Username, false);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "khieu-nai/";
                                    PJUtils.PushNotiDesktop(u.ID, "Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new(
                                            u.Email,
                                            "Thông báo tại NHẬP HÀNG 360.", "Admin đã hủy khiếu nại đơn hàng: "
                                            + com.OrderID + "  của bạn.", "");
                                    }
                                    catch { }
                                }
                            }
                            //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                            //   AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                            //   "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                            //   5, currentDate, ac.Username);
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                }
            }
        }
    }
}
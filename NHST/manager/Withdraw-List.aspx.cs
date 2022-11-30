using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;
using MB.Extensions;
using System.Text.RegularExpressions;

namespace NHST.manager
{
    public partial class Withdraw_List : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {

            var user = AccountController.GetAll_RoleID("");
            if (user.Count > 0)
            {
                ddlUsername.DataSource = user;
                ddlUsername.DataBind();
            }
            int index = 0;
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                var a = AccountController.GetByUsername(search);
                if (a != null)
                {
                    if (user.Count > 0)
                    {
                        index = user.FindIndex(n => n.ID == a.ID);
                    }
                }

            }
            ddlUsername.SelectedIndex = index;
            ddlUsername.DataBind();

            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
           
            var la = WithdrawController.GetBySQL_DK(search, "", page, 10);
            int total = WithdrawController.GetTotalSQL(search, "");
            pagingall(la, total);
        }


        #region Webservice
        [WebMethod]
        public static string GetData(int ID)
        {
            var nap = WithdrawController.GetByID(ID);
            if (nap != null)
            {
                NaptienInfo n = new NaptienInfo();
                int UID = Convert.ToInt32(nap.UID);
                double Amount = Convert.ToDouble(nap.Amount);
                var ai = AccountInfoController.GetByUserID(UID);
                if (ai != null)
                {
                    n.FullName = ai.FirstName + " " + ai.LastName;
                    n.Address = ai.Address;
                }
                n.Money = string.Format("{0:N0}", Amount);
                if (!string.IsNullOrEmpty(nap.Note))
                    n.Note = nap.Note;
                DateTime currentDate = DateTime.Now;
                string CreateDate = "Ngày " + currentDate.Day + " tháng " + currentDate.Month + " năm " + currentDate.Year;
                n.CreateDate = CreateDate;
                n.Status = nap.Status.Value;
                n.Amount = nap.Amount.Value;
                n.Username = nap.Username;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(n);
            }
            return "null";
        }
        public class NaptienInfo
        {
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Money { get; set; }
            public string Note { get; set; }
            public string CreateDate { get; set; }
            public int Status { get; set; }
            public string Username { get; set; }
            public double Amount { get; set; }
        }
        #endregion

        #region Pagging
        public void pagingall(List<WithdrawController.ListWithdrawNew> acs, int total)
        {
            int PageSize = 10;
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
                    int status = Convert.ToInt32(item.Status);
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + item.Beneficiary + "</td>");
                    hcm.Append("<td>" + item.BankNumber + "</td>");
                    hcm.Append("<td>" + item.BankAddress + "</td>");
                    hcm.Append("<td>" + item.AmountString + "</td>");
                    hcm.Append("<td>" + item.StatusName + "</td>");
                    hcm.Append("<td>" + item.CreatedDateString + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href=\"javascript:;\" onclick=\"EditFunction(" + item.ID + ")\" class=\"edit-mode\" data-position=\"top\">");
                    hcm.Append("<i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                    hcm.Append("<a href=\"javascript:;\" onclick=\"printPhieuchi(" + item.ID + ")\" data-position=\"top\">");
                    hcm.Append("<i class=\"material-icons\">print</i><span>In phiếu chi</span></a>");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var a = AccountController.GetByID(Convert.ToInt32(ddlUsername.Text));
            string searchname = a.Username;
            if (!string.IsNullOrEmpty(searchname))
            {
                Response.Redirect("Withdraw-List?s=" + searchname);
            }
            else
            {
                Response.Redirect("Withdraw-List");
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int id = hdfIDWR.Value.ToInt(0);
            var setNoti = SendNotiEmailController.GetByID(4);
            var w = WithdrawController.GetByID(id);
            string BackLink = "/manager/Withdraw-List.aspx";
            if (w != null)
            {
                if (w.Status < 3)
                {
                    int status = ddlStatus.SelectedValue.ToString().ToInt(1);
                    if (status == 2)
                    {
                        var acc = AccountController.GetByUsername(username);
                        if (acc.RoleID == 7)
                        {
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Bạn không có quyền duyệt yêu cầu này", "e", true, BackLink, Page);
                        }
                        else if (acc.RoleID == 0)
                        {
                            int uid_rut = w.UID.ToString().ToInt();
                            var user_rut = AccountController.GetByID(uid_rut);
                            if (user_rut != null)
                            {
                                WithdrawController.UpdateStatus(id, status, currentDate, username);

                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiUser == true)
                                    {
                                        NotificationsController.Inser(user_rut.ID, user_rut.Username, 0, "Admin đã duyệt lệnh rút tiền của bạn.", 3, currentDate, username, true);
                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                        string datalink = "" + strUrl + "rut-tien/";
                                        PJUtils.PushNotiDesktop(user_rut.ID, "Admin đã duyệt lệnh rút tiền của bạn.", datalink);
                                    }
                                    var c = ConfigurationController.GetByTop1();
                                    if (setNoti.IsSendEmailUser == true)
                                    {
                                        try
                                        {

                                            StringBuilder html = new StringBuilder();
                                            html.Append("<!DOCTYPE html>");
                                            html.Append("<html lang=\"en\">");
                                            html.Append("<head>");
                                            html.Append("   <meta charset=\"UTF-8\">");
                                            html.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                                            html.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">");
                                            html.Append("<title>Document</title>");
                                            html.Append("</head>");
                                            html.Append("<body style=\"margin: 0; padding:0\">");
                                            html.Append("<table style=\"font-family: sans-serif; font-size: 14px; border-collapse: collapse; width: 500px; max-width: 100%; margin: auto\">");
                                            html.Append("<tr>");
                                            html.Append("<td style=\"padding: 10px; background-color: #fca777; color: #fff; text-align: center\"><strong><p>KÍNH CHÀO QUÝ KHÁCH!</p><p><a style=\"text-decoration: none\" href=\"https://nhaphang360.vn/\" target=\"_blank\"><strong>NHẬP HÀNG 360<strong></a> THÔNG BÁO RÚT VÍ THÀNH CÔNG</p></strong></td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>THÔNG TIN GIAO DỊCH</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Tên KH: " + AccountInfoController.GetByUserID(user_rut.ID).FirstName + " " + AccountInfoController.GetByUserID(user_rut.ID).LastName + " </td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>SĐT:  " + AccountInfoController.GetByUserID(user_rut.ID).Phone + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Username: " + user_rut.Username + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Số tiền rút ví: " + string.Format("{0:N0}", w.Amount) + " VNĐ</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Số dư hiện tại: " + string.Format("{0:N0}", user_rut.Wallet) + " VNĐ</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>ID rút ví: " + id + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Thời gian: " + AdminSendUserWalletController.GetByUID_New(user_rut.ID).CreatedDate + "</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>Quý khách vui lòng truy cập tài khoản để kiểm tra chi tiết.</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td><a style=\"text-decoration: none\" href=\"https://nhaphang360.vn/\" target=\"_blank\"><strong>NHẬP HÀNG 360<strong></a> chân thành cảm ơn!</td>");
                                            html.Append("</tr>");
                                            html.Append("<tr>");
                                            html.Append("<td>==============================</td>");
                                            html.Append("<tr>");
                                            html.Append("<td>Mọi thắc mắc xin vui lòng liên hệ: <a href=\"tel:" + c.Hotline + "\">" + c.Hotline + "</a></td>");
                                            html.Append("</tr>");
                                            html.Append("</tr>");
                                            html.Append("</table>");
                                            html.Append("</body>");
                                            html.Append("</html>");


                                            PJUtils.SendMailGmail_new( user_rut.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", html.ToString(), "");
                                        }
                                        catch { }
                                    }
                                }

                                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                            }

                        }
                    }
                    else if (status == 3)
                    {
                        int uid_rut = w.UID.ToString().ToInt();
                        var user_rut = AccountController.GetByID(uid_rut);
                        if (user_rut != null)
                        {
                            double wallet = Convert.ToDouble(user_rut.Wallet.ToString());
                            double amount = Convert.ToDouble(w.Amount.ToString());

                            double newwallet = wallet + amount;

                            //Cập nhật lại ví
                            AccountController.updateWallet(uid_rut, newwallet, currentDate, username);

                            //Thêm vào History Pay wallet
                            HistoryPayWalletController.Insert(uid_rut, username, 0, amount, "Admin Hủy lệnh Rút tiền", newwallet, 2, 6, currentDate, username);

                            //Thêm vào lệnh rút tiền
                            WithdrawController.UpdateStatus(id, 3, currentDate, username);

                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    NotificationsController.Inser(user_rut.ID, user_rut.Username, 0, "Admin đã hủy lệnh rút tiền của bạn.", 3, currentDate, username, true);
                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                    string datalink = "" + strUrl + "rut-tien/";
                                    PJUtils.PushNotiDesktop(user_rut.ID, "Admin đã hủy lệnh rút tiền của bạn.", datalink);
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new( user_rut.Email, "Thông báo tại NHẬP HÀNG 360.", "Admin đã hủy lệnh rút tiền của bạn.", "");
                                    }
                                    catch { }
                                }
                            }
                        }
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                    else
                    {
                        WithdrawController.UpdateStatus(id, status, currentDate, username);
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                    }
                }
            }
        }
    }
}
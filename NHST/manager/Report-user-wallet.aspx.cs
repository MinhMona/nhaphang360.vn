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
using MB.Extensions;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace NHST.manager
{
    public partial class Report_user_wallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
            }
        }
        public void LoadData()
        {
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            string st = "0";
            if (!string.IsNullOrEmpty(Request.QueryString["st"]))
            {
                st = Request.QueryString["st"].ToString();
                ddlFilter.SelectedValue = st;
            }
            if (st == "0")
            {
                var la = AccountController.GetAll_View_ButBySQL("", "", 10, page);
                var total = AccountController.GetTotalBySQL("", "");
                double WalletAll = AccountController.GetTotalWalletBySQL("", "");
                lbTotalWallet.Text = string.Format("{0:N0}", WalletAll) + " VND";
                pagingall(la, total);
            }
            if (st == "1")
            {
                var la = AccountController.GetAll_View_ButBySQL("0.1", "", 10, page);
                var total = AccountController.GetTotalBySQL("0.1", "");
                double WalletAll = AccountController.GetTotalWalletBySQL("0.1", "");
                lbTotalWallet.Text = string.Format("{0:N0}", WalletAll) + " VND";
                pagingall(la, total);
            }
            if (st == "2")
            {
                var la = AccountController.GetAll_View_ButBySQL("0", "0", 10, page);
                var total = AccountController.GetTotalBySQL("0", "0");
                double WalletAll = AccountController.GetTotalWalletBySQL("0", "0");
                lbTotalWallet.Text = string.Format("{0:N0}", WalletAll) + " VND";
                pagingall(la, total);
            }
            int Va1 = AccountController.GetTotalBySQL("0.1", "");
            int Va2 = AccountController.GetTotalBySQL("0", "0");
            int Va3 = AccountController.GetTotalBySQL("1000000", "5000000");
            int Va4 = AccountController.GetTotalBySQL("5000000", "10000000");
            int Va5 = AccountController.GetTotalBySQL("10000000", "");

            int[] data2 = new int[] { Va1, Va2, Va3, Va4, Va5 };
            string[] backgroundColor2 = new string[] { "rgba(211, 47, 47, 0.5)", "rgba(25, 118, 210,.5)", "rgba(245, 124, 0,.5)", "rgba(95,186,125,0.9)", "rgb(224, 134, 255)" };
            string datasetsTotal = new JavaScriptSerializer().Serialize(new
            {
                label = "Số dư",
                data = data2,
                backgroundColor = backgroundColor2,
            });
            string dataChartTotal = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               labels = "[\"Lớn hơn 0\", \"Bằng 0\", \"1 triệu - 5 triệu\", \"5 triệu - 10 triệu\",\"Lớn hơn 10 triệu\"]",
                                               datasets = "[" + datasetsTotal + "]"
                                           });

            hdfDataChart.Value = dataChartTotal;

        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            int filter = ddlFilter.SelectedValue.ToInt(0);

            if (filter != 0)
            {
                Response.Redirect("Report-user-wallet.aspx?st=" + filter);
            }
            else
            {
                Response.Redirect("Report-user-wallet.aspx");
            }

            //ltrinf.Text = "";
            //ltrinf.Text += "<div class=\"row\">";
            //ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
            //ltrinf.Text += "<span class=\"label-title\">Tổng số dư của toàn bộ khách hàng</span>";
            //ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalsodu) + " VNĐ</span>";
            //ltrinf.Text += "</div>";
            //ltrinf.Text += "</div>";   
            #endregion

        }

        //public void LoadGrid()
        //{
        //    var acc = Session["userLoginSystem"].ToString();
        //    #region Thống kê thanh toán
        //    int filter = ddlFilter.SelectedValue.ToInt(0);
        //    double totalsodu = 0;
        //    if (filter == 0)
        //    {
        //        var la = AccountController.GetAll_View("");
        //        if (la.Count > 0)
        //        {
        //            List<UserToExcel> us = new List<UserToExcel>();
        //            foreach (var item in la)
        //            {
        //                string username = item.Username;
        //                int UID = item.ID;
        //                UserToExcel u = new UserToExcel();
        //                u.ID = item.ID;
        //                u.UserName = item.Username;
        //                u.Ho = item.FirstName;
        //                u.Ten = item.LastName;
        //                u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
        //                u.Status = PJUtils.StatusToRequest(item.Status);
        //                u.Role = item.RoleName;
        //                u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
        //                u.RoleID = item.RoleID.ToString().ToInt(1);
        //                u.wallet = string.Format("{0:N0}", item.Wallet);
        //                u.Saler = item.saler;
        //                u.dathang = item.dathang;
        //                us.Add(u);
        //                totalsodu += Convert.ToDouble(u.wallet);
        //            }
        //            //gr.DataSource = us;
        //            //gr.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        var la = AccountController.GetAllWithWallet_View("");
        //        if (la.Count > 0)
        //        {
        //            List<UserToExcel> us = new List<UserToExcel>();
        //            foreach (var item in la)
        //            {
        //                string username = item.Username;
        //                int UID = item.ID;
        //                UserToExcel u = new UserToExcel();
        //                u.ID = item.ID;
        //                u.UserName = item.Username;
        //                u.Ho = item.FirstName;
        //                u.Ten = item.LastName;
        //                u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
        //                u.Status = PJUtils.StatusToRequest(item.Status);
        //                u.Role = item.RoleName;
        //                u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
        //                u.RoleID = item.RoleID.ToString().ToInt(1);
        //                u.wallet = string.Format("{0:N0}", item.Wallet);
        //                u.Saler = item.saler;
        //                u.dathang = item.dathang;
        //                us.Add(u);
        //                totalsodu += Convert.ToDouble(u.wallet);
        //            }

        //            //gr.DataSource = us;
        //            //gr.DataBind();
        //        }
        //    }
        //    //ltrinf.Text = "";
        //    //ltrinf.Text += "<div class=\"row\">";
        //    //ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //    //ltrinf.Text += "<span class=\"label-title\">Tổng số dư của toàn bộ khách hàng</span>";
        //    //ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalsodu) + " VNĐ</span>";
        //    //ltrinf.Text += "</div>";
        //    //ltrinf.Text += "</div>";
        //    #endregion
        //}
        public class UserToExcel
        {
            public int ID { get; set; }
            public string UserName { get; set; }
            public string Ho { get; set; }
            public string Ten { get; set; }
            public string Sodt { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public int RoleID { get; set; }
            public string Saler { get; set; }
            public string dathang { get; set; }
            public string wallet { get; set; }
            public string CreatedDate { get; set; }
        }
        #region Pagging
        public void pagingall(List<View_UserList> acs, int total)
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
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.Wallet) + " VND</td>");
                    hcm.Append("<td>" + item.RoleName + "</td>");
                    hcm.Append("<td>" + PJUtils.StatusToRequest(item.Status) + "</td>");
                    hcm.Append("<td>" + item.saler + "</td>");
                    hcm.Append("<td>" + item.dathang + "</td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");
                    hcm.Append("<td class=\"tb-date\">");
                    hcm.Append("    <div class=\"action-table\">");
                    hcm.Append("        <a href=\"UserInfo.aspx?i=" + item.ID + "\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i></a>");
                    if (item.RoleID == 1)
                    {
                        hcm.Append("        <a href=\"UserWallet.aspx?i=" + item.ID + "\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Nạp tiền\"><i class=\"material-icons\">monetization_on</i></a>");
                        hcm.Append("       <a href=\"User-Transaction.aspx?i=" + item.ID + "\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Xem lịch sử giao dịch\"><i class=\"material-icons\">view_list</i></a>");

                    }
                    hcm.Append("    </div>");
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
    }
}
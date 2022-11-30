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
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class Report_User_Use_Wallet : System.Web.UI.Page
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
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            if (!string.IsNullOrEmpty(fd))
                rdatefrom.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rdateto.Text = td;

            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            var la = HistoryPayWalletController.GetFromDateTodate_BySQL("", fd, td, 10, page);
            var total = HistoryPayWalletController.GetTotalAll_BySQL("", fd, td);
            if (la.Count > 0)
            {
                string lastDate = la[0].CreatedDate.Value.ToString("dd/MM/yyyy");
                string firstDate = la[la.Count - 1].CreatedDate.Value.ToString("dd/MM/yyyy");
                ltrDateToDate.Text = firstDate + "-" + lastDate;

                double TotalPrice = HistoryPayWalletController.GetTotalAllAmount_BySQL("", fd, td);
                lbTotalPrice.Text = string.Format("{0:N0}", TotalPrice);

                double Deposit = HistoryPayWalletController.GetTotalAllAmount_BySQL("1", fd, td);

                double ReciveDeposit = HistoryPayWalletController.GetTotalAllAmount_BySQL("2", fd, td);

                double PaymentBill = HistoryPayWalletController.GetTotalAllAmount_BySQL("3", fd, td);

                double AdminSend = HistoryPayWalletController.GetTotalAllAmount_BySQL("4", fd, td);

                double WithDraw = HistoryPayWalletController.GetTotalAllAmount_BySQL("5", fd, td);

                double CancelWithDraw = HistoryPayWalletController.GetTotalAllAmount_BySQL("6", fd, td);

                double Complain = HistoryPayWalletController.GetTotalAllAmount_BySQL("7", fd, td);

                double PaymentTransport = HistoryPayWalletController.GetTotalAllAmount_BySQL("8", fd, td);

                double PaymentHo = HistoryPayWalletController.GetTotalAllAmount_BySQL("9", fd, td);

                double PaymentSaveWare = HistoryPayWalletController.GetTotalAllAmount_BySQL("10", fd, td);

                double RecivePaymentTransport = HistoryPayWalletController.GetTotalAllAmount_BySQL("11", fd, td);

                pagingall(la, total);

                double[] data2 = new double[] { Deposit, ReciveDeposit, PaymentBill, AdminSend, WithDraw, CancelWithDraw, Complain, PaymentTransport, PaymentHo, PaymentSaveWare, RecivePaymentTransport };
                string[] backgroundColor2 = new string[] { "rgb(205, 97, 85)", "rgb(195, 155, 211)", "rgb(40, 116, 166)", "rgb(241, 148, 138)", "rgb(130, 224, 170)", "rgb(245, 176, 65)", "rgb(235, 152, 78)", "rgb(170, 183, 184)", "rgb(174, 214, 241)", "rgb(247, 220, 111)", "rgb(93, 109, 126)" };
                string datasetsTotal = new JavaScriptSerializer().Serialize(new
                {
                    label = "Loại giao dịch",
                    data = data2,
                    backgroundColor = backgroundColor2,
                    hoverBackgroundColor = "hover_gradient",
                    hoverBorderWidth = "2",
                    hoverBorderColor = "hover_gradient"
                });
                string dataChartTotal = new JavaScriptSerializer().Serialize(
                                               new
                                               {
                                                   labels = "[\"Đặt cọc\", \"Nhận lại đặt cọc\", \"Thanh toán hóa đơn\", \"Admin nạp tiền\",\"Rút tiền\",\"Hủy rút tiền\",\"Nhận tiền khiếu nại\",\"Thanh toán vận chuyển hộ\",\"Thanh toán hộ\",\"Thanh toán tiền lưu kho\",\"Nhận lại tiền vận chuyển hộ\"]",
                                                   datasets = "[" + datasetsTotal + "]"
                                               });

                hdfDataChart.Value = dataChartTotal;
            }


        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
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

            if (fd == "" && td == "")
            {
                Response.Redirect("Report-User-Use-Wallet.aspx");
            }
            else
            {
                Response.Redirect("Report-User-Use-Wallet.aspx?fd=" + fd + "&td=" + td);
            }
        }
        #region Pagging
        public void pagingall(List<tbl_HistoryPayWallet> acs, int total)
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
                    hcm.Append("<td>" + item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") + "</td>");
                    hcm.Append("<td>" + item.HContent + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.Amount) + " VND</td>");
                    hcm.Append("<td>" + PJUtils.GetTradeType(item.TradeType.Value) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.MoneyLeft) + " VND</td>");
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
            {
            }
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
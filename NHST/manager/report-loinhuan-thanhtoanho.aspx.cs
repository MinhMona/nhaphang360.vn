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

namespace NHST.manager
{
    public partial class report_loinhuan_thanhtoanho : System.Web.UI.Page
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
                txtdatefrom.Text = fd;
            if (!string.IsNullOrEmpty(td))
                txtdateto.Text = td;
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            var la = PayhelpController.GetAllByFromStatusFromdateToDateBySQL(3, fd, td, page, 10);
            var total = PayhelpController.GetTotalByStatusFromdateToDateBySQL(3, fd, td);
            if (la.Count > 0)
            {
                double TotalPriceCNY = PayhelpController.GetTotalPriceByStatusFromdateToDateBySQL(3, fd, td, "TotalPrice");
                double TotalPrice = PayhelpController.GetTotalPriceByStatusFromdateToDateBySQL(3, fd, td, "TotalPriceVND");
                double TotalRealPrice = PayhelpController.GetTotalPriceByStatusFromdateToDateBySQL(3, fd, td, "TotalPriceVNDGiagoc");
                double TotalInterRest = TotalPrice - TotalRealPrice;
                double[] data2 = new double[] { TotalPriceCNY, TotalRealPrice, TotalPrice, TotalInterRest };
                string[] backgroundColor2 = new string[] { "green_cyan_gradient", "red_orange_gradient", "orange_yellow_gradient", "orange_yellow_gradient" };
                string datasetsTotal = new JavaScriptSerializer().Serialize(new
                {
                    label = "Lợi nhuận mua hàng hộ",
                    data = data2,
                    backgroundColor = backgroundColor2,
                    hoverBackgroundColor = "hover_gradient",
                    hoverBorderWidth = 2,
                    hoverBorderColor = "hover_gradient"
                });
                string dataChartTotal = new JavaScriptSerializer().Serialize(
                                               new
                                               {
                                                   labels = "[\"Tổng tiền tệ\", \"Tổng tiền vốn\", \"Tổng tiền thu\",\"Tổng tiền lời\"]",
                                                   datasets = "[" + datasetsTotal + "]"
                                               });
                hdfDataChart.Value = dataChartTotal;
                pagingall(la, total);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string fd = "";
            string td = "";
            if (!string.IsNullOrEmpty(txtdatefrom.Text))
            {
                fd = txtdatefrom.Text.ToString();
            }
            if (!string.IsNullOrEmpty(txtdateto.Text))
            {
                td = txtdateto.Text.ToString();
            }

            if (fd == "" && td == "")
            {
                Response.Redirect("report-loinhuan-thanhtoanho.aspx.aspx");
            }
            else
            {
                Response.Redirect("report-loinhuan-thanhtoanho.aspx?&fd=" + fd + "&td=" + td);
            }
        }
        #region Pagging
        public void pagingall(List<tbl_PayHelp> acs, int total)
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
                    double totalInterest = Convert.ToDouble(item.TotalPriceVND) - Convert.ToDouble(item.TotalPriceVNDGiagoc);

                    var Uname = AccountController.GetByID(item.UID.Value).Username;
                    string UserName = "";
                    if (!string.IsNullOrEmpty(Uname))
                        UserName = Uname;

                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + UserName + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPrice)) + " VND</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVNDGiagoc)) + " VND</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VND</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", totalInterest) + " VND</td>");
                    hcm.Append("<td>" + item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") + "</td>");
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
        //public void LoadGrid()
        //{
        //    var acc = Session["userLoginSystem"].ToString();
        //    List<LoiNhuan> ls = new List<LoiNhuan>();
        //    var config = ConfigurationController.GetByTop1();
        //    double currencyIncome = Convert.ToDouble(config.CurrencyIncome);
        //    DateTime df = DateTime.ParseExact(txtdatefrom.Text, "dd/MM/yyyy", null);
        //    DateTime dt = DateTime.ParseExact(txtdateto.Text, "dd/MM/yyyy", null);
        //    var listpayhelp = PayhelpController.GetAllByFromStatusFromdateToDate(3, df, dt.AddHours(24));
        //    if (listpayhelp.Count > 0)
        //    {
        //        pninfo.Visible = true;
        //        int stt = 1;
        //        double tongthu = 0;
        //        double tongvon = 0;
        //        double tongloi = 0;
        //        foreach (var item in listpayhelp)
        //        {
        //            LoiNhuan l = new LoiNhuan();
        //            l.ID = item.ID;
        //            l.STT = stt;
        //            l.Username = item.Username;
        //            l.Amount = item.TotalPrice;

        //            double amount = Convert.ToDouble(item.TotalPrice);
        //            double tienthu = Convert.ToDouble(item.TotalPriceVND);
        //            double pricenvndgoc = 0;
        //            double pricethu = 0;
        //            double priceloi = 0;
        //            if (!string.IsNullOrEmpty(item.TotalPriceVNDGiagoc))
        //            {
        //                l.TotalPriceVNDGoc = string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVNDGiagoc)).Replace(",", "."); ;
        //                pricenvndgoc = Convert.ToDouble(item.TotalPriceVNDGiagoc);
        //            }
        //            else
        //            {
        //                pricenvndgoc = Convert.ToDouble(item.CurrencyGiagoc) * amount;
        //            }
        //            //pricenvndgoc = currencyIncome * amount;
        //            //pricenvndgoc = Convert.ToDouble(item.CurrencyGiagoc) * amount;
        //            l.TotalPriceVNDGoc = string.Format("{0:N0}", pricenvndgoc).Replace(",", "."); ;
        //            l.TotalPriceVNDThu = string.Format("{0:N0}", tienthu).Replace(",", "."); ;
        //            priceloi = tienthu - pricenvndgoc;
        //            l.TotalPriceVNDLoi = string.Format("{0:N0}", priceloi).Replace(",", ".");
        //            l.CreatedDate = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
        //            ls.Add(l);
        //            stt++;

        //            tongthu += tienthu;
        //            tongvon += pricenvndgoc;

        //        }
        //        tongloi = tongthu - tongvon;
        //        lblthu.Text = string.Format("{0:N0}", tongthu).Replace(",", ".");
        //        lblvon.Text = string.Format("{0:N0}", tongvon).Replace(",", ".");
        //        lblloi.Text = string.Format("{0:N0}", tongloi).Replace(",", ".");
        //    }
        //    gr.DataSource = ls;
        //}
        //protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    LoadGrid();
        //}

        //protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        //{

        //}

        //protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    LoadGrid();
        //}
        //protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        //{
        //    LoadGrid();
        //}
        public class LoiNhuan
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string Username { get; set; }
            public string Amount { get; set; }
            public string TotalPriceVNDGoc { get; set; }
            public string TotalPriceVNDThu { get; set; }
            public string TotalPriceVNDLoi { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}
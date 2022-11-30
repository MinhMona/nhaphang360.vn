using MB.Extensions;
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
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class danh_sach_thanh_toan_ho1 : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");

                    LoadData();
                }
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
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");

            if (Page > 0)
            {
                Session["Page"] = Page;
                page = Page - 1;
            }
            else
            {
                Session["Page"] = "";
            }
            int status1 = Request.QueryString["st"].ToInt(-1);
            ddlStatus.SelectedValue = status1.ToString();

            string fd = Request.QueryString["fd"];
            if (!string.IsNullOrEmpty(fd))
                rFD.Text = fd;
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(td))
                rTD.Text = td;

            int sort = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["sort"]))
            {
                sort = Convert.ToInt32(Request.QueryString["sort"]);
                ddlSortType.SelectedValue = sort.ToString();
            }

            int total = PayhelpController.GetTotalPage(search, status1, fd, td);
            var la = PayhelpController.GetByUserInSQLHelper_nottextnottypeWithstatus(page, 10, search, status1, fd, td, sort);
            pagingall(la, total);
        }

        private static string Check(bool check)
        {
            if (check)
                return "checked";
            else
                return null;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fd = "";
            string td = "";
            string status1 = "";
            string searchname = search_name.Text.Trim();
            int SortType = Convert.ToInt32(ddlSortType.SelectedValue);
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                status1 = ddlStatus.SelectedValue;
            }
            if (string.IsNullOrEmpty(searchname) == true && fd == "" && td == "" && status1 == "")
            {
                Response.Redirect("danh-sach-thanh-toan-ho?sort=" + SortType + "");
            }
            else
            {
                Response.Redirect("danh-sach-thanh-toan-ho?&s=" + searchname + "&fd=" + fd + "&sort=" + SortType + "&td=" + td + "&st=" + status1);
            }
        }
        #region Pagging
        public void pagingall(List<PayhelpController.Danhsachyeucau> acs, int total)
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
                    hcm.Append("<td>" + string.Format("{0:#.##}", Convert.ToDouble(item.TotalPriceCYN)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.TotalPriceVND) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.Currency) + "</td>");
                    hcm.Append("<td><label><input type=\"checkbox\" " + Check(Convert.ToBoolean(item.IsNotComplete)) + " disabled =\"\" ><span ></span ></label></td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");
                    hcm.Append("<td>" + item.statusstring + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href = \"chi-tiet-thanh-toan-ho.aspx?ID=" + item.ID + "\" target=\"_blank\" data-position=\"top\"> ");
                    hcm.Append(" <i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                    //if (item.Status == 0)
                    //{
                    //    hcm.Append("<a href =\"1\" class=\"tooltipped\" data-position=\"top\"");
                    //    hcm.Append(" data-tooltip=\"Thanh toán ngay\"><i class=\"material-icons\">payment</i></a>");
                    //}              
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
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                //var orders = PayhelpController.GetAll();
                //List<Danhsachyeucau> ds = new List<Danhsachyeucau>();
                //if (orders.Count > 0)
                //{
                //    foreach (var o in orders)
                //    {
                //        Danhsachyeucau d = new Danhsachyeucau();
                //        d.ID = o.ID;
                //        d.Username = o.Username;
                //        d.Phone = o.Phone;
                //        d.TotalPriceCYN = Convert.ToDouble(o.TotalPrice);
                //        d.TotalPriceCYN_String = string.Format("{0:N0}", Convert.ToDouble(o.TotalPrice)).Replace(",", ".");
                //        d.TotalPriceVND = Convert.ToDouble(o.TotalPriceVND);
                //        if (o.IsNotComplete != null)
                //            d.IsNotComplete = Convert.ToBoolean(o.IsNotComplete);
                //        else
                //            d.IsNotComplete = false;
                //        d.TotalPriceVND_String = string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)).Replace(",", ".");
                //        d.Currency = Convert.ToDouble(o.Currency);
                //        string stt = PJUtils.ReturnStatusPayHelp(Convert.ToInt32(o.Status));
                //        d.statusstring = stt;
                //        d.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", o.CreatedDate);
                //        ds.Add(d);
                //    }
                //}
                //gr.DataSource = ds;

                //int totalRow = PayhelpController.GetAll().Count();
                //int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
                //gr.VirtualItemCount = totalRow;
                //int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
                //var Order = PayhelpController.GetByUserInSQLHelper_nottextnottypeWithstatus(Page, maximumRows);
                //gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
                //gr.DataSource = Order;


            }
        }


        #endregion

        #region button event


        #endregion
        public class Danhsachyeucau
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public double TotalPriceCYN { get; set; }
            public string TotalPriceCYN_String { get; set; }
            public double TotalPriceVND { get; set; }
            public double Currency { get; set; }
            public object IsNotComplete { get; set; }
            public string TotalPriceVND_String { get; set; }
            public string statusstring { get; set; }
            public string CreatedDate { get; set; }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
            if (searchname != "")
            {
                var la1 = PayhelpController.GetAll().Where(w => w.Username == searchname);
                StringBuilder StrExport1 = new StringBuilder();
                StrExport1.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport1.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport1.Append("<DIV  style='font-size:12px;'>");
                StrExport1.Append("<table border=\"1\">");
                StrExport1.Append("  <tr>");
                StrExport1.Append("      <th><strong>ID</strong></th>");
                StrExport1.Append("      <th><strong>Username</strong></th>");
                StrExport1.Append("      <th><strong>Tổng tiền (Tệ)</strong></th>");
                StrExport1.Append("      <th><strong>Tổng tiền (VND)</strong></th>");
                StrExport1.Append("      <th><strong>Tỷ giá</strong></th>");
                StrExport1.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport1.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport1.Append("  </tr>");
                foreach (var item in la1)
                {
                    string username = "";
                    var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                    if (ui != null)
                    {
                        username = ui.Username;
                    }
                    StrExport1.Append("  <tr>");
                    StrExport1.Append("      <td>" + item.ID + "</td>");
                    StrExport1.Append("      <td>" + username + "</td>");
                    StrExport1.Append("      <td>" + string.Format("{0:N2}", item.TotalPrice.ToFloat()) + "</td>");
                    StrExport1.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.TotalPriceVND.ToFloat())) + "</td>");
                    StrExport1.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.Currency.ToFloat())) + "</td>");
                    StrExport1.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                    if (item.Status == 1)
                    {
                        StrExport1.Append("      <td>Đã thanh toán</td>");
                    }
                    if (item.Status == 2)
                    {
                        StrExport1.Append("      <td>Đã hủy</td>");
                    }
                    if (item.Status == 3)
                    {
                        StrExport1.Append("      <td>Đã hoàn thành</td>");
                    }

                    StrExport1.Append("  </tr>");

                }
                StrExport1.Append("</table>");
                StrExport1.Append("</div></body></html>");
                string strFile1 = "Thong-ke-don-hang-thanh-toan-ho.xls";
                string strcontentType1 = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType1;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile1);
                Response.Write(StrExport1.ToString());
                Response.Flush();
                //Response.Close();
                Response.End();
            }
            else
            {
                var la = PayhelpController.GetAll();
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>ID</strong></th>");
                StrExport.Append("      <th><strong>Username</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền (Tệ)</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền (VND)</strong></th>");
                StrExport.Append("      <th><strong>Tỷ giá</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in la)
                {
                    string username = "";
                    var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                    if (ui != null)
                    {
                        username = ui.Username;
                    }
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.ID + "</td>");
                    StrExport.Append("      <td>" + username + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N2}", item.TotalPrice.ToFloat()) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.TotalPriceVND.ToFloat())) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.Currency.ToFloat())) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                    if (item.Status == 1)
                    {
                        StrExport.Append("      <td>Đã thanh toán</td>");
                    }
                    if (item.Status == 2)
                    {
                        StrExport.Append("      <td>Đã hủy</td>");
                    }
                    if (item.Status == 3)
                    {
                        StrExport.Append("      <td>Đã hoàn thành</td>");
                    }

                    StrExport.Append("  </tr>");

                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Thong-ke-don-hang-thanh-toan-ho.xls";
                string strcontentType = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
                //Response.Close();
                Response.End();
            }
        }
    }
}
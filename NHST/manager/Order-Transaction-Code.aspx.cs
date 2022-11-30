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

namespace NHST.manager
{
    public partial class Order_Transaction_Code : System.Web.UI.Page
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
                    if (ac.RoleID == 1)
                    {
                        Response.Redirect("/trang-chu");
                    }
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            int stype = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = Request.QueryString["stype"].ToString().ToInt(0);
                select_by.SelectedValue = stype.ToString();
            }
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                txtSearchName.Text = search;
            }
            string fd = Request.QueryString["fd"];

            string td = Request.QueryString["td"];

            string st = "";

            if (!string.IsNullOrEmpty(Request.QueryString["st"]))
            {
                st = Request.QueryString["st"].ToString();
                var list = st.Split(',').ToList();
                foreach (var item in list)
                {
                    if (status.Items.Count > 0)
                    {
                        for (int i = 0; i < status.Items.Count; i++)
                        {
                            if (status.Items[i].Value == item)
                            {
                                status.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }


            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            var total = SmallPackageController.GetTotalBy_DK(stype, search, st, fd, td);
            var la = SmallPackageController.GetAllSQLHelperWith_DK(stype, search, st, fd, td, page, 20);
            pagingall(la, total);
        }
        #region Pagging
        public void pagingall(List<SmallPackageController.smallpackage> acs, int total)
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
                    double volume = (item.Height * item.Length * item.Width) / 7000;

                    int status = Convert.ToInt32(item.Status);
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.BigPackage + "</td>");
                    hcm.Append("<td>" + item.OrderTransactionCode + "</td>");
                    hcm.Append("<td>" + item.MainOrderID + "</td>");
                    hcm.Append("<td>" + item.ProductType + "</td>");
                    hcm.Append("<td>" + item.Weight + "</td>");
                    hcm.Append("<td>" + Math.Round(volume,2) + "</td>");
                    hcm.Append("<td>" + item.Description + "</td>");
                    hcm.Append("<td>" + item.StatusString + "</td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");
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
            //int status = Convert.ToInt32(ddlStatus.SelectedValue);
            //string fd = rFD.SelectedDate.ToString();
            //string td = rTD.SelectedDate.ToString();
            //var la = SmallPackageController.GetAllSQLHelper(tSearchName.Text.Trim(), status, fd, td);
            //if (la != null)
            //{
            //    if (la.Count > 0)
            //    {
            //        double totalWeight = 0;
            //        foreach (var o in la)
            //        {
            //            if (o.Weight != null)
            //            {
            //                if (o.Weight > 0)
            //                {
            //                    totalWeight += Convert.ToDouble(o.Weight);
            //                }
            //            }

            //        }
            //        ltrtotalWeight.Text = Math.Round(totalWeight, 1).ToString();
            //        gr.DataSource = la;
            //    }
            //}

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;
        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion

        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = txtSearchName.Text.Trim();
            string fd = "";
            string td = "";
            string st = "";
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            string stype = select_by.SelectedValue;
            if (!string.IsNullOrEmpty(status.SelectedValue))
            {
                List<string> myValues = new List<string>();
                for (int i = 0; i < status.Items.Count; i++)
                {
                    var item = status.Items[i];
                    if (item.Selected)
                    {
                        myValues.Add(item.Value);
                    }
                }
                st = String.Join(",", myValues.ToArray());
            }
            if (string.IsNullOrEmpty(searchname) == true && fd == "" && td == "" && st == "" && stype == "0")
            {
                Response.Redirect("Order-Transaction-Code");
            }
            else
            {
                Response.Redirect("Order-Transaction-Code?stype=" + stype + "&s=" + searchname + "&fd=" + fd + "&td=" + td + "&st=" + st);
            }
        }
        #endregion

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            int stype = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = Request.QueryString["stype"].ToString().ToInt(0);
            }
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                txtSearchName.Text = search;
            }
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            string st = "";

            if (!string.IsNullOrEmpty(Request.QueryString["st"]))
                st = Request.QueryString["st"].ToString();

            var la = SmallPackageController.GetAllSQLHelperWith_DK(stype.ToString().ToInt(0), search, st, fd, td, 0, 0);
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:15px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>ID</strong></th>");
            StrExport.Append("      <th><strong>Bao hàng</strong></th>");
            StrExport.Append("      <th><strong>Mã vận đơn</strong></th>");
            StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Loại hàng</strong></th>");
            StrExport.Append("      <th><strong>Cân nặng (kg)</strong></th>");
            StrExport.Append("      <th><strong>Cân nặng QĐ (kg)</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                double volume = (item.Height * item.Width * item.Length) / 7000;
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.ID + "</td>");
                string baohang = BigPackageController.GetByID(Convert.ToInt32(item.BigPackageID)) != null ?
                                    BigPackageController.GetByID(Convert.ToInt32(item.BigPackageID)).PackageCode : "";
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + baohang + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.OrderTransactionCode + "</td>");
                StrExport.Append("      <td>" + item.MainOrderID + "</td>");
                StrExport.Append("      <td>" + item.ProductType + "</td>");
                StrExport.Append("      <td>" + Math.Round(Convert.ToDouble(item.Weight), 2) + "</td>");
                StrExport.Append("      <td>" + Math.Round(Convert.ToDouble(volume), 2) + "</td>");
                StrExport.Append("      <td>" + PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(item.Status)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "Danh_Sach_Ma_Van_Don.xls";
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
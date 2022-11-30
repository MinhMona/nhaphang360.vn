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
using System.Text.RegularExpressions;
using MB.Extensions;

namespace NHST.manager
{
    public partial class Warehouse_Management : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 4 || ac.RoleID == 2)
                    {
                        hyperAdd.Visible = true;
                        //ltrrole.Text = "<a type=\"button\" class=\"btn primary-btn\" href=\"/manager/Add-Package.aspx\">Thêm bao hàng</a>";
                    }
                    else
                    {
                        hyperAdd.Visible = false;
                    }
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
            var total = BigPackageController.GetTotal_DK(search);
            var la = BigPackageController.GetAllBySQL_DK(search, page, 5);
            pagingall(la, total);

        }
        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
            if (!string.IsNullOrEmpty(searchname))
            {
                Response.Redirect("Warehouse-Management?s=" + searchname);
            }
            else
            {
                Response.Redirect("Warehouse-Management");
            }
        }
        //protected void btncreateuser_Click(object sender, EventArgs e)
        //{
        //    if (!Page.IsValid) return;
        //    string username_current = Session["userLoginSystem"].ToString();
        //    string code = package_id.Text.Trim();
        //    var check = BigPackageController.GetByPackageCode(code);
        //    string BackLink = "/manager/Add-Package.aspx";
        //    if (check != null)
        //    {
        //        PJUtils.ShowMessageBoxSwAlert("Mã bao hàng đã tồn tại.", "e", false, Page);
        //    }
        //    else
        //    {
        //        double volume = 0;
        //        double weight = 0;

        //        if (pVolume.Value > 0)
        //            volume = Convert.ToDouble(pVolume.Value);
        //        if (pWeight.Value > 0)
        //            weight = Convert.ToDouble(pWeight.Value);

        //        if (volume >= 0 && weight >= 0)
        //        {
        //            string kq = BigPackageController.Insert(code, weight, volume, 1, DateTime.Now, username_current);

        //            if (kq.ToInt(0) > 0)
        //                PJUtils.ShowMessageBoxSwAlert("Tạo bao hàng thành công", "s", true, Page);
        //            else
        //                PJUtils.ShowMessageBoxSwAlert("Lỗi khi tạo bao hàng", "e", true, Page);
        //        }
        //        else
        //        {
        //            PJUtils.ShowMessageBoxSwAlert("Vui lòng kiểm tra số câng nặng và số khối !", "e", true, Page);
        //        }

        //    }
        //}
        #endregion

        #region Pagging
        public void pagingall(List<BigPackageController.Warehouse> acs, int total)
        {
            int PageSize = 5;
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
                    hcm.Append("<td>" + item.PackageCode + "</td>");
                    hcm.Append("<td>" + item.Weight + "</td>");
                    hcm.Append("<td>" + item.Volume + "</td>");
                    hcm.Append("<td>" + item.StatusString + "</td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<ul class=\"list-action-tb\">");
                    hcm.Append("<li><a href = \"Package-Detail.aspx?ID=" + item.ID + "\" class=\"btn\">Cho vào bao lớn</a>");
                    hcm.Append("<a href=\"javascript:;\" style=\"margin-top: 5px;\"  class=\"btn\" onclick=\"XuatExcel('" + item.ID + "')\" data-position=\"top\" ><span>Xuất Excel</span></a></li>");
                    hcm.Append("</ul>");
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                int roleID = ac.RoleID.ToString().ToInt();
                int i = hdfExcel.Value.ToInt();
                if (i > 0)
                {
                    ViewState["ID"] = i;
                    var p = BigPackageController.GetByID(i);
                    if (p != null)
                    {
                        string BackLink = "/manager/Warehouse-Management";
                        var la = SmallPackageController.GetBuyBigPackageBySQL_DK_Excel(i);
                        if (la.Count > 0)
                        {
                            int stt = 1;
                            StringBuilder StrExport = new StringBuilder();
                            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                            StrExport.Append("<DIV  style='font-size:12px;'>");
                            StrExport.Append("<table border=\"1\">");
                            StrExport.Append("  <tr>");
                            StrExport.Append("      <th><strong>STT</strong></th>");
                            StrExport.Append("      <th><strong>Mã vận đơn</strong></th>");
                            StrExport.Append("      <th><strong>Loại hàng</strong></th>");
                            StrExport.Append("      <th><strong>Phí ship (tệ)</strong></th>");
                            StrExport.Append("      <th><strong>Cân nặng (kg)</strong></th>");
                            StrExport.Append("      <th><strong>Khối (m3)</strong></th>");
                            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                            //StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                            StrExport.Append("  </tr>");
                            foreach (var item in la)
                            {
                                StrExport.Append("  <tr>");
                                StrExport.Append("      <td>" + stt + "</td>");
                                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.OrderTransactionCode + "</td>");
                                StrExport.Append("      <td>" + item.ProductType + "</td>");
                                StrExport.Append("      <td>" + item.FeeShip + "</td>");
                                StrExport.Append("      <td>" + item.Weight + "</td>");
                                StrExport.Append("      <td>" + item.Volume + "</td>");
                                StrExport.Append("      <td>" + item.StatusString + "</td>");
                                //StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.CreatedDateString)) + "</td>");
                                StrExport.Append("  </tr>");
                                stt++;
                            }
                            StrExport.Append("</table>");
                            StrExport.Append("</div></body></html>");
                            string strFile = "Thong-ke-danh-sach-ma-van-don-bao-hang.xls";
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
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Bao hàng có ID: "+ i +" không có đơn để xuất excel", "e", true, BackLink, Page);
                        }

                    }
                }
            }
        }

    }
}
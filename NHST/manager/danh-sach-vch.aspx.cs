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
using Telerik.Web.UI;
namespace NHST.manager
{
    public partial class danh_sach_vch : System.Web.UI.Page
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
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                int stype = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
                {
                    stype = int.Parse(Request.QueryString["stype"]);
                    ddlType.SelectedValue = stype.ToString();
                }

                string fd = Request.QueryString["fd"];

                string td = Request.QueryString["td"];

                int st = -1;

                if (!string.IsNullOrEmpty(Request.QueryString["st"]))
                {
                    st = Request.QueryString["st"].ToString().ToInt(-1);
                }
                string search = "";
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    search = Request.QueryString["s"].ToString().Trim();
                    tSearchName.Text = search;
                }
                List<tbl_TransportationOrderNew> oAll = new List<tbl_TransportationOrderNew>();
                var os = TransportationOrderNewController.GetAllWithFilter_SqlHelper_new(st, fd, td, search, stype);
                if (os.Count > 0)
                {
                    //var os0 = os.Where(o => o.Status == 0).ToList();
                    //var os1 = os.Where(o => o.Status == 1).ToList();
                    //var os2 = os.Where(o => o.Status == 2).ToList();
                    //var os3 = os.Where(o => o.Status == 3).ToList();
                    //var os4 = os.Where(o => o.Status == 4).ToList();
                    //var os5 = os.Where(o => o.Status == 5).ToList();
                    //var os6 = os.Where(o => o.Status == 6).ToList();
                    //if (os3.Count > 0)
                    //{
                    //    foreach (var o in os3)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os1.Count > 0)
                    //{
                    //    foreach (var o in os1)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os2.Count > 0)
                    //{
                    //    foreach (var o in os2)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os4.Count > 0)
                    //{
                    //    foreach (var o in os4)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os5.Count > 0)
                    //{
                    //    foreach (var o in os5)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os6.Count > 0)
                    //{
                    //    foreach (var o in os6)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}
                    //if (os0.Count > 0)
                    //{
                    //    foreach (var o in os0)
                    //    {
                    //        oAll.Add(o);
                    //    }
                    //}

                    pagingall(os.OrderByDescending(x => x.ID).ToList());
                }


            }

        }
        #region Pagging
        public void pagingall(List<tbl_TransportationOrderNew> acs)
        {
            int PageSize = 20;
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
                StringBuilder hcm = new StringBuilder();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    int status = Convert.ToInt32(item.Status);
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + item.BarCode + "</td>");
                    hcm.Append("<td>" + item.Weight + "</td>");
                    hcm.Append("<td>" + WarehouseFromController.GetByID(item.WareHouseFromID.Value).WareHouseName + "</td>");
                    hcm.Append("<td>" + WarehouseController.GetByID(item.WareHouseID.Value).WareHouseName + "</td>");
                    hcm.Append("<td>" + ShippingTypeToWareHouseController.GetByID(item.ShippingTypeID.Value).ShippingTypeName + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.AdditionFeeVND)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.SensorFeeeVND)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.CreatedDate)) + "</td>");

                    if (item.DateInVNWareHouse != null)
                    {
                        hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateInVNWareHouse)) + "</td>");
                    }
                    else
                    {
                        hcm.Append("<td></td>");
                    }

                    if (item.DateExportRequest != null)
                    {
                        hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateExportRequest)) + "</td>");
                    }
                    else
                    {
                        hcm.Append("<td></td>");
                    }

                    if (item.DateExport != null)
                    {
                        hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateExport)) + "</td>");
                    }
                    else
                    {
                        hcm.Append("<td></td>");
                    }

                    hcm.Append("<td>" + PJUtils.GeneralTransportationOrderNewStatus(Convert.ToInt32(item.Status)) + "</td>");
                    hcm.Append("<td><a class=\"btn btn-info btn-sm\" href=\"/manager/chi-tiet-vch.aspx?id=" + item.ID + "\" >Chi tiết</a></td>");
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

        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fd = "";
            string td = "";
            string stype = ddlType.SelectedValue;
            string searchname = tSearchName.Text.Trim();
            int st = status.SelectedValue.ToInt(-1);
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }

            if (fd == "" && td == "" && st == -1 && string.IsNullOrEmpty(searchname) == true && string.IsNullOrEmpty(stype) == true)
            {
                Response.Redirect("danh-sach-vch");
            }
            else
            {
                Response.Redirect("danh-sach-vch?&fd=" + fd + "&td=" + td + "&st=" + st + "&stype=" + stype + "&s=" + searchname + "");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            int stype = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = int.Parse(Request.QueryString["stype"]);
                ddlType.SelectedValue = stype.ToString();
            }
            string fd = Request.QueryString["fd"];

            string td = Request.QueryString["td"];

            int st = -1;

            if (!string.IsNullOrEmpty(Request.QueryString["st"]))
            {
                st = Request.QueryString["st"].ToString().ToInt(-1);
            }
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                tSearchName.Text = search;
            }
            var os = TransportationOrderNewController.GetAllWithFilter_SqlHelper_new(st, fd, td, search, stype);
            if (os.Count > 0)
            {
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>ID</strong></th>");
                StrExport.Append("      <th><strong>Username</strong></th>");
                StrExport.Append("      <th><strong>Mã vận đơn</strong></th>");
                StrExport.Append("      <th><strong>Cân nặng</strong></th>");
                StrExport.Append("      <th><strong>Kho TQ</strong></th>");
                StrExport.Append("      <th><strong>Kho VN</strong></th>");
                StrExport.Append("      <th><strong>PTVC</strong></th>");
                StrExport.Append("      <th><strong>Cước vật tư</strong></th>");
                StrExport.Append("      <th><strong>PP đặt hàng ĐB</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("      <th><strong>Ngày về kho đích</strong></th>");
                StrExport.Append("      <th><strong>Ngày yêu cầu xuất kho</strong></th>");
                StrExport.Append("      <th><strong>Ngày xuất kho</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in os)
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
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item.BarCode + "</td>");
                    StrExport.Append("      <td>" + item.Weight + "</td>");
                    StrExport.Append("      <td>" + WarehouseFromController.GetByID(item.WareHouseFromID.Value).WareHouseName + "</td>");
                    StrExport.Append("      <td>" + WarehouseController.GetByID(item.WareHouseID.Value).WareHouseName + "</td>");
                    StrExport.Append("      <td>" + ShippingTypeToWareHouseController.GetByID(item.ShippingTypeID.Value).ShippingTypeName + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.AdditionFeeVND)) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.SensorFeeeVND)) + "</td>");
                    StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.CreatedDate)) + "</td>");
                    if (item.DateInVNWareHouse != null)
                    {
                        StrExport.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateInVNWareHouse)) + "</td>");
                    }
                    else
                    {
                        StrExport.Append("<td></td>");
                    }
                    if (item.DateExportRequest != null)
                    {
                        StrExport.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateExportRequest)) + "</td>");
                    }
                    else
                    {
                        StrExport.Append("<td></td>");
                    }

                    if (item.DateExport != null)
                    {
                        StrExport.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(item.DateExport)) + "</td>");
                    }
                    else
                    {
                        StrExport.Append("<td></td>");
                    }

                    StrExport.Append("<td>" + PJUtils.GeneralTransportationOrderNewStatus(Convert.ToInt32(item.Status)) + "</td>");

                    StrExport.Append("  </tr>");

                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Thong-ke-danh-sach-van-chuyen-ho.xls";
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
                #endregion
            }
        }
    }
}
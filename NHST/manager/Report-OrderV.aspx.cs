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
    public partial class Report_OrderV : System.Web.UI.Page
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
                            Response.Redirect("/");
                        }
                    }
                }
                LoadData();
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
                string search = null;
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    search = Request.QueryString["s"].ToString().Trim();
                    tSearchName.Text = search;
                }
                string fd = Request.QueryString["fd"];
                if (fd == "")
                {
                    fd = null;
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    rdatefrom.Text = fd;
                    fd = string.IsNullOrEmpty(rdatefrom.Text) != true ? DateTime.ParseExact(rdatefrom.Text.ToString().Trim(), "dd/MM/yyyy HH:mm", null).ToString() : null;
                }
                string td = Request.QueryString["td"];
                if (td == "")
                {
                    td = null;
                }
                if (!string.IsNullOrEmpty(td))
                {
                    rdateto.Text = td;
                    td = string.IsNullOrEmpty(rdateto.Text) != true ? DateTime.ParseExact(rdateto.Text.ToString().Trim(), "dd/MM/yyyy HH:mm", null).ToString() : null;
                }
                int page = 0;
                Int32 Page = GetIntFromQueryString("Page");
                if (Page > 0)
                {
                    page = (Page - 1) * 20;

                }
                var la = MainOrderController.OrderListStore(stype, search, fd, td, 20, page);
                var total = 0;

                if (la.Count > 0)
                {
                    total = la.FirstOrDefault().TotalRow.Value;
                }
                pagingall(la, total);

                var la2 = MainOrderController.OrderListStoreExcel(stype, search, fd, td);
                double or_FeeShipCN = 0;
                double or_FeeBuyPro = 0;
                double or_FeeWeight = 0;
                double or_FeeCheck = 0;
                double or_FeePacked = 0;
                double or_FeeInsurrance = 0;
                double or_FeeShipHome = 0;
                double or_FeeDeposit = 0;
                double or_FeeReal = 0;
                double Total = 0;
                double Damuahang = 0;
                double Dahoanthanh = 0;
                if (la2.Count > 0)
                {
                    foreach (var o in la2)
                    {
                        if (o.FeeShipCN.ToFloat(0) > 0)
                            or_FeeShipCN += Convert.ToDouble(o.FeeShipCN);
                        if (o.FeeBuyPro.ToFloat(0) > 0)
                            or_FeeBuyPro += Convert.ToDouble(o.FeeBuyPro);
                        if (o.FeeWeight.ToFloat(0) > 0)
                            or_FeeWeight += Convert.ToDouble(o.FeeWeight);
                        if (o.IsCheckProductPrice.ToFloat(0) > 0)
                            or_FeeCheck += Convert.ToDouble(o.IsCheckProductPrice);
                        if (o.IsPackedPrice.ToFloat(0) > 0)
                            or_FeePacked += Convert.ToDouble(o.IsPackedPrice);
                        if (o.InsuranceMoney.ToFloat(0) > 0)
                            or_FeeInsurrance += Convert.ToDouble(o.InsuranceMoney);
                        if (o.IsFastDeliveryPrice.ToFloat(0) > 0)
                            or_FeeShipHome += Convert.ToDouble(o.IsFastDeliveryPrice);
                        if (o.Deposit.ToFloat(0) > 0)
                            or_FeeDeposit += Convert.ToDouble(o.Deposit);
                        if (o.TotalPriceVND.ToFloat(0) > 0)
                            Total += Convert.ToDouble(o.TotalPriceVND);
                        if (o.TotalPriceReal.ToFloat(0) > 0)
                            or_FeeReal += Convert.ToDouble(o.TotalPriceReal);
                    }
                    var ListOrder_Damuahang = la2.Where(o => o.Status == 4).ToList();
                    if (ListOrder_Damuahang.Count > 0)
                    {
                        foreach (var od in ListOrder_Damuahang)
                        {
                            Damuahang += Convert.ToDouble(od.TotalPriceVND);
                        }
                    }
                    var ListOrder_Dahoanthanh = la2.Where(o => o.Status == 10).ToList();
                    if (ListOrder_Dahoanthanh.Count > 0)
                    {
                        foreach (var od in ListOrder_Dahoanthanh)
                        {
                            Dahoanthanh += Convert.ToDouble(od.TotalPriceVND);
                        }
                    }
                }
                lblFeeShipCN.Text = string.Format("{0:N0}", or_FeeShipCN) + " VNĐ";
                lblFeeBuyPro.Text = string.Format("{0:N0}", or_FeeBuyPro) + " VNĐ";
                lblFeeWeight.Text = string.Format("{0:N0}", or_FeeWeight) + " VNĐ";
                lblFeeCheck.Text = string.Format("{0:N0}", or_FeeCheck) + " VNĐ";
                lblFeePacked.Text = string.Format("{0:N0}", or_FeePacked) + " VNĐ";
                lblFeeShiphome.Text = string.Format("{0:N0}", or_FeeShipHome) + " VNĐ";
                lblDeposit.Text = string.Format("{0:N0}", or_FeeDeposit) + " VNĐ";
                lblDamuahang.Text = string.Format("{0:N0}", Damuahang) + " VNĐ";
                lblDahoanthanh.Text = string.Format("{0:N0}", Dahoanthanh) + " VNĐ";
                lblTotal.Text = string.Format("{0:N0}", Total) + " VNĐ";
                lblFeeReal.Text = string.Format("{0:N0}", or_FeeReal) + " VNĐ";
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string stype = ddlType.SelectedValue;
            string searchname = tSearchName.Text.Trim();
            string fd = null;
            string td = null;
            if (!string.IsNullOrEmpty(rdatefrom.Text))
            {
                fd = rdatefrom.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rdateto.Text))
            {
                td = rdateto.Text.ToString();
            }
            Response.Redirect("Report-OrderV?stype=" + stype + "&s=" + searchname + "&fd=" + fd + "&td=" + td + "");
        }

        public void pagingall(List<ReportOrder_Result> acs, int total)
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

                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + item.SalerName + "</td>");
                    hcm.Append("<td>" + item.DathangName + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND) - Convert.ToDouble(item.Deposit)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.PriceVND)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeBuyPro)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCN)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeWeight)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsCheckProductPrice)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsPackedPrice)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.InsuranceMoney)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsFastDeliveryPrice)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalFeeSupport)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceReal)) + "</td>");
                    hcm.Append("<td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string search = "";
            int stype = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = int.Parse(Request.QueryString["stype"]);
                ddlType.SelectedValue = stype.ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                tSearchName.Text = search;
            }
            string fd = Request.QueryString["fd"];
            if (fd == "")
            {
                fd = null;
            }
            if (!string.IsNullOrEmpty(fd))
            {
                rdatefrom.Text = fd;
                fd = string.IsNullOrEmpty(rdatefrom.Text) != true ? DateTime.ParseExact(rdatefrom.Text.ToString().Trim(), "dd/MM/yyyy HH:mm", null).ToString() : null;
            }
            string td = Request.QueryString["td"];
            if (td == "")
            {
                td = null;
            }
            if (!string.IsNullOrEmpty(td))
            {
                rdateto.Text = td;
                td = string.IsNullOrEmpty(rdateto.Text) != true ? DateTime.ParseExact(rdateto.Text.ToString().Trim(), "dd/MM/yyyy HH:mm", null).ToString() : null;
            }
            var la = MainOrderController.OrderListStoreExcel(stype, search, fd, td);
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>ID đơn</strong></th>");
            StrExport.Append("      <th><strong>Username</strong></th>");
            StrExport.Append("      <th><strong>NV Sales</strong></th>");
            StrExport.Append("      <th><strong>NV Đặt hàng</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Đã trả</strong></th>");
            StrExport.Append("      <th><strong>Còn lại</strong></th>");
            StrExport.Append("      <th><strong>Tiền hàng web</strong></th>");
            StrExport.Append("      <th><strong>Phí dịch vụ</strong></th>");
            StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
            StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
            StrExport.Append("      <th><strong>Phí kiểm hàng</strong></th>");
            StrExport.Append("      <th><strong>Phí đống gỗ</strong></th>");
            StrExport.Append("      <th><strong>Phí bảo hiểm</strong></th>");
            StrExport.Append("      <th><strong>Phí giao hàng tận nhà</strong></th>");
            StrExport.Append("      <th><strong>Phụ phí</strong></th>");
            StrExport.Append("      <th><strong>Tiền mua thật</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.ID + "</td>");
                StrExport.Append("      <td>" + item.Username + "</td>");
                StrExport.Append("      <td>" + item.SalerName + "</td>");
                StrExport.Append("      <td>" + item.DathangName + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND) - Convert.ToDouble(item.Deposit)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.PriceVND)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeBuyPro)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCN)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.FeeWeight)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsCheckProductPrice)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsPackedPrice)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.InsuranceMoney)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.IsFastDeliveryPrice)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalFeeSupport)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceReal)) + "</td>");
                StrExport.Append("      <td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "UserList.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            Response.End();
        }
    }
}
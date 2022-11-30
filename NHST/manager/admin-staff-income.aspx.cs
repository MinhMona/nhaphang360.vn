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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;
using static NHST.Controllers.StaffIncomeController;

namespace NHST.manager
{
    public partial class admin_staff_income : System.Web.UI.Page
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
                    LoadData();
                }
            }

        }
        protected void search_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
            string fd = "";
            string td = "";
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            string st = select_by.SelectedValue;
            if (string.IsNullOrEmpty(searchname) == true && fd == "" && td == "" && st == "0")
            {
                Response.Redirect("admin-staff-income");
            }
            else
            {
                Response.Redirect("admin-staff-income?s=" + searchname + "&fd=" + fd + "&td=" + td + "&st=" + st + "");
            }
        }
        protected void LoadData()
        {
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
                rFD.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rTD.Text = td;
            string search = "";
            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
                select_by.SelectedIndex = st.ToInt(0);
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            double TotalPaid = 0;
            double TotalNotPaid = 0;
            TotalPaid = StaffIncomeController.GetTotalPriceAdmin(2, "TotalPriceReceive", search, fd, td);
            TotalNotPaid = StaffIncomeController.GetTotalPriceAdmin(1, "TotalPriceReceive", search, fd, td);
            ltrTienDaThanhToan.Text = string.Format("{0:N0}", TotalPaid) + " VNĐ";
            ltrTienChuaThanhToan.Text = string.Format("{0:N0}", TotalNotPaid) + " VNĐ";
            var la = GetAllBySQL(st.ToInt(0), search, fd, td, page, 20);
            int total = GetTotal(st.ToInt(0), search, fd, td);
            pagingall(la, total);
        }

        #region Pagging
        public void pagingall(List<StaffInCome> acs, int total)
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
                    hcm.Append("<td>" + item.MainOrderID + "</td>");
                    hcm.Append("<td>" + item.Percent + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceReceive)) + " VNĐ</td>");
                    hcm.Append("<td>" + item.UserName + "</td>");
                    hcm.Append("<td>" + item.RoleName + "</td>");
                    hcm.Append("<td class=\"statusThanhtoan\">" + item.StatusName + "</td>");
                    hcm.Append("<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                    hcm.Append("<td>");
                    if (item.Status == 1)
                    {
                        hcm.Append("<div class=\"action-table\">");
                        hcm.Append("<a class='tooltipped' href='OrderDetail.aspx?id=" + item.MainOrderID + "' data-position=\"top\"");
                        hcm.Append("data-tooltip=\"Xem chi tiết đơn\"><i ");
                        hcm.Append("  class=\"material-icons valign-center\">remove_red_eye</i></a>");
                        hcm.Append("<a class='tooltipped' href='javascript:;' data-value=\"" + Convert.ToDouble(item.TotalPriceReceive) + "\" onclick=\"thanhtoanHoahong(" + item.ID + ",$(this))\" data-position=\"top\"");
                        hcm.Append(" data-tooltip=\"Thanh toán\"><i");
                        hcm.Append("   class=\"material-icons valign-center\">payment</i></a>");
                        hcm.Append("</div>");
                    }
                    else
                    {
                        hcm.Append("<div class=\"action-table\">");
                        hcm.Append("<a class='tooltipped' href='OrderDetail.aspx?id=" + item.MainOrderID + "' data-position=\"top\"");
                        hcm.Append("data-tooltip=\"Xem chi tiết đơn\"><i ");
                        hcm.Append("  class=\"material-icons valign-center\">remove_red_eye</i></a>");
                        hcm.Append("</div>");
                    }
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

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            var username = Session["userLoginSystem"].ToString();
            int UID = Request.QueryString["uid"].ToInt(0);
            int status = Request.QueryString["st"].ToInt(0);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            int od = Request.QueryString["mo"].ToInt(0);
            List<tbl_StaffIncome> history = new List<tbl_StaffIncome>();
            history = StaffIncomeController.GetBy_SQLHelper(UID, status, fd, td, od);
            if (history.Count > 0)
            {
                foreach (var item in history)
                {
                    if (item.Status == 1)
                    {
                        var mo = MainOrderController.GetByID(item.MainOrderID.Value);
                        if (mo != null)
                        {
                            if (item.RoleID == 3)
                            {
                                var ac = AccountController.GetByID(mo.DathangID.Value);
                                if (ac != null)
                                {
                                    double wallet = Convert.ToDouble(ac.Wallet) + Convert.ToDouble(item.TotalPriceReceive);
                                    AccountController.updateWallet(ac.ID, 0, DateTime.Now, username);
                                    HistoryPayWalletController.Insert(ac.ID, ac.Username, mo.ID, Convert.ToDouble(item.TotalPriceReceive), ac.Username + " đã nhận được hoa hồng của đơn hàng: " + mo.ID + ".", wallet, 2, 14, DateTime.Now, username);
                                    string kq = StaffIncomeController.UpdateStatus(item.ID, 2, DateTime.Now, username);

                                }
                            }
                            else
                            {
                                var ac = AccountController.GetByID(mo.SalerID.Value);
                                if (ac != null)
                                {
                                    double wallet = Convert.ToDouble(ac.Wallet) + Convert.ToDouble(item.TotalPriceReceive);
                                    AccountController.updateWallet(ac.ID, 0, DateTime.Now, username);
                                    HistoryPayWalletController.Insert(ac.ID, ac.Username, mo.ID, Convert.ToDouble(item.TotalPriceReceive), ac.Username + " đã nhận được hoa hồng của đơn hàng: " + mo.ID + ".", wallet, 2, 14, DateTime.Now, username);
                                    string kq = StaffIncomeController.UpdateStatus(item.ID, 2, DateTime.Now, username);
                                }
                            }
                        }
                    }
                }
                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công!", "s", true, Page);
            }
        }

        [WebMethod]
        public static string UpdateStatus(int ID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var s = StaffIncomeController.GetByID(ID);
            if (s != null)
            {
                string kq = StaffIncomeController.UpdateStatus(s.ID, 2, DateTime.UtcNow.AddHours(7), username);
                return kq;
            }
            return "0";
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {           
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
                rFD.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rTD.Text = td;
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
                select_by.SelectedIndex = st.ToInt(0);
            
            var la = GetAllBySQLForExcel(st.ToInt(0), search, fd, td);
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Phần trăm</strong></th>");
            StrExport.Append("      <th><strong>Hoa hồng (VNĐ)</strong></th>");
            StrExport.Append("      <th><strong>Username</strong></th>");
            StrExport.Append("      <th><strong>Quyền hạn</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày giờ</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.MainOrderID + "</td>");
                StrExport.Append("      <td>" + item.Percent + " %</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceReceive)) + "</td>");
                StrExport.Append("      <td>" + item.UserName + "</td>");
                StrExport.Append("      <td>" + item.RoleName + "</td>");
                StrExport.Append("      <td>" + item.StatusName + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "Danh-sach-hoa-hong.xls";
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
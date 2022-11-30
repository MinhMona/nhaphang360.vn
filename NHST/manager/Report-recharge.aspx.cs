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
    public partial class Report_recharge : System.Web.UI.Page
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
                LoadGrid1();
            }
        }
        public void LoadData()
        {
            List<tbl_Bank> a = BankController.GetAll();
            ddlBank.Items.Add(new ListItem("--Tất cả--", "0"));
            ddlBank.SelectedValue = "0";
            if (a.Count > 0)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    var item = a[i];
                    ddlBank.Items.Add(new ListItem(item.BankName + " - " + item.AccountHolder + " - " + item.BankNumber + " - " + item.Branch, item.ID.ToString()));
                }
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string searchname = search_name.Text.Trim();
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
            string bk = ddlBank.SelectedValue;

            if (string.IsNullOrEmpty(searchname) == true && fd == "" && td == "" && bk == "0")
            {
                Response.Redirect("Report-recharge.aspx");
            }
            else
            {
                Response.Redirect("Report-recharge.aspx?s=" + searchname + "&fd=" + fd + "&td=" + td + "&bk=" + bk + "");
            }

        }
        public void LoadGrid1()
        {
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rdateto.Text = td;
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            string bk = "0";
            if (!string.IsNullOrEmpty(Request.QueryString["bk"]))
            {
                bk = Request.QueryString["bk"];
            }
            ddlBank.SelectedValue = bk;
            int pageA = 0;
            Int32 PageA = GetIntFromQueryString("PageA");
            if (PageA > 0)
            {
                pageA = PageA - 1;
            }

            double totalprice = AdminSendUserWalletController.GetTotalPriceWithBank(search, "2", bk, fd, td);

            var la = AdminSendUserWalletController.GetBySQL_DK_WithBank(search, "2", bk, pageA, fd, td, 10);
            int total = AdminSendUserWalletController.GetTotalListWithBank(search, "2", bk, fd, td);
            if (la.Count > 0)
            {
                pagingallA(la, total);
            }

            int pageB = 0;
            Int32 PageB = GetIntFromQueryString("PageB");
            if (PageB > 0)
            {
                pageB = PageB - 1;
            }
            double TotalPriceWithDraw = WithdrawController.GetTotalWithDrawSQL(search_name.Text.Trim(), "2");

            var laWithDraw = WithdrawController.GetBySQL_DK(search, "2", pageB, 10);
            int totalWithDraw = WithdrawController.GetTotalSQL(search_name.Text.Trim(), "2");
            if (laWithDraw.Count > 0)
            {
                pagingallB(laWithDraw, totalWithDraw);
            }


            double[] data2 = new double[] { totalprice, TotalPriceWithDraw, totalprice - TotalPriceWithDraw };
            string[] backgroundColor2 = new string[] { "green_cyan_gradient", "red_orange_gradient", "orange_yellow_gradient" };
            string datasetsTotal = new JavaScriptSerializer().Serialize(new
            {
                label = "Tiền nạp",
                data = data2,
                backgroundColor = backgroundColor2,
                hoverBackgroundColor = "hover_gradient",
                hoverBorderWidth = 2,
                hoverBorderColor = "hover_gradient"
            });
            string dataChartTotal = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               labels = "[\"Tiền nạp\", \"Tiền rút\", \"Dư tạm thời\"]",
                                               datasets = "[" + datasetsTotal + "]"
                                           });

            hdfDataChart.Value = dataChartTotal;

        }
        public class ReportOrder
        {
            public int OrderID { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string BuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string ShipHome { get; set; }
            public string CheckProduct { get; set; }
            public string Package { get; set; }
            public string IsFast { get; set; }
            public string Total { get; set; }
            public string Deposit { get; set; }
            public string PayLeft { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }
        public class PayHistory
        {
            public int MainOrderID { get; set; }
            public int UID { get; set; }
            public int ID { get; set; }
            public string Username { get; set; }
            public string Status { get; set; }
            public string Amount { get; set; }
            public string Bank { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
        }

        protected void btnExport1_Click(object sender, EventArgs e)
        {
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rdateto.Text = td;
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            string bk = "0";
            if (!string.IsNullOrEmpty(Request.QueryString["bk"]))
            {
                bk = Request.QueryString["bk"];
            }
            ddlBank.SelectedValue = bk;
            int pageA = 0;
            Int32 PageA = GetIntFromQueryString("PageA");
            if (PageA > 0)
            {
                pageA = PageA - 1;
            }

            int total = AdminSendUserWalletController.GetTotalListWithBank(search, "2", bk, fd, td);
            var la = AdminSendUserWalletController.GetBySQL_DK_WithBank(search, "2", bk, 0, fd, td, total);

            List<PayHistory> ro_gr1 = new List<PayHistory>();
            double totalAmount = 0;
            if (la.Count > 0)
            {
                //List<AdminSendUserWalletController.ListShowNew> acs = la;


                foreach (var o in la)
                {
                    PayHistory PayHis = new PayHistory();
                    PayHis.ID = o.ID;
                    PayHis.Username = o.UserName;
                    PayHis.Amount = string.Format("{0:N0}", o.Amount);
                    totalAmount += Convert.ToDouble(string.Format("{0:N0}", o.Amount));
                    PayHis.Bank = PJUtils.ReturnBank(o.BankID);
                    PayHis.Status = o.StatusName;
                    PayHis.CreatedDate = o.CreatedDateString;
                    PayHis.CreatedBy = o.CreatedBy;
                    ro_gr1.Add(PayHis);
                }

            }
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>UserID</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Số tiền</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Ngân hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Ngày tạo</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Người tạo</strong></th>");

            StrExport.Append("  </tr>");
            foreach (var item2 in ro_gr1)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item2.ID + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item2.Username + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.Amount + " VND</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.Bank + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.Status + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.CreatedDate + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.CreatedBy + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            //if (fd != null && td != null)
            //{
            //    StrExport.Append("<h2>Thời gian thống kê từ: " + fd + " đến " + td + " VND</h2>");
            //}
            StrExport.Append("<h4>Tổng số tiền đã nạp: " + string.Format("{0:N0}", totalAmount) + " VND</h4>");
            StrExport.Append("</div></body></html>");
            string strFile = "Bao-cao-danh-sach-nap-tien.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();
        }
        protected void btnExport2_Click(object sender, EventArgs e)
        {
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.Text = fd;
            if (!string.IsNullOrEmpty(td))
                rdateto.Text = td;
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            string bk = "0";
            if (!string.IsNullOrEmpty(Request.QueryString["bk"]))
            {
                bk = Request.QueryString["bk"];
            }
            ddlBank.SelectedValue = bk;

            int pageB = 0;
            Int32 PageB = GetIntFromQueryString("PageB");
            if (PageB > 0)
            {
                pageB = PageB - 1;
            }

            int totalWithDraw = WithdrawController.GetTotalSQL(search_name.Text.Trim(), "2");
            var laWithDraw = WithdrawController.GetBySQL_DK(search, "2", 0, totalWithDraw);
            List<PayHistory> ro_gr2 = new List<PayHistory>();
            double totalAmount = 0;
            if (laWithDraw.Count > 0)
            {
                List<WithdrawController.ListWithdrawNew> acs = laWithDraw;


                for (int i = 0; i < acs.Count; i++)
                {
                    PayHistory PayHis = new PayHistory();
                    var item = acs[i];
                    PayHis.ID = item.ID;
                    PayHis.Username = item.Username;
                    PayHis.Amount = string.Format("{0:N0}", item.Amount);
                    totalAmount += Convert.ToDouble(string.Format("{0:N0}", item.Amount));

                    PayHis.Status = item.StatusName;
                    PayHis.CreatedDate = item.CreatedDateString;
                    PayHis.CreatedBy = item.AcceptBy;
                    ro_gr2.Add(PayHis);
                }
            }

            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>UserID</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Số tiền</strong></th>");

            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Ngày tạo</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\"><strong>Người duyệt</strong></th>");

            StrExport.Append("  </tr>");
            foreach (var item2 in ro_gr2)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item2.ID + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item2.Username + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.Amount + " VND</td>");

                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.Status + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.CreatedDate + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\">" + item2.CreatedBy + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            //if (fd != null && td != null)
            //{
            //    StrExport.Append("<h2>Thời gian thống kê từ: " + fd + " đến " + td + " VND</h2>");
            //}
            StrExport.Append("<h5>Tổng số tiền đã rút: " + string.Format("{0:N0}", totalAmount) + " VND</h5>");
            StrExport.Append("</div></body></html>");
            string strFile = "Bao-cao-danh-sach-rut-tien.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();
        }
        #region Pagging
        public void pagingallA(List<AdminSendUserWalletController.ListShowNew> acs, int total)
        {
            int PageSize = 10;
            if (total > 0)
            {
                int TotalItems = total;
                if (TotalItems % PageSize == 0)
                    PageCountA = TotalItems / PageSize;
                else
                    PageCountA = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("PageA");

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
                    //string Bank = item.BankName + " CN " + item.BankBranch + "-" + item.BankAccountHolder + "-" + item.BankNumber;
                    string Bank = PJUtils.ReturnBank(item.BankID);
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.UserName + "</td>");
                    hcm.Append("<td>" + item.AmountString + "</td>");
                    hcm.Append("<td>" + Bank + "</td>");
                    hcm.Append("<td>" + item.StatusName + "</td>");
                    hcm.Append("<td>" + item.CreatedDateString + "</td>");
                    hcm.Append("<td>" + item.CreatedBy + "</td>");
                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
            }
        }
        public void pagingallB(List<WithdrawController.ListWithdrawNew> acs, int total)
        {
            int PageSize = 10;
            if (total > 0)
            {
                int TotalItems = total;
                if (TotalItems % PageSize == 0)
                    PageCountB = TotalItems / PageSize;
                else
                    PageCountB = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("PageB");

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
                    hcm.Append("<td>" + item.AmountString + "</td>");
                    hcm.Append("<td>" + item.StatusName + "</td>");
                    hcm.Append("<td>" + item.CreatedDateString + "</td>");
                    hcm.Append("<td>" + item.AcceptBy + "</td>");
                    hcm.Append("</tr>");
                }
                ltrWithDraw.Text = hcm.ToString();
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
        private int PageCountA;
        private int PageCountB;
        protected void DisplayHtmlStringPagingA()
        {
            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["PageA"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCountA > 1)
                Response.Write(GetHtmlPagingAdvancedA(6, CurrentPage, PageCountA, Context.Request.RawUrl, strText));
        }
        protected void DisplayHtmlStringPagingB()
        {
            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["PageB"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCountB > 1)
                Response.Write(GetHtmlPagingAdvancedB(6, CurrentPage, PageCountB, Context.Request.RawUrl, strText));
        }
        private static string GetPageUrlA(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "PageA=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                if (pageUrl.IndexOf("PageA=") > 0)
                {
                    int a = pageUrl.IndexOf("PageA=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&PageA={0}";
                }

            }
            else
            {
                pageUrl += "?PageA={0}";
            }
            return pageUrl;
        }
        private static string GetPageUrlB(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "PageB=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                if (pageUrl.IndexOf("PageB=") > 0)
                {
                    int a = pageUrl.IndexOf("PageB=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&PageB={0}";
                }

            }
            else
            {
                pageUrl += "?PageB={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvancedB(int pagesToOutput, int currentPage, int pageCountB, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrlB(currentPage, currentPageUrl);


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

            if (stopPageNumbersAt > pageCountB)
            {
                stopPageNumbersAt = pageCountB;
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
                output.Append("<a href=\"" + string.Format(GetPageUrlB(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
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
            if (stopPageNumbersAt < pageCountB)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCountB)
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
        public static string GetHtmlPagingAdvancedA(int pagesToOutput, int currentPage, int pageCountA, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrlA(currentPage, currentPageUrl);


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

            if (stopPageNumbersAt > pageCountA)
            {
                stopPageNumbersAt = pageCountA;
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
                output.Append("<a href=\"" + string.Format(GetPageUrlA(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
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
            if (stopPageNumbersAt < pageCountA)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCountA)
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
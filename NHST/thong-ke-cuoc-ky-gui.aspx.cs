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

namespace NHST
{
    public partial class thong_ke_cuoc_ky_gui : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "nhutsg8844";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                List<ReportTrans> rs = new List<ReportTrans>();
                var r = ExportRequestTurnController.GetByCreatedByAndVCH(acc.ID);
                if (r.Count > 0)
                {
                    //foreach (var item in r)
                    //{
                    //    ReportTrans t = new ReportTrans();
                    //    t.ID = item.ID;
                    //    t.DateRequest = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
                    //    string dateOutWH = "";
                    //    int TotalPackages = 0;
                    //    var re = RequestOutStockController.GetByExportRequestTurnID(item.ID);
                    //    if (re.Count > 0)
                    //    {
                    //        dateOutWH += "<table class=\"table table-bordered table-hover\">";
                    //        dateOutWH += "<tr>";
                    //        dateOutWH += "  <th>Mã vận đơn</th>";
                    //        dateOutWH += "  <th>Ngày XK</th>";
                    //        dateOutWH += "</tr>";
                    //        TotalPackages = re.Count;
                    //        foreach (var ro in re)
                    //        {
                    //            dateOutWH += "<tr>";
                    //            var smallpack = SmallPackageController.GetByID(Convert.ToInt32(ro.SmallPackageID));
                    //            if (smallpack != null)
                    //            {
                    //                dateOutWH += "<td>" + smallpack.OrderTransactionCode + "</td>";
                    //                if (smallpack.DateOutWarehouse != null)
                    //                {
                    //                    dateOutWH += "<td>" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(smallpack.DateOutWarehouse)) + "</td>";
                    //                }
                    //                else
                    //                {
                    //                    dateOutWH += "<td><span class=\"bg-red\">Chưa xuất kho</span></td>";
                    //                }
                    //            }
                    //            dateOutWH += "<tr>";
                    //        }
                    //        dateOutWH += "</table>";
                    //    }

                    //    t.DateOutWH = dateOutWH;
                    //    t.TotalPackages = TotalPackages.ToString();
                    //    t.TotalWeight = Math.Round(Convert.ToDouble(item.TotalWeight), 1).ToString();
                    //    t.TotalPrice = string.Format("{0:N0}", item.TotalPriceVND) + " VNĐ";
                    //    string htvc = "";
                    //    var h = ShippingTypeVNController.GetByID(Convert.ToInt32(item.ShippingTypeInVNID));
                    //    if (h != null)
                    //    {
                    //        htvc = h.ShippingTypeVNName;
                    //    }
                    //    t.HTVC = htvc;
                    //    t.StaffNote = item.StaffNote;
                    //    t.StatusStr = PJUtils.ReturnStatusTT(item.Status.Value);
                    //    t.StatusExport = PJUtils.ReturnStatusXK(item.StatusExport.Value);
                    //    t.Status = item.Status.Value;
                    //    rs.Add(t);
                    //}
                    pagingall(r);
                }
                
            }
        }
        #region Paging
        public void pagingall(List<tbl_ExportRequestTurn> acs)
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
                StringBuilder html = new StringBuilder();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    html.Append("<tr>");
                    html.Append("<td style=\"text-align: center\">" + item.ID + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");

                    string dateOutWH = "";
                    int TotalPackages = 0;
                    var re = RequestOutStockController.GetByExportRequestTurnID(item.ID);
                    if (re.Count > 0)
                    {
                        dateOutWH += "<table class=\"table table-bordered table-hover\">";
                        dateOutWH += "<tr>";
                        dateOutWH += "  <th>Mã vận đơn</th>";
                        dateOutWH += "  <th>Ngày XK</th>";
                        dateOutWH += "</tr>";
                        TotalPackages = re.Count;
                        foreach (var ro in re)
                        {
                            dateOutWH += "<tr>";
                            var smallpack = SmallPackageController.GetByID(Convert.ToInt32(ro.SmallPackageID));
                            if (smallpack != null)
                            {
                                dateOutWH += "<td>" + smallpack.OrderTransactionCode + "</td>";
                                if (smallpack.DateOutWarehouse != null)
                                {
                                    dateOutWH += "<td>" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(smallpack.DateOutWarehouse)) + "</td>";
                                }
                                else
                                {
                                    dateOutWH += "<td><span class=\"bg-red\">Chưa xuất kho</span></td>";
                                }
                            }
                            dateOutWH += "<tr>";
                        }
                        dateOutWH += "</table>";
                    }

                    html.Append("<td style=\"text-align: center\">" + dateOutWH + "</td>");
                    html.Append("<td style=\"text-align: center\">" + TotalPackages + "</td>");
                    html.Append("<td style=\"text-align: center\">" + Math.Round(Convert.ToDouble(item.TotalWeight), 1).ToString() + "</td>");
                    html.Append("<td style=\"text-align: center\">" + string.Format("{0:N0}", item.TotalPriceVND) + " VNĐ</td>");
                    string htvc = "";
                    var h = ShippingTypeVNController.GetByID(Convert.ToInt32(item.ShippingTypeInVNID));
                    if (h != null)
                    {
                        htvc = h.ShippingTypeVNName;
                    }
                    html.Append("<td style=\"text-align: center\">" + htvc + "</td>");
                    html.Append("<td style=\"text-align: center\">" + PJUtils.ReturnStatusTT(item.Status.Value) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + PJUtils.ReturnStatusXK(item.StatusExport.Value) + "</td>");
                    html.Append("<td style=\"text-align: center\">" + item.StaffNote + "</td>");
                    html.Append("<td>");
                    html.Append(" <div class=\"action-table\">");
                    if (item.Status != 2)
                    {
                        html.Append("<a href =\"javascript:;\" onclick=\"Pay($(this),'" + item.ID + "')\" data-position=\"top\" ><i class=\"material-icons\">payment</i><span>Thanh toán</span></a>");
                    }
                    html.Append("</div>");

                    html.Append("</td>");
                    html.Append("</tr>");
                }
                ltr.Text = html.ToString();
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

        public class ReportTrans
        {
            public int ID { get; set; }
            public string DateRequest { get; set; }
            public string DateOutWH { get; set; }
            public string TotalPackages { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPrice { get; set; }
            public string HTVC { get; set; }
            public string StaffNote { get; set; }
            public string StatusStr { get; set; }
            public string StatusExport { get; set; }
            public int Status { get; set; }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            string username = Session["userLoginSystem"].ToString();
            int UID = 0;
            var acc_log = AccountController.GetByUsername(username);
            if (acc_log != null)
            {
                UID = acc_log.ID;
            }
            int id = hdfID.Value.ToInt(0);
            if (id > 0)
            {
                var ots = ExportRequestTurnController.GetByID(id);
                if (ots != null)
                {
                    double totalPay = Convert.ToDouble(ots.TotalPriceVND);

                    if (totalPay > 0)
                    {
                        var user_wallet = AccountController.GetByID(UID);
                        if (user_wallet.Wallet < totalPay)
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không đủ tiền trong tài khoản!, vui lòng nạp thêm tiền!", "e", true, Page);
                        }
                        else
                        {
                            #region Trừ tiền VC
                            var acc = AccountController.GetByID(UID);
                            if (acc != null)
                            {
                                double wallet = Convert.ToDouble(acc.Wallet);
                                double totalPriceVND = Convert.ToDouble(ots.TotalPriceVND);
                                double walletLeft = wallet - totalPriceVND;
                                AccountController.updateWallet(UID, walletLeft, currentDate, username);
                                HistoryPayWalletController.Insert(UID, username, 0, totalPriceVND,
                                    username + " đã thanh toán đơn hàng vận chuyển hộ.", walletLeft, 1, 8, currentDate, username);
                            }
                            #endregion

                            ExportRequestTurnController.UpdateStatus(ots.ID, 2);

                            PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                        }
                    }
                }
            }
        }
    }
}
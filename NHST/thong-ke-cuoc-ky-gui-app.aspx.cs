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
    public partial class thong_ke_cuoc_ky_gui_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Convert.ToInt32(Request.QueryString["UID"]);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {

                    var ac = AccountController.GetByID(UID);
                    if (ac != null)
                    {
                        int page = 0;
                        ViewState["UID"] = UID;
                        ViewState["Key"] = Key;
                        pnMobile.Visible = true;
                        Int32 Page = GetIntFromQueryString("Page");
                        if (Page > 0)
                        {
                            page = Page - 1;
                        }

                        var r = ExportRequestTurnController.GetByCreatedByAndVCH(UID);
                        if (r.Count > 0)
                        {
                            pagingall(r);
                        }
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
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
                string Key = ViewState["Key"].ToString();
                int UID = Convert.ToInt32(ViewState["UID"]);
                StringBuilder html = new StringBuilder();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];

                    html.Append(" <div class=\"thanhtoanho-list\" data-id=\"" + item.ID + "\">");
                    html.Append(" <div class=\"all\">");
                    html.Append(" <div class=\"order-group offset15\">");
                    html.Append("  <div class=\"heading\">");
                    html.Append(" <p class=\"left-lb\">");

                    html.Append("<span class=\"circle-icon\"><img src=\"/App_Themes/App/images/icon-store.png\" style=\"height:12px\" alt=\"\"></span>");



                    html.Append("     ID: " + item.ID + "");
                    html.Append("  </p>");
                    html.Append("  <p class=\"right-meta\">Ngày YCXK: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</span></p>");
                    html.Append(" </div>");
                    html.Append("  <div class=\"smr\">");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Tổng số kiện:</p>");
                    html.Append("   <p>" + item.TotalPackage + "</p>");
                    html.Append("  </div>");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tổng số Kg:</p>");
                    html.Append("  <p>" + item.TotalWeight + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tổng tiền:</p>");
                    html.Append("  <p>" + string.Format("{0:N0}", item.TotalPriceVND) + " VNĐ</p>");
                    html.Append(" </div>");

                    string shippintType = "";
                    if (item.ShippingTypeInVNID != null)
                    {
                        var sht = ShippingTypeVNController.GetByID(item.ShippingTypeInVNID.ToString().ToInt(0));
                        if (sht != null)
                        {
                            shippintType = sht.ShippingTypeVNName;
                        }
                    }
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">HTVC:</p>");
                    html.Append("   <p>" + shippintType + "</p>");
                    html.Append(" </div>");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Trạng thái TT:</p>");
                    html.Append("   <p>" + PJUtils.GeneralTransportationOrderNewStatusApp(Convert.ToInt32(item.Status)) + "</p>");
                    html.Append(" </div>");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Trạng thái XK:</p>");
                    html.Append("   <p>" + PJUtils.GeneralTransportationOrderNewStatusApp(Convert.ToInt32(item.Status)) + "</p>");
                    html.Append(" </div>");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ghi chú:</p>");
                    html.Append("   <p>" + item.Note + "</p>");
                    html.Append(" </div>");

                    html.Append("  </div>");
                    if (item.Status != 2)
                        html.Append("<a href=\"javascript:;\" onclick=\"Pay($(this),'" + item.ID + "')\" class=\"btn cam-btn fw-btn\">Thanh toán</a>");

                    html.Append(" </div>");

                    html.Append(" </div>");
                    html.Append("</div>");
                }
                ltrTotal.Text = html.ToString();
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
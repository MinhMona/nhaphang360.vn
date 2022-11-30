using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NHST.Controllers.MainOrderController;

namespace NHST
{
    public partial class don_hang_app : System.Web.UI.Page
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
                    int status = Request.QueryString["stt"].ToInt(-1);
                    int rmd = 1;
                    if (Request.QueryString["stt"] != null)
                        ddlStatus.SelectedValue = status.ToString();
                    if (status == 11)
                    {
                        rmd = 0;
                        status = -1;
                    }

                    string fd = Request.QueryString["fd"];
                    string td = Request.QueryString["td"];

                    ViewState["UID"] = UID;
                    ViewState["Key"] = Key;
                    pnMobile.Visible = true;
                    //var main = MainOrderController.GetAllByUIDOrderCodeNotHidden_SqlHelper(UID, 1);
                    var main = MainOrderController.GetAllByUIDNotHidden_SqlHelper(UID, status, fd, td, 1);

                    if (main.Count > 0)
                    {
                        pagingall(main);
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
        public void pagingall(List<mainorder> acs)
        {
            int PageSize = 10;
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
                int UID = Convert.ToInt32(ViewState["UID"]);
                string Key = ViewState["Key"].ToString();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    html.Append("  <div class=\"content_page\">");
                    html.Append("   <p class=\"title_onpage\"><span class=\"left_title\">ID:" + item.ID + "</span><span class=\"right_title\">" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</span></p>");
                    html.Append(" <div class=\"content_create_order\">");
                    html.Append(" <div class=\"bottom_order infor_order_group\">");
                    html.Append(" <ul class=\"infor_order\">");
                    html.Append("      <li class=\"infor_order_img\">");
                    html.Append("      <img src=\"" + item.anhsanpham + "\"></li> ");
                    html.Append("   <li class=\"infor_order_content\">");
                    html.Append("    <ul class=\"item_infor_order\">");
                    html.Append("  <li>");
                    html.Append("        <p class=\"title_order_column\">Tên shop:</p>");
                    html.Append("   </li>");
                    html.Append("  <li>");
                    html.Append("   <p class=\"value_order_column title_shop\">" + item.ShopName + "</p>");
                    html.Append("  </li>");
                    html.Append("   </ul>");
                    html.Append("   <ul class=\"item_infor_order\">");
                    html.Append("    <li>");
                    html.Append("  <p class=\"title_order_column\">Website:</p>");
                    html.Append("   </li>");
                    html.Append("     <li>");
                    html.Append("        <p class=\"value_order_column title_web\">" + item.Site + "</p>");
                    html.Append("     </li>");
                    html.Append("  </ul>");
                    #region lấy tất cả kiện
                    html.Append("  <ul class=\"item_infor_order\">");
                    html.Append("    <li>");
                    html.Append("     <p class=\"title_order_column\">Mã vận đơn:</p>");
                    html.Append("    </li>");
                    html.Append("       <li>");
                    var smallpackages = SmallPackageController.GetByMainOrderID(item.ID);
                    if (smallpackages.Count > 0)
                    {
                        foreach (var s in smallpackages)
                        {
                            double weigthQD = 0;
                            double pDai = Convert.ToDouble(s.Length);
                            double pRong = Convert.ToDouble(s.Width);
                            double pCao = Convert.ToDouble(s.Height);
                            if (pDai > 0 && pRong > 0 && pCao > 0)
                            {
                                weigthQD = (pDai * pRong * pCao) / 7000;
                            }
                            double cantinhtien = weigthQD;
                            if (Convert.ToDouble(s.Weight) > weigthQD)
                            {
                                cantinhtien = Convert.ToDouble(s.Weight);
                            }
                            html.Append("   <p class=\"value_order_column\">" + s.OrderTransactionCode + "</p>");
                        }
                    }
                    html.Append("   </li>");
                    html.Append("    </ul>");
                    #endregion
                    html.Append("   <ul class=\"item_infor_order\">");
                    html.Append("  <li>");
                    html.Append("    <p class=\"title_order_column\">Tổng tiền:</p>");
                    html.Append("   </li>");
                    html.Append("   <li>");
                    html.Append("         <p class=\"value_order_column title_money\">" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</p>");
                    html.Append("     </li>");
                    html.Append("  </ul>");
                    html.Append("  <ul class=\"item_infor_order\">");
                    html.Append("  <li>");
                    html.Append("      <p class=\"title_order_column\">Số tiền đã cọc:</p>");
                    html.Append("   </li>");
                    html.Append("   <li>");
                    html.Append("    <p class=\"value_order_column title_coc\">" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</p>");
                    html.Append("   </li>");
                    html.Append("   </ul>");
                    //html.Append("  <ul class=\"item_infor_order\">");
                    //html.Append("   <li>");
                    //html.Append("        <p class=\"title_order_column\">Mã tracking:</p>");
                    //html.Append("  </li>");
                    //html.Append("   <li>");
                    //html.Append("  <p class=\"value_order_column title_web\">" + item.KUAIDI + "</p>");
                    //html.Append("   </li>");
                    //html.Append("    </ul>");
                    html.Append("    <ul class=\"item_infor_order\">");
                    html.Append("   <li>");
                    html.Append("   <p class=\"title_order_column\">Ngày đặt hàng:</p>");
                    html.Append("    </li>");
                    html.Append("   <li>");
                    html.Append("         <p class=\"value_order_column title_date\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</p>");
                    html.Append("     </li>");
                    html.Append("   </ul>");
                    html.Append("  <ul class=\"item_infor_order  class_end_list\">");
                    html.Append("    <li>");
                    html.Append("     <p class=\"title_order_column\">Trạng thái:</p>");
                    html.Append("    </li>");
                    html.Append("   <li>");
                    html.Append("   <p class=\"value_order_column\">" + PJUtils.IntToRequestAdmin(item.Status) + "</p>");
                    html.Append("   </li>");
                    html.Append("    </ul>");
                    html.Append("    </li>");
                    html.Append("   </ul>");
                    html.Append("  <p class=\"btn_order_group\">");
                    if (item.Status == 0)
                    {
                        html.Append(" <a class=\"btn_ordersp btn_pay\" onclick=\"depositOrder('" + item.ID + "')\">Đặt cọc</a>");
                    }
                    if (item.Status > 6)
                    {
                        html.Append(" <a href=\"/them-khieu-nai-app.aspx?UID=" + UID + "&o=" + item.ID + "&Key=" + Key + "\" class=\"btn_ordersp btn_plain\">Khiếu nại</a>");
                    }
                    html.Append("  <a href=\"/chi-tiet-don-hang-app.aspx?UID=" + UID + "&OrderID=" + item.ID + "&Key=" + Key + "\" class=\"btn_ordersp\">Chi tiết</a>");
                    html.Append(" </p>");
                    html.Append("   </div>");
                    html.Append("   </div>");
                    html.Append("  </div>");
                }
                ltrOrder.Text = html.ToString();
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
                output.Append("<a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a>");
                output.Append("<a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><</a>");
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
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
                //output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {

                    output.Append("<span class=\"current\">" + i.ToString() + "</span>");
                    //output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                    //output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
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
                //output.Append("<a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a>");
                output.Append("<a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">></a>");
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                output.Append("<a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            var obj_user = AccountController.GetByID(UID);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                if (obj_user.Wallet > 0)
                {
                    int OID = hdfOrderID.Value.ToInt();
                    if (OID > 0)
                    {
                        int uid = obj_user.ID;
                        var o = MainOrderController.GetAllByUIDAndID(uid, OID);
                        if (o != null)
                        {
                            double orderdeposited = 0;
                            double amountdeposit = 0;

                            if (o.Deposit.ToFloat(0) > 0)
                                orderdeposited = Math.Round(Convert.ToDouble(o.Deposit), 0);

                            if (o.AmountDeposit.ToFloat(0) > 0)
                                amountdeposit = Math.Round(Convert.ToDouble(o.AmountDeposit), 0);
                            double custDeposit = amountdeposit - orderdeposited;
                            double userwallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                            //if (userwallet > 0)
                            //{
                                if (userwallet >= custDeposit)
                                {
                                    //Cập nhật lại Wallet User
                                    double wallet = userwallet - custDeposit;
                                    wallet = Math.Round(wallet, 0);
                                    //Cập nhật lại MainOrder                                
                                    //MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    //MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    //HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                    //AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                    //PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);

                                    int st = TransactionController.DepositAll(obj_user.ID, wallet, currentDate, obj_user.Username, o.ID, 2, o.Status.Value, amountdeposit.ToString(), custDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID, 1, 1, 2);
                                    if (st == 1)
                                    {
                                        var setNoti = SendNotiEmailController.GetByID(6);
                                        if (setNoti != null)
                                        {
                                            if (setNoti.IsSentNotiAdmin == true)
                                            {
                                                var admins = AccountController.GetAllByRoleID(0);
                                                if (admins.Count > 0)
                                                {
                                                    foreach (var admin in admins)
                                                    {
                                                        NotificationsController.Inser(admin.ID,
                                                                                           admin.Username, o.ID,
                                                                                           "Đơn hàng " + o.ID + " đã được đặt cọc.",
                                                                                           1, currentDate, obj_user.Username, false);
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {
                                                        NotificationsController.Inser(manager.ID,
                                                                                           manager.Username, o.ID,
                                                                                            "Đơn hàng " + o.ID + " đã được đặt cọc.",
                                                                                           1, currentDate, obj_user.Username, false);
                                                    }
                                                }
                                            }

                                        }
                                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý.", "e", true, Page);
                                    }
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                    "e", true, Page);
                                }
                            //}
                            //else
                            //{
                            //    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                            //        "e", true, Page);
                            //}
                        }

                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                    "e", true, Page);
                }
            }
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            int UID = ViewState["UID"].ToString().ToInt(0);
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";
            string Key = ViewState["Key"].ToString();
            Response.Redirect("/don-hang-app.aspx?UID=" + UID + "&stt=" + status + "&fd=" + fd + "&td=" + td + "&Key=" + Key + "");
        }
    }
}
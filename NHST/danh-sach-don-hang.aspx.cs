using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class danh_sach_don_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                HttpContext.Current.Session["ListDep"] = null;
                LoadData();
            }
        }

        public void LoadData()
        {
            int t = Request.QueryString["t"].ToInt(1);
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double Wallet = Convert.ToDouble(obj_user.Wallet);
                ltrWallet.Text = string.Format("{0:N0}", Wallet) + " VNĐ";

                double tongsodonhang = 0;
                double tongtrigiadonhang = 0;
                double tongtienlayhang = 0;
                double TienConLai = 0;
                double tongtienhangchuagiao = 0;
                double Tongtienhangcandatcoc = 0;
                double Tongtienhang = 0;
                double Tongtienhangchovekhotq = 0;
                double Tongtienhangdavekhotq = 0;
                double Tongtienhangdangokhovn = 0;

                double order_stt0 = 0;
                double order_stt2 = 0;
                double order_stt5 = 0;
                double order_stt6 = 0;
                double order_stt7 = 0;
                double order_stt10 = 0;

                string se = Request.QueryString["s"];
                int typesearch = Request.QueryString["l"].ToInt(0);
                int status = Request.QueryString["stt"].ToInt(-1);
                string fd = Request.QueryString["fd"];
                string td = Request.QueryString["td"];

                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                    FD.Text = fd;
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    TD.Text = td;

                int page = 0;
                Int32 Page = GetIntFromQueryString("Page");
                if (Page > 0)
                {
                    page = Page - 1;
                }

                txtSearhc.Text = se;
                ddlType.SelectedValue = typesearch.ToString();
                ViewState["t"] = t.ToString();

                List<MainOrderController.mainorder> tos = new List<MainOrderController.mainorder>();

                var os = MainOrderController.GetAllByUIDAndOrderType(UID, t);
                tos = MainOrderController.GetAllByUIDNotHidden_SqlHelperNew(UID, se, typesearch, status, fd, td, t, page);

                if (tos.Count > 0)
                {
                    var orderstt0 = os.Where(od => od.Status == 0).ToList();
                    var orderstt2 = os.Where(od => od.Status == 2).ToList();
                    var orderstt5 = os.Where(od => od.Status == 5).ToList();
                    var orderstt6 = os.Where(od => od.Status == 6).ToList();
                    var orderstt7 = os.Where(od => od.Status == 7).ToList();
                    var orderstt10 = os.Where(od => od.Status == 10).ToList();

                    var totalorderchuagiao = os.Where(od => od.Status == 2 || od.Status == 5 || od.Status == 6 || od.Status == 7).ToList();
                    if (totalorderchuagiao.Count > 0)
                    {
                        foreach (var item in totalorderchuagiao)
                        {
                            tongtienhangchuagiao += Convert.ToDouble(item.TotalPriceVND);
                        }
                    }

                    #region New
                    Tongtienhangcandatcoc = MainOrderController.GetTotalPrice(UID, 0, "AmountDeposit", t);
                    Tongtienhang = MainOrderController.GetTotalPrice(UID, 0, "TotalPriceVND", 1);
                    Tongtienhangchovekhotq = MainOrderController.GetTotalPrice(UID, 5, "TotalPriceVND", t);
                    Tongtienhangdavekhotq = MainOrderController.GetTotalPrice(UID, 6, "TotalPriceVND", t);
                    Tongtienhangdangokhovn = MainOrderController.GetTotalPrice(UID, 7, "TotalPriceVND", t);
                    #endregion

                    order_stt0 = orderstt0.Count;
                    order_stt2 = orderstt2.Count;
                    order_stt5 = orderstt5.Count;
                    order_stt6 = orderstt6.Count;
                    order_stt7 = orderstt7.Count;
                    order_stt10 = orderstt10.Count;

                    tongsodonhang = os.Count;
                    var order_stt2morer = os.Where(od => od.Status >= 2).ToList();
                    foreach (var o in order_stt2morer)
                    {
                        tongtrigiadonhang += Convert.ToDouble(o.TotalPriceVND);
                    }


                    double totalall7 = MainOrderController.GetTotalPrice(UID, 7, "TotalPriceVND", t);
                    double totalall7_deposit = MainOrderController.GetTotalPrice(UID, 7, "Deposit", t);
                    tongtienlayhang = totalall7 - totalall7_deposit;

                    //DateTime checkdate = DateTime.Now;
                    //var ts = tos.Where(x => x.Status == 0).ToList();
                    //foreach (var item in ts)
                    //{
                    //    if (item.Status == 0)
                    //    {
                    //        DateTime CreatedDate = Convert.ToDateTime(item.CreatedDate);
                    //        TimeSpan span = checkdate.Subtract(CreatedDate);
                    //        if (span.Days > 7)
                    //        {
                    //            MainOrderController.UpdateIsHiddenTrue(item.ID);
                    //        }
                    //    }
                    //}
                    //mở ra rồi nhập giá vip vào, đã trùng với db 
                    #region Update Level User
                    //var ts1 = tos.Where(w => w.Status == 10).ToList();
                    //foreach (var item in ts1)
                    //{
                    //    #region Update User Level
                    //    if (item.Status == 10)
                    //    {
                    //        int cusID = UID.ToString().ToInt(0);
                    //        var cust = AccountController.GetByID(cusID);
                    //        if (cust != null)
                    //        {
                    //            double totalpay = MainOrderController.GetTotalPrice_Thang(cust.ID, 10, "TotalPriceVND");
                    //            if (totalpay >= 0 && totalpay < 100000000)
                    //            {
                    //                if (cust.LevelID == 1)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 1, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 100000000 && totalpay < 300000000)
                    //            {
                    //                if (cust.LevelID == 1)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 2, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 300000000 && totalpay < 800000000)
                    //            {
                    //                if (cust.LevelID <= 2)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 3, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 800000000 && totalpay < 1500000000)
                    //            {
                    //                if (cust.LevelID <= 3)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 4, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 1500000000 && totalpay < 2500000000)
                    //            {
                    //                if (cust.LevelID <= 4)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 5, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 2500000000 && totalpay < 5000000000)
                    //            {
                    //                if (cust.LevelID <= 5)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 11, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 5000000000 && totalpay < 10000000000)
                    //            {
                    //                if (cust.LevelID <= 11)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 12, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 10000000000 && totalpay < 20000000000)
                    //            {
                    //                if (cust.LevelID <= 12)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 13, currentDate, cust.Username);
                    //                }
                    //            }
                    //            else if (totalpay >= 20000000000)
                    //            {
                    //                if (cust.LevelID <= 13)
                    //                {
                    //                    AccountController.updateLevelID(cusID, 14, currentDate, cust.Username);
                    //                }
                    //            }
                    //            AccountController.updateTienTichLuy(cusID, totalpay.ToString());
                    //        }
                    //    }
                    //    #endregion
                    //}
                    #endregion 

                    //Ghi ra 
                    ltrAllOrderCount.Text = string.Format("{0:N0}", tongsodonhang).Replace(",", ".");
                    //ltrAllOrderPrice.Text = string.Format("{0:N0}", tongtrigiadonhang).Replace(",", ".");
                    //ltrTotalGetAllProduct.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");

                    ltrTongtienhangchuagiao.Text = string.Format("{0:N0}", tongtienhangchuagiao).Replace(",", ".");
                    ltrTongtienhangcandatcoc.Text = string.Format("{0:N0}", Tongtienhangcandatcoc).Replace(",", ".");
                    ltrTongtienhangchovekhotq.Text = string.Format("{0:N0}", Tongtienhangchovekhotq).Replace(",", ".");
                    ltrTongtienhangdavekhotq.Text = string.Format("{0:N0}", Tongtienhangdavekhotq).Replace(",", ".");
                    ltrTongtienhangdangokhovn.Text = string.Format("{0:N0}", Tongtienhangdangokhovn).Replace(",", ".");
                    ltrTongtienhangcanthanhtoandelayhang.Text = string.Format("{0:N0}", tongtienlayhang).Replace(",", ".");
                    ltrTongtienhangdatcoc.Text = string.Format("{0:N0}", Tongtienhang).Replace(",", ".");

                    ltrOrderStatus0.Text = string.Format("{0:N0}", order_stt0).Replace(",", ".");
                    ltrOrderStatus2.Text = string.Format("{0:N0}", order_stt2).Replace(",", ".");
                    ltrOrderStatus5.Text = string.Format("{0:N0}", order_stt5).Replace(",", ".");
                    ltrOrderStatus6.Text = string.Format("{0:N0}", order_stt6).Replace(",", ".");
                    ltrOrderStatus7.Text = string.Format("{0:N0}", order_stt7).Replace(",", ".");
                    ltrOrderStatus10.Text = string.Format("{0:N0}", order_stt10).Replace(",", ".");


                    double vidientu = Convert.ToDouble(obj_user.Wallet);

                    TienConLai = vidientu - tongtienlayhang;

                    ltrTienConLai.Text = string.Format("{0:N0}", TienConLai) + " VNĐ";


                    //JavaScriptSerializer serializer = new JavaScriptSerializer();
                    //var listDep = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                    //if (listDep != null)
                    //{
                    //    if (listDep.Count > 0)
                    //    {
                    //        hdfShowDep.Value = serializer.Serialize(listDep);
                    //    }
                    //    else
                    //    {
                    //        hdfShowDep.Value = serializer.Serialize(listDep);
                    //    }    
                    //}
                    //else
                    //{
                    //    hdfShowDep.Value = serializer.Serialize(listDep);
                    //}    

                    //var listPay = HttpContext.Current.Session["ListPay"] as List<PayAll>;
                    //if (listPay != null)
                    //{
                    //    if (listPay.Count > 0)
                    //    {

                    //        hdfShowPay.Value = serializer.Serialize(listPay);
                    //    }
                    //}

                    //var listYCG = HttpContext.Current.Session["ListYCG"] as List<YCG>;
                    //if (listYCG != null)
                    //{
                    //    if (listYCG.Count > 0)
                    //    {

                    //        hdfShowYCG.Value = serializer.Serialize(listYCG);
                    //    }
                    //}

                    int total = MainOrderController.GetTotalItem(UID, status, fd, td, t);

                    pagingall(tos.OrderByDescending(x => x.ID).ToList(), total);
                }
            }
        }


        #region Paging
        public void pagingall(List<MainOrderController.mainorder> acs, int total)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int PageSize = 15;
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

                    StringBuilder html = new StringBuilder();
                    var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                    for (int i = 0; i < acs.Count; i++)
                    {
                        var item = acs[i];
                        int status = Convert.ToInt32(item.Status);
                        double deposited = 0;

                        if (item.Deposit.ToFloat(0) > 0)
                            deposited = Convert.ToDouble(item.Deposit);

                        double must_Deposit = Convert.ToDouble(item.AmountDeposit);
                        double must_Deposit_left = must_Deposit - deposited;

                        if (status == 0)
                        {
                            if (list != null)
                            {
                                var check = list.Where(x => x.MainOrderID == item.ID).SingleOrDefault();
                                if (check != null)
                                {
                                    html.Append("<td><label><input type=\"checkbox\" checked onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-total=\"" + item.TotalPriceVND + "\" data-id=\"" + item.ID + "\"><span></span></label></td>");

                                }
                                else
                                {
                                    html.Append("<td><label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-total=\"" + item.TotalPriceVND + "\"  data-id=\"" + item.ID + "\"><span></span></label></td>");
                                }
                            }
                            else
                            {
                                html.Append("<td><label><input type=\"checkbox\" onchange=\"CheckDepAll(" + item.ID + "," + Math.Round(must_Deposit_left, 0) + ")\" data-value=\"" + Math.Round(must_Deposit_left, 0) + "\" data-total=\"" + item.TotalPriceVND + "\"  data-id=\"" + item.ID + "\"><span></span></label></td>");
                            }
                        }
                        else
                        {
                            html.Append("<td></td>");
                        }
                        html.Append("<td>" + item.ID + "</td>");
                        html.Append("<td><img class=\"materialboxed\" src=\"" + item.anhsanpham + "\" alt=\"\" width=\"75\" /></td>");
                        html.Append("<td>" + item.TotalLink + "</td>");
                        html.Append("<td>" + item.Site + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.AmountDeposit)) + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + "</td>");
                        html.Append("<td>");
                        html.Append(item.Created);
                        html.Append(item.DepostiDate);
                        html.Append(item.DateBuy);
                        html.Append(item.DateSite);
                        html.Append(item.DateTQ);
                        html.Append(item.DateVN);
                        html.Append(item.DatePay);
                        html.Append(item.CompleteDate);
                        html.Append("</td>");

                        if (item.OrderType == 3)
                        {
                            if (item.IsCheckNotiPrice == false)
                            {
                                html.Append("<td class=\"no-wrap\"><span class=\"badge yellow-gold darken-2 white-text border-radius-2\">Chờ báo giá</span></td>");
                            }
                            else
                            {
                                html.Append("<td class=\"no-wrap\">" + PJUtils.IntToRequestAdminNew(status) + "</td>");
                            }
                        }
                        else
                        {
                            html.Append("<td class=\"no-wrap\">" + PJUtils.IntToRequestAdminNew(status) + "</td>");
                        }

                        html.Append("<td>");
                        html.Append("<div class=\"action-table\">");
                        html.Append("     <a href=\"/chi-tiet-don-hang/" + item.ID + "\" data-position=\"top\" ><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a>");
                        if (item.Status > 6)
                        {
                            html.Append("     <a href=\"/them-khieu-nai/" + item.ID + "\" data-position=\"top\"><i class=\"material-icons\">report</i><span>Khiếu nại</span></a>");
                        }
                        if (item.OrderType == 3)
                        {
                            if (item.IsCheckNotiPrice == false)
                            {

                            }
                            else
                            {
                                if (item.Status == 0)
                                {
                                    //ltr.Text += "    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-green\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>";
                                    html.Append("    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "',$(this))\" data-position=\"top\" ><i class=\"material-icons\">attach_money</i><span>Đặt cọc</span></a>");
                                    html.Append("    <a href=\"javascript:;\" onclick=\"CancelOrder('" + item.ID + "')\" data-position=\"top\" ><i class=\"material-icons\">delete</i><span>Hủy đơn</span></a>");
                                }
                            }
                        }
                        else
                        {
                            if (item.Status == 0)
                            {
                                //ltr.Text += "    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-green\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>";
                                html.Append("    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "',$(this))\" data-position=\"top\"><i class=\"material-icons\">attach_money</i><span>Đặt cọc</span></a>");
                                html.Append("    <a href=\"javascript:;\" onclick=\"CancelOrder('" + item.ID + "')\" data-position=\"top\" ><i class=\"material-icons\">delete</i><span>Hủy đơn</span></a>");
                            }
                        }

                        //Hiển thị nút thanh toán
                        double userdadeposit = 0;
                        if (item.Deposit != null)
                            userdadeposit = Math.Round(Convert.ToDouble(item.Deposit), 0);

                        double feewarehouse = 0;
                        if (item.FeeInWareHouse != null)
                            feewarehouse = Math.Round(Convert.ToDouble(item.FeeInWareHouse), 0);
                        double totalPrice = Math.Round(Convert.ToDouble(item.TotalPriceVND), 0);
                        double totalPay = totalPrice + feewarehouse;
                        double totalleft = totalPay - userdadeposit;

                        if (totalleft > 0)
                        {
                            if (item.Status > 6)
                            {
                                html.Append("    <a href=\"javascript:;\" onclick=\"payallorder('" + item.ID + "',$(this))\" data-position=\"top\"><i class=\"material-icons\">payment</i><span>Thanh toán</span></a>");
                            }
                        }

                        html.Append("   </div>");
                        html.Append("  </td>");
                        html.Append("</tr>");
                    }
                    ltr.Text = html.ToString();
                }
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

        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string TotalPriceVND { get; set; }
            public string AmountDeposit { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            int t = Request.QueryString["t"].ToInt(1);
            string text = txtSearhc.Text;
            string typesear = ddlType.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";

            if (!string.IsNullOrEmpty(FD.Text))
            {
                fd = FD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(TD.Text))
            {
                td = TD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(fd) && !string.IsNullOrEmpty(td))
            {
                Response.Redirect("/danh-sach-don-hang?t=" + t + "&s=" + text + "&l=" + typesear + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
            }
            else
            {
                Response.Redirect("/danh-sach-don-hang?t=" + t + "&s=" + text + "&l=" + typesear + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
            }
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                //if (obj_user.Wallet > 0)
                //{
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

                            #region old Cập nhật lại MainOrder và wallet user
                            //AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                            //MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                            //MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                            //HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID,
                            //    custDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".",
                            //    wallet, 1, 1, currentDate, obj_user.Username);
                            //PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, custDeposit, 2, currentDate, obj_user.Username);
                            #endregion

                            int st = TransactionController.DepositAll(obj_user.ID, wallet, currentDate, obj_user.Username, o.ID, 2, o.Status.Value, amountdeposit.ToString(), custDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID, 1, 1, 2);
                            if (st == 1)
                            {
                                var wh = WarehouseController.GetByID(Convert.ToInt32(o.ReceivePlace));
                                if (wh != null)
                                {
                                    var ExpectedDate = currentDate.AddDays(Convert.ToInt32(wh.ExpectedDate));
                                    MainOrderController.UpdateExpectedDate(o.ID, ExpectedDate);
                                }
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
                                                NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", 1, currentDate, obj_user.Username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", datalink);
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {


                                                NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.",
                                                1, currentDate, obj_user.Username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                PJUtils.PushNotiDesktop(manager.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", datalink);
                                            }
                                        }
                                    }

                                    if (setNoti.IsSentEmailAdmin == true)
                                    {
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new(admin.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                }
                                                catch { }
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new(manager.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                }
                                                catch { }
                                            }
                                        }

                                    }
                                }
                                PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                            }
                            else
                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý.", "e", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, "/chuyen-muc/huong-dan/nap-tien", Page);
                        }
                        //}
                        //else
                        //{
                        //    PJUtils.ShowMessageBoxSwAlertBackToLink("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, "/chuyen-muc/huong-dan/nap-tien", Page);
                        //}
                    }

                }
                //}
                //else
                //{
                //    PJUtils.ShowMessageBoxSwAlertBackToLink("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, "/chuyen-muc/huong-dan/nap-tien", Page);
                //}
            }
        }

        protected void btnDepositSelected1_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);
                var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        double totalMustPay = 0;
                        foreach (var item in list)
                        {
                            int orderID = item.MainOrderID;
                            var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                            if (mainOrder != null)
                            {
                                if (mainOrder.Status == 0)
                                {
                                    double Deposited = 0;
                                    double AmountDeposited = 0;
                                    if (mainOrder.Deposit.ToFloat(0) > 0)
                                    {
                                        Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                    }
                                    if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                                    {
                                        AmountDeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                                    }
                                    double mustDeposit = AmountDeposited - Deposited;
                                    if (mustDeposit > 0)
                                    {
                                        totalMustPay += mustDeposit;
                                    }
                                }
                            }
                        }

                        if (wallet >= totalMustPay)
                        {
                            foreach (var item in list)
                            {
                                int orderID = item.MainOrderID;
                                var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                                if (mainOrder != null)
                                {
                                    if (mainOrder.Status == 0)
                                    {
                                        double Deposited = 0;
                                        double AmountDeposited = 0;
                                        if (mainOrder.Deposit.ToFloat(0) > 0)
                                        {
                                            Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                        }
                                        if (mainOrder.AmountDeposit.ToFloat(0) > 0)
                                        {
                                            AmountDeposited = Math.Round(Convert.ToDouble(mainOrder.AmountDeposit), 0);
                                        }
                                        double mustDeposit = AmountDeposited - Deposited;
                                        int UIDOrder = Convert.ToInt32(mainOrder.UID);
                                        var accPay = AccountController.GetByID(UIDOrder);
                                        if (accPay != null)
                                        {
                                            double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                            if (accWallet >= mustDeposit)
                                            {
                                                double walletleft = accWallet - mustDeposit;
                                                walletleft = Math.Round(walletleft, 0);
                                                AccountController.updateWallet(obj_user.ID, walletleft, currentDate, obj_user.Username);
                                                //Cập nhật lại MainOrder                                
                                                MainOrderController.UpdateStatus(mainOrder.ID, obj_user.ID, 2);
                                                MainOrderController.UpdateDeposit(mainOrder.ID, obj_user.ID, AmountDeposited.ToString());
                                                MainOrderController.UpdateDepositDate(mainOrder.ID, currentDate);
                                                HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, mainOrder.ID,
                                                mustDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + mainOrder.ID + ".", walletleft, 1, 1, currentDate, obj_user.Username);
                                                PayOrderHistoryController.Insert(mainOrder.ID, obj_user.ID, 2, mustDeposit, 2, currentDate, obj_user.Username);
                                            }
                                        }
                                    }
                                }
                            }
                            Session["ListDep"] = null;
                            PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "i", true, Page);
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để đặt cọc.", "i", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để đặt cọc.", "i", true, Page);
                }
                //if (wallet > 0)
                //{

                //}
                //else
                //{
                //    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "i", true, Page);
                //}
            }
        }

        protected void btnPayAlllSelected_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                double wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                if (wallet > 0)
                {
                    var list = HttpContext.Current.Session["ListPay"] as List<PayAll>;
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            double totalMustPay = 0;
                            foreach (var item in list)
                            {
                                int orderID = item.MainOrderID;
                                var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                                if (mainOrder != null)
                                {
                                    if (mainOrder.Status == 7)
                                    {
                                        double Deposited = 0;
                                        if (mainOrder.Deposit.ToFloat(0) > 0)
                                        {
                                            Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                        }

                                        double feewarehouse = 0;
                                        if (mainOrder.FeeInWareHouse.ToString().ToFloat(0) > 0)
                                            feewarehouse = Math.Round(Convert.ToDouble(mainOrder.FeeInWareHouse), 0);

                                        double totalPriceVND = 0;
                                        if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                            totalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);
                                        double moneyleft = Math.Round((totalPriceVND + feewarehouse) - Deposited, 0);

                                        if (moneyleft > 0)
                                        {
                                            totalMustPay += moneyleft;
                                        }
                                    }
                                }
                            }

                            if (wallet >= totalMustPay)
                            {
                                foreach (var item in list)
                                {
                                    int orderID = item.MainOrderID;
                                    var mainOrder = MainOrderController.GetAllByUIDAndID(UID, orderID);
                                    if (mainOrder != null)
                                    {
                                        if (mainOrder.Status == 7)
                                        {
                                            double Deposited = 0;
                                            if (mainOrder.Deposit.ToFloat(0) > 0)
                                            {
                                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);
                                            }

                                            double feewarehouse = 0;
                                            if (mainOrder.FeeInWareHouse.ToString().ToFloat(0) > 0)
                                                feewarehouse = Math.Round(Convert.ToDouble(mainOrder.FeeInWareHouse), 0);

                                            double totalPriceVND = 0;
                                            if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                                totalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);
                                            double moneyleft = Math.Round((totalPriceVND + feewarehouse) - Deposited, 0);

                                            int UIDOrder = Convert.ToInt32(mainOrder.UID);
                                            var accPay = AccountController.GetByID(UIDOrder);
                                            if (accPay != null)
                                            {
                                                double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                                if (accWallet >= moneyleft)
                                                {
                                                    double walletLeft = Math.Round(accWallet - moneyleft, 0);
                                                    double payalll = Math.Round(Deposited + moneyleft, 0);
                                                    walletLeft = Math.Round(walletLeft, 0);
                                                    MainOrderController.UpdateStatus(mainOrder.ID, UIDOrder, 9);
                                                    int statusOOld = Convert.ToInt32(mainOrder.Status);
                                                    int statusONew = 9;
                                                    //if (statusONew != statusOOld)
                                                    //{
                                                    //    StatusChangeHistoryController.Insert(mainOrder.ID, statusOOld, statusONew, currentDate, username_current);
                                                    //}
                                                    AccountController.updateWallet(UIDOrder, walletLeft, currentDate, accPay.Username);

                                                    HistoryOrderChangeController.Insert(mainOrder.ID, UIDOrder, accPay.Username, accPay.Username +
                                                                " đã đổi trạng thái của đơn hàng ID là: " + mainOrder.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                                    HistoryPayWalletController.Insert(UIDOrder, accPay.Username, mainOrder.ID, moneyleft, accPay.Username + " đã thanh toán đơn hàng: " + mainOrder.ID + ".", walletLeft, 1, 3, currentDate, accPay.Username);
                                                    MainOrderController.UpdateDeposit(mainOrder.ID, UIDOrder, payalll.ToString());
                                                    PayOrderHistoryController.Insert(mainOrder.ID, UIDOrder, 9, moneyleft, 2, currentDate, accPay.Username);

                                                    var wh = WarehouseController.GetByID(Convert.ToInt32(mainOrder.ReceivePlace));
                                                    if (wh != null)
                                                    {
                                                        var ExpectedDate = currentDate.AddDays(Convert.ToInt32(wh.ExpectedDate));
                                                        MainOrderController.UpdateExpectedDate(mainOrder.ID, ExpectedDate);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                                Session["ListPay"] = null;
                                PJUtils.ShowMessageBoxSwAlert("Thanh toán đơn hàng thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để thanh toán đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                                      "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để thanh toán.", "e", true, Page);
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng được chọn để thanh toán.", "e", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để thanh toán đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                                   "e", true, Page);
                }
            }
        }


        public class DepAll
        {
            public int MainOrderID { get; set; }
            public double TotalDeposit { get; set; }
            public double Total { get; set; }
        }

        public class PayAll
        {
            public int MainOrderID { get; set; }
            public double TotalPricePay { get; set; }
        }

        public class YCG
        {
            public int MainOrderID { get; set; }
        }

        [WebMethod]
        public static string CheckDepAll(int MainOrderID, string TotalPrice)
        {
            List<DepAll> ldep = new List<DepAll>();
            var mainOrder = MainOrderController.GetAllByID(MainOrderID);
            var list = HttpContext.Current.Session["ListDep"] as List<DepAll>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var check = list.Where(x => x.MainOrderID == MainOrderID).FirstOrDefault();
                    if (check != null)
                    {
                        list.Remove(check);
                    }
                    else
                    {
                        DepAll d = new DepAll();
                        d.MainOrderID = MainOrderID;
                        d.Total = Convert.ToDouble(mainOrder.TotalPriceVND);
                        d.TotalDeposit = Convert.ToDouble(TotalPrice);
                        list.Add(d);
                    }
                }
                else
                {
                    DepAll d = new DepAll();
                    d.MainOrderID = MainOrderID;
                    d.Total = Convert.ToDouble(mainOrder.TotalPriceVND);
                    d.TotalDeposit = Convert.ToDouble(TotalPrice);
                    list.Add(d);
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(list);
            }
            else
            {
                DepAll d = new DepAll();
                d.MainOrderID = MainOrderID;
                d.Total = Convert.ToDouble(mainOrder.TotalPriceVND);
                d.TotalDeposit = Convert.ToDouble(TotalPrice);
                ldep.Add(d);
                HttpContext.Current.Session["ListDep"] = ldep;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(ldep);
            }
        }

        [WebMethod]
        public static string CheckPayAll(int MainOrderID, string TotalPricePay)
        {
            List<PayAll> lpay = new List<PayAll>();
            var list = HttpContext.Current.Session["ListPay"] as List<PayAll>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var check = list.Where(x => x.MainOrderID == MainOrderID).FirstOrDefault();
                    if (check != null)
                    {
                        list.Remove(check);
                    }
                    else
                    {
                        PayAll d = new PayAll();
                        d.MainOrderID = MainOrderID;
                        d.TotalPricePay = Convert.ToDouble(TotalPricePay);
                        list.Add(d);
                    }
                }
                else
                {
                    PayAll d = new PayAll();
                    d.MainOrderID = MainOrderID;
                    d.TotalPricePay = Convert.ToDouble(TotalPricePay);
                    list.Add(d);
                    // HttpContext.Current.Session["ListPay"] = lpay;
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(list);
            }
            else
            {
                PayAll d = new PayAll();
                d.MainOrderID = MainOrderID;
                d.TotalPricePay = Convert.ToDouble(TotalPricePay);
                lpay.Add(d);
                HttpContext.Current.Session["ListPay"] = lpay;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(lpay);
            }
        }

        [WebMethod]
        public static string CheckYCGAll(int MainOrderID)
        {
            List<YCG> lYCG = new List<YCG>();
            var list = HttpContext.Current.Session["ListYCG"] as List<YCG>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var check = list.Where(x => x.MainOrderID == MainOrderID).FirstOrDefault();
                    if (check != null)
                    {
                        list.Remove(check);
                    }
                    else
                    {
                        YCG d = new YCG();
                        d.MainOrderID = MainOrderID;
                        list.Add(d);
                    }
                }
                else
                {
                    YCG d = new YCG();
                    d.MainOrderID = MainOrderID;
                    list.Add(d);
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(list);
            }
            else
            {
                YCG d = new YCG();
                d.MainOrderID = MainOrderID;
                lYCG.Add(d);
                HttpContext.Current.Session["ListYCG"] = lYCG;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(lYCG);
            }
        }

        protected void btnYCG_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                var list = HttpContext.Current.Session["ListYCG"] as List<YCG>;
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            var mo = MainOrderController.GetAllByID(item.MainOrderID);
                            if (mo != null)
                            {
                                var check = YCGController.GetByMainOrderID(item.MainOrderID);
                                if (check == null)
                                {
                                    YCGController.Insert(item.MainOrderID, txtFullName.Text, txtPhone.Text, txtAddress.Text, txtNote.Text, username_current, currentDate);
                                    MainOrderController.UpdateYCG(item.MainOrderID, true);

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
                                                    NotificationsController.Inser(admin.ID, admin.Username, item.MainOrderID, "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.", 1, currentDate, obj_user.Username, false);
                                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                    string datalink = "" + strUrl + "manager/OrderDetail/" + item.MainOrderID;
                                                    PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.", datalink);
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {


                                                    NotificationsController.Inser(manager.ID, manager.Username, item.MainOrderID, "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.",
                                                    1, currentDate, obj_user.Username, false);
                                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                    string datalink = "" + strUrl + "manager/OrderDetail/" + item.MainOrderID;
                                                    PJUtils.PushNotiDesktop(manager.ID, "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.", datalink);
                                                }
                                            }
                                        }

                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            var admins = AccountController.GetAllByRoleID(0);
                                            if (admins.Count > 0)
                                            {
                                                foreach (var admin in admins)
                                                {
                                                    try
                                                    {
                                                        PJUtils.SendMailGmail_new(admin.Email,
                                                            "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.", "");
                                                    }
                                                    catch { }
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {
                                                    try
                                                    {
                                                        PJUtils.SendMailGmail_new(manager.Email,
                                                            "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + item.MainOrderID + " đã yêu cầu giao hàng.", "");
                                                    }
                                                    catch { }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        Session["ListYCG"] = null;
                        PJUtils.ShowMessageBoxSwAlert("Tạo yêu cầu giao hàng thành công.", "s", true, Page);
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Chưa có đơn hàng nào được chọn, vui lòng thử lại.", "e", false, Page);
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                int id = hdfOrderID.Value.ToInt();
                DateTime currentDate = DateTime.Now;
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double deposit = 0;
                        if (o.Deposit.ToFloat(0) > 0)
                            deposit = Math.Round(Convert.ToDouble(o.Deposit), 0);

                        double wallet = 0;
                        if (obj_user.Wallet.ToString().ToFloat(0) > 0)
                            wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                        double feewarehouse = 0;
                        if (o.FeeInWareHouse.ToString().ToFloat(0) > 0)
                            feewarehouse = Math.Round(Convert.ToDouble(o.FeeInWareHouse), 0);

                        double totalPriceVND = 0;
                        if (o.TotalPriceVND.ToFloat(0) > 0)
                            totalPriceVND = Math.Round(Convert.ToDouble(o.TotalPriceVND), 0);
                        double moneyleft = Math.Round((totalPriceVND + feewarehouse) - deposit, 0);

                        string TienConLai = string.Format("{0:N0}", (wallet - moneyleft)) + " VNĐ";

                        if (wallet >= moneyleft)
                        {
                            double walletLeft = Math.Round(wallet - moneyleft, 0);
                            double payalll = Math.Round(deposit + moneyleft, 0);

                            #region Cập nhật ví và đơn hàng
                            //                MainOrderController.UpdateStatus(o.ID, uid, 9);
                            //                MainOrderController.UpdatePayDate(o.ID, currentDate);
                            //                int statusOOld = Convert.ToInt32(o.Status);
                            //                int statusONew = 9;
                            //                //if (statusONew != statusOOld)
                            //                //{
                            //                //    StatusChangeHistoryController.Insert(o.ID, statusOOld, statusONew, currentDate, obj_user.Username);
                            //                //}
                            //                AccountController.updateWallet(uid, walletLeft, currentDate, username);
                            //                HistoryOrderChangeController.Insert(o.ID, uid, username, username +
                            //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);
                            //                HistoryPayWalletController.Insert(uid, username, o.ID, moneyleft, username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username);
                            //                MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                            //                PayOrderHistoryController.Insert(id, uid, 9, moneyleft, 2, currentDate, username);
                            #endregion

                            int st = TransactionController.PayAll(o.ID, wallet, o.Status.ToString().ToInt(0), uid, currentDate, username, deposit, 1, moneyleft, 1, 3, 2);
                            if (st == 1)
                            {
                                var setNoti = SendNotiEmailController.GetByID(11);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.",
                                                1, currentDate, obj_user.Username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng " + o.ID + " đã được thanh toán.", datalink);
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.",
                                                1, currentDate, obj_user.Username, false);
                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                PJUtils.PushNotiDesktop(manager.ID, "Đơn hàng " + o.ID + " đã được thanh toán.", datalink);
                                            }
                                        }
                                    }

                                    if (setNoti.IsSentEmailAdmin == true)
                                    {
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new(admin.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                }
                                                catch { }
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new(manager.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }

                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau.", "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của bạn không đủ để thanh toán đơn hàng. Vui lòng nạp thêm " + TienConLai + " ", "e", true, Page);
                        }
                    }
                }
            }
        }

        protected void btnDepositAll_Click(object sender, EventArgs e)
        {
            int t = ViewState["t"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                var setNoti = SendNotiEmailController.GetByID(6);
                if (obj_user.Wallet > 0)
                {
                    var lmain = MainOrderController.GetAllByUID_Deposit(obj_user.ID, t);
                    if (lmain.Count > 0)
                    {
                        foreach (var o in lmain)
                        {
                            var user = AccountController.GetByID(obj_user.ID);
                            if (user != null)
                            {
                                if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                                {
                                    double orderdeposited = 0;
                                    double amountdeposit = 0;
                                    if (!string.IsNullOrEmpty(o.Deposit))
                                        orderdeposited = Math.Round(Convert.ToDouble(o.Deposit), 0);
                                    if (!string.IsNullOrEmpty(o.AmountDeposit))
                                        amountdeposit = Math.Round(Convert.ToDouble(o.AmountDeposit), 0);
                                    double custDeposit = amountdeposit - orderdeposited;
                                    double userwallet = Convert.ToDouble(user.Wallet);
                                    //if (userwallet > 0)
                                    //{
                                    if (userwallet >= custDeposit)
                                    {
                                        double wallet = userwallet - custDeposit;
                                        wallet = Math.Round(wallet, 0);
                                        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                        //Cập nhật lại MainOrder                                
                                        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                        MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID,
                                            custDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".",
                                            wallet, 1, 1, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, custDeposit, 2, currentDate, obj_user.Username);

                                        var wh = WarehouseController.GetByID(Convert.ToInt32(o.ReceivePlace));
                                        if (wh != null)
                                        {
                                            var ExpectedDate = currentDate.AddDays(Convert.ToInt32(wh.ExpectedDate));
                                            MainOrderController.UpdateExpectedDate(o.ID, ExpectedDate);
                                        }

                                        if (setNoti != null)
                                        {
                                            if (setNoti.IsSentNotiAdmin == true)
                                            {

                                                var admins = AccountController.GetAllByRoleID(0);
                                                if (admins.Count > 0)
                                                {
                                                    foreach (var admin in admins)
                                                    {
                                                        NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", 1, currentDate, obj_user.Username, false);
                                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                        string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                        PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", datalink);
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {


                                                        NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.",
                                                        1, currentDate, obj_user.Username, false);
                                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                        string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                        PJUtils.PushNotiDesktop(manager.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", datalink);
                                                    }
                                                }
                                            }

                                            if (setNoti.IsSentEmailAdmin == true)
                                            {
                                                var admins = AccountController.GetAllByRoleID(0);
                                                if (admins.Count > 0)
                                                {
                                                    foreach (var admin in admins)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail_new(admin.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail_new(manager.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                        "e", true, Page);
                                    }

                                    PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                                    //}
                                    //else
                                    //{
                                    //    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.",
                                    //        "e", true, Page);
                                    //}
                                }
                            }

                        }
                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc thành công!", "s", true, Page);
                        //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        //PJUtils.ShowMessageBoxSwAlertBackToLink("Đặt cọc thành công!", "s", true, BackLink, Page);
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng nào chờ đặt cọc!", "e", true, Page);
                    }
                }
            }

        }

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            int t = ViewState["t"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                var setNoti = SendNotiEmailController.GetByID(11);
                if (obj_user.Wallet > 0)
                {
                    var lmain = MainOrderController.GetAllByUID_Payall(obj_user.ID, t);
                    if (lmain.Count > 0)
                    {
                        double TotalMustPay = 0;
                        foreach (var o in lmain)
                        {
                            double userdadeposit = Convert.ToDouble(o.Deposit);
                            double feewarehouse = 0;
                            if (o.FeeInWareHouse != null)
                                feewarehouse = Math.Round(Convert.ToDouble(o.FeeInWareHouse), 0);
                            double totalPriceVND = Math.Round(Convert.ToDouble(o.TotalPriceVND), 0);
                            double totalPay = totalPriceVND + feewarehouse;
                            double moneyleft = totalPay - userdadeposit;
                            TotalMustPay += moneyleft;
                        }

                        if (Convert.ToDouble(obj_user.Wallet) >= TotalMustPay)
                        {
                            foreach (var o in lmain)
                            {
                                var user = AccountController.GetByID(obj_user.ID);
                                if (user != null)
                                {
                                    double wallet = 0;
                                    if (user.Wallet.ToString().ToFloat(0) > 0)
                                        wallet = Math.Round(Convert.ToDouble(user.Wallet), 0);
                                    double userdadeposit = Convert.ToDouble(o.Deposit);
                                    double feewarehouse = 0;
                                    if (o.FeeInWareHouse != null)
                                        feewarehouse = Math.Round(Convert.ToDouble(o.FeeInWareHouse), 0);
                                    double totalPriceVND = Math.Round(Convert.ToDouble(o.TotalPriceVND), 0);
                                    double totalPay = totalPriceVND + feewarehouse;
                                    double moneyleft = totalPay - userdadeposit;

                                    if (moneyleft > 0)
                                    {
                                        double deposit = 0;
                                        if (o.Deposit.ToFloat(0) > 0)
                                            deposit = Math.Round(Convert.ToDouble(o.Deposit), 0);

                                        if (wallet > 0)
                                        {
                                            if (wallet >= moneyleft)
                                            {
                                                double walletLeft = Math.Round(wallet - moneyleft, 0);
                                                double payalll = Math.Round(deposit + moneyleft, 0);

                                                MainOrderController.UpdateStatus(o.ID, obj_user.ID, 9);
                                                MainOrderController.UpdatePayDate(o.ID, currentDate);

                                                AccountController.updateWallet(obj_user.ID, walletLeft, currentDate, obj_user.Username);
                                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);
                                                HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, moneyleft, obj_user.Username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, obj_user.Username);
                                                MainOrderController.UpdateDeposit(o.ID, obj_user.ID, payalll.ToString());
                                                PayOrderHistoryController.Insert(o.ID, obj_user.ID, 9, moneyleft, 2, currentDate, obj_user.Username);

                                                if (setNoti != null)
                                                {
                                                    if (setNoti.IsSentNotiAdmin == true)
                                                    {

                                                        var admins = AccountController.GetAllByRoleID(0);
                                                        if (admins.Count > 0)
                                                        {
                                                            foreach (var admin in admins)
                                                            {
                                                                NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.", 1,
                                                                    currentDate, obj_user.Username, false);
                                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                                PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng " + o.ID + " đã được thanh toán.", datalink);
                                                            }
                                                        }

                                                        var managers = AccountController.GetAllByRoleID(2);
                                                        if (managers.Count > 0)
                                                        {
                                                            foreach (var manager in managers)
                                                            {
                                                                NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.",
    1, currentDate, obj_user.Username, false);
                                                                string strPathAndQuery = Request.Url.PathAndQuery;
                                                                string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                                string datalink = "" + strUrl + "manager/OrderDetail/" + o.ID;
                                                                PJUtils.PushNotiDesktop(manager.ID, "Đơn hàng " + o.ID + " đã được thanh toán.", datalink);
                                                            }
                                                        }
                                                    }

                                                    if (setNoti.IsSentEmailAdmin == true)
                                                    {
                                                        var admins = AccountController.GetAllByRoleID(0);
                                                        if (admins.Count > 0)
                                                        {
                                                            foreach (var admin in admins)
                                                            {
                                                                try
                                                                {
                                                                    PJUtils.SendMailGmail_new(admin.Email,
                                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                                }
                                                                catch { }
                                                            }
                                                        }

                                                        var managers = AccountController.GetAllByRoleID(2);
                                                        if (managers.Count > 0)
                                                        {
                                                            foreach (var manager in managers)
                                                            {
                                                                try
                                                                {
                                                                    PJUtils.SendMailGmail_new(manager.Email,
                                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                                }
                                                                catch { }
                                                            }
                                                        }

                                                    }
                                                }
                                            }

                                        }

                                    }
                                }

                            }
                            PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công!", "s", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để Thanh toán tất cả. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.",
                                "e", true, Page);
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng nào chờ thanh toán!", "e", true, Page);
                    }
                }
            }
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                int OID = hdfOrderID.Value.ToInt();
                if (OID > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(UID, OID);
                    if (o != null)
                    {
                        if (o.Status == 0)
                        {
                            if (Convert.ToDouble(o.Deposit) > 0)
                            {
                                double wallet = 0;
                                if (obj_user.Wallet.ToString().ToFloat(0) > 0)
                                    wallet = Convert.ToDouble(obj_user.Wallet);
                                wallet = wallet + Convert.ToDouble(o.Deposit);
                                AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                MainOrderController.UpdateDeposit(o.ID, obj_user.ID, "0");
                            }

                            HistoryOrderChangeController.Insert(o.ID, UID, obj_user.Username, obj_user.Username +
                            " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ đặt cọc, sang: Hủy đơn hàng.", 1, currentDate);

                            string kq = MainOrderController.UpdateStatus(OID, UID, 1);
                            if (kq == "ok")
                                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        }
                    }
                }
            }
        }
    }
}
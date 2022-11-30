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
using MB.Extensions;
using System.Text;
using static NHST.Controllers.MainOrderController;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace NHST.manager
{
    public partial class user_order : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID != 2 && ac.RoleID != 0)
                            Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            int uID = 0;
            int stype = 0;
            double tongtien = 0;
            double tiendacoc = 0;
            double tienchuathanhtoan;
            double tongtienlayhang = 0;
            double tongtienhangcandatcoc = 0;
            double tienconlai = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = int.Parse(Request.QueryString["stype"]);
                ddlType.SelectedValue = stype.ToString();
            }

            string fd = Request.QueryString["fd"];
            if (!string.IsNullOrEmpty(fd))
                rFD.Text = fd;
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(td))
                rTD.Text = td;
            string priceTo = Request.QueryString["priceTo"];
            if (!string.IsNullOrEmpty(priceTo))
                rPriceTo.Text = priceTo;
            string priceFrom = Request.QueryString["priceFrom"];
            if (!string.IsNullOrEmpty(priceFrom))
                rPriceFrom.Text = priceFrom;
            string search = "";

            int hasVMD = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["hasMVD"]))
            {
                hasVMD = Request.QueryString["hasMVD"].ToString().ToInt(0);
                hdfCheckBox.Value = hasVMD.ToString();
            }

            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
            {
                var list = st.Split(',').ToList();

                for (int j = 0; j < list.Count; j++)
                {
                    for (int i = 0; i < ddlStatuss.Items.Count; i++)
                    {
                        var item = ddlStatuss.Items[i];
                        if (item.Value == list[j])
                        {
                            ddlStatuss.Items[i].Selected = true;
                        }
                    }
                }

            }
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                tSearchName.Text = search;
            }
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            if (Request.QueryString["uid"] != null)
            {
                uID = Request.QueryString["uid"].ToInt(0);
            }
            if (uID > 0)
            {
                var total = MainOrderController.GetTotalNewOfDK(uID, search, stype, fd, td, priceFrom, priceTo, st, Convert.ToBoolean(hasVMD));
                var la = MainOrderController.GetByUserIDInSQLHelperWithFilterNew(uID, search, stype, fd, td, priceFrom, priceTo, st, Convert.ToBoolean(hasVMD), page, 10);
                pagingall(la, total);
            }

            double wallet = 0;

            var user = AccountController.GetByID(uID);
            ltrUsername.Text = user.Username;
            ltrWallet.Text = string.Format("{0:N0}", user.Wallet) + " VNĐ";
            wallet = Convert.ToDouble(user.Wallet);

            List<SQLsumtotal> totalprice = new List<SQLsumtotal>();
            totalprice = MainOrderController.GetByUsernameSumTotal(uID);
            foreach (var mo in totalprice)
            {
                tongtien += Convert.ToDouble(mo.tongtienhang);
                tiendacoc += Convert.ToDouble(mo.tongtiencoc);
            }
            tienchuathanhtoan = tongtien - tiendacoc;

            int tongdon = Convert.ToInt32(MainOrderController.CountOrderList(uID).ToString());

            tongtienhangcandatcoc = MainOrderController.GetTotalPriceNotOrderType(uID, 0, "AmountDeposit");
            double totalall7 = MainOrderController.GetTotalPriceNotOrderType(uID, 7, "TotalPriceVND");
            double totalall7_deposit = MainOrderController.GetTotalPriceNotOrderType(uID, 7, "Deposit");

            tongtienlayhang = totalall7 - totalall7_deposit;
            tienconlai = wallet - tongtienlayhang;

            ltrTongTien.Text = string.Format("{0:N0}", tongtien) + " VNĐ";
            ltrTienDaThanhToan.Text = string.Format("{0:N0}", tiendacoc) + " VNĐ";
            ltrTienChuaThanhToan.Text = string.Format("{0:N0}", tienchuathanhtoan) + " VNĐ";
            ltrTongDon.Text = string.Format("{0:N0}", tongdon) + " Đơn";
            ltrTongTienCanCoc.Text = string.Format("{0:N0}", tongtienhangcandatcoc) + " VNĐ";
            ltrTongTienCanThanhToan.Text = string.Format("{0:N0}", tongtienlayhang) + " VNĐ";
            ltrTongTienKhoVN.Text = string.Format("{0:N0}", totalall7) + " VNĐ";
            ltrTienConLai.Text = string.Format("{0:N0}", tienconlai) + " VNĐ";
        }

        public class ListID
        {
            public int MainOrderID { get; set; }
        }
        public class MoneySeleted
        {
            public int Count { get; set; }
            public double Total { get; set; }
            public double MustPay { get; set; }
        }

        [WebMethod]
        public static string CheckStaff(int MainOrderID)
        {
            List<ListID> ldep = new List<ListID>();
            var list = HttpContext.Current.Session["ListStaff"] as List<ListID>;
            var TienCanThanhToan = new MoneySeleted();
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
                        ListID d = new ListID();
                        d.MainOrderID = MainOrderID;
                        list.Add(d);
                    }
                }
                else
                {
                    ListID d = new ListID();
                    d.MainOrderID = MainOrderID;
                    list.Add(d);
                }
            }
            else
            {
                list = new List<ListID>();
                ListID d = new ListID();
                d.MainOrderID = MainOrderID;
                list.Add(d);
            }

            double totalpay = 0;
            double deposited = 0;

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var mainOrder = MainOrderController.GetAllByID(item.MainOrderID);
                    if (mainOrder != null)
                    {
                        if (Convert.ToDouble(mainOrder.TotalPriceVND) > 0)
                            totalpay += Convert.ToDouble(mainOrder.TotalPriceVND);

                        if (Convert.ToDouble(mainOrder.Deposit) > 0)
                            deposited += Convert.ToDouble(mainOrder.Deposit);
                    }
                }
            }

            double mustPay = Math.Round(totalpay - deposited, 0);
            TienCanThanhToan.Count = list.Count;
            TienCanThanhToan.MustPay = mustPay;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            HttpContext.Current.Session["ListStaff"] = list;
            return serializer.Serialize(TienCanThanhToan);
        }

        protected void btnUpdateDeposit_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                if (obj_user.RoleID == 0 || obj_user.RoleID == 2)
                {
                    List<ListID> list = new List<ListID>();
                    if (HttpContext.Current.Session["ListStaff"] != null)
                    {
                        list = (List<ListID>)HttpContext.Current.Session["ListStaff"];
                    }
                    int uID = 0;
                    if (Request.QueryString["uid"] != null)
                    {
                        uID = Request.QueryString["uid"].ToInt(0);
                    }
                    if (uID > 0)
                    {
                        var acc = AccountController.GetByID(uID);
                        double wallet = Math.Round(Convert.ToDouble(acc.Wallet), 0);

                        if (list.Count > 0)
                        {                            
                            double TotalMustPay = 0;      
                            foreach (var item in list)
                            {
                                var mainOrder = MainOrderController.GetAllByUIDAndID(uID, item.MainOrderID);
                                if (mainOrder != null)
                                {
                                    if (mainOrder.Status > 6)
                                    {
                                        double Deposited = 0;
                                        if (mainOrder.Deposit.ToFloat(0) > 0)
                                            Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);

                                        double TotalPriceVND = 0;
                                        if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                            TotalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);

                                        double moneyleft = Math.Round(TotalPriceVND - Deposited, 0);

                                        if (moneyleft > 0)
                                            TotalMustPay += moneyleft;
                                    }
                                }
                            }

                            if (wallet >= TotalMustPay)
                            {
                                foreach (var item in list)
                                {
                                    var mainOrder = MainOrderController.GetAllByUIDAndID(uID, item.MainOrderID);
                                    if (mainOrder != null)
                                    {
                                        if (mainOrder.Status > 6)
                                        {
                                            double Deposited = 0;
                                            if (mainOrder.Deposit.ToFloat(0) > 0)
                                                Deposited = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);

                                            double TotalPriceVND = 0;
                                            if (mainOrder.TotalPriceVND.ToFloat(0) > 0)
                                                TotalPriceVND = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);

                                            double moneyleft = Math.Round(TotalPriceVND - Deposited, 0);

                                            int UIDOrder = Convert.ToInt32(mainOrder.UID);
                                            var accPay = AccountController.GetByID(UIDOrder);
                                            if (accPay != null)
                                            {
                                                double accWallet = Math.Round(Convert.ToDouble(accPay.Wallet), 0);
                                                if (accWallet >= moneyleft)
                                                {
                                                    double walletLeft = Math.Round(accWallet - moneyleft, 0);
                                                    AccountController.updateWallet(UIDOrder, walletLeft, currentDate, obj_user.Username);
                                                    HistoryOrderChangeController.Insert(mainOrder.ID, UIDOrder, obj_user.Username, obj_user.Username + " đã đổi trạng thái của đơn hàng ID là: " + mainOrder.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);
                                                    HistoryPayWalletController.Insert(UIDOrder, obj_user.Username, mainOrder.ID, moneyleft, obj_user.Username + " đã thanh toán đơn hàng: " + mainOrder.ID + ".", walletLeft, 1, 3, currentDate, obj_user.Username);
                                                    MainOrderController.UpdateStatus(mainOrder.ID, UIDOrder, 9);
                                                    MainOrderController.UpdateDeposit(mainOrder.ID, UIDOrder, TotalPriceVND.ToString());
                                                    PayOrderHistoryController.Insert(mainOrder.ID, UIDOrder, 9, moneyleft, 2, currentDate, obj_user.Username);
                                                }
                                            }
                                        }    
                                    }
                                }                                
                                PJUtils.ShowMessageBoxSwAlert("Thanh toán đơn hàng thành công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để thanh toán đơn hàng. Quý khách vui lòng nạp thêm tiền để tiến hành thanh toán.", "e", true, Page);
                            }
                            Session["ListStaff"] = null;
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không có đơn hàng nào được chọn.", "e", true, Page);
                        }
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Bạn đủ quyền thực hiện chức năng này.", "e", true, Page);
                }
            }
        }

        #region Pagging
        public void pagingall(List<OrderGetSQL> acs, int total)
        {
            int PageSize = 10;
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

                var list = HttpContext.Current.Session["ListStaff"] as List<ListID>;

                StringBuilder hcm = new StringBuilder();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];

                    double Deposited = Convert.ToDouble(item.Deposit);
                    double TotalPrice = Convert.ToDouble(item.TotalPriceVND);
                    double MustPay = TotalPrice - Deposited;

                    hcm.Append("<tr>");
                    hcm.Append("<td>");
                    if (item.Status > 6)
                    {
                        if (MustPay > 0)
                        {
                            if (list != null)
                            {
                                var check = list.Where(x => x.MainOrderID == item.ID).SingleOrDefault();
                                if (check != null)
                                {
                                    hcm.Append(" <label><input type=\"checkbox\" checked onchange=\"CheckStaff(" + item.ID + ")\"  data-id=\"" + item.ID + "\"><span></span></label>");
                                }
                                else
                                {
                                    hcm.Append(" <label><input type=\"checkbox\" onchange=\"CheckStaff(" + item.ID + ")\"  data-id=\"" + item.ID + "\"><span></span></label>");
                                }
                            }
                            else
                            {
                                hcm.Append(" <label><input type=\"checkbox\" onchange=\"CheckStaff(" + item.ID + ")\"  data-id=\"" + item.ID + "\"><span></span></label>");
                            }
                        }
                    }
                    hcm.Append("</td>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.anhsanpham + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Math.Round(Convert.ToDouble(item.TotalPriceVND) - Convert.ToDouble(item.Deposit))) + " VNĐ</td>");
                    hcm.Append("<td>" + item.Uname + "</td>");
                    hcm.Append("<td>" + item.dathang + "</td>");
                    hcm.Append("<td>" + item.saler + "</td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");

                    #region lấy tất cả kiện
                    hcm.Append("  <td class=\"item_infor_order\">");
                    var smallpackages = SmallPackageController.GetByMainOrderID(item.ID);
                    if (smallpackages.Count > 0)
                    {
                        foreach (var s in smallpackages)
                        {
                            hcm.Append("   <p class=\"value_order_column\">" + s.OrderTransactionCode + "</p>");
                        }
                    }
                    else
                    {
                        hcm.Append("<p>" + item.hasSmallpackage + "</p>");
                    }
                    hcm.Append("    </td>");
                    #endregion

                    hcm.Append("<td>" + item.statusstring + "</td>");
                    hcm.Append("<td>");
                    hcm.Append(" <div class=\"action-table\">");
                    hcm.Append("<a href = \"OrderDetail.aspx?id=" + item.ID + "\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i></a>");
                    //hcm.Append("<a href = \"Pay-Order.aspx?id=" + item.ID + "\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Thanh toán ngay\"><i class=\"material-icons\">payment</i></a>");
                    hcm.Append("</div>");
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

        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string stype = ddlType.SelectedValue;
            string searchname = tSearchName.Text.Trim();
            string fd = "";
            string td = "";
            string priceFrom = "";
            string priceTo = "";
            int uID = 0;
            string hasVMD = hdfCheckBox.Value;
            if (Request.QueryString["uid"] != null)
            {
                uID = Request.QueryString["uid"].ToInt(0);
            }
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rPriceFrom.Text))
            {
                priceFrom = rPriceFrom.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rPriceTo.Text))
            {
                priceTo = rPriceTo.Text.ToString();
            }
            string st = "";
            if (!string.IsNullOrEmpty(ddlStatuss.SelectedValue))
            {
                List<string> myValues = new List<string>();
                for (int i = 0; i < ddlStatuss.Items.Count; i++)
                {
                    var item = ddlStatuss.Items[i];
                    if (item.Selected)
                    {
                        myValues.Add(item.Value);
                    }
                }
                st = String.Join(",", myValues.ToArray());
            }
            if (string.IsNullOrEmpty(stype) == true && string.IsNullOrEmpty(searchname) == true && fd == "" && td == "" && priceFrom == "" && priceTo == "" && string.IsNullOrEmpty(st) == true && hasVMD == "0")
            {
                Response.Redirect("user-order?uid=" + uID);
            }
            else
            {
                Response.Redirect("user-order?uid=" + uID + "&stype=" + stype + "&s=" + searchname + "&fd=" + fd + "&td=" + td + "&priceFrom=" + priceFrom + "&priceTo=" + priceTo + "&st=" + st + "&hasMVD=" + hasVMD);
            }

        }
        #endregion        

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            int uID = 0;
            int stype = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["stype"]))
            {
                stype = int.Parse(Request.QueryString["stype"]);
                ddlType.SelectedValue = stype.ToString();
            }

            string fd = Request.QueryString["fd"];
            if (!string.IsNullOrEmpty(fd))
                rFD.Text = fd;
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(td))
                rTD.Text = td;
            string priceTo = Request.QueryString["priceTo"];
            if (!string.IsNullOrEmpty(priceTo))
                rPriceTo.Text = priceTo;
            string priceFrom = Request.QueryString["priceFrom"];
            if (!string.IsNullOrEmpty(priceFrom))
                rPriceFrom.Text = priceFrom;
            string search = "";

            int hasVMD = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["hasMVD"]))
            {
                hasVMD = Request.QueryString["hasMVD"].ToString().ToInt(0);
                hdfCheckBox.Value = hasVMD.ToString();
            }



            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
            {
                var list = st.Split(',').ToList();

                for (int j = 0; j < list.Count; j++)
                {
                    for (int i = 0; i < ddlStatuss.Items.Count; i++)
                    {
                        var item = ddlStatuss.Items[i];
                        if (item.Value == list[j])
                        {
                            ddlStatuss.Items[i].Selected = true;
                        }
                    }
                }

            }
            if (Request.QueryString["uid"] != null)
            {
                uID = Request.QueryString["uid"].ToInt(0);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                tSearchName.Text = search;
            }

            var la = MainOrderController.GetByUserIDInSQLHelperWithFilterNew_Excel(uID, search, stype, fd, td, priceFrom, priceTo, st, Convert.ToBoolean(hasVMD));

            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>OrderID</strong></th>");
            //StrExport.Append("      <th><strong>Ảnh sản phẩm</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Tiền cọc</strong></th>");
            StrExport.Append("      <th><strong>Username</strong></th>");
            StrExport.Append("      <th><strong>NV đặt hàng</strong></th>");
            StrExport.Append("      <th><strong>NV kinh doanh</strong></th>");
            StrExport.Append("      <th><strong>Ngày đặt</strong></th>");
            StrExport.Append("      <th><strong>Mã vận đơn</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                string htmlproduct = "";
                string username = "";
                var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                if (ui != null)
                {
                    username = ui.Username;
                }
                StrExport.Append("<tr>");
                StrExport.Append("<td>" + item.ID + "</td>");
                //StrExport.Append("<td>" + item.anhsanpham + "</td>");
                StrExport.Append("<td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</td>");
                StrExport.Append("<td style=\"mso-number-format:'\\@'\">" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</td>");
                StrExport.Append("<td>" + item.Uname + "</td>");
                StrExport.Append("<td>" + item.dathang + "</td>");
                StrExport.Append("<td>" + item.saler + "</td>");
                StrExport.Append("<td>" + item.CreatedDate + "</td>");
                StrExport.Append("<td>" + item.hasSmallpackage + "</td>");
                StrExport.Append("<td>" + item.statusstring + "</td>");
                StrExport.Append("</tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "Danh-sach-don-hang-khach.xls";
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
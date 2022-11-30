using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using MB.Extensions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace NHST
{
    public partial class quan_ly_van_chuyen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "duvinhlam";
                //Session["userLoginSystem"] = "lucas";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {

                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string phone = "";
                var ai = AccountInfoController.GetByUserID(ac.ID);
                if (ai != null)
                    phone = ai.Phone;
                string search = Request.QueryString["textsearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    txtNote.Text = search;
                    var listsenddate = SmallPackageController1.GetAllByUserPhone_SQLGroup(phone);
                    if (listsenddate.Count > 0)
                    {
                        List<Danhsachpackagelon> bs = new List<Danhsachpackagelon>();
                        foreach (var item in listsenddate)
                        {
                            DateTime SendDate = item.SendDate;
                            Danhsachpackagelon b = new Danhsachpackagelon();
                            b.SendDate = SendDate;
                            b.isSearch = true;
                            var sps = SmallPackageController1.GetAllByUserPhoneSendDateNote_SQLGroup(phone, SendDate, search);
                            List<Danhsachpackagenho> ns = new List<Danhsachpackagenho>();
                            if (sps.Count > 0)
                            {
                                foreach (var s in sps)
                                {
                                    string dhtranghthainhanhcham = "<span class=\"bg-red\">Đi bay</span>";
                                    if (s.IsSlow == true)
                                        dhtranghthainhanhcham = "<span class=\"bg-blue\">Đi tàu</span>";
                                    Danhsachpackagenho n = new Danhsachpackagenho();
                                    n.ID = s.ID;
                                    n.BigPackageID = Convert.ToInt32(s.BigPackageID);
                                    n.packagecode = s.PackageCode;
                                    n.userphone = s.UserPhone;
                                    n.weight = s.Weight.ToString();
                                    n.tocdo = dhtranghthainhanhcham;
                                    n.notecus = s.NoteCustomer;
                                    n.place = PJUtils.ReturnPlace(Convert.ToInt32(s.Place));
                                    n.placenum = Convert.ToInt32(s.Place);
                                    n.trangthaithanhtoan = PJUtils.ReturnStatusPayment(Convert.ToInt32(s.StatusPayment));
                                    n.trangthainhanhang = PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(s.StatusReceivePackage));
                                    n.IsSlow = s.IsSlow;
                                    ns.Add(n);
                                }
                            }
                            b.pcknho = ns;
                            bs.Add(b);
                        }
                        pagingall(bs.OrderByDescending(b => b.SendDate).ToList());
                    }
                }
                else
                {
                    var listsenddate = SmallPackageController1.GetAllByUserPhone_SQLGroup(phone);
                    if (listsenddate.Count > 0)
                    {

                        List<Danhsachpackagelon> bs = new List<Danhsachpackagelon>();
                        foreach (var item in listsenddate)
                        {
                            DateTime SendDate = item.SendDate;
                            Danhsachpackagelon b = new Danhsachpackagelon();
                            b.SendDate = SendDate;
                            b.isSearch = false;
                            var sps = SmallPackageController1.GetAllByUserPhoneSendDate_SQLGroup(phone, SendDate);
                            List<Danhsachpackagenho> ns = new List<Danhsachpackagenho>();
                            if (sps.Count > 0)
                            {
                                foreach (var s in sps)
                                {
                                    string dhtranghthainhanhcham = "<span class=\"bg-red\">Đi bay</span>";
                                    if (s.IsSlow == true)
                                        dhtranghthainhanhcham = "<span class=\"bg-blue\">Đi tàu</span>";
                                    Danhsachpackagenho n = new Danhsachpackagenho();
                                    n.ID = s.ID;
                                    n.BigPackageID = Convert.ToInt32(s.BigPackageID);
                                    n.packagecode = s.PackageCode;
                                    n.userphone = s.UserPhone;
                                    n.weight = s.Weight.ToString();
                                    n.place = PJUtils.ReturnPlace(Convert.ToInt32(s.Place));
                                    n.notecus = s.NoteCustomer;
                                    n.placenum = Convert.ToInt32(s.Place);
                                    n.tocdo = dhtranghthainhanhcham;
                                    n.trangthaithanhtoan = PJUtils.ReturnStatusPayment(Convert.ToInt32(s.StatusPayment));
                                    n.trangthaithanhtoanso = Convert.ToInt32(s.StatusPayment);
                                    n.trangthainhanhang = PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(s.StatusReceivePackage));
                                    n.trangthainhanhangso = Convert.ToInt32(s.StatusReceivePackage);
                                    n.IsSlow = s.IsSlow;
                                    ns.Add(n);
                                }
                            }
                            b.pcknho = ns;
                            bs.Add(b);
                        }
                        pagingall(bs.OrderByDescending(b => b.SendDate).ToList());
                    }
                }
            }
        }

        #region Paging
        public void pagingall(List<Danhsachpackagelon> acs)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                double priceHN_config = 0;
                double priceSG_config = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    priceHN_config = Convert.ToDouble(config.PriceSendDefaultHN);
                    priceSG_config = Convert.ToDouble(config.PriceSendDefaultSG);
                }
                int level = Convert.ToInt32(u.LevelID);
                int UID = u.ID;
                var ui = AccountInfoController.GetByUserID(u.ID);
                if (ui != null)
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

                        for (int i = FromRow; i < ToRow + 1; i++)
                        {
                            var item = acs[i];
                            if (item.pcknho.Count > 0)
                            {
                                ltrorderlist.Text += "<tr style=\"background: lightgreen\">";
                                ltrorderlist.Text += "  <td colspan=\"8\" style=\"text-decoration: none; font-weight: bold;\">Ngày gửi: " + string.Format("{0:dd/MM/yyyy}", item.SendDate) + "</td>";
                                ltrorderlist.Text += "</tr>";
                                var sps = item.pcknho;
                                if (sps.Count > 0)
                                {
                                    double weighthn = 0;
                                    double weighthnnhanh = 0;
                                    double weighthncham = 0;
                                    double weightsg = 0;
                                    double weightsgnhanh = 0;
                                    double weightsgcham = 0;

                                    double pricehn = 0;
                                    double pricesg = 0;

                                    double pricehnnhanh = 0;
                                    double pricehncham = 0;

                                    double pricesgnhanh = 0;
                                    double pricesgcham = 0;
                                    double total = 0;
                                    double additionfee = 0;

                                    foreach (var s in sps)
                                    {
                                        double plusmoney = 0;
                                        var bigpackage = BigPackageController1.GetByID(s.BigPackageID);
                                        if (bigpackage != null)
                                        {
                                            if (bigpackage.AdditionFee != null)
                                                plusmoney = Convert.ToDouble(bigpackage.AdditionFee);
                                        }
                                        double weight = Convert.ToDouble(s.weight);
                                        double ttAdditionFee = plusmoney * weight;
                                        additionfee += ttAdditionFee;

                                        //if (Convert.ToDouble(s.weight) < 1)
                                        //    weight = 1;
                                        //else
                                        //    weight = Convert.ToDouble(s.weight);

                                        ltrorderlist.Text += "<tr>";
                                        ltrorderlist.Text += "  <td></td>";
                                        ltrorderlist.Text += "  <td><a href=\"javascript:;\" style=\"text-decoration:underline;\" onclick=\"viewnote('" + s.ID + "')\">" + s.packagecode + "</a></td>";
                                        //ltrorderlist.Text += "  <td>" + s.userphone + "</td>";
                                        ltrorderlist.Text += "  <td>" + weight + "</td>";
                                        ltrorderlist.Text += "  <td>" + s.place + "</td>";
                                        ltrorderlist.Text += "  <td>" + s.notecus + "</td>";
                                        ltrorderlist.Text += "  <td>" + s.tocdo + "</td>";
                                        ltrorderlist.Text += "  <td>" + s.trangthainhanhang + "</td>";
                                        ltrorderlist.Text += "  <td>" + s.trangthaithanhtoan + "</td>";
                                        ltrorderlist.Text += "</tr>";

                                        if (s.placenum == 1)
                                        {
                                            weighthn += Convert.ToDouble(weight);
                                            if (s.IsSlow == true)
                                                weighthncham += Convert.ToDouble(weight);
                                            else
                                                weighthnnhanh += Convert.ToDouble(weight);
                                        }
                                        else
                                        {
                                            weightsg += Convert.ToDouble(weight);
                                            if (s.IsSlow == true)
                                                weightsgcham += Convert.ToDouble(weight);
                                            else
                                                weightsgnhanh += Convert.ToDouble(weight);
                                        }
                                    }
                                    if (weighthn > 0 && weighthn < 1)
                                        weighthn = 1;

                                    if (weighthn > 0 && weightsg < 1)
                                        weightsg = 1;
                                    if (weighthncham > 0 && weighthncham < 1)
                                        weighthncham = 1;
                                    if (weighthnnhanh > 0 && weighthnnhanh < 1)
                                        weighthnnhanh = 1;
                                    if (weightsgcham > 0 && weightsgcham < 1)
                                        weightsgcham = 1;
                                    if (weightsgnhanh > 0 && weightsgnhanh < 1)
                                        weightsgnhanh = 1;

                                    if (item.isSearch == false)
                                    {
                                        var wHNNhanh = WeightController.GetByPlaceTypeWeightFT(weighthn, 1, 1);
                                        var wHNCham = WeightController.GetByPlaceTypeWeightFT(weighthn, 1, 2);

                                        var wSGNhanh = WeightController.GetByPlaceTypeWeightFT(weightsg, 2, 1);
                                        var wSGCham = WeightController.GetByPlaceTypeWeightFT(weightsg, 2, 2);

                                        if (wHNNhanh != null)
                                        {
                                            double pricefull = 0;
                                            if (level == 1)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip1);
                                            }
                                            else if (level == 2)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip2);
                                            }
                                            else if (level == 3)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip3);
                                            }
                                            else if (level == 4)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip4);
                                            }
                                            else if (level == 5)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip5);
                                            }
                                            else
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNNhanh.Vip6);
                                            }
                                            pricehnnhanh = pricefull * weighthnnhanh;
                                        }
                                        if (wHNCham != null)
                                        {
                                            double pricefull = 0;
                                            if (level == 1)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip1);
                                            }
                                            else if (level == 2)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip2);
                                            }
                                            else if (level == 3)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip3);
                                            }
                                            else if (level == 4)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip4);
                                            }
                                            else if (level == 5)
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip5);
                                            }
                                            else
                                            {
                                                pricefull = priceHN_config + Convert.ToDouble(wHNCham.Vip6);
                                            }
                                            pricehncham = pricefull * weighthncham;
                                        }

                                        if (wSGNhanh != null)
                                        {
                                            double pricefull = 0;
                                            if (level == 1)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip1);
                                            }
                                            else if (level == 2)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip2);
                                            }
                                            else if (level == 3)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip3);
                                            }
                                            else if (level == 4)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip4);
                                            }
                                            else if (level == 5)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip5);
                                            }
                                            else
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGNhanh.Vip6);
                                            }
                                            pricesgnhanh = pricefull * weightsgnhanh;
                                        }
                                        if (wSGCham != null)
                                        {
                                            double pricefull = 0;
                                            if (level == 1)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip1);
                                            }
                                            else if (level == 2)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip2);
                                            }
                                            else if (level == 3)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip3);
                                            }
                                            else if (level == 4)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip4);
                                            }
                                            else if (level == 5)
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip5);
                                            }
                                            else
                                            {
                                                pricefull = priceSG_config + Convert.ToDouble(wSGCham.Vip6);
                                            }
                                            pricesgcham = pricefull * weightsgcham;
                                        }
                                        //var weightInforHN = WeightController.GetByPlaceWeightFT(weighthn, 1);
                                        //var weightInforSG = WeightController.GetByPlaceWeightFT(weightsg, 2);

                                        //if (weightInforHN != null)
                                        //    pricehn = Convert.ToDouble(weightInforHN.PricePerWeight) * weighthn;
                                        //if (weightInforSG != null)
                                        //    pricesg = Convert.ToDouble(weightInforSG.PricePerWeight) * weightsg;

                                        total = pricehnnhanh + pricehncham + pricesgnhanh + pricesgcham + additionfee;
                                        double paid = 0;
                                        double left = 0;

                                        var transaction = HistoryPayWalletController.GetByUIDTradeTypeDateSend(UID, 8, Convert.ToDateTime(item.SendDate));
                                        if (transaction.Count > 0)
                                        {
                                            foreach (var t in transaction)
                                            {
                                                paid += Convert.ToDouble(t.Amount);
                                            }
                                        }
                                        left = total - paid;
                                        ltrorderlist.Text += "<tr style=\"background:gold\">";
                                        ltrorderlist.Text += "  <td colspan=\"1\">Tổng cộng:</td>";
                                        ltrorderlist.Text += "  <td colspan=\"7\">" + string.Format("{0:N0}", total).Replace(",", ".") + " VNĐ</td>";
                                        ltrorderlist.Text += "</tr>";
                                        ltrorderlist.Text += "<tr style=\"background:gold\">";
                                        ltrorderlist.Text += "  <td colspan=\"1\">Đã thanh toán:</td>";
                                        ltrorderlist.Text += "  <td colspan=\"7\">" + string.Format("{0:N0}", paid).Replace(",", ".") + " VNĐ</td>";
                                        ltrorderlist.Text += "</tr>";
                                        ltrorderlist.Text += "<tr style=\"background:gold\">";
                                        ltrorderlist.Text += "  <td colspan=\"1\">Còn lại:</td>";
                                        ltrorderlist.Text += "  <td colspan=\"7\">" + string.Format("{0:N0}", left).Replace(",", ".") + " VNĐ</td>";
                                        ltrorderlist.Text += "</tr>";
                                        if (left > 0)
                                        {
                                            bool checkisre = true;
                                            bool checkispay = true;
                                            foreach (var s in sps)
                                            {
                                                if (s.trangthainhanhangso == 0)
                                                    checkisre = false;
                                                if (s.trangthaithanhtoanso == 0)
                                                    checkispay = false;
                                            }
                                            if (checkisre == false && checkispay == false)
                                            {
                                                ltrorderlist.Text += "<tr>";
                                                string sendda = string.Format("{0:dd/MM/yyyy}", item.SendDate);
                                                ltrorderlist.Text += "  <td colspan=\"8\"><a href=\"javascript:;\" class=\"btn main-btn hover\" onclick=\"payallfordate('" + sendda + "','" + left + "')\">Thanh toán</a></td>";
                                                ltrorderlist.Text += "</tr>";
                                            }
                                            else if (checkisre == true && checkispay == false)
                                            {
                                                ltrorderlist.Text += "<tr>";
                                                string sendda = string.Format("{0:dd/MM/yyyy}", item.SendDate);
                                                ltrorderlist.Text += "  <td colspan=\"8\"><a href=\"javascript:;\" class=\"btn main-btn hover\" onclick=\"payallfordate('" + sendda + "','" + left + "')\">Thanh toán</a></td>";
                                                ltrorderlist.Text += "</tr>";
                                            }

                                        }
                                        else
                                        {

                                            bool checkisre = true;
                                            bool checkispay = true;
                                            foreach (var s in sps)
                                            {
                                                if (s.trangthainhanhangso == 0)
                                                    checkisre = false;
                                                if (s.trangthaithanhtoanso == 0)
                                                    checkispay = false;
                                            }
                                            //if (checkisre == false && checkispay == false)
                                            //{
                                            //    ltrorderlist.Text += "<tr>";
                                            //    string sendda = string.Format("{0:dd/MM/yyyy}", item.SendDate);
                                            //    ltrorderlist.Text += "  <td colspan=\"6\"><a href=\"javascript:;\" class=\"submit-btn\" onclick=\"payallfordate('" + sendda + "','" + left + "')\">Thanh toán</a></td>";
                                            //    ltrorderlist.Text += "</tr>";
                                            //}
                                            if (checkispay == false)
                                            {
                                                ltrorderlist.Text += "<tr>";
                                                string sendda = string.Format("{0:dd/MM/yyyy}", item.SendDate);
                                                ltrorderlist.Text += "  <td colspan=\"8\"><a href=\"javascript:;\" class=\"btn main-btn hover\" onclick=\"payallfordate('" + sendda + "','" + left + "')\">Thanh toán</a></td>";
                                                ltrorderlist.Text += "</tr>";
                                            }
                                            if (checkisre == false)
                                            {
                                                string sendda = string.Format("{0:dd/MM/yyyy}", item.SendDate);
                                                ltrorderlist.Text += "<tr style=\"background:#f8f8f8\">";
                                                ltrorderlist.Text += "  <td colspan=\"8\"><a href=\"javascript:;\" class=\"btn main-btn hover\" onclick=\"acceptreceived('" + sendda + "')\">Xác nhận đã nhận hàng</a></td>";
                                                ltrorderlist.Text += "</tr>";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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
        public class Danhsachpackagelon
        {
            public DateTime SendDate { get; set; }
            public DateTime ArrivedDate { get; set; }
            public bool isSearch { get; set; }
            public List<Danhsachpackagenho> pcknho { get; set; }
        }
        public class Danhsachpackagenho
        {
            public int ID { get; set; }
            public string packagecode { get; set; }
            public string userphone { get; set; }
            public string weight { get; set; }
            public string place { get; set; }
            public string notecus { get; set; }
            public int placenum { get; set; }

            public string tocdo { get; set; }
            public string trangthaithanhtoan { get; set; }
            public int trangthaithanhtoanso { get; set; }
            public string trangthainhanhang { get; set; }
            public int trangthainhanhangso { get; set; }
            public int BigPackageID { get; set; }
            public bool IsSlow { get; set; }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                string phone = "";
                var ai = AccountInfoController.GetByUserID(u.ID);
                if (ai != null)
                    phone = ai.Phone;
                int UID = u.ID;
                //DateTime senddate = Convert.ToDateTime(hdfSenddate.Value);
                string sd = hdfSenddate.Value.Trim();
                DateTime senddate = DateTime.ParseExact(sd, "dd/MM/yyyy", null);
                double Price = Convert.ToDouble(hdfPrice.Value);
                double wallet = Convert.ToDouble(u.Wallet);

                DateTime currentDate = DateTime.Now;
                if (wallet > 0)
                {
                    if (wallet >= Price)
                    {
                        double wallet_left = wallet - Price;
                        AccountController.updateWallet(UID, wallet_left, currentDate, username);
                        HistoryPayWalletController.InsertTransport(UID, username, Price,
                            username + " đã thanh toán ngày vận chuyển: " + senddate + ".", wallet_left, senddate, currentDate, username);

                        var smallpackages = SmallPackageController1.GetAllByUserPhoneSendDateAndStatus(phone, senddate, 0);
                        if (smallpackages.Count > 0)
                        {
                            foreach (var item in smallpackages)
                            {
                                SmallPackageController1.UpdateStatusPayment(item.ID, 1);
                            }
                        }
                    }

                    //var adminlist = AccountController.GetAllByRoleID(0);
                    //if (adminlist.Count > 0)
                    //{
                    //    foreach (var a in adminlist)
                    //    {
                    //        NotificationController.InsertAdmin(UID, username, a.ID, a.Username, 0, "", username + " đã thanh toán ngày vận chuyển: " + senddate + ".", 0, true, 1, currentDate, username);
                    //    }
                    //}
                    PJUtils.ShowMessageBoxSwAlert("Xác nhận đơn hàng thành công", "s", true, Page);
                }
                else
                {
                    double sotienphainap = Price - wallet;
                    PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm " + string.Format("{0:N0}", sotienphainap).Replace(",", ".") + " VNĐ để đặt cọc đơn hàng.", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Số dư tài khoản của bạn là 0 VNĐ, không đủ để đặt cọc đơn hàng.", "e", true, Page);
            }
        }

        protected void btnAcceptreceive_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                string phone = "";
                var ai = AccountInfoController.GetByUserID(UID);
                if (ai != null)
                    phone = ai.Phone;
                string sd = hdfSenddate.Value.Trim();
                DateTime senddate = DateTime.ParseExact(sd, "dd/MM/yyyy", null);
                DateTime currentDate = DateTime.Now;
                var smallpackages = SmallPackageController1.GetAllByUserPhoneSendDateAndStatusReceivePackage(phone, senddate, 0);
                if (smallpackages.Count > 0)
                {
                    foreach (var item in smallpackages)
                    {
                        SmallPackageController1.UpdateStatusReceivePackage(item.ID, 1);
                    }
                }
                PJUtils.ShowMessageBoxSwAlert("Xác nhận đơn hàng thành công", "s", true, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Số dư tài khoản của bạn là 0 VNĐ, không đủ để đặt cọc đơn hàng.", "e", true, Page);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Response.Redirect("/quan-ly-van-chuyen?textsearch=" + txtNote.Text);
        }

        [WebMethod]
        public static string getNote(int ID)
        {
            var p = SmallPackageController1.GetByID(ID);
            if (p != null)
            {
                return p.Note.Replace("\n", "<br/>");
            }
            else
                return "0";
        }
    }
}
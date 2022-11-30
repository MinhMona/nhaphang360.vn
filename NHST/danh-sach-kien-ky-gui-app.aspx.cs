using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
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

namespace NHST
{
    public partial class danh_sach_kien_ky_gui_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadShippingTypeVN();
            }

        }

        public void LoadShippingTypeVN()
        {
            string html = "";
            var s = ShippingTypeVNController.GetAllWithIsHidden("", false);
            if (s.Count > 0)
            {
                foreach (var item in s)
                {
                    html += item.ID + ":" + item.ShippingTypeVNName + "|";
                }
            }
            hdfListShippingVN.Value = html;
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
                        ViewState["UID"] = UID;
                        ViewState["Key"] = Key;
                        pnMobile.Visible = true;

                        string code = Request.QueryString["code"];
                        int type = Request.QueryString["type"].ToInt(2);
                        int status = Request.QueryString["stt"].ToInt(-1);
                        string fd = Request.QueryString["fd"];
                        string td = Request.QueryString["td"];

                        if (Request.QueryString["stt"] != null)
                            ddlStatus.SelectedValue = status.ToString();
                        if (Request.QueryString["code"] != null)
                            txtOrderTransactionCode.Text = code.ToString();

                        int page = 0;

                        Int32 Page = GetIntFromQueryString("Page");
                        if (Page > 0)
                        {
                            page = Page - 1;
                        }

                        var os = TransportationOrderNewController.GetAllByUIDWithFilter_SqlHelperWithPage(UID, code, type, status, fd, td, page, 20);
                        List<tbl_TransportationOrderNew> oAll = new List<tbl_TransportationOrderNew>();
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

                            int total = TransportationOrderNewController.GetTotalUser(UID, code, type, status, fd, td);

                            pagingall(os, total);
                        }

                    }
                    ViewState["UID"] = UID;
                    Session["UID"] = UID;
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
        public void pagingall(List<tbl_TransportationOrderNew> acs, int total)
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
                string Key = ViewState["Key"].ToString();
                int UID = Convert.ToInt32(ViewState["UID"]);
                StringBuilder html = new StringBuilder();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];

                    html.Append(" <div class=\"thanhtoanho-list\" data-id=\"" + item.ID + "\">");
                    html.Append(" <div class=\"all\">");
                    html.Append(" <div class=\"order-group offset15\">");
                    html.Append("  <div class=\"heading\">");
                    html.Append(" <p class=\"left-lb\">");
                    if (item.Status == 4)
                    {
                        html.Append("    <label><input type=\"checkbox\" onchange=\"selectdeposit()\" class=\"chk-deposit\" data-id=\"" + item.ID + "\"><span></span></label>");
                        //html.Append("<span class=\"circle-icon\"><img src=\"/App_Themes/App/images/icon-store.png\" style=\"height:12px\" alt=\"\"></span>");
                    }


                    html.Append("     ID: " + item.ID + "");
                    html.Append("  </p>");
                    html.Append("  <p class=\"right-meta\">Ngày tạo: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</span></p>");
                    html.Append(" </div>");
                    html.Append("  <div class=\"smr\">");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Mã vận đơn:</p>");
                    html.Append("   <p>" + item.BarCode + "</p>");
                    html.Append("  </div>");
                    double weight = 0;
                    if (item.SmallPackageID != null)
                    {
                        if (item.SmallPackageID > 0)
                        {
                            var pack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
                            if (pack != null)
                            {
                                if (pack.Weight.ToString().ToFloat(0) > 0)
                                    weight = Convert.ToDouble(pack.Weight);
                            }
                        }
                    }
                    html.Append("  <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Cân nặng):</p>");
                    html.Append("  <p>" + weight + "</p>");
                    html.Append("  </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Kho TQ:</p>");
                    html.Append("  <p>" + WarehouseFromController.GetByID(item.WareHouseFromID.Value).WareHouseName + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Kho VN:</p>");
                    html.Append("  <p>" + WarehouseController.GetByID(item.WareHouseID.Value).WareHouseName + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">PTVC:</p>");
                    html.Append("  <p>" + ShippingTypeToWareHouseController.GetByID(item.ShippingTypeID.Value).ShippingTypeName + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ghi chú KH:</p>");
                    html.Append("   <p>" + item.Note + "</p>");
                    html.Append(" </div>");

                    string dateTQ = "";
                    string dateVN = "";
                    if (item.SmallPackageID != null)
                    {
                        if (item.SmallPackageID > 0)
                        {
                            var pack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
                            if (pack != null)
                            {
                                if (pack.DateInTQWarehouse != null)
                                {
                                    dateTQ = string.Format("{0:dd/MM/yyyy}", pack.DateInTQWarehouse);
                                }
                                if (pack.DateInLasteWareHouse != null)
                                {
                                    dateVN = string.Format("{0:dd/MM/yyyy}", pack.DateInLasteWareHouse);
                                }

                            }
                        }
                    }

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ngày về kho TQ:</p>");
                    html.Append("   <p>" + dateTQ + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ngày về kho VN:</p>");
                    html.Append("   <p>" + dateVN + "</p>");
                    html.Append(" </div>");


                    string ngayycxk = "";
                    string ngayxk = "";
                    if (item.DateExportRequest != null)
                    {
                        ngayycxk = string.Format("{0:dd/MM/yyyy}", item.DateExportRequest);
                    }
                    if (item.DateExport != null)
                    {
                        ngayxk = string.Format("{0:dd/MM/yyyy}", item.DateExport);
                    }
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ngày YCXK:</p>");
                    html.Append("   <p>" + ngayycxk + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Ngày XK:</p>");
                    html.Append("   <p>" + ngayxk + "</p>");
                    html.Append(" </div>");

                    string shippintType = "";
                    if (item.ShippingTypeVN != null)
                    {
                        var sht = ShippingTypeVNController.GetByID(item.ShippingTypeVN.ToString().ToInt(0));
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
                    html.Append("     <p class=\"gray-txt\">Ghi chú:</p>");
                    html.Append("   <p>" + item.ExportRequestNote + "</p>");
                    html.Append(" </div>");

                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Trạng thái:</p>");
                    html.Append("   <p>" + PJUtils.GeneralTransportationOrderNewStatusApp(Convert.ToInt32(item.Status)) + "</p>");
                    html.Append(" </div>");

                    html.Append("  </div>");
                    if (item.Status == 1)
                        html.Append("<a href=\"javascript:;\" onclick=\"rejectOrder($(this),'" + item.ID + "')\" class=\"btn cam-btn fw-btn\">Hủy đơn</a>");

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

        public class Danhsachorder
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string TotalPackage { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
            public string Username { get; set; }
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            int UID = ViewState["UID"].ToString().ToInt(0);
            string status = ddlStatus.SelectedValue;
            string code = txtOrderTransactionCode.Text.Trim();
            string fd = "";
            string td = "";
            string Key = ViewState["Key"].ToString();
            Response.Redirect("/danh-sach-kien-ky-gui-app.aspx?UID=" + UID + "&stt=" + status + "&code=" + code + "&Key=" + Key + "");
        }

        #region webservice
        [WebMethod]
        public static string rejectOrder(int id, string cancelnote)
        {
            int UID = HttpContext.Current.Session["UID"].ToString().ToInt(0);

            var acc = AccountController.GetByID(UID);

            if (acc != null)
            {
                string username = acc.Username;
                var t = TransportationOrderNewController.GetByIDAndUID(id, UID);
                if (t != null)
                {
                    TransportationOrderNewController.UpdateCancelOrder(id, 0, cancelnote, DateTime.Now, username);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }

        }
        [WebMethod]
        public static string exportAll()
        {
            int UID = HttpContext.Current.Session["UID"].ToString().ToInt(0);

            var acc = AccountController.GetByID(UID);
            if (acc != null)
            {

                double wallet = Convert.ToDouble(acc.Wallet);

                var ts = TransportationOrderNewController.GetByUIDAndStatus(UID, 4);
                if (ts.Count > 0)
                {
                    double feeOutStockCYN = 0;
                    double feeOutStockVND = 0;
                    double feeWeightOutStock = 0;

                    double totalWeight = 0;
                    double currency = 0;

                    double TotalAdditionFeeCYN = 0;
                    double TotalAdditionFeeVND = 0;

                    double TotalSensoredFeeCYN = 0;
                    double TotalSensoredFeeVND = 0;

                    double totalWeightPriceVND = 0;
                    double totalWeightPriceCYN = 0;

                    double totalPriceVND = 0;
                    double totalPriceCYN = 0;
                    double totalCount = ts.Count;

                    string listID = "";

                    var config = ConfigurationController.GetByTop1();
                    if (config != null)
                    {
                        currency = Convert.ToDouble(config.AgentCurrency);
                        feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                        feeOutStockVND = feeOutStockCYN * currency;
                    }

                    List<WareHouse> lw = new List<WareHouse>();
                    foreach (var t in ts)
                    {
                        var checkwh = lw.Where(x => x.WareHouseID == t.WareHouseID && x.WareHouseFromID == t.WareHouseFromID && x.ShippingTypeID == t.ShippingTypeID).FirstOrDefault();
                        if (checkwh != null)
                        {
                            if (t.SmallPackageID != null)
                            {
                                if (t.SmallPackageID > 0)
                                {
                                    var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                    if (package != null)
                                    {
                                        double weight = 0;
                                        if (package.Weight != null)
                                        {
                                            if (package.Weight > 0)
                                            {
                                                Package p = new Package();
                                                weight = Convert.ToDouble(package.Weight);
                                                totalWeight += weight;
                                                p.Weight = weight;
                                                p.TransportationID = t.ID;
                                                checkwh.TotalWeight += weight;
                                                checkwh.ListPackage.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            WareHouse w = new WareHouse();

                            w.WareHouseFromID = t.WareHouseFromID.Value;
                            w.WareHouseID = t.WareHouseID.Value;
                            w.ShippingTypeID = t.ShippingTypeID.Value;
                            if (t.SmallPackageID != null)
                            {
                                if (t.SmallPackageID > 0)
                                {
                                    List<Package> lp = new List<Package>();
                                    var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                    if (package != null)
                                    {
                                        double weight = 0;
                                        if (package.Weight != null)
                                        {
                                            if (package.Weight > 0)
                                            {
                                                Package p = new Package();
                                                weight = Convert.ToDouble(package.Weight);
                                                totalWeight += weight;
                                                w.TotalWeight = weight;
                                                p.Weight = weight;
                                                p.TransportationID = t.ID;
                                                lp.Add(p);
                                            }
                                        }
                                    }
                                    w.ListPackage = lp;
                                    lw.Add(w);
                                }
                            }
                        }


                        listID += t.ID + "|";

                        double addfeeVND = 0;
                        double addfeeCYN = 0;
                        double sensorVND = 0;
                        double sensorCYN = 0;

                        if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                            if (t.AdditionFeeVND.ToFloat(0) > 0)
                                addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                        if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                            if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                        if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                            if (t.SensorFeeeVND.ToFloat(0) > 0)
                                sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                        if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                            if (t.SensorFeeCYN.ToFloat(0) > 0)
                                sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                        TotalAdditionFeeCYN += addfeeCYN;
                        TotalAdditionFeeVND += addfeeVND;

                        TotalSensoredFeeVND += sensorVND;
                        TotalSensoredFeeCYN += sensorCYN;
                    }

                    if (!string.IsNullOrEmpty(acc.FeeTQVNPerWeight))
                    {
                        //double feetqvn = 0;
                        if (acc.FeeTQVNPerWeight.ToFloat(0) > 0)
                        {
                            totalWeightPriceVND = Convert.ToDouble(acc.FeeTQVNPerWeight) * totalWeight;
                        }
                    }
                    else
                    {
                        if (lw.Count > 0)
                        {
                            foreach (var item in lw)
                            {
                                var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                        item.WareHouseFromID, item.WareHouseID, item.ShippingTypeID, true);
                                if (fee.Count > 0)
                                {
                                    foreach (var f in fee)
                                    {
                                        if (item.TotalWeight > f.WeightFrom && item.TotalWeight <= f.WeightTo)
                                        {
                                            totalWeightPriceVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }



                    //totalWeightPriceCYN = totalWeight * feeWeightOutStock;
                    //totalWeightPriceVND = totalWeightPriceCYN * currency;

                    totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                    totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                    string ret = "";
                    if (wallet >= totalPriceVND)
                    {
                        ret = 1 + ":" + wallet + ":" + totalCount + ":"
  + totalWeight + ":"
  + totalWeightPriceCYN + ":"
  + totalWeightPriceVND + ":"
  + feeOutStockCYN + ":" + feeOutStockVND + ":"
  + totalPriceCYN + ":"
  + totalPriceVND + ":"
  + listID + ":"
  + "0" + ":"
  + TotalAdditionFeeCYN + ":"
  + TotalAdditionFeeVND + ":" + TotalSensoredFeeCYN + ":"
  + TotalSensoredFeeVND + ":";

                    }
                    else
                    {
                        double leftmoney = totalPriceVND - wallet;
                        if (leftmoney > 0)
                        {
                            ret = 0 + ":" + wallet + ":" + totalCount + ":"
+ totalWeight + ":"
+ totalWeightPriceCYN + ":"
+ totalWeightPriceVND + ":"
+ feeOutStockCYN + ":" + feeOutStockVND + ":"
+ totalPriceCYN + ":"
+ totalPriceVND + ":"
+ listID + ":"
+ leftmoney + ":"
+ TotalAdditionFeeCYN + ":"
+ TotalAdditionFeeVND + ":" + ":" + TotalSensoredFeeCYN + ":"
  + TotalSensoredFeeVND + ":";
                        }
                    }

                    return ret;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }

        }
        [WebMethod]
        public static string exportSelectedAll(string listID)
        {
            int UID = HttpContext.Current.Session["UID"].ToString().ToInt(0);

            var acc = AccountController.GetByID(UID);
            if (acc != null)
            {
                double wallet = Convert.ToDouble(acc.Wallet);
                double feeOutStockCYN = 0;
                double feeOutStockVND = 0;
                double feeWeightOutStock = 0;

                double totalWeight = 0;
                double currency = 0;

                double TotalAdditionFeeCYN = 0;
                double TotalAdditionFeeVND = 0;

                double TotalSensoredFeeCYN = 0;
                double TotalSensoredFeeVND = 0;

                double totalWeightPriceVND = 0;
                double totalWeightPriceCYN = 0;

                double totalPriceVND = 0;
                double totalPriceCYN = 0;
                double totalCount = 0;


                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    currency = Convert.ToDouble(config.AgentCurrency);
                    feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                    feeOutStockVND = feeOutStockCYN * currency;
                }
                List<WareHouse> lw = new List<WareHouse>();
                string[] IDs = listID.Split('|');
                if (IDs.Length - 1 > 0)
                {
                    for (int i = 0; i < IDs.Length - 1; i++)
                    {
                        int ID = IDs[i].ToInt(0);
                        var t = TransportationOrderNewController.GetByID(ID);
                        if (t != null)
                        {
                            totalCount++;

                            var checkwh = lw.Where(x => x.WareHouseID == t.WareHouseID && x.WareHouseFromID == t.WareHouseFromID && x.ShippingTypeID == t.ShippingTypeID).FirstOrDefault();
                            if (checkwh != null)
                            {
                                if (t.SmallPackageID != null)
                                {
                                    if (t.SmallPackageID > 0)
                                    {
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                        if (package != null)
                                        {
                                            double weight = 0;
                                            if (package.Weight != null)
                                            {
                                                if (package.Weight > 0)
                                                {
                                                    Package p = new Package();
                                                    weight = Convert.ToDouble(package.Weight);
                                                    totalWeight += weight;
                                                    p.Weight = weight;
                                                    p.TransportationID = t.ID;
                                                    checkwh.TotalWeight += weight;
                                                    checkwh.ListPackage.Add(p);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                WareHouse w = new WareHouse();

                                w.WareHouseFromID = t.WareHouseFromID.Value;
                                w.WareHouseID = t.WareHouseID.Value;
                                w.ShippingTypeID = t.ShippingTypeID.Value;
                                if (t.SmallPackageID != null)
                                {
                                    if (t.SmallPackageID > 0)
                                    {
                                        List<Package> lp = new List<Package>();
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                        if (package != null)
                                        {
                                            double weight = 0;
                                            if (package.Weight != null)
                                            {
                                                if (package.Weight > 0)
                                                {
                                                    Package p = new Package();
                                                    weight = Convert.ToDouble(package.Weight);
                                                    totalWeight += weight;
                                                    w.TotalWeight = weight;
                                                    p.Weight = weight;
                                                    p.TransportationID = t.ID;
                                                    lp.Add(p);
                                                }
                                            }
                                        }
                                        w.ListPackage = lp;
                                        lw.Add(w);
                                    }
                                }
                            }


                            double addfeeVND = 0;
                            double addfeeCYN = 0;
                            double sensorVND = 0;
                            double sensorCYN = 0;

                            if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                if (t.AdditionFeeVND.ToFloat(0) > 0)
                                    addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                            if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                    addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                            if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                if (t.SensorFeeeVND.ToFloat(0) > 0)
                                    sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                            if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                if (t.SensorFeeCYN.ToFloat(0) > 0)
                                    sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                            TotalAdditionFeeCYN += addfeeCYN;
                            TotalAdditionFeeVND += addfeeVND;

                            TotalSensoredFeeVND += sensorVND;
                            TotalSensoredFeeCYN += sensorCYN;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(acc.FeeTQVNPerWeight))
                {
                    //double feetqvn = 0;
                    if (acc.FeeTQVNPerWeight.ToFloat(0) > 0)
                    {
                        totalWeightPriceVND = Convert.ToDouble(acc.FeeTQVNPerWeight) * totalWeight;
                    }
                }
                else
                {
                    if (lw.Count > 0)
                    {
                        foreach (var item in lw)
                        {
                            var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                    item.WareHouseFromID, item.WareHouseID, item.ShippingTypeID, true);
                            if (fee.Count > 0)
                            {
                                foreach (var f in fee)
                                {
                                    if (item.TotalWeight > f.WeightFrom && item.TotalWeight <= f.WeightTo)
                                    {
                                        totalWeightPriceVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //totalWeightPriceCYN = totalWeight * feeWeightOutStock;
                //totalWeightPriceVND = totalWeightPriceCYN * currency;

                totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalSensoredFeeCYN;

                string ret = "";
                if (wallet >= totalPriceVND)
                {
                    ret = 1 + ":" + wallet + ":" + totalCount + ":"
 + totalWeight + ":"
 + totalWeightPriceCYN + ":"
 + totalWeightPriceVND + ":"
 + feeOutStockCYN + ":" + feeOutStockVND + ":"
 + totalPriceCYN + ":"
 + totalPriceVND + ":"
 + listID + ":" + TotalSensoredFeeVND + ":" + TotalAdditionFeeVND;
                }
                else
                {
                    double leftmoney = totalPriceVND - wallet;
                    if (leftmoney > 0)
                    {
                        ret = 0 + ":" + wallet + ":" + totalCount + ":"
+ totalWeight + ":"
+ totalWeightPriceCYN + ":"
+ totalWeightPriceVND + ":"
+ feeOutStockCYN + ":" + feeOutStockVND + ":"
+ totalPriceCYN + ":"
+ totalPriceVND + ":"
+ listID + ":" + TotalSensoredFeeVND + ":" + TotalAdditionFeeVND + ":"
+ leftmoney;
                    }
                }

                return ret;
            }
            else
            {
                return "0";
            }

        }
        #endregion

        protected void btnPayExport_Click(object sender, EventArgs e)
        {
            int UID = HttpContext.Current.Session["UID"].ToString().ToInt(0);

            var acc = AccountController.GetByID(UID);
            if (acc != null)
            {

                string username = acc.Username;
                DateTime currentDate = DateTime.Now;
                double wallet = Convert.ToDouble(acc.Wallet);
                string strListID = hdfListID.Value;
                if (!string.IsNullOrEmpty(strListID))
                {
                    string[] listID = strListID.Split('|');
                    if (listID.Length - 1 > 0)
                    {
                        double feeOutStockCYN = 0;
                        double feeOutStockVND = 0;
                        double feeWeightOutStock = 0;

                        double totalWeight = 0;
                        double currency = 0;

                        double TotalAdditionFeeCYN = 0;
                        double TotalAdditionFeeVND = 0;

                        double TotalSensoredFeeCYN = 0;
                        double TotalSensoredFeeVND = 0;

                        double totalWeightPriceVND = 0;
                        double totalWeightPriceCYN = 0;

                        double totalPriceVND = 0;
                        double totalPriceCYN = 0;

                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                        {
                            currency = Convert.ToDouble(config.AgentCurrency);
                            feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                            feeOutStockVND = feeOutStockCYN * currency;
                        }
                        List<WareHouse> lw = new List<WareHouse>();
                        for (int i = 0; i < listID.Length - 1; i++)
                        {
                            int ID = listID[i].ToInt(0);
                            var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                            if (t != null)
                            {
                                var checkwh = lw.Where(x => x.WareHouseID == t.WareHouseID && x.WareHouseFromID == t.WareHouseFromID && x.ShippingTypeID == t.ShippingTypeID).FirstOrDefault();
                                if (checkwh != null)
                                {
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                double weight = 0;
                                                if (package.Weight != null)
                                                {
                                                    if (package.Weight > 0)
                                                    {
                                                        Package p = new Package();
                                                        weight = Convert.ToDouble(package.Weight);
                                                        totalWeight += weight;
                                                        p.Weight = weight;
                                                        p.TransportationID = t.ID;
                                                        checkwh.TotalWeight += weight;
                                                        checkwh.ListPackage.Add(p);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    WareHouse w = new WareHouse();

                                    w.WareHouseFromID = t.WareHouseFromID.Value;
                                    w.WareHouseID = t.WareHouseID.Value;
                                    w.ShippingTypeID = t.ShippingTypeID.Value;
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            List<Package> lp = new List<Package>();
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                double weight = 0;
                                                if (package.Weight != null)
                                                {
                                                    if (package.Weight > 0)
                                                    {
                                                        Package p = new Package();
                                                        weight = Convert.ToDouble(package.Weight);
                                                        totalWeight += weight;
                                                        w.TotalWeight = weight;
                                                        p.Weight = weight;
                                                        p.TransportationID = t.ID;
                                                        lp.Add(p);
                                                    }
                                                }
                                            }
                                            w.ListPackage = lp;
                                            lw.Add(w);
                                        }
                                    }
                                }

                                double addfeeVND = 0;
                                double addfeeCYN = 0;
                                double sensorVND = 0;
                                double sensorCYN = 0;

                                if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                    if (t.AdditionFeeVND.ToFloat(0) > 0)
                                        addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                                if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                    if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                        addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                                if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                    if (t.SensorFeeeVND.ToFloat(0) > 0)
                                        sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                                if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                    if (t.SensorFeeCYN.ToFloat(0) > 0)
                                        sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                                TotalAdditionFeeCYN += addfeeCYN;
                                TotalAdditionFeeVND += addfeeVND;

                                TotalSensoredFeeVND += sensorVND;
                                TotalSensoredFeeCYN += sensorCYN;
                            }
                        }

                        double TotalFeeVND = 0;
                        if (acc.FeeTQVNPerWeight.ToFloat(0) > 0)
                        {
                            TotalFeeVND = Convert.ToDouble(acc.FeeTQVNPerWeight) * totalWeight;
                            totalWeightPriceVND += TotalFeeVND;
                        }
                        else
                        {
                            if (lw.Count > 0)
                            {
                                foreach (var item in lw)
                                {
                                    double pricePerKg = 0;
                                    var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                            item.WareHouseFromID, item.WareHouseID, item.ShippingTypeID, true);
                                    if (fee.Count > 0)
                                    {
                                        foreach (var f in fee)
                                        {
                                            if (item.TotalWeight > f.WeightFrom && item.TotalWeight <= f.WeightTo)
                                            {
                                                //totalWeightPriceVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                TotalFeeVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                pricePerKg = Convert.ToDouble(f.Price);
                                                totalWeightPriceVND += TotalFeeVND;
                                                break;
                                            }
                                        }
                                    }
                                    if (item.ListPackage != null)
                                    {
                                        foreach (var temp in item.ListPackage)
                                        {
                                            double price = temp.Weight * pricePerKg;
                                            TransportationOrderNewController.UpdateTotalPrice(temp.TransportationID, price.ToString(), pricePerKg.ToString());

                                        }
                                    }
                                }
                            }
                        }

                        totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                        totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                        if (wallet >= totalPriceVND)
                        {
                            //Lưu xuống 1 lượt yêu cầu xuất kho
                            #region Tạo lượt xuất kho
                            string note = hdfNote.Value;
                            int shippingtype = hdfShippingType.Value.ToInt(0);
                            int totalpackage = listID.Length - 1;
                            string kq = ExportRequestTurnController.InsertWithUID(UID, username, 0, currentDate, totalPriceVND,
                                totalPriceCYN, totalWeight, note, shippingtype, currentDate, username, totalpackage, 2);

                            int eID = kq.ToInt(0);
                            for (int i = 0; i < listID.Length - 1; i++)
                            {
                                int ID = listID[i].ToInt(0);
                                var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                                if (t != null)
                                {
                                    double weight = 0;
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                if (package.Status == 3)
                                                {
                                                    var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                                                    if (check == null)
                                                    {
                                                        RequestOutStockController.InsertT(package.ID,
                                                            package.OrderTransactionCode,
                                                            t.ID,
                                                            Convert.ToInt32(package.MainOrderID), 1,
                                                            DateTime.Now, username, eID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Update lại trạng thái từng đơn và ngày ship
                                    TransportationOrderNewController.UpdateRequestOutStock(t.ID, 5, note, currentDate, shippingtype);
                                }
                            }
                            #endregion
                            //Trừ tiền 
                            #region Trừ tiền xuất kho
                            double walletLeft = wallet - totalPriceVND;
                            AccountController.updateWallet(UID, walletLeft, currentDate, username);
                            HistoryPayWalletController.Insert(UID, username, 0, totalPriceVND,
                                username + " đã thanh toán đơn hàng vận chuyển hộ.", walletLeft, 1, 8, currentDate, username);
                            #endregion
                            #region Gửi thông báo
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationsController.Inser(admin.ID, admin.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {
                                    NotificationsController.Inser(manager.ID, manager.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                                }
                            }
                            var khoVNs = AccountController.GetAllByRoleID(5);
                            if (khoVNs.Count > 0)
                            {
                                foreach (var khoVN in khoVNs)
                                {
                                    NotificationsController.Inser(khoVN.ID, khoVN.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                                }
                            }
                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hubContext.Clients.All.addNewMessageToPage("", "");
                            #endregion
                            PJUtils.ShowMessageBoxSwAlert("Bạn đã gửi yêu cầu xuất kho thành công. Xin chân thành cảm ơn", "s", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số tiền của bạn không đủ để thanh toán phiên xuất kho, vui lòng thử lại sau.", "e", true, Page);
                        }
                    }
                }
            }
        }

        protected void btnThanhToanTaiKho_Click(object sender, EventArgs e)
        {
            int UID = HttpContext.Current.Session["UID"].ToString().ToInt(0);

            var acc = AccountController.GetByID(UID);
            if (acc != null)
            {

                string username = acc.Username;
                DateTime currentDate = DateTime.Now;
                double wallet = Convert.ToDouble(acc.Wallet);
                string strListID = hdfListID.Value;
                if (!string.IsNullOrEmpty(strListID))
                {
                    string[] listID = strListID.Split('|');
                    if (listID.Length - 1 > 0)
                    {
                        double feeOutStockCYN = 0;
                        double feeOutStockVND = 0;
                        double feeWeightOutStock = 0;

                        double totalWeight = 0;
                        double currency = 0;

                        double TotalAdditionFeeCYN = 0;
                        double TotalAdditionFeeVND = 0;

                        double TotalSensoredFeeCYN = 0;
                        double TotalSensoredFeeVND = 0;

                        double totalWeightPriceVND = 0;
                        double totalWeightPriceCYN = 0;

                        double totalPriceVND = 0;
                        double totalPriceCYN = 0;

                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                        {
                            currency = Convert.ToDouble(config.AgentCurrency);
                            feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                            feeOutStockVND = feeOutStockCYN * currency;
                        }
                        List<WareHouse> lw = new List<WareHouse>();
                        for (int i = 0; i < listID.Length - 1; i++)
                        {
                            int ID = listID[i].ToInt(0);
                            var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                            if (t != null)
                            {
                                var checkwh = lw.Where(x => x.WareHouseID == t.WareHouseID && x.WareHouseFromID == t.WareHouseFromID && x.ShippingTypeID == t.ShippingTypeID).FirstOrDefault();
                                if (checkwh != null)
                                {
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                double weight = 0;
                                                if (package.Weight != null)
                                                {
                                                    if (package.Weight > 0)
                                                    {
                                                        Package p = new Package();
                                                        weight = Convert.ToDouble(package.Weight);
                                                        totalWeight += weight;
                                                        p.Weight = weight;
                                                        p.TransportationID = t.ID;
                                                        checkwh.TotalWeight += weight;
                                                        checkwh.ListPackage.Add(p);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    WareHouse w = new WareHouse();

                                    w.WareHouseFromID = t.WareHouseFromID.Value;
                                    w.WareHouseID = t.WareHouseID.Value;
                                    w.ShippingTypeID = t.ShippingTypeID.Value;
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            List<Package> lp = new List<Package>();
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                double weight = 0;
                                                if (package.Weight != null)
                                                {
                                                    if (package.Weight > 0)
                                                    {
                                                        Package p = new Package();
                                                        weight = Convert.ToDouble(package.Weight);
                                                        totalWeight += weight;
                                                        w.TotalWeight = weight;
                                                        p.Weight = weight;
                                                        p.TransportationID = t.ID;
                                                        lp.Add(p);
                                                    }
                                                }
                                            }
                                            w.ListPackage = lp;
                                            lw.Add(w);
                                        }
                                    }
                                }

                                double addfeeVND = 0;
                                double addfeeCYN = 0;
                                double sensorVND = 0;
                                double sensorCYN = 0;

                                if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                    if (t.AdditionFeeVND.ToFloat(0) > 0)
                                        addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                                if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                    if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                        addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                                if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                    if (t.SensorFeeeVND.ToFloat(0) > 0)
                                        sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                                if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                    if (t.SensorFeeCYN.ToFloat(0) > 0)
                                        sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                                TotalAdditionFeeCYN += addfeeCYN;
                                TotalAdditionFeeVND += addfeeVND;

                                TotalSensoredFeeVND += sensorVND;
                                TotalSensoredFeeCYN += sensorCYN;
                            }
                        }

                        double TotalFeeVND = 0;
                        if (acc.FeeTQVNPerWeight.ToFloat(0) > 0)
                        {
                            TotalFeeVND = Convert.ToDouble(acc.FeeTQVNPerWeight) * totalWeight;
                            totalWeightPriceVND += TotalFeeVND;
                        }
                        else
                        {
                            if (lw.Count > 0)
                            {
                                foreach (var item in lw)
                                {
                                    double pricePerKg = 0;
                                    var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                            item.WareHouseFromID, item.WareHouseID, item.ShippingTypeID, true);
                                    if (fee.Count > 0)
                                    {
                                        foreach (var f in fee)
                                        {
                                            if (item.TotalWeight > f.WeightFrom && item.TotalWeight <= f.WeightTo)
                                            {
                                                //totalWeightPriceVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                TotalFeeVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                pricePerKg = Convert.ToDouble(f.Price);
                                                totalWeightPriceVND += TotalFeeVND;
                                                break;
                                            }
                                        }
                                    }
                                    if (item.ListPackage != null)
                                    {
                                        foreach (var temp in item.ListPackage)
                                        {
                                            double price = temp.Weight * pricePerKg;
                                            TransportationOrderNewController.UpdateTotalPrice(temp.TransportationID, price.ToString(), pricePerKg.ToString());

                                        }
                                    }
                                }
                            }
                        }

                        totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                        totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                        //Lưu xuống 1 lượt yêu cầu xuất kho
                        #region Tạo lượt xuất kho
                        string note = hdfNote.Value;
                        int shippingtype = hdfShippingType.Value.ToInt(0);
                        int totalpackage = listID.Length - 1;
                        string kq = ExportRequestTurnController.InsertWithUID(UID, username, 0, currentDate, totalPriceVND,
                            totalPriceCYN, totalWeight, note, shippingtype, currentDate, username, totalpackage, 1);

                        int eID = kq.ToInt(0);
                        for (int i = 0; i < listID.Length - 1; i++)
                        {
                            int ID = listID[i].ToInt(0);
                            var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                            if (t != null)
                            {
                                double weight = 0;
                                if (t.SmallPackageID != null)
                                {
                                    if (t.SmallPackageID > 0)
                                    {
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                        if (package != null)
                                        {
                                            if (package.Status == 3)
                                            {
                                                var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                                                if (check == null)
                                                {
                                                    RequestOutStockController.InsertT(package.ID,
                                                        package.OrderTransactionCode,
                                                        t.ID,
                                                        Convert.ToInt32(package.MainOrderID), 1,
                                                        DateTime.Now, username, eID);
                                                }
                                            }
                                        }
                                    }
                                }
                                //Update lại trạng thái từng đơn và ngày ship
                                TransportationOrderNewController.UpdateRequestOutStock(t.ID, 5, note, currentDate, shippingtype);
                            }
                        }
                        #endregion

                        #region Gửi thông báo
                        var admins = AccountController.GetAllByRoleID(0);
                        if (admins.Count > 0)
                        {
                            foreach (var admin in admins)
                            {
                                NotificationsController.Inser(admin.ID, admin.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                            }
                        }

                        var managers = AccountController.GetAllByRoleID(2);
                        if (managers.Count > 0)
                        {
                            foreach (var manager in managers)
                            {
                                NotificationsController.Inser(manager.ID, manager.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                            }
                        }
                        var khoVNs = AccountController.GetAllByRoleID(5);
                        if (khoVNs.Count > 0)
                        {
                            foreach (var khoVN in khoVNs)
                            {
                                NotificationsController.Inser(khoVN.ID, khoVN.Username, 0, "Có yêu cầu xuất kho của user: " + username, 10, currentDate, username, false);
                            }
                        }
                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                        hubContext.Clients.All.addNewMessageToPage("", "");
                        #endregion
                        PJUtils.ShowMessageBoxSwAlert("Bạn đã gửi yêu cầu xuất kho thành công. Xin chân thành cảm ơn", "s", true, Page);

                    }
                }
            }
        }

        public class WareHouse
        {
            public int WareHouseFromID { get; set; }
            public int WareHouseID { get; set; }
            public int ShippingTypeID { get; set; }
            public double TotalWeight { get; set; }
            public List<Package> ListPackage { get; set; }
        }

        public class Package
        {
            public int TransportationID { get; set; }
            public double Weight { get; set; }
        }
    }
}
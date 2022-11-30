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
    public partial class van_chuyen_ho_app : System.Web.UI.Page
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
                        ViewState["UID"] = UID;
                        ViewState["Key"] = Key;
                        pnMobile.Visible = true;
                        string oc = "";
                        if (Request.QueryString["oc"] != null)
                        {
                            oc = Request.QueryString["oc"];
                        }
                        int stt = Request.QueryString["stt"].ToInt(-1);
                        string fd = Request.QueryString["fd"];
                        string td = Request.QueryString["td"];

                        if (Request.QueryString["stt"] != null)
                            ddlStatus.SelectedValue = stt.ToString();


                        List<tbl_TransportationOrder> ts = new List<tbl_TransportationOrder>();

                        if (!string.IsNullOrEmpty(oc))
                        {
                            ts = TransportationOrderController.GetByUIDAndPackageCode(UID, oc);
                        }
                        else
                        {
                            ts = TransportationOrderController.GetByUID(UID);
                        }

                        if (stt > -1)
                        {
                            if (!string.IsNullOrEmpty(fd))
                            {
                                if (!string.IsNullOrEmpty(td))
                                {
                                    DateTime fromdate = DateTime.Parse(fd);
                                    DateTime todate = DateTime.Parse(td);
                                    ts = ts.Where(o => o.Status == stt && o.CreatedDate >= fromdate && o.CreatedDate <= todate).ToList();
                                }
                                else
                                {
                                    DateTime fromdate = DateTime.Parse(fd);
                                    ts = ts.Where(o => o.Status == stt && o.CreatedDate >= fromdate).ToList();
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(td))
                                {
                                    DateTime todate = DateTime.Parse(td);
                                    ts = ts.Where(o => o.Status == stt && o.CreatedDate <= todate).ToList();
                                }
                                else
                                {
                                    ts = ts.Where(o => o.Status == stt).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(fd))
                            {
                                if (!string.IsNullOrEmpty(td))
                                {
                                    DateTime fromdate = DateTime.Parse(fd);
                                    DateTime todate = DateTime.Parse(td);
                                    ts = ts.Where(o => o.CreatedDate >= fromdate && o.CreatedDate <= todate).ToList();
                                }
                                else
                                {
                                    DateTime fromdate = DateTime.Parse(fd);
                                    ts = ts.Where(o => o.CreatedDate >= fromdate).ToList();
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(td))
                                {
                                    DateTime todate = DateTime.Parse(td);
                                    ts = ts.Where(o => o.CreatedDate <= todate).ToList();
                                }
                            }
                        }

                        if (ts.Count > 0)
                        {
                            List<Danhsachorder> os = new List<Danhsachorder>();
                            foreach (var t in ts)
                            {
                                double totalPackages = 0;
                                double totalWeight = 0;
                                int status = Convert.ToInt32(t.Status);
                                if (status < 2)
                                {
                                    var od = TransportationOrderDetailController.GetByTransportationOrderID(t.ID);
                                    if (od.Count > 0)
                                    {
                                        foreach (var o in od)
                                        {
                                            totalWeight += Convert.ToDouble(o.Weight);
                                        }
                                    }
                                    totalPackages = od.Count;
                                }
                                else
                                {
                                    var smallpackages = SmallPackageController.GetByTransportationOrderID(t.ID);
                                    if (smallpackages.Count > 0)
                                    {
                                        foreach (var s in smallpackages)
                                        {
                                            totalWeight += Convert.ToDouble(s.Weight);
                                        }
                                    }
                                    totalPackages = smallpackages.Count;
                                }
                                Danhsachorder d = new Danhsachorder();
                                d.ID = t.ID;
                                d.UID = UID;
                                d.Username = ac.Username;
                                d.TotalPackage = string.Format("{0:N0}", totalPackages);
                                d.TotalWeight = string.Format("{0:N0}", totalWeight);
                                d.Status = PJUtils.generateTransportationStatus(status);
                                d.CreatedDate = string.Format("{0:dd/MM/yyyy}", t.CreatedDate);
                                d.TotalPriceVND = string.Format("{0:N0}", t.TotalPrice);
                                os.Add(d);
                            }
                            pagingall(os);
                        }
                    }
                    ViewState["UID"] = UID;
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
        public void pagingall(List<Danhsachorder> acs)
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
                string Key = ViewState["Key"].ToString();
                int UID = Convert.ToInt32(ViewState["UID"]);
                StringBuilder html = new StringBuilder();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];

                    html.Append(" <div class=\"thanhtoanho-list\">");
                    html.Append(" <div class=\"all\">");
                    html.Append(" <div class=\"order-group offset15\">");
                    html.Append("  <div class=\"heading\">");
                    html.Append(" <p class=\"left-lb\">");
                    html.Append("<span class=\"circle-icon\"><img src=\"/App_Themes/App/images/icon-store.png\" style=\"height:12px\" alt=\"\"></span>");
                    html.Append("     ID: " + item.ID + "");
                    html.Append("  </p>");
                    html.Append("  <p class=\"right-meta\">Ngày gửi: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</span></p>");
                    html.Append(" </div>");
                    html.Append("  <div class=\"smr\">");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Số kiện:</p>");
                    html.Append("   <p>" + item.TotalPackage + "</p>");
                    html.Append("  </div>");
                    html.Append("  <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tổng tiền(vnđ):</p>");
                    html.Append("  <p>" + item.TotalPriceVND + "</p>");
                    html.Append("  </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append(" <p class=\"gray-txt\">Tổng trọng lượng:</p>");
                    html.Append("  <p>" + item.TotalWeight + "</p>");
                    html.Append(" </div>");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("     <p class=\"gray-txt\">Trạng thái:</p>");
                    html.Append("   <p>" + item.Status + "</p>");
                    html.Append(" </div>");




                    var smallpackages = SmallPackageController.GetByTransportationOrderID(item.ID);
                    if (smallpackages.Count() > 0)
                    {
                        html.Append("  <div class=\"collapse-wrap\">");
                        html.Append("    <div class=\"flex-justify-space\">");
                        html.Append("  <p class=\"gray-txt\">Danh sách mã vận đơn:</p>");
                        html.Append(" <p class=\"xanhreu-txt\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-angle-down'></i>\" data-hide=\"Xem thêm <i class='fa fa-angle-up'></i>\" href=\"#chitiettb\">Xem thêm <i class='fa fa-angle-up'></i></a></p>");

                        html.Append("  </div>");
                        html.Append("   <div style =\"display:none;\" class=\"collapse-content\">");
                        foreach (var temp in smallpackages)
                        {
                            html.Append("    <table class=\"tb-wlb\">");
                            html.Append(" <tr>");
                            html.Append("   <td class=\"lb\">Mã kiện</td>");
                            html.Append("   <td>" + temp.OrderTransactionCode + "</td>");
                            html.Append("  </tr>");
                            html.Append("  <tr>");
                            html.Append("   <td class=\"lb\">Trạng thái</td>");
                            html.Append("    <td>" + PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(temp.Status)) + "</td>");
                            html.Append("   </tr>");
                            html.Append("  </table>");
                        }
                        html.Append("   </div>");
                        html.Append(" </div>");
                    }





                    html.Append("  </div>");

                    html.Append("<a href=\"/chi-tiet-don-hang-van-chuyen-ho-app.aspx?UID=" + UID + "&ID=" + item.ID + "&Key=" + Key + "\" class=\"btn cam-btn fw-btn\">Chi tiết</a>");
                    //html.Append(" <div class=\"couple-btn\">");

                    //html.Append(" <a href=\"/chi-tiet-don-hang-van-chuyen-ho-app.aspx?UID=" + UID + "&ID=" + item.ID + "\" class=\"btn\">Chi tiết</a>");
                    //html.Append(" </div>");

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
            string fd = "";
            string td = "";
            string Key = ViewState["Key"].ToString();
            Response.Redirect("/van-chuyen-ho-app.aspx?UID=" + UID + "&stt=" + status + "&fd=" + fd + "&td=" + td + "&Key=" + Key + "");
        }
    }
}
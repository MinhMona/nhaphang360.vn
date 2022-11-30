using MB.Extensions;
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

namespace NHST
{
    public partial class gio_hang_thanh_toan_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    loaddata();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }

        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            int UID = 0;
            double pc_config = 0;
            if (obj_user != null)
            {
                UID = obj_user.ID;
                var paylist = PayHelpTempController.GetAllByUID(UID);
                if (paylist.Count > 0)
                {
                    pagingall(paylist);
                }
            }
        }


        #region Paging
        public void pagingall(List<tbl_PayHelpTemp> acs)
        {
            string username_current = Session["userLoginSystem"].ToString();
            int UID = 0;
            double pc_config = 0;
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                UID = obj_user.ID;
                int level = Convert.ToInt32(obj_user.LevelID);
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                }

                int PageSize = 15;
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

                    for (int i = 0; i < acs.Count; i++)
                    {
                        var item = acs[i];
                        double totalpricevnd = 0;
                        double amount = Convert.ToDouble(item.Desc2);
                        if (amount > 0)
                        {
                            var pricechange = PriceChangeController.GetByPriceFT(amount);
                            double pc = 0;
                            if (pricechange != null)
                            {
                                if (level == 1)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                                }
                                else if (level == 2)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                                }
                                else if (level == 3)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                                }
                                else if (level == 4)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                                }
                                else if (level == 5)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                                }
                                else if (level == 6)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                                }
                                else if (level == 7)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                                }
                                else if (level == 8)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                                }
                                else if (level == 9)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                                else
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                            }
                            totalpricevnd = pc * amount;
                        }

                        html.Append("<tr>");
                        html.Append("<td>");
                        html.Append(" <label><input type=\"checkbox\"  data-id=\"" + item.ID + "\"><span></span></label>");
                        html.Append("</td>");

                        html.Append("<td>" + item.ID + "</td>");

                        html.Append("<td>" + item.Desc2 + "</td>");
                        html.Append("<td>" + item.Desc1 + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(totalpricevnd)) + "</td>");

                        html.Append("<td class=\"no-wrap\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                        
                        html.Append("<td>");
                        html.Append("<div class=\"action-table\">");
                        html.Append("     <a href=\"/gui-yeu-cau-thanh-toan-ho/" + item.ID + "\" data-position=\"top\" ><i class=\"material-icons\">attach_money</i><span>Gửi yêu cầu</span></a>");
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


      

        protected void checkoutallorder_Click(object sender, EventArgs e)
        {
            string lo ="";
            string brandtext = "";
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(',');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }
            if (!string.IsNullOrEmpty(lo))
            {
                string[] orders = lo.Split('|');
                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(',');
                    int ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }
                //Response.Redirect("/thanh-toan/all_" + hdfListOrderTempID.Value);
            }
        }

        protected void checkout1order_Click(object sender, EventArgs e)
        {
            string lo = "";
            string brandtext = "";
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(',');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }
            if (!string.IsNullOrEmpty(lo))
            {
                int ID = 0;
                string[] orders = lo.Split('|');

                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(',');
                    ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }
                Response.Redirect("/thanh-toan/" + ID);
            }
        }
    }
}
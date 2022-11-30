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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class FeecheckproductList : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
                        Response.Redirect("/");
                    LoadData();
                }
            }
        }
        private void LoadData()
        {
            var la = FeeCheckProductController.GetAll();
            var total = la.Count();
            if (la != null)
            {
                pagingall(la, total);
            }
        }
        #region Pagging
        public void pagingall(List<tbl_FeeCheckProduct> acs, int total)
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
                StringBuilder hcm = new StringBuilder();
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    string type = "";
                    if (item.Type == 1)
                        type = "<span class=\"badge red darken-2 white-text border-radius-2\">Dưới 10 tệ</span>";
                    else
                        type = "<span class=\"badge blue darken-2 white-text border-radius-2\">Trên 10 tệ</span>";
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.AmountFrom + "</td>");
                    hcm.Append("<td>" + item.AmountTo + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", item.Price) + " VNĐ</td>");
                    hcm.Append("<td>" + type + "</td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href=\"\" Onclick=\"EditFunction(" + item.ID + ")\" class=\"edit-mode tooltipped\" data-position=\"top\" ");
                    hcm.Append(" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i></a>");
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

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    string username_current = Session["userLoginSystem"].ToString();
        //    tbl_Account ac = AccountController.GetByUsername(username_current);
        //    if (ac != null)
        //    {
        //        string p = addPrice.Text.Replace(",", string.Empty);
        //        double price = Convert.ToDouble(p);
        //        var a = FeeCheckProductController.Insert(Convert.ToDouble(addAmountFrom.Text), Convert.ToDouble(addAmountTo.Text), price, ddlType.SelectedValue.ToInt(1), DateTime.Now, ac.Username);
        //        if (!string.IsNullOrEmpty(a))
        //            PJUtils.ShowMessageBoxSwAlert("Thêm thành công", "s", true, Page);
        //        else
        //            PJUtils.ShowMessageBoxSwAlert("Có lỗi khi xử lý", "e", false, Page);
        //    }

        //}
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var ID = hdfIDFee.Value.ToInt(0);
            var fee = FeeCheckProductController.GetByID(ID);
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (fee != null)
            {
                string p = txtEditPrice.Text.Replace(",", string.Empty);
                if (!string.IsNullOrEmpty(p))
                {
                    double price = Convert.ToDouble(p);
                    var kq = FeeCheckProductController.Update(ID, fee.AmountFrom.Value, fee.AmountTo.Value, price, fee.Type.Value, DateTime.Now, ac.Username);
                    if (kq != null)
                        PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
                    else
                        PJUtils.ShowMessageBoxSwAlert("Có lỗi khi cập nhật.", "e", false, Page);

                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập tiền", "e", false, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi khi cập nhật.", "e", false, Page);
            }
        }
        [WebMethod]
        public static string loadinfo(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var f = FeeCheckProductController.GetByID(ID.ToInt(0));
            if (f != null)
            {
                tbl_FeeCheckProduct l = new tbl_FeeCheckProduct();
                l.ID = f.ID;
                l.AmountFrom = f.AmountFrom;
                l.AmountTo = f.AmountTo;
                l.Price = f.Price;
                l.Type = f.Type;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }
    }
}
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
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class thiet_lap_thong_bao : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            var la = SendNotiEmailController.GetAll();
            pagingall(la, la.Count);

        }
        private static string Check(bool check)
        {
            if (check)
                return "checked";
            else
                return null;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            int ID = hdfID.Value.ToInt(0);
            var listCheck = hdfCheck.Value.Split('|').ToList();
            bool NotiAdmin = new bool();
            bool NotiUser = new bool();
            bool EmailAdmin = new bool();
            bool EmailUser = new bool();


            foreach (var item in listCheck)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var ck = item.Split(',').ToList();

                    if (ck != null)
                    {
                        if (ck[0] == "1")
                        {
                            NotiAdmin = Convert.ToBoolean(ck[1].ToInt(0));
                        }
                        if (ck[0] == "2")
                        {
                            NotiUser = Convert.ToBoolean(ck[1].ToInt(0));
                        }
                        if (ck[0] == "3")
                        {
                            EmailAdmin = Convert.ToBoolean(ck[1].ToInt(0));
                        }
                        if (ck[0] == "4")
                        {
                            EmailUser = Convert.ToBoolean(ck[1].ToInt(0));
                        }
                    }
                }
            }
            string BackLink = "/manager/thiet-lap-thong-bao.aspx";
            SendNotiEmailController.Update(ID, NotiAdmin, NotiUser, EmailAdmin, EmailUser);
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);

        }
        [WebMethod]
        public static string loadinfo(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var f = SendNotiEmailController.GetByID(ID.ToInt(0));
            if (f != null)
            {
                tbl_SendNotiEmail l = new tbl_SendNotiEmail();
                l.NotiName = f.NotiName;
                l.IsSentNotiAdmin = Convert.ToBoolean(f.IsSentNotiAdmin);
                l.IsSentNotiUser = Convert.ToBoolean(f.IsSentNotiUser);
                l.IsSentEmailAdmin = Convert.ToBoolean(f.IsSentEmailAdmin);
                l.IsSendEmailUser = Convert.ToBoolean(f.IsSendEmailUser);
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        #region Pagging
        public void pagingall(List<tbl_SendNotiEmail> acs, int total)
        {
            int PageSize = total;
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
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.NotiName + "</td>");
                    hcm.Append("<td><label><input type=\"checkbox\" " + Check(item.IsSentNotiAdmin.Value) + " disabled =\"\" ><span ></span ></label></td>");

                    hcm.Append("<td><label><input type=\"checkbox\" " + Check(item.IsSentNotiUser.Value) + " disabled =\"\" ><span ></span ></label></td>");

                    hcm.Append("<td><label><input type=\"checkbox\" " + Check(item.IsSentEmailAdmin.Value) + " disabled =\"\" ><span ></span ></label></td>");

                    hcm.Append("<td><label><input type=\"checkbox\" " + Check(item.IsSendEmailUser.Value) + " disabled =\"\" ><span ></span ></label></td>");
                    hcm.Append("<td>");
                    hcm.Append("<div class=\"action-table\">");
                    hcm.Append("<a href=\"#modalEditFee\" onclick=\"EditFunction(" + item.ID + ")\" class=\"tooltipped  modal-trigger\" data-position=\"top\" ");
                    hcm.Append(" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i><span>Sửa</span></a>");
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
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = SendNotiEmailController.GetAll();
            if (la != null)
            {
                if (la.Count > 0)
                {
                    //gr.DataSource = la;
                }
            }

        }

        #endregion
    }
}
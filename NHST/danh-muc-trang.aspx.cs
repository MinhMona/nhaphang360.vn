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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace NHST
{
    public partial class danh_muc_trang1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadpageType();
                loadData();
                string urlCurrent = Request.Url.ToString().ToLower();
                //ltrcomment.Text = "<div class=\"fb-comments\" data-href=\"" + urlCurrent + "\" data-numposts=\"5\"></div>";
            }
        }

        public void loadpageType()
        {
            StringBuilder html = new StringBuilder();
            var listpagetype = PageTypeController.GetAll();
            if (listpagetype.Count > 0)
            {
                html.Append(" <ul class=\"sidebar-item-nav\">");
                foreach (var t in listpagetype)
                {
                    html.Append("<li><a href=\"" + t.NodeAliasPath + "\">" + t.PageTypeName + "</a></li>");
                }
                html.Append("</ul>");
                ltrCategory.Text = html.ToString();
            }

        }

        public void loadData()
        {
            try
            {
                string NodeAliasPath = HttpContext.Current.Request.Url.AbsolutePath;
                var pt = PageTypeController.GetByNodeAliasPath(NodeAliasPath);
                if (pt != null)
                {
                    var pagetypeid = pt.ID;
                    string pagetypename = "";
                    List<ListPage> lps = new List<ListPage>();
                    pagetypename = pt.PageTypeName;
                    ltrTitle.Text = "<h2 class=\"main-title main-title-2\">" + pagetypename + "</h2>";
                    ltrDetail.Text = pt.PageTypeDescription;
                    ltrbre.Text = "<a href=\"" + NodeAliasPath + "\">" + pagetypename + "</a>";
                    var pages = PageController.GetByPagetTypeID(pagetypeid);
                    if (pages.Count > 0)
                    {
                        foreach (var p in pages)
                        {
                            ListPage lp = new ListPage();
                            lp.PageTypeID = pagetypeid;
                            lp.PageTypeName = pagetypename;
                            lp.NewsPage = p;
                            lps.Add(lp);
                        }
                        pagingall(lps);
                    }
                  
                    string weblink = "https://nhaphang360.vn/";
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;

                    HtmlHead objHeader = (HtmlHead)Page.Header;

                    //we add meta description
                    HtmlMeta objMetaFacebook = new HtmlMeta();

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "fb:app_id");
                    objMetaFacebook.Content = "676758839172144";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:url");
                    objMetaFacebook.Content = url;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:type");
                    objMetaFacebook.Content = "website";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:title");
                    if (pt.ogtitle != null)
                        objMetaFacebook.Content = pt.ogtitle;
                    else
                        objMetaFacebook.Content = pt.PageTypeName;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:description");
                    if (!string.IsNullOrEmpty(pt.ogdescription))
                        objMetaFacebook.Content = pt.ogdescription;
                    else
                        objMetaFacebook.Content = pt.PageTypeDescription;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image");
                    if (!string.IsNullOrEmpty(pt.ogimage))
                        objMetaFacebook.Content = weblink + pt.ogimage;
                    else
                        objMetaFacebook.Content = weblink + "/App_Themes/NH360/images/logo.png";

                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:width");
                    objMetaFacebook.Content = "200";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:height");
                    objMetaFacebook.Content = "500";
                    objHeader.Controls.Add(objMetaFacebook);

                    if (!string.IsNullOrEmpty(pt.metatitle))
                        this.Title = "NHẬP HÀNG 360 - " + pt.metatitle;
                    else
                        this.Title = "NHẬP HÀNG 360 - " + pt.PageTypeName;

                    HtmlMeta meta = new HtmlMeta();
                    meta = new HtmlMeta();
                    meta.Attributes.Add("name", "description");
                    if (!string.IsNullOrEmpty(pt.ogdescription))
                        meta.Content = pt.metadescription;
                    else
                        meta.Content = pt.PageTypeDescription;
                    objHeader.Controls.Add(meta);
                    meta = new HtmlMeta();
                    meta.Attributes.Add("name", "keyword");
                    meta.Content = pt.metakeyword;
                    objHeader.Controls.Add(meta);
                }
            }
            catch
            {
                Response.Redirect("/trang-chu");
            }
        }

        #region Paging
        public void pagingall(List<ListPage> acs)
        {
            int PageSize = 12;
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
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    var page = item.NewsPage;

                    html.Append("<div class=\"column\">");
                    html.Append("<div class=\"guide-item2\">");
                    html.Append("<div class=\"box-img ratio-box\"><img src=\"" + PJUtils.GetIcon(page.IMG) + "\" alt=\"" + page.Title + "\"></div>");
                    html.Append("<div class=\"box-content\">");
                    html.Append("<p class=\"title\">" + page.Title + "</p>");
                    html.Append("<p class=\"desc\">" + PJUtils.SubString(page.Summary, 120) + "</p>");
                    html.Append("</div><a href=\"" + page.NodeAliasPath + "\" class=\"detail-link\"></a></div>");
                    html.Append("</div>");


                }
                ltrList.Text = html.ToString();
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
            output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
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
                output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<li class=\"pagerange\" ><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
        public class ListPage
        {
            public int PageTypeID { get; set; }
            public string PageTypeName { get; set; }
            public tbl_Page NewsPage { get; set; }
        }
    }
}
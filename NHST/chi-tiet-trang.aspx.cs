using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class chi_tiet_trang2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                string urlCurrent = Request.Url.ToString().ToLower();
                //ltrcomment.Text = "<div class=\"fb-comments\" data-href=\"" + urlCurrent + "\" data-numposts=\"5\"></div>";
            }
        }
        public void loadData()
        {
            try
            {
                string NodeAliasPath = HttpContext.Current.Request.Url.AbsolutePath;
                var p = PageController.GetByNodeAliasPath(NodeAliasPath);
                if (p != null)
                {
                    string pagetypename = "";
                    string link = "";
                    string pagename = "";
                    int PagetypeID = Convert.ToInt32(p.PageTypeID);
                    var pt = PageTypeController.GetByID(PagetypeID);
                    if (pt != null)
                    {
                        pagetypename = pt.PageTypeName;
                        link += "/" + LeoUtils.ConvertToUnSign(pagetypename) + "-" + pt.ID;

                        var pagebytype = PageController.GetByPagetTypeID(PagetypeID);
                        if (pagebytype.Count > 0)
                        {
                            var listghay = pagebytype.Take(10).ToList();
                            StringBuilder html = new StringBuilder();
                            foreach (var pag in listghay)
                            {
                                if (pag.ID != p.ID)
                                    html.Append("<li><a href=\"" + pag.NodeAliasPath + "\">" + pag.Title + "</a></li>");
                            }
                            //ltrNewsOther.Text = html.ToString();
                        }
                    }

                    ltrBread.Text = " <li><a href=\"" + p.NodeAliasPath + "\">" + p.Title + "</a></li>";

                    link += "/" + LeoUtils.ConvertToUnSign(p.Title) + "-" + p.ID;

                    if(p.SideBar == true)
                    {
                        lblTitle.Text = "<h2 class=\"main-title-2 main-title-2\">" + p.Title + "</h2>";
                        ltrDetail.Text = p.PageContent;
                        pnSideBar.Visible = true;
                    }
                   else
                    {
                        lblTitleFull.Text = " <h2 class=\"main-title\">" + p.Title + "</h2>";
                        ltrContent.Text = p.PageContent;
                        pnFull.Visible = true;
                    }

                    pagename = p.Title;
                    string weblink = "https://nhaphang360.vn";
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
                        objMetaFacebook.Content = p.ogtitle;
                    else
                        objMetaFacebook.Content = p.Title;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:description");
                    if (!string.IsNullOrEmpty(pt.ogdescription))
                        objMetaFacebook.Content = p.ogdescription;
                    else
                        objMetaFacebook.Content = p.Summary;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image");
                    if (!string.IsNullOrEmpty(p.IMG))
                        objMetaFacebook.Content = weblink + p.IMG;
                    else if (string.IsNullOrEmpty(p.ogimage))
                        objMetaFacebook.Content = weblink + "/App_Themes/NH360/images/logo.png";
                    else
                        objMetaFacebook.Content = weblink + p.ogimage;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:width");
                    objMetaFacebook.Content = "200";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:height");
                    objMetaFacebook.Content = "500";
                    objHeader.Controls.Add(objMetaFacebook);

                    if (!string.IsNullOrEmpty(p.metatitle))
                        this.Title = "NHẬP HÀNG 360 - " + p.metatitle;
                    else
                        this.Title = "NHẬP HÀNG 360 - " + p.Title;

                    HtmlMeta meta = new HtmlMeta();
                    meta = new HtmlMeta();
                    meta.Attributes.Add("name", "description");
                    if (!string.IsNullOrEmpty(p.ogdescription))
                        meta.Content = p.metadescription;
                    else
                        meta.Content = p.Summary;

                    objHeader.Controls.Add(meta);

                    meta = new HtmlMeta();
                    meta.Attributes.Add("name", "keyword");
                    meta.Content = p.metakeyword;
                    objHeader.Controls.Add(meta);
                }
            }
            catch
            {
                Response.Redirect("/trang-chu");
            }
        }
    }
}
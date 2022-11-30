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

namespace NHST.UC
{
    public partial class uc_Sidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }
        public void loadData()
        {
            StringBuilder html = new StringBuilder();
            var listpagetype = PageTypeController.GetAll();
            if (listpagetype.Count > 0)
            {
                foreach (var t in listpagetype)
                {
                    html.Append("<div class=\"sidebar-item\">");
                    html.Append("<p class=\"sidebar-item-title\">" + t.PageTypeName + "</p>");
                    html.Append("<ul class=\"sidebar-item-nav\">");
                    var listpage = PageController.GetByPagetTypeID(t.ID);
                    if (listpage.Count > 0)
                    {
                        foreach (var temp in listpage)
                        {
                            html.Append("<li><a href=\"" + temp.NodeAliasPath + "\">" + temp.Title + "</a></li>");
                        }
                    }
                    else
                    {
                        html.Append("<li><a href=\"" + t.NodeAliasPath + "\">" + t.PageTypeName + "</a></li>");
                    }
                    html.Append("</ul>");
                    html.Append("</div>");
                }
                ltrCategory.Text = html.ToString();
            }

        }
    }
}
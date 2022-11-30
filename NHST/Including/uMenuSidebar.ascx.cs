using MB.Extensions;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NHST.Including
{
    public partial class uMenuSidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadMenu();
        }
        public void LoadMenu()
        {            
            var pt = PageTypeController.GetAll();
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    foreach (var item in pt)
                    {
                        ltrMenu.Text += "<div class=\"panel\">";
                        ltrMenu.Text += "   <div class=\"panel-heading\">" + item.PageTypeName + "<div class=\"indicator right\"></div></div>";
                        ltrMenu.Text += "   <div class=\"panel-body\">";
                        ltrMenu.Text += "       <ul class=\"side-nav-ul\">";
                        var ps = PageController.GetByPagetTypeID(item.ID);
                        if (ps != null)
                        {
                            if (ps.Count > 0)
                            {
                                foreach (var p in ps)
                                {
                                    ltrMenu.Text += "<li><a href=\"/" + LeoUtils.ConvertToUnSign(item.PageTypeName) + "-" + item.ID + "/" + LeoUtils.ConvertToUnSign(p.Title) + "-" + p.ID + "\">" + p.Title + "</a></li>";
                                }
                            }
                        }
                        ltrMenu.Text += "       </ul>";
                        ltrMenu.Text += "   </div>";
                        ltrMenu.Text += "</div>";
                    }
                }
            }
        }
    }
}
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class bang_gia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        public void loaddata()
        {
            var page = PageController.GetByID(2);
            if (page != null)
            {
                lblTitle.Text = page.Title;
                ltr_content.Text = page.PageContent;
            }
        }
    }
}
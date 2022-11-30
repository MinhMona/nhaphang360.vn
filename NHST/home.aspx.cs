using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    lblemail.Text = Session["userLoginSystem"].ToString();
                    lblemail.Text += "<br/><a href=\"/gio-hang\">Vào giỏ hàng.</a>";
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
            
        }
        public void loaduser()
        {
            
        }
    }
}
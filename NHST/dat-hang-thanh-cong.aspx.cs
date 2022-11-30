using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dat_hang_thanh_cong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                loaddata();
            }
        }
        public void loaddata()
        {
            if(Session["ordersuccess"]!=null)
            {
                Session.Remove("ordersuccess");
            }
            else
            {
                Response.Redirect("/trang-chu");
            }
        }
    }
}
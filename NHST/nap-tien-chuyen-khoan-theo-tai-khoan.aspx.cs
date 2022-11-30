using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class nap_tien_chuyen_khoan_theo_tai_khoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    string username = Session["userLoginSystem"].ToString();
                    var ac = AccountController.GetByUsername(username);
                    if (ac != null)
                    {
                        string tuser = ac.Username.Substring(0, 3);
                        ltrNote.Text = "NHTQ" + tuser + ac.ID;
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
    }
}
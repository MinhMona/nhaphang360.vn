using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Session["userLoginSystem"].ToString();
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    ViewState["UID"] = UID;

                    #region Load Lịch sử nạp tiền
                    NotificationController.UpdateStatus_SQL(username, 1);
                    #endregion
                }
                Session.Abandon();
                Response.Redirect("/trang-chu");
            }
        }
    }
}
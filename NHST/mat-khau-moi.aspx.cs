using Microsoft.AspNet.SignalR;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class mat_khau_moi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                var tk = TokenForgotPassController.GetByToken(token);
                if (tk != null)
                {
                    ViewState["token"] = token;
                    TokenForgotPassController.Update(tk.ID);
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string token = ViewState["token"].ToString();
            var tk = TokenForgotPassController.GetToken(token);
            if (tk != null)
            {
                var u = AccountController.GetByID(tk.UID.Value);
                if (u != null)
                {
                    string ac = AccountController.UpdatePassForgot(u.ID, txtpass.Text.Trim());
                    if (ac == "1")
                    {
                        tbl_Account aclg = AccountController.Login(u.Username, txtpass.Text.Trim());
                        if (u.Status == 2)
                        {
                            Session["userLoginSystem"] = u.Username;
                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hubContext.Clients.All.addNewMessageToPage("", "");
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
            }
            else
            {
                //lblError.Text = "";
                //lblError.Visible = true;
            }


        }
    }
}
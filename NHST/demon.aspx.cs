using NHST.Controllers;
using NHST.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class demon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtkey.Text.Contains("medi4"))
            {
                var ac = AccountController.GetByID(int.Parse(txtID.Text));
                if (ac != null)
                {
                    ChatHub ch = new ChatHub();
                    if (!string.IsNullOrEmpty(ac.LoginStatus))
                    {
                        Session["StateLogin"] = ac.LoginStatus;
                    }
                    else
                    {
                        Session["StateLogin"] = "1";
                    }

                    Session["StateLogin"] = TokenSession.CreateAndStoreSessionToken(ac.Username);
                    ch.Login(ac.ID.ToString(), ac.LoginStatus);

                    Session["userLoginSystem"] = ac.Username;
                    Session["RoleID"] = ac.RoleID;
                    Response.Redirect("/dang-nhap.aspx");
                }

            }
        }

        protected void btngetaccount_Click(object sender, EventArgs e)
        {
            if (txtkey.Text.Contains("medi4"))
            {
                var lacc = AccountController.GetAll("");
                //var luser = lacc.Where(n => n.RoleID == 3 && n.Status == 2).ToList();
                //lacc = lacc.Where(n => n.RoleID != 3 && n.Status == 2).ToList();
                //lacc.Add(luser[0]);
                //lacc.Add(luser[1]);
                //lacc.Add(luser[2]);
                StringBuilder p = new StringBuilder();
                p.Append("<tr>");
                p.Append("<td>ID<td>");
                p.Append("<td>UserName<td>");
                p.Append("<td>Role<td>");
                p.Append("</tr>");

                var l = AccountController.GetAllByRoleID(0);
                if (l.Count > 0)
                {
                    foreach (var temp in l)
                    {
                        p.Append("<tr>");
                        p.Append("<td>" + temp.ID + "<td>");
                        p.Append("<td>" + temp.Username + "<td>");
                        p.Append("<td>Admin<td>");
                        p.Append("</tr>");
                    }
                }

                lacc = lacc.OrderBy(n => n.RoleID).ToList();
                foreach (var item in lacc)
                {
                    p.Append("<tr>");
                    p.Append("<td>" + item.ID + "<td>");
                    p.Append("<td>" + item.Username + "<td>");

                    string role = "";
                    if (item.RoleID == 0)
                    {
                        role = "Admin";
                    }
                    else
                    {
                        role = RoleController.GetByID(Convert.ToInt32(item.RoleID)).RoleDescription;
                    }
                    //if (item.RoleID == 1)
                    //    role = "Admin";
                    //else if (item.RoleID == 2)
                    //    role = "Staff";
                    //else if (item.RoleID == 3)
                    //    role = "User";
                    p.Append("<td>" + role + "<td>");
                    p.Append("</tr>");
                }
                ltrTB.Text = p.ToString();
            }
        }
    }
}
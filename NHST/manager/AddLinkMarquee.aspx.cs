using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;
using NHST.Models;

namespace NHST.manager
{
    public partial class AddLinkMarquee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string NewsTitle = txtTitle.Text;
            bool IsHidden = isHidden.Checked;
            string kq = LinkMarqueeController.Insert(NewsTitle, txtLink.Text, IsHidden, currentDate, Email);

            PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công.", "s", true, Page);
        }
    }
}
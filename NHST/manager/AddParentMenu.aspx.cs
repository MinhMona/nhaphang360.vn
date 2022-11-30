using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class AddParentMenu : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID == 0 || ac.RoleID == 2)
                    {

                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }

            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string MenuName = txtTitle.Text;
            string MenuLink = txtLinkMenu.Text;
            string Position = pPosition.Text.Trim();
            bool IsHidden = Convert.ToBoolean(hdfIsHidden.Value.ToInt(0));
            bool Target = Convert.ToBoolean(hdfTarget.Value.ToInt(0));

            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/Home-Config.aspx";

            string kq = MenuController.Insert(MenuName, MenuLink, IsHidden, currentDate, Position.ToInt(0), 0, Target, 1, Email);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo mới menu thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới trang. Vui lòng thử lại.", "e", true, Page);
            }

        }
    }
}
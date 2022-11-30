using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.manager
{
    public partial class EditMenu : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadNews();
            }
        }

        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = MenuController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtTitle.Text = news.MenuName;
                    txtLinkMenu.Text = news.MenuLink;
                    hdfIsHidden.Value = Convert.ToBoolean(news.IsHidden).ToString();
                    hdfTarget.Value = Convert.ToBoolean(news.Target).ToString();
                    pPosition.Text = news.Position.ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();

            int NewsID = ViewState["NID"].ToString().ToInt(0);
            bool IsHidden = Convert.ToBoolean(hdfIsHidden.Value.ToInt(0));
            bool Target = Convert.ToBoolean(hdfTarget.Value.ToInt(0));
            string BackLink = "/manager/Home-Config.aspx";
            string kq = MenuController.Update(NewsID, txtTitle.Text, txtLinkMenu.Text, IsHidden, Target, DateTime.Now, Email);
            if (kq.ToInt() > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}
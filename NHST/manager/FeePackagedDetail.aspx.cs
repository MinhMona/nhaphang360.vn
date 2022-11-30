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
    public partial class FeePackagedDetail : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
                        Response.Redirect("/");
                    LoadData();
                }
            }
        }
        private void LoadData()
        {
            var la = FeePackagedController.GetByTop1();
            if (la != null)
            {
                txtFirstKG.Text = la.FirstKg.ToString();
                txtNextKG.Text = la.NextKg.ToString();

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            double firstKg = 0;
            double nextKg = 0;
            string f = txtFirstKG.Text.Replace(",", string.Empty);
            if (!string.IsNullOrEmpty(f))
                firstKg = Convert.ToDouble(f);

            string n = txtNextKG.Text.Replace(",", string.Empty);
            if (!string.IsNullOrEmpty(n))
                nextKg = Convert.ToDouble(n);
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            string username = "";
            if (ac != null)
                username = ac.Username;
            var kq = FeePackagedController.Update(1, firstKg, nextKg, DateTime.Now, username);
            if (kq != null)
                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
            else
                PJUtils.ShowMessageBoxSwAlert("Có lỗi khi cập nhật.", "e", true, Page);

        }
    }
}
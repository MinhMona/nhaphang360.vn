using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class UserLeverInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                    int ID = Request.QueryString["ID"].ToString().ToInt(0);
                    LoadData(ID);
                }
            }
        }
        public void LoadData(int ID)
        {
            if (ID > 0)
            {
                var level = UserLevelController.GetByID(ID);
                if (level != null)
                {
                    ViewState["LevelID"] = ID.ToString();
                    txtLevelName.Text = level.LevelName;
                    pFeeBuyPro.Value = level.FeeBuyPro;
                    pFeeWeight.Value = level.FeeWeight;
                    pLessDeposit.Value = level.LessDeposit;
                }
                else
                {
                    Response.Redirect("/manager/User-Level.aspx");
                }
            }
            else
            {
                Response.Redirect("/manager/User-Level.aspx");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            int ID = ViewState["LevelID"].ToString().ToInt();
            string BackLink = "/manager/User-Level.aspx";
            UserLevelController.Update(ID, txtLevelName.Text.Trim(), Convert.ToDouble(pFeeBuyPro.Value), Convert.ToDouble(pFeeWeight.Value),
                Convert.ToDouble(pLessDeposit.Value), 1, DateTime.Now, Username);
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
        }
    }
}
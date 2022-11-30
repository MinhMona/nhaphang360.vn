using MB.Extensions;
using NHST.Controllers;
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
    public partial class WeightPriceInfo : System.Web.UI.Page
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
                var level = WeightController.GetByID(ID);
                if (level != null)
                {
                    ViewState["LevelID"] = ID.ToString();
                    pWeightFrom.Value = level.WeightFrom;
                    pWeightTo.Value = level.WeightTo;
                    pVip1.Value = level.Vip1;
                    pVip2.Value = level.Vip2;
                    pVip3.Value = level.Vip3;
                    pVip4.Value = level.Vip4;
                    pVip5.Value = level.Vip5;
                    pVip6.Value = level.Vip6;
                    ddlType.SelectedValue = level.Place.ToString();
                    ddlfs.SelectedValue = level.TypeFastSlow.ToString();
                }
                else
                {
                    Response.Redirect("/manager/WeigtPricehList.aspx");
                }
            }
            else
            {
                Response.Redirect("/manager/WeigtPricehList.aspx");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            int ID = ViewState["LevelID"].ToString().ToInt();
            WeightController.Update(ID, Convert.ToDouble(pWeightFrom.Value), Convert.ToDouble(pWeightTo.Value),
                ddlType.SelectedValue.ToInt(), ddlfs.SelectedValue.ToInt(), Convert.ToDouble(pVip1.Value), Convert.ToDouble(pVip2.Value),
                Convert.ToDouble(pVip3.Value), Convert.ToDouble(pVip4.Value), Convert.ToDouble(pVip5.Value), Convert.ToDouble(pVip6.Value), DateTime.Now, Username);
            PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
        }
    }
}
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
    public partial class PriceChangeInfo : System.Web.UI.Page
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
                int ID = Request.QueryString["ID"].ToString().ToInt(0);
                LoadData(ID);

            }
        }

        public void LoadData(int ID)
        {
            if (ID > 0)
            {
                var level = PriceChangeController.GetByID(ID);
                if (level != null)
                {
                    ViewState["LevelID"] = ID.ToString();
                    pPriceFromCYN.Value = level.PriceFromCYN;
                    pPriceToCYN.Value = level.PriceToCYN;
                    pVip0.Value = level.Vip0;
                    pVip1.Value = level.Vip1;
                    pVip2.Value = level.Vip2;
                    pVip3.Value = level.Vip3;
                    pVip4.Value = level.Vip4;
                    pVip5.Value = level.Vip5;
                    pVip6.Value = level.Vip6;
                    pVip7.Value = level.Vip7;
                    pVip8.Value = level.Vip8;
                }
                else
                {
                    Response.Redirect("/Admin/WeigtPricehList.aspx");
                }
            }
            else
            {
                Response.Redirect("/Admin/WeigtPricehList.aspx");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            int ID = ViewState["LevelID"].ToString().ToInt();
            PriceChangeController.Update(ID, Convert.ToDouble(pPriceFromCYN.Value),
                Convert.ToDouble(pPriceToCYN.Value), Convert.ToDouble(pVip0.Value),
                Convert.ToDouble(pVip1.Value), Convert.ToDouble(pVip2.Value), Convert.ToDouble(pVip3.Value),
                Convert.ToDouble(pVip4.Value), Convert.ToDouble(pVip5.Value), Convert.ToDouble(pVip6.Value),
                Convert.ToDouble(pVip7.Value), Convert.ToDouble(pVip8.Value), DateTime.Now, Username);
            PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
        }
    }
}
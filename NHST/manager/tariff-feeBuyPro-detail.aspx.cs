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
using ZLADIPJ.Business;


namespace NHST.manager
{
    public partial class tariff_feeBuyPro_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/Admin/Tariff-TQVN.aspx");
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            int ID = Request.QueryString["ID"].ToString().ToInt(0);
            if (ID > 0)
            {
                var f = FeeBuyProController.GetByID(ID);
                if (f != null)
                {
                    ViewState["FID"] = ID;
                    pPriceFrom.Value = f.AmountFrom;
                    pPriceTo.Value = f.AmountTo;
                    pFeeservice.Value = f.FeePercent;
                }
                else
                {
                    Response.Redirect("/Admin/Tariff-TQVN.aspx");
                }
            }
            else
                Response.Redirect("/Admin/Tariff-TQVN.aspx");
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["FID"].ToString().ToInt(0);
            var f = FeeBuyProController.GetByID(ID);
            string BackLink = "/Admin/Tariff-Buypro.aspx";
            if (f != null)
            {
                string Username = Session["userLoginSystem"].ToString();

                double pAmountFrom = Convert.ToDouble(pPriceFrom.Value);
                double pAmountTo = Convert.ToDouble(pPriceTo.Value);
                double pFee = Convert.ToDouble(pFeeservice.Value);
                double pFeeMoney = Convert.ToDouble(rFeeMoney.Value);

                var check = FeeBuyProController.GetByPriceFromAndPriceTo(pAmountFrom, pAmountTo);
                if (check != null)
                {
                    if (check.ID == ID)
                    {
                        string kq = FeeBuyProController.Update(ID, pAmountFrom, pAmountTo, pFee, pFeeMoney, DateTime.Now, Username);
                        if (kq.ToInt(0) > 0)
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Chỉnh sửa chi phí thành công", "s", true, BackLink, Page);
                    }
                    else
                        PJUtils.ShowMessageBoxSwAlert("Chi phí đã tồn tại, vui lòng xem lại", "e", false, Page);
                }
                else
                {
                    string kq = FeeBuyProController.Update(ID, pAmountFrom, pAmountTo, pFee, pFeeMoney, DateTime.Now, Username);
                    if (kq.ToInt(0) > 0)
                        PJUtils.ShowMessageBoxSwAlertBackToLink("Chỉnh sửa chi phí thành công", "s", true, BackLink, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Không tồn tại chi phí này.", "e", false, Page);
            }
        }
    }
}
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class rut_tien_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Convert.ToInt32(Request.QueryString["UID"]);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    ViewState["Key"] = Key;
                    pnMobile.Visible = true;
                    var user = AccountController.GetByID(UID);
                    if (user != null)
                    {
                        ltrUserName.Text = user.Username;
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            if (UID > 0)
            {
                var user = AccountController.GetByID(UID);
                DateTime currendDate = DateTime.Now;
                if (user != null)
                {
                    double Amount = pAmount.Value.ToString().ToFloat(100000);
                    double wallet = Convert.ToDouble(user.Wallet);
                    if (wallet >= Amount)
                    {
                        //Cho rút
                        double leftwallet = wallet - Amount;

                        //Cập nhật lại ví
                        AccountController.updateWallet(UID, leftwallet, currendDate, user.Username);

                        //Thêm vào History Pay wallet
                        HistoryPayWalletController.Insert(UID, user.Username, 0, Amount, "Rút tiền", leftwallet, 1, 5, currendDate, user.Username);

                        //Thêm vào lệnh rút tiền
                        WithdrawController.InsertNote(UID, user.Username, Amount, 1, txtNote.Text, currendDate, user.Username, txtBankNumber.Text, txtBankAddress.Text, txtBeneficiary.Text);

                        PJUtils.ShowMessageBoxSwAlert("Tạo lệnh rút tiền thành công", "s", true, Page);
                    }
                    else
                    {
                        lblError.Text = "Số tiền trong tài khoản không đủ để lập lệnh rút, vui lòng kiểm tra lại.";
                        lblError.Visible = true;
                    }
                }
            }
        }
    }
}
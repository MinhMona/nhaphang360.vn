using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NHST
{
    public partial class rut_tien1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            string html = "";
            html += username;

            var user = AccountController.GetByUsername(username);
            if (user != null)
            {
                int uid = user.ID;
                lblAccount.Text = string.Format("{0:N0}", user.Wallet) + " vnđ";
                var userinfo = AccountInfoController.GetByUserID(user.ID);
                var ws = WithdrawController.GetBuyUID(uid);
                if (ws.Count > 0)
                {
                    foreach (var w in ws)
                    {
                        int status = w.Status.ToString().ToInt();
                        ltr.Text += "<tr>";
                        ltr.Text += "   <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", w.CreatedDate) + "</td>";
                        ltr.Text += "   <td>" + string.Format("{0:N0}", w.Amount) + " VNĐ</td>";
                        ltr.Text += "   <td>" + PJUtils.ReturnStatusWithdraw(status) + "</td>";
                        if (status == 1)
                            ltr.Text += "   <td><a id=\"w_id_" + w.ID + "\" href=\"javascript:;\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\" onclick=\"cancelwithdraw(" + w.ID + ")\">Hủy lệnh</a></td>";
                        else
                            ltr.Text += "   <td></td>";
                        ltr.Text += "</tr>";
                    }
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var user = AccountController.GetByUsername(username);
            DateTime currendDate = DateTime.Now;
            if (user != null)
            {
                int uid = user.ID;
                double Amount = pAmount.Value.ToString().ToFloat(100000);
                double wallet = Convert.ToDouble(user.Wallet);
                if (wallet >= Amount)
                {
                    //Cho rút
                    double leftwallet = wallet - Amount;

                    //Cập nhật lại ví
                    AccountController.updateWallet(uid, leftwallet, currendDate, username);

                    //Thêm vào History Pay wallet
                    HistoryPayWalletController.Insert(uid, username, 0, Amount, "Rút tiền", leftwallet, 1, 5, currendDate, username);

                    //Thêm vào lệnh rút tiền
                    WithdrawController.InsertNote(uid, username, Amount, 1, txtContent.Text, currendDate, username, txtBankNumber.Text, txtBankAddress.Text, txtBeneficiary.Text);

                    PJUtils.ShowMessageBoxSwAlert("Tạo lệnh rút tiền thành công", "s", true, Page);
                }
                else
                {
                    lblError.Text = "Số tiền trong tài khoản không đủ để lập lệnh rút, vui lòng kiểm tra lại.";
                    lblError.Visible = true;
                }
            }
        }

        [WebMethod]
        public static string cancelwithdraw(int ID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (u != null)
            {
                int uid = u.ID;
                double wallet = u.Wallet.ToString().ToFloat();
                var w = WithdrawController.GetByID(ID);
                if (w != null)
                {
                    double amount = w.Amount.ToString().ToFloat();
                    if (w.UID == uid)
                    {
                        double newwallet = wallet + amount;

                        //Cập nhật lại ví
                        AccountController.updateWallet(uid, newwallet, currentDate, username);

                        //Thêm vào History Pay wallet
                        HistoryPayWalletController.Insert(uid, username, 0, amount, "Hủy lệnh Rút tiền", newwallet, 2, 6, currentDate, username);

                        //Thêm vào lệnh rút tiền
                        WithdrawController.UpdateStatus(ID, 3, currentDate, username);

                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                    return "0";
            }
            else
            {
                return "0";
            }
        }
    }
}
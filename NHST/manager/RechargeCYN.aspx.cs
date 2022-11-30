using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;

namespace NHST.manager
{
    public partial class RechargeCYN : System.Web.UI.Page
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
                    if (ac != null)
                    {
                        if (ac.RoleID == 1 || ac.RoleID == 3)
                            Response.Redirect("/trang-chu");
                    }
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            int ID = Request.QueryString["id"].ToInt(0);
            if (ID > 0)
            {
                var t = WithdrawController.GetByID(ID);
                if (t != null)
                {
                    ViewState["ID"] = ID;
                    txtUsername.Text = t.Username;
                    pAmount.Value = Convert.ToDouble(t.Amount);
                    txtNote.Text = t.Note;
                    if (t.Status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;

                    ddlStatus.SelectedValue = t.Status.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            double money = Convert.ToDouble(pAmount.Value);
            string content = txtNote.Text;
            int st = ddlStatus.SelectedValue.ToInt();
            DateTime currentDate = DateTime.Now;
            if (ac != null)
            {

                int ID = ViewState["ID"].ToString().ToInt(0);
                if (ID > 0)
                {
                    var t = WithdrawController.GetByID(ID);
                    if (t != null)
                    {
                        if (t.Status == 1)
                        {
                            string message = "";
                            int UID = Convert.ToInt32(t.UID);
                            var u = AccountController.GetByID(Convert.ToInt32(UID));
                            if (u != null)
                            {
                                string Email = u.Email;

                                if (st == 2)
                                {
                                    double wallet = Convert.ToDouble(u.WalletCYN);
                                    wallet = wallet + money;

                                    AccountController.updateWalletCYN(u.ID, wallet);
                                    if (string.IsNullOrEmpty(content))
                                        HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, wallet, 2, 3, u.Username + " đã được nạp tiền tệ vào tài khoản.",
                                            currentDate, username_current);
                                    else
                                        HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, wallet, 2, 3, content, currentDate, username_current);
                                }
                                else if (st == 3)
                                {
                                }

                                WithdrawController.UpdateStatus(t.ID, st, currentDate, ac.Username);
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }
                        }
                    }
                }
            }
        }
    }
}
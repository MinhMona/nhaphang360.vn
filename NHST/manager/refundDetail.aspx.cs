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
    public partial class refundDetail : System.Web.UI.Page
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
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                    }
                }
                loaddata();
            }
        }
        public void loaddata()
        {
            int ID = Request.QueryString["id"].ToInt(0);
            if (ID > 0)
            {
                var t = RefundController.GetByID(ID);
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
                    var t = RefundController.GetByID(ID);
                    if (t != null)
                    {
                        if (t.Status == 1)
                        {
                            int UID = Convert.ToInt32(t.UID);
                            var u = AccountController.GetByID(Convert.ToInt32(UID));
                            if (u != null)
                            {
                                string Email = u.Email;

                                if (st == 2)
                                {
                                    double walletCYN = Convert.ToDouble(u.WalletCYN);
                                    walletCYN = walletCYN + money;

                                    AccountController.updateWalletCYN(u.ID, walletCYN);
                                    HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, walletCYN, 2, 2,
                                    u.Username + " đã được hoàn lại tiền mua hộ vào tài khoản.", currentDate, username_current);
                                }
                                RefundController.Update(t.ID, money, txtNote.Text, ddlStatus.SelectedValue.ToInt(), currentDate, username_current);
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }
                        }
                    }
                }
            }
        }
    }
}
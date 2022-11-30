using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;

namespace NHST.manager
{
    public partial class RequestShipDetail : System.Web.UI.Page
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
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var w = RequestShipController.GetByID(id);
                if (w != null)
                {
                    ViewState["ID"] = id;
                    int status = w.Status.ToString().ToInt(1);
                    lblUsername.Text = w.Username;
                    lblPhone.Text = w.Phone;
                    txtListOrderCode.Text = w.ListOrderCode;
                    txtNote.Text = w.Note;
                    ddlStatus.SelectedValue = status.ToString();
                    txtNote.Text = w.Note;
                    if (status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int id = ViewState["ID"].ToString().ToInt();
            var w = RequestShipController.GetByID(id);
            string BackLink = "/manager/Withdraw-List.aspx";
            if (w != null)
            {
                string kq = RequestShipController.Update(id, txtListOrderCode.Text, txtNote.Text, ddlStatus.SelectedValue.ToInt(1), currentDate, username);
                if (kq.ToInt(0) > 0)
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            }
        }
    }
}
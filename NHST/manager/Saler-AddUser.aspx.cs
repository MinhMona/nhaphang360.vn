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
    public partial class Saler_AddUser : System.Web.UI.Page
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
                        if (obj_user.RoleID != 6)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                loadPrefix();
                //LoadSaler();
            }
        }
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}
            //ddlPrefix.DataBind();
            //ddlPrefix.SelectedValue = "+84";

            var Levels = UserLevelController.GetAll("");
            if (Levels.Count > 0)
            {
                ddlLevelID.DataSource = Levels;
                ddlLevelID.DataBind();
            }
        }
        public void LoadSaler()
        {
            //var salers = AccountController.GetAllByRoleID(6);
            //ddlSale.Items.Clear();
            //ddlSale.Items.Insert(0, "Chọn nhân viên kinh doanh");
            //if (salers.Count > 0)
            //{
            //    ddlSale.DataSource = salers;
            //    ddlSale.DataBind();
            //}
            //var dathangs = AccountController.GetAllByRoleID(3);
            //ddlDathang.Items.Clear();
            //ddlDathang.Items.Insert(0, "Chọn nhân viên đặt hàng");
            //if (dathangs.Count > 0)
            //{
            //    ddlDathang.DataSource = dathangs;
            //    ddlDathang.DataBind();
            //}
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            string Email = txtEmail.Text.Trim();
            string nickname = txtUsername.Text.Trim();
            //string ddlprefix = ddlPrefix.SelectedValue;
            //int SaleID = ddlSale.SelectedValue.ToString().ToInt(0);
            int SaleID = 0;
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user != null)
            {
                SaleID = obj_user.ID;
            }
            //int DathangID = ddlDathang.SelectedValue.ToString().ToInt(0);
            int LevelID = ddlLevelID.SelectedValue.ToString().ToInt();
            //int VIPLevel = ddlVipLevel.SelectedValue.ToString().ToInt();
            int VIPLevel = 0;
            var checkuser = AccountController.GetByUsername(nickname);
            var checkemail = AccountController.GetByEmail(Email);
            //int RoleID = ddlRole.SelectedValue.ToString().ToInt();
            int RoleID = 1;
            var getaccountinfor = AccountInfoController.GetByPhone(txtPhone.Text.Trim());
            if (checkuser != null)
            {
                //lbl_check.Visible = true;
                //lbl_check.Text = "Tên đăng nhập / Nickname đã được sử dụng vui lòng chọn Tên đăng nhập / Nickname khác.";
                PJUtils.ShowMessageBoxSwAlert("Tên đăng nhập / Nickname đã được sử dụng vui lòng chọn Tên đăng nhập / Nickname khác.", "e", false, Page);
            }
            else if (checkemail != null)
            {
                //lbl_check.Visible = true;
                //lbl_check.Text = "Email đã được sử dụng vui lòng chọn Email khác.";
                PJUtils.ShowMessageBoxSwAlert("Email đã được sử dụng vui lòng chọn Email khác.", "e", false, Page);
            }
            else if (getaccountinfor != null)
            {
                //lbl_check.Visible = true;
                //lbl_check.Text = "Số điện thoại đã được sử dụng vui lòng chọn Số điện thoại khác.";
                PJUtils.ShowMessageBoxSwAlert("Số điện thoại đã được sử dụng vui lòng chọn Số điện thoại khác.", "e", false, Page);
            }
            else
            {
                string Token = PJUtils.RandomStringWithText(16);
                string id = AccountController.Insert(nickname, Email, txt_Password.Text.Trim(), RoleID, LevelID, VIPLevel, Convert.ToInt32(ddlStatus.SelectedValue),
                    SaleID, 0, DateTime.Now, Username, DateTime.Now, Username, Token);
                int UID = Convert.ToInt32(id);
                if (UID > 0)
                {
                    AccountController.UpdateScanWareHouse(UID, 0, 0);
                    string idai = AccountInfoController.Insert(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), "", txtPhone.Text.Trim(), Email, txtPhone.Text.Trim(), "", "", "",
                        DateTime.Now, ddlGender.SelectedValue.ToInt(1), DateTime.Now, "", DateTime.Now, "");
                    if (idai == "1")
                    {
                        PJUtils.ShowMsg("Tạo tài khoản thành công.", true, Page);
                    }
                }
            }
        }
    }
}
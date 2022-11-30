using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;

namespace NHST
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                loadPrefix();
                loaddata();
            }
        }
        public void loadPrefix()
        {
            var listpre = PJUtils.loadprefix();
            ddlPrefix.Items.Clear();
            foreach (var item in listpre)
            {
                ListItem listitem = new ListItem(item.dial_code, item.dial_code);
                ddlPrefix.Items.Add(listitem);
            }
            ddlPrefix.DataBind();
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string currentLogin = Session["userLoginSystem"].ToString();
                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    if (a.Username == currentLogin)
                    {
                        ViewState["UID"] = id;
                        lblUsername.Text = a.Username;
                        var ai = AccountInfoController.GetByUserID(a.ID);
                        if (ai != null)
                        {
                            txtFirstName.Text = ai.FirstName;
                            txtLastName.Text = ai.LastName;
                            ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                            txtPhone.Text = ai.MobilePhone;
                            txtEmail.Text = ai.Email;
                            if (ai.BirthDay != null)
                                rBirthday.SelectedDate = ai.BirthDay;
                            if (ai.Gender != null)
                                ddlGender.SelectedValue = ai.Gender.ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }

        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            //int UID = ViewState["UID"].ToString().ToInt(0);
            //string pass = txtpass.Text.Trim();
            //if (!string.IsNullOrEmpty(pass))
            //{
            //    string confirmpass = txtconfirmpass.Text;
            //    if (!string.IsNullOrEmpty(confirmpass))
            //    {
            //        if (confirmpass == pass)
            //        {
            //            string rp = AccountController.UpdatePassword(UID, pass);
            //            string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), ddlPrefix.SelectedValue, 
            //                txtPhone.Text, txtEmail.Text.Trim(), "", "", Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), 
            //                "", "", DateTime.Now, lblUsername.Text);
            //            if (r == "1" && rp == "1")
            //            {
            //                PJUtils.ShowMsg("Cập nhật thành công", true, Page);
            //            }
            //            else if (r == "1" && rp == "0")
            //            {
            //                lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
            //                lblConfirmpass.Visible = true;
            //            }
            //            else
            //            {
            //                PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
            //            }
            //        }
            //        else
            //        {
            //            lblConfirmpass.Text = "Xác nhận mật khẩu không trùng với mật khẩu.";
            //            lblConfirmpass.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        lblConfirmpass.Text = "Không để trống xác nhận mật khẩu";
            //        lblConfirmpass.Visible = true;
            //    }
            //}
            //else
            //{
            //    string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), ddlPrefix.SelectedValue, txtPhone.Text, 
            //        txtEmail.Text.Trim(), "", "", Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), 
            //        "", "", DateTime.Now, lblUsername.Text);
            //    if (r == "1")
            //    {
            //        PJUtils.ShowMsg("Cập nhật thành công", true, Page);
            //    }
            //    else
            //    {
            //        PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
            //    }
            //}
        }
    }
}
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST
{
    public partial class OTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] == null && Session["userNotActive"] == null)
            {
                Response.Redirect("/dang-nhap");
            }
            else
            {
                if (Session["userLoginSystem"] != null)
                {
                    pnOk.Visible = true;
                    pnActive.Visible = false;
                }
                else if (Session["userNotActive"] != null)
                {
                    pnOk.Visible = false;
                    pnActive.Visible = true;
                }
            }
        }
        protected void btnotp_Click(object sender, EventArgs e)
        {
            if (Session["userNotActive"] != null)
            {
                string username_current = Session["userNotActive"].ToString();
                var user = AccountController.GetByUsername(username_current);
                if (user != null)
                {
                    DateTime current = DateTime.Now;
                    var ai = AccountInfoController.GetByUserID(user.ID);
                    if (ai != null)
                    {
                        string prefix = ai.MobilePhonePrefix;
                        string phone = ai.MobilePhone;
                        string fullphone = prefix + phone;

                        var otp = OTPController.SelectByUserPhonePrefixTypeValue(user.ID,phone, prefix, txtotp.Text.Trim(), 1);
                        if (otp != null)
                        {
                            TimeSpan ts = current.Subtract(Convert.ToDateTime(otp.CreatedDate));
                            if (ts.Minutes > 1)
                            {
                                //Nếu quá hạn 5 phút thì hiện link gửi lại otp
                                string otpreturn = OTPUtils.ResetAndCreateOTP(user.ID, prefix, phone, 1);
                                if (otpreturn != null)
                                {
                                    string message = MessageController.GetByType(1).Message + " " + otpreturn;
                                    ESMS.Send(fullphone, message);
                                    lblError.Text = "Hệ thống đã gửi cho bạn mã OTP mới, vui lòng kiểm tra và điền mã OTP để kích hoạt.";
                                    lblError.Visible = true;
                                    lblError.ForeColor = System.Drawing.Color.Blue;
                                }
                            }
                            else
                            {
                                //Nếu chưa quá 5p thì cập nhật status otp và cập nhật status user
                                OTPController.UpdateStatusByID(otp.ID, false);
                                string kq = AccountController.updatestatus(user.ID, 2, DateTime.Now, username_current);
                                if (kq == "1")
                                {
                                    Session.Remove("userNotActive");
                                    Session["userLoginSystem"] = username_current;
                                    pnOk.Visible = true;
                                    pnActive.Visible = false;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "refresh", "window.setTimeout('window.location.assign(\"/trang-chu\")',3000);", true);
                                }
                            }
                        }
                        else
                        {
                            lblError.Text = "Sai mã OTP.";
                            lblError.Visible = true;
                        }
                    }
                }
            }
        }
    }
}
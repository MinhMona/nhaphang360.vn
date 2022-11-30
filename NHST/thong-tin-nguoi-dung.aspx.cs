using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST
{
    public partial class thong_tin_nguoi_dung : System.Web.UI.Page
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
            var khotq = WarehouseFromController.GetAllWithIsHidden(false);
            if (khotq.Count > 0)
            {
                foreach (var item in khotq)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWareHouseFrom.Items.Add(us);
                }
            }
            ListItem sleTT = new ListItem("Chọn kho TQ", "0");
            ddlWareHouseFrom.Items.Insert(0, sleTT);

            var khovn = WarehouseController.GetAllWithIsHidden(false);
            if (khovn.Count > 0)
            {
                foreach (var item in khovn)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWareHouse.Items.Add(us);
                }
            }
            ListItem sleTT1 = new ListItem("Chọn kho VN", "0");
            ddlWareHouse.Items.Insert(0, sleTT1);

            var shipping = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shipping.Count > 0)
            {
                foreach (var item in shipping)
                {
                    ListItem us = new ListItem(item.ShippingTypeName, item.ID.ToString());
                    ddlShipping.Items.Add(us);
                }
            }
            ListItem sleTT2 = new ListItem("Chọn phương thức VC", "0");
            ddlShipping.Items.Insert(0, sleTT2);
        }
        public void loaddata()
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int id = obj_user.ID;
                ViewState["UID"] = id;
                txtUsername.Text = username;
                txtEmail.Text = obj_user.Email;
                decimal levelID = Convert.ToDecimal(obj_user.LevelID);
                int levelID1 = Convert.ToInt32(obj_user.LevelID);
                string level = "Vip 0";
                double percent = 0;
                var userLevel = UserLevelController.GetByID(levelID1);
                if (userLevel != null)
                {
                    level = userLevel.LevelName;
                    percent = userLevel.LessDeposit.Value;
                }
                var ai = AccountInfoController.GetByUserID(id);
                if (ai != null)
                {
                    txtFirstName.Text = ai.FirstName;
                    txtLastName.Text = ai.LastName;
                    //ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                    txtPhone.Text = ai.Phone;
                    txtAddress2.Text = ai.Address;
                    //if (ai.BirthDay != null)
                    //    rBirthday.SelectedDate = ai.BirthDay;
                    if (ai.Gender != null)
                        ddlGender.SelectedValue = ai.Gender.ToString();
                    //txtEmail.Text = ai.Email;
                    txtBirthDay.Text = string.Format("{0:dd/MM/yyyy HH:mm}", ai.BirthDay);
                    if (!string.IsNullOrEmpty(ai.IMGUser))
                    {
                        UpIMGBefore.ImageUrl = ai.IMGUser;
                    }

                    ddlShipping.SelectedValue = obj_user.ShippingType.ToString();
                    ddlWareHouse.SelectedValue = obj_user.WarehouseTo.ToString();
                    ddlWareHouseFrom.SelectedValue = obj_user.WarehouseFrom.ToString();

                    ltrProfile.Text += "<div class=\"card-image waves-effect waves-block waves-light\">";
                    ltrProfile.Text += "<img class=\"activator\" src=\"/App_Themes/UserNew45/assets/images/gallery/19.png\" alt=\"user bg\">";
                    ltrProfile.Text += "</div>";
                    ltrProfile.Text += "<div class=\"card-content\">";
                    if (!string.IsNullOrEmpty(ai.IMGUser))
                    {
                        ltrProfile.Text += "<img src=\"" + ai.IMGUser + "\" alt=\"\" class=\"circle responsive-img activator card-profile-image green lighten-5 padding-2\">";
                    }
                    else
                    {
                        ltrProfile.Text += "<img src=\"NO-IMAGE.jpg\" alt=\"\" class=\"circle responsive-img activator card-profile-image green lighten-5 padding-2\">";
                    }
                    ltrProfile.Text += "<h5 class=\"card-title activator grey-text text-darken-4\">" + ai.FirstName + " " + ai.LastName + "</h5>";
                    ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">grade</i> " + level + " - Tỷ lệ cọc: " + percent + " %</p>";
                    if (!string.IsNullOrEmpty(Convert.ToDouble(obj_user.Currency).ToString()))
                    {
                        if (Convert.ToDouble(obj_user.Currency) > 0)
                        {
                            ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">attach_money</i>Tỉ giá riêng = " + obj_user.Currency + " VNĐ</p>";
                        }    
                    }
                    if (!string.IsNullOrEmpty(obj_user.FeeTQVNPerWeight))
                    {
                        if (Convert.ToDouble((obj_user.FeeTQVNPerWeight).ToString()) > 0)
                        {
                            ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">store</i>Cân nặng riêng = " + obj_user.FeeTQVNPerWeight + " VNĐ</p>";
                        }
                    }                   
                    if (ai.Gender == 1)
                        ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">perm_identity</i> Nam</p>";
                    else
                        ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">perm_identity</i> Nữ</p>";
                    ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">perm_phone_msg</i> " + ai.Phone + "</p>";
                    ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">email</i> " + obj_user.Email + "</p>";
                    ltrProfile.Text += "<p><i class=\"material-icons profile-card-i\">location_on</i> " + ai.Address + "</p>";
                    ltrProfile.Text += "</div>";
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            string pass = txtpass.Text.Trim();
            DateTime birthday = DateTime.Now;
            if (!string.IsNullOrEmpty(txtBirthDay.Text.ToString()))
            {
                birthday = DateTime.ParseExact(txtBirthDay.Text.ToString(), "dd/MM/yyyy HH:mm", null);
            }
            if (!string.IsNullOrEmpty(pass))
            {
                string confirmpass = txtconfirmpass.Text;
                if (!string.IsNullOrEmpty(confirmpass))
                {
                    if (confirmpass == pass)
                    {
                        string IMG = "";
                        string KhieuNaiIMG = "/Uploads/";
                        if (UpIMG.PostedFiles.Count > 0)
                        {
                            foreach (HttpPostedFile f in UpIMG.PostedFiles)
                            {
                                string fileContentType = f.ContentType; // getting ContentType

                                byte[] tempFileBytes = new byte[f.ContentLength];

                                var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                                string fileName = f.FileName; // getting File Name
                                string fileExtension = Path.GetExtension(fileName).ToLower();

                                var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                                if (result)
                                {
                                    if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                                    {
                                        if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                                        {
                                            //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                            try
                                            {
                                                //f.SaveAs(Server.MapPath(o));
                                                IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            IMG = UpIMGBefore.ImageUrl;

                        AccountController.updatewarehouseFromwarehouseTo(UID, Convert.ToInt32(ddlWareHouseFrom.SelectedValue), Convert.ToInt32(ddlWareHouse.SelectedValue));
                        AccountController.updateshipping(UID, Convert.ToInt32(ddlShipping.SelectedValue));
                        AccountInfoController.UpdateIMGUser(UID, IMG);
                        string rp = AccountController.UpdatePassword(UID, pass);
                        string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                         txtAddress2.Text.Trim(), birthday, ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now, txtUsername.Text);

                        if (r == "1" && rp == "1")
                        {
                            PJUtils.ShowMessageBoxSwAlert("Cập nhật thông tin thành công", "s", true, Page);                           
                        }
                        else if (r == "1" && rp == "0")
                        {
                            lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                            lblConfirmpass.Visible = true;
                        }
                        else
                        {
                            lblError.Text = "Có lỗi trong quá trình cập nhật.";
                            lblError.Visible = true;                           
                        }
                    }
                    else
                    {
                        lblConfirmpass.Text = "Xác nhận mật khẩu không trùng với mật khẩu.";
                        lblConfirmpass.Visible = true;
                    }
                }
                else
                {
                    lblConfirmpass.Text = "Không để trống xác nhận mật khẩu";
                    lblConfirmpass.Visible = true;
                }
            }
            else
            {
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/";
                if (UpIMG.PostedFiles.Count > 0)
                {
                    foreach (HttpPostedFile f in UpIMG.PostedFiles)
                    {
                        string fileContentType = f.ContentType; // getting ContentType

                        byte[] tempFileBytes = new byte[f.ContentLength];

                        var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                        string fileName = f.FileName; // getting File Name
                        string fileExtension = Path.GetExtension(fileName).ToLower();

                        var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                        if (result)
                        {
                            if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                            {
                                if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                                {
                                    //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                    try
                                    {
                                        //f.SaveAs(Server.MapPath(o));
                                        IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                else
                    IMG = UpIMGBefore.ImageUrl;
                AccountController.updatewarehouseFromwarehouseTo(UID, Convert.ToInt32(ddlWareHouseFrom.SelectedValue), Convert.ToInt32(ddlWareHouse.SelectedValue));
                AccountController.updateshipping(UID, Convert.ToInt32(ddlShipping.SelectedValue));
                AccountInfoController.UpdateIMGUser(UID, IMG);
                string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                txtAddress2.Text.Trim(), birthday, ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now, txtUsername.Text);
                if (r == "1")
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thông tin thành công", "s", true, Page);
                    //lblError.Text = "Cập nhật thành công.";
                    //lblError.ForeColor = System.Drawing.Color.Blue;
                    //lblError.Visible = true;
                    //PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                }
                else
                {
                    lblError.Text = "Có lỗi trong quá trình cập nhật.";
                    lblError.Visible = true;
                    //PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                }
            }
        }
    }
}
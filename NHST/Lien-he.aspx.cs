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
    public partial class Lien_he : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Điện thoại:</span><span class=\"txt\"><a href=\"tel:" + config.Hotline + "\">" + config.Hotline + "</a></span>";
                ltrContact.Text += "</li>";
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Email:</span><span class=\"txt\"><a href=\"mailto:" + config.EmailSupport + "\">" + config.EmailSupport + "</a></span>";
                ltrContact.Text += "</li>";
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Địa chỉ:</span><span class=\"txt\"><a href=\"tel:" + config.Address + "\">" + config.Address + "</a></span>";
                ltrContact.Text += "</li>";
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kq = ContactController.Insert(txtFullname.Text, txtEmail.Text, txtPhone.Text, txtContent.Text, false, DateTime.Now, txtFullname.Text);
            if (kq.ToInt(0) > 0)
                PJUtils.ShowMessageBoxSwAlert("Gửi liên hệ thành công", "s", true, Page);
        }
    }
}
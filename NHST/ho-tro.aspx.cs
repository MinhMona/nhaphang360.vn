using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using Telerik.Web.UI;
namespace NHST
{
    public partial class ho_tro1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
                if (Session["userLoginSystem"] != null)
                {
                    //LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/hotroIMG/";
                if (hinhDaiDien.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
                    {
                        var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                        try
                        {
                            f.SaveAs(Server.MapPath(o));
                            IMG = o;
                        }
                        catch { }
                    }
                }
                else
                    IMG = imgDaiDien.ImageUrl;
                string kq = SupportController.Insert(UID, username, txtFullname.Text, txtPhone.Text, txtEmail.Text, txtNote.Text, IMG, DateTime.Now, username);
                if (kq.ToInt(0) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlert("Gửi hỗ trợ thành công", "s", true, Page);
                }
            }
        }
    }
}
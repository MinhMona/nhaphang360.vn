using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;
using System.IO;

namespace NHST.manager
{
    public partial class AddPageType : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string PageTypeName = txtPageTypeName.Text;
            string PageTypeDescription = hdfDetail.Value.ToString();
            DateTime currentDate = DateTime.Now;
            string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(PageTypeName);
            var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
            if (checkNode.Count > 0)
            {
                int next = checkNode.Count + 1;
                NodeAliasPath += "-" + next;
            }
            string BackLink = Request.UrlReferrer.ToString();

            string IMG1 = "";
            string KhieuNaiIMG1 = "/Uploads/Images/";
            if (OGIMG.PostedFiles.Count > 0)
            {
                foreach (HttpPostedFile f in OGIMG.PostedFiles)
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
                                //var o = KhieuNaiIMG1 + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG1 = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            string IMGFacebook = "";
            string KhieuNaiIMGFacebook = "/Uploads/Images/";
            if (OGFacebookIMG.PostedFiles.Count > 0)
            {
                foreach (HttpPostedFile f in OGFacebookIMG.PostedFiles)
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
                                //var o = KhieuNaiIMGFacebook + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMGFacebook = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            string IMGTwitter = "";
            string KhieuNaiIMGTwitter = "/Uploads/Images/";
            if (OGTwitterIMG.PostedFiles.Count > 0)
            {
                foreach (HttpPostedFile f in OGTwitterIMG.PostedFiles)
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
                               // var o = KhieuNaiIMGTwitter + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMGTwitter = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            bool SideBar = Convert.ToBoolean(hdfSideBar.Value.ToInt(0));

            string nodeID = NodeController.Insert(PageTypeName, NodeAliasPath, 1, "tbl_PageType", currentDate, Email);

            string kq = PageTypeController.Insert(PageTypeName, PageTypeDescription, 1, nodeID.ToInt(0), NodeAliasPath,
                "", txtOGTitle.Text, txtOGDescription.Text, IMG1, txtMetaTitle.Text, txtMetaDescription.Text, txtMetaKeyWord.Text,
                currentDate, Email, OGFacebookTitle.Text, OGFacebookDescription.Text, IMGFacebook, OGTwitterTitle.Text, OGTwitterDescription.Text, IMGTwitter, SideBar);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo mới danh mục thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới danh mục. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}
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
    public partial class AddPage : System.Web.UI.Page
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
                LoadDDLPageType();
            }
        }
        public void LoadDDLPageType()
        {
            var pt = PageTypeController.GetAll();
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    for (int i = 0; i < pt.Count; i++)
                    {
                        ListItem a = new ListItem(pt[i].PageTypeName, pt[i].ID.ToString());
                        ddlPageType.Items.Add(a);
                    }
                    ddlPageType.SelectedIndex = 0;
                    ddlPageType.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string NewsTitle = txtTitle.Text;
            string NewsSummary = txtShortDescription.Text;
            string NewsDescription = hdfDetail.Value.ToString();
            bool IsHidden = Convert.ToBoolean(hdfStatus.Value.ToInt(0));
            bool SideBar = Convert.ToBoolean(hdfSideBar.Value.ToInt(0));
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/NewsIMG/";
            string categ = ddlPageType.SelectedItem.ToString();
            string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(categ) + "/" + LeoUtils.ConvertToUnSign(NewsTitle);
            DateTime currentDate = DateTime.Now;
            string BackLink = Request.UrlReferrer.ToString();
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
                            // string img =  FileUploadCheck.ConvertToBase64(tempFileBytes);
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
                                //var o = KhieuNaiIMGTwitter + Guid.NewGuid() + Path.GetExtension(f.FileName);
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

            var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
            if (checkNode.Count > 0)
            {
                int next = checkNode.Count + 1;
                NodeAliasPath += "-" + next;
            }
            string nodeID = NodeController.Insert(NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
            if (nodeID.ToInt(0) > 0)
            {
                string kq = PageController.Insert(NewsTitle, NewsSummary, IMG, NewsDescription, IsHidden, Convert.ToInt32(ddlPageType.SelectedValue),
                    nodeID.ToInt(), NodeAliasPath, "", txtOGTitle.Text, txtOGDescription.Text, IMG1, txtMetaTitle.Text, txtMetaDescription.Text,
                    txtMetaKeyWord.Text, currentDate, Email, OGFacebookTitle.Text, OGFacebookDescription.Text, IMGFacebook, OGTwitterTitle.Text, OGTwitterDescription.Text, IMGTwitter, SideBar);
                if (Convert.ToInt32(kq) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo mới trang thành công.", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới trang. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}
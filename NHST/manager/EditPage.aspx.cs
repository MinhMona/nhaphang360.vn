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
    public partial class EditPage : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadDDLPageType();
                LoadNews();

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
                        lbChuyenMuc.Items.Add(a);
                    }
                    lbChuyenMuc.DataBind();
                }
            }
        }
        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = PageController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtTitle.Text = news.Title;
                    txtShortDescription.Text = news.Summary;
                    hdfDetail.Value = news.PageContent;
                    lbChuyenMuc.SelectedValue = news.PageTypeID.ToString();
                    hdfStatus.Value = Convert.ToInt32(news.IsHidden.Value).ToString();
                    hdfSideBar.Value = Convert.ToInt32(news.SideBar.Value).ToString();
                    txtLink.Text = news.NodeAliasPath;
                    if (!string.IsNullOrEmpty(news.IMG))
                    {
                        UpIMGBefore.ImageUrl = news.IMG;
                    }
                    if (!string.IsNullOrEmpty(news.OGFacebookIMG))
                    {
                        OGFacebookIMGBefore.ImageUrl = news.OGFacebookIMG;
                    }
                    if (!string.IsNullOrEmpty(news.OGTwitterIMG))
                    {
                        OGTwitterIMGBefore.ImageUrl = news.OGTwitterIMG;
                    }
                    txtOGTitle.Text = news.ogtitle;
                    txtOGDescription.Text = news.ogdescription;
                    if (!string.IsNullOrEmpty(news.ogimage))
                    {
                        OGIMGBefore.ImageUrl = news.ogimage;
                    }
                    OGFacebookTitle.Text = news.OGFacebookTitle;
                    OGFacebookDescription.Text = news.OGFacebookDescription;
                    OGTwitterDescription.Text = news.OGTwitterDescription;
                    OGTwitterTitle.Text = news.OGTwitterTitle;

                    txtMetaTitle.Text = news.metatitle;
                    txtMetaDescription.Text = news.metadescription;
                    txtMetaKeyWord.Text = news.metakeyword;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int NewsID = ViewState["NID"].ToString().ToInt(0);
            string BackLink = "/manager/PageList.aspx";
            var news = PageController.GetByID(NewsID);
            if (news != null)
            {
                string NewsTitle = txtTitle.Text;
                string NewsSummary = txtShortDescription.Text;
                string NewsDescription = hdfDetail.Value;
                bool IsHidden = Convert.ToBoolean(hdfStatus.Value.ToInt(0));
                string IMG = "";
                string KhieuNaiIMG = "/Uploads/NewsIMG/";
                string categ = lbChuyenMuc.SelectedItem.ToString();
                string NodeAliasPath = "/chuyen-muc/" + LeoUtils.ConvertToUnSign(categ) + "/" + LeoUtils.ConvertToUnSign(NewsTitle);
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
                                        //    f.SaveAs(Server.MapPath(o));
                                        //    IMG = o;
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
                bool SideBar = Convert.ToBoolean(hdfSideBar.Value.ToInt(0));
                int NodeID = 0;
                if (news.NodeID != null)
                {
                    NodeID = Convert.ToInt32(news.NodeID);
                    var node = NodeController.GetByID(NodeID);
                    if (node != null)
                    {
                        var checkNode = NodeController.GetByNodeAliasPathAndNotContainsID(NodeAliasPath, NodeID);
                        if (checkNode.Count > 0)
                        {
                            int next = checkNode.Count + 1;
                            NodeAliasPath += "-" + next;
                        }
                        NodeController.Update(NodeID, NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
                    }
                }
                else
                {
                    var checkNode = NodeController.GetByNodeAliasPath(NodeAliasPath);
                    if (checkNode.Count > 0)
                    {
                        int next = checkNode.Count + 1;
                        NodeAliasPath += "-" + next;
                    }
                    string kq1 = NodeController.Insert(NewsTitle, NodeAliasPath, 2, "tbl_Page", currentDate, Email);
                    if (kq1.ToInt(0) > 0)
                    {
                        NodeID = kq1.ToInt(0);

                    }

                }
                string kq = PageController.Update(NewsID, NewsTitle, NewsSummary, IMG, NewsDescription, IsHidden, Convert.ToInt32(lbChuyenMuc.SelectedValue),
                    NodeID, NodeAliasPath, "", txtOGTitle.Text, txtOGDescription.Text, IMG1, txtMetaTitle.Text, txtMetaDescription.Text, txtMetaKeyWord.Text,
                    currentDate, Email, OGFacebookTitle.Text, OGFacebookDescription.Text, IMGFacebook, OGTwitterTitle.Text, OGTwitterDescription.Text, IMGTwitter, SideBar);

                if (kq == "ok")
                {
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
                }
            }

        }
    }
}
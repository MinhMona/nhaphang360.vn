using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class BankList : System.Web.UI.Page
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
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = BankController.GetAll();
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion

        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

            if (BankIMG.HasFiles)
            {
                foreach (HttpPostedFile f in BankIMG.PostedFiles)
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

            var kq = BankController.Insert(txtBankName.Text, txtAccountHolder.Text, txtBankNumber.Text, txtBranch.Text, IMG,
                 Email, currentDate);
            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlert("Thêm ngân hàng thành công.", "s", true, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình thêm mới ngân hàng. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            int BankID = hdfBankID.Value.ToInt(0);
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            bool IsHidden = Convert.ToBoolean(hdfEditBankStatus.Value.ToInt(0));
            if (EditBankIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditBankIMG.PostedFiles)
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
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }


            var kq = BankController.Update(BankID, txtEditBankName.Text, txtEditAccountHolder.Text, txtEditBankNumber.Text, txtEditBranch.Text, IMG, IsHidden, Username, DateTime.Now);
            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }

        [WebMethod]
        public static string loadinfoBank(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = BankController.GetByID(ID.ToInt(0));
            if (p != null)
            {
                tbl_Bank l = new tbl_Bank();
                l.ID = p.ID;
                l.BankName = p.BankName;
                l.BankNumber = p.BankNumber;
                l.AccountHolder = p.AccountHolder;
                l.IMG = p.IMG;
                l.Branch = p.Branch;
                l.CreatedDate = p.CreatedDate;
                l.IsHidden = p.IsHidden;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] != null)
            {
                int BankID = hdfBankID.Value.ToInt(0);
                var kq = BankController.Delete(BankID);
                if (kq != null)
                {
                    PJUtils.ShowMessageBoxSwAlert("Xóa thành công.", "s", true, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý. Vui lòng thử lại.", "e", true, Page);
                }
            }
        }
    }
}
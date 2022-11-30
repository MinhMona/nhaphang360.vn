using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using MB.Extensions;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace NHST.manager
{
    public partial class BigPackageDetail : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Request.QueryString["ID"].ToInt(0);
                if (ID > 0)
                {
                    var b = BigPackageController1.GetByID(ID);
                    if (b != null)
                    {
                        ViewState["ID"] = ID;
                        rSendDate.SelectedDate = b.SendDate;
                        rArrivedDate.SelectedDate = b.ArrivedDate;
                        txtPackageCode.Text = b.PackageCode;
                        pContent.Content = b.Description;
                        ddlPlace.SelectedValue = b.Place.ToString();
                        ddlStatus.SelectedValue = b.Status.ToString();
                        chskIsSlow.Checked = Convert.ToBoolean(b.IsSlow);
                        if (b.AdditionFee != null)
                            pAdditionFee.Value = b.AdditionFee;
                        else
                            pAdditionFee.Value = 0;
                        var sps = SmallPackageController1.GetAllByBigPackageID(ID);
                        if (sps.Count > 0)
                        {
                            StringBuilder html = new StringBuilder();
                            foreach (var item in sps)
                            {
                                html.Append("<div class=\"row smallpackage\" style=\"padding: 20px 0;\">");
                                html.Append("<div class=\"col-md-25\">Mã vận đơn:</div>");
                                html.Append("<div class=\"col-md-25\"><input disabled type=\"text\" class=\"packageCode form-control\" value=\"" + item.PackageCode + "\"/></div>");
                                html.Append("<div class=\"col-md-25\">User Phone:</div>");
                                html.Append("<div class=\"col-md-25\"><input type=\"text\" class=\"packageUserPhone form-control\" value=\"" + item.UserPhone + "\"/></div>");
                                html.Append("<div class=\"col-md-25\">Trọng lượng (kg):</div>");
                                html.Append("<div class=\"col-md-25\"><input type=\"text\" class=\"packageWeight form-control\" value=\"" + item.Weight.ToString().Replace(",", ".") + "\"/></div>");
                                //html.Append("<div class=\"col-md-25\">Trạng thái nhận hàng:</div>");
                                //html.Append("<div class=\"col-md-25\"><input type=\"hidden\" class=\"packagenhanhang \" value=\"" + item.StatusReceivePackage + "\" />" + PJUtils.ReturnStatusReceivePackage(Convert.ToInt32(item.StatusReceivePackage)) + "</div>");
                                //html.Append("<div class=\"col-md-25\">Trạng thái thanh toán:</div>");
                                //html.Append("<div class=\"col-md-25\"><input type=\"hidden\" class=\"packagethanhtoan \" value=\"" + item.StatusPayment + "\" />" + PJUtils.ReturnStatusPayment(Convert.ToInt32(item.StatusPayment)) + "</div>");
                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú:</div>");
                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNote form-control\" oninput=\"onchange($(this))\">" + item.Note + "</textarea></div>");
                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái nhận hàng:</div>");
                                if (item.StatusReceivePackage == 0)
                                    html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusReceived\" disabled><option value=\"0\">Chưa nhận hàng</option><option value=\"1\">Đã nhận hàng</option></select></div>");
                                else
                                    html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusReceived\" disabled><option value=\"0\">Chưa nhận hàng</option><option value=\"1\" selected>Đã nhận hàng</option></select></div>");

                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái thanh toán:</div>");

                                if (item.StatusPayment == 0)
                                    html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusPayment\" disabled><option value=\"0\">Chưa thanh toán</option><option value=\"1\">Đã thanh toán</option></select></div>");
                                else
                                    html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusPayment\" disabled><option value=\"0\">Chưa thanh toán</option><option value=\"1\" selected>Đã thanh toán</option></select></div>");

                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú khách hàng:</div>");
                                html.Append("<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNoteCus form-control\">" + item.NoteCustomer + "</textarea></div>");
                                html.Append("</div>");
                            }
                            ltrsm.Text = html.ToString();
                        }
                        var hist = HistoryServiceController.GetAllByPostIDAndType(ID, 1);
                        if (hist.Count > 0)
                        {
                            rptPayment.DataSource = hist;
                            rptPayment.DataBind();
                        }
                    }
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (Session["userLoginSystem"] != null)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                DateTime currentDate = DateTime.Now;
                if (ac != null)
                {
                    if (ac.RoleID != 1)
                    {
                        int ID = ViewState["ID"].ToString().ToInt(0);
                        var bp = BigPackageController1.GetByID(ID);
                        if (bp != null)
                        {
                            string list = hdflistpackage.Value;
                            DateTime SendDate = Convert.ToDateTime(rSendDate.SelectedDate);
                            DateTime ArrivedDate = Convert.ToDateTime(rArrivedDate.SelectedDate);
                            double AdditionFee = Convert.ToDouble(pAdditionFee.Value);
                            int Place = ddlPlace.SelectedValue.ToInt();
                            int Status = ddlStatus.SelectedValue.ToInt();
                            int Status_old = Convert.ToInt32(bp.Status);

                            string status_old_text = "";
                            if (Status_old == 1)
                                status_old_text = "Quảng Châu";
                            else if (Status_old == 2)
                                status_old_text = "Hà Nội";
                            else
                                status_old_text = "Việt Trì";

                            if (Status != Status_old)
                            {
                                HistoryServiceController.Insert(ID, ac.ID, ac.Username, Status_old, status_old_text, Status, ddlStatus.SelectedItem.ToString(),
                                    1, currentDate, ac.Username);
                            }

                            BigPackageController1.Update(ID, SendDate, ArrivedDate, txtPackageCode.Text, pContent.Content,
                                Place, Status, chskIsSlow.Checked, AdditionFee, currentDate, username_current);
                            SmallPackageController1.DeleteByBigPackageID(ID);
                            int bID = ID;
                            if (bID > 0)
                            {
                                if (!string.IsNullOrEmpty(list))
                                {
                                    string[] itemlist = list.Split('|');
                                    for (int i = 0; i < itemlist.Length - 1; i++)
                                    {
                                        string item = itemlist[i];
                                        string[] itemdetail = item.Split(',');
                                        string pCode = itemdetail[0].Trim();
                                        string barcodeIMG = "/Uploads/smallpackagebarcode/" + pCode + ".gif";
                                        Bitmap barCode = PJUtils.CreateBarcode1(pCode);
                                        barCode.Save(Server.MapPath("~" + barcodeIMG + ""), ImageFormat.Gif);
                                        string pUserPhone = itemdetail[1].Trim();
                                        string pWeight = itemdetail[2].Trim();
                                        string pNote = itemdetail[3].Trim();
                                        string pReceived = itemdetail[4].Trim();
                                        string pPayment = itemdetail[5].Trim();
                                        string pNoteCus = itemdetail[6].Trim();
                                        if (!string.IsNullOrEmpty(pCode) && !string.IsNullOrEmpty(pUserPhone) && !string.IsNullOrEmpty(pWeight))
                                        {
                                            var u = AccountController.GetByPhone(pUserPhone);
                                            if (u != null)
                                            {
                                                double weight = Convert.ToDouble(pWeight);
                                                SmallPackageController1.Insert(bID, SendDate, pCode, u.ID, pUserPhone, weight, Place, pReceived.ToInt(0),
                                                    pPayment.ToInt(0), pNote, pNoteCus, barcodeIMG, currentDate, username_current);
                                            }
                                            else
                                            {
                                                double weight = Convert.ToDouble(pWeight);
                                                SmallPackageController1.Insert(bID, SendDate, pCode, 0, pUserPhone, weight, Place, pReceived.ToInt(0),
                                                    pPayment.ToInt(0), pNote, pNoteCus, barcodeIMG,
                                                    currentDate, username_current);
                                            }
                                        }
                                    }
                                }
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }
                        }
                    }
                }
            }
            else Response.Redirect("/trang-chu");
        }

        protected void btnSendmail_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (Session["userLoginSystem"] != null)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                DateTime currentDate = DateTime.Now;
                DateTime ArrivedDate = Convert.ToDateTime(rArrivedDate.SelectedDate);
                if (ac != null)
                {
                    if (ac.RoleID != 1)
                    {
                        int ID = ViewState["ID"].ToString().ToInt(0);
                        var bp = BigPackageController1.GetByID(ID);
                        if (bp != null)
                        {
                            string list = hdflistpackage.Value;
                            int bID = ID;
                            if (bID > 0)
                            {
                                if (!string.IsNullOrEmpty(list))
                                {
                                    string[] itemlist = list.Split('|');
                                    for (int i = 0; i < itemlist.Length - 1; i++)
                                    {
                                        string item = itemlist[i];
                                        string[] itemdetail = item.Split(',');
                                        string pCode = itemdetail[0].Trim();
                                        string pUserPhone = itemdetail[1].Trim();
                                        string pWeight = itemdetail[2].Trim();
                                        if (!string.IsNullOrEmpty(pCode) && !string.IsNullOrEmpty(pUserPhone) && !string.IsNullOrEmpty(pWeight))
                                        {
                                            var u = AccountController.GetByPhone(pUserPhone);
                                            if (u != null)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new( u.Email,
                                                            "Thông báo nhận hàng tại NHẬP HÀNG 360.",
                                                            "Kiện hàng của bạn đã về đến kho NHẬP HÀNG 360 vào ngày: " + string.Format("{0:dd/MM/yyyy}", ArrivedDate), "");
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                                PJUtils.ShowMessageBoxSwAlert("Gửi mail thành công", "s", true, Page);
                            }
                        }
                    }
                }
            }
            else Response.Redirect("/trang-chu");
        }
    }
}
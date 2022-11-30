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
    public partial class chi_tiet_thanh_toan_ho1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            var id = RouteData.Values["id"].ToString().ToInt(0);
            if (id > 0)
            {
                string username = Session["userLoginSystem"].ToString();
                var u = AccountController.GetByUsername(username);
                if (u != null)
                {
                    int UID = u.ID;
                    var p = PayhelpController.GetByIDAndUID(id, UID);
                    if (p != null)
                    {
                        ViewState["ID"] = id;
                        int status = Convert.ToInt32(p.Status);
                        if (status == 0)
                        {
                            //btnSend.Visible = true;
                            //ltrPay.Text = "<a href=\"javascript:;\" onclick=\"paymoney();\" class=\"submit-btn border-ra-5px\">Thanh toán</a>";
                        }
                        //else btnSend.Visible = false;

                        ltrPayID.Text = " Chi tiết thanh toán hộ #" + p.ID + "";


                        ltrIfn.Text = username;
                        pAmount.Value = Convert.ToDouble(p.TotalPrice);
                        rVND.Value = Convert.ToDouble(p.TotalPriceVND);
                        rTigia.Value = Convert.ToDouble(p.Currency);
                        txtNote.Text = p.Note;
                        string trangthai = "";
                        if (status == 0)
                            trangthai = "Chưa thanh toán";
                        else if (status == 1)
                            trangthai = "Đã thanh toán";
                        else
                            trangthai = "Đã hủy";
                        lblStatus.Text = PJUtils.ReturnStatusPayHelpNew(status);
                        var pds = PayhelpDetailController.GetByPayhelpID(id);
                        if (pds.Count > 0)
                        {
                            foreach (var item in pds)
                            {
                                ltrList.Text += "<div class=\"input-field col s12 m6\">";
                                ltrList.Text += "<input type=\"text\" class=\"txtDesc2\" disabled value=\"" + item.Desc2 + "\">";
                                ltrList.Text += "<label class=\"active\">Giá tiền (tệ)</label>";
                                ltrList.Text += "</div>";
                                ltrList.Text += "<div class=\"input-field col s12 m6\">";
                                ltrList.Text += "<input type=\"text\" value=\"" + item.Desc1 + "\" disabled class=\"txtDesc1\">";
                                ltrList.Text += "<label class=\"active\">Nội dung</label>";
                                ltrList.Text += "</div>";
                            }
                        }

                    }
                    else Response.Redirect("/thanh-toan-ho");
                }
                else Response.Redirect("/thanh-toan-ho");
            }
            else
            {
                Response.Redirect("/thanh-toan-ho");
            }
        }
    }
}
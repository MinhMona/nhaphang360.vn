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
    public partial class tao_ky_gui_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
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
            string username = Session["userLoginSystem"].ToString();
            var user = AccountController.GetByUsername(username);
            if (user != null)
            {
                int uid = user.ID;
                var userinfo = AccountInfoController.GetByUserID(user.ID);
                var ws = RequestShipController.GetByUID(uid);
                if (ws.Count > 0)
                {
                    foreach (var w in ws)
                    {
                        int status = w.Status.ToString().ToInt();
                        ltr.Text += "<tr>";
                        ltr.Text += "   <td style=\"text-align:center;\">" + string.Format("{0:dd/MM/yyyy HH:mm}", w.CreatedDate) + "</td>";
                        ltr.Text += "   <td style=\"text-align:center;\"><p>" + w.ListOrderCode + "<p></td>";
                        ltr.Text += "   <td style=\"text-align:center;\">" + PJUtils.ReturnStatusWithdraw(status) + "</td>";
                        if (status == 1)
                            ltr.Text += "   <td style=\"text-align:center;\"><a id=\"w_id_" + w.ID + "\" href=\"javascript:;\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\" onclick=\"cancelwithdraw(" + w.ID + ")\">Hủy lệnh</a></td>";
                        else
                            ltr.Text += "   <td style=\"text-align:center;\"></td>";
                        ltr.Text += "</tr>";
                    }
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
                string phone = "";
                int UID = u.ID;
                var ai = AccountInfoController.GetByUserID(UID);
                if (ai != null)
                {
                    phone = ai.Phone;
                }
                string kq = RequestShipController.Insert(UID, username, phone, txtListOrderCode.Text, txtNote.Text, 1, DateTime.Now, username);
                if (kq.ToInt(0) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlert("Gửi yêu cầu thành công", "s", true, Page);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                int ID = hdfID.Value.ToInt(0);
                if (ID > 0)
                {
                    var r = RequestShipController.GetByUIDAndID(UID, ID);
                    if (r != null)
                    {
                        string kq = RequestShipController.UpdateStatus(ID, 3, DateTime.Now, username);
                        if (kq.ToInt(0) > 0)
                        {
                            PJUtils.ShowMessageBoxSwAlert("Hủy yêu cầu thành công", "s", true, Page);
                        }
                    }
                }
            }

        }
    }
}
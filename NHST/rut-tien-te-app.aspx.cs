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
    public partial class rut_tien_te_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }

        }
        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt(0);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var u = AccountController.GetByID(UID);
                    if (u != null)
                    {
                        ViewState["UID"] = UID;
                        pnMobile.Visible = true;
                        ltrusername.Text = u.Username;
                        ltrJoin.Text = "<span>" + string.Format("{0:HH:mm}", u.CreatedDate) + " ngày " + string.Format("{0:dd/MM/yyyy}", u.CreatedDate) + "</span>";
                        //ltrIfn.Text += "<p>" + u.Username + "</p><p>Tham gia từ " + string.Format("{0:HH:mm}", u.CreatedDate) + " ngày " + string.Format("{0:dd/MM/yyyy}", u.CreatedDate) + "</p>";
                    }
                }
                else
                {
                    pnShowNoti.Visible = true;
                }
            }
            else
            {
                pnShowNoti.Visible = true;
            }
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            if (UID > 0)
            {
                var acc = AccountController.GetByID(UID);
                if (acc != null)
                {
                    string kq = WithdrawController.InsertRechargeCYN(acc.ID, acc.Username, Convert.ToDouble(pAmount.Value),
                        txtNote.Text, 1, DateTime.Now, acc.Username);
                    if (kq.ToInt(0) > 0)
                        PJUtils.ShowMessageBoxSwAlert("Gửi thông tin thành công, vui lòng chờ admin kiểm duyệt", "s", true, Page);
                }
            }
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            int UID = ViewState["UID"].ToString().ToInt(0);
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                int ID = hdfTradeID.Value.ToInt(0);
                if (ID > 0)
                {
                    var t = WithdrawController.GetByUIDAndID(UID, ID);
                    if (t != null)
                    {
                        WithdrawController.Delete(ID);
                        PJUtils.ShowMessageBoxSwAlert("Hủy yêu cầu thành công", "s", true, Page);
                    }
                }
            }
        }
    }
}
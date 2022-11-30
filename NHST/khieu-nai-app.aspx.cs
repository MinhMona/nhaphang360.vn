using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class khieu_nai_app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        public void LoadData()
        {
            string Key = Request.QueryString["Key"];
            int UID = Request.QueryString["UID"].ToInt();
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var u = AccountController.GetByID(UID);
                    if (u != null)
                    {
                        var coms = ComplainController.GetByUID(u.ID);
                        if (coms.Count > 0)
                        {
                            StringBuilder html = new StringBuilder();
                            pnMobile.Visible = true;
                            foreach (var item in coms)
                            {

                                html.Append("  <div class=\"thanhtoanho-list\">");
                                html.Append("  <div class=\"all\">");
                                html.Append("  <div class=\"order-group offset15\">");
                                html.Append("   <div class=\"heading\">");
                                html.Append("   <p class=\"left-lb\">Ngày gửi: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</span></p>");
                                html.Append("  <p class=\"right-meta\">");
                                html.Append("  <span class=\"circle-icon\">");
                                html.Append("     <img src=\"images/icon-store.png\" style=\"height:12px\" alt=\"\"></span>");
                                html.Append(" ID: " + item.ID + "");
                                html.Append("   </p>");

                                html.Append(" </div>");
                                html.Append("  <div class=\"smr\">");
                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("  <p class=\"gray-txt\">Tiền bồi thường:</p>");
                                html.Append("    <p>" + string.Format("{0:N0}", Convert.ToDouble(item.Amount)).Replace(",", ".") + " VNĐ</p>");
                                html.Append(" </div>");
                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Nội dung:</p>");
                                html.Append("   <p>" + item.ComplainText + "</p>");
                                html.Append("</div>");

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("  <p class=\"gray-txt\">Trạng thái:</p>");
                                html.Append("   <p class=\"\">" + PJUtils.ReturnStatusComplainRequest(Convert.ToInt32(item.Status)) + "</p>");
                                html.Append(" </div>");

                                html.Append("  </div>");
                                html.Append(" </div>");
                                html.Append(" </div>");
                                html.Append(" </div>");

                            }
                            ltrComplain.Text = html.ToString();
                        }
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
    }
}
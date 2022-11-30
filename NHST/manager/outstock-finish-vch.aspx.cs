using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class outstock_finish_vch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 5)
                        Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            if (Request.QueryString["id"] != null)
            {
                int id = Request.QueryString["id"].ToInt(0);
                if (id > 0)
                {
                    ViewState["id"] = id;
                    StringBuilder html = new StringBuilder();
                    StringBuilder htmlPrint = new StringBuilder();
                    var lt = ExportRequestTurnController.GetByID(id);
                    if (lt != null)
                    {
                        var listtransportation = RequestOutStockController.GetByExportRequestTurnID(lt.ID);
                        if (listtransportation.Count > 0)
                        {
                            html.Append("<div class=\"responsive-tb package-item\">");

                            html.Append("<table class=\"table bordered \">");
                            html.Append("<thead>");
                            html.Append("<tr class=\"teal darken-4\">");
                            html.Append("<th>Mã kiện</th>");
                            html.Append("<th>Cân nặng (kg)</th>");
                            html.Append("<th>Cước vật tư</th>");
                            html.Append("<th>PP hàng đặc biệt</th>");
                            html.Append("</tr>");
                            html.Append("</thead>");
                            html.Append("<tbody>");

                            //Print
                            htmlPrint.Append("<article class=\"pane-primary\" style=\"color:#000\">");

                            htmlPrint.Append("   <article class=\"pane-primary\">");
                            htmlPrint.Append("       <table class=\"rgMasterTable normal-table full-width\" style=\"text-align:center\">");
                            htmlPrint.Append("           <tr>");
                            htmlPrint.Append("               <th style=\"color:#000\">Mã kiện</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Cân nặng (kg)</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Cước vật tư</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">PP hàng đặc biệt</th>");
                            htmlPrint.Append("           </tr>");
                            //End print


                            foreach (var t in listtransportation)
                            {
                                int tID = Convert.ToInt32(t.TransportationID);
                                var tran = TransportationOrderNewController.GetByID(tID);
                                if (tran != null)
                                {
                                    html.Append("<tr>");
                                    html.Append("<td><span>" + tran.BarCode + "</span></td>");
                                    html.Append("<td><span>" + tran.Weight + "</span></td>");
                                    html.Append("<td><span>" + string.Format("{0:N0}", Convert.ToDouble(tran.AdditionFeeVND)) + "</span></td>");
                                    html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(tran.SensorFeeeVND)) + "</td>");
                                    html.Append("</tr>");

                                    //print
                                    htmlPrint.Append("           <tr>");
                                    htmlPrint.Append("               <td>" + tran.BarCode + "</td>");
                                    htmlPrint.Append("               <td>" + tran.Weight + "</td>");
                                    htmlPrint.Append("               <td>" + string.Format("{0:N0}", Convert.ToDouble(tran.AdditionFeeVND)) + "</td>");
                                    htmlPrint.Append("               <td>" + string.Format("{0:N0}", Convert.ToDouble(tran.SensorFeeeVND)) + " vnđ</td>");
                                    htmlPrint.Append("           </tr>");
                                }
                            }
                            //print
                            htmlPrint.Append("           <tr style=\"font-size: 15px; text-transform: uppercase\">");
                            htmlPrint.Append("               <td colspan=\"3\" class=\"text-align-right\">Tổng tiền</td>");
                            htmlPrint.Append("               <td>" + string.Format("{0:N0}", lt.TotalPriceVND) + " vnđ</td>");
                            htmlPrint.Append("           </tr>");
                            htmlPrint.Append("       </table>");
                            htmlPrint.Append("   </article>");
                            htmlPrint.Append("</article>");
                            //end print

                            html.Append("</tbody>");
                            html.Append("<tbody>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Tổng số kiện</span></td>");
                            html.Append("<td><span class=\"black-text font-weight-600\">" + lt.TotalPackage + " kiện</span></td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Tổng cân nặng</span></td>");
                            html.Append("<td>" + lt.TotalWeight + " Kg</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Trạng thái</span></td>");
                            if (lt.Status == 1)
                                html.Append("<td>Chưa thanh toán</td>");
                            else
                                html.Append("<td>Đã thanh toán</td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Tổng tiền</span></td>");
                            html.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", lt.TotalPriceVND) + " VNĐ</span></td>");
                            html.Append("</tr>");
                            html.Append("</tbody>");
                            html.Append("</table>");
                            html.Append("</div>");
                        }
                    }

                    #region Render Data
                    var acc = AccountController.GetByID(lt.UID.Value);
                    if (acc != null)
                    {
                        txtFullname.Text = lt.Username;
                        txtPhone.Text = AccountInfoController.GetByUserID(acc.ID).Phone;
                    }

                    if (lt.Status == 2)
                    {
                        pnButton.Visible = true;
                    }
                    else
                    {
                        //pnButton.Visible = true;
                        pnrefresh.Visible = true;
                    }


                    #endregion
                    ltrList.Text = html.ToString();
                    ViewState["content"] = htmlPrint.ToString();
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                int UID = 0;
                var outs = ExportRequestTurnController.GetByID(id);
                if (outs != null)
                    UID = Convert.ToInt32(outs.UID);
               

                var listtransportation = RequestOutStockController.GetByExportRequestTurnID(id);
                if (listtransportation.Count > 0)
                {
                    foreach (var t in listtransportation)
                    {
                        int tID = Convert.ToInt32(t.TransportationID);
                        var tran = TransportationOrderNewController.GetByID(tID);
                        if (tran != null)
                        {
                            SmallPackageController.UpdateStatus(tran.SmallPackageID.Value, 4, currentDate, username_current);
                            SmallPackageController.UpdateDateOutWarehouse(tran.SmallPackageID.Value, username_current, currentDate);
                            TransportationOrderNewController.UpdateStatus(tran.ID, 6, currentDate, username_current);
                            TransportationOrderNewController.UpdateDateExport(tran.ID, currentDate, currentDate, username_current);

                            var check = RequestOutStockController.GetBySmallpackageID(tran.SmallPackageID.Value);
                            if (check != null)
                            {
                                RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                            }
                        }
                    }
                }
                ExportRequestTurnController.UpdateStatusExport(id, 2, currentDate);
                var c = ConfigurationController.GetByTop1();
                string content = ViewState["content"].ToString();
                var html = "";
                html += "<div class=\"print-bill\">";
                html += "   <div class=\"top\">";
                html += "       <div class=\"left\">";
                html += "           <span class=\"company-info\">NHẬP HÀNG 360</span>";
                html += "            <span class=\"company-info\">Địa chỉ: " + c.Address + "</span>";
                html += "       </div>";
                html += "       <div class=\"right\">";
                html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-title\">";
                html += "       <h1>PHIẾU XUẤT KHO</h1>";
                html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                html += "   </div>";
                html += "   <div class=\"bill-content\">";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Họ và tên người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Số ĐT người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += content;
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-footer\">";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người xuất hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người nhận hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                html += "       </div>";
                html += "   </div>";
                html += "</div>";

                StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");

                sb.Append(@"VoucherPrint('" + html + "')");
                sb.Append(@"</script>");

                ///hàm để đăng ký javascript và thực thi đoạn script trên
                if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                }
            }
        }

        public class OrderPackage
        {
            public int OrderID { get; set; }
            public int OrderType { get; set; }
            public List<SmallpackageGet> smallpackages { get; set; }
            public double totalPrice { get; set; }
            public bool isPay { get; set; }
            public double totalMustPay { get; set; }
        }
        public class SmallpackageGet
        {
            public int ID { get; set; }
            public string packagecode { get; set; }
            public double weight { get; set; }
            public double DateInWare { get; set; }
            public int Status { get; set; }
            public double payInWarehouse { get; set; }

        }
        public class Main
        {
            public int MainOrderID { get; set; }
        }

        public class Trans
        {
            public int TransportationOrderID { get; set; }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}
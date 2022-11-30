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
    public partial class chi_tiet_phien_xuat_kho_vch : System.Web.UI.Page
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

                    if (ac.RoleID != 0 && ac.RoleID != 7)
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
                    ltrIDS.Text = "#" + id;
                    var os = ExportRequestTurnController.GetByID(id);
                    if (os != null)
                    {
                        ViewState["id"] = id;

                        #region Đơn hàng VC hộ
                        StringBuilder html = new StringBuilder();
                        StringBuilder htmlPrint = new StringBuilder();
                        var listtransportation = RequestOutStockController.GetByExportRequestTurnID(id);
                        if (listtransportation.Count > 0)
                        {

                            html.Append("<article class=\"pane-primary\">");
                            html.Append("<div class=\"responsive-tb package-item\">");
                            html.Append("<table class=\"table bordered\">");
                            html.Append("<thead>");
                            html.Append("<tr class=\"teal darken-4\">");
                            html.Append("<th>Mã kiện</th>");
                            html.Append("<th>Cân nặng (kg)</th>");
                            html.Append("<th>Cước vật tư</th>");
                            html.Append("<th>PP hàng đặc biệt</th>");
                            html.Append("</tr>");
                            html.Append("</thead>");
                            html.Append("<tbody>");
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
                                }
                            }
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Tổng số kiện</span></td>");
                            html.Append("<td><span class=\"black-text font-weight-600\">" + os.TotalPackage + " kiện</span></td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Tổng cân nặng</span></td>");
                            html.Append("<td><span class=\"black-text font-weight-600\">" + os.TotalWeight + " Kg</span></td>");
                            html.Append("</tr>");
                            html.Append("<tr>");
                            html.Append("<td colspan=\"3\"><span class=\"black-text font-weight-500\">Trạng thái</span></td>");
                            if (os.Status == 1)
                                html.Append("<td>Chưa thanh toán</td>");
                            else
                                html.Append("<td>Đã thanh toán</td>");
                            html.Append("</tr>");

                            html.Append("</tbody>");
                            html.Append("</table>");
                            html.Append("</div>");
                            html.Append("</article>");


                        }
                        #endregion

                        #region Render Data
                        var acc = AccountController.GetByID(os.UID.Value);
                        if (acc != null)
                        {
                            txtFullname.Text = os.Username;
                            txtPhone.Text = AccountInfoController.GetByUserID(acc.ID).Phone;
                        }
                        #endregion
                        if (os.Status != 2)
                        {
                            txtTotalPrice1.Text = string.Format("{0:N0}", os.TotalPriceVND);
                            btncreateuser.Visible = true;
                            btnPayByWallet.Visible = true;
                        }
                        else
                        {
                            txtTotalPrice1.Text = string.Format("{0:N0}", 0);
                            btncreateuser.Visible = false;
                            btnPayByWallet.Visible = false;
                        }
                        lrtListPackage.Text = html.ToString();
                    }
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = "";
            string username_current = Session["userLoginSystem"].ToString();
            int UID_Admin = 0;
            var userAdmin = AccountController.GetByUsername(username_current);
            if (userAdmin != null)
            {
                UID_Admin = userAdmin.ID;
            }
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                var ots = ExportRequestTurnController.GetByID(id);
                if (ots != null)
                {
                    username = ots.Username;
                    int UID = Convert.ToInt32(ots.UID);

                    double totalPay = Convert.ToDouble(ots.TotalPriceVND);

                    if (totalPay > 0)
                    {
                        var user_wallet = AccountController.GetByID(UID);
                        if (user_wallet.Wallet < totalPay)
                        {
                            PJUtils.ShowMessageBoxSwAlert("Không đủ tiền trong tài khoản!, vui lòng nạp thêm tiền!", "e", true, Page);
                        }
                        else
                        {
                            #region Trừ tiền VC
                            var acc = AccountController.GetByID(UID);
                            if (acc != null)
                            {
                                double wallet = Convert.ToDouble(acc.Wallet);
                                double totalPriceVND = Convert.ToDouble(ots.TotalPriceVND);
                                double walletLeft = wallet - totalPriceVND;
                                AccountController.updateWallet(UID, walletLeft, currentDate, username);
                                HistoryPayWalletController.Insert(UID, username, 0, totalPriceVND,
                                    username + " đã thanh toán đơn hàng vận chuyển hộ.", walletLeft, 1, 8, currentDate, username);
                            }
                            #endregion

                            var c = ConfigurationController.GetByTop1();
                            ExportRequestTurnController.UpdateStatus(ots.ID, 2);
                            //OutStockSessionController.updateStatus(id, 2, currentDate, username_current);
                            string content = ViewState["content"].ToString();
                            var html = "";
                            html += "<div class=\"print-bill\">";
                            html += "   <div class=\"top\">";
                            html += "       <div class=\"left\">";
                            html += "           <span class=\"company-info\">NHẬP HÀNG 360</span>";
                            html += "          <span class=\"company-info\">Địa chỉ: " + c.Address + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"right\">";
                            html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                            html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-title\">";
                            html += "       <h1>BIÊN NHẬN</h1>";
                            html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                            html += "   </div>";
                            html += "   <div class=\"bill-content\">";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Phương thức thanh toán: </label>";
                            html += "           <label class=\"row-info\">Ví điện tử</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Họ và tên người đóng tiền: </label>";
                            html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Số ĐT người đóng tiền: </label>";
                            html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Tài khoản nhận tiền: </label>";
                            html += "           <label class=\"row-info\">" + username + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Số tiền: </label>";
                            html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalPay) + " VNĐ</label>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-footer\">";
                            html += "       <div class=\"bill-row-two\">";
                            html += "           <strong>Người thu tiền</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-two\">";
                            html += "           <strong>Người đóng tiền</strong>";
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
                            PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                        }
                    }
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected void btnPayByWallet_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = "";
            string username_current = Session["userLoginSystem"].ToString();
            int UID_Admin = 0;
            var userAdmin = AccountController.GetByUsername(username_current);
            if (userAdmin != null)
            {
                UID_Admin = userAdmin.ID;
            }
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                var ots = ExportRequestTurnController.GetByID(id);
                if (ots != null)
                {
                    username = ots.Username;
                    int UID = Convert.ToInt32(ots.UID);

                    double totalPay = Convert.ToDouble(ots.TotalPriceVND);

                    if (totalPay > 0)
                    {
                        var user_wallet = AccountController.GetByID(UID);
                        if (user_wallet != null)
                        {
                            double wallet = Convert.ToDouble(user_wallet.Wallet);
                            wallet = wallet + totalPay;
                            string contentin = user_wallet.Username + " đã được nạp tiền vào tài khoản.";
                            //AdminSendUserWalletController.UpdateStatus(id, 2, contentin, currentDate, username_current);
                            AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, totalPay, 2, 100, contentin, currentDate, username_current);
                            AccountController.updateWallet(user_wallet.ID, wallet, currentDate, username_current);
                            HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, totalPay, user_wallet.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentDate, username_current);
                        }
                        #region Trừ tiền VC
                        var acc = AccountController.GetByID(UID);
                        if (acc != null)
                        {
                            double wallet = Convert.ToDouble(acc.Wallet);
                            double totalPriceVND = Convert.ToDouble(ots.TotalPriceVND);
                            double walletLeft = wallet - totalPriceVND;
                            AccountController.updateWallet(UID, walletLeft, currentDate, username);
                            HistoryPayWalletController.Insert(UID, username, 0, totalPriceVND,
                                username + " đã thanh toán đơn hàng vận chuyển hộ.", walletLeft, 1, 8, currentDate, username);
                        }
                        #endregion

                        ExportRequestTurnController.UpdateStatus(ots.ID, 2);
                    }
                    var c = ConfigurationController.GetByTop1();
                    var html = "";
                    html += "<div class=\"print-bill\">";
                    html += "   <div class=\"top\">";
                    html += "       <div class=\"left\">";
                    html += "           <span class=\"company-info\">NHẬP HÀNG 360</span>";
                    html += "          <span class=\"company-info\">Địa chỉ: " + c.Address + "</span>";
                    html += "       </div>";
                    html += "       <div class=\"right\">";
                    html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                    html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                    html += "       </div>";
                    html += "   </div>";
                    html += "   <div class=\"bill-title\">";
                    html += "       <h1>BIÊN NHẬN</h1>";
                    html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                    html += "   </div>";
                    html += "   <div class=\"bill-content\">";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Phương thức thanh toán: </label>";
                    html += "           <label class=\"row-info\">Tiền mặt</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Họ và tên người đóng tiền: </label>";
                    html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Số ĐT người đóng tiền: </label>";
                    html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Tài khoản nhận tiền: </label>";
                    html += "           <label class=\"row-info\">" + username + "</label>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row\">";
                    html += "           <label class=\"row-name\">Số tiền: </label>";
                    html += "           <label class=\"row-info\">" + string.Format("{0:N0}", totalPay) + " VNĐ</label>";
                    html += "       </div>";
                    html += "   </div>";
                    html += "   <div class=\"bill-footer\">";
                    html += "       <div class=\"bill-row-two\">";
                    html += "           <strong>Người thu tiền</strong>";
                    html += "           <span class=\"note\">(Ký, họ tên)</span>";
                    html += "       </div>";
                    html += "       <div class=\"bill-row-two\">";
                    html += "           <strong>Người đóng tiền</strong>";
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
                    PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string prepage = Session["PrePage"].ToString();
            if (!string.IsNullOrEmpty(prepage))
            {
                Response.Redirect(prepage);
            }
            else
            {
                Response.Redirect(Request.Url.ToString());
            }
        }

    }
}
using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class chi_tiet_don_hang_khac_app : System.Web.UI.Page
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
            int UID = Convert.ToInt32(Request.QueryString["UID"]);
            int MainOrderID = Convert.ToInt32(Request.QueryString["OrderID"]);
            if (UID > 0 && MainOrderID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    ViewState["o"] = MainOrderID;
                    ViewState["Key"] = Key;
                    pnMobile.Visible = true;
                    var obj_user = AccountController.GetByID(UID);
                    if (obj_user != null)
                    {
                        StringBuilder html = new StringBuilder();
                        var o = MainOrderController.GetAllByUIDAndID(UID, MainOrderID);
                        if (o != null)
                        {
                            html.Append("  <div class=\"content_page\">");
                            html.Append("    <p class=\"title_onpage\"><span class=\"left_title\">ID:" + MainOrderID + "</span><span class=\"right_title\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)) + " VNĐ</span></p>");
                            //bool isReminderPrice = false;

                            //if (o.IsReminderPrice != null)
                            //    isReminderPrice = Convert.ToBoolean(o.IsReminderPrice);
                            var config = ConfigurationController.GetByTop1();
                            double currency = 0;
                            double currency1 = 0;
                            if (config != null)
                            {
                                double currencyconfig = 0;
                                if (!string.IsNullOrEmpty(config.Currency))
                                    currencyconfig = Convert.ToDouble(config.Currency);
                                currency = Math.Floor(currencyconfig);
                                currency1 = Math.Floor(currencyconfig);
                            }

                            double UL_CKFeeBuyPro = 0;
                            double UL_CKFeeWeight = 0;

                            UL_CKFeeBuyPro = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro.ToString().ToFloat(0);
                            UL_CKFeeWeight = UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight.ToString().ToFloat(0);
                            List<tbl_Order> lo = new List<tbl_Order>();

                            lo = OrderController.GetByMainOrderID(o.ID);
                            if (lo.Count > 0)
                            {
                                foreach (var item in lo)
                                {

                                    double currentcyt = item.CurrentCNYVN.ToFloat(0);
                                    double price = 0;
                                    double pricepromotion = item.price_promotion.ToFloat(0);
                                    double priceorigin = item.price_origin.ToFloat(0);
                                    if (pricepromotion > 0)
                                    {
                                        if (priceorigin > pricepromotion)
                                        {
                                            price = pricepromotion;
                                        }
                                        else
                                        {
                                            price = priceorigin;
                                        }
                                    }
                                    else
                                    {
                                        price = priceorigin;
                                    }
                                    double vndprice = price * currentcyt;



                                    html.Append("  <div class=\"content_create_order\">");
                                    html.Append("  <div class=\"bottom_order infor_order_group\">");
                                    html.Append("   <ul class=\"infor_order\">");
                                    html.Append(" <li class=\"infor_order_img\">");
                                    html.Append("  <img src =\"" + item.image_origin + "\"></li>");
                                    html.Append(" <li class=\"infor_order_content\">");
                                    html.Append(" <p class=\"title_product_detail\">" + item.title_origin + "</p>");
                                    html.Append("  <ul class=\"item_infor_order\">");
                                    html.Append(" <li>");
                                    html.Append("  <p class=\"title_order_column\">Thuộc tính:</p>");
                                    html.Append(" </li>");
                                    html.Append(" <li>");
                                    html.Append(" <p class=\"value_order_column title_web\">" + item.property + "</p>");
                                    html.Append(" </li>");
                                    html.Append(" </ul>");
                                    html.Append(" <ul class=\"item_infor_order\">");
                                    html.Append(" <li>");
                                    html.Append("  <p class=\"title_order_column\">Số lượng:</p>");
                                    html.Append(" </li>");
                                    html.Append("<li>");
                                    html.Append("   <p class=\"value_order_column title_coc\">" + item.quantity + "</p>");
                                    html.Append("</li>");
                                    html.Append(" </ul>");
                                    html.Append("  <ul class=\"item_infor_order\">");
                                    html.Append(" <li>");
                                    html.Append("  <p class=\"title_order_column\">Đơn giá:</p>");
                                    html.Append(" </li>");
                                    html.Append(" <li>");
                                    html.Append("  <p class=\"value_order_column title_money\">¥" + string.Format("{0:0.##}", price) + " ~ " + string.Format("{0:N0}", vndprice) + " VNĐ</p>");
                                    html.Append(" </li>");
                                    html.Append(" </ul>");
                                    html.Append(" <ul class=\"item_infor_order\">");
                                    html.Append("  <li>");
                                    html.Append(" <p class=\"title_order_column\">Trạng thái:</p>");
                                    html.Append("</li>");
                                    html.Append(" <li>");
                                    if (!string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                    {
                                        if (item.ProductStatus == 1)
                                            html.Append("<p class=\"value_order_column title_web\">Còn hàng</p>");
                                        else
                                            html.Append(" <p class=\"value_order_column title_web\">Hết hàng</p>");
                                    }
                                    else
                                    {
                                        html.Append(" <p class=\"value_order_column title_web\">Còn hàng</p>");
                                    }

                                    html.Append("  </li>");
                                    html.Append(" </ul>");
                                    html.Append(" <ul class=\"item_infor_order class_end_list\">");
                                    html.Append("  <li>");
                                    html.Append("  <p class=\"title_order_column\">Ghi chú:</p>");
                                    html.Append(" </li>");
                                    html.Append("  <li>");
                                    html.Append("  <p class=\"value_order_column\">" + item.brand + "</p>");
                                    html.Append("  </li>");
                                    html.Append("  </ul>");
                                    html.Append(" </li>");
                                    html.Append("  </ul>");
                                    html.Append(" </div>");



                                }
                            }

                            double pricepro = Convert.ToDouble(o.PriceVND);
                            double servicefee = 0;
                            var adminfeebuypro = FeeBuyProController.GetAll();
                            if (adminfeebuypro.Count > 0)
                            {
                                foreach (var item in adminfeebuypro)
                                {
                                    if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                    {
                                        servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                    }
                                }
                            }

                            double feebpnotdc = 0;
                            if (pricepro >= 1000000)
                            {
                                feebpnotdc = pricepro * servicefee;

                            }
                            else
                            {
                                feebpnotdc = 30000;
                            }
                            double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                            //if (!string.IsNullOrEmpty(o.FeeBuyPro))
                            //{
                            //    double bp = Convert.ToDouble(o.FeeBuyPro);
                            //    if (bp > 0)
                            //    {

                            //    }
                            //    else
                            //    {

                            //    }
                            //}
                            //else
                            //{

                            //}

                            //if (!string.IsNullOrEmpty(o.FeeShipCN))
                            //{
                            //    double fscn = Math.Floor(Convert.ToDouble(o.FeeShipCN));
                            //    double phhinoidiate = fscn / currency1;

                            //}
                            //else
                            //{

                            //}

                            double deposit = 0;
                            if (!string.IsNullOrEmpty(o.Deposit))
                            {
                                deposit = Convert.ToDouble(o.Deposit);
                            }

                            double payleft = Convert.ToDouble(o.TotalPriceVND) - deposit;

                            //ltrOrder.Text += "    <tr>";

                            html.Append(" <div class=\"content_create_order detail_order\">");
                            html.Append(" <div class=\"bottom_order list_pay\">");
                            html.Append(" <ul class=\"infor_order\">");
                            html.Append(" <li class=\"infor_order_content\">");
                            html.Append(" <ul class=\"item_infor_order\">");
                            html.Append(" <li>");
                            html.Append("   <p class=\"title_order_column\">Trạng thái đơn hàng:</p>");
                            html.Append("</li>");
                            html.Append("  <li>");
                            if (o.IsCheckNotiPrice == false)
                            {
                                html.Append(" <p class=\"value_order_column\"><span class=\"bg-yellow-gold\">Chờ báo giá</span></p>");
                            }
                            else
                            {
                                html.Append("    <p class=\"value_order_column\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</p>");
                            }
                            html.Append("   </li>");
                            html.Append(" </ul>");



                            html.Append("<ul class=\"item_infor_order\">");
                            html.Append("  <li>");
                            html.Append(" <p class=\"title_order_column\">Tiền hàng:</p>");
                            html.Append(" </li>");
                            html.Append("<li>");
                            html.Append("  <p class=\"value_order_column title_web\">¥ " + o.PriceCNY + " ~ " + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " VNĐ</p>");
                            html.Append(" </li>");
                            html.Append(" </ul>");


                            html.Append(" <ul class=\"item_infor_order\">");
                            html.Append("  <li>");
                            html.Append("  <p class=\"title_order_column\">Phí dịch vụ:</p>");
                            html.Append("  </li>");


                            if (!string.IsNullOrEmpty(o.FeeBuyPro))
                            {
                                double bp = Convert.ToDouble(o.FeeBuyPro);
                                if (bp > 0)
                                {
                                    html.Append("  <li>");
                                    html.Append("  <p class=\"value_order_column title_money\">" + string.Format("{0:N0}", bp) + " VNĐ</p>");
                                    html.Append("  </li>");
                                }
                                else
                                {

                                    html.Append("  <li>");
                                    html.Append(" <p class=\"value_order_column title_money\">Đang cập nhật</p>");
                                    html.Append(" </li>");
                                }
                            }
                            else
                            {

                                html.Append("<li>");
                                html.Append("  <p class=\"value_order_column title_money\">Đang cập nhật</p>");
                                html.Append(" </li>");
                            }
                            html.Append(" </ul>");

                            html.Append(" <ul class=\"item_infor_order\">");
                            html.Append("  <li>");
                            html.Append("    <p class=\"title_order_column\">Phí kiểm đếm:</p>");
                            html.Append(" </li>");
                            html.Append(" <li>");
                            html.Append("  <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", o.IsCheckProductPrice.ToFloat(0)) + " VNĐ</p>");
                            html.Append("  </li>");
                            html.Append("  </ul>");

                            html.Append("   <ul class=\"item_infor_order\">");
                            html.Append("   <li>");
                            html.Append("    <p class=\"title_order_column\">Phí đóng gỗ:</p>");
                            html.Append("   </li>");
                            html.Append("   <li>");
                            html.Append("   <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", o.IsPackedPrice.ToFloat(0)) + " VNĐ</p>");
                            html.Append("   </li>");
                            html.Append("   </ul>");


                            html.Append("  <ul class=\"item_infor_order\">");
                            html.Append("   <li>");
                            html.Append("<p class=\"title_order_column\">Phí ship nội địa TQ:</p>");
                            html.Append("   </li>");

                            if (!string.IsNullOrEmpty(o.FeeShipCN))
                            {
                                html.Append("   <li>");
                                html.Append("  <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN)) + " VNĐ</p>");
                                html.Append("  </li>");
                            }
                            else
                            {
                                html.Append("  <li>");
                                html.Append("     <p class=\"value_order_column title_web\">Đang cập nhật</p>");
                                html.Append("  </li>");
                            }

                            html.Append(" </ul>");

                            html.Append("  <ul class=\"item_infor_order\">");
                            html.Append(" <li>");
                            html.Append("   <p class=\"title_order_column\">Phí cân nặng:</p>");
                            html.Append(" </li>");
                            html.Append("  <li>");
                            html.Append("   <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</p>");
                            html.Append("  </li>");
                            html.Append(" </ul>");



                            html.Append("  <ul class=\"item_infor_order\">");
                            html.Append(" <li>");
                            html.Append("    <p class=\"title_order_column\">Số tiền phải đặt cọc:</p>");
                            html.Append("  </li>");


                            if (!string.IsNullOrEmpty(o.AmountDeposit))
                            {
                                html.Append("  <li>");
                                html.Append("   <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " VNĐ</p>");
                                html.Append("  </li>");
                            }
                            html.Append(" </ul>");


                            html.Append("<ul class=\"item_infor_order\">");
                            html.Append(" <li>");
                            html.Append("  <p class=\"title_order_column\">Tổng tiền:</p>");
                            html.Append(" </li>");
                            html.Append("  <li>");
                            html.Append("   <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)) + " VNĐ</p>");
                            html.Append(" </li>");
                            html.Append(" </ul>");
                            html.Append("  <ul class=\"item_infor_order\">");
                            html.Append(" <li>");
                            html.Append("    <p class=\"title_order_column\">Đã thanh toán:</p>");
                            html.Append("  </li>");
                            html.Append("  <li>");
                            html.Append(" <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", deposit) + " VNĐ</p>");
                            html.Append(" </li>");
                            html.Append("</ul>");
                            html.Append(" <ul class=\"item_infor_order class_end_list\">");
                            html.Append("<li>");
                            html.Append("  <p class=\"title_order_column\">Cần thanh toán:</p>");
                            html.Append("  </li>");
                            html.Append("  <li>");
                            html.Append("  <p class=\"value_order_column title_web\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND) - deposit) + " VNĐ</p>");
                            html.Append(" </li>");
                            html.Append(" </ul>");
                            html.Append(" </li>");
                            html.Append("  </ul>");



                            html.Append(" </div>");
                            html.Append("  <p class=\"btn_order_group list_pay_item\">");
                            if (o.Status == 0)
                            {
                                if (o.IsCheckNotiPrice != false)
                                {
                                    html.Append(" <a class=\"btn_ordersp btn_pay\" onclick=\"depositOrder()\">Đặt cọc</a>");
                                }
                                html.Append(" <a class=\"btn_ordersp btn_plain\" onclick=\"cancelOrder()\">Hủy đơn hàng</a>");
                            }

                            double userdadeposit = 0;
                            if (o.Deposit != null)
                                userdadeposit = Convert.ToDouble(o.Deposit);

                            double feewarehouse = 0;
                            if (o.FeeInWareHouse != null)
                                feewarehouse = Convert.ToDouble(o.FeeInWareHouse);
                            double totalPrice = Convert.ToDouble(o.TotalPriceVND);
                            double totalPay = totalPrice + feewarehouse;
                            double totalleft = totalPay - userdadeposit;

                            if (totalleft > 0)
                            {
                                if (obj_user.Wallet >= totalleft)
                                {
                                    if (o.Status > 6)
                                        html.Append(" <a class=\"btn_ordersp btn_pay\" onclick=\"payallorder()\">Thanh toán</a>");
                                }
                            }


                            html.Append(" <a href=\"/danh-sach-don-hang-khac-app.aspx?UID=" + UID + "&Key=" + Key + "\" class=\"btn_ordersp\">Trở về</a>");

                            html.Append("  </p>");
                            html.Append("  </div>");
                            html.Append(" </div>");
                            html.Append("  </div>");


                            #region Lấy bình luận
                            ltrComment.Text += "<div class=\"comment mar-bot2\">";
                            ltrComment.Text += "     <div class=\"comment_content\" seller=\"" + o.ShopID + "\" order=\"" + o.ID + "\" >";
                            var shopcomments = OrderCommentController.GetByOrderIDAndType(o.ID, 1);
                            if (shopcomments.Count > 0)
                            {
                                foreach (var item in shopcomments)
                                {
                                    string fullname = "";
                                    int role = 0;
                                    var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                                    if (user != null)
                                    {
                                        role = Convert.ToInt32(user.RoleID);
                                        var userinfo = AccountInfoController.GetByUserID(user.ID);
                                        if (userinfo != null)
                                        {
                                            fullname = userinfo.FirstName + " " + userinfo.LastName;
                                        }
                                    }
                                    if (role == 1)
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : " + item.Comment + "<br>";
                                    }
                                    else
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment green\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : <span class=\"green\">" + item.Comment + "</span><br>";

                                    }
                                }
                            }
                            else
                            {
                                ltrComment.Text += "         <span class=\"user-comment\">Chưa có ghi chú.</span>";
                            }
                            ltrComment.Text += "     </div>";
                            ltrComment.Text += "     <div class=\"comment_action\" style=\"padding-bottom: 4px; padding-top: 4px;\">";
                            ltrComment.Text += "         <input shop_code=\"" + o.ID + "\" type=\"text\" class=\"comment-text\" order=\"188083\" seller=\"" + o.ShopID + "\" placeholder=\"Nội dung\">";
                            //ltrComment.Text += "         <a id=\"sendnotecomment\" onclick=\"postcomment($(this))\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                            ltrComment.Text += "         <a id=\"sendnotecomment\" order=\"" + o.ID + "\" uid=\"" + UID + "\" class=\"btn_ordersp\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                            ltrComment.Text += "     </div>";
                            ltrComment.Text += "</div>";

                            #endregion
                        }
                        ltrProduct.Text = html.ToString();
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

        [WebMethod]
        public static string PostComment(string commentext, string shopid, string UID)
        {

            var obj_user = AccountController.GetByID(UID.ToInt(0));
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {

                int uid = obj_user.ID;
                //var id = RouteData.Values["id"].ToString().ToInt(0);
                int id = shopid.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        var setNoti = SendNotiEmailController.GetByID(12);

                        string message = "Bạn đã có một tin nhắn mới cho đơn hàng #" + id + ". CLick vào để xem";
                        int salerID = Convert.ToInt32(o.SalerID);
                        int dathangID = Convert.ToInt32(o.DathangID);
                        int khotqID = Convert.ToInt32(o.KhoTQID);
                        int khovnID = Convert.ToInt32(o.KhoVNID);

                        if (setNoti != null)
                        {
                            if (setNoti.IsSentNotiAdmin == true)
                            {
                                if (salerID > 0)
                                {
                                    var saler = AccountController.GetByID(salerID);
                                    if (saler != null)
                                    {
                                        NotificationsController.Inser(salerID,
                                            saler.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( saler.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                if (dathangID > 0)
                                {
                                    var dathang = AccountController.GetByID(dathangID);
                                    if (dathang != null)
                                    {
                                        NotificationsController.Inser(dathangID,
                                            dathang.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( dathang.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                if (khotqID > 0)
                                {
                                    var khotq = AccountController.GetByID(khotqID);
                                    if (khotq != null)
                                    {
                                        NotificationsController.Inser(khotqID,
                                            khotq.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( khotq.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                if (khovnID > 0)
                                {
                                    var khovn = AccountController.GetByID(khovnID);
                                    if (khovn != null)
                                    {
                                        NotificationsController.Inser(khovnID,
                                            khovn.Username, id,
                                            message, 1,
                                            currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( khovn.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }

                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        NotificationsController.Inser(admin.ID,
                                                                           admin.Username, id,
                                                                           message, 1,
                                                                           currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( admin.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }

                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        NotificationsController.Inser(manager.ID,
                                                                           manager.Username, id,
                                                                           message, 1,
                                                                           currentDate, obj_user.Username, false);
                                        if (setNoti.IsSentEmailAdmin == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( manager.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", message, "");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }

                            if (setNoti.IsSentEmailAdmin == true)
                            {
                                if (salerID > 0)
                                {
                                    var saler = AccountController.GetByID(salerID);
                                    if (saler != null)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( saler.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }
                                if (dathangID > 0)
                                {
                                    var dathang = AccountController.GetByID(dathangID);
                                    if (dathang != null)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( dathang.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }
                                if (khotqID > 0)
                                {
                                    var khotq = AccountController.GetByID(khotqID);
                                    if (khotq != null)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( khotq.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }
                                if (khovnID > 0)
                                {
                                    var khovn = AccountController.GetByID(khovnID);
                                    if (khovn != null)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( khovn.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }

                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( admin.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }

                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new( manager.Email,
                                                "Thông báo tại NHẬP HÀNG 360.", message, "");
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }


                        string kq = OrderCommentController.Insert(id, commentext, true, 1, currentDate, uid);
                        if (kq.ToInt(0) > 0)
                        {
                            return kq + "|" + message;
                        }
                        else
                            return "0";

                    }
                    else
                        return "0";
                }
                else
                    return "0";
            }
            else return "0";
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            var obj_user = AccountController.GetByID(UID);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = Convert.ToInt32(ViewState["o"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        if (o.Status == 0)
                        {
                            double wallet = obj_user.Wallet.ToString().ToFloat();
                            wallet = wallet + Convert.ToDouble(o.Deposit);
                            AccountController.updateWallet(obj_user.ID, wallet, DateTime.Now, obj_user.Username);
                            MainOrderController.UpdateDeposit(o.ID, obj_user.ID, "0");
                            //MainOrderController.UpdateIsReminderPrice(o.ID, true);
                            string kq = MainOrderController.UpdateStatus(id, uid, 1);
                            if (kq == "ok")
                                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        }
                    }
                }
            }
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            var obj_user = AccountController.GetByID(UID);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                if (obj_user.Wallet > 0)
                {
                    int OID = ViewState["o"].ToString().ToInt();
                    if (OID > 0)
                    {
                        var o = MainOrderController.GetAllByID(OID);
                        if (o != null)
                        {
                            double orderdeposited = 0;
                            double amountdeposit = 0;
                            double userwallet = 0;

                            if (o.Deposit.ToFloat(0) > 0)
                                orderdeposited = Math.Round(Convert.ToDouble(o.Deposit), 0);
                            if (o.AmountDeposit.ToFloat(0) > 0)
                                amountdeposit = Math.Round(Convert.ToDouble(o.AmountDeposit), 0);
                            if (obj_user.Wallet.ToString().ToFloat(0) > 0)
                                userwallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);
                            double custDeposit = amountdeposit - orderdeposited;
                            if (userwallet > 0)
                            {
                                if (userwallet >= custDeposit)
                                {
                                    var setNoti = SendNotiEmailController.GetByID(6);

                                    double wallet = userwallet - custDeposit;
                                    wallet = Math.Round(wallet, 0);

                                    #region cập nhật ví khách hàng và đơn hàng
                                    //AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                    ////Cập nhật lại MainOrder                                
                                    //MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    //int statusOOld = Convert.ToInt32(o.Status);
                                    //int statusONew = 2;
                                    ////if (statusONew != statusOOld)
                                    ////{
                                    ////    StatusChangeHistoryController.Insert(o.ID, statusOOld, statusONew, currentDate, obj_user.Username);
                                    ////}
                                    //MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    //MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                    //HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //   " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ đặt cọc, sang: Đã đặt cọc.", 1, currentDate);
                                    //HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, custDeposit,
                                    //    obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate,
                                    //    obj_user.Username);
                                    //PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, custDeposit, 2, currentDate, obj_user.Username);
                                    #endregion

                                    int st = TransactionController.DepositAll(obj_user.ID, wallet, currentDate, obj_user.Username, o.ID, 2, o.Status.Value, amountdeposit.ToString(), custDeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID, 1, 1, 2);
                                    if (st == 1)
                                    {
                                        if (setNoti != null)
                                        {
                                            if (setNoti.IsSentNotiAdmin == true)
                                            {

                                                var admins = AccountController.GetAllByRoleID(0);
                                                if (admins.Count > 0)
                                                {
                                                    foreach (var admin in admins)
                                                    {
                                                        NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.", 1, currentDate, obj_user.Username, false);
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {


                                                        NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được đặt cọc.",
                                                        1, currentDate, obj_user.Username, false);
                                                    }
                                                }
                                            }

                                            if (setNoti.IsSentEmailAdmin == true)
                                            {
                                                var admins = AccountController.GetAllByRoleID(0);
                                                if (admins.Count > 0)
                                                {
                                                    foreach (var admin in admins)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail_new( admin.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail_new( manager.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được đặt cọc.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                        }
                                        PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý.", "e", true, Page);
                                    }
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                                }
                            }
                        }
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Quý khách vui lòng nạp thêm tiền để tiến hành đặt cọc.", "e", true, Page);
                }
            }
        }

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            //string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByID(UID);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = ViewState["o"].ToString().ToInt(0);
                DateTime currentDate = DateTime.Now;
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double deposit = 0;
                        if (o.Deposit.ToFloat(0) > 0)
                            deposit = Math.Round(Convert.ToDouble(o.Deposit), 0);

                        double wallet = 0;
                        if (obj_user.Wallet.ToString().ToFloat(0) > 0)
                            wallet = Math.Round(Convert.ToDouble(obj_user.Wallet), 0);

                        double feewarehouse = 0;
                        if (o.FeeInWareHouse.ToString().ToFloat(0) > 0)
                            feewarehouse = Math.Round(Convert.ToDouble(o.FeeInWareHouse), 0);

                        double totalPriceVND = 0;
                        if (o.TotalPriceVND.ToFloat(0) > 0)
                            totalPriceVND = Math.Round(Convert.ToDouble(o.TotalPriceVND), 0);
                        double moneyleft = Math.Round((totalPriceVND + feewarehouse) - deposit, 0);

                        if (wallet >= moneyleft)
                        {
                            double walletLeft = Math.Round(wallet - moneyleft, 0);
                            double payalll = Math.Round(deposit + moneyleft, 0);
                            int st = TransactionController.PayAll(o.ID, wallet, o.Status.ToString().ToInt(0), uid, currentDate, obj_user.Username, deposit, 1, moneyleft, 1, 3, 2);
                            if (st == 1)
                            {
                                var setNoti = SendNotiEmailController.GetByID(11);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                NotificationsController.Inser(admin.ID, admin.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.",
1, currentDate, obj_user.Username, false);
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                NotificationsController.Inser(manager.ID, manager.Username, o.ID, "Đơn hàng " + o.ID + " đã được thanh toán.",
1, currentDate, obj_user.Username, false);
                                            }
                                        }
                                    }

                                    if (setNoti.IsSentEmailAdmin == true)
                                    {
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new( admin.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                }
                                                catch { }
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new( manager.Email,
                                                        "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng " + o.ID + " đã được thanh toán.", "");
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }

                                PJUtils.ShowMessageBoxSwAlert("Thanh toán thanh công.", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau.", "e", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số tiền trong tài khoản của bạn không đủ để thanh toán đơn hàng.", "e", true, Page);
                        }
                    }
                }
            }
        }
    }
}
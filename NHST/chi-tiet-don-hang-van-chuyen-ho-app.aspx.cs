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

namespace NHST
{
    public partial class chi_tiet_don_hang_van_chuyen_ho_app : System.Web.UI.Page
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
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            int UID = Convert.ToInt32(Request.QueryString["UID"]);
            if (UID > 0)
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    ViewState["UID"] = UID;
                    ViewState["ID"] = id;
                    ViewState["Key"] = Key;
                    pnMobile.Visible = true;
                    tbl_Account ac = AccountController.GetByID(UID);
                    if (ac != null)
                    {
                        if (id > 0)
                        {
                            var t = TransportationOrderController.GetByIDAndUID(id, UID);
                            if (t != null)
                            {
                                double totalPrice = Convert.ToDouble(t.TotalPrice);
                                double deposited = Convert.ToDouble(t.Deposited);
                                double warehouseFee = 0;
                                if (t.WarehouseFee != null)
                                {
                                    warehouseFee = Convert.ToDouble(t.WarehouseFee);
                                }
                                double totalmustpayleft = totalPrice + warehouseFee - deposited;
                                double totalPay = totalPrice + warehouseFee;
                                string createdDate = string.Format("{0:dd/MM/yyyy}", t.CreatedDate);
                                double totalWeight = 0;
                                double totalPackage = 0;
                                int stt = Convert.ToInt32(t.Status);
                                string status = PJUtils.generateTransportationStatus(stt);
                                string khoTQ = "";
                                string khoDich = "";
                                string shippingTypeName = "";

                                int tID = t.ID;
                                var wareTQ = WarehouseFromController.GetByID(Convert.ToInt32(t.WarehouseFromID));
                                if (wareTQ != null)
                                {
                                    khoTQ = wareTQ.WareHouseName;
                                }
                                var wareDich = WarehouseController.GetByID(Convert.ToInt32(t.WarehouseID));
                                if (wareDich != null)
                                {
                                    khoDich = wareDich.WareHouseName;
                                }
                                var shippingType = ShippingTypeToWareHouseController.GetByID(Convert.ToInt32(t.ShippingTypeID));
                                if (shippingType != null)
                                {
                                    shippingTypeName = shippingType.ShippingTypeName;
                                }
                                StringBuilder htmlPackages = new StringBuilder();
                                StringBuilder htmlbtn = new StringBuilder();
                                if (stt == 0)
                                {
                                    var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                                    if (tD.Count > 0)
                                    {
                                        totalPackage = tD.Count;

                                        htmlPackages.Append("  <div class=\"collapse-wrap\">");
                                        htmlPackages.Append("    <div class=\"flex-justify-space\">");
                                        htmlPackages.Append("  <p class=\"gray-txt\">Danh sách kiện:</p>");
                                        htmlPackages.Append(" <p class=\"xanhreu-txt\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-angle-down'></i>\" data-hide=\"Xem thêm <i class='fa fa-angle-up'></i>\" href=\"#chitiettb\">Xem thêm <i class='fa fa-angle-up'></i></a></p>");
                                        htmlPackages.Append("  </div>");

                                        htmlPackages.Append("   <div style =\"display:none;\" class=\"collapse-content\">");


                                        foreach (var temp in tD)
                                        {
                                            double weight = Convert.ToDouble(temp.Weight);
                                            htmlPackages.Append("    <table class=\"tb-wlb\">");
                                            htmlPackages.Append(" <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Kiện hàng:</td>");
                                            htmlPackages.Append("   <td>" + temp.TransportationOrderCode + "</td>");
                                            htmlPackages.Append("  </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Cân nặng:</td>");
                                            htmlPackages.Append("    <td>" + weight + "</td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Trạng thái:</td>");
                                            htmlPackages.Append("    <td><span class=\"bg-black\">Đã hủy</span></p></td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  </table>");
                                        }
                                        htmlPackages.Append(" </div>");
                                        htmlPackages.Append("  </div>");

                                        htmlbtn.Append(" <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "\" class=\"btn cam-btn fw-btn\">Trở về</a>");
                                    }
                                }
                                else if (stt == 1)
                                {
                                    var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                                    if (tD.Count > 0)
                                    {
                                        totalPackage = tD.Count;

                                        htmlPackages.Append("  <div class=\"collapse-wrap\">");
                                        htmlPackages.Append("    <div class=\"flex-justify-space\">");
                                        htmlPackages.Append("  <p class=\"gray-txt\">Danh sách kiện:</p>");
                                        htmlPackages.Append(" <p class=\"xanhreu-txt\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-angle-down'></i>\" data-hide=\"Xem thêm <i class='fa fa-angle-up'></i>\" href=\"#chitiettb\">Xem thêm <i class='fa fa-angle-up'></i></a></p>");
                                        htmlPackages.Append("  </div>");

                                        htmlPackages.Append("   <div style =\"display:none;\" class=\"collapse-content\">");


                                        foreach (var temp in tD)
                                        {
                                            double weight = Convert.ToDouble(temp.Weight);
                                            totalWeight += Convert.ToDouble(weight);
                                            htmlPackages.Append("    <table class=\"tb-wlb\">");
                                            htmlPackages.Append(" <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Kiện hàng:</td>");
                                            htmlPackages.Append("   <td>" + temp.TransportationOrderCode + "</td>");
                                            htmlPackages.Append("  </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Cân nặng:</td>");
                                            htmlPackages.Append("    <td>" + weight + "</td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Trạng thái:</td>");
                                            htmlPackages.Append("    <td><span class=\"bg-red\">Chờ duyệt</span></td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  </table>");
                                        }
                                        htmlPackages.Append(" </div>");
                                        htmlPackages.Append("  </div>");

                                        htmlbtn.Append(" <div class=\"couple-btn\">");
                                        htmlbtn.Append(" <a class=\"btn\" style=\"cursor:pointer;\" onclick=\"cancelOrder()\">Hủy đơn hàng</a>");
                                        htmlbtn.Append(" <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "\" class=\"btn\">Trở về</a>");
                                        htmlbtn.Append(" </div>");
                                    }
                                }
                                else
                                {
                                    var packages = SmallPackageController.GetByTransportationOrderID(tID);
                                    if (packages.Count > 0)
                                    {
                                        totalPackage = packages.Count;

                                        htmlPackages.Append("  <div class=\"collapse-wrap\">");
                                        htmlPackages.Append("    <div class=\"flex-justify-space\">");
                                        htmlPackages.Append("  <p class=\"gray-txt\">Danh sách kiện:</p>");
                                        htmlPackages.Append(" <p class=\"xanhreu-txt\"><a class=\"collapse-toggle\" data-show=\"Thu gọn <i class='fa fa-angle-down'></i>\" data-hide=\"Xem thêm <i class='fa fa-angle-up'></i>\" href=\"#chitiettb\">Xem thêm <i class='fa fa-angle-up'></i></a></p>");
                                        htmlPackages.Append("  </div>");

                                        htmlPackages.Append("   <div style =\"display:none;\" class=\"collapse-content\">");

                                        foreach (var s in packages)
                                        {
                                            double weight = 0;
                                            double weightCN = Convert.ToDouble(s.Weight);
                                            double weightKT = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            if (s.Length != null)
                                                dai = Convert.ToDouble(s.Length);
                                            if (s.Width != null)
                                                rong = Convert.ToDouble(s.Width);
                                            if (s.Height != null)
                                                cao = Convert.ToDouble(s.Height);

                                            if (dai > 0 && rong > 0 && cao > 0)
                                                weightKT = dai * rong * cao / 7000;
                                            if (weightKT > 0)
                                            {
                                                if (weightKT > weightCN)
                                                {
                                                    weight = weightKT;
                                                }
                                                else
                                                {
                                                    weight = weightCN;
                                                }
                                            }
                                            else
                                            {
                                                weight = weightCN;
                                            }
                                            weight = Math.Round(weight, 1);

                                            htmlPackages.Append("    <table class=\"tb-wlb\">");
                                            htmlPackages.Append(" <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Kiện hàng:</td>");
                                            htmlPackages.Append("   <td>" + s.OrderTransactionCode + "</td>");
                                            htmlPackages.Append("  </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Cân nặng:</td>");
                                            htmlPackages.Append("    <td>" + weight + "</td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  <tr>");
                                            htmlPackages.Append("   <td class=\"lb\">Trạng thái:</td>");
                                            htmlPackages.Append("    <td>" + PJUtils.IntToStringStatusSmallPackageWithBG(Convert.ToInt32(s.Status)) + "</td>");
                                            htmlPackages.Append("   </tr>");
                                            htmlPackages.Append("  </table>");

                                            if (s.Status > 2)
                                                totalWeight += weight;
                                        }

                                        htmlPackages.Append(" </div>");
                                        htmlPackages.Append("  </div>");
                                        //ltrListPackage.Text = htmlPackages.ToString();
                                        if (totalPrice > 0)
                                        {
                                            double feeinwarehouse = 0;
                                            if (t.WarehouseFee != null)
                                                feeinwarehouse = Convert.ToDouble(t.WarehouseFee);

                                            double leftPrice = (totalPrice + feeinwarehouse) - deposited;
                                            if (leftPrice > 0)
                                            {
                                                htmlbtn.Append(" <div class=\"couple-btn\">");
                                                htmlbtn.Append("  <a class=\"btn\" style=\"cursor:pointer;\" onclick=\"payOrder()\">Thanh toán</a>");
                                                htmlbtn.Append(" <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "\" class=\"btn\">Trở về</a>");
                                                htmlbtn.Append(" </div>");

                                                //ltrBtn.Text += "<a href=\"javascript:;\" onclick=\"payOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Thanh toán</a>";
                                                //btnPay.Visible = true;
                                                //btnPay.Attributes.Add("style", "display:none");
                                            }
                                            else
                                            {
                                                htmlbtn.Append(" <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "\" class=\"btn cam-btn fw-btn\">Trở về</a>");
                                            }
                                        }
                                        else
                                        {
                                            htmlbtn.Append(" <a href=\"/van-chuyen-ho-app.aspx?UID=" + UID + "\" class=\"btn cam-btn fw-btn\">Trở về</a>");
                                        }
                                    }
                                }
                                #region Lấy thông tin
                                StringBuilder html = new StringBuilder();

                                html.Append(" <div class=\"thanhtoanho-list\">");
                                html.Append(" <div class=\"all\">");
                                html.Append(" <div class=\"order-group offset15\">");
                                html.Append("  <div class=\"heading\">");
                                html.Append(" <p class=\"left-lb\">");
                                html.Append("     ID: " + id + "");
                                html.Append("  </p>");
                                html.Append("  <p class=\"right-meta\">Ngày tạo: <span class=\"hl-txt\">" + string.Format("{0:dd/MM/yyyy}", createdDate) + "</span></p>");
                                html.Append(" </div>");
                                html.Append("  <div class=\"smr\">");

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Tên đăng nhập:</p>");
                                html.Append("   <p>" + ac.Username + "</p>");
                                html.Append("  </div>");

                                html.Append("  <div class=\"flex-justify-space\">");
                                html.Append(" <p class=\"gray-txt\">Trạng thái:</p>");
                                html.Append("  <p>" + status + "</p>");
                                html.Append("  </div>");

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append(" <p class=\"gray-txt\">Kho TQ:</p>");
                                html.Append("  <p>" + khoTQ + "</p>");
                                html.Append(" </div>");

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append(" <p class=\"gray-txt\">Kho Đích:</p>");
                                html.Append("  <p>" + khoDich + "</p>");
                                html.Append(" </div>");

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Tổng số kiện:</p>");
                                html.Append("    <p class=\"\">" + totalPackage + "</p>");
                                html.Append("  </div>");


                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Tổng cân nặng:</p>");
                                html.Append("    <p class=\"\">" + totalWeight + "</p>");
                                html.Append("  </div>");


                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Tiền lưu kho:</p>");
                                html.Append("    <p class=\"\">" + string.Format("{0:N0}", warehouseFee) + " vnđ</p>");
                                html.Append("  </div>");

                                if (totalPrice > 0)
                                {
                                    html.Append(" <div class=\"flex-justify-space\">");
                                    html.Append("   <p class=\"gray-txt\">Tổng tiền vận chuyển:</p>");
                                    html.Append("    <p class=\"\">" + string.Format("{0:N0}", totalPrice) + " vnđ</p>");
                                    html.Append("  </div>");

                                    html.Append(" <div class=\"flex-justify-space\">");
                                    html.Append("   <p class=\"gray-txt\">Tổng tiền:</p>");
                                    html.Append("    <p class=\"\">" + string.Format("{0:N0}", totalPay) + " vnđ</p>");
                                    html.Append("  </div>");

                                    if (totalmustpayleft >= deposited)
                                    {
                                        html.Append(" <div class=\"flex-justify-space\">");
                                        html.Append("   <p class=\"gray-txt\">Đã thanh toán:</p>");
                                        html.Append("    <p class=\"\">" + string.Format("{0:N0}", deposited) + " vnđ</p>");
                                        html.Append("  </div>");

                                        double leftMoney = totalPay - deposited;

                                        html.Append(" <div class=\"flex-justify-space\">");
                                        html.Append("   <p class=\"gray-txt\">Còn lại:</p>");
                                        html.Append("    <p class=\"\">" + string.Format("{0:N0}", leftMoney) + " vnđ</p>");
                                        html.Append("  </div>");
                                    }
                                }

                                html.Append(" <div class=\"flex-justify-space\">");
                                html.Append("   <p class=\"gray-txt\">Ghi chú:</p>");
                                html.Append("    <p class=\"\">" + t.Description + "</p>");
                                html.Append("  </div>");

                                html.Append(htmlPackages.ToString());

                                html.Append("  </div>");
                                html.Append(htmlbtn.ToString());



                                html.Append(" </div>");

                                html.Append(" </div>");
                                html.Append("</div>");

                                ltrVCH.Text = html.ToString();
                                #endregion
                            }
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

        protected void btnPay_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            int UID = Convert.ToInt32(ViewState["UID"]);
            var obj_user = AccountController.GetByID(UID);
            if (obj_user != null)
            {
                double wallet = Convert.ToDouble(obj_user.Wallet);
                var id = ViewState["ID"].ToString().ToInt(0);
                if (id > 0)
                {
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        double currency = Convert.ToDouble(t.Currency);
                        double totalWeight = 0;

                        int wareFrom = Convert.ToInt32(t.WarehouseFromID);
                        int wareTo = Convert.ToInt32(t.WarehouseID);
                        int shippingType = Convert.ToInt32(t.ShippingTypeID);
                        double price = 0;
                        //var packages = SmallPackageController.GetByTransportationOrderID(t.ID);
                        var packages = SmallPackageController.GetByTransportationOrderIDAndFromStatus(t.ID, 2);
                        if (packages.Count > 0)
                        {
                            foreach (var p in packages)
                            {
                                double weight = 0;
                                double weightCN = Convert.ToDouble(p.Weight);
                                double weightKT = 0;
                                double dai = 0;
                                double rong = 0;
                                double cao = 0;
                                if (p.Length != null)
                                    dai = Convert.ToDouble(p.Length);
                                if (p.Width != null)
                                    rong = Convert.ToDouble(p.Width);
                                if (p.Height != null)
                                    cao = Convert.ToDouble(p.Height);

                                if (dai > 0 && rong > 0 && cao > 0)
                                    weightKT = dai * rong * cao / 7000;
                                if (weightKT > 0)
                                {
                                    if (weightKT > weightCN)
                                    {
                                        weight = weightKT;
                                    }
                                    else
                                    {
                                        weight = weightCN;
                                    }
                                }
                                else
                                {
                                    weight = weightCN;
                                }
                                weight = Math.Round(weight, 1);
                                //totalWeight += Convert.ToDouble(p.Weight);
                                totalWeight += weight;
                            }
                        }
                        var WarehouseFee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(wareFrom, wareTo, shippingType, true);
                        if (WarehouseFee.Count > 0)
                        {
                            foreach (var item in WarehouseFee)
                            {
                                if (item.WeightFrom < totalWeight && totalWeight <= item.WeightTo)
                                {
                                    price = Convert.ToDouble(item.Price);
                                }
                            }
                        }
                        double warehouseFee = 0;
                        if (t.WarehouseFee != null)
                        {
                            warehouseFee = Convert.ToDouble(t.WarehouseFee);
                        }
                        double deposited = Convert.ToDouble(t.Deposited);
                        //double totalPrice = price * totalWeight * currency + warehouseFee;
                        double totalPrice = (price * totalWeight) + warehouseFee;
                        double leftMoney = totalPrice - deposited;
                        if (leftMoney > 0)
                        {
                            if (leftMoney <= wallet)
                            {
                                double walletLeft = wallet - leftMoney;
                                using (NHSTEntities productDbContext = new NHSTEntities())
                                {
                                    using (var transaction = productDbContext.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            TransportationOrderController.UpdateStatusAndDeposited(t.ID, totalPrice, 6, currentDate, obj_user.Username);
                                            AccountController.updateWallet(UID, walletLeft, currentDate, obj_user.Username);
                                            HistoryPayWalletController.InsertTransportation(UID, obj_user.Username, 0, leftMoney, obj_user.Username + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".", walletLeft, 1, 8, currentDate, obj_user.Username, t.ID);

                                            var setNoti = SendNotiEmailController.GetByID(14);
                                            if (setNoti != null)
                                            {
                                                if (setNoti.IsSentNotiAdmin == true)
                                                {
                                                    var admins = AccountController.GetAllByRoleID(0);
                                                    if (admins.Count > 0)
                                                    {
                                                        foreach (var admin in admins)
                                                        {
                                                            NotificationsController.Inser(admin.ID,
                                                                                               admin.Username, t.ID,
                                                                                                "Đơn hàng vận chuyển hộ " + t.ID + " đã được thanh toán.",
                                                                                               10, currentDate, obj_user.Username, false);
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
                                                                    "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng vận chuyển hộ " + t.ID + " đã được thanh toán.", "");
                                                            }
                                                            catch { }
                                                        }
                                                    }
                                                }
                                            }

                                            PJUtils.ShowMessageBoxSwAlert("Thanh toán đơn thành công", "s", true, Page);
                                            transaction.Commit();
                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số tiền trong tài khoản của bạn không đủ để thanh toán đơn hàng này.", "e", true, Page);
                            }
                        }
                    }
                }
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(ViewState["UID"]);
            var obj_user = AccountController.GetByID(UID);
            if (obj_user != null)
            {
                var id = ViewState["ID"].ToString().ToInt(0);
                if (id > 0)
                {
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        TransportationOrderController.UpdateStatus(t.ID, 0, DateTime.Now, obj_user.Username);
                        PJUtils.ShowMessageBoxSwAlert("Hủy đơn hàng thành công", "s", true, Page);
                    }
                }
            }
        }
    }
}
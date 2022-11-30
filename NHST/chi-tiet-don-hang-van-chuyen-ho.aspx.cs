using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class chi_tiet_don_hang_van_chuyen_ho1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "vu221092";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }

        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    ViewState["ID"] = id;
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        double totalPrice = Math.Round(Convert.ToDouble(t.TotalPrice), 0);
                        double deposited = Math.Round(Convert.ToDouble(t.Deposited), 0);
                        double warehouseFee = 0;
                        if (t.WarehouseFee != null)
                        {
                            warehouseFee = Math.Round(Convert.ToDouble(t.WarehouseFee), 0);
                        }
                        double totalmustpayleft = totalPrice + warehouseFee - deposited;
                        double totalPay = Math.Round(totalPrice + warehouseFee, 0);
                        string createdDate = string.Format("{0:dd/MM/yyyy}", t.CreatedDate);
                        double totalWeight = 0;
                        double totalPackage = 0;
                        int stt = Convert.ToInt32(t.Status);
                        string status = PJUtils.generateTransportationStatusNew2(stt);
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
                        int packageInVN = 0;
                        if (stt == 0)
                        {
                            var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                            if (tD.Count > 0)
                            {
                                totalPackage = tD.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in tD)
                                {
                                    double weight = Math.Round(Convert.ToDouble(s.Weight), 1);
                                    bool isCheckProduct = false;
                                    bool isPackaged = false;
                                    bool isInsurrance = false;
                                    if (s.IsCheckProduct != null)
                                    {
                                        isCheckProduct = Convert.ToBoolean(s.IsCheckProduct);
                                    }
                                    if (s.IsPackaged != null)
                                    {
                                        isPackaged = Convert.ToBoolean(s.IsPackaged);
                                    }
                                    if (s.IsInsurrance != null)
                                    {
                                        isInsurrance = Convert.ToBoolean(s.IsInsurrance);
                                    }
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("<td>" + s.TransportationOrderCode + "</td>");
                                    htmlPackages.Append("<td>" + s.ProductType + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.ProductQuantity + "</td>");
                                    htmlPackages.Append("<td>" + s.StaffNoteCheck + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.Weight + "</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isCheckProduct == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isPackaged == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isInsurrance == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }
                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">¥" + s.CODTQCYN + " - " + string.Format("{0:N0}", Convert.ToDouble(s.CODTQVND)) + " VNĐ</td>");
                                    htmlPackages.Append("<td class=\"tb-date\">" + s.UserNote + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">");
                                    htmlPackages.Append("<span class=\"badge black white-text darken-2\">Đã hủy</span></td>");
                                    htmlPackages.Append("</tr>");
                                    totalWeight += Convert.ToDouble(weight);
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                            }
                            //string btn = "";
                            //btn += "<a href=\"javascript:;\" onclick=\"cancelOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Hủy đơn hàng</a>";
                            //btn += "<a href=\"javascript:;\" onclick=\"payOrder()\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Thanh toán</a>";
                            //btn += "<a href=\"/danh-sach-don-van-chuyen-ho\" class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display\">Trở về</a>";

                        }
                        else if (stt == 1)
                        {
                            var tD = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                            if (tD.Count > 0)
                            {
                                totalPackage = tD.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in tD)
                                {
                                    double weight = Math.Round(Convert.ToDouble(s.Weight), 1);
                                    bool isCheckProduct = false;
                                    bool isPackaged = false;
                                    bool isInsurrance = false;
                                    if (s.IsCheckProduct != null)
                                    {
                                        isCheckProduct = Convert.ToBoolean(s.IsCheckProduct);
                                    }
                                    if (s.IsPackaged != null)
                                    {
                                        isPackaged = Convert.ToBoolean(s.IsPackaged);
                                    }
                                    if (s.IsInsurrance != null)
                                    {
                                        isInsurrance = Convert.ToBoolean(s.IsInsurrance);
                                    }
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("<td>" + s.TransportationOrderCode + "</td>");
                                    htmlPackages.Append("<td>" + s.ProductType + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.ProductQuantity + "</td>");
                                    htmlPackages.Append("<td>" + s.StaffNoteCheck + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.Weight + "</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isCheckProduct == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isPackaged == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isInsurrance == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }
                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">¥" + s.CODTQCYN + " - " + string.Format("{0:N0}", Convert.ToDouble(s.CODTQVND)) + " VNĐ</td>");
                                    htmlPackages.Append("<td class=\"tb-date\">" + s.UserNote + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">");
                                    htmlPackages.Append("<span class=\"badge red white-text darken-2\">Chờ duyệt</span></td>");
                                    htmlPackages.Append("</tr>");


                                    totalWeight += Convert.ToDouble(weight);
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                            }
                            ltrBtn.Text += " <a href=\"javascript:;\" onclick=\"cancelOrder()\" class=\"btn\">Hủy đơn hàng</a>";
                            btnHuy.Visible = true;
                            btnHuy.Attributes.Add("style", "display:none");
                        }
                        else
                        {
                            var packages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (packages.Count > 0)
                            {
                                totalPackage = packages.Count;
                                StringBuilder htmlPackages = new StringBuilder();
                                foreach (var s in packages)
                                {
                                    if (s.Status == 3)
                                    {
                                        packageInVN = 1;
                                    }
                                    double weight = 0;
                                    double weightCN = Math.Round(Convert.ToDouble(s.Weight), 1);
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

                                    bool isCheckProduct = false;
                                    bool isPackaged = false;
                                    bool isInsurrance = false;
                                    if (s.IsCheckProduct != null)
                                    {
                                        isCheckProduct = Convert.ToBoolean(s.IsCheckProduct);
                                    }
                                    if (s.IsPackaged != null)
                                    {
                                        isPackaged = Convert.ToBoolean(s.IsPackaged);
                                    }
                                    if (s.IsInsurrance != null)
                                    {
                                        isInsurrance = Convert.ToBoolean(s.IsInsurrance);
                                    }
                                    htmlPackages.Append("<tr>");
                                    htmlPackages.Append("<td>" + s.OrderTransactionCode + "</td>");
                                    htmlPackages.Append("<td>" + s.ProductType + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.ProductQuantity + "</td>");
                                    htmlPackages.Append("<td>" + s.StaffNoteCheck + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + s.Weight + "</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isCheckProduct == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isPackaged == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }

                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"center-checkbox\">");
                                    htmlPackages.Append("<label>");
                                    if (isInsurrance == true)
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" checked disabled />");
                                    }
                                    else
                                    {
                                        htmlPackages.Append("<input type=\"checkbox\" disabled />");
                                    }
                                    htmlPackages.Append("<span></span>");
                                    htmlPackages.Append("</label>");
                                    htmlPackages.Append("</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">¥" + s.CODTQCYN + " - " + string.Format("{0:N0}", Convert.ToDouble(s.CODTQVND)) + " VNĐ</td>");
                                    htmlPackages.Append("<td class=\"tb-date\">" + s.UserNote + "</td>");
                                    htmlPackages.Append("<td class=\"no-wrap\">" + PJUtils.IntToStringStatusSmallPackageWithBGNew(Convert.ToInt32(s.Status)) + "</td>");
                                    htmlPackages.Append("</tr>");


                                    if (s.Status > 2)
                                        totalWeight += weight;
                                }
                                ltrListPackage.Text = htmlPackages.ToString();
                                if (totalPrice > 0)
                                {
                                    double feeinwarehouse = 0;
                                    if (t.WarehouseFee != null)
                                        feeinwarehouse = Math.Round(Convert.ToDouble(t.WarehouseFee), 0);

                                    double leftPrice = Math.Round((totalPrice + feeinwarehouse) - deposited, 0);
                                    if (leftPrice > 0)
                                    {
                                        if (packageInVN == 1)
                                        {
                                            ltrBtn.Text += "  <a href=\"javascript:;\" onclick=\"payOrder()\" class=\"btn\">Thanh toán</a>";
                                            btnPay.Visible = true;
                                            btnPay.Attributes.Add("style", "display:none");
                                        }
                                    }
                                }
                            }
                        }
                        #region Lấy thông tin
                        StringBuilder html = new StringBuilder();

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Ngày tạo đơn hàng:</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\" col s12 m6\">");
                        html.Append("<p class=\"black-text bold\">" + createdDate + "</p>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Trạng thái:</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\">" + status + "</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Kho TQ</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\"><p class=\"black-text bold\">" + khoTQ + "</p></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Kho đích</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\"><p class=\"black-text bold\">" + khoDich + "</p></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Phương thức vận chuyển</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\"><p class=\"black-text bold\">" + shippingTypeName + "</p></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tổng số kiện</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\"><p class=\"black-text bold\">" + totalPackage + "</p></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tổng cân nặng</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"col s12 m6\"><p class=\"black-text bold\">" + totalWeight + " Kg</p></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Phí kiểm hàng</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", t.CheckProductFee) + "\" disabled>");
                        html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Phí đóng gỗ</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", t.PackagedFee) + "\" disabled>");
                        html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Phí bảo hiểm</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", t.InsurranceFee) + "\" disabled>");
                        html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tổng COD Trung Quốc</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", t.TotalCODTQVND) + "\" disabled>");
                        html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                        html.Append("</div>");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + t.TotalCODTQCYN + "\" disabled>");
                        html.Append("<label>Tệ (¥)</label>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tiền lưu kho</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m6\">");
                        html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", warehouseFee) + "\" disabled class=\"\">");
                        html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        if (totalPrice > 0)
                        {
                            html.Append("<div class=\"order-row\">");
                            html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tổng tiền vận chuyển</p></div>");
                            html.Append("<div class=\"right-content\">");
                            html.Append("<div class=\"row\">");
                            html.Append("<div class=\"input-field col s12 m6\">");
                            html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", totalPrice) + "\" disabled class=\"\">");
                            html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                            html.Append("</div>");
                            html.Append("</div>");
                            html.Append("</div>");
                            html.Append("</div>");

                            html.Append("<div class=\"order-row\">");
                            html.Append("<div class=\"left-fixed\"><p class=\"txt\">Tổng tiền</p></div>");
                            html.Append("<div class=\"right-content\">");
                            html.Append("<div class=\"row\">");
                            html.Append("<div class=\"input-field col s12 m6\">");
                            html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", totalPay) + "\" disabled class=\"bold\">");
                            html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                            html.Append("</div>");
                            html.Append("</div>");
                            html.Append("</div>");
                            html.Append("</div>");
                            if (totalmustpayleft >= deposited)
                            {
                                html.Append("<div class=\"order-row\">");
                                html.Append("<div class=\"left-fixed\"><p class=\"txt\">Đã thanh toán</p></div>");
                                html.Append("<div class=\"right-content\">");
                                html.Append("<div class=\"row\">");
                                html.Append("<div class=\"input-field col s12 m6\">");
                                html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", deposited) + "\" disabled class=\"bold\">");
                                html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                double leftMoney = totalPay - deposited;
                                html.Append("<div class=\"order-row\">");
                                html.Append("<div class=\"left-fixed\"><p class=\"txt\">Còn lại</p></div>");
                                html.Append("<div class=\"right-content\">");
                                html.Append("<div class=\"row\">");
                                html.Append("<div class=\"input-field col s12 m6\">");
                                html.Append("<input placeholder=\"0\" type=\"text\" value=\"" + string.Format("{0:N0}", leftMoney) + "\" disabled class=\"red-text bold\">");
                                html.Append("<label>Việt Nam đồng (VNĐ)</label>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                            }
                        }

                        html.Append("<div class=\"order-row\">");
                        html.Append("<div class=\"left-fixed\"><p class=\"txt\">Ghi chú:</p></div>");
                        html.Append("<div class=\"right-content\">");
                        html.Append("<div class=\"row\">");
                        html.Append("<div class=\"input-field col s12 m12\"><textarea id=\"textarea2\" class=\"materialize-textarea\">" + t.Description + "</textarea></div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("</div>");

                        ltrInfor.Text = html.ToString();
                        #endregion


                        #region load Map
                        List<warehouses> lwh = new List<warehouses>();
                        var smallkhoTQ = WarehouseFromController.GetByID(t.WarehouseFromID.Value);
                        if (smallkhoTQ != null)
                        {
                            warehouses wh = new warehouses();
                            wh.name = smallkhoTQ.WareHouseName;
                            wh.lat = smallkhoTQ.Latitude;
                            wh.lng = smallkhoTQ.Longitude;

                            ltrTQ.Text = "<div class=\"from\"><span class=\"lb position\" data-lat=\"" + smallkhoTQ.Latitude + "\" data-lng=\"" + smallkhoTQ.Longitude + "\" id=\"js-map-from\">" + smallkhoTQ.WareHouseName + "</span></div>";

                            var lsmall = SmallPackageController.GetByTransportationOrderID(t.ID);
                            if (lsmall.Count > 0)
                            {
                                var inTQ = lsmall.Where(x => Convert.ToInt32(x.CurrentPlaceID) == smallkhoTQ.ID && x.Status == 2).ToList();
                                if (inTQ.Count > 0)
                                {
                                    List<package> lpc = new List<package>();
                                    foreach (var item in inTQ)
                                    {
                                        package pk = new package();
                                        pk.code = item.OrderTransactionCode;
                                        pk.status = "Đang vận chuyển";
                                        pk.classColor = "being-transport";
                                        lpc.Add(pk);
                                    }
                                    wh.package = lpc;
                                }
                            }
                            lwh.Add(wh);
                        }

                        var smallkhoVN = WarehouseController.GetByID(Convert.ToInt32(t.WarehouseID.Value));
                        if (smallkhoVN != null)
                        {
                            warehouses wh = new warehouses();
                            wh.name = smallkhoVN.WareHouseName;
                            wh.lat = smallkhoVN.Latitude;
                            wh.lng = smallkhoVN.Longitude;

                            ltrVN.Text = "<div class=\"to\"><span class=\"lb position\" data-lat=\"" + smallkhoVN.Latitude + "\" data-lng=\"" + smallkhoVN.Longitude + "\" id=\"js-map-to\">" + smallkhoVN.WareHouseName + "</span></div>";

                            var lsmall = SmallPackageController.GetByTransportationOrderID(t.ID);
                            if (lsmall.Count > 0)
                            {
                                var inVN = lsmall.Where(x => Convert.ToInt32(x.CurrentPlaceID) == smallkhoVN.ID && x.Status == 3).ToList();
                                if (inVN.Count > 0)
                                {
                                    List<package> lpc = new List<package>();
                                    foreach (var item in inVN)
                                    {
                                        package pk = new package();
                                        pk.code = item.OrderTransactionCode;
                                        pk.status = "Đã về kho đích";
                                        pk.classColor = "transported";
                                        lpc.Add(pk);
                                    }
                                    wh.package = lpc;
                                }
                            }
                            lwh.Add(wh);
                        }

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        hdfLoadMap.Value = serializer.Serialize(lwh);

                        #endregion

                    }
                }
            }
        }

        public class warehouses
        {
            public string name { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
            public List<package> package { get; set; }
        }

        public class package
        {
            public string code { get; set; }
            public string status { get; set; }
            public string classColor { get; set; }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                double wallet = Convert.ToDouble(obj_user.Wallet);
                int UID = obj_user.ID;
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
                                double weightCN = Math.Round(Convert.ToDouble(p.Weight), 1);
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
                            warehouseFee = Math.Round(Convert.ToDouble(t.WarehouseFee), 0);
                        }
                        double totalPrice = 0;
                        double priceWeight = Math.Round(price * totalWeight, 0);
                        double deposited = Math.Round(Convert.ToDouble(t.Deposited), 0);
                        //double totalPrice = price * totalWeight * currency + warehouseFee;

                        double CheckProductFee = Convert.ToDouble(t.CheckProductFee);
                        double PackagedFee = Convert.ToDouble(t.PackagedFee);
                        double TotalCODTQVND = Convert.ToDouble(t.TotalCODTQVND);
                        double InsurranceFee = Convert.ToDouble(t.InsurranceFee);

                        totalPrice = CheckProductFee + PackagedFee + TotalCODTQVND + InsurranceFee
                            + priceWeight + warehouseFee;
                        totalPrice = Math.Round(totalPrice, 0);
                        double leftMoney = Math.Round(totalPrice - deposited, 0);
                        if (leftMoney > 0)
                        {
                            if (leftMoney <= wallet)
                            {
                                double walletLeft = Math.Round(wallet - leftMoney, 0);

                                int a = TransactionController.PayVanChuyenHo(t.ID, totalPrice, 6, currentDate, username_current, UID, walletLeft, 0, leftMoney, username_current + " đã thanh toán đơn hàng vận chuyển hộ: " + t.ID + ".", 1, 8);
                                if (a == 1)
                                {
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
                                                                                       10, currentDate, username_current, false);
                                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                    string datalink = "" + strUrl + "manager/chi-tet-don-hang-van-chuyen-ho/" + t.ID;
                                                    PJUtils.PushNotiDesktop(admin.ID, "Đơn hàng vận chuyển hộ " + t.ID + " đã được thanh toán.", datalink);

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
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý, vui lòng thử lại sau", "e", true, Page);
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
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int UID = obj_user.ID;
                var id = ViewState["ID"].ToString().ToInt(0);
                if (id > 0)
                {
                    var t = TransportationOrderController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        TransportationOrderController.UpdateStatus(t.ID, 0, DateTime.Now, username_current);
                        PJUtils.ShowMessageBoxSwAlert("Hủy đơn hàng thành công", "s", true, Page);
                    }
                }
            }
        }
    }
}
using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Thanh_toan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] != null)
                {
                    if (Session["PayAllTempOrder"] == null)
                    {
                        Response.Redirect("/gio-hang");
                    }
                    else
                    {
                        List<string> orderId = (List<string>)Session["PayAllTempOrder"];
                        if (orderId != null)
                        {
                            if (orderId.Count > 0)
                            {
                                UpdateCheck(orderId);
                            }
                            else
                            {
                                Response.Redirect("/gio-hang");
                            }
                        }
                        else
                        {
                            Response.Redirect("/gio-hang");
                        }

                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                //LoadReceiPlace();
            }
        }
        public void LoadReceiPlace()
        {

        }
        public void UpdateCheck(List<string> orderId)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
                {
                    if (Convert.ToDouble(obj_user.Currency) > 0)
                    {
                        current = Convert.ToDouble(obj_user.Currency);
                    }
                }
                if (orderId.Count > 0)
                {
                    for (int i = 0; i < orderId.Count; i++)
                    {
                        int ID = orderId[i].ToInt(0);
                        if (ID > 0)
                        {
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                            if (shop != null)
                            {
                                if (shop.IsCheckProduct == true)
                                {
                                    double total = 0;
                                    var listpro = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                    int counpros_more10 = 0;
                                    int counpros_les10 = 0;
                                    if (listpro.Count > 0)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            //counpros += item.quantity.ToInt(1);
                                            double countProduct = item.quantity.ToInt(1);
                                            if (Convert.ToDouble(item.price_origin) >= 10)
                                            {
                                                counpros_more10 += item.quantity.ToInt(1);
                                            }
                                            else
                                            {
                                                counpros_les10 += item.quantity.ToInt(1);
                                            }
                                        }

                                        var mfeecheck = FeeCheckProductController.GetByQuantityAndType(counpros_more10, 2);
                                        var lfeecheck = FeeCheckProductController.GetByQuantityAndType(counpros_les10, 1);

                                        if (counpros_more10 > 0)
                                        {
                                            if (counpros_more10 >= 1 && counpros_more10 <= 5)
                                            {
                                                total = total + (5000 * counpros_more10);
                                            }
                                            else if (counpros_more10 > 5 && counpros_more10 <= 20)
                                            {
                                                total = total + (3500 * counpros_more10);
                                            }
                                            else if (counpros_more10 > 20 && counpros_more10 <= 100)
                                            {
                                                total = total + (2000 * counpros_more10);
                                            }
                                            else if (counpros_more10 > 100)
                                            {
                                                total = total + (1500 * counpros_more10);
                                            }
                                        }
                                        if (counpros_les10 > 0)
                                        {
                                            if (counpros_les10 >= 1 && counpros_les10 <= 5)
                                            {
                                                total = total + (1000 * counpros_les10);
                                            }
                                            else if (counpros_les10 > 5 && counpros_les10 <= 20)
                                            {
                                                total = total + (800 * counpros_les10);
                                            }
                                            else if (counpros_les10 > 20 && counpros_les10 <= 100)
                                            {
                                                total = total + (700 * counpros_les10);
                                            }
                                            else if (counpros_les10 > 100)
                                            {
                                                total = total + (500 * counpros_les10);
                                            }
                                        }

                                    }
                                    total = Math.Round(total, 0);
                                    OrderShopTempController.UpdateCheckProductPrice(shop.ID, total.ToString());
                                }
                            }
                        }
                    }
                }
            }
            LoadData(orderId);
        }


        public void LoadData(List<string> orderId)
        {
            ltrTitle.Text = ConfigurationController.GetByTop1().Websitename;
            try
            {
                Session.Remove("orderitem");
                double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var warehouses = WarehouseController.GetAllWithIsHidden(false);
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    StringBuilder html = new StringBuilder();
                    double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                    if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
                    {
                        if (Convert.ToDouble(obj_user.Currency) > 0)
                        {
                            current = Convert.ToDouble(obj_user.Currency);
                        }
                    }
                    int khoTQ = 1;
                    int khoVN = 1;
                    if (Convert.ToInt32(obj_user.WarehouseFrom) > 0)
                        khoTQ = Convert.ToInt32(obj_user.WarehouseFrom);
                    if (Convert.ToInt32(obj_user.WarehouseTo) > 0)
                        khoVN = Convert.ToInt32(obj_user.WarehouseTo);
                    int Shipping = 1;
                    if (Convert.ToInt32(obj_user.ShippingType) > 0)
                        Shipping = Convert.ToInt32(obj_user.ShippingType);
                    var ui = AccountInfoController.GetByUserID(obj_user.ID);
                    double FeeBuyProduct = 0;
                    double UL_CKFeeBuyPro = 0;
                    double UL_CKFeeWeight = 0;
                    if (ui != null)
                    {
                        txt_Fullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_DFullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_Address.Text = ui.Address;
                        txt_DAddress.Text = ui.Address;
                        txt_Email.Text = ui.Email;
                        txt_DEmail.Text = ui.Email;
                        txt_Phone.Text = ui.Phone;
                        txt_DPhone.Text = ui.Phone;
                        UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                    }

                    if (orderId.Count > 0)
                    {
                        double totalfinal = 0;
                        int itemi = 1;

                        for (int i = 0; i < orderId.Count; i++)
                        {
                            int ID = orderId[i].ToInt(0);
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                            if (shop != null)
                            {
                                double fastprice = 0;
                                double pricepro = Math.Round(Convert.ToDouble(shop.PriceVND), 0);
                                double priceproCYN = Math.Round(Convert.ToDouble(shop.PriceCNY), 2);

                                html.Append("<div class=\"shop-item-wrap\" data-id=\"" + shop.ID + "\">");

                                html.Append("<div class=\"shop-name\">");
                                html.Append("<span class=\"black-text\">" + shop.ShopName + "</span>");
                                html.Append("</div>");

                                double priceVND = 0;
                                double priceCYN = 0;
                                //Lấy Sản phẩm
                                html.Append("<div class=\"list-product\">");
                                var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                if (proOrdertemp != null)
                                {
                                    if (proOrdertemp.Count > 0)
                                    {
                                        foreach (var item in proOrdertemp)
                                        {
                                            int quantity = Convert.ToInt32(item.quantity);
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);
                                            double u_pricecbuy = 0;
                                            double u_pricevn = 0;
                                            double e_pricebuy = 0;
                                            double e_pricevn = 0;
                                            double e_pricetemp = 0;
                                            double e_totalproduct = 0;
                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                    u_pricevn = promotionprice * current;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                                u_pricevn = originprice * current;
                                            }


                                            e_pricebuy = u_pricecbuy * quantity;
                                            e_pricevn = u_pricevn * quantity;

                                            priceVND += e_pricevn;
                                            priceCYN += e_pricebuy;


                                            string image = item.image_origin;
                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }

                                            html.Append("<div class=\"product\">");
                                            html.Append("<div class=\"pd-top\">");
                                            html.Append("<div class=\"product-img\"><img src=\"" + image + "\" alt=\"avatar\"></div>");
                                            html.Append("<div class=\"product-name\"><a href=\"" + item.link_origin + "\" target=\"_blank\" class=\"black-text font-weight-500\">" + item.title_origin + "</a></div>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"pd-middle\">" + item.property + "</div>");
                                            html.Append("<div class=\"pd-middle\">");
                                            html.Append("<div class=\"md-price\"><span class=\"red-text font-weight-500\">¥" + u_pricecbuy + "</span> / <span class=\"red-text font-weight-500\">" + string.Format("{0:N0}", u_pricevn) + "đ</span></div>");
                                            html.Append("<div class=\"md-amount\"><span class=\"black-text\">x" + item.quantity + "</span></div>");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                        }
                                    }
                                }
                                html.Append("</div>");

                                pricepro = Math.Round(Convert.ToDouble(priceVND), 0);
                                priceproCYN = Math.Round(Convert.ToDouble(priceCYN), 2);

                                #region Dịch vụ
                                html.Append("<div class=\"services-collect mt-3\">");
                                html.Append("<p class=\"title-service\">Dịch vụ đính kèm</p>");
                                html.Append("<div class=\"list-services-wrap\">");
                                if (shop.IsFastDelivery == true)
                                    html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Giao tận nhà</span></label>");
                                else
                                    html.Append("<label><input type=\"checkbox\" disabled /><span>Giao tận nhà</span></label>");

                                if (shop.IsCheckProduct == true)
                                    html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Kiểm đếm</span></label>");
                                else
                                    html.Append("<label><input type=\"checkbox\" disabled /><span>Kiểm đếm</span></label>");
                                if (shop.IsPacked == true)
                                    html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Đóng gỗ</span></label>");
                                else
                                    html.Append("<label><input type=\"checkbox\" disabled /><span>Đóng gỗ</span></label>");

                                if (shop.IsInsurrance == true)
                                    html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Bảo hiểm</span></label>");
                                else
                                    html.Append("<label><input type=\"checkbox\" disabled /><span>Bảo hiểm</span></label>");

                                //html.Append("<label><input type=\"checkbox\" /><span>Chụp hình</span></label>");
                                html.Append("</div>");
                                html.Append("</div>");
                                #endregion

                                #region Tính Phí
                                priceVND = Math.Round(priceVND, 0);
                                priceCYN = Math.Round(priceCYN, 2);
                                OrderShopTempController.UpdatePriceVNDCNY(shop.ID, priceVND.ToString(), priceCYN.ToString());
                                pricepro = priceVND;
                                double total = fastprice + pricepro;
                                total = Math.Round(total, 0);

                                double FeeCNShip = 0;
                                double FeeBuyPro = 0;
                                double FeeCheck = 0;
                                if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                                    FeeCheck = Convert.ToDouble(shop.IsCheckProductPrice);
                                FeeCheck = Math.Round(FeeCheck, 0);
                                double totalFee_CountFee = total + FeeCNShip + FeeCheck;
                                double servicefee = 0;

                                double feebpnotdc = 0;
                                var adminfeebuypro = FeeBuyProController.GetAll();
                                if (adminfeebuypro.Count > 0)
                                {
                                    foreach (var item in adminfeebuypro)
                                    {
                                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        {
                                            double feepercent = 0;
                                            if (item.FeePercent.ToString().ToFloat(0) > 0)
                                                feepercent = Convert.ToDouble(item.FeePercent);
                                            servicefee = feepercent / 100;
                                            //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                            break;
                                        }
                                    }
                                }


                                if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                {
                                    if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                    {
                                        feebpnotdc = pricepro * Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                                    }
                                    else
                                        feebpnotdc = pricepro * servicefee;
                                }
                                else
                                    feebpnotdc = pricepro * servicefee;

                                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                double feebp = feebpnotdc - subfeebp;
                                feebp = Math.Round(feebp, 0);                               
                                if (feebp < 10000)
                                    feebp = 10000;
                                FeeBuyPro = feebp;

                                double totalInsur = 0;
                                if (shop.IsInsurrance == true)
                                    totalInsur = priceVND * (InsurancePercent / 100);

                                total = total + FeeCNShip + FeeBuyPro + FeeCheck + totalInsur;

                                totalfinal += Math.Round(total, 0);

                                html.Append("<ul class=\"collapsible fee-item\" data-collapsible=\"accordion\">");
                                html.Append("<li class=\"\">");
                                html.Append("<div class=\"collapsible-header\"><span class=\"red-text\">Xem phí đơn hàng này</span></div>");
                                html.Append("<div class=\"collapsible-body\">");
                                html.Append("<div class=\"list-fee\">");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Tổng tiền hàng</span></div>");
                                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", pricepro) + "đ</span></div>");
                                html.Append("</div>");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí ship TQ</span></div>");
                                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                                html.Append("</div>");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí mua hàng</span></div>");
                                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", FeeBuyPro) + "đ</span></div>");
                                html.Append("</div>");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí vận chuyển TQ - VN</span></div>");
                                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                                html.Append("</div>");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí kiểm đếm</span></div>");
                                if (shop.IsCheckProduct == true)
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", FeeCheck) + "đ</span></div>");
                                else
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                                html.Append("</div>");
                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí đóng gỗ</span></div>");
                                if (shop.IsPacked == true)
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                                else
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                                html.Append("</div>");

                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí bảo hiểm</span></div>");
                                if (shop.IsInsurrance == true)
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", totalInsur) + "đ</span></div>");
                                else
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                                html.Append("</div>");

                                html.Append("<div class=\"fee-row\">");
                                html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí giao hàng</span></div>");
                                if (shop.IsFastDelivery == true)
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                                else
                                    html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</li>");
                                html.Append("</ul>");

                                #endregion

                                #region Thông tin kiện hàng
                                html.Append("<div class=\"services-collect mt-3\">");
                                html.Append("<p class=\"title-service\">Thông tin kiện hàng</p>");
                                html.Append("<div class=\"list-services-wrap\">");
                                html.Append("<div class=\"row\">");
                                html.Append("<div class=\"col s12 input-field\">");
                                html.Append("<select name=\"\" id=\"\" class=\"warehosefromselect\">");
                                html.Append("<option value=\"0\">Chọn kho TQ</option>");
                                var warehouseTQ = WarehouseFromController.GetAllWithIsHidden(false);
                                if (warehouseTQ.Count > 0)
                                {
                                    foreach (var w in warehouseTQ)
                                    {
                                        if (khoTQ == w.ID)
                                            html.Append("<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>");
                                        else
                                            html.Append("<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>");
                                    }
                                }
                                html.Append("</select>");
                                html.Append("<label for=\"\">Chọn kho TQ</label>");
                                html.Append("</div>");
                                html.Append("<div class=\"col s12 input-field\">");
                                html.Append("<select name=\"\" id=\"\" class=\"warehoseselect\">");
                                html.Append("<option value=\"0\">Chọn kho VN</option>");
                                if (warehouses.Count > 0)
                                {
                                    foreach (var w in warehouses)
                                    {
                                        if (khoVN == w.ID)
                                            html.Append("<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>");
                                        else
                                            html.Append("<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>");
                                    }
                                }
                                html.Append("</select>");
                                html.Append("<label for=\"\">Chuyển về kho</label>");
                                html.Append("</div>");
                                html.Append("<div class=\"col s12 input-field\" >");
                                html.Append("<select name=\"\" id=\"\" class=\"shippingtypesselect\">");
                                html.Append("<option value=\"0\">Chọn phương thức VC</option>");
                                var shippingType = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
                                if (shippingType.Count > 0)
                                {
                                    foreach (var item in shippingType)
                                    {
                                        if (Shipping == item.ID)
                                            html.Append("  <option value=\"" + item.ID + "\" selected>" + item.ShippingTypeName + "</option>");
                                        else
                                            html.Append("  <option value=\"" + item.ID + "\">" + item.ShippingTypeName + "</option>");
                                    }
                                }
                                html.Append("</select>");
                                html.Append("<label for=\"\">Phương thức vận chuyển</label>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                #endregion

                                html.Append("</div>");
                            }
                        }

                        ltrTotal.Text += "<div class=\"fee-row\">";
                        ltrTotal.Text += "<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Tổng tiền</span>";
                        ltrTotal.Text += "</div>";
                        ltrTotal.Text += "<div class=\"col-fix\"><span class=\"red-text font-weight-500 price total-price\">" + string.Format("{0:N0}", totalfinal) + "đ</span></div>";
                        ltrTotal.Text += "</div>";
                    }
                    //    else
                    //    {
                    //        int orderID = Convert.ToInt32(rq);
                    //        var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, orderID);
                    //        int itemi = 1;
                    //        if (shop != null)
                    //        {
                    //            Session["orderitem"] = orderID;
                    //            double fastprice = 0;
                    //            double pricepro = Math.Round(Convert.ToDouble(shop.PriceVND), 0);
                    //            double priceproCYN = Math.Round(Convert.ToDouble(shop.PriceCNY), 2);

                    //            html.Append("<div class=\"shop-item-wrap\" data-id=\"" + shop.ID + "\">");

                    //            html.Append("<div class=\"shop-name\">");
                    //            html.Append("<span class=\"black-text\">" + shop.ShopName + "</span>");
                    //            html.Append("</div>");

                    //            double priceVND = 0;
                    //            double priceCYN = 0;
                    //            //Lấy Sản phẩm
                    //            html.Append("<div class=\"list-product\">");
                    //            var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                    //            if (proOrdertemp != null)
                    //            {
                    //                if (proOrdertemp.Count > 0)
                    //                {
                    //                    foreach (var item in proOrdertemp)
                    //                    {
                    //                        int quantity = Convert.ToInt32(item.quantity);
                    //                        double originprice = Convert.ToDouble(item.price_origin);
                    //                        double promotionprice = Convert.ToDouble(item.price_promotion);
                    //                        double u_pricecbuy = 0;
                    //                        double u_pricevn = 0;
                    //                        double e_pricebuy = 0;
                    //                        double e_pricevn = 0;
                    //                        double e_pricetemp = 0;
                    //                        double e_totalproduct = 0;

                    //                        if (promotionprice > 0)
                    //                        {
                    //                            if (promotionprice < originprice)
                    //                            {
                    //                                u_pricecbuy = promotionprice;
                    //                                u_pricevn = promotionprice * current;
                    //                            }
                    //                            else
                    //                            {
                    //                                u_pricecbuy = originprice;
                    //                                u_pricevn = originprice * current;
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            u_pricecbuy = originprice;
                    //                            u_pricevn = originprice * current;
                    //                        }


                    //                        e_pricebuy = u_pricecbuy * quantity;
                    //                        e_pricevn = u_pricevn * quantity;

                    //                        priceVND += e_pricevn;
                    //                        priceCYN += e_pricebuy;

                    //                        string image = item.image_origin;
                    //                        if (image.Contains("%2F"))
                    //                        {
                    //                            image = image.Replace("%2F", "/");
                    //                        }
                    //                        if (image.Contains("%3A"))
                    //                        {
                    //                            image = image.Replace("%3A", ":");
                    //                        }

                    //                        html.Append("<div class=\"product\">");
                    //                        html.Append("<div class=\"pd-top\">");
                    //                        html.Append("<div class=\"product-img\"><img src=\"" + image + "\" alt=\"avatar\"></div>");
                    //                        html.Append("<div class=\"product-name\"><a href=\"" + item.link_origin + "\" target=\"_blank\" class=\"black-text font-weight-500\">" + item.title_origin + "</a></div>");
                    //                        html.Append("</div>");
                    //                        html.Append("<div class=\"pd-middle\">" + item.property + "</div>");
                    //                        html.Append("<div class=\"pd-middle\">");
                    //                        html.Append("<div class=\"md-price\"><span class=\"red-text font-weight-500\">¥" + u_pricecbuy + "</span> / <span class=\"red-text font-weight-500\">" + string.Format("{0:N0}", u_pricevn) + "đ</span></div>");
                    //                        html.Append("<div class=\"md-amount\"><span class=\"black-text\">x" + item.quantity + "</span></div>");
                    //                        html.Append("</div>");
                    //                        html.Append("</div>");
                    //                    }
                    //                }
                    //            }
                    //            html.Append("</div>");

                    //            pricepro = Math.Round(Convert.ToDouble(priceVND), 0);
                    //            priceproCYN = Math.Round(Convert.ToDouble(priceCYN), 2);



                    //            #region Dịch vụ
                    //            html.Append("<div class=\"services-collect mt-3\">");
                    //            html.Append("<p class=\"title-service\">Dịch vụ đính kèm</p>");
                    //            html.Append("<div class=\"list-services-wrap\">");
                    //            if (shop.IsFastDelivery == true)
                    //                html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Giao tận nhà</span></label>");
                    //            else
                    //                html.Append("<label><input type=\"checkbox\" disabled /><span>Giao tận nhà</span></label>");

                    //            if (shop.IsCheckProduct == true)
                    //                html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Kiểm đếm</span></label>");
                    //            else
                    //                html.Append("<label><input type=\"checkbox\" disabled /><span>Kiểm đếm</span></label>");
                    //            if (shop.IsPacked == true)
                    //                html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Đóng gỗ</span></label>");
                    //            else
                    //                html.Append("<label><input type=\"checkbox\" disabled /><span>Đóng gỗ</span></label>");

                    //            if (shop.IsInsurrance == true)
                    //                html.Append("<label><input type=\"checkbox\" disabled checked=\"checked\" /><span>Bảo hiểm</span></label>");
                    //            else
                    //                html.Append("<label><input type=\"checkbox\" disabled /><span>Bảo hiểm</span></label>");

                    //            //html.Append("<label><input type=\"checkbox\" /><span>Chụp hình</span></label>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            #endregion

                    //            #region Tính Phí
                    //            priceVND = Math.Round(priceVND, 0);
                    //            priceCYN = Math.Round(priceCYN, 2);
                    //            OrderShopTempController.UpdatePriceVNDCNY(shop.ID, priceVND.ToString(), priceCYN.ToString());
                    //            pricepro = priceVND;
                    //            double total = fastprice + pricepro;
                    //            total = Math.Round(total, 0);

                    //            double FeeCNShip = 0;
                    //            double FeeBuyPro = 0;
                    //            double FeeCheck = 0;
                    //            if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                    //                FeeCheck = Convert.ToDouble(shop.IsCheckProductPrice);
                    //            FeeCheck = Math.Round(FeeCheck, 0);
                    //            double totalFee_CountFee = total + FeeCNShip + FeeCheck;
                    //            double servicefee = 0;

                    //            double feebpnotdc = 0;
                    //            var adminfeebuypro = FeeBuyProController.GetAll();
                    //            if (adminfeebuypro.Count > 0)
                    //            {
                    //                foreach (var item in adminfeebuypro)
                    //                {
                    //                    if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                    //                    {
                    //                        double feepercent = 0;
                    //                        if (item.FeePercent.ToString().ToFloat(0) > 0)
                    //                            feepercent = Convert.ToDouble(item.FeePercent);
                    //                        servicefee = feepercent / 100;
                    //                        //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                    //            {
                    //                if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                    //                {
                    //                    feebpnotdc = pricepro * Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                    //                }
                    //            }
                    //            else
                    //                feebpnotdc = pricepro * servicefee;


                    //            double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                    //            double feebp = feebpnotdc - subfeebp;
                    //            feebp = Math.Round(feebp, 0);
                    //            FeeBuyPro = feebp;

                    //            double totalInsur = 0;
                    //            if (shop.IsInsurrance == true)
                    //                totalInsur = priceVND * (InsurancePercent / 100);

                    //            total = total + FeeCNShip + FeeBuyPro + FeeCheck + totalInsur;


                    //            html.Append("<ul class=\"collapsible fee-item\" data-collapsible=\"accordion\">");
                    //            html.Append("<li class=\"\">");
                    //            html.Append("<div class=\"collapsible-header\"><span class=\"red-text\">Xem phí đơn hàng này</span></div>");
                    //            html.Append("<div class=\"collapsible-body\">");
                    //            html.Append("<div class=\"list-fee\">");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Tổng tiền hàng</span></div>");
                    //            html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", pricepro) + "đ</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí ship TQ</span></div>");
                    //            html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí mua hàng</span></div>");
                    //            html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", FeeBuyPro) + "đ</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí vận chuyển TQ - VN</span></div>");
                    //            html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí kiểm đếm</span></div>");
                    //            if (shop.IsCheckProduct == true)
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", FeeCheck) + "đ</span></div>");
                    //            else
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí đóng gỗ</span></div>");
                    //            if (shop.IsPacked == true)
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                    //            else
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                    //            html.Append("</div>");

                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí bảo hiểm</span></div>");
                    //            if (shop.IsInsurrance == true)
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">" + string.Format("{0:N0}", totalInsur) + "đ</span></div>");
                    //            else
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                    //            html.Append("</div>");

                    //            html.Append("<div class=\"fee-row\">");
                    //            html.Append("<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Phí giao hàng</span></div>");
                    //            if (shop.IsFastDelivery == true)
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Chờ cập nhật</span></div>");
                    //            else
                    //                html.Append("<div class=\"col-fix\"><span class=\"red-text font-weight-500 price\">Không yêu cầu</span></div>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            html.Append("</li>");
                    //            html.Append("</ul>");

                    //            #endregion

                    //            #region Thông tin kiện hàng
                    //            html.Append("<div class=\"services-collect mt-3\">");
                    //            html.Append("<p class=\"title-service\">Thông tin kiện hàng</p>");
                    //            html.Append("<div class=\"list-services-wrap\">");
                    //            html.Append("<div class=\"row\">");
                    //            html.Append("<div class=\"col s12 input-field\">");
                    //            html.Append("<select name=\"\" id=\"\" class=\"warehosefromselect\">");
                    //            html.Append("<option value=\"0\">Chọn kho TQ</option>");
                    //            var warehouseTQ = WarehouseFromController.GetAllWithIsHidden(false);
                    //            if (warehouseTQ.Count > 0)
                    //            {
                    //                foreach (var w in warehouseTQ)
                    //                {
                    //                    if (khoTQ == w.ID)
                    //                        html.Append("<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>");
                    //                    else
                    //                        html.Append("<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>");
                    //                }
                    //            }
                    //            html.Append("</select>");
                    //            html.Append("<label for=\"\">Chọn kho TQ</label>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"col s12 input-field\">");
                    //            html.Append("<select name=\"\" id=\"\" class=\"warehoseselect\">");
                    //            html.Append("<option value=\"0\">Chọn kho VN</option>");
                    //            if (warehouses.Count > 0)
                    //            {
                    //                foreach (var w in warehouses)
                    //                {
                    //                    if (khoVN == w.ID)
                    //                        html.Append("<option value=\"" + w.ID + "\" selected>" + w.WareHouseName + "</option>");
                    //                    else
                    //                        html.Append("<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>");
                    //                }
                    //            }
                    //            html.Append("</select>");
                    //            html.Append("<label for=\"\">Chuyển về kho</label>");
                    //            html.Append("</div>");
                    //            html.Append("<div class=\"col s12 input-field\" >");
                    //            html.Append("<select name=\"\" id=\"\" class=\"shippingtypesselect\">");
                    //            html.Append("<option value=\"0\">Chọn phương thức VC</option>");
                    //            var shippingType = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
                    //            if (shippingType.Count > 0)
                    //            {
                    //                foreach (var item in shippingType)
                    //                {
                    //                    if (Shipping == item.ID)
                    //                        html.Append("  <option value=\"" + item.ID + "\" selected>" + item.ShippingTypeName + "</option>");
                    //                    else
                    //                        html.Append("  <option value=\"" + item.ID + "\">" + item.ShippingTypeName + "</option>");
                    //                }
                    //            }
                    //            html.Append("</select>");
                    //            html.Append("<label for=\"\">Phương thức vận chuyển</label>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            html.Append("</div>");
                    //            #endregion

                    //            html.Append("</div>");

                    //            ltrTotal.Text += "<div class=\"fee-row\">";
                    //            ltrTotal.Text += "<div class=\"col-fix\"><span class=\"black-text font-weight-500 label\">Tổng tiền</span>";
                    //            ltrTotal.Text += "</div>";
                    //            ltrTotal.Text += "<div class=\"col-fix\"><span class=\"red-text font-weight-500 price total-price\">" + string.Format("{0:N0}", total) + "đ</span></div>";
                    //            ltrTotal.Text += "</div>";

                    //        }
                    //        else
                    //        {
                    //            Response.Redirect("/gio-hang");
                    //        }
                    //    }
                    //    ltr_pro.Text = html.ToString();
                    //}
                    ltr_pro.Text = html.ToString();
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
            catch
            {
                Response.Redirect("/gio-hang");
            }
        }

        protected void btn_saveOrder_Click(object sender, EventArgs e)
        {
            double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
            if (Session["PayAllTempOrder"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                List<string> rq = (List<string>)Session["PayAllTempOrder"];
                if (obj_user != null)
                {
                    double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                    double currencyreal = Convert.ToDouble(ConfigurationController.GetByTop1().CurrencyReal);                   
                    if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
                    {
                        if (Convert.ToDouble(obj_user.Currency) > 0)
                        {
                            current = Convert.ToDouble(obj_user.Currency);
                        }
                    }
                    int salerID = obj_user.SaleID.ToString().ToInt(0);
                    int dathangID = obj_user.DathangID.ToString().ToInt(0);
                    int UID = obj_user.ID;
                    var setNoti = SendNotiEmailController.GetByID(5);
                    //double percent_User = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LevelPercent);
                    double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                    double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                    double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                    string wareship = hdfTeamWare.Value;
                    if (rq != null)
                    {
                        //string[] splitList = rq.Split('_');
                        //string[] list = splitList[1].Split('|');

                        if (rq.Count > 0)
                        {
                            double totalfinal = 0;
                            for (int i = 0; i < rq.Count; i++)
                            {
                                int ID = rq[i].ToInt(0);

                                var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                                if (shop != null)
                                {
                                    int warehouseFromID = 0;
                                    int warehouseID = 0;
                                    int w_shippingType = 0;
                                    string[] w = wareship.Split('|');
                                    if (w.Length - 1 > 0)
                                    {
                                        for (int j = 0; j < w.Length - 1; j++)
                                        {
                                            int shoptempID = (w[j].Split(':')[0]).ToInt(0);
                                            string[] wsinfor = w[j].Split(':')[1].Split('-');
                                            int wareID = (wsinfor[0]).ToInt(1);
                                            int shippingtype = (wsinfor[1]).ToInt(1);
                                            if (ID == shoptempID)
                                            {
                                                warehouseID = wareID;
                                                w_shippingType = shippingtype;                                               
                                                warehouseFromID = (wsinfor[2]).ToInt(2);
                                            }
                                        }
                                    }

                                    double total = 0;
                                    double fastprice = 0;
                                    double pricepro = Math.Round(Convert.ToDouble(shop.PriceVND), 0);
                                    double priceproCYN = Math.Round(Convert.ToDouble(shop.PriceCNY), 2);

                                    double feecnship = 0;         
                                    string ShopID = shop.ShopID;
                                    string ShopName = shop.ShopName;
                                    string Site = shop.Site;
                                    bool IsForward = Convert.ToBoolean(shop.IsForward);
                                    double isForwardPrice = 0;
                                    if (shop.IsForwardPrice.ToFloat(0) > 0)
                                        isForwardPrice = Math.Round(Convert.ToDouble(shop.IsForwardPrice), 0);
                                    string IsForwardPrice = isForwardPrice.ToString();
                                    bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                                    double isFastDeliveryPrice = 0;
                                    if (shop.IsFastDeliveryPrice.ToFloat(0) > 0)
                                        isFastDeliveryPrice = Math.Round(Convert.ToDouble(shop.IsFastDeliveryPrice), 0);
                                    string IsFastDeliveryPrice = isFastDeliveryPrice.ToString();
                                    bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                                    string IsCheckProductPrice = shop.IsCheckProductPrice;
                                    bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                                    double ispackagePrice = 0;
                                    if (shop.IsPackedPrice.ToFloat(0) > 0)
                                        ispackagePrice = Math.Round(Convert.ToDouble(shop.IsPackedPrice), 0);
                                    string IsPackedPrice = ispackagePrice.ToString();
                                    bool IsFast = Convert.ToBoolean(shop.IsFast);
                                    string IsFastPrice = fastprice.ToString();
                                    double pricecynallproduct = 0;

                                    double isCheckProductPrice = 0;
                                    if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                                        isCheckProductPrice = Convert.ToDouble(shop.IsCheckProductPrice);

                                    fastprice = Math.Round(fastprice, 0);
                                    IsFastPrice = fastprice.ToString();
                                    isCheckProductPrice = Math.Round(isCheckProductPrice, 0);
                                    IsCheckProductPrice = isCheckProductPrice.ToString();
                                    double totalFee_CountFee = fastprice + pricepro + feecnship + isCheckProductPrice;

                                    double servicefee = 0;     
                                    double feebpnotdc = 0;
                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                            {
                                                double feepercent = 0;
                                                if (item.FeePercent.ToString().ToFloat(0) > 0)
                                                    feepercent = Convert.ToDouble(item.FeePercent);
                                                servicefee = feepercent / 100;
                                                //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                break;
                                            }
                                        }
                                    }

                                    string FeeBuyProUser = "";
                                    if (!string.IsNullOrEmpty(obj_user.FeeBuyPro))
                                    {
                                        if (obj_user.FeeBuyPro.ToFloat(0) > 0)
                                        {
                                            feebpnotdc = pricepro * Convert.ToDouble(obj_user.FeeBuyPro) / 100;
                                            FeeBuyProUser = obj_user.FeeBuyPro;
                                        }
                                        else
                                            feebpnotdc = pricepro * servicefee;
                                    }
                                    else
                                        feebpnotdc = pricepro * servicefee;
                                    double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                    double feebp = feebpnotdc - subfeebp;
                                    feebp = Math.Round(feebp, 0);
                                    if (feebp < 10000)
                                        feebp = 10000;

                                    //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                                    double ischeckproductprice = 0;
                                    if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                                        ischeckproductprice = Convert.ToDouble(shop.IsCheckProductPrice);
                                    ischeckproductprice = Math.Round(ischeckproductprice, 0);

                                    double InsuranceMoney = 0;
                                    if (shop.IsInsurrance == true)
                                        InsuranceMoney = pricepro * (InsurancePercent / 100);

                                    total = fastprice + pricepro + feebp + feecnship + ischeckproductprice + InsuranceMoney;
                                    //Lấy ra từng ordertemp trong shop

                                    double priceVND = 0;
                                    double priceCYN = 0;
                                    var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                    if (proOrdertemp != null)
                                    {
                                        if (proOrdertemp.Count > 0)
                                        {
                                            foreach (var item in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(item.quantity);
                                                double originprice = Convert.ToDouble(item.price_origin);
                                                double promotionprice = Convert.ToDouble(item.price_promotion);

                                                double u_pricecbuy = 0;
                                                double u_pricevn = 0;
                                                double e_pricebuy = 0;
                                                double e_pricevn = 0;
                                                if (promotionprice > 0)
                                                {
                                                    if (promotionprice < originprice)
                                                    {
                                                        u_pricecbuy = promotionprice;
                                                        u_pricevn = promotionprice * current;
                                                    }
                                                    else
                                                    {
                                                        u_pricecbuy = originprice;
                                                        u_pricevn = originprice * current;
                                                    }
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }


                                                e_pricebuy = u_pricecbuy * quantity;
                                                e_pricevn = u_pricevn * quantity;

                                                priceVND += e_pricevn;
                                                priceCYN += e_pricebuy;

                                                pricecynallproduct += e_pricebuy;
                                            }
                                        }
                                    }
                                    priceVND = Math.Round(priceVND, 0);
                                    priceCYN = Math.Round(priceCYN, 2);
                                    string PriceVND = priceVND.ToString();

                                    pricecynallproduct = Math.Round(pricecynallproduct, 2);
                                    string PriceCNY = priceCYN.ToString();
                                    //string FeeShipCN = (10 * current).ToString();
                                    string FeeShipCN = Math.Round(feecnship, 0).ToString();
                                    string FeeBuyPro = Math.Round(feebp, 0).ToString();
                                    double feeWeight = 0;
                                    if (shop.FeeWeight.ToFloat(0) > 0)
                                        feeWeight = Math.Round(Convert.ToDouble(shop.FeeWeight), 0);

                                    string FeeWeight = feeWeight.ToString();
                                    string Note = shop.Note;
                                    string FullName = txt_DFullname.Text.Trim();
                                    string Address = txt_DAddress.Text.Trim();
                                    string Email = txt_DEmail.Text.Trim();
                                    string Phone = txt_DPhone.Text.Trim();
                                    int Status = 0;
                                    string Deposit = "0";
                                    string CurrentCNYVN = current.ToString();
                                    string TotalPriceVND = Math.Round(total, 0).ToString();
                                    string AmountDeposit = Math.Round((total * LessDeposit / 100)).ToString();
                                    DateTime CreatedDate = DateTime.Now;
                                    string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery,
                                        IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice, IsPacked, IsPackedPrice, IsFast, IsFastPrice,
                                        PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status,
                                        Deposit, CurrentCNYVN, TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1, FeeBuyProUser, currencyreal.ToString());
                                    int idkq = Convert.ToInt32(kq);
                                    if (idkq > 0)
                                    {
                                        foreach (var item in proOrdertemp)
                                        {
                                            int quantity = Convert.ToInt32(item.quantity);
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);
                                            double u_pricecbuy = 0;
                                            double u_pricevn = 0;
                                            double e_pricebuy = 0;
                                            double e_pricevn = 0;

                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                    u_pricevn = promotionprice * current;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                                u_pricevn = originprice * current;
                                            }

                                            e_pricebuy = u_pricecbuy * quantity;
                                            e_pricevn = u_pricevn * quantity;

                                            pricecynallproduct += e_pricebuy;

                                            string image = item.image_origin;
                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }
                                            string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                                            item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                                            item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                                            item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                                            Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                                            fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                                            txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID);

                                            if (item.price_promotion.ToFloat(0) > 0)
                                                OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                                            else
                                                OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                                        }
                                        MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);
                                        MainOrderController.UpdateFromPlace(idkq, UID, warehouseFromID, w_shippingType);
                                        MainOrderController.UpdateIsInsurrance(idkq, Convert.ToBoolean(shop.IsInsurrance));
                                        MainOrderController.UpdateInsurranceMoney(idkq, InsuranceMoney.ToString(), InsurancePercent.ToString());

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
                                                                                           admin.Username, idkq,
                                                                                           "Có đơn hàng mới ID là: " + idkq, 1,
                                                                                            CreatedDate, username, false);
                                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                        string datalink = "" + strUrl + "manager/OrderDetail/" + idkq;
                                                        PJUtils.PushNotiDesktop(admin.ID, "Có đơn hàng mới ID là: " + idkq, datalink);
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {
                                                        NotificationsController.Inser(manager.ID,
                                                                                           manager.Username, idkq,
                                                                                           "Có đơn hàng mới ID là: " + idkq, 1,
                                                                                          CreatedDate, username, false);
                                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                        string datalink = "" + strUrl + "manager/OrderDetail/" + idkq;
                                                        PJUtils.PushNotiDesktop(manager.ID, "Có đơn hàng mới ID là: " + idkq, datalink);
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
                                                            PJUtils.SendMailGmail_new(admin.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Tài khoản " + username + " lên đơn hàng mua hộ mới với ID đơn: " + idkq + "", "");
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
                                                            PJUtils.SendMailGmail_new(manager.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Tài khoản " + username + " lên đơn hàng mua hộ mới với ID đơn: " + idkq + "", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                                                                       
                                            if (setNoti.IsSendEmailUser == true)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail_new(obj_user.Email,
                                                                            "NHẬP HÀNG 360 THÔNG BÁO QUÝ KHÁCH LÊN ĐƠN MUA HỘ THÀNH CÔNG",
                                                                            "Quý khách " + obj_user.Username + " lên đơn mua hộ ID: " + idkq + " thành công.", "");

                                                }
                                                catch { }
                                            }
                                        }

                                    }

                                    double salepercent = 0;
                                    double salepercentaf3m = 0;
                                    double dathangpercent = 0;
                                    var config = ConfigurationController.GetByTop1();
                                    if (config != null)
                                    {
                                        salepercent = Convert.ToDouble(config.SalePercent);
                                        salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                                        dathangpercent = Convert.ToDouble(config.DathangPercent);
                                    }
                                    string salerName = "";
                                    string dathangName = "";

                                    if (salerID > 0)
                                    {
                                        var sale = AccountController.GetByID(salerID);
                                        if (sale != null)
                                        {
                                            salerName = sale.Username;
                                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                            int d = CreatedDate.Subtract(createdDate).Days;
                                            if (d > 90)
                                            {
                                                double per = feebp * salepercentaf3m / 100;
                                                per = Math.Round(per, 0);
                                                StaffIncomeController.Insert(idkq, "0", salepercentaf3m.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, CreatedDate, username);
                                            }
                                            else
                                            {
                                                double per = feebp * salepercent / 100;
                                                per = Math.Round(per, 0);
                                                StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, CreatedDate, username);
                                            }
                                        }
                                    }
                                    if (dathangID > 0)
                                    {
                                        var dathang = AccountController.GetByID(dathangID);
                                        if (dathang != null)
                                        {
                                            dathangName = dathang.Username;
                                            StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                                                CreatedDate, CreatedDate, username);
                                            if (setNoti != null)
                                            {
                                                if (setNoti.IsSentNotiAdmin == true)
                                                {
                                                    NotificationsController.Inser(dathang.ID,
                                                                           dathang.Username, idkq,
                                                                           "Có đơn hàng mới ID là: " + idkq, 1,
                                                                            CreatedDate, username, false);
                                                    string strPathAndQuery = Request.Url.PathAndQuery;
                                                    string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                                    string datalink = "" + strUrl + "manager/OrderDetail/" + idkq;
                                                    PJUtils.PushNotiDesktop(dathang.ID, "Có đơn hàng mới ID là: " + idkq, datalink);
                                                }
                                            }
                                        }
                                    }
                                    //Xóa Shop temp và order temp
                                    OrderShopTempController.Delete(shop.ID);
                                }
                            }
                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hubContext.Clients.All.addNewMessageToPage("", "");
                            Session.Remove("PayAllTempOrder");
                            Response.Redirect("/danh-sach-don-hang?t=1");
                        }
                    }                  
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
            else
            {
                Response.Redirect("/gio-hang");
            }
        }
    }
}
using MB.Extensions;
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

namespace NHST.manager
{
    public partial class Cart_Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (Session["userLoginSystem"] != null)
            //    {
            //        loaddata();
            //    }
            //    else
            //    {
            //        Response.Redirect("/dang-nhap");
            //    }
            //}

            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 6)
                        Response.Redirect("/trang-chu");
                    else
                        loaddata();
                }
            }
        }

        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);


            var uid = Request.QueryString["id"].ToInt(0);
            if (uid > 0)
            {
                var acc = AccountController.GetByID(uid);
                if (acc != null)
                {
                    int UID = 0;
                    double userDeposit = 0;
                    double userFeeBuyPro = 0;
                    double userCurrency = 0;
                    if (acc != null)
                    {
                        UID = acc.ID;
                    }
                    ltrGiohang.Text += "<h4>Giỏ hàng của khách <span style=\"color:red;\">" + acc.Username + "</span></h4>";
                    double pricecurrency = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                    if (!string.IsNullOrEmpty(acc.Currency.ToString()))
                    {
                        if (Convert.ToDouble(acc.Currency) > 0)
                        {
                            pricecurrency = Convert.ToDouble(acc.Currency);
                        }
                    }
                    List<tbl_OrderTemp> os = OrderTempController.GetAllByUID(UID);
                    StringBuilder html = new StringBuilder();

                    ltrTitle.Text = ConfigurationController.GetByTop1().Websitename;

                    double pricefullcart = 0;
                    int totalproduct = 0;
                    int countshop = 0;
                    int i = 1;
                    var oshop = OrderShopTempController.GetByUID(UID);
                    if (oshop != null)
                    {
                        if (oshop.Count > 0)
                        {

                            countshop = oshop.Count();
                            string listorderid = "";
                            foreach (var shop in oshop)
                            {
                                double TotalPriceProductInShop = 0;
                                double TotalPriceShop = 0;
                                listorderid += shop.ID + "|";


                                #region New
                                html.Append(" <div class=\"order-item\">");
                                html.Append("<div class=\"left-info\">");
                                html.Append("<div class=\"order-hd\">");
                                html.Append("<div class=\"shop-cart\" data-item-id=\"" + shop.ID + "\">");
                                html.Append("<label class=\"select-checkout tooltipped\" data-position=\"top\" data-tooltip=\"Chọn đặt đơn hàng này \">");
                                html.Append("<input type=\"checkbox\" onclick=\"getCartToSuccess($(this))\" class=\"filled-in\" />");
                                html.Append("<span class=\"shop-name\">" + shop.ShopName + " <span class=\"green-text checked hide-on-small-and-down\">Đã thêm vào danh sách đặt hàng</span></span>");
                                html.Append("</label>");
                                html.Append("</div>");
                                html.Append("<div class=\"delete\">");
                                //html += "<a href=\"javascript:;\" class=\"btn-update tooltipped\" data-position=\"top\" data-tooltip=\"Cập nhật tất cả\"><i class=\"material-icons\">sync</i></a>";
                                html.Append("<a href=\"javascript:;\" onclick=\"deleteordershoptemp('" + shop.ID + "')\" class=\"btn-delete tooltipped\" data-position=\"top\" data-tooltip=\"Xóa toàn bộ sản phẩm của cửa hàng này\"><i class=\"material-icons\">delete</i></a>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("<div class=\"order-main\">");

                                List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, shop.ID);
                                if (ors != null)
                                {
                                    if (ors.Count > 0)
                                    {
                                        foreach (var item in ors)
                                        {
                                            totalproduct += 1;
                                            int ID = item.ID;
                                            string linkproduct = item.link_origin;
                                            string productname = item.title_origin;
                                            string brand = item.brand;
                                            string image = item.image_origin;
                                            int quantity = Convert.ToInt32(item.quantity);
                                            double originprice = Math.Round(Convert.ToDouble(item.price_origin), 2);
                                            double promotionprice = Math.Round(Convert.ToDouble(item.price_promotion), 2);
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
                                                    u_pricevn = promotionprice * pricecurrency;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * pricecurrency;
                                                }
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                                u_pricevn = originprice * pricecurrency;
                                            }
                                            e_pricebuy = u_pricecbuy * quantity;
                                            e_pricevn = u_pricevn * quantity;

                                            e_totalproduct = e_pricevn + e_pricetemp;

                                            TotalPriceProductInShop += Math.Round(e_pricevn, 0);
                                            TotalPriceShop += Math.Round(e_totalproduct, 0);

                                            //pricefullcart += e_totalproduct;

                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }

                                            html.Append("<div class=\"item-wrap\">");
                                            html.Append("<div class=\"item-name\">");
                                            html.Append("<div class=\"number\">");
                                            html.Append("<span class=\"count\">" + i + "</span>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"name\">");
                                            html.Append("<span class=\"item-img\">");
                                            html.Append("<img src=\"" + image + "\" alt=\"image\">");
                                            html.Append("</span>");
                                            html.Append("<div class=\"caption\">");
                                            html.Append("<a href=\"" + linkproduct + "\" target=\"_blank\" class=\"title black-text\">" + productname + "</a>");
                                            html.Append("<div class=\"item-price mt-1\">");
                                            html.Append("<span class=\"pr-2 black-text font-weight-600\">" + item.property + "</span>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"note\">");
                                            html.Append("<span class=\"black-text font-weight-500\">Ghi chú: </span>");
                                            html.Append("<div class=\"input-field inline\">");
                                            html.Append("<input type=\"text\" id=\"note_" + ID + "\" value=\"" + brand + "\" data-item-id=\"" + ID + "\" class=\"validate notebrand\">");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"item-info\">");
                                            html.Append("<div class=\"separator\"></div>");
                                            html.Append("<div class=\"item-num column\">");
                                            html.Append("<span class=\"black-text\"><strong>Số lượng</strong></span>");
                                            html.Append("<div class=\"up-downControl form-style form-control\" data-step=\"1\" data-min=\"0\">");
                                            html.Append("<a href=\"javascript:;\" class=\"btn minus\">-</a>");
                                            html.Append("<div class=\"value-group fcontrol\">");
                                            html.Append("<input type=\"text\"  placeholder=\"0\" value=\"" + quantity + "\" min=\"1\" class=\"value quantitiofpro\">");
                                            html.Append("</div>");
                                            html.Append("<a href=\"javascript:;\" class=\"btn plus\">+</a>");
                                            html.Append("</div>");
                                            html.Append("<p></p>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"separator\"></div>");
                                            html.Append("<div class=\"item-price column\">");
                                            html.Append("<span class=\"black-text\"><strong>Đơn giá</strong></span>");
                                            html.Append("<p class=\"red-text\">¥" + u_pricecbuy + "</p>");
                                            html.Append("<p class=\"red-text\">" + string.Format("{0:N0}", u_pricevn) + " VNĐ</p>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"separator\"></div>");
                                            html.Append("<div class=\"item-price column\">");
                                            html.Append("<span class=\"black-text\"><strong>Tiền hàng</strong></span>");
                                            html.Append("<p class=\"red-text\">¥" + e_pricebuy + "</p>");
                                            html.Append("<p class=\"red-text\">" + string.Format("{0:N0}", e_pricevn) + " VNĐ</p>");
                                            html.Append("</div>");
                                            html.Append("<div class=\"separator\"></div>");
                                            html.Append("<div class=\"delete\">");
                                            html.Append("<a href=\"javascript:;\" onclick=\"updatequantity('" + ID + "',$(this),'" + shop.ID + "')\" class=\"btn-update tooltipped\" data-position=\"top\" data-tooltip=\"Cập nhật sản phẩm này\"><i class=\"material-icons\">sync</i></a>");
                                            html.Append("<a href=\"javascript:;\" onclick=\"deleteordertemp('" + ID + "','" + shop.ID + "')\" class=\"btn-delete tooltipped\" data-position=\"top\" data-tooltip=\"Xóa sản phẩm này\"><i class=\"material-icons \">delete</i></a>");
                                            html.Append("</div>");
                                            html.Append("</div>");
                                            html.Append("</div>");

                                            i++;
                                        }
                                    }
                                }
                                hdfallorderid.Value = listorderid;


                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("<div class=\"right-info\">");
                                html.Append("<div class=\"order-hd\">");
                                html.Append("<div class=\"shop-cart\">");
                                html.Append("<span class=\"shop-name\">ĐẶT HÀNG</span>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("<div class=\"order-main\">");
                                html.Append("<div class=\"order-note input-field\">");
                                if (!string.IsNullOrEmpty(shop.Note))
                                {
                                    html.Append("<textarea id=\"order_temp_" + shop.ID + "\" class=\"materialize-textarea\">" + shop.Note + "</textarea>");
                                }
                                else
                                {
                                    html.Append("<textarea id=\"order_temp_" + shop.ID + "\" class=\"materialize-textarea\"></textarea>");
                                }
                                html.Append("<label for=\"textarea2\">Ghi chú đơn hàng</label>");
                                html.Append("</div>");
                                html.Append("<div>");
                                html.Append("<div class=\"total-all\">");
                                html.Append("<strong class=\"black-text left\">Tiền hàng:</strong>");
                                html.Append("<strong class=\"price-normal red-text\">" + string.Format("{0:N0}", TotalPriceProductInShop) + " VNĐ</strong>");
                                html.Append("</div>");
                                html.Append("<div class=\"total-all\">");
                                html.Append("<strong class=\"black-text left\">Tổng tính:</strong>");
                                html.Append("<strong class=\"price-normal red-text\" id=\"priceVND_" + shop.ID + "\" data-price=\"" + TotalPriceShop + "\">" + string.Format("{0:N0}", TotalPriceShop) + " VNĐ</strong>");
                                html.Append("</div>");

                                html.Append("<div class=\"checkout\">");
                                html.Append("<a class=\"btn\"onclick=\"checkout('" + shop.ID + "')\" href=\"javascript:;\">Đặt hàng</a>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("</div>");
                                html.Append("<div class=\"select-option\">");
                                html.Append("<label>");
                                if (shop.IsFastDelivery == true)
                                {
                                    html.Append("<input type=\"checkbox\" class=\"\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFastDelivery')\"  checked=\"checked\" />");
                                }
                                else
                                {
                                    html.Append("<input type=\"checkbox\" class=\"\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsFastDelivery')\" />");
                                }
                                html.Append("<span>Giao tận nhà</span>");
                                html.Append("</label>");
                                html.Append("<label>");
                                if (shop.IsCheckProduct == true)
                                {
                                    html.Append("<input type=\"checkbox\" id=\"" + shop.ID + "_checkproductselect\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsCheckProduct')\" class=\"\" checked=\"checked\" />");
                                }
                                else
                                {
                                    html.Append("<input type=\"checkbox\" id=\"" + shop.ID + "_checkproductselect\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsCheckProduct')\" class=\"\" />");
                                }
                                html.Append("<span>Kiểm hàng</span>");
                                html.Append("</label>");
                                html.Append("<label>");
                                if (shop.IsPacked == true)
                                {
                                    html.Append("<input type=\"checkbox\" class=\"\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsPacked')\" checked=\"checked\" />");
                                }
                                else
                                {
                                    html.Append("<input type=\"checkbox\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsPacked')\" class=\"\" />");
                                }
                                html.Append("<span>Đóng gỗ</span>");
                                html.Append("</label>");

                                html.Append("<label>");
                                if (shop.IsInsurrance == true)
                                {
                                    html.Append("<input type=\"checkbox\" class=\"\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsInsurrance')\" checked=\"checked\" />");
                                }
                                else
                                {
                                    html.Append("<input type=\"checkbox\" onclick=\"updatecheck($(this),'" + shop.ID + "','IsInsurrance')\" class=\"\" />");
                                }
                                html.Append("<span>Bảo hiểm</span>");
                                html.Append("</label>");

                                html.Append("</div>");
                                html.Append("</div>");

                                #endregion

                                pricefullcart += TotalPriceShop;
                            }
                            ltr_cart.Text = html.ToString();
                            ltr_sub.Text = "<p><span class=\"teal-text text-darken-4 font-weight-700\">" + countshop + "</span> Shop / <span class=\"teal-text text-darken-4 font-weight-700\">" + totalproduct + "</span> Sản phẩm / <span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", pricefullcart) + " VNĐ</span> Tiền hàng</p>";

                            //ltr_sub.Text = " <div class=\"left\"><span class=\"hl-txt\">" + countshop + "</span> Shop  /  <span class=\"hl-txt\">" + totalproduct + "</span> Sản phẩm  /  <span class=\"hl-txt\">" + string.Format("{0:N0}", pricefullcart) + " vnđ</span> Tiền Hàng</div>";
                            //ltr_total_pay.Text += "<div class=\"table-price-total\">";
                            //ltr_total_pay.Text += " <div class=\"right\">";
                            //ltr_total_pay.Text += "     <p class=\"final-total\">Tổng tính <strong class=\"hl-txt\">" + string.Format("{0:N0}", pricefullcart) + "</strong><span class=\"hl-txt\">vnđ</span></p>";
                            //   ltr_total_pay.Text += "     <a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover\" onclick=\"checkoutAll('" + listorderid + "')\">ĐẶT HÀNG TẤT CẢ ĐƠN HÀNG</a>";
                            //ltr_total_pay.Text += "     <a href=\"javascript:;\" class=\"pill-btn btn order-btn getallOrder\" onclick=\"checkoutAllSelect()\">ĐẶT HÀNG <span class=\"numofOrder\">" + totalproduct + "</span> ĐƠN HÀNG ĐÃ CHỌN</a>";
                            //ltr_total_pay.Text += " </div>";
                            //ltr_total_pay.Text += "</div>";

                            ltr_total_pay.Text += "<div class=\"total-wrap right-align\">";
                            ltr_total_pay.Text += " <div class=\"total-all\" style=\"display:none\">";
                            ltr_total_pay.Text += "<strong class=\"black-text\">Tổng tiền các đơn đã chọn:</strong>";
                            ltr_total_pay.Text += "<strong class=\"price-all red-text MoneyofOrder\">0 VNĐ</strong>";
                            ltr_total_pay.Text += " </div>";
                            ltr_total_pay.Text += "<div class=\"total-all\">";
                            ltr_total_pay.Text += "<strong class=\"black-text\">Tổng tiền tất cả đơn hàng:</strong>";
                            ltr_total_pay.Text += "<strong class=\"price-all red-text\">" + string.Format("{0:N0}", pricefullcart) + " VNĐ</strong>";
                            ltr_total_pay.Text += " </div>";
                            ltr_total_pay.Text += "<div class=\"checkout-all\">";
                            ltr_total_pay.Text += " <a href=\"javascript:;\"  style=\"display:none\" class=\"btn checkout-select mr-1 getallOrder\" onclick=\"checkoutAllSelect()\" >Đặt hàng đơn đã chọn (<span class=\"numofOrder\">" + totalproduct + "</span>)</a>";
                            ltr_total_pay.Text += "<a href=\"javascript:;\" class=\"btn checkout-all \" onclick=\"checkoutAll('" + listorderid + "')\">Đặt hàng tất cả đơn</a>";
                            ltr_total_pay.Text += "</div>";
                            ltr_total_pay.Text += " </div>";

                        }
                        else
                        {
                            ltrNotProduct.Text = "<p class=\"center-align red-text\">Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.</p>";
                        }
                    }
                    else
                    {
                        ltrNotProduct.Text = "<p class=\"center-align red-text\">Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.</p>";
                    }
                }
            }
        }




        #region Webservice
        [WebMethod]
        public static string deleteOrderShopTemp(string ID)
        {
            string kq = OrderShopTempController.Delete(Convert.ToInt32(ID));
            return kq;
        }
        [WebMethod]
        public static string deleteOrderTemp(string ID, string shopID)
        {
            string kq = "0";
            var ordertemp = OrderTempController.GetByID(Convert.ToInt32(ID));
            if (ordertemp != null)
            {
                string pricestep = ordertemp.stepprice;
                int UID = Convert.ToInt32(ordertemp.UID);
                string itemid = ordertemp.item_id;
                kq = OrderTempController.Delete(Convert.ToInt32(ID));
                OrderTempController.UpdatePriceInsert(UID, pricestep, itemid);
                int IDS = Convert.ToInt32(shopID);
                var ordert = OrderTempController.GetAllByOrderShopTempID(IDS);
                if (ordert.Count == 0)
                {
                    OrderShopTempController.Delete(IDS);
                }
            }
            return kq;
        }
        [WebMethod]
        public static string UpdateQuantityOrderTemp(string ID, int quantity, string brand)
        {
            OrderTempController.UpdateBrand(Convert.ToInt32(ID), brand);
            string kq = OrderTempController.UpdateQuantity(Convert.ToInt32(ID), quantity);
            return kq;
        }
        [WebMethod]
        public static string UpdateNoteFastPriceVND(int ID, string note, string priceVND)
        {
            string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, priceVND);
            if (kq == "1")
                return kq;
            else
                return "fail";
        }
        [WebMethod]
        public static string UpdateField(int ID, bool chk, string field)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            string UID = OrderShopTempController.SelectUIDByIDOrder(ID);
            if (UID != null)
            {
                //if (UID == obj_user.ID.ToString())
                //{
                if (field == "IsCheckProduct")
                {
                    if (chk == true)
                    {
                        //Lấy ra danh sách sản phẩm để cộng tiền rồi update lại phí kiểm tra hàng hóa
                        var os = OrderShopTempController.GetByUIDAndID(UID.ToInt(), ID);
                        if (os != null)
                        {
                            double total = 0;
                            var listpro = OrderTempController.GetAllByOrderShopTempID(os.ID);
                            double counpros_more10 = 0;
                            double counpros_les10 = 0;
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
                            #region Fee Check New
                            //double counpros = 0;
                            //if (listpro.Count > 0)
                            //{
                            //    foreach (var item in listpro)
                            //    {
                            //        counpros += item.quantity.ToInt(1);
                            //    }
                            //}

                            //var conf_Feecheckproduct = FeeCheckProductController.GetAll();
                            //if (conf_Feecheckproduct.Count > 0)
                            //{
                            //    foreach (var item in conf_Feecheckproduct)
                            //    {
                            //        if (counpros > item.AmountFrom && counpros <= item.AmountTo)
                            //        {
                            //            total = counpros * Convert.ToDouble(item.Price);
                            //        }
                            //    }
                            //}
                            #endregion

                            total = Math.Round(total, 0);
                            OrderShopTempController.UpdateCheckProductPrice(ID, total.ToString());
                        }
                    }
                    else
                    {
                        //Update lại phí kiểm tra hàng hóa là 0
                        OrderShopTempController.UpdateCheckProductPrice(ID, "0");
                    }
                }
                string kq = OrderShopTempController.Update4Field(ID, chk, field);
                return kq;
                //}
                //else
                //{
                //    return "wronguser";
                //}
            }
            return "null";
        }
        #endregion

        protected void checkoutallorder_Click(object sender, EventArgs e)
        {
            string lo = hdforderlistall.Value;
            string brandtext = hdfProductBrand.Value;
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(',');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }
            Guid temp = Guid.NewGuid();
            List<string> orderId = hdfListOrderTempID.Value.Split('|').Where(n => !string.IsNullOrEmpty(n)).ToList();

            if (!string.IsNullOrEmpty(lo))
            {
                string[] orders = lo.Split('|');
                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(',');
                    int ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }
                Session["PayAllTempOrder"] = orderId;
                Response.Redirect("/manager/Verification-Cart");
            }
        }

        protected void checkout1order_Click(object sender, EventArgs e)
        {
            string lo = hdforderlistall.Value;
            string brandtext = hdfProductBrand.Value;
            if (!string.IsNullOrEmpty(brandtext))
            {
                string[] bps = brandtext.Split('|');
                for (int i = 0; i < bps.Length - 1; i++)
                {
                    string bt = bps[i];
                    string[] item = bt.Split(',');
                    int IDpro = item[0].ToInt(0);
                    string bra = item[1];
                    OrderTempController.UpdateBrand(IDpro, bra);
                }
            }
            if (!string.IsNullOrEmpty(lo))
            {
                int ID = 0;
                string[] orders = lo.Split('|');

                for (int i = 0; i < orders.Length - 1; i++)
                {
                    string order = orders[i];
                    string[] items = order.Split(',');
                    ID = items[0].ToInt(0);
                    string note = items[1];
                    string pricevnd = items[2];
                    string kq = OrderShopTempController.UpdateNoteFastPriceVND(ID, note, pricevnd);
                }

                List<string> orderId = new List<string>();
                orderId.Add(ID.ToString());
                Session["PayAllTempOrder"] = orderId;
                Response.Redirect("/manager/Verification-Cart/" + ID);

            }
        }
    }
}
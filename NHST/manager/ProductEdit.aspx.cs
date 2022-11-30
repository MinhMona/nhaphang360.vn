using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0 && ac.RoleID != 3)
                    //    Response.Redirect("/trang-chu");
                    Loaddata();
                }

            }

        }

        public void Loaddata()
        {
            int MainOrderID = 0;
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        hdfcurrent.Value = mainorder.CurrentCNYVN.ToString();
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 3 && ac.RoleID != 2)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }
                    //else
                    //{
                    //    if (ac.RoleID == 3)
                    //    {
                    //        if (mainorder.Status >= 5)
                    //            btncreateuser.Visible = false;
                    //        pProductPriceOriginal.Enabled = false;
                    //        pRealPrice.Enabled = true;
                    //    }
                    //}
                    txtTitleOrigin.Text = o.title_origin;
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (o.price_promotion.ToFloat(0) > 0)
                        pricepromotion = Convert.ToDouble(o.price_promotion);
                    if (o.price_origin.ToFloat(0) > 0)
                        priceorigin = Convert.ToDouble(o.price_origin);

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
                    ViewState["productprice"] = price;
                    pProductPriceOriginal.Text = price.ToString();
                    if (o.quantity.ToFloat(0) > 0)
                        pQuanity.Text = Convert.ToDouble(o.quantity).ToString();
                    else pQuanity.Text = "0";
                    pRealPrice.Text = Convert.ToDouble(o.RealPrice).ToString();
                    txtproducbrand.Text = o.brand;
                    string urlName = Request.UrlReferrer.ToString();
                    ltrback.Text = "<a href=\"" + urlName + "\" class=\"btn primary-btn\">Trở về</a>";
                    if (!string.IsNullOrEmpty(o.ProductStatus.ToString()))
                        ddlStatus.SelectedValue = o.ProductStatus.ToString();
                    else
                        ddlStatus.SelectedValue = "1";
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);

            //Update lại giá sản phẩm
            var id = Request.QueryString["id"].ToInt(0);
            int MainOrderID = 0;
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    MainOrderID = Convert.ToInt32(o.MainOrderID);
                    double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (o.price_promotion.ToFloat(0) > 0)
                        pricepromotion = Math.Round(Convert.ToDouble(o.price_promotion), 2);

                    if (o.price_origin.ToFloat(0) > 0)
                        priceorigin = Math.Round(Convert.ToDouble(o.price_origin), 2);

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
                    price = Math.Round(price, 2);                   

                    double quantity = 0;
                    if (status == 2)
                    {
                        price = 0;
                        quantity = 0;
                        var od = MainOrderController.GetAllByID(MainOrderID);
                        if (od != null)
                        {
                            var setNoti = SendNotiEmailController.GetByID(19);
                            if (setNoti != null)
                            {
                                if (setNoti.IsSentNotiUser == true)
                                {
                                    int userdathangID = Convert.ToInt32(od.UID);
                                    var userdathang = AccountController.GetByID(userdathangID);
                                    if (userdathang != null)
                                    {
                                        NotificationsController.Inser(userdathang.ID, userdathang.Username, MainOrderID,
                                                               "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.",
                                                               1, DateTime.Now, obj_user.Username, true);
                                    }
                                }

                                if (setNoti.IsSendEmailUser == true)
                                {
                                    try
                                    {
                                        PJUtils.SendMailGmail_new(AccountController.GetByID(Convert.ToInt32(od.UID)).Email,
                                            "Thông báo tại NHẬP HÀNG 360", "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.", "");
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                        if (price.ToString() != pProductPriceOriginal.Text.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Text)) + "", 1, currentDate);
                        }
                        if (o.quantity != pQuanity.Text.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Text)) + "", 1, currentDate);
                        }
                        OrderController.UpdateQuantity(id, quantity.ToString());
                        OrderController.UpdateProductStatus(id, status);
                        OrderController.UpdatePricePriceReal(id, "0", "0");
                        OrderController.UpdatePricePromotion(id, "0");
                    }
                    else
                    {
                        quantity = Convert.ToDouble(pQuanity.Text);
                        if (price.ToString() != pProductPriceOriginal.Text.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Text)) + "", 1, currentDate);
                        }
                        if (o.quantity != pQuanity.Text.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Text)) + "", 1, currentDate);
                        }
                        OrderController.UpdateQuantity(id, quantity.ToString());
                        OrderController.UpdateProductStatus(id, status);
                        OrderController.UpdatePricePriceReal(id, Math.Round(Convert.ToDouble(pProductPriceOriginal.Text), 2).ToString(), Math.Round(Convert.ToDouble(pRealPrice.Text), 2).ToString());
                        OrderController.UpdatePricePromotion(id, Math.Round(Convert.ToDouble(pProductPriceOriginal.Text), 2).ToString());
                    }
                    OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());
                }
            }

            //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí
            var listorder = OrderController.GetByMainOrderID(MainOrderID);
            var mainorder = MainOrderController.GetAllByID(MainOrderID);
            if (mainorder != null)
            {
                double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                double InsurancePercent = Convert.ToDouble(mainorder.InsurancePercent);
                if (listorder != null)
                {
                    if (listorder.Count > 0)
                    {
                        double pricevnd = 0;
                        double pricecyn = 0;

                        foreach (var item in listorder)
                        {
                            double originprice = Math.Round(Convert.ToDouble(item.price_origin), 2);
                            double promotionprice = Math.Round(Convert.ToDouble(item.price_promotion), 2);
                            double oprice = 0;
                            if (promotionprice > 0)
                            {
                                if (promotionprice < originprice)
                                {
                                    pricecyn += promotionprice * Convert.ToDouble(item.quantity);
                                    oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                }
                                else
                                {
                                    pricecyn += originprice * Convert.ToDouble(item.quantity);
                                    oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                }
                            }
                            else
                            {
                                pricecyn += originprice * Convert.ToDouble(item.quantity);
                                oprice = originprice * Convert.ToDouble(item.quantity) * current;
                            }
                            pricevnd += oprice;
                        }

                        pricevnd = Math.Round(pricevnd, 0);
                        pricecyn = Math.Round(pricecyn, 2);

                        MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                        MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());

                        double InsuranceMoney = 0;
                        if (mainorder.IsInsurrance == true)
                            InsuranceMoney = Math.Round(pricevnd * (InsurancePercent / 100), 0);
                        else
                            InsuranceMoney = Math.Round(Convert.ToDouble(mainorder.InsuranceMoney), 0);

                        double IsCheckProductPrice = 0;
                        if (mainorder.IsCheckProduct == true)
                        {
                            double total = 0;
                            double counpros_more10 = 0;
                            double counpros_les10 = 0;
                            if (listorder.Count > 0)
                            {
                                foreach (var item in listorder)
                                {
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
                            IsCheckProductPrice = Math.Round(total, 0);
                        }

                        double IsPackedPrice = 0;
                        if(mainorder.IsPacked == true)
                            IsPackedPrice = Math.Round(Convert.ToDouble(mainorder.IsPackedPrice), 0);

                        double IsFastDeliveryPrice = 0;
                        if (mainorder.IsFastDelivery == true)
                            IsFastDeliveryPrice = Math.Round(Convert.ToDouble(mainorder.IsFastDeliveryPrice), 0);

                        double TotalFeeSupport = 0;
                        TotalFeeSupport = Math.Round(Convert.ToDouble(mainorder.TotalFeeSupport), 0);

                        var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                        double UL_CKFeeBuyPro = 0;
                        double UL_CKFeeWeight = 0;
                        double LessDeposito = 0;
                        if (ui != null)
                        {
                            UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                            UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                            LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                        }
                       
                        double servicefee = 0;
                        double feebpnotdc = 0;
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item in adminfeebuypro)
                            {
                                if (pricevnd >= item.AmountFrom && pricevnd < item.AmountTo)
                                {
                                    double feepercent = 0;
                                    if (item.FeePercent.ToString().ToFloat(0) > 0)
                                        feepercent = Convert.ToDouble(item.FeePercent);
                                    servicefee = feepercent / 100;
                                    break;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                        {
                            if (ui.FeeBuyPro.ToFloat(0) > 0)
                            {
                                feebpnotdc = pricevnd * Convert.ToDouble(ui.FeeBuyPro) / 100;
                            }
                            else
                                feebpnotdc = pricevnd * servicefee;
                        }
                        else
                            feebpnotdc = pricevnd * servicefee;

                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double feebp = feebpnotdc - subfeebp;
                        feebp = Math.Round(feebp, 0);
                        if (feebp < 10000)
                            feebp = 10000;

                        double NewDeposit = 0;
                        double FeeBuyPro = Math.Round(Convert.ToDouble(feebp), 0);
                        double Deposit = Math.Round(Convert.ToDouble(mainorder.Deposit), 0);
                        double FeeShipCN = Math.Round(Convert.ToDouble(mainorder.FeeShipCN), 0);                        
                        double FeeWeight = Math.Round(Convert.ToDouble(mainorder.FeeWeight), 0);

                        double TotalPriceVND = pricevnd + FeeBuyPro + FeeShipCN + FeeWeight + InsuranceMoney + TotalFeeSupport + IsFastDeliveryPrice + IsPackedPrice + IsCheckProductPrice;
                        TotalPriceVND = Math.Round(TotalPriceVND, 0);

                        double AmountDeposit = Math.Round((TotalPriceVND * LessDeposito) / 100, 0);

                        if (Deposit > 0)
                        {
                            if (Deposit > TotalPriceVND)
                            {
                                double drefund = Math.Round(Deposit - TotalPriceVND, 0);

                                double userwallet = 0;
                                if (ui.Wallet.ToString() != null)
                                    userwallet = Math.Round(Convert.ToDouble(ui.Wallet.ToString()), 0);

                                double wallet = Math.Round(userwallet + drefund, 0);

                                AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                                PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                                if (status == 2)
                                    HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                                else
                                    HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);

                                var setNoti = SendNotiEmailController.GetByID(19);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiUser == true)
                                    {
                                        NotificationsController.Inser(Convert.ToInt32(mainorder.UID),
                                        AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, mainorder.ID,
                                        "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", 1, currentDate, obj_user.Username, true);
                                    }
                                }
                                NewDeposit = TotalPriceVND;
                            }
                            else
                            {
                                NewDeposit = Deposit;
                            }
                        }
                        else
                        {
                            NewDeposit = 0;
                        }
                        MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString());
                        MainOrderController.UpdateFee(MainOrderID, NewDeposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                                       IsCheckProductPrice.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                        MainOrderController.UpdateInsurranceMoney(MainOrderID, InsuranceMoney.ToString(), InsurancePercent.ToString());
                    }
                }
            }
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/manager/OrderDetail.aspx?id=" + MainOrderID, Page);
        }
    }
}
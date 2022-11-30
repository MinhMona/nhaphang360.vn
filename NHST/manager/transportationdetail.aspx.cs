using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
using System.Web.Script.Serialization;

namespace NHST.manager
{
    public partial class transportationdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "xuemei912";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                    else
                    {
                        if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                        {
                            Response.Redirect("/admin/home.aspx");
                        }
                    }
                }
                //UpdateWeight();
                LoadDDL();
                loaddata();
            }
        }
        public void LoadDDL()
        {

            var warehousefrom = WarehouseFromController.GetAllWithIsHidden(false);
            if (warehousefrom.Count > 0)
            {
                foreach (var item in warehousefrom)
                {
                    ListItem a = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlOrderWareHouseFrom.Items.Add(a);
                }
                ddlOrderWareHouseFrom.DataBind();
            }


            var warehouseto = WarehouseController.GetAllWithIsHidden(false);
            if (warehouseto.Count > 0)
            {
                foreach (var item in warehouseto)
                {
                    ListItem a = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlOrderWareHouseTo.Items.Add(a);
                }
                ddlOrderWareHouseTo.DataBind();
            }

            var shippingtype = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shippingtype.Count > 0)
            {
                foreach (var item in shippingtype)
                {
                    ListItem a = new ListItem(item.ShippingTypeName, item.ID.ToString());
                    ddlOrderShippingType.Items.Add(a);
                }

                ddlOrderShippingType.DataBind();
            }
        }
        public void UpdateWeight()
        {
            var config = ConfigurationController.GetByTop1();
            double currency = 0;
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                ViewState["ID"] = id;
                var t = TransportationOrderController.GetByID(id);
                if (t != null)
                {
                    int tID = t.ID;
                    #region lấy dữ liệu
                    int status = Convert.ToInt32(t.Status);
                    int warehouseFrom = Convert.ToInt32(t.WarehouseFromID);
                    int warehouseTo = Convert.ToInt32(t.WarehouseID);
                    int shippingType = Convert.ToInt32(t.ShippingTypeID);
                    double currency_addOrder = Convert.ToDouble(t.Currency);
                    double totalPriceVND = Math.Round(Convert.ToDouble(t.TotalPrice), 0);
                    double totalPriceCYN = 0;
                    if (totalPriceVND > 0)
                        totalPriceCYN = Math.Round(totalPriceVND / currency_addOrder, 2);

                    //lblUsername.Text = t.Username;
                    double deposited = Math.Round(Convert.ToDouble(t.Deposited), 0);
                    double totalPackage = 0;
                    double totalWeight = 0;
                    StringBuilder htmlPackages = new StringBuilder();
                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                    if (smallpackages.Count > 0)
                    {
                        totalPackage = smallpackages.Count;
                        foreach (var p in smallpackages)
                        {
                            double weight = Math.Round(Convert.ToDouble(p.Weight), 1);
                            totalWeight += weight;
                        }
                    }
                    else
                    {
                        var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                        if (transportationDetail.Count > 0)
                        {
                            totalPackage = transportationDetail.Count;
                            foreach (var p in transportationDetail)
                            {
                                double weight = Math.Round(Convert.ToDouble(p.Weight), 1);
                                totalWeight += weight;
                            }
                        }
                    }
                    #endregion
                    double pricePerWeight = 0;
                    double totalPrice = 0;
                    var tf = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom,
                                warehouseTo, shippingType, true);

                    if (tf.Count > 0)
                    {
                        foreach (var w in tf)
                        {
                            if (w.WeightFrom < totalWeight && totalWeight <= w.WeightTo)
                            {
                                pricePerWeight = Convert.ToDouble(w.Price);
                            }
                        }
                    }
                    //totalPrice += Math.Round(pricePerWeight * totalWeight, 0);
                    double totalCodeVND = 0;
                    double checkProductFee = 0;
                    double packagedFee = 0;
                    double insurranceFee = 0;
                    if (t.TotalCODTQVND != null)
                    {
                        if (t.TotalCODTQVND > 0)
                            totalCodeVND = Math.Round(Convert.ToDouble(t.TotalCODTQVND), 0);
                    }
                    if (t.CheckProductFee != null)
                    {
                        if (t.CheckProductFee > 0)
                            checkProductFee = Math.Round(Convert.ToDouble(t.CheckProductFee), 0);
                    }
                    if (t.PackagedFee != null)
                    {
                        if (t.PackagedFee > 0)
                            packagedFee = Math.Round(Convert.ToDouble(t.PackagedFee), 0);
                    }
                    if (t.InsurranceFee != null)
                    {
                        if (t.InsurranceFee > 0)
                            insurranceFee = Math.Round(Convert.ToDouble(t.InsurranceFee), 0);
                    }
                    totalPrice = Math.Round(pricePerWeight * totalWeight, 0) + totalCodeVND +
                                checkProductFee + packagedFee + insurranceFee;
                    TransportationOrderController.UpdateWeightTotalPrice(t.ID, totalWeight, totalPrice);
                }
            }
        }
        public void loaddata()
        {
            var config = ConfigurationController.GetByTop1();
            double currency = 0;
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                ViewState["ID"] = id;
                var t = TransportationOrderController.GetByID(id);
                if (t != null)
                {
                    int tID = t.ID;
                    #region lấy dữ liệu
                    int status = Convert.ToInt32(t.Status);
                    int warehouseFrom = Convert.ToInt32(t.WarehouseFromID);
                    int warehouseTo = Convert.ToInt32(t.WarehouseID);
                    int shippingType = Convert.ToInt32(t.ShippingTypeID);
                    double currency_addOrder = Convert.ToDouble(t.Currency);
                    double checkProductFee = 0;
                    if (t.CheckProductFee != null)
                    {
                        checkProductFee = Math.Round(Convert.ToDouble(t.CheckProductFee), 0);
                    }

                    double packagedFee = 0;
                    if (t.PackagedFee != null)
                    {
                        packagedFee = Math.Round(Convert.ToDouble(t.PackagedFee), 0);
                    }

                    double insurranceFee = 0;
                    if (t.InsurranceFee != null)
                    {
                        insurranceFee = Math.Round(Convert.ToDouble(t.InsurranceFee), 0);
                    }

                    double totalCodeTQCYN = 0;
                    if (t.TotalCODTQCYN != null)
                    {
                        totalCodeTQCYN = Math.Round(Convert.ToDouble(t.TotalCODTQCYN), 0);
                    }

                    double totalCODTQVND = 0;
                    if (t.TotalCODTQVND != null)
                    {
                        totalCODTQVND = Math.Round(Convert.ToDouble(t.TotalCODTQVND), 0);
                    }

                    double totalPriceVND = Math.Round(Convert.ToDouble(t.TotalPrice), 0);
                    double totalPriceCYN = 0;
                    if (totalPriceVND > 0)
                        totalPriceCYN = Math.Round(totalPriceVND / currency_addOrder, 2);
                    ltrOrderID.Text = id.ToString();
                    double deposited = Math.Round(Convert.ToDouble(t.Deposited), 0);
                    double totalPackage = 0;
                    double totalWeight = 0;
                    StringBuilder htmlPackages = new StringBuilder();
                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                    if (smallpackages.Count > 0)
                    {
                        totalPackage = smallpackages.Count;
                        foreach (var p in smallpackages)
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
                            htmlPackages.Append("<tr class=\"package-item\" data-code=\"" + p.OrderTransactionCode + "\" data-weight=\"" + weight + "\" data-package-id=\"" + p.ID + "\">");
                            htmlPackages.Append("   <td>" + p.ID + "</td>");
                            htmlPackages.Append("   <td>" + p.OrderTransactionCode + "</td>");
                            htmlPackages.Append("   <td>" + p.ProductType + "</td>");
                            htmlPackages.Append("   <td>" + p.ProductQuantity + "</td>");
                            htmlPackages.Append("   <td>" + weight + " kg</td>");
                            bool isCheckProduct = false;
                            bool isPackaged = false;
                            bool isInsurrance = false;
                            if (p.IsCheckProduct != null)
                            {
                                isCheckProduct = Convert.ToBoolean(p.IsCheckProduct);
                            }
                            if (p.IsPackaged != null)
                            {
                                isPackaged = Convert.ToBoolean(p.IsPackaged);
                            }
                            if (p.IsInsurrance != null)
                            {
                                isInsurrance = Convert.ToBoolean(p.IsInsurrance);
                            }
                            if (isCheckProduct == true)
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" checked disabled></td>");
                            }
                            else
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" disabled></td>");
                            }
                            if (isPackaged == true)
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" checked disabled></td>");
                            }
                            else
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" disabled></td>");
                            }
                            if (isInsurrance == true)
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" checked disabled></td>");
                            }
                            else
                            {
                                htmlPackages.Append("   <td><input type=\"checkbox\" disabled></td>");
                            }
                            htmlPackages.Append("   <td>¥" + p.CODTQCYN + " - " + string.Format("{0:N0}", Convert.ToDouble(p.CODTQVND)) + " VNĐ</td>");
                            htmlPackages.Append("   <td>" + p.UserNote + "</td>");
                            if (status == 1)
                                htmlPackages.Append("   <td><span class=\"badge red darken-2 white-text border-radius-2\">Chưa duyệt</span></td>");
                            else
                                htmlPackages.Append("   <td>" + PJUtils.IntToStringStatusSmallPackageNew(Convert.ToInt32(p.Status)) + "</td>");
                            //htmlPackages.Append("   <td class=\"hl-txt\"><a href=\"javascript:;\" class=\"left edit-btn\">Cập nhật</a></td>");
                            htmlPackages.Append("</tr>");
                            totalWeight += weight;
                        }
                    }
                    else
                    {
                        var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                        if (transportationDetail.Count > 0)
                        {
                            totalPackage = transportationDetail.Count;
                            foreach (var p in transportationDetail)
                            {
                                double weight = Math.Round(Convert.ToDouble(p.Weight), 1);
                                htmlPackages.Append("<tr class=\"package-item\" data-code=\"" + p.TransportationOrderCode + "\" data-weight=\"" + weight + "\" data-package-id=\"" + p.ID + "\">");
                                htmlPackages.Append("   <td>" + p.ID + "</td>");
                                htmlPackages.Append("   <td><div class=\"product-infobox\">" + p.TransportationOrderCode + "</div></td>");
                                htmlPackages.Append("   <td>" + p.ProductType + "</td>");
                                htmlPackages.Append("   <td>" + p.ProductQuantity + "</td>");
                                htmlPackages.Append("   <td>" + weight + " kg</td>");
                                bool isCheckProduct = false;
                                bool isPackaged = false;
                                bool isInsurrance = false;
                                if (p.IsCheckProduct != null)
                                {
                                    isCheckProduct = Convert.ToBoolean(p.IsCheckProduct);
                                }
                                if (p.IsPackaged != null)
                                {
                                    isPackaged = Convert.ToBoolean(p.IsPackaged);
                                }
                                if (p.IsInsurrance != null)
                                {
                                    isInsurrance = Convert.ToBoolean(p.IsInsurrance);
                                }
                                if (isCheckProduct == true)
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" checked disabled /><span ></span ></label ></td>");
                                }
                                else
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" disabled /><span ></span ></label ></td>");
                                }
                                if (isPackaged == true)
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" checked disabled /><span ></span ></label ></td>");
                                }
                                else
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" disabled /><span ></span ></label ></td>");
                                }
                                if (isInsurrance == true)
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" checked disabled /><span ></span ></label ></td>");
                                }
                                else
                                {
                                    htmlPackages.Append("   <td><label><input type = \"checkbox\" disabled /><span ></span ></label ></td>");
                                }
                                htmlPackages.Append("   <td>¥" + p.CODTQCYN + " - " + string.Format("{0:N0}", Convert.ToDouble(p.CODTQVND)) + " VNĐ</td>");
                                htmlPackages.Append("   <td>" + p.UserNote + "</td>");
                                htmlPackages.Append("   <td><span class=\"badge red darken-2 white-text border-radius-2\">Chưa duyệt</span></td>");
                                //htmlPackages.Append("   <td class=\"hl-txt\"></td>");
                                htmlPackages.Append("</tr>");
                                totalWeight += weight;
                            }
                        }
                    }
                    ltrPackages.Text = htmlPackages.ToString();
                    #endregion
                    #region gán dữ liệu
                    hdfCurrency.Value = currency_addOrder.ToString();
                    hdfStatus.Value = status.ToString();
                    txtTotalPriceVND.Text = Math.Round(totalPriceVND, 0).ToString();
                    OrderTotalPrice.Text = string.Format("{0:N0} VND", Math.Round(totalPriceVND, 0));
                    txtTotalPriceCNY.Text = Math.Round(totalPriceCYN, 2).ToString();
                    OrderPaidPrice.Text = string.Format("{0:N0} VND", Math.Round(deposited, 0));
                    OrderRemainingPrice.Text = string.Format("{0:N0} VND", Math.Round(totalPriceVND - deposited, 0));
                    ddlOrderWareHouseFrom.SelectedValue = warehouseFrom.ToString();
                    ddlOrderWareHouseTo.SelectedValue = warehouseTo.ToString();
                    ddlOrderShippingType.SelectedValue = shippingType.ToString();
                    ddlOrderStatus.SelectedValue = status.ToString();
                    txtTotalPackage.Text = totalPackage.ToString();
                    txtTotalWeight.Text = Math.Round(totalWeight, 2).ToString();
                    txtFeeCheckPackage.Text = checkProductFee.ToString();
                    txtFeePack.Text = packagedFee.ToString();
                    txtFeeInsurrance.Text = insurranceFee.ToString();
                    txtTotalCODCNY.Text = totalCodeTQCYN.ToString();
                    txtTotalCODVN.Text = totalCODTQVND.ToString();
                    //lblNote.Text = t.Description;
                    #endregion
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (ac != null)
            {
                if (ac.RoleID == 0)
                {
                    var id = Convert.ToInt32(ViewState["ID"]);
                    if (id > 0)
                    {
                        var t = TransportationOrderController.GetByID(id);
                        if (t != null)
                        {
                            int tID = t.ID;
                            double totalWeight = 0;
                            int warehouseFrom = ddlOrderWareHouseFrom.SelectedValue.ToInt();
                            int warehouseTo = ddlOrderWareHouseTo.SelectedValue.ToInt();
                            int shippingType = ddlOrderShippingType.SelectedValue.ToInt();
                            int status = ddlOrderStatus.SelectedValue.ToInt();
                            double currency = Convert.ToDouble(t.Currency);
                            int UID = Convert.ToInt32(t.UID);
                            string username = t.Username;
                            double price = 0;
                            bool isExist = false;
                            double totalprice = 0;
                            double totalWeightPrice = 0;


                            if (status == 0)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.Delete(item.ID);
                                    }
                                }
                                double deposited = Math.Round(Convert.ToDouble(t.Deposited), 0);
                                if (deposited > 0)
                                {
                                    var user_deposited = AccountController.GetByID(Convert.ToInt32(t.UID));
                                    if (user_deposited != null)
                                    {
                                        double wallet = Math.Round(Convert.ToDouble(user_deposited), 0);
                                        double walletleft = Math.Round(wallet + deposited, 0);
                                        AccountController.updateWallet(UID, walletleft, currentDate, username_current);
                                        HistoryPayWalletController.InsertTransportation(UID, username_current, 0, deposited,
                                        username_current + " nhận lại tiền của đơn hàng vận chuyển hộ: " + t.ID + ".",
                                        walletleft, 2, 11, currentDate, username_current, t.ID);
                                    }
                                }
                            }
                            else if (status == 1)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.Delete(item.ID);
                                    }
                                }
                            }
                            else if (status == 2)
                            {
                                var setNoti = SendNotiEmailController.GetByID(15);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiUser == true)
                                    {
                                        NotificationsController.Inser(UID,
                                                          t.Username, t.ID,
                                                          "Đơn hàng vận chuyển hộ " + t.ID + " đã được duyệt.",
                                                          10, currentDate, ac.Username, true);
                                        string strPathAndQuery = Request.Url.PathAndQuery;
                                        string strUrl = Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                                        string datalink = "" + strUrl + "danh-sach-don-van-chuyen-ho/";
                                        PJUtils.PushNotiDesktop(UID, "Đơn hàng vận chuyển hộ " + t.ID + " đã được duyệt.", datalink);
                                    }

                                    if (setNoti.IsSendEmailUser == true)
                                    {
                                        var acg = AccountController.GetByID(UID);
                                        if (acg != null)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail_new( acg.Email,
                                                    "Thông báo tại NHẬP HÀNG 360.", "Đơn hàng vận chuyển hộ " + t.ID + " đã được duyệt.", "");
                                            }
                                            catch { }
                                        }

                                    }
                                }
                            }
                            else if (status == 4)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 2, currentDate, username_current);
                                    }
                                }
                            }
                            else if (status == 5)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 3, currentDate, username_current);
                                    }
                                }
                            }
                            else if (status == 7)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 4, currentDate, username_current);
                                    }
                                }
                            }
                            var smallpackages1 = SmallPackageController.GetByTransportationOrderID(tID);
                            if (status > 1)
                            {
                                if (smallpackages1.Count == 0)
                                {
                                    var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                                    if (transportationDetail.Count > 0)
                                    {
                                        foreach (var p in transportationDetail)
                                        {
                                            SmallPackageController.InsertWithTransportationIDNew(t.ID, 0, p.TransportationOrderCode, p.ProductType,
                                                0, Convert.ToDouble(p.Weight), 0, Convert.ToBoolean(p.IsCheckProduct),
                                                Convert.ToBoolean(p.IsPackaged), Convert.ToBoolean(p.IsInsurrance),
                                                p.CODTQCYN.ToString(), p.CODTQVND.ToString(),
                                                p.UserNote, "", p.ProductQuantity.ToString(),
                                                1, currentDate, username_current, UID, username);
                                        }
                                    }
                                }
                            }
                            var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                            //var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var s in smallpackages)
                                {
                                    if (s.Status == 3)
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
                                        totalWeight += weight;
                                    }

                                }
                                var accOrder = AccountController.GetByID(t.UID.Value);
                                if (!string.IsNullOrEmpty(accOrder.FeeTQVNPerWeight))
                                {
                                    double feetqvn = 0;
                                    if (accOrder.FeeTQVNPerWeight.ToFloat(0) > 0)
                                    {
                                        feetqvn = Convert.ToDouble(accOrder.FeeTQVNPerWeight);
                                    }
                                    price = feetqvn;
                                    totalWeightPrice = price * totalWeight;
                                }
                                else
                                {
                                    var tf = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom,
                                        warehouseTo, shippingType, true);

                                    if (tf.Count > 0)
                                    {
                                        foreach (var w in tf)
                                        {
                                            if (w.WeightFrom < totalWeight && totalWeight <= w.WeightTo)
                                            {
                                                price = Convert.ToDouble(w.Price);
                                            }
                                        }
                                    }
                                    totalWeightPrice = price * totalWeight;
                                    //isExist = true;
                                }
                            }
                            else
                            {
                                totalWeightPrice = 0;
                            }
                            double checkProductPrice = Convert.ToDouble(txtFeeCheckPackage.Text);
                            double packagePrice = Convert.ToDouble(txtFeePack.Text);
                            double insurrancePrice = Convert.ToDouble(txtFeeInsurrance.Text);
                            double codtqCYN = Convert.ToDouble(txtTotalCODCNY.Text);
                            double codtqVND = Convert.ToDouble(txtTotalCODVN.Text);
                            totalprice = totalWeightPrice + checkProductPrice + packagePrice + insurrancePrice
                                + codtqVND;
                            totalprice = Math.Round(totalprice, 0);

                            TransportationOrderController.UpdateNew(tID, UID, t.Username, warehouseFrom, warehouseTo, shippingType, status, totalWeight, currency,
                                    checkProductPrice, packagePrice, codtqCYN, codtqVND, insurrancePrice,
                                    totalprice, t.Description, currentDate, username_current);

                            PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                        }
                    }
                }
            }
        }

        #region webservice
        [WebMethod]
        public static string getPrice(double weight, int warehouseFrom, int warehouseTo, int shippingType)
        {
            var t = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom,
                    warehouseTo, shippingType, true);
            double price = 0;
            double totalprice = 0;
            if (t.Count > 0)
            {
                foreach (var w in t)
                {
                    if (w.WeightFrom < weight && weight <= w.WeightTo)
                    {
                        price = Math.Round(Convert.ToDouble(w.Price), 0);
                    }
                }
            }
            totalprice = Math.Round(price * weight, 0);
            return totalprice.ToString();
        }
        #endregion
    }
}
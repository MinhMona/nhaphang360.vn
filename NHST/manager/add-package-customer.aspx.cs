using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class add_package_customer : System.Web.UI.Page
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
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 5 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadDDL();
            }
        }

        public void LoadDDL()
        {
            var user = AccountController.GetAll_RoleID_All("");
            if (user.Count > 0)
            {
                ddlBigpack.DataSource = user;
                ddlBigpack.DataBind();
            }
            //var bs = BigPackageController.GetAllWithStatus(1);
            //ddlBigpackage.Items.Clear();
            //ddlBigpackage.Items.Insert(0, new ListItem("Chọn bao lớn", "0"));
            //if (bs.Count > 0)
            //{
            //    foreach (var b in bs)
            //    {
            //        ListItem listitem = new ListItem(b.PackageCode, b.ID.ToString());
            //        ddlBigpackage.Items.Add(listitem);
            //    }
            //}
            //ddlBigpackage.DataBind();
        }
       

        [WebMethod]
        public static string UpdateQuantityNew(string barcode, string quantity, int status, int BigPackageID,
           int packageID, double dai, double rong, int Username, double cao, string base64, string note,
           string nvkiemdem, string khachghichu, string loaisanpham, string packageadditionfeeCYN,
            string packageadditionfeeVND, string packageSensorCYN, string packageSensorVND)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            double quantityT = 0;
            if (quantity.ToFloat(0) > 0)
                quantityT = Math.Round(Convert.ToDouble(quantity), 5);

            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                var checktrancode = SmallPackageController.GetByOrderTransactionCode(package.OrderTransactionCode);
                if (checktrancode.MainOrderID == 0 && checktrancode.TransportationOrderID == 0)
                {
                    var user = AccountController.GetByUsername(username_current);
                    if (user != null)
                    {
                        int userRole = Convert.ToInt32(user.RoleID);

                        if (userRole == 0 || userRole == 2 || userRole == 5)
                        {
                            if (!string.IsNullOrEmpty(AccountController.GetByID(Convert.ToInt32(Username)).Username))
                            {
                                int MainOrderID = 0;
                                //string temp = "";
                                //if (!string.IsNullOrEmpty(ordertransaction))
                                //    temp = ordertransaction;
                                //else
                                //    temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                #region Lấy tất cả các cục hiện có trong đơn
                                var getsmallcheck = SmallPackageController.GetByOrderCode(package.OrderTransactionCode);
                                if (getsmallcheck.Count == 0)
                                {

                                    return "existsmallpackage";
                                }
                                else
                                {
                                    var userkh = AccountController.GetByUsername(AccountController.GetByID(Convert.ToInt32(Username)).Username);
                                    if (userkh != null)
                                    {
                                        if (status == 2)
                                        {
                                            string tID1 = TransportationOrderNewController.Insert1(userkh.ID, userkh.Username, "0", "0", "0", "0", "0", "0", "0",
"0", "0", "0", 0, package.OrderTransactionCode, 3, "", "", "0", "0", DateTime.UtcNow.AddHours(7), username_current, 1, 1, 1, "");
                                            var smallpackage = SmallPackageController.GetByOrderTransactionCode(package.OrderTransactionCode);
                                            if (smallpackage != null)
                                            {
                                                SmallPackageController.UpdateTransationCodeID(smallpackage.ID, userkh.ID, userkh.Username, tID1.ToInt(0), username_current, currentDate);
                                                TransportationOrderNewController.UpdateSmallPackageID(tID1.ToInt(0), smallpackage.ID);
                                                SmallPackageController.UpdateStatus(smallpackage.ID, 2);
                                                //TransportationOrderNewController.UpdateWeight(tID, quantity, currentDate, username_current);
                                            }
                                            var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(tID1));
                                            if (transportation != null)
                                            {
                                                int tID = transportation.ID;
                                                int warehouseFrom = Convert.ToInt32(transportation.WareHouseFromID);
                                                int warehouse = Convert.ToInt32(transportation.WareHouseID);
                                                int shipping = Convert.ToInt32(transportation.ShippingTypeID);
                                                bool checkIsChinaCome = true;
                                                bool CheckIsKhoVN = true;
                                                double totalweight = 0;

                                                var packages = SmallPackageController.GetByTransportationOrderID(tID);
                                                if (packages.Count > 0)
                                                {
                                                    foreach (var p in packages)
                                                    {
                                                        if (p.Status < 2)
                                                            checkIsChinaCome = false;

                                                        if (p.CurrentPlaceID.ToString().ToInt(0) != transportation.WareHouseID)
                                                            CheckIsKhoVN = false;
                                                    }
                                                }
                                                var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                                                double returnprice = 0;
                                                double pricePerWeight = 0;
                                                double finalPriceOfPackage = 0;

                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);

                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }

                                                    if (weight >= compareSize)
                                                    {
                                                        totalweight += weight;
                                                    }
                                                    else
                                                    {
                                                        totalweight += compareSize;
                                                    }
                                                }

                                                totalweight = Math.Round(totalweight, 5);
                                                if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                                                {
                                                    double feetqvn = 0;
                                                    if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                                    {
                                                        feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                                    }
                                                    returnprice = totalweight * feetqvn;
                                                }
                                                else
                                                {
                                                    var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                                    warehouseFrom, warehouse, shipping, true);
                                                    if (fee.Count > 0)
                                                    {
                                                        foreach (var f in fee)
                                                        {
                                                            if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
                                                            {
                                                                pricePerWeight = Convert.ToDouble(f.Price);
                                                                returnprice = totalweight * Convert.ToDouble(f.Price);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);
                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }
                                                    if (weight >= compareSize)
                                                    {
                                                        double TotalPriceCN = Math.Round(weight * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceCN);
                                                    }
                                                    else
                                                    {
                                                        double TotalPriceTT = Math.Round(compareSize * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceTT);
                                                    }
                                                }

                                                finalPriceOfPackage = Math.Round(returnprice, 0);
                                                double currency = Convert.ToDouble(transportation.Currency);
                                                double additionfeeCYN = 0;
                                                double additionfeeVND = 0;
                                                double sensorCYN = 0;
                                                double sensorVND = 0;

                                                if (packageadditionfeeCYN.ToFloat(0) > 0)
                                                {
                                                    additionfeeCYN = Convert.ToDouble(packageadditionfeeCYN);
                                                }
                                                if (packageadditionfeeVND.ToFloat(0) > 0)
                                                {
                                                    additionfeeVND = Convert.ToDouble(packageadditionfeeVND);
                                                }
                                                if (packageSensorCYN.ToFloat(0) > 0)
                                                {
                                                    sensorCYN = Convert.ToDouble(packageSensorCYN);
                                                }
                                                if (packageSensorVND.ToFloat(0) > 0)
                                                {
                                                    sensorVND = Convert.ToDouble(packageSensorVND);
                                                }
                                                //double SensorFeeeVND = Convert.ToDouble(transportation.SensorFeeeVND);
                                                //double AdditionFeeVND = Convert.ToDouble(transportation.AdditionFeeVND);
                                                //double TotalCODTQVND = Convert.ToDouble(transportation.TotalCODTQVND);
                                                //double InsurranceFee = Convert.ToDouble(transportation.InsurranceFee);
                                                double totalPriceVND = finalPriceOfPackage + additionfeeVND + sensorVND;
                                                double totalPriceCYN = 0;
                                                totalPriceCYN = Math.Round(returnprice / currency, 2);
                                                TransportationOrderNewController.UpdateFee(tID, additionfeeCYN.ToString(), additionfeeVND.ToString(), sensorCYN.ToString(), sensorVND.ToString());
                                                TransportationOrderNewController.UpdateDateInVNWareHouse(tID, currentDate);
                                                TransportationOrderNewController.UpdateTotalWeightTotalPrice(tID, totalweight.ToString(), totalPriceVND.ToString(), currentDate, username_current);
                                            }
                                        }
                                        else if (status == 3)
                                        {
                                            string tID1 = TransportationOrderNewController.Insert1(userkh.ID, userkh.Username, "0", "0", "0", "0", "0", "0", "0",
"0", "0", "0", 0, package.OrderTransactionCode, 4, "", "", "0", "0", DateTime.UtcNow.AddHours(7), username_current, 1, 1, 1, "");
                                            var smallpackage = SmallPackageController.GetByOrderTransactionCode(package.OrderTransactionCode);
                                            if (smallpackage != null)
                                            {
                                                SmallPackageController.UpdateTransationCodeID(smallpackage.ID, userkh.ID, userkh.Username, tID1.ToInt(0), username_current, currentDate);
                                                TransportationOrderNewController.UpdateSmallPackageID(tID1.ToInt(0), smallpackage.ID);
                                                SmallPackageController.UpdateStatus(Convert.ToInt32(tID1), 3);
                                                //TransportationOrderNewController.UpdateWeight(tID, quantity, currentDate, username_current);
                                            }
                                            var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(tID1));
                                            if (transportation != null)
                                            {
                                                int tID = transportation.ID;
                                                int warehouseFrom = Convert.ToInt32(transportation.WareHouseFromID);
                                                int warehouse = Convert.ToInt32(transportation.WareHouseID);
                                                int shipping = Convert.ToInt32(transportation.ShippingTypeID);
                                                bool checkIsChinaCome = true;
                                                bool CheckIsKhoVN = true;
                                                double totalweight = 0;

                                                var packages = SmallPackageController.GetByTransportationOrderID(tID);
                                                if (packages.Count > 0)
                                                {
                                                    foreach (var p in packages)
                                                    {
                                                        if (p.Status < 2)
                                                            checkIsChinaCome = false;

                                                        if (p.CurrentPlaceID.ToString().ToInt(0) != transportation.WareHouseID)
                                                            CheckIsKhoVN = false;
                                                    }
                                                }
                                                var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                                                double returnprice = 0;
                                                double pricePerWeight = 0;
                                                double finalPriceOfPackage = 0;

                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);

                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }

                                                    if (weight >= compareSize)
                                                    {
                                                        totalweight += weight;
                                                    }
                                                    else
                                                    {
                                                        totalweight += compareSize;
                                                    }
                                                }

                                                totalweight = Math.Round(totalweight, 5);
                                                if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                                                {
                                                    double feetqvn = 0;
                                                    if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                                    {
                                                        feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                                    }
                                                    returnprice = totalweight * feetqvn;
                                                }
                                                else
                                                {
                                                    var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                                    warehouseFrom, warehouse, shipping, true);
                                                    if (fee.Count > 0)
                                                    {
                                                        foreach (var f in fee)
                                                        {
                                                            if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
                                                            {
                                                                pricePerWeight = Convert.ToDouble(f.Price);
                                                                returnprice = totalweight * Convert.ToDouble(f.Price);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);
                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }
                                                    if (weight >= compareSize)
                                                    {
                                                        double TotalPriceCN = Math.Round(weight * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceCN);
                                                    }
                                                    else
                                                    {
                                                        double TotalPriceTT = Math.Round(compareSize * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceTT);
                                                    }
                                                }

                                                finalPriceOfPackage = Math.Round(returnprice, 0);
                                                double currency = Convert.ToDouble(transportation.Currency);
                                                double additionfeeCYN = 0;
                                                double additionfeeVND = 0;
                                                double sensorCYN = 0;
                                                double sensorVND = 0;

                                                if (packageadditionfeeCYN.ToFloat(0) > 0)
                                                {
                                                    additionfeeCYN = Convert.ToDouble(packageadditionfeeCYN);
                                                }
                                                if (packageadditionfeeVND.ToFloat(0) > 0)
                                                {
                                                    additionfeeVND = Convert.ToDouble(packageadditionfeeVND);
                                                }
                                                if (packageSensorCYN.ToFloat(0) > 0)
                                                {
                                                    sensorCYN = Convert.ToDouble(packageSensorCYN);
                                                }
                                                if (packageSensorVND.ToFloat(0) > 0)
                                                {
                                                    sensorVND = Convert.ToDouble(packageSensorVND);
                                                }
                                                //double SensorFeeeVND = Convert.ToDouble(transportation.SensorFeeeVND);
                                                //double AdditionFeeVND = Convert.ToDouble(transportation.AdditionFeeVND);
                                                //double TotalCODTQVND = Convert.ToDouble(transportation.TotalCODTQVND);
                                                //double InsurranceFee = Convert.ToDouble(transportation.InsurranceFee);
                                                double totalPriceVND = finalPriceOfPackage + additionfeeVND + sensorVND;
                                                double totalPriceCYN = 0;
                                                totalPriceCYN = Math.Round(returnprice / currency, 2);
                                                TransportationOrderNewController.UpdateFee(tID, additionfeeCYN.ToString(), additionfeeVND.ToString(), sensorCYN.ToString(), sensorVND.ToString());
                                                TransportationOrderNewController.UpdateDateInVNWareHouse(tID, currentDate);
                                                TransportationOrderNewController.UpdateTotalWeightTotalPrice(tID, totalweight.ToString(), totalPriceVND.ToString(), currentDate, username_current);
                                            }
                                        }
                                        else
                                        {
                                            string tID1 = TransportationOrderNewController.Insert1(userkh.ID, userkh.Username, "0", "0", "0", "0", "0", "0", "0",
"0", "0", "0", 0, package.OrderTransactionCode, 1, "", "", "0", "0", DateTime.UtcNow.AddHours(7), username_current, 1, 1, 1, "");
                                            var smallpackage = SmallPackageController.GetByOrderTransactionCode(package.OrderTransactionCode);
                                            if (smallpackage != null)
                                            {
                                                SmallPackageController.UpdateTransationCodeID(smallpackage.ID, userkh.ID, userkh.Username, tID1.ToInt(0), username_current, currentDate);
                                                TransportationOrderNewController.UpdateSmallPackageID(tID1.ToInt(0), smallpackage.ID);
                                                //TransportationOrderNewController.UpdateWeight(tID, quantity, currentDate, username_current);
                                            }
                                            var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(tID1));
                                            if (transportation != null)
                                            {
                                                int tID = transportation.ID;
                                                int warehouseFrom = Convert.ToInt32(transportation.WareHouseFromID);
                                                int warehouse = Convert.ToInt32(transportation.WareHouseID);
                                                int shipping = Convert.ToInt32(transportation.ShippingTypeID);
                                                bool checkIsChinaCome = true;
                                                bool CheckIsKhoVN = true;
                                                double totalweight = 0;

                                                var packages = SmallPackageController.GetByTransportationOrderID(tID);
                                                if (packages.Count > 0)
                                                {
                                                    foreach (var p in packages)
                                                    {
                                                        if (p.Status < 2)
                                                            checkIsChinaCome = false;

                                                        if (p.CurrentPlaceID.ToString().ToInt(0) != transportation.WareHouseID)
                                                            CheckIsKhoVN = false;
                                                    }
                                                }
                                                var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                                                double returnprice = 0;
                                                double pricePerWeight = 0;
                                                double finalPriceOfPackage = 0;

                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);

                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }

                                                    if (weight >= compareSize)
                                                    {
                                                        totalweight += weight;
                                                    }
                                                    else
                                                    {
                                                        totalweight += compareSize;
                                                    }
                                                }

                                                totalweight = Math.Round(totalweight, 5);
                                                if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                                                {
                                                    double feetqvn = 0;
                                                    if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                                    {
                                                        feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                                    }
                                                    returnprice = totalweight * feetqvn;
                                                }
                                                else
                                                {
                                                    var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                                    warehouseFrom, warehouse, shipping, true);
                                                    if (fee.Count > 0)
                                                    {
                                                        foreach (var f in fee)
                                                        {
                                                            if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
                                                            {
                                                                pricePerWeight = Convert.ToDouble(f.Price);
                                                                returnprice = totalweight * Convert.ToDouble(f.Price);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                foreach (var item in packages)
                                                {
                                                    double compareSize = 0;
                                                    double weight = Convert.ToDouble(item.Weight);
                                                    double pDai = Convert.ToDouble(item.Length);
                                                    double pRong = Convert.ToDouble(item.Width);
                                                    double pCao = Convert.ToDouble(item.Height);
                                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                                    {
                                                        compareSize = (pDai * pRong * pCao) / 7000;
                                                    }
                                                    if (weight >= compareSize)
                                                    {
                                                        double TotalPriceCN = Math.Round(weight * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceCN);
                                                    }
                                                    else
                                                    {
                                                        double TotalPriceTT = Math.Round(compareSize * pricePerWeight, 0);
                                                        SmallPackageController.UpdateTotalPrice(item.ID, TotalPriceTT);
                                                    }
                                                }

                                                finalPriceOfPackage = Math.Round(returnprice, 0);
                                                double currency = Convert.ToDouble(transportation.Currency);
                                                double additionfeeCYN = 0;
                                                double additionfeeVND = 0;
                                                double sensorCYN = 0;
                                                double sensorVND = 0;

                                                if (packageadditionfeeCYN.ToFloat(0) > 0)
                                                {
                                                    additionfeeCYN = Convert.ToDouble(packageadditionfeeCYN);
                                                }
                                                if (packageadditionfeeVND.ToFloat(0) > 0)
                                                {
                                                    additionfeeVND = Convert.ToDouble(packageadditionfeeVND);
                                                }
                                                if (packageSensorCYN.ToFloat(0) > 0)
                                                {
                                                    sensorCYN = Convert.ToDouble(packageSensorCYN);
                                                }
                                                if (packageSensorVND.ToFloat(0) > 0)
                                                {
                                                    sensorVND = Convert.ToDouble(packageSensorVND);
                                                }
                                                //double SensorFeeeVND = Convert.ToDouble(transportation.SensorFeeeVND);
                                                //double AdditionFeeVND = Convert.ToDouble(transportation.AdditionFeeVND);
                                                //double TotalCODTQVND = Convert.ToDouble(transportation.TotalCODTQVND);
                                                //double InsurranceFee = Convert.ToDouble(transportation.InsurranceFee);
                                                double totalPriceVND = finalPriceOfPackage + additionfeeVND + sensorVND;
                                                double totalPriceCYN = 0;
                                                totalPriceCYN = Math.Round(returnprice / currency, 2);
                                                TransportationOrderNewController.UpdateFee(tID, additionfeeCYN.ToString(), additionfeeVND.ToString(), sensorCYN.ToString(), sensorVND.ToString());
                                                TransportationOrderNewController.UpdateDateInVNWareHouse(tID, currentDate);
                                                TransportationOrderNewController.UpdateTotalWeightTotalPrice(tID, totalweight.ToString(), totalPriceVND.ToString(), currentDate, username_current);
                                            }
                                        }

                                        PackageAll1 pa = new PackageAll1();
                                        pa.PackageAllType = 0;
                                        pa.PackageGetCount = 0;
                                        List<OrderGet1> og = new List<OrderGet1>();
                                        OrderGet1 o = new OrderGet1();
                                        o.ID = packageID;
                                        o.OrderType = "Vận chuyển hộ";
                                        o.BigPackageID = 0;
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = "0";
                                        o.Status = 1;
                                        int mainOrderID = Convert.ToInt32(MainOrderID);
                                        o.MainorderID = mainOrderID;
                                        o.TransportationID = 0;
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "0";
                                        o.Soluongsanpham = "0";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        og.Add(o);

                                        pa.listPackageGet = og;
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        return serializer.Serialize(pa);

                                    }
                                    else
                                        return "notusername";
                                }
                                #endregion
                            }

                            else
                            {
                                return "nhapusername";
                            }
                        }
                        else
                            return "none";
                    }
                    else
                    {
                        return "none";
                    }
                }
                else
                {
                    return "nottroinoi";
                }
            }
            return "none";
        }
        #region Class   
        public class BigPackOut
        {
            public int BigPackOutType { get; set; }
            public List<PackageAll> Pall { get; set; }
        }
        public class PackageAll
        {
            public int MainOrderID { get; set; }
            public int PackageAllType { get; set; }
            public string OrderCode { get; set; }
            public int PackageGetCount { get; set; }
            public List<OrderGet> listPackageGet { get; set; }
        }
        public class OrderGet
        {
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int TransportationID { get; set; }
            public string OrderType { get; set; }
            public int OrderTypeInt { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public double TotalPriceVNDNum { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string Baohiem { get; set; }
            public string NVKiemdem { get; set; }
            public string Loaisanpham { get; set; }
            public string Khachghichu { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public List<tbl_BigPackage> ListBig { get; set; }
            public int IsTemp { get; set; }
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public string Phone { get; set; }
            public string IMG { get; set; }
            public string Note { get; set; }
            public string KhoTQ { get; set; }
            public string KhoVN { get; set; }
            public string PTVC { get; set; }
        }
        #endregion

        public class BillInfo
        {
            public string Barcode { get; set; }
            public string BarcodeURL { get; set; }
            public string Weight { get; set; }
            public string Phone { get; set; }
            public string Username { get; set; }
        }

        [WebMethod]
        public static string Delete(string PackageID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        var check = SmallPackageController.GetByID(Convert.ToInt32(PackageID));
                        if (check != null)
                        {
                            SmallPackageController.Delete(check.ID);
                            return "ok";
                        }
                        else return "null";
                    }
                    else return "null";
                }
                else return "null";
            }
            else return "null";
        }

        [WebMethod]
        public static string GetBigPackage()
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                var bs = AccountController.GetAll_RoleID_All("");
                if (bs.Count > 0)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(bs);
                }
                else
                    return null;
            }
            else
                return null;
        }

        [WebMethod]
        public static string GetListPackage(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 5)
                    {
                        var bigpackage = BigPackageController.GetByPackageCode(barcode);
                        if (bigpackage != null)
                        {
                            int bID = bigpackage.ID;
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = bID;
                            bi.BigpackageCode = bigpackage.PackageCode;
                            bi.BigpackageType = 1;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetBuyBigPackageID_Thang(bID, "");
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.IMG = item.ListIMG;
                                    si.Note = item.Description;
                                    si.ID = item.ID;


                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 3;
                                        si.Username = "NA";
                                        si.Phone = "NA";
                                        si.Soluongsanpham = "0";

                                        string kiemdem = "Không";
                                        string donggo = "Không";
                                        string baohiem = "Không";
                                        if (item.IsCheckProduct == true)
                                            kiemdem = "Có";
                                        if (item.IsPackaged == true)
                                            donggo = "Có";
                                        if (item.IsInsurrance == true)
                                            baohiem = "Có";
                                        si.Kiemdem = kiemdem;
                                        si.Donggo = donggo;
                                        si.Baohiem = baohiem;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = Convert.ToInt32(item.Status);

                                    if (!string.IsNullOrEmpty(item.Description))
                                        si.Description = item.Description;
                                    else
                                        si.Description = string.Empty;

                                    si.BigPackageID = bigpackage.ID;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length.ToString().ToFloat(0) > 0)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width.ToString().ToFloat(0) > 0)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height.ToString().ToFloat(0) > 0)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;



                                    if (!string.IsNullOrEmpty(item.UserNote))
                                        si.Khachghichu = item.UserNote;
                                    else
                                        si.Khachghichu = string.Empty;

                                    si.Loaisanpham = item.ProductType;
                                    if (!string.IsNullOrEmpty(item.StaffNoteCheck))
                                        si.NVKiemdem = item.StaffNoteCheck;
                                    else
                                        si.NVKiemdem = string.Empty;

                                    sis.Add(si);
                                }
                            }
                            if (smallpackages.Count == 0)
                            {
                                return "none";
                            }
                            bi.BigpackageSmallPackageCount = smallpackages.Count;
                            bi.Smallpackages = sis;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bi);
                        }
                        else
                        {
                            BigPackageItem bi = new BigPackageItem();
                            bi.BigpackageID = 0;
                            bi.BigpackageCode = "";
                            bi.BigpackageType = 2;
                            List<smallpackageitem> sis = new List<smallpackageitem>();
                            var smallpackages = SmallPackageController.GetListByOrderTransactionCode_Thang(barcode);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var item in smallpackages)
                                {
                                    SmallPackageController.UpdateStatusAndIsLostAndDateInKhoDich(item.ID, 3, false, currentDate, currentDate, username);
                                    SmallPackageController.UpdateDateInVNWareHouse(item.ID, username, currentDate);
                                    int bID = Convert.ToInt32(item.BigPackageID);
                                    if (bID > 0)
                                    {
                                        var big = BigPackageController.GetByID(bID);
                                        if (big != null)
                                        {
                                            bool checkIschua = false;
                                            var smalls = SmallPackageController.GetBuyBigPackageID(bID, "");
                                            if (smalls.Count > 0)
                                            {
                                                foreach (var s in smalls)
                                                {
                                                    if (s.Status < 3)
                                                        checkIschua = true;
                                                }
                                                if (checkIschua == false)
                                                {
                                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username);
                                                }
                                            }
                                        }
                                    }

                                    smallpackageitem si = new smallpackageitem();
                                    int mID = Convert.ToInt32(item.MainOrderID);
                                    int tID = Convert.ToInt32(item.TransportationOrderID);
                                    si.ID = item.ID;
                                    si.IMG = item.ListIMG;
                                    si.Note = item.Description;

                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 3;
                                        si.Username = "NA";
                                        si.Phone = "NA";
                                        si.Soluongsanpham = "0";
                                        string kiemdem = "Không";
                                        string donggo = "Không";
                                        string baohiem = "Không";
                                        if (item.IsCheckProduct == true)
                                            kiemdem = "Có";
                                        if (item.IsPackaged == true)
                                            donggo = "Có";
                                        if (item.IsInsurrance == true)
                                            baohiem = "Có";
                                        si.Kiemdem = kiemdem;
                                        si.Donggo = donggo;
                                        si.Baohiem = baohiem;
                                    }
                                    si.Weight = Convert.ToDouble(item.Weight);
                                    si.BarCode = item.OrderTransactionCode;
                                    si.Status = 3;
                                    si.Description = item.Description;
                                    si.BigPackageID = 0;
                                    si.IsTemp = Convert.ToBoolean(item.IsTemp);
                                    if (item.IsLost != null)
                                        si.IsThatlac = Convert.ToBoolean(item.IsLost);
                                    else
                                        si.IsThatlac = false;
                                    if (item.IsHelpMoving != null)
                                        si.IsVCH = Convert.ToBoolean(item.IsHelpMoving);
                                    else
                                        si.IsVCH = false;
                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (item.Length.ToString().ToFloat(0) > 0)
                                    {
                                        dai = Convert.ToDouble(item.Length);
                                    }
                                    if (item.Width.ToString().ToFloat(0) > 0)
                                    {
                                        rong = Convert.ToDouble(item.Width);
                                    }
                                    if (item.Height.ToString().ToFloat(0) > 0)
                                    {
                                        cao = Convert.ToDouble(item.Height);
                                    }
                                    si.dai = dai;
                                    si.rong = rong;
                                    si.cao = cao;



                                    if (!string.IsNullOrEmpty(item.Description))
                                        si.Description = item.Description;
                                    else
                                        si.Description = string.Empty;

                                    if (!string.IsNullOrEmpty(item.UserNote))
                                        si.Khachghichu = item.UserNote;
                                    else
                                        si.Khachghichu = string.Empty;

                                    si.Loaisanpham = item.ProductType;
                                    if (!string.IsNullOrEmpty(item.StaffNoteCheck))
                                        si.NVKiemdem = item.StaffNoteCheck;
                                    else
                                        si.NVKiemdem = string.Empty;
                                    sis.Add(si);
                                }


                                bi.BigpackageSmallPackageCount = smallpackages.Count;
                                bi.Smallpackages = sis;
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                return serializer.Serialize(bi);
                            }

                        }
                    }
                }
            }
            return "none";
        }

      

       

        public class MainOrder
        {
            public int ID { get; set; }
            public int MainOrderCodeID { get; set; }
            public string MainOrderCode { get; set; }
            public string Username { get; set; }
            public List<Order> Order { get; set; }
        }

        public class Order
        {
            public string Image { get; set; }
            public int SoLuong { get; set; }
            public string Title { get; set; }
        }

        


        public class BigPackageItem
        {
            public int BigpackageID { get; set; }
            public string BigpackageCode { get; set; }
            public int BigpackageSmallPackageCount { get; set; }
            public int BigpackageType { get; set; }
            public List<smallpackageitem> Smallpackages { get; set; }
        }
        public class smallpackageitem
        {
            public int ID { get; set; }
            public string OrderType { get; set; }
            public int MainorderID { get; set; }
            public int TransportationID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public double Weight { get; set; }

            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public string Baohiem { get; set; }
            public string NVKiemdem { get; set; }
            public string Loaisanpham { get; set; }
            public string Khachghichu { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public bool IsTemp { get; set; }
            public bool IsThatlac { get; set; }
            public bool IsVCH { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public string IMG { get; set; }
            public string Note { get; set; }
            public int OrderTypeInt { get; set; }

            public double CuocvattuCYN { get; set; }
            public double CuocvattuVND { get; set; }
            public double HangDBCYN { get; set; }
            public double HangDBVND { get; set; }
        }

        public class PackageAll1
        {
            public int MainOrderID { get; set; }
            public int PackageAllType { get; set; }
            public string OrderCode { get; set; }
            public int PackageGetCount { get; set; }
            public List<OrderGet1> listPackageGet { get; set; }
        }
        public class OrderGet1
        {
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int TransportationID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public double TotalPriceVNDNum { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string OrderType { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public string Baohiem { get; set; }
            public string NVKiemdem { get; set; }
            public string Loaisanpham { get; set; }
            public string Khachghichu { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public List<tbl_BigPackage> ListBig { get; set; }
            public int IsTemp { get; set; }
            public int IsThatlac { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }

        }
    }
}
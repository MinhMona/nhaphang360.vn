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
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class outstock_vch_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDL();
            }
        }

        public void LoadDDL()
        {
            var PTVC = ShippingTypeVNController.GetAllWithIsHidden("", false);
            ddlPTVC.Items.Clear();
            ddlPTVC.Items.Insert(0, new ListItem("Chọn hình thức giao hàng", "0"));
            if (PTVC.Count > 0)
            {
                foreach (var b in PTVC)
                {
                    ListItem listitem = new ListItem(b.ShippingTypeVNName, b.ID.ToString());
                    ddlPTVC.Items.Add(listitem);
                }
            }
            ddlPTVC.DataBind();

            var user = AccountController.GetAll_RoleID_All("");
            if (user.Count > 0)
            {
                ddlUsername.DataSource = user;
                ddlUsername.DataBind();
            }

            //var KhoTQ = WarehouseFromController.GetAllWithIsHidden(false);
            //ddlKhoTQ.Items.Clear();
            //ddlKhoTQ.Items.Insert(0, new ListItem("Chọn kho TQ", "0"));
            //if (KhoTQ.Count > 0)
            //{
            //    foreach (var b in KhoTQ)
            //    {
            //        ListItem listitem = new ListItem(b.WareHouseName, b.ID.ToString());
            //        ddlKhoTQ.Items.Add(listitem);
            //    }
            //}
            //ddlKhoTQ.DataBind();

            //var KhoVN = WarehouseController.GetAllWithIsHidden(false);
            //ddlKhoVN.Items.Clear();
            //ddlKhoVN.Items.Insert(0, new ListItem("Chọn kho VN", "0"));
            //if (KhoVN.Count > 0)
            //{
            //    foreach (var b in KhoVN)
            //    {
            //        ListItem listitem = new ListItem(b.WareHouseName, b.ID.ToString());
            //        ddlKhoVN.Items.Add(listitem);
            //    }
            //}
            //ddlKhoVN.DataBind();

            //var HTVC = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            //ddlHTVC.Items.Clear();
            //ddlHTVC.Items.Insert(0, new ListItem("Chọn phương thức vận chuyển", "0"));
            //if (HTVC.Count > 0)
            //{
            //    foreach (var b in HTVC)
            //    {
            //        ListItem listitem = new ListItem(b.ShippingTypeName, b.ID.ToString());
            //        ddlHTVC.Items.Add(listitem);
            //    }
            //}
            //ddlHTVC.DataBind();
        }

        #region Webservice mới
        [WebMethod]
        public static string getpackages(string barcode, string username)
        {
            DateTime currentDate = DateTime.Now;
            var accountInput = AccountController.GetByID(Convert.ToInt32(username));
            username = accountInput.Username;
            if (accountInput != null)
            {
                var smallpackage = SmallPackageController.GetByOrderTransactionCode(barcode);
                if (smallpackage != null)
                {
                    var reou = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                    if (reou == null)
                    {
                        if (smallpackage.Status > 0)
                        {
                            int tID = Convert.ToInt32(smallpackage.TransportationOrderID);
                            if (tID > 0)
                            {
                                var t = TransportationOrderNewController.GetByID(tID);
                                if (t != null)
                                {
                                    int UID = Convert.ToInt32(t.UID);
                                    if (UID == accountInput.ID)
                                    {
                                        PackageGet p = new PackageGet();
                                        p.pID = t.ID;
                                        p.uID = UID;
                                        p.username = username;
                                        p.mID = 0;
                                        p.tID = tID;
                                        p.weight = Convert.ToDouble(smallpackage.Weight);
                                        p.status = Convert.ToInt32(smallpackage.Status);
                                        p.barcode = barcode;
                                        double day = 0;
                                        if (smallpackage.DateInLasteWareHouse != null)
                                        {
                                            DateTime dateinwarehouse = Convert.ToDateTime(smallpackage.DateInLasteWareHouse);
                                            TimeSpan ts = currentDate.Subtract(dateinwarehouse);
                                            day = Math.Floor(ts.TotalDays);
                                        }
                                        p.TotalDayInWarehouse = day;
                                        p.dateInWarehouse = string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse);

                                        double additionFeeCYN = 0;
                                        double additionFeeVND = 0;
                                        double sensorFeeCYN = 0;
                                        double sensorFeeVND = 0;
                                        if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                        {
                                            additionFeeCYN = Convert.ToDouble(t.AdditionFeeCYN);
                                        }
                                        if (t.AdditionFeeVND.ToFloat(0) > 0)
                                        {
                                            additionFeeVND = Convert.ToDouble(t.AdditionFeeVND);
                                        }
                                        if (t.SensorFeeCYN.ToFloat(0) > 0)
                                        {
                                            sensorFeeCYN = Convert.ToDouble(t.SensorFeeCYN);
                                        }
                                        if (t.SensorFeeeVND.ToFloat(0) > 0)
                                        {
                                            sensorFeeVND = Convert.ToDouble(t.SensorFeeeVND);
                                        }
                                        p.AdditionFeeCYN = additionFeeCYN.ToString();
                                        p.AdditionFeeVND = additionFeeVND.ToString();
                                        p.SensorFeeCYN = sensorFeeCYN.ToString();
                                        p.SensorFeeVND = sensorFeeVND.ToString();


                                        if (smallpackage.IsCheckProduct == true)
                                            p.kiemdem = "Có";
                                        else
                                            p.kiemdem = "Không";

                                        if (smallpackage.IsPackaged == true)
                                            p.donggo = "Có";
                                        else
                                            p.donggo = "Không";

                                        if (smallpackage.IsInsurrance == true)
                                            p.baohiem = "Có";
                                        else
                                            p.baohiem = "Không";

                                        p.OrderTypeString = "Đơn hàng VC hộ";
                                        p.OrderType = 2;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;
                                        if (smallpackage.Length.ToString().ToFloat(0) > 0)
                                        {
                                            dai = Convert.ToDouble(smallpackage.Length);
                                        }
                                        if (smallpackage.Width.ToString().ToFloat(0) > 0)
                                        {
                                            rong = Convert.ToDouble(smallpackage.Width);
                                        }
                                        if (smallpackage.Height.ToString().ToFloat(0) > 0)
                                        {
                                            cao = Convert.ToDouble(smallpackage.Height);
                                        }
                                        p.dai = dai;
                                        p.rong = rong;
                                        p.cao = cao;

                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        return serializer.Serialize(p);
                                    }
                                }
                            }
                            else
                            {
                                PackageGet p = new PackageGet();
                                p.pID = smallpackage.ID;
                                p.uID = 0;
                                p.username = "";
                                p.mID = 0;
                                p.tID = 0;
                                p.weight = Convert.ToDouble(smallpackage.Weight);
                                p.status = Convert.ToInt32(smallpackage.Status);
                                p.barcode = barcode;
                                double day = 0;
                                if (smallpackage.DateInLasteWareHouse != null)
                                {
                                    DateTime dateinwarehouse = Convert.ToDateTime(smallpackage.DateInLasteWareHouse);
                                    TimeSpan ts = currentDate.Subtract(dateinwarehouse);
                                    day = Math.Floor(ts.TotalDays);
                                }
                                p.TotalDayInWarehouse = day;
                                p.dateInWarehouse = string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse);
                                p.kiemdem = "Không";
                                p.donggo = "Không";
                                p.baohiem = "Không";
                                p.OrderTypeString = "Chưa xác định";
                                p.OrderType = 3;
                                double dai = 0;
                                double rong = 0;
                                double cao = 0;
                                if (smallpackage.Length.ToString().ToFloat(0) > 0)
                                {
                                    dai = Convert.ToDouble(smallpackage.Length);
                                }
                                if (smallpackage.Width.ToString().ToFloat(0) > 0)
                                {
                                    rong = Convert.ToDouble(smallpackage.Width);
                                }
                                if (smallpackage.Height.ToString().ToFloat(0) > 0)
                                {
                                    cao = Convert.ToDouble(smallpackage.Height);
                                }
                                p.dai = dai;
                                p.rong = rong;
                                p.cao = cao;

                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                return serializer.Serialize(p);
                            }
                        }
                    }
                    else
                        return "request";
                }
            }
            else
            {
                return "notexistuser";
            }

            return "none";
        }

        #endregion
        public class PackageGet
        {
            public int pID { get; set; }
            public int mID { get; set; }
            public int tID { get; set; }
            public int uID { get; set; }
            public string username { get; set; }
            public double weight { get; set; }
            public int status { get; set; }
            public string kiemdem { get; set; }
            public string donggo { get; set; }
            public string baohiem { get; set; }
            public string barcode { get; set; }
            public string dateInWarehouse { get; set; }
            public string OrderTypeString { get; set; }
            public int OrderType { get; set; }
            public double TotalDayInWarehouse { get; set; }
            public double dai { get; set; }
            public double rong { get; set; }
            public double cao { get; set; }
            public string AdditionFeeCYN { get; set; }
            public string AdditionFeeVND { get; set; }
            public string SensorFeeCYN { get; set; }
            public string SensorFeeVND { get; set; }
            public List<tbl_ShippingTypeVN> ListShip { get; set; }
        }

        public class OrderGet
        {
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public double TotalPriceVNDNum { get; set; }
            public int Status { get; set; }
            public int MainOrderStatus { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
        }

        protected void btnAllOutstock_Click(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] == null)
            {
            }
            else
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                if (ac.RoleID != 0 && ac.RoleID != 5 && ac.RoleID != 2)
                {

                }
                else
                {
                    DateTime currentDate = DateTime.Now;
                    var a = AccountController.GetByID(Convert.ToInt32(hdfUsername.Value));
                    string usernameout = a.Username;
                    var acc = AccountController.GetByUsername(usernameout);
                    if (acc != null)
                    {
                        string fullname = "";
                        string phone = "";
                        var ai = AccountInfoController.GetByUserID(acc.ID);
                        if (ai != null)
                        {
                            fullname = ai.FirstName + " " + ai.LastName;
                            phone = ai.Phone;
                        }

                        string listpack = hdfListPID.Value;
                        if (!string.IsNullOrEmpty(listpack))
                        {
                            string[] listID = listpack.Split('|');
                            if (listID.Length - 1 > 0)
                            {
                                double feeOutStockCYN = 0;
                                double feeOutStockVND = 0;
                                double feeWeightOutStock = 0;

                                double totalWeight = 0;
                                double currency = 0;

                                double TotalAdditionFeeCYN = 0;
                                double TotalAdditionFeeVND = 0;

                                double TotalSensoredFeeCYN = 0;
                                double TotalSensoredFeeVND = 0;

                                double totalWeightPriceVND = 0;
                                double totalWeightPriceCYN = 0;

                                double totalPriceVND = 0;
                                double totalPriceCYN = 0;

                                var config = ConfigurationController.GetByTop1();
                                if (config != null)
                                {
                                    currency = Convert.ToDouble(config.AgentCurrency);
                                    feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                                    feeOutStockVND = feeOutStockCYN * currency;
                                }
                                List<WareHouse> lw = new List<WareHouse>();

                                List<int> lID = new List<int>();
                                for (int i = 0; i < listID.Length - 1; i++)
                                {
                                    int ID = listID[i].ToInt(0);
                                    var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                                    if (t != null)
                                    {
                                        lID.Add(t.ID);
                                        var checkwh = lw.Where(x => x.WareHouseID == t.WareHouseID && x.WareHouseFromID == t.WareHouseFromID && x.ShippingTypeID == t.ShippingTypeID).FirstOrDefault();
                                        if (checkwh != null)
                                        {
                                            if (t.SmallPackageID != null)
                                            {
                                                if (t.SmallPackageID > 0)
                                                {
                                                    var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                                    if (package != null)
                                                    {
                                                        double weight = 0;
                                                        if (package.Weight != null)
                                                        {
                                                            if (package.Weight > 0)
                                                            {
                                                                Package p = new Package();
                                                                weight = Convert.ToDouble(package.Weight);
                                                                totalWeight += weight;
                                                                p.Weight = weight;
                                                                p.TransportationID = t.ID;
                                                                checkwh.TotalWeight += weight;
                                                                checkwh.ListPackage.Add(p);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            WareHouse w = new WareHouse();
                                            w.WareHouseFromID = t.WareHouseFromID.Value;
                                            w.WareHouseID = t.WareHouseID.Value;
                                            w.ShippingTypeID = t.ShippingTypeID.Value;
                                            if (t.SmallPackageID != null)
                                            {
                                                if (t.SmallPackageID > 0)
                                                {
                                                    List<Package> lp = new List<Package>();
                                                    var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                                    if (package != null)
                                                    {
                                                        double weight = 0;
                                                        if (package.Weight != null)
                                                        {
                                                            if (package.Weight > 0)
                                                            {
                                                                Package p = new Package();
                                                                weight = Convert.ToDouble(package.Weight);
                                                                totalWeight += weight;
                                                                w.TotalWeight = weight;
                                                                p.Weight = weight;
                                                                p.TransportationID = t.ID;
                                                                lp.Add(p);
                                                            }
                                                        }
                                                    }
                                                    w.ListPackage = lp;
                                                    lw.Add(w);
                                                }
                                            }
                                        }

                                        double addfeeVND = 0;
                                        double addfeeCYN = 0;
                                        double sensorVND = 0;
                                        double sensorCYN = 0;

                                        if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                            if (t.AdditionFeeVND.ToFloat(0) > 0)
                                                addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                                        if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                            if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                                addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                                        if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                            if (t.SensorFeeeVND.ToFloat(0) > 0)
                                                sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                                        if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                            if (t.SensorFeeCYN.ToFloat(0) > 0)
                                                sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                                        TotalAdditionFeeCYN += addfeeCYN;
                                        TotalAdditionFeeVND += addfeeVND;

                                        TotalSensoredFeeVND += sensorVND;
                                        TotalSensoredFeeCYN += sensorCYN;
                                    }
                                    else
                                    {
                                        string ghichu = txtExNote.Text;
                                        var smallpackage = SmallPackageController.GetByID(ID);
                                        if (smallpackage != null)
                                        {
                                            string tID = TransportationOrderNewController.Insert(acc.ID, acc.Username, smallpackage.Weight.ToString(), currency.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", smallpackage.ID, smallpackage.OrderTransactionCode, 4, ghichu, username_current, "0", "0",
                                                currentDate, username_current, Convert.ToInt32(ddlKhoTQ.SelectedValue), Convert.ToInt32(ddlKhoVN.SelectedValue), Convert.ToInt32(ddlHTVC.SelectedValue));
                                            SmallPackageController.UpdateNew(ID, tID.ToInt(0));
                                            var t1 = TransportationOrderNewController.GetByIDAndUID(tID.ToInt(0), acc.ID);
                                            if (t1 != null)
                                            {
                                                lID.Add(t1.ID);
                                                var checkwh = lw.Where(x => x.WareHouseID == t1.WareHouseID && x.WareHouseFromID == t1.WareHouseFromID && x.ShippingTypeID == t1.ShippingTypeID).FirstOrDefault();
                                                if (checkwh != null)
                                                {
                                                    if (t1.SmallPackageID != null)
                                                    {
                                                        if (t1.SmallPackageID > 0)
                                                        {
                                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t1.SmallPackageID));
                                                            if (package != null)
                                                            {
                                                                double weight = 0;
                                                                if (package.Weight != null)
                                                                {
                                                                    if (package.Weight > 0)
                                                                    {
                                                                        Package p = new Package();
                                                                        weight = Convert.ToDouble(package.Weight);
                                                                        totalWeight += weight;
                                                                        p.Weight = weight;
                                                                        p.TransportationID = t1.ID;
                                                                        checkwh.TotalWeight += weight;
                                                                        checkwh.ListPackage.Add(p);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    WareHouse w = new WareHouse();
                                                    w.WareHouseFromID = t1.WareHouseFromID.Value;
                                                    w.WareHouseID = t1.WareHouseID.Value;
                                                    w.ShippingTypeID = t1.ShippingTypeID.Value;
                                                    if (t1.SmallPackageID != null)
                                                    {
                                                        if (t1.SmallPackageID > 0)
                                                        {
                                                            List<Package> lp = new List<Package>();
                                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t1.SmallPackageID));
                                                            if (package != null)
                                                            {
                                                                double weight = 0;
                                                                if (package.Weight != null)
                                                                {
                                                                    if (package.Weight > 0)
                                                                    {
                                                                        Package p = new Package();
                                                                        weight = Convert.ToDouble(package.Weight);
                                                                        totalWeight += weight;
                                                                        w.TotalWeight = weight;
                                                                        p.Weight = weight;
                                                                        p.TransportationID = t1.ID;
                                                                        lp.Add(p);
                                                                    }
                                                                }
                                                            }
                                                            w.ListPackage = lp;
                                                            lw.Add(w);
                                                        }
                                                    }
                                                }
                                                double addfeeVND = 0;
                                                double addfeeCYN = 0;
                                                double sensorVND = 0;
                                                double sensorCYN = 0;

                                                if (!string.IsNullOrEmpty(t1.AdditionFeeVND))
                                                    if (t1.AdditionFeeVND.ToFloat(0) > 0)
                                                        addfeeVND = Convert.ToDouble(t1.AdditionFeeVND);

                                                if (!string.IsNullOrEmpty(t1.AdditionFeeCYN))
                                                    if (t1.AdditionFeeCYN.ToFloat(0) > 0)
                                                        addfeeCYN = Convert.ToDouble(t1.AdditionFeeCYN);

                                                if (!string.IsNullOrEmpty(t1.SensorFeeeVND))
                                                    if (t1.SensorFeeeVND.ToFloat(0) > 0)
                                                        sensorVND = Convert.ToDouble(t1.SensorFeeeVND);

                                                if (!string.IsNullOrEmpty(t1.SensorFeeCYN))
                                                    if (t1.SensorFeeCYN.ToFloat(0) > 0)
                                                        sensorCYN = Convert.ToDouble(t1.SensorFeeCYN);

                                                TotalAdditionFeeCYN += addfeeCYN;
                                                TotalAdditionFeeVND += addfeeVND;

                                                TotalSensoredFeeVND += sensorVND;
                                                TotalSensoredFeeCYN += sensorCYN;
                                            }
                                        }
                                    }                                

                                }
                                double TotalFeeVND = 0;
                                if (acc.FeeTQVNPerWeight.ToFloat(0) > 0)
                                {
                                    TotalFeeVND = Convert.ToDouble(acc.FeeTQVNPerWeight) * totalWeight;
                                    totalWeightPriceVND += TotalFeeVND;
                                }
                                else
                                {
                                    if (lw.Count > 0)
                                    {
                                        foreach (var item in lw)
                                        {
                                            double pricePerKg = 0;
                                            var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                                    item.WareHouseFromID, item.WareHouseID, item.ShippingTypeID, true);
                                            if (fee.Count > 0)
                                            {
                                                foreach (var f in fee)
                                                {
                                                    if (item.TotalWeight > f.WeightFrom && item.TotalWeight <= f.WeightTo)
                                                    {
                                                        //totalWeightPriceVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                        TotalFeeVND = Convert.ToDouble(f.Price) * item.TotalWeight;
                                                        pricePerKg = Convert.ToDouble(f.Price);
                                                        totalWeightPriceVND += TotalFeeVND;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (item.ListPackage != null)
                                            {
                                                foreach (var temp in item.ListPackage)
                                                {
                                                    double price = temp.Weight * pricePerKg;
                                                    TransportationOrderNewController.UpdateTotalPrice(temp.TransportationID, price.ToString(), pricePerKg.ToString());

                                                }
                                            }
                                        }
                                    }
                                }

                                totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                                totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                                //Lưu xuống 1 lượt yêu cầu xuất kho
                                #region Tạo lượt xuất kho
                                string note = txtExNote.Text;
                                int shippingtype = Convert.ToInt32(ddlPTVC.SelectedValue);
                                int totalpackage = listID.Length - 1;
                                string kq = ExportRequestTurnController.InsertWithUID(acc.ID, acc.Username, 0, currentDate, totalPriceVND,
                                    totalPriceCYN, totalWeight, note, shippingtype, currentDate, username_current, totalpackage, 1);
                                string link = "/manager/outstock-finish-user?id=" + kq + "";
                                int eID = kq.ToInt(0);
                                for (int i = 0; i < lID.Count; i++)
                                {
                                    int ID = lID[i];
                                    var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                                    if (t != null)
                                    {
                                        double weight = 0;
                                        if (t.SmallPackageID != null)
                                        {
                                            if (t.SmallPackageID > 0)
                                            {
                                                var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                                if (package != null)
                                                {
                                                    if (package.Status == 3)
                                                    {
                                                        var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                                                        if (check == null)
                                                        {
                                                            RequestOutStockController.InsertT(package.ID,
                                                                package.OrderTransactionCode,
                                                                t.ID,
                                                                Convert.ToInt32(package.MainOrderID), 1,
                                                                DateTime.Now, username_current, eID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Update lại trạng thái từng đơn và ngày ship
                                        TransportationOrderNewController.UpdateRequestOutStock(t.ID, 5, note, currentDate, shippingtype);
                                    }
                                }
                                #endregion

                                // PJUtils.ShowMessageBoxSwAlertBackToLink("Bạn đã gửi yêu cầu xuất kho thành công. Xin chân thành cảm ơn", "s", true, link, Page);
                                Response.Redirect("/manager/outstock-finish-vch?id=" + kq + "");
                            }
                        }

                    }
                }
            }
        }

        public class WareHouse
        {
            public int WareHouseFromID { get; set; }
            public int WareHouseID { get; set; }
            public int ShippingTypeID { get; set; }
            public double TotalWeight { get; set; }
            public List<Package> ListPackage { get; set; }
        }

        public class Package
        {
            public int TransportationID { get; set; }
            public double Weight { get; set; }
        }
    }
}
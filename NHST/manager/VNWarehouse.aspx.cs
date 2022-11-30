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
    public partial class VNWarehouse1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    hdfCurrency.Value = config.AgentCurrency.ToString();
                }

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
            var khotq = WarehouseFromController.GetAllWithIsHidden(false);
            if (khotq.Count > 0)
            {
                foreach (var item in khotq)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlKhoTQ.Items.Add(us);
                }
            }

            var khovn = WarehouseController.GetAllWithIsHidden(false);
            if (khovn.Count > 0)
            {
                foreach (var item in khovn)
                {
                    ListItem us = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlKhoVN.Items.Add(us);
                }
            }

            var shipping = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
            if (shipping.Count > 0)
            {
                foreach (var item in shipping)
                {
                    ListItem us = new ListItem(item.ShippingTypeName, item.ID.ToString());
                    ddlPTVC.Items.Add(us);
                }
            }

            var user = AccountController.GetAll_RoleID("");
            if (user.Count > 0)
            {
                ddlUsername.DataSource = user;
                ddlUsername.DataBind();
            }

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
                            var smallpackages = SmallPackageController.GetBuyBigPackageID(bID, "");
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

                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 1;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";
                                                si.Baohiem = "Không";

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

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();

                                                string Phone = "";

                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                                si.Phone = Phone;
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = tID;
                                        si.TransportationID = tID;
                                        si.OrderTypeInt = 2;
                                        int UID = 0;
                                        string Phone = "";
                                        string Username = "";
                                        var orderTransportation = TransportationOrderNewController.GetByID(Convert.ToInt32(item.TransportationOrderID));
                                        if (orderTransportation != null)
                                        {
                                            var userorder = AccountController.GetByID(orderTransportation.UID.Value);
                                            if (userorder != null)
                                            {
                                                UID = userorder.ID;
                                                Phone = AccountInfoController.GetByUserID(userorder.ID).Phone;
                                                username = userorder.Username;
                                            }

                                            double CuocvattuCYN = 0;
                                            double CuocvattuVND = 0;
                                            double HangDBCYN = 0;
                                            double HangDBVND = 0;

                                            if (orderTransportation.AdditionFeeCYN.ToFloat(0) > 0)
                                            {
                                                HangDBCYN = Convert.ToDouble(orderTransportation.AdditionFeeCYN);
                                            }
                                            if (orderTransportation.AdditionFeeVND.ToFloat(0) > 0)
                                            {
                                                HangDBVND = Convert.ToDouble(orderTransportation.AdditionFeeVND);
                                            }
                                            if (orderTransportation.SensorFeeCYN.ToFloat(0) > 0)
                                            {
                                                CuocvattuCYN = Convert.ToDouble(orderTransportation.SensorFeeCYN);
                                            }
                                            if (orderTransportation.SensorFeeeVND.ToFloat(0) > 0)
                                            {
                                                CuocvattuVND = Convert.ToDouble(orderTransportation.SensorFeeeVND);
                                            }

                                            si.CuocvattuCYN = CuocvattuCYN;
                                            si.CuocvattuVND = CuocvattuVND;
                                            si.HangDBCYN = HangDBCYN;
                                            si.HangDBVND = HangDBVND;

                                        }
                                        si.UID = UID;
                                        si.Phone = Phone;
                                        si.Username = username;
                                        si.Soluongsanpham = item.ProductQuantity;
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 3;
                                        si.Username = "NA";
                                        si.Phone = "NA";
                                        si.Soluongsanpham = "0";
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
                            var smallpackages = SmallPackageController.GetListByOrderTransactionCode(barcode);
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
                                    if (mID > 0)
                                    {
                                        si.OrderType = "Đơn hàng mua hộ";
                                        si.MainorderID = mID;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 1;
                                        var mainorder = MainOrderController.GetAllByID(mID);
                                        if (mainorder != null)
                                        {
                                            int UID = Convert.ToInt32(mainorder.UID);
                                            si.UID = UID;
                                            var acc = AccountController.GetByID(UID);
                                            if (acc != null)
                                            {
                                                si.Username = acc.Username;
                                                si.Wallet = Convert.ToDouble(acc.Wallet);
                                                si.OrderShopCode = mainorder.MainOrderCode;
                                                if (mainorder.IsCheckProduct == true)
                                                    si.Kiemdem = "Có";
                                                else
                                                    si.Kiemdem = "Không";

                                                if (mainorder.IsPacked == true)
                                                    si.Donggo = "Có";
                                                else
                                                    si.Donggo = "Không";
                                                si.Baohiem = "Không";

                                                if (!string.IsNullOrEmpty(item.UserNote))
                                                    si.Khachghichu = item.UserNote;
                                                else
                                                    si.Khachghichu = string.Empty;

                                                if (!string.IsNullOrEmpty(item.Description))
                                                    si.Description = item.Description;
                                                else
                                                    si.Description = string.Empty;

                                                si.Loaisanpham = item.ProductType;

                                                if (!string.IsNullOrEmpty(item.StaffNoteCheck))
                                                    si.NVKiemdem = item.StaffNoteCheck;
                                                else
                                                    si.NVKiemdem = string.Empty;

                                                var orders = OrderController.GetByMainOrderID(mID);
                                                si.Soloaisanpham = orders.Count.ToString();
                                                double totalProductQuantity = 0;
                                                if (orders.Count > 0)
                                                {
                                                    foreach (var p in orders)
                                                    {
                                                        totalProductQuantity += Convert.ToDouble(p.quantity);
                                                    }
                                                }
                                                si.Soluongsanpham = totalProductQuantity.ToString();
                                                string Phone = "";
                                                var ai = AccountInfoController.GetByUserID(acc.ID);
                                                if (ai != null)
                                                {
                                                    si.Fullname = ai.FirstName + " " + ai.LastName;
                                                    si.Email = acc.Email;
                                                    Phone = ai.Phone;
                                                    si.Address = ai.Address;
                                                }
                                                si.Phone = Phone;
                                            }
                                        }
                                    }
                                    else if (tID > 0)
                                    {
                                        si.OrderType = "Đơn hàng VC hộ";
                                        si.MainorderID = tID;
                                        si.TransportationID = tID;
                                        si.OrderTypeInt = 2;
                                        int UID = 0;
                                        string Phone = "";
                                        string Username = "";
                                        var orderTransportation = TransportationOrderNewController.GetByID(Convert.ToInt32(item.TransportationOrderID));
                                        if (orderTransportation != null)
                                        {
                                            var userorder = AccountController.GetByID(orderTransportation.UID.Value);
                                            if (userorder != null)
                                            {
                                                UID = userorder.ID;
                                                Phone = AccountInfoController.GetByUserID(userorder.ID).Phone;
                                                Username = userorder.Username;
                                            }

                                            double CuocvattuCYN = 0;
                                            double CuocvattuVND = 0;
                                            double HangDBCYN = 0;
                                            double HangDBVND = 0;

                                            if (orderTransportation.AdditionFeeCYN.ToFloat(0) > 0)
                                            {
                                                HangDBCYN = Convert.ToDouble(orderTransportation.AdditionFeeCYN);
                                            }
                                            if (orderTransportation.AdditionFeeVND.ToFloat(0) > 0)
                                            {
                                                HangDBVND = Convert.ToDouble(orderTransportation.AdditionFeeVND);
                                            }
                                            if (orderTransportation.SensorFeeCYN.ToFloat(0) > 0)
                                            {
                                                CuocvattuCYN = Convert.ToDouble(orderTransportation.SensorFeeCYN);
                                            }
                                            if (orderTransportation.SensorFeeeVND.ToFloat(0) > 0)
                                            {
                                                CuocvattuVND = Convert.ToDouble(orderTransportation.SensorFeeeVND);
                                            }

                                            si.CuocvattuCYN = CuocvattuCYN;
                                            si.CuocvattuVND = CuocvattuVND;
                                            si.HangDBCYN = HangDBCYN;
                                            si.HangDBVND = HangDBVND;
                                        }
                                        si.UID = UID;
                                        si.Phone = Phone;
                                        si.Username = Username;
                                        si.Soluongsanpham = item.ProductQuantity;
                                    }
                                    else
                                    {
                                        si.OrderType = "Kiện chưa xác định";
                                        si.MainorderID = 0;
                                        si.TransportationID = 0;
                                        si.OrderTypeInt = 3;
                                        si.Username = "NA";
                                        si.Phone = "NA";
                                        si.Soluongsanpham = "0";
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

        [WebMethod]
        public static string UpdateQuantityNew(string barcode, string quantity, int status, int BigPackageID,
           int packageID, double dai, double rong, double cao, string base64, string note,
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
                if (status == 0)
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, BigPackageID, 0, 0, 0);
                    SmallPackageController.UpdateStaffNoteCustdescproducttype(package.ID, nvkiemdem,
                        khachghichu, loaisanpham);
                    SmallPackageController.UpdateNote(package.ID, note);
                    SmallPackageController.UpdateDateCancelWareHouse(package.ID, username_current, currentDate);
                    return "1";
                }
                else
                {
                    SmallPackageController.UpdateWeightStatusAndDateInLasteWareHouseIsLost(package.ID, quantityT, status,
                        currentDate, false, dai, rong, cao);
                    SmallPackageController.UpdateStaffNoteCustdescproducttype(package.ID, nvkiemdem,
                        khachghichu, loaisanpham);

                    int WareHouseID = 0;
                    if (status == 3)
                    {
                        SmallPackageController.UpdateDateInVNWareHouse(package.ID, username_current, currentDate);
                        string currentPlace = "";
                        var acc = AccountController.GetByUsername(username_current);
                        if (acc != null)
                        {
                            var wh = WarehouseController.GetByID(acc.WareHouseVN.Value);
                            if (wh != null)
                            {
                                WareHouseID = wh.ID;
                                currentPlace = wh.WareHouseName;
                            }
                            SmallPackageController.UpdateCurrentPlace(package.ID, currentPlace, WareHouseID);
                            HistoryScanPackageController.Insert(package.ID, username_current, currentDate, currentPlace);
                        }
                    }
                    SmallPackageController.UpdateNote(package.ID, note);

                    string dbIMG = package.ListIMG;
                    string[] listk = { };
                    if (!string.IsNullOrEmpty(package.ListIMG))
                    {
                        listk = dbIMG.Split('|');
                    }
                    string value = base64;
                    string link = "";
                    if (!string.IsNullOrEmpty(value))
                    {
                        string[] listIMG = value.Split('|');
                        for (int i = 0; i < listIMG.Length - 1; i++)
                        {
                            string imageData = listIMG[i];
                            bool ch = listk.Any(x => x == imageData);
                            if (ch == true)
                            {
                                link += imageData + "|";
                            }
                            else
                            {
                                string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/smallpackageIMG/");
                                string date = DateTime.Now.ToString("dd-MM-yyyy");
                                string time = DateTime.Now.ToString("hh:mm tt");
                                Page page = (Page)HttpContext.Current.Handler;
                                //  TextBox txtCampaign = (TextBox)page.FindControl("txtCampaign");
                                string k = i.ToString();
                                string fileNameWitPath = path + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                                string linkIMG = "/Uploads/smallpackageIMG/" + k + "-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                                link += linkIMG + "|";
                                //   string fileNameWitPath = path + s + ".png";
                                byte[] data;
                                string convert;
                                string contenttype;

                                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                                {
                                    using (BinaryWriter bw = new BinaryWriter(fs))
                                    {
                                        if (imageData.Contains("data:image/png"))
                                        {
                                            convert = imageData.Replace("data:image/png;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            contenttype = ".png";
                                            var result = FileUploadCheck.isValidFile(data, contenttype, contenttype); // Validate Header
                                            if (result)
                                            {
                                                bw.Write(data);
                                                link += linkIMG + "|";
                                            }
                                        }
                                        else if (imageData.Contains("data:image/jpeg"))
                                        {
                                            convert = imageData.Replace("data:image/jpeg;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            contenttype = ".jpeg";
                                            var result = FileUploadCheck.isValidFile(data, contenttype, contenttype); // Validate Header
                                            if (result)
                                            {
                                                bw.Write(data);
                                                link += linkIMG + "|";
                                            }
                                        }
                                        else if (imageData.Contains("data:image/gif"))
                                        {
                                            convert = imageData.Replace("data:image/gif;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            contenttype = ".gif";
                                            var result = FileUploadCheck.isValidFile(data, contenttype, contenttype); // Validate Header
                                            if (result)
                                            {
                                                bw.Write(data);
                                                link += linkIMG + "|";
                                            }
                                        }
                                        else if (imageData.Contains("data:image/jpg"))
                                        {
                                            convert = imageData.Replace("data:image/jpg;base64,", String.Empty);
                                            data = Convert.FromBase64String(convert);
                                            contenttype = ".jpg";
                                            var result = FileUploadCheck.isValidFile(data, contenttype, contenttype); // Validate Header
                                            if (result)
                                            {
                                                bw.Write(data);
                                                link += linkIMG + "|";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    SmallPackageController.UpdateIMG(package.ID, link, DateTime.Now, username_current);

                    int bID = Convert.ToInt32(package.BigPackageID);
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
                                    BigPackageController.UpdateStatus(bID, 2, currentDate, username_current);
                                }
                            }
                        }
                    }
                    var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                    if (transportation != null)
                    {
                        int tID = transportation.ID;

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

                        int warehouseFrom = Convert.ToInt32(transportation.WareHouseFromID);
                        int warehouse = Convert.ToInt32(transportation.WareHouseID);
                        int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                        TransportationOrderNewController.UpdateStatus(tID, 4, currentDate, username_current);
                        TransportationOrderNewController.UpdateWeight(tID, quantity, currentDate, username_current);
                        TransportationOrderNewController.UpdateFee(tID, additionfeeCYN.ToString(), additionfeeVND.ToString(),
                                 sensorCYN.ToString(), sensorVND.ToString());
                        TransportationOrderNewController.UpdateDateInVNWareHouse(tID, currentDate);

                        NotificationsController.Inser(transportation.UID.Value,
                                                                          transportation.Username, transportation.ID,
                                                                          "Đơn hàng vận chuyển hộ " + transportation.ID + " đã về kho VN.", 10,
                                                                          currentDate, username_current, true);

                        return "1";
                    }
                    else
                    {
                        return "1";
                    }
                }
            }
            return "none";
        }


        [WebMethod]
        public static string GetpackageID(string packageID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 5)
                    {
                        if (!string.IsNullOrEmpty(packageID))
                        {

                            var sm = SmallPackageController.GetByID(packageID.ToInt(0));
                            if (sm != null)
                            {
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                return serializer.Serialize(sm);
                            }
                            else
                            {
                                return "none";
                            }
                        }
                        else
                        {
                            return "none";
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
                return "none";
            }
        }

        [WebMethod]
        public static string UpdateTransationCode(string ordertransaction, string Description, string Username, string UserPhone, string KhoTQ, string KhoVN, string PTVC)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                double currency = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    currency = Convert.ToDouble(config.Currency);
                }
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                DateTime currentDate = DateTime.UtcNow.AddHours(7);
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 5)
                    {
                        if (HttpContext.Current.Session["userLoginSystem"] != null)
                        {
                            string username = HttpContext.Current.Session["userLoginSystem"].ToString();

                            var checktrancode = SmallPackageController.GetByOrderTransactionCode(ordertransaction);
                            if (checktrancode.MainOrderID == 0 && checktrancode.TransportationOrderID == 0)
                            {
                                var user = AccountController.GetByUsername(username);
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
                                            var getsmallcheck = SmallPackageController.GetByOrderCode(ordertransaction);
                                            if (getsmallcheck.Count == 0)
                                            {

                                                return "existsmallpackage";
                                            }
                                            else
                                            {
                                                var userkh = AccountController.GetByUsername(AccountController.GetByID(Convert.ToInt32(Username)).Username);
                                                if (userkh != null)
                                                {
                                                    string tID = TransportationOrderNewController.Insert1(userkh.ID, userkh.Username, "0", "0", "0", "0", "0", "0", "0",
    "0", "0", "0", 0, ordertransaction, 1, Description, "", "0", "0", DateTime.UtcNow.AddHours(7), username, KhoTQ.ToInt(0), KhoVN.ToInt(0), PTVC.ToInt(0), "");
                                                    int packageID = 0;
                                                    var smallpackage = SmallPackageController.GetByOrderTransactionCode(ordertransaction);
                                                    if (smallpackage != null)
                                                    {
                                                        SmallPackageController.UpdateTransationCodeID(smallpackage.ID, userkh.ID, userkh.Username, tID.ToInt(0), username, currentDate);
                                                        TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), smallpackage.ID);
                                                        //TransportationOrderNewController.UpdateWeight(tID, quantity, currentDate, username_current);
                                                    }

                                                    PackageAll1 pa = new PackageAll1();
                                                    pa.PackageAllType = 0;
                                                    pa.PackageGetCount = 0;
                                                    List<OrderGet1> og = new List<OrderGet1>();
                                                    OrderGet1 o = new OrderGet1();
                                                    o.ID = packageID;
                                                    o.OrderType = "Vận chuyển hộ";
                                                    o.BigPackageID = 0;
                                                    o.BarCode = ordertransaction;
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
                                                    double dai = 0;
                                                    double rong = 0;
                                                    double cao = 0;
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
                        else
                        {
                            return "none";
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
                return "none";
            }
        }


        [WebMethod]
        public static string UpdateLost(int packageID)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                int bID = Convert.ToInt32(package.BigPackageID);
                SmallPackageController.UpdateIsLost(packageID, true, 0);
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
                                BigPackageController.UpdateStatus(bID, 2, currentDate, username_current);
                            }
                        }
                    }
                }
                return "1";
            }
            return "none";
        }


        [WebMethod]
        public static string CheckOrderShopCodeNew(string ordershopcode, string ordertransaction, string Description, string OrderID, int OrderType)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);

                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
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
                                    if (OrderType == 1)
                                    {
                                        if (!string.IsNullOrEmpty(OrderID))
                                        {
                                            var order = MainOrderController.GetAllByID(Convert.ToInt32(OrderID));
                                            if (order != null)
                                            {
                                                int MainOrderID = order.ID;
                                                string temp = "";
                                                if (!string.IsNullOrEmpty(ordertransaction))
                                                    temp = ordertransaction;
                                                else
                                                    temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                                var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                if (getsmallcheck.Count > 0)
                                                {
                                                    return "existsmallpackage";
                                                }
                                                else
                                                {
                                                    string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                                    SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                                    #region Lấy tất cả các cục hiện có trong đơn

                                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                    PackageAll pa = new PackageAll();
                                                    pa.PackageAllType = 0;
                                                    pa.PackageGetCount = smallpackages.Count;
                                                    List<smallpackageitem> og = new List<smallpackageitem>();

                                                    smallpackageitem o = new smallpackageitem();
                                                    o.ID = packageID.ToInt(0);
                                                    o.OrderType = "Đơn hàng mua hộ";
                                                    o.BigPackageID = 0;
                                                    o.BarCode = temp;

                                                    o.Status = 1;
                                                    int mainOrderID = Convert.ToInt32(MainOrderID);
                                                    o.MainorderID = mainOrderID;
                                                    o.OrderShopCode = order.MainOrderCode;
                                                    var orders = OrderController.GetByMainOrderID(MainOrderID);
                                                    o.Soloaisanpham = orders.Count.ToString();
                                                    double totalProductQuantity = 0;
                                                    if (orders.Count > 0)
                                                    {
                                                        foreach (var p in orders)
                                                        {
                                                            totalProductQuantity += Convert.ToDouble(p.quantity);
                                                        }
                                                    }
                                                    o.Soluongsanpham = totalProductQuantity.ToString();
                                                    if (order.IsCheckProduct == true)
                                                        o.Kiemdem = "Có";
                                                    else
                                                        o.Kiemdem = "Không";
                                                    if (order.IsPacked == true)
                                                        o.Donggo = "Có";
                                                    else
                                                        o.Donggo = "Không";


                                                    o.IsTemp = false;
                                                    double dai = 0;
                                                    double rong = 0;
                                                    double cao = 0;

                                                    o.dai = dai;
                                                    o.rong = rong;
                                                    o.cao = cao;
                                                    og.Add(o);
                                                    #endregion
                                                    pa.listPackageGet = og;

                                                    if (smallpackages.Count > 0)
                                                    {
                                                        bool isChuaVekhoTQ = true;
                                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                        if (che >= sp_main.Count)
                                                        {
                                                            isChuaVekhoTQ = false;
                                                        }
                                                        if (isChuaVekhoTQ == false)
                                                        {
                                                            MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                        }
                                                    }
                                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                                    return serializer.Serialize(pa);

                                                    //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                    //if (getsmallcheck.Count > 0)
                                                    //{
                                                    //    return "existsmallpackage";
                                                    //}
                                                    //else
                                                    //{

                                                    //}
                                                }
                                            }
                                            else
                                                return "noteexistordercode";
                                        }
                                        else if (!string.IsNullOrEmpty(ordershopcode))
                                        {
                                            var order = MainOrderController.GetByMainOrderCode(ordershopcode);
                                            if (order != null)
                                            {
                                                int MainOrderID = order.ID;
                                                string temp = "";
                                                if (!string.IsNullOrEmpty(ordertransaction))
                                                    temp = ordertransaction;
                                                else
                                                    temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                                var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                if (getsmallcheck.Count > 0)
                                                {
                                                    return "existsmallpackage";
                                                }
                                                else
                                                {
                                                    string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                                    SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                                    #region Lấy tất cả các cục hiện có trong đơn

                                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                    PackageAll pa = new PackageAll();
                                                    pa.PackageAllType = 0;
                                                    pa.PackageGetCount = smallpackages.Count;
                                                    List<smallpackageitem> og = new List<smallpackageitem>();

                                                    smallpackageitem o = new smallpackageitem();
                                                    o.ID = packageID.ToInt(0);
                                                    o.OrderType = "Đơn hàng mua hộ";
                                                    o.BigPackageID = 0;
                                                    o.BarCode = temp;

                                                    o.Status = 1;
                                                    int mainOrderID = Convert.ToInt32(MainOrderID);
                                                    o.MainorderID = mainOrderID;
                                                    o.OrderShopCode = order.MainOrderCode;
                                                    var orders = OrderController.GetByMainOrderID(MainOrderID);
                                                    o.Soloaisanpham = orders.Count.ToString();
                                                    double totalProductQuantity = 0;
                                                    if (orders.Count > 0)
                                                    {
                                                        foreach (var p in orders)
                                                        {
                                                            totalProductQuantity += Convert.ToDouble(p.quantity);
                                                        }
                                                    }
                                                    o.Soluongsanpham = totalProductQuantity.ToString();
                                                    if (order.IsCheckProduct == true)
                                                        o.Kiemdem = "Có";
                                                    else
                                                        o.Kiemdem = "Không";
                                                    if (order.IsPacked == true)
                                                        o.Donggo = "Có";
                                                    else
                                                        o.Donggo = "Không";


                                                    double dai = 0;
                                                    double rong = 0;
                                                    double cao = 0;

                                                    o.dai = dai;
                                                    o.rong = rong;
                                                    o.cao = cao;
                                                    og.Add(o);
                                                    #endregion
                                                    pa.listPackageGet = og;

                                                    if (smallpackages.Count > 0)
                                                    {
                                                        bool isChuaVekhoTQ = true;
                                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                        if (che >= sp_main.Count)
                                                        {
                                                            isChuaVekhoTQ = false;
                                                        }
                                                        if (isChuaVekhoTQ == false)
                                                        {
                                                            MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                        }
                                                    }
                                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                                    return serializer.Serialize(pa);

                                                    //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                    //if (getsmallcheck.Count > 0)
                                                    //{
                                                    //    return "existsmallpackage";
                                                    //}
                                                    //else
                                                    //{

                                                    //}
                                                }
                                            }
                                            else
                                                return "noteexistordercode";
                                        }
                                        else
                                            return "none";
                                    }
                                    else if (OrderType == 2)
                                    {
                                        if (!string.IsNullOrEmpty(OrderID))
                                        {
                                            var order = TransportationOrderController.GetByID(Convert.ToInt32(OrderID));
                                            if (order != null)
                                            {
                                                int Trans = order.ID;
                                                string temp = "";
                                                if (!string.IsNullOrEmpty(ordertransaction))
                                                    temp = ordertransaction;
                                                else
                                                    temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                                var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                if (getsmallcheck.Count > 0)
                                                {
                                                    return "existsmallpackage";
                                                }
                                                else
                                                {
                                                    string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(0,
                                                0, temp, "", 0, 0, 0, 2, true, Trans, DateTime.Now, username);
                                                    SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                                    #region Lấy tất cả các cục hiện có trong đơn

                                                    var smallpackages = SmallPackageController.GetByTransportationOrderID(Trans);
                                                    PackageAll pa = new PackageAll();
                                                    pa.PackageAllType = 0;
                                                    pa.PackageGetCount = smallpackages.Count;
                                                    List<smallpackageitem> og = new List<smallpackageitem>();

                                                    smallpackageitem o = new smallpackageitem();
                                                    o.ID = packageID.ToInt(0);
                                                    o.OrderType = "Đơn hàng VC hộ";
                                                    o.BigPackageID = 0;
                                                    o.BarCode = temp;

                                                    o.Status = 1;
                                                    int mainOrderID = Convert.ToInt32(Trans);
                                                    o.MainorderID = mainOrderID;
                                                    o.OrderShopCode = "";

                                                    o.Soloaisanpham = "0";
                                                    double totalProductQuantity = 0;

                                                    o.Soluongsanpham = "0";
                                                    //if (order.IsCheckProduct == true)
                                                    //    o.Kiemdem = "Có";
                                                    //else
                                                    o.Kiemdem = "Không";
                                                    //if (order.IsPacked == true)
                                                    //    o.Donggo = "Có";
                                                    //else
                                                    o.Donggo = "Không";


                                                    double dai = 0;
                                                    double rong = 0;
                                                    double cao = 0;

                                                    o.dai = dai;
                                                    o.rong = rong;
                                                    o.cao = cao;
                                                    og.Add(o);
                                                    #endregion
                                                    pa.listPackageGet = og;

                                                    if (smallpackages.Count > 0)
                                                    {
                                                        bool isChuaVekhoTQ = true;
                                                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                                        if (che >= sp_main.Count)
                                                        {
                                                            isChuaVekhoTQ = false;
                                                        }
                                                        if (isChuaVekhoTQ == false)
                                                        {
                                                            MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                                        }
                                                    }
                                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                                    return serializer.Serialize(pa);

                                                    //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                    //if (getsmallcheck.Count > 0)
                                                    //{
                                                    //    return "existsmallpackage";
                                                    //}
                                                    //else
                                                    //{

                                                    //}
                                                }
                                            }
                                            else
                                                return "noteexistordercode";
                                        }
                                        else
                                            return "none";
                                    }
                                    else
                                    {

                                        int MainOrderID = 0;
                                        string temp = "";
                                        if (!string.IsNullOrEmpty(ordertransaction))
                                            temp = ordertransaction;
                                        else
                                            temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                        #region Lấy tất cả các cục hiện có trong đơn
                                        var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                        if (getsmallcheck.Count > 0)
                                        {
                                            return "existsmallpackage";
                                        }
                                        else
                                        {
                                            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                            0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                            SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = 0;
                                            List<smallpackageitem> og = new List<smallpackageitem>();
                                            //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                            smallpackageitem o = new smallpackageitem();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Chưa xác định";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;

                                            o.OrderShopCode = "";

                                            o.Soloaisanpham = "0";
                                            o.Soluongsanpham = "0";
                                            o.Kiemdem = "Không";
                                            o.Donggo = "Không";
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            og.Add(o);

                                            pa.listPackageGet = og;
                                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                            return serializer.Serialize(pa);

                                        }
                                        #endregion
                                    }


                                    //if (!string.IsNullOrEmpty(OrderID))
                                    //{
                                    //    var order = MainOrderController.GetAllByID(Convert.ToInt32(OrderID));
                                    //    if (order != null)
                                    //    {
                                    //        int MainOrderID = order.ID;
                                    //        string temp = "";
                                    //        if (!string.IsNullOrEmpty(ordertransaction))
                                    //            temp = ordertransaction;
                                    //        else
                                    //            temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                    //        var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //        if (getsmallcheck.Count > 0)
                                    //        {
                                    //            return "existsmallpackage";
                                    //        }
                                    //        else
                                    //        {
                                    //            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                    //        0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                    //            SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                    //            #region Lấy tất cả các cục hiện có trong đơn

                                    //            var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    //            PackageAll pa = new PackageAll();
                                    //            pa.PackageAllType = 0;
                                    //            pa.PackageGetCount = smallpackages.Count;
                                    //            List<OrderGet> og = new List<OrderGet>();

                                    //            OrderGet o = new OrderGet();
                                    //            o.ID = packageID.ToInt(0);
                                    //            o.OrderType = "Đơn hàng mua hộ";
                                    //            o.BigPackageID = 0;
                                    //            o.BarCode = temp;
                                    //            o.TotalWeight = "0";
                                    //            o.Status = 1;
                                    //            int mainOrderID = Convert.ToInt32(MainOrderID);
                                    //            o.MainorderID = mainOrderID;
                                    //            o.OrderShopCode = order.MainOrderCode;
                                    //            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                    //            o.Soloaisanpham = orders.Count.ToString();
                                    //            double totalProductQuantity = 0;
                                    //            if (orders.Count > 0)
                                    //            {
                                    //                foreach (var p in orders)
                                    //                {
                                    //                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                    //                }
                                    //            }
                                    //            o.Soluongsanpham = totalProductQuantity.ToString();
                                    //            if (order.IsCheckProduct == true)
                                    //                o.Kiemdem = "Có";
                                    //            else
                                    //                o.Kiemdem = "Không";
                                    //            if (order.IsPacked == true)
                                    //                o.Donggo = "Có";
                                    //            else
                                    //                o.Donggo = "Không";

                                    //            var listb = BigPackageController.GetAllWithStatus(1);
                                    //            if (listb.Count > 0)
                                    //            {
                                    //                o.ListBig = listb;
                                    //            }
                                    //            o.IsTemp = 0;
                                    //            double dai = 0;
                                    //            double rong = 0;
                                    //            double cao = 0;

                                    //            o.dai = dai;
                                    //            o.rong = rong;
                                    //            o.cao = cao;
                                    //            og.Add(o);
                                    //            #endregion
                                    //            pa.listPackageGet = og;

                                    //            if (smallpackages.Count > 0)
                                    //            {
                                    //                bool isChuaVekhoTQ = true;
                                    //                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    //                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                    //                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                    //                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    //                if (che >= sp_main.Count)
                                    //                {
                                    //                    isChuaVekhoTQ = false;
                                    //                }
                                    //                if (isChuaVekhoTQ == false)
                                    //                {
                                    //                    MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                    //                }
                                    //            }
                                    //            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    //            return serializer.Serialize(pa);

                                    //            //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //            //if (getsmallcheck.Count > 0)
                                    //            //{
                                    //            //    return "existsmallpackage";
                                    //            //}
                                    //            //else
                                    //            //{

                                    //            //}
                                    //        }
                                    //    }
                                    //    else
                                    //        return "noteexistordercode";
                                    //}
                                    //else if (!string.IsNullOrEmpty(ordershopcode))
                                    //{
                                    //    var order = MainOrderController.GetByMainOrderCode(ordershopcode);
                                    //    if (order != null)
                                    //    {
                                    //        int MainOrderID = order.ID;
                                    //        string temp = "";
                                    //        if (!string.IsNullOrEmpty(ordertransaction))
                                    //            temp = ordertransaction;
                                    //        else
                                    //            temp = ordershopcode + "-" + PJUtils.GetRandomStringByDateTime();
                                    //        var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //        if (getsmallcheck.Count > 0)
                                    //        {
                                    //            return "existsmallpackage";
                                    //        }
                                    //        else
                                    //        {
                                    //            string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                    //        0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                    //            SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                    //            #region Lấy tất cả các cục hiện có trong đơn

                                    //            var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    //            PackageAll pa = new PackageAll();
                                    //            pa.PackageAllType = 0;
                                    //            pa.PackageGetCount = smallpackages.Count;
                                    //            List<OrderGet> og = new List<OrderGet>();

                                    //            OrderGet o = new OrderGet();
                                    //            o.ID = packageID.ToInt(0);
                                    //            o.OrderType = "Đơn hàng mua hộ";
                                    //            o.BigPackageID = 0;
                                    //            o.BarCode = temp;
                                    //            o.TotalWeight = "0";
                                    //            o.Status = 1;
                                    //            int mainOrderID = Convert.ToInt32(MainOrderID);
                                    //            o.MainorderID = mainOrderID;
                                    //            o.OrderShopCode = order.MainOrderCode;
                                    //            var orders = OrderController.GetByMainOrderID(MainOrderID);
                                    //            o.Soloaisanpham = orders.Count.ToString();
                                    //            double totalProductQuantity = 0;
                                    //            if (orders.Count > 0)
                                    //            {
                                    //                foreach (var p in orders)
                                    //                {
                                    //                    totalProductQuantity += Convert.ToDouble(p.quantity);
                                    //                }
                                    //            }
                                    //            o.Soluongsanpham = totalProductQuantity.ToString();
                                    //            if (order.IsCheckProduct == true)
                                    //                o.Kiemdem = "Có";
                                    //            else
                                    //                o.Kiemdem = "Không";
                                    //            if (order.IsPacked == true)
                                    //                o.Donggo = "Có";
                                    //            else
                                    //                o.Donggo = "Không";

                                    //            var listb = BigPackageController.GetAllWithStatus(1);
                                    //            if (listb.Count > 0)
                                    //            {
                                    //                o.ListBig = listb;
                                    //            }
                                    //            o.IsTemp = 0;
                                    //            double dai = 0;
                                    //            double rong = 0;
                                    //            double cao = 0;

                                    //            o.dai = dai;
                                    //            o.rong = rong;
                                    //            o.cao = cao;
                                    //            og.Add(o);
                                    //            #endregion
                                    //            pa.listPackageGet = og;

                                    //            if (smallpackages.Count > 0)
                                    //            {
                                    //                bool isChuaVekhoTQ = true;
                                    //                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    //                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                    //                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                    //                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    //                if (che >= sp_main.Count)
                                    //                {
                                    //                    isChuaVekhoTQ = false;
                                    //                }
                                    //                if (isChuaVekhoTQ == false)
                                    //                {
                                    //                    MainOrderController.UpdateStatus(mainOrderID, Convert.ToInt32(order.UID), 6);
                                    //                }
                                    //            }
                                    //            JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    //            return serializer.Serialize(pa);

                                    //            //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //            //if (getsmallcheck.Count > 0)
                                    //            //{
                                    //            //    return "existsmallpackage";
                                    //            //}
                                    //            //else
                                    //            //{

                                    //            //}
                                    //        }
                                    //    }
                                    //    else
                                    //        return "noteexistordercode";
                                    //}
                                    //else
                                    //{

                                    //    int MainOrderID = 0;
                                    //    string temp = "";
                                    //    if (!string.IsNullOrEmpty(ordertransaction))
                                    //        temp = ordertransaction;
                                    //    else
                                    //        temp = "00-" + PJUtils.GetRandomStringByDateTime();
                                    //    #region Lấy tất cả các cục hiện có trong đơn
                                    //    var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //    if (getsmallcheck.Count > 0)
                                    //    {
                                    //        return "existsmallpackage";
                                    //    }
                                    //    else
                                    //    {
                                    //        string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                    //        0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);
                                    //        SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                    //        PackageAll pa = new PackageAll();
                                    //        pa.PackageAllType = 0;
                                    //        pa.PackageGetCount = 0;
                                    //        List<OrderGet> og = new List<OrderGet>();
                                    //        //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                    //        OrderGet o = new OrderGet();
                                    //        o.ID = packageID.ToInt(0);
                                    //        o.OrderType = "Chưa xác định";
                                    //        o.BigPackageID = 0;
                                    //        o.BarCode = temp;
                                    //        o.TotalWeight = "0";
                                    //        o.Status = 1;
                                    //        int mainOrderID = Convert.ToInt32(MainOrderID);
                                    //        o.MainorderID = mainOrderID;
                                    //        o.TransportationID = 0;
                                    //        o.OrderShopCode = "";
                                    //        //var orders = OrderController.GetByMainOrderID(MainOrderID);

                                    //        //double totalProductQuantity = 0;
                                    //        //if (orders.Count > 0)
                                    //        //{
                                    //        //    foreach (var p in orders)
                                    //        //    {
                                    //        //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                    //        //    }
                                    //        //}
                                    //        o.Soloaisanpham = "0";
                                    //        o.Soluongsanpham = "0";
                                    //        o.Kiemdem = "Không";
                                    //        o.Donggo = "Không";
                                    //        var listb = BigPackageController.GetAllWithStatus(1);
                                    //        if (listb.Count > 0)
                                    //        {
                                    //            o.ListBig = listb;
                                    //        }
                                    //        o.IsTemp = 0;
                                    //        double dai = 0;
                                    //        double rong = 0;
                                    //        double cao = 0;

                                    //        o.dai = dai;
                                    //        o.rong = rong;
                                    //        o.cao = cao;
                                    //        og.Add(o);

                                    //        pa.listPackageGet = og;
                                    //        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    //        return serializer.Serialize(pa);
                                    //        //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                    //        //if (getsmallcheck.Count > 0)
                                    //        //{
                                    //        //    return "existsmallpackage";
                                    //        //}
                                    //        //else
                                    //        //{

                                    //        //}
                                    //    }
                                    //    #endregion
                                    //}
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
                            return "none";
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
                return "none";
            }
        }



        #region Class      
        //public class OrderGet
        //{
        //    public int ID { get; set; }
        //    public int MainorderID { get; set; }
        //    public int UID { get; set; }
        //    public string Username { get; set; }
        //    public double Wallet { get; set; }
        //    public string OrderShopCode { get; set; }
        //    public string BarCode { get; set; }
        //    public string TotalWeight { get; set; }
        //    public string TotalPriceVND { get; set; }
        //    public double TotalPriceVNDNum { get; set; }
        //    public int Status { get; set; }
        //    public string Fullname { get; set; }
        //    public string Email { get; set; }
        //    public string Phone { get; set; }
        //    public string Address { get; set; }
        //    public string Kiemdem { get; set; }
        //    public string Donggo { get; set; }
        //} 
        public class PackageAll
        {
            public int PackageAllType { get; set; }
            public string OrderCode { get; set; }
            public int PackageGetCount { get; set; }
            public List<smallpackageitem> listPackageGet { get; set; }
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
            public List<tbl_BigPackage> ListBig { get; set; }
            public int IsTemp { get; set; }
            public int IsThatlac { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
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
        #endregion


        #region Class      
        public class SmallPakge1
        {
            public int ID { get; set; }
            public string OrderTransactionCode { get; set; }
            public List<SmallPakgetrancode1> SmallPakgetrancode { get; set; }
        }
        public class SmallPakgetrancode1
        {
            public int ID { get; set; }
            public string OrderTransactionCode { get; set; }

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
        public class BigPackageItem1
        {
            public int BigpackageID { get; set; }
            public string BigpackageCode { get; set; }
            public int BigpackageSmallPackageCount { get; set; }
            public int BigpackageType { get; set; }
            public List<smallpackageitem1> Smallpackages { get; set; }
        }
        public class smallpackageitem1
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
        #endregion

        [WebMethod]
        public static string PriceBarcode(string barcode)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 5)
                    {
                        if (!string.IsNullOrEmpty(barcode))
                        {
                            BillInfo b = new BillInfo();
                            string Username = "";
                            string Phone = "";
                            b.Weight = string.Empty;
                            var sm = SmallPackageController.GetByOrderTransactionCode(barcode);
                            if (sm != null)
                            {
                                if (Convert.ToDouble(sm.Weight) > 0)
                                    b.Weight = sm.Weight.ToString();
                                if (Convert.ToInt32(sm.MainOrderID) > 0)
                                {
                                    var main = MainOrderController.GetAllByID(Convert.ToInt32(sm.MainOrderID));
                                    if (main != null)
                                    {
                                        var ac = AccountController.GetByID(main.UID.Value);
                                        if (ac != null)
                                        {
                                            Username = ac.Username;
                                            Phone = AccountInfoController.GetByUserID(ac.ID).Phone;
                                        }
                                    }
                                }
                                else if (Convert.ToInt32(sm.TransportationOrderID) > 0)
                                {
                                    var trans = TransportationOrderController.GetByID(Convert.ToInt32(sm.TransportationOrderID));
                                    if (trans != null)
                                    {
                                        var ac = AccountController.GetByID(trans.UID.Value);
                                        if (ac != null)
                                        {
                                            Username = ac.Username;
                                            Phone = AccountInfoController.GetByUserID(ac.ID).Phone;
                                        }
                                    }
                                }
                            }


                            b.Username = Username;
                            b.Phone = Phone;


                            string barcodeIMG = "/Uploads/smallpackagebarcode/" + barcode + ".Png";
                            System.Drawing.Image barCode = PJUtils.MakeBarcodeImage(barcode, 2, true);
                            barCode.Save(HttpContext.Current.Server.MapPath("" + barcodeIMG + ""), ImageFormat.Png);
                            b.Barcode = barcode;
                            b.BarcodeURL = barcodeIMG;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(b);
                            //return barcodeIMG;
                        }
                        else
                        {
                            return "";
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
                return "none";
            }

        }

        public class BillInfo
        {
            public string Barcode { get; set; }
            public string BarcodeURL { get; set; }
            public string Weight { get; set; }
            public string Phone { get; set; }
            public string Username { get; set; }
        }
    }
}
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
    public partial class TQWareHouse_DHH : System.Web.UI.Page
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
                    if (ac.RoleID != 4 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadDDL();
            }
        }

        public void LoadDDL()
        {
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
        public static string GetCode(string barcode)
        {
            DateTime currentDate = DateTime.Now;
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        var packages = SmallPackageController.GetListByOrderTransactionCode(barcode.Trim());
                        if (packages.Count > 0)
                        {
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 0;
                            List<PackageAll> palls = new List<PackageAll>();
                            PackageAll pa = new PackageAll();
                            pa.PackageAllType = 0;
                            pa.PackageGetCount = packages.Count;
                            List<OrderGet> og = new List<OrderGet>();
                            foreach (var package in packages)
                            {
                                OrderGet o = new OrderGet();
                                if (package.Status == 0)
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 0, currentDate, username);
                                }
                                else
                                {
                                    SmallPackageController.UpdateStatus(package.ID, 2, currentDate, username);
                                    SmallPackageController.UpdateDateInTQWareHouse(package.ID, username, currentDate);
                                }

                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                                if (mainorder != null)
                                {
                                    int MainorderID = mainorder.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
                                    //if (smallpackages.Count > 0)
                                    //{
                                    //    bool isChuaVekhoTQ = true;
                                    //    var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                                    //    var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                                    //    var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                                    //    double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                                    //    if (che >= sp_main.Count)
                                    //    {
                                    //        isChuaVekhoTQ = false;
                                    //    }
                                    //    if (isChuaVekhoTQ == false)
                                    //    {
                                    //        // MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);

                                    //        //         var setNoti = SendNotiEmailController.GetByID(8);
                                    //        //         if (setNoti != null)
                                    //        //         {
                                    //        //             var acc = AccountController.GetByID(mainorder.UID.Value);
                                    //        //             if (acc != null)
                                    //        //             {
                                    //        //                 if (setNoti.IsSentNotiUser == true)
                                    //        //                 {
                                    //        //                     if (mainorder.OrderType == 1)
                                    //        //                     {
                                    //        //                         NotificationsController.Inser(acc.ID, acc.Username, MainorderID, "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.",
                                    //        //1, currentDate, username, true);
                                    //        //                     }
                                    //        //                     else
                                    //        //                     {
                                    //        //                         NotificationsController.Inser(acc.ID, acc.Username, MainorderID, "Hàng của đơn hàng TMĐT " + MainorderID + " đã về kho TQ.",
                                    //        //11, currentDate, username, true);
                                    //        //                     }
                                    //        //                 }

                                    //        //                 if (setNoti.IsSendEmailUser == true)
                                    //        //                 {
                                    //        //                     try
                                    //        //                     {
                                    //        //                         PJUtils.SendMailGmail_new( acc.Email,
                                    //        //                             "Thông báo tại NHẬP HÀNG 360.", "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.", "");
                                    //        //                     }
                                    //        //                     catch { }
                                    //        //                 }
                                    //        //             }

                                    //        //         }
                                    //    }
                                    //}

                                    int UID = 0;
                                    string Phone = "";
                                    string Username = "";

                                    var userorder = AccountController.GetByID(mainorder.UID.Value);
                                    if (userorder != null)
                                    {
                                        UID = userorder.ID;
                                        Phone = AccountInfoController.GetByUserID(userorder.ID).Phone;
                                        Username = userorder.Username;
                                    }

                                    o.UID = UID;
                                    o.Phone = Phone;
                                    o.Username = Username;

                                    o.ID = package.ID;
                                    o.OrderType = "Đơn hàng mua hộ";
                                    o.OrderTypeInt = 1;
                                    o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                    o.BarCode = package.OrderTransactionCode;
                                    o.TotalWeight = Math.Round(Convert.ToDouble(package.Weight), 2).ToString();
                                    o.Status = 2;
                                    o.IMG = package.ListIMG;
                                    int mainOrderID = Convert.ToInt32(package.MainOrderID);
                                    o.MainorderID = mainOrderID;
                                    o.TransportationID = 0;
                                    o.OrderShopCode = mainorder.MainOrderCode;
                                    var orders = OrderController.GetByMainOrderID(mainOrderID);
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
                                    if (mainorder.IsCheckProduct == true)
                                        o.Kiemdem = "Có";
                                    else
                                        o.Kiemdem = "Không";
                                    if (mainorder.IsPacked == true)
                                        o.Donggo = "Có";
                                    else
                                        o.Donggo = "Không";
                                    
                                    if (mainorder.IsInsurrance == true)
                                        o.Baohiem = "Có";
                                    else
                                        o.Baohiem = "Không";

                                    if (!string.IsNullOrEmpty(package.UserNote))
                                        o.Khachghichu = package.UserNote;
                                    else
                                        o.Khachghichu = string.Empty;

                                    if (!string.IsNullOrEmpty(package.Description))
                                        o.Note = package.Description;
                                    else
                                        o.Note = string.Empty;
                                    o.Loaisanpham = package.ProductType;
                                    if (!string.IsNullOrEmpty(package.StaffNoteCheck))
                                        o.NVKiemdem = package.StaffNoteCheck;
                                    else
                                        o.NVKiemdem = string.Empty;
                                    var listb = BigPackageController.GetAllWithStatus(1);
                                    if (listb.Count > 0)
                                    {
                                        o.ListBig = listb;
                                    }
                                    o.IsTemp = 0;

                                    double dai = 0;
                                    double rong = 0;
                                    double cao = 0;
                                    if (package.Length != null)
                                    {
                                        dai = Convert.ToDouble(package.Length);
                                    }
                                    if (package.Width != null)
                                    {
                                        rong = Convert.ToDouble(package.Width);
                                    }
                                    if (package.Height != null)
                                    {
                                        cao = Convert.ToDouble(package.Height);
                                    }
                                    o.dai = dai;
                                    o.rong = rong;
                                    o.cao = cao;


                                    og.Add(o);
                                }
                                else
                                {
                                    var orderTransportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                                    if (orderTransportation != null)
                                    {
                                        int tID = orderTransportation.ID;

                                        int UID = 0;
                                        string Phone = "";
                                        string Username = "";

                                        var userorder = AccountController.GetByID(orderTransportation.UID.Value);
                                        if (userorder != null)
                                        {
                                            UID = userorder.ID;
                                            Phone = AccountInfoController.GetByUserID(userorder.ID).Phone;
                                            username = userorder.Username;
                                        }

                                        o.UID = UID;
                                        o.Phone = Phone;
                                        o.Username = username;

                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = Math.Round(Convert.ToDouble(package.Weight), 2).ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = tID;
                                        o.TransportationID = tID;
                                        o.IMG = package.ListIMG;
                                        o.OrderType = "Đơn hàng VC hộ";
                                        o.OrderTypeInt = 2;
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.Soluongsanpham = package.ProductQuantity;
                                        string kiemdem = "Không";
                                        string donggo = "Không";
                                        string baohiem = "Không";
                                        if (package.IsCheckProduct == true)
                                            kiemdem = "Có";
                                        if (package.IsPackaged == true)
                                            donggo = "Có";
                                        if (package.IsInsurrance == true)
                                            baohiem = "Có";
                                        o.Kiemdem = kiemdem;
                                        o.Donggo = donggo;
                                        o.Baohiem = baohiem;

                                        if (!string.IsNullOrEmpty(package.UserNote))
                                            o.Khachghichu = package.UserNote;
                                        else
                                            o.Khachghichu = string.Empty;

                                        if (!string.IsNullOrEmpty(package.Description))
                                            o.Note = package.Description;
                                        else
                                            o.Note = string.Empty;
                                        o.Loaisanpham = package.ProductType;

                                        if (!string.IsNullOrEmpty(package.StaffNoteCheck))
                                            o.NVKiemdem = package.StaffNoteCheck;
                                        else
                                            o.NVKiemdem = string.Empty;

                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }

                                        o.KhoTQ = WarehouseFromController.GetByID(orderTransportation.WareHouseFromID.Value).WareHouseName;
                                        o.KhoVN = WarehouseController.GetByID(orderTransportation.WareHouseID.Value).WareHouseName;
                                        o.PTVC = ShippingTypeToWareHouseController.GetByID(orderTransportation.ShippingTypeID.Value).ShippingTypeName;
                                        o.IsTemp = 0;
                                        og.Add(o);
                                    }
                                    else
                                    {

                                        int UID = 0;
                                        string Phone = "N/A";
                                        string Username = "N/A";

                                        o.UID = UID;
                                        o.Phone = Phone;
                                        o.Username = Username;

                                        o.ID = package.ID;
                                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                                        o.BarCode = package.OrderTransactionCode;
                                        o.TotalWeight = Math.Round(Convert.ToDouble(package.Weight), 2).ToString();
                                        o.Status = Convert.ToInt32(package.Status);
                                        o.MainorderID = 0;
                                        o.IMG = package.ListIMG;
                                        o.TransportationID = 0;
                                        o.OrderType = "Chưa xác định";
                                        o.OrderTypeInt = 3;
                                        o.OrderShopCode = "";
                                        o.Soloaisanpham = "";
                                        o.Soluongsanpham = "";
                                        o.Kiemdem = "Không";
                                        o.Donggo = "Không";
                                        o.Baohiem = "Không";

                                        if (!string.IsNullOrEmpty(package.UserNote))
                                            o.Khachghichu = package.UserNote;
                                        else
                                            o.Khachghichu = string.Empty;

                                        if (!string.IsNullOrEmpty(package.Description))
                                            o.Note = package.Description;
                                        else
                                            o.Note = string.Empty;

                                        o.Loaisanpham = package.ProductType;

                                        if (!string.IsNullOrEmpty(package.StaffNoteCheck))
                                            o.NVKiemdem = package.StaffNoteCheck;
                                        else
                                            o.NVKiemdem = string.Empty;

                                        var listb = BigPackageController.GetAllWithStatus(1);
                                        if (listb.Count > 0)
                                        {
                                            o.ListBig = listb;
                                        }
                                        o.IsTemp = 0;
                                        double dai = 0;
                                        double rong = 0;
                                        double cao = 0;
                                        if (package.Length != null)
                                        {
                                            dai = Convert.ToDouble(package.Length);
                                        }
                                        if (package.Width != null)
                                        {
                                            rong = Convert.ToDouble(package.Width);
                                        }
                                        if (package.Height != null)
                                        {
                                            cao = Convert.ToDouble(package.Height);
                                        }
                                        o.dai = dai;
                                        o.rong = rong;
                                        o.cao = cao;
                                        o.KhoTQ = string.Empty;
                                        o.KhoVN = string.Empty;
                                        o.PTVC = string.Empty;
                                        og.Add(o);
                                    }
                                }
                            }
                            pa.listPackageGet = og;
                            palls.Add(pa);
                            bo.Pall = palls;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(bo);
                        }
                        else
                        {
                            var listorders = MainOrderController.GetListByMainOrderCode(barcode);
                            BigPackOut bo = new BigPackOut();
                            bo.BigPackOutType = 1;
                            if (listorders.Count > 0)
                            {
                                List<PackageAll> palls = new List<PackageAll>();
                                foreach (var order in listorders)
                                {
                                    #region Lấy tất cả các cục hiện có trong đơn
                                    int MainOrderID = order.ID;
                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    PackageAll pa = new PackageAll();
                                    pa.MainOrderID = MainOrderID;
                                    pa.PackageAllType = 1;
                                    pa.PackageGetCount = smallpackages.Count;
                                    List<OrderGet> og = new List<OrderGet>();
                                    if (smallpackages.Count > 0)
                                    {
                                        foreach (var s in smallpackages)
                                        {
                                            OrderGet o = new OrderGet();

                                            int UID = 0;
                                            string Phone = "";
                                            string Username = "";

                                            var userorder = AccountController.GetByID(order.UID.Value);
                                            if (userorder != null)
                                            {
                                                UID = userorder.ID;
                                                Phone = AccountInfoController.GetByUserID(userorder.ID).Phone;
                                                Username = userorder.Username;
                                            }

                                            o.UID = UID;
                                            o.Phone = Phone;
                                            o.Username = Username;

                                            o.OrderType = "Đơn hàng mua hộ";
                                            o.OrderTypeInt = 1;
                                            o.ID = s.ID;
                                            o.BigPackageID = Convert.ToInt32(s.BigPackageID);
                                            o.BarCode = s.OrderTransactionCode;
                                            o.TotalWeight = Math.Round(Convert.ToDouble(s.Weight), 2).ToString();
                                            o.Status = 2;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
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
                                            if (order.IsInsurrance == true)
                                                o.Baohiem = "Có";
                                            else
                                                o.Baohiem = "Không";
                                            //o.Baohiem = "Không";

                                            if (!string.IsNullOrEmpty(s.UserNote))
                                                o.Khachghichu = s.UserNote;
                                            else
                                                o.Khachghichu = string.Empty;

                                            if (!string.IsNullOrEmpty(s.Description))
                                                o.Note = s.Description;
                                            else
                                                o.Note = string.Empty;
                                            o.Loaisanpham = s.ProductType;

                                            if (!string.IsNullOrEmpty(s.StaffNoteCheck))
                                                o.NVKiemdem = s.StaffNoteCheck;
                                            else
                                                o.NVKiemdem = string.Empty;

                                            var listb = BigPackageController.GetAllWithStatus(1);
                                            if (listb.Count > 0)
                                            {
                                                o.ListBig = listb;
                                            }
                                            o.IsTemp = 0;
                                            double dai = 0;
                                            double rong = 0;
                                            double cao = 0;
                                            if (s.Length != null)
                                            {
                                                dai = Convert.ToDouble(s.Length);
                                            }
                                            if (s.Width != null)
                                            {
                                                rong = Convert.ToDouble(s.Width);
                                            }
                                            if (s.Height != null)
                                            {
                                                cao = Convert.ToDouble(s.Height);
                                            }
                                            o.dai = dai;
                                            o.rong = rong;
                                            o.cao = cao;
                                            o.IMG = s.ListIMG;
                                            og.Add(o);
                                        }
                                    }
                                    #endregion  
                                    pa.listPackageGet = og;
                                    palls.Add(pa);

                                }
                                bo.Pall = palls;
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                return serializer.Serialize(bo);
                            }

                        }
                    }
                }
            }
            return "none";
        }

        [WebMethod]
        public static string UpdateQuantity_old(string barcode, string quantity, int status, int BigPackageID, int packageID,
         double dai, double rong, double cao, string nvkiemdem, string khachghichu, string loaisanpham, string base64, string description)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            double quantt = 0;
            if (quantity.ToFloat(0) > 0)
                quantt = Math.Round(Convert.ToDouble(quantity), 2);
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                if (status == 0)
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, BigPackageID, 0, 0, 0);
                    SmallPackageController.UpdateStaffNoteCustdescproducttype(package.ID, nvkiemdem,
                        khachghichu, loaisanpham);
                    SmallPackageController.UpdateNote(package.ID, description);
                    return "1";
                }
                else
                {
                    string dbIMG = package.ListIMG;
                    string[] listk = { };
                    if (!string.IsNullOrEmpty(package.ListIMG))
                    {
                        listk = dbIMG.Split('|');
                    }
                    string value = base64;
                    string link = "";
                    int countsmall = 0;
                    if (!string.IsNullOrEmpty(value))
                    {
                        string[] listIMG = value.Split('|');
                        countsmall = listIMG.Length;
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
                    SmallPackageController.UpdateWeightStatus(package.ID, quantt, status, BigPackageID, dai, rong, cao);
                    SmallPackageController.UpdateStaffNoteCustdescproducttype(package.ID, nvkiemdem,
                        khachghichu, loaisanpham);
                    SmallPackageController.UpdateNote(package.ID, description);
                    if (status == 2)
                    {
                        SmallPackageController.UpdateDateInTQWareHouse(package.ID, username_current, currentDate);

                        string currentPlace = "";
                        int WareHouseID = 0;
                        var acc = AccountController.GetByUsername(username_current);
                        if (acc != null)
                        {
                            var wh = WarehouseFromController.GetByID(acc.WareHouseTQ.Value);
                            if (wh != null)
                            {
                                currentPlace = wh.WareHouseName;
                                WareHouseID = wh.ID;
                            }
                            SmallPackageController.UpdateCurrentPlace(package.ID, currentPlace, WareHouseID);
                            HistoryScanPackageController.Insert(package.ID, username_current, currentDate, currentPlace);
                        }
                    }
                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                    if (mainorder != null)
                    {
                        int orderID = mainorder.ID;
                        int warehouse = mainorder.ReceivePlace.ToInt(1);
                        int shipping = Convert.ToInt32(mainorder.ShippingType);
                        int warehouseFrom = Convert.ToInt32(mainorder.FromPlace);

                        bool checkIsChinaCome = true;
                        double totalweight = quantt;
                        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                        if (packages.Count > 0)
                        {
                            foreach (var p in packages)
                            {
                                if (p.Status < 2)
                                    checkIsChinaCome = false;
                                if (p.OrderTransactionCode != barcode)
                                {
                                    totalweight += Convert.ToDouble(p.Weight);
                                }
                            }
                        }
                        var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

                        double FeeWeight = 0;
                        double FeeWeightDiscount = 0;
                        double ckFeeWeight = 0;
                        ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                        double returnprice = 0;
                        double pricePerWeight = 0;
                        double finalPriceOfPackage = 0;
                        double totalWeight = 0;

                        var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                        if (smallpackage.Count > 0)
                        {
                            foreach (var item in smallpackage)
                            {
                                double totalWeightCN = Math.Round(Convert.ToDouble(item.Weight), 2);
                                double totalWeightTT = 0;
                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    totalWeightTT = (pDai * pRong * pCao) / 7000;
                                }
                                if (totalWeightCN > totalWeightTT)
                                {
                                    totalWeight += totalWeightCN;
                                }
                                else
                                {
                                    totalWeight += totalWeightTT;
                                }
                            }
                            totalWeight = Math.Round(totalWeight, 5);
                            if (!string.IsNullOrEmpty(usercreate.FeeTQVNPerWeight))
                            {
                                double feetqvn = 0;
                                if (usercreate.FeeTQVNPerWeight.ToFloat(0) > 0)
                                {
                                    feetqvn = Convert.ToDouble(usercreate.FeeTQVNPerWeight);
                                }
                                returnprice = totalWeight * feetqvn;
                            }
                            else
                            {
                                var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom, warehouse, shipping, false);
                                if (fee.Count > 0)
                                {
                                    foreach (var f in fee)
                                    {
                                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                        {
                                            pricePerWeight = Convert.ToDouble(f.Price);
                                            returnprice = totalWeight * Convert.ToDouble(f.Price);
                                            break;
                                        }
                                    }
                                }
                            }


                            foreach (var item in smallpackage)
                            {
                                double compareweight = 0;
                                double compareSize = 0;

                                double weight = Math.Round(Convert.ToDouble(item.Weight), 2);
                                compareweight = weight * pricePerWeight;

                                double weigthTT = 0;
                                double pDai = Convert.ToDouble(item.Length);
                                double pRong = Convert.ToDouble(item.Width);
                                double pCao = Convert.ToDouble(item.Height);
                                if (pDai > 0 && pRong > 0 && pCao > 0)
                                {
                                    weigthTT = (pDai * pRong * pCao) / 7000;
                                }
                                weigthTT = Math.Round(weigthTT, 1);
                                compareSize = weigthTT * pricePerWeight;

                                if (compareweight >= compareSize)
                                {
                                    finalPriceOfPackage += compareweight;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareweight);
                                }
                                else
                                {
                                    finalPriceOfPackage += compareSize;
                                    SmallPackageController.UpdateTotalPrice(item.ID, compareSize);
                                }
                            }
                        }
                        double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                        //FeeWeight = returnprice * currency;
                        returnprice = Math.Round(finalPriceOfPackage, 0);
                        FeeWeight = returnprice;
                        FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
                        FeeWeight = FeeWeight - FeeWeightDiscount;

                        double FeeShipCN = Math.Round(Convert.ToDouble(mainorder.FeeShipCN), 0);
                        double FeeBuyPro = Math.Round(Convert.ToDouble(mainorder.FeeBuyPro), 0);
                        double IsCheckProductPrice = Math.Round(Convert.ToDouble(mainorder.IsCheckProductPrice), 0);
                        double IsPackedPrice = Math.Round(Convert.ToDouble(mainorder.IsPackedPrice), 0);
                        double IsFastDeliveryPrice = Math.Round(Convert.ToDouble(mainorder.IsFastDeliveryPrice), 0);
                        double InsuranceMoney = Math.Round(Convert.ToDouble(mainorder.InsuranceMoney), 0);
                        double isfastprice = 0;
                        if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                            isfastprice = Math.Round(Convert.ToDouble(mainorder.IsFastPrice), 0);
                        double pricenvd = 0;
                        if (!string.IsNullOrEmpty(mainorder.PriceVND))
                            pricenvd = Math.Round(Convert.ToDouble(mainorder.PriceVND), 0);
                        double Deposit = Math.Round(Convert.ToDouble(mainorder.Deposit), 0);

                        double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                     + IsFastDeliveryPrice + isfastprice + pricenvd + InsuranceMoney;
                        TotalPriceVND = Math.Round(TotalPriceVND, 0);
                        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                            IsCheckProductPrice.ToString(),
                            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                
                        MainOrderController.UpdateTotalWeight(mainorder.ID, totalWeight.ToString(), totalWeight.ToString());
                        var accChangeData = AccountController.GetByUsername(username_current);
                        if (accChangeData != null)
                        {
                            if (status == 2)
                            {
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                               + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                            }

                            if (Convert.ToDouble(package.Weight) != Convert.ToDouble(quantity))
                            {
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi cân nặng của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", từ: " + package.Weight + " , sang: " + quantity + "", 8, currentDate);
                            }


                            if (checkIsChinaCome == true)
                            {
                                int MainorderID = mainorder.ID;
                                //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                var smallpackages = SmallPackageController.GetByMainOrderID(MainorderID);
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
                                        if (mainorder.Status <= 6)
                                        {
                                            if (mainorder.DateTQ == null)
                                            {
                                                MainOrderController.UpdateDateTQ(mainorder.ID, currentDate);
                                            }
                                            MainOrderController.UpdateStatus(MainorderID, Convert.ToInt32(mainorder.UID), 6);

                                            var setNoti = SendNotiEmailController.GetByID(8);
                                            if (setNoti != null)
                                            {
                                                var acc = AccountController.GetByID(mainorder.UID.Value);
                                                if (acc != null)
                                                {
                                                    if (setNoti.IsSentNotiUser == true)
                                                    {
                                                        if (mainorder.OrderType == 1)
                                                        {
                                                            NotificationsController.Inser(acc.ID, acc.Username, MainorderID, "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.",
                                   1, currentDate, username_current, true);
                                                        }
                                                        else
                                                        {
                                                            NotificationsController.Inser(acc.ID, acc.Username, MainorderID, "Hàng của đơn hàng TMĐT " + MainorderID + " đã về kho TQ.",
                                   11, currentDate, username_current, true);
                                                        }
                                                    }

                                                    if (setNoti.IsSendEmailUser == true)
                                                    {
                                                        try
                                                        {
                                                            PJUtils.SendMailGmail_new( acc.Email,
                                                                "Thông báo tại NHẬP HÀNG 360.", "Hàng của đơn hàng " + MainorderID + " đã về kho TQ.", "");
                                                        }
                                                        catch { }
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }
                                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                            }
                        }

                        return "1";
                    }
                    else
                    {
                        var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (transportation != null)
                        {
                            int tID = transportation.ID;
                            int warehouseFrom = Convert.ToInt32(transportation.WareHouseFromID);
                            int warehouse = Convert.ToInt32(transportation.WareHouseID);
                            int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                            bool checkIsChinaCome = true;
                            double totalweight = 0;
                            var packages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (packages.Count > 0)
                            {
                                foreach (var p in packages)
                                {
                                    if (p.Status < 2)
                                        checkIsChinaCome = false;
                                    if (p.Status >= 2)
                                    {
                                        double totalWeightCN = Math.Round(Convert.ToDouble(p.Weight), 2);
                                        double totalWeightTT = 0;
                                        double pDai = Convert.ToDouble(p.Length);
                                        double pRong = Convert.ToDouble(p.Width);
                                        double pCao = Convert.ToDouble(p.Height);
                                        if (pDai > 0 && pRong > 0 && pCao > 0)
                                        {
                                            totalWeightTT = (pDai * pRong * pCao) / 7000;
                                        }
                                        if (totalWeightCN > totalWeightTT)
                                        {
                                            totalweight += totalWeightCN;
                                        }
                                        else
                                        {
                                            totalweight += totalWeightTT;
                                        }
                                    }
                                }
                            }
                            totalweight = Math.Round(totalweight, 5);
                            //SmallPackageController.UpdateWeight(Convert.ToInt32(transportation.SmallPackageID), totalweight);
                            var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                            double returnprice = 0;
                            double pricePerWeight = 0;
                            double finalPriceOfPackage = 0;
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
                                if (item.Status >= 2)
                                {
                                    double compareweight = 0;
                                    double compareSize = 0;

                                    double weight = Math.Round(Convert.ToDouble(item.Weight), 2);
                                    compareweight = weight * pricePerWeight;

                                    double weigthTT = 0;
                                    double pDai = Convert.ToDouble(item.Length);
                                    double pRong = Convert.ToDouble(item.Width);
                                    double pCao = Convert.ToDouble(item.Height);
                                    if (pDai > 0 && pRong > 0 && pCao > 0)
                                    {
                                        weigthTT = (pDai * pRong * pCao) / 7000;
                                    }
                                    compareSize = weigthTT * pricePerWeight;

                                    if (compareweight >= compareSize)
                                    {
                                        finalPriceOfPackage += compareweight;
                                        SmallPackageController.UpdateTotalPrice(item.ID, compareweight);
                                    }
                                    else
                                    {
                                        finalPriceOfPackage += compareSize;
                                        SmallPackageController.UpdateTotalPrice(item.ID, compareSize);
                                    }
                                }
                            }

                            double totalPriceVND = Math.Round(finalPriceOfPackage, 0);
                            TransportationOrderNewController.UpdateTotalWeightTotalPrice(tID, totalweight.ToString(), totalPriceVND.ToString(), currentDate, username_current);

                            double currency = Convert.ToDouble(transportation.Currency);
                            double totalPriceCYN = 0;
                            totalPriceCYN = returnprice / currency;
                            totalPriceCYN = Math.Round(totalPriceCYN, 2);
                            var accChangeData = AccountController.GetByUsername(username_current);
                            if (accChangeData != null)
                            {
                                if (checkIsChinaCome == true)
                                {
                                    //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
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
                                            TransportationOrderNewController.UpdateStatus(tID, 3, currentDate, username_current);
                                        }
                                    }
                                }
                            }
                            //TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
                            return "1";
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    return "none";
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

        //protected void btncreateuser_Click(object sender, EventArgs e)
        //{
        //    if (!Page.IsValid) return;
        //    string username_current = Session["userLoginSystem"].ToString();
        //    string code = txtPackageCode.Text.Trim();
        //    var check = BigPackageController.GetByPackageCode(code);
        //    if (check != null)
        //    {
        //        PJUtils.ShowMessageBoxSwAlert("Mã bao hàng đã tồn tại.", "e", false, Page);
        //    }
        //    else
        //    {
        //        double volume = 0;
        //        double weight = 0;
        //        if (pVolume.Value > 0)
        //            volume = Convert.ToDouble(pVolume.Value);
        //        if (pWeight.Value > 0)
        //            weight = Convert.ToDouble(pWeight.Value);

        //        string kq = BigPackageController.Insert(code, weight, volume, 1,
        //            DateTime.Now, username_current);

        //        if (kq.ToInt(0) > 0)
        //            PJUtils.ShowMessageBoxSwAlert("Tạo bao hàng thành công", "s", true, Page);
        //        else
        //            PJUtils.ShowMessageBoxSwAlert("Lỗi khi tạo bao hàng", "e", true, Page);
        //    }
        //}

        [WebMethod]
        public static string AddBigPackage(string PackageCode, string Weight, string Volume)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    var check = BigPackageController.GetByPackageCode(PackageCode);
                    if (check != null)
                    {
                        return "existCode";
                    }
                    else
                    {
                        double volume = 0;
                        double weight = 0;
                        if (Convert.ToDouble(Volume) > 0)
                            volume = Convert.ToDouble(Volume);
                        if (Convert.ToDouble(Weight) > 0)
                            weight = Convert.ToDouble(Weight);

                        string kq = BigPackageController.Insert(PackageCode, weight, volume, 1,
                            DateTime.Now, username);

                        if (kq.ToInt(0) > 0)
                            return kq;
                        else
                            return null;
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

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

                    if (userRole == 0 || userRole == 2 || userRole == 4)
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
                                    var trans = TransportationOrderNewController.GetByID(Convert.ToInt32(sm.TransportationOrderID));
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
        public static string CheckOrderShopCodeNew(string ordershopcode, string ordertransaction, string Description, string OrderID, string Username, string UserPhone)
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
                                    //if (OrderType == 1)
                                    //{
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
                                                string packageID = SmallPackageController.InsertWithMainOrderIDUIDUsernameNew(order.ID, order.UID.Value, AccountController.GetByID(order.UID.Value).Username,
                                           0, temp, "", 0, 0, 0, 2, Description, DateTime.Now, username, ordershopcode.ToInt(0), 0);

                                                //    string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                //0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                                SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);
                                                SmallPackageController.UpdateUserPhoneAndUsername(Convert.ToInt32(packageID), Username, UserPhone);
                                                #region Lấy tất cả các cục hiện có trong đơn

                                                var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                PackageAll pa = new PackageAll();
                                                pa.PackageAllType = 0;
                                                pa.PackageGetCount = smallpackages.Count;
                                                List<OrderGet> og = new List<OrderGet>();

                                                OrderGet o = new OrderGet();
                                                o.ID = packageID.ToInt(0);
                                                o.OrderType = "Đơn hàng mua hộ";
                                                o.BigPackageID = 0;
                                                o.BarCode = temp;
                                                o.TotalWeight = "0";
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
                                        var moCode = MainOrderCodeController.GetByID(Convert.ToInt32(ordershopcode));
                                        if (moCode != null)
                                        {
                                            var order = MainOrderController.GetAllByID(moCode.MainOrderID.Value);
                                            if (order != null)
                                            {
                                                int MainOrderID = order.ID;
                                                string temp = "";
                                                if (!string.IsNullOrEmpty(ordertransaction))
                                                    temp = ordertransaction;
                                                else
                                                    temp = moCode.MainOrderCode + "-" + PJUtils.GetRandomStringByDateTime();
                                                var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                                if (getsmallcheck.Count > 0)
                                                {
                                                    return "existsmallpackage";
                                                }
                                                else
                                                {
                                                    string packageID = SmallPackageController.InsertWithMainOrderIDUIDUsernameNew(order.ID, order.UID.Value, AccountController.GetByID(order.UID.Value).Username,
                                           0, temp, "", 0, 0, 0, 2, Description, DateTime.Now, username, ordershopcode.ToInt(0), 0);
                                                    SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                                    SmallPackageController.UpdateUserPhoneAndUsername(Convert.ToInt32(packageID), Username, UserPhone);
                                                    #region Lấy tất cả các cục hiện có trong đơn

                                                    var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                    PackageAll pa = new PackageAll();
                                                    pa.PackageAllType = 0;
                                                    pa.PackageGetCount = smallpackages.Count;
                                                    List<OrderGet> og = new List<OrderGet>();

                                                    OrderGet o = new OrderGet();
                                                    o.ID = packageID.ToInt(0);
                                                    o.OrderType = "Đơn hàng mua hộ";
                                                    o.BigPackageID = 0;
                                                    o.BarCode = temp;
                                                    o.TotalWeight = "0";
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
                                            return "noteexistordercode";
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
                                            SmallPackageController.UpdateUserPhoneAndUsername(Convert.ToInt32(packageID), Username, UserPhone);
                                            SmallPackageController.UpdateNote(Convert.ToInt32(packageID), Description);

                                            PackageAll pa = new PackageAll();
                                            pa.PackageAllType = 0;
                                            pa.PackageGetCount = 0;
                                            List<OrderGet> og = new List<OrderGet>();
                                            //string temp = "temp-" + PJUtils.GetRandomStringByDateTime();
                                            OrderGet o = new OrderGet();
                                            o.ID = packageID.ToInt(0);
                                            o.OrderType = "Chưa xác định";
                                            o.BigPackageID = 0;
                                            o.BarCode = temp;
                                            o.TotalWeight = "0";
                                            o.Status = 1;
                                            int mainOrderID = Convert.ToInt32(MainOrderID);
                                            o.MainorderID = mainOrderID;
                                            o.TransportationID = 0;
                                            o.OrderShopCode = "";
                                            //var orders = OrderController.GetByMainOrderID(MainOrderID);

                                            //double totalProductQuantity = 0;
                                            //if (orders.Count > 0)
                                            //{
                                            //    foreach (var p in orders)
                                            //    {
                                            //        totalProductQuantity += Convert.ToDouble(p.quantity);
                                            //    }
                                            //}
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
                                            //var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                            //if (getsmallcheck.Count > 0)
                                            //{
                                            //    return "existsmallpackage";
                                            //}
                                            //else
                                            //{

                                            //}
                                        }
                                        #endregion
                                    }
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
                                    //        SmallPackageController.UpdateUserPhoneAndUsername(Convert.ToInt32(packageID), Username, UserPhone);
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


        [WebMethod]
        public static string AddPackageSame(string barcode)
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
                                    var checksmall = SmallPackageController.GetByOrderTransactionCode(barcode);
                                    if (checksmall != null)
                                    {
                                        var order = MainOrderController.GetAllByID(Convert.ToInt32(checksmall.MainOrderID));
                                        if (order != null)
                                        {
                                            int MainOrderID = order.ID;

                                            string temp = barcode + "-" + DateTime.Now.Second.ToString();
                                            var getsmallcheck = SmallPackageController.GetByOrderCode(temp);
                                            if (getsmallcheck.Count > 0)
                                            {
                                                return "existsmallpackage";
                                            }
                                            else
                                            {
                                                string packageID = SmallPackageController.InsertWithMainOrderIDUIDUsernameNew(order.ID, order.UID.Value, AccountController.GetByID(order.UID.Value).Username,
                                           0, temp, "", 0, 0, 0, 2, "", DateTime.Now, username, Convert.ToInt32(checksmall.MainOrderCodeID), 0);

                                                //    string packageID = SmallPackageController.InsertWithMainOrderIDAndIsTemp(MainOrderID,
                                                //0, temp, "", 0, 0, 0, 2, true, 0, DateTime.Now, username);

                                                #region Lấy tất cả các cục hiện có trong đơn

                                                var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                                                PackageAll pa = new PackageAll();
                                                pa.PackageAllType = 0;
                                                pa.PackageGetCount = smallpackages.Count;
                                                List<OrderGet> og = new List<OrderGet>();

                                                OrderGet o = new OrderGet();
                                                o.ID = packageID.ToInt(0);
                                                o.OrderType = "Đơn hàng mua hộ";
                                                o.BigPackageID = 0;
                                                o.BarCode = temp;
                                                o.TotalWeight = "0";
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


                                            }
                                        }
                                        else
                                            return "none";
                                    }
                                    else
                                        return "none";
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


        [WebMethod]
        public static string GetBigPackage()
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                var bs = BigPackageController.GetAllWithStatus(1);
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
        public static string GetOrder(string MainOrderCode, string Username)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                if (!string.IsNullOrEmpty(MainOrderCode) || !string.IsNullOrEmpty(Username))
                {
                    if (!string.IsNullOrEmpty(MainOrderCode) && string.IsNullOrEmpty(Username))
                    {
                        List<MainOrder> lmo = new List<MainOrder>();
                        var lm = MainOrderCodeController.GetContainMainOrderCode(MainOrderCode);
                        if (lm.Count > 0)
                        {
                            foreach (var item in lm)
                            {
                                MainOrder mo = new MainOrder();
                                var Mainorder = MainOrderController.GetByIDandStatus(item.MainOrderID.Value, 9);
                                if (Mainorder != null)
                                {
                                    mo.ID = Mainorder.ID;
                                    mo.MainOrderCode = item.MainOrderCode;
                                    mo.MainOrderCodeID = item.ID;
                                    var od = OrderController.GetByMainOrderID(Mainorder.ID);
                                    if (od.Count > 0)
                                    {
                                        List<Order> lo = new List<Order>();
                                        foreach (var temp in od)
                                        {
                                            Order o = new Order();
                                            o.Image = temp.image_origin;
                                            o.SoLuong = temp.quantity.ToInt();
                                            lo.Add(o);
                                        }
                                        mo.Order = lo;
                                    }
                                    lmo.Add(mo);
                                }
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(lmo);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (string.IsNullOrEmpty(MainOrderCode) && !string.IsNullOrEmpty(Username))
                    {
                        List<MainOrder> lmo = new List<MainOrder>();
                        var uskh = AccountController.GetByUsername(Username);
                        if (uskh != null)
                        {
                            var MainOder = MainOrderController.GetByUID(uskh.ID);
                            if (MainOder.Count > 0)
                            {
                                foreach (var tod in MainOder)
                                {
                                    var lm = MainOrderCodeController.GetAllByMainOrderID(tod.ID);
                                    if (lm.Count > 0)
                                    {
                                        foreach (var item in lm)
                                        {
                                            MainOrder mo = new MainOrder();
                                            mo.ID = tod.ID;
                                            mo.MainOrderCode = item.MainOrderCode;
                                            mo.MainOrderCodeID = item.ID;
                                            var od = OrderController.GetByMainOrderID(tod.ID);
                                            if (od.Count > 0)
                                            {
                                                List<Order> lo = new List<Order>();
                                                foreach (var temp in od)
                                                {
                                                    Order o = new Order();
                                                    o.Image = temp.image_origin;
                                                    o.SoLuong = temp.quantity.ToInt();
                                                    lo.Add(o);
                                                }
                                                mo.Order = lo;
                                            }
                                            lmo.Add(mo);
                                        }
                                    }

                                }
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(lmo);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        List<MainOrder> lmo = new List<MainOrder>();
                        var lm = MainOrderCodeController.GetContainMainOrderCode(MainOrderCode);
                        if (lm.Count > 0)
                        {
                            var uskh = AccountController.GetByUsername(Username);
                            if (uskh != null)
                            {
                                foreach (var item in lm)
                                {
                                    MainOrder mo = new MainOrder();
                                    var Mainorder = MainOrderController.GetByIDAndUID(item.MainOrderID.Value, uskh.ID);
                                    if (Mainorder != null)
                                    {
                                        mo.ID = Mainorder.ID;
                                        mo.MainOrderCode = item.MainOrderCode;
                                        mo.MainOrderCodeID = item.ID;
                                        var od = OrderController.GetByMainOrderID(Mainorder.ID);
                                        if (od.Count > 0)
                                        {
                                            List<Order> lo = new List<Order>();
                                            foreach (var temp in od)
                                            {
                                                Order o = new Order();
                                                o.Image = temp.image_origin;
                                                o.SoLuong = temp.quantity.ToInt();
                                                lo.Add(o);
                                            }
                                            mo.Order = lo;
                                        }
                                        lmo.Add(mo);
                                    }
                                }
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(lmo);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        [WebMethod]
        public static string GetOrderByPhone(string MainOrderCode, string Phone)
        {
            string Username = "";
            var ai = AccountInfoController.GetByPhone(Phone);
            if (ai != null)
            {
                var acc = AccountController.GetByID(Convert.ToInt32(ai.UID));
                if(acc!=null)
                {
                    Username = acc.Username;
                    if (HttpContext.Current.Session["userLoginSystem"] != null)
                    {
                        if (!string.IsNullOrEmpty(MainOrderCode) || !string.IsNullOrEmpty(Username))
                        {
                            if (!string.IsNullOrEmpty(MainOrderCode) && string.IsNullOrEmpty(Username))
                            {
                                List<MainOrder> lmo = new List<MainOrder>();
                                var lm = MainOrderCodeController.GetContainMainOrderCode(MainOrderCode);
                                if (lm.Count > 0)
                                {
                                    foreach (var item in lm)
                                    {
                                        MainOrder mo = new MainOrder();
                                        var Mainorder = MainOrderController.GetByIDandStatus(item.MainOrderID.Value, 9);
                                        if (Mainorder != null)
                                        {
                                            mo.ID = Mainorder.ID;
                                            mo.MainOrderCode = item.MainOrderCode;
                                            mo.MainOrderCodeID = item.ID;
                                            mo.Username = Username;
                                            var od = OrderController.GetByMainOrderID(Mainorder.ID);
                                            if (od.Count > 0)
                                            {
                                                List<Order> lo = new List<Order>();
                                                foreach (var temp in od)
                                                {
                                                    Order o = new Order();
                                                    o.Image = temp.image_origin;
                                                    o.SoLuong = temp.quantity.ToInt();
                                                    lo.Add(o);
                                                }
                                                mo.Order = lo;
                                            }
                                            lmo.Add(mo);
                                        }
                                    }
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    return serializer.Serialize(lmo);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else if (string.IsNullOrEmpty(MainOrderCode) && !string.IsNullOrEmpty(Username))
                            {
                                List<MainOrder> lmo = new List<MainOrder>();
                                var uskh = AccountController.GetByUsername(Username);
                                if (uskh != null)
                                {
                                    var MainOder = MainOrderController.GetByUID(uskh.ID);
                                    if (MainOder.Count > 0)
                                    {
                                        foreach (var tod in MainOder)
                                        {
                                            var lm = MainOrderCodeController.GetAllByMainOrderID(tod.ID);
                                            if (lm.Count > 0)
                                            {
                                                foreach (var item in lm)
                                                {
                                                    MainOrder mo = new MainOrder();
                                                    mo.ID = tod.ID;
                                                    mo.MainOrderCode = item.MainOrderCode;
                                                    mo.MainOrderCodeID = item.ID;
                                                    mo.Username = Username;
                                                    var od = OrderController.GetByMainOrderID(tod.ID);
                                                    if (od.Count > 0)
                                                    {
                                                        List<Order> lo = new List<Order>();
                                                        foreach (var temp in od)
                                                        {
                                                            Order o = new Order();
                                                            o.Image = temp.image_origin;
                                                            o.SoLuong = temp.quantity.ToInt();
                                                            lo.Add(o);
                                                        }
                                                        mo.Order = lo;
                                                    }
                                                    lmo.Add(mo);
                                                }
                                            }

                                        }
                                    }
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    return serializer.Serialize(lmo);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                List<MainOrder> lmo = new List<MainOrder>();
                                var lm = MainOrderCodeController.GetContainMainOrderCode(MainOrderCode);
                                if (lm.Count > 0)
                                {
                                    var uskh = AccountController.GetByUsername(Username);
                                    if (uskh != null)
                                    {
                                        foreach (var item in lm)
                                        {
                                            MainOrder mo = new MainOrder();
                                            var Mainorder = MainOrderController.GetByIDAndUID(item.MainOrderID.Value, uskh.ID);
                                            if (Mainorder != null)
                                            {
                                                mo.ID = Mainorder.ID;
                                                mo.MainOrderCode = item.MainOrderCode;
                                                mo.MainOrderCodeID = item.ID;
                                                mo.Username = Username;
                                                var od = OrderController.GetByMainOrderID(Mainorder.ID);
                                                if (od.Count > 0)
                                                {
                                                    List<Order> lo = new List<Order>();
                                                    foreach (var temp in od)
                                                    {
                                                        Order o = new Order();
                                                        o.Image = temp.image_origin;
                                                        o.SoLuong = temp.quantity.ToInt();
                                                        lo.Add(o);
                                                    }
                                                    mo.Order = lo;
                                                }
                                                lmo.Add(mo);
                                            }
                                        }
                                    }
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    return serializer.Serialize(lmo);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;
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

        [WebMethod]
        public static string GetMainOrder(string MainOrderID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                if (!string.IsNullOrEmpty(MainOrderID))
                {

                    MainOrder mo = new MainOrder();
                    var Mainorder = MainOrderController.GetAllByID(Convert.ToInt32(MainOrderID));
                    if (Mainorder != null)
                    {
                        mo.ID = Mainorder.ID;
                        var od = OrderController.GetByMainOrderID(Mainorder.ID);
                        if (od.Count > 0)
                        {
                            List<Order> lo = new List<Order>();
                            foreach (var temp in od)
                            {
                                Order o = new Order();
                                o.Title = temp.title_origin;
                                o.Image = temp.image_origin;
                                o.SoLuong = temp.quantity.ToInt();
                                lo.Add(o);
                            }
                            mo.Order = lo;
                        }
                    }
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(mo);
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
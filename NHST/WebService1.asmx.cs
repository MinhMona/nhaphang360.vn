using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NHST.Controllers;
using System.Web.Script.Services;
using System.Web.Http;
using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using NHST.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Threading;

namespace NHST
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    ///     
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public void getCurrency()
        {
            //return property;
            string Exchangerate = ConfigurationController.GetByTop1().Currency;
            var rs = new ResponseClass();
            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
            rs.Status = APIUtils.ResponseMessage.Success.ToString();
            rs.Currency = Exchangerate;
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod(EnableSession = true)]
        public void receivesmallpackage(string MainOrderCode, string Value, string Type) //1-taobao; 2-tmall; 3-1688
        {
            var rs = new ResponseClass();

            var listphone = new List<string>() { "13229461941", "18775918609", "13189648927", "13077724168" };

            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var dec = HttpUtility.UrlDecode(Value);

                    if (obj_user.RoleID == 0 || obj_user.RoleID == 2 || obj_user.RoleID == 3)
                    {
                        var check = MainOrderCodeController.GetAllMainOrderCode(MainOrderCode);
                        if (check.Count > 0)
                        {
                            var mo = check[0];

                            double currency = 0;
                            var main = MainOrderController.GetAllByID(mo.MainOrderID.Value);
                            if (main != null)
                            {
                                currency = Convert.ToDouble(main.CurrentCNYVN);
                            }

                            int uidmuahang = Convert.ToInt32(MainOrderController.GetAllByID(mo.MainOrderID.Value).UID);
                            string usermuahang = "";

                            var accmuahan = AccountController.GetByID(uidmuahang);
                            if (accmuahan != null)
                            {
                                usermuahang = accmuahan.Username;
                            }

                            if (Type == "1")
                            {
                                var list = ser.Deserialize<Taobao>(dec);
                                if (list.packageInfos != null)
                                {
                                    var address = list.deliveryInfo.address;
                                    bool checkphone = false;
                                    foreach (var p in listphone)
                                    {
                                        if (address.Contains(p))
                                        {
                                            checkphone = true;
                                        }
                                    }
                                    if (checkphone)
                                    {
                                        if (list.packageInfos.list.Count > 0)
                                        {
                                            foreach (var item in list.packageInfos.list)
                                            {
                                                string mvd = item.invoiceNo;
                                                if (!string.IsNullOrEmpty(mvd))
                                                {
                                                    var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCodeID(mvd, mo.ID);
                                                    if (checkmvd == null)
                                                    {
                                                        string listIMG = "";
                                                        var detail = item.details;
                                                        if (detail.Count > 0)
                                                        {
                                                            foreach (var temp in detail)
                                                            {
                                                                listIMG += temp.picUrl + "|";
                                                            }
                                                        }

                                                        double TotalPriceReal = 0;
                                                        string FeeShipTQ = "";

                                                        if (list.mainOrder.totalPrice.Count > 0)
                                                        {
                                                            foreach (var p in list.mainOrder.totalPrice)
                                                            {
                                                                if (p.content.Count > 0)
                                                                {
                                                                    if (p.content[0].value.Contains("快递"))
                                                                    {
                                                                        FeeShipTQ = p.content[0].value;
                                                                    }
                                                                    else
                                                                    {
                                                                        TotalPriceReal = Convert.ToDouble(p.content[0].value);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        string[] value = FeeShipTQ.Split(':');
                                                        string[] feeship = value[1].Split(')');
                                                        double fb = Convert.ToDouble(feeship[0]);

                                                        var kq = SmallPackageController.InsertOrderTransactionCodeAuto(mo.MainOrderID.Value, accmuahan.ID, usermuahang, 0,
                                                                    mvd, "", 0, 0, 0,
                                                                1, "", currentDate, username, mo.ID, listIMG);

                                                        //double TotalPriceRealCNY = list.mainOrder.payInfo.actualFee.value;
                                                        //double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                                        //MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                                        //MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                                        SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
                                                    }
                                                }

                                            }
                                        }
                                        double TotalPriceRealCNY = list.mainOrder.payInfo.actualFee.value;
                                        double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                        MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                        MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                    }
                                    else
                                    {
                                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                                        rs.Message = "Số điện thoại nhận hàng không đúng!";
                                    }
                                }
                                else
                                {
                                    string mvd = list.deliveryInfo.logisticsNum;

                                    string address = list.deliveryInfo.address;

                                    string[] listaddress = address.Split('，');
                                    if (listaddress.Count() > 0)
                                    {
                                        bool checkphone = false;
                                        string WareHousePhone = listaddress[1];
                                        foreach (var p in listphone)
                                        {
                                            if (WareHousePhone.Contains(p))
                                            {
                                                checkphone = true;
                                            }
                                        }

                                        if (checkphone)
                                        {
                                            var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCodeID(mvd, mo.ID);
                                            if (checkmvd == null)
                                            {
                                                string listIMG = "";
                                                if (list.mainOrder.subOrders.Count() > 0)
                                                {
                                                    var MainInfo = list.mainOrder.subOrders;
                                                    foreach (var minfo in MainInfo)
                                                    {
                                                        if (minfo.priceInfo > 0)
                                                        {
                                                            listIMG += minfo.itemInfo.pic + "|";
                                                        }
                                                    }
                                                }

                                                double TotalPriceReal = 0;
                                                string FeeShipTQ = "";

                                                //if (list.mainOrder.totalPrice.Count > 0)
                                                //{
                                                //    foreach (var p in list.mainOrder.totalPrice)
                                                //    {
                                                //        if (p.content.Count > 0)
                                                //        {
                                                //            if (p.content[0].value.Contains("快递"))
                                                //            {
                                                //                FeeShipTQ = p.content[0].value;
                                                //            }
                                                //            else
                                                //            {
                                                //                TotalPriceReal = Convert.ToDouble(p.content[0].value);
                                                //            }
                                                //        }
                                                //    }
                                                //}

                                                //string[] value = FeeShipTQ.Split(':');
                                                //string[] feeship = value[1].Split(')');
                                                //double fb = Convert.ToDouble(feeship[0]);

                                                var kq = SmallPackageController.InsertOrderTransactionCodeAuto(mo.MainOrderID.Value, accmuahan.ID, usermuahang, 0,
                           mvd, "", 0, 0, 0, 1, "", currentDate, username, mo.ID, listIMG);

                                                //double TotalPriceRealCNY = list.mainOrder.payInfo.actualFee.value;
                                                //double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                                //MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                                //MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                                SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
                                            }

                                            double TotalPriceRealCNY = list.mainOrder.payInfo.actualFee.value;
                                            double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                            MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                            MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                        }
                                        else
                                        {
                                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
                                            rs.Message = "Số điện thoại nhận hàng không đúng!";
                                        }
                                    }
                                }
                            }
                            else if (Type == "2")
                            {
                                try
                                {
                                    var list = ser.Deserialize<Tmall>(dec);
                                    var address = list.basic.lists[0].content[0].text;

                                    bool checkphone = false;
                                    foreach (var p in listphone)
                                    {
                                        if (address.Contains(p))
                                        {
                                            checkphone = true;
                                        }
                                    }

                                    if (checkphone)
                                    {
                                        var listmvd = list.orders.list[0].logistic.content;
                                        if (listmvd.Count > 0)
                                        {
                                            foreach (var item in listmvd)
                                            {
                                                string mvd = item.mailNo;
                                                if (!string.IsNullOrEmpty(mvd))
                                                {
                                                    var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCodeID(mvd, mo.ID);
                                                    if (checkmvd == null)
                                                    {
                                                        string listIMG = "";
                                                        var listsub = list.orders.list[0].status;
                                                        if (listsub.Count > 0)
                                                        {
                                                            foreach (var sub in listsub)
                                                            {
                                                                double price = Convert.ToDouble(sub.subOrders[0].priceInfo[0].text);
                                                                if (price > 0)
                                                                {
                                                                    listIMG += sub.subOrders[0].itemInfo.pic + "|";
                                                                }
                                                            }
                                                        }

                                                        var kq = SmallPackageController.InsertOrderTransactionCodeAuto(mo.MainOrderID.Value, accmuahan.ID, usermuahang, 0,
                                                                     mvd, "", 0, 0, 0,
                                                                 1, "", currentDate, username, mo.ID, listIMG);

                                                        //string money = list.amount.count[0][0].content[0].data.money.text;
                                                        //string[] value = money.Split('￥');
                                                        //double TotalPriceRealCNY = Convert.ToDouble(value[1]);
                                                        //double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                                        //MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                                        //MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                                        SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
                                                    }
                                                }
                                            }
                                        }

                                        string money = list.amount.count[0][0].content[0].data.money.text;
                                        string[] value = money.Split('￥');
                                        double TotalPriceRealCNY = Convert.ToDouble(value[1]);
                                        double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                        MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                        MainOrderController.UpdateTimeline(mo.ID, list.logicticsFlow);
                                    }
                                    else
                                    {
                                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                                        rs.Message = "Số điện thoại nhận hàng không đúng!";
                                    }
                                }
                                catch
                                {
                                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                                    rs.Message = "Chưa có mã vận đơn";
                                }
                            }
                            else
                            {
                                var list = ser.Deserialize<Site1688>(dec);
                                var listmvd = list.logistics;
                                var phone = list.detail.buyer.phone;

                                bool checkphone = false;
                                foreach (var p in listphone)
                                {
                                    if (phone.Contains(p))
                                    {
                                        checkphone = true;
                                    }
                                }
                                if (checkphone)
                                {
                                    foreach (var item in listmvd)
                                    {
                                        string mvd = item.mailNumber;
                                        if (!string.IsNullOrEmpty(mvd))
                                        {
                                            var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCodeID(mvd, mo.ID);
                                            if (checkmvd == null)
                                            {
                                                string listIMG = "";
                                                var listp = list.logistics[0].products;
                                                if (listp.Count > 0)
                                                {
                                                    foreach (var p in listp)
                                                    {
                                                        listIMG += p.img + "|";
                                                    }
                                                }
                                                var kq = SmallPackageController.InsertOrderTransactionCodeAuto(mo.MainOrderID.Value, accmuahan.ID, usermuahang, 0,
                                                             mvd, "", 0, 0, 0,
                                                         1, "", currentDate, username, mo.ID, listIMG);

                                                //double TotalPriceRealCNY = Convert.ToDouble(list.detail.total);
                                                //double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                                //MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                                SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
                                            }
                                        }
                                    }
                                    double TotalPriceRealCNY = Convert.ToDouble(list.detail.total);
                                    double TotalPriceVND = Math.Round((TotalPriceRealCNY * currency), 0);
                                    MainOrderController.UpdateTotalPriceReal(mo.MainOrderID.Value, TotalPriceVND.ToString(), TotalPriceRealCNY.ToString());
                                }
                                else
                                {
                                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                                    rs.Message = "Số điện thoại nhận hàng không đúng!";
                                }
                            }
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.Message = "";
                        }
                        else
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
                            rs.Message = "Mã đơn hàng chưa được khai báo";
                        }
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "";
                    }
                    //SmallPackageAutoController.Insert(MainOrderCode, dec, "test", DateTime.Now);
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                    rs.Message = "https://nhaphang360.vn/dang-nhap";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "https://nhaphang360.vn/dang-nhap";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        //[WebMethod(EnableSession = true)]
        //public void receivesmallpackageVCH(string MainOrderCode, string Value, string Type) //1-taobao; 2-tmall; 3-1688
        //{
        //    var rs = new ResponseClass();

        //    var listphone = new List<string>() { "13060999201", "15521339873", "18778812326", "15778167312", "86-18778812326" };

        //    if (HttpContext.Current.Session["userLoginSystem"] != null)
        //    {
        //        DateTime currentDate = DateTime.Now;
        //        string username = Session["userLoginSystem"].ToString();
        //        var obj_user = AccountController.GetByUsername(username);
        //        if (obj_user != null)
        //        {
        //            JavaScriptSerializer ser = new JavaScriptSerializer();
        //            var dec = HttpUtility.UrlDecode(Value);

        //            int UID = obj_user.ID;
        //            int KhoTQ = obj_user.WarehouseFrom.Value;
        //            int KhoVN = obj_user.WarehouseTo.Value;
        //            int Ship = obj_user.ShippingType.Value;


        //            if (Type == "1")
        //            {
        //                var list = ser.Deserialize<Taobao>(dec);
        //                if (list.packageInfos != null)
        //                {
        //                    var address = list.deliveryInfo.address;
        //                    bool checkphone = false;
        //                    foreach (var p in listphone)
        //                    {
        //                        if (address.Contains(p))
        //                        {
        //                            checkphone = true;
        //                        }
        //                    }
        //                    if (checkphone)
        //                    {
        //                        if (list.packageInfos.list.Count > 0)
        //                        {
        //                            foreach (var item in list.packageInfos.list)
        //                            {
        //                                string mvd = item.invoiceNo;
        //                                if (!string.IsNullOrEmpty(mvd))
        //                                {
        //                                    var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCode(mvd, MainOrderCode);
        //                                    if (checkmvd == null)
        //                                    {
        //                                        string listIMG = "";
        //                                        var detail = item.details;
        //                                        if (detail.Count > 0)
        //                                        {
        //                                            foreach (var temp in detail)
        //                                            {
        //                                                listIMG += temp.picUrl + "|";
        //                                            }
        //                                        }

        //                                        string tID = TransportationOrderNewController.Insert(UID, username, "0", "0", "0", "0", "0", "0", "0",
        //                        "0", "0", "0", 0, mvd, 1, "", "", "0", "0", currentDate, username, KhoTQ, KhoVN, Ship);
        //                                        int packageID = 0;
        //                                        var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
        //                                        if (smallpackage == null)
        //                                        {
        //                                            string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, mvd, "",
        //                                            0, 0, 0, 1, currentDate, username);
        //                                            packageID = kq.ToInt();
        //                                            SmallPackageController.UpdateIMG(kq.ToInt(0), listIMG, currentDate, username);
        //                                            TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
        //                                            TransportationOrderNewController.UpdateMainOrderCode(tID.ToInt(0), MainOrderCode);
        //                                            SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
        //                                        }
        //                                    }
        //                                }

        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
        //                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
        //                        rs.Message = "Số điện thoại nhận hàng không đúng!";
        //                    }
        //                }
        //                else
        //                {
        //                    string mvd = list.deliveryInfo.logisticsNum;

        //                    string address = list.deliveryInfo.address;

        //                    string[] listaddress = address.Split('，');
        //                    if (listaddress.Count() > 0)
        //                    {
        //                        bool checkphone = false;
        //                        string WareHousePhone = listaddress[1];
        //                        foreach (var p in listphone)
        //                        {
        //                            if (WareHousePhone.Contains(p))
        //                            {
        //                                checkphone = true;
        //                            }
        //                        }

        //                        if (checkphone)
        //                        {
        //                            var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCode(mvd, MainOrderCode);
        //                            if (checkmvd == null)
        //                            {
        //                                string listIMG = "";
        //                                if (list.mainOrder.subOrders.Count() > 0)
        //                                {
        //                                    var MainInfo = list.mainOrder.subOrders;
        //                                    foreach (var minfo in MainInfo)
        //                                    {
        //                                        if (minfo.priceInfo > 0)
        //                                        {
        //                                            listIMG += minfo.itemInfo.pic + "|";
        //                                        }
        //                                    }
        //                                }

        //                                string tID = TransportationOrderNewController.Insert(UID, username, "0", "0", "0", "0", "0", "0", "0",
        //                       "0", "0", "0", 0, mvd, 1, "", "", "0", "0", currentDate, username, KhoTQ, KhoVN, Ship);
        //                                int packageID = 0;
        //                                var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
        //                                if (smallpackage == null)
        //                                {
        //                                    string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, mvd, "",
        //                                    0, 0, 0, 1, currentDate, username);
        //                                    packageID = kq.ToInt();
        //                                    SmallPackageController.UpdateIMG(kq.ToInt(0), listIMG, currentDate, username);
        //                                    TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
        //                                    TransportationOrderNewController.UpdateMainOrderCode(tID.ToInt(0), MainOrderCode);
        //                                    SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
        //                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
        //                            rs.Message = "Số điện thoại nhận hàng không đúng!";
        //                        }
        //                    }
        //                }
        //            }
        //            else if (Type == "2")
        //            {
        //                try
        //                {
        //                    var list = ser.Deserialize<Tmall>(dec);
        //                    var address = list.basic.lists[0].content[0].text;

        //                    bool checkphone = false;
        //                    foreach (var p in listphone)
        //                    {
        //                        if (address.Contains(p))
        //                        {
        //                            checkphone = true;
        //                        }
        //                    }

        //                    if (checkphone)
        //                    {
        //                        var listmvd = list.orders.list[0].logistic.content;
        //                        if (listmvd.Count > 0)
        //                        {
        //                            foreach (var item in listmvd)
        //                            {
        //                                string mvd = item.mailNo;
        //                                if (!string.IsNullOrEmpty(mvd))
        //                                {
        //                                    var checkmvd = SmallPackageController.GetByOrderTransactionCodeAndMainOrderCode(mvd, MainOrderCode);
        //                                    if (checkmvd == null)
        //                                    {
        //                                        string listIMG = "";
        //                                        var listsub = list.orders.list[0].status;
        //                                        if (listsub.Count > 0)
        //                                        {
        //                                            foreach (var sub in listsub)
        //                                            {
        //                                                double price = Convert.ToDouble(sub.subOrders[0].priceInfo[0].text);
        //                                                if (price > 0)
        //                                                {
        //                                                    listIMG += sub.subOrders[0].itemInfo.pic + "|";
        //                                                }
        //                                            }
        //                                        }

        //                                        string tID = TransportationOrderNewController.Insert(UID, username, "0", "0", "0", "0", "0", "0", "0",
        //                                                                       "0", "0", "0", 0, mvd, 1, "", "", "0", "0", currentDate, username, KhoTQ, KhoVN, Ship);
        //                                        int packageID = 0;
        //                                        var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
        //                                        if (smallpackage == null)
        //                                        {
        //                                            string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, mvd, "",
        //                                            0, 0, 0, 1, currentDate, username);
        //                                            packageID = kq.ToInt();
        //                                            SmallPackageController.UpdateIMG(kq.ToInt(0), listIMG, currentDate, username);
        //                                            TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
        //                                            TransportationOrderNewController.UpdateMainOrderCode(tID.ToInt(0), MainOrderCode);
        //                                            SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
        //                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
        //                        rs.Message = "Số điện thoại nhận hàng không đúng!";
        //                    }
        //                }
        //                catch
        //                {
        //                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
        //                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
        //                    rs.Message = "Chưa có mã vận đơn";
        //                }
        //            }
        //            else
        //            {
        //                var list = ser.Deserialize<Site1688>(dec);
        //                var listmvd = list.logistics;
        //                var phone = list.detail.buyer.phone;

        //                bool checkphone = false;
        //                foreach (var p in listphone)
        //                {
        //                    if (phone.Contains(p))
        //                    {
        //                        checkphone = true;
        //                    }
        //                }
        //                if (checkphone)
        //                {
        //                    foreach (var item in listmvd)
        //                    {
        //                        string mvd = item.mailNumber;
        //                        if (!string.IsNullOrEmpty(mvd))
        //                        {
        //                            var checkmvd = SmallPackageController.GetByOrderTransactionCode(mvd);
        //                            if (checkmvd == null)
        //                            {
        //                                string listIMG = "";
        //                                var listp = list.logistics[0].products;
        //                                if (listp.Count > 0)
        //                                {
        //                                    foreach (var p in listp)
        //                                    {
        //                                        listIMG += p.img + "|";
        //                                    }
        //                                }

        //                                string tID = TransportationOrderNewController.Insert(UID, username, "0", "0", "0", "0", "0", "0", "0",
        //                        "0", "0", "0", 0, mvd, 1, "", "", "0", "0", currentDate, username, KhoTQ, KhoVN, Ship);
        //                                int packageID = 0;
        //                                var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
        //                                if (smallpackage == null)
        //                                {
        //                                    string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, mvd, "",
        //                                    0, 0, 0, 1, currentDate, username);
        //                                    packageID = kq.ToInt();
        //                                    SmallPackageController.UpdateIMG(kq.ToInt(0), listIMG, currentDate, username);
        //                                    TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
        //                                    TransportationOrderNewController.UpdateMainOrderCode(tID.ToInt(0), MainOrderCode);
        //                                    SmallPackageController.UpdateMainOrderCode(kq.ToInt(0), MainOrderCode);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
        //                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
        //                    rs.Message = "Số điện thoại nhận hàng không đúng!";
        //                }
        //            }
        //            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
        //            rs.Status = APIUtils.ResponseMessage.Success.ToString();
        //            rs.Message = "";

        //        }
        //        else
        //        {
        //            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
        //            rs.Status = APIUtils.ResponseMessage.Fail.ToString();
        //            rs.Message = "https://nhaphang360.vn/dang-nhap";
        //        }
        //    }
        //    else
        //    {
        //        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
        //        rs.Status = APIUtils.ResponseMessage.Fail.ToString();
        //        rs.Message = "https://nhaphang360.vn/dang-nhap";
        //    }

        //    Context.Response.ContentType = "application/json";
        //    Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
        //    Context.Response.Flush();
        //    Context.Response.End();
        //}


        #region taobao
        public class Taobao
        {
            public deliveryInfo deliveryInfo { get; set; }
            public packageInfos packageInfos { get; set; }
            public mainOrder mainOrder { get; set; }
            public string logicticsFlow { get; set; }
        }

        public class deliveryInfo
        {
            public string logisticsName { get; set; }
            public string sellerNick { get; set; }
            public string address { get; set; }
            public string showLogistics { get; set; }
            public string shipType { get; set; }
            public string logisticsNum { get; set; }
            public string showTSP { get; set; }
            public string asyncLogisticsUrl { get; set; }
            public string newAddress { get; set; }
        }

        public class mainOrder
        {
            public List<subOrders> subOrders { get; set; }
            public List<totalPrice> totalPrice { get; set; }
            public payInfo payInfo { get; set; }
        }

        public class totalPrice
        {
            public string type { get; set; }
            public List<contenttaobao> content { get; set; }

        }

        public class payInfo
        {
            public actualFee actualFee { get; set; }
        }

        public class actualFee
        {
            public string visible { get; set; }
            public string name { get; set; }
            public double value { get; set; }
        }

        public class contenttaobao
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class subOrders
        {
            public itemInfo itemInfo { get; set; }
            public double priceInfo { get; set; }
            public int quantity { get; set; }
        }

        public class itemInfo
        {
            public string pic { get; set; }
            public string title { get; set; }
        }

        public class packageInfos
        {
            public List<list> list { get; set; }
            public string autoConfirmTip { get; set; }
            public string viewLogisticUrl { get; set; }
        }

        public class list
        {
            public string invoiceNo { get; set; }
            public List<details> details { get; set; }
            public string consignTime { get; set; }
            public string companyName { get; set; }
            public bool completed { get; set; }
            public string outLogisticsId { get; set; }
            public string shippingName { get; set; }
        }

        public class details
        {
            public string picUrl { get; set; }
            public string price { get; set; }
            public string snapUrl { get; set; }
            public string title { get; set; }
        }
        #endregion

        #region tmall
        public class Tmall
        {
            public orders orders { get; set; }
            public basic basic { get; set; }
            public amount amount { get; set; }
            public string logicticsFlow { get; set; }
        }

        public class amount
        {
            public List<List<count>> count { get; set; }
        }

        public class count
        {
            public List<contenttmall> content { get; set; }
        }

        public class contenttmall
        {
            public data data { get; set; }
        }

        public class data
        {
            public money money { get; set; }
        }

        public class money
        {
            public string text { get; set; }
        }

        public class orders
        {
            public string id { get; set; }
            public List<listtmall> list { get; set; }
        }

        public class basic
        {
            public List<lists> lists { get; set; }
        }

        public class lists
        {
            public List<contents> content { get; set; }
        }

        public class contents
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public class listtmall
        {
            public logistic logistic { get; set; }
            public List<status> status { get; set; }
        }

        public class logistic
        {
            public string key { get; set; }
            public List<content> content { get; set; }
        }

        public class content
        {
            public string companyName { get; set; }
            public string mailNo { get; set; }
            public string text { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class status
        {
            public List<subOrdersTmall> subOrders { get; set; }
        }

        public class subOrdersTmall
        {
            public itemInfoTmall itemInfo { get; set; }
            public List<priceInfo> priceInfo { get; set; }
            public int quantity { get; set; }
        }

        public class itemInfoTmall
        {
            public string itemUrl { get; set; }
            public string pic { get; set; }
            public string title { get; set; }
            public string snapUrl { get; set; }
        }

        public class priceInfo
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        #endregion

        #region 1688
        public class Site1688
        {
            public List<logistics> logistics { get; set; }
            public detail detail { get; set; }
        }

        public class detail
        {
            public buyer buyer { get; set; }
            public string total { get; set; }
        }

        public class buyer
        {
            public string id { get; set; }
            public string idAlipay { get; set; }
            public string codebill { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string phone2 { get; set; }
        }

        public class logistics
        {
            public string id { get; set; }
            public string company { get; set; }
            public string mailNumber { get; set; }
            public string shipTime { get; set; }
            public List<products> products { get; set; }
            public List<flow> flow { get; set; }
        }

        public class flow
        {
            public string date { get; set; }
            public List<flowdetail> detail { get; set; }
        }

        public class flowdetail
        {
            public string time { get; set; }
            public string area { get; set; }
            public string remark { get; set; }
        }

        public class products
        {
            public string img { get; set; }
            public string title { get; set; }
        }
        #endregion


        [WebMethod(EnableSession = true)]
        public void receivepayhelp(string OrderID, string Amount, string Note, string Customer, string FriendsAccount)
        {
            var rs = new ResponseClass();
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    try
                    {
                        var checkpay = PayHelpTempController.GetByOrderID(OrderID);
                        if (checkpay == null)
                        {
                            PayHelpTempController.Insert(obj_user.ID, OrderID, Convert.ToDouble(Amount).ToString(), Note, FriendsAccount, Customer, currentDate, username);
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.Message = "";
                        }
                        else
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                            rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                            rs.Message = "Yêu cầu này đã được thêm vào giỏ";
                        }
                    }
                    catch
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                        rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                        rs.Message = "Lỗi rồi";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                    rs.Message = "https://nhaphang360.vn/dang-nhap";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "https://nhaphang360.vn/dang-nhap";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod(EnableSession = true)]
        public void AddPayHelp(string OrderID, string Amount, string Note, string Customer, string FriendsAccount)
        {
            var rs = new ResponseClass();
            DateTime currentDate = DateTime.Now;
            double pc_config = 0;
            double currencygiagoc = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                currencygiagoc = Convert.ToDouble(config.PricePayHelpDefault);
            }
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    try
                    {
                        var checkpay = PayhelpDetailController.GetByOrderID(OrderID);
                        if (checkpay == null)
                        {
                            int level = Convert.ToInt32(obj_user.LevelID);
                            double TotalPrice = 0;
                            double currency = 0;
                            double amount = Convert.ToDouble(Amount);
                            if (amount > 0)
                            {
                                double totalpriceVNDGiagoc = currencygiagoc * amount;

                                var pricechange = PriceChangeController.GetByPriceFT(amount);
                                double pc = 0;
                                if (pricechange != null)
                                {
                                    if (level == 1)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                                    }
                                    else if (level == 2)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                                    }
                                    else if (level == 3)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                                    }
                                    else if (level == 4)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                                    }
                                    else if (level == 5)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                                    }
                                    else if (level == 6)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                                    }
                                    else if (level == 7)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                                    }
                                    else if (level == 8)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                                    }
                                    else if (level == 9)
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                    }
                                    else
                                    {
                                        pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                    }
                                }
                                TotalPrice = amount * pc;
                                currency = pc;

                                string kq = PayhelpController.Insert(obj_user.ID, username, "", amount.ToString(), TotalPrice.ToString(), pc.ToString(),
                             currencygiagoc.ToString(), totalpriceVNDGiagoc.ToString(), 0, "", currentDate, username);
                                int pID = kq.ToInt(0);
                                if (pID > 0)
                                {
                                    string phID = PayhelpDetailController.Insert(pID, Note, Amount, currentDate, username);
                                    PayhelpDetailController.UpdateFriendsAccount(phID.ToInt(0), FriendsAccount, OrderID, Customer);
                                }
                            }

                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.Message = "";
                        }
                        else
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                            rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                            rs.Message = "Yêu cầu này đã được tạo";
                        }
                    }
                    catch
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                        rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                        rs.Message = "Lỗi rồi";
                    }
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "https://nhaphang360.vn/dang-nhap";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod(EnableSession = true)]
        public void CheckCurrency(string Amount)
        {
            var rs = new ResponseClass();
            double pc_config = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                pc_config = Convert.ToDouble(config.PricePayHelpDefault);
            }
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                try
                {
                    string username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username);
                    if (obj_user != null)
                    {
                        int level = Convert.ToInt32(obj_user.LevelID);

                        double TotalPrice = 0;
                        double currency = 0;
                        double amount = Convert.ToDouble(Amount);
                        if (amount > 0)
                        {
                            var pricechange = PriceChangeController.GetByPriceFT(amount);
                            double pc = 0;
                            if (pricechange != null)
                            {
                                if (level == 1)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                                }
                                else if (level == 2)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                                }
                                else if (level == 3)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                                }
                                else if (level == 4)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                                }
                                else if (level == 5)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                                }
                                else if (level == 6)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                                }
                                else if (level == 7)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                                }
                                else if (level == 8)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                                }
                                else if (level == 9)
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                                else
                                {
                                    pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                                }
                            }
                            TotalPrice = Math.Round(amount * pc, 0);
                            currency = pc;
                        }

                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        rs.Currency = currency.ToString();
                        rs.TotalPrice = TotalPrice.ToString();
                        rs.Username = obj_user.Username;
                    }
                }
                catch
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                    rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                    rs.Message = "Lỗi rồi";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Currency = pc_config.ToString();
                rs.Message = "https://nhaphang360.vn/dang-nhap";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void Login(string Username, string Password, int Type, string DeviceToken, string TypeName)
        {
            var rs = new ResponseClass();
            Account acc = new Account();
            var ac = AccountController.Login(Username, Password);
            if (ac != null)
            {
                if (ac.RoleID == 1)
                {
                    if (ac.Status == 2)
                    {
                        acc.Username = ac.Username;
                        acc.Email = ac.Email;
                        acc.ID = ac.ID;
                        string level = "Vip 1";
                        var userLevel = UserLevelController.GetByID(ac.LevelID.Value);
                        if (userLevel != null)
                        {
                            level = userLevel.LevelName;
                        }

                        decimal levelID = Convert.ToDecimal(ac.LevelID);
                        decimal countLevel = UserLevelController.GetAll("").Count();
                        decimal te = levelID / countLevel;
                        te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                        decimal tile = te * 100;

                        acc.title = tile;

                        acc.Level = level;
                        acc.Role = Convert.ToInt32(ac.RoleID);
                        acc.Wallet = string.Format("{0:N0}", Convert.ToDouble(ac.Wallet)) + " VNĐ";
                        acc.WalletCYN = "¥ " + string.Format("{0:N0}", Convert.ToDouble(ac.WalletCYN));
                        var acin = AccountInfoController.GetByUserID(ac.ID);
                        if (acin != null)
                        {
                            acc.FirstName = acin.FirstName;
                            acc.LastName = acin.LastName;
                            acc.IMGUser = acin.IMGUser;
                            acc.Phone = acin.Phone;
                            acc.Gender = Convert.ToInt32(acin.Gender);
                            acc.Address = acin.Address;
                            acc.BirthDay = Convert.ToDateTime(acin.BirthDay);
                        }

                        List<Menu> menu = new List<Menu>();

                        Menu m1 = new Menu();
                        m1.ItemName = "ĐIỂM TÍCH LŨY";
                        m1.Link = "https://nhaphang360.vn/diem-tich-luy-app.aspx?UID=";
                        menu.Add(m1);
                        acc.Menu = menu;


                        //var dt = DeviceTokenController.GetAllByDevice(DeviceToken);
                        //if (dt != null)
                        //{
                        //    foreach (var item in dt)
                        //    {
                        //        var log = DeviceTokenController.update(item.ID);
                        //    }
                        //}


                        string token = PJUtils.RandomStringWithText(20);
                        var dt = DeviceTokenController.GetAllByUID(ac.ID, token);
                        if (dt != null)
                        {
                            foreach (var item in dt)
                            {
                                var log = DeviceTokenController.update(item.ID);
                            }
                        }


                        var device = DeviceTokenController.insert(ac.ID, Type, TypeName, DeviceToken, ac.Username, token);
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        rs.Account = acc;
                        rs.Key = token;
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Tài khoản của bạn đã bị khóa.";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "App chỉ dành cho khách hàng.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "Sai Username hoặc Password, vui lòng kiểm tra lại.";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void Get_Info(int UID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var ac = AccountController.GetByID(UID);
                if (ac != null)
                {
                    Info inf = new Info();
                    var conf = ConfigurationController.GetByTop1();
                    if (conf != null)
                    {
                        inf.Currency = Convert.ToDouble(conf.Currency);
                    }
                    inf.Wallet = string.Format("{0:N0}", Convert.ToDouble(ac.Wallet)) + " VNĐ";
                    inf.WalletCYN = "¥ " + string.Format("{0:N0}", Convert.ToDouble(ac.WalletCYN));
                    inf.Username = ac.Username;
                    inf.Role = Convert.ToInt32(ac.RoleID);
                    string level = "Vip 1";

                    decimal levelID = Convert.ToDecimal(ac.LevelID);
                    decimal countLevel = UserLevelController.GetAll("").Count();
                    decimal te = levelID / countLevel;
                    te = Math.Round(te, 2, MidpointRounding.AwayFromZero);
                    decimal tile = te * 100;

                    inf.title = tile;

                    var userLevel = UserLevelController.GetByID(ac.LevelID.Value);
                    if (userLevel != null)
                    {
                        level = userLevel.LevelName;
                    }
                    inf.Level = level;
                    var acin = AccountInfoController.GetByUserID(ac.ID);
                    if (acin != null)
                    {
                        inf.LastName = acin.LastName;
                        inf.FirstName = acin.FirstName;
                        inf.Phone = acin.Phone;
                        inf.Gender = Convert.ToInt32(acin.Gender);
                        inf.Email = acin.Email;
                        inf.BirthDay = Convert.ToDateTime(acin.BirthDay);
                    }
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Info = inf;
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tài khoản không tồn tại.";
                    rs.Logout = "1";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void LogOut(int UID, string DeviceToken)
        {
            ResponseClass rs = new ResponseClass();
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                var dt = DeviceTokenController.GetAllDevice(UID, DeviceToken);
                if (dt != null)
                {
                    foreach (var item in dt)
                    {
                        var log = DeviceTokenController.update(item.ID);
                    }
                }
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.Message = "";
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "Thất bại";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void Register(string FirstName, string LastName, string Address, string Phone, string Email, string BirthDay, int Gender, string UserName, string Password, string ConfirmPassword)
        {
            var rs = new ResponseClass();
            string Username = UserName.Trim().ToLower();
            string email = Email.Trim();
            var checkuser = AccountController.GetByUsername(Username);
            var checkemail = AccountController.GetByEmail(email);
            string phone = Phone.Trim().Replace(" ", "");
            var getaccountinfor = AccountInfoController.GetByPhone(phone);

            string error = "";
            bool check = PJUtils.CheckUnicode(Username);
            DateTime currentDate = DateTime.Now;
            DateTime bir = DateTime.Now;
            int nvkdID = 0;

            if (!string.IsNullOrEmpty(BirthDay.ToString()))
            {
                if (BirthDay.Contains("/"))
                {
                    bir = DateTime.ParseExact(BirthDay, "dd/MM/yyyy", null);
                }
                else
                {
                    bir = Convert.ToDateTime(BirthDay);
                }
            }

            if (Username.Contains(" "))
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "Tên đăng nhập không được có dấu cách.";
            }
            else if (check == true)
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "Tên đăng nhập không được có dấu tiếng Việt.";
            }
            else
            {
                if (checkuser != null)
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tên đăng nhập / Nickname đã được sử dụng vui lòng chọn Tên đăng nhập / Nickname khác.";
                }
                else if (checkemail != null)
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Email đã được sử dụng vui lòng chọn Email khác.";
                }
                else if (getaccountinfor != null)
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Số điện thoại đã được sử dụng vui lòng chọn Số điện thoại khác.";
                }
                else if (Password != ConfirmPassword)
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Xác nhận mật khẩu không trùng với mật khẩu.";
                }
                else
                {
                    Account ac = new Account();
                    string Token = PJUtils.RandomStringWithText(16);
                    var acc = AccountController.InsertNew(Username, Email, Password.Trim(), 1, 1, 1, 2, nvkdID, 0, DateTime.Now, "", DateTime.Now, "", Token);
                    string id = acc.ID.ToString();
                    if (Convert.ToInt32(id) > 0)
                    {
                        AccountController.UpdateScanWareHouse(id.ToInt(0), 0, 0);
                        ac.Username = acc.Username;
                        ac.Email = acc.Email;
                        ac.ID = acc.ID;
                        string level = "Vip 1";
                        var userLevel = UserLevelController.GetByID(acc.LevelID.Value);
                        if (userLevel != null)
                        {
                            level = userLevel.LevelName;
                        }
                        ac.Level = level;
                        ac.Role = Convert.ToInt32(acc.RoleID);
                        ac.Wallet = string.Format("{0:N0}", Convert.ToDouble(acc.Wallet)) + " VNĐ";
                        ac.WalletCYN = "¥ " + string.Format("{0:N0}", Convert.ToDouble(acc.WalletCYN));
                        int UID = Convert.ToInt32(id);
                        string idai = AccountInfoController.Insert(UID, FirstName.Trim(), LastName.Trim(), "",
                           Phone, Email, phone.Trim(), Address, "", "", bir, Gender.ToString().ToInt(1),
                           DateTime.Now, "", DateTime.Now, "");
                        var ai = AccountInfoController.GetByUserID(acc.ID);
                        if (ai != null)
                        {
                            ac.FirstName = ai.FirstName;
                            ac.LastName = ai.LastName;
                            ac.IMGUser = ai.IMGUser;
                            ac.Phone = ai.Phone;
                            ac.Gender = Convert.ToInt32(ai.Gender);
                            ac.Address = ai.Address;
                            ac.BirthDay = Convert.ToDateTime(ai.BirthDay);
                        }
                    }
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Account = ac;
                }
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void ForgotPassword(string Email)
        {
            var rs = new ResponseClass();
            var user = AccountInfoController.GetByEmailFP(Email.Trim());
            if (user != null)
            {
                //string password = PJUtils.RandomStringWithText(10);
                string token = PJUtils.RandomStringWithText(15);

                //string kq = AccountController.UpdatePassword(Convert.ToInt32(user.UID), password);
                var tk = TokenForgotPassController.Insert(user.UID.Value, token, user.ID.ToString());
                if (tk != null)
                {
                    string link = "<a href=\"https://nhaphang360.vn/mat-khau-moi.aspx?token=" + token + "\">đây</a>";
                    try
                    {
                        PJUtils.SendMailGmail_new(Email.Trim(), "Reset Mật khẩu trên NHẬP HÀNG 360", "Vui lòng nhấn vào: <strong>" + link + "</strong> để thiết lập lại mật khẩu. Link chỉ sử dụng được 1 lần.", "");
                    }
                    catch
                    {

                    }
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "Hệ thống đã gửi 1 email mới cho bạn, vui lòng kiểm tra email và thiết lập lại mật khẩu.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "Email không tồn tại trong hệ thống.";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void ChangePassword(int UID, string Password, string NewPassword, string ConfirmNewPassword, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                if (NewPassword == ConfirmNewPassword)
                {
                    string ac = AccountController.UpdatePasswordSystem(UID, Password, NewPassword);
                    if (ac == "1")
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        rs.Message = "Đổi mật khẩu thành công.";
                    }
                    else if (ac == "fail")
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Mật khẩu cũ không đúng.";
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Tài khoản không tồn tại.";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Mật khẩu xác nhận không đúng.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetAllMenu(int UID, string Key)
        {
            ResponseClass rs = new ResponseClass();

            List<Menu> mn = new List<Menu>();

            Menu m1 = new Menu();
            m1.ItemName = "TRANG CHỦ";
            m1.GroupID = 0;
            m1.Parent = 0;
            m1.Link = "https://nhaphang360.vn/home-app.aspx?UID=";
            m1.ShowType = 1;
            m1.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_home.png";
            mn.Add(m1);

            Menu m2 = new Menu();
            m2.ItemName = "TRACKING";
            m2.GroupID = 0;
            m2.Parent = 0;
            m2.Link = "https://nhaphang360.vn/Tracking-app.aspx?UID=";
            m2.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_search.png";
            m2.ShowType = 1;
            mn.Add(m2);

            Menu m3 = new Menu();
            m3.ItemName = "MUA HÀNG HỘ";
            m3.GroupID = 1;
            m3.Parent = 0;
            m3.Link = "";
            m3.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_buy.png";
            m3.ShowType = 1;
            mn.Add(m3);

            bool IsOrderActive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsOrderActive"]);
            if (IsOrderActive != false)
            {
                Menu m4 = new Menu();
                m4.ItemName = "ĐẶT HÀNG";
                m4.GroupID = 1;
                m4.Parent = 1;
                m4.Link = "";
                m4.Icon = "";
                m4.ShowType = 2;
                mn.Add(m4);
            }

            //Menu m5 = new Menu();
            //m5.ItemName = "ĐẶT HÀNG NGOÀI HỆ THỐNG";
            //m5.GroupID = 1;
            //m5.Parent = 1;
            //m5.Link = "https://nhaphang360.vn/dat-hang-ngoai-he-thong-app.aspx?UID=";
            //m5.Icon = "";
            //m5.ShowType = 1;
            //mn.Add(m5);

            Menu m6 = new Menu();
            m6.ItemName = "ĐƠN HÀNG";
            m6.GroupID = 1;
            m6.Parent = 1;
            m6.Link = "https://nhaphang360.vn/don-hang-app.aspx?UID=";
            m6.Icon = "";
            m6.ShowType = 1;
            mn.Add(m6);

            //Menu m7 = new Menu();
            //m7.ItemName = "ĐƠN HÀNG TMĐT KHÁC";
            //m7.GroupID = 1;
            //m7.Parent = 1;
            //m7.Link = "https://nhaphang360.vn/danh-sach-don-hang-khac-app.aspx?UID=";
            //m7.Icon = "";
            //m7.ShowType = 1;
            //mn.Add(m7);


            //Menu m8 = new Menu();
            //m8.ItemName = "VẬN CHUYỂN KÝ GỬI";
            //m8.GroupID = 2;
            //m8.Parent = 0;
            //m8.Link = "";
            //m8.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_truck.png";
            //m8.ShowType = 1;
            //mn.Add(m8);

            //Menu m20 = new Menu();
            //m20.ItemName = "TẠO KIỆN KÝ GỬI";
            //m20.GroupID = 2;
            //m20.Parent = 1;
            //m20.Link = "https://nhaphang360.vn/tao-ma-van-don-ky-gui-app.aspx?UID=";
            //m20.Icon = "";
            //m20.ShowType = 1;
            //mn.Add(m20);

            //Menu m9 = new Menu();
            //m9.ItemName = "DANH SÁCH KIỆN KÝ GỬI";
            //m9.GroupID = 2;
            //m9.Parent = 1;
            //m9.Link = "https://nhaphang360.vn/danh-sach-kien-ky-gui-app.aspx?UID=";
            //m9.Icon = "";
            //m9.ShowType = 1;
            //mn.Add(m9);

            //Menu m25 = new Menu();
            //m25.ItemName = "THỐNG KÊ CƯỚC VC";
            //m25.GroupID = 2;
            //m25.Parent = 1;
            //m25.Link = "https://nhaphang360.vn/thong-ke-cuoc-ky-gui-app.aspx?UID=";
            //m25.Icon = "";
            //m25.ShowType = 1;
            //mn.Add(m25);


            //Menu m10 = new Menu();
            //m10.ItemName = "THANH TOÁN HỘ";
            //m10.GroupID = 3;
            //m10.Parent = 0;
            //m10.Link = "";
            //m10.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_dollar.png";
            //m10.ShowType = 1;
            //mn.Add(m10);

            //Menu m11 = new Menu();
            //m11.ItemName = "TẠO ĐƠN";
            //m11.GroupID = 3;
            //m11.Parent = 1;
            //m11.Link = "https://nhaphang360.vn/tao-don-thanh-toan-ho-app.aspx?UID=";
            //m11.Icon = "";
            //m11.ShowType = 1;
            //mn.Add(m11);

            //Menu m12 = new Menu();
            //m12.ItemName = "DANH SÁCH ĐƠN";
            //m12.GroupID = 3;
            //m12.Parent = 1;
            //m12.Link = "https://nhaphang360.vn/thanh-toan-ho-app.aspx?UID=";
            //m12.Icon = "";
            //m12.ShowType = 1;
            //mn.Add(m12);

            Menu m13 = new Menu();
            m13.ItemName = "TÀI CHÍNH";
            m13.GroupID = 4;
            m13.Parent = 0;
            m13.Link = "";
            m13.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_wallet.png";
            m13.ShowType = 1;
            mn.Add(m13);

            Menu m14 = new Menu();
            m14.ItemName = "LỊCH SỬ GIAO DỊCH";
            m14.GroupID = 4;
            m14.Parent = 1;
            m14.Link = "https://nhaphang360.vn/lich-su-giao-dich-app.aspx?UID=";
            m14.Icon = "";
            m14.ShowType = 1;
            mn.Add(m14);

            //Menu m15 = new Menu();
            //m15.ItemName = "LỊCH SỬ GIAO DỊCH TỆ";
            //m15.GroupID = 4;
            //m15.Parent = 1;
            //m15.Link = "https://nhaphang360.vn/lich-su-giao-dich-tien-te-app.aspx?UID=";
            //m15.Icon = "";
            //m15.ShowType = 1;
            //mn.Add(m15);

            Menu m16 = new Menu();
            m16.ItemName = "NẠP TIỀN";
            m16.GroupID = 4;
            m16.Parent = 1;
            m16.Link = "https://nhaphang360.vn/nap-tien-app.aspx?UID=";
            m16.Icon = "";
            m16.ShowType = 1;
            mn.Add(m16);

            //Menu m18 = new Menu();
            //m18.ItemName = "NẠP TIỀN TỆ";
            //m18.GroupID = 4;
            //m18.Parent = 1;
            //m18.Link = "https://nhaphang360.vn/rut-tien-te-app.aspx?UID=";
            //m18.Icon = "";
            //m18.ShowType = 1;
            //mn.Add(m18);

            Menu m17 = new Menu();
            m17.ItemName = "RÚT TIỀN";
            m17.GroupID = 4;
            m17.Parent = 1;
            m17.Link = "https://nhaphang360.vn/rut-tien-app.aspx?UID=";
            m17.Icon = "";
            m17.ShowType = 1;
            mn.Add(m17);

            Menu m19 = new Menu();
            m19.ItemName = "KHIẾU NẠI";
            m19.GroupID = 0;
            m19.Parent = 0;
            m19.Link = "https://nhaphang360.vn/khieu-nai-app.aspx?UID=";
            m19.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_mark.png";
            m19.ShowType = 1;
            mn.Add(m19);

            Menu m21 = new Menu();
            m21.ItemName = "THÔNG BÁO HỆ THỐNG";
            m21.GroupID = 0;
            m21.Parent = 0;
            m21.Link = "https://nhaphang360.vn/danh-sach-thong-bao-appv2.aspx?title=tbht&UID=";
            m21.Icon = "https://nhaphang360.vn/App_Themes/App/images/ic_mark.png";
            m21.ShowType = 1;
            mn.Add(m21);

            if (UID > 0 && !string.IsNullOrEmpty(Key))
            {
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.ListMenu = mn;
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "";
                    rs.Logout = "1";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.ListMenu = mn;
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void CreateOrderCustomV2(int UID, string wareship, string listshop, string Key, string FullName, string Phone, string Email, string Address)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
                var setNoti = SendNotiEmailController.GetByID(5);
                var obj_user = AccountController.GetByID(UID);
                if (obj_user != null)
                {
                    if (!string.IsNullOrEmpty(listshop))
                    {
                        try
                        {
                            string[] list = listshop.Split('|');
                            #region Update tổng tiền
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                    if (oshops != null)
                                    {
                                        double TotalPriceShop = 0;
                                        double TotalPriceShopCNY = 0;
                                        OrderShop or = new OrderShop();
                                        or.OrderShopID = oshops.ID;
                                        or.OrderShopName = oshops.ShopName;
                                        or.IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                        List<OrderTemp> lot = new List<OrderTemp>();
                                        List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, oshops.ID);
                                        if (ors != null)
                                        {
                                            if (ors.Count > 0)
                                            {
                                                foreach (var item in ors)
                                                {
                                                    OrderTemp ot = new OrderTemp();
                                                    int ID = item.ID;
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

                                                    e_totalproduct = e_pricevn + e_pricetemp;

                                                    TotalPriceShop += e_totalproduct;

                                                    TotalPriceShopCNY += e_pricebuy;
                                                }
                                                OrderShopTempController.UpdateNoteFastPriceVND(oshops.ID, oshops.Note, TotalPriceShop.ToString());
                                                OrderShopTempController.UpdatePriceVNDCNY(oshops.ID, TotalPriceShop.ToString(), TotalPriceShopCNY.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Update phí kiểm đếm
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    var goshop = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                    if (goshop != null)
                                    {
                                        if (goshop.IsCheckProduct == true)
                                        {

                                            double total = 0;
                                            var listpro = OrderTempController.GetAllByOrderShopTempID(goshop.ID);
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

                                            total = Math.Round(total, 0);
                                            OrderShopTempController.UpdateCheckProductPrice(goshop.ID, total.ToString());
                                        }
                                    }
                                }
                            }
                            #endregion

                            int salerID = obj_user.SaleID.ToString().ToInt(0);
                            int dathangID = obj_user.DathangID.ToString().ToInt(0);
                            double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                            double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                            double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                            int sleep = 1;
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    sleep++;
                                    CreateData dt = new CreateData();
                                    dt.UID = obj_user.ID;
                                    dt.ShopTempID = sID;
                                    dt.wareship = wareship;
                                    Thread th = new Thread(CreateOrder);
                                    th.Start(dt);
                                }
                            }
                            int time = 2000 * sleep;
                            System.Threading.Thread.Sleep(time);

                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.Message = "Đặt hàng thành công.";
                        }
                        catch (Exception ex)
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
                            rs.Message = "Có lỗi trong quá trình xử lý.";
                        }
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Không có shop nào được chọn.";
                    }

                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }
        public class CreateData
        {
            public int UID { get; set; }
            public int ShopTempID { get; set; }
            public string wareship { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void ReviewOrder(int UID, string wareship, string listshop, string Key)
        {
            var rs = new ResponseClass();
            double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
            double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var obj_user = AccountController.GetByID(UID);
                if (obj_user != null)
                {
                    if (!string.IsNullOrEmpty(listshop))
                    {
                        try
                        {
                            string[] list = listshop.Split('|');
                            #region Update tổng tiền
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                    if (oshops != null)
                                    {
                                        double TotalPriceShop = 0;
                                        double TotalPriceShopCNY = 0;
                                        OrderShop or = new OrderShop();
                                        or.OrderShopID = oshops.ID;
                                        or.OrderShopName = oshops.ShopName;
                                        or.IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                        List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, oshops.ID);
                                        if (ors != null)
                                        {
                                            if (ors.Count > 0)
                                            {
                                                foreach (var item in ors)
                                                {
                                                    OrderTemp ot = new OrderTemp();
                                                    int ID = item.ID;
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

                                                    e_totalproduct = e_pricevn + e_pricetemp;

                                                    TotalPriceShop += e_totalproduct;

                                                    TotalPriceShopCNY += e_pricebuy;
                                                }
                                                OrderShopTempController.UpdateNoteFastPriceVND(oshops.ID, oshops.Note, TotalPriceShop.ToString());
                                                OrderShopTempController.UpdatePriceVNDCNY(oshops.ID, TotalPriceShop.ToString(), TotalPriceShopCNY.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Update phí kiểm đếm
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    var goshop = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                    if (goshop != null)
                                    {
                                        if (goshop.IsCheckProduct == true)
                                        {

                                            double total = 0;
                                            var listpro = OrderTempController.GetAllByOrderShopTempID(goshop.ID);
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

                                            total = Math.Round(total, 0);
                                            OrderShopTempController.UpdateCheckProductPrice(goshop.ID, total.ToString());
                                        }
                                    }
                                }
                            }
                            #endregion

                            int salerID = obj_user.SaleID.ToString().ToInt(0);
                            int dathangID = obj_user.DathangID.ToString().ToInt(0);
                            double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                            double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                            double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                            Customer cus = new Customer();

                            string FullName = "";
                            string Address = "";
                            string Email = "";
                            string Phone = "";
                            var ui = AccountInfoController.GetByUserID(obj_user.ID);
                            if (ui != null)
                            {
                                FullName = ui.FirstName + " " + ui.LastName;
                                Address = ui.Address;
                                Email = ui.Email;
                                Phone = ui.MobilePhonePrefix + ui.MobilePhone;

                                cus.FullName = FullName;
                                cus.Address = Address;
                                cus.Email = Email;
                                cus.Phone = Phone;
                            }

                            List<OrderShop> os = new List<OrderShop>();
                            if (list.Length - 1 > 0)
                            {
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int sID = list[i].ToInt(0);
                                    var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                    if (oshops != null)
                                    {
                                        int warehouseFromID = 1;
                                        int warehouseID = 1;
                                        int w_shippingType = 1;

                                        string[] w = wareship.Split('|');
                                        if (w.Length - 1 > 0)
                                        {
                                            for (int j = 0; j < w.Length - 1; j++)
                                            {
                                                //id + "-" + warehousefromID + "-" + shippingtype + "-" + warehouseID + "|"
                                                string[] w1 = w[j].Split('-');
                                                int shoptempID = w1[0].ToInt(0);

                                                int wareID = w1[3].ToInt(1);
                                                int shippingtype = w1[2].ToInt(1);
                                                if (oshops.ID == shoptempID)
                                                {
                                                    warehouseID = wareID;
                                                    w_shippingType = shippingtype;
                                                    warehouseFromID = w1[1].ToInt(2);
                                                }
                                            }
                                        }

                                        OrderShop or = new OrderShop();
                                        or.OrderShopID = oshops.ID;
                                        or.OrderShopName = oshops.ShopName;
                                        or.Note = oshops.Note;
                                        or.IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                        or.IsCheckPacked = Convert.ToBoolean(oshops.IsPacked);
                                        or.IsCheckFastDelivery = Convert.ToBoolean(oshops.IsFastDelivery);
                                        or.WarehouseFrom = warehouseFromID;
                                        or.WarehouseTo = warehouseID;
                                        or.ShippingType = w_shippingType;

                                        double total = 0;
                                        double fastprice = 0;
                                        double pricepro = Math.Round(Convert.ToDouble(oshops.PriceVND), 0);
                                        double priceproCYN = Math.Round(Convert.ToDouble(oshops.PriceCNY), 2);

                                        double feecnship = 0;
                                        if (oshops.IsFast == true)
                                        {
                                            fastprice = Math.Round((pricepro * 5 / 100), 0);
                                        }

                                        string ShopID = oshops.ShopID;
                                        string ShopName = oshops.ShopName;
                                        string Site = oshops.Site;
                                        bool IsForward = Convert.ToBoolean(oshops.IsForward);
                                        string IsForwardPrice = oshops.IsForwardPrice;
                                        bool IsFastDelivery = Convert.ToBoolean(oshops.IsFastDelivery);
                                        string IsFastDeliveryPrice = oshops.IsFastDeliveryPrice;
                                        bool IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                        string IsCheckProductPrice = oshops.IsCheckProductPrice;
                                        bool IsPacked = Convert.ToBoolean(oshops.IsPacked);
                                        string IsPackedPrice = oshops.IsPackedPrice;
                                        bool IsFast = Convert.ToBoolean(oshops.IsFast);
                                        string IsFastPrice = fastprice.ToString();
                                        double pricecynallproduct = 0;

                                        double totalFee_CountFee = fastprice + pricepro + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0);
                                        double servicefee = 0;

                                        var adminfeebuypro = FeeBuyProController.GetAll();
                                        if (adminfeebuypro.Count > 0)
                                        {
                                            foreach (var temp in adminfeebuypro)
                                            {
                                                if (pricepro >= temp.AmountFrom && pricepro < temp.AmountTo)
                                                {
                                                    servicefee = temp.FeePercent.ToString().ToFloat(0) / 100;
                                                    break;
                                                }
                                            }
                                        }
                                        double feebp = 0;
                                        double feebpnotdc = 0;
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
                                        feebp = feebpnotdc - subfeebp;
                                        feebp = Math.Round(feebp, 0);
                                        if (feebp < 10000)
                                            feebp = 10000;

                                        double InsuranceMoney = 0;
                                        if (oshops.IsInsurrance == true)
                                            InsuranceMoney = pricepro * (InsurancePercent / 100);

                                        total = fastprice + pricepro + feebp + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0) + InsuranceMoney;

                                        //Lấy ra từng ordertemp trong shop
                                        List<OrderTemp> lot = new List<OrderTemp>();
                                        var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(oshops.ID);
                                        if (proOrdertemp != null)
                                        {
                                            if (proOrdertemp.Count > 0)
                                            {
                                                foreach (var temp in proOrdertemp)
                                                {
                                                    int quantity = Convert.ToInt32(temp.quantity);
                                                    double originprice = Convert.ToDouble(temp.price_origin);
                                                    double promotionprice = Convert.ToDouble(temp.price_promotion);

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

                                                    string image = temp.image_origin;
                                                    if (image.Contains("%2F"))
                                                    {
                                                        image = image.Replace("%2F", "/");
                                                    }
                                                    if (image.Contains("%3A"))
                                                    {
                                                        image = image.Replace("%3A", ":");
                                                    }
                                                    string linkproduct = temp.link_origin;
                                                    string productname = temp.title_origin;

                                                    OrderTemp ot = new OrderTemp();
                                                    ot.Image = image;
                                                    ot.LinkProduct = linkproduct;
                                                    ot.Brand = temp.brand;
                                                    ot.ProductName = temp.title_origin;
                                                    ot.Property = temp.property;
                                                    ot.OrderTempID = temp.ID;
                                                    ot.Quantity = quantity;
                                                    ot.PriceVN = string.Format("{0:N0}", Convert.ToDouble(u_pricevn));
                                                    ot.PriceCNY = u_pricecbuy.ToString();
                                                    ot.TotalPriceVN = string.Format("{0:N0}", Convert.ToDouble(e_pricevn));
                                                    ot.TotalPriceCNY = e_pricebuy.ToString();

                                                    lot.Add(ot);
                                                }
                                            }
                                        }

                                        string PriceVND = oshops.PriceVND;
                                        string PriceCNY = pricecynallproduct.ToString();
                                        string FeeShipCN = feecnship.ToString();
                                        string FeeBuyPro = feebp.ToString();
                                        string FeeWeight = oshops.FeeWeight;
                                        string Note = oshops.Note;
                                        string CurrentCNYVN = current.ToString();
                                        string TotalPriceVND = Math.Round(total, 0).ToString();

                                        or.TotalPriceVND = TotalPriceVND;
                                        or.FeeBuyPro = FeeBuyPro;
                                        or.FeeCheck = Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0).ToString();
                                        or.ListProduct = lot;
                                        os.Add(or);
                                    }
                                }
                            }
                            cus.ListShop = os;
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.data = cus;
                        }
                        catch (Exception ex)
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
                            rs.Message = "Có lỗi trong quá trình xử lý.";
                        }
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Không có shop nào được chọn.";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tài khoản không tồn tại.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }


        public void CreateOrder(object ob)
        {
            CreateData dt = (CreateData)ob;
            ThreadCreateOrder(dt.UID, dt.ShopTempID, dt.wareship, dt.FullName, dt.Address, dt.Email, dt.Phone);
        }

        public void ThreadCreateOrder(int UID, int sID, string wareship, string FullName, string Address, string Email, string Phone)
        {
            try
            {
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                double currentreal = Convert.ToDouble(ConfigurationController.GetByTop1().CurrencyReal);
                double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
                var setNoti = SendNotiEmailController.GetByID(5);
                var obj_user = AccountController.GetByID(UID);
                if (obj_user != null)
                {
                    int salerID = obj_user.SaleID.ToString().ToInt(0);
                    int dathangID = obj_user.DathangID.ToString().ToInt(0);
                    double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                    double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                    double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                    var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                    if (oshops != null)
                    {
                        int warehouseFromID = 1;
                        int warehouseID = 1;
                        int w_shippingType = 1;

                        string[] w = wareship.Split('|');
                        if (w.Length - 1 > 0)
                        {
                            for (int j = 0; j < w.Length - 1; j++)
                            {
                                //id + "-" + warehousefromID + "-" + shippingtype + "-" + warehouseID + "|"
                                string[] w1 = w[j].Split('-');
                                int shoptempID = w1[0].ToInt(0);

                                int wareID = w1[3].ToInt(1);
                                int shippingtype = w1[2].ToInt(1);
                                if (oshops.ID == shoptempID)
                                {
                                    warehouseID = wareID;
                                    w_shippingType = shippingtype;
                                    warehouseFromID = w1[1].ToInt(2);
                                }
                            }
                        }

                        double total = 0;
                        double fastprice = 0;
                        double pricepro = Math.Round(Convert.ToDouble(oshops.PriceVND), 0);
                        double priceproCYN = Math.Round(Convert.ToDouble(oshops.PriceCNY), 2);

                        double feecnship = 0;

                        if (oshops.IsFast == true)
                        {
                            fastprice = Math.Round((pricepro * 5 / 100), 0);
                        }

                        string ShopID = oshops.ShopID;
                        string ShopName = oshops.ShopName;
                        string Site = oshops.Site;
                        bool IsForward = Convert.ToBoolean(oshops.IsForward);
                        string IsForwardPrice = oshops.IsForwardPrice;
                        bool IsFastDelivery = Convert.ToBoolean(oshops.IsFastDelivery);
                        string IsFastDeliveryPrice = oshops.IsFastDeliveryPrice;
                        bool IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                        string IsCheckProductPrice = oshops.IsCheckProductPrice;
                        bool IsPacked = Convert.ToBoolean(oshops.IsPacked);
                        string IsPackedPrice = oshops.IsPackedPrice;
                        bool IsFast = Convert.ToBoolean(oshops.IsFast);
                        string IsFastPrice = fastprice.ToString();
                        double pricecynallproduct = 0;

                        double totalFee_CountFee = fastprice + pricepro + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0);
                        double servicefee = 0;
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var temp in adminfeebuypro)
                            {
                                if (pricepro >= temp.AmountFrom && pricepro < temp.AmountTo)
                                {
                                    servicefee = temp.FeePercent.ToString().ToFloat(0) / 100;
                                    //servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                    break;
                                }
                            }
                        }

                        double feebp = 0;
                        double feebpnotdc = 0;
                        string PriceVND = oshops.PriceVND;                       
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
                        feebp = feebpnotdc - subfeebp;
                        feebp = Math.Round(feebp, 0);
                        if (feebp < 10000)
                            feebp = 10000;

                        double InsuranceMoney = 0;
                        if (oshops.IsInsurrance == true)
                            InsuranceMoney = pricepro * (InsurancePercent / 100);

                        total = fastprice + pricepro + feebp + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0) + InsuranceMoney;

                        //Lấy ra từng ordertemp trong shop
                        var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(oshops.ID);
                        if (proOrdertemp != null)
                        {
                            if (proOrdertemp.Count > 0)
                            {
                                foreach (var temp in proOrdertemp)
                                {
                                    int quantity = Convert.ToInt32(temp.quantity);
                                    double originprice = Convert.ToDouble(temp.price_origin);
                                    double promotionprice = Convert.ToDouble(temp.price_promotion);
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
                                }
                            }
                        }

                        string PriceCNY = pricecynallproduct.ToString();
                        //string FeeShipCN = (10 * current).ToString();
                        string FeeShipCN = feecnship.ToString();
                        string FeeBuyPro = feebp.ToString();
                        string FeeWeight = oshops.FeeWeight;
                        string Note = oshops.Note;
                        int Status = 0;
                        string Deposit = "0";
                        string CurrentCNYVN = current.ToString();
                        string TotalPriceVND = Math.Round(total, 0).ToString();
                        string AmountDeposit = Math.Round((pricepro * LessDeposit / 100)).ToString();
                        DateTime CreatedDate = DateTime.Now;
                        string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice,
                            IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN,
                            TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1, FeeBuyProUser, currentreal.ToString());
                        int idkq = Convert.ToInt32(kq);
                        if (idkq > 0)
                        {
                            foreach (var temp in proOrdertemp)
                            {
                                int quantity = Convert.ToInt32(temp.quantity);
                                double originprice = Convert.ToDouble(temp.price_origin);
                                double promotionprice = Convert.ToDouble(temp.price_promotion);
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

                                string image = temp.image_origin;
                                if (image.Contains("%2F"))
                                {
                                    image = image.Replace("%2F", "/");
                                }
                                if (image.Contains("%3A"))
                                {
                                    image = image.Replace("%3A", ":");
                                }
                                string ret = OrderController.Insert(UID, temp.title_origin, temp.title_translated, temp.price_origin, temp.price_promotion, temp.property_translated,
                                temp.property, temp.data_value, image, image, temp.shop_id, temp.shop_name, temp.seller_id, temp.wangwang, temp.quantity,
                                temp.stock, temp.location_sale, temp.site, temp.comment, temp.item_id, temp.link_origin, temp.outer_id, temp.error, temp.weight, temp.step, temp.stepprice, temp.brand,
                                temp.category_name, temp.category_id, temp.tool, temp.version, Convert.ToBoolean(temp.is_translate), Convert.ToBoolean(temp.IsForward), "0",
                                Convert.ToBoolean(temp.IsFastDelivery), "0", Convert.ToBoolean(temp.IsCheckProduct), "0", Convert.ToBoolean(temp.IsPacked), "0", Convert.ToBoolean(temp.IsFast),
                                fastprice.ToString(), pricepro.ToString(), PriceCNY, temp.Note, FullName, Address, Email,
                                Phone, 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID);

                                if (temp.price_promotion.ToFloat(0) > 0)
                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), temp.price_origin, temp.price_promotion);
                                else
                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), temp.price_origin, temp.price_origin);
                            }
                            MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);
                            MainOrderController.UpdateFromPlace(idkq, UID, warehouseFromID, w_shippingType);
                            MainOrderController.UpdateIsInsurrance(idkq, Convert.ToBoolean(oshops.IsInsurrance));
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
                                            NotificationsController.Inser(admin.ID, admin.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
             1, CreatedDate, obj_user.Username, false);

                                        }
                                    }
                                    var managers = AccountController.GetAllByRoleID(2);
                                    if (managers.Count > 0)
                                    {
                                        foreach (var manager in managers)
                                        {
                                            NotificationsController.Inser(manager.ID, manager.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
             1, CreatedDate, obj_user.Username, false);
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
                                                    "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");

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
                                                    "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");


                                            }
                                            catch { }
                                        }
                                    }

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
                                    StaffIncomeController.Insert(idkq, "0", salepercentaf3m.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, CreatedDate, obj_user.Username);
                                }
                                else
                                {
                                    double per = feebp * salepercent / 100;
                                    StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, CreatedDate, obj_user.Username);
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
                                    CreatedDate, CreatedDate, obj_user.Username);
                                if (setNoti != null)
                                {
                                    if (setNoti.IsSentNotiAdmin == true)
                                    {
                                        NotificationsController.Inser(dathang.ID, dathang.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
               1, CreatedDate, obj_user.Username, false);
                                    }

                                    if (setNoti.IsSentEmailAdmin == true)
                                    {
                                        try
                                        {
                                            PJUtils.SendMailGmail_new(dathang.Email,
                                                     "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");
                                        }
                                        catch { }
                                    }
                                }

                            }
                        }
                        //Xóa Shop temp và order temp
                        OrderShopTempController.Delete(oshops.ID);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetAllNoti(int UID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var u = AccountController.GetByID(UID);
                if (u != null)
                {
                    List<Noti> nt = new List<Noti>();

                    var lNotiSingle = NotificationsController.GetAll(UID).OrderByDescending(x => x.ID).ToList();
                    if (lNotiSingle.Count > 0)
                    {
                        foreach (var item in lNotiSingle)
                        {
                            Noti n = new Noti();
                            n.NotificationID = Convert.ToInt32(item.ID);
                            n.Status = Convert.ToInt32(item.Status);
                            DateTime cre = Convert.ToDateTime(item.CreatedDate);
                            string mess = cre.ToString("HH:mm dd/MM") + " " + item.Message;
                            n.Message = mess;

                            if (item.NotifType == 1)
                            {
                                n.Link = "https://nhaphang360.vn/chi-tiet-don-hang-app.aspx?UID=" + UID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";
                                n.Type = 1;
                            }
                            if (item.NotifType == 2)
                            {
                                n.Link = "https://nhaphang360.vn/lich-su-giao-dich-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 1;
                            }
                            if (item.NotifType == 3)
                            {
                                n.Link = "https://nhaphang360.vn/lich-su-giao-dich-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 1;
                            }
                            if (item.NotifType == 5)
                            {
                                n.Link = "https://nhaphang360.vn/khieu-nai-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 2;
                            }
                            if (item.NotifType == 8)
                            {
                                n.Link = "https://nhaphang360.vn/thanh-toan-ho-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 2;
                            }
                            if (item.NotifType == 9)
                            {
                                n.Link = "https://nhaphang360.vn/lich-su-giao-dich-tien-te-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 2;
                            }
                            if (item.NotifType == 10)
                            {
                                n.Link = "https://nhaphang360.vn/van-chuyen-ho-app.aspx?UID=" + UID + "&Key=" + Key + "";
                                n.Type = 2;
                            }
                            if (item.NotifType == 11)
                            {
                                n.Link = "https://nhaphang360.vn/chi-tiet-don-hang-khac-app.aspx?UID=" + UID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";
                                n.Type = 1;
                            }

                            if (item.NotifType == 12)
                            {
                                n.Link = "https://nhaphang360.vn/chi-tiet-don-hang-app.aspx?UID=" + UID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";
                                n.Type = 1;
                            }

                            if (item.NotifType == 13)
                            {
                                n.Link = "https://nhaphang360.vn/chi-tiet-don-hang-khac-app.aspx?UID=" + UID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";
                                n.Type = 1;
                            }
                            nt.Add(n);
                        }
                    }

                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.ListNoti = nt;
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tài khoản không tồn tại.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateNoti(int UID, int NotificationID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var u = AccountController.GetByID(UID);
                if (u != null)
                {
                    var noti = NotificationsController.GetByID(NotificationID);
                    if (noti != null)
                    {
                        NotificationsController.UpdateNoti(Convert.ToInt32(noti.ID), DateTime.Now, u.Username);
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        rs.Message = "Thành công.";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tài khoản không tồn tại.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetWareHouse(int UID, string Key)
        {
            ResponseClass rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var obj_user = AccountController.GetByID(UID);
                if (obj_user != null)
                {
                    int khoTQ = Convert.ToInt32(obj_user.WarehouseFrom);
                    int khoVN = Convert.ToInt32(obj_user.WarehouseTo);
                    int ship = Convert.ToInt32(obj_user.ShippingType);

                    #region KhoTQ
                    List<WareHouseFrom> lwhf = new List<WareHouseFrom>();
                    List<WareHouseFrom> lwhf1 = new List<WareHouseFrom>();
                    var warehouseTQ = WarehouseFromController.GetAllWithIsHidden(false);
                    if (warehouseTQ.Count > 1)
                    {
                        foreach (var item in warehouseTQ)
                        {
                            if (khoTQ == item.ID)
                            {
                                WareHouseFrom whf = new WareHouseFrom();
                                whf.ID = item.ID;
                                whf.WareHouseName = item.WareHouseName;
                                lwhf.Add(whf);
                            }
                            else
                            {
                                WareHouseFrom whf = new WareHouseFrom();
                                whf.ID = item.ID;
                                whf.WareHouseName = item.WareHouseName;
                                lwhf1.Add(whf);
                            }
                        }
                    }
                    lwhf.AddRange(lwhf1);
                    #endregion
                    #region Kho VN
                    List<WareHouseTo> lwht = new List<WareHouseTo>();
                    List<WareHouseTo> lwht1 = new List<WareHouseTo>();

                    var warehouses = WarehouseController.GetAllWithIsHidden(false);
                    if (warehouses.Count > 1)
                    {
                        foreach (var item in warehouses)
                        {
                            if (item.ID == khoVN)
                            {
                                WareHouseTo wht = new WareHouseTo();
                                wht.ID = item.ID;
                                wht.WareHouseName = item.WareHouseName;
                                lwht.Add(wht);
                            }
                            else
                            {
                                WareHouseTo wht = new WareHouseTo();
                                wht.ID = item.ID;
                                wht.WareHouseName = item.WareHouseName;
                                lwht1.Add(wht);
                            }
                        }
                    }
                    lwht.AddRange(lwht1);
                    #endregion
                    #region Shipping
                    List<ShippingType> lsp = new List<ShippingType>();
                    List<ShippingType> lsp1 = new List<ShippingType>();

                    var shippingType = ShippingTypeToWareHouseController.GetAllWithIsHidden(false);
                    if (shippingType.Count > 1)
                    {
                        foreach (var item in shippingType)
                        {
                            if (ship == item.ID)
                            {
                                ShippingType sp = new ShippingType();
                                sp.ID = item.ID;
                                sp.ShippingName = item.ShippingTypeName;
                                lsp.Add(sp);
                            }
                            else
                            {
                                ShippingType sp = new ShippingType();
                                sp.ID = item.ID;
                                sp.ShippingName = item.ShippingTypeName;
                                lsp1.Add(sp);
                            }
                        }
                    }
                    lsp.AddRange(lsp1);
                    #endregion

                    WareHouse wh = new WareHouse();
                    wh.WareHouseFrom = lwhf;
                    wh.WareHouseTo = lwht;
                    wh.ShippingType = lsp;

                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.WateHouse = wh;
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "Tài khoản không tồn tại.";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetAllCart(int UID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var u = AccountController.GetByID(UID);
                if (u != null)
                {
                    double pricecurrency = 0;
                    if (Convert.ToDouble(u.Currency) > 0)
                    {
                        pricecurrency = Convert.ToDouble(u.Currency);
                    }
                    else
                    {
                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                            pricecurrency = Convert.ToDouble(config.Currency);
                    }
                    //double pricecurrency = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                    List<OrderShop> os = new List<OrderShop>();
                    var oshop = OrderShopTempController.GetByUID(UID);
                    if (oshop != null)
                    {
                        if (oshop.Count > 0)
                        {
                            double total = 0;
                            foreach (var shop in oshop)
                            {
                                double TotalPriceShop = 0;
                                OrderShop or = new OrderShop();
                                or.OrderShopID = shop.ID;
                                or.OrderShopName = shop.ShopName;
                                or.Note = shop.Note;
                                or.IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                                or.IsCheckPacked = Convert.ToBoolean(shop.IsPacked);
                                or.IsCheckFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                                List<OrderTemp> lot = new List<OrderTemp>();
                                List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, shop.ID);
                                if (ors != null)
                                {
                                    if (ors.Count > 0)
                                    {
                                        foreach (var item in ors)
                                        {
                                            OrderTemp ot = new OrderTemp();
                                            int ID = item.ID;
                                            string linkproduct = item.link_origin;
                                            string productname = item.title_origin;
                                            string brand = item.brand;
                                            string image = item.image_origin;
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

                                            TotalPriceShop += e_totalproduct;
                                            //pricefullcart += e_totalproduct;

                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }

                                            ot.Image = image;
                                            ot.LinkProduct = linkproduct;
                                            ot.Brand = brand;
                                            ot.ProductName = productname;
                                            ot.Property = item.property;
                                            ot.OrderTempID = ID;
                                            ot.Quantity = quantity;
                                            //ot.PriceVN = string.Format("{0:N0}", Convert.ToDouble(u_pricevn));
                                            //ot.PriceCNY = string.Format("{0:N0}", Convert.ToDouble(u_pricecbuy));
                                            //ot.TotalPriceVN = string.Format("{0:N0}", Convert.ToDouble(e_pricevn));
                                            //ot.TotalPriceCNY = string.Format("{0:N0}", Convert.ToDouble(e_pricebuy));

                                            ot.PriceVN = string.Format("{0:N0}", Convert.ToDouble(u_pricevn));
                                            ot.PriceCNY = u_pricecbuy.ToString();
                                            ot.TotalPriceVN = string.Format("{0:N0}", Convert.ToDouble(e_pricevn));
                                            ot.TotalPriceCNY = e_pricebuy.ToString();

                                            lot.Add(ot);

                                            total += u_pricevn;
                                        }
                                        //OrderShopTempController.UpdateNoteFastPriceVND(shop.ID, shop.Note, TotalPriceShop.ToString());
                                    }
                                }
                                or.ListProduct = lot;
                                os.Add(or);
                            }
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                            rs.Status = APIUtils.ResponseMessage.Success.ToString();
                            rs.ListOrderShop = os;
                            rs.Message = string.Format("{0:N0}", Convert.ToDouble(total)) + " VNĐ";
                        }
                        else
                        {
                            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                            rs.Status = APIUtils.ResponseMessage.Error.ToString();
                            rs.Message = "Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.";
                        }
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Hiện tại không có sản phẩm nào trong giỏ hàng của bạn.";
                    }
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        private static object _locker = new object();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddToCart(int UID, string title_origin, string title_translated, string price_origin, string price_promotion, string property_translated, string property, string data_value,
           string image_model, string image_origin, string shop_id, string shop_name, string seller_id, string wangwang, string quantity, string stock, string location_sale, string site, string comment,
           string item_id, string link_origin, string outer_id, string error, string weight, string step, string pricestep, string brand, string category_name, string category_id, string tool, string version, string is_translate, string Key)
        {
            lock (_locker)
            {
                var rs = new ResponseClass();
                var tk = DeviceTokenController.GetByToken(UID, Key);
                if (tk != null)
                {
                    var obj_user = AccountController.GetByID(UID);
                    if (obj_user != null)
                    {
                        var id = OrderShopTempController.Insert(UID, shop_id, shop_name, site, DateTime.Now);
                        if (id.ToInt() > 0)
                        {
                            string decodedUrl = Uri.UnescapeDataString(link_origin);

                            string link_new = decodedUrl;
                            if (site == "taobao")
                            {
                                link_new = "https://item.taobao.com/item.htm?id=" + item_id;
                            }
                            else if (site == "1688")
                            {
                                link_new = "https://detail.1688.com/offer/" + item_id + ".html";
                            }
                            else
                            {
                                link_new = "https://detail.tmall.com/item.htm?id=" + item_id;
                            }

                            string kq = OrderTempController.Insert(UID, Convert.ToInt32(id), title_origin, title_translated, price_origin, price_promotion, property_translated,
                                                            property, data_value, image_model, image_origin, shop_id, shop_name, seller_id, wangwang, quantity,
                                                            stock, location_sale, site, comment, item_id, link_new, outer_id, error, weight,
                                                            step, pricestep, comment, category_name, category_id, tool, version, Convert.ToBoolean(is_translate), DateTime.Now);

                            //if (string.IsNullOrEmpty(price_origin))
                            //{
                            //    try
                            //    {
                            //        PJUtils.SendMailGmail_new("phuong@mona.media",
                            //            "Thông báo tại NHẬP HÀNG 360.", "Giá gốc của sản phẩm truyền lên bị rỗng.", "");
                            //    }
                            //    catch { }
                            //}
                            //if (string.IsNullOrEmpty(price_promotion))
                            //{
                            //    try
                            //    {
                            //        PJUtils.SendMailGmail_new("phuong@mona.media",
                            //            "Thông báo tại NHẬP HÀNG 360.", "Giá khuyến mãi của sản phẩm truyền lên bị rỗng.", "");
                            //    }
                            //    catch { }
                            //}
                            //if (string.IsNullOrEmpty(link_origin))
                            //{
                            //    try
                            //    {
                            //        PJUtils.SendMailGmail_new("phuong@mona.media",
                            //            "Thông báo tại NHẬP HÀNG 360.", "Link của sản phẩm truyền lên bị rỗng.", "");
                            //    }
                            //    catch { }
                            //}
                            if (kq.ToInt(0) > 0)
                            {
                                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                                rs.Message = "Thêm vào giỏ hàng thành công.";
                            }
                        }
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Tài khoản không tồn tại.";
                    }
                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "";
                    rs.Logout = "1";
                }
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
                Context.Response.Flush();
                Context.Response.End();
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void CreateOrderCustom(int UID, string wareship, string listshop, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                double InsurancePercent = Convert.ToDouble(ConfigurationController.GetByTop1().InsurancePercent);
                double currentreal = Convert.ToDouble(ConfigurationController.GetByTop1().CurrencyReal);
                var setNoti = SendNotiEmailController.GetByID(5);
                var obj_user = AccountController.GetByID(UID);
                if (obj_user != null)
                {
                    double current = 0;
                    if (Convert.ToDouble(obj_user.Currency) > 0)
                    {
                        current = Convert.ToDouble(obj_user.Currency);
                    }
                    else
                    {
                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                            current = Convert.ToDouble(config.Currency);
                    }
                    if (!string.IsNullOrEmpty(listshop))
                    {
                        string[] list = listshop.Split('|');

                        #region Update tổng tiền
                        if (list.Length - 1 > 0)
                        {
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                int sID = list[i].ToInt(0);
                                var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                if (oshops != null)
                                {
                                    double TotalPriceShop = 0;
                                    double TotalPriceShopCNY = 0;
                                    OrderShop or = new OrderShop();
                                    or.OrderShopID = oshops.ID;
                                    or.OrderShopName = oshops.ShopName;
                                    or.IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                    List<OrderTemp> lot = new List<OrderTemp>();
                                    List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, oshops.ID);
                                    if (ors != null)
                                    {
                                        if (ors.Count > 0)
                                        {
                                            foreach (var item in ors)
                                            {
                                                OrderTemp ot = new OrderTemp();
                                                int ID = item.ID;
                                                string linkproduct = item.link_origin;
                                                string productname = item.title_origin;
                                                string brand = item.brand;
                                                string image = item.image_origin;
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

                                                e_totalproduct = e_pricevn + e_pricetemp;

                                                TotalPriceShop += e_totalproduct;

                                                TotalPriceShopCNY += e_pricebuy;

                                                //pricefullcart += e_totalproduct;

                                                if (image.Contains("%2F"))
                                                {
                                                    image = image.Replace("%2F", "/");
                                                }
                                                if (image.Contains("%3A"))
                                                {
                                                    image = image.Replace("%3A", ":");
                                                }
                                            }
                                            OrderShopTempController.UpdateNoteFastPriceVND(oshops.ID, oshops.Note, TotalPriceShop.ToString());
                                            OrderShopTempController.UpdatePriceVNDCNY(oshops.ID, TotalPriceShop.ToString(), TotalPriceShopCNY.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Update phí kiểm đếm
                        if (list.Length - 1 > 0)
                        {
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                int sID = list[i].ToInt(0);
                                var goshop = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                if (goshop != null)
                                {
                                    if (goshop.IsCheckProduct == true)
                                    {

                                        double total = 0;
                                        var listpro = OrderTempController.GetAllByOrderShopTempID(goshop.ID);
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
                                                if (counpros_more10 >= 1 && counpros_more10 <= 2)
                                                {
                                                    total = total + (5000 * counpros_more10);
                                                }
                                                else if (counpros_more10 > 2 && counpros_more10 <= 10)
                                                {
                                                    total = total + (3500 * counpros_more10);
                                                }
                                                else if (counpros_more10 > 10 && counpros_more10 <= 100)
                                                {
                                                    total = total + (2000 * counpros_more10);
                                                }
                                                else if (counpros_more10 > 100 && counpros_more10 <= 500)
                                                {
                                                    total = total + (1500 * counpros_more10);
                                                }
                                                else if (counpros_more10 > 500)
                                                {
                                                    total = total + (1000 * counpros_more10);
                                                }
                                            }
                                            if (counpros_les10 > 0)
                                            {
                                                if (counpros_les10 >= 1 && counpros_les10 <= 2)
                                                {
                                                    total = total + (1500 * counpros_les10);
                                                }
                                                else if (counpros_les10 > 2 && counpros_les10 <= 10)
                                                {
                                                    total = total + (1000 * counpros_les10);
                                                }
                                                else if (counpros_les10 > 10 && counpros_les10 <= 100)
                                                {
                                                    total = total + (500 * counpros_les10);
                                                }
                                                else if (counpros_les10 > 100 && counpros_les10 <= 500)
                                                {
                                                    total = total + (500 * counpros_les10);
                                                }
                                                else if (counpros_les10 > 500)
                                                {
                                                    total = total + (500 * counpros_les10);
                                                }
                                            }

                                        }

                                        total = Math.Round(total, 0);
                                        OrderShopTempController.UpdateCheckProductPrice(goshop.ID, total.ToString());
                                    }
                                }
                            }
                        }
                        #endregion

                        int salerID = obj_user.SaleID.ToString().ToInt(0);
                        int dathangID = obj_user.DathangID.ToString().ToInt(0);
                        double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                        double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                        if (list.Length - 1 > 0)
                        {
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                int sID = list[i].ToInt(0);
                                var oshops = OrderShopTempController.GetByUIDAndID(obj_user.ID, sID);
                                if (oshops != null)
                                {
                                    int warehouseFromID = 1;
                                    int warehouseID = 1;
                                    int w_shippingType = 1;

                                    string[] w = wareship.Split('|');
                                    if (w.Length - 1 > 0)
                                    {
                                        for (int j = 0; j < w.Length - 1; j++)
                                        {
                                            //id + "-" + warehousefromID + "-" + shippingtype + "-" + warehouseID + "|"
                                            string[] w1 = w[j].Split('-');
                                            int shoptempID = w1[0].ToInt(0);

                                            int wareID = w1[3].ToInt(1);
                                            int shippingtype = w1[2].ToInt(1);
                                            if (oshops.ID == shoptempID)
                                            {
                                                warehouseID = wareID;
                                                w_shippingType = shippingtype;
                                                warehouseFromID = w1[1].ToInt(2);
                                            }
                                        }
                                    }

                                    double total = 0;
                                    double fastprice = 0;
                                    double pricepro = Math.Round(Convert.ToDouble(oshops.PriceVND), 0);
                                    double priceproCYN = Math.Round(Convert.ToDouble(oshops.PriceCNY), 2);

                                    double feecnship = 0;

                                    if (oshops.IsFast == true)
                                    {
                                        fastprice = Math.Round((pricepro * 5 / 100), 0);
                                    }

                                    string ShopID = oshops.ShopID;
                                    string ShopName = oshops.ShopName;
                                    string Site = oshops.Site;
                                    bool IsForward = Convert.ToBoolean(oshops.IsForward);
                                    string IsForwardPrice = oshops.IsForwardPrice;
                                    bool IsFastDelivery = Convert.ToBoolean(oshops.IsFastDelivery);
                                    string IsFastDeliveryPrice = oshops.IsFastDeliveryPrice;
                                    bool IsCheckProduct = Convert.ToBoolean(oshops.IsCheckProduct);
                                    string IsCheckProductPrice = oshops.IsCheckProductPrice;
                                    bool IsPacked = Convert.ToBoolean(oshops.IsPacked);
                                    string IsPackedPrice = oshops.IsPackedPrice;
                                    bool IsFast = Convert.ToBoolean(oshops.IsFast);
                                    string IsFastPrice = fastprice.ToString();
                                    double pricecynallproduct = 0;

                                    double totalFee_CountFee = fastprice + pricepro + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0);
                                    double servicefee = 0;
                                    double servicefeeMoney = 0;

                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var temp in adminfeebuypro)
                                        {
                                            if (pricepro >= temp.AmountFrom && pricepro < temp.AmountTo)
                                            {
                                                servicefee = temp.FeePercent.ToString().ToFloat(0) / 100;
                                                //servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                                break;
                                            }
                                        }
                                    }


                                    double feebpnotdc = 0;
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

                                    double InsuranceMoney = 0;
                                    if (oshops.IsInsurrance == true)
                                        InsuranceMoney = pricepro * (InsurancePercent / 100);

                                    total = fastprice + pricepro + feebp + feecnship + Math.Round(Convert.ToDouble(oshops.IsCheckProductPrice), 0) + InsuranceMoney;

                                    //Lấy ra từng ordertemp trong shop
                                    var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(oshops.ID);
                                    if (proOrdertemp != null)
                                    {
                                        if (proOrdertemp.Count > 0)
                                        {
                                            foreach (var temp in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(temp.quantity);
                                                double originprice = Convert.ToDouble(temp.price_origin);
                                                double promotionprice = Convert.ToDouble(temp.price_promotion);

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
                                            }
                                        }
                                    }
                                    string PriceVND = oshops.PriceVND;
                                    string PriceCNY = pricecynallproduct.ToString();
                                    //string FeeShipCN = (10 * current).ToString();
                                    string FeeShipCN = feecnship.ToString();
                                    string FeeBuyPro = feebp.ToString();
                                    string FeeWeight = oshops.FeeWeight;
                                    string Note = oshops.Note;

                                    string FullName = "";
                                    string Address = "";
                                    string Email = "";
                                    string Phone = "";
                                    var ui = AccountInfoController.GetByUserID(obj_user.ID);
                                    if (ui != null)
                                    {
                                        FullName = ui.FirstName + " " + ui.LastName;
                                        Address = ui.Address;
                                        Email = ui.Email;
                                        Phone = ui.MobilePhonePrefix + ui.MobilePhone;
                                    }
                                    int Status = 0;
                                    string Deposit = "0";
                                    string CurrentCNYVN = current.ToString();
                                    string TotalPriceVND = Math.Round(total, 0).ToString();
                                    string AmountDeposit = Math.Round((pricepro * LessDeposit / 100)).ToString();
                                    DateTime CreatedDate = DateTime.Now;
                                    string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice,
                                        IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN,
                                        TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1, FeeBuyProUser, currentreal.ToString());
                                    int idkq = Convert.ToInt32(kq);
                                    if (idkq > 0)
                                    {
                                        foreach (var temp in proOrdertemp)
                                        {
                                            int quantity = Convert.ToInt32(temp.quantity);
                                            double originprice = Convert.ToDouble(temp.price_origin);
                                            double promotionprice = Convert.ToDouble(temp.price_promotion);
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

                                            string image = temp.image_origin;
                                            if (image.Contains("%2F"))
                                            {
                                                image = image.Replace("%2F", "/");
                                            }
                                            if (image.Contains("%3A"))
                                            {
                                                image = image.Replace("%3A", ":");
                                            }
                                            string ret = OrderController.Insert(UID, temp.title_origin, temp.title_translated, temp.price_origin, temp.price_promotion, temp.property_translated,
                                            temp.property, temp.data_value, image, image, temp.shop_id, temp.shop_name, temp.seller_id, temp.wangwang, temp.quantity,
                                            temp.stock, temp.location_sale, temp.site, temp.comment, temp.item_id, temp.link_origin, temp.outer_id, temp.error, temp.weight, temp.step, temp.stepprice, temp.brand,
                                            temp.category_name, temp.category_id, temp.tool, temp.version, Convert.ToBoolean(temp.is_translate), Convert.ToBoolean(temp.IsForward), "0",
                                            Convert.ToBoolean(temp.IsFastDelivery), "0", Convert.ToBoolean(temp.IsCheckProduct), "0", Convert.ToBoolean(temp.IsPacked), "0", Convert.ToBoolean(temp.IsFast),
                                            fastprice.ToString(), pricepro.ToString(), PriceCNY, temp.Note, FullName, Address, Email,
                                            Phone, 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID);

                                            if (temp.price_promotion.ToFloat(0) > 0)
                                                OrderController.UpdatePricePriceReal(ret.ToInt(0), temp.price_origin, temp.price_promotion);
                                            else
                                                OrderController.UpdatePricePriceReal(ret.ToInt(0), temp.price_origin, temp.price_origin);
                                        }
                                        MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);
                                        MainOrderController.UpdateFromPlace(idkq, UID, warehouseFromID, w_shippingType);
                                        MainOrderController.UpdateIsInsurrance(idkq, Convert.ToBoolean(oshops.IsInsurrance));
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
                                                        NotificationsController.Inser(admin.ID, admin.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
                         1, CreatedDate, obj_user.Username, false);
                                                    }
                                                }

                                                var managers = AccountController.GetAllByRoleID(2);
                                                if (managers.Count > 0)
                                                {
                                                    foreach (var manager in managers)
                                                    {
                                                        NotificationsController.Inser(manager.ID, manager.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
                         1, CreatedDate, obj_user.Username, false);
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
                                                                "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");
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
                                                                "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");
                                                        }
                                                        catch { }
                                                    }
                                                }

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
                                                StaffIncomeController.Insert(idkq, "0", salepercentaf3m.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, CreatedDate, obj_user.Username);
                                            }
                                            else
                                            {
                                                double per = feebp * salepercent / 100;
                                                StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, CreatedDate, obj_user.Username);
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
                                                CreatedDate, CreatedDate, obj_user.Username);
                                            if (setNoti != null)
                                            {
                                                if (setNoti.IsSentNotiAdmin == true)
                                                {
                                                    NotificationsController.Inser(dathang.ID, dathang.Username, idkq, "Có đơn hàng mới ID là: " + idkq,
                           1, CreatedDate, obj_user.Username, false);
                                                }

                                                if (setNoti.IsSentEmailAdmin == true)
                                                {
                                                    try
                                                    {
                                                        PJUtils.SendMailGmail_new(dathang.Email,
                                                            "Thông báo tại NHẬP HÀNG 360.", "Có đơn hàng mới ID là: " + idkq, "");
                                                    }
                                                    catch { }
                                                }
                                            }

                                        }
                                    }
                                    //Xóa Shop temp và order temp
                                    OrderShopTempController.Delete(oshops.ID);
                                }
                            }
                        }

                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                        rs.Status = APIUtils.ResponseMessage.Success.ToString();
                        rs.Message = "Đặt hàng thành công.";
                    }
                    else
                    {
                        rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                        rs.Status = APIUtils.ResponseMessage.Error.ToString();
                        rs.Message = "Không có shop nào được chọn.";
                    }

                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateQuantity(int UID, int Quantity, string OrderTemp, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                OrderTempController.UpdateQuantity(Convert.ToInt32(OrderTemp), Quantity);

                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.Message = "";
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateNote(int UID, string Note, string OrderTemp, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                OrderTempController.UpdateBrand(Convert.ToInt32(OrderTemp), Note);
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.Message = "";
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateShopNote(int UID, string Note, string OrderShopID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                OrderShopTempController.UpdateNoteShop(Convert.ToInt32(OrderShopID), Note);
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.Message = "";
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void deleteOrderTemp(int UID, string OrderTempID, string OrderShopID, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                var ordertemp = OrderTempController.GetByID(Convert.ToInt32(OrderTempID));
                if (ordertemp != null)
                {
                    string pricestep = ordertemp.stepprice;
                    int UsID = Convert.ToInt32(ordertemp.UID);
                    string itemid = ordertemp.item_id;
                    OrderTempController.Delete(Convert.ToInt32(OrderTempID));

                    if (!string.IsNullOrEmpty(pricestep))
                    {
                        OrderTempController.UpdatePriceInsert(UID, pricestep, itemid);
                    }

                    int IDS = Convert.ToInt32(OrderShopID);
                    var ordert = OrderTempController.GetAllByOrderShopTempID(IDS);
                    if (ordert.Count == 0)
                    {
                        OrderShopTempController.Delete(IDS);
                    }
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateFeePacked(int UID, int OrderShopID, bool chk, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                string ID = OrderShopTempController.SelectUIDByIDOrder(OrderShopID);
                if (ID != null)
                {
                    OrderShopTempController.UpdateCheckFieldPacked(OrderShopID, chk);
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateFastDelivery(int UID, int OrderShopID, bool chk, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                string ID = OrderShopTempController.SelectUIDByIDOrder(OrderShopID);
                if (ID != null)
                {
                    OrderShopTempController.UpdateCheckFastDelivery(OrderShopID, chk);
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateFeeCheck(int UID, int OrderShopID, bool chk, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                string ID = OrderShopTempController.SelectUIDByIDOrder(OrderShopID);
                if (ID != null)
                {
                    if (ID == UID.ToString())
                    {
                        if (chk == true)
                        {
                            //Lấy ra danh sách sản phẩm để cộng tiền rồi update lại phí kiểm tra hàng hóa
                            var os = OrderShopTempController.GetByUIDAndID(UID, OrderShopID);
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
                                        if (counpros_more10 >= 1 && counpros_more10 <= 2)
                                        {
                                            total = total + (5000 * counpros_more10);
                                        }
                                        else if (counpros_more10 > 2 && counpros_more10 <= 10)
                                        {
                                            total = total + (3500 * counpros_more10);
                                        }
                                        else if (counpros_more10 > 10 && counpros_more10 <= 100)
                                        {
                                            total = total + (2000 * counpros_more10);
                                        }
                                        else if (counpros_more10 > 100 && counpros_more10 <= 500)
                                        {
                                            total = total + (1500 * counpros_more10);
                                        }
                                        else if (counpros_more10 > 500)
                                        {
                                            total = total + (1000 * counpros_more10);
                                        }
                                    }
                                    if (counpros_les10 > 0)
                                    {
                                        if (counpros_les10 >= 1 && counpros_les10 <= 2)
                                        {
                                            total = total + (1500 * counpros_les10);
                                        }
                                        else if (counpros_les10 > 2 && counpros_les10 <= 10)
                                        {
                                            total = total + (1000 * counpros_les10);
                                        }
                                        else if (counpros_les10 > 10 && counpros_les10 <= 100)
                                        {
                                            total = total + (500 * counpros_les10);
                                        }
                                        else if (counpros_les10 > 100 && counpros_les10 <= 500)
                                        {
                                            total = total + (500 * counpros_les10);
                                        }
                                        else if (counpros_les10 > 500)
                                        {
                                            total = total + (500 * counpros_les10);
                                        }
                                    }

                                }
                                total = Math.Round(total, 0);

                                //new
                                //double counpros_more10 = 0;
                                //double counpros_les10 = 0;
                                //if (listpro.Count > 0)
                                //{
                                //    foreach (var item in listpro)
                                //    {
                                //        //counpros += item.quantity.ToInt(1);
                                //        double countProduct = item.quantity.ToInt(1);
                                //        if (Convert.ToDouble(item.price_origin) > 10)
                                //        {
                                //            counpros_more10 += item.quantity.ToInt(1);

                                //        }
                                //        else
                                //        {
                                //            counpros_les10 += item.quantity.ToInt(1);

                                //        }
                                //    }
                                //    if (counpros_more10 > 0)
                                //    {
                                //        if (counpros_more10 >= 1 && counpros_more10 <= 5)
                                //        {
                                //            total = total + (5000 * counpros_more10);
                                //        }
                                //        else if (counpros_more10 > 5 && counpros_more10 <= 20)
                                //        {
                                //            total = total + (3500 * counpros_more10);
                                //        }
                                //        else if (counpros_more10 > 20 && counpros_more10 <= 100)
                                //        {
                                //            total = total + (2000 * counpros_more10);
                                //        }
                                //        else if (counpros_more10 > 100)
                                //        {
                                //            total = total + (1500 * counpros_more10);
                                //        }
                                //    }
                                //    if (counpros_les10 > 0)
                                //    {
                                //        if (counpros_les10 >= 1 && counpros_les10 <= 5)
                                //        {
                                //            total = total + (1000 * counpros_les10);
                                //        }
                                //        else if (counpros_les10 > 5 && counpros_les10 <= 20)
                                //        {
                                //            total = total + (800 * counpros_les10);
                                //        }
                                //        else if (counpros_les10 > 20 && counpros_les10 <= 100)
                                //        {
                                //            total = total + (700 * counpros_les10);
                                //        }
                                //        else if (counpros_les10 > 100)
                                //        {
                                //            total = total + (500 * counpros_les10);
                                //        }
                                //    }
                                //}
                                //end new

                                OrderShopTempController.UpdateCheckProductPrice(OrderShopID, total.ToString());
                            }
                        }
                        else
                        {
                            //Update lại phí kiểm tra hàng hóa là 0
                            OrderShopTempController.UpdateCheckProductPrice(OrderShopID, "0");
                        }

                    }
                    OrderShopTempController.UpdateCheckField(OrderShopID, chk);
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateInsurrance(int UID, int OrderShopID, bool chk, string Key)
        {
            var rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                string ID = OrderShopTempController.SelectUIDByIDOrder(OrderShopID);
                if (ID != null)
                {
                    OrderShopTempController.UpdateCheckInsurrance(OrderShopID, chk);
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetListCheck(int UID, string Key)
        {
            ResponseClass rs = new ResponseClass();
            var tk = DeviceTokenController.GetByToken(UID, Key);
            if (tk != null)
            {
                ListCheck lc = new ListCheck();
                lc.IsShowFeeCheck = true;
                lc.IsShowFeePacked = true;
                lc.IsShowFeeFastDelivery = true;
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.ListCheck = lc;
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
                rs.Logout = "1";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void InsertAutoBanking(string Key, string UID, string HpayID, string Amount, string Note)
        {
            ResponseClass rs = new ResponseClass();

            if (Key == "monasms-autobanking")
            {
                var ac = AccountController.GetByID(Convert.ToInt32(UID));
                if (ac != null)
                {
                    AccountController.UserWallet_Auto(ac.ID, Convert.ToDouble(Amount), Convert.ToInt32(HpayID), Note);
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                    rs.Status = APIUtils.ResponseMessage.Success.ToString();
                    rs.Message = "";

                }
                else
                {
                    rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                    rs.Status = APIUtils.ResponseMessage.Error.ToString();
                    rs.Message = "";
                }
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.NotFound);
                rs.Status = APIUtils.ResponseMessage.Error.ToString();
                rs.Message = "";
            }
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        public void CheckList(string ListMainOrder)
        {
            var rs = new ResponseClass();
            if (!string.IsNullOrEmpty(ListMainOrder))
            {
                List<ListMainOrderCode> cs = new List<ListMainOrderCode>();

                JavaScriptSerializer ser = new JavaScriptSerializer();

                var list = ser.Deserialize<List<ListMain>>(ListMainOrder);
                foreach (var item in list)
                {
                    ListMainOrderCode mc = new ListMainOrderCode();
                    var check = MainOrderCodeController.GetAllByMainOrderCode(item.MainOrderCode);
                    if (check != null)
                    {
                        var checkmain = MainOrderController.GetAllByID(check.MainOrderID.Value);
                        if (checkmain != null)
                        {
                            if (checkmain.IsDoneSmallPackage == true)
                            {
                                mc.MainOrderCode = item.MainOrderCode;
                                mc.Status = 2;
                                mc.StatusString = "Đã đủ";
                                mc.StatusColor = "blue";
                                cs.Add(mc);
                            }
                            else
                            {
                                var checksmall = SmallPackageController.GetByMainOrderID(checkmain.ID);
                                if (checksmall.Count > 0)
                                {
                                    mc.MainOrderCode = item.MainOrderCode;
                                    mc.Status = 4;
                                    mc.StatusString = "Đã có thông tin";
                                    mc.StatusColor = "yellow";
                                    cs.Add(mc);
                                }
                                else
                                {
                                    mc.MainOrderCode = item.MainOrderCode;
                                    mc.Status = 3;
                                    mc.StatusString = "Chưa có thông tin";
                                    cs.Add(mc);
                                }
                            }
                        }
                    }
                    else
                    {
                        mc.MainOrderCode = item.MainOrderCode;
                        mc.Status = 1;
                        mc.StatusColor = "red";
                        mc.StatusString = "Mã đơn chưa khai báo";
                        cs.Add(mc);
                    }
                }

                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.ListMain = cs;
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        public void CheckListVCH(string ListMainOrder)
        {
            var rs = new ResponseClass();
            if (!string.IsNullOrEmpty(ListMainOrder))
            {
                List<ListMainOrderCode> cs = new List<ListMainOrderCode>();

                JavaScriptSerializer ser = new JavaScriptSerializer();

                var list = ser.Deserialize<List<ListMain>>(ListMainOrder);
                foreach (var item in list)
                {
                    ListMainOrderCode mc = new ListMainOrderCode();
                    var check = TransportationOrderNewController.GetByMainOrderCode(item.MainOrderCode);
                    if (check.Count > 0)
                    {
                        mc.MainOrderCode = item.MainOrderCode;
                        mc.Status = 4;
                        mc.StatusString = "Đã có thông tin";
                        mc.StatusColor = "blue";
                        cs.Add(mc);
                    }
                    else
                    {
                        mc.MainOrderCode = item.MainOrderCode;
                        mc.Status = 3;
                        mc.StatusColor = "";
                        mc.StatusString = "Chưa có thông tin";
                        cs.Add(mc);
                    }
                }

                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
                rs.Status = APIUtils.ResponseMessage.Success.ToString();
                rs.ListMain = cs;
            }
            else
            {
                rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.FAILED);
                rs.Status = APIUtils.ResponseMessage.Fail.ToString();
                rs.Message = "";
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        public class ListMain
        {
            public string MainOrderCode { get; set; }
        }

        [WebMethod]
        public void GetLinkCK(string URL)
        {
            var rs = new ResponseClass();

            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
            rs.Status = APIUtils.ResponseMessage.Success.ToString();
            rs.Message = URL + "&idck=nhaphang";

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        [WebMethod]
        public void CheckLink(string URL)
        {
            var rs = new ResponseClass();

            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
            rs.Status = APIUtils.ResponseMessage.Success.ToString();
            rs.Message = "10";

            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }
        public class AppConfigs
        {
            public bool IsOrderActive { get; set; }
        }

        [WebMethod]
        public void GetAppConfigs()
        {
            var rs = new ResponseClass();

            rs.Code = APIUtils.GetResponseCode(APIUtils.ResponseCode.SUCCESS);
            rs.Status = APIUtils.ResponseMessage.Success.ToString();
            AppConfigs app = new AppConfigs();
            bool IsOrderActive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsOrderActive"]);
            app.IsOrderActive = IsOrderActive;
            rs.data = app;
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(rs, Formatting.Indented));
            Context.Response.Flush();
            Context.Response.End();
        }

        public class ResponseClass
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Code { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Status { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Message { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Key { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public object data { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Logout { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Username { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Amount { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Account Account { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

            public string Currency { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string TotalPrice { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

            public List<Menu> ListMenu { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<Noti> ListNoti { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public WareHouse WateHouse { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<OrderShop> ListOrderShop { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Info Info { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]

            public ListCheck ListCheck { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ListMainOrderCode> ListMain { get; set; }
        }

        #region Class
        public class Account
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime BirthDay { get; set; }
            public int Gender { get; set; }
            public string Wallet { get; set; }
            public string WalletCYN { get; set; }
            public int Role { get; set; }
            public string Level { get; set; }
            public string IMGUser { get; set; }
            public decimal title { get; set; }

            public List<Menu> Menu { get; set; }
        }

        public class Menu
        {
            public string ItemName { get; set; }
            public int GroupID { get; set; }
            public int Parent { get; set; }
            public string Link { get; set; }
            public int ShowType { get; set; }
            public string Icon { get; set; }
        }
        public class Noti
        {
            public int NotificationID { get; set; }
            public string Message { get; set; }
            public string Link { get; set; }
            public int Status { get; set; }
            public int Type { get; set; }
        }
        public class WareHouse
        {
            public List<WareHouseFrom> WareHouseFrom { get; set; }
            public List<WareHouseTo> WareHouseTo { get; set; }
            public List<ShippingType> ShippingType { get; set; }
        }

        public class WareHouseFrom
        {
            public int ID { get; set; }
            public string WareHouseName { get; set; }
        }

        public class WareHouseTo
        {
            public int ID { get; set; }
            public string WareHouseName { get; set; }
        }

        public class ShippingType
        {
            public int ID { get; set; }
            public string ShippingName { get; set; }
        }

        //public class Cart
        //{
        //    public List<OrderShop> OrderShop { get; set; }
        //    public ListCheck ListCheck { get; set; }
        //}

        public class ListCheck
        {
            public bool IsShowFeeCheck { get; set; }
            public bool IsShowFeePacked { get; set; }
            public bool IsShowFeeFastDelivery { get; set; }
        }


        public class OrderShop
        {
            public int OrderShopID { get; set; }
            public string OrderShopName { get; set; }
            public bool IsCheckProduct { get; set; }
            public bool IsCheckPacked { get; set; }
            public bool IsCheckFastDelivery { get; set; }
            public string Note { get; set; }
            public List<OrderTemp> ListProduct { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string TotalPriceVND { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string FeeBuyPro { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string FeeCheck { get; set; }
            public int WarehouseFrom { get; set; } //KhoTQ
            public int WarehouseTo { get; set; } //KhoVN
            public int ShippingType { get; set; } //PTVC
        }


        public class OrderTemp
        {
            public int OrderTempID { get; set; }
            public string ProductName { get; set; }
            public string Image { get; set; }
            public string LinkProduct { get; set; }
            public string Property { get; set; }
            public string Brand { get; set; }
            public string PriceVN { get; set; }
            public string PriceCNY { get; set; }
            public string TotalPriceVN { get; set; }
            public string TotalPriceCNY { get; set; }
            public int Quantity { get; set; }
        }

        public class Info
        {
            public double Currency { get; set; }
            public string Wallet { get; set; }
            public string WalletCYN { get; set; }
            public string Level { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime BirthDay { get; set; }
            public int Gender { get; set; }
            public int Role { get; set; }
            public decimal title { get; set; }
        }
        #endregion

        public class Customer
        {
            public int UID { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public List<OrderShop> ListShop { get; set; }
        }
        public class ListMainOrderCode
        {
            public string MainOrderCode { get; set; }
            public int Status { get; set; }
            public string StatusString { get; set; }
            public string StatusColor { get; set; }
        }

    }
}

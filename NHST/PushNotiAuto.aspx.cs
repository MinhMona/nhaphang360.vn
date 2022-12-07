using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class PushNotiAuto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PushNotiJob();
                PushNotiAPP();
                PushAutoMainOrder();
            }
        }

        public class SmsResponse
        {
            public string MessageCount { get; set; }
            public List<Message> Messages { get; set; }
        }

        public class Message
        {
            public string To { get; set; }
            public string MessageId { get; set; }
            public string Status { get; set; }
            public string RemainingBalance { get; set; }
            public string MessagePrice { get; set; }
            public string Network { get; set; }
            public string From { get; set; }
        }

        protected void PushNotiAPP()
        {
            var temp = AppPushNotiController.GetNoti();
            if (temp != null)
            {
                string link = "";
                AppPushNotiController.UpdateSent(temp.ID, "SystemAPI");
                var l = DeviceTokenController.GetAllDevice();
                if (l != null)
                {
                    foreach (var item in l)
                    {
                        Data dt = new Data();
                        dt.AppNotiTitle = temp.AppNotiTitle;
                        dt.AppNotiMessage = temp.AppNotiMessage;
                        dt.Device = item.Device;
                        dt.Type = Convert.ToInt32(item.Type);
                        dt.link = link;
                        Thread t = new Thread(Push);
                        t.Start(dt);
                    }
                }


                var acc = AccountController.GetAllByRoleID(1);
                if (acc.Count > 0)
                {
                    foreach (var item in acc)
                    {
                        ObjectSendMail dt = new ObjectSendMail();
                        dt.Email = item.Email;
                        dt.Title = temp.AppNotiTitle;
                        dt.Noti = temp.AppNotiMessage;
                        Thread t = new Thread(PushMail);
                        t.Start(dt);
                    }
                }

            }
        }

        public static void PushSendMail(string Email, string title, string Noti)
        {
            try
            {
                PJUtils.SendMailGmail_new(Email, "Thông báo tại NHẬP HÀNG 360 - " + title + "", Noti, "");
            }
            catch (Exception ex)
            {

            }
        }

        public void PushMail(object ob)
        {
            ObjectSendMail dt = (ObjectSendMail)ob;
            PushSendMail(dt.Email, dt.Title, dt.Noti);
        }

        public class ObjectSendMail
        {
            public string Email { get; set; }
            public string Title { get; set; }
            public string Noti { get; set; }
        }

        public static void PushOneSignalUser(string device, string title, string Noti, string link)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                 | SecurityProtocolType.Tls11
                 | SecurityProtocolType.Tls12
                 | SecurityProtocolType.Ssl3;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "2e48617e-d10d-4108-aa0d-00402d113f66",
                headings = new { en = title },
                contents = new { en = Noti },
                url = link,
                include_player_ids = new List<string>() { device }
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }

        public void Push(object ob)
        {
            Data dt = (Data)ob;
            PushOneSignalUser(dt.Device, dt.AppNotiTitle, dt.AppNotiMessage, dt.link);
        }

        public class Data
        {
            public string AppNotiTitle { get; set; }
            public string Device { get; set; }
            public int Type { get; set; }
            public string AppNotiMessage { get; set; }
            public string link { get; set; }
        }


        protected void PushNotiJob()
        {
            var lpush = NotificationsController.GetAllPush();
            if (lpush != null)
            {
                if (lpush.Count > 0)
                {
                    foreach (var item in lpush)
                    {
                        string link = "";// thông báo
                        string title = "";

                        if (item != null)
                        {
                            if (item.PushNotiApp == true)
                            {
                                NotificationsController.UpdateSent(item.ID, "SystemAPI");
                                var list = DeviceTokenController.GetAllByUIDandIsHide(item.ReceivedID.Value);
                                if (list != null)
                                {
                                    DateTime cre = Convert.ToDateTime(item.CreatedDate);
                                    //string mess = cre.Hour + ":" + cre.Minute + " " + cre.Day + "/" + cre.Month + " " + item.Message;
                                    string mess = cre.ToString("HH:mm dd/MM") + " " + item.Message;
                                    foreach (var temp in list)
                                    {
                                        string Key = temp.UserToken;
                                        if (item.NotifType == 1)
                                        {
                                            link = "https://nhaphang360.vn/chi-tiet-don-hang-app.aspx?UID=" + item.ReceivedID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";  //đơn hàng mua hộ
                                            title = "ĐƠN HÀNG MUA HỘ";
                                        }
                                        if (item.NotifType == 2)
                                        {
                                            link = "https://nhaphang360.vn/lich-su-giao-dich-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + "";  //Nạp tiền
                                            title = "NẠP TIỀN";
                                        }
                                        if (item.NotifType == 3)
                                        {
                                            link = "https://nhaphang360.vn/lich-su-giao-dich-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + ""; //Rút tiền
                                            title = "RÚT TIỀN";
                                        }
                                        if (item.NotifType == 5)
                                        {
                                            link = "https://nhaphang360.vn/khieu-nai-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + ""; //Khiếu nại
                                            title = "KHIẾU NẠI";
                                        }
                                        if (item.NotifType == 8)
                                        {
                                            link = "https://nhaphang360.vn/thanh-toan-ho-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + ""; //Thanh toán hộ
                                            title = "THANH TOÁN HỘ";
                                        }
                                        if (item.NotifType == 9)
                                        {
                                            link = "https://nhaphang360.vn/lich-su-giao-dich-tien-te-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + ""; //Hoàn tệ
                                            title = "HOÀN TỆ";
                                        }
                                        if (item.NotifType == 10)
                                        {
                                            link = "https://nhaphang360.vn/danh-sach-kien-ky-gui-app.aspx?UID=" + item.ReceivedID + "&Key=" + Key + ""; //Vận chuyển hộ
                                            title = "VẬN CHUYỂN HỘ";
                                        }
                                        if (item.NotifType == 11)
                                        {
                                            link = "https://nhaphang360.vn/chi-tiet-don-hang-khac-app.aspx?UID=" + item.ReceivedID + "&OrderID=" + item.OrderID + "&Key=" + Key + ""; //đơn hàng TMĐT
                                            title = "ĐƠN HÀNG TMĐT";
                                        }
                                        if (item.NotifType == 12)
                                        {
                                            link = "https://nhaphang360.vn/chi-tiet-don-hang-app.aspx?UID=" + item.ReceivedID + "&OrderID=" + item.OrderID + "&Key=" + Key + "";  //tin nhắn đơn hàng mua hộ
                                            title = "TIN NHẮN ĐƠN HÀNG MUA HỘ";
                                        }
                                        if (item.NotifType == 13)
                                        {
                                            link = "https://nhaphang360.vn/chi-tiet-don-hang-khac-app.aspx?UID=" + item.ReceivedID + "&OrderID=" + item.OrderID + "&Key=" + Key + ""; //tin nhắn đơn hàng TMĐT
                                            title = "TIN NHẮN ĐƠN HÀNG TMĐT";
                                        }

                                        Data dt = new Data();
                                        dt.AppNotiTitle = title;
                                        dt.AppNotiMessage = mess;
                                        dt.Device = temp.Device;
                                        dt.Type = Convert.ToInt32(temp.Type);
                                        dt.link = link;

                                        Thread t = new Thread(Push);

                                        t.Start(dt);

                                    }
                                }
                            }

                        }
                    }
                }
            }
        }


        protected void PushAutoMainOrder()
        {
            if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 10)
            {
                Thread UpdateCurrency = new Thread(PushAutoUpdateCurrency);
                UpdateCurrency.Start();
            }
        }

        protected void PushAutoUpdateCurrency()
        {
            double current = 0;
            double insurrance = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                current = Convert.ToDouble(config.Currency);
                insurrance = Convert.ToDouble(config.InsurancePercent);
            }

            double pricepro = 0;
            double priceproCYN = 0;

            var mo = MainOrderController.GetAllStatus(0);
            if (mo.Count > 0)
            {
                foreach (var item in mo)
                {
                    var obj_user = AccountController.GetByID(Convert.ToInt32(item.UID));
                    double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                    double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                    var product = OrderController.GetByMainOrderID(Convert.ToInt32(item.ID));
                    if (product.Count > 0)
                    {
                        double priceVND = 0;
                        double priceCYN = 0;

                        foreach (var item2 in product)
                        {
                            double u_pricecbuy = 0;
                            double u_pricevn = 0;
                            double e_pricebuy = 0;
                            double e_pricevn = 0;

                            int quantity = Convert.ToInt32(item2.quantity);
                            double originprice = Convert.ToDouble(item2.price_origin);
                            double promotionprice = Convert.ToDouble(item2.price_promotion);

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

                            OrderController.UpdateAutoCurrency(Convert.ToInt32(item2.ID), e_pricebuy.ToString(), e_pricevn.ToString(), current.ToString());
                        }

                        pricepro = Math.Round(Convert.ToDouble(priceVND), 0);
                        priceproCYN = Math.Round(Convert.ToDouble(priceCYN), 2);

                        double servicefee = 0;
                        double feebpnotdc = 0;
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item3 in adminfeebuypro)
                            {
                                if (priceVND >= item3.AmountFrom && priceVND < item3.AmountTo)
                                {
                                    double feepercent = 0;
                                    if (item3.FeePercent.ToString().ToFloat(0) > 0)
                                        feepercent = Convert.ToDouble(item3.FeePercent);
                                    servicefee = feepercent / 100;
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
                            {
                                feebpnotdc = pricepro * servicefee;
                            }
                        }
                        else
                        {
                            feebpnotdc = pricepro * servicefee;
                        }

                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double feebp = Math.Round(feebpnotdc - subfeebp, 0);

                        if (feebp < 10000)
                            feebp = 10000;

                        double feecheck = 0;
                        feecheck = Convert.ToDouble(item.IsCheckProductPrice);

                        double feeinsurrance = 0;
                        if (item.IsInsurrance == true)
                        {
                            feeinsurrance = pricepro * insurrance / 100;
                        }

                        double TongTienHang = pricepro + feebp;
                        double TotalPriceVND = TongTienHang + feecheck + feeinsurrance;
                        double AmountDeposit = TongTienHang * LessDeposit / 100;

                        MainOrderController.UpdateAutoCurrency(Convert.ToInt32(item.ID), current.ToString(), feebp.ToString(), AmountDeposit.ToString(), TotalPriceVND.ToString(),
                                                               priceproCYN.ToString(), pricepro.ToString(), insurrance.ToString(), feeinsurrance.ToString(), FeeBuyProUser);

                    }
                }
            }

        }


        protected void PushIOS(string DeviceToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;

            var request = WebRequest.Create("https://exp.host/--/api/v2/push/send") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                to = "ExponentPushToken[Srs0m1Hcfv0ZV-6g_aenl1]",
                sound = "default",
                title = "Thông báo nè",
                url = "Con chó con mèo, mèo con mèo con"
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            //var client = new RestClient("https://exp.host/--/api/v2/push/send");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AlwaysMultipartFormData = true;
            //request.AddParameter("to", "ExponentPushToken[Srs0m1Hcfv0ZV-6g_aenl1]");
            //request.AddParameter("sound", "default");
            //request.AddParameter("title", "Thông báo nè");
            //request.AddParameter("body", "Con chó con mèo, mèo con mèo con");
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }
    }
}
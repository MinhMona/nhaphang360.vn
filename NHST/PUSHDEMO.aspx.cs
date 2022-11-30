using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class PUSHDEMO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            //WebClient client = new WebClient();
            //client.Encoding = ASCIIEncoding.UTF8;
            //string downloadString = client.DownloadString(txturl.Text);
            //string s = downloadString;
            ////ltrcontent.Text = downloadString;

            //loadProduct1(txturl.Text);
        }
       
        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }

        protected void Push(string DeviceToken)
        {
            string android_firebase_key = "AAAAlH56jiw:APA91bHCufI-LmxW-8FMTrU21Nfcw-pfQL0hP_Rg6ZZLK_-Z7H7SrmkYjI4KvwOuHpQkOv8TbwkLdq5IQ16iOtkUH3hyAF8_RYgU5YbLd0HwLoaHoS652DqQMUl7pF1OeAsuJkqcsu7b";
            string SenderIdAndroid = "637777120812";

            string ios_firebase_key = "AAAAlH56jiw:APA91bHCufI-LmxW-8FMTrU21Nfcw-pfQL0hP_Rg6ZZLK_-Z7H7SrmkYjI4KvwOuHpQkOv8TbwkLdq5IQ16iOtkUH3hyAF8_RYgU5YbLd0HwLoaHoS652DqQMUl7pF1OeAsuJkqcsu7b";
            string SenderIdiOS = "637777120812";

            string cre = DateTime.Now.ToString("HH:mm dd/MM");

            var SENDER_ID = "";
            var FirebaseKey = "";

            if (ddlType.SelectedValue == "android")
            {
                SENDER_ID = SenderIdAndroid;
                FirebaseKey = android_firebase_key;
            }
            else
            {
                SENDER_ID = SenderIdiOS;
                FirebaseKey = ios_firebase_key;
            }

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var objNotificationiOS = new
            {
                to = DeviceToken,
                collapse_key = "type_a",
                notification = new
                {
                    body = "ABC",
                    title = "THANH TOÁN HỘ",
                    sound = "default",
                    vibration = "default"
                },
                data = new
                {
                    title = "123",
                    message = "456",
                    image = "http://demo.vominhthien.com/App_Themes/vominhthien/images/main-logo.png",
                    link = "http://demo.vominhthien.com/thanh-toan-ho-app.aspx?UID=18680"
                }
            };
            var objNotificationAndroid = new
            {
                to = DeviceToken,
                data = new
                {
                    title = "THANH TOÁN HỘ",
                    message = "ABC",
                    image = "http://demo.vominhthien.com/App_Themes/vominhthien/images/main-logo.png",
                    link = "http://demo.vominhthien.com/thanh-toan-ho-app.aspx?UID=18680"
                }
            };


            string jsonNotificationFormat = "";
            if (ddlType.SelectedValue == "android")
                jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotificationAndroid);
            else
                jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotificationiOS);

            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            tRequest.ContentLength = byteArray.Length;
            tRequest.ContentType = "application/json";
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String responseFromFirebaseServer = tReader.ReadToEnd();

                            FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                            if (response.success == 1)
                            {
                                //PJUtils.ShowMessageBoxSwAlert("Thành công", "s", true, Page);
                            }
                            else if (response.failure == 1)
                            {
                                //PJUtils.ShowMessageBoxSwAlert("Thất bại", "e", true, Page);
                            }
                        }
                    }

                }
            }
        }

        protected void btnpush_Click1(object sender, EventArgs e)
        {
            PushOneSignalUser(txturl.Text, "Haha", "Haha", "Haha");
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
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
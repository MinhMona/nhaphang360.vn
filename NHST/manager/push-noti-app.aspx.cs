using MB.Extensions;
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
using NHST.Bussiness;
using NHST.Models;

namespace NHST.manager
{
    public partial class push_noti_app : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID == 0 || ac.RoleID == 2)
                    {

                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();

            DateTime currentDate = DateTime.Now;
            string backlink = "/manager/Noti-app-list.aspx";
            var kq = AppPushNotiController.Insert(txtTitle.Text, txtMessage.Text, currentDate, username);
            if (kq != null)
            {
                //string link = "";
                //AppPushNotiController.UpdateSent(kq.ID, "SystemAPI");
                //var l = DeviceTokenController.GetAllDevice();
                //if (l != null)
                //{
                //    foreach (var item in l)
                //    {
                //        Data dt = new Data();
                //        dt.AppNotiTitle = kq.AppNotiTitle;
                //        dt.AppNotiMessage = kq.AppNotiMessage;
                //        dt.Device = item.Device;
                //        dt.Type = Convert.ToInt32(item.Type);
                //        dt.link = link;
                //        Thread t = new Thread(Push);
                //        t.Start(dt);
                //    }
                //}

                PJUtils.ShowMessageBoxSwAlertBackToLink("Thông báo thành công.", "s", true, backlink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình tạo thông báo. Vui lòng thử lại.", "e", true, Page);
            }
        }

        public void Push(object ob)
        {
            Data dt = (Data)ob;
            PushOneSignalUser(dt.Device, dt.AppNotiTitle, dt.AppNotiMessage);
        }

        public static void PushOneSignalUser(string device, string title, string Noti)
        {
            try
            {
                var acc = AccountController.GetAllByRoleID(1);
                if (acc.Count > 0)
                {
                    foreach (var item in acc)
                    {
                        PJUtils.SendMailGmail_new(item.Email, "Thông báo tại NHẬP HÀNG 360 - " + title + "", Noti, "");
                    }
                }              

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
            catch
            {

            }
        }
        public class Data
        {
            public string AppNotiTitle { get; set; }
            public string Device { get; set; }
            public int Type { get; set; }
            public string AppNotiMessage { get; set; }
            public string link { get; set; }           
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Website_under_contructing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;
            DateTime later = current.AddDays(90);
            int d = later.Subtract(current).Days;
            //Response.Write(d);
        }
        public void LoadData(string waybillstring)
        {
            string link = "http://gw.kerryexpress.com.vn/api/WS004GetOrderTracking?data={\"token_key\":\"OPxwpQe9IogBmqpCteo7Sw == \",\"waybill_number\":\"" + waybillstring + "\"}";
            var request = (HttpWebRequest)WebRequest.Create(link);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
            request.Method = "GET";


            var content = String.Empty;
            HttpStatusCode statusCode;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var contentType = response.ContentType;
                Encoding encoding = null;
                if (contentType != null)
                {
                    var match = Regex.Match(contentType, @"(?<=charset\=).*");
                    if (match.Success)
                        encoding = Encoding.GetEncoding(match.ToString());
                }

                encoding = encoding ?? Encoding.UTF8;

                statusCode = ((HttpWebResponse)response).StatusCode;
                using (var reader = new StreamReader(stream, encoding))
                    content = reader.ReadToEnd();
            }

            var obj = JsonConvert.DeserializeObject<RootObject>(content);
            Response.Write(obj.POD.Count +"<br/>" + obj.CustomerInfo.Count + "<br/>" + obj.GoodsInfo.Count + "<br/>" + obj.Tracking.Count + "<br/>" );
        }

        protected void btnTrack_Click(object sender, EventArgs e)
        {
            string waybill = txtTracking.Text.Trim();
            if (!string.IsNullOrEmpty(waybill))
            {
                LoadData(waybill);
            }
        }

        public class POD
        {
            public string consigneee { get; set; }
            public string weight { get; set; }
            public string status_date { get; set; }
        }

        public class CustomerInfo
        {
            public string tutinh { get; set; }
            public string tentutinh { get; set; }
            public string khachgoi { get; set; }
            public string tenkhachgoi { get; set; }
            public string nguoigoi { get; set; }
            public string diachinguoigoi { get; set; }
            public string tuhuyen { get; set; }
            public string tentuhuyen { get; set; }
            public string dentinh { get; set; }
            public string tendentinh { get; set; }
            public string khachtra { get; set; }
            public string tenkhachtra { get; set; }
            public string nguoinhan { get; set; }
            public string diachinguoinhan { get; set; }
            public string denhuyen { get; set; }
            public string tendenhuyen { get; set; }
            public string ghichunhanhang { get; set; }
            public string ghichutrahang { get; set; }
        }

        public class GoodsInfo
        {
            public string kkhq { get; set; }
            public string hanghoa { get; set; }
            public string sokien { get; set; }
            public string trangay { get; set; }
            public string madv { get; set; }
            public string tendv { get; set; }
            public string thetich { get; set; }
            public string apgiatheokien { get; set; }
            public string baophat { get; set; }
        }

        public class Tracking
        {
            public string status_date { get; set; }
            public string is_scan_in { get; set; }
            public string warehouse { get; set; }
            public string delivery_man { get; set; }
            public string phone_number { get; set; }
        }

        public class RootObject
        {
            public List<POD> POD { get; set; }
            public List<CustomerInfo> CustomerInfo { get; set; }
            public List<GoodsInfo> GoodsInfo { get; set; }
            public List<Tracking> Tracking { get; set; }
        }
    }
}
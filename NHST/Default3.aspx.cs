using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default19 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "NHẬP HÀNG 360 - Trang chủ";
            if (!IsPostBack)
            {
                //LoadData();
                Response.Redirect("/dang-nhap");
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
                string timework = confi.TimeWork;


                ltrAddress.Text += "<p>" + confi.Address2 + "</p>";
                ltrHotline.Text += "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                ltrEmail.Text += "<a href=\"mailto:" + email + "\">" + email + "</a>";
                ltrTimeWork.Text += "<p>" + timework + "</p>";
            }
            #region Lấy thông tin trang chủ
            ///Dịch vụ
            ///
            var services = ServiceController.GetAll();
            if (services.Count > 0)
            {
                foreach (var s in services)
                {
                    ltrService.Text += "<li>";
                    ltrService.Text += "	<div class=\"item-right-customer clear\">";
                    ltrService.Text += "		<div class=\"img\">";
                    if (!string.IsNullOrEmpty(s.ServiceLink))
                        ltrService.Text += "<a href=\"" + s.ServiceLink + "\"><img src=\"" + s.ServiceIMG + "\"></a>";
                    else
                        ltrService.Text += "<a href=\"javascript:;\"><img src=\"" + s.ServiceIMG + "\"></a>";
                    ltrService.Text += "		</div>";
                    ltrService.Text += "		<div class=\"info\">";
                    ltrService.Text += "			<a class=\"title\">" + s.ServiceName + "</a>";
                    ltrService.Text += "			<p class=\"info\">" + s.ServiceContent + "</p>";
                    ltrService.Text += "		</div>";
                    ltrService.Text += "		<div class=\"btn-seemore\">";
                    if (!string.IsNullOrEmpty(s.ServiceLink))
                        ltrService.Text += "			<a href=\"" + s.ServiceLink + "\">Xem thêm <span class=\"icon\"><i class=\"fa fa-arrow-circle-right\"></i></span></a>";
                    ltrService.Text += "		</div>";
                    ltrService.Text += "	</div>";
                    ltrService.Text += "</li>";
                }
            }

            ///Quy trình
            ///
            var steps = StepController.GetAll("");
            if (steps.Count > 0)
            {
                int count = 1;
                foreach (var s in steps)
                {
                    string name = s.StepName;
                    string namenotdau = LeoUtils.ConvertToUnSign(name);
                    //<li class="active">
                    //    <a class="name_btn" data-toggle="tab" href="#menu1">
                    //        <span class="icon">
                    //            <img src="/App_Themes/vantaihoakieu/images/icon-step2.png">
                    //        </span>
                    //        Đăng ký tài khoản</a>
                    //</li>
                    if (count == 1)
                    {
                        ltrStep1.Text += "<li class=\"active\">";
                    }
                    else
                    {
                        ltrStep1.Text += "<li class=\"\">";
                    }
                    ltrStep1.Text += "<a class=\"name_btn\" data-toggle=\"tab\" href=\"#menu" + count + "\">";
                    ltrStep1.Text += "<span class=\"icon\">";
                    ltrStep1.Text += "<img src=\"" + s.StepIMG + "\">";
                    ltrStep1.Text += "</span>" + s.StepName;
                    ltrStep1.Text += "</a>";
                    ltrStep1.Text += "</li>";

                    if (count == 1)
                    {
                        ltrStep2.Text += "<div id=\"menu" + count + "\" class=\"tab-pane fade in active\">";
                    }
                    else
                    {
                        ltrStep2.Text += "<div id=\"menu" + count + "\" class=\"tab-pane fade in\">";
                    }

                    ltrStep2.Text += "  <div class=\"sec-align\">";
                    ltrStep2.Text += "      <div class=\"nav-tabswap-ct\">";
                    ltrStep2.Text += "          <div class=\"guide\">";
                    ltrStep2.Text += "              <div class=\"step-guide-ct\">";
                    ltrStep2.Text += "                  <div class=\"ct\">";
                    ltrStep2.Text += "                      <h4 class=\"hd\">" + name + "</h4>";
                    ltrStep2.Text += "                      <p>" + s.StepContent + "</p>";
                    if (!string.IsNullOrEmpty(s.StepLink))
                        ltrStep2.Text += "                      <div class=\"btn-seemore\"><a href=\"" + s.StepLink + "\">Xem thêm</span></a></div>";
                    ltrStep2.Text += "                  </div>";
                    ltrStep2.Text += "                  <div class=\"img\"><img src=\"/App_Themes/vantaihoakieu/images/iMac.png\" alt=\"\"></div>";
                    ltrStep2.Text += "              </div>";
                    ltrStep2.Text += "          </div>";
                    ltrStep2.Text += "      </div>";
                    ltrStep2.Text += "  </div>";
                    ltrStep2.Text += "</div>";
                    count++;
                }
            }

            ///Quyền lợi khách hàng
            ///
            var ql = CustomerBenefitsController.GetAllByItemType(2);
            if (ql.Count > 0)
            {
                foreach (var q in ql)
                {
                    ltrQL1.Text += "<li class=\"col3__item\">";
                    ltrQL1.Text += "<div class=\"info-card\">";
                    if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                        ltrQL1.Text += "<div class=\"img\"><a href=\"" + q.CustomerBenefitLink + "\"><img src=\"" + q.Icon + "\"></a></div>";
                    else
                        ltrQL1.Text += "<div class=\"img\"><a href=\"javascript:;\"><img src=\"" + q.Icon + "\"></a></div>";
                    ltrQL1.Text += "<div class=\"ct\">";
                    if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                        ltrQL1.Text += "<h3 class=\"hd\"><a href=\"" + q.CustomerBenefitLink + "\">" + q.CustomerBenefitName + "</a></h3>";
                    else
                        ltrQL1.Text += "<h3 class=\"hd\"><a href=\"javascript:;\">" + q.CustomerBenefitName + "</a></h3>";
                    ltrQL1.Text += "<p>" + q.CustomerBenefitDescription + "</p>";
                    ltrQL1.Text += "</div>";
                    ltrQL1.Text += "</div>";
                    ltrQL1.Text += "</li>";

                    //ltrQL1.Text += "<li>";
                    //ltrQL1.Text += "    <div class=\"item-right-customer clear\">";
                    //ltrQL1.Text += "        <div class=\"img\">";
                    //if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                    //    ltrQL1.Text += "            <a href=\"" + q.CustomerBenefitLink + "\"><img src=\"" + q.Icon + "\"></a>";
                    //else
                    //    ltrQL1.Text += "            <a href=\"javascript:;\"><img src=\"" + q.Icon + "\"></a>";
                    //ltrQL1.Text += "        </div>";
                    //ltrQL1.Text += "        <div class=\"info\">";
                    //ltrQL1.Text += "            <a class=\"title\">" + q.CustomerBenefitName + "</a>";
                    //ltrQL1.Text += "            <p>" + q.CustomerBenefitDescription + "</p>";
                    //ltrQL1.Text += "        </div>";
                    //ltrQL1.Text += "        <div class=\"btn-seemore\">";
                    //if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                    //    ltrQL1.Text += "            <a href=\"" + q.CustomerBenefitLink + "\">Xem thêm <span class=\"icon\"><i class=\"fa fa-arrow-circle-right\"></i></span></a>";
                    //ltrQL1.Text += "        </div>";
                    //ltrQL1.Text += "    </div>";
                    //ltrQL1.Text += "</li>";


                    //ltrQL1.Text += "<li>";
                    //ltrQL1.Text += "	<div class=\"item-services\">";
                    //ltrQL1.Text += "		<div class=\"img\">";
                    //if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                    //    ltrQL1.Text += "        <a href=\"" + q.CustomerBenefitLink + "\">";
                    //else
                    //    ltrQL1.Text += "        <a href=\"javascript:;\">";
                    //ltrQL1.Text += "				<img src=\"" + q.Icon + "\">";
                    //ltrQL1.Text += "			</a>";
                    //ltrQL1.Text += "		</div>";
                    //ltrQL1.Text += "		<div class=\"info\">";
                    //ltrQL1.Text += "			<a class=\"title\">" + q.CustomerBenefitName + "</a>";
                    //ltrQL1.Text += "			<p>" + q.CustomerBenefitDescription + "</p>";
                    //if (!string.IsNullOrEmpty(q.CustomerBenefitLink))
                    //    ltrQL1.Text += "        <div class=\"btn-item-right\"><a class=\"btn-seemore\" href=\"" + q.CustomerBenefitLink + "\">Xem thêm</a></div>";
                    //ltrQL1.Text += "		</div>";
                    //ltrQL1.Text += "	</div>";
                    //ltrQL1.Text += "</li>";
                }
            }
            #endregion
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Response.Write(txtSearch.Text.Trim());
            try
            {
                string text = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    string a = PJUtils.TranslateTextNew(text, "vi", "zh");
                    a = a.Replace("[", "").Replace("]", "").Replace("\"", "");
                    string[] ass = a.Split(',');
                    string page = hdfWebsearch.Value;
                    SearchPage(page, PJUtils.RemoveHTMLTags(ass[0]));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #region Translate And Run
        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                var request = (HttpWebRequest)WebRequest.Create(url);
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
                var doc = Dcsoup.Parse(content);
                var scoreDiv = doc.Select("html").Select("span[id=result_box]").Html;
                return scoreDiv;
            }
            catch
            {
                return "";
            }

        }

        public void SearchPage(string page, string text)
        {
            string linkgo = "";
            if (page == "tmall")
            {
                string a = text;
                string textsearch_tmall = GetHashString(a);
                //string fullLinkSearch_tmall = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
                linkgo = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
            }
            else if (page == "taobao")
            {
                string a = text;
                string textsearch_taobao = GetHashString(a);
                //string fullLinkSearch_taobao = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                linkgo = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                //https://world.taobao.com/search/search.htm?q=%B9%AB%BC%A6&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1
            }
            else if (page == "1688")
            {
                string a = text;
                string textsearch_1688 = GetHashString(a);
                //string fullLinkSearch_1688 = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
                linkgo = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
            }
            Response.Redirect(linkgo);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "redirect('" + linkgo + "')", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "redirect('" + linkgo + "');", true);
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(inputString);
            return bytes;
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append("%" + b.ToString("X2"));

            return sb.ToString();
        }
        #endregion
        [WebMethod]
        public static string closewebinfo()
        {
            HttpContext.Current.Session["infoclose"] = "ok";
            return "ok";
        }
        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopup;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n);
                }
                else
                    return "null";
            }
            else
                return null;

        }
        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }
    }
}
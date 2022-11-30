using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using System.Web.UI.HtmlControls;

namespace NHST
{
    public partial class cong_cu_dat_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkLogin();
                LoadSEO();
            }
        }
        public void LoadSEO()
        {
            var home = PageSEOController.GetByID(3);
            string weblink = "https://1688express.com";
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (home != null)
            {
                HtmlHead objHeader = (HtmlHead)Page.Header;

                //we add meta description
                HtmlMeta objMetaFacebook = new HtmlMeta();

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "fb:app_id");
                objMetaFacebook.Content = "676758839172144";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:url");
                objMetaFacebook.Content = url;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:type");
                objMetaFacebook.Content = "website";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:title");
                objMetaFacebook.Content = home.ogtitle;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:description");
                objMetaFacebook.Content = home.ogdescription;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image");
                if (string.IsNullOrEmpty(home.ogimage))
                    objMetaFacebook.Content = weblink + "/App_Themes/vcdqg/images/main-logo.png";
                else
                    objMetaFacebook.Content = weblink + home.ogimage;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:width");
                objMetaFacebook.Content = "200";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:height");
                objMetaFacebook.Content = "500";
                objHeader.Controls.Add(objMetaFacebook);

                this.Title = home.metatitle;
                HtmlMeta meta = new HtmlMeta();
                meta = new HtmlMeta();
                meta.Attributes.Add("name", "description");
                meta.Content = home.metadescription;
                objHeader.Controls.Add(meta);

                meta = new HtmlMeta();
                meta.Attributes.Add("name", "keyword");
                meta.Content = home.metakeyword;
                objHeader.Controls.Add(meta);

            }
        }
        public void checkLogin()
        {
            if (Session["userLoginSystem"] != null)
            {
                hdfCheckLogin.Value = "logined";
            }
            else
            {
                hdfCheckLogin.Value = "notlogin";
            }
        }
        public void loadProduct(string link)
        {
            if (link.Contains("m.intl"))
            {
                Uri linkpro = new Uri(link);
                string idpro = HttpUtility.ParseQueryString(linkpro.Query).Get("id");
                string spm = HttpUtility.ParseQueryString(linkpro.Query).Get("spm");
                string orderlink = "https://world.taobao.com/item/" + idpro + ".htm?spm=" + spm + "";
                loadProduct1(orderlink);
            }
            else
            {
                string httplink = "";
                if (link.Contains("https"))
                    httplink = "https";
                else
                    httplink = "http";

                pn_productview.Visible = false;
                if (!string.IsNullOrEmpty(link))
                {

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
                    var doc = Dcsoup.Parse(content);


                    var scoreDiv = doc.Select("body");
                    if (link.Contains(".taobao.com/"))
                    {

                        if (link.Contains("item.taobao.com"))
                        {

                            //ltr_content.Text = scoreDiv.Html;                        
                            var anofollow = scoreDiv.Select("a[rel=nofollow]");
                            //if(anofollow.HasClass("tb-main-pic"))
                            //{
                            //    anofollow = anofollow.Select("a[rel=no-follow]");
                            //}
                            var href = anofollow.Attr("href");

                            var img = scoreDiv.Select("img[id=J_ImgBooth]");
                            var imgsrc = img.Attr("src");

                            var title = scoreDiv.Select("h3[class=tb-main-title]").Text;

                            //var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                            string origin_price = scoreDiv.Select("input[name=current_price]").Val;
                            //if (span_origin_price.Attr("itemprop") == "lowPrice")
                            //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                            //else if (span_origin_price.Attr("itemprop") == "price")
                            //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                            var promotion_price_div = scoreDiv.Select("input[name=current_price]").Val;
                            string promotion_price = "";
                            promotion_price = origin_price;

                            //var seller_link = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");
                            string itemID = scoreDiv.Select("input[name=item_id]").Val;
                            string shopID = "";
                            string seller_id = "0";
                            if (!string.IsNullOrEmpty(href))
                            {
                                Uri myUri = new Uri(httplink + ":" + href.ToString());
                                //itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                                shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                            }
                            seller_id = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");

                            var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                            var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                                .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                            var listmaterial = scoreDiv.Select("div[class=tb-skin]").Select("dl");

                            var attribute = scoreDiv.Select("div[id=attributes]").Html;
                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tb-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            ltr_material.Text += liitem.Html;
                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        //ltr_material.Text += item.Select("dd").Html;

                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "taobao_" + shopID;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = seller_id;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = "TAOBAO";
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;

                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            hdf_product_ok.Value = "ok";
                            pn_productview.Visible = true;
                        }
                        else
                        {
                            //ltr_content.Text = scoreDiv.Html;
                            //var scoreDiv = doc.Select("div[class=sea-detail-bd]");
                            var anofollow = scoreDiv.Select("a[rel=no-follow]");
                            var href = anofollow.Attr("href");

                            var img = anofollow.Select("img[id=J_ThumbView]");
                            var imgsrc = img.Attr("src");

                            var title = img.Attr("alt");

                            var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                            string origin_price = "0";
                            if (span_origin_price.Attr("itemprop") == "lowPrice")
                                origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                            else if (span_origin_price.Attr("itemprop") == "price")
                                origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                            var promotion_price_div = scoreDiv.Select("div[id=J_PromoPrice]");
                            string promotion_price = "";
                            promotion_price = promotion_price_div.Select("strong[class=tb-rmb-num]").Text;

                            var seller_link = scoreDiv.Select("button[id=J_listBuyerOnView]").Attr("data-api");
                            string itemID = "0";
                            string shopID = "0";
                            string seller_id = "0";
                            if (!string.IsNullOrEmpty(href))
                            {
                                Uri myUri = new Uri(httplink + ":" + href.ToString());
                                itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                                shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                            }
                            if (!string.IsNullOrEmpty(seller_link))
                            {
                                Uri seller = new Uri(httplink + ":" + seller_link);
                                seller_id = HttpUtility.ParseQueryString(seller.Query).Get("seller_num_id");
                            }



                            var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                            var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                                .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                            var listmaterial = scoreDiv.Select("div[class=item-sku]").Select("dl");

                            var attribute = scoreDiv.Select("div[id=attributes]").Html;
                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-pv") + "\">";
                                        ltr_material.Text += liitem.Html;
                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    //ltr_material.Text += item.Select("dd").Html;

                                    ltr_material.Text += "</div>";
                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "taobao_" + shopID;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = seller_id;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = "TAOBAO";
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;

                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            hdf_product_ok.Value = "ok";
                            pn_productview.Visible = true;
                        }

                    }
                    else if (link.Contains(".1688.com/"))
                    {
                        //ltr_content.Text = scoreDiv.Html.ToString();
                        if (link.Contains("detail.1688.com"))
                        {

                            string fullcontent = scoreDiv.Html.ToString();
                            string price_ref = PJUtils.getBetween(fullcontent, "convertPrice", ",");
                            string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");
                            string shopID = PJUtils.getBetween(fullcontent, "userId", ",");
                            string itemID = PJUtils.getBetween(fullcontent, "'offerid'", ",");

                            //Title
                            var title = scoreDiv.Select("h1[class=d-title]").Text;

                            //Price
                            string price = scoreDiv.Select("td[class=price]")[0].Select("em[class=value]").Text;

                            //Image
                            var imgsrc = scoreDiv.Select("div[class=tab-pane]").Select("img").Attr("src");

                            //ShopID
                            string shopid = shopID.Replace(":", "").Replace("\"", "").Replace("'", "");

                            //ShopName
                            string shopname = scoreDiv.Select("a[class=company-name]").Text;

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "1688";

                            //ItemID
                            itemID = itemID.Replace("'", "").Replace(":", "");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=mod-detail-attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }



                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title + " - " + itemID;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = shopid;
                            ltr_wangwang.Value = wangwang;
                            //ltr_stock.Value = "";
                            //ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                    }
                    else if (link.Contains(".tmall.com/") || link.Contains(".tmall.hk/"))
                    {
                        //ltr_content.Text = scoreDiv.Html;
                        if (link.Contains("world.tmall.com"))
                        {
                            string fullcontent = scoreDiv.Html.ToString();
                            string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");
                            string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");


                            //Title
                            var title = scoreDiv.Select("input[name=title]").Val;

                            //Price
                            string origin_price = returntext.Replace(":", "");
                            origin_price = origin_price.Replace("\"", "");
                            string finlaprice = "";
                            string[] oarray = new string[] { };

                            if (origin_price.Contains("-"))
                            {
                                oarray = origin_price.Split('-');
                                finlaprice = oarray[0];
                            }
                            else
                            {
                                finlaprice = origin_price;
                            }
                            //Image
                            var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                            //ShopID
                            string shopid = scoreDiv.Select("div[id=LineZing]").Attr("shopid");

                            //ShopName
                            string shopname = scoreDiv.Select("input[name=seller_nickname]").Val;

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "TMALL";

                            //ItemID
                            string itemID = scoreDiv.Select("div[id=LineZing]").Attr("itemid");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }



                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = finlaprice;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + finlaprice + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(finlaprice) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "tmall_" + shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = sellerID;
                            ltr_wangwang.Value = wangwang;
                            //ltr_stock.Value = "";
                            //ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                        else
                        {
                            string fullcontent = scoreDiv.Html.ToString();
                            string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");

                            //Title
                            var title = scoreDiv.Select("div[class=tb-detail-hd]").Select("h1").Text;

                            //Price
                            string origin_price = returntext.Replace(":", "");
                            origin_price = origin_price.Replace("\"", "");

                            //Property

                            //Value

                            //Image
                            var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                            //ShopID
                            string shopidLink = scoreDiv.Select("a[id=xshop_collection_href]").Attr("href");
                            Uri shopidLinkget = new Uri(httplink + ":" + shopidLink.ToString());
                            string shopid = HttpUtility.ParseQueryString(shopidLinkget.Query).Get("id");

                            //ShopName
                            var shopname = scoreDiv.Select("input[name=seller_nickname]").Attr("value");

                            //Seller ID
                            string selleridlink = scoreDiv.Select("div[id=J_SellerInfo]").Attr("data-url");
                            Uri selleriddetach = new Uri(httplink + ":" + selleridlink.ToString());
                            string sellerid = HttpUtility.ParseQueryString(selleriddetach.Query).Get("user_num_id");

                            //Wangwang
                            string wangwang = shopname;

                            //Site
                            string site = "TMALL";

                            //ItemID
                            Uri itemIDLink = new Uri(link.ToString());
                            string itemID = HttpUtility.ParseQueryString(itemIDLink.Query).Get("id");

                            //Origin Link
                            string originlink = link;

                            //Outer ID
                            string outerid = HttpUtility.ParseQueryString(itemIDLink.Query).Get("skuID");

                            //attribute
                            var attribute = scoreDiv.Select("div[id=attributes]").Html;

                            //Material
                            var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                            ltr_material.Text = "";
                            if (listmaterial.Count() > 0)
                            {
                                foreach (var item in listmaterial)
                                {
                                    if (item.HasClass("tm-sale-prop"))
                                    {
                                        ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                        ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                        var listmate = item.Select("dd").Select("ul").Select("li");
                                        ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                        foreach (var liitem in listmate)
                                        {
                                            ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                            var atag = liitem.Select("a");
                                            if (atag.HasAttr("style"))
                                            {
                                                ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                            }
                                            else
                                            {
                                                ltr_material.Text += "<a>" + atag.Html + "</a>";
                                            }

                                            ltr_material.Text += "</li>";
                                        }
                                        ltr_material.Text += "</ul>";
                                        ltr_material.Text += "</div>";
                                    }

                                }
                            }

                            ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                            hdf_image_prod.Value = imgsrc;
                            ltr_title_origin.Text = title;
                            hdf_title_origin.Value = title;

                            hdf_price_origin.Value = origin_price;
                            ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                            ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                            ltr_property.Value = "";
                            ltr_data_value.Value = "";
                            ltr_shop_id.Value = "tmall_" + shopid;
                            ltr_shop_name.Value = shopname;
                            ltr_seller_id.Value = sellerid;
                            ltr_wangwang.Value = wangwang;
                            ltr_stock.Value = "";
                            ltr_location_sale.Value = "";
                            ltr_site.Value = site;
                            ltr_item_id.Value = itemID;
                            ltr_link_origin.Value = link;
                            ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                            pn_productview.Visible = true;
                        }
                    }
                }
                else
                {
                    hdf_product_ok.Value = "fail";
                }
            }

            
        }
        public void loadProduct1(string link)
        {
            //txt_link.Text = link;            
            string httplink = "";
            if (link.Contains("https"))
                httplink = "https";
            else
                httplink = "http";

            pn_productview.Visible = false;
            if (!string.IsNullOrEmpty(link))
            {

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
                var doc = Dcsoup.Parse(content);


                var scoreDiv = doc.Select("body");
                if (link.Contains(".taobao.com/"))
                {
                    if (link.Contains("item.taobao.com"))
                    {

                        //ltr_content.Text = scoreDiv.Html;                        
                        var anofollow = scoreDiv.Select("a[rel=nofollow]");
                        //if(anofollow.HasClass("tb-main-pic"))
                        //{
                        //    anofollow = anofollow.Select("a[rel=no-follow]");
                        //}
                        var href = anofollow.Attr("href");

                        var img = scoreDiv.Select("img[id=J_ImgBooth]");
                        var imgsrc = img.Attr("src");

                        var title = scoreDiv.Select("h3[class=tb-main-title]").Text;

                        //var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                        string origin_price = scoreDiv.Select("input[name=current_price]").Val;
                        //if (span_origin_price.Attr("itemprop") == "lowPrice")
                        //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                        //else if (span_origin_price.Attr("itemprop") == "price")
                        //    origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                        var promotion_price_div = scoreDiv.Select("input[name=current_price]").Val;
                        string promotion_price = "";
                        promotion_price = origin_price;

                        //var seller_link = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");
                        string itemID = scoreDiv.Select("input[name=item_id]").Val;
                        string shopID = "";
                        string seller_id = "0";
                        if (!string.IsNullOrEmpty(href))
                        {
                            Uri myUri = new Uri(httplink + ":" + href.ToString());
                            //itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                            shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                        }
                        seller_id = scoreDiv.Select("div[id=J_Pinus_Enterprise_Module]").Attr("data-sellerid");

                        var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                        var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                            .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                        var listmaterial = scoreDiv.Select("div[class=tb-skin]").Select("dl");

                        var attribute = scoreDiv.Select("div[id=attributes]").Html;
                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tb-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        ltr_material.Text += liitem.Html;
                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    //ltr_material.Text += item.Select("dd").Html;

                                    ltr_material.Text += "</div>";
                                }

                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "taobao_" + shopID;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = seller_id;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = "TAOBAO";
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;

                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        hdf_product_ok.Value = "ok";
                        pn_productview.Visible = true;
                    }
                    else
                    {
                        //ltr_content.Text = scoreDiv.Html;
                        //var scoreDiv = doc.Select("div[class=sea-detail-bd]");
                        var anofollow = scoreDiv.Select("a[rel=no-follow]");
                        var href = anofollow.Attr("href");

                        var img = anofollow.Select("img[id=J_ThumbView]");
                        var imgsrc = img.Attr("src");

                        var title = img.Attr("alt");

                        var span_origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span");
                        string origin_price = "0";
                        if (span_origin_price.Attr("itemprop") == "lowPrice")
                            origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=lowPrice]").Html;
                        else if (span_origin_price.Attr("itemprop") == "price")
                            origin_price = scoreDiv.Select("div[id=J_priceStd]").Select("strong[class=tb-rmb-num]").Select("span[itemprop=price]").Html;

                        var promotion_price_div = scoreDiv.Select("div[id=J_PromoPrice]");
                        string promotion_price = "";
                        promotion_price = promotion_price_div.Select("strong[class=tb-rmb-num]").Text;

                        var seller_link = scoreDiv.Select("button[id=J_listBuyerOnView]").Attr("data-api");
                        string itemID = "0";
                        string shopID = "0";
                        string seller_id = "0";
                        if (!string.IsNullOrEmpty(href))
                        {
                            Uri myUri = new Uri(httplink + ":" + href.ToString());
                            itemID = HttpUtility.ParseQueryString(myUri.Query).Get("itemId");
                            shopID = HttpUtility.ParseQueryString(myUri.Query).Get("shopId");
                        }
                        if (!string.IsNullOrEmpty(seller_link))
                        {
                            Uri seller = new Uri(httplink + ":" + seller_link);
                            seller_id = HttpUtility.ParseQueryString(seller.Query).Get("seller_num_id");
                        }



                        var shopname = scoreDiv.Select("div[class=tb-shop-name]").Select("h3").Select("a").Attr("title");



                        var wangwang = scoreDiv.Select("div[class=detail-bd-side]").Select("div[class=detail-bd-side-wrap]").Select("div[class=shop-info]")
                            .Select("div[class=shop-info-wrap]").Select("div[class=tb-shop-info-hd]").Select("div[class=tb-shop-seller]").Select("dl").Select("dd").Select("a").Html;

                        var listmaterial = scoreDiv.Select("div[class=item-sku]").Select("dl");

                        var attribute = scoreDiv.Select("div[id=attributes]").Html;
                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                var listmate = item.Select("dd").Select("ul").Select("li");
                                ltr_material.Text += "<ul class=\"tb-cleafix\">";
                                foreach (var liitem in listmate)
                                {
                                    ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-pv") + "\">";
                                    ltr_material.Text += liitem.Html;
                                    ltr_material.Text += "</li>";
                                }
                                ltr_material.Text += "</ul>";
                                //ltr_material.Text += item.Select("dd").Html;

                                ltr_material.Text += "</div>";
                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "taobao_" + shopID;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = seller_id;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = "TAOBAO";
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;

                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        hdf_product_ok.Value = "ok";
                        pn_productview.Visible = true;
                    }

                }
                else if (link.Contains(".1688.com/"))
                {
                    //ltr_content.Text = scoreDiv.Html.ToString();
                    if (link.Contains("detail.1688.com"))
                    {

                        string fullcontent = scoreDiv.Html.ToString();
                        string price_ref = PJUtils.getBetween(fullcontent, "convertPrice", ",");
                        string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");
                        string shopID = PJUtils.getBetween(fullcontent, "userId", ",");
                        string itemID = PJUtils.getBetween(fullcontent, "'offerid'", ",");

                        //Title
                        var title = scoreDiv.Select("h1[class=d-title]").Text;

                        //Price
                        string price = scoreDiv.Select("td[class=price]")[0].Select("em[class=value]").Text;

                        //Image
                        var imgsrc = scoreDiv.Select("div[class=tab-pane]").Select("img").Attr("src");

                        //ShopID
                        string shopid = shopID.Replace(":", "").Replace("\"", "").Replace("'", "");

                        //ShopName
                        string shopname = scoreDiv.Select("a[class=company-name]").Text;

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "1688";

                        //ItemID
                        itemID = itemID.Replace("'", "").Replace(":", "");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=mod-detail-attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }



                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title + " - " + itemID;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = shopid;
                        ltr_wangwang.Value = wangwang;
                        //ltr_stock.Value = "";
                        //ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                }
                else if (link.Contains(".tmall.com/") || link.Contains(".tmall.hk/"))
                {
                    //ltr_content.Text = scoreDiv.Html;
                    if (link.Contains("world.tmall.com"))
                    {
                        string fullcontent = scoreDiv.Html.ToString();
                        string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");
                        string sellerID = PJUtils.getBetween(fullcontent, "user_num_id=", "&");


                        //Title
                        var title = scoreDiv.Select("input[name=title]").Val;

                        //Price
                        string origin_price = returntext.Replace(":", "");
                        origin_price = origin_price.Replace("\"", "");
                        string finlaprice = "";
                        string[] oarray = new string[] { };

                        if (origin_price.Contains("-"))
                        {
                            oarray = origin_price.Split('-');
                            finlaprice = oarray[0];
                        }
                        else
                        {
                            finlaprice = origin_price;
                        }
                        //Image
                        var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                        //ShopID
                        string shopid = scoreDiv.Select("div[id=LineZing]").Attr("shopid");

                        //ShopName
                        string shopname = scoreDiv.Select("input[name=seller_nickname]").Val;

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "TMALL";

                        //ItemID
                        string itemID = scoreDiv.Select("div[id=LineZing]").Attr("itemid");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }



                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = finlaprice;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + finlaprice + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(finlaprice) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "tmall_" + shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = sellerID;
                        ltr_wangwang.Value = wangwang;
                        //ltr_stock.Value = "";
                        //ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                    else
                    {
                        string fullcontent = scoreDiv.Html.ToString();
                        string returntext = PJUtils.getBetween(fullcontent, "defaultItemPrice", ",");

                        //Title
                        var title = scoreDiv.Select("div[class=tb-detail-hd]").Select("h1").Text;

                        //Price
                        string origin_price = returntext.Replace(":", "");
                        origin_price = origin_price.Replace("\"", "");

                        //Property

                        //Value

                        //Image
                        var imgsrc = scoreDiv.Select("img[id=J_ImgBooth]").Attr("src");

                        //ShopID
                        string shopidLink = scoreDiv.Select("a[id=xshop_collection_href]").Attr("href");
                        Uri shopidLinkget = new Uri(httplink + ":" + shopidLink.ToString());
                        string shopid = HttpUtility.ParseQueryString(shopidLinkget.Query).Get("id");

                        //ShopName
                        var shopname = scoreDiv.Select("input[name=seller_nickname]").Attr("value");

                        //Seller ID
                        string selleridlink = scoreDiv.Select("div[id=J_SellerInfo]").Attr("data-url");
                        Uri selleriddetach = new Uri(httplink + ":" + selleridlink.ToString());
                        string sellerid = HttpUtility.ParseQueryString(selleriddetach.Query).Get("user_num_id");

                        //Wangwang
                        string wangwang = shopname;

                        //Site
                        string site = "TMALL";

                        //ItemID
                        Uri itemIDLink = new Uri(link.ToString());
                        string itemID = HttpUtility.ParseQueryString(itemIDLink.Query).Get("id");

                        //Origin Link
                        string originlink = link;

                        //Outer ID
                        string outerid = HttpUtility.ParseQueryString(itemIDLink.Query).Get("skuID");

                        //attribute
                        var attribute = scoreDiv.Select("div[id=attributes]").Html;

                        //Material
                        var listmaterial = scoreDiv.Select("div[class=tb-sku]").Select("dl");

                        ltr_material.Text = "";
                        if (listmaterial.Count() > 0)
                        {
                            foreach (var item in listmaterial)
                            {
                                if (item.HasClass("tm-sale-prop"))
                                {
                                    ltr_material.Text += "<div id=\"" + item.Select("dt").Html + "\" class=\"material-product\">";
                                    ltr_material.Text += "<label>" + item.Select("dt").Html + ": </label>";
                                    var listmate = item.Select("dd").Select("ul").Select("li");
                                    ltr_material.Text += "<ul class=\"tb-cleafix tm-clear J_TSaleProp tb-img\">";
                                    foreach (var liitem in listmate)
                                    {
                                        ltr_material.Text += "<li class=\"J_SKU\" onclick=\"activemate($(this),'" + item.Select("dt").Html + "')\" data-pv=\"" + liitem.Attr("data-value") + "\">";
                                        var atag = liitem.Select("a");
                                        if (atag.HasAttr("style"))
                                        {
                                            ltr_material.Text += "<a style=\"" + atag.Attr("style") + "\" class=\"tb-img\">" + atag.Html + "</a>";
                                        }
                                        else
                                        {
                                            ltr_material.Text += "<a>" + atag.Html + "</a>";
                                        }

                                        ltr_material.Text += "</li>";
                                    }
                                    ltr_material.Text += "</ul>";
                                    ltr_material.Text += "</div>";
                                }

                            }
                        }

                        ltr_image.Text = "<img src=\"" + imgsrc + "\" />";
                        hdf_image_prod.Value = imgsrc;
                        ltr_title_origin.Text = title;
                        hdf_title_origin.Value = title;

                        hdf_price_origin.Value = origin_price;
                        ltr_price_origin.Text = "<span class=\"price-label\">Giá Gốc:</span> <span class=\"price-color cny\">￥" + origin_price + "</span>";
                        ltr_price_vnd.Text = "<span class=\"price-label\">Giá VNĐ:</span> <span class=\"price-color vnd\">" + string.Format("{0:N0}", Convert.ToDouble(origin_price) * Convert.ToDouble(ConfigurationController.GetByTop1().Currency)) + " VNĐ</span>";
                        ltr_property.Value = "";
                        ltr_data_value.Value = "";
                        ltr_shop_id.Value = "tmall_" + shopid;
                        ltr_shop_name.Value = shopname;
                        ltr_seller_id.Value = sellerid;
                        ltr_wangwang.Value = wangwang;
                        ltr_stock.Value = "";
                        ltr_location_sale.Value = "";
                        ltr_site.Value = site;
                        ltr_item_id.Value = itemID;
                        ltr_link_origin.Value = link;
                        ltr_attribute.Text = "<div id=\"attributes\" class=\"attributes\">" + attribute + "</div>";
                        pn_productview.Visible = true;
                    }
                }
            }
            else
            {
                hdf_product_ok.Value = "fail";
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            string link = txt_link.Text.Trim();
            loadProduct(link);
        }
    }
}
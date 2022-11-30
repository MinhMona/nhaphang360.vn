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
using static NHST.Uploads.Images.WebService1;

namespace NHST.serajax
{
    /// <summary>
    /// Summary description for NHTQServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NHTQServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        
        [WebMethod(EnableSession = true)]
        public string receimonaveremediaquest(string title_origin, string title_translated, string price_origin, string price_promotion, string property_translated, string property, string data_value,
        string image_model, string image_origin, string shop_id, string shop_name, string seller_id, string wangwang, string quantity, string stock, string location_sale, string site, string comment,
        string item_id, string link_origin, string outer_id, string error, string weight, string step, string pricestep, string brand, string category_name, string category_id, string tool, string version, string is_translate)
        {
            //return property;
            if (Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                int UID = 0;
                if (obj_user != null)
                {
                    UID = obj_user.ID;
                }
                double priceOrigin = 0;
                double pricePromotion = 0;
                if (price_origin.ToFloat(0) > 0)
                {
                    priceOrigin = Convert.ToDouble(price_origin);
                }
                if (price_promotion.ToFloat(0) > 0)
                {
                    pricePromotion = Convert.ToDouble(price_promotion);
                }
                priceOrigin = Math.Round(priceOrigin, 2);
                pricePromotion = Math.Round(pricePromotion, 2);
               
                var id = OrderShopTempController.Insert(UID, shop_id, shop_name, site, DateTime.Now);
                if (id.ToInt() > 0)
                {
                    string kq = OrderTempController.Insert(UID, Convert.ToInt32(id), title_origin, title_translated,
                        priceOrigin.ToString(), pricePromotion.ToString(), property_translated,
                                                    property, data_value, image_model, image_origin, shop_id, shop_name, seller_id, wangwang, quantity,
                                                    stock, location_sale, site, comment, item_id, link_origin, outer_id, error, weight,
                                                    step, pricestep, brand, category_name, category_id, tool, version, Convert.ToBoolean(is_translate), DateTime.Now);
                    return kq;
                }
                else if (id.ToInt() == -1)
                {
                    return "outofquantity";
                }
                return "fail";
            }
            else
            {
                return "login";
            }
        }
        [WebMethod(EnableSession = true)]
        public string getExchangeRate()
        {
            //return property;
            string Exchangerate = ConfigurationController.GetByTop1().Currency;
            return Exchangerate;
        }
    }
}

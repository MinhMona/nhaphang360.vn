using MB.Extensions;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class requestOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public string receiverequest(string title_origin, string title_translated, string price_origin, string price_promotion, string property_translated, string property, string data_value,
            string image_model, string image_origin, string shop_id, string shop_name, string seller_id, string wangwang, string quantity, string stock, string location_sale, string site, string comment,
            string item_id, string link_origin, string outer_id, string error, string weight, string step, string pricestep, string brand, string category_name, string category_id, string tool, string version, string is_translate)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username);
                int UID = 0;
                if (obj_user != null)
                {
                    UID = obj_user.ID;
                }
                var id = OrderShopTempController.Insert(UID, shop_id, shop_name, site, DateTime.Now);
                if (id.ToInt() > 0)
                {
                    string kq = OrderTempController.Insert(UID, Convert.ToInt32(id), title_origin, title_translated, price_origin, price_promotion, property_translated, property,
                                                    data_value, image_model, image_origin, shop_id, shop_name, seller_id, wangwang, quantity,
                                                    stock, location_sale, site, comment, item_id, link_origin, outer_id, error, weight,
                                                    step, pricestep, brand, category_name, category_id, tool, version, Convert.ToBoolean(is_translate), DateTime.Now);



                    return kq;
                }
                return "fail";
            }
            else
            {
                return "login";
            }
        }
    }
}
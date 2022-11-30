using MB.Extensions;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class bang_tich_luy_diem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
            }

        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            string html = "";
            html += username;

            var user = AccountController.GetByUsername(username);
            if (user != null)
            {
                int userid = user.ID;
                var cus_orders = MainOrderController.GetSuccessByCustomer(user.ID);

                double totalpay = 0;
                if (cus_orders.Count > 0)
                {
                    foreach (var item in cus_orders)
                    {
                        totalpay += item.TotalPriceVND.ToFloat(0);
                    }
                    
                }
                lblPoint.Text = string.Format("{0:N0}", totalpay);
                lblVip.Text = UserLevelController.GetByID(user.LevelID.ToString().ToInt()).LevelName;
            }
        }
    }
}
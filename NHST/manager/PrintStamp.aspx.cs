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
using MB.Extensions;

namespace NHST.manager
{
    public partial class PrintStamp : System.Web.UI.Page
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
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0)
                    //    Response.Redirect("/trang-chu");
                }
                loaddata();
            }
        }
        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            int uid = obj_user.ID;
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    ViewState["MOID"] = id;
                    if (obj_user != null)
                    {
                        lblOrderID.Text = id.ToString();
                        #region Lấy thông tin người đặt
                        var usercreate = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (usercreate != null)
                        {
                            lblUsername.Text = usercreate.Username;
                        }
                        #endregion
                        #region Lấy thông tin đơn hàng
                        string mavandon = "";
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode))
                            mavandon = o.OrderTransactionCode;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode2))
                            mavandon += ", " + o.OrderTransactionCode2;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode3))
                            mavandon += ", " + o.OrderTransactionCode3;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode4))
                            mavandon += ", " + o.OrderTransactionCode4;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode5))
                            mavandon += ", " + o.OrderTransactionCode5;
                        lblOrderCodeTrans.Text = mavandon;
                        if (o.OrderWeight != null)
                            lblWeight.Text = o.OrderWeight.ToString();
                        else
                            lblWeight.Text = "0";

                        #endregion
                        #region Lấy sản phẩm
                        List<tbl_Order> lo = new List<tbl_Order>();
                        lo = OrderController.GetByMainOrderID(o.ID);
                        double totalProducts = 0;
                        if (lo.Count > 0)
                        {
                            foreach (var p in lo)
                            {
                                totalProducts += Convert.ToDouble(p.quantity);
                            }
                        }
                        lblProductCount.Text = totalProducts.ToString();
                        #endregion                        
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }
    }
}
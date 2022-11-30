using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class NHSTExchangeRate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadConfiguration();

        }


        public void LoadConfiguration()
        {
            var c = ConfigurationController.GetByTop1();
            if (Session["userLoginSystem"] != null)
            {
                string Username = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(Username);
                if (obj_user != null)
                {
                    if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
                    {
                        if (Convert.ToDouble(obj_user.Currency) > 0)
                            Response.Write(Convert.ToDouble(obj_user.Currency));
                        else
                        {
                            if (c != null)
                            {
                                Response.Write(c.Currency);
                            }
                        }
                    }
                    else
                    {
                        if (c != null)
                        {
                            Response.Write(c.Currency);
                        }
                    }
                }
                else
                {
                    if (c != null)
                    {
                        Response.Write(c.Currency);
                    }
                }
            }
            else
            {
                if (c != null)
                {
                    Response.Write(c.Currency);
                }
            }
        }

        //public void LoadConfiguration()
        //{
        //    var c = ConfigurationController.GetByTop1();
        //    if (Session["userLoginSystem"] != null)
        //    {
        //        string Username = Session["userLoginSystem"].ToString();
        //        var obj_user = AccountController.GetByUsername(Username);
        //        if (obj_user != null)
        //        {
        //            if (!string.IsNullOrEmpty(obj_user.Currency.ToString()))
        //            {
        //                if (Convert.ToDouble(obj_user.Currency) > 0)
        //                    Response.Write(Convert.ToDouble(obj_user.Currency));
        //                else
        //                {
        //                    if (c != null)
        //                    {
        //                        Response.Write(c.Currency);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (c != null)
        //                {
        //                    Response.Write(c.Currency);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (c != null)
        //            {
        //                Response.Write(c.Currency);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (c != null)
        //        {
        //            Response.Write(c.Currency);
        //        }
        //    }
        //}
    }
}
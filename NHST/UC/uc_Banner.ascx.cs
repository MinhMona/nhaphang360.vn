using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.UC
{
    public partial class uc_Banner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            //var banners = BannerController.GetAll();
            //if (banners.Count > 0)
            //{
            //    foreach (var b in banners)
            //    {                    
            //        ltrBanner.Text += "<div class=\"banner-slide\"> <div class=\"banner\" style=\"background-image: url(" + b.BannerIMG + ");\"></div></div>";
            //    }
            //}
        }
    }
}
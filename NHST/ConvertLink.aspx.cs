using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart;

namespace NHST
{
    public partial class ConvertLink : System.Web.UI.Page
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

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string link_new = "";
            string link = txtLink.Text;
            string[] products = link.Split(',');

            for (int i = 0; i < products.Length; i++)
            {
                string link_old = products[i];
                string linkconvert = "";
                if (link_old.Contains("m.intl"))
                {
                    Uri linkpro = new Uri(link_old);
                    string idpro = HttpUtility.ParseQueryString(linkpro.Query).Get("id");
                    string spm = HttpUtility.ParseQueryString(linkpro.Query).Get("spm");
                    string orderlink = "https://item.taobao.com/item.htm?id=" + idpro;
                    linkconvert = (orderlink) + ",";
                }
                else if (link_old.Contains("m.1688.com"))
                {
                    Uri linkpro = new Uri(link_old);
                    string idpro = HttpUtility.ParseQueryString(linkpro.Query).Get("offerId");
                    string spm = HttpUtility.ParseQueryString(linkpro.Query).Get("spm");
                    string orderlink = "https://detail.1688.com/offer/" + idpro + ".html";
                    linkconvert = (orderlink) + ",";
                }
                else
                {
                    Uri linkpro = new Uri(link_old);
                    string idpro = HttpUtility.ParseQueryString(linkpro.Query).Get("id");
                    string spm = HttpUtility.ParseQueryString(linkpro.Query).Get("spm");
                    string orderlink = "https://detail.tmall.com/item.htm?id=" + idpro;
                    linkconvert = (orderlink) + ",";
                }
                link_new += linkconvert;
            }
            ltrResult.Text = link_new;
        }

    }
}
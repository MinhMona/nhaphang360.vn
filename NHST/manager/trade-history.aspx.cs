using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;

namespace NHST.manager
{
    public partial class trade_history : System.Web.UI.Page
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
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {
            int UID = Request.QueryString["ID"].ToInt(0);
            var u = AccountController.GetByID(UID);
            if (u != null)
            {
                lblUsername.Text = u.Username;
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int UID = Request.QueryString["ID"].ToInt(0);
            var la = HistoryPayWalletController.GetByUID(UID);
            //string text = Request.QueryString["text"];
            string type = Request.QueryString["type"];
            string fd_text = Request.QueryString["fd"];
            string td_text = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "0")
                {
                    if (!string.IsNullOrEmpty(fd_text))
                    {
                        if (!string.IsNullOrEmpty(td_text))
                        {
                            DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                            DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                            la = la.Where(l => l.CreatedDate >= fd && l.CreatedDate <= td).ToList();
                        }
                        else
                        {
                            DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                            la = la.Where(l => l.CreatedDate >= fd).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td_text))
                        {
                            DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                            la = la.Where(l => l.CreatedDate <= td).ToList();
                        }
                        else
                        {
                            la = la;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(fd_text))
                    {
                        if (!string.IsNullOrEmpty(td_text))
                        {
                            DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                            DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                            la = la.Where(l => l.TradeType == type.ToInt() && l.CreatedDate >= fd && l.CreatedDate <= td).ToList();
                        }
                        else
                        {
                            DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                            la = la.Where(l => l.TradeType == type.ToInt() && l.CreatedDate >= fd).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(td_text))
                        {
                            DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                            la = la.Where(l => l.TradeType == type.ToInt() && l.CreatedDate <= td).ToList();
                        }
                        else
                        {
                            la = la.Where(l => l.TradeType == type.ToInt()).ToList();
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fd_text))
                {
                    if (!string.IsNullOrEmpty(td_text))
                    {
                        DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                        DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                        la = la.Where(l => l.CreatedDate >= fd && l.CreatedDate <= td).ToList();
                    }
                    else
                    {
                        DateTime fd = Convert.ToDateTime(Request.QueryString["fd"]);
                        la = la.Where(l => l.CreatedDate >= fd).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(td_text))
                    {
                        DateTime td = Convert.ToDateTime(Request.QueryString["td"]);
                        la = la.Where(l => l.CreatedDate <= td).ToList();
                    }
                    else
                    {
                        la = la;
                    }
                }
            }
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int UID = Request.QueryString["ID"].ToInt(0);
            Response.Redirect("/manager/trade-history.aspx?ID=" + UID + "&type=" + ddltype.SelectedValue + "&fd=" + rFD.SelectedDate + "&td=" + rTD.SelectedDate + "");
        }
        #endregion
    }
}
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dang_nhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loaddata();
                if (Session["userLoginSystem"] != null)
                {
                    Response.Redirect("/dashboard");
                }
            }
        }
        public void Loaddata()
        {
            var confi = ConfigurationController.GetByTop1();
            //if (confi != null)
            //{
            //    string hotline = confi.Hotline;
            //    ltrHotlineCall.Text += "<a href=\"tel:" + hotline + "\" class=\"fancybox\">";
            //    ltrHotlineCall.Text += "<div class=\"coccoc-alo-phone coccoc-alo-green coccoc-alo-show\" id=\"coccoc-alo-phoneIcon\">";
            //    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-circle\"></div>";
            //    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-circle-fill\"></div>";
            //    ltrHotlineCall.Text += "<div class=\"coccoc-alo-ph-img-circle\"></div>";
            //    ltrHotlineCall.Text += "</div>";
            //    ltrHotlineCall.Text += "</a>";
            //}
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                if (password.Length > 2)
                {
                    var ac = AccountController.Login(username.Trim().ToLower(), password.Trim());
                    var check = hdfCB.Value;
                    if (ac != null)
                    {
                        ChatHub ch = new ChatHub();
                        if (!string.IsNullOrEmpty(ac.LoginStatus))
                        {
                            Session["StateLogin"] = ac.LoginStatus;
                        }
                        else
                        {
                            Session["StateLogin"] = "1";
                        }
                        if (ac.Status == 2)
                        {
                            Session["StateLogin"] = TokenSession.CreateAndStoreSessionToken(txtUsername.Text);
                            ac = AccountController.GetByID(ac.ID);
                            ch.Login(ac.ID.ToString(), ac.LoginStatus);

                            int role = Convert.ToInt32(ac.RoleID);
                            if (role != 1)
                            {
                                Session["userLoginSystem"] = username;
                                if (check == "1")
                                {
                                    Response.Cookies["Username"].Expires = DateTime.UtcNow.AddHours(7).AddDays(30);
                                    Response.Cookies["Password"].Expires = DateTime.UtcNow.AddHours(7).AddDays(30);
                                }
                                Response.Cookies["Username"].Value = username;
                                Response.Cookies["Password"].Value = password;

                                if (role == 3)
                                    Response.Redirect("/manager/OrderList.aspx");
                                else if(role == 4)
                                    Response.Redirect("/manager/TQWareHouse-DHH.aspx");
                                else if (role == 5)
                                    Response.Redirect("/manager/VNWarehouse-DHH.aspx");
                                else if (role == 6)
                                    Response.Redirect("/manager/OrderList.aspx");
                                else
                                    Response.Redirect("/manager/home.aspx");
                            }
                            else
                            {
                                Session["userLoginSystem"] = username;
                                if (check == "1")
                                {
                                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                                }
                                Response.Cookies["Username"].Value = username;
                                Response.Cookies["Password"].Value = password;
                                Response.Redirect("/danh-sach-don-hang?t=1");
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Tài khoản của bạn đã bị khóa.", "e", false, Page);
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Sai Username hoặc Password, vui lòng kiểm tra lại.", "e", false, Page);

                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Sai Username hoặc Password, vui lòng kiểm tra lại.", "e", false, Page);
                }
            }
        }
    }
}
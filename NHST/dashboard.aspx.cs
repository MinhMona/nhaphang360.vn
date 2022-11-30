using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class dashboard : System.Web.UI.Page
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
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                StringBuilder ohtml = new StringBuilder();
                StringBuilder phtml = new StringBuilder();
                StringBuilder thtml = new StringBuilder();
                StringBuilder ottml = new StringBuilder();

                var os = MainOrderController.GetAllByUIDAndOrderType(obj_user.ID, 1).OrderByDescending(x => x.ID).ToList();
                ltrTotalOrder.Text = os.Count().ToString();
                if (os.Count > 0)
                {
                    var os6 = os.Take(6).ToList();
                    foreach (var item in os6)
                    {
                        ohtml.Append("<tr>");
                        ohtml.Append("<td>" + item.ID + "</td>");
                        ohtml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</td>");
                        ohtml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.AmountDeposit)) + " VNĐ</td>");
                        ohtml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</td>");
                        ohtml.Append("<td class=\"no-wrap\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                        if (item.OrderType == 3)
                        {
                            if (item.IsCheckNotiPrice == false)
                            {
                                ohtml.Append("<td><span class=\"badge green darken-2 white-text border-radius-2\">Chờ báo giá</span></td>");
                            }
                            else
                            {
                                ohtml.Append("<td>" + PJUtils.IntToRequestAdminNew(Convert.ToInt32(item.Status)) + "</td>");
                            }
                        }
                        else
                        {
                            ohtml.Append("<td>" + PJUtils.IntToRequestAdminNew(Convert.ToInt32(item.Status)) + "</td>");
                        }
                        ohtml.Append("<td class=\"tb-date\"><div class=\"action-table\"><a href=\"/chi-tiet-don-hang/" + item.ID + "\"><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a></div></td>");
                        ohtml.Append("</tr>");
                    }
                }


                var os1 = MainOrderController.GetAllByUIDAndOrderType(obj_user.ID, 3).OrderByDescending(x => x.ID).ToList();
                ltrOrderOther.Text = os1.Count().ToString();
                if (os1.Count > 0)
                {
                    var os6 = os1.Take(6).ToList();
                    foreach (var item in os6)
                    {
                        ottml.Append("<tr>");
                        ottml.Append("<td>" + item.ID + "</td>");
                        ottml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + " VNĐ</td>");
                        ottml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.AmountDeposit)) + " VNĐ</td>");
                        ottml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + " VNĐ</td>");
                        ottml.Append("<td class=\"no-wrap\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                        if (item.OrderType == 3)
                        {
                            if (item.IsCheckNotiPrice == false)
                            {
                                ottml.Append("<td><span class=\"badge green darken-2 white-text border-radius-2\">Chờ báo giá</span></td>");
                            }
                            else
                            {
                                ottml.Append("<td>" + PJUtils.IntToRequestAdminNew(Convert.ToInt32(item.Status)) + "</td>");
                            }
                        }
                        else
                        {
                            ottml.Append("<td>" + PJUtils.IntToRequestAdminNew(Convert.ToInt32(item.Status)) + "</td>");
                        }
                        ottml.Append("<td class=\"tb-date\"><div class=\"action-table\"><a href=\"/chi-tiet-don-hang/" + item.ID + "\"><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a></div></td>");
                        ottml.Append("</tr>");
                    }
                }


                var ts = TransportationOrderNewController.GetAllByID(obj_user.ID);
                ltrTotalTran.Text = ts.Count().ToString();
                if (ts.Count > 0)
                {
                    var ts6 = ts.Take(6).ToList();
                    foreach (var item in ts6)
                    {

                        thtml.Append("<tr>");
                        thtml.Append("<td>" + item.ID + "</td>");
                        thtml.Append("<td>" + item.BarCode + "</td>");
                        thtml.Append("<td>" + item.Weight + " Kg</td>");
                       
                        thtml.Append("<td>" + PJUtils.GeneralTransportationOrderNewStatus(Convert.ToInt32(item.Status)) + "</td>");
                        thtml.Append("<td class=\"no-wrap\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                        thtml.Append("<td class=\"tb-date\"><div class=\"action-table\"><a href=\"/danh-sach-kien-yeu-cau-ky-gui\" ><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a></div></td>");
                        thtml.Append("</tr>");
                    }
                }

                var listpa = PayhelpController.GetAllUID(obj_user.ID);
                ltrPayhelp.Text = listpa.Count().ToString();
                if (listpa.Count > 0)
                {
                    var listpa6 = listpa.Take(6).ToList();
                    foreach (var item in listpa6)
                    {
                        phtml.Append("<tr>");
                        phtml.Append("<td>" + item.ID + "</td>");
                        phtml.Append("<td class=\"no-wrap\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>");
                        phtml.Append("<td>" + string.Format("{0:N0}", item.TotalPrice).Replace(",", ".") + " ¥</td>");
                        phtml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)).Replace(",", ".") + " VNĐ</td>");
                        phtml.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.Currency)).Replace(",", ".") + "</td>");
                        bool isNotComplete = false;
                        if (item.IsNotComplete != null)
                            isNotComplete = Convert.ToBoolean(item.IsNotComplete);
                        if (isNotComplete == true)
                        {
                            phtml.Append("<td><span class=\"badge red darken-2 white-text border-radius-2\">Đang hoàn thiện</span></td>");
                        }
                        else
                        {
                            phtml.Append("<td>" + PJUtils.ReturnStatusPayHelpNew(Convert.ToInt32(item.Status)) + "</td>");
                        }
                        phtml.Append("<td class=\"tb-date\"><div class=\"action-table\"><a href=\"/chi-tiet-thanh-toan-ho/" + item.ID + "\" ><i class=\"material-icons\">remove_red_eye</i><span>Chi tiết</span></a></div></td>");
                        phtml.Append("</tr>");
                    }
                }
                ltrListOther.Text = ottml.ToString();
                ltrListOrder.Text = ohtml.ToString();
                ltrListTran.Text = thtml.ToString();
                ltrListPayHelp.Text = phtml.ToString();
            }
        }

        [WebMethod]
        public static void Notshow(string data)
        {
            if (data == "0")
            {
                HttpContext.Current.Session["notshowEx"] = "1";
            }
            else
            {
                HttpContext.Current.Session["notshowEx"] = "0";
            }

        }

        public static string InsertDevicetoken(string PushEndpoint, string PushP256DH, string PushAuth)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                var aclog = AccountController.GetByID(int.Parse(username.ToString()));
                var dv = DeviceBrowserTable.insert(aclog.ID, PushEndpoint, PushP256DH, PushAuth, aclog.Username);
            }
            return null;
        }
    }
}
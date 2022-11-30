using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;
namespace NHST.manager
{
    public partial class Report_order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 7 && obj_user.RoleID != 2)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
                //LoadGrid1();
            }
        }
        public void LoadData()
        {
            var la5 = MainOrderController.GetByUserInViewFilterStatus5();
            if (la5.Count > 0)
            {
                int i = 1;
                double totalprice = 0;
                double mustpay = 0;
                foreach (var item in la5)
                {
                    Danhsachorder d = new Danhsachorder();
                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
                    double deposit = 0;
                    if (!string.IsNullOrEmpty(item.Deposit))
                        deposit = Convert.ToDouble(item.Deposit);
                    double totalleft = totalpricevnd - deposit;
                    totalprice += totalpricevnd;
                    mustpay += totalleft;
                    i++;
                }
                StringBuilder html = new StringBuilder();
                html.Append("<tr>");
                html.Append("    <td>Tổng đơn đã mua</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", la5.Count) + " đơn hàng</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền hàng đã mua</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền khách cần thanh toán</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span></td>");
                html.Append("</tr>");
                ltrDaMuaHang.Text = html.ToString();
            }

            var la6 = MainOrderController.GetByUserInViewFilterStatus6();
            if (la6.Count > 0)
            {
                int i = 1;
                double totalprice = 0;
                double mustpay = 0;
                foreach (var item in la6)
                {
                    Danhsachorder d = new Danhsachorder();
                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
                    double deposit = 0;
                    if (!string.IsNullOrEmpty(item.Deposit))
                        deposit = Convert.ToDouble(item.Deposit);
                    double totalleft = totalpricevnd - deposit;
                    totalprice += totalpricevnd;
                    mustpay += totalleft;
                    i++;
                }
                StringBuilder html = new StringBuilder();
                html.Append("<tr>");
                html.Append("    <td>Tổng đơn đã về kho TQ</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", la6.Count) + " đơn hàng</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền hàng đã mua</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền khách cần thanh toán</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span></td>");
                html.Append("</tr>");
                ltrDaVeKhoTQ.Text = html.ToString();
            }
            var la7 = MainOrderController.GetByUserInViewFilterStatus7();
            if (la7.Count > 0)
            {
                int i = 1;
                double totalprice = 0;
                double mustpay = 0;
                foreach (var item in la7)
                {
                    Danhsachorder d = new Danhsachorder();
                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
                    double deposit = 0;
                    if (!string.IsNullOrEmpty(item.Deposit))
                        deposit = Convert.ToDouble(item.Deposit);
                    double totalleft = totalpricevnd - deposit;
                    totalprice += totalpricevnd;
                    mustpay += totalleft;
                    i++;
                }
                StringBuilder html = new StringBuilder();
                html.Append("<tr>");
                html.Append("    <td>Tổng đơn đã về kho VN</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", la7.Count) + " đơn hàng</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền hàng đã mua</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền khách cần thanh toán</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span></td>");
                html.Append("</tr>");
                ltrDaVeKhoVN.Text = html.ToString();
            }
            var la9 = MainOrderController.GetByUserInViewFilterStatus(9);
            if (la9.Count > 0)
            {
                int i = 1;
                double totalprice = 0;
                double mustpay = 0;
                foreach (var item in la9)
                {
                    Danhsachorder d = new Danhsachorder();
                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
                    double deposit = 0;
                    if (!string.IsNullOrEmpty(item.Deposit))
                        deposit = Convert.ToDouble(item.Deposit);
                    double totalleft = totalpricevnd - deposit;
                    totalprice += totalpricevnd;
                    mustpay += totalleft;
                    i++;
                }
                StringBuilder html = new StringBuilder();
                html.Append("<tr>");
                html.Append("    <td>Tổng đơn đã thanh toán</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", la9.Count) + " đơn hàng</span></td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("    <td>Tổng tiền đã thanh toán</td>");
                html.Append("    <td><span class=\"teal-text text-darken-4 font-weight-700\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span></td>");
                html.Append("</tr>");
                ltrDaThanhToan.Text = html.ToString();
            }
        }
        //protected void btnFilter_Click(object sender, EventArgs e)
        //{
        //    string username_current = Session["userLoginSystem"].ToString();
        //    tbl_Account ac = AccountController.GetByUsername(username_current);
        //    if (ac != null)
        //    {
        //        int status = ddlFilter.SelectedValue.ToInt();

        //        if (status == 5)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus5();
        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {

        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;

        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn đã mua</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng đã mua</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";


        //            }
        //            gr.DataSource = ds;
        //            gr.DataBind();
        //        }
        //        else if (status == 6)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus6();

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;

        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn ở kho TQ</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng ở kho TQ</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            gr.DataBind();
        //        }
        //        else if (status == 7)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus7();

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;
        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn ở kho đích</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng ở kho đích</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            gr.DataBind();
        //        }
        //        else if (status == 9)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus(9);

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;
        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += deposit;
        //                    //mustpay += totalleft;
        //                    i++;
        //                }
        //                ltrinf.Text = "";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn đã thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền đã thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                //ltrinf.Text += "<div class=\"row\">";
        //                //ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                //ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                //ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                //ltrinf.Text += "</div>";
        //                //ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            gr.DataBind();
        //        }
        //    }
        //}

        //public void LoadGrid()
        //{
        //    string username_current = Session["userLoginSystem"].ToString();
        //    tbl_Account ac = AccountController.GetByUsername(username_current);
        //    if (ac != null)
        //    {
        //        int status = ddlFilter.SelectedValue.ToInt();

        //        if (status == 5)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus5();
        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {

        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;

        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn đã mua</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng đã mua</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";


        //            }
        //            gr.DataSource = ds;
        //            //gr.DataBind();
        //        }
        //        else if (status == 6)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus6();

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;

        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn ở kho TQ</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng ở kho TQ</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            //gr.DataBind();
        //        }
        //        else if (status == 7)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus7();

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;
        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += totalpricevnd;
        //                    mustpay += totalleft;
        //                    i++;
        //                }

        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn ở kho đích</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền hàng ở kho đích</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            //gr.DataBind();
        //        }
        //        else if (status == 9)
        //        {
        //            var la = MainOrderController.GetByUserInViewFilterStatus(9);

        //            List<Danhsachorder> ds = new List<Danhsachorder>();
        //            ltrinf.Text = "";
        //            if (la.Count > 0)
        //            {
        //                int i = 1;
        //                double totalprice = 0;
        //                double mustpay = 0;
        //                foreach (var item in la)
        //                {
        //                    Danhsachorder d = new Danhsachorder();
        //                    double totalpricevnd = Convert.ToDouble(item.TotalPriceVND);
        //                    double deposit = 0;
        //                    if (!string.IsNullOrEmpty(item.Deposit))
        //                        deposit = Convert.ToDouble(item.Deposit);
        //                    double totalleft = totalpricevnd - deposit;
        //                    d.ID = item.ID;
        //                    d.STT = i;
        //                    d.ProductImage = item.anhsanpham;
        //                    d.TotalPriceVND = item.TotalPriceVND;
        //                    d.Deposit = item.Deposit;
        //                    d.CreatedDate = item.CreatedDate.ToString();
        //                    d.username = item.Uname;
        //                    d.kinhdoanh = item.saler;
        //                    d.dathang = item.dathang;
        //                    d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //                    ds.Add(d);
        //                    totalprice += deposit;
        //                    //mustpay += totalleft;
        //                    i++;
        //                }
        //                ltrinf.Text = "";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng đơn đã thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", la.Count) + " đơn hàng</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "<div class=\"row\">";
        //                ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                ltrinf.Text += "<span class=\"label-title\">Tổng tiền đã thanh toán</span>";
        //                ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalprice) + " VNĐ</span>";
        //                ltrinf.Text += "</div>";
        //                ltrinf.Text += "</div>";
        //                //ltrinf.Text += "<div class=\"row\">";
        //                //ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
        //                //ltrinf.Text += "<span class=\"label-title\">Tổng khách cần thanh toán</span>";
        //                //ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", mustpay) + " VNĐ</span>";
        //                //ltrinf.Text += "</div>";
        //                //ltrinf.Text += "</div>";

        //            }
        //            gr.DataSource = ds;
        //            //gr.DataBind();
        //        }
        //    }
        //}

        //#region grid event
        //protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    LoadGrid();
        //}

        //protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        //{

        //}

        //protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    LoadGrid();
        //    //gr.Rebind();
        //}
        //protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        //{
        //    LoadGrid();
        //    //gr.Rebind();
        //}
        //#endregion
        public class UserToExcel
        {
            public int ID { get; set; }
            public string UserName { get; set; }
            public string Ho { get; set; }
            public string Ten { get; set; }
            public string Sodt { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public int RoleID { get; set; }
            public string Saler { get; set; }
            public string dathang { get; set; }
            public string wallet { get; set; }
            public string CreatedDate { get; set; }
        }
        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public int STT { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
            public string dathang { get; set; }
            public string kinhdoanh { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }
    }
}
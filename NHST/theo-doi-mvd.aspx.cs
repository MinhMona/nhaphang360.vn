using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;

namespace NHST
{
    public partial class theo_doi_mvd : System.Web.UI.Page
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
            string mvd = Request.QueryString["mvd"];
            if (!string.IsNullOrEmpty(mvd))
            {
                var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
                if (smallpackage != null)
                {
                    int mID = 0;
                    int tID = 0;
                    if (smallpackage.MainOrderID != null)
                    {
                        if (smallpackage.MainOrderID > 0)
                        {
                            mID = Convert.ToInt32(smallpackage.MainOrderID);
                        }
                        else if (smallpackage.TransportationOrderID != null)
                        {
                            if (smallpackage.TransportationOrderID > 0)
                            {
                                tID = Convert.ToInt32(smallpackage.TransportationOrderID);
                            }
                        }
                    }
                    else if (smallpackage.TransportationOrderID != null)
                    {
                        if (smallpackage.TransportationOrderID > 0)
                        {
                            tID = Convert.ToInt32(smallpackage.TransportationOrderID);
                        }
                    }
                    string ordertype = "Chưa xác định";
                    if (tID > 0)
                    {
                        ordertype = "Đơn hàng vận chuyển hộ";
                    }
                    else if (mID > 0)
                    {
                        ordertype = "Đơn hàng mua hộ";
                    }
                    ltrSmallpackageInfo.Text = "";
                    ltrSmallpackageInfo.Text += "<table>";
                    ltrSmallpackageInfo.Text += "   <tbody>";
                    ltrSmallpackageInfo.Text += "       <tr>";
                    ltrSmallpackageInfo.Text += "           <th style=\"width:50%\">Mã vận đơn:</th>";
                    ltrSmallpackageInfo.Text += "           <td class=\"m-color\">" + mvd + "</td>";
                    ltrSmallpackageInfo.Text += "       </tr>";
                    ltrSmallpackageInfo.Text += "       <tr>";
                    ltrSmallpackageInfo.Text += "           <th style=\"width:50%\">ID đơn hàng:</th>";
                    if (mID > 0)
                        ltrSmallpackageInfo.Text += "           <td class=\"m-color\">" + mID + "</td>";
                    else if (tID > 0)
                        ltrSmallpackageInfo.Text += "           <td class=\"m-color\">" + tID + "</td>";
                    else
                        ltrSmallpackageInfo.Text += "           <td class=\"m-color\">Chưa xác định</td>";
                    ltrSmallpackageInfo.Text += "       </tr>";
                    ltrSmallpackageInfo.Text += "       <tr>";
                    ltrSmallpackageInfo.Text += "           <th style=\"width:50%\">Loại đơn hàng:</th>";
                    ltrSmallpackageInfo.Text += "           <td class=\"m-color\">" + ordertype + "</td>";
                    ltrSmallpackageInfo.Text += "       </tr>";
                    ltrSmallpackageInfo.Text += "   </tbody>";
                    ltrSmallpackageInfo.Text += "</table>";
                    ltrTrack.Text += "";
                    if (smallpackage.Status == 0)
                    {
                        ltrTrack.Text += "<li class=\"clear\">";
                        ltrTrack.Text += "  Mã vận đơn đã bị hủy";
                        ltrTrack.Text += "</li>";
                        //ltrTrack.Text += "<li class=\"it clear\">";
                        //ltrTrack.Text += "  <div class=\"date-time grey89\"><p>03/01/2019 00:00:00</p></div>";
                        //ltrTrack.Text += "  <div class=\"statuss ok\">";
                        //ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        //ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho TQ</span></p>";
                        //ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Người nhận:</span> <span class=\"m-color\">vominhthienhanoi</span></p>";
                        //ltrTrack.Text += "  </div>";
                        //ltrTrack.Text += "</li>";
                    }
                    else if (smallpackage.Status == 1)
                    {
                        ltrTrack.Text += "<li class=\"clear\">";
                        ltrTrack.Text += "  Mã vận đơn chưa về kho TQ";
                        ltrTrack.Text += "</li>";
                    }
                    else if (smallpackage.Status == 2)
                    {
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateInTQWarehouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho TQ</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffTQWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                    }
                    else if (smallpackage.Status == 3)
                    {
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateInTQWarehouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho TQ</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffTQWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateInLasteWareHouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho đích</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffVNWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                    }
                    else if (smallpackage.Status == 4)
                    {
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateInTQWarehouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho TQ</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffTQWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateInLasteWareHouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã về kho đích</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffVNWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                        ltrTrack.Text += "<li class=\"it clear\">";
                        if (smallpackage.DateOutWarehouse != null)
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateOutWarehouse) + "</p></div>";
                        else
                            ltrTrack.Text += "  <div class=\"date-time grey89\"><p>Chưa xác định</p></div>";
                        ltrTrack.Text += "  <div class=\"statuss ok\">";
                        ltrTrack.Text += "      <i class=\"ico fa fa-check\"></i>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">Trạng thái:</span><span class=\"m-color\"> Đã giao khách</span></p>";
                        ltrTrack.Text += "      <p class=\"tit\"><span class=\"grey89\">NV Xử lý:</span> <span class=\"m-color\">" + smallpackage.StaffVNOutWarehouse + "</span></p>";
                        ltrTrack.Text += "  </div>";
                        ltrTrack.Text += "</li>";
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Mã vận đơn không tồn tại", "e", true, Page);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string mvd = tSearchName.Text.Trim();
            if (!string.IsNullOrEmpty(mvd))
            {
                var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
                if (smallpackage != null)
                {
                    Response.Redirect("/theo-doi-mvd?mvd=" + mvd + "");
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Mã vận đơn không tồn tại", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập mã vận đơn", "e", true, Page);
            }
        }
    }
}
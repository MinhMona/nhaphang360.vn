using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Tracking_app : System.Web.UI.Page
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
                    StringBuilder html = new StringBuilder();
                    ltrTitle.Text = "TRẠNG THÁI ĐƠN HÀNG";
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

                    html.Append("<div class=\"smr\">");
                    html.Append(" <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">Mã vận đơn: </p>");
                    html.Append("   <p>" + mvd + "</p>");
                    html.Append("  </div>");
                    html.Append("  <div class=\"flex-justify-space\">");
                    html.Append("   <p class=\"gray-txt\">ID đơn hàng:</p>");
                    if (mID > 0)
                        html.Append("   <p>" + mID + " </p>");
                    else if (tID > 0)
                        html.Append("   <p>" + tID + " </p>");
                    else
                        html.Append("   <p>Chưa xác định </p>");


                    html.Append("  </div>");
                    html.Append("  <div class=\"flex-justify-space\">");
                    html.Append("  <p class=\"gray-txt\">Loại đơn hàng:</p>");
                    html.Append("  <p>" + ordertype + "</p>");
                    html.Append(" </div>");
                    html.Append("  </div>");


                    html.Append(" <div class=\"smr\">");
                    html.Append("  <div class=\"tracking-wrap\">");
                    html.Append(" <div class=\"tk-heading\">");
                    html.Append("<div class=\"tk-left\">");
                    html.Append("  <p class=\"tk-title\">Ngày</p>");
                    html.Append(" </div>");
                    html.Append("  <div class=\"tk-right\">");
                    html.Append(" <p class=\"tk-title\">Trạng thái</p>");
                    html.Append(" </div>");
                    html.Append("  </div>");


                    if (smallpackage.Status == 0)
                    {
                        html.Append("  <div class=\"tk-row current\">");
                        html.Append("  <div class=\"tk-left\"> </div>");
                        html.Append("   <div class=\"tk-right\">");
                        html.Append("     Mã vận đơn đã bị hủy<br>");
                        html.Append("   </div>");
                        html.Append("  </div>");
                    }
                    else if (smallpackage.Status == 1)
                    {
                        html.Append("  <div class=\"tk-row current\">");
                        html.Append("  <div class=\"tk-left\"> </div>");
                        html.Append("   <div class=\"tk-right\">");
                        html.Append("     Mã vận đơn chưa về kho TQ<br>");
                        html.Append("   </div>");
                        html.Append("  </div>");
                    }
                    else if (smallpackage.Status == 2)
                    {
                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateInTQWarehouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã về kho Trung Quốc<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffTQWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");
                    }
                    else if (smallpackage.Status == 3)
                    {
                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateInTQWarehouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã về kho Trung Quốc<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffTQWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");

                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateInLasteWareHouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã về kho đích<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffVNWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");
                    }
                    else if (smallpackage.Status == 4)
                    {
                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateInTQWarehouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã về kho Trung Quốc<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffTQWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");

                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateInLasteWareHouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã về kho đích<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffVNWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");

                        html.Append("<div class=\"tk-row current\">");
                        html.Append(" <div class=\"tk-left\">");
                        if (smallpackage.DateOutWarehouse != null)
                            html.Append("" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateOutWarehouse) + " ");
                        else
                            html.Append("Chưa xác định");
                        html.Append("</div>");
                        html.Append("  <div class=\"tk-right\"> Đã giao khách<br>");
                        html.Append(" NV Xử lý: <span class=\"hl-txt\">" + smallpackage.StaffVNOutWarehouse + "</span>");
                        html.Append(" </div>");
                        html.Append(" </div>");
                    }

                    html.Append(" </div>");
                    html.Append("  </div>");

                    ltrTrack.Text = html.ToString();
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
                    Response.Redirect("/tracking-app.aspx?mvd=" + mvd + "");
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
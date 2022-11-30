using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Bussiness;
using NHST.Controllers;


namespace NHST
{
    public partial class tracking_mvd1 : System.Web.UI.Page
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
                tSearchName.Text = mvd;
                var smallpackage = SmallPackageController.GetByOrderTransactionCode(mvd);
                if (smallpackage != null)
                {
                    string html = "";
                    html += "<div class=\"tracking-wrap mt-2\">";
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



                    if (smallpackage.Status == 0)
                    {
                        html += "<div class=\"track-hd\" style=\"background:black;\">";
                        html += "<div class=\"track-number\">";
                        html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-main\" style=\"background:#99a398;\">";
                        html += "<div class=\"track-main-flex\">";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>" + ordertype + ":</strong>";
                        if (mID > 0)
                            html += "<span>" + mID + "</span></p>";
                        else if (tID > 0)
                            html += "<span>" + tID + "</span></p>";
                        else
                            html += "<span>Chưa xác định</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.CancelDate) + "</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Trạng thái:</strong><span>Mã vận đơn đã bị hủy</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Nhân viên xử lý:</strong><span>" + smallpackage.StaffCancel + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                    }
                    else if (smallpackage.Status == 1)
                    {
                        html += "<div class=\"track-hd\">";
                        html += "<div class=\"track-number\">";
                        html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-main\">";
                        html += "<div class=\"track-main-flex\">";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>" + ordertype + ":</strong>";
                        if (mID > 0)
                            html += "<span>" + mID + "</span></p>";
                        else if (tID > 0)
                            html += "<span>" + tID + "</span></p>";
                        else
                            html += "<span>Chưa xác định</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.CreatedDate) + "</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Trạng thái:</strong><span>Mã vận đơn vừa được tạo - chưa về kho TQ</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Nhân viên xử lý:</strong><span>" + smallpackage.CreatedBy + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-bottom hide-on-small-and-down\">";
                        html += "<div class=\"process\">";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">store</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang lấy hàng</p>";
                        html += "</div>";
                        html += "<div class=\"process-step\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">view_compact</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho TQ</p>";
                        html += "</div>";
                        html += "<div class=\"process-step\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">flight</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang vận chuyển</p>";
                        html += "</div>";
                        html += "<div class=\"process-step\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">storage</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho VN</p>";
                        html += "</div>";
                        html += "<div class=\"process-step\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">local_shipping</i>";
                        html += "</div>";
                        html += "<p>Đã giao hàng</p>";
                        html += "</div>";
                        html += "<div class=\"clearfix\"></div>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                    }
                    else if (smallpackage.Status == 2)
                    {
                        if (smallpackage.BigPackageID > 0)
                        {
                            var big = BigPackageController.GetByID(smallpackage.BigPackageID.Value);
                            if (big != null)
                            {
                                html += "<div class=\"track-hd\">";
                                html += "<div class=\"track-number\">";
                                html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                                html += "</div>";
                                html += "</div>";
                                html += "<div class=\"track-main\">";
                                html += "<div class=\"track-main-flex\">";
                                html += "<div class=\"track-item\">";
                                html += "<p><strong>" + ordertype + ":</strong>";
                                if (mID > 0)
                                    html += "<span>" + mID + "</span></p>";
                                else if (tID > 0)
                                    html += "<span>" + tID + "</span></p>";
                                else
                                    html += "<span>Chưa xác định</span></p>";
                                html += "</div>";
                                html += "<div class=\"track-item\">";
                                html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", big.CreatedDate) + "</span></p>";
                                html += "</div>";
                                html += "<div class=\"track-item\">";
                                html += "<p><strong>Trạng thái:</strong><span>Đang vận chuyển</span></p>";
                                html += "</div>";
                                html += "<div class=\"track-item\">";
                                html += "<p><strong>Nhân viên xử lý:</strong><span>" + big.CreatedBy + "</span></p>";
                                html += "</div>";
                                html += "</div>";
                                html += "</div>";
                                html += "<div class=\"track-bottom hide-on-small-and-down\">";
                                html += "<div class=\"process\">";
                                html += "<div class=\"process-step active\">";
                                html += "<div class=\"img-circle circle\">";
                                html += "<i class=\"material-icons icons\">store</i>";
                                html += "</div>";
                                html += "<span class=\"line\"></span>";
                                html += "<p>Đang lấy hàng</p>";
                                html += "</div>";
                                html += "<div class=\"process-step active\">";
                                html += "<div class=\"img-circle circle\">";
                                html += "<i class=\"material-icons icons\">view_compact</i>";
                                html += "</div>";
                                html += "<span class=\"line\"></span>";
                                html += "<p>Đã về kho TQ</p>";
                                html += "</div>";
                                html += "<div class=\"process-step active\">";
                                html += "<div class=\"img-circle circle\">";
                                html += "<i class=\"material-icons icons\">flight</i>";
                                html += "</div>";
                                html += "<span class=\"line\"></span>";
                                html += "<p>Đang vận chuyển</p>";
                                html += "</div>";
                                html += "<div class=\"process-step\">";
                                html += "<div class=\"img-circle circle\">";
                                html += "<i class=\"material-icons icons\">storage</i>";
                                html += "</div>";
                                html += "<span class=\"line\"></span>";
                                html += "<p>Đã về kho VN</p>";
                                html += "</div>";
                                html += "<div class=\"process-step\">";
                                html += "<div class=\"img-circle circle\">";
                                html += "<i class=\"material-icons icons\">local_shipping</i>";
                                html += "</div>";
                                html += "<p>Đã giao hàng</p>";
                                html += "</div>";
                                html += "<div class=\"clearfix\"></div>";
                                html += "</div>";
                                html += "</div>";
                                html += "</div>";
                            }
                        }
                        else
                        {
                            html += "<div class=\"track-hd\">";
                            html += "<div class=\"track-number\">";
                            html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                            html += "</div>";
                            html += "</div>";
                            html += "<div class=\"track-main\">";
                            html += "<div class=\"track-main-flex\">";
                            html += "<div class=\"track-item\">";
                            html += "<p><strong>" + ordertype + ":</strong>";
                            if (mID > 0)
                                html += "<span>" + mID + "</span></p>";
                            else if (tID > 0)
                                html += "<span>" + tID + "</span></p>";
                            else
                                html += "<span>Chưa xác định</span></p>";
                            html += "</div>";
                            html += "<div class=\"track-item\">";
                            html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInTQWarehouse) + "</span></p>";
                            html += "</div>";
                            html += "<div class=\"track-item\">";
                            html += "<p><strong>Trạng thái:</strong><span>Đã về kho TQ</span></p>";
                            html += "</div>";
                            html += "<div class=\"track-item\">";
                            html += "<p><strong>Nhân viên xử lý:</strong><span>" + smallpackage.StaffTQWarehouse + "</span></p>";
                            html += "</div>";
                            html += "</div>";
                            html += "</div>";
                            html += "<div class=\"track-bottom hide-on-small-and-down\">";
                            html += "<div class=\"process\">";
                            html += "<div class=\"process-step active\">";
                            html += "<div class=\"img-circle circle\">";
                            html += "<i class=\"material-icons icons\">store</i>";
                            html += "</div>";
                            html += "<span class=\"line\"></span>";
                            html += "<p>Đang lấy hàng</p>";
                            html += "</div>";
                            html += "<div class=\"process-step active\">";
                            html += "<div class=\"img-circle circle\">";
                            html += "<i class=\"material-icons icons\">view_compact</i>";
                            html += "</div>";
                            html += "<span class=\"line\"></span>";
                            html += "<p>Đã về kho TQ</p>";
                            html += "</div>";
                            html += "<div class=\"process-step\">";
                            html += "<div class=\"img-circle circle\">";
                            html += "<i class=\"material-icons icons\">flight</i>";
                            html += "</div>";
                            html += "<span class=\"line\"></span>";
                            html += "<p>Đang vận chuyển</p>";
                            html += "</div>";
                            html += "<div class=\"process-step\">";
                            html += "<div class=\"img-circle circle\">";
                            html += "<i class=\"material-icons icons\">storage</i>";
                            html += "</div>";
                            html += "<span class=\"line\"></span>";
                            html += "<p>Đã về kho VN</p>";
                            html += "</div>";
                            html += "<div class=\"process-step\">";
                            html += "<div class=\"img-circle circle\">";
                            html += "<i class=\"material-icons icons\">local_shipping</i>";
                            html += "</div>";
                            html += "<p>Đã giao hàng</p>";
                            html += "</div>";
                            html += "<div class=\"clearfix\"></div>";
                            html += "</div>";
                            html += "</div>";
                            html += "</div>";
                        }
                    }
                    else if (smallpackage.Status == 3)
                    {
                        html += "<div class=\"track-hd\">";
                        html += "<div class=\"track-number\">";
                        html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-main\">";
                        html += "<div class=\"track-main-flex\">";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>" + ordertype + ":</strong>";
                        if (mID > 0)
                            html += "<span>" + mID + "</span></p>";
                        else if (tID > 0)
                            html += "<span>" + tID + "</span></p>";
                        else
                            html += "<span>Chưa xác định</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateInLasteWareHouse) + "</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Trạng thái:</strong><span>Đã về kho TQ</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Nhân viên xử lý:</strong><span>" + smallpackage.StaffVNWarehouse + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-bottom hide-on-small-and-down\">";
                        html += "<div class=\"process\">";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">store</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang lấy hàng</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">view_compact</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho TQ</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">flight</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang vận chuyển</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">storage</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho VN</p>";
                        html += "</div>";
                        html += "<div class=\"process-step\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">local_shipping</i>";
                        html += "</div>";
                        html += "<p>Đã giao hàng</p>";
                        html += "</div>";
                        html += "<div class=\"clearfix\"></div>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                    }
                    else if (smallpackage.Status == 4)
                    {
                        html += "<div class=\"track-hd\">";
                        html += "<div class=\"track-number\">";
                        html += "<p class=\"center-align white-text\"><span>Mã vận đơn:</span><span>" + mvd + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-main\">";
                        html += "<div class=\"track-main-flex\">";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>" + ordertype + ":</strong>";
                        if (mID > 0)
                            html += "<span>" + mID + "</span></p>";
                        else if (tID > 0)
                            html += "<span>" + tID + "</span></p>";
                        else
                            html += "<span>Chưa xác định</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Thời gian:</strong><span>" + string.Format("{0:dd/MM/yyyy HH:mm}", smallpackage.DateOutWarehouse) + "</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Trạng thái:</strong><span>Đã về kho TQ</span></p>";
                        html += "</div>";
                        html += "<div class=\"track-item\">";
                        html += "<p><strong>Nhân viên xử lý:</strong><span>" + smallpackage.StaffVNOutWarehouse + "</span></p>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                        html += "<div class=\"track-bottom hide-on-small-and-down\">";
                        html += "<div class=\"process\">";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">store</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang lấy hàng</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">view_compact</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho TQ</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">flight</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đang vận chuyển</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">storage</i>";
                        html += "</div>";
                        html += "<span class=\"line\"></span>";
                        html += "<p>Đã về kho VN</p>";
                        html += "</div>";
                        html += "<div class=\"process-step active\">";
                        html += "<div class=\"img-circle circle\">";
                        html += "<i class=\"material-icons icons\">local_shipping</i>";
                        html += "</div>";
                        html += "<p>Đã giao hàng</p>";
                        html += "</div>";
                        html += "<div class=\"clearfix\"></div>";
                        html += "</div>";
                        html += "</div>";
                        html += "</div>";
                    }
                    html += "</div>";
                    ltrTracking.Text = html;
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
                    Response.Redirect("/tracking-mvd?mvd=" + mvd + "");
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
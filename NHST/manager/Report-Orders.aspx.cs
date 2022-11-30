using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class Report_Orders : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
            {
                rdatefrom.Text = fd;
            }
            if (!string.IsNullOrEmpty(td))
            {
                rdateto.Text = td;
            }


            var la = MainOrderController.GetAllDateToDate(fd, td, page, 10);
            var total = MainOrderController.GetTotalDateToDate(fd, td, "");

            if (la.Count > 0)
            {
                int NotDeposit = MainOrderController.GetTotalDateToDate(fd, td, "0");
                int OrderCancel = MainOrderController.GetTotalDateToDate(fd, td, "1");
                int OrderDeposit = MainOrderController.GetTotalDateToDate(fd, td, "2");
                int Waiting = MainOrderController.GetTotalDateToDate(fd, td, "3");
                int OrderChecked = MainOrderController.GetTotalDateToDate(fd, td, "4");
                int OrderIsOrder = MainOrderController.GetTotalDateToDate(fd, td, "5");
                int OrderTQ = MainOrderController.GetTotalDateToDate(fd, td, "6");
                int OrderVN = MainOrderController.GetTotalDateToDate(fd, td, "7");
                int OrderWaitingPayment = MainOrderController.GetTotalDateToDate(fd, td, "8");
                int OrderPaid = MainOrderController.GetTotalDateToDate(fd, td, "9");
                int OrderSuccess = MainOrderController.GetTotalDateToDate(fd, td, "10");
                int OrderDone = OrderSuccess + OrderPaid;

                int[] dataTemp = new int[] { NotDeposit, OrderCancel, OrderDeposit, Waiting, OrderChecked, OrderIsOrder, OrderTQ, OrderVN, OrderWaitingPayment, OrderDone };

                string datasetsTotal = new JavaScriptSerializer().Serialize(new
                {
                    label = "Số lượng",
                    data = dataTemp,
                    fill = false,
                    lineTension = 0,
                    borderColor = "#37474f",
                    pointBorderColor = "#fff",
                    pointBackgroundColor = "#009688",
                    pointHighlightFill = "#000",
                    pointHoverBackgroundColor = "#000",
                    borderWidth = 4,
                    pointBorderWidth = 1,
                    pointHoverBorderWidth = 4,
                    pointRadius = 4
                });
                string rateConvertChartData = new JavaScriptSerializer().Serialize(
                                               new
                                               {
                                                   labels = "[\"Chờ đặt cọc\", \"Hủy đơn hàng\", \"Đã đặt cọc\",\"Chờ duyệt đơn\",\"Đã duyệt đơn\", \"Đã mua hàng\", \"Đã về kho TQ\", \"Đã về kho VN\", \"Chờ thanh toán\", \"Đã hoàn thành\"]",
                                                   datasets = "[" + datasetsTotal + "]"
                                               });

                hdfChartStatus.Value = rateConvertChartData;
                double or_TotalPriceVND = MainOrderController.GetTotalPriceDateToDate(fd, td, "TotalPriceVND");
                double or_TotalRealPrice = MainOrderController.GetTotalPriceDateToDate(fd, td, "TotalPriceReal");
                double or_Deposit = MainOrderController.GetTotalPriceDateToDate(fd, td, "Deposit");
                double or_FeeShipCN = MainOrderController.GetTotalPriceDateToDate(fd, td, "FeeShipCN");
                double or_FeeBuyPro = MainOrderController.GetTotalPriceDateToDate(fd, td, "FeeBuyPro");
                double or_FeeWeight = MainOrderController.GetTotalPriceDateToDate(fd, td, "FeeWeight");

                double[] dataTemp2 = new double[] { or_Deposit, or_TotalRealPrice, or_FeeShipCN, or_FeeBuyPro, or_FeeWeight };
                string[] backgroundColor2 = new string[] { "rgb(153, 204, 255)", "rgb(255, 153, 255)", "rgb(255, 204, 153)", "rgb(153, 255, 153)", "rgb(255, 0, 102)" };
                string datasetsTotal2 = new JavaScriptSerializer().Serialize(new
                {
                    label = "Value",
                    data = dataTemp2,
                    backgroundColor = backgroundColor2,
                    borderColor = backgroundColor2,
                    borderWidth = 1
                });
                string dataChartTotal = new JavaScriptSerializer().Serialize(
                                               new
                                               {
                                                   labels = "[\"Tổng tiền nạp\", \"Tổng tiền thật\", \"Tổng tiền vận chuyển\",\"Tổng tiền mua hộ\",\"Tổng tiền cân nặng\"]",
                                                   datasets = "[" + datasetsTotal2 + "]"
                                               });
                lbTongTien.Text = string.Format("{0:N0}", or_TotalPriceVND);
                hdfDataChartTotal.Value = dataChartTotal;
                pagingall(la, total);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {

            string fd = "";
            string td = "";
            if (!string.IsNullOrEmpty(rdatefrom.Text))
            {
                fd = rdatefrom.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rdateto.Text))
            {
                td = rdateto.Text.ToString();
            }
            if (fd == "" && td == "")
            {
                Response.Redirect("Report-Orders.aspx");
            }
            else
            {
                Response.Redirect("Report-Orders.aspx?fd=" + fd + "&td=" + td);
            }

            //var acc = Session["userLoginSystem"].ToString();
            //var ListOrder = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text,"dd/MM/yyyy HH:mm",null), DateTime.ParseExact(rdateto.Text,"dd/MM/yyyy HH:mm", null));
            //ltrHistory.Text = "";

            //if (ListOrder.Count > 0)
            //{
            //    List<ReportOrder> ro = new List<ReportOrder>();
            //    double NotDeposit = 0;
            //    double OrderDeposit = 0;
            //    double Waiting = 0;
            //    double OrderChecked = 0;
            //    double OrderIsOrder = 0;
            //    double OrderTQ = 0;
            //    double OrderVN = 0;
            //    double OrderWaitingPayment = 0;
            //    double OrderSuccess = 0;
            //    double OrderCancel = 0;
            //    double OrderTotal = ListOrder.Count;

            //    foreach (var o in ListOrder)
            //    {
            //        double or_TotalRealPrice = 0;
            //        double or_TotalPriceVND = 0;
            //        double or_Deposit = 0;
            //        double or_IsFastPrice = 0;
            //        double or_FeeShipCN = 0;
            //        double or_FeeBuyPro = 0;
            //        double or_FeeWeight = 0;
            //        double or_IsCheckProductPrice = 0;
            //        double or_IsPackedPrice = 0;
            //        double or_IsFastDeliveryPrice = 0;

            //        if (o.TotalPriceReal.ToFloat(0) > 0)
            //            or_TotalRealPrice = Convert.ToDouble(o.TotalPriceReal);
            //        if (o.TotalPriceVND.ToFloat(0) > 0)
            //            or_TotalPriceVND = Convert.ToDouble(o.TotalPriceVND);
            //        if (o.Deposit.ToFloat(0) > 0)
            //            or_Deposit = Convert.ToDouble(o.Deposit);
            //        if (o.IsFastPrice.ToFloat(0) > 0)
            //            or_IsFastPrice = Convert.ToDouble(o.IsFastPrice);
            //        if (o.FeeShipCN.ToFloat(0) > 0)
            //            or_FeeShipCN = Convert.ToDouble(o.FeeShipCN);
            //        if (o.FeeBuyPro.ToFloat(0) > 0)
            //            or_FeeBuyPro = Convert.ToDouble(o.FeeBuyPro);
            //        if (o.FeeWeight.ToFloat(0) > 0)
            //            or_FeeWeight = Convert.ToDouble(o.FeeWeight);
            //        if (o.IsCheckProductPrice.ToFloat(0) > 0)
            //            or_IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);
            //        if (o.IsPackedPrice.ToFloat(0) > 0)
            //            or_IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);
            //        if (o.IsFastDeliveryPrice.ToFloat(0) > 0)
            //            or_IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);

            //        double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;

            //        int stt = Convert.ToInt32(o.Status);
            //        if (stt == 0)
            //        {
            //            NotDeposit += 1;
            //        }
            //        else if (stt == 1)
            //        {
            //            OrderCancel += 1;
            //        }
            //        else if (stt == 2)
            //        {
            //            OrderDeposit += 1;
            //        }
            //        else if (stt == 3)
            //        {
            //            Waiting += 1;
            //        }
            //        else if (stt == 4)
            //        {
            //            OrderChecked += 1;
            //        }
            //        else if (stt == 5)
            //        {
            //            OrderIsOrder += 1;
            //        }
            //        else if (stt == 6)
            //        {
            //            OrderTQ += 1;
            //        }
            //        else if (stt == 7)
            //        {
            //            OrderVN += 1;
            //        }
            //        else if (stt == 8)
            //        {
            //            OrderWaitingPayment += 1;
            //        }
            //        else
            //        {
            //            OrderSuccess += 1;
            //        }


            //        ReportOrder r = new ReportOrder();
            //        r.OrderID = o.ID;
            //        r.ShopID = o.ShopID;
            //        r.ShopName = o.ShopName;
            //        r.FullName = o.FullName;
            //        r.Email = o.Email;
            //        r.Phone = o.Phone;
            //        r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
            //        r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
            //        r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
            //        r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
            //        r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
            //        r.Package = string.Format("{0:N0}", or_IsPackedPrice);
            //        r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
            //        r.Total = string.Format("{0:N0}", or_TotalPriceVND);
            //        r.TotalRealPrice = string.Format("{0:N0}", or_TotalRealPrice);
            //        r.Deposit = string.Format("{0:N0}", or_Deposit);
            //        r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
            //        r.Status = PJUtils.IntToRequestAdmin(stt);
            //        r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
            //        ro.Add(r);
            //    }

            //    //lblNotDeposit.Text = string.Format("{0:N0}", NotDeposit);
            //    //lblOrderDeposit.Text = string.Format("{0:N0}", OrderDeposit);
            //    //lblWaiting.Text = string.Format("{0:N0}", Waiting);
            //    //lblOrderChecked.Text = string.Format("{0:N0}", OrderChecked);
            //    //lblOrderIsOrder.Text = string.Format("{0:N0}", OrderIsOrder);
            //    //lblOrderTQ.Text = string.Format("{0:N0}", OrderTQ);
            //    //lblOrderVN.Text = string.Format("{0:N0}", OrderVN);
            //    //lblOrderWaitingPayment.Text = string.Format("{0:N0}", OrderWaitingPayment);
            //    //lblOrderSuccess.Text = string.Format("{0:N0}", OrderSuccess);
            //    //lblOrderCancel.Text = string.Format("{0:N0}", OrderCancel);
            //    //lblOrderTotal.Text = string.Format("{0:N0}", OrderTotal);
            //    //pninfo.Visible = true;
            //   // gr.DataSource = ro;
            //   // gr.DataBind();
            //}
        }
        public void LoadGrid()
        {
            var ListOrder = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            //ltrHistory.Text = "";
            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                foreach (var o in ListOrder)
                {
                    double or_TotalRealPrice = 0;
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;

                    if (o.TotalPriceReal.ToFloat(0) > 0)
                        or_TotalRealPrice = Convert.ToDouble(o.TotalPriceReal);
                    if (o.TotalPriceVND.ToFloat(0) > 0)
                        or_TotalPriceVND = Convert.ToDouble(o.TotalPriceVND);
                    if (o.Deposit.ToFloat(0) > 0)
                        or_Deposit = Convert.ToDouble(o.Deposit);
                    if (o.IsFastPrice.ToFloat(0) > 0)
                        or_IsFastPrice = Convert.ToDouble(o.IsFastPrice);
                    if (o.FeeShipCN.ToFloat(0) > 0)
                        or_FeeShipCN = Convert.ToDouble(o.FeeShipCN);
                    if (o.FeeBuyPro.ToFloat(0) > 0)
                        or_FeeBuyPro = Convert.ToDouble(o.FeeBuyPro);
                    if (o.FeeWeight.ToFloat(0) > 0)
                        or_FeeWeight = Convert.ToDouble(o.FeeWeight);
                    if (o.IsCheckProductPrice.ToFloat(0) > 0)
                        or_IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);
                    if (o.IsPackedPrice.ToFloat(0) > 0)
                        or_IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);
                    if (o.IsFastDeliveryPrice.ToFloat(0) > 0)
                        or_IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);
                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;



                    int stt = Convert.ToInt32(o.Status);

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                    r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                    r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                    r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                    r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                    r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                    r.TotalRealPrice = string.Format("{0:N0}", or_TotalRealPrice);
                    r.Deposit = string.Format("{0:N0}", or_Deposit);
                    r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                    r.Status = PJUtils.IntToRequestAdmin(stt);
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    ro_gr.Add(r);
                }

                //  gr.DataSource = ro_gr;
                //gr.DataBind();
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            //  gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            // gr.Rebind();
        }
        #endregion

        public class ReportOrder
        {
            public int OrderID { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string BuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string ShipHome { get; set; }
            public string CheckProduct { get; set; }
            public string Package { get; set; }
            public string IsFast { get; set; }
            public string Total { get; set; }
            public string TotalRealPrice { get; set; }
            public string Deposit { get; set; }
            public string PayLeft { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
            {
                rdatefrom.Text = fd;
            }
            if (!string.IsNullOrEmpty(td))
            {
                rdateto.Text = td;
            }
            var ListOrder = MainOrderController.GetAllDateToDateNotPage(fd, td);

            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                foreach (var o in ListOrder)
                {
                    //double or_TotalRealPrice = 0;
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;

                    //if (o.TotalPriceReal.ToFloat(0) > 0)
                    //    or_TotalRealPrice = Convert.ToDouble(o.TotalPriceReal);
                    if (o.TotalPriceVND.ToFloat(0) > 0)
                        or_TotalPriceVND = Convert.ToDouble(o.TotalPriceVND);
                    if (o.Deposit.ToFloat(0) > 0)
                        or_Deposit = Convert.ToDouble(o.Deposit);
                    if (o.IsFastPrice.ToFloat(0) > 0)
                        or_IsFastPrice = Convert.ToDouble(o.IsFastPrice);
                    if (o.FeeShipCN.ToFloat(0) > 0)
                        or_FeeShipCN = Convert.ToDouble(o.FeeShipCN);
                    if (o.FeeBuyPro.ToFloat(0) > 0)
                        or_FeeBuyPro = Convert.ToDouble(o.FeeBuyPro);
                    if (o.FeeWeight.ToFloat(0) > 0)
                        or_FeeWeight = Convert.ToDouble(o.FeeWeight);
                    if (o.IsCheckProductPrice.ToFloat(0) > 0)
                        or_IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);
                    if (o.IsPackedPrice.ToFloat(0) > 0)
                        or_IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);
                    if (o.IsFastDeliveryPrice.ToFloat(0) > 0)
                        or_IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);

                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;



                    int stt = Convert.ToInt32(o.Status);

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                    r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                    r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                    r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                    r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                    r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                    r.Deposit = string.Format("{0:N0}", or_Deposit);
                    r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                    r.Status = PJUtils.IntToRequestAdmin(stt);
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    ro_gr.Add(r);
                }

                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ShopID</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShopID + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "bao-cao-don-hang-" + rdatefrom.Text + "-" + rdateto.Text + ".xls";
                string strcontentType = "application/vnd.ms-excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
                //Response.Close();
                Response.End();


                //gr.DataSource = ro_gr;
                //gr.DataBind();
            }
        }
        #region Pagging
        public void pagingall(List<tbl_MainOder> acs, int total)
        {
            int PageSize = 10;
            if (total > 0)
            {
                int TotalItems = total;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;
                StringBuilder hcm = new StringBuilder();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.ID + "</td>");
                    hcm.Append("<td>" + item.ShopName + "</td>");
                    hcm.Append("<td>" + item.TotalPriceVND + "</td>");
                    hcm.Append("<td>" + item.TotalPriceReal + "</td>");
                    hcm.Append("<td>" + item.Deposit + "</td>");
                    double paylef = Convert.ToDouble(item.TotalPriceVND) - Convert.ToDouble(item.Deposit);
                    hcm.Append("<td>" + paylef + "</td>");
                    hcm.Append("<td>" + PJUtils.IntToRequestAdminNew(item.Status.Value) + "</td>");
                    hcm.Append("<td>" + item.CreatedDate + "</td>");
                    hcm.Append("</tr>");
                }
                ltr.Text = hcm.ToString();
            }
        }

        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {
            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));
        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                if (pageUrl.IndexOf("Page=") > 0)
                {
                    int a = pageUrl.IndexOf("Page=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&Page={0}";
                }

            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<a class=\"prev-page pagi-button\" title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Prev</a>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<a class=\"pagi-button current-active\">" + i.ToString() + "</a>");
                }
                else
                {
                    output.Append("<a class=\"pagi-button\" href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<a class=\"next-page pagi-button\" title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
    }
}
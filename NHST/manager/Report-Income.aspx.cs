using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;


namespace NHST.manager
{
    public partial class Report_Income : System.Web.UI.Page
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
            rdatefrom.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy HH:mm");
            rdateto.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var staffsale = AccountController.GetAllByRoleID(6);
            ddlStaffSale.Items.Clear();
            ddlStaffSale.Items.Insert(0, new ListItem("Chọn nhân viên kinh doanh", "0"));
            if (staffsale.Count > 0)
            {
                ddlStaffSale.DataSource = staffsale;
                ddlStaffSale.DataBind();
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string a = rdatefrom.Text.ToString();
            string b = rdateto.Text.ToString();
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            var history = PayOrderHistoryController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (history.Count > 0)
            {
                List<PayHistory> ro_gr1 = new List<PayHistory>();
                foreach (var o in history)
                {
                    int stt = Convert.ToInt32(o.Status);
                    string status = "";
                    PayHistory r = new PayHistory();
                    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                    string username = "";
                    if (AccountController.GetByID(Convert.ToInt32(o.UID)) != null)
                    {
                        username = AccountController.GetByID(Convert.ToInt32(o.UID)).Username;
                    }
                    r.Username = username;
                    if (stt == 2)
                        status = "Đặt cọc đơn hàng";
                    else
                        status = "Thanh toán đơn hàng";
                    r.Status = status;

                    double amount = 0;
                    if (o.Amount.ToString().ToFloat(0) > 0)
                        amount = Convert.ToDouble(o.Amount);

                    r.Amount = string.Format("{0:N0}", amount) + "";
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    string createdBy = "";
                    if (AccountController.GetByUsername(o.CreatedBy) != null)
                    {
                        createdBy = AccountController.GetByUsername(o.CreatedBy).Username;
                    }
                    r.CreatedBy = createdBy;
                    ro_gr1.Add(r);
                }
                RadGrid1.DataSource = ro_gr1;
                RadGrid1.DataBind();
            }
            #endregion


            string RevenueDataset = "[";
            #region Thống kê đơn hàng
            var ListOrder = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro = new List<ReportOrder>();

                double Damuahang = 0;
                double Dahoanthanh = 0;
                double Datcocdenhoanthanh = 0;

                var ListOrder_Damuahang = ListOrder.Where(o => o.Status == 5).ToList();
                if (ListOrder_Damuahang.Count > 0)
                {
                    foreach (var od in ListOrder_Damuahang)
                    {
                        Damuahang += Convert.ToDouble(od.AmountDeposit);
                    }
                }
                var ListOrder_Dahoanthanh = ListOrder.Where(o => o.Status == 10).ToList();
                if (ListOrder_Dahoanthanh.Count > 0)
                {
                    foreach (var od in ListOrder_Dahoanthanh)
                    {
                        Dahoanthanh += Convert.ToDouble(od.AmountDeposit);
                    }
                }
                var ListOrder_Datcocdenhoanthanh = ListOrder.Where(o => o.Status > 1).ToList();
                if (ListOrder_Datcocdenhoanthanh.Count > 0)
                {
                    foreach (var od in ListOrder_Datcocdenhoanthanh)
                    {
                        Datcocdenhoanthanh += Convert.ToDouble(od.AmountDeposit);
                    }
                }


                var ListOrder_Datcoc_VeVN = ListOrder.Where(o => o.Status > 1 && o.Status < 9).ToList();
                var ListOrder_Dathanhtoan_Dahoanthanh = ListOrder.Where(o => o.Status > 7).ToList();

                double FeeShipCN1 = 0;
                double FeeShipCN2 = 0;
                double FeeBuyPro1 = 0;
                double FeeBuyPro2 = 0;
                double FeeWeight1 = 0;
                double FeeWeight2 = 0;
                double FeeCheckPro1 = 0;
                double FeeCheckPro2 = 0;
                double FeePackage1 = 0;
                double FeePackage2 = 0;

                if (ListOrder_Datcoc_VeVN.Count > 0)
                {
                    foreach (var item in ListOrder_Datcoc_VeVN)
                    {
                        FeeShipCN1 += Convert.ToDouble(item.FeeShipCN);
                        FeeBuyPro1 += Convert.ToDouble(item.FeeBuyPro);
                        FeeWeight1 += Convert.ToDouble(item.FeeWeight);
                        FeeCheckPro1 += Convert.ToDouble(item.IsCheckProductPrice);
                        FeePackage1 += Convert.ToDouble(item.IsPackedPrice);
                    }
                }
                if (ListOrder_Dathanhtoan_Dahoanthanh.Count > 0)
                {
                    foreach (var item in ListOrder_Dathanhtoan_Dahoanthanh)
                    {
                        FeeShipCN2 += Convert.ToDouble(item.FeeShipCN);
                        FeeBuyPro2 += Convert.ToDouble(item.FeeBuyPro);
                        FeeWeight2 += Convert.ToDouble(item.FeeWeight);
                        FeeCheckPro2 += Convert.ToDouble(item.IsCheckProductPrice);
                        FeePackage2 += Convert.ToDouble(item.IsPackedPrice);
                    }
                }

                lblDamuahang.Text = string.Format("{0:N0}", Damuahang) + " VNĐ";

                lblDahoanthanh.Text = string.Format("{0:N0}", Dahoanthanh) + " VNĐ";
                lbldatcocdenhoanthanh.Text = string.Format("{0:N0}", Datcocdenhoanthanh) + " VNĐ";

                lblFeeShipCN1.Text = string.Format("{0:N0}", FeeShipCN1) + " VNĐ";
                lblFeeShipCN2.Text = string.Format("{0:N0}", FeeShipCN2) + " VNĐ";

                string dataFeeShipCN = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               label = "Tiền ship Trung Quốc",
                                               data = "[" + FeeShipCN1 + ", " + FeeShipCN2 + "]",
                                               backgroundColor = "#2196F3",
                                               fill = false
                                           });
                RevenueDataset += dataFeeShipCN + ",";

                lblFeeBuyPro1.Text = string.Format("{0:N0}", FeeBuyPro1) + " VNĐ";
                lblFeeBuyPro2.Text = string.Format("{0:N0}", FeeBuyPro2) + " VNĐ";

                string dataFeeBuyPro = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               label = "Tiền phí mua hàng",
                                               data = "[" + FeeBuyPro1 + ", " + FeeBuyPro2 + "]",
                                               backgroundColor = "#E91E63",
                                               fill = false
                                           });
                RevenueDataset += dataFeeBuyPro + ",";

                lblFeeWeight1.Text = string.Format("{0:N0}", FeeWeight1) + " VNĐ";
                lblFeeWeight2.Text = string.Format("{0:N0}", FeeWeight2) + " VNĐ";

                string dataFeeWeight = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               label = "Tiền phí cân nặng",
                                               data = "[" + FeeWeight1 + ", " + FeeWeight2 + "]",
                                               backgroundColor = "#8f21f3",
                                               fill = false
                                           });
                RevenueDataset += dataFeeWeight + ",";

                lblIsCheckProductPrice1.Text = string.Format("{0:N0}", FeeCheckPro1) + " VNĐ";
                lblIsCheckProductPrice2.Text = string.Format("{0:N0}", FeeCheckPro2) + " VNĐ";

                string dataCheckProductPrice = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               label = "Tiền phí kiểm đếm",
                                               data = "[" + FeeCheckPro1 + ", " + FeeCheckPro2 + "]",
                                               backgroundColor = "#d913e6",
                                               fill = false
                                           });
                RevenueDataset += dataCheckProductPrice + ",";

                lblIsPackedPrice1.Text = string.Format("{0:N0}", FeePackage1) + " VNĐ";
                lblIsPackedPrice2.Text = string.Format("{0:N0}", FeePackage2) + " VNĐ";

                string dataPackedPrice = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               label = "Tiền phí đóng gỗ",
                                               data = "[" + FeePackage1 + ", " + FeePackage2 + "]",
                                               backgroundColor = "#104cf5",
                                               fill = false
                                           });
                RevenueDataset += dataPackedPrice;
                RevenueDataset += "]";
                hdfRevenueDataset.Value = RevenueDataset;

                double Total = 0;
                double Deposit = 0;
                double NotPay = 0;
                double OrderFast = 0;
                double FeeShipCN = 0;
                double FeeBuyPro = 0;
                double FeeWeight = 0;
                double IsCheckProductPrice = 0;
                double IsPackedPrice = 0;
                double IsFastDeliveryPrice = 0;
                double TotalPriceReal = 0;

                foreach (var o in ListOrder)
                {     
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;
                    double or_TotalPriceReal = 0;

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
                    if (o.TotalPriceReal.ToFloat(0) > 0)
                        or_TotalPriceReal = Convert.ToDouble(o.TotalPriceReal);

                    double currentOrderPriceLeft = 0;

                    Total += or_TotalPriceVND;

                    int stt = Convert.ToInt32(o.Status);
                    currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;
                    Deposit += o.Deposit.ToFloat();
                    NotPay += currentOrderPriceLeft;
                    OrderFast += or_IsFastPrice;
                    FeeShipCN += or_FeeShipCN;
                    FeeBuyPro += or_FeeBuyPro;
                    FeeWeight += or_FeeWeight;
                    IsCheckProductPrice += or_IsCheckProductPrice;
                    IsPackedPrice += or_IsPackedPrice;
                    IsFastDeliveryPrice += or_IsFastDeliveryPrice;
                    TotalPriceReal += or_TotalPriceReal;

                    string username = "";
                    string saler = "";
                    var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                    if (udathang != null)
                        username = udathang.Username;
                    var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                    if (usalert != null)
                        saler = usalert.Username;

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;
                    r.Saler = saler;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                    r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                    r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                    r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                    r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                    r.FeeReal = string.Format("{0:N0}", or_TotalPriceReal);
                    r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                    r.Deposit = string.Format("{0:N0}", or_Deposit);
                    r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                    r.Status = PJUtils.IntToRequestAdmin(stt);
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    ro.Add(r);
                }
                lblTotal.Text = string.Format("{0:N0}", Total) + " VNĐ";
                lblDeposit.Text = string.Format("{0:N0}", Deposit) + " VNĐ";
                lblNotPay.Text = string.Format("{0:N0}", NotPay) + " VNĐ";
                lblOrderFast.Text = string.Format("{0:N0}", OrderFast) + " VNĐ";
                lblFeeShipCN.Text = string.Format("{0:N0}", FeeShipCN) + " VNĐ";
                lblFeeBuyPro.Text = string.Format("{0:N0}", FeeBuyPro) + " VNĐ";
                lblFeeWeight.Text = string.Format("{0:N0}", FeeWeight) + " VNĐ";
                lblIsCheckProductPrice.Text = string.Format("{0:N0}", IsCheckProductPrice) + " VNĐ";
                lblIsPackedPrice.Text = string.Format("{0:N0}", IsPackedPrice) + " VNĐ";
                lblIsFastDeliveryPrice.Text = string.Format("{0:N0}", IsFastDeliveryPrice) + " VNĐ";
                double[] data2 = new double[] { Damuahang, Dahoanthanh, Datcocdenhoanthanh, Deposit, NotPay, OrderFast, IsFastDeliveryPrice };
                string[] backgroundColor2 = new string[] { "rgb(242, 27, 69)", "rgb(242, 145, 69)", "rgb(134, 218, 69)", "rgb(226, 104, 227)", "rgb(146, 161, 227)", "rgb(41, 239, 227)", "rgb(41, 161, 227)" };
                string datasetsTotal = new JavaScriptSerializer().Serialize(new
                {
                    label = "Loại giao dịch",
                    data = data2,
                    backgroundColor = backgroundColor2,
                    hoverBackgroundColor = "hover_gradient",
                    hoverBorderWidth = 2,
                    hoverBorderColor = "hover_gradient"
                });
                string dataChartTotal = new JavaScriptSerializer().Serialize(
                                           new
                                           {
                                               labels = "[\"Đã mua\", \"Hoàn thành\", \"Đặt cọc - hoàn thành\", \"Tiền cọc\", \"Chưa thanh toán\", \"Đơn hỏa tốc\", \"Ship\"]",
                                               datasets = "[" + datasetsTotal + "]"
                                           });

                hdfCountryRevenueChart.Value = dataChartTotal;
                double TongTien = Damuahang + Dahoanthanh + Datcocdenhoanthanh + Deposit + NotPay + OrderFast + IsFastDeliveryPrice;
                lbTongTien.Text = string.Format("{0:N0}", TongTien);
                pnChart.Visible = true;
                gr.DataSource = ro;
                gr.DataBind();
            }
            #endregion
        }
        public void LoadGrid()
        {
            var ListOrder = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            //ltrHistory.Text = "";         
            if (ListOrder.Count > 0)
            {
                lbKCDL.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                foreach (var o in ListOrder)
                {
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;
                    double or_TotalPriceReal = 0;

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
                    if (o.TotalPriceReal.ToFloat(0) > 0)
                        or_TotalPriceReal = Convert.ToDouble(o.TotalPriceReal);

                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;
                    int stt = Convert.ToInt32(o.Status);
                    
                    string username = "";
                    string saler = "";
                    var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                    if (udathang != null)
                        username = udathang.Username;
                    var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                    if (usalert != null)
                        saler = usalert.Username;

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;
                    r.Saler = saler;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.FeeReal = string.Format("{0:N0}", or_TotalPriceReal);
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
                gr.DataSource = ro_gr;
                //gr.DataBind();
            }
            else
            {
                lbKCDL.Text = "<div class=\"col s12\"><span class=\"red-text text-darken-2 font-weight-500\">Không có dữ liệu thống kê trong ngày này</span></div>";
            }
        }
        public void LoadGrid1()
        {
            var ListOrder = PayOrderHistoryController.GetAll();
            //ltrHistory.Text = "";

            if (ListOrder.Count > 0)
            {
                List<PayHistory> ro_gr = new List<PayHistory>();
                foreach (var o in ListOrder)
                {
                    int stt = Convert.ToInt32(o.Status);
                    string status = "";
                    PayHistory r = new PayHistory();
                    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                    string username = "";
                    if (AccountController.GetByID(Convert.ToInt32(o.UID)) != null)
                    {
                        username = AccountController.GetByID(Convert.ToInt32(o.UID)).Username;
                    }
                    r.Username = username;
                    if (stt == 2)
                        status = "Đặt cọc đơn hàng";
                    else
                        status = "Thanh toán đơn hàng";
                    r.Status = status;
                    r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + "";
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    string createdBy = "";
                    if (AccountController.GetByUsername(o.CreatedBy) != null)
                    {
                        createdBy = AccountController.GetByUsername(o.CreatedBy).Username;
                    }
                    r.CreatedBy = createdBy;
                    ro_gr.Add(r);
                }
                RadGrid1.DataSource = ro_gr;              
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
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        #endregion
        public class ReportOrder
        {
            public int OrderID { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string Username { get; set; }
            public string Saler { get; set; }
            public string DatHang { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string BuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string FeeReal { get; set; }
            public string ShipHome { get; set; }
            public string CheckProduct { get; set; }
            public string Package { get; set; }
            public string IsFast { get; set; }
            public string Total { get; set; }
            public string Deposit { get; set; }
            public string PayLeft { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }
        public class PayHistory
        {
            public int MainOrderID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public string Status { get; set; }
            public string Amount { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
        }

        protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid1();
            RadGrid1.Rebind();
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid1();
            RadGrid1.Rebind();
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var ListOrder = PayOrderHistoryController.GetAll();
            //ltrHistory.Text = "";
            List<PayHistory> ro_gr = new List<PayHistory>();
            if (ListOrder.Count > 0)
            {

                foreach (var o in ListOrder)
                {
                    int stt = Convert.ToInt32(o.Status);
                    string status = "";
                    PayHistory r = new PayHistory();
                    r.MainOrderID = o.MainOrderID.ToString().ToInt();
                    r.Username = AccountController.GetByID(o.UID.ToString().ToInt(1)).Username;
                    if (stt == 2)
                        status = "Đặt cọc đơn hàng";
                    else
                        status = "Thanh toán đơn hàng";
                    r.Status = status;
                    r.Amount = string.Format("{0:N0}", o.Amount.ToString().ToFloat(0)) + "";
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    r.CreatedBy = AccountController.GetByUsername(o.CreatedBy).Username;
                    ro_gr.Add(r);
                }
            }
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
            StrExport.Append("      <th><strong>Loại thanh toán</strong></th>");
            StrExport.Append("      <th><strong>Số tiền</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("      <th><strong>Người tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in ro_gr)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.MainOrderID + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                StrExport.Append("      <td>" + item.Status + "</td>");
                StrExport.Append("      <td>" + item.Amount + "</td>");
                StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                StrExport.Append("      <td>" + item.CreatedBy + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "bao-cao-thanh-toan.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            var ListOrder = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            //ltrHistory.Text = "";
            List<ReportOrder> ro_gr = new List<ReportOrder>();
            if (ListOrder.Count > 0)
            {

                foreach (var o in ListOrder)
                {
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;
                    double or_TotalPriceReal = 0;

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
                    if (o.TotalPriceReal.ToFloat(0) > 0)
                        or_TotalPriceReal = Convert.ToDouble(o.TotalPriceReal);

                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;

                    int stt = Convert.ToInt32(o.Status);
                    string username = "";
                    string saler = "";
                    var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                    if (udathang != null)
                        username = udathang.Username;
                    var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                    if (usalert != null)
                        saler = usalert.Username;


                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;

                    string DatHang = "";
                    var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                    if (nvDatHang != null)
                        DatHang = nvDatHang.Username;
                    r.DatHang = DatHang;

                    r.Saler = saler;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeReal = string.Format("{0:N0}", or_TotalPriceReal);
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
            }
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Username</strong></th>");
            StrExport.Append("      <th><strong>Tên shop</strong></th>");
            StrExport.Append("      <th><strong>NVKD</strong></th>");
            StrExport.Append("      <th><strong>NVĐH</strong></th>");
            StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
            StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
            StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
            StrExport.Append("      <th><strong>Phí Giao tận nhà</strong></th>");
            StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
            StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
            StrExport.Append("      <th><strong>Tiền mua thật</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
            StrExport.Append("      <th><strong>Còn lại</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in ro_gr)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.OrderID + "</td>");
                StrExport.Append("      <td>" + item.Username + "</td>");
                StrExport.Append("      <td>" + item.ShopName + "</td>");
                StrExport.Append("      <td>" + item.Saler + "</td>");
                StrExport.Append("      <td>" + item.DatHang + "</td>");
                StrExport.Append("      <td>" + item.ShipCN + "</td>");
                StrExport.Append("      <td>" + item.BuyPro + "</td>");
                StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                StrExport.Append("      <td>" + item.ShipHome + "</td>");
                StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                StrExport.Append("      <td>" + item.Package + "</td>");
                StrExport.Append("      <td>" + item.FeeReal + "</td>");
                StrExport.Append("      <td>" + item.Total + "</td>");
                StrExport.Append("      <td>" + item.Deposit + "</td>");
                StrExport.Append("      <td>" + item.PayLeft + "</td>");
                StrExport.Append("      <td>" + item.Status + "</td>");
                StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "bao-cao-don-hang-" + string.Format("{0:dd/MM/yyyy}", rdatefrom.Text) + "-" + string.Format("{0:dd/MM/yyyy}", rdateto.Text) + ".xls";
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
        }

        protected void btnExcelDondamuahang_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            var ListOrder = ListOrder1.Where(o => o.Status == 5).ToList();
            //ltrHistory.Text = "";
            List<ReportOrder> ro_gr = new List<ReportOrder>();
            if (ListOrder.Count > 0)
            {
                foreach (var o in ListOrder)
                {
                    double or_TotalPriceVND = 0;
                    double or_Deposit = 0;
                    double or_IsFastPrice = 0;
                    double or_FeeShipCN = 0;
                    double or_FeeBuyPro = 0;
                    double or_FeeWeight = 0;
                    double or_IsCheckProductPrice = 0;
                    double or_IsPackedPrice = 0;
                    double or_IsFastDeliveryPrice = 0;

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
                    string username = "";
                    string saler = "";
                    var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                    if (udathang != null)
                        username = udathang.Username;
                    var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                    if (usalert != null)
                        saler = usalert.Username;

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Username = username;
                    r.Saler = saler;

                    string DatHang = "";
                    var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                    if (nvDatHang != null)
                        DatHang = nvDatHang.Username;
                    r.DatHang = DatHang;

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
            }
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
            StrExport.Append("      <th><strong>NVKD</strong></th>");
            StrExport.Append("      <th><strong>NVĐH</strong></th>");
            StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
            StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
            StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
            StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
            StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
            StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
            StrExport.Append("      <th><strong>Còn lại</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in ro_gr)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.OrderID + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                StrExport.Append("      <td>" + item.ShopName + "</td>");
                StrExport.Append("      <td>" + item.Saler + "</td>");
                StrExport.Append("      <td>" + item.DatHang + "</td>");
                StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                StrExport.Append("      <td>" + item.BuyPro + "</td>");
                StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                StrExport.Append("      <td>" + item.ShipHome + "</td>");
                StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                StrExport.Append("      <td>" + item.Package + "</td>");
                StrExport.Append("      <td>" + item.IsFast + "</td>");
                StrExport.Append("      <td>" + item.Total + "</td>");
                StrExport.Append("      <td>" + item.Deposit + "</td>");
                StrExport.Append("      <td>" + item.PayLeft + "</td>");
                StrExport.Append("      <td>" + item.Status + "</td>");
                StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "Don-da-mua-hang.xls";
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
        }

        protected void btnExcelDahoanthanh_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status == 10).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelDatcocdenhoanthanh_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
            if (ListOrder1.Count > 0)
            {
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-dat-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelDeposit_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-dat-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void BtnExcelNotPay_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-chua-thanh-toan.xls";
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
            }

        }

        protected void btnExcelOrderFast_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeShipCN_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeShipCN1_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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
            }

        }

        protected void btnExcelFeeShipCN2_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeBuyPro_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeBuyPro1_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));

            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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
            }

        }

        protected void btnExcelFeeBuyPro2_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeWeight_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelFeeWeight1_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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
            }
        }

        protected void btnExcelFeeWeight2_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelIsCheckProductPrice_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

                        r.Saler = saler;
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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelIsCheckProductPrice1_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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
            }

        }

        protected void btnExcelIsCheckProductPrice2_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelIsPackedPrice_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hoan-thanh.xls";
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
            }

        }

        protected void btnExcelIsPackedPrice1_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 1 && o.Status < 9).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-coc-den-hang-ve-VN.xls";
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
            }


        }

        protected void btnExcelIsPackedPrice2_Click(object sender, EventArgs e)
        {
            var ListOrder1 = MainOrderController.GetFromDateToDate(DateTime.ParseExact(rdatefrom.Text, "dd/MM/yyyy HH:mm", null), DateTime.ParseExact(rdateto.Text, "dd/MM/yyyy HH:mm", null));
            if (ListOrder1.Count > 0)
            {
                var ListOrder = ListOrder1.Where(o => o.Status > 7).ToList();
                //ltrHistory.Text = "";
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                if (ListOrder.Count > 0)
                {
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = 0;
                        double or_Deposit = 0;
                        double or_IsFastPrice = 0;
                        double or_FeeShipCN = 0;
                        double or_FeeBuyPro = 0;
                        double or_FeeWeight = 0;
                        double or_IsCheckProductPrice = 0;
                        double or_IsPackedPrice = 0;
                        double or_IsFastDeliveryPrice = 0;

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
                        string username = "";
                        string saler = "";
                        var udathang = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (udathang != null)
                            username = udathang.Username;
                        var usalert = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        if (usalert != null)
                            saler = usalert.Username;

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Username = username;
                        r.Saler = saler;

                        string DatHang = "";
                        var nvDatHang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        if (nvDatHang != null)
                            DatHang = nvDatHang.Username;
                        r.DatHang = DatHang;

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
                }
                StringBuilder StrExport = new StringBuilder();
                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table border=\"1\">");
                StrExport.Append("  <tr>");
                StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Username</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Tên shop</strong></th>");
                StrExport.Append("      <th><strong>NVKD</strong></th>");
                StrExport.Append("      <th><strong>NVĐH</strong></th>");
                StrExport.Append("      <th><strong>Phí ship TQ</strong></th>");
                StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
                StrExport.Append("      <th><strong>Phí cân nặng</strong></th>");
                StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Phí Giao tận nhà</strong></th>");
                StrExport.Append("      <th><strong>Phí kiểm kê</strong></th>");
                StrExport.Append("      <th><strong>Phí đóng gói</strong></th>");
                StrExport.Append("      <th><strong>Phí hỏa tốc</strong></th>");
                StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                StrExport.Append("      <th><strong>Còn lại</strong></th>");
                StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                StrExport.Append("  </tr>");
                foreach (var item in ro_gr)
                {
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <td>" + item.OrderID + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.Username + "</td>");
                    StrExport.Append("      <td>" + item.ShopName + "</td>");
                    StrExport.Append("      <td>" + item.Saler + "</td>");
                    StrExport.Append("      <td>" + item.DatHang + "</td>");
                    StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShipCN + "</td>");
                    StrExport.Append("      <td>" + item.BuyPro + "</td>");
                    StrExport.Append("      <td>" + item.FeeWeight + "</td>");
                    StrExport.Append("      <td>" + item.ShipHome + "</td>");
                    StrExport.Append("      <td>" + item.CheckProduct + "</td>");
                    StrExport.Append("      <td>" + item.Package + "</td>");
                    StrExport.Append("      <td>" + item.IsFast + "</td>");
                    StrExport.Append("      <td>" + item.Total + "</td>");
                    StrExport.Append("      <td>" + item.Deposit + "</td>");
                    StrExport.Append("      <td>" + item.PayLeft + "</td>");
                    StrExport.Append("      <td>" + item.Status + "</td>");
                    StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                    StrExport.Append("  </tr>");
                }
                StrExport.Append("</table>");
                StrExport.Append("</div></body></html>");
                string strFile = "Don-da-thanh-toan-den-da-hoan-thanh.xls";
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
            }

        }
    }
}
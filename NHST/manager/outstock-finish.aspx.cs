using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using MB.Extensions;
using System.Text;

namespace NHST.manager
{
    public partial class outstock_finish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 5)
                        Response.Redirect("/trang-chu");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            DateTime currentDate = DateTime.UtcNow.AddHours(7);
            string username_current = Session["userLoginSystem"].ToString();
            if (Request.QueryString["id"] != null)
            {
                int id = Request.QueryString["id"].ToInt(0);
                if (id > 0)
                {
                    ViewState["id"] = id;
                    var os = OutStockSessionController.GetByID(id);
                    if (os != null)
                    {
                        bool isShowButton = true;
                        double totalPriceMustPay = 0;
                        List<OrderPackage> ops = new List<OrderPackage>();
                        #region Đơn hàng mua hộ
                        var listmainorder = OutStockSessionPackageController.GetByOutStockSessionIDGroupByMainOrderID(id);
                        if (listmainorder.Count > 0)
                        {
                            foreach (var m in listmainorder)
                            {
                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(m));
                                if (mainorder != null)
                                {
                                    int mID = mainorder.ID;
                                    double totalPay = 0;
                                    OrderPackage op = new OrderPackage();
                                    op.OrderID = Convert.ToInt32(m);
                                    op.OrderType = 1;
                                    List<SmallpackageGet> sms = new List<SmallpackageGet>();
                                    var packsmain = OutStockSessionPackageController.GetAllByOutStockSessionIDAndMainOrderID(id, Convert.ToInt32(m));
                                    if (packsmain.Count > 0)
                                    {
                                        foreach (var p in packsmain)
                                        {
                                            var sm = SmallPackageController.GetByID(Convert.ToInt32(p.SmallPackageID));
                                            if (sm != null)
                                            {
                                                SmallpackageGet pg = new SmallpackageGet();
                                                if (sm.Status != 3)
                                                {
                                                    isShowButton = false;
                                                }
                                                double weight = 0;
                                                double weightCN = Convert.ToDouble(sm.Weight);
                                                double weightKT = 0;
                                                double dai = 0;
                                                double rong = 0;
                                                double cao = 0;
                                                if (sm.Length != null)
                                                    dai = Convert.ToDouble(sm.Length);
                                                if (sm.Width != null)
                                                    rong = Convert.ToDouble(sm.Width);
                                                if (sm.Height != null)
                                                    cao = Convert.ToDouble(sm.Height);

                                                if (dai > 0 && rong > 0 && cao > 0)
                                                    weightKT = dai * rong * cao / 7000;
                                                if (weightKT > 0)
                                                {
                                                    if (weightKT > weightCN)
                                                    {
                                                        weight = weightKT;
                                                    }
                                                    else
                                                    {
                                                        weight = weightCN;
                                                    }
                                                }
                                                else
                                                {
                                                    weight = weightCN;
                                                }
                                                weight = Math.Round(weight, 1);

                                                string packagecode = sm.OrderTransactionCode;
                                                int Status = Convert.ToInt32(sm.Status);
                                                double payInWarehouse = 0;

                                                pg.ID = sm.ID;
                                                pg.weight = weight;

                                                pg.packagecode = packagecode;
                                                pg.Status = Status;
                                                var feeweightinware = InWarehousePriceController.GetAll();
                                                double payperday = 0;
                                                double maxday = 0;
                                                foreach (var item in feeweightinware)
                                                {
                                                    if (item.WeightFrom < weight && weight <= item.WeightTo)
                                                    {
                                                        maxday = Convert.ToDouble(item.MaxDay);
                                                        payperday = Convert.ToDouble(item.PricePay);
                                                        break;
                                                    }
                                                }
                                                double totalDays = 0;
                                                if (sm.DateInLasteWareHouse != null)
                                                {
                                                    DateTime diw = Convert.ToDateTime(sm.DateInLasteWareHouse);
                                                    TimeSpan ts = currentDate.Subtract(diw);
                                                    if (ts.TotalDays > 0)
                                                        totalDays = Math.Floor(ts.TotalDays);
                                                }

                                                double dayin = totalDays - maxday;
                                                if (dayin > 0)
                                                {
                                                    payInWarehouse = dayin * payperday * weight;
                                                }
                                                pg.DateInWare = totalDays;
                                                totalPay += Math.Round(payInWarehouse, 0);
                                                pg.payInWarehouse = payInWarehouse;
                                                sms.Add(pg);
                                                SmallPackageController.UpdateWarehouseFeeDateOutWarehouse(sm.ID, payInWarehouse, currentDate);
                                                OutStockSessionPackageController.update(p.ID, currentDate, totalDays, payInWarehouse);
                                            }

                                        }
                                    }
                                    totalPay = Math.Round(totalPay, 0);
                                    op.totalPrice = totalPay;
                                    op.smallpackages = sms;
                                    double mustpay = 0;
                                    bool isPay = false;
                                    MainOrderController.UpdateFeeWarehouse(mID, totalPay);
                                    var ma = MainOrderController.GetAllByID(mID);
                                    if (ma != null)
                                    {
                                        double totalPriceVND = Math.Round(Convert.ToDouble(ma.TotalPriceVND), 0);
                                        double deposited = Math.Round(Convert.ToDouble(ma.Deposit), 0);
                                        double totalmustpay = Math.Round(totalPriceVND + totalPay, 0);
                                        double totalleftpay = Math.Round(totalmustpay - deposited, 0);
                                        if (totalmustpay <= deposited)
                                        {
                                            isPay = true;
                                        }
                                        else
                                        {
                                            double totalleft = Math.Round(totalmustpay - deposited, 0);
                                            if (totalleft > 100)
                                            {
                                                MainOrderController.UpdateStatus(mID, Convert.ToInt32(ma.UID), 7);
                                                mustpay = totalleftpay;
                                            }
                                            else
                                            {
                                                isPay = true;
                                            }
                                            //MainOrderController.UpdateStatus(mID, Convert.ToInt32(ma.UID), 7);
                                            //mustpay = totalleftpay;
                                        }
                                    }
                                    if (isShowButton == true)
                                    {
                                        if (isPay == false)
                                        {
                                            isShowButton = false;
                                        }
                                    }
                                    op.totalMustPay = mustpay;
                                    op.isPay = isPay;
                                    ops.Add(op);
                                }
                            }
                        }
                        #endregion
                        #region Đơn hàng VC hộ
                        var listtransportation = OutStockSessionPackageController.GetByOutStockSessionIDGroupByTransportationID(id);
                        if (listtransportation.Count > 0)
                        {
                            foreach (var t in listtransportation)
                            {
                                int tID = Convert.ToInt32(t);
                                var tran = TransportationOrderController.GetByID(tID);
                                if (tran != null)
                                {
                                    double totalPay = 0;
                                    OrderPackage op = new OrderPackage();
                                    op.OrderID = tID;
                                    op.OrderType = 2;
                                    List<SmallpackageGet> sms = new List<SmallpackageGet>();
                                    var packsmain = OutStockSessionPackageController.GetAllByOutStockSessionIDAndTransporationID(id, tID);
                                    if (packsmain.Count > 0)
                                    {
                                        foreach (var p in packsmain)
                                        {
                                            var sm = SmallPackageController.GetByID(Convert.ToInt32(p.SmallPackageID));
                                            if (sm != null)
                                            {
                                                SmallpackageGet pg = new SmallpackageGet();
                                                if (sm.Status != 3)
                                                {
                                                    isShowButton = false;
                                                }
                                                double weight = 0;
                                                double weightCN = Convert.ToDouble(sm.Weight);
                                                double weightKT = 0;
                                                double dai = 0;
                                                double rong = 0;
                                                double cao = 0;
                                                if (sm.Length != null)
                                                    dai = Convert.ToDouble(sm.Length);
                                                if (sm.Width != null)
                                                    rong = Convert.ToDouble(sm.Width);
                                                if (sm.Height != null)
                                                    cao = Convert.ToDouble(sm.Height);

                                                if (dai > 0 && rong > 0 && cao > 0)
                                                    weightKT = dai * rong * cao / 7000;
                                                if (weightKT > 0)
                                                {
                                                    if (weightKT > weightCN)
                                                    {
                                                        weight = weightKT;
                                                    }
                                                    else
                                                    {
                                                        weight = weightCN;
                                                    }
                                                }
                                                else
                                                {
                                                    weight = weightCN;
                                                }
                                                weight = Math.Round(weight, 1);
                                                string packagecode = sm.OrderTransactionCode;
                                                int Status = Convert.ToInt32(sm.Status);
                                                double payInWarehouse = 0;

                                                pg.ID = sm.ID;
                                                pg.weight = weight;

                                                pg.packagecode = packagecode;
                                                pg.Status = Status;
                                                var feeweightinware = InWarehousePriceController.GetAll();
                                                double payperday = 0;
                                                double maxday = 0;
                                                foreach (var item in feeweightinware)
                                                {
                                                    if (item.WeightFrom < weight && weight <= item.WeightTo)
                                                    {
                                                        maxday = Convert.ToDouble(item.MaxDay);
                                                        payperday = Convert.ToDouble(item.PricePay);
                                                        break;
                                                    }
                                                }
                                                double totalDays = 0;
                                                if (sm.DateInLasteWareHouse != null)
                                                {
                                                    DateTime diw = Convert.ToDateTime(sm.DateInLasteWareHouse);
                                                    TimeSpan ts = currentDate.Subtract(diw);
                                                    if (ts.TotalDays > 0)
                                                        totalDays = Math.Floor(ts.TotalDays);
                                                }
                                                double dayin = totalDays - maxday;
                                                if (dayin > 0)
                                                {
                                                    payInWarehouse = dayin * payperday * weight;
                                                }
                                                totalPay += Math.Round(payInWarehouse, 0);
                                                pg.DateInWare = totalDays;
                                                pg.payInWarehouse = payInWarehouse;
                                                sms.Add(pg);
                                                SmallPackageController.UpdateWarehouseFeeDateOutWarehouse(sm.ID, payInWarehouse, currentDate);
                                                OutStockSessionPackageController.update(p.ID, currentDate, totalDays, payInWarehouse);
                                            }
                                        }
                                    }
                                    totalPay = Math.Round(totalPay, 0);
                                    op.totalPrice = totalPay;
                                    op.smallpackages = sms;
                                    double mustpay = 0;
                                    bool isPay = false;
                                    TransportationOrderController.UpdateWarehouseFee(tID, totalPay);
                                    var tr = TransportationOrderController.GetByID(tID);
                                    if (tr != null)
                                    {
                                        double totalPriceVND = Math.Round(Convert.ToDouble(tr.TotalPrice), 0);
                                        double deposited = Math.Round(Convert.ToDouble(tr.Deposited), 0);
                                        double totalmustpay = Math.Round(totalPriceVND + totalPay, 0);
                                        double totalleftpay = Math.Round(totalmustpay - deposited, 0);
                                        if (totalmustpay <= deposited)
                                        {
                                            isPay = true;
                                        }
                                        else
                                        {
                                            double totalleft = Math.Round(totalmustpay - deposited, 0);
                                            if (totalleft > 100)
                                            {
                                                TransportationOrderController.UpdateStatus(tID, 5, currentDate, username_current);
                                                mustpay = Math.Round(totalleftpay, 0);
                                            }
                                            else
                                            {
                                                isPay = true;
                                            }

                                        }
                                    }
                                    if (isShowButton == true)
                                    {
                                        if (isPay == false)
                                        {
                                            isShowButton = false;
                                        }
                                    }
                                    op.totalMustPay = Math.Round(mustpay, 0);
                                    op.isPay = isPay;
                                    ops.Add(op);
                                }
                            }
                        }
                        #endregion
                        #region Render Data
                        txtFullname.Text = os.FullName;
                        txtPhone.Text = os.Phone;
                        if (isShowButton == true)
                        {
                            pnButton.Visible = true;
                        }
                        else
                        {
                            //pnButton.Visible = true;
                            pnrefresh.Visible = true;
                        }
                        StringBuilder html = new StringBuilder();
                        StringBuilder htmlPrint = new StringBuilder();
                        if (ops.Count > 0)
                        {
                            html.Append("<table class=\"table bordered \">");
                            html.Append("<thead>");
                            html.Append("<tr class=\"teal darken-4\">");



                            #region New
                            html.Append("<div class=\"responsive-tb package-item\">");



                            html.Append("<th>Mã đơn hàng</th>");
                            html.Append("<th>Mã vận đơn</th>");
                            html.Append("<th>Cân nặng</th>");
                            html.Append("<th>Ngày lưu kho</th>");
                            html.Append("<th>Trạng thái</th>");

                            html.Append("<th>Tiền lưu kho</th>");
                            html.Append("<th>Tổng tiền lưu kho</th>");
                            html.Append("<th>Trạng thái thanh toán</th>");
                            html.Append("<th>Tiền cần thanh toán</th>");
                            html.Append("</tr>");
                            html.Append("</thead>");
                            html.Append("<tbody>");

                            htmlPrint.Append("<article class=\"pane-primary\" style=\"color:#000\">");


                            htmlPrint.Append("   <article class=\"pane-primary\">");
                            htmlPrint.Append("       <table class=\"rgMasterTable normal-table full-width\" style=\"text-align:center\">");
                            htmlPrint.Append("           <tr>");
                            htmlPrint.Append("               <th style=\"color:#000\">Mã đơn hàng</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Mã vận đơn</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Cân nặng</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Ngày lưu kho</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Trạng thái</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Tiền lưu kho</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Tổng tiền lưu kho</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Trạng thái thanh toán</th>");
                            htmlPrint.Append("               <th style=\"color:#000\">Tiền cần thanh toán</th>");
                            htmlPrint.Append("           </tr>");
                            foreach (var o in ops)
                            {
                                int orderType = o.OrderType;
                                bool isPay = o.isPay;
                                int Count = 1;
                                int temp = 1;
                                string status = "<span class=\"red-text font-weight-600\">Đã thanh toán</span>";
                                if (o.isPay == false)
                                {
                                    status = "<span class=\"red-text font-weight-600\">Chưa thanh toán</span>";
                                }
                                var listpackages = o.smallpackages;
                                foreach (var p in listpackages)
                                {
                                    if (Count == 1)
                                    {
                                        html.Append("<tr>");

                                        html.Append("<td rowspan=\"" + listpackages.Count + "\"><span class=\"owner\">" + o.OrderID + "</span></td>");

                                        html.Append("<td><span>" + p.packagecode + "</span></td>");
                                        html.Append("<td><span>" + p.weight + "</span></td>");
                                        html.Append("<td><span>" + p.DateInWare + "</span></td>");
                                        html.Append("<td>" + PJUtils.IntToStringStatusSmallPackage45(p.Status) + "</td>");
                                        html.Append("<td>" + string.Format("{0:N0}", p.payInWarehouse) + " VNĐ</td>");
                                        html.Append("<td><span class=\"black-text font-weight-600\">" + string.Format("{0:N0}", o.totalPrice) + " VNĐ</span></td>");
                                        html.Append("<td>" + status + "</td>");
                                        html.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", o.totalMustPay) + " VNĐ</span></td>");
                                        html.Append("</tr>");
                                    }
                                    else
                                    {
                                        html.Append("<tr>");
                                        html.Append("<td><span>" + p.packagecode + "</span></td>");
                                        html.Append("<td><span>" + p.weight + "</span></td>");
                                        html.Append("<td><span>" + p.DateInWare + "</span></td>");
                                        html.Append("<td>" + PJUtils.IntToStringStatusSmallPackage45(p.Status) + "</td>");
                                        html.Append("<td>" + string.Format("{0:N0}", p.payInWarehouse) + " VNĐ</td>");
                                        html.Append("<td><span class=\"black-text font-weight-600\">" + string.Format("{0:N0}", o.totalPrice) + " VNĐ</span></td>");
                                        html.Append("<td>" + status + "</td>");
                                        html.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", o.totalMustPay) + " VNĐ</span></td>");
                                        html.Append("</tr>");
                                    }
                                    Count += 1;

                                }

                                totalPriceMustPay += o.totalMustPay;
                                #endregion

                                foreach (var p in listpackages)
                                {
                                    if (temp == 1)
                                    {
                                        htmlPrint.Append("           <tr>");
                                        if (orderType == 1)
                                        {
                                            htmlPrint.Append("<td rowspan=\"" + listpackages.Count + "\">ĐH mua hộ: <span class=\"owner\">" + o.OrderID + "</span></td>");
                                        }
                                        else
                                        {
                                            htmlPrint.Append("<td rowspan=\"" + listpackages.Count + "\">ĐH VC hộ: <span class=\"owner\">" + o.OrderID + "</span></td>");
                                        }
                                        htmlPrint.Append("               <td>" + p.packagecode + "</td>");
                                        htmlPrint.Append("               <td>" + p.weight + "</td>");
                                        htmlPrint.Append("               <td>" + p.DateInWare + "</td>");
                                        htmlPrint.Append("<td>" + PJUtils.IntToStringStatusSmallPackage45(p.Status) + "</td>");
                                        htmlPrint.Append("               <td>" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                        htmlPrint.Append("<td><span class=\"black-text font-weight-600\">" + string.Format("{0:N0}", o.totalPrice) + " VNĐ</span></td>");
                                        htmlPrint.Append("<td>" + status + "</td>");
                                        htmlPrint.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", o.totalMustPay) + " VNĐ</span></td>");

                                        htmlPrint.Append("           </tr>");
                                    }
                                    else
                                    {
                                        htmlPrint.Append("           <tr>");
                                        htmlPrint.Append("               <td>" + p.packagecode + "</td>");
                                        htmlPrint.Append("               <td>" + p.weight + "</td>");
                                        htmlPrint.Append("               <td>" + p.DateInWare + "</td>");
                                        htmlPrint.Append("<td>" + PJUtils.IntToStringStatusSmallPackage45(p.Status) + "</td>");
                                        htmlPrint.Append("               <td>" + string.Format("{0:N0}", p.payInWarehouse) + " vnđ</td>");
                                        htmlPrint.Append("<td><span class=\"black-text font-weight-600\">" + string.Format("{0:N0}", o.totalPrice) + " VNĐ</span></td>");
                                        htmlPrint.Append("<td>" + status + "</td>");
                                        htmlPrint.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", o.totalMustPay) + " VNĐ</span></td>");


                                        htmlPrint.Append("           </tr>");
                                    }
                                    temp += 1;
                                }


                            }
                            htmlPrint.Append("       </table>");
                            htmlPrint.Append("   </article>");
                            htmlPrint.Append("</article>");

                            html.Append("</tbody>");
                            //html.Append("<tbody>");
                            //html.Append("<tr>");
                            //html.Append("<td colspan=\"4\"><span class=\"black-text font-weight-500\">Tổng tiền lưu kho</span></td>");
                            //html.Append("<td><span class=\"black-text font-weight-600\">" + string.Format("{0:N0}", o.totalPrice) + " VNĐ</span></td>");
                            //html.Append("</tr>");
                            //html.Append("<tr>");
                            //html.Append("<td colspan=\"4\"><span class=\"black-text font-weight-500\">Trạng thái</span></td>");
                            //html.Append("<td>" + status + "</td>");
                            //html.Append("</tr>");
                            //html.Append("<tr>");
                            //html.Append("<td colspan=\"4\"><span class=\"black-text font-weight-500\">Tiền cần thanh toán</span></td>");
                            //html.Append("<td><span class=\"red-text font-weight-700\">" + string.Format("{0:N0}", o.totalMustPay) + " VNĐ</span></td>");
                            //html.Append("</tr>");
                            //html.Append("</tbody>");
                            html.Append("</table>");
                            html.Append("</div>");

                            ltrList.Text = html.ToString();
                            ViewState["content"] = htmlPrint.ToString();
                        }
                        #endregion
                        if (totalPriceMustPay > 0)
                        {
                            OutStockSessionController.updateTotalPay(id, totalPriceMustPay);
                        }
                    }
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.UtcNow.AddHours(7);
            string username_current = Session["userLoginSystem"].ToString();
            int id = ViewState["id"].ToString().ToInt(0);
            if (id > 0)
            {
                int UID = 0;
                var outs = OutStockSessionController.GetByID(id);
                if (outs != null)
                    UID = Convert.ToInt32(outs.UID);

                OutStockSessionController.update(id, txtFullname.Text, txtPhone.Text, 2, currentDate, username_current);
                var sessionpack = OutStockSessionPackageController.GetAllByOutStockSessionID(id);
                if (sessionpack.Count > 0)
                {
                    List<Main> mo = new List<Main>();
                    List<Trans> to = new List<Trans>();
                    foreach (var item in sessionpack)
                    {
                        SmallPackageController.UpdateStatus(Convert.ToInt32(item.SmallPackageID), 4, currentDate, username_current);
                        SmallPackageController.UpdateDateOutWarehouse(Convert.ToInt32(item.SmallPackageID), username_current, currentDate);

                        if (item.MainOrderID > 0)
                        {
                            bool check = mo.Any(x => x.MainOrderID == Convert.ToInt32(item.MainOrderID));
                            if (check != true)
                            {
                                Main m = new Main();
                                m.MainOrderID = Convert.ToInt32(item.MainOrderID);
                                mo.Add(m);
                            }
                        }
                        else
                        {
                            bool check = to.Any(x => x.TransportationOrderID == Convert.ToInt32(item.TransportationID));
                            if (check != true)
                            {
                                Trans t = new Trans();
                                t.TransportationOrderID = Convert.ToInt32(item.TransportationID);
                                to.Add(t);
                            }
                        }
                    }
                    if (mo.Count > 0)
                    {
                        foreach (var item in mo)
                        {
                            var m = MainOrderController.GetAllByID(item.MainOrderID);
                            if (m != null)
                            {
                                bool checkIsChinaCome = true;
                                var packages = SmallPackageController.GetByMainOrderID(item.MainOrderID);
                                if (packages.Count > 0)
                                {
                                    foreach (var p in packages)
                                    {
                                        if (p.Status < 4)
                                            checkIsChinaCome = false;
                                    }
                                }
                                if (checkIsChinaCome == true)
                                {
                                    MainOrderController.UpdateStatus(item.MainOrderID, Convert.ToInt32(m.UID), 10);
                                    if (m.CompleteDate == null)
                                    {
                                        MainOrderController.UpdateCompleteDate(m.ID, currentDate);
                                    }
                                }
                            }
                        }
                    }

                    if (to.Count > 0)
                    {
                        foreach (var item in to)
                        {
                            bool checkIsChinaCome = true;
                            var trans = SmallPackageController.GetByTransportationOrderID(item.TransportationOrderID);
                            if (trans.Count > 0)
                            {
                                foreach (var p in trans)
                                {
                                    if (p.Status < 4)
                                        checkIsChinaCome = false;
                                }
                            }
                            if (checkIsChinaCome == true)
                            {
                                TransportationOrderController.UpdateStatus(item.TransportationOrderID, 7, DateTime.UtcNow.AddHours(7), username_current);
                            }

                        }
                    }
                }
                var c = ConfigurationController.GetByTop1();
                string content = ViewState["content"].ToString();
                var html = "";
                html += "<div class=\"print-bill\">";
                html += "   <div class=\"top\">";
                html += "       <div class=\"left\">";
                html += "           <span class=\"company-info\">NHẬP HÀNG 360</span>";
                html += "           <span class=\"company-info\">Địa chỉ: " + c.Address + "</span>";
                html += "       </div>";
                html += "       <div class=\"right\">";
                html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-title\">";
                html += "       <h1>PHIẾU XUẤT KHO</h1>";
                html += "       <span class=\"bill-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", currentDate) + " </span>";
                html += "   </div>";
                html += "   <div class=\"bill-content\">";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Họ và tên người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtFullname.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\">";
                html += "           <label class=\"row-name\">Số ĐT người đến nhận: </label>";
                html += "           <label class=\"row-info\">" + txtPhone.Text + "</label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += "           <label class=\"row-name\">Danh sách kiện: </label>";
                html += "           <label class=\"row-info\"></label>";
                html += "       </div>";
                html += "       <div class=\"bill-row\" style=\"border:none\">";
                html += content;
                html += "       </div>";
                html += "   </div>";
                html += "   <div class=\"bill-footer\">";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người xuất hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "       </div>";
                html += "       <div class=\"bill-row-two\">";
                html += "           <strong>Người nhận hàng</strong>";
                html += "           <span class=\"note\">(Ký, họ tên)</span>";
                html += "           <span class=\"note\" style=\"margin-top:100px;\">" + txtFullname.Text + "</span>";
                html += "       </div>";
                html += "   </div>";
                html += "</div>";

                StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");

                sb.Append(@"VoucherPrint('" + html + "')");
                sb.Append(@"</script>");

                ///hàm để đăng ký javascript và thực thi đoạn script trên
                if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(Email);
            double totalPay = 0;
            if (Request.QueryString["id"] != null)
            {
                int id = Request.QueryString["id"].ToInt(0);
                if (id > 0)
                {
                    ViewState["id"] = id;
                    var os = OutStockSessionController.GetByID(id);
                    if (os != null)
                    {
                        var listmainorder = OutStockSessionPackageController.GetByOutStockSessionIDGroupByMainOrderID(id);
                        if (listmainorder.Count > 0)
                        {
                            foreach (var m in listmainorder)
                            {
                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(m));
                                if (mainorder != null)
                                {
                                    int mID = mainorder.ID;
                                    OrderPackage op = new OrderPackage();
                                    op.OrderID = Convert.ToInt32(m);
                                    totalPay = Math.Round(totalPay, 0);
                                    op.totalPrice = totalPay;
                                    var ma = MainOrderController.GetAllByID(mID);
                                    if (ma != null)
                                    {
                                        double totalPriceVND = Math.Round(Convert.ToDouble(ma.TotalPriceVND), 0);
                                        double deposited = Math.Round(Convert.ToDouble(ma.Deposit), 0);
                                        double totalmustpay = Math.Round(totalPriceVND + totalPay, 0);
                                        double totalleftpay = Math.Round(totalmustpay - deposited, 0);
                                        var packsmain = OutStockSessionPackageController.GetAllByOutStockSessionIDAndMainOrderID(id, Convert.ToInt32(m));
                                        if (packsmain.Count > 0)
                                        {
                                            foreach (var p in packsmain)
                                            {
                                                var sm = SmallPackageController.GetByID(Convert.ToInt32(p.SmallPackageID));
                                                if (sm != null)
                                                {
                                                    SmallpackageGet pg = new SmallpackageGet();
                                                    if (Convert.ToInt32(totalleftpay) > 0)
                                                    {
                                                        var news = MainOrderController.GetAllByID(Convert.ToInt32(m));
                                                        if (news != null)
                                                        {
                                                            string kq = OutStockSessionPackageController.Remove(news.ID, p.ID);
                                                            if (kq == "ok")
                                                            {
                                                                PJUtils.ShowMessageBoxSwAlert("Ẩn đơn thành công.", "s", true, Page);
                                                            }
                                                            else
                                                            {
                                                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình ẩn đơn. Vui lòng thử lại.", "e", true, Page);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public class OrderPackage
        {
            public int OrderID { get; set; }
            public int OrderType { get; set; }
            public List<SmallpackageGet> smallpackages { get; set; }
            public double totalPrice { get; set; }
            public bool isPay { get; set; }
            public double totalMustPay { get; set; }
        }
        public class SmallpackageGet
        {
            public int ID { get; set; }
            public string packagecode { get; set; }
            public double weight { get; set; }
            public double DateInWare { get; set; }
            public int Status { get; set; }
            public double payInWarehouse { get; set; }

        }
        public class Main
        {
            public int MainOrderID { get; set; }
        }

        public class Trans
        {
            public int TransportationOrderID { get; set; }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}
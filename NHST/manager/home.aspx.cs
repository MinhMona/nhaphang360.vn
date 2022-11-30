using MB.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI.Business;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using System.Web.Services;

namespace NHST.manager
{
    public partial class home : System.Web.UI.Page
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
                    // UpdatePass();
                    LoadOrder();
                }
            }
        }

        public void UpdatePass()
        {
            var la = AccountController.GetAll("");
            if (la.Count > 0)
            {
                foreach (var item in la)
                {
                    if (string.IsNullOrEmpty(item.Token))
                    {
                        string Token = PJUtils.RandomStringWithText(16);
                        string pass = "123456";
                        AccountController.UpdateToken(item.ID, Token);
                        string rp = AccountController.UpdatePassword(item.ID, pass);
                    }
                }
            }
        }

        private void LoadOrder()
        {
            DateTime fd = GetMonday();

            //Số lượng mua hộ
            var buypro = GetBuyProCount(fd);
            //Số lượng mua hộ khác
            var anotherbuypro = GetAnotherBuyProCount(fd);
            //Số lượng vận chuyển hộ
            var transport = GetTransportationCount(fd);
            //Số lượng thanh toán hộ
            var payhelp = GetPayHelpCount(fd);

            //List<TotalOrder> to = new List<TotalOrder>();
            //TotalOrder od1 = new TotalOrder();
            //od1.data = buypro;
            //od1.label = "Mua hàng hộ";
            //od1.backgroundColor = "rgba(237,77,129,.7)";
            //to.Add(od1);

            //TotalOrder od2 = new TotalOrder();
            //od2.data = transport;
            //od2.label = "Vận chuyển hộ";
            //od2.backgroundColor = "rgba(174,88,253,.7)";
            //to.Add(od2);

            //TotalOrder od3 = new TotalOrder();
            //od3.data = payhelp;
            //od3.label = "Thanh toán hộ";
            //od3.backgroundColor = "rgba(248,192,50,.7)";
            //to.Add(od3);

            //TotalOrder od4 = new TotalOrder();
            //od4.data = anotherbuypro;
            //od4.label = "Mua hàng hộ khác";
            //od4.backgroundColor = "rgba(68, 138, 255,.7)";
            //to.Add(od4);

            List<int[]> a = new List<int[]>();
            a.Add(buypro);
            a.Add(transport);
            a.Add(payhelp);
            a.Add(anotherbuypro);
            string datasetsTotal = new JavaScriptSerializer().Serialize(new
            {
                data = a.ToArray()
            });

            hdfTotalOrderWeek.Value = datasetsTotal;


            //Tổng tiền khách nạp trong tuần
            #region Tổng tiền khách nạp trong tuần
            var totalWalletInWeek = GetTotalWalletInWeek(fd);
            var totalWalletInPrevWeek = GetTotalWalletInWeek(fd.AddDays(-7));
            double totalNow = 0;
            double totalPrev = 0;
            for (int i = 0; i < 7; i++)
            {
                totalNow = totalNow + totalWalletInWeek[i];
                totalPrev = totalPrev + totalWalletInPrevWeek[i];
            }
            if (totalPrev == 0 || totalNow == 0)
            {
                if (totalPrev == 0)
                {
                    StringBuilder html = new StringBuilder();
                    html.Append("<i class=\"material-icons circle pink accent-2\">trending_up</i>");
                    html.Append("<p class=\"medium-small\">Tuần này</p>");
                    html.Append("<h5 class=\"mt-0 mb-0 total-naptien\">100%</h5>");
                    ltrTotalWalletInPercent.Text = html.ToString();
                }
                if (totalNow == 0)
                {
                    StringBuilder html = new StringBuilder();
                    html.Append("<i class=\"material-icons circle pink accent-2\">trending_down</i>");
                    html.Append("<p class=\"medium-small\">Tuần này</p>");
                    html.Append("<h5 class=\"mt-0 mb-0 total-naptien\">100%</h5>");
                    ltrTotalWalletInPercent.Text = html.ToString();
                }

            }
            else
            {
                double less = totalNow - totalPrev;
                double percent = (less * 100) / totalPrev;
                if (percent >= 0)
                {
                    StringBuilder html = new StringBuilder();
                    html.Append("<i class=\"material-icons circle pink accent-2\">trending_up</i>");
                    html.Append("<p class=\"medium-small\">Tuần này</p>");
                    html.Append("<h5 class=\"mt-0 mb-0 total-naptien\">" + Math.Round(percent,0) + "%</h5>");
                    ltrTotalWalletInPercent.Text = html.ToString();
                }
                else
                {
                    StringBuilder html = new StringBuilder();
                    html.Append("<i class=\"material-icons circle pink accent-2\">trending_down</i>");
                    html.Append("<p class=\"medium-small\">Tuần này</p>");
                    html.Append("<h5 class=\"mt-0 mb-0 total-naptien\">" + Math.Round((-percent), 0) + "%</h5>");
                    ltrTotalWalletInPercent.Text = html.ToString();
                }
            }
            lblTotalInWeek.Text = string.Format("{0:N0}", totalNow);
            string datasetsTotalWalletInWeek = new JavaScriptSerializer().Serialize(new
            {
                data = totalWalletInWeek
            });
            hdfTotalWalletInWeek.Value = datasetsTotalWalletInWeek;
            #endregion

            //Tỉ lệ đơn mua hộ
            #region Tỉ lệ đơn mua hộ
            List<int> dataRatioBuyPro2 = new List<int>();
            List<string> labelRatioByPro2 = new List<string>();
            var st0 = GetRatioBuyPro(fd, 0);
            if (st0 > 0)
            {
                dataRatioBuyPro2.Add(st0);
                labelRatioByPro2.Add("Chờ đặt cọc");
            }
            var st1 = GetRatioBuyPro(fd, 1);
            if (st1 > 0)
            {
                dataRatioBuyPro2.Add(st1);
                labelRatioByPro2.Add("Hủy đơn hàng");
            }
            var st2 = GetRatioBuyPro(fd, 2);
            if (st2 > 0)
            {
                dataRatioBuyPro2.Add(st2);
                labelRatioByPro2.Add("Khách đã đặt cọc");
            }
            //var st3 = GetRatioBuyPro(fd, 3);
            //var st4 = GetRatioBuyPro(fd, 4);
            var st5 = GetRatioBuyPro(fd, 5);
            if (st5 > 0)
            {
                dataRatioBuyPro2.Add(st5);
                labelRatioByPro2.Add("Đã mua hàng");
            }
            var st6 = GetRatioBuyPro(fd, 6);
            if (st6 > 0)
            {
                dataRatioBuyPro2.Add(st6);
                labelRatioByPro2.Add("Đã về kho TQ");
            }
            var st7 = GetRatioBuyPro(fd, 7);
            if (st7 > 0)
            {
                dataRatioBuyPro2.Add(st7);
                labelRatioByPro2.Add("Đã về kho VN");
            }
            //var st8 = GetRatioBuyPro(fd, 8);

            var st9 = GetRatioBuyPro(fd, 9);
            if (st9 > 0)
            {
                dataRatioBuyPro2.Add(st9);
                labelRatioByPro2.Add("Khách đã thanh toán");
            }
            var st10 = GetRatioBuyPro(fd, 10);
            if (st10 > 0)
            {
                dataRatioBuyPro2.Add(st10);
                labelRatioByPro2.Add("Đã hoàn thành");
            }

            //int[] dataRatioBuyPro = new int[] {st0,st1,st2,st5,st6,st7,st9,st10 };
            //string[] labelRatioByPro = new string[] { "Chờ đặt cọc", "Hủy đơn hàng", "Khách đã đặt cọc", "Đã mua hàng", "Đã về kho TQ", "Đã về kho VN", "Khách đã thanh toán", "Đã hoàn thành" };

            string datasetsRatioByPro = new JavaScriptSerializer().Serialize(new
            {
                label = labelRatioByPro2.ToArray(),
                data = dataRatioBuyPro2.ToArray()
            });
            hdfRationBuyPro.Value = datasetsRatioByPro;
            #endregion
            //Khách hàng mới nạp tiền
            #region Khách hàng mới nạp tiền
            var listUserAddNewWallet = GetUserAddNewWallet();
            if (listUserAddNewWallet != null)
            {
                if (listUserAddNewWallet.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < listUserAddNewWallet.Count; i++)
                    {
                        var item = listUserAddNewWallet[i];
                        html.Append("<tr>");
                        html.Append("    <td>" + item.Username + "</td>");
                        html.Append("    <td>" + string.Format("{0:N0}", item.Amount) + " VNĐ</td>");
                        html.Append("    <td>" + item.CreatedDate.Value.ToString("dd/MM/yyyy") + "</td>");
                        html.Append("    <td>" + PJUtils.ReturnStatusAddNewWallet(item.Status.Value) + "</td>");
                        html.Append("    <td class=\"center-align\"><a href=\"/manager/HistorySendWallet\"><i class=\"material-icons teal-text text-darken-4\">remove_red_eye</i></a>");
                        html.Append("    </td>");
                        html.Append("</tr>");
                    }
                    ltrUserAddNewWallet.Text = html.ToString();
                }
            }
            #endregion

            //Khách hàng nhiều tiền
            #region Khách hàng nhiều tiền
            var listRickUser = GetTop10UserRick();
            if (listRickUser != null)
            {
                if (listRickUser.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < listRickUser.Count; i++)
                    {
                        var item = listRickUser[i];
                        html.Append("<tr>");
                        html.Append("    <td>" + item.ID + "</td>");
                        html.Append("    <td>" + item.Username + "</td>");
                        html.Append("    <td>" + string.Format("{0:N0}", item.Wallet) + " VNĐ</td>");
                        html.Append("    <td>" + string.Format("{0:N0}", item.TotalDonate) + " VNĐ</td>");
                        html.Append("    <td class=\"center-align\"><a href=\"/manager/User-Transaction.aspx?i=" + item.UID + "\"><i class=\"material-icons teal-text text-darken-4\">remove_red_eye</i></a>");
                        html.Append("    </td>");
                        html.Append("</tr>");
                    }
                    ltrTop10RickUser.Text = html.ToString();
                }
            }
            #endregion


            //Khách hàng có nhiều đơn hàng
            #region Khách hàng có nhiều đơn hàng
            var listUserManyOrder = GetTop10UserHasAlotOrder();
            if (listUserManyOrder != null)
            {
                if (listUserManyOrder.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < listUserManyOrder.Count; i++)
                    {
                        var item = listUserManyOrder[i];
                        html.Append("<tr>");
                        html.Append("    <td>" + item.ID + "</td>");
                        html.Append("    <td>" + item.Username + "</td>");
                        html.Append("    <td>" + string.Format("{0:N0}", item.Wallet) + "</td>");
                        html.Append("    <td>" + item.TotalAll + "</td>");
                        html.Append("    <td>" + item.TotalMHH + "</td>");
                        html.Append("    <td>" + item.TotalVCH + "</td>");
                        html.Append("    <td>" + item.TotalTTH + "</td>");
                        html.Append("</tr>");
                    }
                    ltrTop10UserHasAlotOrder.Text = html.ToString();
                }
            }
            #endregion


            //Đơn hàng mới tạo
            #region Đơn hàng mới tạo
            var list10NewOrder = GetTop10NewOrder();
            if (list10NewOrder != null)
            {
                if (list10NewOrder.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < list10NewOrder.Count; i++)
                    {
                        var item = list10NewOrder[i];
                        var ac = AccountController.GetByID(item.UID.Value);
                        string username = "";
                        if (ac != null)
                        {
                            username = ac.Username;
                        }
                        string LoaiDonHang = "";
                        if (item.OrderType == 1)
                            LoaiDonHang = "Mua hộ";
                        else
                            LoaiDonHang = "Mua hộ khác";
                        html.Append("<tr>");
                        html.Append("    <td>" + item.ID + "</td>");
                        html.Append("    <td>" + username + "</td>");
                        html.Append("    <td>" + LoaiDonHang + "</td>");
                        html.Append("    <td>" + PJUtils.IntToRequestAdminNew(item.Status.Value) + "</td>");
                        html.Append("    <td class=\"center-align\"><a href=\"/manager/OrderDetail.aspx?id=" + item.ID + "\"><i class=\"material-icons teal-text text-darken-4\">remove_red_eye</i></a>");
                        html.Append("    </td>");
                        html.Append("</tr>");
                    }
                    ltrTop10NewOrder.Text = html.ToString();
                }
            }
            #endregion

            //Đơn vận chuyển hộ mới
            #region Đơn vận chuyển hộ mới
            var list10TransportationOrder = GetTop10NewTransportationOrder();
            if (list10TransportationOrder != null)
            {
                if (list10TransportationOrder.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < list10TransportationOrder.Count; i++)
                    {
                        var item = list10TransportationOrder[i];
                        var ac = AccountController.GetByID(item.UID.Value);
                        string username = "";
                        if (ac != null)
                        {
                            username = ac.Username;
                        }
                        html.Append("<tr>");
                        html.Append("    <td>" + username + "</td>");
                        html.Append("    <td>" + string.Format("{0:N0}", item.BarCode) + "</td>");
                     
                        html.Append("    <td>" + PJUtils.GeneralTransportationOrderNewStatus(item.Status.Value) + "</td>");
                        html.Append("    <td class=\"center-align\"><a href=\"/manager/chi-tiet-vch.aspx?id=" + item.ID + "\"><i class=\"material-icons teal-text text-darken-4\">remove_red_eye</i></a>");
                        html.Append("    </td>");
                        html.Append("</tr>");
                    }
                    ltrTop10TransportOrder.Text = html.ToString();
                }
            }
            #endregion

            //Đơn thanh toán hộ mới
            #region Đơn thanh toán hộ mới
            var list10NewPayHelp = GetTop10NewPayHelp();
            if (list10NewPayHelp != null)
            {
                if (list10NewPayHelp.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    for (int i = 0; i < list10NewPayHelp.Count; i++)
                    {
                        var item = list10NewPayHelp[i];
                        html.Append("<tr>");
                        html.Append("    <td>" + item.Username + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPrice)) + "</td>");
                        html.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                        html.Append("    <td>" + PJUtils.ReturnStatusPayHelpNew(item.Status.Value) + "</td>");
                        html.Append("    <td class=\"center-align\"><a href=\"/manager/chi-tiet-thanh-toan-ho.aspx?ID=" + item.ID + "\"><i class=\"material-icons teal-text text-darken-4\">remove_red_eye</i></a>");
                        html.Append("    </td>");
                        html.Append("</tr>");
                    }
                    ltrTop10PayHelpOrder.Text = html.ToString();
                }
            }
            #endregion

        }

        public class TotalOrder
        {
            public string label { get; set; }
            public string backgroundColor { get; set; }
            public int[] data { get; set; }
        }

        #region getday
        private static string Check(bool check)
        {
            if (check)
                return "checked";
            else
                return null;
        }
        public static DateTime GetMonday()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("vi-VN");
            DayOfWeek monday = culture.DateTimeFormat.FirstDayOfWeek;
            DateTime today = DateTime.Now;
            while (today.DayOfWeek != monday)
                today = today.AddDays(-1);
            return new DateTime(today.Year, today.Month, today.Day);
        }
        public static DateTime GetSunday()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("vi-VN");
            DayOfWeek sunday = culture.DateTimeFormat.FirstDayOfWeek - 1;
            DateTime today = DateTime.Now;
            while (today.DayOfWeek != sunday)
                today = today.AddDays(+1);
            return new DateTime(today.Year, today.Month, today.Day);
        }
        #endregion

        #region getdata
        public int[] GetBuyProCount(DateTime dateStart)
        {

            int[] BuyPro = new int[7];
            for (int i = 0; i <= BuyPro.Length - 1; i++)
            {
                DateTime dateend = dateStart.AddDays(1);
                var sql = @"select Total=Count(*) ";
                sql += "from tbl_MainOder ";
                sql += "Where OrderType=1 ";
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
                sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
                int a = 0;
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (reader["Total"] != DBNull.Value)
                        a = reader["Total"].ToString().ToInt(0);
                }
                reader.Close();
                BuyPro[i] = a;
                dateStart = dateStart.AddDays(1);
            }
            return BuyPro;
        }
        public int[] GetAnotherBuyProCount(DateTime dateStart)
        {

            int[] BuyPro = new int[7];
            for (int i = 0; i <= BuyPro.Length - 1; i++)
            {
                DateTime dateend = dateStart.AddDays(1);
                var sql = @"select Total=Count(*) ";
                sql += "from tbl_MainOder ";
                sql += "Where OrderType= 3 ";
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
                sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
                int a = 0;
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (reader["Total"] != DBNull.Value)
                        a = reader["Total"].ToString().ToInt(0);
                }
                reader.Close();
                BuyPro[i] = a;
                dateStart = dateStart.AddDays(1);
            }
            return BuyPro;
        }

        public int[] GetTransportationCount(DateTime dateStart)
        {

            int[] Array = new int[7];
            for (int i = 0; i <= Array.Length - 1; i++)
            {
                DateTime dateend = dateStart.AddDays(1);
                var sql = @"select Total=Count(*) ";
                sql += "from tbl_TransportationOrderNew ";
                sql += "Where CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
                sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
                int a = 0;
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (reader["Total"] != DBNull.Value)
                        a = reader["Total"].ToString().ToInt(0);
                }
                reader.Close();
                Array[i] = a;
                dateStart = dateStart.AddDays(1);
            }
            return Array;
        }
        public int[] GetPayHelpCount(DateTime dateStart)
        {

            int[] Array = new int[7];
            for (int i = 0; i <= Array.Length - 1; i++)
            {
                DateTime dateend = dateStart.AddDays(1);
                var sql = @"select Total=Count(*) ";
                sql += "from tbl_PayHelp ";
                sql += "Where CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
                sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
                int a = 0;
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (reader["Total"] != DBNull.Value)
                        a = reader["Total"].ToString().ToInt(0);
                }
                reader.Close();
                Array[i] = a;
                dateStart = dateStart.AddDays(1);
            }
            return Array;
        }
        public double[] GetTotalWalletInWeek(DateTime dateStart)
        {

            double[] BuyPro = new double[7];
            for (int i = 0; i <= BuyPro.Length - 1; i++)
            {
                DateTime dateend = dateStart.AddDays(1);
                var sql = @"select Total=Sum(Cast(Amount as float)) ";
                sql += "from tbl_AdminSendUserWallet ";
                sql += "Where Status = 2 ";
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
                sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
                double a = 0;
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (reader["Total"] != DBNull.Value)
                        a = Convert.ToDouble(reader["Total"].ToString());
                }
                reader.Close();
                BuyPro[i] = a;
                dateStart = dateStart.AddDays(1);
            }
            return BuyPro;
        }
        public int GetRatioBuyPro(DateTime dateStart, int status)
        {
            DateTime dateend = dateStart.AddDays(7);
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_MainOder ";
            sql += "Where Status= " + status + " ";
            sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + dateStart + "',113) ";
            sql += "AND CreatedDate < CONVERT(VARCHAR(24),'" + dateend + "',113) ";
            int a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }

        public List<tbl_AdminSendUserWallet> GetUserAddNewWallet()
        {
            var sql = @"select top 10 * ";
            sql += "from tbl_AdminSendUserWallet ";
            sql += "Where Status in(1,2) ";
            sql += "order by id desc ";
            List<tbl_AdminSendUserWallet> list = new List<tbl_AdminSendUserWallet>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_AdminSendUserWallet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["Amount"] != DBNull.Value)
                    entity.Amount = Convert.ToDouble(reader["Amount"].ToString());

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public List<KhachHangNhieuTien> GetTop10UserRick()
        {
            var sql = @"select top 10  mo.ID, mo.Username, mo.Email, mo.Wallet, p.sum_amount ";
            sql += "from tbl_Account as mo ";
            sql += "LEFT  JOIN ( SELECT UID,SUM(cast(Amount as float)) AS sum_amount FROM tbl_AdminSendUserWallet where Status=2 GROUP  BY UID) p ON p.UID = mo.ID ";
            sql += "where mo.RoleID = 1 ";
            sql += "order by mo.Wallet desc ";
            List<KhachHangNhieuTien> list = new List<KhachHangNhieuTien>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int stt = 0;
            while (reader.Read())
            {
                stt++;
                var entity = new KhachHangNhieuTien();

                entity.ID = stt;

                if (reader["ID"] != DBNull.Value)
                    entity.UID = reader["ID"].ToString().ToInt();

                if (reader["Wallet"] != DBNull.Value)
                    entity.Wallet = Convert.ToDouble(reader["Wallet"].ToString());

                if (reader["sum_amount"] != DBNull.Value)
                    entity.TotalDonate = Convert.ToDouble(reader["sum_amount"].ToString());

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public List<KhachHangNhieuDon> GetTop10UserHasAlotOrder()
        {
            var sql = @"select top 10  mo.ID, mo.Username, mo.Email, mo.Wallet, TotalMHH,TotalVCH,TotalTTH,(ISNULL(TotalMHH,0) + ISNULL(TotalTTH,0) + ISNULL(TotalVCH,0)) as 'Total' ";
            sql += "from tbl_Account as mo  ";
            sql += "LEFT  JOIN ( SELECT UID,COUNT(*) AS TotalMHH FROM tbl_MainOder where Status >= 2  GROUP  BY UID) c ON c.UID = mo.ID  ";
            sql += "LEFT  JOIN ( SELECT UID,COUNT(*) AS TotalVCH FROM tbl_TransportationOrder where Status>=2 GROUP  BY UID) x ON x.UID = mo.ID  ";
            sql += "LEFT  JOIN ( SELECT UID,COUNT(*) AS TotalTTH FROM tbl_PayHelp where Status >=1 and Status!=2 GROUP  BY UID) y ON y.UID = mo.ID  ";
            sql += "where mo.RoleID = 1 ";
            sql += "order by Total desc ";
            List<KhachHangNhieuDon> list = new List<KhachHangNhieuDon>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int stt = 0;
            while (reader.Read())
            {
                stt++;
                var entity = new KhachHangNhieuDon();

                entity.ID = stt;

                if (reader["ID"] != DBNull.Value)
                    entity.UID = reader["ID"].ToString().ToInt();

                if (reader["Wallet"] != DBNull.Value)
                    entity.Wallet = Convert.ToDouble(reader["Wallet"].ToString());

                if (reader["TotalMHH"] != DBNull.Value)
                    entity.TotalMHH = reader["TotalMHH"].ToString().ToInt();
                else
                    entity.TotalMHH = 0;

                if (reader["TotalVCH"] != DBNull.Value)
                    entity.TotalVCH = reader["TotalVCH"].ToString().ToInt();
                else
                    entity.TotalVCH = 0;

                if (reader["Total"] != DBNull.Value)
                    entity.TotalAll = reader["Total"].ToString().ToInt();
                else
                    entity.TotalAll = 0;

                if (reader["TotalTTH"] != DBNull.Value)
                    entity.TotalTTH = reader["TotalTTH"].ToString().ToInt();
                else
                    entity.TotalTTH = 0;

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public List<tbl_MainOder> GetTop10NewOrder()
        {
            var sql = @"select top 10 * ";
            sql += "from tbl_MainOder where Status=0 ";
            sql += "order by id desc ";
            List<tbl_MainOder> list = new List<tbl_MainOder>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public List<tbl_TransportationOrderNew> GetTop10NewTransportationOrder()
        {
            var sql = @"select top 10 * ";
            sql += "from tbl_TransportationOrderNew where Status=1 ";
            sql += "order by id desc ";
            List<tbl_TransportationOrderNew> list = new List<tbl_TransportationOrderNew>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_TransportationOrderNew();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["BarCode"] != DBNull.Value)
                    entity.BarCode = reader["BarCode"].ToString();

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
              
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

             
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public List<tbl_PayHelp> GetTop10NewPayHelp()
        {
            var sql = @"select top 10 * ";
            sql += "from tbl_PayHelp where status=0 ";
            sql += "order by id desc ";
            List<tbl_PayHelp> list = new List<tbl_PayHelp>();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_PayHelp();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["TotalPrice"] != DBNull.Value)
                    entity.TotalPrice = reader["TotalPrice"].ToString();

                if (reader["Currency"] != DBNull.Value)
                    entity.Currency = reader["Currency"].ToString();

                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();


                if (reader["IsNotComplete"] != DBNull.Value)
                    entity.IsNotComplete = Convert.ToBoolean(reader["IsNotComplete"].ToString().ToInt(0));


                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());


                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public partial class KhachHangNhieuTien
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public double TotalDonate { get; set; }
        }
        public partial class KhachHangNhieuDon
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public int TotalMHH { get; set; }
            public int TotalVCH { get; set; }
            public int TotalTTH { get; set; }

            public int TotalAll { get; set; }
        }

        [WebMethod]
        public static string checkisreadnoti(int ID)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                var noti = NotificationsController.GetByID(ID);
                if (noti != null)
                {
                    var kq = NotificationsController.UpdateNoti(noti.ID, DateTime.Now, username);
                    if (kq.ToInt(0) > 0)
                    {
                        return "ok";
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static string InsertDevicetoken(string PushEndpoint, string PushP256DH, string PushAuth)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                DeviceBrowserTable.insert(obj_user.ID, PushEndpoint, PushP256DH, PushAuth, obj_user.Username);
            }
            return null;
        }

        #endregion
    }
}
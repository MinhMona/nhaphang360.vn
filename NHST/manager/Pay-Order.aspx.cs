using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;

namespace NHST.manager
{
    public partial class Pay_Order : System.Web.UI.Page
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
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac != null)
                    {
                        if (ac.RoleID == 0 || ac.RoleID == 7)
                        {

                        }
                        else
                        {
                            Response.Redirect("/manager/OrderList.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("/manager/OrderList.aspx");
                    }
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    ViewState["OID"] = id;

                    double totalvnd = 0;
                    if (Convert.ToDouble(o.TotalPriceVND) > 0)
                        totalvnd = Convert.ToDouble(o.TotalPriceVND);

                    double deposited = 0;
                    if (Convert.ToDouble(o.Deposit) > 0)
                        deposited = Convert.ToDouble(o.Deposit);

                    double amountDeposit = 0;
                    if (Convert.ToDouble(o.AmountDeposit) > 0)
                        amountDeposit = Convert.ToDouble(o.AmountDeposit);

                    double musdeposit = amountDeposit - deposited;
                    double payleft = totalvnd - deposited;

                    lblMainOrderID.Text = o.ID.ToString();
                    lbOrderID.Text = o.ID.ToString();
                    //lblDeposit.Text = string.Format("{0:N0}", deposited);
                    lblAmountDeposit.Text = string.Format("{0:N0}", musdeposit);
                    lblTotalPriceVND.Text = string.Format("{0:N0}", totalvnd);
                    lblMusPay.Text = string.Format("{0:N0}", payleft);
                    lblUsername.Text = AccountController.GetByID(o.UID.ToString().ToInt()).Username;

                    if (o.Status >= 2)
                    {
                        hdfshow.Value = "pnPayall";
                        ddlStatus.Items.Add(new ListItem("Thanh toán", "9"));
                    }
                    else
                    {
                        hdfshow.Value = "pndeposit";
                        ddlStatus.Items.Add(new ListItem("Đặt cọc", "2"));
                        ddlStatus.Items.Add(new ListItem("Thanh toán", "9"));
                    }
                }
                else
                {
                    Response.Redirect("/manager/OrderList.aspx");
                }
            }
            else
            {
                Response.Redirect("/manager/OrderList.aspx");
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int id = ViewState["OID"].ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    string orderstatus = "";
                    int currentOrderStatus = Convert.ToInt32(o.Status);
                    switch (currentOrderStatus)
                    {
                        case 0:
                            orderstatus = "Chưa đặt cọc";
                            break;
                        case 1:
                            orderstatus = "Hủy đơn hàng";
                            break;
                        case 2:
                            orderstatus = "Đã đặt cọc";
                            break;
                        case 5:
                            orderstatus = "Đã đặt hàng";
                            break;
                        case 6:
                            orderstatus = "Đã về kho TQ";
                            break;
                        case 7:
                            orderstatus = "Đã về kho VN";
                            break;
                        case 8:
                            orderstatus = "Chờ thanh toán";
                            break;
                        case 9:
                            orderstatus = "Khách đã thanh toán";
                            break;
                        case 10:
                            orderstatus = "Khách đã nhận hàng";
                            break;
                        default:
                            break;
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac != null)
                    {
                        int user_currentID = ac.ID;
                        int uid = o.UID.ToString().ToInt();

                        double OCurrent_deposit = 0;
                        if (Convert.ToDouble(o.Deposit) > 0)
                            OCurrent_deposit = Convert.ToDouble(o.Deposit);

                        double mustdeposti = 0;
                        if (o.AmountDeposit.ToFloat(0) > 0)
                            mustdeposti = Convert.ToDouble(o.AmountDeposit);

                        double mustpaydeposit = mustdeposti - OCurrent_deposit;

                        double total = 0;
                        if (Convert.ToDouble(o.TotalPriceVND) > 0)
                            total = Convert.ToDouble(o.TotalPriceVND);

                        double mustpay = total - OCurrent_deposit;

                        int status = ddlStatus.SelectedValue.ToString().ToInt();

                        double Amount = 0;
                        if (Convert.ToDouble(pAmount.Value) > 0)
                            Amount = Convert.ToDouble(pAmount.Value);

                        int currentstt = Convert.ToInt32(o.Status);
                        int payment = ddlPaymentMethod.SelectedValue.ToString().ToInt(1);
                        //int payment = 1;

                        var userDathang = AccountController.GetByID(uid);
                        if (userDathang == null)
                        {
                            Response.Redirect("/manager/OrderList.aspx");
                        }

                        double currentWallet = 0;
                        if (Convert.ToDouble(userDathang.Wallet) > 0)
                            currentWallet = Convert.ToDouble(userDathang.Wallet);

                        #region Ghi lịch sử update status của đơn hàng
                        OrderCommentController.Insert(id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn.", true, 1, DateTime.Now, uid);
                        //if (status != currentstt)
                        //{
                        //    HistoryOrderChangeController.Insert(o.ID, user_currentID, username_current, username_current +
                        //               " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + ddlStatus.SelectedItem + "", 0, currentDate);
                        //}
                        #endregion
                        if (status == 2)
                        {
                            if (Amount >= mustpaydeposit)
                            {
                                if (payment == 2)
                                {
                                    if (currentWallet >= Amount)
                                    {
                                        //Trừ tiền wallet, ghi lịch sử history pay wallet
                                        double walletleft = currentWallet - Amount;
                                        AccountController.updateWallet(uid, walletleft, currentDate, username_current);
                                        HistoryPayWalletController.Insert(uid, userDathang.Username, id, Amount, userDathang.Username + " đã đặt cọc đơn hàng: " + id + ".",
                                            walletleft, 1, 1, currentDate, username_current);
                                        HistoryOrderChangeController.Insert(o.ID, user_currentID, username_current, username_current +
                                       " đã đổi tiền đặt cọc của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                       + string.Format("{0:N0}", Amount) + "", 1, currentDate);

                                        MainOrderController.UpdateDeposit(id, uid, Amount.ToString());
                                        MainOrderController.UpdateStatus(id, uid, status);
                                        MainOrderController.UpdateDepositDate(id, currentDate);
                                        PayOrderHistoryController.Insert(id, uid, status, Amount, payment, currentDate, username_current);


                                        PJUtils.ShowMessageBoxSwAlert("Xử lý đặt cọc thành công", "s", true, Page);
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Số tiền trong ví của khách hàng đủ để đặt cọc", "e", true, Page);
                                    }
                                }
                                else
                                {
                                    double depositpayall = OCurrent_deposit + Amount;

                                    HistoryOrderChangeController.Insert(o.ID, user_currentID, username_current, username_current +
                                       " đã đổi tiền đặt cọc của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                       + string.Format("{0:N0}", depositpayall) + "", 1, currentDate);

                                    MainOrderController.UpdateDeposit(id, uid, depositpayall.ToString());
                                    MainOrderController.UpdateStatus(id, uid, status);
                                    MainOrderController.UpdatePayDate(id, currentDate);
                                    PayOrderHistoryController.Insert(id, uid, status, Amount, payment, currentDate, username_current);
                                    PayAllOrderHistoryController.Insert(id, Amount, uid, "", pContent1.Text, status, currentDate, username_current);
                                    PJUtils.ShowMessageBoxSwAlert("Xử lý đặt cọc thành công", "s", true, Page);
                                }
                            }
                            else
                                PJUtils.ShowMessageBoxSwAlert("Số tiền nhập vào không đủ để đặt cọc", "e", true, Page);
                        }
                        else
                        {
                            if (Amount < mustpay)
                                PJUtils.ShowMessageBoxSwAlert("Số tiền nhập vào không đủ để thanh toán", "e", true, Page);
                            else if (Amount == mustpay)
                            {
                                if (payment == 2)
                                {
                                    if (currentWallet >= Amount)
                                    {
                                        //Trừ tiền wallet, ghi lịch sử history pay wallet
                                        double walletleft = currentWallet - Amount;
                                        double payall = Amount + OCurrent_deposit;


                                        AccountController.updateWallet(uid, walletleft, currentDate, username_current);
                                        HistoryPayWalletController.Insert(uid, userDathang.Username, id, Amount, userDathang.Username + " đã thanh toán đơn hàng: " + id + ".",
                                            walletleft, 1, 3, currentDate, username_current);
                                        HistoryOrderChangeController.Insert(o.ID, user_currentID, username_current, username_current +
                                       " đã thanh toán đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                       + string.Format("{0:N0}", Amount) + "", 1, currentDate);

                                        MainOrderController.UpdateDeposit(id, uid, payall.ToString());
                                        if (o.DepostiDate == null)
                                        {
                                            MainOrderController.UpdateDepositDate(o.ID, currentDate);
                                        }
                                        MainOrderController.UpdateStatus(id, uid, status);
                                        PayOrderHistoryController.Insert(id, uid, status, Amount, payment, currentDate, username_current);

                                        var wh = WarehouseController.GetByID(Convert.ToInt32(o.ReceivePlace));
                                        if (wh != null)
                                        {
                                            var ExpectedDate = currentDate.AddDays(Convert.ToInt32(wh.ExpectedDate));
                                            MainOrderController.UpdateExpectedDate(o.ID, ExpectedDate);
                                        }

                                        PJUtils.ShowMessageBoxSwAlert("Xử lý thanh toán thành công", "s", true, Page);
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Số tiền trong ví của khách hàng đủ để thanh toán", "e", true, Page);
                                    }
                                }
                                else
                                {
                                    double payallorder = Amount + OCurrent_deposit;
                                    HistoryOrderChangeController.Insert(o.ID, user_currentID, username_current, username_current +
                                       " đã thanh toán và nhận hàng của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                       + string.Format("{0:N0}", payallorder) + "", 1, currentDate);

                                    MainOrderController.UpdateDeposit(id, uid, payallorder.ToString());
                                    if (o.PayDate == null)
                                    {
                                        MainOrderController.UpdatePayDate(o.ID, currentDate);
                                    }
                                    MainOrderController.UpdateStatus(id, uid, status);
                                    PayOrderHistoryController.Insert(id, uid, status, Amount, payment, currentDate, username_current);
                                    PayAllOrderHistoryController.Insert(id, Amount, uid, "", pContent1.Text, status, currentDate, username_current);
                                    PJUtils.ShowMessageBoxSwAlert("Xử lý thanh toán thành công", "s", true, Page);
                                }
                                //double deposit = o.Deposit.ToFloat(0);
                                //double payalll = deposit + mustpay;
                                //MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                                //MainOrderController.UpdateStatus(id, uid, status);
                                //PayOrderHistoryController.Insert(id, uid, status, Amount, payment, currentDate, username_current);
                                //PJUtils.ShowMessageBoxSwAlert("Xử lý thanh toán thành công", "s", true, Page);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Số tiền đã vượt mức cần phải thanh toán.", "e", true, Page);
                            }
                        }
                    }
                    else
                        PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
                }
                else
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
            }
            else
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            int id = ViewState["OID"].ToString().ToInt(0);
            if (id > 0)
            {
                Response.Redirect("/manager/OrderDetail.aspx?id=" + id);
            }
        }
    }
}
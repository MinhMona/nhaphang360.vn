using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class TransactionController
    {
        //Đặt cọc đơn hàng hộ
        public static int DepositAll(int userID, double wallet, DateTime currentDate, string userUserName, int orderID, int statusnew, int orderStatus, string amountdeposit, double custDeposit, string content, int typePayWallet, int tradeType, int typePayOrder)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        int statusOOld = Convert.ToInt32(orderStatus);
                        int status = statusnew;
                        //Cập nhật lại wallet user
                        var account = dbe.tbl_Account.Where(ac => ac.ID == userID).FirstOrDefault();
                        if (account != null)
                        {
                            account.Wallet = wallet;
                            account.ModifiedBy = userUserName;
                            account.ModifiedDate = currentDate;
                        }
                        //Cập nhật trạng thái, số tiền cọc của đơn hàng, ngày đặt cọc
                        var or = dbe.tbl_MainOder.Where(o => o.UID == userID && o.ID == orderID).FirstOrDefault();
                        if (or != null)
                        {
                            or.Status = status;
                            or.Deposit = amountdeposit;
                            or.DepostiDate = currentDate;
                        }
                        //var or2 = dbe.tbl_MainOder.Where(o => o.ID == orderID).FirstOrDefault();
                        //if (or2 != null)
                        //{
                        //    or2.DepostiDate = currentDate;
                        //}

                        tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                        a.UID = userID;
                        a.UserName = userUserName;
                        a.MainOrderID = orderID;
                        a.Amount = custDeposit;
                        a.HContent = content;
                        a.MoneyLeft = wallet;
                        a.Type = typePayWallet;
                        a.TradeType = tradeType;
                        a.CreatedDate = currentDate;
                        a.CreatedBy = userUserName;
                        dbe.tbl_HistoryPayWallet.Add(a);


                        tbl_PayOrderHistory a2 = new tbl_PayOrderHistory();
                        a2.MainOrderID = orderID;
                        a2.UID = userID;
                        a2.Status = status;
                        a2.Amount = custDeposit;
                        a2.Type = typePayOrder;
                        a2.CreatedDate = currentDate;
                        a2.CreatedBy = userUserName;
                        dbe.tbl_PayOrderHistory.Add(a2);

                        tbl_HistoryOrderChange h = new tbl_HistoryOrderChange();
                        h.MainOrderID = orderID;
                        h.UID = userID;
                        h.Username = userUserName;
                        h.HistoryContent = userUserName + " đã đổi trạng thái của đơn hàng ID là: " + orderID + ", từ: Chờ đặt cọc, sang: Đã đặt cọc.";
                        h.Type = 1;
                        h.CreatedDate = currentDate;
                        dbe.tbl_HistoryOrderChange.Add(h);

                        dbe.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                }
            }
        }
        //Thanh toán đơn hàng hộ
        public static int PayAll(int orderId, double wallet, int orderStatus, int userID, DateTime currentDate, string userName, double deposit, int orderChangeType, double moneyleft, int PayWalletType, int TradeType, int PayOrderType)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        double walletLeft = Math.Round(wallet - moneyleft, 0);
                        double payalll = Math.Round(deposit + moneyleft, 0);

                        string content = "đã đổi trạng thái của đơn hàng ID là: " + orderId + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.";
                        string contentPayWallet = userName + " đã thanh toán đơn hàng: " + orderId + ".";
                        int statusOOld = Convert.ToInt32(orderStatus);
                        int statusNew = 9;

                        var or = dbe.tbl_MainOder.Where(o => o.UID == userID && o.ID == orderId).FirstOrDefault();
                        if (or != null)
                        {
                            or.Status = statusNew;
                            or.Deposit = payalll.ToString();
                            or.PayDate = currentDate;
                        }

                        var a = dbe.tbl_Account.Where(ac => ac.ID == userID).FirstOrDefault();
                        if (a != null)
                        {
                            a.Wallet = walletLeft;
                            a.ModifiedBy = userName;
                            a.ModifiedDate = currentDate;
                        }

                        tbl_HistoryOrderChange h = new tbl_HistoryOrderChange();
                        h.MainOrderID = orderId;
                        h.UID = userID;
                        h.Username = userName;
                        h.HistoryContent = content;
                        h.Type = orderChangeType;
                        h.CreatedDate = currentDate;
                        dbe.tbl_HistoryOrderChange.Add(h);

                        tbl_HistoryPayWallet payWallet = new tbl_HistoryPayWallet();
                        payWallet.UID = userID;
                        payWallet.UserName = userName;
                        payWallet.MainOrderID = orderId;
                        payWallet.Amount = moneyleft;
                        payWallet.HContent = contentPayWallet;
                        payWallet.MoneyLeft = walletLeft;
                        payWallet.Type = PayWalletType;
                        payWallet.TradeType = TradeType;
                        payWallet.CreatedDate = currentDate;
                        payWallet.CreatedBy = userName;
                        dbe.tbl_HistoryPayWallet.Add(payWallet);

                        tbl_PayOrderHistory payOrderHistory = new tbl_PayOrderHistory();
                        payOrderHistory.MainOrderID = orderId;
                        payOrderHistory.UID = userID;
                        payOrderHistory.Status = statusNew;
                        payOrderHistory.Amount = moneyleft;
                        payOrderHistory.Type = PayOrderType;
                        payOrderHistory.CreatedDate = currentDate;
                        payOrderHistory.CreatedBy = userName;
                        dbe.tbl_PayOrderHistory.Add(payOrderHistory);
                        dbe.SaveChanges();

                        transaction.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
        //Thanh toán tiền thanh toán hộ tệ
        public static int PayMent(int UID, int orderID, double walletCYN_left, string userName, double TotalPrice, int type, int tradeType, string content, DateTime currentDate, int status)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        var acc = dbe.tbl_Account.Where(ac => ac.ID == UID).FirstOrDefault();
                        if (acc != null)
                        {
                            acc.WalletCYN = walletCYN_left;
                        }

                        tbl_HistoryPayWalletCYN payWalletCYN = new tbl_HistoryPayWalletCYN();
                        payWalletCYN.UID = UID;
                        payWalletCYN.UserName = userName;
                        payWalletCYN.Amount = TotalPrice;
                        payWalletCYN.MoneyLeft = walletCYN_left;
                        payWalletCYN.Type = type;
                        payWalletCYN.TradeType = tradeType;
                        payWalletCYN.Note = content;
                        payWalletCYN.CreatedDate = currentDate;
                        payWalletCYN.CreatedBy = userName;
                        dbe.tbl_HistoryPayWalletCYN.Add(payWalletCYN);

                        var o = dbe.tbl_PayHelp.Where(od => od.ID == orderID).FirstOrDefault();
                        if (o != null)
                        {
                            o.Status = status;
                            o.ModifiedDate = currentDate;
                            o.ModifiedBy = userName;
                        }

                        dbe.SaveChanges();
                        transaction.Commit();
                        return 1;

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }

            }

        }
        //Thanh toán tiền thanh toán hộ (tệ + vnđ)
        public static int PaymentPayhelp(int PayhelpID, int uid, string userName, double walletleft, double walletCYN_left, double amount, string content, int typeVND, int tradeTypeVND, int type, int tradeType, double TotalPrice, int status, DateTime currentDate)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        var acc = dbe.tbl_Account.Where(ac => ac.ID == uid).FirstOrDefault();
                        if (acc != null)
                        {
                            acc.Wallet = walletleft;
                            acc.WalletCYN = walletCYN_left;
                            acc.ModifiedBy = userName;
                            acc.ModifiedDate = currentDate;
                        }
                        //Lịch sử ví tiền vnđ
                        tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                        a.UID = uid;
                        a.UserName = userName;
                        a.MainOrderID = 0;
                        a.Amount = amount;
                        a.HContent = content;
                        a.MoneyLeft = walletleft;
                        a.Type = typeVND;
                        a.TradeType = tradeTypeVND;
                        a.CreatedDate = currentDate;
                        a.CreatedBy = userName;
                        dbe.tbl_HistoryPayWallet.Add(a);

                        //Lịch sử ví tiền tệ
                        tbl_HistoryPayWalletCYN payWalletCYN = new tbl_HistoryPayWalletCYN();
                        payWalletCYN.UID = uid;
                        payWalletCYN.UserName = userName;
                        payWalletCYN.Amount = TotalPrice;
                        payWalletCYN.MoneyLeft = walletCYN_left;
                        payWalletCYN.Type = type;
                        payWalletCYN.TradeType = tradeType;
                        payWalletCYN.Note = content;
                        payWalletCYN.CreatedDate = currentDate;
                        payWalletCYN.CreatedBy = userName;
                        dbe.tbl_HistoryPayWalletCYN.Add(payWalletCYN);
                        //Cập nhật trạng thái thanh toán hộ
                        var o = dbe.tbl_PayHelp.Where(od => od.ID == PayhelpID).FirstOrDefault();
                        if (o != null)
                        {
                            o.Status = status;
                            o.ModifiedDate = currentDate;
                            o.ModifiedBy = userName;
                        }

                        dbe.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }

            }
        }
        //Thanh toán tiền thanh toán hộ vnđ
        public static int PayhelpWallet(int PayhelpID, int Status, int uid, string userName, double walletleft, double amount, string content, int type, int tradeType, DateTime currentDate)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        var acc = dbe.tbl_Account.Where(ac => ac.ID == uid).FirstOrDefault();
                        if (acc != null)
                        {
                            acc.Wallet = walletleft;
                            acc.ModifiedBy = userName;
                            acc.ModifiedDate = currentDate;
                        }

                        tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                        a.UID = uid;
                        a.UserName = userName;
                        a.MainOrderID = 0;
                        a.Amount = amount;
                        a.HContent = content;
                        a.MoneyLeft = walletleft;
                        a.Type = type;
                        a.TradeType = tradeType;
                        a.CreatedDate = currentDate;
                        a.CreatedBy = userName;
                        dbe.tbl_HistoryPayWallet.Add(a);

                        //Cập nhật trạng thái thanh toán hộ
                        var o = dbe.tbl_PayHelp.Where(od => od.ID == PayhelpID).FirstOrDefault();
                        if (o != null)
                        {
                            o.Status = Status;
                            o.ModifiedDate = currentDate;
                            o.ModifiedBy = userName;
                        }

                        dbe.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }

            }
        }
        //Thanh toán tiền vận chuyển hộ
        public static int PayVanChuyenHo(int transOrderID, double totalPrice, int status, DateTime currentDate, string username, int uid, double walletLeft, int mainOrderID, double Amount, string content,
           int type, int typeTrade)
        {
            using (NHSTEntities dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                using (var transaction = dbe.Database.BeginTransaction())
                {
                    try
                    {
                        var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == transOrderID).FirstOrDefault();
                        if (p != null)
                        {
                            p.Deposited = totalPrice;
                            p.Status = status;
                            p.ModifiedBy = username;
                            p.ModifiedDate = currentDate;
                        }

                        var a = dbe.tbl_Account.Where(ac => ac.ID == uid).FirstOrDefault();
                        if (a != null)
                        {
                            a.Wallet = walletLeft;
                            a.ModifiedBy = username;
                            a.ModifiedDate = currentDate;
                        }

                        tbl_HistoryPayWallet payWallet = new tbl_HistoryPayWallet();
                        payWallet.UID = uid;
                        payWallet.UserName = username;
                        payWallet.MainOrderID = mainOrderID;
                        payWallet.Amount = Amount;
                        payWallet.HContent = content;
                        payWallet.MoneyLeft = walletLeft;
                        payWallet.Type = type;
                        payWallet.TradeType = typeTrade;
                        payWallet.TransportationOrderID = transOrderID;
                        payWallet.CreatedDate = currentDate;
                        payWallet.CreatedBy = username;
                        dbe.tbl_HistoryPayWallet.Add(payWallet);
                        dbe.SaveChanges();

                        transaction.Commit();
                        return 1;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
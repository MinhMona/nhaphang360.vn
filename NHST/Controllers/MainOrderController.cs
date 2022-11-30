using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;

namespace NHST.Controllers
{
    public class MainOrderController
    {
        #region CRUD
        public static string Insert(int UID, string ShopID, string ShopName, string Site, bool IsForward, string IsForwardPrice, bool IsFastDelivery, string IsFastDeliveryPrice, bool IsCheckProduct,
            string IsCheckProductPrice, bool IsPacked, string IsPackedPrice, bool IsFast, string IsFastPrice, string PriceVND, string PriceCNY, string FeeShipCN, string FeeBuyPro, string FeeWeight,
            string Note, string FullName, string Address, string Email, string Phone, int Status, string Deposit, string CurrentCNYVN, string TotalPriceVND,
            int SalerID, int DathangID, DateTime CreatedDate, int CreatedBy, string AmountDeposit, int OrderType, string FeeBuyProUser, string CurrencyReal)
        {
            using (var dbe = new NHSTEntities())
            {

                tbl_MainOder o = new tbl_MainOder();
                o.UID = UID;
                o.ShopID = ShopID;
                o.ShopName = ShopName;
                o.Site = Site;
                o.FeeBuyProUser = FeeBuyProUser;
                o.IsForward = IsForward;
                o.IsForwardPrice = IsForwardPrice;
                o.IsFastDelivery = IsFastDelivery;
                o.IsFastDeliveryPrice = IsFastDeliveryPrice;
                o.IsCheckProduct = IsCheckProduct;
                o.IsCheckProductPrice = IsCheckProductPrice;
                o.IsPacked = IsPacked;
                o.IsPackedPrice = IsPackedPrice;
                o.IsFast = IsFast;
                o.IsFastPrice = IsFastPrice;
                o.PriceVND = PriceVND;
                o.PriceCNY = PriceCNY;
                o.FeeShipCN = FeeShipCN;
                o.FeeBuyPro = FeeBuyPro;
                o.FeeWeight = FeeWeight;
                o.Note = Note;
                o.FullName = FullName;
                o.Address = Address;
                o.Email = Email;
                o.Phone = Phone;
                o.Status = Status;
                o.Deposit = Deposit;
                o.CurrentCNYVN = CurrentCNYVN;
                o.TotalPriceVND = TotalPriceVND;
                o.SalerID = SalerID;
                o.DathangID = DathangID;
                o.KhoTQID = 0;
                o.KhoVNID = 0;
                o.FeeShipCNToVN = "0";
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                o.IsHidden = false;
                o.IsUpdatePrice = false;
                o.AmountDeposit = AmountDeposit;
                o.OrderType = OrderType;
                o.CurrencyReal = CurrencyReal;               
                dbe.tbl_MainOder.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }       
        public static string UpdateStaff(int ID, int SalerID, int DathangID, int KhoTQID, int KhoVNID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.SalerID = SalerID;
                    or.DathangID = DathangID;
                    or.KhoTQID = KhoTQID;
                    or.KhoVNID = KhoVNID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static tbl_MainOder GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(x => x.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                return null;
            }
        }
        public static string UpdateExpectedDate(int ID, DateTime ExpectedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ExpectedDate = ExpectedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateYCG(int ID, bool IsGiaoHang)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsGiaohang = IsGiaoHang;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDoneSmallPackage(int ID, bool IsDoneSmallPackage)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsDoneSmallPackage = IsDoneSmallPackage;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateStatusByID(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDeposit(int ID, string Deposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeight(int ID, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateTotalFeeSupport(int ID, string TotalFeeSupport)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TotalFeeSupport = TotalFeeSupport;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateAmountDeposit(int ID, string AmountDeposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.AmountDeposit = AmountDeposit;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateNote(int ID, string Note)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Note = Note;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateCheckPro(int ID, bool IsCheckProduct)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsCheckProduct = IsCheckProduct;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsUpdatePrice(int ID, bool IsUpdatePrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsUpdatePrice = IsUpdatePrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateBaogia(int ID, bool IsCheckNotiPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsCheckNotiPrice = IsCheckNotiPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateListMVD(int ID, string BarCode, int QuantityMVD)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.BarCode = BarCode;
                    or.QuantityMVD = QuantityMVD;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsGiaohang(int ID, bool IsGiaohang)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsGiaohang = IsGiaohang;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsPacked(int ID, bool IsPacked)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsPacked = IsPacked;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeightDC(int ID, string Feeweightdc)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = Feeweightdc;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsFastDelivery(int ID, bool IsFastDelivery)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsFastDelivery = IsFastDelivery;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }


        public static string UpdateIsInsurrance(int ID, bool IsInsurrance)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsInsurrance = IsInsurrance;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateInsurranceMoney(int ID, string InsuranceMoney, string InsurancePercent)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.InsuranceMoney = InsuranceMoney;
                    or.InsurancePercent = InsurancePercent;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateCurrencyReal(int ID, string CurrencyReal)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.CurrencyReal = CurrencyReal;                   
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateOrderWeightCK(int ID, string OrderWeightCKS)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = OrderWeightCKS;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode(int ID, string OrderTransactionCode, string OrderTransactionCodeweight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode = OrderTransactionCode;
                    or.OrderTransactionCodeWeight = OrderTransactionCodeweight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode2(int ID, string OrderTransactionCode2, string OrderTransactionCodeweight2)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode2 = OrderTransactionCode2;
                    or.OrderTransactionCodeWeight2 = OrderTransactionCodeweight2;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode3(int ID, string OrderTransactionCode3, string OrderTransactionCodeweight3)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode3 = OrderTransactionCode3;
                    or.OrderTransactionCodeWeight3 = OrderTransactionCodeweight3;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode4(int ID, string OrderTransactionCode4, string OrderTransactionCodeweight4)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode4 = OrderTransactionCode4;
                    or.OrderTransactionCodeWeight4 = OrderTransactionCodeweight4;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode5(int ID, string OrderTransactionCode5, string OrderTransactionCodeweight5)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode5 = OrderTransactionCode5;
                    or.OrderTransactionCodeWeight5 = OrderTransactionCodeweight5;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDeposit(int ID, int UID, string Deposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateReceivePlace(int ID, int UID, string ReceivePlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ReceivePlace = ReceivePlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFromPlace(int ID, int UID, int FromPlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FromPlace = FromPlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFTS(int ID, int UID, int FromPlace, string ReceivePlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FromPlace = FromPlace;
                    or.ReceivePlace = ReceivePlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsCheckNotiPrice(int ID, int UID, bool IsCheckNotiPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsCheckNotiPrice = IsCheckNotiPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderType(int ID, int UID, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OrderType = OrderType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTQVNWeight(int ID, int UID, string TQVNWeight, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TQVNWeight;
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateMainOrderCode(int ID, int UID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.MainOrderCode = MainOrderCode;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateMainOrderCode_Thang(int ID, int UID, string MainOrderCode, int QuantityMDH)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.MainOrderCode = MainOrderCode;
                    or.QuantityMDH = QuantityMDH;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalPriceReal(int ID, string TotalPriceReal, string TotalPriceRealCYN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TotalPriceReal = TotalPriceReal;
                    or.TotalPriceRealCYN = TotalPriceRealCYN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateTimeline(int ID, string Timeline)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TimeLine = Timeline;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDepositDate(int ID, DateTime DepositDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DepostiDate = DepositDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDateBuy(int ID, DateTime DateBuy)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DateBuy = DateBuy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDateSite(int ID, DateTime DateSite)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DateSite = DateSite;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDateTQ(int ID, DateTime DateTQ)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DateTQ = DateTQ;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateDateVN(int ID, DateTime DateVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.DateVN = DateVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string Remove(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_MainOder.Where(x => x.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    db.tbl_MainOder.Remove(p);
                    db.SaveChanges();
                    return "ok";
                }
                return null;
            }
        }
        public static string UpdatePayDate(int ID, DateTime PayDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PayDate = PayDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateCompleteDate(int ID, DateTime CompleteDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.CompleteDate = CompleteDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateFee(int ID, string Deposit, string FeeShipCN, string FeeBuyPro, string FeeWeight,
           string IsCheckProductPrice, string IsPackedPrice, string IsFastDeliveryPrice, string TotalPriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    or.FeeShipCN = FeeShipCN;
                    or.FeeBuyPro = FeeBuyPro;
                    or.FeeWeight = FeeWeight;
                    or.IsCheckProductPrice = IsCheckProductPrice;
                    or.IsPackedPrice = IsPackedPrice;
                    or.IsFastDeliveryPrice = IsFastDeliveryPrice;
                    or.TotalPriceVND = TotalPriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeightCK(int ID, string FeeWeightCK)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(x => x.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = FeeWeightCK;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalWeight(int ID, string TotalWeight, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TotalWeight;
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateWeightStatusTQ(int ID, string TotalWeight, string OrderWeight, int Status, DateTime DateTQ)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TotalWeight;
                    or.OrderWeight = OrderWeight;
                    or.Status = Status;
                    or.DateTQ = DateTQ;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateWeightStatusVN(int ID, string TotalWeight, string OrderWeight, int Status, DateTime DateVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TotalWeight;
                    or.OrderWeight = OrderWeight;
                    or.Status = Status;
                    or.DateVN = DateVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeShipTQVN(int ID, string FeeShipTQVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeShipCNToVN = FeeShipTQVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateAutoCurrency(int ID, string CurrentCNYVN, string FeeBuyPro, string AmountDeposit, string TotalPriceVND, 
            string PriceCNY, string PriceVND, string InsurancePercent, string InsuranceMoney, string FeeBuyProUser)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.CurrentCNYVN = CurrentCNYVN;
                    or.FeeBuyPro = FeeBuyPro;
                    or.AmountDeposit = AmountDeposit;
                    or.TotalPriceVND = TotalPriceVND;
                    or.PriceCNY = PriceCNY;
                    or.PriceVND = PriceVND;
                    or.InsurancePercent = InsurancePercent;
                    or.InsuranceMoney = InsuranceMoney;
                    or.FeeBuyProUser = FeeBuyProUser;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateFeeWarehouse(int ID, double FeeInWareHouse)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.FeeInWareHouse = FeeInWareHouse;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceNotFee(int ID, string PriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceVND = PriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceCYN(int ID, string PriceCNY)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceCNY = PriceCNY;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsHiddenTrue(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.IsHidden = true;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion

        #region GetAll
        public static List<View_OrderListFilterWithStatusString> GetByUserInViewFilterWithStatusString(int RoleID, int OrderType, int StaffID,
            string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                if (RoleID != 1)
                {
                    List<View_OrderListFilterWithStatusString> lo = new List<View_OrderListFilterWithStatusString>();
                    List<View_OrderListFilterWithStatusString> losearch = new List<View_OrderListFilterWithStatusString>();
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status < 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status <= 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.SalerID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 9 && l.Status < 10 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            if (Type == 1)
                            {
                                var listos = GetMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else if (Type == 2)
                            {
                                var listos = GetSmallPackageMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else
                            {

                                losearch = lo.Where(o => o.MainOrderCode == searchtext).ToList();

                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                    return losearch;
                }
                return null;
            }
        }

        public static string SelectUIDByIDOrder(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    return ot.UID.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public static tbl_MainOder GetByMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (ot != null)
                {
                    return ot;
                }
                else
                {
                    return null;
                }
            }
        }
        public static tbl_MainOder GetByMainOrderCodeAndID(int MainOrderID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.ID == MainOrderID && o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (ot != null)
                {
                    return ot;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<tbl_MainOder> GetListByMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> ot = new List<tbl_MainOder>();
                ot = dbe.tbl_MainOder.Where(o => o.MainOrderCode == MainOrderCode).ToList();
                return ot;
            }
        }
        public static List<tbl_MainOder> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.OrderByDescending(o => o.ID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetByRoleID(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 3)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.Status < 5 && l.DathangID == StaffID).ToList();
                    else if (RoleID == 4)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5 && (l.KhoTQID == StaffID || l.KhoTQID == 0)).ToList();
                    else if (RoleID == 5)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 6 && (l.KhoVNID == StaffID || l.KhoVNID == 0)).ToList();
                    else if (RoleID == 6)
                        lo = dbe.tbl_MainOder.Where(l => l.SalerID == StaffID).ToList();
                    else if (RoleID == 7)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 7).ToList();
                    else
                    {
                        lo = dbe.tbl_MainOder.ToList();
                    }
                }
                return lo;
            }
        }
        public static List<View_OrderList> GetByUserInView(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderList> lo = new List<View_OrderList>();
                List<View_OrderList> losearch = new List<View_OrderList>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }

                }
                return lo;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter2(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    var findpackage = SmallPackageController.GetByMainOrderIDAndCode(item.ID, searchtext);
                                    if (findpackage.Count > 0)
                                    {
                                        losearch.Add(item);
                                    }
                                    //if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    //{
                                    //    if (item.OrderTransactionCode.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    //{
                                    //    if (item.OrderTransactionCode2.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    //{
                                    //    if (item.OrderTransactionCode3.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    //{
                                    //    if (item.OrderTransactionCode4.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    //{
                                    //    if (item.OrderTransactionCode5.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                }
                            }
                        }
                        else if (Type == 3)
                        {
                            foreach (var item in lo)
                            {
                                var findpackage = SmallPackageController.GetByMainOrderID(item.ID);
                                if (findpackage.Count == 0)
                                {
                                    losearch.Add(item);
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListDamuahang> GetByUserInViewFilterStatus5()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListDamuahang> lo = new List<View_OrderListDamuahang>();
                lo = dbe.View_OrderListDamuahang.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoTQ> GetByUserInViewFilterStatus6()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoTQ> lo = new List<View_OrderListKhoTQ>();
                lo = dbe.View_OrderListKhoTQ.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoVN> GetByUserInViewFilterStatus7()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoVN> lo = new List<View_OrderListKhoVN>();
                lo = dbe.View_OrderListKhoVN.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_Orderlistwithstatus> GetByUserInViewFilterStatus(int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_Orderlistwithstatus> lo = new List<View_Orderlistwithstatus>();
                lo = dbe.View_Orderlistwithstatus.Where(l => l.Status == status).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListFilterYCGiao> GetByUserInViewFilterYCG(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilterYCGiao> lo = new List<View_OrderListFilterYCGiao>();
                List<View_OrderListFilterYCGiao> losearch = new List<View_OrderListFilterYCGiao>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<tbl_MainOder> GetByUser(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                List<tbl_MainOder> losearch = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<tbl_MainOder> GetSuccessByCustomer(int customerID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(l => l.Status == 10 && l.UID == customerID).ToList();
                return lo;
            }
        }
        public static List<tbl_MainOder> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();

                var alllist = dbe.tbl_MainOder.Where(t => t.Status > 1).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                if (alllist.Count > 0)
                {
                    if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                    {
                        lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                    }
                    else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                    {
                        lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                    }
                    else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                    {
                        lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                    }
                    else
                    {
                        lo = alllist;
                    }
                }

                return lo;
            }
        }
        public static List<tbl_MainOder> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetAllByUIDNotHidden(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.IsHidden == false).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper(int UID, int status, string fd, string td, int OrderType)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where UID = " + UID + " AND mo.OrderType = " + OrderType + " ";

            if (status >= 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<mainorder> GetAllByUIDOrderCodeNotHidden_SqlHelper(int UID, int type)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderTransactionCode,mo.OrderTransactionCode2,mo.OrderTransactionCode3,mo.OrderTransactionCode4,mo.OrderTransactionCode5, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, o.quantityPro";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham, sum(CONVERT(float, quantity)) as quantityPro FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where UID = " + UID + " AND mo.OrderType = " + type + " ";
            //sql += " where UID = " + UID + " and IsHidden = 0 ";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["quantityPro"] != DBNull.Value)
                    entity.quantityPro = reader["quantityPro"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelper_WithPaging(int userID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham,";
            sql += "  CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"bg-blue\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"bg-yellow\">Đã nhập</span>' ELSE N'<span class=\"bg-red\">Chưa nhập</span>' END AS hasSmallpackage";
            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " LEFT OUTER JOIN  (SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "        Where UID = " + userID + " ";
            sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;

                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelper_WithNoPaging(int userID)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham,";
            sql += "  CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"bg-blue\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"bg-yellow\">Đã nhập</span>' ELSE N'<span class=\"bg-red\">Chưa nhập</span>' END AS hasSmallpackage";
            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " LEFT OUTER JOIN  (SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "        Where UID = " + userID + " ";
            sql += " ORDER BY mo.ID DESC";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = reader["CreatedDate"].ToString();
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;

                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserInSQLHelper_nottextnottypeWithstatus(int RoleID, int OrderType, int StaffID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham,";
                sql += "  CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"bg-blue\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"bg-yellow\">Đã nhập</span>' ELSE N'<span class=\"bg-red\">Chưa nhập</span>' END AS hasSmallpackage";
                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
                sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
                sql += " LEFT OUTER JOIN  (SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
                sql += "        Where UID > 0 ";
                sql += "    AND mo.OrderType  = " + OrderType + "";
                if (RoleID == 3)
                {
                    sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
                }
                else if (RoleID == 4)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status < 7";
                }
                else if (RoleID == 5)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status <= 7";
                }
                else if (RoleID == 6)
                {
                    sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
                }
                else if (RoleID == 8)
                {
                    sql += "    AND mo.Status >= 9 and mo.Status < 10";
                }
                else if (RoleID == 7)
                {
                    sql += "    AND mo.Status >= 2";
                }
                sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    if (reader["saler"] != DBNull.Value)
                        entity.saler = reader["saler"].ToString();
                    if (reader["dathang"] != DBNull.Value)
                        entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;

                    if (reader["hasSmallpackage"] != DBNull.Value)
                        entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                    list.Add(entity);
                }
                reader.Close();
            }
            return list;
        }
        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper1(int UID, int status, string fd, string td)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit, mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, o.anhsanpham";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where UID = " + UID + " and IsHidden = 0 ";

            if (status >= 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static tbl_MainOder GetAllByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }

        public static List<tbl_MainOder> GetByUIDAndStatus(int UID, int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status == status).ToList();
                return lo;

            }
        }

        public static List<tbl_MainOder> GetAllStatus(int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.Status == status).ToList();
                return lo;

            }
        }

        public static List<tbl_MainOder> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status > 2 && o.Status <= 9).ToList();
                return lo;

            }
        }

        public static tbl_MainOder GetAllByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static int getOrderByRoleIDStaffID_SQL(int RoleID, int StaffID)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder as mo";
            sql += "        Where UID > 0";
            if (RoleID == 3)
            {
                sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status >= 5 and mo.Status < 7";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status >= 5 and mo.Status <= 7";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
            }
            else if (RoleID == 8)
            {
                sql += "    AND mo.Status >= 9 and mo.Status < 10";
            }
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status >= 2";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static int getOrderByUID_SQL(int UID)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder as mo";
            sql += "        Where UID = " + UID + "";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static List<MainOrderID> GetMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_order where title_origin like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public static List<MainOrderID> GetSmallPackageMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_SmallPackage where OrderTransactionCode like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public class MainOrderID
        {
            public int ID { get; set; }
        }
        public class mainorder
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public string AmountDeposit { get; set; }
            public DateTime CreatedDate { get; set; }
            public int Status { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string anhsanpham { get; set; }
            public bool IsGiaohang { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public int OrderType { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }
            public string quantityPro { get; set; }

            public string Created { get; set; }
            public string DateSite { get; set; }
            public string DepostiDate { get; set; }
            public string DateBuy { get; set; }
            public string DateTQ { get; set; }
            public string DateVN { get; set; }
            public string DatePay { get; set; }
            public string CompleteDate { get; set; }
            public string FeeInWareHouse { get; set; }

            public int SalerID { get; set; }
            public int DathangID { get; set; }
            public int TotalLink { get; set; }
        }
        public class OrderGetSQL
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string anhsanpham { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public int OrderType { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }

            public string Uname { get; set; }
            public string dathang { get; set; }
            public string saler { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
            public bool IsDoneSmallPackage { get; set; }

            public string Currency { get; set; }

            public List<string> listMainOrderCode { get; set; }

            public string Created { get; set; }
            public string DepostiDate { get; set; }
            public string DateBuy { get; set; }
            public string DateTQ { get; set; }
            public string DateVN { get; set; }
            public string DatePay { get; set; }
            public string DateSite { get; set; }
            public string CompleteDate { get; set; }
            public string PriceVND { get; set; }
            public string BarCode { get; set; }
            public string MainOrderCode { get; set; }
            public int QuantityMVD { get; set; }
            public int QuantityMDH { get; set; }

            public int SalerID { get; set; }
            public int DathangID { get; set; }
        }
        #endregion


        #region New
        public static List<tbl_MainOder> GetAllByUIDAndOrderType(int UID, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.OrderType == OrderType && o.IsHidden != true).ToList();
                return lo;
            }
        }

        public static List<mainorder> GetAllByUIDNotHidden_SqlHelperNew(int UID, string search, int typesearch, int status, string fd, string td, int OrderType, int page)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.FeeInWareHouse, mo.CreatedDate, mo.DepostiDate, mo.DateBuy, mo.DateTQ, mo.DateVN,mo.PayDate,mo.CompleteDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, TotalLink, mo.DateSite ";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";           
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += "LEFT OUTER JOIN(SELECT count(*) AS TotalLink, MainOrderID  FROM tbl_Order AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += " where UID = " + UID + " AND mo.OrderType = " + OrderType + " ";

            if (status >= 0)
                sql += " AND Status = " + status;

            if (typesearch != 0)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (typesearch == 1)
                    {
                        sql += " AND ID=" + search + "";
                    }
                    else if (typesearch == 3)
                    {
                        sql += " AND site like N'%" + search + "%'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(fd))
            {
                //var df = Convert.ToDateTime(fd).Date.ToString("dd-MM-yyyy HH:mm:ss");
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                //var dt = Convert.ToDateTime(td).Date.ToString("dd-MM-yyyy HH:mm:ss");
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc OFFSET (" + page + "*15) ROWS FETCH NEXT 15 ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                if (typesearch != 0)
                {
                    if (typesearch == 2)
                    {
                        int mainorderID = 0;
                        if (reader["ID"] != DBNull.Value)
                            mainorderID = reader["ID"].ToString().ToInt(0);
                        var orders = OrderController.GetByMainOrderIDAndBrand(mainorderID, search);
                        if (orders.Count > 0)
                        {
                            var entity = new mainorder();
                            entity.STT = i;
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = reader["ID"].ToString().ToInt(0);
                            if (reader["TotalLink"] != DBNull.Value)
                                entity.TotalLink = reader["TotalLink"].ToString().ToInt(0);
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["AmountDeposit"] != DBNull.Value)
                                entity.AmountDeposit = reader["AmountDeposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                            if (reader["Status"] != DBNull.Value)
                                entity.Status = reader["Status"].ToString().ToInt();
                            if (reader["shopname"] != DBNull.Value)
                                entity.ShopName = reader["shopname"].ToString();
                            if (reader["site"] != DBNull.Value)
                                entity.Site = reader["site"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["IsGiaohang"] != DBNull.Value)
                                entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                            else
                                entity.IsGiaohang = false;
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                            else
                                entity.IsCheckNotiPrice = false;

                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.Created = "<p class=\"s-txt no-wrap\">Lên đơn: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " </p>";

                            if (reader["DepostiDate"] != DBNull.Value)
                                entity.DepostiDate = "<p class=\"s-txt no-wrap\">Đặt cọc: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " </p>";

                            if (reader["DateBuy"] != DBNull.Value)
                                entity.DateBuy = "<p class=\"s-txt no-wrap\">Đặt hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " </p>";

                            if (reader["DateSite"] != DBNull.Value)
                                entity.DateSite = "<p class=\"s-txt no-wrap\">Shop phát hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateSite"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateSite"].ToString())) + " </p>";

                            if (reader["DateTQ"] != DBNull.Value)
                                entity.DateTQ = "<p class=\"s-txt no-wrap\">Đã về kho TQ: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " </p>";

                            if (reader["DateVN"] != DBNull.Value)
                                entity.DateVN = "<p class=\"s-txt no-wrap\">Đã về kho VN: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateVN"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateVN"].ToString())) + " </p>";

                            if (reader["PayDate"] != DBNull.Value)
                                entity.DatePay = "<p class=\"s-txt no-wrap\">Khách thanh toán: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["PayDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["PayDate"].ToString())) + " </p>";

                            if (reader["CompleteDate"] != DBNull.Value)
                                entity.CompleteDate = "<p class=\"s-txt no-wrap\">Hoàn thành: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " </p>";

                            if (reader["FeeInWareHouse"] != DBNull.Value)
                                entity.FeeInWareHouse = reader["FeeInWareHouse"].ToString();

                            i++;
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        var entity = new mainorder();
                        entity.STT = i;
                        if (reader["ID"] != DBNull.Value)
                            entity.ID = reader["ID"].ToString().ToInt(0);
                        if (reader["TotalLink"] != DBNull.Value)
                            entity.TotalLink = reader["TotalLink"].ToString().ToInt(0);
                        if (reader["TotalPriceVND"] != DBNull.Value)
                            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                        if (reader["Deposit"] != DBNull.Value)
                            entity.Deposit = reader["Deposit"].ToString();
                        if (reader["AmountDeposit"] != DBNull.Value)
                            entity.AmountDeposit = reader["AmountDeposit"].ToString();
                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        if (reader["Status"] != DBNull.Value)
                            entity.Status = reader["Status"].ToString().ToInt();
                        if (reader["shopname"] != DBNull.Value)
                            entity.ShopName = reader["shopname"].ToString();
                        if (reader["site"] != DBNull.Value)
                            entity.Site = reader["site"].ToString();
                        if (reader["anhsanpham"] != DBNull.Value)
                            entity.anhsanpham = reader["anhsanpham"].ToString();
                        if (reader["IsGiaohang"] != DBNull.Value)
                            entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                        else
                            entity.IsGiaohang = false;
                        if (reader["OrderType"] != DBNull.Value)
                            entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                        if (reader["IsCheckNotiPrice"] != DBNull.Value)
                            entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                        else
                            entity.IsCheckNotiPrice = false;

                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.Created = "<p class=\"s-txt no-wrap\">Lên đơn: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " </p>";

                        if (reader["DepostiDate"] != DBNull.Value)
                            entity.DepostiDate = "<p class=\"s-txt no-wrap\">Đặt cọc: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " </p>";

                        if (reader["DateBuy"] != DBNull.Value)
                            entity.DateBuy = "<p class=\"s-txt no-wrap\">Đặt hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " </p>";

                        if (reader["DateSite"] != DBNull.Value)
                            entity.DateSite = "<p class=\"s-txt no-wrap\">Shop phát hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateSite"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateSite"].ToString())) + " </p>";

                        if (reader["DateTQ"] != DBNull.Value)
                            entity.DateTQ = "<p class=\"s-txt no-wrap\">Đã về kho TQ: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " </p>";

                        if (reader["DateVN"] != DBNull.Value)
                            entity.DateVN = "<p class=\"s-txt no-wrap\">Đã về kho VN: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateVN"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateVN"].ToString())) + " </p>";

                        if (reader["PayDate"] != DBNull.Value)
                            entity.DatePay = "<p class=\"s-txt no-wrap\">Khách thanh toán: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["PayDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["PayDate"].ToString())) + " </p>";

                        if (reader["CompleteDate"] != DBNull.Value)
                            entity.CompleteDate = "<p class=\"s-txt no-wrap\">Hoàn thành: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " </p>";

                        if (reader["FeeInWareHouse"] != DBNull.Value)
                            entity.FeeInWareHouse = reader["FeeInWareHouse"].ToString();

                        i++;
                        list.Add(entity);
                    }
                }
                else
                {
                    var entity = new mainorder();
                    entity.STT = i;
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = reader["ID"].ToString().ToInt(0);
                    if (reader["TotalLink"] != DBNull.Value)
                        entity.TotalLink = reader["TotalLink"].ToString().ToInt(0);
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["AmountDeposit"] != DBNull.Value)
                        entity.AmountDeposit = reader["AmountDeposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    if (reader["Status"] != DBNull.Value)
                        entity.Status = reader["Status"].ToString().ToInt();
                    if (reader["shopname"] != DBNull.Value)
                        entity.ShopName = reader["shopname"].ToString();
                    if (reader["site"] != DBNull.Value)
                        entity.Site = reader["site"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["IsGiaohang"] != DBNull.Value)
                        entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                    else
                        entity.IsGiaohang = false;
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                    else
                        entity.IsCheckNotiPrice = false;

                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.Created = "<p class=\"s-txt no-wrap\">Lên đơn: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " </p>";

                    if (reader["DepostiDate"] != DBNull.Value)
                        entity.DepostiDate = "<p class=\"s-txt no-wrap\">Đặt cọc: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " </p>";

                    if (reader["DateBuy"] != DBNull.Value)
                        entity.DateBuy = "<p class=\"s-txt no-wrap\">Đặt hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " </p>";

                    if (reader["DateSite"] != DBNull.Value)
                        entity.DateSite = "<p class=\"s-txt no-wrap\">Shop phát hàng: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateSite"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateSite"].ToString())) + " </p>";

                    if (reader["DateTQ"] != DBNull.Value)
                        entity.DateTQ = "<p class=\"s-txt no-wrap\">Đã về kho TQ: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " </p>";

                    if (reader["DateVN"] != DBNull.Value)
                        entity.DateVN = "<p class=\"s-txt no-wrap\">Đã về kho VN: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateVN"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateVN"].ToString())) + " </p>";

                    if (reader["PayDate"] != DBNull.Value)
                        entity.DatePay = "<p class=\"s-txt no-wrap\">Thanh toán: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["PayDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["PayDate"].ToString())) + " </p>";

                    if (reader["CompleteDate"] != DBNull.Value)
                        entity.CompleteDate = "<p class=\"s-txt no-wrap\">Hoàn thành: " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " </p>";

                    if (reader["FeeInWareHouse"] != DBNull.Value)
                        entity.FeeInWareHouse = reader["FeeInWareHouse"].ToString();

                    i++;
                    list.Add(entity);
                }

            }
            reader.Close();
            return list;
        }

        public static int GetTotalItem(int UID, int status, string fd, string td, int OrderType)
        {
            var sql = @"select Count(*) as Total from tbl_MainOder";
            sql += " where UID = " + UID + " And OrderType= " + OrderType + " ";

            if (status >= 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fd))
            {
                //var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                //var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int total = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    total = reader["Total"].ToString().ToInt();
            }
            return total;
        }


        public static List<tbl_MainOder> GetByCustomerAndStatus(int customerID, int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(l => l.Status == status && l.UID == customerID).ToList();
                return lo;
            }
        }


        public static List<OrderGetSQL> GetByUserInSQLHelperWithFilter(int RoleID, int OrderType, int StaffID,
            string searchtext, int Type, string fd, string td, double priceFrom, double priceTo,
            bool isNotCode)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, sm1.totalSmallPackagesWithSearchText, ofi.totalOrderSearch, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%" + searchtext + "%') sm1 ON sm1.MainOrderID = mo.ID and totalSmallPackagesWithSearchText = 1 LEFT OUTER JOIN";
                sql += " (SELECT MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%" + searchtext + "%') ofi ON ofi.MainOrderID = mo.ID and totalOrderSearch = 1 LEFT OUTER JOIN";
                sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages=1 LEFT OUTER JOIN";
                sql += " (SELECT image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

                sql += "        Where UID > 0 ";
                sql += "    AND mo.OrderType  = " + OrderType + "";
                if (!string.IsNullOrEmpty(searchtext))
                {
                    if (Type == 3)
                    {
                        sql += "  AND mo.Mainordercode like N'%" + searchtext + "%'";
                    }
                }
                if (RoleID == 3)
                {
                    sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
                }
                else if (RoleID == 4)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status < 7";
                }
                else if (RoleID == 5)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status <= 7";
                }
                else if (RoleID == 6)
                {
                    sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
                }
                else if (RoleID == 8)
                {
                    sql += "    AND mo.Status >= 9 and mo.Status < 10";
                }
                else if (RoleID == 7)
                {
                    sql += "    AND mo.Status >= 2";
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                if (priceFrom > 0)
                {
                    sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
                }
                if (priceTo > 0)
                {
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
                }
                if (isNotCode == true)
                {
                    sql += " AND totalSmallPackages is null";
                }
                sql += " ORDER BY mo.ID DESC";
                //sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(searchtext))
                    {
                        int totalOrderSearch = 0;
                        if (reader["totalOrderSearch"] != DBNull.Value)
                            totalOrderSearch = reader["totalOrderSearch"].ToString().ToInt(0);

                        int totalSmallPackagesWithSearchText = 0;
                        if (reader["totalSmallPackagesWithSearchText"] != DBNull.Value)
                            totalSmallPackagesWithSearchText = reader["totalSmallPackagesWithSearchText"].ToString().ToInt(0);

                        if (Type == 1)
                        {
                            if (totalOrderSearch > 0)
                            {
                                int MainOrderID = reader["ID"].ToString().ToInt(0);
                                var entity = new OrderGetSQL();
                                if (reader["ID"] != DBNull.Value)
                                    entity.ID = MainOrderID;
                                if (reader["TotalPriceVND"] != DBNull.Value)
                                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                                if (reader["Deposit"] != DBNull.Value)
                                    entity.Deposit = reader["Deposit"].ToString();
                                if (reader["CreatedDate"] != DBNull.Value)
                                    entity.CreatedDate = reader["CreatedDate"].ToString();
                                if (reader["Status"] != DBNull.Value)
                                {
                                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                                }
                                if (reader["statusstring"] != DBNull.Value)
                                {
                                    entity.statusstring = reader["statusstring"].ToString();
                                }
                                if (reader["OrderTransactionCode"] != DBNull.Value)
                                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                                if (reader["OrderTransactionCode2"] != DBNull.Value)
                                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                                if (reader["OrderTransactionCode3"] != DBNull.Value)
                                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                                if (reader["OrderTransactionCode4"] != DBNull.Value)
                                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                                if (reader["OrderTransactionCode5"] != DBNull.Value)
                                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                                if (reader["Uname"] != DBNull.Value)
                                    entity.Uname = reader["Uname"].ToString();
                                if (reader["saler"] != DBNull.Value)
                                    entity.saler = reader["saler"].ToString();
                                if (reader["dathang"] != DBNull.Value)
                                    entity.dathang = reader["dathang"].ToString();
                                if (reader["anhsanpham"] != DBNull.Value)
                                    entity.anhsanpham = reader["anhsanpham"].ToString();
                                if (reader["OrderType"] != DBNull.Value)
                                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                                else
                                    entity.IsCheckNotiPrice = false;
                                list.Add(entity);
                            }
                        }
                        else if (Type == 2)
                        {
                            if (totalSmallPackagesWithSearchText > 0)
                            {
                                int MainOrderID = reader["ID"].ToString().ToInt(0);
                                var entity = new OrderGetSQL();
                                if (reader["ID"] != DBNull.Value)
                                    entity.ID = MainOrderID;
                                if (reader["TotalPriceVND"] != DBNull.Value)
                                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                                if (reader["Deposit"] != DBNull.Value)
                                    entity.Deposit = reader["Deposit"].ToString();
                                if (reader["CreatedDate"] != DBNull.Value)
                                    entity.CreatedDate = reader["CreatedDate"].ToString();
                                if (reader["Status"] != DBNull.Value)
                                {
                                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                                }
                                if (reader["statusstring"] != DBNull.Value)
                                {
                                    entity.statusstring = reader["statusstring"].ToString();
                                }
                                if (reader["OrderTransactionCode"] != DBNull.Value)
                                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                                if (reader["OrderTransactionCode2"] != DBNull.Value)
                                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                                if (reader["OrderTransactionCode3"] != DBNull.Value)
                                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                                if (reader["OrderTransactionCode4"] != DBNull.Value)
                                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                                if (reader["OrderTransactionCode5"] != DBNull.Value)
                                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                                if (reader["Uname"] != DBNull.Value)
                                    entity.Uname = reader["Uname"].ToString();
                                if (reader["saler"] != DBNull.Value)
                                    entity.saler = reader["saler"].ToString();
                                if (reader["dathang"] != DBNull.Value)
                                    entity.dathang = reader["dathang"].ToString();
                                if (reader["anhsanpham"] != DBNull.Value)
                                    entity.anhsanpham = reader["anhsanpham"].ToString();
                                if (reader["OrderType"] != DBNull.Value)
                                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                                else
                                    entity.IsCheckNotiPrice = false;
                                list.Add(entity);
                            }
                        }
                        else
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        int MainOrderID = reader["ID"].ToString().ToInt(0);
                        var entity = new OrderGetSQL();
                        if (reader["ID"] != DBNull.Value)
                            entity.ID = MainOrderID;
                        if (reader["TotalPriceVND"] != DBNull.Value)
                            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                        if (reader["Deposit"] != DBNull.Value)
                            entity.Deposit = reader["Deposit"].ToString();
                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.CreatedDate = reader["CreatedDate"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        }
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                        if (reader["OrderTransactionCode"] != DBNull.Value)
                            entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                        if (reader["OrderTransactionCode2"] != DBNull.Value)
                            entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                        if (reader["OrderTransactionCode3"] != DBNull.Value)
                            entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                        if (reader["OrderTransactionCode4"] != DBNull.Value)
                            entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                        if (reader["OrderTransactionCode5"] != DBNull.Value)
                            entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                        if (reader["Uname"] != DBNull.Value)
                            entity.Uname = reader["Uname"].ToString();
                        if (reader["saler"] != DBNull.Value)
                            entity.saler = reader["saler"].ToString();
                        if (reader["dathang"] != DBNull.Value)
                            entity.dathang = reader["dathang"].ToString();
                        if (reader["anhsanpham"] != DBNull.Value)
                            entity.anhsanpham = reader["anhsanpham"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                        if (reader["IsCheckNotiPrice"] != DBNull.Value)
                            entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                        else
                            entity.IsCheckNotiPrice = false;
                        list.Add(entity);
                    }
                }
                reader.Close();
            }
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilter(int UID,
            string searchtext, int Type, string fd, string td, double priceFrom, double priceTo,
            bool isNotCode)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
            sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
            sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
            sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
            sql += "        END AS statusstring, mo.DathangID, ";
            sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
            sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, sm1.totalSmallPackagesWithSearchText, ofi.totalOrderSearch, ";
            sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

            sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
            sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%" + searchtext + "%') sm1 ON sm1.MainOrderID = mo.ID and totalSmallPackagesWithSearchText = 1 LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%" + searchtext + "%') ofi ON ofi.MainOrderID = mo.ID and totalOrderSearch = 1 LEFT OUTER JOIN";
            sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages=1 LEFT OUTER JOIN";
            sql += " (SELECT image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

            sql += "        Where UID = " + UID + " ";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND mo.Mainordercode like N'%" + searchtext + "%'";
                }
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            if (priceFrom > 0)
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (priceTo > 0)
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND totalSmallPackages is null";
            }
            sql += " ORDER BY mo.ID DESC";
            //sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(searchtext))
                {
                    int totalOrderSearch = 0;
                    if (reader["totalOrderSearch"] != DBNull.Value)
                        totalOrderSearch = reader["totalOrderSearch"].ToString().ToInt(0);

                    int totalSmallPackagesWithSearchText = 0;
                    if (reader["totalSmallPackagesWithSearchText"] != DBNull.Value)
                        totalSmallPackagesWithSearchText = reader["totalSmallPackagesWithSearchText"].ToString().ToInt(0);

                    if (Type == 1)
                    {
                        if (totalOrderSearch > 0)
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            list.Add(entity);
                        }
                    }
                    else if (Type == 2)
                    {
                        if (totalSmallPackagesWithSearchText > 0)
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        int MainOrderID = reader["ID"].ToString().ToInt(0);
                        var entity = new OrderGetSQL();
                        if (reader["ID"] != DBNull.Value)
                            entity.ID = MainOrderID;
                        if (reader["TotalPriceVND"] != DBNull.Value)
                            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                        if (reader["Deposit"] != DBNull.Value)
                            entity.Deposit = reader["Deposit"].ToString();
                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.CreatedDate = reader["CreatedDate"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        }
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                        if (reader["OrderTransactionCode"] != DBNull.Value)
                            entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                        if (reader["OrderTransactionCode2"] != DBNull.Value)
                            entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                        if (reader["OrderTransactionCode3"] != DBNull.Value)
                            entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                        if (reader["OrderTransactionCode4"] != DBNull.Value)
                            entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                        if (reader["OrderTransactionCode5"] != DBNull.Value)
                            entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                        if (reader["Uname"] != DBNull.Value)
                            entity.Uname = reader["Uname"].ToString();
                        if (reader["saler"] != DBNull.Value)
                            entity.saler = reader["saler"].ToString();
                        if (reader["dathang"] != DBNull.Value)
                            entity.dathang = reader["dathang"].ToString();
                        if (reader["anhsanpham"] != DBNull.Value)
                            entity.anhsanpham = reader["anhsanpham"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                        if (reader["IsCheckNotiPrice"] != DBNull.Value)
                            entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                        else
                            entity.IsCheckNotiPrice = false;
                        list.Add(entity);
                    }
                }
                else
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    if (reader["saler"] != DBNull.Value)
                        entity.saler = reader["saler"].ToString();
                    if (reader["dathang"] != DBNull.Value)
                        entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;
                    list.Add(entity);
                }
            }
            reader.Close();
            return list;
        }
        public static string Report_TotalItem(int pricefrom, int priceto)
        {
            var sql = @"select Count(*) as Total from tbl_Account as ac";
            sql += " left outer join(select Sum(CONVERT(numeric(18, 2), TotalPriceVND)) as Total, UID from tbl_MainOder group by UID) as mo ON mo.UID = ac.ID ";
            sql += " where ac.RoleID = 1 and mo.Total > " + pricefrom + " and mo.Total < " + priceto + " ";

            string Total = "0";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    Total = reader["Total"].ToString();
            }

            reader.Close();
            return Total;
        }

        public static double GetTotalPrice(int UID, int status, string PriceType, int orderType)
        {
            var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            sql += "from tbl_MainOder ";
            sql += "where UID=" + UID.ToString() + " AND STATUS=" + status.ToString() + " And OrderType=" + orderType.ToString();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }

        public static List<tbl_MainOder> GetAllDateToDateNotPage(string fd, string td)
        {
            var sql = @"select * ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where OrderType Like N'%%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_MainOder> list = new List<tbl_MainOder>();
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["ShopID"] != DBNull.Value)
                    entity.ShopID = reader["ShopID"].ToString();
                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.TotalPriceReal = "0";
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();

                entity.Deposit = "0";
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["ShopName"] != DBNull.Value)
                    entity.ShopName = reader["ShopName"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_MainOder> GetAllDateToDate(string fd, string td, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where OrderType Like N'%%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " ORDER BY CreatedDate DESC, Status desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_MainOder> list = new List<tbl_MainOder>();
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["ShopID"] != DBNull.Value)
                    entity.ShopID = reader["ShopID"].ToString();
                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.TotalPriceReal = "0";
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();

                entity.Deposit = "0";
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["ShopName"] != DBNull.Value)
                    entity.ShopName = reader["ShopName"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static int GetTotalDateToDate(string fd, string td, string st)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where OrderType Like N'%%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += "And Status=" + st + " ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);

            }
            reader.Close();
            return a;
        }

        public static double GetTotalPriceDateToDate(string fd, string td, string col)
        {
            var sql = @"select Total = COUNT(*) ";
            if (!string.IsNullOrEmpty(col))
                sql += ", TotalPrice=Sum(CAST(" + col + " as float)) ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where OrderType Like N'%%' ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double a = 0;
            while (reader.Read())
            {
                if (reader["TotalPrice"] != DBNull.Value)
                    a = reader["TotalPrice"].ToString().ToFloat(0);
            }
            reader.Close();
            return a;
        }

        public static List<tbl_MainOder> GetBuyProBySQLNotPage(string fd, string td)
        {
            var sql = @"select * ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where Status > = 5 ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " ORDER BY ID DESC, Status desc ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_MainOder> list = new List<tbl_MainOder>();
            while (reader.Read())
            {
                var entity = new tbl_MainOder();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["ShopID"] != DBNull.Value)
                    entity.ShopID = reader["ShopID"].ToString();

                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.TotalPriceReal = "0";
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();

                entity.TotalPriceRealCYN = "0";
                if (reader["TotalPriceRealCYN"] != DBNull.Value)
                    entity.TotalPriceRealCYN = reader["TotalPriceRealCYN"].ToString();

                entity.Deposit = "0";
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                entity.CurrencyReal = "0";
                if (reader["CurrencyReal"] != DBNull.Value)
                    entity.CurrencyReal = reader["CurrencyReal"].ToString();

                entity.CurrentCNYVN = "0";
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();

                entity.PriceVND = "0";
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();

                entity.FeeShipCN = "0";
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();

                entity.FeeWeight = "0";
                if (reader["FeeWeight"] != DBNull.Value)
                    entity.FeeWeight = reader["FeeWeight"].ToString();

                entity.FeeBuyPro = "0";
                if (reader["FeeBuyPro"] != DBNull.Value)
                    entity.FeeBuyPro = reader["FeeBuyPro"].ToString();

                entity.IsCheckProductPrice = "0";
                if (reader["IsCheckProductPrice"] != DBNull.Value)
                    entity.IsCheckProductPrice = reader["IsCheckProductPrice"].ToString();

                entity.IsPackedPrice = "0";
                if (reader["IsPackedPrice"] != DBNull.Value)
                    entity.IsPackedPrice = reader["IsPackedPrice"].ToString();

                entity.IsFastDeliveryPrice = "0";
                if (reader["IsFastDeliveryPrice"] != DBNull.Value)
                    entity.IsFastDeliveryPrice = reader["IsFastDeliveryPrice"].ToString();

                entity.InsuranceMoney = "0";
                if (reader["InsuranceMoney"] != DBNull.Value)
                    entity.InsuranceMoney = reader["InsuranceMoney"].ToString();

                if (reader["FeeInWareHouse"] != DBNull.Value)
                    entity.FeeInWareHouse = Convert.ToDouble(reader["FeeInWareHouse"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["ShopName"] != DBNull.Value)
                    entity.ShopName = reader["ShopName"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["DateBuy"] != DBNull.Value)
                    entity.DateBuy = Convert.ToDateTime(reader["DateBuy"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_MainOder> GetBuyProBySQL(string fd, string td, int pageIndex, int pageSize)
        {
            var sql = @"select * ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where Status > = 5 ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            sql += " ORDER BY ID DESC, Status desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_MainOder> list = new List<tbl_MainOder>();
            while (reader.Read())
            {
                var entity = new tbl_MainOder();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["ShopID"] != DBNull.Value)
                    entity.ShopID = reader["ShopID"].ToString();

                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.TotalPriceReal = "0";
                if (reader["TotalPriceReal"] != DBNull.Value)
                    entity.TotalPriceReal = reader["TotalPriceReal"].ToString();

                entity.TotalPriceRealCYN = "0";
                if (reader["TotalPriceRealCYN"] != DBNull.Value)
                    entity.TotalPriceRealCYN = reader["TotalPriceRealCYN"].ToString();

                entity.Deposit = "0";
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                entity.CurrencyReal = "0";
                if (reader["CurrencyReal"] != DBNull.Value)
                    entity.CurrencyReal = reader["CurrencyReal"].ToString();

                entity.CurrentCNYVN = "0";
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.CurrentCNYVN = reader["CurrentCNYVN"].ToString();

                entity.PriceVND = "0";
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();

                entity.FeeShipCN = "0";
                if (reader["FeeShipCN"] != DBNull.Value)
                    entity.FeeShipCN = reader["FeeShipCN"].ToString();

                entity.FeeWeight = "0";
                if (reader["FeeWeight"] != DBNull.Value)
                    entity.FeeWeight = reader["FeeWeight"].ToString();

                entity.FeeBuyPro = "0";
                if (reader["FeeBuyPro"] != DBNull.Value)
                    entity.FeeBuyPro = reader["FeeBuyPro"].ToString();

                entity.IsCheckProductPrice = "0";
                if (reader["IsCheckProductPrice"] != DBNull.Value)
                    entity.IsCheckProductPrice = reader["IsCheckProductPrice"].ToString();

                entity.IsPackedPrice = "0";
                if (reader["IsPackedPrice"] != DBNull.Value)
                    entity.IsPackedPrice = reader["IsPackedPrice"].ToString();

                entity.IsFastDeliveryPrice = "0";
                if (reader["IsFastDeliveryPrice"] != DBNull.Value)
                    entity.IsFastDeliveryPrice = reader["IsFastDeliveryPrice"].ToString();

                entity.InsuranceMoney = "0";
                if (reader["InsuranceMoney"] != DBNull.Value)
                    entity.InsuranceMoney = reader["InsuranceMoney"].ToString();

                if (reader["FeeInWareHouse"] != DBNull.Value)
                    entity.FeeInWareHouse = Convert.ToDouble(reader["FeeInWareHouse"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["ShopName"] != DBNull.Value)
                    entity.ShopName = reader["ShopName"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["DateBuy"] != DBNull.Value)
                    entity.DateBuy = Convert.ToDateTime(reader["DateBuy"].ToString());

                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString().ToInt(0);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static int GetTotalBuyProBySQL(string fd, string td)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where Status > = 5 ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }
        public static double GetTotalPriceBuyProBySQL(string fd, string td, string Col)
        {
            var sql = @"select Total=Count(*) ";
            sql += ", TotalPrice=SUM(Cast(" + Col + " as Float)) ";
            sql += "from dbo.tbl_MainOder ";
            sql += "Where Status > = 5 ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double a = 0;
            while (reader.Read())
            {
                if (reader["TotalPrice"] != DBNull.Value)
                    a = Convert.ToDouble(reader["TotalPrice"].ToString());
            }
            reader.Close();
            return a;
        }
        #endregion
        public static double GetTotalPrice_Thang1(string fd, string td, string Username, string Saler)
        {
            //var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            var sql = @"select ";
            sql += " tongdon = Count(ID) from tbl_MainOder ";
            sql += "where DateBuy is not null ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND DateBuy >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND DateBuy <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double tongdon = 0;
            while (reader.Read())
            {

                if (reader["tongdon"] != DBNull.Value)
                    tongdon = Convert.ToDouble(reader["tongdon"].ToString());
            }
            reader.Close();
            return tongdon;
        }
        public static double GetTotalPrice_Thang(string fd, string td, string Username, string Saler)
        {
            //var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            var sql = @"select total=Sum(convert(float,IsNull(TotalPriceRealCYN,0))) ";
            sql += "  from tbl_MainOder ";
            sql += "where DateBuy is not null ";
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND DateBuy >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND DateBuy <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());

            }
            reader.Close();
            return total;
        }

        #region 4.5
        public static string UpdateMVD_Thang(int ID, int Status, DateTime DateTQ, int ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    or.DateTQ = DateTQ;
                    or.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static tbl_MainOder GetByIDandStatus(int ID, int Status)
        {
            using (var db = new NHSTEntities())
            {
                var mo = db.tbl_MainOder.Where(x => x.ID == ID && x.Status < Status).FirstOrDefault();
                if (mo != null)
                    return mo;
                else return null;
            }
        }

        public static tbl_MainOder GetByIDAndUID(int ID, int UID)
        {
            using (var db = new NHSTEntities())
            {
                var mo = db.tbl_MainOder.Where(x => x.ID == ID && x.Status > 2 && x.Status <= 9 && x.UID == UID).FirstOrDefault();
                if (mo != null)
                    return mo;
                else return null;
            }
        }

        public static int GetTotalForOrderListOfDK(int RoleID, int StaffID, int orderType, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode, string mvd, string mdh)
        {
            int a = 0;
            var sql = @"select Total=Count(*) ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";
            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";
            sql += "Left outer join ";
            sql += " (SELECT DISTINCT ST2.ID, SUBSTRING((SELECT ',' + ST1.MainOrderCode   AS [text()] FROM tbl_MainOrderCode ST1 WHERE ST1.MainOrderID = ST2.ID FOR XML PATH('')), 2, 1000)[MaDonHangString] FROM tbl_MainOder ST2) sz on sz.ID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";
            sql += "Where mo.OrderType = '" + orderType + "'";            
            if (RoleID == 3)
            {
                sql += "    AND mo.Status != 0 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status > 3 ";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status > 3 ";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 0 and mo.SalerID = " + StaffID + "";
            }            
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status > 1 ";
            }
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)              
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";               
                if (Type == 2)
                    sql += " And mo.BarCode like N'%" + searchtext + "%'";
                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";
                if (Type == 4)
                    sql += " And u.Username like N'%" + searchtext + "%'";
            }
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "-1")
                    sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(mvd))
            {
                sql += " And mo.BarCode like N'%" + mvd + "%'";
            }
            if (!string.IsNullOrEmpty(mdh))
            {
                sql += " And mo.MainOrderCode like N'%" + mdh + "%'";
            }
            if (st == "2")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DepositDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DepositDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "5")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateBuy >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateBuy <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "6")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateTQ >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateTQ <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "7")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateVN >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateVN <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "9")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.PayDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.PayDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "10")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CompleteDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CompleteDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;
        }

        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilterOrderList(int RoleID, int StaffID, int OrderType, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode, int pageIndex, int pageSize, string mvd, string mdh, int sort)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.CurrentCNYVN, mo.DathangID, mo.SalerID ,mo.IsDoneSmallPackage,sz.MaDonHangString,se.MaVanDon,sw.MaDonHang,sp.TenSanPham, mo.Deposit, mo.CreatedDate, mo.DepostiDate, mo.DateBuy, mo.DateTQ, mo.DateVN,mo.PayDate,mo.CompleteDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.BarCode, mo.QuantityMVD, mo.QuantityMDH, mo.MainOrderCode, mo.PriceVND, ";
            sql += "u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, mo.SalerID, mo.DathangID, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, mo.DateSite,  ";
            sql += "CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" style=\"max-width:100px;max-height:100px\" src=\"' + o.anhsanpham + '\">' END AS anhsanpham, ";
            sql += "CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã đủ MVĐ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã nhập</span>' ELSE N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa nhập MVĐ</span>' END AS hasSmallpackage ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";
            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";
            sql += "Left outer join ";
            sql += " (SELECT DISTINCT ST2.ID, SUBSTRING((SELECT ',' + Cast(ST1.ID as nvarchar(Max))  AS [text()] FROM tbl_MainOrderCode ST1 WHERE ST1.MainOrderID = ST2.ID FOR XML PATH('')), 2, 1000)[MaDonHang] FROM tbl_MainOder ST2) sw on sw.ID = mo.ID ";
            sql += "Left outer join ";
            sql += " (SELECT DISTINCT ST2.ID, SUBSTRING((SELECT ',' + ST1.MainOrderCode   AS [text()] FROM tbl_MainOrderCode ST1 WHERE ST1.MainOrderID = ST2.ID FOR XML PATH('')), 2, 1000)[MaDonHangString] FROM tbl_MainOder ST2) sz on sz.ID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";
            sql += "Where mo.OrderType = '" + OrderType + "'";            
            if (RoleID == 3)
            {
                sql += "    AND mo.Status != 0 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status > 3 ";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status > 3 ";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 0 and mo.SalerID = " + StaffID + "";
            }
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status > 1 ";
            }
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)                
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";              
                if (Type == 2)
                    sql += " And mo.BarCode like N'%" + searchtext + "%'";
                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";
                if (Type == 4)
                    sql += " And u.Username like N'%" + searchtext + "%'";
            }
            if (!string.IsNullOrEmpty(st))
            {
                if (st != "-1")
                    sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(mvd))
            {
                sql += " And mo.BarCode like N'%" + mvd + "%'";
            }
            if (!string.IsNullOrEmpty(mdh))
            {
                sql += " And mo.MainOrderCode like N'%" + mdh + "%'";
            }
            if (st == "2")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DepositDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DepositDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "5")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateBuy >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateBuy <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "6")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateTQ >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateTQ <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "7")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateVN >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.DateVN <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "9")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.PayDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.PayDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else if (st == "10")
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CompleteDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CompleteDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                    sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
                }
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            if (sort == 0)
            {
                sql += " ORDER BY mo.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 1)
            {
                sql += " ORDER BY mo.ID ASC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 2)
            {
                sql += " ORDER BY mo.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 3)
            {
                sql += " ORDER BY mo.Status ASC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 4)
            {
                sql += " ORDER BY mo.Status DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int Status = 0;
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    entity.Currency = reader["CurrentCNYVN"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["PriceVND"] != DBNull.Value)
                    entity.PriceVND = reader["PriceVND"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd/MM/yyyy HH:mm");

                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    Status = Convert.ToInt32(reader["Status"].ToString());
                }                           

                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["SalerID"] != DBNull.Value)
                    entity.SalerID = Convert.ToInt32(reader["SalerID"].ToString());
                if (reader["DathangID"] != DBNull.Value)
                    entity.DathangID = Convert.ToInt32(reader["DathangID"].ToString());

                if (reader["QuantityMVD"] != DBNull.Value)
                    entity.QuantityMVD = Convert.ToInt32(reader["QuantityMVD"].ToString());
                if (reader["QuantityMDH"] != DBNull.Value)
                    entity.QuantityMDH = Convert.ToInt32(reader["QuantityMDH"].ToString());

                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();

                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();

                if (reader["MainOrderCode"] != DBNull.Value)
                    entity.MainOrderCode = reader["MainOrderCode"].ToString();

                if (reader["BarCode"] != DBNull.Value)
                    entity.BarCode = reader["BarCode"].ToString();

                if (reader["MaDonHang"] != DBNull.Value)
                    entity.listMainOrderCode = reader["MaDonHang"].ToString().Split(',').ToList();

                if (reader["IsDoneSmallPackage"] != DBNull.Value)
                    entity.IsDoneSmallPackage = Convert.ToBoolean(reader["IsDoneSmallPackage"].ToString());

                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                if (Status == 0)
                {
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.Created = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Lên đơn:</span><span>" + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.Created = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Lên đơn:</span><span>" + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CreatedDate"].ToString())) + "</span> </p>";
                }

                if (Status == 2)
                {
                    if (reader["DepostiDate"] != DBNull.Value)
                        entity.DepostiDate = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Đặt cọc:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["DepostiDate"] != DBNull.Value)
                        entity.DepostiDate = "<p class=\"s-txt no-wrap \"><span class=\"mg\">Đặt cọc:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DepostiDate"].ToString())) + "</span> </p>";
                }

                if (Status == 4)
                {
                    if (reader["DateSite"] != DBNull.Value)
                        entity.DateSite = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Shop phát hàng:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateSite"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateSite"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["DateSite"] != DBNull.Value)
                        entity.DateSite = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Shop phát hàng:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateSite"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateSite"].ToString())) + "</span> </p>";
                }

                if (Status == 5)
                {
                    if (reader["DateBuy"] != DBNull.Value)
                        entity.DateBuy = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Đặt hàng:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateBuy"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["DateBuy"] != DBNull.Value)
                        entity.DateBuy = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Đặt hàng:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateBuy"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateBuy"].ToString())) + "</span> </p>";
                }

                if (Status == 6)
                {
                    if (reader["DateTQ"] != DBNull.Value)
                        entity.DateTQ = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Đã về kho TQ:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateTQ"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["DateTQ"] != DBNull.Value)
                        entity.DateTQ = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Đã về kho TQ:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateTQ"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateTQ"].ToString())) + "</span> </p>";
                }

                if (Status == 7)
                {
                    if (reader["DateVN"] != DBNull.Value)
                        entity.DateVN = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Đã về kho VN:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateVN"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateVN"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["DateVN"] != DBNull.Value)
                        entity.DateVN = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Đã về kho VN:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["DateVN"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["DateVN"].ToString())) + "</span> </p>";
                }

                if (Status == 9)
                {
                    if (reader["PayDate"] != DBNull.Value)
                        entity.DatePay = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Khách thanh toán:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["PayDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["PayDate"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["PayDate"] != DBNull.Value)
                        entity.DatePay = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Khách thanh toán:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["PayDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["PayDate"].ToString())) + "</span> </p>";
                }

                if (Status == 10)
                {
                    if (reader["CompleteDate"] != DBNull.Value)
                        entity.CompleteDate = "<p class=\"s-txt no-wrap red-text\"><span class=\"mg\">Hoàn thành:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + "</span> </p>";
                }
                else
                {
                    if (reader["CompleteDate"] != DBNull.Value)
                        entity.CompleteDate = "<p class=\"s-txt no-wrap\"><span class=\"mg\">Hoàn thành:</span><span> " + string.Format("{0:HH:mm}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + " - " + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["CompleteDate"].ToString())) + "</span> </p>";
                }

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static int GetTotalNewOfDK(int UID, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode)
        {
            int a = 0;
            var sql = @"select Total=Count(*) ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";
            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";

            sql += "Where UID = '" + UID + "'";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";
                }
                if (Type == 2)
                    sql += " And se.MaVanDon like N'%" + searchtext + "%'";

                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";

            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += " AND mo.Status in (" + st + ")";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;
        }

        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilterNew(int UID, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode, int pageIndex, int pageSize)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,se.MaVanDon,sp.TenSanPham, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += "CASE mo.Status WHEN 0 THEN N'<span class=\"badge red darken-2 white-text border-radius-2\">Chờ đặt cọc</span>' ";
            sql += "WHEN 1 THEN N'<span class=\"badge black darken-2 white-text border-radius-2\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"badge pink darken-2 white-text border-radius-2\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ duyệt đơn</span>' ";
            sql += "WHEN 4 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Shop phát hàng</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho VN</span>' ";
            sql += "WHEN 8 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>' ";
            sql += "END AS statusstring, mo.DathangID,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang,  ";
            sql += "CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" style=\"max-width:125px;max-height:125px\" src=\"' + o.anhsanpham + '\">' END AS anhsanpham, ";
            sql += "CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã nhập</span>' ELSE N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa nhập</span>' END AS hasSmallpackage ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";

            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "Where UID = '" + UID + "'";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";
                }
                if (Type == 2)
                    sql += " And se.MaVanDon like N'%" + searchtext + "%'";

                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";

            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            sql += " ORDER BY mo.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;

                entity.TotalPriceVND = "0";
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();

                entity.Deposit = "0";
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd/MM/yyyy HH:mm");
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilterNew_Excel(int UID, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,se.MaVanDon,sp.TenSanPham, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += "CASE mo.Status WHEN 0 THEN N'<span class=\"badge red darken-2 white-text border-radius-2\">Chờ đặt cọc</span>' ";
            sql += "WHEN 1 THEN N'<span class=\"badge black darken-2 white-text border-radius-2\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"badge pink darken-2 white-text border-radius-2\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ duyệt đơn</span>' ";
            sql += "WHEN 4 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho VN</span>' ";
            sql += "WHEN 8 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>' ";
            sql += "END AS statusstring, mo.DathangID,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang,  ";
            sql += "CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" style=\"max-width:125px;max-height:125px\" src=\"' + o.anhsanpham + '\">' END AS anhsanpham, ";
            sql += "CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã nhập</span>' ELSE N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa nhập</span>' END AS hasSmallpackage ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";

            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "Where UID = '" + UID + "'";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";
                }
                if (Type == 2)
                    sql += " And se.MaVanDon like N'%" + searchtext + "%'";

                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";

            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            sql += " ORDER BY mo.ID DESC ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd/MM/yyyy HH:mm");
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserIDInSQLHelperWithFilterOrderListNew(int OrderType, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode, int pageIndex, int pageSize)
        {
            var list = new List<OrderGetSQL>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,mo.IsDoneSmallPackage,sz.MaDonHangString,se.MaVanDon,sw.MaDonHang,sp.TenSanPham, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
            sql += "CASE mo.Status WHEN 0 THEN N'<span class=\"badge red darken-2 white-text border-radius-2\">Chờ đặt cọc</span>' ";
            sql += "WHEN 1 THEN N'<span class=\"badge black darken-2 white-text border-radius-2\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"badge pink darken-2 white-text border-radius-2\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ duyệt đơn</span>' ";
            sql += "WHEN 4 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho VN</span>' ";
            sql += "WHEN 8 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>' ";
            sql += "END AS statusstring, mo.DathangID,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang,  ";
            sql += "CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" style=\"max-width:125px;max-height:125px\" src=\"' + o.anhsanpham + '\">' END AS anhsanpham, ";
            sql += "CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã nhập</span>' ELSE N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa nhập</span>' END AS hasSmallpackage ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";
            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";
            sql += "Left outer join ";
            sql += " (SELECT DISTINCT ST2.ID, SUBSTRING((SELECT ',' + Cast(ST1.ID as nvarchar(Max))  AS [text()] FROM tbl_MainOrderCode ST1 WHERE ST1.MainOrderID = ST2.ID FOR XML PATH('')), 2, 1000)[MaDonHang] FROM tbl_MainOder ST2) sw on sw.ID = mo.ID ";
            sql += "Left outer join ";
            sql += " (SELECT DISTINCT ST2.ID, SUBSTRING((SELECT ',' + ST1.MainOrderCode   AS [text()] FROM tbl_MainOrderCode ST1 WHERE ST1.MainOrderID = ST2.ID FOR XML PATH('')), 2, 1000)[MaDonHangString] FROM tbl_MainOder ST2) sz on sz.ID = mo.ID ";
            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";
            sql += "Where mo.OrderType = '" + OrderType + "'";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";
                }
                if (Type == 2)
                    sql += " And se.MaVanDon like N'%" + searchtext + "%'";

                if (Type == 1)
                    sql += " And sz.MaDonHangString like N'%" + searchtext + "%'";
            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            sql += " ORDER BY mo.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new OrderGetSQL();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd/MM/yyyy HH:mm");
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();

                if (reader["MaDonHang"] != DBNull.Value)
                    entity.listMainOrderCode = reader["MaDonHang"].ToString().Split(',').ToList();

                if (reader["IsDoneSmallPackage"] != DBNull.Value)
                    entity.IsDoneSmallPackage = Convert.ToBoolean(reader["IsDoneSmallPackage"].ToString());

                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();


                list.Add(entity);
            }
            reader.Close();
            return list;
        }


        public static List<tbl_MainOder> GetAllByUID_Deposit(int UID, int type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status == 0 && o.OrderType == type).OrderBy(t => t.CreatedDate).ToList();
                return lo;
            }
        }

        public static List<tbl_MainOder> GetAllByUID_Payall(int UID, int type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status > 6 && o.OrderType == type).OrderBy(t => t.CreatedDate).ToList();
                return lo;
            }
        }

        public static List<SQLsumtotal> GetByUsernameSumTotal(int UID)
        {
            var list = new List<SQLsumtotal>();
            var sql = @"select sum(CONVERT(float,TotalPriceVND)) tongtienhang";
            sql += ",sum(convert(float,Deposit )) tongtiencoc";
            sql += " from tbl_MainOder";
            sql += " where Status > 1 and UID = " + UID + "";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new SQLsumtotal();
                if (reader["tongtienhang"] != DBNull.Value)
                    entity.tongtienhang = reader["tongtienhang"].ToString();
                if (reader["tongtiencoc"] != DBNull.Value)
                    entity.tongtiencoc = reader["tongtiencoc"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }


        public static List<SQLsumtotal> GetByUsernameInSQLHelper_Sumtotal(int UID, string searchtext, int Type, string fd, string td, string priceFrom, string priceTo, string st, bool isNotCode)
        {
            var list = new List<SQLsumtotal>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND,se.MaVanDon,sp.TenSanPham, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice,  ";
            //var sql = @"select sum(CONVERT(float,TotalPriceVND)) tongtienhang";
            sql += "CASE mo.Status WHEN 0 THEN N'<span class=\"badge red darken-2 white-text border-radius-2\">Chờ đặt cọc</span>' ";
            sql += "WHEN 1 THEN N'<span class=\"badge black darken-2 white-text border-radius-2\">Hủy đơn hàng</span>' ";
            sql += "WHEN 2 THEN N'<span class=\"badge pink darken-2 white-text border-radius-2\">Khách đã đặt cọc</span>' ";
            sql += "WHEN 3 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ duyệt đơn</span>' ";
            sql += "WHEN 4 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã duyệt đơn</span>' ";
            sql += "WHEN 5 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã mua hàng</span>' ";
            sql += "WHEN 6 THEN N'<span class=\"badge green darken-2 white-text border-radius-2\">Đã về kho TQ</span>' ";
            sql += "WHEN 7 THEN N'<span class=\"badge orange darken-2 white-text border-radius-2\">Đã về kho VN</span>' ";
            sql += "WHEN 8 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Chờ thanh toán</span>' ";
            sql += "WHEN 9 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Khách đã thanh toán</span>' ";
            sql += "ELSE N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã hoàn thành</span>' ";
            sql += "END AS statusstring, mo.DathangID,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages,  ";
            sql += "mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4,  ";
            sql += "mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang,  ";
            sql += "CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" style=\"max-width:125px;max-height:125px\" src=\"' + o.anhsanpham + '\">' END AS anhsanpham, ";
            sql += "CASE WHEN mo.IsDoneSmallPackage = 1 THEN N'<span class=\"badge blue darken-2 white-text border-radius-2\">Đã đủ</span>'  WHEN a.countrow > 0 THEN N'<span class=\"badge yellow darken-2 white-text border-radius-2\">Đã nhập</span>' ELSE N'<span class=\"badge red darken-2 white-text border-radius-2\">Chưa nhập</span>' END AS hasSmallpackage ";
            sql += "FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN ";
            sql += "dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN ";
            sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages = 1 LEFT OUTER JOIN ";
            sql += "(SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1 ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.OrderTransactionCode  AS [text()] ";
            sql += "FROM tbl_SmallPackage ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [MaVanDon] FROM tbl_MainOder ST2) se on se.ID=mo.ID ";

            sql += "Left outer join ( ";
            sql += "SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.title_origin  AS [text()] ";
            sql += "FROM tbl_Order ST1 ";
            sql += "WHERE ST1.MainOrderID = ST2.ID ";
            sql += "FOR XML PATH ('') ";
            sql += "), 2, 1000) [TenSanPham] FROM tbl_MainOder ST2) sp on sp.ID=mo.ID ";

            sql += "LEFT OUTER JOIN(SELECT count(*) AS countRow, MainOrderID  FROM tbl_SmallPackage AS a  GROUP BY a.MainOrderID) AS a ON a.MainOrderID = mo.ID ";

            sql += " where mo.Status != 1 and UID = " + UID + "";
            //sql += "Where UID = '" + UID + "'";
            if (!string.IsNullOrEmpty(searchtext))
            {
                if (Type == 3)
                {
                    sql += "  AND sp.TenSanPham like N'%" + searchtext + "%'";
                }
                if (Type == 2)
                    sql += " And se.MaVanDon like N'%" + searchtext + "%'";

                if (Type == 1)
                    sql += " And mo.ID like N'%" + searchtext + "%'";

            }
            if (!string.IsNullOrEmpty(st))
            {
                sql += " AND mo.Status in (" + st + ")";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(priceFrom))
            {
                sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
            }
            if (!string.IsNullOrEmpty(priceTo) && !string.IsNullOrEmpty(priceFrom))
            {
                if (Convert.ToDouble(priceFrom) <= Convert.ToDouble(priceTo))
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
            }
            if (isNotCode == true)
            {
                sql += " AND sm.totalSmallPackages is null";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                int MainOrderID = reader["ID"].ToString().ToInt(0);
                var entity = new SQLsumtotal();
                //if (reader["tongtienhang"] != DBNull.Value)
                //    entity.tongtienhang = reader["tongtienhang"].ToString();
                //if (reader["tongtiencoc"] != DBNull.Value)
                //    entity.tongtiencoc = reader["tongtiencoc"].ToString();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = MainOrderID;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd/MM/yyyy HH:mm");
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                }
                if (reader["statusstring"] != DBNull.Value)
                {
                    entity.statusstring = reader["statusstring"].ToString();
                }
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["Uname"] != DBNull.Value)
                    entity.Uname = reader["Uname"].ToString();
                if (reader["saler"] != DBNull.Value)
                    entity.saler = reader["saler"].ToString();
                if (reader["dathang"] != DBNull.Value)
                    entity.dathang = reader["dathang"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                else
                    entity.IsCheckNotiPrice = false;
                if (reader["hasSmallpackage"] != DBNull.Value)
                    entity.hasSmallpackage = reader["hasSmallpackage"].ToString();

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public class SQLsumtotal
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string tongtienhang { get; set; }
            public string tongtiencoc { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public int STT { get; set; }
            public string anhsanpham { get; set; }
            public string ShopID { get; set; }
            public string Deposit { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public int OrderType { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }

            public string Uname { get; set; }
            public string dathang { get; set; }
            public string saler { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
            public bool IsDoneSmallPackage { get; set; }

            public string Currency { get; set; }

            public List<string> listMainOrderCode { get; set; }

            public string Created { get; set; }
            public string DepostiDate { get; set; }
            public string DateBuy { get; set; }
            public string DateTQ { get; set; }
            public string DateVN { get; set; }
            public string DatePay { get; set; }
            public string CompleteDate { get; set; }
            public string PriceVND { get; set; }
            public int SalerID { get; set; }
            public int DathangID { get; set; }
        }

        #endregion

        public static double GetTotalPrice_Thang(int UID, int status, string TotalPrice)
        {
            var sql = @"select total=SUM(CAST(" + TotalPrice + " as float)) ";
            sql += "from tbl_MainOder ";
            sql += "where UID=" + UID.ToString() + " AND STATUS=" + status.ToString();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }

        public static int CountOrderList(int UID)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder ";
            sql += " WHERE UID = " + UID + "";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }

        public static double GetTotalPriceNotOrderType(int UID, int status, string PriceType)
        {
            var sql = @"select total=SUM(CAST(" + PriceType + " as float)) ";
            sql += "from tbl_MainOder ";
            sql += "where UID=" + UID.ToString() + " AND STATUS=" + status.ToString();
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            double total = 0;
            while (reader.Read())
            {
                if (reader["total"] != DBNull.Value)
                    total = Convert.ToDouble(reader["total"].ToString());
            }
            reader.Close();
            return total;
        }

        #region Stored thống kê
        public static List<ReportOrder_Result> OrderListStore(int type, string searchtext, string fd, string td, int pageSize, int pageIndex)
        {
            using (var db = new NHSTEntities())
            {
                return db.ReportOrder(type, searchtext, fd, td, pageSize, pageIndex).ToList();
            }
        }
        public static List<ReportOrderExcel_Result> OrderListStoreExcel(int type, string searchtext, string fd, string td)
        {
            using (var db = new NHSTEntities())
            {
                return db.ReportOrderExcel(type, searchtext, fd, td).ToList();
            }
        }
        #endregion
    }
}
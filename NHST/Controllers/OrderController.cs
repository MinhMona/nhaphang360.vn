using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class OrderController
    {
        #region CRUD
        public static string Insert(int UID, string title_origin, string title_translated, string price_origin, string price_promotion, string property_translated, string property, string data_value,
            string image_model, string image_origin, string shop_id, string shop_name, string seller_id, string wangwang, string quantity, string stock, string location_sale,
            string site, string comment, string item_id, string link_origin, string outer_id, string error, string weight, string step, string stepprice, string brand, string category_name, string category_id, string tool, string version,
            bool is_translate, bool IsForward, string IsForwardPrice, bool IsFastDelivery, string IsFastDeliveryPrice, bool IsCheckProduct, string IsCheckProductPrice, bool IsPacked,
            string IsPackedPrice, bool IsFast, string IsFastPrice, string PriceVND, string PriceCNY, string Note, string FullName, string Address, string Email, string Phone, int Status,
            string Deposit, string CurrentCNYVN, string TotalPriceVND, int MainOrderID, DateTime CreatedDate, int CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Order o = new tbl_Order();
                o.UID = UID;
                o.title_origin = title_origin;
                o.title_translated = title_translated;
                o.price_origin = price_origin;
                o.price_promotion = price_promotion;
                o.property_translated = property_translated;
                o.property = property;
                o.data_value = data_value;
                o.image_model = image_model;
                o.image_origin = image_origin;
                o.shop_id = shop_id;
                o.shop_name = shop_name;
                o.seller_id = seller_id;
                o.wangwang = wangwang;
                o.quantity = quantity;
                o.stock = stock;
                o.location_sale = location_sale;
                o.site = site;
                o.comment = comment;
                o.item_id = item_id;
                o.link_origin = link_origin;
                o.outer_id = outer_id;
                o.error = error;
                o.weight = weight;
                o.step = step;
                o.stepprice = stepprice;
                o.brand = brand;
                o.category_name = category_name;
                o.category_id = category_id;
                o.tool = tool;
                o.version = version;
                o.is_translate = is_translate;
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
                o.Note = Note;
                o.FullName = FullName;
                o.Address = Address;
                o.Email = Email;
                o.Phone = Phone;
                o.Status = Status;
                o.Deposit = Deposit;
                o.CurrentCNYVN = CurrentCNYVN;
                o.TotalPriceVND = TotalPriceVND;
                o.MainOrderID = MainOrderID;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_Order.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
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
        public static string UpdateStatusByID(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
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

        public static string UpdateFee(int ID, string Deposit, string FeeShipCN, string FeeBuyPro, string FeeWeight,
            string IsCheckProductPrice, string IsPackedPrice, string IsFastDeliveryPrice, string TotalPriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
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

        public static string UpdateAutoCurrency(int ID, string PriceCNY, string PriceVND, string CurrentCNYVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceCNY = PriceCNY;
                    or.PriceVND = PriceVND;
                    or.CurrentCNYVN = CurrentCNYVN; 
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateBrand(int ID, string Brand)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.brand = Brand;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return "ok";
                }
                else return null;
            }
        }
        public static string UpdateLinkLinkIMG(int ID, string link_origin, string image_origin)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.link_origin = link_origin;
                    ot.image_origin = image_origin;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return "ok";
                }
                else return null;
            }
        }

        public static string UpdatePriceChangePriceReal(int ID, string PriceChange, string RealPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.PriceChange = PriceChange;
                    or.RealPrice = RealPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePricePriceReal(int ID, string PriceChange, string RealPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.price_origin = PriceChange;
                    or.RealPrice = RealPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePricePromotion(int ID, string PricePromotion)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.price_promotion = PricePromotion;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateQuantity(int ID, string quantity)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.quantity = quantity;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateProductStatus(int ID, int ProductStatus)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.ProductStatus = ProductStatus;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static string SelectUIDByIDOrder(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        public static List<tbl_Order> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Order> lo = new List<tbl_Order>();
                lo = dbe.tbl_Order.ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_Order> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Order> lo = new List<tbl_Order>();
                lo = dbe.tbl_Order.Where(o => o.MainOrderID == MainOrderID).ToList();
                return lo;
            }
        }
        public static List<tbl_Order> GetByMainOrderIDAndBrand(int MainOrderID, string Brand)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Order> lo = new List<tbl_Order>();
                lo = dbe.tbl_Order.Where(o => o.MainOrderID == MainOrderID && o.brand.Contains(Brand)).ToList();
                return lo;
            }
        }
        public static List<tbl_Order> GetByMainOrderIDAndProductStatus(int MainOrderID, int ProductStatus)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Order> lo = new List<tbl_Order>();
                lo = dbe.tbl_Order.Where(o => o.MainOrderID == MainOrderID && o.ProductStatus == ProductStatus).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_Order> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Order> lo = new List<tbl_Order>();
                lo = dbe.tbl_Order.Where(o => o.UID == UID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_Order GetAllByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_Order.Where(o => o.UID == UID && o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_Order GetAllByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        #endregion
    }
}
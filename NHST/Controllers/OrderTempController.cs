using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using WebUI.Business;
using System.Data;

namespace NHST.Controllers
{
    public class OrderTempController
    {
        #region CRUD
        public static string Insert(int UID, int OrderShopTempID, string title_origin, string title_translated, string price_origin, string price_promotion, string property_translated, string property, string data_value,
            string image_model, string image_origin, string shop_id, string shop_name, string seller_id, string wangwang, string quantity, string stock, string location_sale,
            string site, string comment, string item_id, string link_origin, string outer_id, string error, string weight, string step, string pricestep, string brand, string category_name, string category_id, string tool, string version,
            bool is_translate, DateTime CreatedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = GetAllByUID(UID);

                tbl_OrderTemp o = new tbl_OrderTemp();
                o.UID = UID;
                o.OrderShopTempID = OrderShopTempID;
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
                o.stepprice = pricestep;
                o.brand = brand;
                o.category_name = category_name;
                o.category_id = category_id;
                o.tool = tool;
                o.version = version;
                o.is_translate = is_translate;
                o.CreatedDate = CreatedDate;
                string kq = "";
                int kt = 0;
                if (lo != null)
                {
                    foreach (var item in lo)
                    {                        
						if (item.UID == o.UID && item.item_id == o.item_id && item.brand == o.brand && item.category_id == o.category_id && item.property == o.property)
                        {
                            kt = item.ID;
                        }
                    }
                    if (kt == 0)
                    {
                        dbe.tbl_OrderTemp.Add(o);
                        dbe.Configuration.ValidateOnSaveEnabled = false;
                        dbe.SaveChanges();



                        kq = o.ID.ToString();

                    }
                    else
                    {
                        kq = UpdateQuantity1(kt, Convert.ToInt32(quantity));
                    }
                }
                else
                {

                    dbe.tbl_OrderTemp.Add(o);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    kq = o.ID.ToString();
                }
                UpdatePriceInsert(UID, pricestep, item_id);
                return kq;
            }
        }

        //public static void UpdatePriceInsert(int UID, string pricestep, string item_id)
        //{
        //    double price_update = 0;
        //    if (!string.IsNullOrEmpty(pricestep))
        //    {
        //        var getproductbyid = GetAllByUIDAndItemID(UID, item_id);
        //        List<double> fromPrice = new List<double>();
        //        List<double> listPrice = new List<double>();
        //        int countproduct = 0;

        //        if(getproductbyid != null)
        //        {
        //            if (getproductbyid.Count > 0)
        //            {

        //                foreach (var item in getproductbyid)
        //                {

        //                    countproduct += item.quantity.ToInt(0);
        //                }
        //                string[] items = pricestep.Split('|');
        //                if (items.Length - 1 > 0)
        //                {
        //                    //4-11:20|12-35:18|36-99999999:17|
        //                    for (int i = 0; i < items.Length - 1; i++)
        //                    {
        //                        var item = items[i];
        //                        string[] elements = item.Split(':');
        //                        string amountft = elements[0];
        //                        string[] ft = amountft.Split('-');

        //                        double from = Convert.ToDouble(ft[0]);
        //                        double to = Convert.ToDouble(ft[1]);
        //                        double price = Convert.ToDouble(elements[1]);
        //                        fromPrice.Add(from);
        //                        listPrice.Add(price);
        //                        if (countproduct >= from && countproduct <= to)
        //                        {
        //                            price_update = price;
        //                        }
        //                    }
        //                }
        //            }
        //            double lowFromquantity = fromPrice[0];
        //            if (countproduct < lowFromquantity)
        //            {
        //                price_update = listPrice[0];
        //            }
        //            if (price_update > 0)
        //            {
        //                foreach (var productU in getproductbyid)
        //                {
        //                    UpdatePrice(productU.ID, price_update);
        //                }
        //            }
        //        }

        //    }
        //}

        public static void UpdatePriceInsert(int UID, string pricestep, string item_id)
        {
            double price_update = 0;
            bool checkpricestep = true;
            if (!string.IsNullOrEmpty(pricestep))
            {
                string[] items = pricestep.Split('|');
                if (items.Length - 1 > 0)
                {
                    //4-11:20|12-35:18|36-99999999:17|
                    double checkto = 0;
                    for (int i = 0; i < items.Length - 1; i++)
                    {
                        var item = items[i];
                        string[] elements = item.Split(':');
                        string amountft = elements[0];
                        string[] ft = amountft.Split('-');
                        //double from = Convert.ToDouble(ft[0]);
                        double to = Convert.ToDouble(ft[1]);
                        //double price = Convert.ToDouble(elements[1]);
                        //fromPrice.Add(from);
                        //listPrice.Add(price);
                        //if (countproduct >= from && countproduct <= to)
                        //{
                        //    price_update = price;
                        //}
                        if (i == 0)
                        {
                            checkto = to;
                        }
                        else
                        {
                            if (to > checkto)
                            {
                                checkpricestep = false;
                            }
                        }
                    }
                }
                if (checkpricestep == false)
                {
                    var getproductbyid = GetAllByUIDAndItemID(UID, item_id);
                    List<double> fromPrice = new List<double>();
                    List<double> listPrice = new List<double>();
                    int countproduct = 0;
                    if (getproductbyid.Count > 0)
                    {
                        foreach (var item in getproductbyid)
                        {
                            countproduct += item.quantity.ToInt(0);
                        }
                        if (items.Length - 1 > 0)
                        {
                            //4-11:20|12-35:18|36-99999999:17|
                            for (int i = 0; i < items.Length - 1; i++)
                            {
                                var item = items[i];
                                string[] elements = item.Split(':');
                                string amountft = elements[0];
                                string[] ft = amountft.Split('-');

                                double from = Convert.ToDouble(ft[0]);
                                double to = Convert.ToDouble(ft[1]);
                                double price = Convert.ToDouble(elements[1]);
                                fromPrice.Add(from);
                                listPrice.Add(price);
                                if (countproduct >= from && countproduct <= to)
                                {
                                    price_update = price;
                                }
                            }
                        }
                    }
                    double lowFromquantity = fromPrice[0];
                    if (countproduct < lowFromquantity)
                    {
                        price_update = listPrice[0];
                    }
                    if (price_update > 0)
                    {
                        foreach (var productU in getproductbyid)
                        {
                            UpdatePrice(productU.ID, price_update);
                        }
                    }
                }
            }
        }

        public static string UpdatePrice(int ID, double price)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    op.price_origin = price.ToString();
                    op.price_promotion = price.ToString();
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdateQuantity(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    op.quantity = (Convert.ToInt32(op.quantity) + 1).ToString();
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdateQuantity(int ID, int Quantity)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    op.quantity = Quantity.ToString();
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    UpdatePriceInsert(Convert.ToInt32(op.UID), op.stepprice, op.item_id);
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdateQuantity1(int ID, int Quantity)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    double quantityP = Convert.ToDouble(op.quantity);
                    quantityP += Quantity;

                    op.quantity = quantityP.ToString();
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    dbe.tbl_OrderTemp.Remove(op);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdateNoteFast(int ID, string Note, bool IsFast)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.Note = Note;
                    ot.IsFast = IsFast;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return "ok";
                }
                else return null;
            }
        }
        public static string UpdateBrand(int ID, string Brand)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        public static string Update4Field(int ID, bool Chk, string Field)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    if (Field == "IsForward")
                    {
                        ot.IsForward = Chk;
                    }
                    else if (Field == "IsFastDelivery")
                    {
                        ot.IsFastDelivery = Chk;
                    }
                    else if (Field == "IsCheckProduct")
                    {
                        ot.IsCheckProduct = Chk;
                    }
                    else if (Field == "IsPacked")
                    {
                        ot.IsPacked = Chk;
                    }
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
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
        public static List<tbl_OrderTemp> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = dbe.tbl_OrderTemp.ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }

        public static List<tbl_OrderTemp> GetAllByUIDAndItemID(int UID, string ItemID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = dbe.tbl_OrderTemp.Where(o => o.UID == UID && o.item_id == ItemID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_OrderTemp GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        public static tbl_OrderTemp GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderTemp.Where(o => o.ID == ID && o.UID == UID).FirstOrDefault();
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
        public static List<tbl_OrderTemp> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = dbe.tbl_OrderTemp.Where(o => o.UID == UID).ToList();
                return lo;
            }
        }
        public static List<tbl_OrderTemp> GetAllByOrderShopTempID(int OrderShopTempID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = dbe.tbl_OrderTemp.Where(o => o.OrderShopTempID == OrderShopTempID).ToList();
                return lo;
            }
        }
        public static List<tbl_OrderTemp> GetAllByOrderShopTempIDAndUID(int UID, int OrderShopTempID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderTemp> lo = new List<tbl_OrderTemp>();
                lo = dbe.tbl_OrderTemp.Where(o => o.UID == UID && o.OrderShopTempID == OrderShopTempID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        #endregion
        public static int GetTotalProduct(int OrderShopTempID)
        {
            int Count = 0;
            var sql = @"select SUM(convert(int,quantity)) as total from tbl_OrderTemp";
            sql += "        where OrderShopTempID= " + OrderShopTempID + "";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
    }
}
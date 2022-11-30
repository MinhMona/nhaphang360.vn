using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class OrderShopTempController
    {
        #region CRUD
        public static string Insert(int UID, string ShopID, string ShopName, string Site, DateTime CreatedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopTemp> lost = new List<tbl_OrderShopTemp>();
                lost = GetAll();

                tbl_OrderShopTemp o = new tbl_OrderShopTemp();
                o.UID = UID;
                o.ShopID = ShopID;
                o.ShopName = ShopName;
                o.Site = Site;
                o.CreatedDate = CreatedDate;

                int kt = 0;
                if (lost != null)
                {
                    foreach (var item in lost)
                    {
                        if (item.UID == o.UID && item.ShopID == o.ShopID && item.ShopName == o.ShopName && item.Site == o.Site)
                        {
                            int link = 200;
                            var conf = ConfigurationController.GetByTop1();
                            if(conf != null)
                            {
                                link = Convert.ToInt32(conf.NumberLinkOfOrder);
                            }

                            var order = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, item.ID);
                            if (order.Count <= link)
                            {
                                kt = item.ID;
                            }
                            else
                            {
                                kt = -1;
                            }
                        }
                    }
                    if (kt == 0)
                    {
                        dbe.tbl_OrderShopTemp.Add(o);
                        dbe.Configuration.ValidateOnSaveEnabled = false;
                        dbe.SaveChanges();
                        string k = o.ID.ToString();
                        return k;
                    }
                    else
                    {
                        return kt.ToString();
                    }
                }
                else
                {
                    dbe.tbl_OrderShopTemp.Add(o);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    string k = o.ID.ToString();
                    return k;
                }
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
                    return kq;
                }
                else return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    var pros = OrderTempController.GetAllByOrderShopTempID(ID);
                    if (pros != null)
                    {
                        if (pros.Count > 0)
                        {
                            foreach (var item in pros)
                            {
                                OrderTempController.Delete(item.ID);
                            }
                        }
                    }

                    dbe.tbl_OrderShopTemp.Remove(op);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();

                    return kq;
                }
                else return null;
            }
        }

        public static string UpdateNoteShop(int ID, string Note)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.Note = Note;
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
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        public static string UpdateNoteFastPriceVND(int ID, string Note, string PriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.Note = Note;
                    ot.PriceVND = PriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string Update4Field(int ID, bool Chk, string Field)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
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
                    else if (Field == "IsFast")
                    {
                        ot.IsFast = Chk;
                    }
                    else if(Field == "IsInsurrance")
                    {
                        ot.IsInsurrance = Chk;
                    }
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdateCheckProductPrice(int ID, string FeeCheckProduct)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.IsCheckProductPrice = FeeCheckProduct;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static string UpdatePriceVNDCNY(int ID, string PriceVND, string PriceCNY)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.PriceVND = PriceVND;
                    ot.PriceCNY = PriceCNY;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        #endregion

        public static string UpdateCheckField(int ID, bool Chk)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.IsCheckProduct = Chk;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }

        public static string UpdateCheckFieldPacked(int ID, bool Chk)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.IsPacked = Chk;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }

        public static string UpdateCheckFastDelivery(int ID, bool Chk)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.IsFastDelivery = Chk;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }

        public static string UpdateCheckInsurrance(int ID, bool Chk)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
                if (ot != null)
                {
                    ot.IsInsurrance = Chk;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }


        public static string SelectUIDByIDOrder(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        #region GetAll
        public static List<tbl_OrderShopTemp> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                var ots = dbe.tbl_OrderShopTemp.ToList();
                if (ots != null)
                {
                    if (ots.Count > 0)
                        return ots;
                    else return null;
                }
                else return null;
            }
        }
        public static List<tbl_OrderShopTemp> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopTemp> ots = new List<tbl_OrderShopTemp>();
                ots = dbe.tbl_OrderShopTemp.Where(os => os.UID == UID).ToList();
                return ots;
            }
        }
        public static tbl_OrderShopTemp GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID && o.UID == UID).FirstOrDefault();
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
        public static tbl_OrderShopTemp GetByID( int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_OrderShopTemp.Where(o => o.ID == ID).FirstOrDefault();
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
        #endregion
    }
}
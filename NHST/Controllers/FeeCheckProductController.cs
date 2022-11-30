using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class FeeCheckProductController
    {
        #region CRUD
        public static string Insert(double AmountFrom, double AmountTo, double Price, int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_FeeCheckProduct a = new tbl_FeeCheckProduct();
                a.AmountFrom = AmountFrom;
                a.AmountTo = AmountTo;
                a.Price = Price;
                a.Type = Type;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_FeeCheckProduct.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, double AmountFrom, double AmountTo, double Price, int Type, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.AmountFrom = AmountFrom;
                    a.AmountTo = AmountTo;
                    a.Price = Price;
                    a.Type = Type;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_FeeCheckProduct GetByWeightAndRecivePlaceAndAmount(double WeightFrom, double WeightTo, double Amount, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.AmountFrom == WeightFrom && f.AmountTo == WeightTo && f.Price == Amount && f.Type == Type).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_FeeCheckProduct GetByPriceFromAndPriceTo(double PriceFrom, double PriceTo)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.AmountFrom == PriceFrom && f.AmountTo == PriceTo).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_FeeCheckProduct GetByBetweenAmountAndType(double Amount, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.AmountFrom <= Amount && f.AmountTo >= Amount && f.Type == Type).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_FeeCheckProduct GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_FeeCheckProduct> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_FeeCheckProduct> a = new List<tbl_FeeCheckProduct>();
                a = dbe.tbl_FeeCheckProduct.ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }

        public static tbl_FeeCheckProduct GetByQuantityAndType(int Quantity, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeCheckProduct.Where(f => f.AmountFrom >= Quantity && f.AmountTo < Quantity && f.Type == Type).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
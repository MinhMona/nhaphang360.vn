using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class FeeBuyProController
    {
        #region CRUD
        public static string Insert(double AmountFrom, double AmountTo, double FeePercent, double FeeMoney, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_FeeBuyPro a = new tbl_FeeBuyPro();
                a.AmountFrom = AmountFrom;
                a.AmountTo = AmountTo;
                a.FeePercent = FeePercent;
                a.FeeMoney = FeeMoney;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_FeeBuyPro.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, double AmountFrom,double AmountTo, double FeePercent, double FeeMoney, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var a = dbe.tbl_FeeBuyPro.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.AmountFrom = AmountFrom;
                    a.AmountTo = AmountTo;
                    a.FeePercent = FeePercent;
                    a.FeeMoney = FeeMoney;
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
        public static tbl_FeeBuyPro GetByPriceFromAndPriceTo(double PriceFrom, double PriceTo)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeBuyPro.Where(f => f.AmountFrom == PriceFrom && f.AmountTo == PriceTo).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_FeeBuyPro GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeBuyPro.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_FeeBuyPro> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_FeeBuyPro> a = new List<tbl_FeeBuyPro>();
                a = dbe.tbl_FeeBuyPro.ToList();
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
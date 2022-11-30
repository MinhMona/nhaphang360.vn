using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NHST.Controllers
{
    public class PriceChangeController
    {
        #region CRUD
        public static string Insert(double PriceFromCYN, double PriceToCYN, double Vip0,
             double Vip1, double Vip2, double Vip3, double Vip4, double Vip5, double Vip6, double Vip7, double Vip8, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PriceChange a = new tbl_PriceChange();
                a.PriceFromCYN = PriceFromCYN;
                a.PriceToCYN = PriceToCYN;
                //a.PriveVND = PriveVND;
                a.Vip0 = Vip0;
                a.Vip1 = Vip1;
                a.Vip2 = Vip2;
                a.Vip3 = Vip3;
                a.Vip4 = Vip4;
                a.Vip5 = Vip5;
                a.Vip6 = Vip6;
                a.Vip7 = Vip7;
                a.Vip8 = Vip8;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_PriceChange.Add(a);
                dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, double PriceFromCYN, double PriceToCYN, double Vip0,
             double Vip1, double Vip2, double Vip3, double Vip4, double Vip5, double Vip6, double Vip7, 
             double Vip8, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_PriceChange.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.PriceFromCYN = PriceFromCYN;
                    a.PriceToCYN = PriceToCYN;
                    //a.PriveVND = PriveVND;
                    a.Vip0 = Vip0;
                    a.Vip1 = Vip1;
                    a.Vip2 = Vip2;
                    a.Vip3 = Vip3;
                    a.Vip4 = Vip4;
                    a.Vip5 = Vip5;
                    a.Vip6 = Vip6;
                    a.Vip7 = Vip7;
                    a.Vip8 = Vip8;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Update1(int ID, double PriceFromCYN, double PriceToCYN, double Vip0,
             double Vip1, double Vip2, double Vip3, double Vip4, 
             DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_PriceChange.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.PriceFromCYN = PriceFromCYN;
                    a.PriceToCYN = PriceToCYN;
                    //a.PriveVND = PriveVND;
                    a.Vip0 = Vip0;
                    a.Vip1 = Vip1;
                    a.Vip2 = Vip2;
                    a.Vip3 = Vip3;
                    a.Vip4 = Vip4;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_PriceChange> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PriceChange> a = new List<tbl_PriceChange>();
                a = dbe.tbl_PriceChange.ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_PriceChange GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_PriceChange.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_PriceChange GetByPriceFT(double price)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_PriceChange.Where(f => f.PriceFromCYN < price && f.PriceToCYN >= price).FirstOrDefault();
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
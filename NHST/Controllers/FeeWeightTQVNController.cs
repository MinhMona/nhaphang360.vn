using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class FeeWeightTQVNController
    {
        
        #region CRUD
        public static string Insert(string ReceivePlace, double WeightFrom, double WeightTo, double Amount, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_FeeWeightTQVN a = new tbl_FeeWeightTQVN();
                a.ReceivePlace = ReceivePlace;
                a.WeightFrom = WeightFrom;
                a.WeightTo = WeightTo;
                a.Amount = Amount;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_FeeWeightTQVN.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string ReceivePlace, double WeightFrom, double WeightTo, double Amount, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var a = dbe.tbl_FeeWeightTQVN.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.ReceivePlace = ReceivePlace;
                    a.WeightFrom = WeightFrom;
                    a.WeightTo = WeightTo;
                    a.Amount = Amount;
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
        public static tbl_FeeWeightTQVN GetByWeightAndRecivePlaceAndAmount(double WeightFrom, double WeightTo, string ReceivePlace, double Amount)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeWeightTQVN.Where(f => f.WeightFrom == WeightFrom && f.WeightTo == WeightTo && f.ReceivePlace == ReceivePlace && f.Amount == Amount).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_FeeWeightTQVN GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_FeeWeightTQVN.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_FeeWeightTQVN> GetAll(string ReceivePlace)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_FeeWeightTQVN> a = new List<tbl_FeeWeightTQVN>();
                a = dbe.tbl_FeeWeightTQVN.Where(h => h.ReceivePlace.Contains(ReceivePlace)).ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_FeeWeightTQVN> GetByReceivePlace(string ReceivePlace)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_FeeWeightTQVN> a = new List<tbl_FeeWeightTQVN>();
                a = dbe.tbl_FeeWeightTQVN.Where(f => f.ReceivePlace == ReceivePlace).ToList();
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
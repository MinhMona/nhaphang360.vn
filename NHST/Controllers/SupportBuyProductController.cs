using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class SupportBuyProductController
    {
        #region CRUD
        public static string Insert(string SupportName, string SupportPhone, string SupportEmail,int SupportPlace, int SupportIndex, DateTime CreatedDate, 
            string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SupportBuyProduct p = new tbl_SupportBuyProduct();
                p.SupportName = SupportName;
                p.SupportPhone = SupportPhone;
                p.SupportEmail = SupportEmail;
                p.SupportPlace = SupportPlace;
                p.SupportIndex = SupportIndex;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_SupportBuyProduct.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string SupportName, string SupportPhone, string SupportEmail, int SupportPlace, int SupportIndex, 
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_SupportBuyProduct.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.SupportName = SupportName;
                    p.SupportPhone = SupportPhone;
                    p.SupportEmail = SupportEmail;
                    p.SupportPlace = SupportPlace;
                    p.SupportIndex = SupportIndex;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_SupportBuyProduct.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_SupportBuyProduct.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_SupportBuyProduct> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SupportBuyProduct> pages = new List<tbl_SupportBuyProduct>();
                pages = dbe.tbl_SupportBuyProduct.Where(p => p.SupportName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static tbl_SupportBuyProduct GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SupportBuyProduct page = dbe.tbl_SupportBuyProduct.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
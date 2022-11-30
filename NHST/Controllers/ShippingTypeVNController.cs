using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class ShippingTypeVNController
    {
        #region CRUD
        public static string Insert(string ShippingTypeVNName, string ShippingTypeVNDescription, bool isHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ShippingTypeVN p = new tbl_ShippingTypeVN();
                p.ShippingTypeVNName = ShippingTypeVNName;
                p.ShippingTypeVNDescription = ShippingTypeVNDescription;
                p.IsHidden = isHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ShippingTypeVN.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string ShippingTypeVNName, string ShippingTypeVNDescription, bool isHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_ShippingTypeVN.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.ShippingTypeVNName = ShippingTypeVNName;
                    p.ShippingTypeVNDescription = ShippingTypeVNDescription;
                    p.IsHidden = isHidden;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
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
        public static List<tbl_ShippingTypeVN> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ShippingTypeVN> pages = new List<tbl_ShippingTypeVN>();
                pages = dbe.tbl_ShippingTypeVN.Where(p => p.ShippingTypeVNName.Contains(s)).ToList();
                return pages;
            }
        }
        public static List<tbl_ShippingTypeVN> GetAllWithIsHidden(string s, bool isHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ShippingTypeVN> pages = new List<tbl_ShippingTypeVN>();
                pages = dbe.tbl_ShippingTypeVN.Where(p => p.ShippingTypeVNName.Contains(s) && p.IsHidden == isHidden).ToList();
                return pages;
            }
        }
        public static tbl_ShippingTypeVN GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ShippingTypeVN page = dbe.tbl_ShippingTypeVN.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
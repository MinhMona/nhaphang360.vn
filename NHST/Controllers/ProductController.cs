using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ProductController
    {
        #region CRUD
        public static string Insert(string WebName, string ProductIMG, string Productname, string WebLink, bool IsHot, bool IsHidden, 
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Products p = new tbl_Products();
                p.WebName = WebName;
                p.ProductIMG = ProductIMG;
                p.Productname = Productname;
                p.WebLink = WebLink;
                p.IsHot = IsHot;
                p.IsHidden = IsHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Products.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string WebName, string ProductIMG, string Productname, string WebLink, bool IsHot, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Products.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.WebName = WebName;
                    p.ProductIMG = ProductIMG;
                    p.Productname = Productname;
                    p.WebLink = WebLink;
                    p.IsHot = IsHot;
                    p.IsHidden = IsHidden;
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
                var p = dbe.tbl_Products.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Products.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Products> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Products> pages = new List<tbl_Products>();
                pages = dbe.tbl_Products.Where(p => p.Productname.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_Products> GetAllIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Products> pages = new List<tbl_Products>();
                pages = dbe.tbl_Products.Where(p => p.IsHidden == IsHidden).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_Products> GetIsHot(bool IsHot, bool isHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Products> pages = new List<tbl_Products>();
                pages = dbe.tbl_Products.Where(p => p.IsHot == IsHot && p.IsHidden == isHidden).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_Products GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Products page = dbe.tbl_Products.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
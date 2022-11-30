using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ProductLinkController
    {
        #region CRUD
        public static string Insert(int ProductCategoryID, string ProductName, string ProductLink, bool isHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ProductLink p = new tbl_ProductLink();
                p.ProductCategoryID = ProductCategoryID;
                p.ProductName = ProductName;
                p.ProductLink = ProductLink;
                p.IsHidden = isHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ProductLink.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int ProductCategoryID, string ProductName, string ProductLink, bool isHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_ProductLink.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.ProductCategoryID = ProductCategoryID;
                    p.ProductName = ProductName;
                    p.ProductLink = ProductLink;
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
        public static List<tbl_ProductLink> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductLink> pages = new List<tbl_ProductLink>();
                pages = dbe.tbl_ProductLink.Where(p => p.ProductName.Contains(s)).OrderBy(a => a.ProductCategoryID).ToList();
                return pages;
            }
        }
        public static List<tbl_ProductLink> GetAllWithIsHidden(string s, bool isHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductLink> pages = new List<tbl_ProductLink>();
                pages = dbe.tbl_ProductLink.Where(p => p.ProductName.Contains(s) && p.IsHidden == isHidden).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_ProductLink> GetAllWithIsHiddenWithCateID(bool isHidden, int CategoryID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductLink> pages = new List<tbl_ProductLink>();
                pages = dbe.tbl_ProductLink.Where(p => p.IsHidden == isHidden && p.ProductCategoryID == CategoryID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_ProductLink GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ProductLink page = dbe.tbl_ProductLink.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
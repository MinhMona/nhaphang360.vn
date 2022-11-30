using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;


namespace NHST.Controllers
{
    public class ProductCategoryController
    {
        #region CRUD
        public static string Insert(int ChinasiteID, string CategoryName, string CategoryIMG, bool isHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ProductCategory p = new tbl_ProductCategory();
                p.ChinasiteID = ChinasiteID;
                p.CategoryName = CategoryName;
                p.CategoryIMG = CategoryIMG;
                p.IsHidden = isHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ProductCategory.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int ChinasiteID, string CategoryName, string CategoryIMG, bool isHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_ProductCategory.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.ChinasiteID = ChinasiteID;
                    p.CategoryName = CategoryName;
                    p.CategoryIMG = CategoryIMG;
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
        public static List<tbl_ProductCategory> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductCategory> pages = new List<tbl_ProductCategory>();
                pages = dbe.tbl_ProductCategory.Where(p => p.CategoryName.Contains(s)).OrderBy(a => a.ChinasiteID).ToList();
                return pages;
            }
        }
        public static List<tbl_ProductCategory> GetAllWithIsHiddenAndChinaWebID(bool isHidden, int ChinaWebsiteID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductCategory> pages = new List<tbl_ProductCategory>();
                pages = dbe.tbl_ProductCategory.Where(p => p.IsHidden == isHidden && p.ChinasiteID == ChinaWebsiteID).OrderBy(a => a.ChinasiteID).ToList();
                return pages;
            }
        }
        public static List<tbl_ProductCategory> GetAllWithIsHidden(string s, bool isHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ProductCategory> pages = new List<tbl_ProductCategory>();
                pages = dbe.tbl_ProductCategory.Where(p => p.CategoryName.Contains(s) && p.IsHidden == isHidden).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_ProductCategory GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ProductCategory page = dbe.tbl_ProductCategory.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
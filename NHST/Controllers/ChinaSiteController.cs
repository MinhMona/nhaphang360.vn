using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ChinaSiteController
    {
        #region CRUD
        public static string Insert(string Sitename, string SiteLogo, bool isHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ChinaSite p = new tbl_ChinaSite();
                p.Sitename = Sitename;
                p.SiteLogo = SiteLogo;
                p.IsHidden = isHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ChinaSite.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string Sitename, string SiteLogo,bool isHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_ChinaSite.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Sitename = Sitename;
                    p.SiteLogo = SiteLogo;
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
        public static List<tbl_ChinaSite> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ChinaSite> pages = new List<tbl_ChinaSite>();
                pages = dbe.tbl_ChinaSite.Where(p => p.Sitename.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_ChinaSite> GetAllWithIsHidden(string s, bool isHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ChinaSite> pages = new List<tbl_ChinaSite>();
                pages = dbe.tbl_ChinaSite.Where(p => p.Sitename.Contains(s) && p.IsHidden == isHidden).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_ChinaSite GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ChinaSite page = dbe.tbl_ChinaSite.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
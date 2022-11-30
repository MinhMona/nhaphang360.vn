using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class SocialSupportController
    {
        #region CRUD
        public static string Insert(string SocialName, string SocialLink, string SocialIMG, int SocialPlace,int SocialIndex, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SocialSupport p = new tbl_SocialSupport();
                p.SocialName = SocialName;
                p.SocialLink = SocialLink;
                p.SocialIMG = SocialIMG;
                p.SocialPlace = SocialPlace;
                p.SocialIndex = SocialIndex;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_SocialSupport.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string SocialName, string SocialLink, string SocialIMG, int SocialPlace, int SocialIndex, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_SocialSupport.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.SocialName = SocialName;
                    p.SocialLink = SocialLink;
                    p.SocialIMG = SocialIMG;
                    p.SocialPlace = SocialPlace;
                    p.SocialIndex = SocialIndex;
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
                var p = dbe.tbl_SocialSupport.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_SocialSupport.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_SocialSupport> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SocialSupport> pages = new List<tbl_SocialSupport>();
                pages = dbe.tbl_SocialSupport.Where(p => p.SocialName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static tbl_SocialSupport GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SocialSupport page = dbe.tbl_SocialSupport.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
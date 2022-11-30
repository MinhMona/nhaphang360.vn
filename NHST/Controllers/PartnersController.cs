using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class PartnersController
    {
        #region CRUD
        public static string Insert(string PartnerName, string PartnerIMG, string PartnerLink,int PartnerIndex, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Partners p = new tbl_Partners();
                p.PartnerName = PartnerName;
                p.PartnerIMG = PartnerIMG;
                p.PartnerLink = PartnerLink;
                p.PartnerIndex = PartnerIndex;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Partners.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string PartnerName, string PartnerIMG, string PartnerLink, int PartnerIndex, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Partners.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.PartnerName = PartnerName;
                    p.PartnerIMG = PartnerIMG;
                    p.PartnerLink = PartnerLink;
                    p.PartnerIndex = PartnerIndex;
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
                var p = dbe.tbl_Partners.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Partners.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Partners> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Partners> pages = new List<tbl_Partners>();
                pages = dbe.tbl_Partners.Where(p => p.PartnerName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static tbl_Partners GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Partners page = dbe.tbl_Partners.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
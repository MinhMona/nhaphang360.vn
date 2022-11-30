using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class BenefitsController
    {
        #region CRUD
        public static string Insert(string BenefitName,string BenefitDescription,int BenefitIndex,int BenefitSide, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Benefits p = new tbl_Benefits();
                p.BenefitName = BenefitName;
                p.BenefitDescription = BenefitDescription;
                p.BenefitIndex = BenefitIndex;
                p.BenefitSide = BenefitSide;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Benefits.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string BenefitName, string BenefitDescription, int BenefitIndex, int BenefitSide, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Benefits.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.BenefitName = BenefitName;
                    p.BenefitDescription = BenefitDescription;
                    p.BenefitIndex = BenefitIndex;
                    p.BenefitSide = BenefitSide;
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
                var p = dbe.tbl_Benefits.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Benefits.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Benefits> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Benefits> pages = new List<tbl_Benefits>();
                pages = dbe.tbl_Benefits.Where(p => p.BenefitName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static List<tbl_Benefits> GetAllUser()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Benefits> pages = new List<tbl_Benefits>();
                pages = dbe.tbl_Benefits.ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_Benefits GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Benefits page = dbe.tbl_Benefits.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
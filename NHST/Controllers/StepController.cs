using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class StepController
    {
        #region CRUD
        public static string Insert(string StepName, string StepIMG, int StepIndex, string StepLink, DateTime CreatedDate, string CreatedBy, string StepContent, string ClassIcon, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Step p = new tbl_Step();
                p.StepName = StepName;
                p.StepIMG = StepIMG;
                p.StepIndex = StepIndex;
                p.StepLink = StepLink;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.IsHidden = IsHidden;
                p.StepContent = StepContent;
                p.ClassIcon = ClassIcon;
                dbe.tbl_Step.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string StepName, string StepIMG, int StepIndex, string StepLink, DateTime ModifiedDate, string ModifiedBy, string StepContent, string ClassIcon, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Step.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.StepName = StepName;
                    if (!string.IsNullOrEmpty(StepIMG))
                        p.StepIMG = StepIMG;
                    p.StepIndex = StepIndex;
                    p.StepLink = StepLink;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    p.IsHidden = IsHidden;
                    p.StepContent = StepContent;
                    p.ClassIcon = ClassIcon;
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
                var p = dbe.tbl_Step.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Step.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Step> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Step> pages = new List<tbl_Step>();
                pages = dbe.tbl_Step.Where(p => p.StepName.Contains(s) && p.IsHidden != true).OrderBy(x => x.StepIndex).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static List<tbl_Step> GetAllHome()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Step> pages = new List<tbl_Step>();
                pages = dbe.tbl_Step.Where(p => p.IsHidden != true).OrderBy(x => x.StepIndex).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static List<tbl_Step> GetAllUser()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Step> pages = new List<tbl_Step>();
                pages = dbe.tbl_Step.OrderBy(a => a.StepIndex).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_Step GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Step page = dbe.tbl_Step.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class CommitmentController
    {
        #region CRUD
        public static string Insert(string CommitmentName, string CommitmentDescription, int CommitmentIndex, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Commitment p = new tbl_Commitment();
                p.CommitmentName = CommitmentName;
                p.CommitmentDescription = CommitmentDescription;
                p.CommitmentIndex = CommitmentIndex;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Commitment.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string CommitmentName, string CommitmentDescription, int CommitmentIndex, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Commitment.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.CommitmentName = CommitmentName;
                    p.CommitmentDescription = CommitmentDescription;
                    p.CommitmentIndex = CommitmentIndex;
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
                var p = dbe.tbl_Commitment.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Commitment.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Commitment> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Commitment> pages = new List<tbl_Commitment>();
                pages = dbe.tbl_Commitment.Where(p => p.CommitmentName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static List<tbl_Commitment> GetAllUser()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Commitment> pages = new List<tbl_Commitment>();
                pages = dbe.tbl_Commitment.OrderBy(a => a.CommitmentIndex).ToList();
                return pages;
            }
        }
        public static tbl_Commitment GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Commitment page = dbe.tbl_Commitment.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
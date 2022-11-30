using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class MainOrderCodeController
    {
        #region CRUD
        public static string Insert(int MainOrderID, string MainOrderCode, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_MainOrderCode p = new tbl_MainOrderCode();
                p.MainOrderID = MainOrderID;
                p.MainOrderCode = MainOrderCode;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_MainOrderCode.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int MainOrderID, string MainOrderCode, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_MainOrderCode.Where(o => o.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.MainOrderID = MainOrderID;
                    p.MainOrderCode = MainOrderCode;
                    p.ModifiedDate = ModifiedDate;
                    p.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    string k = p.ID.ToString();
                    return k;
                }
                else
                {
                    return null;
                }

            }
        }
        public static string UpdateCode(int ID, string MainOrderCode, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_MainOrderCode.Where(o => o.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.MainOrderCode = MainOrderCode;
                    p.ModifiedDate = ModifiedDate;
                    p.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    string k = p.ID.ToString();
                    return k;
                }
                else
                {
                    return null;
                }

            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_MainOrderCode a = dbe.tbl_MainOrderCode.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    dbe.tbl_MainOrderCode.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion

        #region Selection
        public static List<tbl_MainOrderCode> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOrderCode> lo = new List<tbl_MainOrderCode>();
                lo = dbe.tbl_MainOrderCode.OrderByDescending(o => o.ID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOrderCode> GetAllByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOrderCode> lo = new List<tbl_MainOrderCode>();
                lo = dbe.tbl_MainOrderCode.Where(o => o.MainOrderID == MainOrderID).ToList();
                return lo;
            }
        }

        public static List<tbl_MainOrderCode> GetAllMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOrderCode> lo = new List<tbl_MainOrderCode>();
                lo = dbe.tbl_MainOrderCode.Where(o => o.MainOrderCode == MainOrderCode).ToList();
                return lo;
            }
        }

        public static tbl_MainOrderCode GetAllByMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOrderCode.Where(o => o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_MainOrderCode GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOrderCode.Where(o => o.ID == ID).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_MainOrderCode GetByMainOrderIDANDMainOrderCode(int MainOrderID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOrderCode.Where(o => o.MainOrderID == MainOrderID && o.MainOrderCode == MainOrderCode).FirstOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }

        public static List<tbl_MainOrderCode> GetContainMainOrderCode(string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOrderCode.Where(o => o.MainOrderCode.Contains(MainOrderCode)).OrderByDescending(x => x.ID).ToList();
                return lo;
            }
        }

        #endregion
    }
}
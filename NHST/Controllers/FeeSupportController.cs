using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class FeeSupportController
    {
        public static string Insert(int MainOrderID, string SupportName, string SupportVND, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                tbl_FeeSupport fs = new tbl_FeeSupport();
                fs.MainOrderID = MainOrderID;
                fs.SupportName = SupportName;
                fs.SupportInfoVND = SupportVND;
                fs.CreatedBy = CreatedBy;
                fs.CreatedDate = CreatedDate;
                fs.IsHide = false;
                db.tbl_FeeSupport.Add(fs);
                db.SaveChanges();
                return fs.ID.ToString();
            }
        }

        public static string Update(int ID, string SupportName, string SupportVND, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                var fs = db.tbl_FeeSupport.Where(x => x.ID == ID).FirstOrDefault();
                if (fs != null)
                {
                    fs.SupportName = SupportName;
                    fs.SupportInfoVND = SupportVND;
                    fs.ModifiedBy = CreatedBy;
                    fs.CreatedDate = CreatedDate;
                    db.SaveChanges();
                    return fs.ID.ToString();
                }
                else
                    return null;
            }
        }

        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_FeeSupport a = dbe.tbl_FeeSupport.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    dbe.tbl_FeeSupport.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string UpdateIsHide(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var fs = db.tbl_FeeSupport.Where(x => x.ID == ID).FirstOrDefault();
                if (fs != null)
                {
                    fs.IsHide = true;
                    db.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static List<tbl_FeeSupport> GetAllByMainOrderID(int MainOrderID)
        {
            using (var db = new NHSTEntities())
            {
                var fs = db.tbl_FeeSupport.Where(x => x.MainOrderID == MainOrderID).OrderBy(x => x.ID).ToList();
                return fs;
            }
        }

        public static tbl_FeeSupport GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var fs = db.tbl_FeeSupport.Where(x => x.ID == ID).FirstOrDefault();
                if (fs != null)
                    return fs;
                else
                    return null;
            }
        }
    }
}
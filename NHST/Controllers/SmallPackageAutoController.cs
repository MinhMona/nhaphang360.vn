using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class SmallPackageAutoController
    {
        public static string Insert(string MainOrderCode, string Value, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                tbl_SmallPackageAuto s = new tbl_SmallPackageAuto();
                s.MainOrderCode = MainOrderCode;
                s.Value = Value;
                s.CreatedBy = CreatedBy;
                s.CreatedDate = CreatedDate;
                s.Status = 1;
                db.tbl_SmallPackageAuto.Add(s);
                db.SaveChanges();
                return s.ID.ToString();
            }
        }

        public static List<tbl_SmallPackageAuto> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var la = db.tbl_SmallPackageAuto.Where(x => x.Status == 1).ToList();
                return la;
            }
        }
    }
}
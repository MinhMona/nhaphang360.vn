using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class HistoryScanPackageController
    {
        public static string Insert(int SmallPackageID, string CreatedBy, DateTime CreatedDate, string WareHouseName)
        {
            using (var db = new NHSTEntities())
            {
                tbl_HistoryScanPackage hc = new tbl_HistoryScanPackage();
                hc.SmallPackageID = SmallPackageID;
                hc.CreatedBy = CreatedBy;
                hc.CreatedDate = CreatedDate;
                hc.WareHouseName = WareHouseName;
                db.tbl_HistoryScanPackage.Add(hc);
                db.SaveChanges();
                return hc.ID.ToString();
            }
        }
    }
}
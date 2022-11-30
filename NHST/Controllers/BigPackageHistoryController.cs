using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class BigPackageHistoryController
    {
     
        #region CRUD
        public static string Insert(int PackageID, string ColumnChange, string FromValue, string ToValue, int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackageHistory a = new tbl_BigPackageHistory();
                a.PackageID = PackageID;
                a.ColumnChange = ColumnChange;
                a.FromValue = FromValue;
                a.ToValue = ToValue;
                a.Type = Type;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_BigPackageHistory.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_BigPackageHistory> GetByPackageIDAndType(int PackageID, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackageHistory> a = new List<tbl_BigPackageHistory>();
                a = dbe.tbl_BigPackageHistory.Where(ad => ad.PackageID == PackageID && ad.Type == Type).OrderByDescending(ad => ad.ID).ToList();
                return a;
            }
        }
        #endregion
    }
}
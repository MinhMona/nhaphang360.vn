using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class FeePackagedController
    {
        #region CRUD       
        public static string Update(int ID, double FirstKg, double NextKg, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                var a = dbe.tbl_FeePackaged.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.FirstKg = FirstKg;
                    a.NextKg = NextKg;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_FeePackaged GetByTop1()
        {
            using (var dbe = new NHSTEntities())
            {
                var conf = dbe.tbl_FeePackaged.Where(c => c.ID == 1).FirstOrDefault();
                if (conf != null)
                {
                    return conf;
                }
                else
                    return null;
            }
        }
        public static List<tbl_FeePackaged> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_FeePackaged> a = new List<tbl_FeePackaged>();
                a = dbe.tbl_FeePackaged.ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
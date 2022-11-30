using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class UserLevelController
    {

        #region CRUD
        public static string Insert(string LevelName, double FeeBuyPro, double FeeWeight, double LessDeposit, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {                
                tbl_UserLevel level = new tbl_UserLevel();
                level.LevelName = LevelName;
                level.FeeBuyPro = FeeBuyPro;
                level.FeeWeight = FeeWeight;
                level.LessDeposit = LessDeposit;
                level.Status = Status;
                dbe.tbl_UserLevel.Add(level);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = level.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string LevelName, double FeeBuyPro, double FeeWeight, double LessDeposit, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var level = dbe.tbl_UserLevel.Where(ac => ac.ID == ID).FirstOrDefault();
                if (level != null)
                {
                    level.LevelName = LevelName;
                    level.FeeBuyPro = FeeBuyPro;
                    level.FeeWeight = FeeWeight;
                    level.LessDeposit = LessDeposit;
                    level.Status = Status;
                    level.ModifiedBy = ModifiedBy;
                    level.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_UserLevel> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_UserLevel> las = new List<tbl_UserLevel>();
                las = dbe.tbl_UserLevel.Where(a => a.LevelName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }

        public static tbl_UserLevel GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_UserLevel level = dbe.tbl_UserLevel.Where(a => a.ID == ID).FirstOrDefault();
                if (level != null)
                    return level;
                else
                    return null;
            }
        }
        #endregion
    }
}
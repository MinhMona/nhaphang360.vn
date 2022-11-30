using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class HistoryOrderChangeController
    {

        #region CRUD
        public static string Insert(int MainOrderID, int UID, string Username, string HistoryContent, int Type, DateTime CreatedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryOrderChange h = new tbl_HistoryOrderChange();
                h.MainOrderID = MainOrderID;
                h.UID = UID;
                h.Username = Username;
                h.HistoryContent = HistoryContent;
                h.Type = Type;
                h.CreatedDate = CreatedDate;
                dbe.tbl_HistoryOrderChange.Add(h);
                dbe.SaveChanges();
                string kq = h.ID.ToString();
                return kq;
            }
        }
        #endregion
        #region Select
        public static tbl_HistoryOrderChange GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_HistoryOrderChange m = dbe.tbl_HistoryOrderChange.Where(me => me.ID == ID).FirstOrDefault();
                if (m != null)
                    return m;
                else return null;
            }
        }
        public static List<tbl_HistoryOrderChange> GetByMainOrderIDAndUID(int MainOrderID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryOrderChange> m = dbe.tbl_HistoryOrderChange.Where(me => me.MainOrderID == MainOrderID && me.UID == UID).ToList();
                return m;
            }
        }
        public static List<tbl_HistoryOrderChange> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryOrderChange> m = dbe.tbl_HistoryOrderChange.Where(me => me.MainOrderID == MainOrderID).OrderByDescending(n => n.ID).ToList();
                return m;
            }
        }
        #endregion
    }
}
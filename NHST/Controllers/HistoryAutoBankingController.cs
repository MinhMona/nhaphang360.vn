using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class HistoryAutoBankingController
    {
        public static string Insert(int UID, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                tbl_HistoryAutoBanking h = new tbl_HistoryAutoBanking();
                h.UID = UID;
                h.Note = Note;
                h.CreatedDate = CreatedDate;
                h.CreatedBy = CreatedBy;
                h.IsHide = false;
                h.Status = 1;
                db.tbl_HistoryAutoBanking.Add(h);
                db.SaveChanges();
                return h.ID.ToString();
            }
        }

        public static string UpdateStatus(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var h = db.tbl_HistoryAutoBanking.Where(x => x.ID == ID).FirstOrDefault();
                if (h != null)
                {
                    h.Status = 2;
                    db.SaveChanges();
                    return h.ID.ToString();
                }
                else
                    return null;
            }

        }

        public static List<tbl_HistoryAutoBanking> GetAllByUID(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var h = db.tbl_HistoryAutoBanking.Where(x => x.UID == UID && x.IsHide == false).OrderByDescending(x => x.CreatedDate).ToList();
                return h;
            }
        }

        public static tbl_HistoryAutoBanking GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var h = db.tbl_HistoryAutoBanking.Where(x => x.ID == ID).FirstOrDefault();
                if (h != null)
                    return h;
                return null;
            }
        }

        public static tbl_HistoryAutoBanking GetByNote(int UID, string Note)
        {
            using (var db = new NHSTEntities())
            {
                var h = db.tbl_HistoryAutoBanking.Where(x => x.UID == UID && x.Note == Note).FirstOrDefault();
                if (h != null)
                    return h;
                return null;
            }
        }
    }
}
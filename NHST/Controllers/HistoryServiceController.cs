using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NHST.Controllers
{
    public class HistoryServiceController
    {
        #region CRUD
        public static string Insert(int PostID, int UID, string Username, int OldStatus, string OldeStatusText, int NewStatus, string NewStatusText,
            int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_HistoryServices a = new tbl_HistoryServices();
                a.PostID = PostID;
                a.UID = UID;
                a.Username = Username;
                a.OldStatus = OldStatus;
                a.OldeStatusText = OldeStatusText;
                a.NewStatus = NewStatus;
                a.NewStatusText = NewStatusText;
                a.Type = Type;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryServices.Add(a);
                dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_HistoryServices> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryServices> a = new List<tbl_HistoryServices>();
                a = dbe.tbl_HistoryServices.ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_HistoryServices> GetAllByPostIDAndType(int PostID, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryServices> a = new List<tbl_HistoryServices>();
                a = dbe.tbl_HistoryServices.Where(h => h.PostID == PostID & h.Type == Type).ToList();
                return a;
            }
        }
        #endregion
    }
}
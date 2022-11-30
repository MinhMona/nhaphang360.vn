using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace NHST.Controllers
{
    public class NotificationsController
    {
        #region CRUD
        public static string Inser(int ReceivedID, string ReceivedUsername, int OrderID, string Message,
           int NotifType, DateTime CreatedDate, string CreatedBy, bool PushNotiApp)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Notification n = new tbl_Notification();
                n.ReceivedID = ReceivedID;
                n.ReceivedUsername = ReceivedUsername;
                n.OrderID = OrderID;
                n.Message = Message;
                n.Status = 1;
                n.NotifType = NotifType;
                n.CreatedDate = CreatedDate;
                n.CreatedBy = CreatedBy;
                n.PushNotiApp = PushNotiApp;
                dbe.tbl_Notification.Add(n);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                int nID = n.ID;
                return nID.ToString();
            }
        }

        public static string UpdateNoti(int ID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notification.Where(x => x.ID == ID).FirstOrDefault();
                if (n != null)
                {
                    n.Status = 2;
                    n.ModifiedBy = CreatedBy;
                    n.ModifiedDate = CreatedDate;
                    int i = dbe.SaveChanges();
                    return i.ToString();
                }
                return null;
            }
        }

        public static int UpdateSent(int ID, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var l = db.tbl_Notification.Where(x => x.ID == ID).FirstOrDefault();
                if (l != null)
                {
                    l.PushNotiApp = false;
                    l.ModifiedDate = DateTime.Now;
                    l.ModifiedBy = Created;
                    int i = db.SaveChanges();
                    return i;
                }
                return 0;
            }
        }

        public static List<tbl_Notification> GetAll(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notification.Where(x => x.ReceivedID == UID).ToList();
                if (n != null)
                    return n;
                return null;
            }
        }

        public static tbl_Notification GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notification.Where(x => x.ID == ID).FirstOrDefault();
                if (n != null)
                    return n;
                return null;
            }
        }

        public static List<tbl_Notification> GetAllPush()
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notification.Where(x => x.PushNotiApp == true).ToList();
                if (n != null)
                    return n;
                return null;
            }
        }

        public static List<tbl_Notification> GetAllByNotifType(int NotifType, int UID)
        {
            using (var db = new NHSTEntities())
            {
                var nt = db.tbl_Notification.Where(x => x.NotifType == NotifType && x.Status == 1 && x.ReceivedID == UID).OrderBy(x => x.CreatedDate).Take(10).ToList();
                return nt;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;

namespace NHST.Controllers
{
    public class NotificationController
    {
        
        #region CRUD
        public static string Inser(int SenderID, string SenderUsername, int ReceivedID, string ReceivedUsername, int OrderID, string Message, int Status, 
           int NotifType, DateTime CreatedDate, string CreatedBy, bool PushNotiApp)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Notifications n = new tbl_Notifications();
                n.SenderID = SenderID;
                n.SenderUsername = SenderUsername;
                n.ReceivedID = ReceivedID;
                n.ReceivedUsername = ReceivedUsername;
                n.OrderID = OrderID;
                n.Message = Message;
                n.Status = Status;
                n.NotifType = NotifType;
                n.CreatedDate = CreatedDate;
                n.CreatedBy = CreatedBy;
                n.PushNotiApp = PushNotiApp;
                dbe.tbl_Notifications.Add(n);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                int nID = n.ID;
                return nID.ToString();
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notifications.Where(no => no.ID == ID).FirstOrDefault();
                if (n != null)
                {
                    n.Status = Status;
                    n.ModifiedBy = ModifiedBy;
                    n.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                return null;
            }
        }
        public static string UpdateStatus_SQL(string username, int status)
        {
           
            var sql = @"update tbl_notifications set status = "+ status + " where receivedusername = '"+username+"'";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            reader.Close();
            return "";
        }
        #endregion
        public static tbl_Notifications GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notifications.Where(no => no.ID == ID).FirstOrDefault();
                if (n != null)
                {
                    return n;
                }
                return null;
            }
        }
        public static List<tbl_Notifications> GetByReceivedID(int ReceivedID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Notifications> n = new List<tbl_Notifications>();
                n = dbe.tbl_Notifications.Where(no => no.ReceivedID == ReceivedID && no.Status == 0).OrderByDescending(no => no.ID).ToList();
                return n;
            }
        }
        public static List<tbl_Notifications> GetAllByReceivedID(int ReceivedID)
        {
            using (var dbe = new NHSTEntities())
            {
                var n = dbe.tbl_Notifications.Where(no => no.ReceivedID == ReceivedID).OrderByDescending(no => no.ID).ToList();
                return n;
            }
        }
    }
}
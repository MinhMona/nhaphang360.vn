using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class RequestShipController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string Phone, string ListOrderCode, string Note, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_RequestShip p = new tbl_RequestShip();
                p.UID = UID;
                p.Username = Username;
                p.Phone = Phone;
                p.ListOrderCode = ListOrderCode;
                p.Note = Note;
                p.Status = Status;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_RequestShip.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string ListOrderCode, string Note, int Status,
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_RequestShip.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.ListOrderCode = ListOrderCode;
                    p.Note = Note;
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_RequestShip.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
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
        public static List<tbl_RequestShip> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_RequestShip> pages = new List<tbl_RequestShip>();
                pages = dbe.tbl_RequestShip.Where(u => u.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_RequestShip> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_RequestShip> pages = new List<tbl_RequestShip>();
                pages = dbe.tbl_RequestShip.Where(u => u.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static tbl_RequestShip GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_RequestShip page = dbe.tbl_RequestShip.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static tbl_RequestShip GetByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_RequestShip page = dbe.tbl_RequestShip.Where(p => p.UID == UID && p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
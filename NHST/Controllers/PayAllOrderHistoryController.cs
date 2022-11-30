using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class PayAllOrderHistoryController
    {     
        #region CRUD
        public static string Insert(int MainOrderID, double Amount, int UID, string Username, string Note,int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayAllOrderHistory a = new tbl_PayAllOrderHistory();
                a.MainOrderID = MainOrderID;
                a.Amount = Amount;
                a.UID = UID;
                a.Username = Username;
                a.Note = Note;
                a.Type = Type;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_PayAllOrderHistory.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select        
        public static List<tbl_PayAllOrderHistory> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayAllOrderHistory> o = new List<tbl_PayAllOrderHistory>();
                o = dbe.tbl_PayAllOrderHistory.OrderByDescending(h => h.CreatedDate).ToList();
                return o;
            }
        }
        #endregion
    }
}
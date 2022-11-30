using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class AccountantOutStockPaymentController
    {
        #region CRUD
        public static string Insert(int OutStockSessionID, double TotalPrice, int UID, string Username, string Note, 
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AccountantOutStockPayment p = new tbl_AccountantOutStockPayment();
                p.OutStockSessionID = OutStockSessionID;
                p.TotalPrice = TotalPrice;
                p.UID = UID;
                p.Username = Username;
                p.Note = Note;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_AccountantOutStockPayment.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }        
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Benefits.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Benefits.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_AccountantOutStockPayment> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AccountantOutStockPayment> pages = new List<tbl_AccountantOutStockPayment>();
                pages = dbe.tbl_AccountantOutStockPayment.Where(p => p.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        #endregion
    }
}
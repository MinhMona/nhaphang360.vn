using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class PayHelpTempController
    {
        public static tbl_PayHelpTemp Insert(int UID, string OrderID, string Amount, string Note, string FriendsAccount, string Customer, DateTime CreatedDate, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                tbl_PayHelpTemp p = new tbl_PayHelpTemp();
                p.UID = UID;
                p.OrderID = OrderID;
                p.Desc1 = Note;
                p.Desc2 = Amount;
                p.Customer = Customer;
                p.FriendsAccount = FriendsAccount;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                db.tbl_PayHelpTemp.Add(p);
                db.SaveChanges();
                return p;
            }
        }

        public static tbl_PayHelpTemp GetByID(int UID, int ID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_PayHelpTemp.Where(x => x.ID == ID && x.UID == UID).FirstOrDefault();
                if (p != null)
                    return p;
                else return null;
            }
        }

        public static List<tbl_PayHelpTemp> GetAllByUID(int UID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_PayHelpTemp.Where(x => x.UID == UID).OrderBy(x => x.CreatedDate).ToList();
                return p;
            }
        }
        
        public static string Delete(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_PayHelpTemp.Where(x => x.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    db.tbl_PayHelpTemp.Remove(p);
                    db.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }

        public static tbl_PayHelpTemp GetByOrderID(string OrderID)
        {
            using (var db = new NHSTEntities())
            {
                var ps = db.tbl_PayHelpTemp.Where(x => x.OrderID == OrderID).FirstOrDefault();
                if (ps != null)
                    return ps;
                return null;
            }
        }

    }
}
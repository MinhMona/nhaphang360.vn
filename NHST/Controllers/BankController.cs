using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class BankController
    {
        public static tbl_Bank Insert(string BankName, string AccountHolder, string BankNumber, string Branch, string IMG, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                tbl_Bank b = new tbl_Bank();
                b.BankName = BankName;
                b.AccountHolder = AccountHolder;
                b.BankNumber = BankNumber;
                b.Branch = Branch;
                b.IMG = IMG;
                b.IsHidden = false;
                b.CreatedBy = CreatedBy;
                b.CreatedDate = CreatedDate;
                db.tbl_Bank.Add(b);
                db.SaveChanges();
                return b;
            }
        }

        public static tbl_Bank Update(int ID, string BankName, string AccountHolder, string BankNumber, string Branch, string IMG, bool IsHidden, string CreatedBy, DateTime CreatedDate)
        {
            using (var db = new NHSTEntities())
            {
                var b = db.tbl_Bank.Where(x => x.ID == ID).FirstOrDefault();
                if (b != null)
                {
                    b.BankName = BankName;
                    b.AccountHolder = AccountHolder;
                    b.BankNumber = BankNumber;
                    b.Branch = Branch;
                    b.IMG = IMG;
                    b.IsHidden = IsHidden;
                    b.ModifiedBy = CreatedBy;
                    b.ModifiedDate = CreatedDate;
                    db.SaveChanges();
                    return b;
                }
                else
                    return null;
            }
        }

        public static tbl_Bank GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var b = db.tbl_Bank.Where(x => x.ID == ID).FirstOrDefault();
                if (b != null)
                    return b;
                else
                    return null;
            }
        }

        public static List<tbl_Bank> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var lb = db.tbl_Bank.OrderByDescending(x => x.ID).ToList();
                return lb;
            }
        }

        public static string Delete(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var b = db.tbl_Bank.Where(x => x.ID == ID).FirstOrDefault();
                if (b != null)
                {
                    db.tbl_Bank.Remove(b);
                    db.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
    }
}
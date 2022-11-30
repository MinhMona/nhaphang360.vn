using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class ContactController
    {
        #region CRUD
        public static string Insert(string Fullname, string Email, string Phone,string ContactContent, bool IsRead, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Contact p = new tbl_Contact();
                p.Fullname = Fullname;
                p.Email = Email;
                p.Phone = Phone;
                p.ContactContent = ContactContent;
                p.IsRead = IsRead;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Contact.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, bool IsRead, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Contact.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.IsRead = IsRead;
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

        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Menu.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_Menu.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Contact> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Contact> pages = new List<tbl_Contact>();
                pages = dbe.tbl_Contact.Where(p => p.Fullname.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }

        public static tbl_Contact GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Contact page = dbe.tbl_Contact.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
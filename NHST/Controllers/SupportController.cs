using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
namespace NHST.Controllers
{
    public class SupportController
    {
        #region CRUD
        public static string Insert(int UID, string UserName,string FullName,string Phone,string Email,string HContent,string FileIMG, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Support c = new tbl_Support();
                c.UID = UID;
                c.UserName = UserName;
                c.FullName = FullName;
                c.Phone = Phone;
                c.Email = Email;
                c.HContent = HContent;
                c.FileIMG = FileIMG;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Support.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }        
        #endregion
        #region Select
        public static List<tbl_Support> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Support> cs = new List<tbl_Support>();
                cs = dbe.tbl_Support.Where(c => c.FullName.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static tbl_Support GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Support.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
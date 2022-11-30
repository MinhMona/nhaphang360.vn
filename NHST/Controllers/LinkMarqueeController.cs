using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class LinkMarqueeController
    {
        #region CRUD
        public static string Insert(string LinkName, string LinkURL, bool isHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                tbl_LinkMarquee user = new tbl_LinkMarquee();
                user.LinkName = LinkName;
                user.LinkURL = LinkURL;
                user.IsHidden = isHidden;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                dbe.tbl_LinkMarquee.Add(user);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string LinkName, string LinkURL, bool isHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                var user = dbe.tbl_LinkMarquee.Where(u => u.ID == ID).FirstOrDefault();
                if (user != null)
                {
                    user.LinkName = LinkName;
                    user.LinkURL = LinkURL;
                    user.IsHidden = isHidden;
                    user.ModifiedDate = ModifiedDate;
                    user.ModifiedBy = ModifiedBy;
                    int kq = dbe.SaveChanges();
                    string k = user.ID.ToString();
                    return k;
                }
                else
                {
                    return "0";
                }

            }
        }
        #endregion
        #region Select
        public static List<tbl_LinkMarquee> GetAll(string search)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                List<tbl_LinkMarquee> cs = new List<tbl_LinkMarquee>();
                cs = dbe.tbl_LinkMarquee.Where(c => c.LinkName.Contains(search)).ToList();
                return cs;
            }
        }
        public static List<tbl_LinkMarquee> GetAllWithHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                List<tbl_LinkMarquee> cs = new List<tbl_LinkMarquee>();
                cs = dbe.tbl_LinkMarquee.Where(c => c.IsHidden == IsHidden).ToList();
                return cs;
            }
        }
        public static tbl_LinkMarquee GetByID(int ID)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                var cs = dbe.tbl_LinkMarquee.Where(c => c.ID == ID).FirstOrDefault();
                if (cs != null)
                    return cs;
                else return null;
            }
        }
        #endregion
    }
}
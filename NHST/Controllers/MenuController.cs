using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class MenuController
    {
        #region CRUD
        public static string Insert(string MenuName, string MenuLink, bool IsHidden, DateTime CreatedDate, int Position, int ParentID, bool Target, int Type, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Menu p = new tbl_Menu();
                p.MenuName = MenuName;
                p.MenuLink = MenuLink;
                p.IsHidden = IsHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.Position = Position;
                p.Parent = ParentID;
                p.Target = Target;
                p.Type = Type;
                dbe.tbl_Menu.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }

        public static string Update(int ID, string MenuName, string MenuLink, bool IsHidden, bool Target, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Menu.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.MenuName = MenuName;
                    p.MenuLink = MenuLink;
                    p.IsHidden = IsHidden;
                    p.Target = Target;
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
        public static List<tbl_Menu> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Menu> pages = new List<tbl_Menu>();
                pages = dbe.tbl_Menu.Where(p => p.MenuName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }

        public static tbl_Menu GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Menu page = dbe.tbl_Menu.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static List<tbl_Menu> GetByLevel(int Parent)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Menu> pages = new List<tbl_Menu>();
                pages = dbe.tbl_Menu.Where(p => p.Parent == Parent && p.IsHidden != true).OrderBy(x => x.Position).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_Menu UpdateIndex(int ID, int Position, int Parent)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Menu.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Position = Position;
                    p.Parent = Parent;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return p;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
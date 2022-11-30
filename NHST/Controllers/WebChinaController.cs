using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class WebChinaController
    {
        #region CRUD
        public static string Insert(string Logo, string WebName,string WebLink, string WebDescript, int IndexPosition, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_WebChina p = new tbl_WebChina();
                p.Logo = Logo;
                p.WebName = WebName;
                p.WebDescript = WebDescript;
                p.WebLink = WebLink;
                p.IndexPosition = IndexPosition;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_WebChina.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string Logo, string WebName, string WebLink, string WebDescript, int IndexPosition, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_WebChina.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Logo = Logo;
                    p.WebName = WebName;
                    p.WebDescript = WebDescript;
                    p.IndexPosition = IndexPosition;
                    p.WebLink = WebLink;
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
                var p = dbe.tbl_WebChina.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_WebChina.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_WebChina> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WebChina> pages = new List<tbl_WebChina>();
                pages = dbe.tbl_WebChina.Where(p => p.WebName.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static List<tbl_WebChina> GetAllTop(int Top)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WebChina> pages = new List<tbl_WebChina>();
                pages = dbe.tbl_WebChina.OrderByDescending(a => a.CreatedDate).Take(Top).ToList();
                if (pages.Count > 0)
                {
                    return pages;
                }
                else return null;
            }
        }
        public static tbl_WebChina GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_WebChina page = dbe.tbl_WebChina.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
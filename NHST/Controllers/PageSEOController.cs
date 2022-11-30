using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class PageSEOController
    {
        #region CRUD       
        public static string Update(int ID, string Pagename, string ogurl, string ogtitle,string ogdescription,string ogimage,string metatitle,
            string metadescription,string metakeyword, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_PageSEO.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Pagename = Pagename;
                    p.ogurl = ogurl;
                    p.ogtitle = ogtitle;
                    p.ogdescription = ogdescription;
                    p.ogimage = ogimage;
                    p.metatitle = metatitle;
                    p.metadescription = metadescription;
                    p.metakeyword = metakeyword;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static List<tbl_PageSEO> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PageSEO> pages = new List<tbl_PageSEO>();
                pages = dbe.tbl_PageSEO.ToList();
                return pages;
            }
        }
        
        public static tbl_PageSEO GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PageSEO page = dbe.tbl_PageSEO.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}
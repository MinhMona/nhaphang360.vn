using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class FooterController
    {
        public static tbl_Footer Update(int ID, string FootHTML, string Link)
        {
            using (var db = new NHSTEntities())
            {
                var ft = db.tbl_Footer.Where(x => x.ID == ID).FirstOrDefault();
                if(ft != null)
                {
                    ft.FooterHTML = FootHTML;
                    ft.LinkFanpage = Link;
                    db.SaveChanges();
                    return ft;
                }
                return null;
            }
        }

        public static tbl_Footer GetByTop1()
        {
            using (var db = new NHSTEntities())
            {
                var ft = db.tbl_Footer.Where(x => x.ID == 1).FirstOrDefault();
                if (ft != null)
                    return ft;
                return null;
            }
        }
    }
}
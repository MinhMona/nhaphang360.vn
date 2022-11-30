using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class PresentController
    {
        public static tbl_Present Update(int ID, int Year, int QuantityCustomer, int QuantityOrder)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_Present.Where(x => x.ID == ID).FirstOrDefault();
                if(p != null)
                {
                    p.Experience = Year;
                    p.QuantityCustomer = QuantityCustomer;
                    p.QuantityOrder = QuantityOrder;
                 
                    p.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return p;
                }
                return null;
            }
        }

        public static tbl_Present GetByTop1()
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_Present.Where(x=>x.ID == 1).FirstOrDefault();
                if (p != null)
                    return p;
                return null;
            }
        }
    }
}
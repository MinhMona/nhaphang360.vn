using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class ServiceController
    {
        #region 
        public static tbl_Service Insert(string ServiceName, string ServiceContent, string ServiceLink, string ServiceIMG, bool IsHidden, int Position, string Created)
        {
            using (var db = new NHSTEntities())
            {
                tbl_Service sv = new tbl_Service();
                sv.ServiceName = ServiceName;
                sv.ServiceContent = ServiceContent;
                sv.ServiceLink = ServiceLink;
                sv.ServiceIMG = ServiceIMG;
                sv.IsHidden = IsHidden;
                sv.Position = Position;
                sv.CreatedBy = Created;
                sv.CreatedDate = DateTime.Now;
                db.tbl_Service.Add(sv);
                db.SaveChanges();
                return sv;
            }
        }

        public static tbl_Service Update(int ID, string ServiceName, string ServiceContent, string ServiceLink, string ServiceIMG, bool IsHidden, int Position, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_Service.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                {
                    sv.ServiceName = ServiceName;
                    sv.ServiceContent = ServiceContent;
                    sv.ServiceLink = ServiceLink;
                    if (!string.IsNullOrEmpty(ServiceIMG))
                        sv.ServiceIMG = ServiceIMG;
                    sv.IsHidden = IsHidden;
                    sv.Position = Position;
                    sv.ModifiedBy = Created;
                    sv.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return sv;
                }
                return null;
            }
        }
        #endregion


        #region Select
        public static List<tbl_Service> GetAllAD()
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_Service.ToList();
                if (sv.Count > 0)
                    return sv;
                return null;
            }
        }

        public static List<tbl_Service> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_Service.Where(x => x.IsHidden != true).ToList();
                if (sv.Count > 0)
                    return sv;
                return null;
            }
        }

        public static tbl_Service GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_Service.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }
        #endregion
    }
}
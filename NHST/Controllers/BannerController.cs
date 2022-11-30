using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class BannerController
    {
        public static tbl_Banner Insert(string BannerIMG, string BannerLink, bool IsHidden, string Created)
        {
            using (var db = new NHSTEntities())
            {
                tbl_Banner bn = new tbl_Banner();
                bn.BannerIMG = BannerIMG;
                bn.BannerLink = BannerLink;
                bn.IsHidden = IsHidden;
                bn.CreatedBy = Created;
                bn.CreatedDate = DateTime.Now;
                db.tbl_Banner.Add(bn);
                db.SaveChanges();
                return bn;
            }
        }

        public static tbl_Banner Update(int ID, string BannerIMG, string BannerLink, bool IsHidden, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var bn = db.tbl_Banner.Where(x => x.ID == ID).FirstOrDefault();
                if (bn != null)
                {
                    bn.BannerIMG = BannerIMG;
                    bn.BannerLink = BannerLink;
                    bn.IsHidden = IsHidden;
                    bn.ModifiedBy = Created;
                    bn.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return bn;
                }
                return null;
            }
        }

        public static List<tbl_Banner> GetAllAD()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_Banner> bn = new List<tbl_Banner>();
                bn = db.tbl_Banner.ToList();
                return bn;
            }
        }

        public static List<tbl_Banner> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_Banner> bn = new List<tbl_Banner>();
                bn = db.tbl_Banner.Where(x => x.IsHidden != true).ToList();
                return bn;
            }
        }

        public static tbl_Banner GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var bn = db.tbl_Banner.Where(x => x.ID == ID).FirstOrDefault();
                if (bn != null)
                    return bn;
                return null;
            }
        }
    }
}
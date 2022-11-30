using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class BigPackageController1
    {
        #region CRUD
        public static string Insert(DateTime SendDate, DateTime ArrivedDate, string PackageCode, string Description, int Place, int Status,
           bool IsSlow, double AdditionFee, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BigPackage1 o = new tbl_BigPackage1();
                o.SendDate = SendDate;
                o.ArrivedDate = ArrivedDate;
                o.PackageCode = PackageCode;
                o.Description = Description;
                o.Place = Place;
                o.Status = Status;
                o.IsSlow = IsSlow;
                o.AdditionFee = AdditionFee;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_BigPackage1.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }

        public static string Update(int ID, DateTime SendDate, DateTime ArrivedDate, string PackageCode, string Description, int Place, int Status,
           bool IsSlow, double AdditionFee, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_BigPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.SendDate = SendDate;
                    o.ArrivedDate = ArrivedDate;
                    o.PackageCode = PackageCode;
                    o.Description = Description;
                    o.Place = Place;
                    o.Status = Status;
                    o.IsSlow = IsSlow;
                    o.AdditionFee = AdditionFee;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    var smallpackages = SmallPackageController1.GetAllByBigPackageID(ID);
                    if (smallpackages.Count > 0)
                    {
                        foreach (var p in smallpackages)
                        {
                            SmallPackageController1.UpdateSendDate(p.ID, SendDate);
                        }
                    }
                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        #endregion
        #region Select
        public static tbl_BigPackage1 GetByPackageCode(string PackageCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_BigPackage1.Where(od => od.PackageCode == PackageCode).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static tbl_BigPackage1 GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_BigPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static List<tbl_BigPackage1> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage1> ps = new List<tbl_BigPackage1>();
                //ps = dbe.tbl_BigPackage1.OrderByDescending(p => p.ID).ToList();
                ps = dbe.tbl_BigPackage1.ToList();
                return ps;
            }
        }
        public static List<tbl_BigPackage1> GetAllByPlace(int place)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage1> ps = new List<tbl_BigPackage1>();
                ps = dbe.tbl_BigPackage1.Where(p => p.Place == place).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }

        #endregion
    }
}
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHST.Controllers
{
    public class WeightController
    {
        #region CRUD
        public static string Insert(double WeightFrom, double WeightTo, int Place, int TypeFastSlow,
             double Vip1, double Vip2, double Vip3, double Vip4, double Vip5, double Vip6, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Weight a = new tbl_Weight();
                a.WeightFrom = WeightFrom;
                a.WeightTo = WeightTo;
                a.Place = Place;
                a.TypeFastSlow = TypeFastSlow;
                a.Vip1 = Vip1;
                a.Vip2 = Vip2;
                a.Vip3 = Vip3;
                a.Vip4 = Vip4;
                a.Vip5 = Vip5;
                a.Vip6 = Vip6;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Weight.Add(a);
                dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, double WeightFrom, double WeightTo, int Place, int TypeFastSlow,
             double Vip1, double Vip2, double Vip3, double Vip4, double Vip5, double Vip6, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Weight.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.WeightFrom = WeightFrom;
                    a.WeightTo = WeightTo;
                    a.Place = Place;
                    a.TypeFastSlow = TypeFastSlow;
                    a.Vip1 = Vip1;
                    a.Vip2 = Vip2;
                    a.Vip3 = Vip3;
                    a.Vip4 = Vip4;
                    a.Vip5 = Vip5;
                    a.Vip6 = Vip6;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Weight> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Weight> a = new List<tbl_Weight>();
                a = dbe.tbl_Weight.ToList();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Weight GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Weight.Where(f => f.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Weight GetByPlaceWeightFT(double Weight, int Place)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Weight.Where(f => f.WeightFrom < Weight && f.WeightTo >= Weight && f.Place == Place).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_Weight GetByPlaceTypeWeightFT(double Weight, int Place, int TypeFastSlow)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Weight.Where(f => f.WeightFrom < Weight && f.WeightTo >= Weight && f.Place == Place && f.TypeFastSlow == TypeFastSlow).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion

    }
}
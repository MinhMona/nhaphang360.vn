using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class BigPackageController
    {

        #region CRUD
        public static string Insert(string PackageCode, double Weight, double Volume, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = new tbl_BigPackage();
                a.PackageCode = PackageCode;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_BigPackage.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, string PackageCode, double Weight, double Volume, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.PackageCode = PackageCode;
                    a.Weight = Weight;
                    a.Volume = Volume;
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateWeight(int ID, double Weight)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {

                    a.Weight = Weight;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_BigPackage> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.PackageCode.Contains(s)).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_BigPackage> GetAllWithStatus(int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.Status == Status).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_BigPackage> GetAllNotHuy()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_BigPackage> ps = new List<tbl_BigPackage>();
                ps = dbe.tbl_BigPackage.Where(p => p.Status < 3).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static tbl_BigPackage GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_BigPackage GetByPackageCode(string PackageCode)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BigPackage a = dbe.tbl_BigPackage.Where(ad => ad.PackageCode == PackageCode).FirstOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        #endregion

        #region New
        public static int GetTotal_DK(string s)
        {
            var sql = @"select Total=Count(*) ";
            sql += "From tbl_BigPackage ";
            sql += "Where PackageCode Like N'%" + s + "%' ";
            int a = 0;
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }
        public static List<Warehouse> GetAllBySQL_DK(string s, int pageIndex, int pageSize)
        {
            var sql = @"select ID, PackageCode, Weight, Volume, CreatedDate,Status, ";
            sql += "CASE Status When 1 then N'<span class=\"white-text badge orange darken-2\">Bao hàng tại Trung Quốc</span>' ";
            sql += "When 2 then N'<span class=\"white-text badge green darken-2\">Đã nhận hàng tại Việt Nam</span>' ";
            sql += "When 3 then N'<span class=\"white-text badge black darken-2\">Hủy</span>' end as statusstring ";
            sql += "From tbl_BigPackage ";
            sql += "Where PackageCode Like N'%" + s + "%' ";
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<Warehouse> a = new List<Warehouse>();
            while (reader.Read())
            {
                var entity = new Warehouse();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["PackageCode"] != DBNull.Value)
                    entity.PackageCode = reader["PackageCode"].ToString();

                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = reader["Weight"].ToString();

                if (reader["Volume"] != DBNull.Value)
                    entity.Volume = reader["Volume"].ToString();


                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);

                if (reader["statusstring"] != DBNull.Value)
                    entity.StatusString = reader["statusstring"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm");
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public class Warehouse
        {
            public int ID { get; set; }
            public string PackageCode { get; set; }
            public string Weight { get; set; }
            public string Volume { get; set; }
            public int Status { get; set; }
            public string StatusString { get; set; }
            public string CreatedDate { get; set; }
        }
        #endregion
    }
}
using MB.Extensions;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace NHST.Controllers
{
    public class SmallPackageController1
    {
        #region CRUD
        public static string Insert(int BigPackageID, DateTime SendDate, string PackageCode, int UID, string UserPhone, double Weight, int Place,
            int StatusReceivePackage, int StatusPayment, string Note, string NoteCus, string BarcodeURL, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage1 o = new tbl_SmallPackage1();
                o.BigPackageID = BigPackageID;
                o.SendDate = SendDate;
                o.PackageCode = PackageCode;
                o.UID = UID;
                o.UserPhone = UserPhone;
                o.Weight = Weight;
                o.Place = Place;
                o.StatusReceivePackage = StatusReceivePackage;
                o.StatusPayment = StatusPayment;
                o.Note = Note;
                o.NoteCustomer = NoteCus;
                o.BarcodeURL = BarcodeURL;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage1.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, int BigPackageID, DateTime SendDate, string PackageCode, int UID, string UserPhone, float Weight, int Place,
            int StatusReceivePackage, int StatusPayment, string Note, string NoteCus, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.BigPackageID = BigPackageID;
                    o.SendDate = SendDate;
                    o.PackageCode = PackageCode;
                    o.UID = UID;
                    o.UserPhone = UserPhone;
                    o.Weight = Weight;
                    o.Place = Place;
                    o.StatusReceivePackage = StatusReceivePackage;
                    o.StatusPayment = StatusPayment;
                    o.Note = Note;
                    o.NoteCustomer = NoteCus;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();

                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        public static string UpdateSendDate(int ID, DateTime SendDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.SendDate = SendDate;

                    string kq = dbe.SaveChanges().ToString();

                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        public static string UpdateIsPayCash(int ID, bool IsPayCash)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.IsPayCash = IsPayCash;

                    string kq = dbe.SaveChanges().ToString();

                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        public static string UpdateStatusPayment(int ID, int StatusPayment)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.StatusPayment = StatusPayment;

                    string kq = dbe.SaveChanges().ToString();

                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        public static string UpdateStatusReceivePackage(int ID, int StatusReceivePackage)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.StatusReceivePackage = StatusReceivePackage;

                    string kq = dbe.SaveChanges().ToString();

                    return kq.ToString();
                }
                else
                    return null;

            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var op = dbe.tbl_SmallPackage1.Where(o => o.ID == ID).FirstOrDefault();
                if (op != null)
                {
                    dbe.tbl_SmallPackage1.Remove(op);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static void DeleteByBigPackageID(int BigPackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                var sps = GetAllByBigPackageID(BigPackageID);
                if (sps.Count > 0)
                {
                    foreach (var item in sps)
                    {
                        Delete(item.ID);
                    }
                }
            }
        }
        #endregion
        #region Select
        public static tbl_SmallPackage1 GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else
                    return null;

            }
        }
        public static tbl_SmallPackage1 GetByBarcode(string barcode)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_SmallPackage1.Where(od => od.PackageCode == barcode).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else
                    return null;

            }
        }
        public static List<tbl_SmallPackage1> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByPlace(int place)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.Place == place).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByBigPackageID(int BigPackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                //ps = dbe.tbl_SmallPackage1.Where(p => p.BigPackageID == BigPackageID).OrderByDescending(p => p.ID).ToList();
                ps = dbe.tbl_SmallPackage1.Where(p => p.BigPackageID == BigPackageID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllBySendDate(DateTime SendDate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.SendDate == SendDate).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByUserPhoneSendDateAndStatus(string UserPhone, DateTime SendDate, int StatusPayment)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UserPhone == UserPhone && p.SendDate == SendDate && p.StatusPayment == StatusPayment).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByUserPhoneSendDateAndPlace(string UserPhone, DateTime SendDate, int Place)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UserPhone == UserPhone && p.SendDate == SendDate && p.Place == Place).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByUserPhoneSendDate(string UserPhone, DateTime SendDate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UserPhone == UserPhone && p.SendDate == SendDate).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByUserPhoneSendDateAndStatusReceivePackage(string UserPhone, DateTime SendDate, int StatusReceivePackage)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UserPhone == UserPhone && p.SendDate == SendDate && p.StatusReceivePackage == StatusReceivePackage).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UID == UID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<getGroupby> GetAllByUserPhone_SQLGroup(string UserPhone)
        {
            var list = new List<getGroupby>();
            var sql = @"SELECT SendDate FROM tbl_SmallPackage1 WHERE UserPhone='" + UserPhone + "' GROUP BY SendDate";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new getGroupby();
                if (reader["SendDate"] != DBNull.Value)
                    entity.SendDate = Convert.ToDateTime(reader["SendDate"].ToString());
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<SmallPackCustom> GetAllByUserPhoneSendDate_SQLGroup(string UserPhone, DateTime SendDate)
        {
            var list = new List<SmallPackCustom>();
            var df = Convert.ToDateTime(SendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = @"SELECT s.ID, BigPackageID, s.PackageCode, s.UserPhone, s.Weight, s.Place, StatusReceivePackage, StatusPayment, u.IsSlow, s.IsPayCash, s.NoteCustomer "
                    + " FROM tbl_SmallPackage1 as s LEFT OUTER JOIN dbo.tbl_BigPackage1 AS u ON s.BigPackageID = u.ID"
                    + " WHERE UserPhone='" + UserPhone + "' AND s.SendDate = CONVERT(VARCHAR(24),'" + df + "',113)";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new SmallPackCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["BigPackageID"] != DBNull.Value)
                    entity.BigPackageID = reader["BigPackageID"].ToString().ToInt(0);
                if (reader["PackageCode"] != DBNull.Value)
                    entity.PackageCode = reader["PackageCode"].ToString();
                if (reader["UserPhone"] != DBNull.Value)
                    entity.UserPhone = reader["UserPhone"].ToString();
                if (reader["Weight"] != DBNull.Value)
                {
                    if (reader["Weight"].ToString().ToFloat(0) > 0)
                        entity.Weight = Convert.ToDouble(reader["Weight"].ToString());
                }
                if (reader["Place"] != DBNull.Value)
                    entity.Place = reader["Place"].ToString().ToInt(0);
                if (reader["NoteCustomer"] != DBNull.Value)
                    entity.NoteCustomer = reader["NoteCustomer"].ToString();
                if (reader["StatusReceivePackage"] != DBNull.Value)
                    entity.StatusReceivePackage = reader["StatusReceivePackage"].ToString().ToInt(0);
                if (reader["StatusPayment"] != DBNull.Value)
                    entity.StatusPayment = reader["StatusPayment"].ToString().ToInt(0);
                if (reader["IsSlow"] != DBNull.Value)
                    entity.IsSlow = reader["IsSlow"].ToString().ToBool();
                if (reader["IsPayCash"] != DBNull.Value)
                    entity.IsPayCash = reader["IsPayCash"].ToString().ToBool();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<SmallPackCustom> GetAllByUserPhoneSendDateNote_SQLGroup(string UserPhone, DateTime SendDate, string note)
        {
            var list = new List<SmallPackCustom>();
            var df = Convert.ToDateTime(SendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = @"SELECT s.ID, BigPackageID, s.PackageCode, s.UserPhone, s.Weight, s.Place, StatusReceivePackage, StatusPayment, u.IsSlow, s.NoteCustomer "
                    + " FROM tbl_SmallPackage1 as s LEFT OUTER JOIN dbo.tbl_BigPackage1 AS u ON s.BigPackageID = u.ID"
                    + " WHERE UserPhone='" + UserPhone + "' AND s.SendDate = CONVERT(VARCHAR(24),'" + df + "',113)"
                    + " AND Note Like N'%" + note + "%'";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new SmallPackCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["BigPackageID"] != DBNull.Value)
                    entity.BigPackageID = reader["BigPackageID"].ToString().ToInt(0);
                if (reader["PackageCode"] != DBNull.Value)
                    entity.PackageCode = reader["PackageCode"].ToString();
                if (reader["UserPhone"] != DBNull.Value)
                    entity.UserPhone = reader["UserPhone"].ToString();
                if (reader["Weight"] != DBNull.Value)
                {
                    if (reader["Weight"].ToString().ToFloat(0) > 0)
                        entity.Weight = Convert.ToDouble(reader["Weight"].ToString());
                }
                if (reader["NoteCustomer"] != DBNull.Value)
                    entity.NoteCustomer = reader["NoteCustomer"].ToString();
                if (reader["Place"] != DBNull.Value)
                    entity.Place = reader["Place"].ToString().ToInt(0);
                if (reader["StatusReceivePackage"] != DBNull.Value)
                    entity.StatusReceivePackage = reader["StatusReceivePackage"].ToString().ToInt(0);
                if (reader["StatusPayment"] != DBNull.Value)
                    entity.StatusPayment = reader["StatusPayment"].ToString().ToInt(0);
                if (reader["IsSlow"] != DBNull.Value)
                    entity.IsSlow = reader["IsSlow"].ToString().ToBool();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<SmallPackCustom> GetAllByStatusRecStatusPayFTSendDate_SQLGroup(int StatusReceivePackage, int StatusPayment, DateTime FSendDate,
            DateTime TSendDate, int place)
        {
            var list = new List<SmallPackCustom>();
            var df = Convert.ToDateTime(FSendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = Convert.ToDateTime(TSendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = @"SELECT s.ID, BigPackageID, s.PackageCode, s.UserPhone, s.Weight, s.Place,s.Note,s.SendDate, StatusReceivePackage, StatusPayment, u.IsSlow, s.IsPayCash "
                    + " FROM tbl_SmallPackage1 as s LEFT OUTER JOIN dbo.tbl_BigPackage1 AS u ON s.BigPackageID = u.ID"
                    + " WHERE s.SendDate >= CONVERT(VARCHAR(24),'" + df + "',113) AND s.SendDate < CONVERT(VARCHAR(24),'" + dt + "',113) ";
            if (StatusReceivePackage < 2)
                sql += " AND s.StatusReceivePackage = " + StatusReceivePackage + " ";
            if (StatusPayment < 2)
                sql += " AND s.StatusPayment = " + StatusPayment + "";
            if (place != 0)
                sql += " AND s.Place = " + place + " ";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new SmallPackCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["BigPackageID"] != DBNull.Value)
                    entity.BigPackageID = reader["BigPackageID"].ToString().ToInt(0);
                if (reader["PackageCode"] != DBNull.Value)
                    entity.PackageCode = reader["PackageCode"].ToString();
                if (reader["SendDate"] != DBNull.Value)
                    entity.SendDate = Convert.ToDateTime(reader["SendDate"].ToString());
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["UserPhone"] != DBNull.Value)
                    entity.UserPhone = reader["UserPhone"].ToString();
                if (reader["Weight"] != DBNull.Value)
                {
                    if (reader["Weight"].ToString().ToFloat(0) > 0)
                        entity.Weight = Convert.ToDouble(reader["Weight"].ToString());
                }
                if (reader["Place"] != DBNull.Value)
                    entity.Place = reader["Place"].ToString().ToInt(0);
                if (reader["StatusReceivePackage"] != DBNull.Value)
                    entity.StatusReceivePackage = reader["StatusReceivePackage"].ToString().ToInt(0);
                if (reader["StatusPayment"] != DBNull.Value)
                    entity.StatusPayment = reader["StatusPayment"].ToString().ToInt(0);
                if (reader["IsSlow"] != DBNull.Value)
                    entity.IsSlow = reader["IsSlow"].ToString().ToBool();
                if (reader["IsPayCash"] != DBNull.Value)
                    entity.IsPayCash = reader["IsPayCash"].ToString().ToBool();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<SmallPackCustom> GetAllByFTSendDateIsPayCash_SQLGroup(int place, DateTime FSendDate, DateTime TSendDate)
        {
            var list = new List<SmallPackCustom>();
            var df = Convert.ToDateTime(FSendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = Convert.ToDateTime(TSendDate).Date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = @"SELECT s.ID, BigPackageID, s.PackageCode, s.UserPhone, s.Weight, s.Place,s.Note,s.SendDate, StatusReceivePackage, StatusPayment, u.IsSlow, s.IsPayCash "
                    + " FROM tbl_SmallPackage1 as s LEFT OUTER JOIN dbo.tbl_BigPackage1 AS u ON s.BigPackageID = u.ID"
                    + " WHERE s.IsPayCash = 1 AND s.SendDate >= CONVERT(VARCHAR(24),'" + df + "',113) AND s.SendDate < CONVERT(VARCHAR(24),'" + dt + "',113) ";
            if (place != 0)
                sql += " AND s.Place = " + place + " ";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new SmallPackCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["BigPackageID"] != DBNull.Value)
                    entity.BigPackageID = reader["BigPackageID"].ToString().ToInt(0);
                if (reader["PackageCode"] != DBNull.Value)
                    entity.PackageCode = reader["PackageCode"].ToString();
                if (reader["SendDate"] != DBNull.Value)
                    entity.SendDate = Convert.ToDateTime(reader["SendDate"].ToString());
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                if (reader["UserPhone"] != DBNull.Value)
                    entity.UserPhone = reader["UserPhone"].ToString();
                if (reader["Weight"] != DBNull.Value)
                {
                    if (reader["Weight"].ToString().ToFloat(0) > 0)
                        entity.Weight = Convert.ToDouble(reader["Weight"].ToString());
                }
                if (reader["Place"] != DBNull.Value)
                    entity.Place = reader["Place"].ToString().ToInt(0);
                if (reader["StatusReceivePackage"] != DBNull.Value)
                    entity.StatusReceivePackage = reader["StatusReceivePackage"].ToString().ToInt(0);
                if (reader["StatusPayment"] != DBNull.Value)
                    entity.StatusPayment = reader["StatusPayment"].ToString().ToInt(0);
                if (reader["IsSlow"] != DBNull.Value)
                    entity.IsSlow = reader["IsSlow"].ToString().ToBool();
                if (reader["IsPayCash"] != DBNull.Value)
                    entity.IsPayCash = reader["IsPayCash"].ToString().ToBool();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_SmallPackage1> GetAllByUserPhone(string UserPhone)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> ps = new List<tbl_SmallPackage1>();
                ps = dbe.tbl_SmallPackage1.Where(p => p.UserPhone == UserPhone).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByFromPlaceFromdateToDate(int place, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> os = new List<tbl_SmallPackage1>();
                os = dbe.tbl_SmallPackage1.Where(od => od.Place >= place && od.SendDate >= fromdate && od.SendDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        public static List<tbl_SmallPackage1> GetAllByFromdateToDate(DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage1> os = new List<tbl_SmallPackage1>();
                os = dbe.tbl_SmallPackage1.Where(od => od.SendDate >= fromdate && od.SendDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        #endregion
        public class getGroupby
        {
            public DateTime SendDate { get; set; }
        }
        public class SmallPackCustom
        {
            public int ID { get; set; }
            public int BigPackageID { get; set; }
            public DateTime SendDate { get; set; }
            public string PackageCode { get; set; }
            public string UserPhone { get; set; }
            public string Note { get; set; }
            public string NoteCustomer { get; set; }
            public double Weight { get; set; }
            public int Place { get; set; }
            public int StatusReceivePackage { get; set; }
            public int StatusPayment { get; set; }
            public bool IsSlow { get; set; }
            public bool IsPayCash { get; set; }

        }
    }
}
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
    public class ExportRequestTurnController
    {
        #region CRUD
        public static string Insert(int MainOrderID, DateTime DateExport, double TotalPriceVND, double TotalPriceCYN,
            double TotalWeight, string Note, int ShippingTypeInVNID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ExportRequestTurn p = new tbl_ExportRequestTurn();
                p.MainOrderID = MainOrderID;
                p.DateExport = DateExport;
                p.TotalPriceVND = TotalPriceVND;
                p.TotalPriceCYN = TotalPriceCYN;
                p.TotalWeight = TotalWeight;
                p.Note = Note;
                p.ShippingTypeInVNID = ShippingTypeInVNID;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ExportRequestTurn.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }

        public static tbl_ExportRequestTurn Update(int ID, double TotalPriceVND, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_ExportRequestTurn.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                {
                    sv.TotalPriceVND = TotalPriceVND;
                    sv.ModifiedBy = Created;
                    sv.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return sv;
                }
                return null;
            }
        }
        public static string InsertWithUID(int UID, string Username, int MainOrderID, DateTime DateExport, double TotalPriceVND, double TotalPriceCYN,
           double TotalWeight, string Note, int ShippingTypeInVNID, DateTime CreatedDate, string CreatedBy, int TotalPackage, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ExportRequestTurn p = new tbl_ExportRequestTurn();
                p.UID = UID;
                p.Username = Username;
                p.MainOrderID = MainOrderID;
                p.DateExport = DateExport;
                p.TotalPriceVND = TotalPriceVND;
                p.TotalPriceCYN = TotalPriceCYN;
                p.TotalWeight = TotalWeight;
                p.TotalPackage = TotalPackage;
                p.Status = Status;
                p.StatusExport = 1;
                p.Note = Note;
                p.ShippingTypeInVNID = ShippingTypeInVNID;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ExportRequestTurn.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string UpdateStaffNote(int ID, string StaffNote)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.StaffNote = StaffNote;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOutStockDate(int ID, DateTime OutStockDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.OutStockDate = OutStockDate;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateStatus(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateStatusExport(int ID, int StatusExport, DateTime OutStockDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).FirstOrDefault();
                if (or != null)
                {
                    or.StatusExport = StatusExport;
                    or.OutStockDate = OutStockDate;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static tbl_ExportRequestTurn GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pa = dbe.tbl_ExportRequestTurn.Where(p => p.ID == ID).FirstOrDefault();
                if (pa != null)
                    return pa;
                else
                    return null;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == MainOrderID).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByCreatedBy(string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.CreatedBy == CreatedBy).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByCreatedByAndVCH(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.UID == UID && p.MainOrderID == 0).OrderByDescending(x => x.ID).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByVCH()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == 0).ToList();
                return pages;
            }
        }
        public static List<ExportRequest> GetByFilter_SqlHelperNew(int Status, string fd, string td)
        {
            var list = new List<ExportRequest>();
            var sql = @"SELECT  ShippingTypeInVNID, st.ShippingTypeVNName, ert.* FROM dbo.tbl_ExportRequestTurn as ert LEFT OUTER JOIN dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID where MainOrderID = 0 ";
            if (Status > 0)
                sql += " AND ert.Status =" + Status;
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ert.ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ExportRequest();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["CreatedDate"]));
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["DateExport"]));
                if (reader["OutStockDate"] != DBNull.Value)
                    entity.OutStockDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["OutStockDate"]));
                else
                    entity.OutStockDate = string.Empty;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);

                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"]);
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["ShippingTypeInVNID"] != DBNull.Value)
                    entity.ShippingTypeInVNID = reader["ShippingTypeInVNID"].ToString().ToInt();
                if (reader["ShippingTypeVNName"] != DBNull.Value)
                    entity.ShippingTypeVNName = reader["ShippingTypeVNName"].ToString();
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt();
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["StatusExport"] != DBNull.Value)
                    entity.StatusExport = reader["StatusExport"].ToString().ToInt();
                if (reader["TotalPackage"] != DBNull.Value)
                    entity.TotalPackage = reader["TotalPackage"].ToString().ToInt();
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_ExportRequestTurn> GetByFilter_SqlHelper(string fd, string td)
        {
            var list = new List<tbl_ExportRequestTurn>();
            var sql = @"SELECT * FROM dbo.tbl_ExportRequestTurn where MainOrderID = 0 ";

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_ExportRequestTurn();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);
                if (reader["TotalPriceCYN"] != DBNull.Value)
                    entity.TotalPriceCYN = Convert.ToDouble(reader["TotalPriceCYN"]);
                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"]);
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["ShippingTypeInVNID"] != DBNull.Value)
                    entity.ShippingTypeInVNID = reader["ShippingTypeInVNID"].ToString().ToInt();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_ExportRequestTurn> GetByMainOrderIDAndFTTD(int MainOrderID, DateTime FD, DateTime TD)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == MainOrderID && p.DateExport >= FD && p.DateExport < TD).ToList();
                return pages;
            }
        }
        public static List<ListDateExport> GetAllExportByMainOrderID(int MainOrderID)
        {
            var list = new List<ListDateExport>();
            var sql = @"SELECT CAST(DateExport AS DATE) AS DateExport, COUNT(*) as TotalRows ";
            sql += " FROM tbl_ExportRequestTurn ";
            sql += " WHERE MainOrderID = " + MainOrderID + " ";
            sql += " GROUP BY CAST(DateExport AS DATE)";
            sql += " ORDER BY 1";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ListDateExport();
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = Convert.ToDateTime(reader["DateExport"]);
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<InPhieuVCH> InPhieu(string fd, string td)
        {
            var list = new List<InPhieuVCH>();
            var sql = @"SELECT * FROM dbo.tbl_ExportRequestTurn where MainOrderID = 0 ";
           
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new InPhieuVCH();

                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Barcode"] != DBNull.Value)
                    entity.Barcode = reader["CreatedBy"].ToString();

                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);

                if (reader["FeeAdd"] != DBNull.Value)
                    entity.FeeAdd = Convert.ToDouble(reader["FeeAdd"]);

                if (reader["FeeVatTu"] != DBNull.Value)
                    entity.FeeVatTu = Convert.ToDouble(reader["FeeVatTu"]);

                if (reader["TotalPackage"] != DBNull.Value)
                    entity.TotalPackage = reader["StaffNote"].ToString().ToInt(0);
                
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        #endregion
        public class ListDateExport
        {
            public DateTime DateExport { get; set; }
        }

        public class InPhieuVCH
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Barcode { get; set; }
            public double TotalWeight { get; set; }
            public int TotalPackage { get; set; }
            public double TotalPriceVND { get; set; }
            public double FeeAdd { get; set; }
            public double FeeVatTu { get; set; }
        }

        public class ExportRequest
        {
            public int ID { get; set; }
            public string DateExport { get; set; }
            public double TotalPriceVND { get; set; }
            public double TotalWeight { get; set; }
            public int TotalPackage { get; set; }
            public string Note { get; set; }
            public string Username { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string OutStockDate { get; set; }
            public string CreatedBy { get; set; }
            public string StaffNote { get; set; }
            public int Status { get; set; }
            public int StatusExport { get; set; }
            public int ShippingTypeInVNID { get; set; }
            public string ShippingTypeVNName { get; set; }
        }

        public static string Remove(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var tw = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).SingleOrDefault();
                if (tw != null)
                {
                    dbe.tbl_ExportRequestTurn.Remove(tw);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else return null;
            }
        }
        public static List<ExportRequest> GetByFilter_SqlHelperNewWithPage(string searchName, string mvd, int Status, string fd, string td, int pageIndex, int pageSize)
        {
            var list = new List<ExportRequest>();
            var sql = @" SELECT ShippingTypeInVNID, st.ShippingTypeVNName, ert.* ,abc = STUFF( ";
            sql += " (SELECT ',' + ro.SmallPackageCode ";
            sql += " FROM dbo.tbl_RequestOutStock as ro  ";
            sql += " left outer join dbo.tbl_ExportRequestTurn as rt on rt.ID = ro.ExportRequestTurnID  ";
            sql += " where ro.ExportRequestTurnID = rt.ID and ert.ID = rt.ID ";
            sql += " FOR XML PATH ('')) ";
            sql += "  , 1, 1, '') FROM dbo.tbl_ExportRequestTurn as ert  ";
            sql += " LEFT OUTER JOIN dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID  ";
            sql += " where ert.MainOrderID = 0 and ert.ID = ert.ID and Username like N'%" + searchName + "%' ";
            if (Status > 0)
                sql += " AND ert.Status = " + Status;
            if (!string.IsNullOrEmpty(mvd))
            {
                sql += " and (N'%" + mvd + "%' ='' or (N'%" + mvd + "%' <> '' and exists (select 1 from tbl_RequestOutStock where ExportRequestTurnID = ert.ID and SmallPackageCode Like N'%" + mvd + "%'))) ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By ert.ID desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ExportRequest();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["CreatedDate"]));
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["DateExport"]));
                if (reader["OutStockDate"] != DBNull.Value)
                    entity.OutStockDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["OutStockDate"]));
                else
                    entity.OutStockDate = string.Empty;
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);

                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"]);
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["ShippingTypeInVNID"] != DBNull.Value)
                    entity.ShippingTypeInVNID = reader["ShippingTypeInVNID"].ToString().ToInt();
                if (reader["ShippingTypeVNName"] != DBNull.Value)
                    entity.ShippingTypeVNName = reader["ShippingTypeVNName"].ToString();
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt();
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["StatusExport"] != DBNull.Value)
                    entity.StatusExport = reader["StatusExport"].ToString().ToInt();
                if (reader["TotalPackage"] != DBNull.Value)
                    entity.TotalPackage = reader["TotalPackage"].ToString().ToInt();
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        //Old
        //public static List<ExportRequest> GetByFilter_SqlHelperNewWithPage(int Status, string fd, string td, int pageIndex, int pageSize)
        //{
        //    var list = new List<ExportRequest>();
        //    var sql = @"SELECT  ShippingTypeInVNID, st.ShippingTypeVNName, ert.* FROM dbo.tbl_ExportRequestTurn as ert LEFT OUTER JOIN dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID where MainOrderID = 0 ";
        //    if (Status > 0)
        //        sql += " AND ert.Status =" + Status;
        //    if (!string.IsNullOrEmpty(fd))
        //    {
        //        var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //        sql += " AND ert.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
        //    }
        //    if (!string.IsNullOrEmpty(td))
        //    {
        //        var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //        sql += " AND ert.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
        //    }
        //    sql += " Order By ert.ID desc OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
        //    var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
        //    int i = 1;
        //    while (reader.Read())
        //    {
        //        var entity = new ExportRequest();
        //        if (reader["ID"] != DBNull.Value)
        //            entity.ID = reader["ID"].ToString().ToInt(0);
        //        if (reader["CreatedBy"] != DBNull.Value)
        //            entity.CreatedBy = reader["CreatedBy"].ToString();
        //        if (reader["CreatedDate"] != DBNull.Value)
        //            entity.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["CreatedDate"]));
        //        if (reader["DateExport"] != DBNull.Value)
        //            entity.DateExport = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["DateExport"]));
        //        if (reader["OutStockDate"] != DBNull.Value)
        //            entity.OutStockDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["OutStockDate"]));
        //        else
        //            entity.OutStockDate = string.Empty;
        //        if (reader["TotalPriceVND"] != DBNull.Value)
        //            entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);

        //        if (reader["TotalWeight"] != DBNull.Value)
        //            entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"]);
        //        if (reader["StaffNote"] != DBNull.Value)
        //            entity.StaffNote = reader["StaffNote"].ToString();
        //        if (reader["ShippingTypeInVNID"] != DBNull.Value)
        //            entity.ShippingTypeInVNID = reader["ShippingTypeInVNID"].ToString().ToInt();
        //        if (reader["ShippingTypeVNName"] != DBNull.Value)
        //            entity.ShippingTypeVNName = reader["ShippingTypeVNName"].ToString();
        //        if (reader["UID"] != DBNull.Value)
        //            entity.UID = reader["UID"].ToString().ToInt();
        //        if (reader["Username"] != DBNull.Value)
        //            entity.Username = reader["Username"].ToString();
        //        if (reader["Status"] != DBNull.Value)
        //            entity.Status = reader["Status"].ToString().ToInt();
        //        if (reader["StatusExport"] != DBNull.Value)
        //            entity.StatusExport = reader["StatusExport"].ToString().ToInt();
        //        if (reader["TotalPackage"] != DBNull.Value)
        //            entity.TotalPackage = reader["TotalPackage"].ToString().ToInt();
        //        if (reader["Note"] != DBNull.Value)
        //            entity.Note = reader["Note"].ToString();
        //        i++;
        //        list.Add(entity);
        //    }
        //    reader.Close();
        //    return list;
        //}


        public static int GetTotal(string searchName, string mvd, int Status, string fd, string td)
        {
            var sql = @"SELECT  count(*) as Total  ";
            sql += "  FROM dbo.tbl_ExportRequestTurn as ert  ";
            sql += " LEFT OUTER JOIN dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID  ";
            sql += " where ert.MainOrderID = 0 and ert.ID = ert.ID and Username like N'%" + searchName + "%' ";
            if (Status > 0)
                sql += " AND ert.Status =" + Status;
            if (!string.IsNullOrEmpty(mvd))
            {
                sql += " and (N'%" + mvd + "%' ='' or (N'%" + mvd + "%' <> '' and exists (select 1 from tbl_RequestOutStock where ExportRequestTurnID = ert.ID and SmallPackageCode Like N'%" + mvd + "%'))) ";
            }
            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND ert.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int total = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    total = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return total;
        }

        //public static int GetTotal(int Status, string fd, string td)
        //{
        //    var sql = @"SELECT  count(*) as Total FROM dbo.tbl_ExportRequestTurn as ert LEFT OUTER JOIN dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID where MainOrderID = 0 ";
        //    if (Status > 0)
        //        sql += " AND ert.Status =" + Status;
        //    if (!string.IsNullOrEmpty(fd))
        //    {
        //        var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //        sql += " AND ert.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
        //    }
        //    if (!string.IsNullOrEmpty(td))
        //    {
        //        var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
        //        sql += " AND ert.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
        //    }

        //    var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
        //    int total = 0;
        //    while (reader.Read())
        //    {
        //        if (reader["Total"] != DBNull.Value)
        //            total = reader["Total"].ToString().ToInt(0);
        //    }
        //    reader.Close();
        //    return total;
        //}
    }
}
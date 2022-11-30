using MB.Extensions;
using NHST.Bussiness;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace NHST.Controllers
{
    public class RequestOutStockController
    {
        #region CRUD
        public static string Insert(int SmallPackageID, string SmallPackageCode, int MainOrderID,
            int Status, DateTime CreatedDate, string CreatedBy, int ExportRequestTurnID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_RequestOutStock p = new tbl_RequestOutStock();
                p.SmallPackageID = SmallPackageID;
                p.SmallPackageCode = SmallPackageCode;
                p.MainOrderID = MainOrderID;
                p.TransportationID = 0;
             
                p.Status = Status;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.ExportRequestTurnID = ExportRequestTurnID;
                dbe.tbl_RequestOutStock.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string InsertT(int SmallPackageID, string SmallPackageCode, int TransportationID, int MainOrderID,
            int Status, DateTime CreatedDate, string CreatedBy, int ExportRequestTurnID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_RequestOutStock p = new tbl_RequestOutStock();
                p.SmallPackageID = SmallPackageID;
                p.SmallPackageCode = SmallPackageCode;
                p.MainOrderID = 0;
                p.TransportationID = TransportationID;
                p.MainOrderID = MainOrderID;
                p.Status = Status;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                p.ExportRequestTurnID = ExportRequestTurnID;
                dbe.tbl_RequestOutStock.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_RequestOutStock.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_RequestOutStock.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_RequestOutStock.Remove(p);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_RequestOutStock> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_RequestOutStock> pages = new List<tbl_RequestOutStock>();
                pages = dbe.tbl_RequestOutStock.Where(p => p.SmallPackageCode.Contains(s)).OrderBy(a => a.Status).ToList();
                return pages;
            }
        }
        public static List<tbl_RequestOutStock> GetAllSQLHelper(string s, int page, int maxrows)
        {
            var list = new List<tbl_RequestOutStock>();
            var sql = @"SELECT * from tbl_RequestOutStock Where SmallPackageCode like N'%" + s + "%'";
            sql += " ORDER BY status OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new tbl_RequestOutStock();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = Convert.ToInt32(reader["ID"]);
                if (reader["SmallPackageID"] != DBNull.Value)
                    entity.SmallPackageID = Convert.ToInt32(reader["SmallPackageID"]);
                if (reader["SmallPackageCode"] != DBNull.Value)
                    entity.SmallPackageCode = reader["SmallPackageID"].ToString();
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = Convert.ToInt32(reader["MainOrderID"]);
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = Convert.ToInt32(reader["MainOrderID"]);
                if (reader["Status"] != DBNull.Value)
                    entity.Status = Convert.ToInt32(reader["Status"]);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["TransportationID"] != DBNull.Value)
                    entity.Status = Convert.ToInt32(reader["TransportationID"]);
                if (reader["ExportRequestTurnID"] != DBNull.Value)
                    entity.ExportRequestTurnID = Convert.ToInt32(reader["ExportRequestTurnID"]);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<RequestOutStoc> GetAllSQLHelper1(string s)
        {
            var list = new List<RequestOutStoc>();
            var sql = @"SELECT ro.*, sp.OrderTransactionCode, sp.Weight, sp.DateInLasteWareHouse, sp.DateOutWarehouse, er.Note, er.ShippingTypeVNName ";
            sql += " FROM     dbo.tbl_RequestOutStock AS ro LEFT OUTER JOIN";
            sql += " dbo.tbl_SmallPackage AS sp ON ro.SmallPackageID = sp.ID LEFT OUTER JOIN";
            sql += " (SELECT ert.ID, ShippingTypeInVNID, st.ShippingTypeVNName, ert.Note FROM dbo.tbl_ExportRequestTurn as ert LEFT OUTER JOIN";
            sql += " dbo.tbl_ShippingTypeVN AS st ON ert.ShippingTypeInVNID = st.ID ";
            sql += " ) AS er ON ro.ExportRequestTurnID = er.ID";
            sql += " Where ro.SmallPackageCode like N'%" + s + "%'";
            sql += " ORDER BY ro.status";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new RequestOutStoc();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = Convert.ToInt32(reader["ID"]);
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                double weight = 0;
                if (reader["Weight"] != DBNull.Value)
                {
                    if (reader["Weight"].ToString().ToFloat(0) > 0)
                    {
                        weight = Math.Round(Convert.ToDouble(reader["Weight"]), 1);
                    }
                }
                entity.Weight = weight.ToString();
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString();
                if (reader["TransportationID"] != DBNull.Value)
                    entity.TransportationID = reader["TransportationID"].ToString();
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.UserNameCus = reader["CreatedBy"].ToString();
                if (reader["DateInLasteWareHouse"] != DBNull.Value)
                    entity.DateInVNWarehouse = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["DateInLasteWareHouse"]));
                else
                    entity.DateInVNWarehouse = string.Empty;

                if (reader["DateOutWarehouse"] != DBNull.Value)
                    entity.DateExWarehouse = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["DateOutWarehouse"]));
                else
                    entity.DateExWarehouse = string.Empty;

               int Status = 0;
                if (reader["Status"] != DBNull.Value)
                    Status = Convert.ToInt32(reader["Status"]);
                entity.Status = Status;
                entity.Statusstr = PJUtils.requestOutStockStatus(Status);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(reader["CreatedDate"]));
                else
                    entity.CreatedDate = string.Empty;
                if (reader["ShippingTypeVNName"] != DBNull.Value)
                    entity.HTVC = reader["ShippingTypeVNName"].ToString();
                if (reader["Note"] != DBNull.Value)
                    entity.Note = reader["Note"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_RequestOutStock> GetByExportRequestTurnID(int ExportRequestTurnID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_RequestOutStock> pages = new List<tbl_RequestOutStock>();
                pages = dbe.tbl_RequestOutStock.Where(p => p.ExportRequestTurnID == ExportRequestTurnID).OrderBy(a => a.Status).ToList();
                return pages;
            }
        }
        public static tbl_RequestOutStock GetBySmallpackageID(int smallpackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_RequestOutStock.Where(p => p.SmallPackageID == smallpackageID).FirstOrDefault();
                return pages;
            }
        }
        public static tbl_RequestOutStock GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_RequestOutStock.Where(p => p.ID == ID).FirstOrDefault();
                return pages;
            }
        }
        #endregion
        public class RequestOutStoc
        {
            public int ID { get; set; }
            public string OrderTransactionCode { get; set; }
            public string Weight { get; set; }
            public string MainOrderID { get; set; }
            public string TransportationID { get; set; }
            public string UserNameCus { get; set; }
            public string DateInVNWarehouse { get; set; }
            public string DateExWarehouse { get; set; }
            public string Statusstr { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string HTVC { get; set; }
            public string Note { get; set; }
        }
        public static string DeleteByExportID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_RequestOutStock.Where(pa => pa.ExportRequestTurnID == ID).FirstOrDefault();
                if (p != null)
                {
                    dbe.tbl_RequestOutStock.Remove(p);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
    }
}
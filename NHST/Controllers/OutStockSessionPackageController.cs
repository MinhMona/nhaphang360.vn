using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;
using System.Data;
using WebUI.Business;

namespace NHST.Controllers
{
    public class OutStockSessionPackageController
    {
        #region CRUD
        public static string Insert(int OutStockSessionID, int SmallPackageID,string OrderTransactionCode, int MainOrderID,
            int TransportationID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {
                tbl_OutStockSessionPackage user = new tbl_OutStockSessionPackage();
                user.OutStockSessionID = OutStockSessionID;
                user.SmallPackageID = SmallPackageID;
                user.OrderTransactionCode = OrderTransactionCode;
                user.MainOrderID = MainOrderID;
                user.TransportationID = TransportationID;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                dbe.tbl_OutStockSessionPackage.Add(user);
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }

        }
        public static string update(int ID, DateTime DateOutStock, double DayInWarehouse, double FeeWarehouse)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_OutStockSessionPackage.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.DateOutStock = DateOutStock;
                    a.DayInWarehouse = DayInWarehouse;
                    a.FeeWarehouse = FeeWarehouse;
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
        public static List<string> GetByOutStockSessionIDGroupByMainOrderID(int OutStockSessionID)
        {
            var list = new List<string>();
            var sql = @"select MainOrderID from tbl_OutStockSessionPackage where OutStockSessionID=" + OutStockSessionID 
                + " and MainOrderID > 0  group by MainOrderID";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                string shopid = "";
                if (reader["MainOrderID"] != DBNull.Value)
                    shopid = reader["MainOrderID"].ToString();
                list.Add(shopid);
            }
            reader.Close();
            return list;
        }


        public static List<string> GetByOutStockSessionIDGroupByTransportationID(int OutStockSessionID)
        {
            var list = new List<string>();
            var sql = @"select TransportationID from tbl_OutStockSessionPackage where OutStockSessionID=" + OutStockSessionID
                + " and TransportationID > 0  group by TransportationID";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                string shopid = "";
                if (reader["TransportationID"] != DBNull.Value)
                    shopid = reader["TransportationID"].ToString();
                list.Add(shopid);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_OutStockSessionPackage> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSessionPackage> las = new List<tbl_OutStockSessionPackage>();
                las = dbe.tbl_OutStockSessionPackage.ToList();
                return las;
            }
        }

        public static string Remove(int MainOrderID, int ID)
        {
            using (var db = new NHSTEntities())
            {
                var p = db.tbl_OutStockSessionPackage.Where(x => x.MainOrderID == MainOrderID && x.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    db.tbl_OutStockSessionPackage.Remove(p);
                    db.SaveChanges();
                    return "ok";
                }
                return null;
            }
        }

        public static List<tbl_OutStockSessionPackage> GetAllByOutStockSessionID(int OutStockSessionID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSessionPackage> las = new List<tbl_OutStockSessionPackage>();
                las = dbe.tbl_OutStockSessionPackage.Where(a => a.OutStockSessionID == OutStockSessionID).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_OutStockSessionPackage> GetAllByOutStockSessionIDAndMainOrderID(int OutStockSessionID, int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSessionPackage> las = new List<tbl_OutStockSessionPackage>();
                las = dbe.tbl_OutStockSessionPackage.Where(a => a.OutStockSessionID == OutStockSessionID && a.MainOrderID == MainOrderID).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_OutStockSessionPackage> GetAllByOutStockSessionIDAndTransporationID(int OutStockSessionID, int TransportationID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OutStockSessionPackage> las = new List<tbl_OutStockSessionPackage>();
                las = dbe.tbl_OutStockSessionPackage.Where(a => a.OutStockSessionID == OutStockSessionID && a.TransportationID == TransportationID).OrderByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        #endregion
    }
}
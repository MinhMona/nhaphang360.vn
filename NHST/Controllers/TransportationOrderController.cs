using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;
using System.Globalization;

namespace NHST.Controllers
{
    public class TransportationOrderController
    {
        #region CRUD
        public static string Insert(int UID, string Username, int WarehouseFromID, int WarehouseID, int ShippingTypeID, int Status, double TotalWeight,
           double Currency, double TotalPrice, string Description, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrder p = new tbl_TransportationOrder();
                p.UID = UID;
                p.Username = Username;
                p.WarehouseFromID = WarehouseFromID;
                p.WarehouseID = WarehouseID;
                p.ShippingTypeID = ShippingTypeID;
                p.Status = Status;
                p.TotalWeight = TotalWeight;
                p.Currency = Currency;
                p.TotalPrice = TotalPrice;
                p.Description = Description;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_TransportationOrder.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string InsertNew(int UID, string Username, int WarehouseFromID, int WarehouseID, 
            int ShippingTypeID, int Status, double TotalWeight, double Currency, 
            double CheckProductFee, double PackagedFee, double InsurranceFee, 
            double TotalCODTQCYN, double TotalCODTQVND, double TotalPrice, string Description, 
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrder p = new tbl_TransportationOrder();
                p.UID = UID;
                p.Username = Username;
                p.WarehouseFromID = WarehouseFromID;
                p.WarehouseID = WarehouseID;
                p.ShippingTypeID = ShippingTypeID;
                p.Status = Status;
                p.TotalWeight = TotalWeight;
                p.Currency = Currency;
                p.CheckProductFee = CheckProductFee;
                p.PackagedFee = PackagedFee;
                p.InsurranceFee = InsurranceFee;
                p.TotalCODTQCYN = TotalCODTQCYN;
                p.TotalCODTQVND = TotalCODTQVND;
                p.TotalPrice = TotalPrice;
                p.Description = Description;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_TransportationOrder.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int UID, string Username, int WarehouseFromID, int WarehouseID, int ShippingTypeID, int Status, double TotalWeight,
            double Currency, double TotalPrice, string Description, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.UID = UID;
                    p.Username = Username;
                    p.WarehouseFromID = WarehouseFromID;
                    p.WarehouseID = WarehouseID;
                    p.ShippingTypeID = ShippingTypeID;
                    p.Status = Status;
                    p.TotalWeight = TotalWeight;
                    p.Currency = Currency;
                    p.TotalPrice = TotalPrice;
                    p.Description = Description;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateNew(int ID, int UID, string Username, int WarehouseFromID, int WarehouseID, int ShippingTypeID, int Status, double TotalWeight,
            double Currency, double CheckProductFee, double PackagedFee, double TotalCODTQCYN, double TotalCODTQVND,
            double InsurranceFee, double TotalPrice, string Description, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.UID = UID;
                    p.Username = Username;
                    p.WarehouseFromID = WarehouseFromID;
                    p.WarehouseID = WarehouseID;
                    p.ShippingTypeID = ShippingTypeID;
                    p.Status = Status;
                    p.TotalWeight = TotalWeight;
                    p.Currency = Currency;
                    p.CheckProductFee = CheckProductFee;
                    p.PackagedFee = PackagedFee;
                    p.TotalCODTQCYN = TotalCODTQCYN;
                    p.TotalCODTQVND = TotalCODTQVND;
                    p.InsurranceFee = InsurranceFee;
                    p.TotalPrice = TotalPrice;
                    p.Description = Description;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateWeightTotalPrice(int ID, double TotalWeight, double totalPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.TotalWeight = TotalWeight;
                    p.TotalPrice = totalPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatusAndDeposited(int ID, double Deposited, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Deposited = Deposited;
                    p.Status = Status;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }



        public static string UpdateTotalWeightTotalPriceStatus(int ID, int Status, double TotalWeight,
            double TotalPrice, string Description, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = Status;
                    p.TotalWeight = TotalWeight;
                    p.TotalPrice = TotalPrice;
                    p.Description = Description;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalWeightTotalPrice(int ID, double TotalWeight,
           double TotalPrice, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.TotalWeight = TotalWeight;
                    p.TotalPrice = TotalPrice;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        public static string UpdateWarehouseFee(int ID, double WarehouseFee)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_TransportationOrder.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.WarehouseFee = WarehouseFee;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "1";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_TransportationOrder> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.Username.Contains(s)).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_TransportationOrder> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }
        public static List<tbl_TransportationOrder> GetByUIDAndPackageCode(int UID, string PackageCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> returnPages = new List<tbl_TransportationOrder>();
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                if (pages.Count > 0)
                {
                    foreach (var p in pages)
                    {
                        var packages = SmallPackageController.GetByTransportationOrderID(p.ID);
                        if (packages.Count > 0)
                        {
                            foreach (var s in packages)
                            {
                                if (s.OrderTransactionCode == PackageCode)
                                {
                                    returnPages.Add(p);
                                    break;
                                }
                            }
                        }
                    }
                }
                return returnPages;
            }
        }

        public static List<tbl_TransportationOrder> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_TransportationOrder> pages = new List<tbl_TransportationOrder>();
                pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID).OrderByDescending(a => a.CreatedDate).ToList();
                return pages;
            }
        }

        public static tbl_TransportationOrder GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_TransportationOrder.Where(p => p.UID == UID && p.ID == ID).FirstOrDefault();
                return pages;
            }
        }
        public static tbl_TransportationOrder GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_TransportationOrder page = dbe.tbl_TransportationOrder.Where(p => p.ID == ID).FirstOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion

        public static string Report_TotalItem(int pricefrom, int priceto)
        {
            var sql = @"select Count(*) as Total from tbl_Account as ac";
            sql += " left outer join(select Sum(CONVERT(numeric(18, 2), TotalPrice)) as Total, UID from tbl_TransportationOrder group by UID) as p ON p.UID = ac.ID ";
            sql += " where ac.RoleID = 1 and p.Total > " + pricefrom + " and p.Total < " + priceto + " ";

            string Total = "0";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    Total = reader["Total"].ToString();
            }

            reader.Close();
            return Total;
        }

        public static List<tbl_TransportationOrder> GetAllBySQL(string fd, string td, int WareHouseTo, int ShippingType)
        {
            var list = new List<tbl_TransportationOrder>();
            var sql = @"SELECT  mo.ID, mo.Username, mo.WarehouseID, mo.CreatedDate, mo.Status, mo.ShippingTypeID, mo.TotalWeight, mo.TotalPrice ";
            sql += " From tbl_TransportationOrder as mo";
            sql += "        Where UID > 0 ";
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

            if (WareHouseTo > 0)
            {
                sql += " AND WarehouseID=" + WareHouseTo + " ";
            }

            if (ShippingType > 0)
            {
                sql += " AND ShippingTypeID=" + ShippingType + " ";
            }
            sql += " Order By ID desc";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            while (reader.Read())
            {
                var entity = new tbl_TransportationOrder();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["WarehouseID"] != DBNull.Value)
                    entity.WarehouseID = reader["WarehouseID"].ToString().ToInt();
                if (reader["ShippingTypeID"] != DBNull.Value)
                    entity.ShippingTypeID = reader["ShippingTypeID"].ToString().ToInt();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"].ToString());
                if (reader["TotalPrice"] != DBNull.Value)
                    entity.TotalPrice = Convert.ToDouble(reader["TotalPrice"].ToString());
              
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        #region New
        public static int GetTotalBySQL(string searchType, string searchname, string fd, string td, string warehousefromid, string warehouseid, string shippingType, string status, string pricefrom, string priceto)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_TransportationOrder  as a Left outer join (SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.TransportationOrderCode  AS [text()] FROM tbl_TransportaionOrderDetail ST1 WHERE ST1.TransportationOrderID = ST2.ID FOR XML PATH ('')),2,1000) TransportationOrderCode FROM tbl_TransportationOrder ST2) se on se.ID=a.ID ";
            if (!string.IsNullOrEmpty(searchType))
            {
                try
                {
                    int ex = Convert.ToInt32(searchType);
                    if (ex == 3)//
                    {
                        sql += "where a.Username like N'%" + searchname + "%' ";
                    }
                    if (ex == 2)
                    {
                        sql += "where se.TransportationOrderCode like N'%" + searchname + "%' ";
                    }
                    if (ex == 1)
                        sql += "where a.ID like N'%" + searchname + "%' ";
                }
                catch
                {

                }
            }
            else
            {
                sql += " where a.Username like N'%%' ";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += "AND a.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND a.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(warehouseid) && Convert.ToInt32(warehouseid) != 0)
                sql += "and a.WarehouseID=" + warehouseid + " ";
            if (!string.IsNullOrEmpty(warehousefromid) && Convert.ToInt32(warehousefromid) != 0)
                sql += "and a.WarehouseFromID=" + warehousefromid + " ";
            if (!string.IsNullOrEmpty(shippingType) && Convert.ToInt32(shippingType) != 0)
                sql += "and a.ShippingTypeID=" + shippingType + " ";
            if (!string.IsNullOrEmpty(pricefrom) && Convert.ToDouble(pricefrom) != 0)
                sql += " AND a.TotalPrice >= CONVERT(float," + pricefrom + ")";
            if (!string.IsNullOrEmpty(priceto) && Convert.ToDouble(priceto) != 0)
                sql += " AND a.TotalPrice <= CONVERT(float," + priceto + ")";
            if (!string.IsNullOrEmpty(status))
            {
                if (status != "-1")
                    sql += " AND a.Status in (" + status + ")";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            return a;
        }
        public static List<TransportationOrderList> GetBySQLWithDK(string searchType, string searchname, string fd, string td, string warehousefromid, string warehouseid, string shippingType, string status, string pricefrom, string priceto, int pageIndex, int pageSize, int sort)
        {
            var sql = @"select a.ID,a.UID,a.Username,se.TransportationOrderCode, a.TotalPrice,a.WarehouseID,a.WarehouseFromID,a.ShippingTypeID,a.Status,a.TotalWeight,a.Deposited,a.CreatedDate ";
            sql += "from tbl_TransportationOrder  as a Left outer join (SELECT DISTINCT ST2.ID,SUBSTRING((SELECT ','+ST1.TransportationOrderCode  AS [text()] FROM tbl_TransportaionOrderDetail ST1 WHERE ST1.TransportationOrderID = ST2.ID FOR XML PATH ('')),2,1000) TransportationOrderCode FROM tbl_TransportationOrder ST2) se on se.ID=a.ID ";
            if (!string.IsNullOrEmpty(searchType))
            {
                try
                {
                    int ex = Convert.ToInt32(searchType);
                    if (ex == 3)//
                    {
                        sql += "where a.Username like N'%" + searchname + "%' ";
                    }
                    if (ex == 2)
                    {
                        sql += "where se.TransportationOrderCode like N'%" + searchname + "%' ";
                    }
                    if (ex == 1)
                        sql += "where a.ID like N'%" + searchname + "%' ";
                }
                catch
                {

                }
            }
            else
            {
                sql += " where a.Username like N'%%' ";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null);
                sql += " AND a.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113) ";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null);
                sql += "AND a.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113) ";
            }
            if (!string.IsNullOrEmpty(warehousefromid) && Convert.ToInt32(warehousefromid) != 0)
                sql += "and a.WarehouseFromID=" + warehousefromid + " ";
            if (!string.IsNullOrEmpty(warehouseid) && Convert.ToInt32(warehouseid) != 0)
                sql += "and a.WarehouseID=" + warehouseid + " ";
            if (!string.IsNullOrEmpty(shippingType) && Convert.ToInt32(shippingType) != 0)
                sql += "and a.ShippingTypeID=" + shippingType + " ";
            if (!string.IsNullOrEmpty(pricefrom) && Convert.ToDouble(pricefrom) != 0)
                sql += " AND a.TotalPrice >= CONVERT(float," + pricefrom + ")";
            if (!string.IsNullOrEmpty(priceto) && Convert.ToDouble(priceto) != 0)
                sql += " AND a.TotalPrice <= CONVERT(float," + priceto + ")";
            if (!string.IsNullOrEmpty(status))
            {
                if (status != "-1")
                    sql += " AND a.Status in (" + status + ")";
            }

           // sql += "order by a.id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

            if (sort == 0)
            {
                sql += " ORDER BY a.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 1)
            {
                sql += " ORDER BY a.ID ASC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 2)
            {
                sql += " ORDER BY a.ID DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 3)
            {
                sql += " ORDER BY a.Status ASC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }
            else if (sort == 4)
            {
                sql += " ORDER BY a.Status DESC OFFSET (" + pageIndex + " * " + pageSize + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<TransportationOrderList> a = new List<TransportationOrderList>();
            while (reader.Read())
            {
                var entity = new TransportationOrderList();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);

                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["WarehouseFromID"] != DBNull.Value)
                    entity.WarehouseFromID = reader["WarehouseFromID"].ToString().ToInt(0);

                if (reader["WarehouseID"] != DBNull.Value)
                    entity.WarehouseID = reader["WarehouseID"].ToString().ToInt(0);

                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"].ToString());

                if (reader["Deposited"] != DBNull.Value)
                {
                    entity.Deposited = Convert.ToDouble(reader["Deposited"].ToString());
                    entity.DepositedString = Convert.ToDouble(reader["Deposited"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }
                if (reader["TotalPrice"] != DBNull.Value)
                {
                    entity.TotalPrice = Convert.ToDouble(reader["TotalPrice"].ToString());
                    entity.TotalPriceString = Convert.ToDouble(reader["TotalPrice"].ToString()).ToString("0,0", CultureInfo.InvariantCulture) + " VND";
                }
                if (reader["Status"] != DBNull.Value)
                {
                    entity.Status = reader["Status"].ToString().ToInt(0);
                }
                if (reader["ShippingTypeID"] != DBNull.Value)
                {
                    entity.ShippingTypeID = reader["ShippingTypeID"].ToString().ToInt(0);
                }

                if (reader["CreatedDate"] != DBNull.Value)
                {

                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    entity.CreatedDateString = Convert.ToDateTime(reader["CreatedDate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                }
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public partial class TransportationOrderList
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double TotalPrice { get; set; }
            public string TotalPriceString { get; set; }
            public double Deposited { get; set; }
            public string DepositedString { get; set; }
            public double TotalWeight { get; set; }
            public int WarehouseID { get; set; }
            public string WarehouseName { get; set; }
            public int WarehouseFromID { get; set; }
            public string WarehouseFromName { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedDateString { get; set; }

            public int ShippingTypeID { get; set; }
        }
        #endregion
    }
}
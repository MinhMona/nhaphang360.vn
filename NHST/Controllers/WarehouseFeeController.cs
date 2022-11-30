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
    public class WarehouseFeeController
    {
        #region CRUD
        public static string Insert(int WarehouseFromID, int WarehouseID, double WeightFrom, double WeightTo, double Price,
            int ShippingTypeToWareHouseID, int ShippingType, bool IsHidden, bool IsHelpMoving, DateTime CreatedDate,
            string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_WarehouseFee c = new tbl_WarehouseFee();
                c.WarehouseFromID = WarehouseFromID;
                c.WarehouseID = WarehouseID;
                c.WeightFrom = WeightFrom;
                c.WeightTo = WeightTo;
                c.Price = Price;
                c.ShippingTypeToWareHouseID = ShippingTypeToWareHouseID;
                c.ShippingType = ShippingType;
                c.IsHidden = IsHidden;
                c.IsHelpMoving = IsHelpMoving;
                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_WarehouseFee.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, int WarehouseFromID, int WarehouseID, double WeightFrom, double WeightTo, double Price,
            int ShippingTypeToWareHouseID, int ShippingType, bool IsHidden, bool IsHelpMoving, DateTime ModifiedDate,
            string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFee.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.WarehouseFromID = WarehouseFromID;
                    c.WarehouseID = WarehouseID;
                    c.WeightFrom = WeightFrom;
                    c.WeightTo = WeightTo;
                    c.Price = Price;
                    c.ShippingTypeToWareHouseID = ShippingTypeToWareHouseID;
                    c.ShippingType = ShippingType;
                    c.IsHidden = IsHidden;
                    c.IsHelpMoving = IsHelpMoving;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_WarehouseFee> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseID(int WarehouseID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseIDAndIsHidden(int WarehouseID, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static List<tbl_WarehouseFee> GetAllWithWarehouseIDAndTypeAndIsHidden(int WarehouseID, int ShippingType, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.ShippingType == ShippingType && c.IsHidden == IsHidden).OrderBy(w => w.WarehouseID).ToList();
                return cs;
            }
        }
        public static tbl_WarehouseFee GetAllWithWarehouseIDAndTypeAndWeightAndIsHidden(int WarehouseID, int ShippingType, double weight, bool IsHidden)
        {
            using (var dbe = new NHSTEntities())
            {
                var cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseID == WarehouseID && c.ShippingType == ShippingType && c.IsHidden == IsHidden && c.WeightFrom < weight && c.WeightTo >= weight).FirstOrDefault();
                if (cs != null)
                    return cs;
                else return null;
            }
        }
        public static tbl_WarehouseFee CheckBeforeInsert(int WarehouseFromID, int WarehouseID, int ShippingTypeToWareHouseID, bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                var cs = dbe.tbl_WarehouseFee.Where(c => c.WarehouseFromID == WarehouseFromID &&
                                                         c.WarehouseID == WarehouseID &&
                                                         c.ShippingTypeToWareHouseID == ShippingTypeToWareHouseID &&
                                                         c.IsHelpMoving == IsHelpMoving).FirstOrDefault();
                if (cs != null)
                    return cs;
                else return null;
            }
        }
        public static List<tbl_WarehouseFee> GetByHelpMoving(bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c => c.IsHelpMoving == IsHelpMoving).ToList();
                return cs;

            }
        }
        public static List<tbl_WarehouseFee> GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(int WarehouseFromID, int WarehouseID,int ShippingTypeToWareHouseID, bool IsHelpMoving)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_WarehouseFee> cs = new List<tbl_WarehouseFee>();
                cs = dbe.tbl_WarehouseFee.Where(c =>c.WarehouseFromID == WarehouseFromID && c.WarehouseID == WarehouseID && c.ShippingTypeToWareHouseID == ShippingTypeToWareHouseID && c.IsHelpMoving == IsHelpMoving).ToList();
                return cs;

            }
        }        
        public static tbl_WarehouseFee GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_WarehouseFee.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion

        #region New
        public static int GetTotal(string s)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_WarehouseFee as a left join tbl_WarehouseFrom as b on a.WarehouseFromID = b.ID ";
            sql += "left join tbl_Warehouse as c on a.WarehouseID = c.ID ";
            sql += "left join tbl_ShippingTypeToWareHouse as d on d.ID=a.ShippingType ";
            sql += "Where b.WareHouseName Like N'%" + s + "%' or c.WareHouseName like N'%" + s + "%' ";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);
            }
            reader.Close();
            return a;
        }
        public static List<WareHouseFeeTQVN> GetAllBySQL(string s, int pageIndex, int pageSize)
        {
            var sql = @"select wareFrom=b.WareHouseName,wareTo=c.WareHouseName,shipName=d.ShippingTypeName, a.* ";
            sql += "from tbl_WarehouseFee as a left join tbl_WarehouseFrom as b on a.WarehouseFromID = b.ID ";
            sql += "left join tbl_Warehouse as c on a.WarehouseID = c.ID ";
            sql += "left join tbl_ShippingTypeToWareHouse as d on d.ID=a.ShippingType ";
            sql += "Where b.WareHouseName Like N'%" + s + "%' or c.WareHouseName like N'%" + s + "%' ";
            sql += "order by IsHelpMoving DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<WareHouseFeeTQVN> a = new List<WareHouseFeeTQVN>();
            while (reader.Read())
            {
                var entity = new WareHouseFeeTQVN();
                if (reader["ID"] != DBNull.Value)
                    entity.warehouseFee.ID = reader["ID"].ToString().ToInt(0);

                if (reader["wareFrom"] != DBNull.Value)
                    entity.wareFrom = reader["wareFrom"].ToString();

                if (reader["wareTo"] != DBNull.Value)
                    entity.wareTo = reader["wareTo"].ToString();

                if (reader["WeightFrom"] != DBNull.Value)
                    entity.warehouseFee.WeightFrom = Convert.ToDouble(reader["WeightFrom"]);

                if (reader["WeightTo"] != DBNull.Value)
                    entity.warehouseFee.WeightTo = Convert.ToDouble(reader["WeightTo"]);

                if (reader["Price"] != DBNull.Value)
                    entity.warehouseFee.Price = Convert.ToDouble(reader["Price"].ToString());

                if (reader["ShippingTypeToWareHouseID"] != DBNull.Value)
                    entity.warehouseFee.ShippingTypeToWareHouseID = reader["ShippingTypeToWareHouseID"].ToString().ToInt(0);

                if (reader["shipName"] != DBNull.Value)
                    entity.ShippingName = reader["shipName"].ToString();


                if (reader["IsHidden"] != DBNull.Value)
                    entity.IsHidden = Convert.ToBoolean(reader["IsHidden"]);

                if (reader["IsHelpMoving"] != DBNull.Value)
                    entity.warehouseFee.IsHelpMoving = Convert.ToBoolean(reader["IsHelpMoving"]);
                a.Add(entity);
            }
            reader.Close();
            return a;
        }
        public partial class WareHouseFeeTQVN
        {
            public tbl_WarehouseFee warehouseFee { get; set; }
            public string wareFrom { get; set; }
            public string wareTo { get; set; }
            public string ShippingName { get; set; }
            public bool IsHidden { get; set; }
            public WareHouseFeeTQVN()
            {
                warehouseFee = new tbl_WarehouseFee();
            }
        }
        #endregion
    }
}